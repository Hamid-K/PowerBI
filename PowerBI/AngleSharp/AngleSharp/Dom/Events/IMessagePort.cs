using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Events
{
	// Token: 0x020001F6 RID: 502
	[DomName("MessagePort")]
	public interface IMessagePort : IEventTarget
	{
		// Token: 0x0600110B RID: 4363
		[DomName("postMessage")]
		void Send(object message);

		// Token: 0x0600110C RID: 4364
		[DomName("start")]
		void Open();

		// Token: 0x0600110D RID: 4365
		[DomName("close")]
		void Close();

		// Token: 0x140000EA RID: 234
		// (add) Token: 0x0600110E RID: 4366
		// (remove) Token: 0x0600110F RID: 4367
		[DomName("onmessage")]
		event DomEventHandler MessageReceived;
	}
}
