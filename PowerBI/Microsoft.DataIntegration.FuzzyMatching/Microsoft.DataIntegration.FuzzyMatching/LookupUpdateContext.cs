using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200004F RID: 79
	public class LookupUpdateContext
	{
		// Token: 0x060002CE RID: 718 RVA: 0x0000E214 File Offset: 0x0000C414
		public LookupUpdateContext()
		{
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000E254 File Offset: 0x0000C454
		public LookupUpdateContext(IDomainManager domainManager, ITokenIdProvider tokenIdProvider, RecordBinding binding, IEnumerable<string> domains, IEnumerable<string> exactMatchDomains, JoinSide joinSide)
		{
			this.Initialize(domainManager, tokenIdProvider, binding, domains, exactMatchDomains, joinSide);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000E2B0 File Offset: 0x0000C4B0
		public void Initialize(IDomainManager domainManager, ITokenIdProvider tokenIdProvider, RecordBinding binding, IEnumerable<string> domains, IEnumerable<string> exactMatchDomains, JoinSide joinSide)
		{
			this.m_tokenIdProvider = tokenIdProvider;
			this.m_domainManager = domainManager;
			this.RecordContext = new RecordContext();
			this.RecordContext.TokenSequence = default(WeightedTokenSequence);
			this.RecordContext.ExactMatchTokenSequence = default(TokenSequence);
			this.RecordContext.TransformationMatchList = new ArraySegmentBuilder<WeightedTransformationMatch>();
			this.m_comparisonProviderInfo = new ComparisonProviderInfo(domainManager, tokenIdProvider, binding, domains, exactMatchDomains, joinSide);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000E31C File Offset: 0x0000C51C
		public void Reset()
		{
			this.m_comparisonProviderInfo.ResetSession();
			this.RecordContext.Reset();
			this.m_intAllocator.Reset();
			this.m_tokenWeightAllocator.Reset();
			this.m_tokenIdSegmentBuilder.Reset();
			this.m_tokenWeightSegmentBuilder.Reset();
			this.m_tranMatchAllocator.Reset();
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000E378 File Offset: 0x0000C578
		public void TokenizeAndRuleMatch(IDataRecord record)
		{
			this.TokenizeAndWeigh(this.m_comparisonProviderInfo, this.RecordContext, record, true);
			this.TokenizeExactMatchColumns(this.m_comparisonProviderInfo, record, out this.RecordContext.ExactMatchTokenSequence);
			this.RuleMatch(this.m_comparisonProviderInfo, this.RecordContext, true);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000E3C4 File Offset: 0x0000C5C4
		public void TokenizeAndRuleMatch(IDataRecord record, Stopwatch tokenizationTime, Stopwatch transformationMatchTime)
		{
			tokenizationTime.Start();
			this.TokenizeAndWeigh(this.m_comparisonProviderInfo, this.RecordContext, record, true);
			this.TokenizeExactMatchColumns(this.m_comparisonProviderInfo, record, out this.RecordContext.ExactMatchTokenSequence);
			tokenizationTime.Stop();
			transformationMatchTime.Start();
			this.RuleMatch(this.m_comparisonProviderInfo, this.RecordContext, true);
			transformationMatchTime.Stop();
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000E428 File Offset: 0x0000C628
		internal void RuleMatch(ComparisonProviderInfo cpi, RecordContext rc, bool weigh)
		{
			rc.TransformationMatchList.Reset();
			foreach (DomainProviderInfo domainProviderInfo in cpi.m_domainProviderInfo)
			{
				ArraySegment<TransformationMatch> arraySegment;
				domainProviderInfo.TransformationProvider.Match(domainProviderInfo.TransformationProviderSession, cpi.TokenIdProvider, rc.TokenSequence.Tokens, out arraySegment);
				if (weigh)
				{
					LookupUpdateContext.WeighTransformationMatchList(cpi.TokenIdProvider, domainProviderInfo.TokenWeightProvider, this.m_tranMatchAllocator, this.m_intAllocator, arraySegment, rc.TransformationMatchList);
				}
				else
				{
					foreach (TransformationMatch transformationMatch in arraySegment)
					{
						rc.TransformationMatchList.Add(new WeightedTransformationMatch
						{
							Position = transformationMatch.Position,
							Transformation = new WeightedTransformation
							{
								Transformation = transformationMatch.Transformation
							}
						});
					}
				}
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000E550 File Offset: 0x0000C750
		public static void WeighTransformationMatchList(ITokenIdProvider tokenIdProvider, ITokenWeightProvider weightProvider, ISegmentAllocator<WeightedTransformationMatch> tranMatchAllocator, ISegmentAllocator<int> segmentAllocator, ArraySegment<TransformationMatch> matches, ArraySegmentBuilder<WeightedTransformationMatch> tml)
		{
			foreach (TransformationMatch transformationMatch in matches.AsSafeEnumerable<TransformationMatch>())
			{
				if (!tml.Contains(transformationMatch))
				{
					TokenSequence to = transformationMatch.Transformation.To;
					ArraySegment<int> arraySegment = segmentAllocator.New(to.Count);
					tml.Add(new WeightedTransformationMatch
					{
						Position = transformationMatch.Position,
						Transformation = new WeightedTransformation
						{
							From = transformationMatch.Transformation.From,
							To = transformationMatch.Transformation.To,
							Type = transformationMatch.Transformation.Type,
							Metadata = transformationMatch.Transformation.Metadata,
							ToWeights = arraySegment
						}
					});
					for (int i = 0; i < to.Count; i++)
					{
						int weight = weightProvider.GetWeight(tokenIdProvider, to[i]);
						arraySegment.Array[arraySegment.Offset + i] = weight;
					}
				}
			}
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000E680 File Offset: 0x0000C880
		internal void TokenizeExactMatchColumns(ComparisonProviderInfo cpi, IDataRecord r, out TokenSequence tokenSequence)
		{
			this.m_tokenIdSegmentBuilder.Reset();
			foreach (DomainProviderInfo domainProviderInfo in cpi.m_exactMatchDomainProviderInfo)
			{
				domainProviderInfo.TokenizerContext.Reset();
				foreach (StringExtent stringExtent in domainProviderInfo.Tokenizer.Tokenize(domainProviderInfo.TokenizerContext, r))
				{
					this.m_tokenIdSegmentBuilder.Add(cpi.TokenIdProvider.GetOrCreateTokenId(stringExtent, domainProviderInfo.DomainId));
				}
			}
			tokenSequence = this.m_tokenIdSegmentBuilder.ToSegment(this.m_intAllocator);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000E75C File Offset: 0x0000C95C
		internal void TokenizeAndWeigh(ComparisonProviderInfo cpi, RecordContext rc, IDataRecord r, bool weigh)
		{
			int num = 0;
			this.m_tokenIdSegmentBuilder.Reset();
			this.m_tokenWeightSegmentBuilder.Reset();
			foreach (DomainProviderInfo domainProviderInfo in cpi.m_domainProviderInfo)
			{
				domainProviderInfo.TokenizerContext.Reset();
				foreach (StringExtent stringExtent in domainProviderInfo.Tokenizer.Tokenize(domainProviderInfo.TokenizerContext, r))
				{
					this.m_tokenIdSegmentBuilder.Add(this.m_tokenIdProvider.GetOrCreateTokenId(stringExtent, domainProviderInfo.DomainId));
				}
				if (weigh)
				{
					for (int i = num; i < this.m_tokenIdSegmentBuilder.Count; i++)
					{
						this.m_tokenWeightSegmentBuilder.Add(domainProviderInfo.TokenWeightProvider.GetWeight(cpi.TokenIdProvider, this.m_tokenIdSegmentBuilder[i]));
					}
				}
				num = this.m_tokenIdSegmentBuilder.Count;
			}
			rc.TokenSequence.Tokens = this.m_tokenIdSegmentBuilder.ToSegment(this.m_intAllocator);
			rc.TokenSequence.Weights = this.m_tokenWeightSegmentBuilder.ToSegment(this.m_tokenWeightAllocator);
		}

		// Token: 0x040000E8 RID: 232
		internal ComparisonProviderInfo m_comparisonProviderInfo;

		// Token: 0x040000E9 RID: 233
		internal RecordContext RecordContext;

		// Token: 0x040000EA RID: 234
		internal IOneDimSignatureGenerator SignatureGenerator;

		// Token: 0x040000EB RID: 235
		internal IMultiDimSignatureGenerator MultiDimSignatureGenerator;

		// Token: 0x040000EC RID: 236
		private ITokenIdProvider m_tokenIdProvider;

		// Token: 0x040000ED RID: 237
		private IDomainManager m_domainManager;

		// Token: 0x040000EE RID: 238
		internal BlockedSegmentArray<int> m_intAllocator = new BlockedSegmentArray<int>();

		// Token: 0x040000EF RID: 239
		internal BlockedSegmentArray<int> m_tokenWeightAllocator = new BlockedSegmentArray<int>();

		// Token: 0x040000F0 RID: 240
		internal BlockedSegmentArray<WeightedTransformationMatch> m_tranMatchAllocator = new BlockedSegmentArray<WeightedTransformationMatch>();

		// Token: 0x040000F1 RID: 241
		private ArraySegmentBuilder<int> m_tokenIdSegmentBuilder = new ArraySegmentBuilder<int>();

		// Token: 0x040000F2 RID: 242
		private ArraySegmentBuilder<int> m_tokenWeightSegmentBuilder = new ArraySegmentBuilder<int>();
	}
}
