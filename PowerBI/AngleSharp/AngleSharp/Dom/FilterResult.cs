using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x0200016C RID: 364
	[DomName("NodeFilter")]
	public enum FilterResult : byte
	{
		// Token: 0x040009B0 RID: 2480
		[DomName("FILTER_ACCEPT")]
		Accept = 1,
		// Token: 0x040009B1 RID: 2481
		[DomName("FILTER_REJECT")]
		Reject,
		// Token: 0x040009B2 RID: 2482
		[DomName("FILTER_SKIP")]
		Skip
	}
}
