using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Media
{
	// Token: 0x020001CA RID: 458
	[DomName("HTMLMediaElement")]
	public enum MediaReadyState : byte
	{
		// Token: 0x04000A16 RID: 2582
		[DomName("HAVE_NOTHING")]
		Nothing,
		// Token: 0x04000A17 RID: 2583
		[DomName("HAVE_METADATA")]
		Metadata,
		// Token: 0x04000A18 RID: 2584
		[DomName("HAVE_CURRENT_DATA")]
		CurrentData,
		// Token: 0x04000A19 RID: 2585
		[DomName("HAVE_FUTURE_DATA")]
		FutureData,
		// Token: 0x04000A1A RID: 2586
		[DomName("HAVE_ENOUGH_DATA")]
		EnoughData
	}
}
