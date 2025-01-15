using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CD8 RID: 3288
	[ComVisible(false)]
	[ComEventInterface(typeof(HTMLNamespaceEvents\u0000), typeof(HTMLNamespaceEvents_EventProvider\u0000))]
	public interface HTMLNamespaceEvents_Event
	{
		// Token: 0x14002B07 RID: 11015
		// (add) Token: 0x0601634D RID: 90957
		// (remove) Token: 0x0601634E RID: 90958
		event HTMLNamespaceEvents_onreadystatechangeEventHandler onreadystatechange;
	}
}
