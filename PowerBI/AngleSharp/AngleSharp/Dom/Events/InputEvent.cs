using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Events
{
	// Token: 0x020001E8 RID: 488
	[DomName("InputEvent")]
	public class InputEvent : Event
	{
		// Token: 0x06001019 RID: 4121 RVA: 0x00047068 File Offset: 0x00045268
		public InputEvent()
		{
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x00047455 File Offset: 0x00045655
		[DomConstructor]
		[DomInitDict(1, true)]
		public InputEvent(string type, bool bubbles = false, bool cancelable = false, string data = null)
		{
			this.Init(type, bubbles, cancelable, data ?? string.Empty);
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x0600101B RID: 4123 RVA: 0x00047471 File Offset: 0x00045671
		// (set) Token: 0x0600101C RID: 4124 RVA: 0x00047479 File Offset: 0x00045679
		[DomName("data")]
		public string Data { get; private set; }

		// Token: 0x0600101D RID: 4125 RVA: 0x00047482 File Offset: 0x00045682
		[DomName("initInputEvent")]
		public void Init(string type, bool bubbles, bool cancelable, string data)
		{
			base.Init(type, bubbles, cancelable);
			this.Data = data;
		}
	}
}
