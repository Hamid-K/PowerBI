using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DDC RID: 3548
	[ClassInterface(0)]
	internal sealed class HTMLFormElementEvents2_SinkHelper : HTMLFormElementEvents2
	{
		// Token: 0x06017E88 RID: 97928 RVA: 0x0005A800 File Offset: 0x00059800
		public override bool onreset(IHTMLEventObj A_1)
		{
			return this.m_onresetDelegate != null && this.m_onresetDelegate(A_1);
		}

		// Token: 0x06017E89 RID: 97929 RVA: 0x0005A830 File Offset: 0x00059830
		public override bool onsubmit(IHTMLEventObj A_1)
		{
			return this.m_onsubmitDelegate != null && this.m_onsubmitDelegate(A_1);
		}

		// Token: 0x06017E8A RID: 97930 RVA: 0x0005A860 File Offset: 0x00059860
		public override bool onmousewheel(IHTMLEventObj A_1)
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate(A_1);
		}

		// Token: 0x06017E8B RID: 97931 RVA: 0x0005A890 File Offset: 0x00059890
		public override void onresizeend(IHTMLEventObj A_1)
		{
			if (this.m_onresizeendDelegate != null)
			{
				this.m_onresizeendDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017E8C RID: 97932 RVA: 0x0005A8C0 File Offset: 0x000598C0
		public override bool onresizestart(IHTMLEventObj A_1)
		{
			return this.m_onresizestartDelegate != null && this.m_onresizestartDelegate(A_1);
		}

		// Token: 0x06017E8D RID: 97933 RVA: 0x0005A8F0 File Offset: 0x000598F0
		public override void onmoveend(IHTMLEventObj A_1)
		{
			if (this.m_onmoveendDelegate != null)
			{
				this.m_onmoveendDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017E8E RID: 97934 RVA: 0x0005A920 File Offset: 0x00059920
		public override bool onmovestart(IHTMLEventObj A_1)
		{
			return this.m_onmovestartDelegate != null && this.m_onmovestartDelegate(A_1);
		}

		// Token: 0x06017E8F RID: 97935 RVA: 0x0005A950 File Offset: 0x00059950
		public override bool oncontrolselect(IHTMLEventObj A_1)
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate(A_1);
		}

		// Token: 0x06017E90 RID: 97936 RVA: 0x0005A980 File Offset: 0x00059980
		public override void onmove(IHTMLEventObj A_1)
		{
			if (this.m_onmoveDelegate != null)
			{
				this.m_onmoveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017E91 RID: 97937 RVA: 0x0005A9B0 File Offset: 0x000599B0
		public override void onfocusout(IHTMLEventObj A_1)
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017E92 RID: 97938 RVA: 0x0005A9E0 File Offset: 0x000599E0
		public override void onfocusin(IHTMLEventObj A_1)
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017E93 RID: 97939 RVA: 0x0005AA10 File Offset: 0x00059A10
		public override bool onbeforeactivate(IHTMLEventObj A_1)
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate(A_1);
		}

		// Token: 0x06017E94 RID: 97940 RVA: 0x0005AA40 File Offset: 0x00059A40
		public override bool onbeforedeactivate(IHTMLEventObj A_1)
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate(A_1);
		}

		// Token: 0x06017E95 RID: 97941 RVA: 0x0005AA70 File Offset: 0x00059A70
		public override void ondeactivate(IHTMLEventObj A_1)
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017E96 RID: 97942 RVA: 0x0005AAA0 File Offset: 0x00059AA0
		public override void onactivate(IHTMLEventObj A_1)
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017E97 RID: 97943 RVA: 0x0005AAD0 File Offset: 0x00059AD0
		public override void onmouseleave(IHTMLEventObj A_1)
		{
			if (this.m_onmouseleaveDelegate != null)
			{
				this.m_onmouseleaveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017E98 RID: 97944 RVA: 0x0005AB00 File Offset: 0x00059B00
		public override void onmouseenter(IHTMLEventObj A_1)
		{
			if (this.m_onmouseenterDelegate != null)
			{
				this.m_onmouseenterDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017E99 RID: 97945 RVA: 0x0005AB30 File Offset: 0x00059B30
		public override void onpage(IHTMLEventObj A_1)
		{
			if (this.m_onpageDelegate != null)
			{
				this.m_onpageDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017E9A RID: 97946 RVA: 0x0005AB60 File Offset: 0x00059B60
		public override void onlayoutcomplete(IHTMLEventObj A_1)
		{
			if (this.m_onlayoutcompleteDelegate != null)
			{
				this.m_onlayoutcompleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017E9B RID: 97947 RVA: 0x0005AB90 File Offset: 0x00059B90
		public override void onreadystatechange(IHTMLEventObj A_1)
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017E9C RID: 97948 RVA: 0x0005ABC0 File Offset: 0x00059BC0
		public override void oncellchange(IHTMLEventObj A_1)
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017E9D RID: 97949 RVA: 0x0005ABF0 File Offset: 0x00059BF0
		public override void onrowsinserted(IHTMLEventObj A_1)
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017E9E RID: 97950 RVA: 0x0005AC20 File Offset: 0x00059C20
		public override void onrowsdelete(IHTMLEventObj A_1)
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017E9F RID: 97951 RVA: 0x0005AC50 File Offset: 0x00059C50
		public override bool oncontextmenu(IHTMLEventObj A_1)
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate(A_1);
		}

		// Token: 0x06017EA0 RID: 97952 RVA: 0x0005AC80 File Offset: 0x00059C80
		public override bool onpaste(IHTMLEventObj A_1)
		{
			return this.m_onpasteDelegate != null && this.m_onpasteDelegate(A_1);
		}

		// Token: 0x06017EA1 RID: 97953 RVA: 0x0005ACB0 File Offset: 0x00059CB0
		public override bool onbeforepaste(IHTMLEventObj A_1)
		{
			return this.m_onbeforepasteDelegate != null && this.m_onbeforepasteDelegate(A_1);
		}

		// Token: 0x06017EA2 RID: 97954 RVA: 0x0005ACE0 File Offset: 0x00059CE0
		public override bool oncopy(IHTMLEventObj A_1)
		{
			return this.m_oncopyDelegate != null && this.m_oncopyDelegate(A_1);
		}

		// Token: 0x06017EA3 RID: 97955 RVA: 0x0005AD10 File Offset: 0x00059D10
		public override bool onbeforecopy(IHTMLEventObj A_1)
		{
			return this.m_onbeforecopyDelegate != null && this.m_onbeforecopyDelegate(A_1);
		}

		// Token: 0x06017EA4 RID: 97956 RVA: 0x0005AD40 File Offset: 0x00059D40
		public override bool oncut(IHTMLEventObj A_1)
		{
			return this.m_oncutDelegate != null && this.m_oncutDelegate(A_1);
		}

		// Token: 0x06017EA5 RID: 97957 RVA: 0x0005AD70 File Offset: 0x00059D70
		public override bool onbeforecut(IHTMLEventObj A_1)
		{
			return this.m_onbeforecutDelegate != null && this.m_onbeforecutDelegate(A_1);
		}

		// Token: 0x06017EA6 RID: 97958 RVA: 0x0005ADA0 File Offset: 0x00059DA0
		public override bool ondrop(IHTMLEventObj A_1)
		{
			return this.m_ondropDelegate != null && this.m_ondropDelegate(A_1);
		}

		// Token: 0x06017EA7 RID: 97959 RVA: 0x0005ADD0 File Offset: 0x00059DD0
		public override void ondragleave(IHTMLEventObj A_1)
		{
			if (this.m_ondragleaveDelegate != null)
			{
				this.m_ondragleaveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017EA8 RID: 97960 RVA: 0x0005AE00 File Offset: 0x00059E00
		public override bool ondragover(IHTMLEventObj A_1)
		{
			return this.m_ondragoverDelegate != null && this.m_ondragoverDelegate(A_1);
		}

		// Token: 0x06017EA9 RID: 97961 RVA: 0x0005AE30 File Offset: 0x00059E30
		public override bool ondragenter(IHTMLEventObj A_1)
		{
			return this.m_ondragenterDelegate != null && this.m_ondragenterDelegate(A_1);
		}

		// Token: 0x06017EAA RID: 97962 RVA: 0x0005AE60 File Offset: 0x00059E60
		public override void ondragend(IHTMLEventObj A_1)
		{
			if (this.m_ondragendDelegate != null)
			{
				this.m_ondragendDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017EAB RID: 97963 RVA: 0x0005AE90 File Offset: 0x00059E90
		public override bool ondrag(IHTMLEventObj A_1)
		{
			return this.m_ondragDelegate != null && this.m_ondragDelegate(A_1);
		}

		// Token: 0x06017EAC RID: 97964 RVA: 0x0005AEC0 File Offset: 0x00059EC0
		public override void onresize(IHTMLEventObj A_1)
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017EAD RID: 97965 RVA: 0x0005AEF0 File Offset: 0x00059EF0
		public override void onblur(IHTMLEventObj A_1)
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017EAE RID: 97966 RVA: 0x0005AF20 File Offset: 0x00059F20
		public override void onfocus(IHTMLEventObj A_1)
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017EAF RID: 97967 RVA: 0x0005AF50 File Offset: 0x00059F50
		public override void onscroll(IHTMLEventObj A_1)
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017EB0 RID: 97968 RVA: 0x0005AF80 File Offset: 0x00059F80
		public override void onpropertychange(IHTMLEventObj A_1)
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017EB1 RID: 97969 RVA: 0x0005AFB0 File Offset: 0x00059FB0
		public override void onlosecapture(IHTMLEventObj A_1)
		{
			if (this.m_onlosecaptureDelegate != null)
			{
				this.m_onlosecaptureDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017EB2 RID: 97970 RVA: 0x0005AFE0 File Offset: 0x00059FE0
		public override void ondatasetcomplete(IHTMLEventObj A_1)
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017EB3 RID: 97971 RVA: 0x0005B010 File Offset: 0x0005A010
		public override void ondataavailable(IHTMLEventObj A_1)
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017EB4 RID: 97972 RVA: 0x0005B040 File Offset: 0x0005A040
		public override void ondatasetchanged(IHTMLEventObj A_1)
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017EB5 RID: 97973 RVA: 0x0005B070 File Offset: 0x0005A070
		public override void onrowenter(IHTMLEventObj A_1)
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017EB6 RID: 97974 RVA: 0x0005B0A0 File Offset: 0x0005A0A0
		public override bool onrowexit(IHTMLEventObj A_1)
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate(A_1);
		}

		// Token: 0x06017EB7 RID: 97975 RVA: 0x0005B0D0 File Offset: 0x0005A0D0
		public override bool onerrorupdate(IHTMLEventObj A_1)
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate(A_1);
		}

		// Token: 0x06017EB8 RID: 97976 RVA: 0x0005B100 File Offset: 0x0005A100
		public override void onafterupdate(IHTMLEventObj A_1)
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017EB9 RID: 97977 RVA: 0x0005B130 File Offset: 0x0005A130
		public override bool onbeforeupdate(IHTMLEventObj A_1)
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate(A_1);
		}

		// Token: 0x06017EBA RID: 97978 RVA: 0x0005B160 File Offset: 0x0005A160
		public override bool ondragstart(IHTMLEventObj A_1)
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate(A_1);
		}

		// Token: 0x06017EBB RID: 97979 RVA: 0x0005B190 File Offset: 0x0005A190
		public override void onfilterchange(IHTMLEventObj A_1)
		{
			if (this.m_onfilterchangeDelegate != null)
			{
				this.m_onfilterchangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017EBC RID: 97980 RVA: 0x0005B1C0 File Offset: 0x0005A1C0
		public override bool onselectstart(IHTMLEventObj A_1)
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate(A_1);
		}

		// Token: 0x06017EBD RID: 97981 RVA: 0x0005B1F0 File Offset: 0x0005A1F0
		public override void onmouseup(IHTMLEventObj A_1)
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017EBE RID: 97982 RVA: 0x0005B220 File Offset: 0x0005A220
		public override void onmousedown(IHTMLEventObj A_1)
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017EBF RID: 97983 RVA: 0x0005B250 File Offset: 0x0005A250
		public override void onmousemove(IHTMLEventObj A_1)
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017EC0 RID: 97984 RVA: 0x0005B280 File Offset: 0x0005A280
		public override void onmouseover(IHTMLEventObj A_1)
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017EC1 RID: 97985 RVA: 0x0005B2B0 File Offset: 0x0005A2B0
		public override void onmouseout(IHTMLEventObj A_1)
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017EC2 RID: 97986 RVA: 0x0005B2E0 File Offset: 0x0005A2E0
		public override void onkeyup(IHTMLEventObj A_1)
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017EC3 RID: 97987 RVA: 0x0005B310 File Offset: 0x0005A310
		public override void onkeydown(IHTMLEventObj A_1)
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017EC4 RID: 97988 RVA: 0x0005B340 File Offset: 0x0005A340
		public override bool onkeypress(IHTMLEventObj A_1)
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate(A_1);
		}

		// Token: 0x06017EC5 RID: 97989 RVA: 0x0005B370 File Offset: 0x0005A370
		public override bool ondblclick(IHTMLEventObj A_1)
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate(A_1);
		}

		// Token: 0x06017EC6 RID: 97990 RVA: 0x0005B3A0 File Offset: 0x0005A3A0
		public override bool onclick(IHTMLEventObj A_1)
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate(A_1);
		}

		// Token: 0x06017EC7 RID: 97991 RVA: 0x0005B3D0 File Offset: 0x0005A3D0
		public override bool onhelp(IHTMLEventObj A_1)
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate(A_1);
		}

		// Token: 0x06017EC8 RID: 97992 RVA: 0x0005B400 File Offset: 0x0005A400
		internal HTMLFormElementEvents2_SinkHelper()
		{
			this.m_dwCookie = 0;
			this.m_onresetDelegate = null;
			this.m_onsubmitDelegate = null;
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

		// Token: 0x0400081B RID: 2075
		public HTMLFormElementEvents2_onresetEventHandler m_onresetDelegate;

		// Token: 0x0400081C RID: 2076
		public HTMLFormElementEvents2_onsubmitEventHandler m_onsubmitDelegate;

		// Token: 0x0400081D RID: 2077
		public HTMLFormElementEvents2_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x0400081E RID: 2078
		public HTMLFormElementEvents2_onresizeendEventHandler m_onresizeendDelegate;

		// Token: 0x0400081F RID: 2079
		public HTMLFormElementEvents2_onresizestartEventHandler m_onresizestartDelegate;

		// Token: 0x04000820 RID: 2080
		public HTMLFormElementEvents2_onmoveendEventHandler m_onmoveendDelegate;

		// Token: 0x04000821 RID: 2081
		public HTMLFormElementEvents2_onmovestartEventHandler m_onmovestartDelegate;

		// Token: 0x04000822 RID: 2082
		public HTMLFormElementEvents2_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x04000823 RID: 2083
		public HTMLFormElementEvents2_onmoveEventHandler m_onmoveDelegate;

		// Token: 0x04000824 RID: 2084
		public HTMLFormElementEvents2_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x04000825 RID: 2085
		public HTMLFormElementEvents2_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x04000826 RID: 2086
		public HTMLFormElementEvents2_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x04000827 RID: 2087
		public HTMLFormElementEvents2_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x04000828 RID: 2088
		public HTMLFormElementEvents2_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x04000829 RID: 2089
		public HTMLFormElementEvents2_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x0400082A RID: 2090
		public HTMLFormElementEvents2_onmouseleaveEventHandler m_onmouseleaveDelegate;

		// Token: 0x0400082B RID: 2091
		public HTMLFormElementEvents2_onmouseenterEventHandler m_onmouseenterDelegate;

		// Token: 0x0400082C RID: 2092
		public HTMLFormElementEvents2_onpageEventHandler m_onpageDelegate;

		// Token: 0x0400082D RID: 2093
		public HTMLFormElementEvents2_onlayoutcompleteEventHandler m_onlayoutcompleteDelegate;

		// Token: 0x0400082E RID: 2094
		public HTMLFormElementEvents2_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x0400082F RID: 2095
		public HTMLFormElementEvents2_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x04000830 RID: 2096
		public HTMLFormElementEvents2_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x04000831 RID: 2097
		public HTMLFormElementEvents2_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x04000832 RID: 2098
		public HTMLFormElementEvents2_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x04000833 RID: 2099
		public HTMLFormElementEvents2_onpasteEventHandler m_onpasteDelegate;

		// Token: 0x04000834 RID: 2100
		public HTMLFormElementEvents2_onbeforepasteEventHandler m_onbeforepasteDelegate;

		// Token: 0x04000835 RID: 2101
		public HTMLFormElementEvents2_oncopyEventHandler m_oncopyDelegate;

		// Token: 0x04000836 RID: 2102
		public HTMLFormElementEvents2_onbeforecopyEventHandler m_onbeforecopyDelegate;

		// Token: 0x04000837 RID: 2103
		public HTMLFormElementEvents2_oncutEventHandler m_oncutDelegate;

		// Token: 0x04000838 RID: 2104
		public HTMLFormElementEvents2_onbeforecutEventHandler m_onbeforecutDelegate;

		// Token: 0x04000839 RID: 2105
		public HTMLFormElementEvents2_ondropEventHandler m_ondropDelegate;

		// Token: 0x0400083A RID: 2106
		public HTMLFormElementEvents2_ondragleaveEventHandler m_ondragleaveDelegate;

		// Token: 0x0400083B RID: 2107
		public HTMLFormElementEvents2_ondragoverEventHandler m_ondragoverDelegate;

		// Token: 0x0400083C RID: 2108
		public HTMLFormElementEvents2_ondragenterEventHandler m_ondragenterDelegate;

		// Token: 0x0400083D RID: 2109
		public HTMLFormElementEvents2_ondragendEventHandler m_ondragendDelegate;

		// Token: 0x0400083E RID: 2110
		public HTMLFormElementEvents2_ondragEventHandler m_ondragDelegate;

		// Token: 0x0400083F RID: 2111
		public HTMLFormElementEvents2_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x04000840 RID: 2112
		public HTMLFormElementEvents2_onblurEventHandler m_onblurDelegate;

		// Token: 0x04000841 RID: 2113
		public HTMLFormElementEvents2_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x04000842 RID: 2114
		public HTMLFormElementEvents2_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x04000843 RID: 2115
		public HTMLFormElementEvents2_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x04000844 RID: 2116
		public HTMLFormElementEvents2_onlosecaptureEventHandler m_onlosecaptureDelegate;

		// Token: 0x04000845 RID: 2117
		public HTMLFormElementEvents2_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x04000846 RID: 2118
		public HTMLFormElementEvents2_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x04000847 RID: 2119
		public HTMLFormElementEvents2_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x04000848 RID: 2120
		public HTMLFormElementEvents2_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x04000849 RID: 2121
		public HTMLFormElementEvents2_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x0400084A RID: 2122
		public HTMLFormElementEvents2_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x0400084B RID: 2123
		public HTMLFormElementEvents2_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x0400084C RID: 2124
		public HTMLFormElementEvents2_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x0400084D RID: 2125
		public HTMLFormElementEvents2_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x0400084E RID: 2126
		public HTMLFormElementEvents2_onfilterchangeEventHandler m_onfilterchangeDelegate;

		// Token: 0x0400084F RID: 2127
		public HTMLFormElementEvents2_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x04000850 RID: 2128
		public HTMLFormElementEvents2_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x04000851 RID: 2129
		public HTMLFormElementEvents2_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x04000852 RID: 2130
		public HTMLFormElementEvents2_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x04000853 RID: 2131
		public HTMLFormElementEvents2_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x04000854 RID: 2132
		public HTMLFormElementEvents2_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x04000855 RID: 2133
		public HTMLFormElementEvents2_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x04000856 RID: 2134
		public HTMLFormElementEvents2_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x04000857 RID: 2135
		public HTMLFormElementEvents2_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x04000858 RID: 2136
		public HTMLFormElementEvents2_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x04000859 RID: 2137
		public HTMLFormElementEvents2_onclickEventHandler m_onclickDelegate;

		// Token: 0x0400085A RID: 2138
		public HTMLFormElementEvents2_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x0400085B RID: 2139
		public int m_dwCookie;
	}
}
