using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Portal.Interfaces.Services;

namespace Microsoft.ReportingServices.Portal.Services.Services
{
	// Token: 0x02000038 RID: 56
	internal class TelemetryConfigurationFactory : ITelemetryConfigurationFactory
	{
		// Token: 0x06000249 RID: 585 RVA: 0x0000FA51 File Offset: 0x0000DC51
		private ITelemetryConfiguration Create()
		{
			return new RegistryTelemetryConfiguration();
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000FA58 File Offset: 0x0000DC58
		object ITelemetryConfigurationFactory.Create()
		{
			return this.Create();
		}
	}
}
