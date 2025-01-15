using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Events
{
	// Token: 0x020001DA RID: 474
	[DomName("KeyboardEvent")]
	public enum KeyboardLocation : byte
	{
		// Token: 0x04000A25 RID: 2597
		[DomName("DOM_KEY_LOCATION_STANDARD")]
		Standard,
		// Token: 0x04000A26 RID: 2598
		[DomName("DOM_KEY_LOCATION_LEFT")]
		Left,
		// Token: 0x04000A27 RID: 2599
		[DomName("DOM_KEY_LOCATION_RIGHT")]
		Right,
		// Token: 0x04000A28 RID: 2600
		[DomName("DOM_KEY_LOCATION_NUMPAD")]
		NumPad
	}
}
