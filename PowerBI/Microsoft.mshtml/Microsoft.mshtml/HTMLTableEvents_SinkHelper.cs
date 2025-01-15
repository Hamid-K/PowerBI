using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000E12 RID: 3602
	[ClassInterface(0)]
	internal sealed class HTMLTableEvents_SinkHelper : HTMLTableEvents
	{
		// Token: 0x060191A2 RID: 102818 RVA: 0x0010890C File Offset: 0x0010790C
		public override void onfocusout()
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate();
				return;
			}
		}

		// Token: 0x060191A3 RID: 102819 RVA: 0x00108938 File Offset: 0x00107938
		public override void onfocusin()
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate();
				return;
			}
		}

		// Token: 0x060191A4 RID: 102820 RVA: 0x00108964 File Offset: 0x00107964
		public override void ondeactivate()
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate();
				return;
			}
		}

		// Token: 0x060191A5 RID: 102821 RVA: 0x00108990 File Offset: 0x00107990
		public override void onactivate()
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate();
				return;
			}
		}

		// Token: 0x060191A6 RID: 102822 RVA: 0x001089BC File Offset: 0x001079BC
		public override bool onmousewheel()
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate();
		}

		// Token: 0x060191A7 RID: 102823 RVA: 0x001089E8 File Offset: 0x001079E8
		public override void onmouseleave()
		{
			if (this.m_onmouseleaveDelegate != null)
			{
				this.m_onmouseleaveDelegate();
				return;
			}
		}

		// Token: 0x060191A8 RID: 102824 RVA: 0x00108A14 File Offset: 0x00107A14
		public override void onmouseenter()
		{
			if (this.m_onmouseenterDelegate != null)
			{
				this.m_onmouseenterDelegate();
				return;
			}
		}

		// Token: 0x060191A9 RID: 102825 RVA: 0x00108A40 File Offset: 0x00107A40
		public override void onresizeend()
		{
			if (this.m_onresizeendDelegate != null)
			{
				this.m_onresizeendDelegate();
				return;
			}
		}

		// Token: 0x060191AA RID: 102826 RVA: 0x00108A6C File Offset: 0x00107A6C
		public override bool onresizestart()
		{
			return this.m_onresizestartDelegate != null && this.m_onresizestartDelegate();
		}

		// Token: 0x060191AB RID: 102827 RVA: 0x00108A98 File Offset: 0x00107A98
		public override void onmoveend()
		{
			if (this.m_onmoveendDelegate != null)
			{
				this.m_onmoveendDelegate();
				return;
			}
		}

		// Token: 0x060191AC RID: 102828 RVA: 0x00108AC4 File Offset: 0x00107AC4
		public override bool onmovestart()
		{
			return this.m_onmovestartDelegate != null && this.m_onmovestartDelegate();
		}

		// Token: 0x060191AD RID: 102829 RVA: 0x00108AF0 File Offset: 0x00107AF0
		public override bool oncontrolselect()
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate();
		}

		// Token: 0x060191AE RID: 102830 RVA: 0x00108B1C File Offset: 0x00107B1C
		public override void onmove()
		{
			if (this.m_onmoveDelegate != null)
			{
				this.m_onmoveDelegate();
				return;
			}
		}

		// Token: 0x060191AF RID: 102831 RVA: 0x00108B48 File Offset: 0x00107B48
		public override bool onbeforeactivate()
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate();
		}

		// Token: 0x060191B0 RID: 102832 RVA: 0x00108B74 File Offset: 0x00107B74
		public override bool onbeforedeactivate()
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate();
		}

		// Token: 0x060191B1 RID: 102833 RVA: 0x00108BA0 File Offset: 0x00107BA0
		public override void onpage()
		{
			if (this.m_onpageDelegate != null)
			{
				this.m_onpageDelegate();
				return;
			}
		}

		// Token: 0x060191B2 RID: 102834 RVA: 0x00108BCC File Offset: 0x00107BCC
		public override void onlayoutcomplete()
		{
			if (this.m_onlayoutcompleteDelegate != null)
			{
				this.m_onlayoutcompleteDelegate();
				return;
			}
		}

		// Token: 0x060191B3 RID: 102835 RVA: 0x00108BF8 File Offset: 0x00107BF8
		public override void onbeforeeditfocus()
		{
			if (this.m_onbeforeeditfocusDelegate != null)
			{
				this.m_onbeforeeditfocusDelegate();
				return;
			}
		}

		// Token: 0x060191B4 RID: 102836 RVA: 0x00108C24 File Offset: 0x00107C24
		public override void onreadystatechange()
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate();
				return;
			}
		}

		// Token: 0x060191B5 RID: 102837 RVA: 0x00108C50 File Offset: 0x00107C50
		public override void oncellchange()
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate();
				return;
			}
		}

		// Token: 0x060191B6 RID: 102838 RVA: 0x00108C7C File Offset: 0x00107C7C
		public override void onrowsinserted()
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate();
				return;
			}
		}

		// Token: 0x060191B7 RID: 102839 RVA: 0x00108CA8 File Offset: 0x00107CA8
		public override void onrowsdelete()
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate();
				return;
			}
		}

		// Token: 0x060191B8 RID: 102840 RVA: 0x00108CD4 File Offset: 0x00107CD4
		public override bool oncontextmenu()
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate();
		}

		// Token: 0x060191B9 RID: 102841 RVA: 0x00108D00 File Offset: 0x00107D00
		public override bool onpaste()
		{
			return this.m_onpasteDelegate != null && this.m_onpasteDelegate();
		}

		// Token: 0x060191BA RID: 102842 RVA: 0x00108D2C File Offset: 0x00107D2C
		public override bool onbeforepaste()
		{
			return this.m_onbeforepasteDelegate != null && this.m_onbeforepasteDelegate();
		}

		// Token: 0x060191BB RID: 102843 RVA: 0x00108D58 File Offset: 0x00107D58
		public override bool oncopy()
		{
			return this.m_oncopyDelegate != null && this.m_oncopyDelegate();
		}

		// Token: 0x060191BC RID: 102844 RVA: 0x00108D84 File Offset: 0x00107D84
		public override bool onbeforecopy()
		{
			return this.m_onbeforecopyDelegate != null && this.m_onbeforecopyDelegate();
		}

		// Token: 0x060191BD RID: 102845 RVA: 0x00108DB0 File Offset: 0x00107DB0
		public override bool oncut()
		{
			return this.m_oncutDelegate != null && this.m_oncutDelegate();
		}

		// Token: 0x060191BE RID: 102846 RVA: 0x00108DDC File Offset: 0x00107DDC
		public override bool onbeforecut()
		{
			return this.m_onbeforecutDelegate != null && this.m_onbeforecutDelegate();
		}

		// Token: 0x060191BF RID: 102847 RVA: 0x00108E08 File Offset: 0x00107E08
		public override bool ondrop()
		{
			return this.m_ondropDelegate != null && this.m_ondropDelegate();
		}

		// Token: 0x060191C0 RID: 102848 RVA: 0x00108E34 File Offset: 0x00107E34
		public override void ondragleave()
		{
			if (this.m_ondragleaveDelegate != null)
			{
				this.m_ondragleaveDelegate();
				return;
			}
		}

		// Token: 0x060191C1 RID: 102849 RVA: 0x00108E60 File Offset: 0x00107E60
		public override bool ondragover()
		{
			return this.m_ondragoverDelegate != null && this.m_ondragoverDelegate();
		}

		// Token: 0x060191C2 RID: 102850 RVA: 0x00108E8C File Offset: 0x00107E8C
		public override bool ondragenter()
		{
			return this.m_ondragenterDelegate != null && this.m_ondragenterDelegate();
		}

		// Token: 0x060191C3 RID: 102851 RVA: 0x00108EB8 File Offset: 0x00107EB8
		public override void ondragend()
		{
			if (this.m_ondragendDelegate != null)
			{
				this.m_ondragendDelegate();
				return;
			}
		}

		// Token: 0x060191C4 RID: 102852 RVA: 0x00108EE4 File Offset: 0x00107EE4
		public override bool ondrag()
		{
			return this.m_ondragDelegate != null && this.m_ondragDelegate();
		}

		// Token: 0x060191C5 RID: 102853 RVA: 0x00108F10 File Offset: 0x00107F10
		public override void onresize()
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate();
				return;
			}
		}

		// Token: 0x060191C6 RID: 102854 RVA: 0x00108F3C File Offset: 0x00107F3C
		public override void onblur()
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate();
				return;
			}
		}

		// Token: 0x060191C7 RID: 102855 RVA: 0x00108F68 File Offset: 0x00107F68
		public override void onfocus()
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate();
				return;
			}
		}

		// Token: 0x060191C8 RID: 102856 RVA: 0x00108F94 File Offset: 0x00107F94
		public override void onscroll()
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate();
				return;
			}
		}

		// Token: 0x060191C9 RID: 102857 RVA: 0x00108FC0 File Offset: 0x00107FC0
		public override void onpropertychange()
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate();
				return;
			}
		}

		// Token: 0x060191CA RID: 102858 RVA: 0x00108FEC File Offset: 0x00107FEC
		public override void onlosecapture()
		{
			if (this.m_onlosecaptureDelegate != null)
			{
				this.m_onlosecaptureDelegate();
				return;
			}
		}

		// Token: 0x060191CB RID: 102859 RVA: 0x00109018 File Offset: 0x00108018
		public override void ondatasetcomplete()
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate();
				return;
			}
		}

		// Token: 0x060191CC RID: 102860 RVA: 0x00109044 File Offset: 0x00108044
		public override void ondataavailable()
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate();
				return;
			}
		}

		// Token: 0x060191CD RID: 102861 RVA: 0x00109070 File Offset: 0x00108070
		public override void ondatasetchanged()
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate();
				return;
			}
		}

		// Token: 0x060191CE RID: 102862 RVA: 0x0010909C File Offset: 0x0010809C
		public override void onrowenter()
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate();
				return;
			}
		}

		// Token: 0x060191CF RID: 102863 RVA: 0x001090C8 File Offset: 0x001080C8
		public override bool onrowexit()
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate();
		}

		// Token: 0x060191D0 RID: 102864 RVA: 0x001090F4 File Offset: 0x001080F4
		public override bool onerrorupdate()
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate();
		}

		// Token: 0x060191D1 RID: 102865 RVA: 0x00109120 File Offset: 0x00108120
		public override void onafterupdate()
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate();
				return;
			}
		}

		// Token: 0x060191D2 RID: 102866 RVA: 0x0010914C File Offset: 0x0010814C
		public override bool onbeforeupdate()
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate();
		}

		// Token: 0x060191D3 RID: 102867 RVA: 0x00109178 File Offset: 0x00108178
		public override bool ondragstart()
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate();
		}

		// Token: 0x060191D4 RID: 102868 RVA: 0x001091A4 File Offset: 0x001081A4
		public override void onfilterchange()
		{
			if (this.m_onfilterchangeDelegate != null)
			{
				this.m_onfilterchangeDelegate();
				return;
			}
		}

		// Token: 0x060191D5 RID: 102869 RVA: 0x001091D0 File Offset: 0x001081D0
		public override bool onselectstart()
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate();
		}

		// Token: 0x060191D6 RID: 102870 RVA: 0x001091FC File Offset: 0x001081FC
		public override void onmouseup()
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate();
				return;
			}
		}

		// Token: 0x060191D7 RID: 102871 RVA: 0x00109228 File Offset: 0x00108228
		public override void onmousedown()
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate();
				return;
			}
		}

		// Token: 0x060191D8 RID: 102872 RVA: 0x00109254 File Offset: 0x00108254
		public override void onmousemove()
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate();
				return;
			}
		}

		// Token: 0x060191D9 RID: 102873 RVA: 0x00109280 File Offset: 0x00108280
		public override void onmouseover()
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate();
				return;
			}
		}

		// Token: 0x060191DA RID: 102874 RVA: 0x001092AC File Offset: 0x001082AC
		public override void onmouseout()
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate();
				return;
			}
		}

		// Token: 0x060191DB RID: 102875 RVA: 0x001092D8 File Offset: 0x001082D8
		public override void onkeyup()
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate();
				return;
			}
		}

		// Token: 0x060191DC RID: 102876 RVA: 0x00109304 File Offset: 0x00108304
		public override void onkeydown()
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate();
				return;
			}
		}

		// Token: 0x060191DD RID: 102877 RVA: 0x00109330 File Offset: 0x00108330
		public override bool onkeypress()
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate();
		}

		// Token: 0x060191DE RID: 102878 RVA: 0x0010935C File Offset: 0x0010835C
		public override bool ondblclick()
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate();
		}

		// Token: 0x060191DF RID: 102879 RVA: 0x00109388 File Offset: 0x00108388
		public override bool onclick()
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate();
		}

		// Token: 0x060191E0 RID: 102880 RVA: 0x001093B4 File Offset: 0x001083B4
		public override bool onhelp()
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate();
		}

		// Token: 0x060191E1 RID: 102881 RVA: 0x001093E0 File Offset: 0x001083E0
		internal HTMLTableEvents_SinkHelper()
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

		// Token: 0x04000EB8 RID: 3768
		public HTMLTableEvents_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x04000EB9 RID: 3769
		public HTMLTableEvents_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x04000EBA RID: 3770
		public HTMLTableEvents_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x04000EBB RID: 3771
		public HTMLTableEvents_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x04000EBC RID: 3772
		public HTMLTableEvents_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x04000EBD RID: 3773
		public HTMLTableEvents_onmouseleaveEventHandler m_onmouseleaveDelegate;

		// Token: 0x04000EBE RID: 3774
		public HTMLTableEvents_onmouseenterEventHandler m_onmouseenterDelegate;

		// Token: 0x04000EBF RID: 3775
		public HTMLTableEvents_onresizeendEventHandler m_onresizeendDelegate;

		// Token: 0x04000EC0 RID: 3776
		public HTMLTableEvents_onresizestartEventHandler m_onresizestartDelegate;

		// Token: 0x04000EC1 RID: 3777
		public HTMLTableEvents_onmoveendEventHandler m_onmoveendDelegate;

		// Token: 0x04000EC2 RID: 3778
		public HTMLTableEvents_onmovestartEventHandler m_onmovestartDelegate;

		// Token: 0x04000EC3 RID: 3779
		public HTMLTableEvents_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x04000EC4 RID: 3780
		public HTMLTableEvents_onmoveEventHandler m_onmoveDelegate;

		// Token: 0x04000EC5 RID: 3781
		public HTMLTableEvents_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x04000EC6 RID: 3782
		public HTMLTableEvents_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x04000EC7 RID: 3783
		public HTMLTableEvents_onpageEventHandler m_onpageDelegate;

		// Token: 0x04000EC8 RID: 3784
		public HTMLTableEvents_onlayoutcompleteEventHandler m_onlayoutcompleteDelegate;

		// Token: 0x04000EC9 RID: 3785
		public HTMLTableEvents_onbeforeeditfocusEventHandler m_onbeforeeditfocusDelegate;

		// Token: 0x04000ECA RID: 3786
		public HTMLTableEvents_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x04000ECB RID: 3787
		public HTMLTableEvents_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x04000ECC RID: 3788
		public HTMLTableEvents_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x04000ECD RID: 3789
		public HTMLTableEvents_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x04000ECE RID: 3790
		public HTMLTableEvents_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x04000ECF RID: 3791
		public HTMLTableEvents_onpasteEventHandler m_onpasteDelegate;

		// Token: 0x04000ED0 RID: 3792
		public HTMLTableEvents_onbeforepasteEventHandler m_onbeforepasteDelegate;

		// Token: 0x04000ED1 RID: 3793
		public HTMLTableEvents_oncopyEventHandler m_oncopyDelegate;

		// Token: 0x04000ED2 RID: 3794
		public HTMLTableEvents_onbeforecopyEventHandler m_onbeforecopyDelegate;

		// Token: 0x04000ED3 RID: 3795
		public HTMLTableEvents_oncutEventHandler m_oncutDelegate;

		// Token: 0x04000ED4 RID: 3796
		public HTMLTableEvents_onbeforecutEventHandler m_onbeforecutDelegate;

		// Token: 0x04000ED5 RID: 3797
		public HTMLTableEvents_ondropEventHandler m_ondropDelegate;

		// Token: 0x04000ED6 RID: 3798
		public HTMLTableEvents_ondragleaveEventHandler m_ondragleaveDelegate;

		// Token: 0x04000ED7 RID: 3799
		public HTMLTableEvents_ondragoverEventHandler m_ondragoverDelegate;

		// Token: 0x04000ED8 RID: 3800
		public HTMLTableEvents_ondragenterEventHandler m_ondragenterDelegate;

		// Token: 0x04000ED9 RID: 3801
		public HTMLTableEvents_ondragendEventHandler m_ondragendDelegate;

		// Token: 0x04000EDA RID: 3802
		public HTMLTableEvents_ondragEventHandler m_ondragDelegate;

		// Token: 0x04000EDB RID: 3803
		public HTMLTableEvents_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x04000EDC RID: 3804
		public HTMLTableEvents_onblurEventHandler m_onblurDelegate;

		// Token: 0x04000EDD RID: 3805
		public HTMLTableEvents_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x04000EDE RID: 3806
		public HTMLTableEvents_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x04000EDF RID: 3807
		public HTMLTableEvents_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x04000EE0 RID: 3808
		public HTMLTableEvents_onlosecaptureEventHandler m_onlosecaptureDelegate;

		// Token: 0x04000EE1 RID: 3809
		public HTMLTableEvents_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x04000EE2 RID: 3810
		public HTMLTableEvents_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x04000EE3 RID: 3811
		public HTMLTableEvents_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x04000EE4 RID: 3812
		public HTMLTableEvents_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x04000EE5 RID: 3813
		public HTMLTableEvents_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x04000EE6 RID: 3814
		public HTMLTableEvents_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x04000EE7 RID: 3815
		public HTMLTableEvents_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x04000EE8 RID: 3816
		public HTMLTableEvents_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x04000EE9 RID: 3817
		public HTMLTableEvents_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x04000EEA RID: 3818
		public HTMLTableEvents_onfilterchangeEventHandler m_onfilterchangeDelegate;

		// Token: 0x04000EEB RID: 3819
		public HTMLTableEvents_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x04000EEC RID: 3820
		public HTMLTableEvents_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x04000EED RID: 3821
		public HTMLTableEvents_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x04000EEE RID: 3822
		public HTMLTableEvents_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x04000EEF RID: 3823
		public HTMLTableEvents_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x04000EF0 RID: 3824
		public HTMLTableEvents_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x04000EF1 RID: 3825
		public HTMLTableEvents_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x04000EF2 RID: 3826
		public HTMLTableEvents_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x04000EF3 RID: 3827
		public HTMLTableEvents_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x04000EF4 RID: 3828
		public HTMLTableEvents_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x04000EF5 RID: 3829
		public HTMLTableEvents_onclickEventHandler m_onclickDelegate;

		// Token: 0x04000EF6 RID: 3830
		public HTMLTableEvents_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x04000EF7 RID: 3831
		public int m_dwCookie;
	}
}
