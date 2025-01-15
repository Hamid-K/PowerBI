using System;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B03 RID: 2819
	[Flags]
	public enum OpenOption
	{
		// Token: 0x04004626 RID: 17958
		None = 0,
		// Token: 0x04004627 RID: 17959
		Shared = 2,
		// Token: 0x04004628 RID: 17960
		Exclusive = 4,
		// Token: 0x04004629 RID: 17961
		Browse = 8,
		// Token: 0x0400462A RID: 17962
		Inquire = 32,
		// Token: 0x0400462B RID: 17963
		SaveContext = 128,
		// Token: 0x0400462C RID: 17964
		PassIdentityContext = 256,
		// Token: 0x0400462D RID: 17965
		PassAllContext = 512,
		// Token: 0x0400462E RID: 17966
		SetIdentityContext = 1024,
		// Token: 0x0400462F RID: 17967
		SetAllContext = 2048,
		// Token: 0x04004630 RID: 17968
		FailOnQuiesce = 8192,
		// Token: 0x04004631 RID: 17969
		NoReadAhead = 524288,
		// Token: 0x04004632 RID: 17970
		ReadAhead = 1048576
	}
}
