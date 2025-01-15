using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Events
{
	// Token: 0x020001D9 RID: 473
	[DomName("Event")]
	public enum EventPhase : byte
	{
		// Token: 0x04000A20 RID: 2592
		[DomName("NONE")]
		None,
		// Token: 0x04000A21 RID: 2593
		[DomName("CAPTURING_PHASE")]
		Capturing,
		// Token: 0x04000A22 RID: 2594
		[DomName("AT_TARGET")]
		AtTarget,
		// Token: 0x04000A23 RID: 2595
		[DomName("BUBBLING_PHASE")]
		Bubbling
	}
}
