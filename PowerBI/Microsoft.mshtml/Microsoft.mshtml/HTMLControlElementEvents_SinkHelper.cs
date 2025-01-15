using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DC8 RID: 3528
	[ClassInterface(0)]
	internal sealed class HTMLControlElementEvents_SinkHelper : HTMLControlElementEvents
	{
		// Token: 0x060177F9 RID: 96249 RVA: 0x0001EE98 File Offset: 0x0001DE98
		public override void onfocusout()
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate();
				return;
			}
		}

		// Token: 0x060177FA RID: 96250 RVA: 0x0001EEC4 File Offset: 0x0001DEC4
		public override void onfocusin()
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate();
				return;
			}
		}

		// Token: 0x060177FB RID: 96251 RVA: 0x0001EEF0 File Offset: 0x0001DEF0
		public override void ondeactivate()
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate();
				return;
			}
		}

		// Token: 0x060177FC RID: 96252 RVA: 0x0001EF1C File Offset: 0x0001DF1C
		public override void onactivate()
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate();
				return;
			}
		}

		// Token: 0x060177FD RID: 96253 RVA: 0x0001EF48 File Offset: 0x0001DF48
		public override bool onmousewheel()
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate();
		}

		// Token: 0x060177FE RID: 96254 RVA: 0x0001EF74 File Offset: 0x0001DF74
		public override void onmouseleave()
		{
			if (this.m_onmouseleaveDelegate != null)
			{
				this.m_onmouseleaveDelegate();
				return;
			}
		}

		// Token: 0x060177FF RID: 96255 RVA: 0x0001EFA0 File Offset: 0x0001DFA0
		public override void onmouseenter()
		{
			if (this.m_onmouseenterDelegate != null)
			{
				this.m_onmouseenterDelegate();
				return;
			}
		}

		// Token: 0x06017800 RID: 96256 RVA: 0x0001EFCC File Offset: 0x0001DFCC
		public override void onresizeend()
		{
			if (this.m_onresizeendDelegate != null)
			{
				this.m_onresizeendDelegate();
				return;
			}
		}

		// Token: 0x06017801 RID: 96257 RVA: 0x0001EFF8 File Offset: 0x0001DFF8
		public override bool onresizestart()
		{
			return this.m_onresizestartDelegate != null && this.m_onresizestartDelegate();
		}

		// Token: 0x06017802 RID: 96258 RVA: 0x0001F024 File Offset: 0x0001E024
		public override void onmoveend()
		{
			if (this.m_onmoveendDelegate != null)
			{
				this.m_onmoveendDelegate();
				return;
			}
		}

		// Token: 0x06017803 RID: 96259 RVA: 0x0001F050 File Offset: 0x0001E050
		public override bool onmovestart()
		{
			return this.m_onmovestartDelegate != null && this.m_onmovestartDelegate();
		}

		// Token: 0x06017804 RID: 96260 RVA: 0x0001F07C File Offset: 0x0001E07C
		public override bool oncontrolselect()
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate();
		}

		// Token: 0x06017805 RID: 96261 RVA: 0x0001F0A8 File Offset: 0x0001E0A8
		public override void onmove()
		{
			if (this.m_onmoveDelegate != null)
			{
				this.m_onmoveDelegate();
				return;
			}
		}

		// Token: 0x06017806 RID: 96262 RVA: 0x0001F0D4 File Offset: 0x0001E0D4
		public override bool onbeforeactivate()
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate();
		}

		// Token: 0x06017807 RID: 96263 RVA: 0x0001F100 File Offset: 0x0001E100
		public override bool onbeforedeactivate()
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate();
		}

		// Token: 0x06017808 RID: 96264 RVA: 0x0001F12C File Offset: 0x0001E12C
		public override void onpage()
		{
			if (this.m_onpageDelegate != null)
			{
				this.m_onpageDelegate();
				return;
			}
		}

		// Token: 0x06017809 RID: 96265 RVA: 0x0001F158 File Offset: 0x0001E158
		public override void onlayoutcomplete()
		{
			if (this.m_onlayoutcompleteDelegate != null)
			{
				this.m_onlayoutcompleteDelegate();
				return;
			}
		}

		// Token: 0x0601780A RID: 96266 RVA: 0x0001F184 File Offset: 0x0001E184
		public override void onbeforeeditfocus()
		{
			if (this.m_onbeforeeditfocusDelegate != null)
			{
				this.m_onbeforeeditfocusDelegate();
				return;
			}
		}

		// Token: 0x0601780B RID: 96267 RVA: 0x0001F1B0 File Offset: 0x0001E1B0
		public override void onreadystatechange()
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate();
				return;
			}
		}

		// Token: 0x0601780C RID: 96268 RVA: 0x0001F1DC File Offset: 0x0001E1DC
		public override void oncellchange()
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate();
				return;
			}
		}

		// Token: 0x0601780D RID: 96269 RVA: 0x0001F208 File Offset: 0x0001E208
		public override void onrowsinserted()
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate();
				return;
			}
		}

		// Token: 0x0601780E RID: 96270 RVA: 0x0001F234 File Offset: 0x0001E234
		public override void onrowsdelete()
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate();
				return;
			}
		}

		// Token: 0x0601780F RID: 96271 RVA: 0x0001F260 File Offset: 0x0001E260
		public override bool oncontextmenu()
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate();
		}

		// Token: 0x06017810 RID: 96272 RVA: 0x0001F28C File Offset: 0x0001E28C
		public override bool onpaste()
		{
			return this.m_onpasteDelegate != null && this.m_onpasteDelegate();
		}

		// Token: 0x06017811 RID: 96273 RVA: 0x0001F2B8 File Offset: 0x0001E2B8
		public override bool onbeforepaste()
		{
			return this.m_onbeforepasteDelegate != null && this.m_onbeforepasteDelegate();
		}

		// Token: 0x06017812 RID: 96274 RVA: 0x0001F2E4 File Offset: 0x0001E2E4
		public override bool oncopy()
		{
			return this.m_oncopyDelegate != null && this.m_oncopyDelegate();
		}

		// Token: 0x06017813 RID: 96275 RVA: 0x0001F310 File Offset: 0x0001E310
		public override bool onbeforecopy()
		{
			return this.m_onbeforecopyDelegate != null && this.m_onbeforecopyDelegate();
		}

		// Token: 0x06017814 RID: 96276 RVA: 0x0001F33C File Offset: 0x0001E33C
		public override bool oncut()
		{
			return this.m_oncutDelegate != null && this.m_oncutDelegate();
		}

		// Token: 0x06017815 RID: 96277 RVA: 0x0001F368 File Offset: 0x0001E368
		public override bool onbeforecut()
		{
			return this.m_onbeforecutDelegate != null && this.m_onbeforecutDelegate();
		}

		// Token: 0x06017816 RID: 96278 RVA: 0x0001F394 File Offset: 0x0001E394
		public override bool ondrop()
		{
			return this.m_ondropDelegate != null && this.m_ondropDelegate();
		}

		// Token: 0x06017817 RID: 96279 RVA: 0x0001F3C0 File Offset: 0x0001E3C0
		public override void ondragleave()
		{
			if (this.m_ondragleaveDelegate != null)
			{
				this.m_ondragleaveDelegate();
				return;
			}
		}

		// Token: 0x06017818 RID: 96280 RVA: 0x0001F3EC File Offset: 0x0001E3EC
		public override bool ondragover()
		{
			return this.m_ondragoverDelegate != null && this.m_ondragoverDelegate();
		}

		// Token: 0x06017819 RID: 96281 RVA: 0x0001F418 File Offset: 0x0001E418
		public override bool ondragenter()
		{
			return this.m_ondragenterDelegate != null && this.m_ondragenterDelegate();
		}

		// Token: 0x0601781A RID: 96282 RVA: 0x0001F444 File Offset: 0x0001E444
		public override void ondragend()
		{
			if (this.m_ondragendDelegate != null)
			{
				this.m_ondragendDelegate();
				return;
			}
		}

		// Token: 0x0601781B RID: 96283 RVA: 0x0001F470 File Offset: 0x0001E470
		public override bool ondrag()
		{
			return this.m_ondragDelegate != null && this.m_ondragDelegate();
		}

		// Token: 0x0601781C RID: 96284 RVA: 0x0001F49C File Offset: 0x0001E49C
		public override void onresize()
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate();
				return;
			}
		}

		// Token: 0x0601781D RID: 96285 RVA: 0x0001F4C8 File Offset: 0x0001E4C8
		public override void onblur()
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate();
				return;
			}
		}

		// Token: 0x0601781E RID: 96286 RVA: 0x0001F4F4 File Offset: 0x0001E4F4
		public override void onfocus()
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate();
				return;
			}
		}

		// Token: 0x0601781F RID: 96287 RVA: 0x0001F520 File Offset: 0x0001E520
		public override void onscroll()
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate();
				return;
			}
		}

		// Token: 0x06017820 RID: 96288 RVA: 0x0001F54C File Offset: 0x0001E54C
		public override void onpropertychange()
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate();
				return;
			}
		}

		// Token: 0x06017821 RID: 96289 RVA: 0x0001F578 File Offset: 0x0001E578
		public override void onlosecapture()
		{
			if (this.m_onlosecaptureDelegate != null)
			{
				this.m_onlosecaptureDelegate();
				return;
			}
		}

		// Token: 0x06017822 RID: 96290 RVA: 0x0001F5A4 File Offset: 0x0001E5A4
		public override void ondatasetcomplete()
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate();
				return;
			}
		}

		// Token: 0x06017823 RID: 96291 RVA: 0x0001F5D0 File Offset: 0x0001E5D0
		public override void ondataavailable()
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate();
				return;
			}
		}

		// Token: 0x06017824 RID: 96292 RVA: 0x0001F5FC File Offset: 0x0001E5FC
		public override void ondatasetchanged()
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate();
				return;
			}
		}

		// Token: 0x06017825 RID: 96293 RVA: 0x0001F628 File Offset: 0x0001E628
		public override void onrowenter()
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate();
				return;
			}
		}

		// Token: 0x06017826 RID: 96294 RVA: 0x0001F654 File Offset: 0x0001E654
		public override bool onrowexit()
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate();
		}

		// Token: 0x06017827 RID: 96295 RVA: 0x0001F680 File Offset: 0x0001E680
		public override bool onerrorupdate()
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate();
		}

		// Token: 0x06017828 RID: 96296 RVA: 0x0001F6AC File Offset: 0x0001E6AC
		public override void onafterupdate()
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate();
				return;
			}
		}

		// Token: 0x06017829 RID: 96297 RVA: 0x0001F6D8 File Offset: 0x0001E6D8
		public override bool onbeforeupdate()
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate();
		}

		// Token: 0x0601782A RID: 96298 RVA: 0x0001F704 File Offset: 0x0001E704
		public override bool ondragstart()
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate();
		}

		// Token: 0x0601782B RID: 96299 RVA: 0x0001F730 File Offset: 0x0001E730
		public override void onfilterchange()
		{
			if (this.m_onfilterchangeDelegate != null)
			{
				this.m_onfilterchangeDelegate();
				return;
			}
		}

		// Token: 0x0601782C RID: 96300 RVA: 0x0001F75C File Offset: 0x0001E75C
		public override bool onselectstart()
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate();
		}

		// Token: 0x0601782D RID: 96301 RVA: 0x0001F788 File Offset: 0x0001E788
		public override void onmouseup()
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate();
				return;
			}
		}

		// Token: 0x0601782E RID: 96302 RVA: 0x0001F7B4 File Offset: 0x0001E7B4
		public override void onmousedown()
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate();
				return;
			}
		}

		// Token: 0x0601782F RID: 96303 RVA: 0x0001F7E0 File Offset: 0x0001E7E0
		public override void onmousemove()
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate();
				return;
			}
		}

		// Token: 0x06017830 RID: 96304 RVA: 0x0001F80C File Offset: 0x0001E80C
		public override void onmouseover()
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate();
				return;
			}
		}

		// Token: 0x06017831 RID: 96305 RVA: 0x0001F838 File Offset: 0x0001E838
		public override void onmouseout()
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate();
				return;
			}
		}

		// Token: 0x06017832 RID: 96306 RVA: 0x0001F864 File Offset: 0x0001E864
		public override void onkeyup()
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate();
				return;
			}
		}

		// Token: 0x06017833 RID: 96307 RVA: 0x0001F890 File Offset: 0x0001E890
		public override void onkeydown()
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate();
				return;
			}
		}

		// Token: 0x06017834 RID: 96308 RVA: 0x0001F8BC File Offset: 0x0001E8BC
		public override bool onkeypress()
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate();
		}

		// Token: 0x06017835 RID: 96309 RVA: 0x0001F8E8 File Offset: 0x0001E8E8
		public override bool ondblclick()
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate();
		}

		// Token: 0x06017836 RID: 96310 RVA: 0x0001F914 File Offset: 0x0001E914
		public override bool onclick()
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate();
		}

		// Token: 0x06017837 RID: 96311 RVA: 0x0001F940 File Offset: 0x0001E940
		public override bool onhelp()
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate();
		}

		// Token: 0x06017838 RID: 96312 RVA: 0x0001F96C File Offset: 0x0001E96C
		internal HTMLControlElementEvents_SinkHelper()
		{
			this.m_dwCookie = 0;
			this.m_onfocusoutDelegate = null;
			this.m_onfocusinDelegate = null;
			this.m_ondeactivateDelegate = null;
			this.m_onactivateDelegate = null;
			this.m_onmousewheelDelegate = null;
			this.m_onmouseleaveDelegate = null;
			this.m_onmouseenterDelegate = null;
			this.m_onresizeendDelegate = null;
			this.m_onresizestartDelegate = null;
			this.m_onmoveendDelegate = null;
			this.m_onmovestartDelegate = null;
			this.m_oncontrolselectDelegate = null;
			this.m_onmoveDelegate = null;
			this.m_onbeforeactivateDelegate = null;
			this.m_onbeforedeactivateDelegate = null;
			this.m_onpageDelegate = null;
			this.m_onlayoutcompleteDelegate = null;
			this.m_onbeforeeditfocusDelegate = null;
			this.m_onreadystatechangeDelegate = null;
			this.m_oncellchangeDelegate = null;
			this.m_onrowsinsertedDelegate = null;
			this.m_onrowsdeleteDelegate = null;
			this.m_oncontextmenuDelegate = null;
			this.m_onpasteDelegate = null;
			this.m_onbeforepasteDelegate = null;
			this.m_oncopyDelegate = null;
			this.m_onbeforecopyDelegate = null;
			this.m_oncutDelegate = null;
			this.m_onbeforecutDelegate = null;
			this.m_ondropDelegate = null;
			this.m_ondragleaveDelegate = null;
			this.m_ondragoverDelegate = null;
			this.m_ondragenterDelegate = null;
			this.m_ondragendDelegate = null;
			this.m_ondragDelegate = null;
			this.m_onresizeDelegate = null;
			this.m_onblurDelegate = null;
			this.m_onfocusDelegate = null;
			this.m_onscrollDelegate = null;
			this.m_onpropertychangeDelegate = null;
			this.m_onlosecaptureDelegate = null;
			this.m_ondatasetcompleteDelegate = null;
			this.m_ondataavailableDelegate = null;
			this.m_ondatasetchangedDelegate = null;
			this.m_onrowenterDelegate = null;
			this.m_onrowexitDelegate = null;
			this.m_onerrorupdateDelegate = null;
			this.m_onafterupdateDelegate = null;
			this.m_onbeforeupdateDelegate = null;
			this.m_ondragstartDelegate = null;
			this.m_onfilterchangeDelegate = null;
			this.m_onselectstartDelegate = null;
			this.m_onmouseupDelegate = null;
			this.m_onmousedownDelegate = null;
			this.m_onmousemoveDelegate = null;
			this.m_onmouseoverDelegate = null;
			this.m_onmouseoutDelegate = null;
			this.m_onkeyupDelegate = null;
			this.m_onkeydownDelegate = null;
			this.m_onkeypressDelegate = null;
			this.m_ondblclickDelegate = null;
			this.m_onclickDelegate = null;
			this.m_onhelpDelegate = null;
		}

		// Token: 0x040005D4 RID: 1492
		public HTMLControlElementEvents_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x040005D5 RID: 1493
		public HTMLControlElementEvents_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x040005D6 RID: 1494
		public HTMLControlElementEvents_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x040005D7 RID: 1495
		public HTMLControlElementEvents_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x040005D8 RID: 1496
		public HTMLControlElementEvents_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x040005D9 RID: 1497
		public HTMLControlElementEvents_onmouseleaveEventHandler m_onmouseleaveDelegate;

		// Token: 0x040005DA RID: 1498
		public HTMLControlElementEvents_onmouseenterEventHandler m_onmouseenterDelegate;

		// Token: 0x040005DB RID: 1499
		public HTMLControlElementEvents_onresizeendEventHandler m_onresizeendDelegate;

		// Token: 0x040005DC RID: 1500
		public HTMLControlElementEvents_onresizestartEventHandler m_onresizestartDelegate;

		// Token: 0x040005DD RID: 1501
		public HTMLControlElementEvents_onmoveendEventHandler m_onmoveendDelegate;

		// Token: 0x040005DE RID: 1502
		public HTMLControlElementEvents_onmovestartEventHandler m_onmovestartDelegate;

		// Token: 0x040005DF RID: 1503
		public HTMLControlElementEvents_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x040005E0 RID: 1504
		public HTMLControlElementEvents_onmoveEventHandler m_onmoveDelegate;

		// Token: 0x040005E1 RID: 1505
		public HTMLControlElementEvents_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x040005E2 RID: 1506
		public HTMLControlElementEvents_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x040005E3 RID: 1507
		public HTMLControlElementEvents_onpageEventHandler m_onpageDelegate;

		// Token: 0x040005E4 RID: 1508
		public HTMLControlElementEvents_onlayoutcompleteEventHandler m_onlayoutcompleteDelegate;

		// Token: 0x040005E5 RID: 1509
		public HTMLControlElementEvents_onbeforeeditfocusEventHandler m_onbeforeeditfocusDelegate;

		// Token: 0x040005E6 RID: 1510
		public HTMLControlElementEvents_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x040005E7 RID: 1511
		public HTMLControlElementEvents_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x040005E8 RID: 1512
		public HTMLControlElementEvents_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x040005E9 RID: 1513
		public HTMLControlElementEvents_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x040005EA RID: 1514
		public HTMLControlElementEvents_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x040005EB RID: 1515
		public HTMLControlElementEvents_onpasteEventHandler m_onpasteDelegate;

		// Token: 0x040005EC RID: 1516
		public HTMLControlElementEvents_onbeforepasteEventHandler m_onbeforepasteDelegate;

		// Token: 0x040005ED RID: 1517
		public HTMLControlElementEvents_oncopyEventHandler m_oncopyDelegate;

		// Token: 0x040005EE RID: 1518
		public HTMLControlElementEvents_onbeforecopyEventHandler m_onbeforecopyDelegate;

		// Token: 0x040005EF RID: 1519
		public HTMLControlElementEvents_oncutEventHandler m_oncutDelegate;

		// Token: 0x040005F0 RID: 1520
		public HTMLControlElementEvents_onbeforecutEventHandler m_onbeforecutDelegate;

		// Token: 0x040005F1 RID: 1521
		public HTMLControlElementEvents_ondropEventHandler m_ondropDelegate;

		// Token: 0x040005F2 RID: 1522
		public HTMLControlElementEvents_ondragleaveEventHandler m_ondragleaveDelegate;

		// Token: 0x040005F3 RID: 1523
		public HTMLControlElementEvents_ondragoverEventHandler m_ondragoverDelegate;

		// Token: 0x040005F4 RID: 1524
		public HTMLControlElementEvents_ondragenterEventHandler m_ondragenterDelegate;

		// Token: 0x040005F5 RID: 1525
		public HTMLControlElementEvents_ondragendEventHandler m_ondragendDelegate;

		// Token: 0x040005F6 RID: 1526
		public HTMLControlElementEvents_ondragEventHandler m_ondragDelegate;

		// Token: 0x040005F7 RID: 1527
		public HTMLControlElementEvents_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x040005F8 RID: 1528
		public HTMLControlElementEvents_onblurEventHandler m_onblurDelegate;

		// Token: 0x040005F9 RID: 1529
		public HTMLControlElementEvents_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x040005FA RID: 1530
		public HTMLControlElementEvents_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x040005FB RID: 1531
		public HTMLControlElementEvents_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x040005FC RID: 1532
		public HTMLControlElementEvents_onlosecaptureEventHandler m_onlosecaptureDelegate;

		// Token: 0x040005FD RID: 1533
		public HTMLControlElementEvents_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x040005FE RID: 1534
		public HTMLControlElementEvents_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x040005FF RID: 1535
		public HTMLControlElementEvents_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x04000600 RID: 1536
		public HTMLControlElementEvents_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x04000601 RID: 1537
		public HTMLControlElementEvents_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x04000602 RID: 1538
		public HTMLControlElementEvents_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x04000603 RID: 1539
		public HTMLControlElementEvents_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x04000604 RID: 1540
		public HTMLControlElementEvents_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x04000605 RID: 1541
		public HTMLControlElementEvents_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x04000606 RID: 1542
		public HTMLControlElementEvents_onfilterchangeEventHandler m_onfilterchangeDelegate;

		// Token: 0x04000607 RID: 1543
		public HTMLControlElementEvents_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x04000608 RID: 1544
		public HTMLControlElementEvents_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x04000609 RID: 1545
		public HTMLControlElementEvents_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x0400060A RID: 1546
		public HTMLControlElementEvents_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x0400060B RID: 1547
		public HTMLControlElementEvents_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x0400060C RID: 1548
		public HTMLControlElementEvents_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x0400060D RID: 1549
		public HTMLControlElementEvents_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x0400060E RID: 1550
		public HTMLControlElementEvents_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x0400060F RID: 1551
		public HTMLControlElementEvents_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x04000610 RID: 1552
		public HTMLControlElementEvents_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x04000611 RID: 1553
		public HTMLControlElementEvents_onclickEventHandler m_onclickDelegate;

		// Token: 0x04000612 RID: 1554
		public HTMLControlElementEvents_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x04000613 RID: 1555
		public int m_dwCookie;
	}
}
