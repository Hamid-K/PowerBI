using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Events
{
	// Token: 0x020001E1 RID: 481
	[DomName("CustomEvent")]
	public class CustomEvent : Event
	{
		// Token: 0x06000FE3 RID: 4067 RVA: 0x00047068 File Offset: 0x00045268
		public CustomEvent()
		{
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x00047070 File Offset: 0x00045270
		[DomConstructor]
		[DomInitDict(1, true)]
		public CustomEvent(string type, bool bubbles = false, bool cancelable = false, object details = null)
		{
			this.Init(type, bubbles, cancelable, details);
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x00047083 File Offset: 0x00045283
		// (set) Token: 0x06000FE6 RID: 4070 RVA: 0x0004708B File Offset: 0x0004528B
		[DomName("detail")]
		public object Details { get; private set; }

		// Token: 0x06000FE7 RID: 4071 RVA: 0x00047094 File Offset: 0x00045294
		[DomName("initCustomEvent")]
		public void Init(string type, bool bubbles, bool cancelable, object details)
		{
			base.Init(type, bubbles, cancelable);
			this.Details = details;
		}
	}
}
