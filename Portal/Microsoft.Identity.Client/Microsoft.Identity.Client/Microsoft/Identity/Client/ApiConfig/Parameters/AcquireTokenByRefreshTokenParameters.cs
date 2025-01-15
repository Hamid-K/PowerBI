using System;
using Microsoft.Identity.Client.Core;

namespace Microsoft.Identity.Client.ApiConfig.Parameters
{
	// Token: 0x020002D3 RID: 723
	internal class AcquireTokenByRefreshTokenParameters : AbstractAcquireTokenConfidentialClientParameters, IAcquireTokenParameters
	{
		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001AE1 RID: 6881 RVA: 0x00056F0D File Offset: 0x0005510D
		// (set) Token: 0x06001AE2 RID: 6882 RVA: 0x00056F15 File Offset: 0x00055115
		public string RefreshToken { get; set; }

		// Token: 0x06001AE3 RID: 6883 RVA: 0x00056F1E File Offset: 0x0005511E
		public void LogParameters(ILoggerAdapter logger)
		{
		}
	}
}
