using System;

namespace System.Text.Json.Serialization
{
	// Token: 0x02000069 RID: 105
	[Flags]
	public enum JsonNumberHandling
	{
		// Token: 0x040002B1 RID: 689
		Strict = 0,
		// Token: 0x040002B2 RID: 690
		AllowReadingFromString = 1,
		// Token: 0x040002B3 RID: 691
		WriteAsString = 2,
		// Token: 0x040002B4 RID: 692
		AllowNamedFloatingPointLiterals = 4
	}
}
