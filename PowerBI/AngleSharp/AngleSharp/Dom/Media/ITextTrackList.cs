using System;
using System.Collections;
using System.Collections.Generic;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Media
{
	// Token: 0x020001D5 RID: 469
	[DomName("TextTrackList")]
	public interface ITextTrackList : IEventTarget, IEnumerable<ITextTrack>, IEnumerable
	{
		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000FBE RID: 4030
		[DomName("length")]
		int Length { get; }

		// Token: 0x17000366 RID: 870
		[DomAccessor(Accessors.Getter)]
		ITextTrack this[int index] { get; }

		// Token: 0x140000AB RID: 171
		// (add) Token: 0x06000FC0 RID: 4032
		// (remove) Token: 0x06000FC1 RID: 4033
		[DomName("onaddtrack")]
		event DomEventHandler TrackAdded;

		// Token: 0x140000AC RID: 172
		// (add) Token: 0x06000FC2 RID: 4034
		// (remove) Token: 0x06000FC3 RID: 4035
		[DomName("onremovetrack")]
		event DomEventHandler TrackRemoved;
	}
}
