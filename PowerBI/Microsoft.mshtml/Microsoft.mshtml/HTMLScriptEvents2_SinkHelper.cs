using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DE6 RID: 3558
	[ClassInterface(0)]
	internal sealed class HTMLScriptEvents2_SinkHelper : HTMLScriptEvents2
	{
		// Token: 0x06018255 RID: 98901 RVA: 0x0007D2A0 File Offset: 0x0007C2A0
		public override void onerror(IHTMLEventObj A_1)
		{
			if (this.m_onerrorDelegate != null)
			{
				this.m_onerrorDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018256 RID: 98902 RVA: 0x0007D2D0 File Offset: 0x0007C2D0
		public override bool onmousewheel(IHTMLEventObj A_1)
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate(A_1);
		}

		// Token: 0x06018257 RID: 98903 RVA: 0x0007D300 File Offset: 0x0007C300
		public override void onresizeend(IHTMLEventObj A_1)
		{
			if (this.m_onresizeendDelegate != null)
			{
				this.m_onresizeendDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018258 RID: 98904 RVA: 0x0007D330 File Offset: 0x0007C330
		public override bool onresizestart(IHTMLEventObj A_1)
		{
			return this.m_onresizestartDelegate != null && this.m_onresizestartDelegate(A_1);
		}

		// Token: 0x06018259 RID: 98905 RVA: 0x0007D360 File Offset: 0x0007C360
		public override void onmoveend(IHTMLEventObj A_1)
		{
			if (this.m_onmoveendDelegate != null)
			{
				this.m_onmoveendDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601825A RID: 98906 RVA: 0x0007D390 File Offset: 0x0007C390
		public override bool onmovestart(IHTMLEventObj A_1)
		{
			return this.m_onmovestartDelegate != null && this.m_onmovestartDelegate(A_1);
		}

		// Token: 0x0601825B RID: 98907 RVA: 0x0007D3C0 File Offset: 0x0007C3C0
		public override bool oncontrolselect(IHTMLEventObj A_1)
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate(A_1);
		}

		// Token: 0x0601825C RID: 98908 RVA: 0x0007D3F0 File Offset: 0x0007C3F0
		public override void onmove(IHTMLEventObj A_1)
		{
			if (this.m_onmoveDelegate != null)
			{
				this.m_onmoveDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601825D RID: 98909 RVA: 0x0007D420 File Offset: 0x0007C420
		public override void onfocusout(IHTMLEventObj A_1)
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601825E RID: 98910 RVA: 0x0007D450 File Offset: 0x0007C450
		public override void onfocusin(IHTMLEventObj A_1)
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601825F RID: 98911 RVA: 0x0007D480 File Offset: 0x0007C480
		public override bool onbeforeactivate(IHTMLEventObj A_1)
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate(A_1);
		}

		// Token: 0x06018260 RID: 98912 RVA: 0x0007D4B0 File Offset: 0x0007C4B0
		public override bool onbeforedeactivate(IHTMLEventObj A_1)
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate(A_1);
		}

		// Token: 0x06018261 RID: 98913 RVA: 0x0007D4E0 File Offset: 0x0007C4E0
		public override void ondeactivate(IHTMLEventObj A_1)
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018262 RID: 98914 RVA: 0x0007D510 File Offset: 0x0007C510
		public override void onactivate(IHTMLEventObj A_1)
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018263 RID: 98915 RVA: 0x0007D540 File Offset: 0x0007C540
		public override void onmouseleave(IHTMLEventObj A_1)
		{
			if (this.m_onmouseleaveDelegate != null)
			{
				this.m_onmouseleaveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018264 RID: 98916 RVA: 0x0007D570 File Offset: 0x0007C570
		public override void onmouseenter(IHTMLEventObj A_1)
		{
			if (this.m_onmouseenterDelegate != null)
			{
				this.m_onmouseenterDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018265 RID: 98917 RVA: 0x0007D5A0 File Offset: 0x0007C5A0
		public override void onpage(IHTMLEventObj A_1)
		{
			if (this.m_onpageDelegate != null)
			{
				this.m_onpageDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018266 RID: 98918 RVA: 0x0007D5D0 File Offset: 0x0007C5D0
		public override void onlayoutcomplete(IHTMLEventObj A_1)
		{
			if (this.m_onlayoutcompleteDelegate != null)
			{
				this.m_onlayoutcompleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018267 RID: 98919 RVA: 0x0007D600 File Offset: 0x0007C600
		public override void onreadystatechange(IHTMLEventObj A_1)
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018268 RID: 98920 RVA: 0x0007D630 File Offset: 0x0007C630
		public override void oncellchange(IHTMLEventObj A_1)
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018269 RID: 98921 RVA: 0x0007D660 File Offset: 0x0007C660
		public override void onrowsinserted(IHTMLEventObj A_1)
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601826A RID: 98922 RVA: 0x0007D690 File Offset: 0x0007C690
		public override void onrowsdelete(IHTMLEventObj A_1)
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601826B RID: 98923 RVA: 0x0007D6C0 File Offset: 0x0007C6C0
		public override bool oncontextmenu(IHTMLEventObj A_1)
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate(A_1);
		}

		// Token: 0x0601826C RID: 98924 RVA: 0x0007D6F0 File Offset: 0x0007C6F0
		public override bool onpaste(IHTMLEventObj A_1)
		{
			return this.m_onpasteDelegate != null && this.m_onpasteDelegate(A_1);
		}

		// Token: 0x0601826D RID: 98925 RVA: 0x0007D720 File Offset: 0x0007C720
		public override bool onbeforepaste(IHTMLEventObj A_1)
		{
			return this.m_onbeforepasteDelegate != null && this.m_onbeforepasteDelegate(A_1);
		}

		// Token: 0x0601826E RID: 98926 RVA: 0x0007D750 File Offset: 0x0007C750
		public override bool oncopy(IHTMLEventObj A_1)
		{
			return this.m_oncopyDelegate != null && this.m_oncopyDelegate(A_1);
		}

		// Token: 0x0601826F RID: 98927 RVA: 0x0007D780 File Offset: 0x0007C780
		public override bool onbeforecopy(IHTMLEventObj A_1)
		{
			return this.m_onbeforecopyDelegate != null && this.m_onbeforecopyDelegate(A_1);
		}

		// Token: 0x06018270 RID: 98928 RVA: 0x0007D7B0 File Offset: 0x0007C7B0
		public override bool oncut(IHTMLEventObj A_1)
		{
			return this.m_oncutDelegate != null && this.m_oncutDelegate(A_1);
		}

		// Token: 0x06018271 RID: 98929 RVA: 0x0007D7E0 File Offset: 0x0007C7E0
		public override bool onbeforecut(IHTMLEventObj A_1)
		{
			return this.m_onbeforecutDelegate != null && this.m_onbeforecutDelegate(A_1);
		}

		// Token: 0x06018272 RID: 98930 RVA: 0x0007D810 File Offset: 0x0007C810
		public override bool ondrop(IHTMLEventObj A_1)
		{
			return this.m_ondropDelegate != null && this.m_ondropDelegate(A_1);
		}

		// Token: 0x06018273 RID: 98931 RVA: 0x0007D840 File Offset: 0x0007C840
		public override void ondragleave(IHTMLEventObj A_1)
		{
			if (this.m_ondragleaveDelegate != null)
			{
				this.m_ondragleaveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018274 RID: 98932 RVA: 0x0007D870 File Offset: 0x0007C870
		public override bool ondragover(IHTMLEventObj A_1)
		{
			return this.m_ondragoverDelegate != null && this.m_ondragoverDelegate(A_1);
		}

		// Token: 0x06018275 RID: 98933 RVA: 0x0007D8A0 File Offset: 0x0007C8A0
		public override bool ondragenter(IHTMLEventObj A_1)
		{
			return this.m_ondragenterDelegate != null && this.m_ondragenterDelegate(A_1);
		}

		// Token: 0x06018276 RID: 98934 RVA: 0x0007D8D0 File Offset: 0x0007C8D0
		public override void ondragend(IHTMLEventObj A_1)
		{
			if (this.m_ondragendDelegate != null)
			{
				this.m_ondragendDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018277 RID: 98935 RVA: 0x0007D900 File Offset: 0x0007C900
		public override bool ondrag(IHTMLEventObj A_1)
		{
			return this.m_ondragDelegate != null && this.m_ondragDelegate(A_1);
		}

		// Token: 0x06018278 RID: 98936 RVA: 0x0007D930 File Offset: 0x0007C930
		public override void onresize(IHTMLEventObj A_1)
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018279 RID: 98937 RVA: 0x0007D960 File Offset: 0x0007C960
		public override void onblur(IHTMLEventObj A_1)
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601827A RID: 98938 RVA: 0x0007D990 File Offset: 0x0007C990
		public override void onfocus(IHTMLEventObj A_1)
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601827B RID: 98939 RVA: 0x0007D9C0 File Offset: 0x0007C9C0
		public override void onscroll(IHTMLEventObj A_1)
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601827C RID: 98940 RVA: 0x0007D9F0 File Offset: 0x0007C9F0
		public override void onpropertychange(IHTMLEventObj A_1)
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601827D RID: 98941 RVA: 0x0007DA20 File Offset: 0x0007CA20
		public override void onlosecapture(IHTMLEventObj A_1)
		{
			if (this.m_onlosecaptureDelegate != null)
			{
				this.m_onlosecaptureDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601827E RID: 98942 RVA: 0x0007DA50 File Offset: 0x0007CA50
		public override void ondatasetcomplete(IHTMLEventObj A_1)
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601827F RID: 98943 RVA: 0x0007DA80 File Offset: 0x0007CA80
		public override void ondataavailable(IHTMLEventObj A_1)
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018280 RID: 98944 RVA: 0x0007DAB0 File Offset: 0x0007CAB0
		public override void ondatasetchanged(IHTMLEventObj A_1)
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018281 RID: 98945 RVA: 0x0007DAE0 File Offset: 0x0007CAE0
		public override void onrowenter(IHTMLEventObj A_1)
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018282 RID: 98946 RVA: 0x0007DB10 File Offset: 0x0007CB10
		public override bool onrowexit(IHTMLEventObj A_1)
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate(A_1);
		}

		// Token: 0x06018283 RID: 98947 RVA: 0x0007DB40 File Offset: 0x0007CB40
		public override bool onerrorupdate(IHTMLEventObj A_1)
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate(A_1);
		}

		// Token: 0x06018284 RID: 98948 RVA: 0x0007DB70 File Offset: 0x0007CB70
		public override void onafterupdate(IHTMLEventObj A_1)
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018285 RID: 98949 RVA: 0x0007DBA0 File Offset: 0x0007CBA0
		public override bool onbeforeupdate(IHTMLEventObj A_1)
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate(A_1);
		}

		// Token: 0x06018286 RID: 98950 RVA: 0x0007DBD0 File Offset: 0x0007CBD0
		public override bool ondragstart(IHTMLEventObj A_1)
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate(A_1);
		}

		// Token: 0x06018287 RID: 98951 RVA: 0x0007DC00 File Offset: 0x0007CC00
		public override void onfilterchange(IHTMLEventObj A_1)
		{
			if (this.m_onfilterchangeDelegate != null)
			{
				this.m_onfilterchangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018288 RID: 98952 RVA: 0x0007DC30 File Offset: 0x0007CC30
		public override bool onselectstart(IHTMLEventObj A_1)
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate(A_1);
		}

		// Token: 0x06018289 RID: 98953 RVA: 0x0007DC60 File Offset: 0x0007CC60
		public override void onmouseup(IHTMLEventObj A_1)
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601828A RID: 98954 RVA: 0x0007DC90 File Offset: 0x0007CC90
		public override void onmousedown(IHTMLEventObj A_1)
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601828B RID: 98955 RVA: 0x0007DCC0 File Offset: 0x0007CCC0
		public override void onmousemove(IHTMLEventObj A_1)
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601828C RID: 98956 RVA: 0x0007DCF0 File Offset: 0x0007CCF0
		public override void onmouseover(IHTMLEventObj A_1)
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601828D RID: 98957 RVA: 0x0007DD20 File Offset: 0x0007CD20
		public override void onmouseout(IHTMLEventObj A_1)
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601828E RID: 98958 RVA: 0x0007DD50 File Offset: 0x0007CD50
		public override void onkeyup(IHTMLEventObj A_1)
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601828F RID: 98959 RVA: 0x0007DD80 File Offset: 0x0007CD80
		public override void onkeydown(IHTMLEventObj A_1)
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018290 RID: 98960 RVA: 0x0007DDB0 File Offset: 0x0007CDB0
		public override bool onkeypress(IHTMLEventObj A_1)
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate(A_1);
		}

		// Token: 0x06018291 RID: 98961 RVA: 0x0007DDE0 File Offset: 0x0007CDE0
		public override bool ondblclick(IHTMLEventObj A_1)
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate(A_1);
		}

		// Token: 0x06018292 RID: 98962 RVA: 0x0007DE10 File Offset: 0x0007CE10
		public override bool onclick(IHTMLEventObj A_1)
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate(A_1);
		}

		// Token: 0x06018293 RID: 98963 RVA: 0x0007DE40 File Offset: 0x0007CE40
		public override bool onhelp(IHTMLEventObj A_1)
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate(A_1);
		}

		// Token: 0x06018294 RID: 98964 RVA: 0x0007DE70 File Offset: 0x0007CE70
		internal HTMLScriptEvents2_SinkHelper()
		{
			this.m_dwCookie = 0;
			this.m_onerrorDelegate = null;
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

		// Token: 0x0400096B RID: 2411
		public HTMLScriptEvents2_onerrorEventHandler m_onerrorDelegate;

		// Token: 0x0400096C RID: 2412
		public HTMLScriptEvents2_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x0400096D RID: 2413
		public HTMLScriptEvents2_onresizeendEventHandler m_onresizeendDelegate;

		// Token: 0x0400096E RID: 2414
		public HTMLScriptEvents2_onresizestartEventHandler m_onresizestartDelegate;

		// Token: 0x0400096F RID: 2415
		public HTMLScriptEvents2_onmoveendEventHandler m_onmoveendDelegate;

		// Token: 0x04000970 RID: 2416
		public HTMLScriptEvents2_onmovestartEventHandler m_onmovestartDelegate;

		// Token: 0x04000971 RID: 2417
		public HTMLScriptEvents2_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x04000972 RID: 2418
		public HTMLScriptEvents2_onmoveEventHandler m_onmoveDelegate;

		// Token: 0x04000973 RID: 2419
		public HTMLScriptEvents2_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x04000974 RID: 2420
		public HTMLScriptEvents2_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x04000975 RID: 2421
		public HTMLScriptEvents2_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x04000976 RID: 2422
		public HTMLScriptEvents2_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x04000977 RID: 2423
		public HTMLScriptEvents2_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x04000978 RID: 2424
		public HTMLScriptEvents2_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x04000979 RID: 2425
		public HTMLScriptEvents2_onmouseleaveEventHandler m_onmouseleaveDelegate;

		// Token: 0x0400097A RID: 2426
		public HTMLScriptEvents2_onmouseenterEventHandler m_onmouseenterDelegate;

		// Token: 0x0400097B RID: 2427
		public HTMLScriptEvents2_onpageEventHandler m_onpageDelegate;

		// Token: 0x0400097C RID: 2428
		public HTMLScriptEvents2_onlayoutcompleteEventHandler m_onlayoutcompleteDelegate;

		// Token: 0x0400097D RID: 2429
		public HTMLScriptEvents2_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x0400097E RID: 2430
		public HTMLScriptEvents2_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x0400097F RID: 2431
		public HTMLScriptEvents2_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x04000980 RID: 2432
		public HTMLScriptEvents2_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x04000981 RID: 2433
		public HTMLScriptEvents2_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x04000982 RID: 2434
		public HTMLScriptEvents2_onpasteEventHandler m_onpasteDelegate;

		// Token: 0x04000983 RID: 2435
		public HTMLScriptEvents2_onbeforepasteEventHandler m_onbeforepasteDelegate;

		// Token: 0x04000984 RID: 2436
		public HTMLScriptEvents2_oncopyEventHandler m_oncopyDelegate;

		// Token: 0x04000985 RID: 2437
		public HTMLScriptEvents2_onbeforecopyEventHandler m_onbeforecopyDelegate;

		// Token: 0x04000986 RID: 2438
		public HTMLScriptEvents2_oncutEventHandler m_oncutDelegate;

		// Token: 0x04000987 RID: 2439
		public HTMLScriptEvents2_onbeforecutEventHandler m_onbeforecutDelegate;

		// Token: 0x04000988 RID: 2440
		public HTMLScriptEvents2_ondropEventHandler m_ondropDelegate;

		// Token: 0x04000989 RID: 2441
		public HTMLScriptEvents2_ondragleaveEventHandler m_ondragleaveDelegate;

		// Token: 0x0400098A RID: 2442
		public HTMLScriptEvents2_ondragoverEventHandler m_ondragoverDelegate;

		// Token: 0x0400098B RID: 2443
		public HTMLScriptEvents2_ondragenterEventHandler m_ondragenterDelegate;

		// Token: 0x0400098C RID: 2444
		public HTMLScriptEvents2_ondragendEventHandler m_ondragendDelegate;

		// Token: 0x0400098D RID: 2445
		public HTMLScriptEvents2_ondragEventHandler m_ondragDelegate;

		// Token: 0x0400098E RID: 2446
		public HTMLScriptEvents2_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x0400098F RID: 2447
		public HTMLScriptEvents2_onblurEventHandler m_onblurDelegate;

		// Token: 0x04000990 RID: 2448
		public HTMLScriptEvents2_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x04000991 RID: 2449
		public HTMLScriptEvents2_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x04000992 RID: 2450
		public HTMLScriptEvents2_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x04000993 RID: 2451
		public HTMLScriptEvents2_onlosecaptureEventHandler m_onlosecaptureDelegate;

		// Token: 0x04000994 RID: 2452
		public HTMLScriptEvents2_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x04000995 RID: 2453
		public HTMLScriptEvents2_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x04000996 RID: 2454
		public HTMLScriptEvents2_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x04000997 RID: 2455
		public HTMLScriptEvents2_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x04000998 RID: 2456
		public HTMLScriptEvents2_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x04000999 RID: 2457
		public HTMLScriptEvents2_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x0400099A RID: 2458
		public HTMLScriptEvents2_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x0400099B RID: 2459
		public HTMLScriptEvents2_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x0400099C RID: 2460
		public HTMLScriptEvents2_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x0400099D RID: 2461
		public HTMLScriptEvents2_onfilterchangeEventHandler m_onfilterchangeDelegate;

		// Token: 0x0400099E RID: 2462
		public HTMLScriptEvents2_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x0400099F RID: 2463
		public HTMLScriptEvents2_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x040009A0 RID: 2464
		public HTMLScriptEvents2_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x040009A1 RID: 2465
		public HTMLScriptEvents2_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x040009A2 RID: 2466
		public HTMLScriptEvents2_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x040009A3 RID: 2467
		public HTMLScriptEvents2_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x040009A4 RID: 2468
		public HTMLScriptEvents2_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x040009A5 RID: 2469
		public HTMLScriptEvents2_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x040009A6 RID: 2470
		public HTMLScriptEvents2_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x040009A7 RID: 2471
		public HTMLScriptEvents2_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x040009A8 RID: 2472
		public HTMLScriptEvents2_onclickEventHandler m_onclickDelegate;

		// Token: 0x040009A9 RID: 2473
		public HTMLScriptEvents2_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x040009AA RID: 2474
		public int m_dwCookie;
	}
}
