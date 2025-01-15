using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DCE RID: 3534
	[ClassInterface(0)]
	internal sealed class HTMLSelectElementEvents_SinkHelper : HTMLSelectElementEvents
	{
		// Token: 0x060179B2 RID: 96690 RVA: 0x0002E838 File Offset: 0x0002D838
		public override void onchange()
		{
			if (this.m_onchangeDelegate != null)
			{
				this.m_onchangeDelegate();
				return;
			}
		}

		// Token: 0x060179B3 RID: 96691 RVA: 0x0002E864 File Offset: 0x0002D864
		public override void onfocusout()
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate();
				return;
			}
		}

		// Token: 0x060179B4 RID: 96692 RVA: 0x0002E890 File Offset: 0x0002D890
		public override void onfocusin()
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate();
				return;
			}
		}

		// Token: 0x060179B5 RID: 96693 RVA: 0x0002E8BC File Offset: 0x0002D8BC
		public override void ondeactivate()
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate();
				return;
			}
		}

		// Token: 0x060179B6 RID: 96694 RVA: 0x0002E8E8 File Offset: 0x0002D8E8
		public override void onactivate()
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate();
				return;
			}
		}

		// Token: 0x060179B7 RID: 96695 RVA: 0x0002E914 File Offset: 0x0002D914
		public override bool onmousewheel()
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate();
		}

		// Token: 0x060179B8 RID: 96696 RVA: 0x0002E940 File Offset: 0x0002D940
		public override void onmouseleave()
		{
			if (this.m_onmouseleaveDelegate != null)
			{
				this.m_onmouseleaveDelegate();
				return;
			}
		}

		// Token: 0x060179B9 RID: 96697 RVA: 0x0002E96C File Offset: 0x0002D96C
		public override void onmouseenter()
		{
			if (this.m_onmouseenterDelegate != null)
			{
				this.m_onmouseenterDelegate();
				return;
			}
		}

		// Token: 0x060179BA RID: 96698 RVA: 0x0002E998 File Offset: 0x0002D998
		public override void onresizeend()
		{
			if (this.m_onresizeendDelegate != null)
			{
				this.m_onresizeendDelegate();
				return;
			}
		}

		// Token: 0x060179BB RID: 96699 RVA: 0x0002E9C4 File Offset: 0x0002D9C4
		public override bool onresizestart()
		{
			return this.m_onresizestartDelegate != null && this.m_onresizestartDelegate();
		}

		// Token: 0x060179BC RID: 96700 RVA: 0x0002E9F0 File Offset: 0x0002D9F0
		public override void onmoveend()
		{
			if (this.m_onmoveendDelegate != null)
			{
				this.m_onmoveendDelegate();
				return;
			}
		}

		// Token: 0x060179BD RID: 96701 RVA: 0x0002EA1C File Offset: 0x0002DA1C
		public override bool onmovestart()
		{
			return this.m_onmovestartDelegate != null && this.m_onmovestartDelegate();
		}

		// Token: 0x060179BE RID: 96702 RVA: 0x0002EA48 File Offset: 0x0002DA48
		public override bool oncontrolselect()
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate();
		}

		// Token: 0x060179BF RID: 96703 RVA: 0x0002EA74 File Offset: 0x0002DA74
		public override void onmove()
		{
			if (this.m_onmoveDelegate != null)
			{
				this.m_onmoveDelegate();
				return;
			}
		}

		// Token: 0x060179C0 RID: 96704 RVA: 0x0002EAA0 File Offset: 0x0002DAA0
		public override bool onbeforeactivate()
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate();
		}

		// Token: 0x060179C1 RID: 96705 RVA: 0x0002EACC File Offset: 0x0002DACC
		public override bool onbeforedeactivate()
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate();
		}

		// Token: 0x060179C2 RID: 96706 RVA: 0x0002EAF8 File Offset: 0x0002DAF8
		public override void onpage()
		{
			if (this.m_onpageDelegate != null)
			{
				this.m_onpageDelegate();
				return;
			}
		}

		// Token: 0x060179C3 RID: 96707 RVA: 0x0002EB24 File Offset: 0x0002DB24
		public override void onlayoutcomplete()
		{
			if (this.m_onlayoutcompleteDelegate != null)
			{
				this.m_onlayoutcompleteDelegate();
				return;
			}
		}

		// Token: 0x060179C4 RID: 96708 RVA: 0x0002EB50 File Offset: 0x0002DB50
		public override void onbeforeeditfocus()
		{
			if (this.m_onbeforeeditfocusDelegate != null)
			{
				this.m_onbeforeeditfocusDelegate();
				return;
			}
		}

		// Token: 0x060179C5 RID: 96709 RVA: 0x0002EB7C File Offset: 0x0002DB7C
		public override void onreadystatechange()
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate();
				return;
			}
		}

		// Token: 0x060179C6 RID: 96710 RVA: 0x0002EBA8 File Offset: 0x0002DBA8
		public override void oncellchange()
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate();
				return;
			}
		}

		// Token: 0x060179C7 RID: 96711 RVA: 0x0002EBD4 File Offset: 0x0002DBD4
		public override void onrowsinserted()
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate();
				return;
			}
		}

		// Token: 0x060179C8 RID: 96712 RVA: 0x0002EC00 File Offset: 0x0002DC00
		public override void onrowsdelete()
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate();
				return;
			}
		}

		// Token: 0x060179C9 RID: 96713 RVA: 0x0002EC2C File Offset: 0x0002DC2C
		public override bool oncontextmenu()
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate();
		}

		// Token: 0x060179CA RID: 96714 RVA: 0x0002EC58 File Offset: 0x0002DC58
		public override bool onpaste()
		{
			return this.m_onpasteDelegate != null && this.m_onpasteDelegate();
		}

		// Token: 0x060179CB RID: 96715 RVA: 0x0002EC84 File Offset: 0x0002DC84
		public override bool onbeforepaste()
		{
			return this.m_onbeforepasteDelegate != null && this.m_onbeforepasteDelegate();
		}

		// Token: 0x060179CC RID: 96716 RVA: 0x0002ECB0 File Offset: 0x0002DCB0
		public override bool oncopy()
		{
			return this.m_oncopyDelegate != null && this.m_oncopyDelegate();
		}

		// Token: 0x060179CD RID: 96717 RVA: 0x0002ECDC File Offset: 0x0002DCDC
		public override bool onbeforecopy()
		{
			return this.m_onbeforecopyDelegate != null && this.m_onbeforecopyDelegate();
		}

		// Token: 0x060179CE RID: 96718 RVA: 0x0002ED08 File Offset: 0x0002DD08
		public override bool oncut()
		{
			return this.m_oncutDelegate != null && this.m_oncutDelegate();
		}

		// Token: 0x060179CF RID: 96719 RVA: 0x0002ED34 File Offset: 0x0002DD34
		public override bool onbeforecut()
		{
			return this.m_onbeforecutDelegate != null && this.m_onbeforecutDelegate();
		}

		// Token: 0x060179D0 RID: 96720 RVA: 0x0002ED60 File Offset: 0x0002DD60
		public override bool ondrop()
		{
			return this.m_ondropDelegate != null && this.m_ondropDelegate();
		}

		// Token: 0x060179D1 RID: 96721 RVA: 0x0002ED8C File Offset: 0x0002DD8C
		public override void ondragleave()
		{
			if (this.m_ondragleaveDelegate != null)
			{
				this.m_ondragleaveDelegate();
				return;
			}
		}

		// Token: 0x060179D2 RID: 96722 RVA: 0x0002EDB8 File Offset: 0x0002DDB8
		public override bool ondragover()
		{
			return this.m_ondragoverDelegate != null && this.m_ondragoverDelegate();
		}

		// Token: 0x060179D3 RID: 96723 RVA: 0x0002EDE4 File Offset: 0x0002DDE4
		public override bool ondragenter()
		{
			return this.m_ondragenterDelegate != null && this.m_ondragenterDelegate();
		}

		// Token: 0x060179D4 RID: 96724 RVA: 0x0002EE10 File Offset: 0x0002DE10
		public override void ondragend()
		{
			if (this.m_ondragendDelegate != null)
			{
				this.m_ondragendDelegate();
				return;
			}
		}

		// Token: 0x060179D5 RID: 96725 RVA: 0x0002EE3C File Offset: 0x0002DE3C
		public override bool ondrag()
		{
			return this.m_ondragDelegate != null && this.m_ondragDelegate();
		}

		// Token: 0x060179D6 RID: 96726 RVA: 0x0002EE68 File Offset: 0x0002DE68
		public override void onresize()
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate();
				return;
			}
		}

		// Token: 0x060179D7 RID: 96727 RVA: 0x0002EE94 File Offset: 0x0002DE94
		public override void onblur()
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate();
				return;
			}
		}

		// Token: 0x060179D8 RID: 96728 RVA: 0x0002EEC0 File Offset: 0x0002DEC0
		public override void onfocus()
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate();
				return;
			}
		}

		// Token: 0x060179D9 RID: 96729 RVA: 0x0002EEEC File Offset: 0x0002DEEC
		public override void onscroll()
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate();
				return;
			}
		}

		// Token: 0x060179DA RID: 96730 RVA: 0x0002EF18 File Offset: 0x0002DF18
		public override void onpropertychange()
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate();
				return;
			}
		}

		// Token: 0x060179DB RID: 96731 RVA: 0x0002EF44 File Offset: 0x0002DF44
		public override void onlosecapture()
		{
			if (this.m_onlosecaptureDelegate != null)
			{
				this.m_onlosecaptureDelegate();
				return;
			}
		}

		// Token: 0x060179DC RID: 96732 RVA: 0x0002EF70 File Offset: 0x0002DF70
		public override void ondatasetcomplete()
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate();
				return;
			}
		}

		// Token: 0x060179DD RID: 96733 RVA: 0x0002EF9C File Offset: 0x0002DF9C
		public override void ondataavailable()
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate();
				return;
			}
		}

		// Token: 0x060179DE RID: 96734 RVA: 0x0002EFC8 File Offset: 0x0002DFC8
		public override void ondatasetchanged()
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate();
				return;
			}
		}

		// Token: 0x060179DF RID: 96735 RVA: 0x0002EFF4 File Offset: 0x0002DFF4
		public override void onrowenter()
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate();
				return;
			}
		}

		// Token: 0x060179E0 RID: 96736 RVA: 0x0002F020 File Offset: 0x0002E020
		public override bool onrowexit()
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate();
		}

		// Token: 0x060179E1 RID: 96737 RVA: 0x0002F04C File Offset: 0x0002E04C
		public override bool onerrorupdate()
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate();
		}

		// Token: 0x060179E2 RID: 96738 RVA: 0x0002F078 File Offset: 0x0002E078
		public override void onafterupdate()
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate();
				return;
			}
		}

		// Token: 0x060179E3 RID: 96739 RVA: 0x0002F0A4 File Offset: 0x0002E0A4
		public override bool onbeforeupdate()
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate();
		}

		// Token: 0x060179E4 RID: 96740 RVA: 0x0002F0D0 File Offset: 0x0002E0D0
		public override bool ondragstart()
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate();
		}

		// Token: 0x060179E5 RID: 96741 RVA: 0x0002F0FC File Offset: 0x0002E0FC
		public override void onfilterchange()
		{
			if (this.m_onfilterchangeDelegate != null)
			{
				this.m_onfilterchangeDelegate();
				return;
			}
		}

		// Token: 0x060179E6 RID: 96742 RVA: 0x0002F128 File Offset: 0x0002E128
		public override bool onselectstart()
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate();
		}

		// Token: 0x060179E7 RID: 96743 RVA: 0x0002F154 File Offset: 0x0002E154
		public override void onmouseup()
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate();
				return;
			}
		}

		// Token: 0x060179E8 RID: 96744 RVA: 0x0002F180 File Offset: 0x0002E180
		public override void onmousedown()
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate();
				return;
			}
		}

		// Token: 0x060179E9 RID: 96745 RVA: 0x0002F1AC File Offset: 0x0002E1AC
		public override void onmousemove()
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate();
				return;
			}
		}

		// Token: 0x060179EA RID: 96746 RVA: 0x0002F1D8 File Offset: 0x0002E1D8
		public override void onmouseover()
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate();
				return;
			}
		}

		// Token: 0x060179EB RID: 96747 RVA: 0x0002F204 File Offset: 0x0002E204
		public override void onmouseout()
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate();
				return;
			}
		}

		// Token: 0x060179EC RID: 96748 RVA: 0x0002F230 File Offset: 0x0002E230
		public override void onkeyup()
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate();
				return;
			}
		}

		// Token: 0x060179ED RID: 96749 RVA: 0x0002F25C File Offset: 0x0002E25C
		public override void onkeydown()
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate();
				return;
			}
		}

		// Token: 0x060179EE RID: 96750 RVA: 0x0002F288 File Offset: 0x0002E288
		public override bool onkeypress()
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate();
		}

		// Token: 0x060179EF RID: 96751 RVA: 0x0002F2B4 File Offset: 0x0002E2B4
		public override bool ondblclick()
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate();
		}

		// Token: 0x060179F0 RID: 96752 RVA: 0x0002F2E0 File Offset: 0x0002E2E0
		public override bool onclick()
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate();
		}

		// Token: 0x060179F1 RID: 96753 RVA: 0x0002F30C File Offset: 0x0002E30C
		public override bool onhelp()
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate();
		}

		// Token: 0x060179F2 RID: 96754 RVA: 0x0002F338 File Offset: 0x0002E338
		internal HTMLSelectElementEvents_SinkHelper()
		{
			this.m_dwCookie = 0;
			this.m_onchangeDelegate = null;
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

		// Token: 0x0400066E RID: 1646
		public HTMLSelectElementEvents_onchangeEventHandler m_onchangeDelegate;

		// Token: 0x0400066F RID: 1647
		public HTMLSelectElementEvents_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x04000670 RID: 1648
		public HTMLSelectElementEvents_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x04000671 RID: 1649
		public HTMLSelectElementEvents_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x04000672 RID: 1650
		public HTMLSelectElementEvents_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x04000673 RID: 1651
		public HTMLSelectElementEvents_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x04000674 RID: 1652
		public HTMLSelectElementEvents_onmouseleaveEventHandler m_onmouseleaveDelegate;

		// Token: 0x04000675 RID: 1653
		public HTMLSelectElementEvents_onmouseenterEventHandler m_onmouseenterDelegate;

		// Token: 0x04000676 RID: 1654
		public HTMLSelectElementEvents_onresizeendEventHandler m_onresizeendDelegate;

		// Token: 0x04000677 RID: 1655
		public HTMLSelectElementEvents_onresizestartEventHandler m_onresizestartDelegate;

		// Token: 0x04000678 RID: 1656
		public HTMLSelectElementEvents_onmoveendEventHandler m_onmoveendDelegate;

		// Token: 0x04000679 RID: 1657
		public HTMLSelectElementEvents_onmovestartEventHandler m_onmovestartDelegate;

		// Token: 0x0400067A RID: 1658
		public HTMLSelectElementEvents_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x0400067B RID: 1659
		public HTMLSelectElementEvents_onmoveEventHandler m_onmoveDelegate;

		// Token: 0x0400067C RID: 1660
		public HTMLSelectElementEvents_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x0400067D RID: 1661
		public HTMLSelectElementEvents_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x0400067E RID: 1662
		public HTMLSelectElementEvents_onpageEventHandler m_onpageDelegate;

		// Token: 0x0400067F RID: 1663
		public HTMLSelectElementEvents_onlayoutcompleteEventHandler m_onlayoutcompleteDelegate;

		// Token: 0x04000680 RID: 1664
		public HTMLSelectElementEvents_onbeforeeditfocusEventHandler m_onbeforeeditfocusDelegate;

		// Token: 0x04000681 RID: 1665
		public HTMLSelectElementEvents_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x04000682 RID: 1666
		public HTMLSelectElementEvents_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x04000683 RID: 1667
		public HTMLSelectElementEvents_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x04000684 RID: 1668
		public HTMLSelectElementEvents_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x04000685 RID: 1669
		public HTMLSelectElementEvents_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x04000686 RID: 1670
		public HTMLSelectElementEvents_onpasteEventHandler m_onpasteDelegate;

		// Token: 0x04000687 RID: 1671
		public HTMLSelectElementEvents_onbeforepasteEventHandler m_onbeforepasteDelegate;

		// Token: 0x04000688 RID: 1672
		public HTMLSelectElementEvents_oncopyEventHandler m_oncopyDelegate;

		// Token: 0x04000689 RID: 1673
		public HTMLSelectElementEvents_onbeforecopyEventHandler m_onbeforecopyDelegate;

		// Token: 0x0400068A RID: 1674
		public HTMLSelectElementEvents_oncutEventHandler m_oncutDelegate;

		// Token: 0x0400068B RID: 1675
		public HTMLSelectElementEvents_onbeforecutEventHandler m_onbeforecutDelegate;

		// Token: 0x0400068C RID: 1676
		public HTMLSelectElementEvents_ondropEventHandler m_ondropDelegate;

		// Token: 0x0400068D RID: 1677
		public HTMLSelectElementEvents_ondragleaveEventHandler m_ondragleaveDelegate;

		// Token: 0x0400068E RID: 1678
		public HTMLSelectElementEvents_ondragoverEventHandler m_ondragoverDelegate;

		// Token: 0x0400068F RID: 1679
		public HTMLSelectElementEvents_ondragenterEventHandler m_ondragenterDelegate;

		// Token: 0x04000690 RID: 1680
		public HTMLSelectElementEvents_ondragendEventHandler m_ondragendDelegate;

		// Token: 0x04000691 RID: 1681
		public HTMLSelectElementEvents_ondragEventHandler m_ondragDelegate;

		// Token: 0x04000692 RID: 1682
		public HTMLSelectElementEvents_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x04000693 RID: 1683
		public HTMLSelectElementEvents_onblurEventHandler m_onblurDelegate;

		// Token: 0x04000694 RID: 1684
		public HTMLSelectElementEvents_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x04000695 RID: 1685
		public HTMLSelectElementEvents_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x04000696 RID: 1686
		public HTMLSelectElementEvents_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x04000697 RID: 1687
		public HTMLSelectElementEvents_onlosecaptureEventHandler m_onlosecaptureDelegate;

		// Token: 0x04000698 RID: 1688
		public HTMLSelectElementEvents_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x04000699 RID: 1689
		public HTMLSelectElementEvents_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x0400069A RID: 1690
		public HTMLSelectElementEvents_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x0400069B RID: 1691
		public HTMLSelectElementEvents_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x0400069C RID: 1692
		public HTMLSelectElementEvents_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x0400069D RID: 1693
		public HTMLSelectElementEvents_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x0400069E RID: 1694
		public HTMLSelectElementEvents_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x0400069F RID: 1695
		public HTMLSelectElementEvents_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x040006A0 RID: 1696
		public HTMLSelectElementEvents_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x040006A1 RID: 1697
		public HTMLSelectElementEvents_onfilterchangeEventHandler m_onfilterchangeDelegate;

		// Token: 0x040006A2 RID: 1698
		public HTMLSelectElementEvents_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x040006A3 RID: 1699
		public HTMLSelectElementEvents_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x040006A4 RID: 1700
		public HTMLSelectElementEvents_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x040006A5 RID: 1701
		public HTMLSelectElementEvents_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x040006A6 RID: 1702
		public HTMLSelectElementEvents_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x040006A7 RID: 1703
		public HTMLSelectElementEvents_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x040006A8 RID: 1704
		public HTMLSelectElementEvents_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x040006A9 RID: 1705
		public HTMLSelectElementEvents_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x040006AA RID: 1706
		public HTMLSelectElementEvents_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x040006AB RID: 1707
		public HTMLSelectElementEvents_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x040006AC RID: 1708
		public HTMLSelectElementEvents_onclickEventHandler m_onclickDelegate;

		// Token: 0x040006AD RID: 1709
		public HTMLSelectElementEvents_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x040006AE RID: 1710
		public int m_dwCookie;
	}
}
