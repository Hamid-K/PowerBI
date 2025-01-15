using System;
using Microsoft.ReportingServices.DataProcessing;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200000F RID: 15
	internal interface IDbConnectionPool
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002C RID: 44
		int ConnectionCount { get; }

		// Token: 0x0600002D RID: 45
		IDbConnection GetConnection(ConnectionKey connectionKey);

		// Token: 0x0600002E RID: 46
		bool PoolConnection(IDbPoolableConnection connection, ConnectionKey connectionKey);

		// Token: 0x0600002F RID: 47
		void CloseConnections();
	}
}
