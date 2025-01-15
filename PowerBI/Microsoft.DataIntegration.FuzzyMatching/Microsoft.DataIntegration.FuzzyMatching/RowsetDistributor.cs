using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;
using Microsoft.SqlServer.Server;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000078 RID: 120
	public class RowsetDistributor : IRowsetDistributor, IRecordContextDistributor
	{
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x0001662B File Offset: 0x0001482B
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x00016633 File Offset: 0x00014833
		public IRowsetManager RowsetManager { get; set; }

		// Token: 0x060004CD RID: 1229 RVA: 0x0001663C File Offset: 0x0001483C
		public RowsetDistributor(IRowsetManager rowsetManager)
		{
			this.RowsetManager = rowsetManager;
			this.ClearRequests();
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00016651 File Offset: 0x00014851
		public void ClearRequests()
		{
			this.m_rowsetRequests = new Dictionary<string, List<RowsetDistributor.RequestInfo>>();
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0001665E File Offset: 0x0001485E
		public void RequestRowset(string rowsetName, IRecordUpdate recordUpdate)
		{
			this.RequestRowset(rowsetName, recordUpdate, RowsetDistributor.RequestOptions.None);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00016669 File Offset: 0x00014869
		public void RequestRowset(string rowsetName, IRecordUpdate recordUpdate, int pass)
		{
			this.RequestRowset(rowsetName, recordUpdate, RowsetDistributor.RequestOptions.None, 1);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00016675 File Offset: 0x00014875
		public void RequestRowset(string rowsetName, IRecordUpdate recordUpdate, RowsetDistributor.RequestOptions requestOptions)
		{
			this.RequestRowset(rowsetName, recordUpdate, requestOptions, 1);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00016684 File Offset: 0x00014884
		public void RequestRowset(string rowsetName, IRecordUpdate recordUpdate, RowsetDistributor.RequestOptions requestOptions, int pass)
		{
			if (this.RowsetManager.Rowsets[rowsetName] != null)
			{
				List<RowsetDistributor.RequestInfo> list;
				if (!this.m_rowsetRequests.TryGetValue(rowsetName, ref list))
				{
					list = new List<RowsetDistributor.RequestInfo>();
					this.m_rowsetRequests.Add(rowsetName, list);
				}
				list.Add(new RowsetDistributor.RequestInfo
				{
					RecordUpdate = recordUpdate,
					RequestOptions = requestOptions,
					Pass = pass
				});
			}
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x000166E8 File Offset: 0x000148E8
		public void RequestRowset(string rowsetName, IRecordContextUpdate recordUpdate)
		{
			this.RequestRowset(rowsetName, recordUpdate, RowsetDistributor.RequestOptions.None);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x000166F4 File Offset: 0x000148F4
		public void RequestRowset(string rowsetName, IRecordContextUpdate recordUpdate, RowsetDistributor.RequestOptions requestOptions)
		{
			if (!string.IsNullOrEmpty(rowsetName) && this.RowsetManager.Rowsets[rowsetName] != null)
			{
				List<RowsetDistributor.RequestInfo> list;
				if (!this.m_rowsetRequests.TryGetValue(rowsetName, ref list))
				{
					list = new List<RowsetDistributor.RequestInfo>();
					this.m_rowsetRequests.Add(rowsetName, list);
				}
				list.Add(new RowsetDistributor.RequestInfo
				{
					RecordContextUpdate = recordUpdate,
					RequestOptions = requestOptions
				});
			}
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00016758 File Offset: 0x00014958
		private static void PopulateIRecordUpdate(IRowsetManager rowsetManager, IDomainManager domainManager, ITokenIdProvider tokenIdProvider, RecordBinding recordBinding, IDataReader referenceReader, params IRecordUpdate[] recordUpdateProviders)
		{
			if (!tokenIdProvider.SupportsGetToken)
			{
				tokenIdProvider = new TransientTokenIdProvider(tokenIdProvider);
			}
			List<IUpdateContext> list = new List<IUpdateContext>();
			for (int i = 0; i < recordUpdateProviders.Length; i++)
			{
				IUpdateContext updateContext = recordUpdateProviders[i].BeginUpdate(referenceReader.GetSchemaTable());
				if (updateContext is IRecordUpdateContextInitialize)
				{
					(updateContext as IRecordUpdateContextInitialize).Initialize(rowsetManager, domainManager, tokenIdProvider, recordBinding);
				}
				list.Add(updateContext);
			}
			IReset reset = tokenIdProvider as IReset;
			while (referenceReader.Read())
			{
				if (reset != null)
				{
					reset.Reset();
				}
				for (int j = 0; j < recordUpdateProviders.Length; j++)
				{
					recordUpdateProviders[j].AddRecord(list[j], referenceReader);
				}
			}
			for (int k = 0; k < recordUpdateProviders.Length; k++)
			{
				recordUpdateProviders[k].EndUpdate(list[k]);
			}
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00016828 File Offset: 0x00014A28
		public void DistributeRowset(ConnectionManager connectionManager, IDomainManager domainManager, ITokenIdProvider tokenIdProvider, IRowsetManager rowsetManager, string rowsetName, IRecordUpdate[] consumers, bool loadSqlRowsetIntoMemory)
		{
			IDataReader dataReader = null;
			SqlCommand sqlCommand = null;
			try
			{
				IRowsetDefinition rowsetDefinition = this.RowsetManager.Rowsets[rowsetName];
				RecordBinding recordBinding;
				this.RowsetManager.TryGetRecordBinding(rowsetName, connectionManager, out recordBinding);
				if (rowsetDefinition != null)
				{
					RowsetDistributor.CreateRowsetReader(rowsetDefinition, connectionManager, loadSqlRowsetIntoMemory, out sqlCommand, out dataReader);
					RowsetDistributor.PopulateIRecordUpdate(rowsetManager, domainManager, tokenIdProvider, recordBinding, dataReader, consumers);
				}
			}
			finally
			{
				if (dataReader != null)
				{
					dataReader.Dispose();
					dataReader = null;
				}
				if (sqlCommand != null)
				{
					sqlCommand.Dispose();
					sqlCommand = null;
				}
			}
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x000168A4 File Offset: 0x00014AA4
		public void DistributeRowsets(ConnectionManager connectionManager, IDomainManager domainManager, ITokenIdProvider tokenIdProvider)
		{
			foreach (KeyValuePair<string, List<RowsetDistributor.RequestInfo>> keyValuePair in this.m_rowsetRequests)
			{
				string key = keyValuePair.Key;
				bool flag = false;
				int num = 1;
				List<IRecordUpdate> list = new List<IRecordUpdate>();
				bool flag2;
				do
				{
					list.Clear();
					flag2 = false;
					foreach (RowsetDistributor.RequestInfo requestInfo in keyValuePair.Value)
					{
						if (requestInfo.Pass > num)
						{
							flag2 = true;
						}
						else if (requestInfo.Pass == num)
						{
							IRecordUpdate recordUpdate = requestInfo.RecordUpdate;
							list.Add(recordUpdate);
							if (requestInfo.RequestOptions == RowsetDistributor.RequestOptions.SimultaneousReadWrite && SqlContext.IsAvailable)
							{
								flag = true;
							}
						}
					}
					if (list.Count > 0)
					{
						this.DistributeRowset(connectionManager, domainManager, tokenIdProvider, this.RowsetManager, key, list.ToArray(), flag);
					}
					num++;
				}
				while (flag2);
			}
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x000169C4 File Offset: 0x00014BC4
		private static void CreateRowsetReader(IRowsetDefinition rowset, ConnectionManager connectionManager, bool loadSqlRowsetIntoMemory, out SqlCommand command, out IDataReader reader)
		{
			command = null;
			reader = rowset.CreateDataReader(connectionManager);
			if (loadSqlRowsetIntoMemory && !(rowset is InlineRowset))
			{
				RowsetDistributor.DataAdapterEx dataAdapterEx = new RowsetDistributor.DataAdapterEx();
				DataTable dataTable = new DataTable();
				dataAdapterEx.Fill(dataTable, reader);
				reader = dataTable.CreateDataReader();
			}
		}

		// Token: 0x040001A4 RID: 420
		private Dictionary<string, List<RowsetDistributor.RequestInfo>> m_rowsetRequests;

		// Token: 0x0200015C RID: 348
		public enum RequestOptions
		{
			// Token: 0x040005B1 RID: 1457
			None,
			// Token: 0x040005B2 RID: 1458
			SimultaneousReadWrite
		}

		// Token: 0x0200015D RID: 349
		private class RequestInfo
		{
			// Token: 0x040005B3 RID: 1459
			public IRecordUpdate RecordUpdate;

			// Token: 0x040005B4 RID: 1460
			public IRecordContextUpdate RecordContextUpdate;

			// Token: 0x040005B5 RID: 1461
			public RowsetDistributor.RequestOptions RequestOptions;

			// Token: 0x040005B6 RID: 1462
			public int Pass = 1;
		}

		// Token: 0x0200015E RID: 350
		private class DataAdapterEx : DataAdapter
		{
			// Token: 0x06000CCD RID: 3277 RVA: 0x00037218 File Offset: 0x00035418
			public int Fill(DataTable dataTable, IDataReader dataReader)
			{
				return base.Fill(dataTable, dataReader);
			}
		}
	}
}
