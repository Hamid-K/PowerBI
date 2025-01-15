using System;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000025 RID: 37
	internal sealed class ActiveReportingSessionConnectionFactory : IConnectionFactory
	{
		// Token: 0x060000BA RID: 186 RVA: 0x0000390A File Offset: 0x00001B0A
		public ActiveReportingSessionConnectionFactory(IReportingSessionProvider reportingSessionProvider, string databaseID)
		{
			this.m_reportingSessionProvider = reportingSessionProvider;
			this.m_databaseID = databaseID;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003920 File Offset: 0x00001B20
		public IDbConnection CreateConnection(string dataExtension, string connectionString)
		{
			return this.m_reportingSessionProvider.GetActiveSession(this.m_databaseID).GetConnectionFactory().CreateConnection(dataExtension, connectionString);
		}

		// Token: 0x040000BE RID: 190
		private IReportingSessionProvider m_reportingSessionProvider;

		// Token: 0x040000BF RID: 191
		private string m_databaseID;
	}
}
