using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Events
{
	// Token: 0x020001EB RID: 491
	[DomName("MediaQueryListEvent")]
	public class MediaQueryListEvent : Event
	{
		// Token: 0x06001032 RID: 4146 RVA: 0x00047068 File Offset: 0x00045268
		public MediaQueryListEvent()
		{
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x000475DF File Offset: 0x000457DF
		[DomConstructor]
		[DomInitDict(1, true)]
		public MediaQueryListEvent(string type, bool bubbles = false, bool cancelable = false, string media = null, bool matches = false)
		{
			this.Init(type, bubbles, cancelable, media, matches);
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06001034 RID: 4148 RVA: 0x000475F4 File Offset: 0x000457F4
		// (set) Token: 0x06001035 RID: 4149 RVA: 0x000475FC File Offset: 0x000457FC
		[DomName("matches")]
		public bool IsMatch { get; private set; }

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06001036 RID: 4150 RVA: 0x00047605 File Offset: 0x00045805
		// (set) Token: 0x06001037 RID: 4151 RVA: 0x0004760D File Offset: 0x0004580D
		[DomName("media")]
		public string Media { get; private set; }

		// Token: 0x06001038 RID: 4152 RVA: 0x00047616 File Offset: 0x00045816
		[DomName("initMediaQueryListEvent")]
		public void Init(string type, bool bubbles, bool cancelable, string media, bool matches)
		{
			base.Init(type, bubbles, cancelable);
			this.Media = media;
			this.IsMatch = matches;
		}
	}
}
