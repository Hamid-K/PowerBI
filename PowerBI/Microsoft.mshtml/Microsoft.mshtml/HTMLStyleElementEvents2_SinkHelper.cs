using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DF6 RID: 3574
	[ClassInterface(0)]
	internal sealed class HTMLStyleElementEvents2_SinkHelper : HTMLStyleElementEvents2
	{
		// Token: 0x06018718 RID: 100120 RVA: 0x000A88C8 File Offset: 0x000A78C8
		public override void onerror(IHTMLEventObj A_1)
		{
			if (this.m_onerrorDelegate != null)
			{
				this.m_onerrorDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018719 RID: 100121 RVA: 0x000A88F8 File Offset: 0x000A78F8
		public override void onload(IHTMLEventObj A_1)
		{
			if (this.m_onloadDelegate != null)
			{
				this.m_onloadDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601871A RID: 100122 RVA: 0x000A8928 File Offset: 0x000A7928
		public override bool onmousewheel(IHTMLEventObj A_1)
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate(A_1);
		}

		// Token: 0x0601871B RID: 100123 RVA: 0x000A8958 File Offset: 0x000A7958
		public override void onresizeend(IHTMLEventObj A_1)
		{
			if (this.m_onresizeendDelegate != null)
			{
				this.m_onresizeendDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601871C RID: 100124 RVA: 0x000A8988 File Offset: 0x000A7988
		public override bool onresizestart(IHTMLEventObj A_1)
		{
			return this.m_onresizestartDelegate != null && this.m_onresizestartDelegate(A_1);
		}

		// Token: 0x0601871D RID: 100125 RVA: 0x000A89B8 File Offset: 0x000A79B8
		public override void onmoveend(IHTMLEventObj A_1)
		{
			if (this.m_onmoveendDelegate != null)
			{
				this.m_onmoveendDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601871E RID: 100126 RVA: 0x000A89E8 File Offset: 0x000A79E8
		public override bool onmovestart(IHTMLEventObj A_1)
		{
			return this.m_onmovestartDelegate != null && this.m_onmovestartDelegate(A_1);
		}

		// Token: 0x0601871F RID: 100127 RVA: 0x000A8A18 File Offset: 0x000A7A18
		public override bool oncontrolselect(IHTMLEventObj A_1)
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate(A_1);
		}

		// Token: 0x06018720 RID: 100128 RVA: 0x000A8A48 File Offset: 0x000A7A48
		public override void onmove(IHTMLEventObj A_1)
		{
			if (this.m_onmoveDelegate != null)
			{
				this.m_onmoveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018721 RID: 100129 RVA: 0x000A8A78 File Offset: 0x000A7A78
		public override void onfocusout(IHTMLEventObj A_1)
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018722 RID: 100130 RVA: 0x000A8AA8 File Offset: 0x000A7AA8
		public override void onfocusin(IHTMLEventObj A_1)
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018723 RID: 100131 RVA: 0x000A8AD8 File Offset: 0x000A7AD8
		public override bool onbeforeactivate(IHTMLEventObj A_1)
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate(A_1);
		}

		// Token: 0x06018724 RID: 100132 RVA: 0x000A8B08 File Offset: 0x000A7B08
		public override bool onbeforedeactivate(IHTMLEventObj A_1)
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate(A_1);
		}

		// Token: 0x06018725 RID: 100133 RVA: 0x000A8B38 File Offset: 0x000A7B38
		public override void ondeactivate(IHTMLEventObj A_1)
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018726 RID: 100134 RVA: 0x000A8B68 File Offset: 0x000A7B68
		public override void onactivate(IHTMLEventObj A_1)
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018727 RID: 100135 RVA: 0x000A8B98 File Offset: 0x000A7B98
		public override void onmouseleave(IHTMLEventObj A_1)
		{
			if (this.m_onmouseleaveDelegate != null)
			{
				this.m_onmouseleaveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018728 RID: 100136 RVA: 0x000A8BC8 File Offset: 0x000A7BC8
		public override void onmouseenter(IHTMLEventObj A_1)
		{
			if (this.m_onmouseenterDelegate != null)
			{
				this.m_onmouseenterDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018729 RID: 100137 RVA: 0x000A8BF8 File Offset: 0x000A7BF8
		public override void onpage(IHTMLEventObj A_1)
		{
			if (this.m_onpageDelegate != null)
			{
				this.m_onpageDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601872A RID: 100138 RVA: 0x000A8C28 File Offset: 0x000A7C28
		public override void onlayoutcomplete(IHTMLEventObj A_1)
		{
			if (this.m_onlayoutcompleteDelegate != null)
			{
				this.m_onlayoutcompleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601872B RID: 100139 RVA: 0x000A8C58 File Offset: 0x000A7C58
		public override void onreadystatechange(IHTMLEventObj A_1)
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601872C RID: 100140 RVA: 0x000A8C88 File Offset: 0x000A7C88
		public override void oncellchange(IHTMLEventObj A_1)
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601872D RID: 100141 RVA: 0x000A8CB8 File Offset: 0x000A7CB8
		public override void onrowsinserted(IHTMLEventObj A_1)
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601872E RID: 100142 RVA: 0x000A8CE8 File Offset: 0x000A7CE8
		public override void onrowsdelete(IHTMLEventObj A_1)
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601872F RID: 100143 RVA: 0x000A8D18 File Offset: 0x000A7D18
		public override bool oncontextmenu(IHTMLEventObj A_1)
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate(A_1);
		}

		// Token: 0x06018730 RID: 100144 RVA: 0x000A8D48 File Offset: 0x000A7D48
		public override bool onpaste(IHTMLEventObj A_1)
		{
			return this.m_onpasteDelegate != null && this.m_onpasteDelegate(A_1);
		}

		// Token: 0x06018731 RID: 100145 RVA: 0x000A8D78 File Offset: 0x000A7D78
		public override bool onbeforepaste(IHTMLEventObj A_1)
		{
			return this.m_onbeforepasteDelegate != null && this.m_onbeforepasteDelegate(A_1);
		}

		// Token: 0x06018732 RID: 100146 RVA: 0x000A8DA8 File Offset: 0x000A7DA8
		public override bool oncopy(IHTMLEventObj A_1)
		{
			return this.m_oncopyDelegate != null && this.m_oncopyDelegate(A_1);
		}

		// Token: 0x06018733 RID: 100147 RVA: 0x000A8DD8 File Offset: 0x000A7DD8
		public override bool onbeforecopy(IHTMLEventObj A_1)
		{
			return this.m_onbeforecopyDelegate != null && this.m_onbeforecopyDelegate(A_1);
		}

		// Token: 0x06018734 RID: 100148 RVA: 0x000A8E08 File Offset: 0x000A7E08
		public override bool oncut(IHTMLEventObj A_1)
		{
			return this.m_oncutDelegate != null && this.m_oncutDelegate(A_1);
		}

		// Token: 0x06018735 RID: 100149 RVA: 0x000A8E38 File Offset: 0x000A7E38
		public override bool onbeforecut(IHTMLEventObj A_1)
		{
			return this.m_onbeforecutDelegate != null && this.m_onbeforecutDelegate(A_1);
		}

		// Token: 0x06018736 RID: 100150 RVA: 0x000A8E68 File Offset: 0x000A7E68
		public override bool ondrop(IHTMLEventObj A_1)
		{
			return this.m_ondropDelegate != null && this.m_ondropDelegate(A_1);
		}

		// Token: 0x06018737 RID: 100151 RVA: 0x000A8E98 File Offset: 0x000A7E98
		public override void ondragleave(IHTMLEventObj A_1)
		{
			if (this.m_ondragleaveDelegate != null)
			{
				this.m_ondragleaveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018738 RID: 100152 RVA: 0x000A8EC8 File Offset: 0x000A7EC8
		public override bool ondragover(IHTMLEventObj A_1)
		{
			return this.m_ondragoverDelegate != null && this.m_ondragoverDelegate(A_1);
		}

		// Token: 0x06018739 RID: 100153 RVA: 0x000A8EF8 File Offset: 0x000A7EF8
		public override bool ondragenter(IHTMLEventObj A_1)
		{
			return this.m_ondragenterDelegate != null && this.m_ondragenterDelegate(A_1);
		}

		// Token: 0x0601873A RID: 100154 RVA: 0x000A8F28 File Offset: 0x000A7F28
		public override void ondragend(IHTMLEventObj A_1)
		{
			if (this.m_ondragendDelegate != null)
			{
				this.m_ondragendDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601873B RID: 100155 RVA: 0x000A8F58 File Offset: 0x000A7F58
		public override bool ondrag(IHTMLEventObj A_1)
		{
			return this.m_ondragDelegate != null && this.m_ondragDelegate(A_1);
		}

		// Token: 0x0601873C RID: 100156 RVA: 0x000A8F88 File Offset: 0x000A7F88
		public override void onresize(IHTMLEventObj A_1)
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601873D RID: 100157 RVA: 0x000A8FB8 File Offset: 0x000A7FB8
		public override void onblur(IHTMLEventObj A_1)
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601873E RID: 100158 RVA: 0x000A8FE8 File Offset: 0x000A7FE8
		public override void onfocus(IHTMLEventObj A_1)
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601873F RID: 100159 RVA: 0x000A9018 File Offset: 0x000A8018
		public override void onscroll(IHTMLEventObj A_1)
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018740 RID: 100160 RVA: 0x000A9048 File Offset: 0x000A8048
		public override void onpropertychange(IHTMLEventObj A_1)
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018741 RID: 100161 RVA: 0x000A9078 File Offset: 0x000A8078
		public override void onlosecapture(IHTMLEventObj A_1)
		{
			if (this.m_onlosecaptureDelegate != null)
			{
				this.m_onlosecaptureDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018742 RID: 100162 RVA: 0x000A90A8 File Offset: 0x000A80A8
		public override void ondatasetcomplete(IHTMLEventObj A_1)
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018743 RID: 100163 RVA: 0x000A90D8 File Offset: 0x000A80D8
		public override void ondataavailable(IHTMLEventObj A_1)
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018744 RID: 100164 RVA: 0x000A9108 File Offset: 0x000A8108
		public override void ondatasetchanged(IHTMLEventObj A_1)
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018745 RID: 100165 RVA: 0x000A9138 File Offset: 0x000A8138
		public override void onrowenter(IHTMLEventObj A_1)
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018746 RID: 100166 RVA: 0x000A9168 File Offset: 0x000A8168
		public override bool onrowexit(IHTMLEventObj A_1)
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate(A_1);
		}

		// Token: 0x06018747 RID: 100167 RVA: 0x000A9198 File Offset: 0x000A8198
		public override bool onerrorupdate(IHTMLEventObj A_1)
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate(A_1);
		}

		// Token: 0x06018748 RID: 100168 RVA: 0x000A91C8 File Offset: 0x000A81C8
		public override void onafterupdate(IHTMLEventObj A_1)
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018749 RID: 100169 RVA: 0x000A91F8 File Offset: 0x000A81F8
		public override bool onbeforeupdate(IHTMLEventObj A_1)
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate(A_1);
		}

		// Token: 0x0601874A RID: 100170 RVA: 0x000A9228 File Offset: 0x000A8228
		public override bool ondragstart(IHTMLEventObj A_1)
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate(A_1);
		}

		// Token: 0x0601874B RID: 100171 RVA: 0x000A9258 File Offset: 0x000A8258
		public override void onfilterchange(IHTMLEventObj A_1)
		{
			if (this.m_onfilterchangeDelegate != null)
			{
				this.m_onfilterchangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601874C RID: 100172 RVA: 0x000A9288 File Offset: 0x000A8288
		public override bool onselectstart(IHTMLEventObj A_1)
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate(A_1);
		}

		// Token: 0x0601874D RID: 100173 RVA: 0x000A92B8 File Offset: 0x000A82B8
		public override void onmouseup(IHTMLEventObj A_1)
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601874E RID: 100174 RVA: 0x000A92E8 File Offset: 0x000A82E8
		public override void onmousedown(IHTMLEventObj A_1)
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601874F RID: 100175 RVA: 0x000A9318 File Offset: 0x000A8318
		public override void onmousemove(IHTMLEventObj A_1)
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018750 RID: 100176 RVA: 0x000A9348 File Offset: 0x000A8348
		public override void onmouseover(IHTMLEventObj A_1)
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018751 RID: 100177 RVA: 0x000A9378 File Offset: 0x000A8378
		public override void onmouseout(IHTMLEventObj A_1)
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018752 RID: 100178 RVA: 0x000A93A8 File Offset: 0x000A83A8
		public override void onkeyup(IHTMLEventObj A_1)
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018753 RID: 100179 RVA: 0x000A93D8 File Offset: 0x000A83D8
		public override void onkeydown(IHTMLEventObj A_1)
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018754 RID: 100180 RVA: 0x000A9408 File Offset: 0x000A8408
		public override bool onkeypress(IHTMLEventObj A_1)
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate(A_1);
		}

		// Token: 0x06018755 RID: 100181 RVA: 0x000A9438 File Offset: 0x000A8438
		public override bool ondblclick(IHTMLEventObj A_1)
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate(A_1);
		}

		// Token: 0x06018756 RID: 100182 RVA: 0x000A9468 File Offset: 0x000A8468
		public override bool onclick(IHTMLEventObj A_1)
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate(A_1);
		}

		// Token: 0x06018757 RID: 100183 RVA: 0x000A9498 File Offset: 0x000A8498
		public override bool onhelp(IHTMLEventObj A_1)
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate(A_1);
		}

		// Token: 0x06018758 RID: 100184 RVA: 0x000A94C8 File Offset: 0x000A84C8
		internal HTMLStyleElementEvents2_SinkHelper()
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

		// Token: 0x04000B14 RID: 2836
		public HTMLStyleElementEvents2_onerrorEventHandler m_onerrorDelegate;

		// Token: 0x04000B15 RID: 2837
		public HTMLStyleElementEvents2_onloadEventHandler m_onloadDelegate;

		// Token: 0x04000B16 RID: 2838
		public HTMLStyleElementEvents2_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x04000B17 RID: 2839
		public HTMLStyleElementEvents2_onresizeendEventHandler m_onresizeendDelegate;

		// Token: 0x04000B18 RID: 2840
		public HTMLStyleElementEvents2_onresizestartEventHandler m_onresizestartDelegate;

		// Token: 0x04000B19 RID: 2841
		public HTMLStyleElementEvents2_onmoveendEventHandler m_onmoveendDelegate;

		// Token: 0x04000B1A RID: 2842
		public HTMLStyleElementEvents2_onmovestartEventHandler m_onmovestartDelegate;

		// Token: 0x04000B1B RID: 2843
		public HTMLStyleElementEvents2_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x04000B1C RID: 2844
		public HTMLStyleElementEvents2_onmoveEventHandler m_onmoveDelegate;

		// Token: 0x04000B1D RID: 2845
		public HTMLStyleElementEvents2_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x04000B1E RID: 2846
		public HTMLStyleElementEvents2_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x04000B1F RID: 2847
		public HTMLStyleElementEvents2_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x04000B20 RID: 2848
		public HTMLStyleElementEvents2_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x04000B21 RID: 2849
		public HTMLStyleElementEvents2_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x04000B22 RID: 2850
		public HTMLStyleElementEvents2_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x04000B23 RID: 2851
		public HTMLStyleElementEvents2_onmouseleaveEventHandler m_onmouseleaveDelegate;

		// Token: 0x04000B24 RID: 2852
		public HTMLStyleElementEvents2_onmouseenterEventHandler m_onmouseenterDelegate;

		// Token: 0x04000B25 RID: 2853
		public HTMLStyleElementEvents2_onpageEventHandler m_onpageDelegate;

		// Token: 0x04000B26 RID: 2854
		public HTMLStyleElementEvents2_onlayoutcompleteEventHandler m_onlayoutcompleteDelegate;

		// Token: 0x04000B27 RID: 2855
		public HTMLStyleElementEvents2_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x04000B28 RID: 2856
		public HTMLStyleElementEvents2_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x04000B29 RID: 2857
		public HTMLStyleElementEvents2_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x04000B2A RID: 2858
		public HTMLStyleElementEvents2_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x04000B2B RID: 2859
		public HTMLStyleElementEvents2_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x04000B2C RID: 2860
		public HTMLStyleElementEvents2_onpasteEventHandler m_onpasteDelegate;

		// Token: 0x04000B2D RID: 2861
		public HTMLStyleElementEvents2_onbeforepasteEventHandler m_onbeforepasteDelegate;

		// Token: 0x04000B2E RID: 2862
		public HTMLStyleElementEvents2_oncopyEventHandler m_oncopyDelegate;

		// Token: 0x04000B2F RID: 2863
		public HTMLStyleElementEvents2_onbeforecopyEventHandler m_onbeforecopyDelegate;

		// Token: 0x04000B30 RID: 2864
		public HTMLStyleElementEvents2_oncutEventHandler m_oncutDelegate;

		// Token: 0x04000B31 RID: 2865
		public HTMLStyleElementEvents2_onbeforecutEventHandler m_onbeforecutDelegate;

		// Token: 0x04000B32 RID: 2866
		public HTMLStyleElementEvents2_ondropEventHandler m_ondropDelegate;

		// Token: 0x04000B33 RID: 2867
		public HTMLStyleElementEvents2_ondragleaveEventHandler m_ondragleaveDelegate;

		// Token: 0x04000B34 RID: 2868
		public HTMLStyleElementEvents2_ondragoverEventHandler m_ondragoverDelegate;

		// Token: 0x04000B35 RID: 2869
		public HTMLStyleElementEvents2_ondragenterEventHandler m_ondragenterDelegate;

		// Token: 0x04000B36 RID: 2870
		public HTMLStyleElementEvents2_ondragendEventHandler m_ondragendDelegate;

		// Token: 0x04000B37 RID: 2871
		public HTMLStyleElementEvents2_ondragEventHandler m_ondragDelegate;

		// Token: 0x04000B38 RID: 2872
		public HTMLStyleElementEvents2_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x04000B39 RID: 2873
		public HTMLStyleElementEvents2_onblurEventHandler m_onblurDelegate;

		// Token: 0x04000B3A RID: 2874
		public HTMLStyleElementEvents2_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x04000B3B RID: 2875
		public HTMLStyleElementEvents2_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x04000B3C RID: 2876
		public HTMLStyleElementEvents2_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x04000B3D RID: 2877
		public HTMLStyleElementEvents2_onlosecaptureEventHandler m_onlosecaptureDelegate;

		// Token: 0x04000B3E RID: 2878
		public HTMLStyleElementEvents2_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x04000B3F RID: 2879
		public HTMLStyleElementEvents2_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x04000B40 RID: 2880
		public HTMLStyleElementEvents2_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x04000B41 RID: 2881
		public HTMLStyleElementEvents2_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x04000B42 RID: 2882
		public HTMLStyleElementEvents2_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x04000B43 RID: 2883
		public HTMLStyleElementEvents2_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x04000B44 RID: 2884
		public HTMLStyleElementEvents2_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x04000B45 RID: 2885
		public HTMLStyleElementEvents2_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x04000B46 RID: 2886
		public HTMLStyleElementEvents2_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x04000B47 RID: 2887
		public HTMLStyleElementEvents2_onfilterchangeEventHandler m_onfilterchangeDelegate;

		// Token: 0x04000B48 RID: 2888
		public HTMLStyleElementEvents2_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x04000B49 RID: 2889
		public HTMLStyleElementEvents2_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x04000B4A RID: 2890
		public HTMLStyleElementEvents2_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x04000B4B RID: 2891
		public HTMLStyleElementEvents2_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x04000B4C RID: 2892
		public HTMLStyleElementEvents2_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x04000B4D RID: 2893
		public HTMLStyleElementEvents2_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x04000B4E RID: 2894
		public HTMLStyleElementEvents2_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x04000B4F RID: 2895
		public HTMLStyleElementEvents2_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x04000B50 RID: 2896
		public HTMLStyleElementEvents2_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x04000B51 RID: 2897
		public HTMLStyleElementEvents2_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x04000B52 RID: 2898
		public HTMLStyleElementEvents2_onclickEventHandler m_onclickDelegate;

		// Token: 0x04000B53 RID: 2899
		public HTMLStyleElementEvents2_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x04000B54 RID: 2900
		public int m_dwCookie;
	}
}
