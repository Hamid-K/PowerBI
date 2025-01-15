using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000C4 RID: 196
	[Flags]
	internal enum SubDmlFlags
	{
		// Token: 0x040005E0 RID: 1504
		None = 0,
		// Token: 0x040005E1 RID: 1505
		InsideSubDml = 1,
		// Token: 0x040005E2 RID: 1506
		SelectNotForInsert = 2,
		// Token: 0x040005E3 RID: 1507
		MergeUsing = 4,
		// Token: 0x040005E4 RID: 1508
		UpdateDeleteFrom = 8
	}
}
