using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library.ConnectionPool
{
	// Token: 0x02000351 RID: 849
	internal static class ConnectionPoolManagerProvider
	{
		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06001C11 RID: 7185 RVA: 0x00071EE2 File Offset: 0x000700E2
		internal static IConnectionPoolManager ConnectionPoolManager
		{
			get
			{
				if (ConnectionPoolManagerProvider.m_connectionPoolManager == null)
				{
					ConnectionPoolManagerProvider.m_connectionPoolManager = new ConnectionPoolManagerProvider.DefaultConnectionPoolManager();
				}
				return ConnectionPoolManagerProvider.m_connectionPoolManager;
			}
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x00071EFA File Offset: 0x000700FA
		internal static void InitializeGlobalConnectionPoolManager()
		{
			ConnectionPoolManagerProvider.m_connectionPoolManager = new ConnectionPoolManagerProvider.GlobalConnectionPoolManager();
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x00071F06 File Offset: 0x00070106
		internal static void CloseConnections()
		{
			ConnectionPoolManagerProvider.m_connectionPoolManager.CloseConnections();
		}

		// Token: 0x04000B97 RID: 2967
		private static IConnectionPoolManager m_connectionPoolManager;

		// Token: 0x020004F2 RID: 1266
		private sealed class DefaultConnectionPoolManager : IConnectionPoolManager
		{
			// Token: 0x060024BB RID: 9403 RVA: 0x00086E06 File Offset: 0x00085006
			public IDbConnectionPool CreateConnectionPool(global::System.Action onCloseCallBack)
			{
				return new DbConnectionPool(onCloseCallBack);
			}

			// Token: 0x060024BC RID: 9404 RVA: 0x00005BF2 File Offset: 0x00003DF2
			public void CloseConnections()
			{
			}
		}

		// Token: 0x020004F3 RID: 1267
		private sealed class GlobalConnectionPoolManager : IConnectionPoolManager
		{
			// Token: 0x060024BE RID: 9406 RVA: 0x00086E0E File Offset: 0x0008500E
			internal GlobalConnectionPoolManager()
			{
				this.m_globalConnectionPool = new GlobalDbConnectionPool();
			}

			// Token: 0x060024BF RID: 9407 RVA: 0x00086E21 File Offset: 0x00085021
			public IDbConnectionPool CreateConnectionPool(global::System.Action onCloseCallBack)
			{
				return this.m_globalConnectionPool;
			}

			// Token: 0x060024C0 RID: 9408 RVA: 0x00086E29 File Offset: 0x00085029
			public void CloseConnections()
			{
				this.m_globalConnectionPool.CloseConnections();
			}

			// Token: 0x040011A6 RID: 4518
			private GlobalDbConnectionPool m_globalConnectionPool;
		}
	}
}
