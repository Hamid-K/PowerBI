using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DF2 RID: 3570
	[ClassInterface(0)]
	internal sealed class HTMLScriptEvents_SinkHelper : HTMLScriptEvents
	{
		// Token: 0x0601862D RID: 99885 RVA: 0x000A03D8 File Offset: 0x0009F3D8
		public override void onerror()
		{
			if (this.m_onerrorDelegate != null)
			{
				this.m_onerrorDelegate();
				return;
			}
		}

		// Token: 0x0601862E RID: 99886 RVA: 0x000A0404 File Offset: 0x0009F404
		public override void onfocusout()
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate();
				return;
			}
		}

		// Token: 0x0601862F RID: 99887 RVA: 0x000A0430 File Offset: 0x0009F430
		public override void onfocusin()
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate();
				return;
			}
		}

		// Token: 0x06018630 RID: 99888 RVA: 0x000A045C File Offset: 0x0009F45C
		public override void ondeactivate()
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate();
				return;
			}
		}

		// Token: 0x06018631 RID: 99889 RVA: 0x000A0488 File Offset: 0x0009F488
		public override void onactivate()
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate();
				return;
			}
		}

		// Token: 0x06018632 RID: 99890 RVA: 0x000A04B4 File Offset: 0x0009F4B4
		public override bool onmousewheel()
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate();
		}

		// Token: 0x06018633 RID: 99891 RVA: 0x000A04E0 File Offset: 0x0009F4E0
		public override void onmouseleave()
		{
			if (this.m_onmouseleaveDelegate != null)
			{
				this.m_onmouseleaveDelegate();
				return;
			}
		}

		// Token: 0x06018634 RID: 99892 RVA: 0x000A050C File Offset: 0x0009F50C
		public override void onmouseenter()
		{
			if (this.m_onmouseenterDelegate != null)
			{
				this.m_onmouseenterDelegate();
				return;
			}
		}

		// Token: 0x06018635 RID: 99893 RVA: 0x000A0538 File Offset: 0x0009F538
		public override void onresizeend()
		{
			if (this.m_onresizeendDelegate != null)
			{
				this.m_onresizeendDelegate();
				return;
			}
		}

		// Token: 0x06018636 RID: 99894 RVA: 0x000A0564 File Offset: 0x0009F564
		public override bool onresizestart()
		{
			return this.m_onresizestartDelegate != null && this.m_onresizestartDelegate();
		}

		// Token: 0x06018637 RID: 99895 RVA: 0x000A0590 File Offset: 0x0009F590
		public override void onmoveend()
		{
			if (this.m_onmoveendDelegate != null)
			{
				this.m_onmoveendDelegate();
				return;
			}
		}

		// Token: 0x06018638 RID: 99896 RVA: 0x000A05BC File Offset: 0x0009F5BC
		public override bool onmovestart()
		{
			return this.m_onmovestartDelegate != null && this.m_onmovestartDelegate();
		}

		// Token: 0x06018639 RID: 99897 RVA: 0x000A05E8 File Offset: 0x0009F5E8
		public override bool oncontrolselect()
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate();
		}

		// Token: 0x0601863A RID: 99898 RVA: 0x000A0614 File Offset: 0x0009F614
		public override void onmove()
		{
			if (this.m_onmoveDelegate != null)
			{
				this.m_onmoveDelegate();
				return;
			}
		}

		// Token: 0x0601863B RID: 99899 RVA: 0x000A0640 File Offset: 0x0009F640
		public override bool onbeforeactivate()
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate();
		}

		// Token: 0x0601863C RID: 99900 RVA: 0x000A066C File Offset: 0x0009F66C
		public override bool onbeforedeactivate()
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate();
		}

		// Token: 0x0601863D RID: 99901 RVA: 0x000A0698 File Offset: 0x0009F698
		public override void onpage()
		{
			if (this.m_onpageDelegate != null)
			{
				this.m_onpageDelegate();
				return;
			}
		}

		// Token: 0x0601863E RID: 99902 RVA: 0x000A06C4 File Offset: 0x0009F6C4
		public override void onlayoutcomplete()
		{
			if (this.m_onlayoutcompleteDelegate != null)
			{
				this.m_onlayoutcompleteDelegate();
				return;
			}
		}

		// Token: 0x0601863F RID: 99903 RVA: 0x000A06F0 File Offset: 0x0009F6F0
		public override void onbeforeeditfocus()
		{
			if (this.m_onbeforeeditfocusDelegate != null)
			{
				this.m_onbeforeeditfocusDelegate();
				return;
			}
		}

		// Token: 0x06018640 RID: 99904 RVA: 0x000A071C File Offset: 0x0009F71C
		public override void onreadystatechange()
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate();
				return;
			}
		}

		// Token: 0x06018641 RID: 99905 RVA: 0x000A0748 File Offset: 0x0009F748
		public override void oncellchange()
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate();
				return;
			}
		}

		// Token: 0x06018642 RID: 99906 RVA: 0x000A0774 File Offset: 0x0009F774
		public override void onrowsinserted()
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate();
				return;
			}
		}

		// Token: 0x06018643 RID: 99907 RVA: 0x000A07A0 File Offset: 0x0009F7A0
		public override void onrowsdelete()
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate();
				return;
			}
		}

		// Token: 0x06018644 RID: 99908 RVA: 0x000A07CC File Offset: 0x0009F7CC
		public override bool oncontextmenu()
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate();
		}

		// Token: 0x06018645 RID: 99909 RVA: 0x000A07F8 File Offset: 0x0009F7F8
		public override bool onpaste()
		{
			return this.m_onpasteDelegate != null && this.m_onpasteDelegate();
		}

		// Token: 0x06018646 RID: 99910 RVA: 0x000A0824 File Offset: 0x0009F824
		public override bool onbeforepaste()
		{
			return this.m_onbeforepasteDelegate != null && this.m_onbeforepasteDelegate();
		}

		// Token: 0x06018647 RID: 99911 RVA: 0x000A0850 File Offset: 0x0009F850
		public override bool oncopy()
		{
			return this.m_oncopyDelegate != null && this.m_oncopyDelegate();
		}

		// Token: 0x06018648 RID: 99912 RVA: 0x000A087C File Offset: 0x0009F87C
		public override bool onbeforecopy()
		{
			return this.m_onbeforecopyDelegate != null && this.m_onbeforecopyDelegate();
		}

		// Token: 0x06018649 RID: 99913 RVA: 0x000A08A8 File Offset: 0x0009F8A8
		public override bool oncut()
		{
			return this.m_oncutDelegate != null && this.m_oncutDelegate();
		}

		// Token: 0x0601864A RID: 99914 RVA: 0x000A08D4 File Offset: 0x0009F8D4
		public override bool onbeforecut()
		{
			return this.m_onbeforecutDelegate != null && this.m_onbeforecutDelegate();
		}

		// Token: 0x0601864B RID: 99915 RVA: 0x000A0900 File Offset: 0x0009F900
		public override bool ondrop()
		{
			return this.m_ondropDelegate != null && this.m_ondropDelegate();
		}

		// Token: 0x0601864C RID: 99916 RVA: 0x000A092C File Offset: 0x0009F92C
		public override void ondragleave()
		{
			if (this.m_ondragleaveDelegate != null)
			{
				this.m_ondragleaveDelegate();
				return;
			}
		}

		// Token: 0x0601864D RID: 99917 RVA: 0x000A0958 File Offset: 0x0009F958
		public override bool ondragover()
		{
			return this.m_ondragoverDelegate != null && this.m_ondragoverDelegate();
		}

		// Token: 0x0601864E RID: 99918 RVA: 0x000A0984 File Offset: 0x0009F984
		public override bool ondragenter()
		{
			return this.m_ondragenterDelegate != null && this.m_ondragenterDelegate();
		}

		// Token: 0x0601864F RID: 99919 RVA: 0x000A09B0 File Offset: 0x0009F9B0
		public override void ondragend()
		{
			if (this.m_ondragendDelegate != null)
			{
				this.m_ondragendDelegate();
				return;
			}
		}

		// Token: 0x06018650 RID: 99920 RVA: 0x000A09DC File Offset: 0x0009F9DC
		public override bool ondrag()
		{
			return this.m_ondragDelegate != null && this.m_ondragDelegate();
		}

		// Token: 0x06018651 RID: 99921 RVA: 0x000A0A08 File Offset: 0x0009FA08
		public override void onresize()
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate();
				return;
			}
		}

		// Token: 0x06018652 RID: 99922 RVA: 0x000A0A34 File Offset: 0x0009FA34
		public override void onblur()
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate();
				return;
			}
		}

		// Token: 0x06018653 RID: 99923 RVA: 0x000A0A60 File Offset: 0x0009FA60
		public override void onfocus()
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate();
				return;
			}
		}

		// Token: 0x06018654 RID: 99924 RVA: 0x000A0A8C File Offset: 0x0009FA8C
		public override void onscroll()
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate();
				return;
			}
		}

		// Token: 0x06018655 RID: 99925 RVA: 0x000A0AB8 File Offset: 0x0009FAB8
		public override void onpropertychange()
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate();
				return;
			}
		}

		// Token: 0x06018656 RID: 99926 RVA: 0x000A0AE4 File Offset: 0x0009FAE4
		public override void onlosecapture()
		{
			if (this.m_onlosecaptureDelegate != null)
			{
				this.m_onlosecaptureDelegate();
				return;
			}
		}

		// Token: 0x06018657 RID: 99927 RVA: 0x000A0B10 File Offset: 0x0009FB10
		public override void ondatasetcomplete()
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate();
				return;
			}
		}

		// Token: 0x06018658 RID: 99928 RVA: 0x000A0B3C File Offset: 0x0009FB3C
		public override void ondataavailable()
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate();
				return;
			}
		}

		// Token: 0x06018659 RID: 99929 RVA: 0x000A0B68 File Offset: 0x0009FB68
		public override void ondatasetchanged()
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate();
				return;
			}
		}

		// Token: 0x0601865A RID: 99930 RVA: 0x000A0B94 File Offset: 0x0009FB94
		public override void onrowenter()
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate();
				return;
			}
		}

		// Token: 0x0601865B RID: 99931 RVA: 0x000A0BC0 File Offset: 0x0009FBC0
		public override bool onrowexit()
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate();
		}

		// Token: 0x0601865C RID: 99932 RVA: 0x000A0BEC File Offset: 0x0009FBEC
		public override bool onerrorupdate()
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate();
		}

		// Token: 0x0601865D RID: 99933 RVA: 0x000A0C18 File Offset: 0x0009FC18
		public override void onafterupdate()
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate();
				return;
			}
		}

		// Token: 0x0601865E RID: 99934 RVA: 0x000A0C44 File Offset: 0x0009FC44
		public override bool onbeforeupdate()
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate();
		}

		// Token: 0x0601865F RID: 99935 RVA: 0x000A0C70 File Offset: 0x0009FC70
		public override bool ondragstart()
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate();
		}

		// Token: 0x06018660 RID: 99936 RVA: 0x000A0C9C File Offset: 0x0009FC9C
		public override void onfilterchange()
		{
			if (this.m_onfilterchangeDelegate != null)
			{
				this.m_onfilterchangeDelegate();
				return;
			}
		}

		// Token: 0x06018661 RID: 99937 RVA: 0x000A0CC8 File Offset: 0x0009FCC8
		public override bool onselectstart()
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate();
		}

		// Token: 0x06018662 RID: 99938 RVA: 0x000A0CF4 File Offset: 0x0009FCF4
		public override void onmouseup()
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate();
				return;
			}
		}

		// Token: 0x06018663 RID: 99939 RVA: 0x000A0D20 File Offset: 0x0009FD20
		public override void onmousedown()
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate();
				return;
			}
		}

		// Token: 0x06018664 RID: 99940 RVA: 0x000A0D4C File Offset: 0x0009FD4C
		public override void onmousemove()
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate();
				return;
			}
		}

		// Token: 0x06018665 RID: 99941 RVA: 0x000A0D78 File Offset: 0x0009FD78
		public override void onmouseover()
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate();
				return;
			}
		}

		// Token: 0x06018666 RID: 99942 RVA: 0x000A0DA4 File Offset: 0x0009FDA4
		public override void onmouseout()
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate();
				return;
			}
		}

		// Token: 0x06018667 RID: 99943 RVA: 0x000A0DD0 File Offset: 0x0009FDD0
		public override void onkeyup()
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate();
				return;
			}
		}

		// Token: 0x06018668 RID: 99944 RVA: 0x000A0DFC File Offset: 0x0009FDFC
		public override void onkeydown()
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate();
				return;
			}
		}

		// Token: 0x06018669 RID: 99945 RVA: 0x000A0E28 File Offset: 0x0009FE28
		public override bool onkeypress()
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate();
		}

		// Token: 0x0601866A RID: 99946 RVA: 0x000A0E54 File Offset: 0x0009FE54
		public override bool ondblclick()
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate();
		}

		// Token: 0x0601866B RID: 99947 RVA: 0x000A0E80 File Offset: 0x0009FE80
		public override bool onclick()
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate();
		}

		// Token: 0x0601866C RID: 99948 RVA: 0x000A0EAC File Offset: 0x0009FEAC
		public override bool onhelp()
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate();
		}

		// Token: 0x0601866D RID: 99949 RVA: 0x000A0ED8 File Offset: 0x0009FED8
		internal HTMLScriptEvents_SinkHelper()
		{
			this.m_dwCookie = 0;
			this.m_onerrorDelegate = null;
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

		// Token: 0x04000AC1 RID: 2753
		public HTMLScriptEvents_onerrorEventHandler m_onerrorDelegate;

		// Token: 0x04000AC2 RID: 2754
		public HTMLScriptEvents_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x04000AC3 RID: 2755
		public HTMLScriptEvents_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x04000AC4 RID: 2756
		public HTMLScriptEvents_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x04000AC5 RID: 2757
		public HTMLScriptEvents_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x04000AC6 RID: 2758
		public HTMLScriptEvents_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x04000AC7 RID: 2759
		public HTMLScriptEvents_onmouseleaveEventHandler m_onmouseleaveDelegate;

		// Token: 0x04000AC8 RID: 2760
		public HTMLScriptEvents_onmouseenterEventHandler m_onmouseenterDelegate;

		// Token: 0x04000AC9 RID: 2761
		public HTMLScriptEvents_onresizeendEventHandler m_onresizeendDelegate;

		// Token: 0x04000ACA RID: 2762
		public HTMLScriptEvents_onresizestartEventHandler m_onresizestartDelegate;

		// Token: 0x04000ACB RID: 2763
		public HTMLScriptEvents_onmoveendEventHandler m_onmoveendDelegate;

		// Token: 0x04000ACC RID: 2764
		public HTMLScriptEvents_onmovestartEventHandler m_onmovestartDelegate;

		// Token: 0x04000ACD RID: 2765
		public HTMLScriptEvents_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x04000ACE RID: 2766
		public HTMLScriptEvents_onmoveEventHandler m_onmoveDelegate;

		// Token: 0x04000ACF RID: 2767
		public HTMLScriptEvents_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x04000AD0 RID: 2768
		public HTMLScriptEvents_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x04000AD1 RID: 2769
		public HTMLScriptEvents_onpageEventHandler m_onpageDelegate;

		// Token: 0x04000AD2 RID: 2770
		public HTMLScriptEvents_onlayoutcompleteEventHandler m_onlayoutcompleteDelegate;

		// Token: 0x04000AD3 RID: 2771
		public HTMLScriptEvents_onbeforeeditfocusEventHandler m_onbeforeeditfocusDelegate;

		// Token: 0x04000AD4 RID: 2772
		public HTMLScriptEvents_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x04000AD5 RID: 2773
		public HTMLScriptEvents_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x04000AD6 RID: 2774
		public HTMLScriptEvents_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x04000AD7 RID: 2775
		public HTMLScriptEvents_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x04000AD8 RID: 2776
		public HTMLScriptEvents_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x04000AD9 RID: 2777
		public HTMLScriptEvents_onpasteEventHandler m_onpasteDelegate;

		// Token: 0x04000ADA RID: 2778
		public HTMLScriptEvents_onbeforepasteEventHandler m_onbeforepasteDelegate;

		// Token: 0x04000ADB RID: 2779
		public HTMLScriptEvents_oncopyEventHandler m_oncopyDelegate;

		// Token: 0x04000ADC RID: 2780
		public HTMLScriptEvents_onbeforecopyEventHandler m_onbeforecopyDelegate;

		// Token: 0x04000ADD RID: 2781
		public HTMLScriptEvents_oncutEventHandler m_oncutDelegate;

		// Token: 0x04000ADE RID: 2782
		public HTMLScriptEvents_onbeforecutEventHandler m_onbeforecutDelegate;

		// Token: 0x04000ADF RID: 2783
		public HTMLScriptEvents_ondropEventHandler m_ondropDelegate;

		// Token: 0x04000AE0 RID: 2784
		public HTMLScriptEvents_ondragleaveEventHandler m_ondragleaveDelegate;

		// Token: 0x04000AE1 RID: 2785
		public HTMLScriptEvents_ondragoverEventHandler m_ondragoverDelegate;

		// Token: 0x04000AE2 RID: 2786
		public HTMLScriptEvents_ondragenterEventHandler m_ondragenterDelegate;

		// Token: 0x04000AE3 RID: 2787
		public HTMLScriptEvents_ondragendEventHandler m_ondragendDelegate;

		// Token: 0x04000AE4 RID: 2788
		public HTMLScriptEvents_ondragEventHandler m_ondragDelegate;

		// Token: 0x04000AE5 RID: 2789
		public HTMLScriptEvents_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x04000AE6 RID: 2790
		public HTMLScriptEvents_onblurEventHandler m_onblurDelegate;

		// Token: 0x04000AE7 RID: 2791
		public HTMLScriptEvents_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x04000AE8 RID: 2792
		public HTMLScriptEvents_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x04000AE9 RID: 2793
		public HTMLScriptEvents_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x04000AEA RID: 2794
		public HTMLScriptEvents_onlosecaptureEventHandler m_onlosecaptureDelegate;

		// Token: 0x04000AEB RID: 2795
		public HTMLScriptEvents_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x04000AEC RID: 2796
		public HTMLScriptEvents_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x04000AED RID: 2797
		public HTMLScriptEvents_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x04000AEE RID: 2798
		public HTMLScriptEvents_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x04000AEF RID: 2799
		public HTMLScriptEvents_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x04000AF0 RID: 2800
		public HTMLScriptEvents_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x04000AF1 RID: 2801
		public HTMLScriptEvents_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x04000AF2 RID: 2802
		public HTMLScriptEvents_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x04000AF3 RID: 2803
		public HTMLScriptEvents_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x04000AF4 RID: 2804
		public HTMLScriptEvents_onfilterchangeEventHandler m_onfilterchangeDelegate;

		// Token: 0x04000AF5 RID: 2805
		public HTMLScriptEvents_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x04000AF6 RID: 2806
		public HTMLScriptEvents_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x04000AF7 RID: 2807
		public HTMLScriptEvents_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x04000AF8 RID: 2808
		public HTMLScriptEvents_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x04000AF9 RID: 2809
		public HTMLScriptEvents_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x04000AFA RID: 2810
		public HTMLScriptEvents_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x04000AFB RID: 2811
		public HTMLScriptEvents_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x04000AFC RID: 2812
		public HTMLScriptEvents_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x04000AFD RID: 2813
		public HTMLScriptEvents_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x04000AFE RID: 2814
		public HTMLScriptEvents_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x04000AFF RID: 2815
		public HTMLScriptEvents_onclickEventHandler m_onclickDelegate;

		// Token: 0x04000B00 RID: 2816
		public HTMLScriptEvents_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x04000B01 RID: 2817
		public int m_dwCookie;
	}
}
