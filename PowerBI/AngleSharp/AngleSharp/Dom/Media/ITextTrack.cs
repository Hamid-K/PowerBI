using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Media
{
	// Token: 0x020001D2 RID: 466
	[DomName("TextTrack")]
	public interface ITextTrack : IEventTarget
	{
		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000F94 RID: 3988
		[DomName("kind")]
		string Kind { get; }

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000F95 RID: 3989
		[DomName("label")]
		string Label { get; }

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000F96 RID: 3990
		[DomName("language")]
		string Language { get; }

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000F97 RID: 3991
		// (set) Token: 0x06000F98 RID: 3992
		[DomName("mode")]
		TextTrackMode Mode { get; set; }

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000F99 RID: 3993
		[DomName("cues")]
		ITextTrackCueList Cues { get; }

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000F9A RID: 3994
		[DomName("activeCues")]
		ITextTrackCueList ActiveCues { get; }

		// Token: 0x06000F9B RID: 3995
		[DomName("addCue")]
		void Add(ITextTrackCue cue);

		// Token: 0x06000F9C RID: 3996
		[DomName("removeCue")]
		void Remove(ITextTrackCue cue);

		// Token: 0x140000AA RID: 170
		// (add) Token: 0x06000F9D RID: 3997
		// (remove) Token: 0x06000F9E RID: 3998
		[DomName("oncuechange")]
		event DomEventHandler CueChanged;
	}
}
