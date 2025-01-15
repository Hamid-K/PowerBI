using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000E08 RID: 3592
	[ClassInterface(0)]
	internal sealed class HTMLImgEvents2_SinkHelper : HTMLImgEvents2
	{
		// Token: 0x06018E11 RID: 101905 RVA: 0x000E7FEC File Offset: 0x000E6FEC
		public override void onabort(IHTMLEventObj A_1)
		{
			if (this.m_onabortDelegate != null)
			{
				this.m_onabortDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E12 RID: 101906 RVA: 0x000E801C File Offset: 0x000E701C
		public override void onerror(IHTMLEventObj A_1)
		{
			if (this.m_onerrorDelegate != null)
			{
				this.m_onerrorDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E13 RID: 101907 RVA: 0x000E804C File Offset: 0x000E704C
		public override void onload(IHTMLEventObj A_1)
		{
			if (this.m_onloadDelegate != null)
			{
				this.m_onloadDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E14 RID: 101908 RVA: 0x000E807C File Offset: 0x000E707C
		public override bool onmousewheel(IHTMLEventObj A_1)
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate(A_1);
		}

		// Token: 0x06018E15 RID: 101909 RVA: 0x000E80AC File Offset: 0x000E70AC
		public override void onresizeend(IHTMLEventObj A_1)
		{
			if (this.m_onresizeendDelegate != null)
			{
				this.m_onresizeendDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E16 RID: 101910 RVA: 0x000E80DC File Offset: 0x000E70DC
		public override bool onresizestart(IHTMLEventObj A_1)
		{
			return this.m_onresizestartDelegate != null && this.m_onresizestartDelegate(A_1);
		}

		// Token: 0x06018E17 RID: 101911 RVA: 0x000E810C File Offset: 0x000E710C
		public override void onmoveend(IHTMLEventObj A_1)
		{
			if (this.m_onmoveendDelegate != null)
			{
				this.m_onmoveendDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E18 RID: 101912 RVA: 0x000E813C File Offset: 0x000E713C
		public override bool onmovestart(IHTMLEventObj A_1)
		{
			return this.m_onmovestartDelegate != null && this.m_onmovestartDelegate(A_1);
		}

		// Token: 0x06018E19 RID: 101913 RVA: 0x000E816C File Offset: 0x000E716C
		public override bool oncontrolselect(IHTMLEventObj A_1)
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate(A_1);
		}

		// Token: 0x06018E1A RID: 101914 RVA: 0x000E819C File Offset: 0x000E719C
		public override void onmove(IHTMLEventObj A_1)
		{
			if (this.m_onmoveDelegate != null)
			{
				this.m_onmoveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E1B RID: 101915 RVA: 0x000E81CC File Offset: 0x000E71CC
		public override void onfocusout(IHTMLEventObj A_1)
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E1C RID: 101916 RVA: 0x000E81FC File Offset: 0x000E71FC
		public override void onfocusin(IHTMLEventObj A_1)
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E1D RID: 101917 RVA: 0x000E822C File Offset: 0x000E722C
		public override bool onbeforeactivate(IHTMLEventObj A_1)
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate(A_1);
		}

		// Token: 0x06018E1E RID: 101918 RVA: 0x000E825C File Offset: 0x000E725C
		public override bool onbeforedeactivate(IHTMLEventObj A_1)
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate(A_1);
		}

		// Token: 0x06018E1F RID: 101919 RVA: 0x000E828C File Offset: 0x000E728C
		public override void ondeactivate(IHTMLEventObj A_1)
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E20 RID: 101920 RVA: 0x000E82BC File Offset: 0x000E72BC
		public override void onactivate(IHTMLEventObj A_1)
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E21 RID: 101921 RVA: 0x000E82EC File Offset: 0x000E72EC
		public override void onmouseleave(IHTMLEventObj A_1)
		{
			if (this.m_onmouseleaveDelegate != null)
			{
				this.m_onmouseleaveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E22 RID: 101922 RVA: 0x000E831C File Offset: 0x000E731C
		public override void onmouseenter(IHTMLEventObj A_1)
		{
			if (this.m_onmouseenterDelegate != null)
			{
				this.m_onmouseenterDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E23 RID: 101923 RVA: 0x000E834C File Offset: 0x000E734C
		public override void onpage(IHTMLEventObj A_1)
		{
			if (this.m_onpageDelegate != null)
			{
				this.m_onpageDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E24 RID: 101924 RVA: 0x000E837C File Offset: 0x000E737C
		public override void onlayoutcomplete(IHTMLEventObj A_1)
		{
			if (this.m_onlayoutcompleteDelegate != null)
			{
				this.m_onlayoutcompleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E25 RID: 101925 RVA: 0x000E83AC File Offset: 0x000E73AC
		public override void onreadystatechange(IHTMLEventObj A_1)
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E26 RID: 101926 RVA: 0x000E83DC File Offset: 0x000E73DC
		public override void oncellchange(IHTMLEventObj A_1)
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E27 RID: 101927 RVA: 0x000E840C File Offset: 0x000E740C
		public override void onrowsinserted(IHTMLEventObj A_1)
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E28 RID: 101928 RVA: 0x000E843C File Offset: 0x000E743C
		public override void onrowsdelete(IHTMLEventObj A_1)
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E29 RID: 101929 RVA: 0x000E846C File Offset: 0x000E746C
		public override bool oncontextmenu(IHTMLEventObj A_1)
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate(A_1);
		}

		// Token: 0x06018E2A RID: 101930 RVA: 0x000E849C File Offset: 0x000E749C
		public override bool onpaste(IHTMLEventObj A_1)
		{
			return this.m_onpasteDelegate != null && this.m_onpasteDelegate(A_1);
		}

		// Token: 0x06018E2B RID: 101931 RVA: 0x000E84CC File Offset: 0x000E74CC
		public override bool onbeforepaste(IHTMLEventObj A_1)
		{
			return this.m_onbeforepasteDelegate != null && this.m_onbeforepasteDelegate(A_1);
		}

		// Token: 0x06018E2C RID: 101932 RVA: 0x000E84FC File Offset: 0x000E74FC
		public override bool oncopy(IHTMLEventObj A_1)
		{
			return this.m_oncopyDelegate != null && this.m_oncopyDelegate(A_1);
		}

		// Token: 0x06018E2D RID: 101933 RVA: 0x000E852C File Offset: 0x000E752C
		public override bool onbeforecopy(IHTMLEventObj A_1)
		{
			return this.m_onbeforecopyDelegate != null && this.m_onbeforecopyDelegate(A_1);
		}

		// Token: 0x06018E2E RID: 101934 RVA: 0x000E855C File Offset: 0x000E755C
		public override bool oncut(IHTMLEventObj A_1)
		{
			return this.m_oncutDelegate != null && this.m_oncutDelegate(A_1);
		}

		// Token: 0x06018E2F RID: 101935 RVA: 0x000E858C File Offset: 0x000E758C
		public override bool onbeforecut(IHTMLEventObj A_1)
		{
			return this.m_onbeforecutDelegate != null && this.m_onbeforecutDelegate(A_1);
		}

		// Token: 0x06018E30 RID: 101936 RVA: 0x000E85BC File Offset: 0x000E75BC
		public override bool ondrop(IHTMLEventObj A_1)
		{
			return this.m_ondropDelegate != null && this.m_ondropDelegate(A_1);
		}

		// Token: 0x06018E31 RID: 101937 RVA: 0x000E85EC File Offset: 0x000E75EC
		public override void ondragleave(IHTMLEventObj A_1)
		{
			if (this.m_ondragleaveDelegate != null)
			{
				this.m_ondragleaveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E32 RID: 101938 RVA: 0x000E861C File Offset: 0x000E761C
		public override bool ondragover(IHTMLEventObj A_1)
		{
			return this.m_ondragoverDelegate != null && this.m_ondragoverDelegate(A_1);
		}

		// Token: 0x06018E33 RID: 101939 RVA: 0x000E864C File Offset: 0x000E764C
		public override bool ondragenter(IHTMLEventObj A_1)
		{
			return this.m_ondragenterDelegate != null && this.m_ondragenterDelegate(A_1);
		}

		// Token: 0x06018E34 RID: 101940 RVA: 0x000E867C File Offset: 0x000E767C
		public override void ondragend(IHTMLEventObj A_1)
		{
			if (this.m_ondragendDelegate != null)
			{
				this.m_ondragendDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E35 RID: 101941 RVA: 0x000E86AC File Offset: 0x000E76AC
		public override bool ondrag(IHTMLEventObj A_1)
		{
			return this.m_ondragDelegate != null && this.m_ondragDelegate(A_1);
		}

		// Token: 0x06018E36 RID: 101942 RVA: 0x000E86DC File Offset: 0x000E76DC
		public override void onresize(IHTMLEventObj A_1)
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E37 RID: 101943 RVA: 0x000E870C File Offset: 0x000E770C
		public override void onblur(IHTMLEventObj A_1)
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E38 RID: 101944 RVA: 0x000E873C File Offset: 0x000E773C
		public override void onfocus(IHTMLEventObj A_1)
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E39 RID: 101945 RVA: 0x000E876C File Offset: 0x000E776C
		public override void onscroll(IHTMLEventObj A_1)
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E3A RID: 101946 RVA: 0x000E879C File Offset: 0x000E779C
		public override void onpropertychange(IHTMLEventObj A_1)
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E3B RID: 101947 RVA: 0x000E87CC File Offset: 0x000E77CC
		public override void onlosecapture(IHTMLEventObj A_1)
		{
			if (this.m_onlosecaptureDelegate != null)
			{
				this.m_onlosecaptureDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E3C RID: 101948 RVA: 0x000E87FC File Offset: 0x000E77FC
		public override void ondatasetcomplete(IHTMLEventObj A_1)
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E3D RID: 101949 RVA: 0x000E882C File Offset: 0x000E782C
		public override void ondataavailable(IHTMLEventObj A_1)
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E3E RID: 101950 RVA: 0x000E885C File Offset: 0x000E785C
		public override void ondatasetchanged(IHTMLEventObj A_1)
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E3F RID: 101951 RVA: 0x000E888C File Offset: 0x000E788C
		public override void onrowenter(IHTMLEventObj A_1)
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E40 RID: 101952 RVA: 0x000E88BC File Offset: 0x000E78BC
		public override bool onrowexit(IHTMLEventObj A_1)
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate(A_1);
		}

		// Token: 0x06018E41 RID: 101953 RVA: 0x000E88EC File Offset: 0x000E78EC
		public override bool onerrorupdate(IHTMLEventObj A_1)
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate(A_1);
		}

		// Token: 0x06018E42 RID: 101954 RVA: 0x000E891C File Offset: 0x000E791C
		public override void onafterupdate(IHTMLEventObj A_1)
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E43 RID: 101955 RVA: 0x000E894C File Offset: 0x000E794C
		public override bool onbeforeupdate(IHTMLEventObj A_1)
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate(A_1);
		}

		// Token: 0x06018E44 RID: 101956 RVA: 0x000E897C File Offset: 0x000E797C
		public override bool ondragstart(IHTMLEventObj A_1)
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate(A_1);
		}

		// Token: 0x06018E45 RID: 101957 RVA: 0x000E89AC File Offset: 0x000E79AC
		public override void onfilterchange(IHTMLEventObj A_1)
		{
			if (this.m_onfilterchangeDelegate != null)
			{
				this.m_onfilterchangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E46 RID: 101958 RVA: 0x000E89DC File Offset: 0x000E79DC
		public override bool onselectstart(IHTMLEventObj A_1)
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate(A_1);
		}

		// Token: 0x06018E47 RID: 101959 RVA: 0x000E8A0C File Offset: 0x000E7A0C
		public override void onmouseup(IHTMLEventObj A_1)
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E48 RID: 101960 RVA: 0x000E8A3C File Offset: 0x000E7A3C
		public override void onmousedown(IHTMLEventObj A_1)
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E49 RID: 101961 RVA: 0x000E8A6C File Offset: 0x000E7A6C
		public override void onmousemove(IHTMLEventObj A_1)
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E4A RID: 101962 RVA: 0x000E8A9C File Offset: 0x000E7A9C
		public override void onmouseover(IHTMLEventObj A_1)
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E4B RID: 101963 RVA: 0x000E8ACC File Offset: 0x000E7ACC
		public override void onmouseout(IHTMLEventObj A_1)
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E4C RID: 101964 RVA: 0x000E8AFC File Offset: 0x000E7AFC
		public override void onkeyup(IHTMLEventObj A_1)
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E4D RID: 101965 RVA: 0x000E8B2C File Offset: 0x000E7B2C
		public override void onkeydown(IHTMLEventObj A_1)
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018E4E RID: 101966 RVA: 0x000E8B5C File Offset: 0x000E7B5C
		public override bool onkeypress(IHTMLEventObj A_1)
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate(A_1);
		}

		// Token: 0x06018E4F RID: 101967 RVA: 0x000E8B8C File Offset: 0x000E7B8C
		public override bool ondblclick(IHTMLEventObj A_1)
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate(A_1);
		}

		// Token: 0x06018E50 RID: 101968 RVA: 0x000E8BBC File Offset: 0x000E7BBC
		public override bool onclick(IHTMLEventObj A_1)
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate(A_1);
		}

		// Token: 0x06018E51 RID: 101969 RVA: 0x000E8BEC File Offset: 0x000E7BEC
		public override bool onhelp(IHTMLEventObj A_1)
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate(A_1);
		}

		// Token: 0x06018E52 RID: 101970 RVA: 0x000E8C1C File Offset: 0x000E7C1C
		internal HTMLImgEvents2_SinkHelper()
		{
			this.m_dwCookie = 0;
			this.m_onabortDelegate = null;
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

		// Token: 0x04000D7C RID: 3452
		public HTMLImgEvents2_onabortEventHandler m_onabortDelegate;

		// Token: 0x04000D7D RID: 3453
		public HTMLImgEvents2_onerrorEventHandler m_onerrorDelegate;

		// Token: 0x04000D7E RID: 3454
		public HTMLImgEvents2_onloadEventHandler m_onloadDelegate;

		// Token: 0x04000D7F RID: 3455
		public HTMLImgEvents2_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x04000D80 RID: 3456
		public HTMLImgEvents2_onresizeendEventHandler m_onresizeendDelegate;

		// Token: 0x04000D81 RID: 3457
		public HTMLImgEvents2_onresizestartEventHandler m_onresizestartDelegate;

		// Token: 0x04000D82 RID: 3458
		public HTMLImgEvents2_onmoveendEventHandler m_onmoveendDelegate;

		// Token: 0x04000D83 RID: 3459
		public HTMLImgEvents2_onmovestartEventHandler m_onmovestartDelegate;

		// Token: 0x04000D84 RID: 3460
		public HTMLImgEvents2_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x04000D85 RID: 3461
		public HTMLImgEvents2_onmoveEventHandler m_onmoveDelegate;

		// Token: 0x04000D86 RID: 3462
		public HTMLImgEvents2_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x04000D87 RID: 3463
		public HTMLImgEvents2_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x04000D88 RID: 3464
		public HTMLImgEvents2_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x04000D89 RID: 3465
		public HTMLImgEvents2_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x04000D8A RID: 3466
		public HTMLImgEvents2_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x04000D8B RID: 3467
		public HTMLImgEvents2_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x04000D8C RID: 3468
		public HTMLImgEvents2_onmouseleaveEventHandler m_onmouseleaveDelegate;

		// Token: 0x04000D8D RID: 3469
		public HTMLImgEvents2_onmouseenterEventHandler m_onmouseenterDelegate;

		// Token: 0x04000D8E RID: 3470
		public HTMLImgEvents2_onpageEventHandler m_onpageDelegate;

		// Token: 0x04000D8F RID: 3471
		public HTMLImgEvents2_onlayoutcompleteEventHandler m_onlayoutcompleteDelegate;

		// Token: 0x04000D90 RID: 3472
		public HTMLImgEvents2_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x04000D91 RID: 3473
		public HTMLImgEvents2_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x04000D92 RID: 3474
		public HTMLImgEvents2_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x04000D93 RID: 3475
		public HTMLImgEvents2_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x04000D94 RID: 3476
		public HTMLImgEvents2_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x04000D95 RID: 3477
		public HTMLImgEvents2_onpasteEventHandler m_onpasteDelegate;

		// Token: 0x04000D96 RID: 3478
		public HTMLImgEvents2_onbeforepasteEventHandler m_onbeforepasteDelegate;

		// Token: 0x04000D97 RID: 3479
		public HTMLImgEvents2_oncopyEventHandler m_oncopyDelegate;

		// Token: 0x04000D98 RID: 3480
		public HTMLImgEvents2_onbeforecopyEventHandler m_onbeforecopyDelegate;

		// Token: 0x04000D99 RID: 3481
		public HTMLImgEvents2_oncutEventHandler m_oncutDelegate;

		// Token: 0x04000D9A RID: 3482
		public HTMLImgEvents2_onbeforecutEventHandler m_onbeforecutDelegate;

		// Token: 0x04000D9B RID: 3483
		public HTMLImgEvents2_ondropEventHandler m_ondropDelegate;

		// Token: 0x04000D9C RID: 3484
		public HTMLImgEvents2_ondragleaveEventHandler m_ondragleaveDelegate;

		// Token: 0x04000D9D RID: 3485
		public HTMLImgEvents2_ondragoverEventHandler m_ondragoverDelegate;

		// Token: 0x04000D9E RID: 3486
		public HTMLImgEvents2_ondragenterEventHandler m_ondragenterDelegate;

		// Token: 0x04000D9F RID: 3487
		public HTMLImgEvents2_ondragendEventHandler m_ondragendDelegate;

		// Token: 0x04000DA0 RID: 3488
		public HTMLImgEvents2_ondragEventHandler m_ondragDelegate;

		// Token: 0x04000DA1 RID: 3489
		public HTMLImgEvents2_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x04000DA2 RID: 3490
		public HTMLImgEvents2_onblurEventHandler m_onblurDelegate;

		// Token: 0x04000DA3 RID: 3491
		public HTMLImgEvents2_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x04000DA4 RID: 3492
		public HTMLImgEvents2_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x04000DA5 RID: 3493
		public HTMLImgEvents2_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x04000DA6 RID: 3494
		public HTMLImgEvents2_onlosecaptureEventHandler m_onlosecaptureDelegate;

		// Token: 0x04000DA7 RID: 3495
		public HTMLImgEvents2_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x04000DA8 RID: 3496
		public HTMLImgEvents2_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x04000DA9 RID: 3497
		public HTMLImgEvents2_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x04000DAA RID: 3498
		public HTMLImgEvents2_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x04000DAB RID: 3499
		public HTMLImgEvents2_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x04000DAC RID: 3500
		public HTMLImgEvents2_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x04000DAD RID: 3501
		public HTMLImgEvents2_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x04000DAE RID: 3502
		public HTMLImgEvents2_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x04000DAF RID: 3503
		public HTMLImgEvents2_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x04000DB0 RID: 3504
		public HTMLImgEvents2_onfilterchangeEventHandler m_onfilterchangeDelegate;

		// Token: 0x04000DB1 RID: 3505
		public HTMLImgEvents2_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x04000DB2 RID: 3506
		public HTMLImgEvents2_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x04000DB3 RID: 3507
		public HTMLImgEvents2_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x04000DB4 RID: 3508
		public HTMLImgEvents2_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x04000DB5 RID: 3509
		public HTMLImgEvents2_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x04000DB6 RID: 3510
		public HTMLImgEvents2_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x04000DB7 RID: 3511
		public HTMLImgEvents2_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x04000DB8 RID: 3512
		public HTMLImgEvents2_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x04000DB9 RID: 3513
		public HTMLImgEvents2_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x04000DBA RID: 3514
		public HTMLImgEvents2_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x04000DBB RID: 3515
		public HTMLImgEvents2_onclickEventHandler m_onclickDelegate;

		// Token: 0x04000DBC RID: 3516
		public HTMLImgEvents2_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x04000DBD RID: 3517
		public int m_dwCookie;
	}
}
