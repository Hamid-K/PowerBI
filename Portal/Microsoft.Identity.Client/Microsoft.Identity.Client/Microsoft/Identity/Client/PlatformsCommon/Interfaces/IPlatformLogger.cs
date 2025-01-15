using System;

namespace Microsoft.Identity.Client.PlatformsCommon.Interfaces
{
	// Token: 0x020001FE RID: 510
	internal interface IPlatformLogger
	{
		// Token: 0x06001594 RID: 5524
		void Always(string message);

		// Token: 0x06001595 RID: 5525
		void Error(string message);

		// Token: 0x06001596 RID: 5526
		void Warning(string message);

		// Token: 0x06001597 RID: 5527
		void Verbose(string message);

		// Token: 0x06001598 RID: 5528
		void Information(string message);
	}
}
