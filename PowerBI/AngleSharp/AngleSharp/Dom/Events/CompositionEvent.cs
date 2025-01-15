using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Events
{
	// Token: 0x020001DE RID: 478
	[DomName("CompositionEvent")]
	public class CompositionEvent : UiEvent
	{
		// Token: 0x06000FD7 RID: 4055 RVA: 0x00046FAB File Offset: 0x000451AB
		public CompositionEvent()
		{
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x00046FB3 File Offset: 0x000451B3
		[DomConstructor]
		[DomInitDict(1, true)]
		public CompositionEvent(string type, bool bubbles = false, bool cancelable = false, IWindow view = null, string data = null)
		{
			this.Init(type, bubbles, cancelable, view, data ?? string.Empty);
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x00046FD1 File Offset: 0x000451D1
		// (set) Token: 0x06000FDA RID: 4058 RVA: 0x00046FD9 File Offset: 0x000451D9
		[DomName("data")]
		public string Data { get; private set; }

		// Token: 0x06000FDB RID: 4059 RVA: 0x00046FE2 File Offset: 0x000451E2
		[DomName("initCompositionEvent")]
		public void Init(string type, bool bubbles, bool cancelable, IWindow view, string data)
		{
			base.Init(type, bubbles, cancelable, view, 0);
			this.Data = data;
		}
	}
}
