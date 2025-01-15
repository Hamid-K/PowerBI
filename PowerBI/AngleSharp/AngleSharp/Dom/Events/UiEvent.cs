using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Events
{
	// Token: 0x020001F3 RID: 499
	[DomName("UIEvent")]
	public class UiEvent : Event
	{
		// Token: 0x06001085 RID: 4229 RVA: 0x00047068 File Offset: 0x00045268
		public UiEvent()
		{
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x000479F1 File Offset: 0x00045BF1
		[DomConstructor]
		[DomInitDict(1, true)]
		public UiEvent(string type, bool bubbles = false, bool cancelable = false, IWindow view = null, int detail = 0)
		{
			this.Init(type, bubbles, cancelable, view, detail);
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06001087 RID: 4231 RVA: 0x00047A06 File Offset: 0x00045C06
		// (set) Token: 0x06001088 RID: 4232 RVA: 0x00047A0E File Offset: 0x00045C0E
		[DomName("view")]
		public IWindow View { get; private set; }

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06001089 RID: 4233 RVA: 0x00047A17 File Offset: 0x00045C17
		// (set) Token: 0x0600108A RID: 4234 RVA: 0x00047A1F File Offset: 0x00045C1F
		[DomName("detail")]
		public int Detail { get; private set; }

		// Token: 0x0600108B RID: 4235 RVA: 0x00047A28 File Offset: 0x00045C28
		[DomName("initUIEvent")]
		public void Init(string type, bool bubbles, bool cancelable, IWindow view, int detail)
		{
			base.Init(type, bubbles, cancelable);
			this.View = view;
			this.Detail = detail;
		}
	}
}
