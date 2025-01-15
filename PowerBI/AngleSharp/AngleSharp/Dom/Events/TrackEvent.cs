using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Events
{
	// Token: 0x020001F2 RID: 498
	[DomName("TrackEvent")]
	public class TrackEvent : Event
	{
		// Token: 0x06001080 RID: 4224 RVA: 0x00047068 File Offset: 0x00045268
		public TrackEvent()
		{
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x000479BA File Offset: 0x00045BBA
		[DomConstructor]
		[DomInitDict(1, true)]
		public TrackEvent(string type, bool bubbles = false, bool cancelable = false, object track = null)
		{
			this.Init(type, bubbles, cancelable, track);
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06001082 RID: 4226 RVA: 0x000479CD File Offset: 0x00045BCD
		// (set) Token: 0x06001083 RID: 4227 RVA: 0x000479D5 File Offset: 0x00045BD5
		[DomName("track")]
		public object Track { get; private set; }

		// Token: 0x06001084 RID: 4228 RVA: 0x000479DE File Offset: 0x00045BDE
		[DomName("initTrackEvent")]
		public void Init(string type, bool bubbles, bool cancelable, object track)
		{
			base.Init(type, bubbles, cancelable);
			this.Track = track;
		}
	}
}
