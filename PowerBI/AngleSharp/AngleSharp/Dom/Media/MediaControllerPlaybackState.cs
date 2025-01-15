using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Media
{
	// Token: 0x020001C7 RID: 455
	[DomName("MediaControllerPlaybackState")]
	public enum MediaControllerPlaybackState : byte
	{
		// Token: 0x04000A08 RID: 2568
		[DomName("waiting")]
		Waiting,
		// Token: 0x04000A09 RID: 2569
		[DomName("playing")]
		Playing,
		// Token: 0x04000A0A RID: 2570
		[DomName("ended")]
		Ended
	}
}
