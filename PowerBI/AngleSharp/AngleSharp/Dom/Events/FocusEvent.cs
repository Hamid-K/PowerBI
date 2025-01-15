using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Events
{
	// Token: 0x020001E4 RID: 484
	[DomName("FocusEvent")]
	public class FocusEvent : UiEvent
	{
		// Token: 0x06001006 RID: 4102 RVA: 0x00046FAB File Offset: 0x000451AB
		public FocusEvent()
		{
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x0004733C File Offset: 0x0004553C
		[DomConstructor]
		[DomInitDict(1, true)]
		public FocusEvent(string type, bool bubbles = false, bool cancelable = false, IWindow view = null, int detail = 0, IEventTarget target = null)
		{
			this.Init(type, bubbles, cancelable, view, detail, target);
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06001008 RID: 4104 RVA: 0x00047353 File Offset: 0x00045553
		// (set) Token: 0x06001009 RID: 4105 RVA: 0x0004735B File Offset: 0x0004555B
		[DomName("relatedTarget")]
		public IEventTarget Target { get; private set; }

		// Token: 0x0600100A RID: 4106 RVA: 0x00047364 File Offset: 0x00045564
		[DomName("initFocusEvent")]
		public void Init(string type, bool bubbles, bool cancelable, IWindow view, int detail, IEventTarget target)
		{
			base.Init(type, bubbles, cancelable, view, detail);
			this.Target = target;
		}
	}
}
