using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200083A RID: 2106
	[ComVisible(false)]
	[ComEventInterface(typeof(DWebBridgeEvents\u0000), typeof(DWebBridgeEvents_EventProvider\u0000))]
	public interface DWebBridgeEvents_Event
	{
		// Token: 0x14001AFF RID: 6911
		// (add) Token: 0x0600DF18 RID: 57112
		// (remove) Token: 0x0600DF19 RID: 57113
		event DWebBridgeEvents_onscriptleteventEventHandler onscriptletevent;

		// Token: 0x14001B00 RID: 6912
		// (add) Token: 0x0600DF1A RID: 57114
		// (remove) Token: 0x0600DF1B RID: 57115
		event DWebBridgeEvents_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x14001B01 RID: 6913
		// (add) Token: 0x0600DF1C RID: 57116
		// (remove) Token: 0x0600DF1D RID: 57117
		event DWebBridgeEvents_onclickEventHandler onclick;

		// Token: 0x14001B02 RID: 6914
		// (add) Token: 0x0600DF1E RID: 57118
		// (remove) Token: 0x0600DF1F RID: 57119
		event DWebBridgeEvents_ondblclickEventHandler ondblclick;

		// Token: 0x14001B03 RID: 6915
		// (add) Token: 0x0600DF20 RID: 57120
		// (remove) Token: 0x0600DF21 RID: 57121
		event DWebBridgeEvents_onkeydownEventHandler onkeydown;

		// Token: 0x14001B04 RID: 6916
		// (add) Token: 0x0600DF22 RID: 57122
		// (remove) Token: 0x0600DF23 RID: 57123
		event DWebBridgeEvents_onkeyupEventHandler onkeyup;

		// Token: 0x14001B05 RID: 6917
		// (add) Token: 0x0600DF24 RID: 57124
		// (remove) Token: 0x0600DF25 RID: 57125
		event DWebBridgeEvents_onkeypressEventHandler onkeypress;

		// Token: 0x14001B06 RID: 6918
		// (add) Token: 0x0600DF26 RID: 57126
		// (remove) Token: 0x0600DF27 RID: 57127
		event DWebBridgeEvents_onmousedownEventHandler onmousedown;

		// Token: 0x14001B07 RID: 6919
		// (add) Token: 0x0600DF28 RID: 57128
		// (remove) Token: 0x0600DF29 RID: 57129
		event DWebBridgeEvents_onmousemoveEventHandler onmousemove;

		// Token: 0x14001B08 RID: 6920
		// (add) Token: 0x0600DF2A RID: 57130
		// (remove) Token: 0x0600DF2B RID: 57131
		event DWebBridgeEvents_onmouseupEventHandler onmouseup;
	}
}
