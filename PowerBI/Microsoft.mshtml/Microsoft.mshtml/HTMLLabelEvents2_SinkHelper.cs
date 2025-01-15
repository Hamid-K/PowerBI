using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DDE RID: 3550
	[ClassInterface(0)]
	internal sealed class HTMLLabelEvents2_SinkHelper : HTMLLabelEvents2
	{
		// Token: 0x06017F4D RID: 98125 RVA: 0x000618B4 File Offset: 0x000608B4
		public override bool onmousewheel(IHTMLEventObj A_1)
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate(A_1);
		}

		// Token: 0x06017F4E RID: 98126 RVA: 0x000618E4 File Offset: 0x000608E4
		public override void onresizeend(IHTMLEventObj A_1)
		{
			if (this.m_onresizeendDelegate != null)
			{
				this.m_onresizeendDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017F4F RID: 98127 RVA: 0x00061914 File Offset: 0x00060914
		public override bool onresizestart(IHTMLEventObj A_1)
		{
			return this.m_onresizestartDelegate != null && this.m_onresizestartDelegate(A_1);
		}

		// Token: 0x06017F50 RID: 98128 RVA: 0x00061944 File Offset: 0x00060944
		public override void onmoveend(IHTMLEventObj A_1)
		{
			if (this.m_onmoveendDelegate != null)
			{
				this.m_onmoveendDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017F51 RID: 98129 RVA: 0x00061974 File Offset: 0x00060974
		public override bool onmovestart(IHTMLEventObj A_1)
		{
			return this.m_onmovestartDelegate != null && this.m_onmovestartDelegate(A_1);
		}

		// Token: 0x06017F52 RID: 98130 RVA: 0x000619A4 File Offset: 0x000609A4
		public override bool oncontrolselect(IHTMLEventObj A_1)
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate(A_1);
		}

		// Token: 0x06017F53 RID: 98131 RVA: 0x000619D4 File Offset: 0x000609D4
		public override void onmove(IHTMLEventObj A_1)
		{
			if (this.m_onmoveDelegate != null)
			{
				this.m_onmoveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017F54 RID: 98132 RVA: 0x00061A04 File Offset: 0x00060A04
		public override void onfocusout(IHTMLEventObj A_1)
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017F55 RID: 98133 RVA: 0x00061A34 File Offset: 0x00060A34
		public override void onfocusin(IHTMLEventObj A_1)
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017F56 RID: 98134 RVA: 0x00061A64 File Offset: 0x00060A64
		public override bool onbeforeactivate(IHTMLEventObj A_1)
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate(A_1);
		}

		// Token: 0x06017F57 RID: 98135 RVA: 0x00061A94 File Offset: 0x00060A94
		public override bool onbeforedeactivate(IHTMLEventObj A_1)
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate(A_1);
		}

		// Token: 0x06017F58 RID: 98136 RVA: 0x00061AC4 File Offset: 0x00060AC4
		public override void ondeactivate(IHTMLEventObj A_1)
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017F59 RID: 98137 RVA: 0x00061AF4 File Offset: 0x00060AF4
		public override void onactivate(IHTMLEventObj A_1)
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017F5A RID: 98138 RVA: 0x00061B24 File Offset: 0x00060B24
		public override void onmouseleave(IHTMLEventObj A_1)
		{
			if (this.m_onmouseleaveDelegate != null)
			{
				this.m_onmouseleaveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017F5B RID: 98139 RVA: 0x00061B54 File Offset: 0x00060B54
		public override void onmouseenter(IHTMLEventObj A_1)
		{
			if (this.m_onmouseenterDelegate != null)
			{
				this.m_onmouseenterDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017F5C RID: 98140 RVA: 0x00061B84 File Offset: 0x00060B84
		public override void onpage(IHTMLEventObj A_1)
		{
			if (this.m_onpageDelegate != null)
			{
				this.m_onpageDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017F5D RID: 98141 RVA: 0x00061BB4 File Offset: 0x00060BB4
		public override void onlayoutcomplete(IHTMLEventObj A_1)
		{
			if (this.m_onlayoutcompleteDelegate != null)
			{
				this.m_onlayoutcompleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017F5E RID: 98142 RVA: 0x00061BE4 File Offset: 0x00060BE4
		public override void onreadystatechange(IHTMLEventObj A_1)
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017F5F RID: 98143 RVA: 0x00061C14 File Offset: 0x00060C14
		public override void oncellchange(IHTMLEventObj A_1)
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017F60 RID: 98144 RVA: 0x00061C44 File Offset: 0x00060C44
		public override void onrowsinserted(IHTMLEventObj A_1)
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017F61 RID: 98145 RVA: 0x00061C74 File Offset: 0x00060C74
		public override void onrowsdelete(IHTMLEventObj A_1)
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017F62 RID: 98146 RVA: 0x00061CA4 File Offset: 0x00060CA4
		public override bool oncontextmenu(IHTMLEventObj A_1)
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate(A_1);
		}

		// Token: 0x06017F63 RID: 98147 RVA: 0x00061CD4 File Offset: 0x00060CD4
		public override bool onpaste(IHTMLEventObj A_1)
		{
			return this.m_onpasteDelegate != null && this.m_onpasteDelegate(A_1);
		}

		// Token: 0x06017F64 RID: 98148 RVA: 0x00061D04 File Offset: 0x00060D04
		public override bool onbeforepaste(IHTMLEventObj A_1)
		{
			return this.m_onbeforepasteDelegate != null && this.m_onbeforepasteDelegate(A_1);
		}

		// Token: 0x06017F65 RID: 98149 RVA: 0x00061D34 File Offset: 0x00060D34
		public override bool oncopy(IHTMLEventObj A_1)
		{
			return this.m_oncopyDelegate != null && this.m_oncopyDelegate(A_1);
		}

		// Token: 0x06017F66 RID: 98150 RVA: 0x00061D64 File Offset: 0x00060D64
		public override bool onbeforecopy(IHTMLEventObj A_1)
		{
			return this.m_onbeforecopyDelegate != null && this.m_onbeforecopyDelegate(A_1);
		}

		// Token: 0x06017F67 RID: 98151 RVA: 0x00061D94 File Offset: 0x00060D94
		public override bool oncut(IHTMLEventObj A_1)
		{
			return this.m_oncutDelegate != null && this.m_oncutDelegate(A_1);
		}

		// Token: 0x06017F68 RID: 98152 RVA: 0x00061DC4 File Offset: 0x00060DC4
		public override bool onbeforecut(IHTMLEventObj A_1)
		{
			return this.m_onbeforecutDelegate != null && this.m_onbeforecutDelegate(A_1);
		}

		// Token: 0x06017F69 RID: 98153 RVA: 0x00061DF4 File Offset: 0x00060DF4
		public override bool ondrop(IHTMLEventObj A_1)
		{
			return this.m_ondropDelegate != null && this.m_ondropDelegate(A_1);
		}

		// Token: 0x06017F6A RID: 98154 RVA: 0x00061E24 File Offset: 0x00060E24
		public override void ondragleave(IHTMLEventObj A_1)
		{
			if (this.m_ondragleaveDelegate != null)
			{
				this.m_ondragleaveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017F6B RID: 98155 RVA: 0x00061E54 File Offset: 0x00060E54
		public override bool ondragover(IHTMLEventObj A_1)
		{
			return this.m_ondragoverDelegate != null && this.m_ondragoverDelegate(A_1);
		}

		// Token: 0x06017F6C RID: 98156 RVA: 0x00061E84 File Offset: 0x00060E84
		public override bool ondragenter(IHTMLEventObj A_1)
		{
			return this.m_ondragenterDelegate != null && this.m_ondragenterDelegate(A_1);
		}

		// Token: 0x06017F6D RID: 98157 RVA: 0x00061EB4 File Offset: 0x00060EB4
		public override void ondragend(IHTMLEventObj A_1)
		{
			if (this.m_ondragendDelegate != null)
			{
				this.m_ondragendDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017F6E RID: 98158 RVA: 0x00061EE4 File Offset: 0x00060EE4
		public override bool ondrag(IHTMLEventObj A_1)
		{
			return this.m_ondragDelegate != null && this.m_ondragDelegate(A_1);
		}

		// Token: 0x06017F6F RID: 98159 RVA: 0x00061F14 File Offset: 0x00060F14
		public override void onresize(IHTMLEventObj A_1)
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017F70 RID: 98160 RVA: 0x00061F44 File Offset: 0x00060F44
		public override void onblur(IHTMLEventObj A_1)
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017F71 RID: 98161 RVA: 0x00061F74 File Offset: 0x00060F74
		public override void onfocus(IHTMLEventObj A_1)
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017F72 RID: 98162 RVA: 0x00061FA4 File Offset: 0x00060FA4
		public override void onscroll(IHTMLEventObj A_1)
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017F73 RID: 98163 RVA: 0x00061FD4 File Offset: 0x00060FD4
		public override void onpropertychange(IHTMLEventObj A_1)
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017F74 RID: 98164 RVA: 0x00062004 File Offset: 0x00061004
		public override void onlosecapture(IHTMLEventObj A_1)
		{
			if (this.m_onlosecaptureDelegate != null)
			{
				this.m_onlosecaptureDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017F75 RID: 98165 RVA: 0x00062034 File Offset: 0x00061034
		public override void ondatasetcomplete(IHTMLEventObj A_1)
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017F76 RID: 98166 RVA: 0x00062064 File Offset: 0x00061064
		public override void ondataavailable(IHTMLEventObj A_1)
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017F77 RID: 98167 RVA: 0x00062094 File Offset: 0x00061094
		public override void ondatasetchanged(IHTMLEventObj A_1)
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017F78 RID: 98168 RVA: 0x000620C4 File Offset: 0x000610C4
		public override void onrowenter(IHTMLEventObj A_1)
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017F79 RID: 98169 RVA: 0x000620F4 File Offset: 0x000610F4
		public override bool onrowexit(IHTMLEventObj A_1)
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate(A_1);
		}

		// Token: 0x06017F7A RID: 98170 RVA: 0x00062124 File Offset: 0x00061124
		public override bool onerrorupdate(IHTMLEventObj A_1)
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate(A_1);
		}

		// Token: 0x06017F7B RID: 98171 RVA: 0x00062154 File Offset: 0x00061154
		public override void onafterupdate(IHTMLEventObj A_1)
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017F7C RID: 98172 RVA: 0x00062184 File Offset: 0x00061184
		public override bool onbeforeupdate(IHTMLEventObj A_1)
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate(A_1);
		}

		// Token: 0x06017F7D RID: 98173 RVA: 0x000621B4 File Offset: 0x000611B4
		public override bool ondragstart(IHTMLEventObj A_1)
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate(A_1);
		}

		// Token: 0x06017F7E RID: 98174 RVA: 0x000621E4 File Offset: 0x000611E4
		public override void onfilterchange(IHTMLEventObj A_1)
		{
			if (this.m_onfilterchangeDelegate != null)
			{
				this.m_onfilterchangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017F7F RID: 98175 RVA: 0x00062214 File Offset: 0x00061214
		public override bool onselectstart(IHTMLEventObj A_1)
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate(A_1);
		}

		// Token: 0x06017F80 RID: 98176 RVA: 0x00062244 File Offset: 0x00061244
		public override void onmouseup(IHTMLEventObj A_1)
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017F81 RID: 98177 RVA: 0x00062274 File Offset: 0x00061274
		public override void onmousedown(IHTMLEventObj A_1)
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017F82 RID: 98178 RVA: 0x000622A4 File Offset: 0x000612A4
		public override void onmousemove(IHTMLEventObj A_1)
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017F83 RID: 98179 RVA: 0x000622D4 File Offset: 0x000612D4
		public override void onmouseover(IHTMLEventObj A_1)
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017F84 RID: 98180 RVA: 0x00062304 File Offset: 0x00061304
		public override void onmouseout(IHTMLEventObj A_1)
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017F85 RID: 98181 RVA: 0x00062334 File Offset: 0x00061334
		public override void onkeyup(IHTMLEventObj A_1)
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017F86 RID: 98182 RVA: 0x00062364 File Offset: 0x00061364
		public override void onkeydown(IHTMLEventObj A_1)
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017F87 RID: 98183 RVA: 0x00062394 File Offset: 0x00061394
		public override bool onkeypress(IHTMLEventObj A_1)
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate(A_1);
		}

		// Token: 0x06017F88 RID: 98184 RVA: 0x000623C4 File Offset: 0x000613C4
		public override bool ondblclick(IHTMLEventObj A_1)
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate(A_1);
		}

		// Token: 0x06017F89 RID: 98185 RVA: 0x000623F4 File Offset: 0x000613F4
		public override bool onclick(IHTMLEventObj A_1)
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate(A_1);
		}

		// Token: 0x06017F8A RID: 98186 RVA: 0x00062424 File Offset: 0x00061424
		public override bool onhelp(IHTMLEventObj A_1)
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate(A_1);
		}

		// Token: 0x06017F8B RID: 98187 RVA: 0x00062454 File Offset: 0x00061454
		internal HTMLLabelEvents2_SinkHelper()
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

		// Token: 0x0400085F RID: 2143
		public HTMLLabelEvents2_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x04000860 RID: 2144
		public HTMLLabelEvents2_onresizeendEventHandler m_onresizeendDelegate;

		// Token: 0x04000861 RID: 2145
		public HTMLLabelEvents2_onresizestartEventHandler m_onresizestartDelegate;

		// Token: 0x04000862 RID: 2146
		public HTMLLabelEvents2_onmoveendEventHandler m_onmoveendDelegate;

		// Token: 0x04000863 RID: 2147
		public HTMLLabelEvents2_onmovestartEventHandler m_onmovestartDelegate;

		// Token: 0x04000864 RID: 2148
		public HTMLLabelEvents2_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x04000865 RID: 2149
		public HTMLLabelEvents2_onmoveEventHandler m_onmoveDelegate;

		// Token: 0x04000866 RID: 2150
		public HTMLLabelEvents2_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x04000867 RID: 2151
		public HTMLLabelEvents2_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x04000868 RID: 2152
		public HTMLLabelEvents2_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x04000869 RID: 2153
		public HTMLLabelEvents2_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x0400086A RID: 2154
		public HTMLLabelEvents2_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x0400086B RID: 2155
		public HTMLLabelEvents2_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x0400086C RID: 2156
		public HTMLLabelEvents2_onmouseleaveEventHandler m_onmouseleaveDelegate;

		// Token: 0x0400086D RID: 2157
		public HTMLLabelEvents2_onmouseenterEventHandler m_onmouseenterDelegate;

		// Token: 0x0400086E RID: 2158
		public HTMLLabelEvents2_onpageEventHandler m_onpageDelegate;

		// Token: 0x0400086F RID: 2159
		public HTMLLabelEvents2_onlayoutcompleteEventHandler m_onlayoutcompleteDelegate;

		// Token: 0x04000870 RID: 2160
		public HTMLLabelEvents2_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x04000871 RID: 2161
		public HTMLLabelEvents2_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x04000872 RID: 2162
		public HTMLLabelEvents2_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x04000873 RID: 2163
		public HTMLLabelEvents2_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x04000874 RID: 2164
		public HTMLLabelEvents2_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x04000875 RID: 2165
		public HTMLLabelEvents2_onpasteEventHandler m_onpasteDelegate;

		// Token: 0x04000876 RID: 2166
		public HTMLLabelEvents2_onbeforepasteEventHandler m_onbeforepasteDelegate;

		// Token: 0x04000877 RID: 2167
		public HTMLLabelEvents2_oncopyEventHandler m_oncopyDelegate;

		// Token: 0x04000878 RID: 2168
		public HTMLLabelEvents2_onbeforecopyEventHandler m_onbeforecopyDelegate;

		// Token: 0x04000879 RID: 2169
		public HTMLLabelEvents2_oncutEventHandler m_oncutDelegate;

		// Token: 0x0400087A RID: 2170
		public HTMLLabelEvents2_onbeforecutEventHandler m_onbeforecutDelegate;

		// Token: 0x0400087B RID: 2171
		public HTMLLabelEvents2_ondropEventHandler m_ondropDelegate;

		// Token: 0x0400087C RID: 2172
		public HTMLLabelEvents2_ondragleaveEventHandler m_ondragleaveDelegate;

		// Token: 0x0400087D RID: 2173
		public HTMLLabelEvents2_ondragoverEventHandler m_ondragoverDelegate;

		// Token: 0x0400087E RID: 2174
		public HTMLLabelEvents2_ondragenterEventHandler m_ondragenterDelegate;

		// Token: 0x0400087F RID: 2175
		public HTMLLabelEvents2_ondragendEventHandler m_ondragendDelegate;

		// Token: 0x04000880 RID: 2176
		public HTMLLabelEvents2_ondragEventHandler m_ondragDelegate;

		// Token: 0x04000881 RID: 2177
		public HTMLLabelEvents2_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x04000882 RID: 2178
		public HTMLLabelEvents2_onblurEventHandler m_onblurDelegate;

		// Token: 0x04000883 RID: 2179
		public HTMLLabelEvents2_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x04000884 RID: 2180
		public HTMLLabelEvents2_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x04000885 RID: 2181
		public HTMLLabelEvents2_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x04000886 RID: 2182
		public HTMLLabelEvents2_onlosecaptureEventHandler m_onlosecaptureDelegate;

		// Token: 0x04000887 RID: 2183
		public HTMLLabelEvents2_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x04000888 RID: 2184
		public HTMLLabelEvents2_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x04000889 RID: 2185
		public HTMLLabelEvents2_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x0400088A RID: 2186
		public HTMLLabelEvents2_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x0400088B RID: 2187
		public HTMLLabelEvents2_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x0400088C RID: 2188
		public HTMLLabelEvents2_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x0400088D RID: 2189
		public HTMLLabelEvents2_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x0400088E RID: 2190
		public HTMLLabelEvents2_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x0400088F RID: 2191
		public HTMLLabelEvents2_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x04000890 RID: 2192
		public HTMLLabelEvents2_onfilterchangeEventHandler m_onfilterchangeDelegate;

		// Token: 0x04000891 RID: 2193
		public HTMLLabelEvents2_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x04000892 RID: 2194
		public HTMLLabelEvents2_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x04000893 RID: 2195
		public HTMLLabelEvents2_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x04000894 RID: 2196
		public HTMLLabelEvents2_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x04000895 RID: 2197
		public HTMLLabelEvents2_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x04000896 RID: 2198
		public HTMLLabelEvents2_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x04000897 RID: 2199
		public HTMLLabelEvents2_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x04000898 RID: 2200
		public HTMLLabelEvents2_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x04000899 RID: 2201
		public HTMLLabelEvents2_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x0400089A RID: 2202
		public HTMLLabelEvents2_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x0400089B RID: 2203
		public HTMLLabelEvents2_onclickEventHandler m_onclickDelegate;

		// Token: 0x0400089C RID: 2204
		public HTMLLabelEvents2_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x0400089D RID: 2205
		public int m_dwCookie;
	}
}
