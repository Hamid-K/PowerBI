using System;
using Microsoft.BIServer.HostingEnvironment;

namespace Microsoft.ReportingServices.Portal.Interfaces.Services
{
	// Token: 0x0200008D RID: 141
	public interface IDatabaseService
	{
		// Token: 0x0600045E RID: 1118
		void Init();

		// Token: 0x0600045F RID: 1119
		void EnsureDatabaseAvailable(ILogger logger);
	}
}
