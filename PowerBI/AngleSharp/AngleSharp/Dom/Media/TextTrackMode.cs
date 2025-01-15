using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Media
{
	// Token: 0x020001CB RID: 459
	[DomName("TextTrackMode")]
	public enum TextTrackMode : byte
	{
		// Token: 0x04000A1C RID: 2588
		[DomName("disabled")]
		Disabled,
		// Token: 0x04000A1D RID: 2589
		[DomName("hidden")]
		Hidden,
		// Token: 0x04000A1E RID: 2590
		[DomName("showing")]
		Showing
	}
}
