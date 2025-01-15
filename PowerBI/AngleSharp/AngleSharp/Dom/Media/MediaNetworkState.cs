using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Media
{
	// Token: 0x020001C9 RID: 457
	[DomName("HTMLMediaElement")]
	public enum MediaNetworkState : byte
	{
		// Token: 0x04000A11 RID: 2577
		[DomName("NETWORK_EMPTY")]
		Empty,
		// Token: 0x04000A12 RID: 2578
		[DomName("NETWORK_IDLE")]
		Idle,
		// Token: 0x04000A13 RID: 2579
		[DomName("NETWORK_LOADING")]
		Loading,
		// Token: 0x04000A14 RID: 2580
		[DomName("NETWORK_NO_SOURCE")]
		NoSource
	}
}
