using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000E10 RID: 3600
	[ClassInterface(0)]
	internal sealed class HTMLFrameSiteEvents2_SinkHelper : HTMLFrameSiteEvents2
	{
		// Token: 0x060190E0 RID: 102624 RVA: 0x00101A10 File Offset: 0x00100A10
		public override void onload(IHTMLEventObj A_1)
		{
			if (this.m_onloadDelegate != null)
			{
				this.m_onloadDelegate(A_1);
				return;
			}
		}

		// Token: 0x060190E1 RID: 102625 RVA: 0x00101A40 File Offset: 0x00100A40
		public override bool onmousewheel(IHTMLEventObj A_1)
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate(A_1);
		}

		// Token: 0x060190E2 RID: 102626 RVA: 0x00101A70 File Offset: 0x00100A70
		public override void onresizeend(IHTMLEventObj A_1)
		{
			if (this.m_onresizeendDelegate != null)
			{
				this.m_onresizeendDelegate(A_1);
				return;
			}
		}

		// Token: 0x060190E3 RID: 102627 RVA: 0x00101AA0 File Offset: 0x00100AA0
		public override bool onresizestart(IHTMLEventObj A_1)
		{
			return this.m_onresizestartDelegate != null && this.m_onresizestartDelegate(A_1);
		}

		// Token: 0x060190E4 RID: 102628 RVA: 0x00101AD0 File Offset: 0x00100AD0
		public override void onmoveend(IHTMLEventObj A_1)
		{
			if (this.m_onmoveendDelegate != null)
			{
				this.m_onmoveendDelegate(A_1);
				return;
			}
		}

		// Token: 0x060190E5 RID: 102629 RVA: 0x00101B00 File Offset: 0x00100B00
		public override bool onmovestart(IHTMLEventObj A_1)
		{
			return this.m_onmovestartDelegate != null && this.m_onmovestartDelegate(A_1);
		}

		// Token: 0x060190E6 RID: 102630 RVA: 0x00101B30 File Offset: 0x00100B30
		public override bool oncontrolselect(IHTMLEventObj A_1)
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate(A_1);
		}

		// Token: 0x060190E7 RID: 102631 RVA: 0x00101B60 File Offset: 0x00100B60
		public override void onmove(IHTMLEventObj A_1)
		{
			if (this.m_onmoveDelegate != null)
			{
				this.m_onmoveDelegate(A_1);
				return;
			}
		}

		// Token: 0x060190E8 RID: 102632 RVA: 0x00101B90 File Offset: 0x00100B90
		public override void onfocusout(IHTMLEventObj A_1)
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate(A_1);
				return;
			}
		}

		// Token: 0x060190E9 RID: 102633 RVA: 0x00101BC0 File Offset: 0x00100BC0
		public override void onfocusin(IHTMLEventObj A_1)
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate(A_1);
				return;
			}
		}

		// Token: 0x060190EA RID: 102634 RVA: 0x00101BF0 File Offset: 0x00100BF0
		public override bool onbeforeactivate(IHTMLEventObj A_1)
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate(A_1);
		}

		// Token: 0x060190EB RID: 102635 RVA: 0x00101C20 File Offset: 0x00100C20
		public override bool onbeforedeactivate(IHTMLEventObj A_1)
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate(A_1);
		}

		// Token: 0x060190EC RID: 102636 RVA: 0x00101C50 File Offset: 0x00100C50
		public override void ondeactivate(IHTMLEventObj A_1)
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate(A_1);
				return;
			}
		}

		// Token: 0x060190ED RID: 102637 RVA: 0x00101C80 File Offset: 0x00100C80
		public override void onactivate(IHTMLEventObj A_1)
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate(A_1);
				return;
			}
		}

		// Token: 0x060190EE RID: 102638 RVA: 0x00101CB0 File Offset: 0x00100CB0
		public override void onmouseleave(IHTMLEventObj A_1)
		{
			if (this.m_onmouseleaveDelegate != null)
			{
				this.m_onmouseleaveDelegate(A_1);
				return;
			}
		}

		// Token: 0x060190EF RID: 102639 RVA: 0x00101CE0 File Offset: 0x00100CE0
		public override void onmouseenter(IHTMLEventObj A_1)
		{
			if (this.m_onmouseenterDelegate != null)
			{
				this.m_onmouseenterDelegate(A_1);
				return;
			}
		}

		// Token: 0x060190F0 RID: 102640 RVA: 0x00101D10 File Offset: 0x00100D10
		public override void onpage(IHTMLEventObj A_1)
		{
			if (this.m_onpageDelegate != null)
			{
				this.m_onpageDelegate(A_1);
				return;
			}
		}

		// Token: 0x060190F1 RID: 102641 RVA: 0x00101D40 File Offset: 0x00100D40
		public override void onlayoutcomplete(IHTMLEventObj A_1)
		{
			if (this.m_onlayoutcompleteDelegate != null)
			{
				this.m_onlayoutcompleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x060190F2 RID: 102642 RVA: 0x00101D70 File Offset: 0x00100D70
		public override void onreadystatechange(IHTMLEventObj A_1)
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x060190F3 RID: 102643 RVA: 0x00101DA0 File Offset: 0x00100DA0
		public override void oncellchange(IHTMLEventObj A_1)
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x060190F4 RID: 102644 RVA: 0x00101DD0 File Offset: 0x00100DD0
		public override void onrowsinserted(IHTMLEventObj A_1)
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate(A_1);
				return;
			}
		}

		// Token: 0x060190F5 RID: 102645 RVA: 0x00101E00 File Offset: 0x00100E00
		public override void onrowsdelete(IHTMLEventObj A_1)
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x060190F6 RID: 102646 RVA: 0x00101E30 File Offset: 0x00100E30
		public override bool oncontextmenu(IHTMLEventObj A_1)
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate(A_1);
		}

		// Token: 0x060190F7 RID: 102647 RVA: 0x00101E60 File Offset: 0x00100E60
		public override bool onpaste(IHTMLEventObj A_1)
		{
			return this.m_onpasteDelegate != null && this.m_onpasteDelegate(A_1);
		}

		// Token: 0x060190F8 RID: 102648 RVA: 0x00101E90 File Offset: 0x00100E90
		public override bool onbeforepaste(IHTMLEventObj A_1)
		{
			return this.m_onbeforepasteDelegate != null && this.m_onbeforepasteDelegate(A_1);
		}

		// Token: 0x060190F9 RID: 102649 RVA: 0x00101EC0 File Offset: 0x00100EC0
		public override bool oncopy(IHTMLEventObj A_1)
		{
			return this.m_oncopyDelegate != null && this.m_oncopyDelegate(A_1);
		}

		// Token: 0x060190FA RID: 102650 RVA: 0x00101EF0 File Offset: 0x00100EF0
		public override bool onbeforecopy(IHTMLEventObj A_1)
		{
			return this.m_onbeforecopyDelegate != null && this.m_onbeforecopyDelegate(A_1);
		}

		// Token: 0x060190FB RID: 102651 RVA: 0x00101F20 File Offset: 0x00100F20
		public override bool oncut(IHTMLEventObj A_1)
		{
			return this.m_oncutDelegate != null && this.m_oncutDelegate(A_1);
		}

		// Token: 0x060190FC RID: 102652 RVA: 0x00101F50 File Offset: 0x00100F50
		public override bool onbeforecut(IHTMLEventObj A_1)
		{
			return this.m_onbeforecutDelegate != null && this.m_onbeforecutDelegate(A_1);
		}

		// Token: 0x060190FD RID: 102653 RVA: 0x00101F80 File Offset: 0x00100F80
		public override bool ondrop(IHTMLEventObj A_1)
		{
			return this.m_ondropDelegate != null && this.m_ondropDelegate(A_1);
		}

		// Token: 0x060190FE RID: 102654 RVA: 0x00101FB0 File Offset: 0x00100FB0
		public override void ondragleave(IHTMLEventObj A_1)
		{
			if (this.m_ondragleaveDelegate != null)
			{
				this.m_ondragleaveDelegate(A_1);
				return;
			}
		}

		// Token: 0x060190FF RID: 102655 RVA: 0x00101FE0 File Offset: 0x00100FE0
		public override bool ondragover(IHTMLEventObj A_1)
		{
			return this.m_ondragoverDelegate != null && this.m_ondragoverDelegate(A_1);
		}

		// Token: 0x06019100 RID: 102656 RVA: 0x00102010 File Offset: 0x00101010
		public override bool ondragenter(IHTMLEventObj A_1)
		{
			return this.m_ondragenterDelegate != null && this.m_ondragenterDelegate(A_1);
		}

		// Token: 0x06019101 RID: 102657 RVA: 0x00102040 File Offset: 0x00101040
		public override void ondragend(IHTMLEventObj A_1)
		{
			if (this.m_ondragendDelegate != null)
			{
				this.m_ondragendDelegate(A_1);
				return;
			}
		}

		// Token: 0x06019102 RID: 102658 RVA: 0x00102070 File Offset: 0x00101070
		public override bool ondrag(IHTMLEventObj A_1)
		{
			return this.m_ondragDelegate != null && this.m_ondragDelegate(A_1);
		}

		// Token: 0x06019103 RID: 102659 RVA: 0x001020A0 File Offset: 0x001010A0
		public override void onresize(IHTMLEventObj A_1)
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06019104 RID: 102660 RVA: 0x001020D0 File Offset: 0x001010D0
		public override void onblur(IHTMLEventObj A_1)
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate(A_1);
				return;
			}
		}

		// Token: 0x06019105 RID: 102661 RVA: 0x00102100 File Offset: 0x00101100
		public override void onfocus(IHTMLEventObj A_1)
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate(A_1);
				return;
			}
		}

		// Token: 0x06019106 RID: 102662 RVA: 0x00102130 File Offset: 0x00101130
		public override void onscroll(IHTMLEventObj A_1)
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate(A_1);
				return;
			}
		}

		// Token: 0x06019107 RID: 102663 RVA: 0x00102160 File Offset: 0x00101160
		public override void onpropertychange(IHTMLEventObj A_1)
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06019108 RID: 102664 RVA: 0x00102190 File Offset: 0x00101190
		public override void onlosecapture(IHTMLEventObj A_1)
		{
			if (this.m_onlosecaptureDelegate != null)
			{
				this.m_onlosecaptureDelegate(A_1);
				return;
			}
		}

		// Token: 0x06019109 RID: 102665 RVA: 0x001021C0 File Offset: 0x001011C0
		public override void ondatasetcomplete(IHTMLEventObj A_1)
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601910A RID: 102666 RVA: 0x001021F0 File Offset: 0x001011F0
		public override void ondataavailable(IHTMLEventObj A_1)
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601910B RID: 102667 RVA: 0x00102220 File Offset: 0x00101220
		public override void ondatasetchanged(IHTMLEventObj A_1)
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601910C RID: 102668 RVA: 0x00102250 File Offset: 0x00101250
		public override void onrowenter(IHTMLEventObj A_1)
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601910D RID: 102669 RVA: 0x00102280 File Offset: 0x00101280
		public override bool onrowexit(IHTMLEventObj A_1)
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate(A_1);
		}

		// Token: 0x0601910E RID: 102670 RVA: 0x001022B0 File Offset: 0x001012B0
		public override bool onerrorupdate(IHTMLEventObj A_1)
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate(A_1);
		}

		// Token: 0x0601910F RID: 102671 RVA: 0x001022E0 File Offset: 0x001012E0
		public override void onafterupdate(IHTMLEventObj A_1)
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate(A_1);
				return;
			}
		}

		// Token: 0x06019110 RID: 102672 RVA: 0x00102310 File Offset: 0x00101310
		public override bool onbeforeupdate(IHTMLEventObj A_1)
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate(A_1);
		}

		// Token: 0x06019111 RID: 102673 RVA: 0x00102340 File Offset: 0x00101340
		public override bool ondragstart(IHTMLEventObj A_1)
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate(A_1);
		}

		// Token: 0x06019112 RID: 102674 RVA: 0x00102370 File Offset: 0x00101370
		public override void onfilterchange(IHTMLEventObj A_1)
		{
			if (this.m_onfilterchangeDelegate != null)
			{
				this.m_onfilterchangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06019113 RID: 102675 RVA: 0x001023A0 File Offset: 0x001013A0
		public override bool onselectstart(IHTMLEventObj A_1)
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate(A_1);
		}

		// Token: 0x06019114 RID: 102676 RVA: 0x001023D0 File Offset: 0x001013D0
		public override void onmouseup(IHTMLEventObj A_1)
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate(A_1);
				return;
			}
		}

		// Token: 0x06019115 RID: 102677 RVA: 0x00102400 File Offset: 0x00101400
		public override void onmousedown(IHTMLEventObj A_1)
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate(A_1);
				return;
			}
		}

		// Token: 0x06019116 RID: 102678 RVA: 0x00102430 File Offset: 0x00101430
		public override void onmousemove(IHTMLEventObj A_1)
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06019117 RID: 102679 RVA: 0x00102460 File Offset: 0x00101460
		public override void onmouseover(IHTMLEventObj A_1)
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate(A_1);
				return;
			}
		}

		// Token: 0x06019118 RID: 102680 RVA: 0x00102490 File Offset: 0x00101490
		public override void onmouseout(IHTMLEventObj A_1)
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate(A_1);
				return;
			}
		}

		// Token: 0x06019119 RID: 102681 RVA: 0x001024C0 File Offset: 0x001014C0
		public override void onkeyup(IHTMLEventObj A_1)
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601911A RID: 102682 RVA: 0x001024F0 File Offset: 0x001014F0
		public override void onkeydown(IHTMLEventObj A_1)
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601911B RID: 102683 RVA: 0x00102520 File Offset: 0x00101520
		public override bool onkeypress(IHTMLEventObj A_1)
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate(A_1);
		}

		// Token: 0x0601911C RID: 102684 RVA: 0x00102550 File Offset: 0x00101550
		public override bool ondblclick(IHTMLEventObj A_1)
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate(A_1);
		}

		// Token: 0x0601911D RID: 102685 RVA: 0x00102580 File Offset: 0x00101580
		public override bool onclick(IHTMLEventObj A_1)
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate(A_1);
		}

		// Token: 0x0601911E RID: 102686 RVA: 0x001025B0 File Offset: 0x001015B0
		public override bool onhelp(IHTMLEventObj A_1)
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate(A_1);
		}

		// Token: 0x0601911F RID: 102687 RVA: 0x001025E0 File Offset: 0x001015E0
		internal HTMLFrameSiteEvents2_SinkHelper()
		{
			this.m_dwCookie = 0;
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

		// Token: 0x04000E75 RID: 3701
		public HTMLFrameSiteEvents2_onloadEventHandler m_onloadDelegate;

		// Token: 0x04000E76 RID: 3702
		public HTMLFrameSiteEvents2_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x04000E77 RID: 3703
		public HTMLFrameSiteEvents2_onresizeendEventHandler m_onresizeendDelegate;

		// Token: 0x04000E78 RID: 3704
		public HTMLFrameSiteEvents2_onresizestartEventHandler m_onresizestartDelegate;

		// Token: 0x04000E79 RID: 3705
		public HTMLFrameSiteEvents2_onmoveendEventHandler m_onmoveendDelegate;

		// Token: 0x04000E7A RID: 3706
		public HTMLFrameSiteEvents2_onmovestartEventHandler m_onmovestartDelegate;

		// Token: 0x04000E7B RID: 3707
		public HTMLFrameSiteEvents2_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x04000E7C RID: 3708
		public HTMLFrameSiteEvents2_onmoveEventHandler m_onmoveDelegate;

		// Token: 0x04000E7D RID: 3709
		public HTMLFrameSiteEvents2_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x04000E7E RID: 3710
		public HTMLFrameSiteEvents2_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x04000E7F RID: 3711
		public HTMLFrameSiteEvents2_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x04000E80 RID: 3712
		public HTMLFrameSiteEvents2_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x04000E81 RID: 3713
		public HTMLFrameSiteEvents2_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x04000E82 RID: 3714
		public HTMLFrameSiteEvents2_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x04000E83 RID: 3715
		public HTMLFrameSiteEvents2_onmouseleaveEventHandler m_onmouseleaveDelegate;

		// Token: 0x04000E84 RID: 3716
		public HTMLFrameSiteEvents2_onmouseenterEventHandler m_onmouseenterDelegate;

		// Token: 0x04000E85 RID: 3717
		public HTMLFrameSiteEvents2_onpageEventHandler m_onpageDelegate;

		// Token: 0x04000E86 RID: 3718
		public HTMLFrameSiteEvents2_onlayoutcompleteEventHandler m_onlayoutcompleteDelegate;

		// Token: 0x04000E87 RID: 3719
		public HTMLFrameSiteEvents2_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x04000E88 RID: 3720
		public HTMLFrameSiteEvents2_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x04000E89 RID: 3721
		public HTMLFrameSiteEvents2_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x04000E8A RID: 3722
		public HTMLFrameSiteEvents2_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x04000E8B RID: 3723
		public HTMLFrameSiteEvents2_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x04000E8C RID: 3724
		public HTMLFrameSiteEvents2_onpasteEventHandler m_onpasteDelegate;

		// Token: 0x04000E8D RID: 3725
		public HTMLFrameSiteEvents2_onbeforepasteEventHandler m_onbeforepasteDelegate;

		// Token: 0x04000E8E RID: 3726
		public HTMLFrameSiteEvents2_oncopyEventHandler m_oncopyDelegate;

		// Token: 0x04000E8F RID: 3727
		public HTMLFrameSiteEvents2_onbeforecopyEventHandler m_onbeforecopyDelegate;

		// Token: 0x04000E90 RID: 3728
		public HTMLFrameSiteEvents2_oncutEventHandler m_oncutDelegate;

		// Token: 0x04000E91 RID: 3729
		public HTMLFrameSiteEvents2_onbeforecutEventHandler m_onbeforecutDelegate;

		// Token: 0x04000E92 RID: 3730
		public HTMLFrameSiteEvents2_ondropEventHandler m_ondropDelegate;

		// Token: 0x04000E93 RID: 3731
		public HTMLFrameSiteEvents2_ondragleaveEventHandler m_ondragleaveDelegate;

		// Token: 0x04000E94 RID: 3732
		public HTMLFrameSiteEvents2_ondragoverEventHandler m_ondragoverDelegate;

		// Token: 0x04000E95 RID: 3733
		public HTMLFrameSiteEvents2_ondragenterEventHandler m_ondragenterDelegate;

		// Token: 0x04000E96 RID: 3734
		public HTMLFrameSiteEvents2_ondragendEventHandler m_ondragendDelegate;

		// Token: 0x04000E97 RID: 3735
		public HTMLFrameSiteEvents2_ondragEventHandler m_ondragDelegate;

		// Token: 0x04000E98 RID: 3736
		public HTMLFrameSiteEvents2_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x04000E99 RID: 3737
		public HTMLFrameSiteEvents2_onblurEventHandler m_onblurDelegate;

		// Token: 0x04000E9A RID: 3738
		public HTMLFrameSiteEvents2_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x04000E9B RID: 3739
		public HTMLFrameSiteEvents2_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x04000E9C RID: 3740
		public HTMLFrameSiteEvents2_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x04000E9D RID: 3741
		public HTMLFrameSiteEvents2_onlosecaptureEventHandler m_onlosecaptureDelegate;

		// Token: 0x04000E9E RID: 3742
		public HTMLFrameSiteEvents2_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x04000E9F RID: 3743
		public HTMLFrameSiteEvents2_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x04000EA0 RID: 3744
		public HTMLFrameSiteEvents2_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x04000EA1 RID: 3745
		public HTMLFrameSiteEvents2_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x04000EA2 RID: 3746
		public HTMLFrameSiteEvents2_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x04000EA3 RID: 3747
		public HTMLFrameSiteEvents2_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x04000EA4 RID: 3748
		public HTMLFrameSiteEvents2_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x04000EA5 RID: 3749
		public HTMLFrameSiteEvents2_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x04000EA6 RID: 3750
		public HTMLFrameSiteEvents2_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x04000EA7 RID: 3751
		public HTMLFrameSiteEvents2_onfilterchangeEventHandler m_onfilterchangeDelegate;

		// Token: 0x04000EA8 RID: 3752
		public HTMLFrameSiteEvents2_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x04000EA9 RID: 3753
		public HTMLFrameSiteEvents2_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x04000EAA RID: 3754
		public HTMLFrameSiteEvents2_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x04000EAB RID: 3755
		public HTMLFrameSiteEvents2_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x04000EAC RID: 3756
		public HTMLFrameSiteEvents2_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x04000EAD RID: 3757
		public HTMLFrameSiteEvents2_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x04000EAE RID: 3758
		public HTMLFrameSiteEvents2_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x04000EAF RID: 3759
		public HTMLFrameSiteEvents2_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x04000EB0 RID: 3760
		public HTMLFrameSiteEvents2_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x04000EB1 RID: 3761
		public HTMLFrameSiteEvents2_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x04000EB2 RID: 3762
		public HTMLFrameSiteEvents2_onclickEventHandler m_onclickDelegate;

		// Token: 0x04000EB3 RID: 3763
		public HTMLFrameSiteEvents2_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x04000EB4 RID: 3764
		public int m_dwCookie;
	}
}
