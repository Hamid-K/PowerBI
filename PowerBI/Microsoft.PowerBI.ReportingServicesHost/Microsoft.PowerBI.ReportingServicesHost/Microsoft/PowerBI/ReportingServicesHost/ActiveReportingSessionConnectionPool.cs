using System;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000026 RID: 38
	internal sealed class ActiveReportingSessionConnectionPool : IConnectionPool
	{
		// Token: 0x060000BC RID: 188 RVA: 0x0000393F File Offset: 0x00001B3F
		public ActiveReportingSessionConnectionPool(IReportingSessionProvider reportingSessionProvider, string databaseID)
		{
			this.m_reportingSessionProvider = reportingSessionProvider;
			this.m_databaseID = databaseID;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003955 File Offset: 0x00001B55
		public IDbConnection Get(IDataSourceInfo dataSourceInfo)
		{
			return this.m_reportingSessionProvider.GetActiveSession(this.m_databaseID).GetConnectionPool().Get(dataSourceInfo);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003973 File Offset: 0x00001B73
		public bool Put(IDbConnection connection, IDataSourceInfo dataSourceInfo)
		{
			return this.m_reportingSessionProvider.GetActiveSession(this.m_databaseID).GetConnectionPool().Put(connection, dataSourceInfo);
		}

		// Token: 0x040000C0 RID: 192
		private IReportingSessionProvider m_reportingSessionProvider;

		// Token: 0x040000C1 RID: 193
		private string m_databaseID;
	}
}
