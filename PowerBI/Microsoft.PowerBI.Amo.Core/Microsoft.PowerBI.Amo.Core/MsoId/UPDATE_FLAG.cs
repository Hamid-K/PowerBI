using System;

namespace Microsoft.AnalysisServices.MsoId
{
	// Token: 0x02000125 RID: 293
	[Flags]
	internal enum UPDATE_FLAG
	{
		// Token: 0x04000A4C RID: 2636
		UPDATE_FLAG_NONE = 0,
		// Token: 0x04000A4D RID: 2637
		UPDATE_FLAG_ALL_BIT = 15,
		// Token: 0x04000A4E RID: 2638
		DEFAULT_UPDATE_POLICY = 0,
		// Token: 0x04000A4F RID: 2639
		OFFLINE_MODE_ALLOWED = 1,
		// Token: 0x04000A50 RID: 2640
		NO_UI = 2,
		// Token: 0x04000A51 RID: 2641
		SKIP_CONNECTION_CHECK = 4,
		// Token: 0x04000A52 RID: 2642
		SET_EXTENDED_ERROR = 8,
		// Token: 0x04000A53 RID: 2643
		SEND_VERSION = 16,
		// Token: 0x04000A54 RID: 2644
		UPDATE_DEFAULT = 0
	}
}
