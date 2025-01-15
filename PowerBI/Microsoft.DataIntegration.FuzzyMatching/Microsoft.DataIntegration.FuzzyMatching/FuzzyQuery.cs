using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Diagnostics;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000051 RID: 81
	[Serializable]
	public sealed class FuzzyQuery : IDisposable
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000F1DF File Offset: 0x0000D3DF
		// (set) Token: 0x060002EE RID: 750 RVA: 0x0000F1E7 File Offset: 0x0000D3E7
		public int LookupId { get; private set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000F1F0 File Offset: 0x0000D3F0
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x0000F1F8 File Offset: 0x0000D3F8
		public double Threshold { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000F201 File Offset: 0x0000D401
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x0000F209 File Offset: 0x0000D409
		public int MaxResults { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000F212 File Offset: 0x0000D412
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x0000F21A File Offset: 0x0000D41A
		public IFuzzyComparer Comparer
		{
			get
			{
				return this.m_comparer;
			}
			set
			{
				this.m_comparer = value;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000F223 File Offset: 0x0000D423
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x0000F22B File Offset: 0x0000D42B
		public RecordBinding LeftRecordBinding { get; private set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000F234 File Offset: 0x0000D434
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x0000F23C File Offset: 0x0000D43C
		public DataTable MatchResultSchema { get; private set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000F245 File Offset: 0x0000D445
		public IStatistics Statistics
		{
			get
			{
				return this.m_statistics;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000F24D File Offset: 0x0000D44D
		public FuzzyLookup FuzzyLookup
		{
			get
			{
				return this.m_fuzzyLookup;
			}
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000F255 File Offset: 0x0000D455
		public ITokenIdProvider TokenIdProvider(ISession session)
		{
			return (session as FuzzyQuery.Session).m_sessionIdProvider;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000F262 File Offset: 0x0000D462
		public ITokenIdProvider TokenIdProvider()
		{
			return (this.GetDefaultSession() as FuzzyQuery.Session).m_sessionIdProvider;
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000F274 File Offset: 0x0000D474
		// (set) Token: 0x060002FE RID: 766 RVA: 0x0000F27C File Offset: 0x0000D47C
		public bool EarlyTermination { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002FF RID: 767 RVA: 0x0000F285 File Offset: 0x0000D485
		// (set) Token: 0x06000300 RID: 768 RVA: 0x0000F28D File Offset: 0x0000D48D
		public int MaxComparisons { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000F296 File Offset: 0x0000D496
		// (set) Token: 0x06000302 RID: 770 RVA: 0x0000F29E File Offset: 0x0000D49E
		public bool MinimalComparisonResult { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000F2A7 File Offset: 0x0000D4A7
		// (set) Token: 0x06000304 RID: 772 RVA: 0x0000F2AF File Offset: 0x0000D4AF
		public bool EnablePairSpecificTransformations { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0000F2B8 File Offset: 0x0000D4B8
		// (set) Token: 0x06000306 RID: 774 RVA: 0x0000F2C0 File Offset: 0x0000D4C0
		public bool LookupName { get; set; }

		// Token: 0x06000307 RID: 775 RVA: 0x0000F2C9 File Offset: 0x0000D4C9
		public FuzzyQuery()
		{
			this.Threshold = 0.0;
			this.MaxResults = int.MaxValue;
			this.EarlyTermination = true;
			this.MinimalComparisonResult = false;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000F304 File Offset: 0x0000D504
		public FuzzyQuery(FuzzyLookup fuzzyLookup, string lookupName, RecordBinding leftRecordBinding)
			: this(fuzzyLookup, fuzzyLookup.Lookups[lookupName].LookupId, leftRecordBinding, null)
		{
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000F320 File Offset: 0x0000D520
		public FuzzyQuery(FuzzyLookup fuzzyLookup, int lookupId, RecordBinding leftRecordBinding, FuzzyLookupEntry.FuzzyLookupParameters parameter = null)
			: this()
		{
			if (parameter != null)
			{
				this.MaxResults = parameter.NumberOfMatches;
			}
			this.MaxComparisons = int.MaxValue;
			this.m_fuzzyLookup = fuzzyLookup;
			this.LookupId = lookupId;
			this.LeftRecordBinding = leftRecordBinding;
			Lookup lookup = fuzzyLookup.IndexDefinition.Lookups[lookupId];
			RecordBinding recordBinding = fuzzyLookup.IndexDefinition.RecordBinding;
			DataTable schema = recordBinding.Schema;
			DataTable dataTable = FuzzyQuery.GenerateOutputSchemaForQuery(leftRecordBinding.Schema, schema);
			this.m_matchResultsReaderTemplate = new MatchResultsReader(leftRecordBinding.Schema, schema, dataTable);
			this.MatchResultSchema = this.m_matchResultsReaderTemplate.GetSchemaTable();
			this.RankBy(CompareMatchResultBySimilarityDescRidAsc.Instance, true);
			this.PrepareThresholdSet(0.0);
			if (lookup.Comparer != null)
			{
				IFuzzyComparer fuzzyComparer = lookup.Comparer.CreateInstance() as IFuzzyComparer;
				fuzzyComparer.Initialize(leftRecordBinding, recordBinding, lookup.Domains, lookup.ExactMatchDomains);
				this.Comparer = fuzzyComparer;
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000F40C File Offset: 0x0000D60C
		public ISession CreateSession(IDomainManager domainManager, ITokenIdProvider tokenIdProvider)
		{
			return this.CreateSession(domainManager, tokenIdProvider, null);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000F418 File Offset: 0x0000D618
		public ISession CreateSession(IDomainManager domainManager, ITokenIdProvider tokenIdProvider, IConnectionManager connectionManager)
		{
			FuzzyQuery.Session session = new FuzzyQuery.Session();
			session.m_customDomainManager = domainManager;
			session.m_sessionIdProvider = new SessionTokenIdProvider(tokenIdProvider);
			session.m_dataRecordForFetch = new SimpleDataRecord(this.FuzzyLookup.IndexDefinition.RecordBinding.Schema, new object[this.FuzzyLookup.IndexDefinition.RecordBinding.Schema.Rows.Count]);
			session.m_leftLookupUpdateContext = new LookupUpdateContext(session.m_customDomainManager, session.m_sessionIdProvider, this.LeftRecordBinding, this.Lookup.Domains, this.Lookup.ExactMatchDomains, JoinSide.Left);
			session.m_rightLookupUpdateContext = new LookupUpdateContext(session.m_customDomainManager, session.m_sessionIdProvider, this.FuzzyLookup.IndexDefinition.RecordBinding, this.Lookup.Domains, this.Lookup.ExactMatchDomains, JoinSide.Right);
			FuzzyLookup.CreateSignatureGenerator(this.FuzzyLookup.IndexDefinition.Lookups[this.LookupId], this.FuzzyLookup.DomainManager, out session.m_signatureGenerator);
			session.m_multiDimSignatureGenerator = session.m_signatureGenerator as IMultiDimSignatureGenerator;
			session.m_comparerSession = this.Comparer.CreateSession(domainManager, session.m_sessionIdProvider);
			if (this.m_fuzzyLookup.StateManager is SqlStateManager)
			{
				if (connectionManager == null)
				{
					connectionManager = (this.m_fuzzyLookup.StateManager as SqlStateManager).ConnectionManager;
				}
				session.m_stateManagerSession = (this.m_fuzzyLookup.StateManager as SqlStateManager).CreateSession(connectionManager);
			}
			else if (this.m_fuzzyLookup.StateManager is ISessionable)
			{
				session.m_stateManagerSession = (this.m_fuzzyLookup.StateManager as ISessionable).CreateSession();
			}
			session.m_matchResultsReader = new MatchResultsReader(this.m_matchResultsReaderTemplate.m_inputSchema, this.m_matchResultsReaderTemplate.m_referenceSchema, this.m_matchResultsReaderTemplate.OutputSchema);
			return session;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000F5F2 File Offset: 0x0000D7F2
		public void Dispose()
		{
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0000F5F4 File Offset: 0x0000D7F4
		private Lookup Lookup
		{
			get
			{
				return this.m_fuzzyLookup.IndexDefinition.Lookups[this.LookupId];
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000F611 File Offset: 0x0000D811
		public void RankBy(IComparer<IMatchResult> customComparer, bool isAlwaysLessThanOrEqualToDefaultSimilarity)
		{
			this.m_customRankComparer = customComparer;
			this.m_customRankIsAlwaysLEQ = isAlwaysLessThanOrEqualToDefaultSimilarity;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000F624 File Offset: 0x0000D824
		public void AddCustomPropertyToMatchResultsReader(string propertyName, Type dataType)
		{
			DataRow dataRow = this.m_matchResultsReaderTemplate.OutputSchema.NewRow();
			dataRow[SchemaTableColumn.ColumnName] = SchemaUtils.GenerateUniqueColumnName(this.m_matchResultsReaderTemplate.OutputSchema, propertyName);
			dataRow[SchemaTableColumn.ColumnOrdinal] = this.m_matchResultsReaderTemplate.OutputSchema.Rows.Count;
			dataRow[SchemaTableColumn.DataType] = dataType;
			dataRow[SchemaTableOptionalColumn.ProviderSpecificDataType] = propertyName;
			this.m_matchResultsReaderTemplate.OutputSchema.Rows.Add(dataRow);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000F6B4 File Offset: 0x0000D8B4
		private static int CompareRidInfoByCountDescRidAsc(FuzzyQuery.RidInfo x, FuzzyQuery.RidInfo y)
		{
			if (x.count == y.count)
			{
				if (x.rid < y.rid)
				{
					return -1;
				}
				if (x.rid <= y.rid)
				{
					return 0;
				}
				return 1;
			}
			else
			{
				if (x.count <= y.count)
				{
					return 1;
				}
				return -1;
			}
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000F702 File Offset: 0x0000D902
		public MatchResultsReader Match(IDataRecord inputRecord)
		{
			return this.Match(this.GetDefaultSession(), inputRecord, this.MaxResults);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000F717 File Offset: 0x0000D917
		private ISession GetDefaultSession()
		{
			if (this.m_defaultSession == null)
			{
				this.m_defaultSession = this.CreateSession(this.FuzzyLookup.DomainManager, this.FuzzyLookup.DomainManager.TokenIdProvider);
			}
			return this.m_defaultSession;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000F750 File Offset: 0x0000D950
		public MatchResultsReader Match(IDataRecord inputRecord, int maxResults)
		{
			ISession defaultSession = this.GetDefaultSession();
			ISession session = defaultSession;
			MatchResultsReader matchResultsReader;
			lock (session)
			{
				matchResultsReader = this.Match(defaultSession, inputRecord, maxResults);
			}
			return matchResultsReader;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000F790 File Offset: 0x0000D990
		public MatchResultsReader Match(ISession session, IDataRecord inputRecord)
		{
			return this.Match(session, inputRecord, this.MaxResults);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000F7A0 File Offset: 0x0000D9A0
		public MatchResultsReader Match(ISession _session, IDataRecord inputRecord, int maxResults)
		{
			FuzzyQuery.Session session = (FuzzyQuery.Session)_session;
			if (this.m_statistics.EnableTimers)
			{
				this.m_statistics.TotalQueryTime.Start();
			}
			session.Reset();
			session.m_matchResultsReader.Reset(inputRecord);
			if (this.m_statistics.EnableTimers)
			{
				session.m_leftLookupUpdateContext.TokenizeAndRuleMatch(inputRecord, this.m_statistics.LeftTokenizationTime, this.m_statistics.LeftTransformationMatchTime);
			}
			else
			{
				session.m_leftLookupUpdateContext.TokenizeAndRuleMatch(inputRecord);
			}
			this.m_statistics.LeftTransformationCount += (long)session.m_leftLookupUpdateContext.RecordContext.TransformationMatchList.Count;
			this.Comparer.ResetLeftRecord(session.m_comparerSession, session.m_leftLookupUpdateContext.RecordContext);
			if (session.m_signatureGenerator is IThreshold)
			{
				(session.m_signatureGenerator as IThreshold).Threshold = this.Threshold;
			}
			this.m_statistics.Start(this.m_statistics.SignatureGenerationTime);
			session.m_signatureGenerator.Reset(session.m_leftLookupUpdateContext.RecordContext.TokenSequence, session.m_leftLookupUpdateContext.RecordContext.TransformationMatchList);
			this.m_statistics.Stop(this.m_statistics.SignatureGenerationTime);
			int num = ((session.m_multiDimSignatureGenerator == null) ? 1 : session.m_multiDimSignatureGenerator.NumHashtables);
			if (this.MaxComparisons < 2147483647)
			{
				this.Process_MaxComparisons(inputRecord, session, num);
			}
			else
			{
				this.Process_EarlyTermination(inputRecord, maxResults, session, num);
			}
			MatchResultsReader matchResultsReader = session.m_matchResultsReader;
			if (this.m_statistics.EnableTimers)
			{
				this.m_statistics.SortTime.Start();
			}
			if (this.m_customRankComparer != null)
			{
				IMatchResult[] items = matchResultsReader.m_results.Items;
				Array.Sort<IMatchResult>(items, 0, matchResultsReader.m_results.Count, this.m_customRankComparer);
			}
			if (this.m_statistics.EnableTimers)
			{
				this.m_statistics.SortTime.Stop();
			}
			if (matchResultsReader.m_results.Count > maxResults)
			{
				matchResultsReader.m_results.Truncate(maxResults);
			}
			if (matchResultsReader.m_results.Count == 0)
			{
				matchResultsReader.m_defaultResult.LeftRecord = inputRecord;
				matchResultsReader.m_defaultResult.ComparisonResult = new ComparisonResult
				{
					Similarity = 0.0,
					LeftRecordContext = session.m_leftLookupUpdateContext.RecordContext,
					TotalLeftWeight = (double)Enumerable.Sum(session.m_leftLookupUpdateContext.RecordContext.TokenSequence.Weights.AsEnumerable<int>())
				};
			}
			if (this.m_statistics.EnableTimers)
			{
				this.m_statistics.TotalQueryTime.Stop();
			}
			return matchResultsReader;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000FA2C File Offset: 0x0000DC2C
		private void Process_MaxComparisons(IDataRecord inputRecord, FuzzyQuery.Session session, int numIndexes)
		{
			IFuzzyLookupStateManager stateManager = this.m_fuzzyLookup.StateManager;
			for (int i = 0; i < numIndexes; i++)
			{
				if (session.m_multiDimSignatureGenerator != null)
				{
					if (this.Statistics.EnableTimers)
					{
						this.m_statistics.SignatureGenerationTime.Start();
					}
					session.m_multiDimSignatureGenerator.Reset(i);
					if (this.Statistics.EnableTimers)
					{
						this.m_statistics.SignatureGenerationTime.Stop();
					}
				}
				foreach (int num in session.m_signatureGenerator)
				{
					this.m_statistics.SignaturesLookedUp += 1L;
					if (this.m_statistics.EnableTimers)
					{
						this.m_statistics.RidListRetrievalTime.Start();
					}
					RidList ridList;
					bool flag = stateManager.TryGetSignatureRidList(session.m_stateManagerSession, this.LookupId, i, num, ref session.m_tempRidBuffer, out ridList);
					if (this.m_statistics.EnableTimers)
					{
						this.m_statistics.RidListRetrievalTime.Stop();
					}
					if (flag && ridList.Count > 0)
					{
						for (int j = 0; j < ridList.Count; j++)
						{
							int num2 = ridList[j];
							FuzzyQuery.RidInfo ridInfo;
							if (session.m_ridHash.TryGetValue(num2, ref ridInfo))
							{
								if (ridInfo.lastIdx != i)
								{
									ridInfo.count++;
								}
							}
							else
							{
								ridInfo.rid = num2;
								ridInfo.count = 1;
							}
							ridInfo.lastIdx = i;
							session.m_ridHash[num2] = ridInfo;
						}
						this.m_statistics.NonDistinctRids += (long)ridList.Count;
					}
				}
			}
			IEnumerable<FuzzyQuery.RidInfo> enumerable;
			if (this.MaxComparisons < 2147483647)
			{
				session.m_ridList.Clear();
				session.m_ridList.AddRange(session.m_ridHash.Values);
				session.m_ridList.Sort(new Comparison<FuzzyQuery.RidInfo>(FuzzyQuery.CompareRidInfoByCountDescRidAsc));
				enumerable = session.m_ridList;
			}
			else
			{
				enumerable = session.m_ridHash.Values;
			}
			if (this.m_statistics.EnableTimers)
			{
				this.m_statistics.ReferenceComparisonTime.Start();
			}
			int num3 = 0;
			foreach (FuzzyQuery.RidInfo ridInfo2 in enumerable)
			{
				if (num3 >= this.MaxComparisons && ridInfo2.count < numIndexes)
				{
					break;
				}
				this.ProcessRid(session, stateManager, session.m_leftLookupUpdateContext.RecordContext, inputRecord, ridInfo2.rid);
				num3++;
			}
			if (this.m_statistics.EnableTimers)
			{
				this.m_statistics.ReferenceComparisonTime.Stop();
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000FD10 File Offset: 0x0000DF10
		private void Process_EarlyTermination(IDataRecord inputRecord, int maxResults, FuzzyQuery.Session session, int numIndexes)
		{
			for (int i = 0; i < numIndexes; i++)
			{
				if (session.m_multiDimSignatureGenerator != null)
				{
					if (this.m_statistics.EnableTimers)
					{
						this.m_statistics.SignatureGenerationTime.Start();
					}
					session.m_multiDimSignatureGenerator.Reset(i);
					if (this.m_statistics.EnableTimers)
					{
						this.m_statistics.SignatureGenerationTime.Stop();
					}
				}
				foreach (int num in session.m_signatureGenerator)
				{
					if (this.m_statistics.EnableTimers)
					{
						this.m_statistics.RidListRetrievalTime.Start();
					}
					RidList ridList;
					bool flag = this.m_fuzzyLookup.StateManager.TryGetSignatureRidList(session.m_stateManagerSession, this.LookupId, i, num, ref session.m_tempRidBuffer, out ridList);
					if (this.m_statistics.EnableTimers)
					{
						this.m_statistics.RidListRetrievalTime.Stop();
					}
					if (flag && ridList.Count > 0)
					{
						this.ProcessRidList(session, this.m_fuzzyLookup.StateManager, session.m_leftLookupUpdateContext.RecordContext, inputRecord, ridList);
						this.m_statistics.NonDistinctRids += (long)ridList.Count;
					}
					this.m_statistics.SignaturesLookedUp += 1L;
				}
				double num2;
				if (this.EarlyTermination && this.m_customRankComparer != null && this.m_customRankIsAlwaysLEQ && i < numIndexes - 1 && maxResults < 2147483647 && session.m_matchResultsReader.m_results.Count >= maxResults && this.TryGetMatchingThreshold(i + 1, out num2))
				{
					MatchResultsReader matchResultsReader = session.m_matchResultsReader;
					if (this.m_statistics.EnableTimers)
					{
						this.m_statistics.SortTime.Start();
					}
					IMatchResult[] items = matchResultsReader.m_results.Items;
					Array.Sort<IMatchResult>(items, 0, matchResultsReader.m_results.Count, this.m_customRankComparer);
					if (this.m_statistics.EnableTimers)
					{
						this.m_statistics.SortTime.Stop();
					}
					if (matchResultsReader.m_results[maxResults - 1].ComparisonResult.Similarity >= num2)
					{
						break;
					}
				}
			}
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000FF58 File Offset: 0x0000E158
		private void ProcessRidList(FuzzyQuery.Session session, IFuzzyLookupStateManager stateManager, RecordContext leftRecordContext, IDataRecord leftRecord, RidList ridList)
		{
			for (int i = 0; i < ridList.Count; i++)
			{
				int num = ridList[i];
				if (!session.m_similarityComputedForRid.Contains(num))
				{
					this.ProcessRid(session, stateManager, leftRecordContext, leftRecord, num);
					session.m_similarityComputedForRid.Add(num);
				}
			}
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000FFA8 File Offset: 0x0000E1A8
		private void ProcessRid(FuzzyQuery.Session session, IFuzzyLookupStateManager stateManager, RecordContext leftRecordContext, IDataRecord leftRecord, int rid)
		{
			if (this.m_statistics.EnableTimers)
			{
				this.m_statistics.RightRecordRetrievalTime.Start();
			}
			if (!stateManager.TryGetRecord(session.m_stateManagerSession, rid, ref session.m_dataRecordForFetch.Values))
			{
				throw new Exception("RecordId was unexpectedly not found in the state manager");
			}
			if (this.m_statistics.EnableTimers)
			{
				this.m_statistics.RightRecordRetrievalTime.Stop();
			}
			MatchResult matchResult = session.m_matchResultsReader.m_results.Add();
			matchResult.Reset();
			if (this.m_statistics.EnableTimers)
			{
				this.m_statistics.ReferenceComparisonTime.Start();
			}
			this.Comparer.Threshold = this.Threshold;
			RecordContext recordContext = null;
			ComparisonResult comparisonResult;
			bool flag;
			if (this.EnablePairSpecificTransformations)
			{
				flag = this.Comparer.Compare(session.m_comparerSession, leftRecord, session.m_dataRecordForFetch, out comparisonResult);
			}
			else
			{
				recordContext = session.m_rightLookupUpdateContext.RecordContext;
				if (!stateManager.EnableReferenceContextCaching || !stateManager.TryGetRecordContext(session.m_stateManagerSession, rid, this.LookupId, out recordContext))
				{
					session.m_rightLookupUpdateContext.Reset();
					if (this.m_statistics.EnableTimers)
					{
						session.m_rightLookupUpdateContext.TokenizeAndRuleMatch(session.m_dataRecordForFetch, this.m_statistics.RightTokenizationTime, this.m_statistics.RightTransformationMatchTime);
					}
					else
					{
						session.m_rightLookupUpdateContext.TokenizeAndRuleMatch(session.m_dataRecordForFetch);
					}
					if (stateManager.EnableReferenceContextCaching)
					{
						recordContext = session.m_rightLookupUpdateContext.RecordContext.Clone(HeapSegmentAllocator<int>.Instance, HeapSegmentAllocator<byte>.Instance);
						stateManager.CacheRecordContext(rid, this.LookupId, recordContext);
					}
				}
				this.Comparer.ResetRightRecord(session.m_comparerSession, recordContext);
				flag = this.Comparer.Compare(session.m_comparerSession, out comparisonResult);
			}
			if (this.m_statistics.EnableTimers)
			{
				this.m_statistics.ReferenceComparisonTime.Stop();
			}
			if (flag)
			{
				matchResult.LeftRecord = leftRecord;
				matchResult.RightRecordId = rid;
				matchResult.RightRecord.Schema = session.m_dataRecordForFetch.Schema;
				matchResult.RightRecord.Values = session.m_dataRecordForFetch.Values;
				matchResult.ComparisonResult = comparisonResult;
				if (this.EnablePairSpecificTransformations)
				{
					matchResult.ComparisonResult = comparisonResult.Clone(session.m_weightedTranMatchAllocator, session.m_tranMatchAllocator, session.m_intAllocator, session.m_byteAllocator, false);
				}
				else if (this.MinimalComparisonResult)
				{
					matchResult.ComparisonResult = comparisonResult.Clone(null, null, null, null, true);
				}
				else
				{
					comparisonResult.LeftRecordContext = null;
					comparisonResult.RightRecordContext = null;
					matchResult.ComparisonResult = comparisonResult.Clone(session.m_weightedTranMatchAllocator, session.m_tranMatchAllocator, session.m_intAllocator, session.m_byteAllocator, true);
					comparisonResult.LeftRecordContext = leftRecordContext;
					if (recordContext != session.m_rightLookupUpdateContext.RecordContext)
					{
						matchResult.ComparisonResult.RightRecordContext = recordContext;
					}
					else
					{
						matchResult.ComparisonResult.RightRecordContext = session.m_rightLookupUpdateContext.RecordContext.Clone(session.m_intAllocator, session.m_byteAllocator);
					}
				}
				if (matchResult.ComparisonResult.RightRecordContext != null)
				{
					this.m_statistics.RightTransformationCount += (long)matchResult.ComparisonResult.RightRecordContext.TransformationMatchList.Count;
				}
				this.m_statistics.MatchesReturned += 1L;
			}
			else
			{
				matchResult.Reset();
				session.m_matchResultsReader.m_results.Truncate(session.m_matchResultsReader.m_results.Count - 1);
			}
			this.m_statistics.ReferenceComparisons += 1L;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00010318 File Offset: 0x0000E518
		internal static DataTable GenerateOutputSchemaForQuery(DataTable inputSchema, DataTable referenceSchema)
		{
			DataTable dataTable = SchemaUtils.CreateSchemaTable(StringResources.OutputSchemaName);
			foreach (object obj in inputSchema.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				if (!dataTable.Columns.Contains(dataColumn.ColumnName))
				{
					dataTable.Columns.Add(dataColumn.ColumnName, dataColumn.DataType);
				}
			}
			foreach (object obj2 in referenceSchema.Columns)
			{
				DataColumn dataColumn2 = (DataColumn)obj2;
				if (!dataTable.Columns.Contains(dataColumn2.ColumnName))
				{
					dataTable.Columns.Add(dataColumn2.ColumnName, dataColumn2.DataType);
				}
			}
			int num = 0;
			foreach (object obj3 in inputSchema.Rows)
			{
				DataRow dataRow = (DataRow)obj3;
				DataRow dataRow2 = dataTable.NewRow();
				foreach (object obj4 in inputSchema.Columns)
				{
					DataColumn dataColumn3 = (DataColumn)obj4;
					dataRow2[dataColumn3.ColumnName] = dataRow[dataColumn3.ColumnName];
				}
				dataRow2[SchemaTableColumn.ColumnName] = SchemaUtils.GenerateUniqueColumnName(dataTable, dataRow[SchemaTableColumn.ColumnName] as string);
				dataRow2[SchemaTableColumn.ColumnOrdinal] = num++;
				dataTable.Rows.Add(dataRow2);
			}
			foreach (object obj5 in referenceSchema.Rows)
			{
				DataRow dataRow3 = (DataRow)obj5;
				DataRow dataRow4 = dataTable.NewRow();
				foreach (object obj6 in referenceSchema.Columns)
				{
					DataColumn dataColumn4 = (DataColumn)obj6;
					dataRow4[dataColumn4.ColumnName] = dataRow3[dataColumn4.ColumnName];
				}
				dataRow4[SchemaTableColumn.ColumnName] = SchemaUtils.GenerateUniqueColumnName(dataTable, dataRow3[SchemaTableColumn.ColumnName] as string);
				dataRow4[SchemaTableColumn.ColumnOrdinal] = num++;
				dataTable.Rows.Add(dataRow4);
			}
			DataRow dataRow5 = dataTable.NewRow();
			dataRow5[SchemaTableColumn.ColumnName] = StringResources.SimilarityColumnName;
			dataRow5[SchemaTableColumn.ColumnOrdinal] = dataTable.Rows.Count;
			dataRow5[SchemaTableColumn.DataType] = typeof(double);
			dataRow5[SchemaTableColumn.ColumnSize] = 8;
			dataTable.Rows.Add(dataRow5);
			return dataTable;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00010680 File Offset: 0x0000E880
		private void PrepareThresholdSet(double minThreshold)
		{
			if (!this.m_fuzzyLookup.IndexDefinition.Lookups[this.LookupId].SignatureGenerator.TypeName.Contains(typeof(LshSignatureGenerator).Name))
			{
				return;
			}
			LshSignatureGenerator lshSignatureGenerator = (LshSignatureGenerator)this.m_fuzzyLookup.IndexDefinition.Lookups[this.LookupId].SignatureGenerator.CreateInstance();
			int numHashtables = lshSignatureGenerator.NumHashtables;
			int dimensionsPerSignature = lshSignatureGenerator.DimensionsPerSignature;
			double num = LshSignatureGeneratorV1.SuccessProbability(dimensionsPerSignature, numHashtables, minThreshold);
			double num2 = 0.05;
			this.m_numHashTables = new Vector<FuzzyQuery.LshThresholdSetting>();
			for (double num3 = minThreshold; num3 <= 1.0; num3 += num2)
			{
				FuzzyQuery.LshThresholdSetting lshThresholdSetting = default(FuzzyQuery.LshThresholdSetting);
				lshThresholdSetting.threshold = num3;
				lshThresholdSetting.nHashTables = LshSignatureGeneratorV1.NumHashTables(dimensionsPerSignature, num3, num);
				this.m_numHashTables.Add(lshThresholdSetting);
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00010764 File Offset: 0x0000E964
		private bool TryGetMatchingThreshold(int nHashTables, out double threshold)
		{
			for (int i = 0; i < this.m_numHashTables.Count; i++)
			{
				if (this.m_numHashTables[i].nHashTables == nHashTables)
				{
					threshold = this.m_numHashTables[i].threshold;
					return true;
				}
			}
			threshold = 1.0;
			return false;
		}

		// Token: 0x04000108 RID: 264
		private IFuzzyComparer m_comparer;

		// Token: 0x0400010B RID: 267
		private FuzzyQuery.FuzzyQueryStatistics m_statistics = new FuzzyQuery.FuzzyQueryStatistics();

		// Token: 0x04000111 RID: 273
		private FuzzyLookup m_fuzzyLookup;

		// Token: 0x04000112 RID: 274
		private IComparer<IMatchResult> m_customRankComparer;

		// Token: 0x04000113 RID: 275
		private bool m_customRankIsAlwaysLEQ;

		// Token: 0x04000114 RID: 276
		private MatchResultsReader m_matchResultsReaderTemplate;

		// Token: 0x04000115 RID: 277
		[NonSerialized]
		private ISession m_defaultSession;

		// Token: 0x04000116 RID: 278
		private Vector<FuzzyQuery.LshThresholdSetting> m_numHashTables;

		// Token: 0x02000141 RID: 321
		private class Session : ISession, IDisposable
		{
			// Token: 0x06000C5B RID: 3163 RVA: 0x00035BFC File Offset: 0x00033DFC
			public void Reset()
			{
				this.m_sessionIdProvider.Reset();
				if (this.m_comparerSession != null)
				{
					this.m_comparerSession.Reset();
				}
				if (this.m_stateManagerSession != null)
				{
					this.m_stateManagerSession.Reset();
				}
				this.m_similarityComputedForRid.Clear();
				this.m_ridHash.Clear();
				this.m_ridList.Clear();
				this.m_leftLookupUpdateContext.Reset();
				this.m_rightLookupUpdateContext.Reset();
				this.m_intAllocator.Reset();
				this.m_byteAllocator.Reset();
				this.m_tranMatchAllocator.Reset();
				this.m_weightedTranMatchAllocator.Reset();
			}

			// Token: 0x06000C5C RID: 3164 RVA: 0x00035CA0 File Offset: 0x00033EA0
			public void Dispose()
			{
				if (this.m_comparerSession != null && this.m_comparerSession is IDisposable)
				{
					(this.m_comparerSession as IDisposable).Dispose();
				}
				this.m_comparerSession = null;
				if (this.m_stateManagerSession != null && this.m_stateManagerSession is IDisposable)
				{
					(this.m_stateManagerSession as IDisposable).Dispose();
				}
				this.m_stateManagerSession = null;
			}

			// Token: 0x04000520 RID: 1312
			public IFuzzyComparer m_comparer;

			// Token: 0x04000521 RID: 1313
			public ISession m_comparerSession;

			// Token: 0x04000522 RID: 1314
			public LookupUpdateContext m_leftLookupUpdateContext;

			// Token: 0x04000523 RID: 1315
			public LookupUpdateContext m_rightLookupUpdateContext;

			// Token: 0x04000524 RID: 1316
			public IOneDimSignatureGenerator m_signatureGenerator;

			// Token: 0x04000525 RID: 1317
			public IMultiDimSignatureGenerator m_multiDimSignatureGenerator;

			// Token: 0x04000526 RID: 1318
			public int[] m_tempRidBuffer = new int[1];

			// Token: 0x04000527 RID: 1319
			public SimpleDataRecord m_dataRecordForFetch;

			// Token: 0x04000528 RID: 1320
			public MatchResultsReader m_matchResultsReader;

			// Token: 0x04000529 RID: 1321
			public SessionTokenIdProvider m_sessionIdProvider;

			// Token: 0x0400052A RID: 1322
			public IDomainManager m_customDomainManager;

			// Token: 0x0400052B RID: 1323
			public ISession m_stateManagerSession;

			// Token: 0x0400052C RID: 1324
			public HashSet<int> m_similarityComputedForRid = new HashSet<int>();

			// Token: 0x0400052D RID: 1325
			public Dictionary<int, FuzzyQuery.RidInfo> m_ridHash = new Dictionary<int, FuzzyQuery.RidInfo>();

			// Token: 0x0400052E RID: 1326
			public List<FuzzyQuery.RidInfo> m_ridList = new List<FuzzyQuery.RidInfo>();

			// Token: 0x0400052F RID: 1327
			public BlockedSegmentArray<int> m_intAllocator = new BlockedSegmentArray<int>();

			// Token: 0x04000530 RID: 1328
			public BlockedSegmentArray<byte> m_byteAllocator = new BlockedSegmentArray<byte>();

			// Token: 0x04000531 RID: 1329
			public BlockedSegmentArray<TransformationMatch> m_tranMatchAllocator = new BlockedSegmentArray<TransformationMatch>();

			// Token: 0x04000532 RID: 1330
			public BlockedSegmentArray<WeightedTransformationMatch> m_weightedTranMatchAllocator = new BlockedSegmentArray<WeightedTransformationMatch>();
		}

		// Token: 0x02000142 RID: 322
		[Serializable]
		private struct LshThresholdSetting
		{
			// Token: 0x04000533 RID: 1331
			public double threshold;

			// Token: 0x04000534 RID: 1332
			public int nHashTables;
		}

		// Token: 0x02000143 RID: 323
		[Serializable]
		private struct RidInfo
		{
			// Token: 0x04000535 RID: 1333
			public int rid;

			// Token: 0x04000536 RID: 1334
			public int count;

			// Token: 0x04000537 RID: 1335
			public int lastIdx;
		}

		// Token: 0x02000144 RID: 324
		[Serializable]
		private class FuzzyQueryStatistics : StatisticsBase, IDeserializationCallback
		{
			// Token: 0x06000C5E RID: 3166 RVA: 0x00035D74 File Offset: 0x00033F74
			void IDeserializationCallback.OnDeserialization(object sender)
			{
				this.TotalQueryTime = new Stopwatch();
				this.LeftTokenizationTime = new Stopwatch();
				this.RightTokenizationTime = new Stopwatch();
				this.LeftTransformationMatchTime = new Stopwatch();
				this.RightTransformationMatchTime = new Stopwatch();
				this.SignatureGenerationTime = new Stopwatch();
				this.ReferenceComparisonTime = new Stopwatch();
				this.RightRecordRetrievalTime = new Stopwatch();
				this.RidListRetrievalTime = new Stopwatch();
				this.ComparerTime = new Stopwatch();
				this.SortTime = new Stopwatch();
			}

			// Token: 0x06000C5F RID: 3167 RVA: 0x00035DFC File Offset: 0x00033FFC
			public override void Reset()
			{
				this.LeftTransformationCount = 0L;
				this.RightTransformationCount = 0L;
				this.SignaturesLookedUp = 0L;
				this.NonDistinctRids = 0L;
				this.ReferenceComparisons = 0L;
				this.MatchesReturned = 0L;
				if (base.EnableTimers)
				{
					this.TotalQueryTime.Reset();
					this.LeftTokenizationTime.Reset();
					this.RightTokenizationTime.Reset();
					this.LeftTransformationMatchTime.Reset();
					this.RightTransformationMatchTime.Reset();
					this.SignatureGenerationTime.Reset();
					this.ReferenceComparisonTime.Reset();
					this.RightRecordRetrievalTime.Reset();
					this.RidListRetrievalTime.Reset();
					this.ComparerTime.Reset();
					this.SortTime.Reset();
				}
			}

			// Token: 0x04000538 RID: 1336
			[Statistic(0)]
			public long LeftTransformationCount;

			// Token: 0x04000539 RID: 1337
			[Statistic(0)]
			public long RightTransformationCount;

			// Token: 0x0400053A RID: 1338
			[Statistic(0)]
			public long SignaturesLookedUp;

			// Token: 0x0400053B RID: 1339
			[Statistic(0)]
			public long NonDistinctRids;

			// Token: 0x0400053C RID: 1340
			[Statistic(0)]
			public long ReferenceComparisons;

			// Token: 0x0400053D RID: 1341
			[Statistic(0)]
			public long MatchesReturned;

			// Token: 0x0400053E RID: 1342
			[NonSerialized]
			public Stopwatch TotalQueryTime = new Stopwatch();

			// Token: 0x0400053F RID: 1343
			[NonSerialized]
			public Stopwatch LeftTokenizationTime = new Stopwatch();

			// Token: 0x04000540 RID: 1344
			[NonSerialized]
			public Stopwatch RightTokenizationTime = new Stopwatch();

			// Token: 0x04000541 RID: 1345
			[NonSerialized]
			public Stopwatch LeftTransformationMatchTime = new Stopwatch();

			// Token: 0x04000542 RID: 1346
			[NonSerialized]
			public Stopwatch RightTransformationMatchTime = new Stopwatch();

			// Token: 0x04000543 RID: 1347
			[NonSerialized]
			public Stopwatch SignatureGenerationTime = new Stopwatch();

			// Token: 0x04000544 RID: 1348
			[NonSerialized]
			public Stopwatch ReferenceComparisonTime = new Stopwatch();

			// Token: 0x04000545 RID: 1349
			[NonSerialized]
			public Stopwatch RightRecordRetrievalTime = new Stopwatch();

			// Token: 0x04000546 RID: 1350
			[NonSerialized]
			public Stopwatch RidListRetrievalTime = new Stopwatch();

			// Token: 0x04000547 RID: 1351
			[NonSerialized]
			public Stopwatch ComparerTime = new Stopwatch();

			// Token: 0x04000548 RID: 1352
			[NonSerialized]
			public Stopwatch SortTime = new Stopwatch();
		}
	}
}
