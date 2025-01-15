using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000108 RID: 264
	[Serializable]
	public class TokenMergeTransformationProvider : ITransformationProvider, IPairSpecificTransformationProvider, IProviderInitialize, IRowsetConsumer, ISessionable
	{
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x000309BE File Offset: 0x0002EBBE
		// (set) Token: 0x06000AE7 RID: 2791 RVA: 0x000309C6 File Offset: 0x0002EBC6
		public string DomainName { get; set; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x000309CF File Offset: 0x0002EBCF
		// (set) Token: 0x06000AE9 RID: 2793 RVA: 0x000309D7 File Offset: 0x0002EBD7
		public string ReferenceRowsetName { get; set; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x000309E0 File Offset: 0x0002EBE0
		// (set) Token: 0x06000AEB RID: 2795 RVA: 0x000309E8 File Offset: 0x0002EBE8
		public JoinSide PairSpecificSide { get; set; }

		// Token: 0x06000AEC RID: 2796 RVA: 0x000309F4 File Offset: 0x0002EBF4
		public TokenMergeTransformationProvider()
		{
			this.ReferenceRowsetName = "default";
			this.m_referenceRowsetSink = new TokenMergeTransformationProvider.ReferenceRowsetSink
			{
				Name = "ReferenceRowset",
				Provider = this
			};
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00030A41 File Offset: 0x0002EC41
		public void Initialize(IDomainManager domainManager, string domainName)
		{
			this.DomainName = domainName;
			this.m_domainId = domainManager.GetDomainId(domainName);
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x00030A57 File Offset: 0x0002EC57
		public IList<IRowsetSink> RowsetSinks
		{
			get
			{
				return new IRowsetSink[] { this.m_referenceRowsetSink };
			}
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x00030A68 File Offset: 0x0002EC68
		void IRowsetConsumer.RequestRowsets(IRowsetDistributor rowsetDistributor)
		{
			if (!string.IsNullOrEmpty(this.ReferenceRowsetName))
			{
				rowsetDistributor.RequestRowset(this.ReferenceRowsetName, this.m_referenceRowsetSink);
			}
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00030A89 File Offset: 0x0002EC89
		public void Clear()
		{
			this.m_ahoCorasick = new AhoCorasick();
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00030A96 File Offset: 0x0002EC96
		public ISession CreateSession()
		{
			return new TokenMergeTransformationProvider.Session
			{
				m_ahoCorasickSession = this.m_ahoCorasick.CreateSession()
			};
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00030AB0 File Offset: 0x0002ECB0
		public void Match(ISession _session, ITokenIdProvider tokenIdProvider, IDataRecord leftRecord, IDataRecord rightRecord, RecordBinding leftBinding, RecordBinding rightBinding, TokenSequence leftTokenSeq, TokenSequence rightTokenSeq, out ArraySegment<TransformationMatch> leftTransformationMatchList, out ArraySegment<TransformationMatch> rightTransformationMatchList)
		{
			TokenMergeTransformationProvider.Session session = _session as TokenMergeTransformationProvider.Session;
			session.PairSpecificReset();
			if (JoinSide.Left == this.PairSpecificSide || JoinSide.Both == this.PairSpecificSide)
			{
				this.GeneratePairSpecificTransformations(session, tokenIdProvider, leftTokenSeq, rightTokenSeq, session.m_ps_leftTranMatchListBuilder, out leftTransformationMatchList);
			}
			else
			{
				leftTransformationMatchList = default(ArraySegment<TransformationMatch>);
			}
			if (JoinSide.Right == this.PairSpecificSide || JoinSide.Both == this.PairSpecificSide)
			{
				this.GeneratePairSpecificTransformations(session, tokenIdProvider, rightTokenSeq, leftTokenSeq, session.m_ps_rightTranMatchListBuilder, out rightTransformationMatchList);
				return;
			}
			rightTransformationMatchList = default(ArraySegment<TransformationMatch>);
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00030B29 File Offset: 0x0002ED29
		private static int CompareTokenIdPair(TokenMergeTransformationProvider.TokenIdPair tip1, TokenMergeTransformationProvider.TokenIdPair tip2)
		{
			return tip1.Token.CompareTo(tip2.Token);
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00030B3C File Offset: 0x0002ED3C
		private void GeneratePairSpecificTransformations(TokenMergeTransformationProvider.Session session, ITokenIdProvider tokenIdProvider, TokenSequence tokenSeqFrom, TokenSequence tokenSeqTo, ArraySegmentBuilder<TransformationMatch> tranMatchListBuilder, out ArraySegment<TransformationMatch> transformationMatchList)
		{
			AhoCorasick ahoCorasick = new AhoCorasick();
			ahoCorasick.BeginUpdate();
			Enumerable.Select<int, TokenMergeTransformationProvider.TokenIdPair>(Enumerable.Distinct<int>(Enumerable.Where<int>(tokenSeqTo, (int id) => tokenIdProvider.GetDomainId(id) == this.m_domainId)), (int id) => new TokenMergeTransformationProvider.TokenIdPair(id, tokenIdProvider.GetToken(id))).OrderByDescending(new Func<TokenMergeTransformationProvider.TokenIdPair, TokenMergeTransformationProvider.TokenIdPair, int>(TokenMergeTransformationProvider.CompareTokenIdPair)).ForEach(delegate(TokenMergeTransformationProvider.TokenIdPair p)
			{
				ahoCorasick.Add<StringExtent>(p.Token, p.Id);
			});
			ahoCorasick.EndUpdate();
			this.GenerateTransformations(session, tokenIdProvider, tokenSeqFrom, ahoCorasick, ahoCorasick.CreateSession(), session.m_ps_tokenIdSegmentBuilder, session.m_ps_tokenIdSegmentBuilder2, session.m_ps_intAllocator, tranMatchListBuilder, out transformationMatchList);
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00030C04 File Offset: 0x0002EE04
		private void GenerateTransformations(TokenMergeTransformationProvider.Session session, ITokenIdProvider tokenIdProvider, TokenSequence tokenSeqFrom, AhoCorasick ahoCorasick, ISession ahoCorasickSession, ArraySegmentBuilder<int> tokenIdSegmentBuilder, ArraySegmentBuilder<int> tokenIdSegmentBuilder2, BlockedSegmentArray<int> intAllocator, ArraySegmentBuilder<TransformationMatch> tranMatchListBuilder, out ArraySegment<TransformationMatch> transformationMatchList)
		{
			StringExtent concatenatedString = session.GetConcatenatedString(tokenIdProvider, tokenSeqFrom, this.m_domainId);
			ahoCorasick.ResetLookup<StringExtent>(ahoCorasickSession, concatenatedString);
			while (ahoCorasick.GetNextMatch(ahoCorasickSession))
			{
				int curMatchPos = ahoCorasick.GetCurMatchPos(ahoCorasickSession);
				int curMatchLen = ahoCorasick.GetCurMatchLen(ahoCorasickSession);
				if (session.m_wordBoundary[curMatchPos] != 0 && session.m_wordBoundary[curMatchPos + curMatchLen] > session.m_wordBoundary[curMatchPos] + 1)
				{
					tokenIdSegmentBuilder.Reset();
					tokenIdSegmentBuilder.Add(ahoCorasick.GetCurMatch(ahoCorasickSession));
					tokenIdSegmentBuilder2.Reset();
					for (int i = session.m_wordBoundary[curMatchPos]; i < session.m_wordBoundary[curMatchPos + curMatchLen]; i++)
					{
						tokenIdSegmentBuilder2.Add(tokenSeqFrom[i - 1]);
					}
					ArraySegment<int> arraySegment = tokenIdSegmentBuilder.ToSegment(intAllocator);
					ArraySegment<int> arraySegment2 = tokenIdSegmentBuilder2.ToSegment(intAllocator);
					tranMatchListBuilder.Add(new TransformationMatch
					{
						Position = session.m_wordBoundary[curMatchPos] - 1,
						Transformation = new Transformation(arraySegment2, arraySegment, TransformationType.TokenMerge)
					});
				}
			}
			for (int j = 0; j < concatenatedString.Length + 1; j++)
			{
				session.m_wordBoundary[j] = 0;
			}
			transformationMatchList = tranMatchListBuilder;
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x00030D48 File Offset: 0x0002EF48
		public void Match(ISession session, ITokenIdProvider tokenIdProvider, TokenSequence tokenSeq, out ArraySegment<TransformationMatch> transformationMatchList)
		{
			TokenMergeTransformationProvider.Session session2 = (TokenMergeTransformationProvider.Session)session;
			session.Reset();
			lock (this)
			{
				if (this.m_ahoCorasick != null)
				{
					this.GenerateTransformations(session2, tokenIdProvider, tokenSeq, this.m_ahoCorasick, session2.m_ahoCorasickSession, session2.m_tokenIdSegmentBuilder, session2.m_tokenIdSegmentBuilder2, session2.m_intAllocator, session2.m_tranMatchListBuilder, out transformationMatchList);
				}
				else
				{
					transformationMatchList = session2.m_tranMatchListBuilder;
				}
			}
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x00030DD0 File Offset: 0x0002EFD0
		public IEnumerable<Transformation> Transformations(ITokenIdProvider tokenIdProvider)
		{
			yield break;
		}

		// Token: 0x04000421 RID: 1057
		private AhoCorasick m_ahoCorasick = new AhoCorasick();

		// Token: 0x04000422 RID: 1058
		private int m_domainId = -1;

		// Token: 0x04000426 RID: 1062
		private TokenMergeTransformationProvider.ReferenceRowsetSink m_referenceRowsetSink;

		// Token: 0x020001A5 RID: 421
		[Serializable]
		private class TokenIdPair
		{
			// Token: 0x06000DDF RID: 3551 RVA: 0x0003B364 File Offset: 0x00039564
			public TokenIdPair(int i, StringExtent s)
			{
				this.Id = i;
				this.Token = s;
			}

			// Token: 0x040006EE RID: 1774
			public int Id;

			// Token: 0x040006EF RID: 1775
			public StringExtent Token;
		}

		// Token: 0x020001A6 RID: 422
		private class Session : ISession
		{
			// Token: 0x06000DE0 RID: 3552 RVA: 0x0003B37C File Offset: 0x0003957C
			public Session()
			{
				this.m_wordBoundary = new int[1];
				this.m_wordBoundary[0] = -1;
				this.m_concatStrBuf = new char[0];
				this.m_concatStr = new StringExtent(this.m_concatStrBuf);
			}

			// Token: 0x06000DE1 RID: 3553 RVA: 0x0003B448 File Offset: 0x00039648
			public void Reset()
			{
				this.m_tokenIdSegmentBuilder.Reset();
				this.m_tokenIdSegmentBuilder2.Reset();
				this.m_intAllocator.Reset();
				this.m_tranMatchAllocator.Reset();
				this.m_tranMatchListBuilder.Reset();
				this.m_ahoCorasickSession.Reset();
			}

			// Token: 0x06000DE2 RID: 3554 RVA: 0x0003B498 File Offset: 0x00039698
			public void PairSpecificReset()
			{
				this.m_ps_charSegmentAllocator.Reset();
				this.m_ps_tokenIdSegmentBuilder.Reset();
				this.m_ps_tokenIdSegmentBuilder2.Reset();
				this.m_ps_intAllocator.Reset();
				this.m_ps_tranMatchAllocator.Reset();
				this.m_ps_leftTranMatchListBuilder.Reset();
				this.m_ps_rightTranMatchListBuilder.Reset();
			}

			// Token: 0x06000DE3 RID: 3555 RVA: 0x0003B4F4 File Offset: 0x000396F4
			public StringExtent GetConcatenatedString(ITokenIdProvider tokenIdProvider, TokenSequence tokSeq, int domainId)
			{
				int num = 0;
				int num2 = 0;
				for (int i = 0; i < tokSeq.Count; i++)
				{
					if (tokenIdProvider.GetDomainId(tokSeq[i]) == domainId)
					{
						StringExtent token = tokenIdProvider.GetToken(tokSeq[i]);
						if (num + token.Length > this.m_concatStrBuf.Length)
						{
							Array.Resize<char>(ref this.m_concatStrBuf, num + token.Length);
							Array.Resize<int>(ref this.m_wordBoundary, this.m_concatStrBuf.Length + 1);
							this.m_concatStr.Array = this.m_concatStrBuf;
						}
						this.m_wordBoundary[num] = i + 1;
						Array.Copy(token.Array, token.Offset, this.m_concatStrBuf, num, token.Length);
						num += token.Length;
						num2++;
					}
				}
				this.m_concatStr.Length = num;
				this.m_wordBoundary[num] = num2 + 1;
				return this.m_concatStr;
			}

			// Token: 0x040006F0 RID: 1776
			public ArraySegmentBuilder<int> m_tokenIdSegmentBuilder = new ArraySegmentBuilder<int>();

			// Token: 0x040006F1 RID: 1777
			public ArraySegmentBuilder<int> m_tokenIdSegmentBuilder2 = new ArraySegmentBuilder<int>();

			// Token: 0x040006F2 RID: 1778
			public ArraySegmentBuilder<TransformationMatch> m_tranMatchListBuilder = new ArraySegmentBuilder<TransformationMatch>();

			// Token: 0x040006F3 RID: 1779
			public BlockedSegmentArray<int> m_intAllocator = new BlockedSegmentArray<int>();

			// Token: 0x040006F4 RID: 1780
			public BlockedSegmentArray<TransformationMatch> m_tranMatchAllocator = new BlockedSegmentArray<TransformationMatch>();

			// Token: 0x040006F5 RID: 1781
			public ISession m_ahoCorasickSession;

			// Token: 0x040006F6 RID: 1782
			public int[] m_wordBoundary;

			// Token: 0x040006F7 RID: 1783
			public char[] m_concatStrBuf;

			// Token: 0x040006F8 RID: 1784
			public StringExtent m_concatStr;

			// Token: 0x040006F9 RID: 1785
			public BlockedSegmentArray<char> m_ps_charSegmentAllocator = new BlockedSegmentArray<char>();

			// Token: 0x040006FA RID: 1786
			public ArraySegmentBuilder<int> m_ps_tokenIdSegmentBuilder = new ArraySegmentBuilder<int>();

			// Token: 0x040006FB RID: 1787
			public ArraySegmentBuilder<int> m_ps_tokenIdSegmentBuilder2 = new ArraySegmentBuilder<int>();

			// Token: 0x040006FC RID: 1788
			public BlockedSegmentArray<int> m_ps_intAllocator = new BlockedSegmentArray<int>();

			// Token: 0x040006FD RID: 1789
			public ArraySegmentBuilder<TransformationMatch> m_ps_leftTranMatchListBuilder = new ArraySegmentBuilder<TransformationMatch>();

			// Token: 0x040006FE RID: 1790
			public ArraySegmentBuilder<TransformationMatch> m_ps_rightTranMatchListBuilder = new ArraySegmentBuilder<TransformationMatch>();

			// Token: 0x040006FF RID: 1791
			public BlockedSegmentArray<TransformationMatch> m_ps_tranMatchAllocator = new BlockedSegmentArray<TransformationMatch>();
		}

		// Token: 0x020001A7 RID: 423
		[Serializable]
		private class ReferenceRowsetSink : RowsetSinkBase
		{
			// Token: 0x17000288 RID: 648
			// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x0003B5E4 File Offset: 0x000397E4
			// (set) Token: 0x06000DE5 RID: 3557 RVA: 0x0003B5EC File Offset: 0x000397EC
			public TokenMergeTransformationProvider Provider { get; set; }

			// Token: 0x06000DE6 RID: 3558 RVA: 0x0003B5F5 File Offset: 0x000397F5
			public override IUpdateContext BeginUpdate(DataTable schemaTable)
			{
				return new TokenMergeTransformationProvider.ReferenceRowsetSink.UpdateContext
				{
					DomainName = this.Provider.DomainName
				};
			}

			// Token: 0x06000DE7 RID: 3559 RVA: 0x0003B610 File Offset: 0x00039810
			public override void Add(IUpdateContext _context, TokenSequence tokenSeq)
			{
				TokenMergeTransformationProvider.ReferenceRowsetSink.UpdateContext updateContext = (TokenMergeTransformationProvider.ReferenceRowsetSink.UpdateContext)_context;
				for (int i = 0; i < tokenSeq.Count; i++)
				{
					if (!updateContext.m_dictionaryTokenIds.Contains(tokenSeq[i]) && updateContext.TokenIdProvider.GetDomainId(tokenSeq[i]) == updateContext.DomainId)
					{
						updateContext.m_dictionaryTokenIds.Add(tokenSeq[i]);
						updateContext.m_dictionaryTokens.Add(new TokenMergeTransformationProvider.TokenIdPair(tokenSeq[i], updateContext.TokenIdProvider.GetToken(tokenSeq[i]).AllocClone(updateContext.m_charSegmentAllocator)));
					}
				}
			}

			// Token: 0x06000DE8 RID: 3560 RVA: 0x0003B6B8 File Offset: 0x000398B8
			public override void EndUpdate(IUpdateContext _context)
			{
				TokenMergeTransformationProvider.ReferenceRowsetSink.UpdateContext updateContext = (TokenMergeTransformationProvider.ReferenceRowsetSink.UpdateContext)_context;
				updateContext.m_dictionaryTokens.Sort(new Comparison<TokenMergeTransformationProvider.TokenIdPair>(TokenMergeTransformationProvider.ReferenceRowsetSink.CompareTokenIdPair));
				AhoCorasick ahoCorasick = new AhoCorasick();
				ahoCorasick.BeginUpdate();
				for (int i = updateContext.m_dictionaryTokens.Count - 1; i >= 0; i--)
				{
					ahoCorasick.Add<StringExtent>(updateContext.m_dictionaryTokens[i].Token, updateContext.m_dictionaryTokens[i].Id);
				}
				ahoCorasick.EndUpdate();
				updateContext.Dispose();
				lock (this)
				{
					this.Provider.m_ahoCorasick = ahoCorasick;
				}
			}

			// Token: 0x06000DE9 RID: 3561 RVA: 0x0003B768 File Offset: 0x00039968
			private static int CompareTokenIdPair(TokenMergeTransformationProvider.TokenIdPair tip1, TokenMergeTransformationProvider.TokenIdPair tip2)
			{
				return tip1.Token.CompareTo(tip2.Token);
			}

			// Token: 0x020001C5 RID: 453
			private class UpdateContext : RowsetSinkBase.UpdateContextBase
			{
				// Token: 0x06000E65 RID: 3685 RVA: 0x0003D2AF File Offset: 0x0003B4AF
				public override void Dispose()
				{
					this.m_dictionaryTokens = null;
					this.m_dictionaryTokenPairHashes = null;
					this.m_dictionaryTokenIds = null;
					base.Dispose();
				}

				// Token: 0x06000E66 RID: 3686 RVA: 0x0003D2CC File Offset: 0x0003B4CC
				public override void Initialize(IRowsetManager rowsetManager, IDomainManager domainManager, ITokenIdProvider tokenIdProvider, RecordBinding recordBinding)
				{
					base.Initialize(rowsetManager, domainManager, tokenIdProvider, recordBinding);
					this.m_dictionaryTokenIds = new FastIntHashSet();
					this.m_dictionaryTokenPairHashes = new FastIntHashSet();
					this.m_numDistTokenPairHashes = 0;
					this.m_dictionaryTokens = new List<TokenMergeTransformationProvider.TokenIdPair>();
				}

				// Token: 0x0400077D RID: 1917
				public List<TokenMergeTransformationProvider.TokenIdPair> m_dictionaryTokens;

				// Token: 0x0400077E RID: 1918
				public FastIntHashSet m_dictionaryTokenPairHashes;

				// Token: 0x0400077F RID: 1919
				public int m_numDistTokenPairHashes;

				// Token: 0x04000780 RID: 1920
				public FastIntHashSet m_dictionaryTokenIds;
			}
		}
	}
}
