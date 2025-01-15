using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DDA RID: 3546
	[ClassInterface(0)]
	internal sealed class HTMLFrameSiteEvents_SinkHelper : HTMLFrameSiteEvents
	{
		// Token: 0x06017DC3 RID: 97731 RVA: 0x0005384C File Offset: 0x0005284C
		public override void onload()
		{
			if (this.m_onloadDelegate != null)
			{
				this.m_onloadDelegate();
				return;
			}
		}

		// Token: 0x06017DC4 RID: 97732 RVA: 0x00053878 File Offset: 0x00052878
		public override void onfocusout()
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate();
				return;
			}
		}

		// Token: 0x06017DC5 RID: 97733 RVA: 0x000538A4 File Offset: 0x000528A4
		public override void onfocusin()
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate();
				return;
			}
		}

		// Token: 0x06017DC6 RID: 97734 RVA: 0x000538D0 File Offset: 0x000528D0
		public override void ondeactivate()
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate();
				return;
			}
		}

		// Token: 0x06017DC7 RID: 97735 RVA: 0x000538FC File Offset: 0x000528FC
		public override void onactivate()
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate();
				return;
			}
		}

		// Token: 0x06017DC8 RID: 97736 RVA: 0x00053928 File Offset: 0x00052928
		public override bool onmousewheel()
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate();
		}

		// Token: 0x06017DC9 RID: 97737 RVA: 0x00053954 File Offset: 0x00052954
		public override void onmouseleave()
		{
			if (this.m_onmouseleaveDelegate != null)
			{
				this.m_onmouseleaveDelegate();
				return;
			}
		}

		// Token: 0x06017DCA RID: 97738 RVA: 0x00053980 File Offset: 0x00052980
		public override void onmouseenter()
		{
			if (this.m_onmouseenterDelegate != null)
			{
				this.m_onmouseenterDelegate();
				return;
			}
		}

		// Token: 0x06017DCB RID: 97739 RVA: 0x000539AC File Offset: 0x000529AC
		public override void onresizeend()
		{
			if (this.m_onresizeendDelegate != null)
			{
				this.m_onresizeendDelegate();
				return;
			}
		}

		// Token: 0x06017DCC RID: 97740 RVA: 0x000539D8 File Offset: 0x000529D8
		public override bool onresizestart()
		{
			return this.m_onresizestartDelegate != null && this.m_onresizestartDelegate();
		}

		// Token: 0x06017DCD RID: 97741 RVA: 0x00053A04 File Offset: 0x00052A04
		public override void onmoveend()
		{
			if (this.m_onmoveendDelegate != null)
			{
				this.m_onmoveendDelegate();
				return;
			}
		}

		// Token: 0x06017DCE RID: 97742 RVA: 0x00053A30 File Offset: 0x00052A30
		public override bool onmovestart()
		{
			return this.m_onmovestartDelegate != null && this.m_onmovestartDelegate();
		}

		// Token: 0x06017DCF RID: 97743 RVA: 0x00053A5C File Offset: 0x00052A5C
		public override bool oncontrolselect()
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate();
		}

		// Token: 0x06017DD0 RID: 97744 RVA: 0x00053A88 File Offset: 0x00052A88
		public override void onmove()
		{
			if (this.m_onmoveDelegate != null)
			{
				this.m_onmoveDelegate();
				return;
			}
		}

		// Token: 0x06017DD1 RID: 97745 RVA: 0x00053AB4 File Offset: 0x00052AB4
		public override bool onbeforeactivate()
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate();
		}

		// Token: 0x06017DD2 RID: 97746 RVA: 0x00053AE0 File Offset: 0x00052AE0
		public override bool onbeforedeactivate()
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate();
		}

		// Token: 0x06017DD3 RID: 97747 RVA: 0x00053B0C File Offset: 0x00052B0C
		public override void onpage()
		{
			if (this.m_onpageDelegate != null)
			{
				this.m_onpageDelegate();
				return;
			}
		}

		// Token: 0x06017DD4 RID: 97748 RVA: 0x00053B38 File Offset: 0x00052B38
		public override void onlayoutcomplete()
		{
			if (this.m_onlayoutcompleteDelegate != null)
			{
				this.m_onlayoutcompleteDelegate();
				return;
			}
		}

		// Token: 0x06017DD5 RID: 97749 RVA: 0x00053B64 File Offset: 0x00052B64
		public override void onbeforeeditfocus()
		{
			if (this.m_onbeforeeditfocusDelegate != null)
			{
				this.m_onbeforeeditfocusDelegate();
				return;
			}
		}

		// Token: 0x06017DD6 RID: 97750 RVA: 0x00053B90 File Offset: 0x00052B90
		public override void onreadystatechange()
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate();
				return;
			}
		}

		// Token: 0x06017DD7 RID: 97751 RVA: 0x00053BBC File Offset: 0x00052BBC
		public override void oncellchange()
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate();
				return;
			}
		}

		// Token: 0x06017DD8 RID: 97752 RVA: 0x00053BE8 File Offset: 0x00052BE8
		public override void onrowsinserted()
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate();
				return;
			}
		}

		// Token: 0x06017DD9 RID: 97753 RVA: 0x00053C14 File Offset: 0x00052C14
		public override void onrowsdelete()
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate();
				return;
			}
		}

		// Token: 0x06017DDA RID: 97754 RVA: 0x00053C40 File Offset: 0x00052C40
		public override bool oncontextmenu()
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate();
		}

		// Token: 0x06017DDB RID: 97755 RVA: 0x00053C6C File Offset: 0x00052C6C
		public override bool onpaste()
		{
			return this.m_onpasteDelegate != null && this.m_onpasteDelegate();
		}

		// Token: 0x06017DDC RID: 97756 RVA: 0x00053C98 File Offset: 0x00052C98
		public override bool onbeforepaste()
		{
			return this.m_onbeforepasteDelegate != null && this.m_onbeforepasteDelegate();
		}

		// Token: 0x06017DDD RID: 97757 RVA: 0x00053CC4 File Offset: 0x00052CC4
		public override bool oncopy()
		{
			return this.m_oncopyDelegate != null && this.m_oncopyDelegate();
		}

		// Token: 0x06017DDE RID: 97758 RVA: 0x00053CF0 File Offset: 0x00052CF0
		public override bool onbeforecopy()
		{
			return this.m_onbeforecopyDelegate != null && this.m_onbeforecopyDelegate();
		}

		// Token: 0x06017DDF RID: 97759 RVA: 0x00053D1C File Offset: 0x00052D1C
		public override bool oncut()
		{
			return this.m_oncutDelegate != null && this.m_oncutDelegate();
		}

		// Token: 0x06017DE0 RID: 97760 RVA: 0x00053D48 File Offset: 0x00052D48
		public override bool onbeforecut()
		{
			return this.m_onbeforecutDelegate != null && this.m_onbeforecutDelegate();
		}

		// Token: 0x06017DE1 RID: 97761 RVA: 0x00053D74 File Offset: 0x00052D74
		public override bool ondrop()
		{
			return this.m_ondropDelegate != null && this.m_ondropDelegate();
		}

		// Token: 0x06017DE2 RID: 97762 RVA: 0x00053DA0 File Offset: 0x00052DA0
		public override void ondragleave()
		{
			if (this.m_ondragleaveDelegate != null)
			{
				this.m_ondragleaveDelegate();
				return;
			}
		}

		// Token: 0x06017DE3 RID: 97763 RVA: 0x00053DCC File Offset: 0x00052DCC
		public override bool ondragover()
		{
			return this.m_ondragoverDelegate != null && this.m_ondragoverDelegate();
		}

		// Token: 0x06017DE4 RID: 97764 RVA: 0x00053DF8 File Offset: 0x00052DF8
		public override bool ondragenter()
		{
			return this.m_ondragenterDelegate != null && this.m_ondragenterDelegate();
		}

		// Token: 0x06017DE5 RID: 97765 RVA: 0x00053E24 File Offset: 0x00052E24
		public override void ondragend()
		{
			if (this.m_ondragendDelegate != null)
			{
				this.m_ondragendDelegate();
				return;
			}
		}

		// Token: 0x06017DE6 RID: 97766 RVA: 0x00053E50 File Offset: 0x00052E50
		public override bool ondrag()
		{
			return this.m_ondragDelegate != null && this.m_ondragDelegate();
		}

		// Token: 0x06017DE7 RID: 97767 RVA: 0x00053E7C File Offset: 0x00052E7C
		public override void onresize()
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate();
				return;
			}
		}

		// Token: 0x06017DE8 RID: 97768 RVA: 0x00053EA8 File Offset: 0x00052EA8
		public override void onblur()
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate();
				return;
			}
		}

		// Token: 0x06017DE9 RID: 97769 RVA: 0x00053ED4 File Offset: 0x00052ED4
		public override void onfocus()
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate();
				return;
			}
		}

		// Token: 0x06017DEA RID: 97770 RVA: 0x00053F00 File Offset: 0x00052F00
		public override void onscroll()
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate();
				return;
			}
		}

		// Token: 0x06017DEB RID: 97771 RVA: 0x00053F2C File Offset: 0x00052F2C
		public override void onpropertychange()
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate();
				return;
			}
		}

		// Token: 0x06017DEC RID: 97772 RVA: 0x00053F58 File Offset: 0x00052F58
		public override void onlosecapture()
		{
			if (this.m_onlosecaptureDelegate != null)
			{
				this.m_onlosecaptureDelegate();
				return;
			}
		}

		// Token: 0x06017DED RID: 97773 RVA: 0x00053F84 File Offset: 0x00052F84
		public override void ondatasetcomplete()
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate();
				return;
			}
		}

		// Token: 0x06017DEE RID: 97774 RVA: 0x00053FB0 File Offset: 0x00052FB0
		public override void ondataavailable()
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate();
				return;
			}
		}

		// Token: 0x06017DEF RID: 97775 RVA: 0x00053FDC File Offset: 0x00052FDC
		public override void ondatasetchanged()
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate();
				return;
			}
		}

		// Token: 0x06017DF0 RID: 97776 RVA: 0x00054008 File Offset: 0x00053008
		public override void onrowenter()
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate();
				return;
			}
		}

		// Token: 0x06017DF1 RID: 97777 RVA: 0x00054034 File Offset: 0x00053034
		public override bool onrowexit()
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate();
		}

		// Token: 0x06017DF2 RID: 97778 RVA: 0x00054060 File Offset: 0x00053060
		public override bool onerrorupdate()
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate();
		}

		// Token: 0x06017DF3 RID: 97779 RVA: 0x0005408C File Offset: 0x0005308C
		public override void onafterupdate()
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate();
				return;
			}
		}

		// Token: 0x06017DF4 RID: 97780 RVA: 0x000540B8 File Offset: 0x000530B8
		public override bool onbeforeupdate()
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate();
		}

		// Token: 0x06017DF5 RID: 97781 RVA: 0x000540E4 File Offset: 0x000530E4
		public override bool ondragstart()
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate();
		}

		// Token: 0x06017DF6 RID: 97782 RVA: 0x00054110 File Offset: 0x00053110
		public override void onfilterchange()
		{
			if (this.m_onfilterchangeDelegate != null)
			{
				this.m_onfilterchangeDelegate();
				return;
			}
		}

		// Token: 0x06017DF7 RID: 97783 RVA: 0x0005413C File Offset: 0x0005313C
		public override bool onselectstart()
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate();
		}

		// Token: 0x06017DF8 RID: 97784 RVA: 0x00054168 File Offset: 0x00053168
		public override void onmouseup()
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate();
				return;
			}
		}

		// Token: 0x06017DF9 RID: 97785 RVA: 0x00054194 File Offset: 0x00053194
		public override void onmousedown()
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate();
				return;
			}
		}

		// Token: 0x06017DFA RID: 97786 RVA: 0x000541C0 File Offset: 0x000531C0
		public override void onmousemove()
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate();
				return;
			}
		}

		// Token: 0x06017DFB RID: 97787 RVA: 0x000541EC File Offset: 0x000531EC
		public override void onmouseover()
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate();
				return;
			}
		}

		// Token: 0x06017DFC RID: 97788 RVA: 0x00054218 File Offset: 0x00053218
		public override void onmouseout()
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate();
				return;
			}
		}

		// Token: 0x06017DFD RID: 97789 RVA: 0x00054244 File Offset: 0x00053244
		public override void onkeyup()
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate();
				return;
			}
		}

		// Token: 0x06017DFE RID: 97790 RVA: 0x00054270 File Offset: 0x00053270
		public override void onkeydown()
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate();
				return;
			}
		}

		// Token: 0x06017DFF RID: 97791 RVA: 0x0005429C File Offset: 0x0005329C
		public override bool onkeypress()
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate();
		}

		// Token: 0x06017E00 RID: 97792 RVA: 0x000542C8 File Offset: 0x000532C8
		public override bool ondblclick()
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate();
		}

		// Token: 0x06017E01 RID: 97793 RVA: 0x000542F4 File Offset: 0x000532F4
		public override bool onclick()
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate();
		}

		// Token: 0x06017E02 RID: 97794 RVA: 0x00054320 File Offset: 0x00053320
		public override bool onhelp()
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate();
		}

		// Token: 0x06017E03 RID: 97795 RVA: 0x0005434C File Offset: 0x0005334C
		internal HTMLFrameSiteEvents_SinkHelper()
		{
			this.m_dwCookie = 0;
			this.m_onloadDelegate = null;
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

		// Token: 0x040007D7 RID: 2007
		public HTMLFrameSiteEvents_onloadEventHandler m_onloadDelegate;

		// Token: 0x040007D8 RID: 2008
		public HTMLFrameSiteEvents_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x040007D9 RID: 2009
		public HTMLFrameSiteEvents_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x040007DA RID: 2010
		public HTMLFrameSiteEvents_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x040007DB RID: 2011
		public HTMLFrameSiteEvents_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x040007DC RID: 2012
		public HTMLFrameSiteEvents_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x040007DD RID: 2013
		public HTMLFrameSiteEvents_onmouseleaveEventHandler m_onmouseleaveDelegate;

		// Token: 0x040007DE RID: 2014
		public HTMLFrameSiteEvents_onmouseenterEventHandler m_onmouseenterDelegate;

		// Token: 0x040007DF RID: 2015
		public HTMLFrameSiteEvents_onresizeendEventHandler m_onresizeendDelegate;

		// Token: 0x040007E0 RID: 2016
		public HTMLFrameSiteEvents_onresizestartEventHandler m_onresizestartDelegate;

		// Token: 0x040007E1 RID: 2017
		public HTMLFrameSiteEvents_onmoveendEventHandler m_onmoveendDelegate;

		// Token: 0x040007E2 RID: 2018
		public HTMLFrameSiteEvents_onmovestartEventHandler m_onmovestartDelegate;

		// Token: 0x040007E3 RID: 2019
		public HTMLFrameSiteEvents_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x040007E4 RID: 2020
		public HTMLFrameSiteEvents_onmoveEventHandler m_onmoveDelegate;

		// Token: 0x040007E5 RID: 2021
		public HTMLFrameSiteEvents_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x040007E6 RID: 2022
		public HTMLFrameSiteEvents_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x040007E7 RID: 2023
		public HTMLFrameSiteEvents_onpageEventHandler m_onpageDelegate;

		// Token: 0x040007E8 RID: 2024
		public HTMLFrameSiteEvents_onlayoutcompleteEventHandler m_onlayoutcompleteDelegate;

		// Token: 0x040007E9 RID: 2025
		public HTMLFrameSiteEvents_onbeforeeditfocusEventHandler m_onbeforeeditfocusDelegate;

		// Token: 0x040007EA RID: 2026
		public HTMLFrameSiteEvents_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x040007EB RID: 2027
		public HTMLFrameSiteEvents_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x040007EC RID: 2028
		public HTMLFrameSiteEvents_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x040007ED RID: 2029
		public HTMLFrameSiteEvents_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x040007EE RID: 2030
		public HTMLFrameSiteEvents_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x040007EF RID: 2031
		public HTMLFrameSiteEvents_onpasteEventHandler m_onpasteDelegate;

		// Token: 0x040007F0 RID: 2032
		public HTMLFrameSiteEvents_onbeforepasteEventHandler m_onbeforepasteDelegate;

		// Token: 0x040007F1 RID: 2033
		public HTMLFrameSiteEvents_oncopyEventHandler m_oncopyDelegate;

		// Token: 0x040007F2 RID: 2034
		public HTMLFrameSiteEvents_onbeforecopyEventHandler m_onbeforecopyDelegate;

		// Token: 0x040007F3 RID: 2035
		public HTMLFrameSiteEvents_oncutEventHandler m_oncutDelegate;

		// Token: 0x040007F4 RID: 2036
		public HTMLFrameSiteEvents_onbeforecutEventHandler m_onbeforecutDelegate;

		// Token: 0x040007F5 RID: 2037
		public HTMLFrameSiteEvents_ondropEventHandler m_ondropDelegate;

		// Token: 0x040007F6 RID: 2038
		public HTMLFrameSiteEvents_ondragleaveEventHandler m_ondragleaveDelegate;

		// Token: 0x040007F7 RID: 2039
		public HTMLFrameSiteEvents_ondragoverEventHandler m_ondragoverDelegate;

		// Token: 0x040007F8 RID: 2040
		public HTMLFrameSiteEvents_ondragenterEventHandler m_ondragenterDelegate;

		// Token: 0x040007F9 RID: 2041
		public HTMLFrameSiteEvents_ondragendEventHandler m_ondragendDelegate;

		// Token: 0x040007FA RID: 2042
		public HTMLFrameSiteEvents_ondragEventHandler m_ondragDelegate;

		// Token: 0x040007FB RID: 2043
		public HTMLFrameSiteEvents_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x040007FC RID: 2044
		public HTMLFrameSiteEvents_onblurEventHandler m_onblurDelegate;

		// Token: 0x040007FD RID: 2045
		public HTMLFrameSiteEvents_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x040007FE RID: 2046
		public HTMLFrameSiteEvents_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x040007FF RID: 2047
		public HTMLFrameSiteEvents_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x04000800 RID: 2048
		public HTMLFrameSiteEvents_onlosecaptureEventHandler m_onlosecaptureDelegate;

		// Token: 0x04000801 RID: 2049
		public HTMLFrameSiteEvents_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x04000802 RID: 2050
		public HTMLFrameSiteEvents_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x04000803 RID: 2051
		public HTMLFrameSiteEvents_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x04000804 RID: 2052
		public HTMLFrameSiteEvents_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x04000805 RID: 2053
		public HTMLFrameSiteEvents_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x04000806 RID: 2054
		public HTMLFrameSiteEvents_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x04000807 RID: 2055
		public HTMLFrameSiteEvents_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x04000808 RID: 2056
		public HTMLFrameSiteEvents_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x04000809 RID: 2057
		public HTMLFrameSiteEvents_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x0400080A RID: 2058
		public HTMLFrameSiteEvents_onfilterchangeEventHandler m_onfilterchangeDelegate;

		// Token: 0x0400080B RID: 2059
		public HTMLFrameSiteEvents_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x0400080C RID: 2060
		public HTMLFrameSiteEvents_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x0400080D RID: 2061
		public HTMLFrameSiteEvents_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x0400080E RID: 2062
		public HTMLFrameSiteEvents_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x0400080F RID: 2063
		public HTMLFrameSiteEvents_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x04000810 RID: 2064
		public HTMLFrameSiteEvents_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x04000811 RID: 2065
		public HTMLFrameSiteEvents_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x04000812 RID: 2066
		public HTMLFrameSiteEvents_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x04000813 RID: 2067
		public HTMLFrameSiteEvents_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x04000814 RID: 2068
		public HTMLFrameSiteEvents_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x04000815 RID: 2069
		public HTMLFrameSiteEvents_onclickEventHandler m_onclickDelegate;

		// Token: 0x04000816 RID: 2070
		public HTMLFrameSiteEvents_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x04000817 RID: 2071
		public int m_dwCookie;
	}
}
