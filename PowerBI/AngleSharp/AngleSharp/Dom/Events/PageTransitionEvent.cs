using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Events
{
	// Token: 0x020001EF RID: 495
	[DomName("PageTransitionEvent")]
	public class PageTransitionEvent : Event
	{
		// Token: 0x06001065 RID: 4197 RVA: 0x00047068 File Offset: 0x00045268
		public PageTransitionEvent()
		{
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0004785A File Offset: 0x00045A5A
		[DomConstructor]
		[DomInitDict(1, true)]
		public PageTransitionEvent(string type, bool bubbles = false, bool cancelable = false, bool persisted = false)
		{
			this.Init(type, bubbles, cancelable, persisted);
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x0004786D File Offset: 0x00045A6D
		// (set) Token: 0x06001068 RID: 4200 RVA: 0x00047875 File Offset: 0x00045A75
		[DomName("persisted")]
		public bool IsPersisted { get; private set; }

		// Token: 0x06001069 RID: 4201 RVA: 0x0004787E File Offset: 0x00045A7E
		[DomName("initPageTransitionEvent")]
		public void Init(string type, bool bubbles, bool cancelable, bool persisted)
		{
			base.Init(type, bubbles, cancelable);
			this.IsPersisted = persisted;
		}
	}
}
