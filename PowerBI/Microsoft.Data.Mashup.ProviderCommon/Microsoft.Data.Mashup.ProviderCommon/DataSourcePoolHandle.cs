using System;
using System.Collections.Generic;
using Microsoft.Mashup.EngineHost;

namespace Microsoft.Data.Mashup.ProviderCommon
{
	// Token: 0x02000005 RID: 5
	internal sealed class DataSourcePoolHandle : IDisposable
	{
		// Token: 0x06000016 RID: 22 RVA: 0x0000242C File Offset: 0x0000062C
		public DataSourcePoolHandle(string dataSourcePool)
		{
			object obj = DataSourcePoolHandle.definitionsLock;
			lock (obj)
			{
				this.dataSourcePool = dataSourcePool;
				this.governanceManager = DataSourcePoolHandle.GetConnectionGovernanceManager(dataSourcePool) ?? new ConnectionGovernanceManager();
				DataSourcePoolHandle.handleManager.RegisterHandle(dataSourcePool, this);
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002494 File Offset: 0x00000694
		~DataSourcePoolHandle()
		{
			this.Dispose(false);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000024C4 File Offset: 0x000006C4
		public static ConnectionGovernanceManager GetConnectionGovernanceManager(string dataSourcePool)
		{
			object obj = DataSourcePoolHandle.definitionsLock;
			ConnectionGovernanceManager connectionGovernanceManager;
			lock (obj)
			{
				List<DataSourcePoolHandle> list;
				if (DataSourcePoolHandle.handleManager.TryGetValues(dataSourcePool, out list))
				{
					connectionGovernanceManager = list[0].ConnectionGovernanceManager;
				}
				else
				{
					connectionGovernanceManager = null;
				}
			}
			return connectionGovernanceManager;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002520 File Offset: 0x00000720
		public string DataSourcePool
		{
			get
			{
				return this.dataSourcePool;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002528 File Offset: 0x00000728
		public ConnectionGovernanceManager ConnectionGovernanceManager
		{
			get
			{
				return this.governanceManager;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002530 File Offset: 0x00000730
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000253C File Offset: 0x0000073C
		private void Dispose(bool disposing)
		{
			object obj = DataSourcePoolHandle.definitionsLock;
			lock (obj)
			{
				DataSourcePoolHandle.handleManager.UnregisterHandle(this.DataSourcePool, this);
			}
			if (disposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x04000006 RID: 6
		private static readonly object definitionsLock = new object();

		// Token: 0x04000007 RID: 7
		private static readonly HandleManager<DataSourcePoolHandle> handleManager = new HandleManager<DataSourcePoolHandle>();

		// Token: 0x04000008 RID: 8
		private readonly string dataSourcePool;

		// Token: 0x04000009 RID: 9
		private readonly ConnectionGovernanceManager governanceManager;
	}
}
