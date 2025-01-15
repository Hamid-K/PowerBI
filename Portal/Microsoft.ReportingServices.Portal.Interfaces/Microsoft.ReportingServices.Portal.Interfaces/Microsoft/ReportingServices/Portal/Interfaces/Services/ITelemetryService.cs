using System;
using System.Security.Principal;
using Model;

namespace Microsoft.ReportingServices.Portal.Interfaces.Services
{
	// Token: 0x02000091 RID: 145
	public interface ITelemetryService
	{
		// Token: 0x06000467 RID: 1127
		TelemetryHostData GetSystemProperties(IIdentity identity);
	}
}
