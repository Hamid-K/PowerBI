using System;
using System.Security.Principal;

namespace Microsoft.ReportingServices.Portal.Interfaces.SoapProxy
{
	// Token: 0x02000086 RID: 134
	public interface ISoapRSExecutionProxy
	{
		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000415 RID: 1045
		// (set) Token: 0x06000416 RID: 1046
		int Timeout { get; set; }

		// Token: 0x06000417 RID: 1047
		RenderResults Render(IPrincipal userPrincipal, string format, string deviceInfo);

		// Token: 0x06000418 RID: 1048
		void LoadReport(IPrincipal userPrincipal, string report, string historyId);
	}
}
