using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data.Sql;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Diagnostics;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000058 RID: 88
	[Serializable]
	internal sealed class SqlFuzzyQuery
	{
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600035C RID: 860 RVA: 0x00010D5A File Offset: 0x0000EF5A
		// (set) Token: 0x0600035D RID: 861 RVA: 0x00010D62 File Offset: 0x0000EF62
		private Lookup Lookup { get; set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00010D6B File Offset: 0x0000EF6B
		// (set) Token: 0x0600035F RID: 863 RVA: 0x00010D73 File Offset: 0x0000EF73
		public string LookupName { get; set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000360 RID: 864 RVA: 0x00010D7C File Offset: 0x0000EF7C
		// (set) Token: 0x06000361 RID: 865 RVA: 0x00010D84 File Offset: 0x0000EF84
		public RecordBinding LeftRecordBinding { get; set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000362 RID: 866 RVA: 0x00010D8D File Offset: 0x0000EF8D
		// (set) Token: 0x06000363 RID: 867 RVA: 0x00010D95 File Offset: 0x0000EF95
		public double? Threshold { get; set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000364 RID: 868 RVA: 0x00010D9E File Offset: 0x0000EF9E
		// (set) Token: 0x06000365 RID: 869 RVA: 0x00010DA6 File Offset: 0x0000EFA6
		public IFuzzyComparer Comparer { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000366 RID: 870 RVA: 0x00010DAF File Offset: 0x0000EFAF
		// (set) Token: 0x06000367 RID: 871 RVA: 0x00010DB7 File Offset: 0x0000EFB7
		public string TableNamePrefix { get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00010DC0 File Offset: 0x0000EFC0
		// (set) Token: 0x06000369 RID: 873 RVA: 0x00010DC8 File Offset: 0x0000EFC8
		public string SqlSchemaName { get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600036A RID: 874 RVA: 0x00010DD1 File Offset: 0x0000EFD1
		// (set) Token: 0x0600036B RID: 875 RVA: 0x00010DD9 File Offset: 0x0000EFD9
		public string ConnectionName { get; set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600036C RID: 876 RVA: 0x00010DE2 File Offset: 0x0000EFE2
		public IStatistics Statistics
		{
			get
			{
				return this.m_statistics;
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00010DEA File Offset: 0x0000EFEA
		public SqlFuzzyQuery()
		{
			this.SqlSchemaName = "dbo";
			this.TableNamePrefix = "SqlFuzzyQuery_";
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00010E13 File Offset: 0x0000F013
		public ITokenIdProvider TokenIdProvider(ISession session)
		{
			return (session as SqlFuzzyQuery.Session).m_sessionIdProvider;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00010E20 File Offset: 0x0000F020
		public static DataTable GetMatchResultSchema(Lookup lookupDefinition, RecordBinding leftRecordBinding, RecordBinding rightRecordBinding)
		{
			DataTable dataTable = FuzzyQuery.GenerateOutputSchemaForQuery(leftRecordBinding.Schema, rightRecordBinding.Schema);
			return new MatchResultsReader(leftRecordBinding.Schema, rightRecordBinding.Schema, dataTable).GetSchemaTable();
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00010E58 File Offset: 0x0000F058
		public ISession CreateSession(FuzzyLookup fuzzyLookup, IDomainManager domainManager, ITokenIdProvider tokenIdProvider, IConnectionManager connectionManager)
		{
			if (string.IsNullOrEmpty(this.LookupName))
			{
				this.Lookup = fuzzyLookup.IndexDefinition.Lookups[0];
			}
			else
			{
				this.Lookup = fuzzyLookup.IndexDefinition.Lookups[this.LookupName];
			}
			if (this.LeftRecordBinding == null)
			{
				this.LeftRecordBinding = fuzzyLookup.IndexDefinition.RecordBinding;
			}
			FuzzyLookupDefinition fuzzyLookupDefinition = new FuzzyLookupDefinition(this.LeftRecordBinding, new string[0]);
			fuzzyLookupDefinition.Lookups.Add(new Lookup(this.Lookup)
			{
				LookupId = 0
			});
			SqlStateManager sqlStateManager = new SqlStateManager
			{
				ConnectionName = this.ConnectionName,
				SqlSchemaName = this.SqlSchemaName,
				TableNamePrefix = this.TableNamePrefix,
				UseTemporaryTables = true,
				OverwriteExistingTables = true,
				MemoryLimit = 0L,
				EnableReferenceContextCaching = true,
				ConnectionManager = connectionManager
			};
			sqlStateManager.Initialize(fuzzyLookupDefinition, null);
			SqlFuzzyQuery.Session session = new SqlFuzzyQuery.Session
			{
				m_fuzzyLookup = fuzzyLookup,
				m_connectionManager = connectionManager,
				m_connection = connectionManager.GetConnection(this.ConnectionName),
				m_leftStateManager = sqlStateManager,
				m_customDomainManager = domainManager,
				m_sessionIdProvider = new SessionTokenIdProvider(tokenIdProvider)
			};
			session.m_leftUpdateContext = new IndexUpdateContext(this.LeftRecordBinding, new Lookup[] { this.Lookup }, session.m_customDomainManager, session.m_sessionIdProvider, session.m_sessionIdProvider, JoinSide.Left);
			if (this.Threshold != null && session.m_leftUpdateContext.m_lookupUpdateContexts[0].SignatureGenerator is IThreshold)
			{
				(session.m_leftUpdateContext.m_lookupUpdateContexts[0].SignatureGenerator as IThreshold).Threshold = this.Threshold.Value;
			}
			if (this.Comparer == null)
			{
				if (this.Lookup.Comparer != null)
				{
					session.m_comparer = (IFuzzyComparer)this.Lookup.Comparer.CreateInstance();
					session.m_comparer.Initialize(this.LeftRecordBinding, fuzzyLookup.IndexDefinition.RecordBinding, this.Lookup.Domains, this.Lookup.ExactMatchDomains);
				}
			}
			else
			{
				session.m_comparer = this.Comparer;
			}
			if (this.Threshold != null && this.Comparer is IThreshold)
			{
				(this.Comparer as IThreshold).Threshold = this.Threshold.Value;
			}
			session.m_comparerSession = this.Comparer.CreateSession(domainManager, session.m_sessionIdProvider);
			if (session.m_leftStateManager != null)
			{
				session.m_leftStateManagerSession = session.m_leftStateManager.CreateSession(connectionManager);
			}
			else if (session.m_leftStateManager != null)
			{
				session.m_leftStateManagerSession = ((ISessionable)session.m_leftStateManager).CreateSession();
			}
			DataTable dataTable = FuzzyQuery.GenerateOutputSchemaForQuery(this.LeftRecordBinding.Schema, session.m_fuzzyLookup.IndexDefinition.RecordBinding.Schema);
			session.m_matchResultsReader = new MatchResultsReader(this.LeftRecordBinding.Schema, session.m_fuzzyLookup.IndexDefinition.RecordBinding.Schema, dataTable);
			return session;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00011160 File Offset: 0x0000F360
		public static string CreateJoinQuery(SqlName leftDataTableName, SqlName leftSignatureTableName, SqlName leftContextTableName, int leftLookupId, SqlName rightDataTableName, SqlName rightSignatureTableName, SqlName rightContextTableName, int rightLookupId, bool enableReferenceContextCaching)
		{
			if (enableReferenceContextCaching)
			{
				return string.Format("\r\n                        select l.*, r.*, lc.RecordContext, rc.RecordContext\r\n                        from {0} as l\r\n                        join \r\n                        (\r\n                          select distinct ls.RecordId as r1, rs.RecordId as r2\r\n                          from {1} ls\r\n                          join {3} rs\r\n                          on ls.Signature=rs.Signature and ls.LookupId={7} and rs.LookupId={6} and ls.HashTableId=rs.HashTableId \r\n                        ) as a\r\n                        on l.RID=a.r1 \r\n                        join {2} as r\r\n                        on r.RID=a.r2\r\n                        join {4} as lc\r\n                        on lc.LID={7} and lc.RID=a.r1\r\n                        join {5} as rc\r\n                        on rc.LID={6} and rc.RID=a.r2\r\n                        order by l.RID", new object[] { leftDataTableName.QualifiedName, leftSignatureTableName.QualifiedName, rightDataTableName.QualifiedName, rightSignatureTableName.QualifiedName, leftContextTableName.QualifiedName, rightContextTableName.QualifiedName, rightLookupId, leftLookupId });
			}
			return string.Format("\r\n                        select l.*, r.*\r\n                        from {0} as l\r\n                        join \r\n                        (\r\n                          select distinct ls.RecordId as r1, rs.RecordId as r2\r\n                          from {1} ls\r\n                          join {3} rs\r\n                          on ls.Signature=rs.Signature and rs.LookupId={4} and ls.HashTableId=rs.HashTableId\r\n                        ) as a\r\n                        on l.RID=a.r1 \r\n                        join {2} as r\r\n                        on r.RID=a.r2                        \r\n                        order by l.RID", new object[] { leftDataTableName.QualifiedName, leftSignatureTableName.QualifiedName, rightDataTableName.QualifiedName, rightSignatureTableName.QualifiedName, rightLookupId });
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0001120E File Offset: 0x0000F40E
		public IEnumerable<SqlFuzzyQuery.SqlMatchResult> Match(ISession _session, IDataReader leftRecords)
		{
			SqlFuzzyQuery.Session session = (SqlFuzzyQuery.Session)_session;
			session.Reset();
			bool flag = true;
			if (this.m_statistics.EnableTimers)
			{
				this.m_statistics.TotalQueryTime.Start();
			}
			using (SqlCommand command = session.m_connection.CreateCommand())
			{
				command.CommandTimeout = session.m_connection.ConnectionTimeout;
				if (this.m_statistics.EnableTimers)
				{
					this.m_statistics.LeftIndexBuildTime.Start();
				}
				if (!flag)
				{
					session.StateManagerUpdateContext = session.m_leftStateManager.BeginUpdate(leftRecords.GetSchemaTable());
					while (leftRecords.Read())
					{
						session.m_leftUpdateContext.AddRecord(session.m_leftStateManager, session.StateManagerUpdateContext, leftRecords);
					}
					session.m_leftStateManager.EndUpdate(session.StateManagerUpdateContext);
				}
				else
				{
					session.StateManagerUpdateContext = session.m_leftStateManager.BeginUpdate(leftRecords.GetSchemaTable());
					session.m_leftStateManager.EndUpdate(session.StateManagerUpdateContext);
				}
				if (this.m_statistics.EnableTimers)
				{
					this.m_statistics.LeftIndexBuildTime.Stop();
				}
				bool enableReferenceContextCaching = session.m_fuzzyLookup.StateManager.EnableReferenceContextCaching;
				command.CommandTimeout = session.m_connection.ConnectionTimeout;
				command.CommandText = SqlFuzzyQuery.CreateJoinQuery(session.m_leftStateManager.m_sqlRefStore.m_tableName, session.m_leftStateManager.m_sqlRidStore.m_tableName, session.m_leftStateManager.m_sqlContextStore.m_tableName, 0, (session.m_fuzzyLookup.StateManager as SqlStateManager).m_sqlRefStore.m_tableName, (session.m_fuzzyLookup.StateManager as SqlStateManager).m_sqlRidStore.m_tableName, (session.m_fuzzyLookup.StateManager as SqlStateManager).m_sqlContextStore.m_tableName, this.Lookup.LookupId, enableReferenceContextCaching);
				if (this.m_statistics.EnableTimers)
				{
					this.m_statistics.SqlJoinQueryTime.Start();
				}
				using (IDataReader joinReader = command.ExecuteReader())
				{
					if (this.m_statistics.EnableTimers)
					{
						this.m_statistics.SqlJoinQueryTime.Stop();
					}
					DataTable schemaTable = joinReader.GetSchemaTable();
					OffsetDataRecord leftRecord = new OffsetDataRecord(schemaTable, 0, leftRecords.FieldCount);
					OffsetDataRecord rightRecord = new OffsetDataRecord(schemaTable, leftRecords.FieldCount + 1, session.m_fuzzyLookup.IndexDefinition.RecordBinding.Schema.Rows.Count);
					leftRecord.SetRecord(joinReader);
					rightRecord.SetRecord(joinReader);
					while (joinReader.Read())
					{
						this.m_statistics.ReferenceComparisons += 1L;
						ComparisonResult comparisonResult;
						if (SqlFuzzyQuery.CompareJoinRecords(session.tranMatchAllocator, session.intAllocator, session.byteAllocator, joinReader, leftRecord, rightRecord, session.m_comparerSession, session.m_comparer, session.m_sessionIdProvider, enableReferenceContextCaching, this.m_statistics, out comparisonResult))
						{
							this.m_statistics.MatchesReturned += 1L;
							yield return new SqlFuzzyQuery.SqlMatchResult
							{
								LeftRecord = leftRecord,
								RightRecord = rightRecord,
								ComparisonResult = comparisonResult
							};
						}
						yield return new SqlFuzzyQuery.SqlMatchResult
						{
							LeftRecord = leftRecord,
							RightRecord = rightRecord,
							ComparisonResult = new ComparisonResult()
						};
					}
					leftRecord = null;
					rightRecord = null;
				}
				IDataReader joinReader = null;
			}
			SqlCommand command = null;
			if (this.m_statistics.EnableTimers)
			{
				this.m_statistics.TotalQueryTime.Stop();
			}
			yield break;
			yield break;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0001122C File Offset: 0x0000F42C
		public static bool CompareJoinRecords(ISegmentAllocator<WeightedTransformationMatch> tranMatchAllocator, ISegmentAllocator<int> intAllocator, ISegmentAllocator<byte> byteAllocator, IDataRecord joinRecord, OffsetDataRecord leftRecord, OffsetDataRecord rightRecord, ISession comparerSession, IFuzzyComparer comparer, ITokenIdProvider tokenIdProvider, bool enableReferenceContextCaching, SqlFuzzyQuery.SqlFuzzyQueryStatistics statistics, out ComparisonResult comparisonResult)
		{
			comparerSession.Reset();
			bool flag;
			if (enableReferenceContextCaching)
			{
				RecordContext recordContext = new RecordContext(new BinaryReader(new MemoryStream((byte[])joinRecord[joinRecord.FieldCount - 2])), intAllocator, byteAllocator);
				RecordContext recordContext2 = new RecordContext(new BinaryReader(new MemoryStream((byte[])joinRecord[joinRecord.FieldCount - 1])), intAllocator, byteAllocator);
				comparer.ResetLeftRecord(comparerSession, recordContext);
				comparer.ResetRightRecord(comparerSession, recordContext2);
				flag = comparer.Compare(comparerSession, out comparisonResult);
			}
			else
			{
				flag = comparer.Compare(comparerSession, leftRecord, rightRecord, out comparisonResult);
			}
			if (flag)
			{
				statistics.LeftTransformationCount += (long)comparisonResult.LeftRecordContext.TransformationMatchList.Count;
				statistics.RightTransformationCount += (long)comparisonResult.RightRecordContext.TransformationMatchList.Count;
			}
			return flag;
		}

		// Token: 0x04000125 RID: 293
		private SqlFuzzyQuery.SqlFuzzyQueryStatistics m_statistics = new SqlFuzzyQuery.SqlFuzzyQueryStatistics();

		// Token: 0x02000147 RID: 327
		[Serializable]
		internal class SqlFuzzyQueryStatistics : StatisticsBase, IDeserializationCallback
		{
			// Token: 0x06000C6D RID: 3181 RVA: 0x00036038 File Offset: 0x00034238
			void IDeserializationCallback.OnDeserialization(object sender)
			{
				this.TotalQueryTime = new Stopwatch();
				this.LeftIndexBuildTime = new Stopwatch();
				this.SqlJoinQueryTime = new Stopwatch();
				this.ComparerTime = new Stopwatch();
				this.SortTime = new Stopwatch();
			}

			// Token: 0x06000C6E RID: 3182 RVA: 0x00036074 File Offset: 0x00034274
			public override void Reset()
			{
				this.LeftTransformationCount = 0L;
				this.RightTransformationCount = 0L;
				this.ReferenceComparisons = 0L;
				this.MatchesReturned = 0L;
				if (base.EnableTimers)
				{
					this.TotalQueryTime.Reset();
					this.LeftIndexBuildTime.Reset();
					this.SqlJoinQueryTime.Reset();
					this.ComparerTime.Reset();
					this.SortTime.Reset();
				}
			}

			// Token: 0x0400054F RID: 1359
			[Statistic(0)]
			public long LeftTransformationCount;

			// Token: 0x04000550 RID: 1360
			[Statistic(0)]
			public long RightTransformationCount;

			// Token: 0x04000551 RID: 1361
			[Statistic(0)]
			public long ReferenceComparisons;

			// Token: 0x04000552 RID: 1362
			[Statistic(0)]
			public long MatchesReturned;

			// Token: 0x04000553 RID: 1363
			[NonSerialized]
			public Stopwatch TotalQueryTime = new Stopwatch();

			// Token: 0x04000554 RID: 1364
			[NonSerialized]
			public Stopwatch LeftIndexBuildTime = new Stopwatch();

			// Token: 0x04000555 RID: 1365
			[NonSerialized]
			public Stopwatch SqlJoinQueryTime = new Stopwatch();

			// Token: 0x04000556 RID: 1366
			[NonSerialized]
			public Stopwatch ComparerTime = new Stopwatch();

			// Token: 0x04000557 RID: 1367
			[NonSerialized]
			public Stopwatch SortTime = new Stopwatch();
		}

		// Token: 0x02000148 RID: 328
		private class Session : ISession
		{
			// Token: 0x06000C70 RID: 3184 RVA: 0x00036120 File Offset: 0x00034320
			public void Reset()
			{
				this.m_sessionIdProvider.Reset();
				if (this.m_comparerSession != null)
				{
					this.m_comparerSession.Reset();
				}
				if (this.m_leftStateManagerSession != null)
				{
					this.m_leftStateManagerSession.Reset();
				}
				this.intAllocator.Reset();
				this.byteAllocator.Reset();
				this.tranMatchAllocator.Reset();
			}

			// Token: 0x06000C71 RID: 3185 RVA: 0x00036180 File Offset: 0x00034380
			public void Dispose()
			{
				if (this.m_comparerSession != null && this.m_comparerSession is IDisposable)
				{
					(this.m_comparerSession as IDisposable).Dispose();
				}
				this.m_comparerSession = null;
				if (this.m_leftStateManagerSession != null && this.m_leftStateManagerSession is IDisposable)
				{
					(this.m_leftStateManagerSession as IDisposable).Dispose();
				}
				this.m_leftStateManagerSession = null;
				this.m_connectionManager = null;
			}

			// Token: 0x04000558 RID: 1368
			public FuzzyLookup m_fuzzyLookup;

			// Token: 0x04000559 RID: 1369
			public IConnectionManager m_connectionManager;

			// Token: 0x0400055A RID: 1370
			public SqlConnection m_connection;

			// Token: 0x0400055B RID: 1371
			public SqlStateManager m_leftStateManager;

			// Token: 0x0400055C RID: 1372
			public ISession m_leftStateManagerSession;

			// Token: 0x0400055D RID: 1373
			public IndexUpdateContext m_leftUpdateContext;

			// Token: 0x0400055E RID: 1374
			public IUpdateContext StateManagerUpdateContext;

			// Token: 0x0400055F RID: 1375
			public IDomainManager m_customDomainManager;

			// Token: 0x04000560 RID: 1376
			public SessionTokenIdProvider m_sessionIdProvider;

			// Token: 0x04000561 RID: 1377
			public IFuzzyComparer m_comparer;

			// Token: 0x04000562 RID: 1378
			public ISession m_comparerSession;

			// Token: 0x04000563 RID: 1379
			public MatchResultsReader m_matchResultsReader;

			// Token: 0x04000564 RID: 1380
			public BlockedSegmentArray<int> intAllocator = new BlockedSegmentArray<int>();

			// Token: 0x04000565 RID: 1381
			public BlockedSegmentArray<byte> byteAllocator = new BlockedSegmentArray<byte>();

			// Token: 0x04000566 RID: 1382
			public BlockedSegmentArray<WeightedTransformationMatch> tranMatchAllocator = new BlockedSegmentArray<WeightedTransformationMatch>();
		}

		// Token: 0x02000149 RID: 329
		public class SqlMatchResult
		{
			// Token: 0x04000567 RID: 1383
			public IDataRecord LeftRecord;

			// Token: 0x04000568 RID: 1384
			public IDataRecord RightRecord;

			// Token: 0x04000569 RID: 1385
			public ComparisonResult ComparisonResult;
		}
	}
}
