using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data.Sql;
using Microsoft.DataIntegration.FuzzyMatchingCommon.IO;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000089 RID: 137
	[Serializable]
	public sealed class SqlStateManager : MainMemoryStateManager, IFuzzyLookupStateManager, IRecordContextCache, IInvertedIndexUpdate, IRecordWithIdUpdate, IRecordWithIdLookup, IInvertedIndexLookup, IMemoryUsage, IMemoryLimit, IFuzzyLookupStateManagerInitialize, IRawSerializable, ISerializable, ISessionable
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x00019210 File Offset: 0x00017410
		// (set) Token: 0x0600058A RID: 1418 RVA: 0x00019218 File Offset: 0x00017418
		public IConnectionManager ConnectionManager { get; set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x00019221 File Offset: 0x00017421
		// (set) Token: 0x0600058C RID: 1420 RVA: 0x00019229 File Offset: 0x00017429
		public string ConnectionName { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x00019232 File Offset: 0x00017432
		// (set) Token: 0x0600058E RID: 1422 RVA: 0x0001923A File Offset: 0x0001743A
		public bool UseTemporaryTables { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x00019243 File Offset: 0x00017443
		// (set) Token: 0x06000590 RID: 1424 RVA: 0x0001924B File Offset: 0x0001744B
		public string SqlSchemaName { get; set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x00019254 File Offset: 0x00017454
		// (set) Token: 0x06000592 RID: 1426 RVA: 0x0001925C File Offset: 0x0001745C
		public string TableNamePrefix { get; set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x00019265 File Offset: 0x00017465
		// (set) Token: 0x06000594 RID: 1428 RVA: 0x0001926D File Offset: 0x0001746D
		public bool OverwriteExistingTables { get; set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x00019276 File Offset: 0x00017476
		// (set) Token: 0x06000596 RID: 1430 RVA: 0x0001927E File Offset: 0x0001747E
		public bool WarmCachesOnDeserialization { get; set; }

		// Token: 0x06000597 RID: 1431 RVA: 0x00019287 File Offset: 0x00017487
		public SqlStateManager()
		{
			this.UseTemporaryTables = false;
			this.SqlSchemaName = "dbo";
			this.TableNamePrefix = "FuzzyMatching";
			this.OverwriteExistingTables = false;
			this.WarmCachesOnDeserialization = true;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x000192BA File Offset: 0x000174BA
		private void OpenConnections()
		{
			if (this.ConnectionManager == null)
			{
				throw new InvalidOperationException("Must set ConnectionManager.");
			}
			if (this.m_indexWriteConnection == null)
			{
				this.m_indexWriteConnection = this.ConnectionManager.GetConnection(this.ConnectionName);
			}
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x000192EE File Offset: 0x000174EE
		private void CloseConnections()
		{
			this.m_indexWriteConnection = null;
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x000192F8 File Offset: 0x000174F8
		public new void Initialize(FuzzyLookupDefinition indexDefinition, IConnectionManager connectionManager)
		{
			base.Initialize(indexDefinition, null);
			this.ConnectionManager = connectionManager;
			this.m_signatureFilter = new FastIntHashSet();
			this.m_refRecordStore = new MainMemoryRecordStore(indexDefinition)
			{
				ThrowOnMemoryLimitExceeded = false,
				SerializeRecords = false
			};
			this.m_simpleRidListStore = new SimpleMainMemoryRidListStore(indexDefinition, this.m_statistics);
			this.m_simpleRidListStore.SerializeRidLists = false;
			this.m_sqlRefStore = new SqlRecordStore
			{
				UseTemporaryTables = this.UseTemporaryTables
			};
			this.m_sqlRidStore = new SqlInvertedIndex
			{
				UseTemporaryTables = this.UseTemporaryTables
			};
			this.m_sqlContextStore = new SqlRecordContextStore
			{
				UseTemporaryTables = this.UseTemporaryTables
			};
			this.OpenConnections();
			this.m_sqlRefStore.CreateTables(indexDefinition.RecordBinding.Schema, this.m_indexWriteConnection, this.OverwriteExistingTables, this.SqlSchemaName, this.TableNamePrefix);
			this.m_sqlRidStore.CreateTables(indexDefinition.RecordBinding.Schema, this.m_indexWriteConnection, this.OverwriteExistingTables, this.SqlSchemaName, this.TableNamePrefix);
			this.m_sqlContextStore.CreateTables(indexDefinition.RecordBinding.Schema, this.m_indexWriteConnection, this.OverwriteExistingTables, this.SqlSchemaName, this.TableNamePrefix);
			this.CloseConnections();
			this.SetMemoryLimit(this.m_memoryLimit);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00019440 File Offset: 0x00017640
		private SqlStateManager(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.ConnectionManager = (ConnectionManager)info.GetValue("ConnectionManager", typeof(ConnectionManager));
			this.ConnectionName = (string)info.GetValue("ConnectionName", typeof(string));
			this.UseTemporaryTables = (bool)info.GetValue("UseTemporaryTables", typeof(bool));
			this.SqlSchemaName = (string)info.GetValue("SqlSchemaName", typeof(string));
			this.TableNamePrefix = (string)info.GetValue("TableNamePrefix", typeof(string));
			this.OverwriteExistingTables = (bool)info.GetValue("OverwriteExistingTables", typeof(bool));
			this.WarmCachesOnDeserialization = (bool)info.GetValue("WarmCachesOnDeserialization", typeof(bool));
			this.m_simpleRidListStore = (SimpleMainMemoryRidListStore)info.GetValue("m_ridListStore2", typeof(SimpleMainMemoryRidListStore));
			this.m_sqlRefStore = (SqlRecordStore)info.GetValue("m_sqlRefStore", typeof(SqlRecordStore));
			this.m_sqlRidStore = (SqlInvertedIndex)info.GetValue("m_sqlRidStore", typeof(SqlInvertedIndex));
			this.m_sqlContextStore = (SqlRecordContextStore)info.GetValue("m_sqlContextStore", typeof(SqlRecordContextStore));
			this.m_signatureFilter = (FastIntHashSet)info.GetValue("m_signatureFilter", typeof(FastIntHashSet));
			if (this.WarmCachesOnDeserialization)
			{
				this.OpenConnections();
				this.WarmCaches(this.ConnectionManager.GetConnection(this.ConnectionName));
				this.CloseConnections();
			}
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00019600 File Offset: 0x00017800
		public void DropIndex(SqlConnection connection)
		{
			this.m_sqlRefStore.DropIndex(connection);
			this.m_sqlRidStore.DropIndex(connection);
			this.m_sqlContextStore.DropIndex(connection);
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00019626 File Offset: 0x00017826
		public new ISession CreateSession()
		{
			return new SqlStateManager.ReadSession(this, this.ConnectionManager);
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00019634 File Offset: 0x00017834
		public ISession CreateSession(IConnectionManager connectionManager)
		{
			return new SqlStateManager.ReadSession(this, connectionManager);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00019640 File Offset: 0x00017840
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (this.UseTemporaryTables)
			{
				throw new InvalidOperationException("Serialization is not permitted when SqlIndexOptions.Temporary is true.");
			}
			if (this.m_indexWriteConnection != null)
			{
				throw new SerializationException("Can not serialize while in update mode.  Call EndUpdate() first.");
			}
			base.GetObjectData(info, context);
			info.AddValue("ConnectionManager", this.ConnectionManager);
			info.AddValue("ConnectionName", this.ConnectionName);
			info.AddValue("UseTemporaryTables", this.UseTemporaryTables);
			info.AddValue("SqlSchemaName", this.SqlSchemaName);
			info.AddValue("TableNamePrefix", this.TableNamePrefix);
			info.AddValue("OverwriteExistingTables", this.OverwriteExistingTables);
			info.AddValue("WarmCachesOnDeserialization", this.WarmCachesOnDeserialization);
			info.AddValue("m_ridListStore2", this.m_simpleRidListStore);
			info.AddValue("m_sqlRidStore", this.m_sqlRidStore);
			info.AddValue("m_sqlRefStore", this.m_sqlRefStore);
			info.AddValue("m_sqlContextStore", this.m_sqlContextStore);
			info.AddValue("m_signatureFilter", this.m_signatureFilter);
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00019747 File Offset: 0x00017947
		public override void Serialize(Stream s)
		{
			base.Serialize(s);
			StreamUtilities.WriteInt64(s, s.Position);
			StreamUtilities.WriteInt64(s, s.Position);
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00019768 File Offset: 0x00017968
		public override void Deserialize(Stream s)
		{
			base.Deserialize(s);
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x000197A4 File Offset: 0x000179A4
		public void WarmCaches(SqlConnection connection)
		{
			using (SqlCommand sqlCommand = SqlUtils.CreateSqlCommand(connection))
			{
				sqlCommand.CommandText = string.Format("select * from {0}", this.m_sqlRefStore.m_tableName);
				using (IDataReader dataReader = sqlCommand.ExecuteReader())
				{
					while (dataReader.Read())
					{
						int num = (int)dataReader[this.m_sqlRefStore.m_ridColumnIndex];
						object[] array = new object[dataReader.FieldCount - 1];
						for (int i = 0; i < array.Length; i++)
						{
							array[i] = dataReader[i];
						}
						long memoryUsage = this.m_refRecordStore.MemoryUsage;
						this.m_refRecordStore.AddRecord(num, array);
						if (this.m_refRecordStore.MemoryUsage <= memoryUsage)
						{
							break;
						}
					}
				}
			}
			using (IDataReader dataReader2 = this.m_sqlRidStore.CreateSignaturesReader(connection))
			{
				int num2 = -1;
				int num3 = -1;
				int num4 = -1;
				RidList ridList = default(RidList);
				while (dataReader2.Read())
				{
					if (num4 != dataReader2.GetInt32(0) || num2 != dataReader2.GetInt32(1) || num3 != dataReader2.GetInt32(2))
					{
						if (ridList.Array != null)
						{
							long memoryUsage2 = this.m_simpleRidListStore.MemoryUsage;
							this.m_simpleRidListStore.AddRidList(num2, num3, num4, ridList);
							if (this.m_simpleRidListStore.MemoryUsage <= memoryUsage2)
							{
								ridList.Array = null;
								break;
							}
						}
						num4 = dataReader2.GetInt32(0);
						num2 = dataReader2.GetInt32(1);
						num3 = dataReader2.GetInt32(2);
						ridList = default(RidList);
						ridList.Array = new int[] { dataReader2.GetInt32(3) };
						ridList.Count = 1;
					}
					int num5 = ridList.Count + 1;
					ridList.Count = num5;
					Array.Resize<int>(ref ridList.Array, num5);
					ridList.Array[ridList.Count - 1] = dataReader2.GetInt32(3);
				}
				if (ridList.Array != null)
				{
					this.m_simpleRidListStore.AddRidList(num2, num3, num4, ridList);
				}
			}
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x000199F8 File Offset: 0x00017BF8
		protected override void SetMemoryLimit(long memoryLimit)
		{
			base.SetMemoryLimit(memoryLimit);
			if (this.m_simpleRidListStore != null)
			{
				this.m_simpleRidListStore.MemoryLimit = this.m_ridListStore.MemoryLimit;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x00019A1F File Offset: 0x00017C1F
		public new long MemoryUsage
		{
			get
			{
				return this.m_simpleRidListStore.MemoryUsage + this.m_refRecordStore.MemoryUsage + this.m_refContextCache.MemoryUsage + this.m_signatureFilter.MemoryUsage;
			}
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00019A50 File Offset: 0x00017C50
		public override IUpdateContext BeginUpdate(DataTable schemaTable)
		{
			SqlStateManager.UpdateContext updateContext = new SqlStateManager.UpdateContext();
			updateContext.BaseUpdateContext = base.BeginUpdate(schemaTable);
			this.OpenConnections();
			using (SqlCommand sqlCommand = this.m_indexWriteConnection.CreateCommand())
			{
				sqlCommand.CommandText = "SET XACT_ABORT ON";
				sqlCommand.ExecuteNonQuery();
			}
			updateContext.SqlRidStoreUpdateContext = this.m_sqlRidStore.BeginUpdate(this.m_indexWriteConnection);
			updateContext.SqlRefStoreUpdateContext = this.m_sqlRefStore.BeginUpdate(this.m_indexWriteConnection);
			updateContext.SqlContextStoreUpdateContext = this.m_sqlContextStore.BeginUpdate(this.m_indexWriteConnection);
			return updateContext;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00019AF8 File Offset: 0x00017CF8
		public override void EndUpdate(IUpdateContext _updateContext)
		{
			SqlStateManager.UpdateContext updateContext = (SqlStateManager.UpdateContext)_updateContext;
			base.EndUpdate(updateContext.BaseUpdateContext);
			this.m_sqlRidStore.EndUpdate(updateContext.SqlRidStoreUpdateContext);
			this.m_sqlRefStore.EndUpdate(updateContext.SqlRefStoreUpdateContext);
			this.m_sqlContextStore.EndUpdate(updateContext.SqlContextStoreUpdateContext);
			if (!this.m_simpleRidListStore.AllRidListsInMemory)
			{
				this.m_simpleRidListStore.Clear();
			}
			this.CloseConnections();
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00019B69 File Offset: 0x00017D69
		public void Dispose()
		{
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00019B6C File Offset: 0x00017D6C
		public override void AddRidSignatures(IUpdateContext _updateContext, int rid, int lookupId, int hashTableIndex, IEnumerable<int> signatures)
		{
			this.m_simpleRidListStore.AddRidSignatures(rid, lookupId, hashTableIndex, signatures);
			this.m_sqlRidStore.AddRidSignatures(_updateContext, rid, lookupId, hashTableIndex, signatures);
			if (base.MemoryLimit > 0L)
			{
				foreach (int num in signatures)
				{
					this.m_signatureFilter.Add(SqlStateManager.GetFilterHashCode(lookupId, num));
				}
			}
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00019BF0 File Offset: 0x00017DF0
		public override void RemoveRidSignatures(IUpdateContext _updateContext, int rid, int lookupId, int hashTableIndex, IEnumerable<int> signatures)
		{
			this.m_simpleRidListStore.RemoveRidSignatures(rid, lookupId, hashTableIndex, signatures);
			this.m_sqlRidStore.RemoveRidSignatures(_updateContext, rid, lookupId, hashTableIndex, signatures);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00019C15 File Offset: 0x00017E15
		private static int GetFilterHashCode(int lookupId, int signature)
		{
			signature = lookupId ^ signature;
			if (signature == 0)
			{
				signature = 1;
			}
			return signature;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00019C24 File Offset: 0x00017E24
		public override bool TryGetSignatureRidList(ISession session, int lookupId, int hashTableIndex, int signature, ref int[] ridBuffer, out RidList ridList)
		{
			if (!this.m_simpleRidListStore.TryGetSignatureRidList(null, lookupId, hashTableIndex, signature, ref ridBuffer, out ridList))
			{
				if (!this.m_simpleRidListStore.AllRidListsInMemory && (base.MemoryLimit == 0L || this.m_signatureFilter.Contains(SqlStateManager.GetFilterHashCode(lookupId, signature))))
				{
					this.m_statistics.RidListCacheMisses += 1L;
					if (this.m_sqlRidStore.TryGetSignatureRidList(((SqlStateManager.ReadSession)session).m_ridListStoreSession, lookupId, hashTableIndex, signature, ref ridBuffer, out ridList))
					{
						this.m_simpleRidListStore.AddRidList(lookupId, hashTableIndex, signature, ridList);
						return true;
					}
				}
				ridList = RidList.Empty;
				return false;
			}
			this.m_statistics.RidListCacheHits += 1L;
			return true;
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00019CE3 File Offset: 0x00017EE3
		public override void AddRecord(IUpdateContext _updateContext, IDataRecord record, out int recordId)
		{
			base.AddRecord(_updateContext, record, out recordId);
			this.m_sqlRefStore.AddRecord(record, recordId);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00019CFC File Offset: 0x00017EFC
		public override bool TryRemoveRecord(IUpdateContext _updateContext, IDataRecord record, out int rid)
		{
			if (base.TryRemoveRecord(_updateContext, record, out rid))
			{
				return this.m_sqlRefStore.TryRemoveRecord(rid);
			}
			return this.m_sqlRefStore.TryRemoveRecord(record, out rid);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00019D24 File Offset: 0x00017F24
		public override bool TryGetRecord(ISession session, int rid, ref object[] values)
		{
			SqlStateManager.ReadSession readSession = (SqlStateManager.ReadSession)session;
			if (!base.TryGetRecord(readSession.m_mainMemoryStateManagerSession, rid, ref values))
			{
				this.m_statistics.RightRecordCacheMisses += 1L;
				return this.m_sqlRefStore.TryGetRecord(((SqlStateManager.ReadSession)session).m_referenceStoreSession, rid, ref values);
			}
			this.m_statistics.RightRecordCacheHits += 1L;
			return true;
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00019D8A File Offset: 0x00017F8A
		public override void CacheRecordContext(int rid, int lookupId, RecordContext recordContext)
		{
			if (base.EnableReferenceContextCaching)
			{
				base.CacheRecordContext(rid, lookupId, recordContext);
				this.m_sqlContextStore.AddRecordContext(lookupId, rid, recordContext);
			}
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00019DAB File Offset: 0x00017FAB
		public override bool TryGetRecordContext(ISession session, int rid, int lookupId, out RecordContext recordContext)
		{
			if (base.EnableReferenceContextCaching)
			{
				return base.TryGetRecordContext(session, rid, lookupId, out recordContext) || this.m_sqlContextStore.TryGetRecordContext(((SqlStateManager.ReadSession)session).m_contextStoreSession, lookupId, rid, out recordContext);
			}
			recordContext = null;
			return false;
		}

		// Token: 0x040001DF RID: 479
		private SimpleMainMemoryRidListStore m_simpleRidListStore;

		// Token: 0x040001E0 RID: 480
		internal SqlRecordStore m_sqlRefStore;

		// Token: 0x040001E1 RID: 481
		internal SqlInvertedIndex m_sqlRidStore;

		// Token: 0x040001E2 RID: 482
		internal SqlRecordContextStore m_sqlContextStore;

		// Token: 0x040001E3 RID: 483
		internal FastIntHashSet m_signatureFilter;

		// Token: 0x040001E5 RID: 485
		[NonSerialized]
		private SqlConnection m_indexWriteConnection;

		// Token: 0x02000169 RID: 361
		private class ReadSession : ISession, IDisposable
		{
			// Token: 0x06000CE2 RID: 3298 RVA: 0x00037364 File Offset: 0x00035564
			public ReadSession(SqlStateManager stateManager)
				: this(stateManager, null)
			{
			}

			// Token: 0x06000CE3 RID: 3299 RVA: 0x00037370 File Offset: 0x00035570
			public ReadSession(SqlStateManager stateManager, IConnectionManager connectionManager)
			{
				this.m_stateManager = stateManager;
				this.m_connectionManager = connectionManager;
				this.m_connection = connectionManager.GetConnection(stateManager.ConnectionName);
				this.m_mainMemoryStateManagerSession = this.m_stateManager.CreateSession();
				this.m_referenceStoreSession = this.m_stateManager.m_sqlRefStore.CreateReadSession(this.m_connection);
				this.m_ridListStoreSession = this.m_stateManager.m_sqlRidStore.CreateReadSession(this.m_connection);
				this.m_contextStoreSession = this.m_stateManager.m_sqlContextStore.CreateReadSession(this.m_connection);
			}

			// Token: 0x06000CE4 RID: 3300 RVA: 0x00037408 File Offset: 0x00035608
			public void Reset()
			{
				this.Reset(this.m_connection);
			}

			// Token: 0x06000CE5 RID: 3301 RVA: 0x00037418 File Offset: 0x00035618
			public void Reset(SqlConnection indexReadConnection)
			{
				if (this.m_connection != indexReadConnection)
				{
					this.Dispose();
					this.m_referenceStoreSession = this.m_stateManager.m_sqlRefStore.CreateReadSession(indexReadConnection);
					this.m_ridListStoreSession = this.m_stateManager.m_sqlRidStore.CreateReadSession(indexReadConnection);
					this.m_contextStoreSession = this.m_stateManager.m_sqlContextStore.CreateReadSession(indexReadConnection);
				}
			}

			// Token: 0x06000CE6 RID: 3302 RVA: 0x0003747C File Offset: 0x0003567C
			public void Dispose()
			{
				if (this.m_ridListStoreSession != null && this.m_ridListStoreSession is IDisposable)
				{
					(this.m_ridListStoreSession as IDisposable).Dispose();
					this.m_ridListStoreSession = null;
				}
				if (this.m_referenceStoreSession != null && this.m_ridListStoreSession is IDisposable)
				{
					(this.m_referenceStoreSession as IDisposable).Dispose();
					this.m_referenceStoreSession = null;
				}
				if (this.m_contextStoreSession != null && this.m_contextStoreSession is IDisposable)
				{
					(this.m_contextStoreSession as IDisposable).Dispose();
					this.m_contextStoreSession = null;
				}
			}

			// Token: 0x040005C8 RID: 1480
			private SqlStateManager m_stateManager;

			// Token: 0x040005C9 RID: 1481
			private IConnectionManager m_connectionManager;

			// Token: 0x040005CA RID: 1482
			private SqlConnection m_connection;

			// Token: 0x040005CB RID: 1483
			public ISession m_mainMemoryStateManagerSession;

			// Token: 0x040005CC RID: 1484
			public ISession m_referenceStoreSession;

			// Token: 0x040005CD RID: 1485
			public ISession m_contextStoreSession;

			// Token: 0x040005CE RID: 1486
			public ISession m_ridListStoreSession;
		}

		// Token: 0x0200016A RID: 362
		private class UpdateContext : IUpdateContext
		{
			// Token: 0x040005CF RID: 1487
			public IUpdateContext BaseUpdateContext;

			// Token: 0x040005D0 RID: 1488
			public IUpdateContext SqlRidStoreUpdateContext;

			// Token: 0x040005D1 RID: 1489
			public IUpdateContext SqlRefStoreUpdateContext;

			// Token: 0x040005D2 RID: 1490
			public IUpdateContext SqlContextStoreUpdateContext;
		}
	}
}
