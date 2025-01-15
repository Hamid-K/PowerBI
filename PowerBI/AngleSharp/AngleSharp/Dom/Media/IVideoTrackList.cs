using System;
using System.Collections;
using System.Collections.Generic;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Media
{
	// Token: 0x020001D8 RID: 472
	[DomName("VideoTrackList")]
	public interface IVideoTrackList : IEventTarget, IEnumerable<IVideoTrack>, IEnumerable
	{
		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000FCD RID: 4045
		[DomName("length")]
		int Length { get; }

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000FCE RID: 4046
		[DomName("selectedIndex")]
		int SelectedIndex { get; }

		// Token: 0x1700036F RID: 879
		[DomAccessor(Accessors.Getter)]
		IVideoTrack this[int index] { get; }

		// Token: 0x06000FD0 RID: 4048
		[DomName("getTrackById")]
		IVideoTrack GetTrackById(string id);

		// Token: 0x140000AD RID: 173
		// (add) Token: 0x06000FD1 RID: 4049
		// (remove) Token: 0x06000FD2 RID: 4050
		[DomName("onchange")]
		event DomEventHandler Changed;

		// Token: 0x140000AE RID: 174
		// (add) Token: 0x06000FD3 RID: 4051
		// (remove) Token: 0x06000FD4 RID: 4052
		[DomName("onaddtrack")]
		event DomEventHandler TrackAdded;

		// Token: 0x140000AF RID: 175
		// (add) Token: 0x06000FD5 RID: 4053
		// (remove) Token: 0x06000FD6 RID: 4054
		[DomName("onremovetrack")]
		event DomEventHandler TrackRemoved;
	}
}
