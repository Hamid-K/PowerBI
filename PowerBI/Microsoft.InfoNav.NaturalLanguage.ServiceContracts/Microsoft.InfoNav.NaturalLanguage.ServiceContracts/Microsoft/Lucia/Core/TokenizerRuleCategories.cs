using System;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200012F RID: 303
	[Flags]
	public enum TokenizerRuleCategories
	{
		// Token: 0x040005EE RID: 1518
		None = 0,
		// Token: 0x040005EF RID: 1519
		Basic = 1,
		// Token: 0x040005F0 RID: 1520
		Quotes = 2,
		// Token: 0x040005F1 RID: 1521
		DateTime = 4,
		// Token: 0x040005F2 RID: 1522
		All = 7
	}
}
