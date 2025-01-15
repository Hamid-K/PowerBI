using System;
using Microsoft.Identity.Client.Core;

namespace Microsoft.Identity.Client.ApiConfig.Parameters
{
	// Token: 0x020002D4 RID: 724
	internal class AcquireTokenByUsernamePasswordParameters : AbstractAcquireTokenByUsernameParameters, IAcquireTokenParameters
	{
		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001AE5 RID: 6885 RVA: 0x00056F28 File Offset: 0x00055128
		// (set) Token: 0x06001AE6 RID: 6886 RVA: 0x00056F30 File Offset: 0x00055130
		public string Password { get; set; }

		// Token: 0x06001AE7 RID: 6887 RVA: 0x00056F39 File Offset: 0x00055139
		public void LogParameters(ILoggerAdapter logger)
		{
		}
	}
}
