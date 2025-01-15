using System;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000045 RID: 69
	internal interface IReportingSessionProvider
	{
		// Token: 0x06000188 RID: 392
		IReportingSession GetActiveSession(string databaseID);
	}
}
