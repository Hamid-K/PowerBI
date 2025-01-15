using System;

namespace AngleSharp.Css
{
	// Token: 0x0200010A RID: 266
	[Flags]
	internal enum PropertyFlags : byte
	{
		// Token: 0x0400072D RID: 1837
		None = 0,
		// Token: 0x0400072E RID: 1838
		Inherited = 1,
		// Token: 0x0400072F RID: 1839
		Hashless = 2,
		// Token: 0x04000730 RID: 1840
		Unitless = 4,
		// Token: 0x04000731 RID: 1841
		Animatable = 8,
		// Token: 0x04000732 RID: 1842
		Shorthand = 16
	}
}
