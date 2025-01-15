using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Portal.Interfaces.Configuration
{
	// Token: 0x020000C7 RID: 199
	public interface IOAuthClientConfigurationService
	{
		// Token: 0x060005AA RID: 1450
		bool IsScriptRequest(string path);

		// Token: 0x060005AB RID: 1451
		string GetConfigInfoScript();

		// Token: 0x060005AC RID: 1452
		bool IsOAuthRequired();

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060005AD RID: 1453
		IOAuthConfiguration Configuration { get; }
	}
}
