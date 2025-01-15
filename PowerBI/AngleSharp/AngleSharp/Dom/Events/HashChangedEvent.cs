using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Events
{
	// Token: 0x020001E5 RID: 485
	[DomName("HashChangeEvent")]
	public class HashChangedEvent : Event
	{
		// Token: 0x0600100B RID: 4107 RVA: 0x00047068 File Offset: 0x00045268
		public HashChangedEvent()
		{
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x0004737B File Offset: 0x0004557B
		[DomConstructor]
		[DomInitDict(1, true)]
		public HashChangedEvent(string type, bool bubbles = false, bool cancelable = false, string oldURL = null, string newURL = null)
		{
			this.Init(type, bubbles, cancelable, oldURL ?? string.Empty, newURL ?? string.Empty);
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x0600100D RID: 4109 RVA: 0x000473A2 File Offset: 0x000455A2
		// (set) Token: 0x0600100E RID: 4110 RVA: 0x000473AA File Offset: 0x000455AA
		[DomName("oldURL")]
		public string PreviousUrl { get; private set; }

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x0600100F RID: 4111 RVA: 0x000473B3 File Offset: 0x000455B3
		// (set) Token: 0x06001010 RID: 4112 RVA: 0x000473BB File Offset: 0x000455BB
		[DomName("newURL")]
		public string CurrentUrl { get; private set; }

		// Token: 0x06001011 RID: 4113 RVA: 0x000473C4 File Offset: 0x000455C4
		[DomName("initHashChangedEvent")]
		public void Init(string type, bool bubbles, bool cancelable, string previousUrl, string currentUrl)
		{
			base.Init(type, bubbles, cancelable);
			base.Stop();
			this.PreviousUrl = previousUrl;
			this.CurrentUrl = currentUrl;
		}
	}
}
