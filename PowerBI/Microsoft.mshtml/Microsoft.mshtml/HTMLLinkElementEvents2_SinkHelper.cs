using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DC0 RID: 3520
	[ClassInterface(0)]
	internal sealed class HTMLLinkElementEvents2_SinkHelper : HTMLLinkElementEvents2
	{
		// Token: 0x0601758D RID: 95629 RVA: 0x00008D90 File Offset: 0x00007D90
		public override void onerror(IHTMLEventObj A_1)
		{
			if (this.m_onerrorDelegate != null)
			{
				this.m_onerrorDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601758E RID: 95630 RVA: 0x00008DC0 File Offset: 0x00007DC0
		public override void onload(IHTMLEventObj A_1)
		{
			if (this.m_onloadDelegate != null)
			{
				this.m_onloadDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601758F RID: 95631 RVA: 0x00008DF0 File Offset: 0x00007DF0
		public override bool onmousewheel(IHTMLEventObj A_1)
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate(A_1);
		}

		// Token: 0x06017590 RID: 95632 RVA: 0x00008E20 File Offset: 0x00007E20
		public override void onresizeend(IHTMLEventObj A_1)
		{
			if (this.m_onresizeendDelegate != null)
			{
				this.m_onresizeendDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017591 RID: 95633 RVA: 0x00008E50 File Offset: 0x00007E50
		public override bool onresizestart(IHTMLEventObj A_1)
		{
			return this.m_onresizestartDelegate != null && this.m_onresizestartDelegate(A_1);
		}

		// Token: 0x06017592 RID: 95634 RVA: 0x00008E80 File Offset: 0x00007E80
		public override void onmoveend(IHTMLEventObj A_1)
		{
			if (this.m_onmoveendDelegate != null)
			{
				this.m_onmoveendDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017593 RID: 95635 RVA: 0x00008EB0 File Offset: 0x00007EB0
		public override bool onmovestart(IHTMLEventObj A_1)
		{
			return this.m_onmovestartDelegate != null && this.m_onmovestartDelegate(A_1);
		}

		// Token: 0x06017594 RID: 95636 RVA: 0x00008EE0 File Offset: 0x00007EE0
		public override bool oncontrolselect(IHTMLEventObj A_1)
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate(A_1);
		}

		// Token: 0x06017595 RID: 95637 RVA: 0x00008F10 File Offset: 0x00007F10
		public override void onmove(IHTMLEventObj A_1)
		{
			if (this.m_onmoveDelegate != null)
			{
				this.m_onmoveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017596 RID: 95638 RVA: 0x00008F40 File Offset: 0x00007F40
		public override void onfocusout(IHTMLEventObj A_1)
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017597 RID: 95639 RVA: 0x00008F70 File Offset: 0x00007F70
		public override void onfocusin(IHTMLEventObj A_1)
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017598 RID: 95640 RVA: 0x00008FA0 File Offset: 0x00007FA0
		public override bool onbeforeactivate(IHTMLEventObj A_1)
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate(A_1);
		}

		// Token: 0x06017599 RID: 95641 RVA: 0x00008FD0 File Offset: 0x00007FD0
		public override bool onbeforedeactivate(IHTMLEventObj A_1)
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate(A_1);
		}

		// Token: 0x0601759A RID: 95642 RVA: 0x00009000 File Offset: 0x00008000
		public override void ondeactivate(IHTMLEventObj A_1)
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601759B RID: 95643 RVA: 0x00009030 File Offset: 0x00008030
		public override void onactivate(IHTMLEventObj A_1)
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601759C RID: 95644 RVA: 0x00009060 File Offset: 0x00008060
		public override void onmouseleave(IHTMLEventObj A_1)
		{
			if (this.m_onmouseleaveDelegate != null)
			{
				this.m_onmouseleaveDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601759D RID: 95645 RVA: 0x00009090 File Offset: 0x00008090
		public override void onmouseenter(IHTMLEventObj A_1)
		{
			if (this.m_onmouseenterDelegate != null)
			{
				this.m_onmouseenterDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601759E RID: 95646 RVA: 0x000090C0 File Offset: 0x000080C0
		public override void onpage(IHTMLEventObj A_1)
		{
			if (this.m_onpageDelegate != null)
			{
				this.m_onpageDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601759F RID: 95647 RVA: 0x000090F0 File Offset: 0x000080F0
		public override void onlayoutcomplete(IHTMLEventObj A_1)
		{
			if (this.m_onlayoutcompleteDelegate != null)
			{
				this.m_onlayoutcompleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x060175A0 RID: 95648 RVA: 0x00009120 File Offset: 0x00008120
		public override void onreadystatechange(IHTMLEventObj A_1)
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x060175A1 RID: 95649 RVA: 0x00009150 File Offset: 0x00008150
		public override void oncellchange(IHTMLEventObj A_1)
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x060175A2 RID: 95650 RVA: 0x00009180 File Offset: 0x00008180
		public override void onrowsinserted(IHTMLEventObj A_1)
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate(A_1);
				return;
			}
		}

		// Token: 0x060175A3 RID: 95651 RVA: 0x000091B0 File Offset: 0x000081B0
		public override void onrowsdelete(IHTMLEventObj A_1)
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x060175A4 RID: 95652 RVA: 0x000091E0 File Offset: 0x000081E0
		public override bool oncontextmenu(IHTMLEventObj A_1)
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate(A_1);
		}

		// Token: 0x060175A5 RID: 95653 RVA: 0x00009210 File Offset: 0x00008210
		public override bool onpaste(IHTMLEventObj A_1)
		{
			return this.m_onpasteDelegate != null && this.m_onpasteDelegate(A_1);
		}

		// Token: 0x060175A6 RID: 95654 RVA: 0x00009240 File Offset: 0x00008240
		public override bool onbeforepaste(IHTMLEventObj A_1)
		{
			return this.m_onbeforepasteDelegate != null && this.m_onbeforepasteDelegate(A_1);
		}

		// Token: 0x060175A7 RID: 95655 RVA: 0x00009270 File Offset: 0x00008270
		public override bool oncopy(IHTMLEventObj A_1)
		{
			return this.m_oncopyDelegate != null && this.m_oncopyDelegate(A_1);
		}

		// Token: 0x060175A8 RID: 95656 RVA: 0x000092A0 File Offset: 0x000082A0
		public override bool onbeforecopy(IHTMLEventObj A_1)
		{
			return this.m_onbeforecopyDelegate != null && this.m_onbeforecopyDelegate(A_1);
		}

		// Token: 0x060175A9 RID: 95657 RVA: 0x000092D0 File Offset: 0x000082D0
		public override bool oncut(IHTMLEventObj A_1)
		{
			return this.m_oncutDelegate != null && this.m_oncutDelegate(A_1);
		}

		// Token: 0x060175AA RID: 95658 RVA: 0x00009300 File Offset: 0x00008300
		public override bool onbeforecut(IHTMLEventObj A_1)
		{
			return this.m_onbeforecutDelegate != null && this.m_onbeforecutDelegate(A_1);
		}

		// Token: 0x060175AB RID: 95659 RVA: 0x00009330 File Offset: 0x00008330
		public override bool ondrop(IHTMLEventObj A_1)
		{
			return this.m_ondropDelegate != null && this.m_ondropDelegate(A_1);
		}

		// Token: 0x060175AC RID: 95660 RVA: 0x00009360 File Offset: 0x00008360
		public override void ondragleave(IHTMLEventObj A_1)
		{
			if (this.m_ondragleaveDelegate != null)
			{
				this.m_ondragleaveDelegate(A_1);
				return;
			}
		}

		// Token: 0x060175AD RID: 95661 RVA: 0x00009390 File Offset: 0x00008390
		public override bool ondragover(IHTMLEventObj A_1)
		{
			return this.m_ondragoverDelegate != null && this.m_ondragoverDelegate(A_1);
		}

		// Token: 0x060175AE RID: 95662 RVA: 0x000093C0 File Offset: 0x000083C0
		public override bool ondragenter(IHTMLEventObj A_1)
		{
			return this.m_ondragenterDelegate != null && this.m_ondragenterDelegate(A_1);
		}

		// Token: 0x060175AF RID: 95663 RVA: 0x000093F0 File Offset: 0x000083F0
		public override void ondragend(IHTMLEventObj A_1)
		{
			if (this.m_ondragendDelegate != null)
			{
				this.m_ondragendDelegate(A_1);
				return;
			}
		}

		// Token: 0x060175B0 RID: 95664 RVA: 0x00009420 File Offset: 0x00008420
		public override bool ondrag(IHTMLEventObj A_1)
		{
			return this.m_ondragDelegate != null && this.m_ondragDelegate(A_1);
		}

		// Token: 0x060175B1 RID: 95665 RVA: 0x00009450 File Offset: 0x00008450
		public override void onresize(IHTMLEventObj A_1)
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate(A_1);
				return;
			}
		}

		// Token: 0x060175B2 RID: 95666 RVA: 0x00009480 File Offset: 0x00008480
		public override void onblur(IHTMLEventObj A_1)
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate(A_1);
				return;
			}
		}

		// Token: 0x060175B3 RID: 95667 RVA: 0x000094B0 File Offset: 0x000084B0
		public override void onfocus(IHTMLEventObj A_1)
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate(A_1);
				return;
			}
		}

		// Token: 0x060175B4 RID: 95668 RVA: 0x000094E0 File Offset: 0x000084E0
		public override void onscroll(IHTMLEventObj A_1)
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate(A_1);
				return;
			}
		}

		// Token: 0x060175B5 RID: 95669 RVA: 0x00009510 File Offset: 0x00008510
		public override void onpropertychange(IHTMLEventObj A_1)
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x060175B6 RID: 95670 RVA: 0x00009540 File Offset: 0x00008540
		public override void onlosecapture(IHTMLEventObj A_1)
		{
			if (this.m_onlosecaptureDelegate != null)
			{
				this.m_onlosecaptureDelegate(A_1);
				return;
			}
		}

		// Token: 0x060175B7 RID: 95671 RVA: 0x00009570 File Offset: 0x00008570
		public override void ondatasetcomplete(IHTMLEventObj A_1)
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x060175B8 RID: 95672 RVA: 0x000095A0 File Offset: 0x000085A0
		public override void ondataavailable(IHTMLEventObj A_1)
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate(A_1);
				return;
			}
		}

		// Token: 0x060175B9 RID: 95673 RVA: 0x000095D0 File Offset: 0x000085D0
		public override void ondatasetchanged(IHTMLEventObj A_1)
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate(A_1);
				return;
			}
		}

		// Token: 0x060175BA RID: 95674 RVA: 0x00009600 File Offset: 0x00008600
		public override void onrowenter(IHTMLEventObj A_1)
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate(A_1);
				return;
			}
		}

		// Token: 0x060175BB RID: 95675 RVA: 0x00009630 File Offset: 0x00008630
		public override bool onrowexit(IHTMLEventObj A_1)
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate(A_1);
		}

		// Token: 0x060175BC RID: 95676 RVA: 0x00009660 File Offset: 0x00008660
		public override bool onerrorupdate(IHTMLEventObj A_1)
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate(A_1);
		}

		// Token: 0x060175BD RID: 95677 RVA: 0x00009690 File Offset: 0x00008690
		public override void onafterupdate(IHTMLEventObj A_1)
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate(A_1);
				return;
			}
		}

		// Token: 0x060175BE RID: 95678 RVA: 0x000096C0 File Offset: 0x000086C0
		public override bool onbeforeupdate(IHTMLEventObj A_1)
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate(A_1);
		}

		// Token: 0x060175BF RID: 95679 RVA: 0x000096F0 File Offset: 0x000086F0
		public override bool ondragstart(IHTMLEventObj A_1)
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate(A_1);
		}

		// Token: 0x060175C0 RID: 95680 RVA: 0x00009720 File Offset: 0x00008720
		public override void onfilterchange(IHTMLEventObj A_1)
		{
			if (this.m_onfilterchangeDelegate != null)
			{
				this.m_onfilterchangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x060175C1 RID: 95681 RVA: 0x00009750 File Offset: 0x00008750
		public override bool onselectstart(IHTMLEventObj A_1)
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate(A_1);
		}

		// Token: 0x060175C2 RID: 95682 RVA: 0x00009780 File Offset: 0x00008780
		public override void onmouseup(IHTMLEventObj A_1)
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate(A_1);
				return;
			}
		}

		// Token: 0x060175C3 RID: 95683 RVA: 0x000097B0 File Offset: 0x000087B0
		public override void onmousedown(IHTMLEventObj A_1)
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate(A_1);
				return;
			}
		}

		// Token: 0x060175C4 RID: 95684 RVA: 0x000097E0 File Offset: 0x000087E0
		public override void onmousemove(IHTMLEventObj A_1)
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate(A_1);
				return;
			}
		}

		// Token: 0x060175C5 RID: 95685 RVA: 0x00009810 File Offset: 0x00008810
		public override void onmouseover(IHTMLEventObj A_1)
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate(A_1);
				return;
			}
		}

		// Token: 0x060175C6 RID: 95686 RVA: 0x00009840 File Offset: 0x00008840
		public override void onmouseout(IHTMLEventObj A_1)
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate(A_1);
				return;
			}
		}

		// Token: 0x060175C7 RID: 95687 RVA: 0x00009870 File Offset: 0x00008870
		public override void onkeyup(IHTMLEventObj A_1)
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate(A_1);
				return;
			}
		}

		// Token: 0x060175C8 RID: 95688 RVA: 0x000098A0 File Offset: 0x000088A0
		public override void onkeydown(IHTMLEventObj A_1)
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate(A_1);
				return;
			}
		}

		// Token: 0x060175C9 RID: 95689 RVA: 0x000098D0 File Offset: 0x000088D0
		public override bool onkeypress(IHTMLEventObj A_1)
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate(A_1);
		}

		// Token: 0x060175CA RID: 95690 RVA: 0x00009900 File Offset: 0x00008900
		public override bool ondblclick(IHTMLEventObj A_1)
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate(A_1);
		}

		// Token: 0x060175CB RID: 95691 RVA: 0x00009930 File Offset: 0x00008930
		public override bool onclick(IHTMLEventObj A_1)
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate(A_1);
		}

		// Token: 0x060175CC RID: 95692 RVA: 0x00009960 File Offset: 0x00008960
		public override bool onhelp(IHTMLEventObj A_1)
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate(A_1);
		}

		// Token: 0x060175CD RID: 95693 RVA: 0x00009990 File Offset: 0x00008990
		internal HTMLLinkElementEvents2_SinkHelper()
		{
			this.m_dwCookie = 0;
			this.m_onerrorDelegate = null;
			this.m_onloadDelegate = null;
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

		// Token: 0x040004FC RID: 1276
		public HTMLLinkElementEvents2_onerrorEventHandler m_onerrorDelegate;

		// Token: 0x040004FD RID: 1277
		public HTMLLinkElementEvents2_onloadEventHandler m_onloadDelegate;

		// Token: 0x040004FE RID: 1278
		public HTMLLinkElementEvents2_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x040004FF RID: 1279
		public HTMLLinkElementEvents2_onresizeendEventHandler m_onresizeendDelegate;

		// Token: 0x04000500 RID: 1280
		public HTMLLinkElementEvents2_onresizestartEventHandler m_onresizestartDelegate;

		// Token: 0x04000501 RID: 1281
		public HTMLLinkElementEvents2_onmoveendEventHandler m_onmoveendDelegate;

		// Token: 0x04000502 RID: 1282
		public HTMLLinkElementEvents2_onmovestartEventHandler m_onmovestartDelegate;

		// Token: 0x04000503 RID: 1283
		public HTMLLinkElementEvents2_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x04000504 RID: 1284
		public HTMLLinkElementEvents2_onmoveEventHandler m_onmoveDelegate;

		// Token: 0x04000505 RID: 1285
		public HTMLLinkElementEvents2_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x04000506 RID: 1286
		public HTMLLinkElementEvents2_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x04000507 RID: 1287
		public HTMLLinkElementEvents2_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x04000508 RID: 1288
		public HTMLLinkElementEvents2_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x04000509 RID: 1289
		public HTMLLinkElementEvents2_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x0400050A RID: 1290
		public HTMLLinkElementEvents2_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x0400050B RID: 1291
		public HTMLLinkElementEvents2_onmouseleaveEventHandler m_onmouseleaveDelegate;

		// Token: 0x0400050C RID: 1292
		public HTMLLinkElementEvents2_onmouseenterEventHandler m_onmouseenterDelegate;

		// Token: 0x0400050D RID: 1293
		public HTMLLinkElementEvents2_onpageEventHandler m_onpageDelegate;

		// Token: 0x0400050E RID: 1294
		public HTMLLinkElementEvents2_onlayoutcompleteEventHandler m_onlayoutcompleteDelegate;

		// Token: 0x0400050F RID: 1295
		public HTMLLinkElementEvents2_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x04000510 RID: 1296
		public HTMLLinkElementEvents2_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x04000511 RID: 1297
		public HTMLLinkElementEvents2_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x04000512 RID: 1298
		public HTMLLinkElementEvents2_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x04000513 RID: 1299
		public HTMLLinkElementEvents2_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x04000514 RID: 1300
		public HTMLLinkElementEvents2_onpasteEventHandler m_onpasteDelegate;

		// Token: 0x04000515 RID: 1301
		public HTMLLinkElementEvents2_onbeforepasteEventHandler m_onbeforepasteDelegate;

		// Token: 0x04000516 RID: 1302
		public HTMLLinkElementEvents2_oncopyEventHandler m_oncopyDelegate;

		// Token: 0x04000517 RID: 1303
		public HTMLLinkElementEvents2_onbeforecopyEventHandler m_onbeforecopyDelegate;

		// Token: 0x04000518 RID: 1304
		public HTMLLinkElementEvents2_oncutEventHandler m_oncutDelegate;

		// Token: 0x04000519 RID: 1305
		public HTMLLinkElementEvents2_onbeforecutEventHandler m_onbeforecutDelegate;

		// Token: 0x0400051A RID: 1306
		public HTMLLinkElementEvents2_ondropEventHandler m_ondropDelegate;

		// Token: 0x0400051B RID: 1307
		public HTMLLinkElementEvents2_ondragleaveEventHandler m_ondragleaveDelegate;

		// Token: 0x0400051C RID: 1308
		public HTMLLinkElementEvents2_ondragoverEventHandler m_ondragoverDelegate;

		// Token: 0x0400051D RID: 1309
		public HTMLLinkElementEvents2_ondragenterEventHandler m_ondragenterDelegate;

		// Token: 0x0400051E RID: 1310
		public HTMLLinkElementEvents2_ondragendEventHandler m_ondragendDelegate;

		// Token: 0x0400051F RID: 1311
		public HTMLLinkElementEvents2_ondragEventHandler m_ondragDelegate;

		// Token: 0x04000520 RID: 1312
		public HTMLLinkElementEvents2_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x04000521 RID: 1313
		public HTMLLinkElementEvents2_onblurEventHandler m_onblurDelegate;

		// Token: 0x04000522 RID: 1314
		public HTMLLinkElementEvents2_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x04000523 RID: 1315
		public HTMLLinkElementEvents2_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x04000524 RID: 1316
		public HTMLLinkElementEvents2_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x04000525 RID: 1317
		public HTMLLinkElementEvents2_onlosecaptureEventHandler m_onlosecaptureDelegate;

		// Token: 0x04000526 RID: 1318
		public HTMLLinkElementEvents2_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x04000527 RID: 1319
		public HTMLLinkElementEvents2_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x04000528 RID: 1320
		public HTMLLinkElementEvents2_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x04000529 RID: 1321
		public HTMLLinkElementEvents2_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x0400052A RID: 1322
		public HTMLLinkElementEvents2_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x0400052B RID: 1323
		public HTMLLinkElementEvents2_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x0400052C RID: 1324
		public HTMLLinkElementEvents2_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x0400052D RID: 1325
		public HTMLLinkElementEvents2_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x0400052E RID: 1326
		public HTMLLinkElementEvents2_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x0400052F RID: 1327
		public HTMLLinkElementEvents2_onfilterchangeEventHandler m_onfilterchangeDelegate;

		// Token: 0x04000530 RID: 1328
		public HTMLLinkElementEvents2_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x04000531 RID: 1329
		public HTMLLinkElementEvents2_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x04000532 RID: 1330
		public HTMLLinkElementEvents2_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x04000533 RID: 1331
		public HTMLLinkElementEvents2_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x04000534 RID: 1332
		public HTMLLinkElementEvents2_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x04000535 RID: 1333
		public HTMLLinkElementEvents2_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x04000536 RID: 1334
		public HTMLLinkElementEvents2_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x04000537 RID: 1335
		public HTMLLinkElementEvents2_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x04000538 RID: 1336
		public HTMLLinkElementEvents2_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x04000539 RID: 1337
		public HTMLLinkElementEvents2_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x0400053A RID: 1338
		public HTMLLinkElementEvents2_onclickEventHandler m_onclickDelegate;

		// Token: 0x0400053B RID: 1339
		public HTMLLinkElementEvents2_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x0400053C RID: 1340
		public int m_dwCookie;
	}
}
