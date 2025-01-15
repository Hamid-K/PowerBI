using System;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AEE RID: 2798
	public enum AccountingToken
	{
		// Token: 0x040045B2 RID: 17842
		Unknown,
		// Token: 0x040045B3 RID: 17843
		CicsLuowId,
		// Token: 0x040045B4 RID: 17844
		Os2Default = 4,
		// Token: 0x040045B5 RID: 17845
		DosDefault,
		// Token: 0x040045B6 RID: 17846
		UnixNumericId,
		// Token: 0x040045B7 RID: 17847
		Os400AccountToken = 8,
		// Token: 0x040045B8 RID: 17848
		WindowsDefault,
		// Token: 0x040045B9 RID: 17849
		NtSecurityId = 11,
		// Token: 0x040045BA RID: 17850
		User = 25
	}
}
