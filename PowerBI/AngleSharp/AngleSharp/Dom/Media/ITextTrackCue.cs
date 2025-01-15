using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Media
{
	// Token: 0x020001D3 RID: 467
	[DomName("TextTrackCue")]
	public interface ITextTrackCue : IEventTarget
	{
		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000F9F RID: 3999
		// (set) Token: 0x06000FA0 RID: 4000
		[DomName("id")]
		string Id { get; set; }

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000FA1 RID: 4001
		[DomName("track")]
		ITextTrack Track { get; }

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000FA2 RID: 4002
		// (set) Token: 0x06000FA3 RID: 4003
		[DomName("startTime")]
		double StartTime { get; set; }

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000FA4 RID: 4004
		// (set) Token: 0x06000FA5 RID: 4005
		[DomName("endTime")]
		double EndTime { get; set; }

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000FA6 RID: 4006
		// (set) Token: 0x06000FA7 RID: 4007
		[DomName("pauseOnExit")]
		bool IsPausedOnExit { get; set; }

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000FA8 RID: 4008
		// (set) Token: 0x06000FA9 RID: 4009
		[DomName("vertical")]
		string Vertical { get; set; }

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000FAA RID: 4010
		// (set) Token: 0x06000FAB RID: 4011
		[DomName("snapToLines")]
		bool IsSnappedToLines { get; set; }

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000FAC RID: 4012
		// (set) Token: 0x06000FAD RID: 4013
		[DomName("line")]
		int Line { get; set; }

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000FAE RID: 4014
		// (set) Token: 0x06000FAF RID: 4015
		[DomName("position")]
		int Position { get; set; }

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000FB0 RID: 4016
		// (set) Token: 0x06000FB1 RID: 4017
		[DomName("size")]
		int Size { get; set; }

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000FB2 RID: 4018
		// (set) Token: 0x06000FB3 RID: 4019
		[DomName("align")]
		string Alignment { get; set; }

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000FB4 RID: 4020
		// (set) Token: 0x06000FB5 RID: 4021
		[DomName("text")]
		string Text { get; set; }

		// Token: 0x06000FB6 RID: 4022
		[DomName("getCueAsHTML")]
		IDocumentFragment AsHtml();

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000FB7 RID: 4023
		// (set) Token: 0x06000FB8 RID: 4024
		[DomName("onenter")]
		DomEventHandler Entered { get; set; }

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000FB9 RID: 4025
		// (set) Token: 0x06000FBA RID: 4026
		[DomName("onexit")]
		DomEventHandler Exited { get; set; }
	}
}
