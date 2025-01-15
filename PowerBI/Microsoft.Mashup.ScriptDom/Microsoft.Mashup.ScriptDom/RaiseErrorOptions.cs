using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000167 RID: 359
	[Flags]
	[Serializable]
	internal enum RaiseErrorOptions
	{
		// Token: 0x040018BC RID: 6332
		None = 0,
		// Token: 0x040018BD RID: 6333
		Log = 1,
		// Token: 0x040018BE RID: 6334
		NoWait = 2,
		// Token: 0x040018BF RID: 6335
		SetError = 4
	}
}
