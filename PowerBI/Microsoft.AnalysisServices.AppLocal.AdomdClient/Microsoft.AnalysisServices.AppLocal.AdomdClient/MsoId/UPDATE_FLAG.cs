using System;

namespace Microsoft.AnalysisServices.AdomdClient.MsoId
{
	// Token: 0x02000130 RID: 304
	[Flags]
	internal enum UPDATE_FLAG
	{
		// Token: 0x04000A93 RID: 2707
		UPDATE_FLAG_NONE = 0,
		// Token: 0x04000A94 RID: 2708
		UPDATE_FLAG_ALL_BIT = 15,
		// Token: 0x04000A95 RID: 2709
		DEFAULT_UPDATE_POLICY = 0,
		// Token: 0x04000A96 RID: 2710
		OFFLINE_MODE_ALLOWED = 1,
		// Token: 0x04000A97 RID: 2711
		NO_UI = 2,
		// Token: 0x04000A98 RID: 2712
		SKIP_CONNECTION_CHECK = 4,
		// Token: 0x04000A99 RID: 2713
		SET_EXTENDED_ERROR = 8,
		// Token: 0x04000A9A RID: 2714
		SEND_VERSION = 16,
		// Token: 0x04000A9B RID: 2715
		UPDATE_DEFAULT = 0
	}
}
