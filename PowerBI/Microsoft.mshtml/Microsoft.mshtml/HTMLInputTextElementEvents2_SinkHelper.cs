using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000E0A RID: 3594
	[ClassInterface(0)]
	internal sealed class HTMLInputTextElementEvents2_SinkHelper : HTMLInputTextElementEvents2
	{
		// Token: 0x06018ED9 RID: 102105 RVA: 0x000EF25C File Offset: 0x000EE25C
		public override void onabort(IHTMLEventObj A_1)
		{
			if (this.m_onabortDelegate != null)
			{
				this.m_onabortDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018EDA RID: 102106 RVA: 0x000EF28C File Offset: 0x000EE28C
		public override void onerror(IHTMLEventObj A_1)
		{
			if (this.m_onerrorDelegate != null)
			{
				this.m_onerrorDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018EDB RID: 102107 RVA: 0x000EF2BC File Offset: 0x000EE2BC
		public override void onload(IHTMLEventObj A_1)
		{
			if (this.m_onloadDelegate != null)
			{
				this.m_onloadDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018EDC RID: 102108 RVA: 0x000EF2EC File Offset: 0x000EE2EC
		public override void onselect(IHTMLEventObj A_1)
		{
			if (this.m_onselectDelegate != null)
			{
				this.m_onselectDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018EDD RID: 102109 RVA: 0x000EF31C File Offset: 0x000EE31C
		public override bool onchange(IHTMLEventObj A_1)
		{
			return this.m_onchangeDelegate != null && this.m_onchangeDelegate(A_1);
		}

		// Token: 0x06018EDE RID: 102110 RVA: 0x000EF34C File Offset: 0x000EE34C
		public override bool onmousewheel(IHTMLEventObj A_1)
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate(A_1);
		}

		// Token: 0x06018EDF RID: 102111 RVA: 0x000EF37C File Offset: 0x000EE37C
		public override void onresizeend(IHTMLEventObj A_1)
		{
			if (this.m_onresizeendDelegate != null)
			{
				this.m_onresizeendDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018EE0 RID: 102112 RVA: 0x000EF3AC File Offset: 0x000EE3AC
		public override bool onresizestart(IHTMLEventObj A_1)
		{
			return this.m_onresizestartDelegate != null && this.m_onresizestartDelegate(A_1);
		}

		// Token: 0x06018EE1 RID: 102113 RVA: 0x000EF3DC File Offset: 0x000EE3DC
		public override void onmoveend(IHTMLEventObj A_1)
		{
			if (this.m_onmoveendDelegate != null)
			{
				this.m_onmoveendDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018EE2 RID: 102114 RVA: 0x000EF40C File Offset: 0x000EE40C
		public override bool onmovestart(IHTMLEventObj A_1)
		{
			return this.m_onmovestartDelegate != null && this.m_onmovestartDelegate(A_1);
		}

		// Token: 0x06018EE3 RID: 102115 RVA: 0x000EF43C File Offset: 0x000EE43C
		public override bool oncontrolselect(IHTMLEventObj A_1)
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate(A_1);
		}

		// Token: 0x06018EE4 RID: 102116 RVA: 0x000EF46C File Offset: 0x000EE46C
		public override void onmove(IHTMLEventObj A_1)
		{
			if (this.m_onmoveDelegate != null)
			{
				this.m_onmoveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018EE5 RID: 102117 RVA: 0x000EF49C File Offset: 0x000EE49C
		public override void onfocusout(IHTMLEventObj A_1)
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018EE6 RID: 102118 RVA: 0x000EF4CC File Offset: 0x000EE4CC
		public override void onfocusin(IHTMLEventObj A_1)
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018EE7 RID: 102119 RVA: 0x000EF4FC File Offset: 0x000EE4FC
		public override bool onbeforeactivate(IHTMLEventObj A_1)
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate(A_1);
		}

		// Token: 0x06018EE8 RID: 102120 RVA: 0x000EF52C File Offset: 0x000EE52C
		public override bool onbeforedeactivate(IHTMLEventObj A_1)
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate(A_1);
		}

		// Token: 0x06018EE9 RID: 102121 RVA: 0x000EF55C File Offset: 0x000EE55C
		public override void ondeactivate(IHTMLEventObj A_1)
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018EEA RID: 102122 RVA: 0x000EF58C File Offset: 0x000EE58C
		public override void onactivate(IHTMLEventObj A_1)
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018EEB RID: 102123 RVA: 0x000EF5BC File Offset: 0x000EE5BC
		public override void onmouseleave(IHTMLEventObj A_1)
		{
			if (this.m_onmouseleaveDelegate != null)
			{
				this.m_onmouseleaveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018EEC RID: 102124 RVA: 0x000EF5EC File Offset: 0x000EE5EC
		public override void onmouseenter(IHTMLEventObj A_1)
		{
			if (this.m_onmouseenterDelegate != null)
			{
				this.m_onmouseenterDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018EED RID: 102125 RVA: 0x000EF61C File Offset: 0x000EE61C
		public override void onpage(IHTMLEventObj A_1)
		{
			if (this.m_onpageDelegate != null)
			{
				this.m_onpageDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018EEE RID: 102126 RVA: 0x000EF64C File Offset: 0x000EE64C
		public override void onlayoutcomplete(IHTMLEventObj A_1)
		{
			if (this.m_onlayoutcompleteDelegate != null)
			{
				this.m_onlayoutcompleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018EEF RID: 102127 RVA: 0x000EF67C File Offset: 0x000EE67C
		public override void onreadystatechange(IHTMLEventObj A_1)
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018EF0 RID: 102128 RVA: 0x000EF6AC File Offset: 0x000EE6AC
		public override void oncellchange(IHTMLEventObj A_1)
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018EF1 RID: 102129 RVA: 0x000EF6DC File Offset: 0x000EE6DC
		public override void onrowsinserted(IHTMLEventObj A_1)
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018EF2 RID: 102130 RVA: 0x000EF70C File Offset: 0x000EE70C
		public override void onrowsdelete(IHTMLEventObj A_1)
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018EF3 RID: 102131 RVA: 0x000EF73C File Offset: 0x000EE73C
		public override bool oncontextmenu(IHTMLEventObj A_1)
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate(A_1);
		}

		// Token: 0x06018EF4 RID: 102132 RVA: 0x000EF76C File Offset: 0x000EE76C
		public override bool onpaste(IHTMLEventObj A_1)
		{
			return this.m_onpasteDelegate != null && this.m_onpasteDelegate(A_1);
		}

		// Token: 0x06018EF5 RID: 102133 RVA: 0x000EF79C File Offset: 0x000EE79C
		public override bool onbeforepaste(IHTMLEventObj A_1)
		{
			return this.m_onbeforepasteDelegate != null && this.m_onbeforepasteDelegate(A_1);
		}

		// Token: 0x06018EF6 RID: 102134 RVA: 0x000EF7CC File Offset: 0x000EE7CC
		public override bool oncopy(IHTMLEventObj A_1)
		{
			return this.m_oncopyDelegate != null && this.m_oncopyDelegate(A_1);
		}

		// Token: 0x06018EF7 RID: 102135 RVA: 0x000EF7FC File Offset: 0x000EE7FC
		public override bool onbeforecopy(IHTMLEventObj A_1)
		{
			return this.m_onbeforecopyDelegate != null && this.m_onbeforecopyDelegate(A_1);
		}

		// Token: 0x06018EF8 RID: 102136 RVA: 0x000EF82C File Offset: 0x000EE82C
		public override bool oncut(IHTMLEventObj A_1)
		{
			return this.m_oncutDelegate != null && this.m_oncutDelegate(A_1);
		}

		// Token: 0x06018EF9 RID: 102137 RVA: 0x000EF85C File Offset: 0x000EE85C
		public override bool onbeforecut(IHTMLEventObj A_1)
		{
			return this.m_onbeforecutDelegate != null && this.m_onbeforecutDelegate(A_1);
		}

		// Token: 0x06018EFA RID: 102138 RVA: 0x000EF88C File Offset: 0x000EE88C
		public override bool ondrop(IHTMLEventObj A_1)
		{
			return this.m_ondropDelegate != null && this.m_ondropDelegate(A_1);
		}

		// Token: 0x06018EFB RID: 102139 RVA: 0x000EF8BC File Offset: 0x000EE8BC
		public override void ondragleave(IHTMLEventObj A_1)
		{
			if (this.m_ondragleaveDelegate != null)
			{
				this.m_ondragleaveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018EFC RID: 102140 RVA: 0x000EF8EC File Offset: 0x000EE8EC
		public override bool ondragover(IHTMLEventObj A_1)
		{
			return this.m_ondragoverDelegate != null && this.m_ondragoverDelegate(A_1);
		}

		// Token: 0x06018EFD RID: 102141 RVA: 0x000EF91C File Offset: 0x000EE91C
		public override bool ondragenter(IHTMLEventObj A_1)
		{
			return this.m_ondragenterDelegate != null && this.m_ondragenterDelegate(A_1);
		}

		// Token: 0x06018EFE RID: 102142 RVA: 0x000EF94C File Offset: 0x000EE94C
		public override void ondragend(IHTMLEventObj A_1)
		{
			if (this.m_ondragendDelegate != null)
			{
				this.m_ondragendDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018EFF RID: 102143 RVA: 0x000EF97C File Offset: 0x000EE97C
		public override bool ondrag(IHTMLEventObj A_1)
		{
			return this.m_ondragDelegate != null && this.m_ondragDelegate(A_1);
		}

		// Token: 0x06018F00 RID: 102144 RVA: 0x000EF9AC File Offset: 0x000EE9AC
		public override void onresize(IHTMLEventObj A_1)
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018F01 RID: 102145 RVA: 0x000EF9DC File Offset: 0x000EE9DC
		public override void onblur(IHTMLEventObj A_1)
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018F02 RID: 102146 RVA: 0x000EFA0C File Offset: 0x000EEA0C
		public override void onfocus(IHTMLEventObj A_1)
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018F03 RID: 102147 RVA: 0x000EFA3C File Offset: 0x000EEA3C
		public override void onscroll(IHTMLEventObj A_1)
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018F04 RID: 102148 RVA: 0x000EFA6C File Offset: 0x000EEA6C
		public override void onpropertychange(IHTMLEventObj A_1)
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018F05 RID: 102149 RVA: 0x000EFA9C File Offset: 0x000EEA9C
		public override void onlosecapture(IHTMLEventObj A_1)
		{
			if (this.m_onlosecaptureDelegate != null)
			{
				this.m_onlosecaptureDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018F06 RID: 102150 RVA: 0x000EFACC File Offset: 0x000EEACC
		public override void ondatasetcomplete(IHTMLEventObj A_1)
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018F07 RID: 102151 RVA: 0x000EFAFC File Offset: 0x000EEAFC
		public override void ondataavailable(IHTMLEventObj A_1)
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018F08 RID: 102152 RVA: 0x000EFB2C File Offset: 0x000EEB2C
		public override void ondatasetchanged(IHTMLEventObj A_1)
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018F09 RID: 102153 RVA: 0x000EFB5C File Offset: 0x000EEB5C
		public override void onrowenter(IHTMLEventObj A_1)
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018F0A RID: 102154 RVA: 0x000EFB8C File Offset: 0x000EEB8C
		public override bool onrowexit(IHTMLEventObj A_1)
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate(A_1);
		}

		// Token: 0x06018F0B RID: 102155 RVA: 0x000EFBBC File Offset: 0x000EEBBC
		public override bool onerrorupdate(IHTMLEventObj A_1)
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate(A_1);
		}

		// Token: 0x06018F0C RID: 102156 RVA: 0x000EFBEC File Offset: 0x000EEBEC
		public override void onafterupdate(IHTMLEventObj A_1)
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018F0D RID: 102157 RVA: 0x000EFC1C File Offset: 0x000EEC1C
		public override bool onbeforeupdate(IHTMLEventObj A_1)
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate(A_1);
		}

		// Token: 0x06018F0E RID: 102158 RVA: 0x000EFC4C File Offset: 0x000EEC4C
		public override bool ondragstart(IHTMLEventObj A_1)
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate(A_1);
		}

		// Token: 0x06018F0F RID: 102159 RVA: 0x000EFC7C File Offset: 0x000EEC7C
		public override void onfilterchange(IHTMLEventObj A_1)
		{
			if (this.m_onfilterchangeDelegate != null)
			{
				this.m_onfilterchangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018F10 RID: 102160 RVA: 0x000EFCAC File Offset: 0x000EECAC
		public override bool onselectstart(IHTMLEventObj A_1)
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate(A_1);
		}

		// Token: 0x06018F11 RID: 102161 RVA: 0x000EFCDC File Offset: 0x000EECDC
		public override void onmouseup(IHTMLEventObj A_1)
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018F12 RID: 102162 RVA: 0x000EFD0C File Offset: 0x000EED0C
		public override void onmousedown(IHTMLEventObj A_1)
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018F13 RID: 102163 RVA: 0x000EFD3C File Offset: 0x000EED3C
		public override void onmousemove(IHTMLEventObj A_1)
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018F14 RID: 102164 RVA: 0x000EFD6C File Offset: 0x000EED6C
		public override void onmouseover(IHTMLEventObj A_1)
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018F15 RID: 102165 RVA: 0x000EFD9C File Offset: 0x000EED9C
		public override void onmouseout(IHTMLEventObj A_1)
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018F16 RID: 102166 RVA: 0x000EFDCC File Offset: 0x000EEDCC
		public override void onkeyup(IHTMLEventObj A_1)
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018F17 RID: 102167 RVA: 0x000EFDFC File Offset: 0x000EEDFC
		public override void onkeydown(IHTMLEventObj A_1)
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018F18 RID: 102168 RVA: 0x000EFE2C File Offset: 0x000EEE2C
		public override bool onkeypress(IHTMLEventObj A_1)
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate(A_1);
		}

		// Token: 0x06018F19 RID: 102169 RVA: 0x000EFE5C File Offset: 0x000EEE5C
		public override bool ondblclick(IHTMLEventObj A_1)
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate(A_1);
		}

		// Token: 0x06018F1A RID: 102170 RVA: 0x000EFE8C File Offset: 0x000EEE8C
		public override bool onclick(IHTMLEventObj A_1)
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate(A_1);
		}

		// Token: 0x06018F1B RID: 102171 RVA: 0x000EFEBC File Offset: 0x000EEEBC
		public override bool onhelp(IHTMLEventObj A_1)
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate(A_1);
		}

		// Token: 0x06018F1C RID: 102172 RVA: 0x000EFEEC File Offset: 0x000EEEEC
		internal HTMLInputTextElementEvents2_SinkHelper()
		{
			this.m_dwCookie = 0;
			this.m_onabortDelegate = null;
			this.m_onerrorDelegate = null;
			this.m_onloadDelegate = null;
			this.m_onselectDelegate = null;
			this.m_onchangeDelegate = null;
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

		// Token: 0x04000DC1 RID: 3521
		public HTMLInputTextElementEvents2_onabortEventHandler m_onabortDelegate;

		// Token: 0x04000DC2 RID: 3522
		public HTMLInputTextElementEvents2_onerrorEventHandler m_onerrorDelegate;

		// Token: 0x04000DC3 RID: 3523
		public HTMLInputTextElementEvents2_onloadEventHandler m_onloadDelegate;

		// Token: 0x04000DC4 RID: 3524
		public HTMLInputTextElementEvents2_onselectEventHandler m_onselectDelegate;

		// Token: 0x04000DC5 RID: 3525
		public HTMLInputTextElementEvents2_onchangeEventHandler m_onchangeDelegate;

		// Token: 0x04000DC6 RID: 3526
		public HTMLInputTextElementEvents2_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x04000DC7 RID: 3527
		public HTMLInputTextElementEvents2_onresizeendEventHandler m_onresizeendDelegate;

		// Token: 0x04000DC8 RID: 3528
		public HTMLInputTextElementEvents2_onresizestartEventHandler m_onresizestartDelegate;

		// Token: 0x04000DC9 RID: 3529
		public HTMLInputTextElementEvents2_onmoveendEventHandler m_onmoveendDelegate;

		// Token: 0x04000DCA RID: 3530
		public HTMLInputTextElementEvents2_onmovestartEventHandler m_onmovestartDelegate;

		// Token: 0x04000DCB RID: 3531
		public HTMLInputTextElementEvents2_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x04000DCC RID: 3532
		public HTMLInputTextElementEvents2_onmoveEventHandler m_onmoveDelegate;

		// Token: 0x04000DCD RID: 3533
		public HTMLInputTextElementEvents2_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x04000DCE RID: 3534
		public HTMLInputTextElementEvents2_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x04000DCF RID: 3535
		public HTMLInputTextElementEvents2_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x04000DD0 RID: 3536
		public HTMLInputTextElementEvents2_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x04000DD1 RID: 3537
		public HTMLInputTextElementEvents2_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x04000DD2 RID: 3538
		public HTMLInputTextElementEvents2_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x04000DD3 RID: 3539
		public HTMLInputTextElementEvents2_onmouseleaveEventHandler m_onmouseleaveDelegate;

		// Token: 0x04000DD4 RID: 3540
		public HTMLInputTextElementEvents2_onmouseenterEventHandler m_onmouseenterDelegate;

		// Token: 0x04000DD5 RID: 3541
		public HTMLInputTextElementEvents2_onpageEventHandler m_onpageDelegate;

		// Token: 0x04000DD6 RID: 3542
		public HTMLInputTextElementEvents2_onlayoutcompleteEventHandler m_onlayoutcompleteDelegate;

		// Token: 0x04000DD7 RID: 3543
		public HTMLInputTextElementEvents2_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x04000DD8 RID: 3544
		public HTMLInputTextElementEvents2_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x04000DD9 RID: 3545
		public HTMLInputTextElementEvents2_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x04000DDA RID: 3546
		public HTMLInputTextElementEvents2_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x04000DDB RID: 3547
		public HTMLInputTextElementEvents2_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x04000DDC RID: 3548
		public HTMLInputTextElementEvents2_onpasteEventHandler m_onpasteDelegate;

		// Token: 0x04000DDD RID: 3549
		public HTMLInputTextElementEvents2_onbeforepasteEventHandler m_onbeforepasteDelegate;

		// Token: 0x04000DDE RID: 3550
		public HTMLInputTextElementEvents2_oncopyEventHandler m_oncopyDelegate;

		// Token: 0x04000DDF RID: 3551
		public HTMLInputTextElementEvents2_onbeforecopyEventHandler m_onbeforecopyDelegate;

		// Token: 0x04000DE0 RID: 3552
		public HTMLInputTextElementEvents2_oncutEventHandler m_oncutDelegate;

		// Token: 0x04000DE1 RID: 3553
		public HTMLInputTextElementEvents2_onbeforecutEventHandler m_onbeforecutDelegate;

		// Token: 0x04000DE2 RID: 3554
		public HTMLInputTextElementEvents2_ondropEventHandler m_ondropDelegate;

		// Token: 0x04000DE3 RID: 3555
		public HTMLInputTextElementEvents2_ondragleaveEventHandler m_ondragleaveDelegate;

		// Token: 0x04000DE4 RID: 3556
		public HTMLInputTextElementEvents2_ondragoverEventHandler m_ondragoverDelegate;

		// Token: 0x04000DE5 RID: 3557
		public HTMLInputTextElementEvents2_ondragenterEventHandler m_ondragenterDelegate;

		// Token: 0x04000DE6 RID: 3558
		public HTMLInputTextElementEvents2_ondragendEventHandler m_ondragendDelegate;

		// Token: 0x04000DE7 RID: 3559
		public HTMLInputTextElementEvents2_ondragEventHandler m_ondragDelegate;

		// Token: 0x04000DE8 RID: 3560
		public HTMLInputTextElementEvents2_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x04000DE9 RID: 3561
		public HTMLInputTextElementEvents2_onblurEventHandler m_onblurDelegate;

		// Token: 0x04000DEA RID: 3562
		public HTMLInputTextElementEvents2_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x04000DEB RID: 3563
		public HTMLInputTextElementEvents2_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x04000DEC RID: 3564
		public HTMLInputTextElementEvents2_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x04000DED RID: 3565
		public HTMLInputTextElementEvents2_onlosecaptureEventHandler m_onlosecaptureDelegate;

		// Token: 0x04000DEE RID: 3566
		public HTMLInputTextElementEvents2_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x04000DEF RID: 3567
		public HTMLInputTextElementEvents2_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x04000DF0 RID: 3568
		public HTMLInputTextElementEvents2_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x04000DF1 RID: 3569
		public HTMLInputTextElementEvents2_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x04000DF2 RID: 3570
		public HTMLInputTextElementEvents2_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x04000DF3 RID: 3571
		public HTMLInputTextElementEvents2_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x04000DF4 RID: 3572
		public HTMLInputTextElementEvents2_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x04000DF5 RID: 3573
		public HTMLInputTextElementEvents2_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x04000DF6 RID: 3574
		public HTMLInputTextElementEvents2_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x04000DF7 RID: 3575
		public HTMLInputTextElementEvents2_onfilterchangeEventHandler m_onfilterchangeDelegate;

		// Token: 0x04000DF8 RID: 3576
		public HTMLInputTextElementEvents2_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x04000DF9 RID: 3577
		public HTMLInputTextElementEvents2_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x04000DFA RID: 3578
		public HTMLInputTextElementEvents2_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x04000DFB RID: 3579
		public HTMLInputTextElementEvents2_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x04000DFC RID: 3580
		public HTMLInputTextElementEvents2_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x04000DFD RID: 3581
		public HTMLInputTextElementEvents2_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x04000DFE RID: 3582
		public HTMLInputTextElementEvents2_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x04000DFF RID: 3583
		public HTMLInputTextElementEvents2_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x04000E00 RID: 3584
		public HTMLInputTextElementEvents2_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x04000E01 RID: 3585
		public HTMLInputTextElementEvents2_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x04000E02 RID: 3586
		public HTMLInputTextElementEvents2_onclickEventHandler m_onclickDelegate;

		// Token: 0x04000E03 RID: 3587
		public HTMLInputTextElementEvents2_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x04000E04 RID: 3588
		public int m_dwCookie;
	}
}
