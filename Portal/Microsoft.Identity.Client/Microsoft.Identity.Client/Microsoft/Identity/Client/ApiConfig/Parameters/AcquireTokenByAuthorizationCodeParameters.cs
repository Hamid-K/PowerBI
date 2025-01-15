using System;
using Microsoft.Identity.Client.Core;

namespace Microsoft.Identity.Client.ApiConfig.Parameters
{
	// Token: 0x020002D1 RID: 721
	internal class AcquireTokenByAuthorizationCodeParameters : AbstractAcquireTokenConfidentialClientParameters, IAcquireTokenParameters
	{
		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001AD9 RID: 6873 RVA: 0x00056ED7 File Offset: 0x000550D7
		// (set) Token: 0x06001ADA RID: 6874 RVA: 0x00056EDF File Offset: 0x000550DF
		public string AuthorizationCode { get; set; }

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001ADB RID: 6875 RVA: 0x00056EE8 File Offset: 0x000550E8
		// (set) Token: 0x06001ADC RID: 6876 RVA: 0x00056EF0 File Offset: 0x000550F0
		public string PkceCodeVerifier { get; set; }

		// Token: 0x06001ADD RID: 6877 RVA: 0x00056EF9 File Offset: 0x000550F9
		public void LogParameters(ILoggerAdapter logger)
		{
		}
	}
}
