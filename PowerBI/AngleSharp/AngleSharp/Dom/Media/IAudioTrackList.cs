using System;
using System.Collections;
using System.Collections.Generic;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Media
{
	// Token: 0x020001CD RID: 461
	[DomName("AudioTrackList")]
	public interface IAudioTrackList : IEventTarget, IEnumerable<IAudioTrack>, IEnumerable
	{
		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000F50 RID: 3920
		[DomName("length")]
		int Length { get; }

		// Token: 0x1700033B RID: 827
		[DomAccessor(Accessors.Getter)]
		IAudioTrack this[int index] { get; }

		// Token: 0x06000F52 RID: 3922
		[DomName("getTrackById")]
		IAudioTrack GetTrackById(string id);

		// Token: 0x14000099 RID: 153
		// (add) Token: 0x06000F53 RID: 3923
		// (remove) Token: 0x06000F54 RID: 3924
		[DomName("onchange")]
		event DomEventHandler Changed;

		// Token: 0x1400009A RID: 154
		// (add) Token: 0x06000F55 RID: 3925
		// (remove) Token: 0x06000F56 RID: 3926
		[DomName("onaddtrack")]
		event DomEventHandler TrackAdded;

		// Token: 0x1400009B RID: 155
		// (add) Token: 0x06000F57 RID: 3927
		// (remove) Token: 0x06000F58 RID: 3928
		[DomName("onremovetrack")]
		event DomEventHandler TrackRemoved;
	}
}
