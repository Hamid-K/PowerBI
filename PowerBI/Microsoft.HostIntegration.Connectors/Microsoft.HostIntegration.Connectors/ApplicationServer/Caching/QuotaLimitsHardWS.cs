using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000349 RID: 841
	[Flags]
	internal enum QuotaLimitsHardWS : uint
	{
		// Token: 0x040010D4 RID: 4308
		QUOTA_LIMITS_HARDWS_MIN_ENABLE = 1U,
		// Token: 0x040010D5 RID: 4309
		QUOTA_LIMITS_HARDWS_MIN_DISABLE = 2U,
		// Token: 0x040010D6 RID: 4310
		QUOTA_LIMITS_HARDWS_MAX_ENABLE = 4U,
		// Token: 0x040010D7 RID: 4311
		QUOTA_LIMITS_HARDWS_MAX_DISABLE = 8U
	}
}
