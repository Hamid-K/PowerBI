using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DBE RID: 3518
	[ClassInterface(0)]
	internal sealed class HTMLControlElementEvents2_SinkHelper : HTMLControlElementEvents2
	{
		// Token: 0x060174CE RID: 95438 RVA: 0x00002050 File Offset: 0x00001050
		public override bool onmousewheel(IHTMLEventObj A_1)
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate(A_1);
		}

		// Token: 0x060174CF RID: 95439 RVA: 0x00002080 File Offset: 0x00001080
		public override void onresizeend(IHTMLEventObj A_1)
		{
			if (this.m_onresizeendDelegate != null)
			{
				this.m_onresizeendDelegate(A_1);
				return;
			}
		}

		// Token: 0x060174D0 RID: 95440 RVA: 0x000020B0 File Offset: 0x000010B0
		public override bool onresizestart(IHTMLEventObj A_1)
		{
			return this.m_onresizestartDelegate != null && this.m_onresizestartDelegate(A_1);
		}

		// Token: 0x060174D1 RID: 95441 RVA: 0x000020E0 File Offset: 0x000010E0
		public override void onmoveend(IHTMLEventObj A_1)
		{
			if (this.m_onmoveendDelegate != null)
			{
				this.m_onmoveendDelegate(A_1);
				return;
			}
		}

		// Token: 0x060174D2 RID: 95442 RVA: 0x00002110 File Offset: 0x00001110
		public override bool onmovestart(IHTMLEventObj A_1)
		{
			return this.m_onmovestartDelegate != null && this.m_onmovestartDelegate(A_1);
		}

		// Token: 0x060174D3 RID: 95443 RVA: 0x00002140 File Offset: 0x00001140
		public override bool oncontrolselect(IHTMLEventObj A_1)
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate(A_1);
		}

		// Token: 0x060174D4 RID: 95444 RVA: 0x00002170 File Offset: 0x00001170
		public override void onmove(IHTMLEventObj A_1)
		{
			if (this.m_onmoveDelegate != null)
			{
				this.m_onmoveDelegate(A_1);
				return;
			}
		}

		// Token: 0x060174D5 RID: 95445 RVA: 0x000021A0 File Offset: 0x000011A0
		public override void onfocusout(IHTMLEventObj A_1)
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate(A_1);
				return;
			}
		}

		// Token: 0x060174D6 RID: 95446 RVA: 0x000021D0 File Offset: 0x000011D0
		public override void onfocusin(IHTMLEventObj A_1)
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate(A_1);
				return;
			}
		}

		// Token: 0x060174D7 RID: 95447 RVA: 0x00002200 File Offset: 0x00001200
		public override bool onbeforeactivate(IHTMLEventObj A_1)
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate(A_1);
		}

		// Token: 0x060174D8 RID: 95448 RVA: 0x00002230 File Offset: 0x00001230
		public override bool onbeforedeactivate(IHTMLEventObj A_1)
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate(A_1);
		}

		// Token: 0x060174D9 RID: 95449 RVA: 0x00002260 File Offset: 0x00001260
		public override void ondeactivate(IHTMLEventObj A_1)
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate(A_1);
				return;
			}
		}

		// Token: 0x060174DA RID: 95450 RVA: 0x00002290 File Offset: 0x00001290
		public override void onactivate(IHTMLEventObj A_1)
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate(A_1);
				return;
			}
		}

		// Token: 0x060174DB RID: 95451 RVA: 0x000022C0 File Offset: 0x000012C0
		public override void onmouseleave(IHTMLEventObj A_1)
		{
			if (this.m_onmouseleaveDelegate != null)
			{
				this.m_onmouseleaveDelegate(A_1);
				return;
			}
		}

		// Token: 0x060174DC RID: 95452 RVA: 0x000022F0 File Offset: 0x000012F0
		public override void onmouseenter(IHTMLEventObj A_1)
		{
			if (this.m_onmouseenterDelegate != null)
			{
				this.m_onmouseenterDelegate(A_1);
				return;
			}
		}

		// Token: 0x060174DD RID: 95453 RVA: 0x00002320 File Offset: 0x00001320
		public override void onpage(IHTMLEventObj A_1)
		{
			if (this.m_onpageDelegate != null)
			{
				this.m_onpageDelegate(A_1);
				return;
			}
		}

		// Token: 0x060174DE RID: 95454 RVA: 0x00002350 File Offset: 0x00001350
		public override void onlayoutcomplete(IHTMLEventObj A_1)
		{
			if (this.m_onlayoutcompleteDelegate != null)
			{
				this.m_onlayoutcompleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x060174DF RID: 95455 RVA: 0x00002380 File Offset: 0x00001380
		public override void onreadystatechange(IHTMLEventObj A_1)
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x060174E0 RID: 95456 RVA: 0x000023B0 File Offset: 0x000013B0
		public override void oncellchange(IHTMLEventObj A_1)
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x060174E1 RID: 95457 RVA: 0x000023E0 File Offset: 0x000013E0
		public override void onrowsinserted(IHTMLEventObj A_1)
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate(A_1);
				return;
			}
		}

		// Token: 0x060174E2 RID: 95458 RVA: 0x00002410 File Offset: 0x00001410
		public override void onrowsdelete(IHTMLEventObj A_1)
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x060174E3 RID: 95459 RVA: 0x00002440 File Offset: 0x00001440
		public override bool oncontextmenu(IHTMLEventObj A_1)
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate(A_1);
		}

		// Token: 0x060174E4 RID: 95460 RVA: 0x00002470 File Offset: 0x00001470
		public override bool onpaste(IHTMLEventObj A_1)
		{
			return this.m_onpasteDelegate != null && this.m_onpasteDelegate(A_1);
		}

		// Token: 0x060174E5 RID: 95461 RVA: 0x000024A0 File Offset: 0x000014A0
		public override bool onbeforepaste(IHTMLEventObj A_1)
		{
			return this.m_onbeforepasteDelegate != null && this.m_onbeforepasteDelegate(A_1);
		}

		// Token: 0x060174E6 RID: 95462 RVA: 0x000024D0 File Offset: 0x000014D0
		public override bool oncopy(IHTMLEventObj A_1)
		{
			return this.m_oncopyDelegate != null && this.m_oncopyDelegate(A_1);
		}

		// Token: 0x060174E7 RID: 95463 RVA: 0x00002500 File Offset: 0x00001500
		public override bool onbeforecopy(IHTMLEventObj A_1)
		{
			return this.m_onbeforecopyDelegate != null && this.m_onbeforecopyDelegate(A_1);
		}

		// Token: 0x060174E8 RID: 95464 RVA: 0x00002530 File Offset: 0x00001530
		public override bool oncut(IHTMLEventObj A_1)
		{
			return this.m_oncutDelegate != null && this.m_oncutDelegate(A_1);
		}

		// Token: 0x060174E9 RID: 95465 RVA: 0x00002560 File Offset: 0x00001560
		public override bool onbeforecut(IHTMLEventObj A_1)
		{
			return this.m_onbeforecutDelegate != null && this.m_onbeforecutDelegate(A_1);
		}

		// Token: 0x060174EA RID: 95466 RVA: 0x00002590 File Offset: 0x00001590
		public override bool ondrop(IHTMLEventObj A_1)
		{
			return this.m_ondropDelegate != null && this.m_ondropDelegate(A_1);
		}

		// Token: 0x060174EB RID: 95467 RVA: 0x000025C0 File Offset: 0x000015C0
		public override void ondragleave(IHTMLEventObj A_1)
		{
			if (this.m_ondragleaveDelegate != null)
			{
				this.m_ondragleaveDelegate(A_1);
				return;
			}
		}

		// Token: 0x060174EC RID: 95468 RVA: 0x000025F0 File Offset: 0x000015F0
		public override bool ondragover(IHTMLEventObj A_1)
		{
			return this.m_ondragoverDelegate != null && this.m_ondragoverDelegate(A_1);
		}

		// Token: 0x060174ED RID: 95469 RVA: 0x00002620 File Offset: 0x00001620
		public override bool ondragenter(IHTMLEventObj A_1)
		{
			return this.m_ondragenterDelegate != null && this.m_ondragenterDelegate(A_1);
		}

		// Token: 0x060174EE RID: 95470 RVA: 0x00002650 File Offset: 0x00001650
		public override void ondragend(IHTMLEventObj A_1)
		{
			if (this.m_ondragendDelegate != null)
			{
				this.m_ondragendDelegate(A_1);
				return;
			}
		}

		// Token: 0x060174EF RID: 95471 RVA: 0x00002680 File Offset: 0x00001680
		public override bool ondrag(IHTMLEventObj A_1)
		{
			return this.m_ondragDelegate != null && this.m_ondragDelegate(A_1);
		}

		// Token: 0x060174F0 RID: 95472 RVA: 0x000026B0 File Offset: 0x000016B0
		public override void onresize(IHTMLEventObj A_1)
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate(A_1);
				return;
			}
		}

		// Token: 0x060174F1 RID: 95473 RVA: 0x000026E0 File Offset: 0x000016E0
		public override void onblur(IHTMLEventObj A_1)
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate(A_1);
				return;
			}
		}

		// Token: 0x060174F2 RID: 95474 RVA: 0x00002710 File Offset: 0x00001710
		public override void onfocus(IHTMLEventObj A_1)
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate(A_1);
				return;
			}
		}

		// Token: 0x060174F3 RID: 95475 RVA: 0x00002740 File Offset: 0x00001740
		public override void onscroll(IHTMLEventObj A_1)
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate(A_1);
				return;
			}
		}

		// Token: 0x060174F4 RID: 95476 RVA: 0x00002770 File Offset: 0x00001770
		public override void onpropertychange(IHTMLEventObj A_1)
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x060174F5 RID: 95477 RVA: 0x000027A0 File Offset: 0x000017A0
		public override void onlosecapture(IHTMLEventObj A_1)
		{
			if (this.m_onlosecaptureDelegate != null)
			{
				this.m_onlosecaptureDelegate(A_1);
				return;
			}
		}

		// Token: 0x060174F6 RID: 95478 RVA: 0x000027D0 File Offset: 0x000017D0
		public override void ondatasetcomplete(IHTMLEventObj A_1)
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x060174F7 RID: 95479 RVA: 0x00002800 File Offset: 0x00001800
		public override void ondataavailable(IHTMLEventObj A_1)
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate(A_1);
				return;
			}
		}

		// Token: 0x060174F8 RID: 95480 RVA: 0x00002830 File Offset: 0x00001830
		public override void ondatasetchanged(IHTMLEventObj A_1)
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate(A_1);
				return;
			}
		}

		// Token: 0x060174F9 RID: 95481 RVA: 0x00002860 File Offset: 0x00001860
		public override void onrowenter(IHTMLEventObj A_1)
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate(A_1);
				return;
			}
		}

		// Token: 0x060174FA RID: 95482 RVA: 0x00002890 File Offset: 0x00001890
		public override bool onrowexit(IHTMLEventObj A_1)
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate(A_1);
		}

		// Token: 0x060174FB RID: 95483 RVA: 0x000028C0 File Offset: 0x000018C0
		public override bool onerrorupdate(IHTMLEventObj A_1)
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate(A_1);
		}

		// Token: 0x060174FC RID: 95484 RVA: 0x000028F0 File Offset: 0x000018F0
		public override void onafterupdate(IHTMLEventObj A_1)
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate(A_1);
				return;
			}
		}

		// Token: 0x060174FD RID: 95485 RVA: 0x00002920 File Offset: 0x00001920
		public override bool onbeforeupdate(IHTMLEventObj A_1)
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate(A_1);
		}

		// Token: 0x060174FE RID: 95486 RVA: 0x00002950 File Offset: 0x00001950
		public override bool ondragstart(IHTMLEventObj A_1)
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate(A_1);
		}

		// Token: 0x060174FF RID: 95487 RVA: 0x00002980 File Offset: 0x00001980
		public override void onfilterchange(IHTMLEventObj A_1)
		{
			if (this.m_onfilterchangeDelegate != null)
			{
				this.m_onfilterchangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017500 RID: 95488 RVA: 0x000029B0 File Offset: 0x000019B0
		public override bool onselectstart(IHTMLEventObj A_1)
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate(A_1);
		}

		// Token: 0x06017501 RID: 95489 RVA: 0x000029E0 File Offset: 0x000019E0
		public override void onmouseup(IHTMLEventObj A_1)
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017502 RID: 95490 RVA: 0x00002A10 File Offset: 0x00001A10
		public override void onmousedown(IHTMLEventObj A_1)
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017503 RID: 95491 RVA: 0x00002A40 File Offset: 0x00001A40
		public override void onmousemove(IHTMLEventObj A_1)
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017504 RID: 95492 RVA: 0x00002A70 File Offset: 0x00001A70
		public override void onmouseover(IHTMLEventObj A_1)
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017505 RID: 95493 RVA: 0x00002AA0 File Offset: 0x00001AA0
		public override void onmouseout(IHTMLEventObj A_1)
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017506 RID: 95494 RVA: 0x00002AD0 File Offset: 0x00001AD0
		public override void onkeyup(IHTMLEventObj A_1)
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017507 RID: 95495 RVA: 0x00002B00 File Offset: 0x00001B00
		public override void onkeydown(IHTMLEventObj A_1)
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017508 RID: 95496 RVA: 0x00002B30 File Offset: 0x00001B30
		public override bool onkeypress(IHTMLEventObj A_1)
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate(A_1);
		}

		// Token: 0x06017509 RID: 95497 RVA: 0x00002B60 File Offset: 0x00001B60
		public override bool ondblclick(IHTMLEventObj A_1)
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate(A_1);
		}

		// Token: 0x0601750A RID: 95498 RVA: 0x00002B90 File Offset: 0x00001B90
		public override bool onclick(IHTMLEventObj A_1)
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate(A_1);
		}

		// Token: 0x0601750B RID: 95499 RVA: 0x00002BC0 File Offset: 0x00001BC0
		public override bool onhelp(IHTMLEventObj A_1)
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate(A_1);
		}

		// Token: 0x0601750C RID: 95500 RVA: 0x00002BF0 File Offset: 0x00001BF0
		internal HTMLControlElementEvents2_SinkHelper()
		{
			this.m_dwCookie = 0;
			this.m_onmousewheelDelegate = null;
			this.m_onresizeendDelegate = null;
			this.m_onresizestartDelegate = null;
			this.m_onmoveendDelegate = null;
			this.m_onmovestartDelegate = null;
			this.m_oncontrolselectDelegate = null;
			this.m_onmoveDelegate = null;
			this.m_onfocusoutDelegate = null;
			this.m_onfocusinDelegate = null;
			this.m_onbeforeactivateDelegate = null;
			this.m_onbeforedeactivateDelegate = null;
			this.m_ondeactivateDelegate = null;
			this.m_onactivateDelegate = null;
			this.m_onmouseleaveDelegate = null;
			this.m_onmouseenterDelegate = null;
			this.m_onpageDelegate = null;
			this.m_onlayoutcompleteDelegate = null;
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

		// Token: 0x040004BA RID: 1210
		public HTMLControlElementEvents2_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x040004BB RID: 1211
		public HTMLControlElementEvents2_onresizeendEventHandler m_onresizeendDelegate;

		// Token: 0x040004BC RID: 1212
		public HTMLControlElementEvents2_onresizestartEventHandler m_onresizestartDelegate;

		// Token: 0x040004BD RID: 1213
		public HTMLControlElementEvents2_onmoveendEventHandler m_onmoveendDelegate;

		// Token: 0x040004BE RID: 1214
		public HTMLControlElementEvents2_onmovestartEventHandler m_onmovestartDelegate;

		// Token: 0x040004BF RID: 1215
		public HTMLControlElementEvents2_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x040004C0 RID: 1216
		public HTMLControlElementEvents2_onmoveEventHandler m_onmoveDelegate;

		// Token: 0x040004C1 RID: 1217
		public HTMLControlElementEvents2_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x040004C2 RID: 1218
		public HTMLControlElementEvents2_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x040004C3 RID: 1219
		public HTMLControlElementEvents2_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x040004C4 RID: 1220
		public HTMLControlElementEvents2_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x040004C5 RID: 1221
		public HTMLControlElementEvents2_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x040004C6 RID: 1222
		public HTMLControlElementEvents2_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x040004C7 RID: 1223
		public HTMLControlElementEvents2_onmouseleaveEventHandler m_onmouseleaveDelegate;

		// Token: 0x040004C8 RID: 1224
		public HTMLControlElementEvents2_onmouseenterEventHandler m_onmouseenterDelegate;

		// Token: 0x040004C9 RID: 1225
		public HTMLControlElementEvents2_onpageEventHandler m_onpageDelegate;

		// Token: 0x040004CA RID: 1226
		public HTMLControlElementEvents2_onlayoutcompleteEventHandler m_onlayoutcompleteDelegate;

		// Token: 0x040004CB RID: 1227
		public HTMLControlElementEvents2_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x040004CC RID: 1228
		public HTMLControlElementEvents2_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x040004CD RID: 1229
		public HTMLControlElementEvents2_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x040004CE RID: 1230
		public HTMLControlElementEvents2_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x040004CF RID: 1231
		public HTMLControlElementEvents2_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x040004D0 RID: 1232
		public HTMLControlElementEvents2_onpasteEventHandler m_onpasteDelegate;

		// Token: 0x040004D1 RID: 1233
		public HTMLControlElementEvents2_onbeforepasteEventHandler m_onbeforepasteDelegate;

		// Token: 0x040004D2 RID: 1234
		public HTMLControlElementEvents2_oncopyEventHandler m_oncopyDelegate;

		// Token: 0x040004D3 RID: 1235
		public HTMLControlElementEvents2_onbeforecopyEventHandler m_onbeforecopyDelegate;

		// Token: 0x040004D4 RID: 1236
		public HTMLControlElementEvents2_oncutEventHandler m_oncutDelegate;

		// Token: 0x040004D5 RID: 1237
		public HTMLControlElementEvents2_onbeforecutEventHandler m_onbeforecutDelegate;

		// Token: 0x040004D6 RID: 1238
		public HTMLControlElementEvents2_ondropEventHandler m_ondropDelegate;

		// Token: 0x040004D7 RID: 1239
		public HTMLControlElementEvents2_ondragleaveEventHandler m_ondragleaveDelegate;

		// Token: 0x040004D8 RID: 1240
		public HTMLControlElementEvents2_ondragoverEventHandler m_ondragoverDelegate;

		// Token: 0x040004D9 RID: 1241
		public HTMLControlElementEvents2_ondragenterEventHandler m_ondragenterDelegate;

		// Token: 0x040004DA RID: 1242
		public HTMLControlElementEvents2_ondragendEventHandler m_ondragendDelegate;

		// Token: 0x040004DB RID: 1243
		public HTMLControlElementEvents2_ondragEventHandler m_ondragDelegate;

		// Token: 0x040004DC RID: 1244
		public HTMLControlElementEvents2_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x040004DD RID: 1245
		public HTMLControlElementEvents2_onblurEventHandler m_onblurDelegate;

		// Token: 0x040004DE RID: 1246
		public HTMLControlElementEvents2_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x040004DF RID: 1247
		public HTMLControlElementEvents2_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x040004E0 RID: 1248
		public HTMLControlElementEvents2_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x040004E1 RID: 1249
		public HTMLControlElementEvents2_onlosecaptureEventHandler m_onlosecaptureDelegate;

		// Token: 0x040004E2 RID: 1250
		public HTMLControlElementEvents2_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x040004E3 RID: 1251
		public HTMLControlElementEvents2_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x040004E4 RID: 1252
		public HTMLControlElementEvents2_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x040004E5 RID: 1253
		public HTMLControlElementEvents2_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x040004E6 RID: 1254
		public HTMLControlElementEvents2_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x040004E7 RID: 1255
		public HTMLControlElementEvents2_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x040004E8 RID: 1256
		public HTMLControlElementEvents2_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x040004E9 RID: 1257
		public HTMLControlElementEvents2_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x040004EA RID: 1258
		public HTMLControlElementEvents2_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x040004EB RID: 1259
		public HTMLControlElementEvents2_onfilterchangeEventHandler m_onfilterchangeDelegate;

		// Token: 0x040004EC RID: 1260
		public HTMLControlElementEvents2_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x040004ED RID: 1261
		public HTMLControlElementEvents2_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x040004EE RID: 1262
		public HTMLControlElementEvents2_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x040004EF RID: 1263
		public HTMLControlElementEvents2_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x040004F0 RID: 1264
		public HTMLControlElementEvents2_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x040004F1 RID: 1265
		public HTMLControlElementEvents2_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x040004F2 RID: 1266
		public HTMLControlElementEvents2_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x040004F3 RID: 1267
		public HTMLControlElementEvents2_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x040004F4 RID: 1268
		public HTMLControlElementEvents2_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x040004F5 RID: 1269
		public HTMLControlElementEvents2_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x040004F6 RID: 1270
		public HTMLControlElementEvents2_onclickEventHandler m_onclickDelegate;

		// Token: 0x040004F7 RID: 1271
		public HTMLControlElementEvents2_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x040004F8 RID: 1272
		public int m_dwCookie;
	}
}
