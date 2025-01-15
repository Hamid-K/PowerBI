using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001E4 RID: 484
	[Flags]
	internal enum AutodiscoverEndpoints
	{
		// Token: 0x04000D2C RID: 3372
		None = 0,
		// Token: 0x04000D2D RID: 3373
		Legacy = 1,
		// Token: 0x04000D2E RID: 3374
		Soap = 2,
		// Token: 0x04000D2F RID: 3375
		WsSecurity = 4,
		// Token: 0x04000D30 RID: 3376
		WSSecuritySymmetricKey = 8,
		// Token: 0x04000D31 RID: 3377
		WSSecurityX509Cert = 16,
		// Token: 0x04000D32 RID: 3378
		OAuth = 32
	}
}
