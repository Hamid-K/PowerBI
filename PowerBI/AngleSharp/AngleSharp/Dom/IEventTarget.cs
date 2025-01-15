using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom
{
	// Token: 0x02000186 RID: 390
	[DomName("EventTarget")]
	public interface IEventTarget
	{
		// Token: 0x06000E32 RID: 3634
		[DomName("addEventListener")]
		void AddEventListener(string type, DomEventHandler callback = null, bool capture = false);

		// Token: 0x06000E33 RID: 3635
		[DomName("removeEventListener")]
		void RemoveEventListener(string type, DomEventHandler callback = null, bool capture = false);

		// Token: 0x06000E34 RID: 3636
		void InvokeEventListener(Event ev);

		// Token: 0x06000E35 RID: 3637
		[DomName("dispatchEvent")]
		bool Dispatch(Event ev);
	}
}
