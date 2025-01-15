using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000DE RID: 222
	[Flags]
	internal enum SqlVersionFlags
	{
		// Token: 0x04000934 RID: 2356
		None = 0,
		// Token: 0x04000935 RID: 2357
		TSql80 = 1,
		// Token: 0x04000936 RID: 2358
		TSql90 = 2,
		// Token: 0x04000937 RID: 2359
		TSql100 = 4,
		// Token: 0x04000938 RID: 2360
		TSql110 = 8,
		// Token: 0x04000939 RID: 2361
		TSqlAll = 15,
		// Token: 0x0400093A RID: 2362
		TSql90AndAbove = 14,
		// Token: 0x0400093B RID: 2363
		TSql100AndAbove = 12,
		// Token: 0x0400093C RID: 2364
		TSql110AndAbove = 8,
		// Token: 0x0400093D RID: 2365
		TSqlUnder110 = 7
	}
}
