using System;
using Microsoft.PowerBI.ReportServer.PbixLib.Parsing;

namespace Microsoft.PowerBI.ReportServer.WebApi.Pbix
{
	// Token: 0x02000015 RID: 21
	public interface IPbixTelemetryLogger
	{
		// Token: 0x06000047 RID: 71
		void LogPbixTelemetry(PbixReportElements pbixReportElements);
	}
}
