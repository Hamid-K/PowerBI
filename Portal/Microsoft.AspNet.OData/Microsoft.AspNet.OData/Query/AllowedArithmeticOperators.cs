using System;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000C9 RID: 201
	[Flags]
	public enum AllowedArithmeticOperators
	{
		// Token: 0x040001A2 RID: 418
		None = 0,
		// Token: 0x040001A3 RID: 419
		Add = 1,
		// Token: 0x040001A4 RID: 420
		Subtract = 2,
		// Token: 0x040001A5 RID: 421
		Multiply = 4,
		// Token: 0x040001A6 RID: 422
		Divide = 8,
		// Token: 0x040001A7 RID: 423
		Modulo = 16,
		// Token: 0x040001A8 RID: 424
		All = 31
	}
}
