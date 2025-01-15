using System;

namespace Microsoft.Cloud.Platform.IfxAuditing
{
	// Token: 0x0200032B RID: 811
	public enum CallerIdentityType
	{
		// Token: 0x0400084B RID: 2123
		UPN = 1,
		// Token: 0x0400084C RID: 2124
		PUID,
		// Token: 0x0400084D RID: 2125
		ObjectID,
		// Token: 0x0400084E RID: 2126
		Certificate,
		// Token: 0x0400084F RID: 2127
		Claim,
		// Token: 0x04000850 RID: 2128
		Username,
		// Token: 0x04000851 RID: 2129
		KeyName,
		// Token: 0x04000852 RID: 2130
		SubscriptionID,
		// Token: 0x04000853 RID: 2131
		ApplicationID
	}
}
