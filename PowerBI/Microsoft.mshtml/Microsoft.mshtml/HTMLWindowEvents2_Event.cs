using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007CF RID: 1999
	[ComVisible(false)]
	[ComEventInterface(typeof(HTMLWindowEvents2\u0000), typeof(HTMLWindowEvents2_EventProvider\u0000))]
	public interface HTMLWindowEvents2_Event
	{
		// Token: 0x14001A3C RID: 6716
		// (add) Token: 0x0600D8C6 RID: 55494
		// (remove) Token: 0x0600D8C7 RID: 55495
		event HTMLWindowEvents2_onloadEventHandler onload;

		// Token: 0x14001A3D RID: 6717
		// (add) Token: 0x0600D8C8 RID: 55496
		// (remove) Token: 0x0600D8C9 RID: 55497
		event HTMLWindowEvents2_onunloadEventHandler onunload;

		// Token: 0x14001A3E RID: 6718
		// (add) Token: 0x0600D8CA RID: 55498
		// (remove) Token: 0x0600D8CB RID: 55499
		event HTMLWindowEvents2_onhelpEventHandler onhelp;

		// Token: 0x14001A3F RID: 6719
		// (add) Token: 0x0600D8CC RID: 55500
		// (remove) Token: 0x0600D8CD RID: 55501
		event HTMLWindowEvents2_onfocusEventHandler onfocus;

		// Token: 0x14001A40 RID: 6720
		// (add) Token: 0x0600D8CE RID: 55502
		// (remove) Token: 0x0600D8CF RID: 55503
		event HTMLWindowEvents2_onblurEventHandler onblur;

		// Token: 0x14001A41 RID: 6721
		// (add) Token: 0x0600D8D0 RID: 55504
		// (remove) Token: 0x0600D8D1 RID: 55505
		event HTMLWindowEvents2_onerrorEventHandler onerror;

		// Token: 0x14001A42 RID: 6722
		// (add) Token: 0x0600D8D2 RID: 55506
		// (remove) Token: 0x0600D8D3 RID: 55507
		event HTMLWindowEvents2_onresizeEventHandler onresize;

		// Token: 0x14001A43 RID: 6723
		// (add) Token: 0x0600D8D4 RID: 55508
		// (remove) Token: 0x0600D8D5 RID: 55509
		event HTMLWindowEvents2_onscrollEventHandler onscroll;

		// Token: 0x14001A44 RID: 6724
		// (add) Token: 0x0600D8D6 RID: 55510
		// (remove) Token: 0x0600D8D7 RID: 55511
		event HTMLWindowEvents2_onbeforeunloadEventHandler onbeforeunload;

		// Token: 0x14001A45 RID: 6725
		// (add) Token: 0x0600D8D8 RID: 55512
		// (remove) Token: 0x0600D8D9 RID: 55513
		event HTMLWindowEvents2_onbeforeprintEventHandler onbeforeprint;

		// Token: 0x14001A46 RID: 6726
		// (add) Token: 0x0600D8DA RID: 55514
		// (remove) Token: 0x0600D8DB RID: 55515
		event HTMLWindowEvents2_onafterprintEventHandler onafterprint;
	}
}
