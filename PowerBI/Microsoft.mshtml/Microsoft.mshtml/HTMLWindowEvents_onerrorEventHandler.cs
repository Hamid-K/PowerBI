using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007C9 RID: 1993
	// (Invoke) Token: 0x0600D8BB RID: 55483
	[ComVisible(false)]
	public delegate void HTMLWindowEvents_onerrorEventHandler([MarshalAs(19)] [In] string description, [MarshalAs(19)] [In] string url, [In] int line);
}
