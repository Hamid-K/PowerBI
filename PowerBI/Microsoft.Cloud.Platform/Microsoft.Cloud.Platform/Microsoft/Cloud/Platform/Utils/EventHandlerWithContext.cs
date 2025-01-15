using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000183 RID: 387
	// (Invoke) Token: 0x06000A02 RID: 2562
	public delegate void EventHandlerWithContext<TEventArgs>(object sender, TEventArgs e, object context) where TEventArgs : EventArgs;
}
