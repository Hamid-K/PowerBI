using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200016D RID: 365
	[Flags]
	[Serializable]
	internal enum SetOffsets
	{
		// Token: 0x040018CE RID: 6350
		None = 0,
		// Token: 0x040018CF RID: 6351
		Select = 1,
		// Token: 0x040018D0 RID: 6352
		From = 2,
		// Token: 0x040018D1 RID: 6353
		Order = 4,
		// Token: 0x040018D2 RID: 6354
		Compute = 8,
		// Token: 0x040018D3 RID: 6355
		Table = 16,
		// Token: 0x040018D4 RID: 6356
		Procedure = 32,
		// Token: 0x040018D5 RID: 6357
		Execute = 64,
		// Token: 0x040018D6 RID: 6358
		Statement = 128,
		// Token: 0x040018D7 RID: 6359
		Param = 256
	}
}
