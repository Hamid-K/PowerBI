using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000BA RID: 186
	[Serializable]
	public class FuzzyComparer : IFuzzyComparer, IThreshold
	{
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x0001F50E File Offset: 0x0001D70E
		// (set) Token: 0x06000702 RID: 1794 RVA: 0x0001F516 File Offset: 0x0001D716
		public FuzzyComparisonType ComparisonType { get; private set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x0001F51F File Offset: 0x0001D71F
		// (set) Token: 0x06000704 RID: 1796 RVA: 0x0001F527 File Offset: 0x0001D727
		public RecordBinding LeftDomainBinding { get; private set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x0001F530 File Offset: 0x0001D730
		// (set) Token: 0x06000706 RID: 1798 RVA: 0x0001F538 File Offset: 0x0001D738
		public RecordBinding RightDomainBinding { get; private set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x0001F541 File Offset: 0x0001D741
		// (set) Token: 0x06000708 RID: 1800 RVA: 0x0001F549 File Offset: 0x0001D749
		public string[] ComparisonDomains { get; private set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x0001F552 File Offset: 0x0001D752
		// (set) Token: 0x0600070A RID: 1802 RVA: 0x0001F55A File Offset: 0x0001D75A
		public string[] ExactMatchDomains { get; private set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x0001F563 File Offset: 0x0001D763
		public bool IsSymmetric
		{
			get
			{
				return this.ComparisonType == FuzzyComparisonType.Jaccard;
			}
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001F56E File Offset: 0x0001D76E
		public FuzzyComparer(FuzzyComparisonType comparisonType)
		{
			this.ComparisonType = comparisonType;
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0001F580 File Offset: 0x0001D780
		public void Initialize(RecordBinding leftRecordBinding, RecordBinding rightRecordBinding, IEnumerable<string> domains, IEnumerable<string> exactMatchDomains)
		{
			this.LeftDomainBinding = leftRecordBinding;
			this.RightDomainBinding = rightRecordBinding;
			if (domains == null)
			{
				domains = new string[0];
			}
			if (exactMatchDomains == null)
			{
				exactMatchDomains = new string[0];
			}
			this.ComparisonDomains = new List<string>(domains).ToArray();
			this.ExactMatchDomains = new List<string>(exactMatchDomains).ToArray();
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0001F5D5 File Offset: 0x0001D7D5
		public ISession CreateSession(IDomainManager domainManager, ITokenIdProvider tokenIdProvider)
		{
			return new FuzzyComparer.Session(domainManager, tokenIdProvider, this.LeftDomainBinding, this.RightDomainBinding, this.ComparisonDomains, this.ExactMatchDomains);
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0001F5F6 File Offset: 0x0001D7F6
		private bool ExactMatchDomainsAreEqual(RecordContext leftContext, RecordContext rightContext)
		{
			return leftContext.ExactMatchTokenSequence.Equals(rightContext.ExactMatchTokenSequence);
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0001F609 File Offset: 0x0001D809
		private bool CheckSimilarity(FuzzyComparer.Session session, double threshold)
		{
			return this.CheckSimilarity(session, threshold, session.LeftRecordContext, session.RightRecordContext);
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0001F61F File Offset: 0x0001D81F
		private bool CheckSimilarity(FuzzyComparer.Session session, double threshold, RecordContext leftContext, RecordContext rightContext)
		{
			return (this.ExactMatchDomains == null || this.ExactMatchDomains.Length == 0 || this.ExactMatchDomainsAreEqual(leftContext, rightContext)) && session.m_wtJaccSim.CheckSimilarity(threshold);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0001F64C File Offset: 0x0001D84C
		private void ComputeSimilarity(FuzzyComparer.Session session, double lowerBound, double upperBound, ComparisonResult result)
		{
			result.Reset();
			result.LeftRecordContext = session.LeftRecordContext;
			result.RightRecordContext = session.RightRecordContext;
			result.LeftTransformationsApplied = session.LeftTranMatchListBuilder;
			result.RightTransformationsApplied = session.RightTranMatchListBuilder;
			session.m_wtJaccSim.Similarity(lowerBound, upperBound, result);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0001F6A3 File Offset: 0x0001D8A3
		public void ResetLeftRecord(ISession _session, RecordContext leftRecordContext)
		{
			FuzzyComparer.Session session = (FuzzyComparer.Session)_session;
			session.LeftRecordContext = leftRecordContext;
			session.m_wtJaccSim.ResetLeftRecord(leftRecordContext.TokenSequence, leftRecordContext.TransformationMatchList, true);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0001F6C9 File Offset: 0x0001D8C9
		public void ResetRightRecord(ISession _session, RecordContext rightRecordContext)
		{
			FuzzyComparer.Session session = (FuzzyComparer.Session)_session;
			session.RightRecordContext = rightRecordContext;
			session.m_wtJaccSim.ResetRightRecord(rightRecordContext.TokenSequence, rightRecordContext.TransformationMatchList, this.ComparisonType, true);
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x0001F6F5 File Offset: 0x0001D8F5
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x0001F6FD File Offset: 0x0001D8FD
		public double Threshold { get; set; }

		// Token: 0x06000717 RID: 1815 RVA: 0x0001F708 File Offset: 0x0001D908
		public bool Compare(ISession _session, out ComparisonResult result)
		{
			FuzzyComparer.Session session = (FuzzyComparer.Session)_session;
			result = session.ComparisonResult;
			result.Reset();
			result.LeftRecordContext = session.LeftRecordContext;
			result.RightRecordContext = session.RightRecordContext;
			result.LeftTransformationsApplied = session.LeftTranMatchListBuilder;
			result.RightTransformationsApplied = session.RightTranMatchListBuilder;
			bool flag = this.CheckSimilarity(session, this.Threshold);
			if (flag)
			{
				session.m_wtJaccSim.Similarity(this.Threshold, 1.0, result);
			}
			return flag;
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0001F78C File Offset: 0x0001D98C
		public bool Compare(ISession _session, IDataRecord leftRecord, IDataRecord rightRecord, out ComparisonResult result)
		{
			FuzzyComparer.Session session = (FuzzyComparer.Session)_session;
			session.Reset();
			LookupUpdateContext leftRecordUpdateContext = session.LeftRecordUpdateContext;
			LookupUpdateContext rightRecordUpdateContext = session.RightRecordUpdateContext;
			leftRecordUpdateContext.TokenizeAndRuleMatch(leftRecord);
			rightRecordUpdateContext.TokenizeAndRuleMatch(rightRecord);
			for (int i = 0; i < session.m_pairSpecificAggregators.Count; i++)
			{
				ArraySegment<TransformationMatch> arraySegment;
				ArraySegment<TransformationMatch> arraySegment2;
				session.m_pairSpecificAggregators[i].Match(session.m_pairSpecificAggregatorSessions[i], session.m_tokenIdProvider, leftRecord, rightRecord, this.LeftDomainBinding, this.RightDomainBinding, leftRecordUpdateContext.RecordContext.TokenSequence.Tokens, rightRecordUpdateContext.RecordContext.TokenSequence.Tokens, out arraySegment, out arraySegment2);
				if (arraySegment.Count > 0)
				{
					LookupUpdateContext.WeighTransformationMatchList(session.m_tokenIdProvider, session.m_weightProviders[i], leftRecordUpdateContext.m_tranMatchAllocator, leftRecordUpdateContext.m_intAllocator, arraySegment, leftRecordUpdateContext.RecordContext.TransformationMatchList);
				}
				if (arraySegment2.Count > 0)
				{
					LookupUpdateContext.WeighTransformationMatchList(session.m_tokenIdProvider, session.m_weightProviders[i], rightRecordUpdateContext.m_tranMatchAllocator, rightRecordUpdateContext.m_intAllocator, arraySegment2, rightRecordUpdateContext.RecordContext.TransformationMatchList);
				}
			}
			this.ResetLeftRecord(session, leftRecordUpdateContext.RecordContext);
			this.ResetRightRecord(session, rightRecordUpdateContext.RecordContext);
			return this.Compare(session, out result);
		}

		// Token: 0x02000172 RID: 370
		private class Session : ISession
		{
			// Token: 0x06000CF3 RID: 3315 RVA: 0x000375C8 File Offset: 0x000357C8
			public Session(IDomainManager domainManager, ITokenIdProvider tokenIdProvider, RecordBinding leftRecordBinding, RecordBinding rightRecordBinding, string[] comparisonDomains, string[] exactMatchDomains)
			{
				this.m_tokenIdProvider = tokenIdProvider;
				this.m_domainManager = domainManager;
				if (tokenIdProvider != null && domainManager != null)
				{
					this.LeftRecordUpdateContext = new LookupUpdateContext(domainManager, tokenIdProvider, leftRecordBinding, comparisonDomains, exactMatchDomains, JoinSide.Left);
					this.RightRecordUpdateContext = new LookupUpdateContext(domainManager, tokenIdProvider, rightRecordBinding, comparisonDomains, exactMatchDomains, JoinSide.Right);
				}
				this.m_wtJaccSim = new WtJaccSim();
				DomainManager domainManager2 = (DomainManager)domainManager;
				foreach (string text in comparisonDomains)
				{
					PairSpecificTransformationProviderAggregator pairSpecificTransformationProvider = domainManager2[text].PairSpecificTransformationProvider;
					this.m_pairSpecificAggregators.Add(pairSpecificTransformationProvider);
					this.m_pairSpecificAggregatorSessions.Add(((ISessionable)pairSpecificTransformationProvider).CreateSession());
					this.m_weightProviders.Add(domainManager.GetTokenWeightProvider(text));
				}
				this.ComparisonResult = new ComparisonResult();
			}

			// Token: 0x06000CF4 RID: 3316 RVA: 0x000376EC File Offset: 0x000358EC
			public void Reset()
			{
				this.LeftRecordUpdateContext.Reset();
				this.RightRecordUpdateContext.Reset();
				foreach (ISession session in this.m_pairSpecificAggregatorSessions)
				{
					session.Reset();
				}
				this.m_intAllocator.Reset();
				this.m_byteAllocator.Reset();
				this.m_tranMatchAllocator.Reset();
				this.m_weightedTranMatchAllocator.Reset();
				this.LeftTranMatchListBuilder.Reset();
				this.RightTranMatchListBuilder.Reset();
			}

			// Token: 0x040005E3 RID: 1507
			public ITokenIdProvider m_tokenIdProvider;

			// Token: 0x040005E4 RID: 1508
			public IDomainManager m_domainManager;

			// Token: 0x040005E5 RID: 1509
			public WtJaccSim m_wtJaccSim;

			// Token: 0x040005E6 RID: 1510
			public LookupUpdateContext LeftRecordUpdateContext;

			// Token: 0x040005E7 RID: 1511
			public LookupUpdateContext RightRecordUpdateContext;

			// Token: 0x040005E8 RID: 1512
			public RecordContext LeftRecordContext;

			// Token: 0x040005E9 RID: 1513
			public RecordContext RightRecordContext;

			// Token: 0x040005EA RID: 1514
			public ComparisonResult ComparisonResult;

			// Token: 0x040005EB RID: 1515
			public List<PairSpecificTransformationProviderAggregator> m_pairSpecificAggregators = new List<PairSpecificTransformationProviderAggregator>();

			// Token: 0x040005EC RID: 1516
			public List<ISession> m_pairSpecificAggregatorSessions = new List<ISession>();

			// Token: 0x040005ED RID: 1517
			public List<ITokenWeightProvider> m_weightProviders = new List<ITokenWeightProvider>();

			// Token: 0x040005EE RID: 1518
			public BlockedSegmentArray<int> m_intAllocator = new BlockedSegmentArray<int>();

			// Token: 0x040005EF RID: 1519
			public BlockedSegmentArray<byte> m_byteAllocator = new BlockedSegmentArray<byte>();

			// Token: 0x040005F0 RID: 1520
			public BlockedSegmentArray<TransformationMatch> m_tranMatchAllocator = new BlockedSegmentArray<TransformationMatch>();

			// Token: 0x040005F1 RID: 1521
			public BlockedSegmentArray<WeightedTransformationMatch> m_weightedTranMatchAllocator = new BlockedSegmentArray<WeightedTransformationMatch>();

			// Token: 0x040005F2 RID: 1522
			public ArraySegmentBuilder<TransformationMatch> LeftTranMatchListBuilder = new ArraySegmentBuilder<TransformationMatch>();

			// Token: 0x040005F3 RID: 1523
			public ArraySegmentBuilder<TransformationMatch> RightTranMatchListBuilder = new ArraySegmentBuilder<TransformationMatch>();
		}
	}
}
