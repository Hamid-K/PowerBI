using System;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x02000023 RID: 35
	[Flags]
	public enum LuciaSessionOptions
	{
		// Token: 0x040000B8 RID: 184
		Default = 0,
		// Token: 0x040000B9 RID: 185
		Emulation = 1,
		// Token: 0x040000BA RID: 186
		LiveConnectToOnPremAS = 2,
		// Token: 0x040000BB RID: 187
		LiveConnectToPBIService = 4,
		// Token: 0x040000BC RID: 188
		LiveConnectToPBIServiceOnPrem = 8,
		// Token: 0x040000BD RID: 189
		AllowNonOverlapPartialMatch = 16
	}
}
