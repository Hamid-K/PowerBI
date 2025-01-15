using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Media
{
	// Token: 0x020001C8 RID: 456
	[DomName("MediaError")]
	public enum MediaErrorCode : byte
	{
		// Token: 0x04000A0C RID: 2572
		[DomName("MEDIA_ERR_ABORTED")]
		Aborted = 1,
		// Token: 0x04000A0D RID: 2573
		[DomName("MEDIA_ERR_NETWORK")]
		Network,
		// Token: 0x04000A0E RID: 2574
		[DomName("MEDIA_ERR_DECODE")]
		Decode,
		// Token: 0x04000A0F RID: 2575
		[DomName("MEDIA_ERR_SRC_NOT_SUPPORTED")]
		SourceNotSupported
	}
}
