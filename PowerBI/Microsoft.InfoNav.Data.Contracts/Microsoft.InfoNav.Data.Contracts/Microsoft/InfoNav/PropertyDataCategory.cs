using System;

namespace Microsoft.InfoNav
{
	// Token: 0x02000049 RID: 73
	[Flags]
	public enum PropertyDataCategory
	{
		// Token: 0x040000E5 RID: 229
		None = 0,
		// Token: 0x040000E6 RID: 230
		Month = 1,
		// Token: 0x040000E7 RID: 231
		Year = 2,
		// Token: 0x040000E8 RID: 232
		Decade = 4,
		// Token: 0x040000E9 RID: 233
		Geography = 8,
		// Token: 0x040000EA RID: 234
		Address = 24,
		// Token: 0x040000EB RID: 235
		City = 40,
		// Token: 0x040000EC RID: 236
		Continent = 72,
		// Token: 0x040000ED RID: 237
		Country = 136,
		// Token: 0x040000EE RID: 238
		County = 264,
		// Token: 0x040000EF RID: 239
		Region = 520,
		// Token: 0x040000F0 RID: 240
		PostalCode = 1032,
		// Token: 0x040000F1 RID: 241
		StateOrProvince = 2056,
		// Token: 0x040000F2 RID: 242
		Place = 4104,
		// Token: 0x040000F3 RID: 243
		PartialMatch = 65536
	}
}
