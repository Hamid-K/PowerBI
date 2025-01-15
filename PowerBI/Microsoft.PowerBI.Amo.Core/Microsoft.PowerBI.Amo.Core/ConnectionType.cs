using System;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200001F RID: 31
	public enum ConnectionType
	{
		// Token: 0x040000A2 RID: 162
		Native,
		// Token: 0x040000A3 RID: 163
		Http,
		// Token: 0x040000A4 RID: 164
		LocalServer,
		// Token: 0x040000A5 RID: 165
		LocalCube,
		// Token: 0x040000A6 RID: 166
		Wcf,
		// Token: 0x040000A7 RID: 167
		[Obsolete("The OnPremFromCloudAccess has been deprecated!")]
		OnPremFromCloudAccess = 6
	}
}
