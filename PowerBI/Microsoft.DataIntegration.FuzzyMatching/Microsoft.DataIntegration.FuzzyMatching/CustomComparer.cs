using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200003D RID: 61
	[Serializable]
	public class CustomComparer : IFuzzyComparer, IThreshold
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000B18C File Offset: 0x0000938C
		// (set) Token: 0x06000246 RID: 582 RVA: 0x0000B194 File Offset: 0x00009394
		public double EditTransformationPenalty { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000B19D File Offset: 0x0000939D
		// (set) Token: 0x06000248 RID: 584 RVA: 0x0000B1A5 File Offset: 0x000093A5
		public double StaticTransformationPenalty { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000B1AE File Offset: 0x000093AE
		// (set) Token: 0x0600024A RID: 586 RVA: 0x0000B1B6 File Offset: 0x000093B6
		public double PrefixTransformationPenalty { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000B1BF File Offset: 0x000093BF
		// (set) Token: 0x0600024C RID: 588 RVA: 0x0000B1C7 File Offset: 0x000093C7
		public double FirstLetterMismatchPenalty { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000B1D0 File Offset: 0x000093D0
		// (set) Token: 0x0600024E RID: 590 RVA: 0x0000B1D8 File Offset: 0x000093D8
		public double ContainmentBias { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000B1E1 File Offset: 0x000093E1
		// (set) Token: 0x06000250 RID: 592 RVA: 0x0000B1E9 File Offset: 0x000093E9
		public double Threshold { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000B1F2 File Offset: 0x000093F2
		// (set) Token: 0x06000252 RID: 594 RVA: 0x0000B1FA File Offset: 0x000093FA
		public bool Symmetric { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000B203 File Offset: 0x00009403
		// (set) Token: 0x06000254 RID: 596 RVA: 0x0000B20B File Offset: 0x0000940B
		public double LeftEmptyRightEmptySimilarity { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000B214 File Offset: 0x00009414
		// (set) Token: 0x06000256 RID: 598 RVA: 0x0000B21C File Offset: 0x0000941C
		public double LeftNonEmptyRightEmptySimilarity { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000B225 File Offset: 0x00009425
		// (set) Token: 0x06000258 RID: 600 RVA: 0x0000B22D File Offset: 0x0000942D
		public double LeftEmptyRightNonEmptySimilarity { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000B236 File Offset: 0x00009436
		// (set) Token: 0x0600025A RID: 602 RVA: 0x0000B23E File Offset: 0x0000943E
		public RecordBinding LeftRecordBinding { get; private set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000B247 File Offset: 0x00009447
		// (set) Token: 0x0600025C RID: 604 RVA: 0x0000B24F File Offset: 0x0000944F
		public RecordBinding RightRecordBinding { get; private set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000B258 File Offset: 0x00009458
		// (set) Token: 0x0600025E RID: 606 RVA: 0x0000B260 File Offset: 0x00009460
		public string[] ComparisonDomains { get; private set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000B269 File Offset: 0x00009469
		// (set) Token: 0x06000260 RID: 608 RVA: 0x0000B271 File Offset: 0x00009471
		public string[] ExactMatchDomains { get; private set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000B27A File Offset: 0x0000947A
		public bool IsSymmetric
		{
			get
			{
				return this.Symmetric;
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000B284 File Offset: 0x00009484
		public CustomComparer()
		{
			this.ContainmentBias = 0.5;
			this.Threshold = 0.0;
			this.Symmetric = true;
			this.StaticTransformationPenalty = 0.05;
			this.EditTransformationPenalty = 1.0;
			this.PrefixTransformationPenalty = 0.3;
			this.FirstLetterMismatchPenalty = 3.0;
			this.LeftEmptyRightEmptySimilarity = 1.0;
			this.LeftNonEmptyRightEmptySimilarity = 0.0;
			this.LeftEmptyRightNonEmptySimilarity = 0.0;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000B325 File Offset: 0x00009525
		public void Initialize(RecordBinding leftRecordBinding, RecordBinding rightRecordBinding, IEnumerable<string> domains, IEnumerable<string> exactMatchDomains)
		{
			this.LeftRecordBinding = leftRecordBinding;
			this.RightRecordBinding = rightRecordBinding;
			this.ComparisonDomains = ((domains != null) ? Enumerable.ToArray<string>(domains) : new string[0]);
			this.ExactMatchDomains = ((exactMatchDomains != null) ? Enumerable.ToArray<string>(exactMatchDomains) : new string[0]);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000B368 File Offset: 0x00009568
		public ISession CreateSession(IDomainManager domainManager, ITokenIdProvider tokenIdProvider)
		{
			if (tokenIdProvider == null || domainManager == null)
			{
				throw new ArgumentNullException("Custom Comparer requires a valid ITokenIdProvider and IDomainManager.");
			}
			CustomComparer.Session session = new CustomComparer.Session();
			session.m_tokenIdProvider = tokenIdProvider;
			session.m_jaccardComparer1 = new JaccardComparer();
			session.m_jaccardComparer1.Initialize(this.LeftRecordBinding, this.RightRecordBinding, this.ComparisonDomains, this.ExactMatchDomains);
			session.m_jaccardComparer2 = new JaccardComparer();
			session.m_jaccardComparer2.Initialize(this.RightRecordBinding, this.LeftRecordBinding, this.ComparisonDomains, this.ExactMatchDomains);
			session.m_jaccardSession1 = session.m_jaccardComparer1.CreateSession(domainManager, tokenIdProvider);
			session.m_jaccardSession2 = session.m_jaccardComparer2.CreateSession(domainManager, tokenIdProvider);
			session.m_leftJaccardContainmentComparer1 = new LeftJaccardContainmentComparer();
			session.m_leftJaccardContainmentComparer1.Initialize(this.LeftRecordBinding, this.RightRecordBinding, this.ComparisonDomains, this.ExactMatchDomains);
			session.m_leftJaccardContainmentComparer2 = new LeftJaccardContainmentComparer();
			session.m_leftJaccardContainmentComparer2.Initialize(this.RightRecordBinding, this.LeftRecordBinding, this.ComparisonDomains, this.ExactMatchDomains);
			session.m_leftJaccardContainmentSession1 = session.m_leftJaccardContainmentComparer1.CreateSession(domainManager, tokenIdProvider);
			session.m_leftJaccardContainmentSession2 = session.m_leftJaccardContainmentComparer2.CreateSession(domainManager, tokenIdProvider);
			session.m_weightProviders = new ITokenWeightProvider[this.ComparisonDomains.Length];
			foreach (string text in this.ComparisonDomains)
			{
				int domainId = domainManager.GetDomainId(text);
				if (session.m_weightProviders.Length < domainId + 1)
				{
					Array.Resize<ITokenWeightProvider>(ref session.m_weightProviders, domainId + 1);
				}
				session.m_weightProviders[domainId] = domainManager.GetTokenWeightProvider(text);
			}
			return session;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000B4FA File Offset: 0x000096FA
		public virtual void ResetLeftRecord(ISession _session, RecordContext leftRecordContext)
		{
			((CustomComparer.Session)_session).m_leftRecordContext = leftRecordContext;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000B508 File Offset: 0x00009708
		public virtual void ResetRightRecord(ISession _session, RecordContext rightRecordContext)
		{
			((CustomComparer.Session)_session).m_rightRecordContext = rightRecordContext;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000B518 File Offset: 0x00009718
		public virtual bool Compare(ISession _session, IDataRecord leftRecord, IDataRecord rightRecord, out ComparisonResult result)
		{
			CustomComparer.Session session = (CustomComparer.Session)_session;
			session.Reset();
			if (this.ContainmentBias == 0.0)
			{
				return this.Compare(session, session.m_jaccardComparer1, session.m_jaccardSession1, session.m_jaccardComparer2, session.m_jaccardSession2, leftRecord, rightRecord, this.Threshold, out result);
			}
			if (this.ContainmentBias == 1.0)
			{
				return this.Compare(session, session.m_leftJaccardContainmentComparer1, session.m_leftJaccardContainmentSession1, session.m_leftJaccardContainmentComparer2, session.m_leftJaccardContainmentSession2, leftRecord, rightRecord, this.Threshold, out result);
			}
			Math.Max(0.0, (this.Threshold - (1.0 - this.ContainmentBias)) / this.ContainmentBias);
			ComparisonResult comparisonResult;
			if (!this.Compare(session, session.m_leftJaccardContainmentComparer1, session.m_leftJaccardContainmentSession1, session.m_leftJaccardContainmentComparer2, session.m_leftJaccardContainmentSession2, leftRecord, rightRecord, 0.0, out comparisonResult) || this.ContainmentBias * comparisonResult.Similarity + (1.0 - this.ContainmentBias) * 1.0 < this.Threshold)
			{
				result = ComparisonResult.Empty;
				return false;
			}
			Math.Max(0.0, (this.Threshold - this.ContainmentBias * comparisonResult.Similarity) / (1.0 - this.ContainmentBias));
			ComparisonResult comparisonResult2;
			bool flag = this.Compare(session, session.m_jaccardComparer1, session.m_jaccardSession1, session.m_jaccardComparer2, session.m_jaccardSession2, leftRecord, rightRecord, 0.0, out comparisonResult2);
			if (!flag || this.ContainmentBias * comparisonResult.Similarity + (1.0 - this.ContainmentBias) * comparisonResult2.Similarity < this.Threshold)
			{
				result = ComparisonResult.Empty;
				return false;
			}
			result = (flag ? comparisonResult2 : comparisonResult);
			result.Similarity = this.ContainmentBias * comparisonResult.Similarity + (1.0 - this.ContainmentBias) * comparisonResult2.Similarity;
			result.NumeratorWeight = this.ContainmentBias * comparisonResult.NumeratorWeight + (1.0 - this.ContainmentBias) * comparisonResult2.NumeratorWeight;
			result.DenominatorWeight = this.ContainmentBias * comparisonResult.DenominatorWeight + (1.0 - this.ContainmentBias) * comparisonResult2.DenominatorWeight;
			return result.Similarity >= this.Threshold;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000B77C File Offset: 0x0000997C
		public virtual bool Compare(ISession _session, out ComparisonResult result)
		{
			CustomComparer.Session session = (CustomComparer.Session)_session;
			session.Reset();
			if (this.ContainmentBias == 0.0)
			{
				return this.Compare(session, session.m_jaccardComparer1, session.m_jaccardSession1, session.m_jaccardComparer2, session.m_jaccardSession2, this.Threshold, out result);
			}
			if (this.ContainmentBias == 1.0)
			{
				return this.Compare(session, session.m_leftJaccardContainmentComparer1, session.m_leftJaccardContainmentSession1, session.m_leftJaccardContainmentComparer2, session.m_leftJaccardContainmentSession2, this.Threshold, out result);
			}
			double num = Math.Max(0.0, (this.Threshold - (1.0 - this.ContainmentBias)) / this.ContainmentBias);
			ComparisonResult comparisonResult;
			if (!this.Compare(session, session.m_leftJaccardContainmentComparer1, session.m_leftJaccardContainmentSession1, session.m_leftJaccardContainmentComparer2, session.m_leftJaccardContainmentSession2, num, out comparisonResult) || this.ContainmentBias * comparisonResult.Similarity + (1.0 - this.ContainmentBias) * 1.0 < this.Threshold)
			{
				result = ComparisonResult.Empty;
				return false;
			}
			num = Math.Max(0.0, (this.Threshold - this.ContainmentBias * comparisonResult.Similarity) / (1.0 - this.ContainmentBias));
			ComparisonResult comparisonResult2;
			bool flag = this.Compare(session, session.m_jaccardComparer1, session.m_jaccardSession1, session.m_jaccardComparer2, session.m_jaccardSession2, num, out comparisonResult2);
			if (!flag || this.ContainmentBias * comparisonResult.Similarity + (1.0 - this.ContainmentBias) * comparisonResult2.Similarity < this.Threshold)
			{
				result = ComparisonResult.Empty;
				return false;
			}
			result = (flag ? comparisonResult2 : comparisonResult);
			result.Similarity = this.ContainmentBias * comparisonResult.Similarity + (1.0 - this.ContainmentBias) * comparisonResult2.Similarity;
			result.NumeratorWeight = this.ContainmentBias * comparisonResult.NumeratorWeight + (1.0 - this.ContainmentBias) * comparisonResult2.NumeratorWeight;
			result.DenominatorWeight = this.ContainmentBias * comparisonResult.DenominatorWeight + (1.0 - this.ContainmentBias) * comparisonResult2.DenominatorWeight;
			return result.Similarity >= this.Threshold;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000B9C4 File Offset: 0x00009BC4
		protected virtual bool Compare(CustomComparer.Session session, FuzzyComparer comparer1, ISession comparerSession1, FuzzyComparer comparer2, ISession comparerSession2, double threshold, out ComparisonResult result)
		{
			comparer1.Threshold = threshold;
			comparer2.Threshold = threshold;
			comparer1.ResetLeftRecord(comparerSession1, session.m_leftRecordContext);
			comparer1.ResetRightRecord(comparerSession1, session.m_rightRecordContext);
			bool flag = comparer1.Compare(comparerSession1, out result);
			this.AdjustComparisonResult(session, comparer1, ref flag, ref result);
			if (this.Symmetric && !comparer1.IsSymmetric)
			{
				comparer2.ResetLeftRecord(comparerSession2, session.m_rightRecordContext);
				comparer2.ResetRightRecord(comparerSession2, session.m_leftRecordContext);
				ComparisonResult comparisonResult;
				bool flag2 = comparer2.Compare(comparerSession2, out comparisonResult);
				this.AdjustComparisonResult(session, comparer2, ref flag2, ref comparisonResult);
				if (flag2 && (!flag || comparisonResult.Similarity > result.Similarity))
				{
					result.Similarity = comparisonResult.Similarity;
					result.NumeratorWeight = comparisonResult.NumeratorWeight;
					result.DenominatorWeight = comparisonResult.DenominatorWeight;
					result.LeftTransformationsApplied = comparisonResult.RightTransformationsApplied;
					result.RightTransformationsApplied = comparisonResult.LeftTransformationsApplied;
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000BAC0 File Offset: 0x00009CC0
		protected virtual bool Compare(CustomComparer.Session session, FuzzyComparer comparer1, ISession comparerSession1, FuzzyComparer comparer2, ISession comparerSession2, IDataRecord leftRecord, IDataRecord rightRecord, double threshold, out ComparisonResult result)
		{
			comparer1.Threshold = threshold;
			comparer2.Threshold = threshold;
			bool flag = comparer1.Compare(comparerSession1, leftRecord, rightRecord, out result);
			this.AdjustComparisonResult(session, comparer1, ref flag, ref result);
			if (this.Symmetric && !comparer1.IsSymmetric)
			{
				ComparisonResult comparisonResult;
				bool flag2 = comparer2.Compare(comparerSession2, rightRecord, leftRecord, out comparisonResult);
				this.AdjustComparisonResult(session, comparer2, ref flag2, ref comparisonResult);
				if (flag2 && (!flag || comparisonResult.Similarity > result.Similarity))
				{
					result.Similarity = comparisonResult.Similarity;
					result.NumeratorWeight = comparisonResult.NumeratorWeight;
					result.DenominatorWeight = comparisonResult.DenominatorWeight;
					result.LeftTransformationsApplied = comparisonResult.RightTransformationsApplied;
					result.RightTransformationsApplied = comparisonResult.LeftTransformationsApplied;
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000BB88 File Offset: 0x00009D88
		protected void AdjustComparisonResult(CustomComparer.Session session, FuzzyComparer comparer, ref bool comparerResult, ref ComparisonResult result)
		{
			if (comparerResult && result.DenominatorWeight > 0.0 && (result.LeftTransformationsApplied.Count > 0 || result.RightTransformationsApplied.Count > 0))
			{
				result.DenominatorWeight += this.ComputeDenominatorPenalty(session, result.LeftTransformationsApplied, result.TotalLeftWeight, result.DenominatorWeight) + this.ComputeDenominatorPenalty(session, result.RightTransformationsApplied, result.TotalRightWeight, result.DenominatorWeight);
				result.Similarity = result.NumeratorWeight / result.DenominatorWeight;
			}
			if (result.LeftRecordContext.ExactMatchTokenSequence.Equals(result.RightRecordContext.ExactMatchTokenSequence))
			{
				if (result.LeftRecordContext.TokenSequence.Tokens.Count == 0 && result.RightRecordContext.TokenSequence.Tokens.Count == 0)
				{
					result.Similarity = this.LeftEmptyRightEmptySimilarity;
				}
				else if (result.LeftRecordContext.TokenSequence.Tokens.Count == 0)
				{
					result.Similarity = this.LeftNonEmptyRightEmptySimilarity;
				}
				else if (result.LeftRecordContext.TokenSequence.Tokens.Count == 0)
				{
					result.Similarity = this.LeftEmptyRightNonEmptySimilarity;
				}
			}
			comparerResult = result.Similarity >= comparer.Threshold;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000BD04 File Offset: 0x00009F04
		protected virtual double ComputeTransformationPenalty(CustomComparer.Session session, Transformation t, double weightOfRhs, double totalWeight, double originalDenominatorWeight)
		{
			double num = 0.0;
			if (weightOfRhs > 0.0)
			{
				switch (t.Type)
				{
				case TransformationType.StaticTransformation:
					num += weightOfRhs * this.StaticTransformationPenalty;
					break;
				case TransformationType.EditTransformation:
				{
					EditTransformationMetadata editTransformationMetadata = new EditTransformationMetadata(t.Metadata);
					double num2 = ((editTransformationMetadata.PrefixMatchLength < 1) ? this.FirstLetterMismatchPenalty : 0.0);
					double num3 = this.EditTransformationPenalty + num2;
					num += weightOfRhs * ((Math.Pow(2.0, (double)editTransformationMetadata.EditDistance * num3) - 1.0) / Math.Pow(2.0, num3));
					break;
				}
				case TransformationType.PrefixTransformation:
				{
					PrefixTransformationMetadata prefixTransformationMetadata = new PrefixTransformationMetadata(t.Metadata);
					double num4 = (double)(prefixTransformationMetadata.MaxLength - prefixTransformationMetadata.PrefixMatchLength) / (double)prefixTransformationMetadata.MaxLength * this.PrefixTransformationPenalty;
					num += weightOfRhs * num4 / (num4 + Math.Exp(-num4));
					break;
				}
				default:
					num += weightOfRhs * this.StaticTransformationPenalty;
					break;
				}
			}
			else
			{
				double num5 = 0.0;
				for (int i = 0; i < t.From.Count; i++)
				{
					int num6 = t.From[i];
					num5 += (double)session.m_weightProviders[session.m_tokenIdProvider.GetDomainId(num6)].GetWeight(session.m_tokenIdProvider, num6);
				}
				num += num5 / totalWeight * originalDenominatorWeight * this.StaticTransformationPenalty;
			}
			return num;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000BE8C File Offset: 0x0000A08C
		protected virtual double ComputeDenominatorPenalty(CustomComparer.Session session, IList<TransformationMatch> transformsApplied, double totalWeight, double originalDenominatorWeight)
		{
			double num = 0.0;
			for (int i = 0; i < transformsApplied.Count; i++)
			{
				TransformationMatch transformationMatch = transformsApplied[i];
				double num2 = 0.0;
				for (int j = 0; j < transformationMatch.Transformation.To.Count; j++)
				{
					int num3 = transformationMatch.Transformation.To[j];
					num2 += (double)session.m_weightProviders[session.m_tokenIdProvider.GetDomainId(num3)].GetWeight(session.m_tokenIdProvider, num3);
				}
				num += this.ComputeTransformationPenalty(session, transformationMatch.Transformation, num2, totalWeight, originalDenominatorWeight);
			}
			return num;
		}

		// Token: 0x0200012C RID: 300
		protected class Session : ISession
		{
			// Token: 0x06000BED RID: 3053 RVA: 0x00033AC0 File Offset: 0x00031CC0
			public ComparisonResult NewComparisonResult()
			{
				if (this.m_nextComparisonResult == this.m_comparisonResults.Count)
				{
					this.m_comparisonResults.Add(new ComparisonResult());
				}
				List<ComparisonResult> comparisonResults = this.m_comparisonResults;
				int nextComparisonResult = this.m_nextComparisonResult;
				this.m_nextComparisonResult = nextComparisonResult + 1;
				ComparisonResult comparisonResult = comparisonResults[nextComparisonResult];
				comparisonResult.Reset();
				return comparisonResult;
			}

			// Token: 0x06000BEE RID: 3054 RVA: 0x00033B12 File Offset: 0x00031D12
			public void Reset()
			{
				this.m_jaccardSession1.Reset();
				this.m_jaccardSession2.Reset();
				this.m_leftJaccardContainmentSession1.Reset();
				this.m_leftJaccardContainmentSession2.Reset();
				this.m_nextComparisonResult = 0;
			}

			// Token: 0x04000496 RID: 1174
			public ITokenIdProvider m_tokenIdProvider;

			// Token: 0x04000497 RID: 1175
			public FuzzyComparer m_jaccardComparer1;

			// Token: 0x04000498 RID: 1176
			public FuzzyComparer m_jaccardComparer2;

			// Token: 0x04000499 RID: 1177
			public FuzzyComparer m_leftJaccardContainmentComparer1;

			// Token: 0x0400049A RID: 1178
			public FuzzyComparer m_leftJaccardContainmentComparer2;

			// Token: 0x0400049B RID: 1179
			public ITokenWeightProvider[] m_weightProviders;

			// Token: 0x0400049C RID: 1180
			public RecordContext m_leftRecordContext;

			// Token: 0x0400049D RID: 1181
			public RecordContext m_rightRecordContext;

			// Token: 0x0400049E RID: 1182
			public ISession m_jaccardSession1;

			// Token: 0x0400049F RID: 1183
			public ISession m_jaccardSession2;

			// Token: 0x040004A0 RID: 1184
			public ISession m_leftJaccardContainmentSession1;

			// Token: 0x040004A1 RID: 1185
			public ISession m_leftJaccardContainmentSession2;

			// Token: 0x040004A2 RID: 1186
			private List<ComparisonResult> m_comparisonResults = new List<ComparisonResult>();

			// Token: 0x040004A3 RID: 1187
			private int m_nextComparisonResult;
		}
	}
}
