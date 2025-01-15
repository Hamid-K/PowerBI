using System;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000122 RID: 290
	[Flags]
	public enum StemmingOptions
	{
		// Token: 0x040005D7 RID: 1495
		None = 0,
		// Token: 0x040005D8 RID: 1496
		Noun = 1,
		// Token: 0x040005D9 RID: 1497
		Verb = 2,
		// Token: 0x040005DA RID: 1498
		Adjective = 4,
		// Token: 0x040005DB RID: 1499
		Other = 8,
		// Token: 0x040005DC RID: 1500
		All = 15
	}
}
