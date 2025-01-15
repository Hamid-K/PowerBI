using System;
using System.Collections;
using System.Collections.Generic;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Media
{
	// Token: 0x020001D4 RID: 468
	[DomName("TextTrackCueList")]
	public interface ITextTrackCueList : IEnumerable<ITextTrackCue>, IEnumerable
	{
		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000FBB RID: 4027
		[DomName("length")]
		int Length { get; }

		// Token: 0x17000364 RID: 868
		ITextTrackCue this[int index] { get; }

		// Token: 0x06000FBD RID: 4029
		[DomName("getCueById")]
		IVideoTrack GetCueById(string id);
	}
}
