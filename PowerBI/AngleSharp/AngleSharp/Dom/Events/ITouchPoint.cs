using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Events
{
	// Token: 0x020001F8 RID: 504
	[DomName("Touch")]
	public interface ITouchPoint
	{
		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001112 RID: 4370
		[DomName("identifier")]
		int Id { get; }

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001113 RID: 4371
		[DomName("target")]
		IEventTarget Target { get; }

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001114 RID: 4372
		[DomName("screenX")]
		int ScreenX { get; }

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001115 RID: 4373
		[DomName("screenY")]
		int ScreenY { get; }

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001116 RID: 4374
		[DomName("clientX")]
		int ClientX { get; }

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001117 RID: 4375
		[DomName("clientY")]
		int ClientY { get; }

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001118 RID: 4376
		[DomName("pageX")]
		int PageX { get; }

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06001119 RID: 4377
		[DomName("pageY")]
		int PageY { get; }
	}
}
