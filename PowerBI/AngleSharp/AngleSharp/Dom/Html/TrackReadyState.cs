using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200033C RID: 828
	[DomName("HTMLTrackElement")]
	public enum TrackReadyState : byte
	{
		// Token: 0x04000CBC RID: 3260
		[DomName("NONE")]
		None,
		// Token: 0x04000CBD RID: 3261
		[DomName("LOADING")]
		Loading,
		// Token: 0x04000CBE RID: 3262
		[DomName("LOADED")]
		Loaded,
		// Token: 0x04000CBF RID: 3263
		[DomName("ERROR")]
		Error
	}
}
