using System;
using Microsoft.ReportingServices.DataProcessing;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200064B RID: 1611
	public interface IDbConnectionPool
	{
		// Token: 0x17002017 RID: 8215
		// (get) Token: 0x060057AE RID: 22446
		int ConnectionCount { get; }

		// Token: 0x060057AF RID: 22447
		IDbConnection GetConnection(ConnectionKey connectionKey);

		// Token: 0x060057B0 RID: 22448
		bool PoolConnection(IDbPoolableConnection connection, ConnectionKey connectionKey);

		// Token: 0x060057B1 RID: 22449
		void CloseConnections();
	}
}
