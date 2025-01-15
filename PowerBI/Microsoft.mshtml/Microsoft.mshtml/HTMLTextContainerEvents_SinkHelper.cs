using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DFC RID: 3580
	[ClassInterface(0)]
	internal sealed class HTMLTextContainerEvents_SinkHelper : HTMLTextContainerEvents
	{
		// Token: 0x0601895E RID: 100702 RVA: 0x000BD4BC File Offset: 0x000BC4BC
		public override void onselect()
		{
			if (this.m_onselectDelegate != null)
			{
				this.m_onselectDelegate();
				return;
			}
		}

		// Token: 0x0601895F RID: 100703 RVA: 0x000BD4E8 File Offset: 0x000BC4E8
		public override void onchange()
		{
			if (this.m_onchangeDelegate != null)
			{
				this.m_onchangeDelegate();
				return;
			}
		}

		// Token: 0x06018960 RID: 100704 RVA: 0x000BD514 File Offset: 0x000BC514
		public override void onfocusout()
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate();
				return;
			}
		}

		// Token: 0x06018961 RID: 100705 RVA: 0x000BD540 File Offset: 0x000BC540
		public override void onfocusin()
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate();
				return;
			}
		}

		// Token: 0x06018962 RID: 100706 RVA: 0x000BD56C File Offset: 0x000BC56C
		public override void ondeactivate()
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate();
				return;
			}
		}

		// Token: 0x06018963 RID: 100707 RVA: 0x000BD598 File Offset: 0x000BC598
		public override void onactivate()
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate();
				return;
			}
		}

		// Token: 0x06018964 RID: 100708 RVA: 0x000BD5C4 File Offset: 0x000BC5C4
		public override bool onmousewheel()
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate();
		}

		// Token: 0x06018965 RID: 100709 RVA: 0x000BD5F0 File Offset: 0x000BC5F0
		public override void onmouseleave()
		{
			if (this.m_onmouseleaveDelegate != null)
			{
				this.m_onmouseleaveDelegate();
				return;
			}
		}

		// Token: 0x06018966 RID: 100710 RVA: 0x000BD61C File Offset: 0x000BC61C
		public override void onmouseenter()
		{
			if (this.m_onmouseenterDelegate != null)
			{
				this.m_onmouseenterDelegate();
				return;
			}
		}

		// Token: 0x06018967 RID: 100711 RVA: 0x000BD648 File Offset: 0x000BC648
		public override void onresizeend()
		{
			if (this.m_onresizeendDelegate != null)
			{
				this.m_onresizeendDelegate();
				return;
			}
		}

		// Token: 0x06018968 RID: 100712 RVA: 0x000BD674 File Offset: 0x000BC674
		public override bool onresizestart()
		{
			return this.m_onresizestartDelegate != null && this.m_onresizestartDelegate();
		}

		// Token: 0x06018969 RID: 100713 RVA: 0x000BD6A0 File Offset: 0x000BC6A0
		public override void onmoveend()
		{
			if (this.m_onmoveendDelegate != null)
			{
				this.m_onmoveendDelegate();
				return;
			}
		}

		// Token: 0x0601896A RID: 100714 RVA: 0x000BD6CC File Offset: 0x000BC6CC
		public override bool onmovestart()
		{
			return this.m_onmovestartDelegate != null && this.m_onmovestartDelegate();
		}

		// Token: 0x0601896B RID: 100715 RVA: 0x000BD6F8 File Offset: 0x000BC6F8
		public override bool oncontrolselect()
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate();
		}

		// Token: 0x0601896C RID: 100716 RVA: 0x000BD724 File Offset: 0x000BC724
		public override void onmove()
		{
			if (this.m_onmoveDelegate != null)
			{
				this.m_onmoveDelegate();
				return;
			}
		}

		// Token: 0x0601896D RID: 100717 RVA: 0x000BD750 File Offset: 0x000BC750
		public override bool onbeforeactivate()
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate();
		}

		// Token: 0x0601896E RID: 100718 RVA: 0x000BD77C File Offset: 0x000BC77C
		public override bool onbeforedeactivate()
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate();
		}

		// Token: 0x0601896F RID: 100719 RVA: 0x000BD7A8 File Offset: 0x000BC7A8
		public override void onpage()
		{
			if (this.m_onpageDelegate != null)
			{
				this.m_onpageDelegate();
				return;
			}
		}

		// Token: 0x06018970 RID: 100720 RVA: 0x000BD7D4 File Offset: 0x000BC7D4
		public override void onlayoutcomplete()
		{
			if (this.m_onlayoutcompleteDelegate != null)
			{
				this.m_onlayoutcompleteDelegate();
				return;
			}
		}

		// Token: 0x06018971 RID: 100721 RVA: 0x000BD800 File Offset: 0x000BC800
		public override void onbeforeeditfocus()
		{
			if (this.m_onbeforeeditfocusDelegate != null)
			{
				this.m_onbeforeeditfocusDelegate();
				return;
			}
		}

		// Token: 0x06018972 RID: 100722 RVA: 0x000BD82C File Offset: 0x000BC82C
		public override void onreadystatechange()
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate();
				return;
			}
		}

		// Token: 0x06018973 RID: 100723 RVA: 0x000BD858 File Offset: 0x000BC858
		public override void oncellchange()
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate();
				return;
			}
		}

		// Token: 0x06018974 RID: 100724 RVA: 0x000BD884 File Offset: 0x000BC884
		public override void onrowsinserted()
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate();
				return;
			}
		}

		// Token: 0x06018975 RID: 100725 RVA: 0x000BD8B0 File Offset: 0x000BC8B0
		public override void onrowsdelete()
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate();
				return;
			}
		}

		// Token: 0x06018976 RID: 100726 RVA: 0x000BD8DC File Offset: 0x000BC8DC
		public override bool oncontextmenu()
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate();
		}

		// Token: 0x06018977 RID: 100727 RVA: 0x000BD908 File Offset: 0x000BC908
		public override bool onpaste()
		{
			return this.m_onpasteDelegate != null && this.m_onpasteDelegate();
		}

		// Token: 0x06018978 RID: 100728 RVA: 0x000BD934 File Offset: 0x000BC934
		public override bool onbeforepaste()
		{
			return this.m_onbeforepasteDelegate != null && this.m_onbeforepasteDelegate();
		}

		// Token: 0x06018979 RID: 100729 RVA: 0x000BD960 File Offset: 0x000BC960
		public override bool oncopy()
		{
			return this.m_oncopyDelegate != null && this.m_oncopyDelegate();
		}

		// Token: 0x0601897A RID: 100730 RVA: 0x000BD98C File Offset: 0x000BC98C
		public override bool onbeforecopy()
		{
			return this.m_onbeforecopyDelegate != null && this.m_onbeforecopyDelegate();
		}

		// Token: 0x0601897B RID: 100731 RVA: 0x000BD9B8 File Offset: 0x000BC9B8
		public override bool oncut()
		{
			return this.m_oncutDelegate != null && this.m_oncutDelegate();
		}

		// Token: 0x0601897C RID: 100732 RVA: 0x000BD9E4 File Offset: 0x000BC9E4
		public override bool onbeforecut()
		{
			return this.m_onbeforecutDelegate != null && this.m_onbeforecutDelegate();
		}

		// Token: 0x0601897D RID: 100733 RVA: 0x000BDA10 File Offset: 0x000BCA10
		public override bool ondrop()
		{
			return this.m_ondropDelegate != null && this.m_ondropDelegate();
		}

		// Token: 0x0601897E RID: 100734 RVA: 0x000BDA3C File Offset: 0x000BCA3C
		public override void ondragleave()
		{
			if (this.m_ondragleaveDelegate != null)
			{
				this.m_ondragleaveDelegate();
				return;
			}
		}

		// Token: 0x0601897F RID: 100735 RVA: 0x000BDA68 File Offset: 0x000BCA68
		public override bool ondragover()
		{
			return this.m_ondragoverDelegate != null && this.m_ondragoverDelegate();
		}

		// Token: 0x06018980 RID: 100736 RVA: 0x000BDA94 File Offset: 0x000BCA94
		public override bool ondragenter()
		{
			return this.m_ondragenterDelegate != null && this.m_ondragenterDelegate();
		}

		// Token: 0x06018981 RID: 100737 RVA: 0x000BDAC0 File Offset: 0x000BCAC0
		public override void ondragend()
		{
			if (this.m_ondragendDelegate != null)
			{
				this.m_ondragendDelegate();
				return;
			}
		}

		// Token: 0x06018982 RID: 100738 RVA: 0x000BDAEC File Offset: 0x000BCAEC
		public override bool ondrag()
		{
			return this.m_ondragDelegate != null && this.m_ondragDelegate();
		}

		// Token: 0x06018983 RID: 100739 RVA: 0x000BDB18 File Offset: 0x000BCB18
		public override void onresize()
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate();
				return;
			}
		}

		// Token: 0x06018984 RID: 100740 RVA: 0x000BDB44 File Offset: 0x000BCB44
		public override void onblur()
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate();
				return;
			}
		}

		// Token: 0x06018985 RID: 100741 RVA: 0x000BDB70 File Offset: 0x000BCB70
		public override void onfocus()
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate();
				return;
			}
		}

		// Token: 0x06018986 RID: 100742 RVA: 0x000BDB9C File Offset: 0x000BCB9C
		public override void onscroll()
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate();
				return;
			}
		}

		// Token: 0x06018987 RID: 100743 RVA: 0x000BDBC8 File Offset: 0x000BCBC8
		public override void onpropertychange()
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate();
				return;
			}
		}

		// Token: 0x06018988 RID: 100744 RVA: 0x000BDBF4 File Offset: 0x000BCBF4
		public override void onlosecapture()
		{
			if (this.m_onlosecaptureDelegate != null)
			{
				this.m_onlosecaptureDelegate();
				return;
			}
		}

		// Token: 0x06018989 RID: 100745 RVA: 0x000BDC20 File Offset: 0x000BCC20
		public override void ondatasetcomplete()
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate();
				return;
			}
		}

		// Token: 0x0601898A RID: 100746 RVA: 0x000BDC4C File Offset: 0x000BCC4C
		public override void ondataavailable()
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate();
				return;
			}
		}

		// Token: 0x0601898B RID: 100747 RVA: 0x000BDC78 File Offset: 0x000BCC78
		public override void ondatasetchanged()
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate();
				return;
			}
		}

		// Token: 0x0601898C RID: 100748 RVA: 0x000BDCA4 File Offset: 0x000BCCA4
		public override void onrowenter()
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate();
				return;
			}
		}

		// Token: 0x0601898D RID: 100749 RVA: 0x000BDCD0 File Offset: 0x000BCCD0
		public override bool onrowexit()
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate();
		}

		// Token: 0x0601898E RID: 100750 RVA: 0x000BDCFC File Offset: 0x000BCCFC
		public override bool onerrorupdate()
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate();
		}

		// Token: 0x0601898F RID: 100751 RVA: 0x000BDD28 File Offset: 0x000BCD28
		public override void onafterupdate()
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate();
				return;
			}
		}

		// Token: 0x06018990 RID: 100752 RVA: 0x000BDD54 File Offset: 0x000BCD54
		public override bool onbeforeupdate()
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate();
		}

		// Token: 0x06018991 RID: 100753 RVA: 0x000BDD80 File Offset: 0x000BCD80
		public override bool ondragstart()
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate();
		}

		// Token: 0x06018992 RID: 100754 RVA: 0x000BDDAC File Offset: 0x000BCDAC
		public override void onfilterchange()
		{
			if (this.m_onfilterchangeDelegate != null)
			{
				this.m_onfilterchangeDelegate();
				return;
			}
		}

		// Token: 0x06018993 RID: 100755 RVA: 0x000BDDD8 File Offset: 0x000BCDD8
		public override bool onselectstart()
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate();
		}

		// Token: 0x06018994 RID: 100756 RVA: 0x000BDE04 File Offset: 0x000BCE04
		public override void onmouseup()
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate();
				return;
			}
		}

		// Token: 0x06018995 RID: 100757 RVA: 0x000BDE30 File Offset: 0x000BCE30
		public override void onmousedown()
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate();
				return;
			}
		}

		// Token: 0x06018996 RID: 100758 RVA: 0x000BDE5C File Offset: 0x000BCE5C
		public override void onmousemove()
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate();
				return;
			}
		}

		// Token: 0x06018997 RID: 100759 RVA: 0x000BDE88 File Offset: 0x000BCE88
		public override void onmouseover()
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate();
				return;
			}
		}

		// Token: 0x06018998 RID: 100760 RVA: 0x000BDEB4 File Offset: 0x000BCEB4
		public override void onmouseout()
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate();
				return;
			}
		}

		// Token: 0x06018999 RID: 100761 RVA: 0x000BDEE0 File Offset: 0x000BCEE0
		public override void onkeyup()
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate();
				return;
			}
		}

		// Token: 0x0601899A RID: 100762 RVA: 0x000BDF0C File Offset: 0x000BCF0C
		public override void onkeydown()
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate();
				return;
			}
		}

		// Token: 0x0601899B RID: 100763 RVA: 0x000BDF38 File Offset: 0x000BCF38
		public override bool onkeypress()
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate();
		}

		// Token: 0x0601899C RID: 100764 RVA: 0x000BDF64 File Offset: 0x000BCF64
		public override bool ondblclick()
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate();
		}

		// Token: 0x0601899D RID: 100765 RVA: 0x000BDF90 File Offset: 0x000BCF90
		public override bool onclick()
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate();
		}

		// Token: 0x0601899E RID: 100766 RVA: 0x000BDFBC File Offset: 0x000BCFBC
		public override bool onhelp()
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate();
		}

		// Token: 0x0601899F RID: 100767 RVA: 0x000BDFE8 File Offset: 0x000BCFE8
		internal HTMLTextContainerEvents_SinkHelper()
		{
			this.m_dwCookie = 0;
			this.m_onselectDelegate = null;
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

		// Token: 0x04000BDD RID: 3037
		public HTMLTextContainerEvents_onselectEventHandler m_onselectDelegate;

		// Token: 0x04000BDE RID: 3038
		public HTMLTextContainerEvents_onchangeEventHandler m_onchangeDelegate;

		// Token: 0x04000BDF RID: 3039
		public HTMLTextContainerEvents_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x04000BE0 RID: 3040
		public HTMLTextContainerEvents_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x04000BE1 RID: 3041
		public HTMLTextContainerEvents_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x04000BE2 RID: 3042
		public HTMLTextContainerEvents_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x04000BE3 RID: 3043
		public HTMLTextContainerEvents_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x04000BE4 RID: 3044
		public HTMLTextContainerEvents_onmouseleaveEventHandler m_onmouseleaveDelegate;

		// Token: 0x04000BE5 RID: 3045
		public HTMLTextContainerEvents_onmouseenterEventHandler m_onmouseenterDelegate;

		// Token: 0x04000BE6 RID: 3046
		public HTMLTextContainerEvents_onresizeendEventHandler m_onresizeendDelegate;

		// Token: 0x04000BE7 RID: 3047
		public HTMLTextContainerEvents_onresizestartEventHandler m_onresizestartDelegate;

		// Token: 0x04000BE8 RID: 3048
		public HTMLTextContainerEvents_onmoveendEventHandler m_onmoveendDelegate;

		// Token: 0x04000BE9 RID: 3049
		public HTMLTextContainerEvents_onmovestartEventHandler m_onmovestartDelegate;

		// Token: 0x04000BEA RID: 3050
		public HTMLTextContainerEvents_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x04000BEB RID: 3051
		public HTMLTextContainerEvents_onmoveEventHandler m_onmoveDelegate;

		// Token: 0x04000BEC RID: 3052
		public HTMLTextContainerEvents_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x04000BED RID: 3053
		public HTMLTextContainerEvents_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x04000BEE RID: 3054
		public HTMLTextContainerEvents_onpageEventHandler m_onpageDelegate;

		// Token: 0x04000BEF RID: 3055
		public HTMLTextContainerEvents_onlayoutcompleteEventHandler m_onlayoutcompleteDelegate;

		// Token: 0x04000BF0 RID: 3056
		public HTMLTextContainerEvents_onbeforeeditfocusEventHandler m_onbeforeeditfocusDelegate;

		// Token: 0x04000BF1 RID: 3057
		public HTMLTextContainerEvents_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x04000BF2 RID: 3058
		public HTMLTextContainerEvents_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x04000BF3 RID: 3059
		public HTMLTextContainerEvents_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x04000BF4 RID: 3060
		public HTMLTextContainerEvents_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x04000BF5 RID: 3061
		public HTMLTextContainerEvents_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x04000BF6 RID: 3062
		public HTMLTextContainerEvents_onpasteEventHandler m_onpasteDelegate;

		// Token: 0x04000BF7 RID: 3063
		public HTMLTextContainerEvents_onbeforepasteEventHandler m_onbeforepasteDelegate;

		// Token: 0x04000BF8 RID: 3064
		public HTMLTextContainerEvents_oncopyEventHandler m_oncopyDelegate;

		// Token: 0x04000BF9 RID: 3065
		public HTMLTextContainerEvents_onbeforecopyEventHandler m_onbeforecopyDelegate;

		// Token: 0x04000BFA RID: 3066
		public HTMLTextContainerEvents_oncutEventHandler m_oncutDelegate;

		// Token: 0x04000BFB RID: 3067
		public HTMLTextContainerEvents_onbeforecutEventHandler m_onbeforecutDelegate;

		// Token: 0x04000BFC RID: 3068
		public HTMLTextContainerEvents_ondropEventHandler m_ondropDelegate;

		// Token: 0x04000BFD RID: 3069
		public HTMLTextContainerEvents_ondragleaveEventHandler m_ondragleaveDelegate;

		// Token: 0x04000BFE RID: 3070
		public HTMLTextContainerEvents_ondragoverEventHandler m_ondragoverDelegate;

		// Token: 0x04000BFF RID: 3071
		public HTMLTextContainerEvents_ondragenterEventHandler m_ondragenterDelegate;

		// Token: 0x04000C00 RID: 3072
		public HTMLTextContainerEvents_ondragendEventHandler m_ondragendDelegate;

		// Token: 0x04000C01 RID: 3073
		public HTMLTextContainerEvents_ondragEventHandler m_ondragDelegate;

		// Token: 0x04000C02 RID: 3074
		public HTMLTextContainerEvents_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x04000C03 RID: 3075
		public HTMLTextContainerEvents_onblurEventHandler m_onblurDelegate;

		// Token: 0x04000C04 RID: 3076
		public HTMLTextContainerEvents_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x04000C05 RID: 3077
		public HTMLTextContainerEvents_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x04000C06 RID: 3078
		public HTMLTextContainerEvents_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x04000C07 RID: 3079
		public HTMLTextContainerEvents_onlosecaptureEventHandler m_onlosecaptureDelegate;

		// Token: 0x04000C08 RID: 3080
		public HTMLTextContainerEvents_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x04000C09 RID: 3081
		public HTMLTextContainerEvents_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x04000C0A RID: 3082
		public HTMLTextContainerEvents_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x04000C0B RID: 3083
		public HTMLTextContainerEvents_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x04000C0C RID: 3084
		public HTMLTextContainerEvents_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x04000C0D RID: 3085
		public HTMLTextContainerEvents_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x04000C0E RID: 3086
		public HTMLTextContainerEvents_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x04000C0F RID: 3087
		public HTMLTextContainerEvents_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x04000C10 RID: 3088
		public HTMLTextContainerEvents_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x04000C11 RID: 3089
		public HTMLTextContainerEvents_onfilterchangeEventHandler m_onfilterchangeDelegate;

		// Token: 0x04000C12 RID: 3090
		public HTMLTextContainerEvents_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x04000C13 RID: 3091
		public HTMLTextContainerEvents_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x04000C14 RID: 3092
		public HTMLTextContainerEvents_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x04000C15 RID: 3093
		public HTMLTextContainerEvents_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x04000C16 RID: 3094
		public HTMLTextContainerEvents_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x04000C17 RID: 3095
		public HTMLTextContainerEvents_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x04000C18 RID: 3096
		public HTMLTextContainerEvents_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x04000C19 RID: 3097
		public HTMLTextContainerEvents_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x04000C1A RID: 3098
		public HTMLTextContainerEvents_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x04000C1B RID: 3099
		public HTMLTextContainerEvents_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x04000C1C RID: 3100
		public HTMLTextContainerEvents_onclickEventHandler m_onclickDelegate;

		// Token: 0x04000C1D RID: 3101
		public HTMLTextContainerEvents_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x04000C1E RID: 3102
		public int m_dwCookie;
	}
}
