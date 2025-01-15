using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x02000170 RID: 368
	internal enum QuirksMode : byte
	{
		// Token: 0x040009D4 RID: 2516
		Off,
		// Token: 0x040009D5 RID: 2517
		Limited,
		// Token: 0x040009D6 RID: 2518
		[DomDescription("BackCompat")]
		On
	}
}
