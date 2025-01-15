using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007C3 RID: 1987
	[ComVisible(false)]
	[ComEventInterface(typeof(HTMLWindowEvents\u0000), typeof(HTMLWindowEvents_EventProvider\u0000))]
	public interface HTMLWindowEvents_Event
	{
		// Token: 0x14001A31 RID: 6705
		// (add) Token: 0x0600D89A RID: 55450
		// (remove) Token: 0x0600D89B RID: 55451
		event HTMLWindowEvents_onloadEventHandler onload;

		// Token: 0x14001A32 RID: 6706
		// (add) Token: 0x0600D89C RID: 55452
		// (remove) Token: 0x0600D89D RID: 55453
		event HTMLWindowEvents_onunloadEventHandler onunload;

		// Token: 0x14001A33 RID: 6707
		// (add) Token: 0x0600D89E RID: 55454
		// (remove) Token: 0x0600D89F RID: 55455
		event HTMLWindowEvents_onhelpEventHandler onhelp;

		// Token: 0x14001A34 RID: 6708
		// (add) Token: 0x0600D8A0 RID: 55456
		// (remove) Token: 0x0600D8A1 RID: 55457
		event HTMLWindowEvents_onfocusEventHandler onfocus;

		// Token: 0x14001A35 RID: 6709
		// (add) Token: 0x0600D8A2 RID: 55458
		// (remove) Token: 0x0600D8A3 RID: 55459
		event HTMLWindowEvents_onblurEventHandler onblur;

		// Token: 0x14001A36 RID: 6710
		// (add) Token: 0x0600D8A4 RID: 55460
		// (remove) Token: 0x0600D8A5 RID: 55461
		event HTMLWindowEvents_onerrorEventHandler onerror;

		// Token: 0x14001A37 RID: 6711
		// (add) Token: 0x0600D8A6 RID: 55462
		// (remove) Token: 0x0600D8A7 RID: 55463
		event HTMLWindowEvents_onresizeEventHandler onresize;

		// Token: 0x14001A38 RID: 6712
		// (add) Token: 0x0600D8A8 RID: 55464
		// (remove) Token: 0x0600D8A9 RID: 55465
		event HTMLWindowEvents_onscrollEventHandler onscroll;

		// Token: 0x14001A39 RID: 6713
		// (add) Token: 0x0600D8AA RID: 55466
		// (remove) Token: 0x0600D8AB RID: 55467
		event HTMLWindowEvents_onbeforeunloadEventHandler onbeforeunload;

		// Token: 0x14001A3A RID: 6714
		// (add) Token: 0x0600D8AC RID: 55468
		// (remove) Token: 0x0600D8AD RID: 55469
		event HTMLWindowEvents_onbeforeprintEventHandler onbeforeprint;

		// Token: 0x14001A3B RID: 6715
		// (add) Token: 0x0600D8AE RID: 55470
		// (remove) Token: 0x0600D8AF RID: 55471
		event HTMLWindowEvents_onafterprintEventHandler onafterprint;
	}
}
