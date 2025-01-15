using System;

namespace Microsoft.AnalysisServices.AdomdClient.MsoId
{
	// Token: 0x02000130 RID: 304
	[Flags]
	internal enum UPDATE_FLAG
	{
		// Token: 0x04000A86 RID: 2694
		UPDATE_FLAG_NONE = 0,
		// Token: 0x04000A87 RID: 2695
		UPDATE_FLAG_ALL_BIT = 15,
		// Token: 0x04000A88 RID: 2696
		DEFAULT_UPDATE_POLICY = 0,
		// Token: 0x04000A89 RID: 2697
		OFFLINE_MODE_ALLOWED = 1,
		// Token: 0x04000A8A RID: 2698
		NO_UI = 2,
		// Token: 0x04000A8B RID: 2699
		SKIP_CONNECTION_CHECK = 4,
		// Token: 0x04000A8C RID: 2700
		SET_EXTENDED_ERROR = 8,
		// Token: 0x04000A8D RID: 2701
		SEND_VERSION = 16,
		// Token: 0x04000A8E RID: 2702
		UPDATE_DEFAULT = 0
	}
}
