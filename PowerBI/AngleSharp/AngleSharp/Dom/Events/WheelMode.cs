using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Events
{
	// Token: 0x020001DD RID: 477
	[DomName("WheelEvent")]
	public enum WheelMode : byte
	{
		// Token: 0x04000A33 RID: 2611
		[DomName("DOM_DELTA_PIXEL")]
		Pixel,
		// Token: 0x04000A34 RID: 2612
		[DomName("DOM_DELTA_LINE")]
		Line,
		// Token: 0x04000A35 RID: 2613
		[DomName("DOM_DELTA_PAGE")]
		Page
	}
}
