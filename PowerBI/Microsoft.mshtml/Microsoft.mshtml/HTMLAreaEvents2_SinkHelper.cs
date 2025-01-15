using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000E02 RID: 3586
	[ClassInterface(0)]
	internal sealed class HTMLAreaEvents2_SinkHelper : HTMLAreaEvents2
	{
		// Token: 0x06018BB9 RID: 101305 RVA: 0x000D2AB4 File Offset: 0x000D1AB4
		public override bool onmousewheel(IHTMLEventObj A_1)
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate(A_1);
		}

		// Token: 0x06018BBA RID: 101306 RVA: 0x000D2AE4 File Offset: 0x000D1AE4
		public override void onresizeend(IHTMLEventObj A_1)
		{
			if (this.m_onresizeendDelegate != null)
			{
				this.m_onresizeendDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018BBB RID: 101307 RVA: 0x000D2B14 File Offset: 0x000D1B14
		public override bool onresizestart(IHTMLEventObj A_1)
		{
			return this.m_onresizestartDelegate != null && this.m_onresizestartDelegate(A_1);
		}

		// Token: 0x06018BBC RID: 101308 RVA: 0x000D2B44 File Offset: 0x000D1B44
		public override void onmoveend(IHTMLEventObj A_1)
		{
			if (this.m_onmoveendDelegate != null)
			{
				this.m_onmoveendDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018BBD RID: 101309 RVA: 0x000D2B74 File Offset: 0x000D1B74
		public override bool onmovestart(IHTMLEventObj A_1)
		{
			return this.m_onmovestartDelegate != null && this.m_onmovestartDelegate(A_1);
		}

		// Token: 0x06018BBE RID: 101310 RVA: 0x000D2BA4 File Offset: 0x000D1BA4
		public override bool oncontrolselect(IHTMLEventObj A_1)
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate(A_1);
		}

		// Token: 0x06018BBF RID: 101311 RVA: 0x000D2BD4 File Offset: 0x000D1BD4
		public override void onmove(IHTMLEventObj A_1)
		{
			if (this.m_onmoveDelegate != null)
			{
				this.m_onmoveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018BC0 RID: 101312 RVA: 0x000D2C04 File Offset: 0x000D1C04
		public override void onfocusout(IHTMLEventObj A_1)
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018BC1 RID: 101313 RVA: 0x000D2C34 File Offset: 0x000D1C34
		public override void onfocusin(IHTMLEventObj A_1)
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018BC2 RID: 101314 RVA: 0x000D2C64 File Offset: 0x000D1C64
		public override bool onbeforeactivate(IHTMLEventObj A_1)
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate(A_1);
		}

		// Token: 0x06018BC3 RID: 101315 RVA: 0x000D2C94 File Offset: 0x000D1C94
		public override bool onbeforedeactivate(IHTMLEventObj A_1)
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate(A_1);
		}

		// Token: 0x06018BC4 RID: 101316 RVA: 0x000D2CC4 File Offset: 0x000D1CC4
		public override void ondeactivate(IHTMLEventObj A_1)
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018BC5 RID: 101317 RVA: 0x000D2CF4 File Offset: 0x000D1CF4
		public override void onactivate(IHTMLEventObj A_1)
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018BC6 RID: 101318 RVA: 0x000D2D24 File Offset: 0x000D1D24
		public override void onmouseleave(IHTMLEventObj A_1)
		{
			if (this.m_onmouseleaveDelegate != null)
			{
				this.m_onmouseleaveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018BC7 RID: 101319 RVA: 0x000D2D54 File Offset: 0x000D1D54
		public override void onmouseenter(IHTMLEventObj A_1)
		{
			if (this.m_onmouseenterDelegate != null)
			{
				this.m_onmouseenterDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018BC8 RID: 101320 RVA: 0x000D2D84 File Offset: 0x000D1D84
		public override void onpage(IHTMLEventObj A_1)
		{
			if (this.m_onpageDelegate != null)
			{
				this.m_onpageDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018BC9 RID: 101321 RVA: 0x000D2DB4 File Offset: 0x000D1DB4
		public override void onlayoutcomplete(IHTMLEventObj A_1)
		{
			if (this.m_onlayoutcompleteDelegate != null)
			{
				this.m_onlayoutcompleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018BCA RID: 101322 RVA: 0x000D2DE4 File Offset: 0x000D1DE4
		public override void onreadystatechange(IHTMLEventObj A_1)
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018BCB RID: 101323 RVA: 0x000D2E14 File Offset: 0x000D1E14
		public override void oncellchange(IHTMLEventObj A_1)
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018BCC RID: 101324 RVA: 0x000D2E44 File Offset: 0x000D1E44
		public override void onrowsinserted(IHTMLEventObj A_1)
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018BCD RID: 101325 RVA: 0x000D2E74 File Offset: 0x000D1E74
		public override void onrowsdelete(IHTMLEventObj A_1)
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018BCE RID: 101326 RVA: 0x000D2EA4 File Offset: 0x000D1EA4
		public override bool oncontextmenu(IHTMLEventObj A_1)
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate(A_1);
		}

		// Token: 0x06018BCF RID: 101327 RVA: 0x000D2ED4 File Offset: 0x000D1ED4
		public override bool onpaste(IHTMLEventObj A_1)
		{
			return this.m_onpasteDelegate != null && this.m_onpasteDelegate(A_1);
		}

		// Token: 0x06018BD0 RID: 101328 RVA: 0x000D2F04 File Offset: 0x000D1F04
		public override bool onbeforepaste(IHTMLEventObj A_1)
		{
			return this.m_onbeforepasteDelegate != null && this.m_onbeforepasteDelegate(A_1);
		}

		// Token: 0x06018BD1 RID: 101329 RVA: 0x000D2F34 File Offset: 0x000D1F34
		public override bool oncopy(IHTMLEventObj A_1)
		{
			return this.m_oncopyDelegate != null && this.m_oncopyDelegate(A_1);
		}

		// Token: 0x06018BD2 RID: 101330 RVA: 0x000D2F64 File Offset: 0x000D1F64
		public override bool onbeforecopy(IHTMLEventObj A_1)
		{
			return this.m_onbeforecopyDelegate != null && this.m_onbeforecopyDelegate(A_1);
		}

		// Token: 0x06018BD3 RID: 101331 RVA: 0x000D2F94 File Offset: 0x000D1F94
		public override bool oncut(IHTMLEventObj A_1)
		{
			return this.m_oncutDelegate != null && this.m_oncutDelegate(A_1);
		}

		// Token: 0x06018BD4 RID: 101332 RVA: 0x000D2FC4 File Offset: 0x000D1FC4
		public override bool onbeforecut(IHTMLEventObj A_1)
		{
			return this.m_onbeforecutDelegate != null && this.m_onbeforecutDelegate(A_1);
		}

		// Token: 0x06018BD5 RID: 101333 RVA: 0x000D2FF4 File Offset: 0x000D1FF4
		public override bool ondrop(IHTMLEventObj A_1)
		{
			return this.m_ondropDelegate != null && this.m_ondropDelegate(A_1);
		}

		// Token: 0x06018BD6 RID: 101334 RVA: 0x000D3024 File Offset: 0x000D2024
		public override void ondragleave(IHTMLEventObj A_1)
		{
			if (this.m_ondragleaveDelegate != null)
			{
				this.m_ondragleaveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018BD7 RID: 101335 RVA: 0x000D3054 File Offset: 0x000D2054
		public override bool ondragover(IHTMLEventObj A_1)
		{
			return this.m_ondragoverDelegate != null && this.m_ondragoverDelegate(A_1);
		}

		// Token: 0x06018BD8 RID: 101336 RVA: 0x000D3084 File Offset: 0x000D2084
		public override bool ondragenter(IHTMLEventObj A_1)
		{
			return this.m_ondragenterDelegate != null && this.m_ondragenterDelegate(A_1);
		}

		// Token: 0x06018BD9 RID: 101337 RVA: 0x000D30B4 File Offset: 0x000D20B4
		public override void ondragend(IHTMLEventObj A_1)
		{
			if (this.m_ondragendDelegate != null)
			{
				this.m_ondragendDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018BDA RID: 101338 RVA: 0x000D30E4 File Offset: 0x000D20E4
		public override bool ondrag(IHTMLEventObj A_1)
		{
			return this.m_ondragDelegate != null && this.m_ondragDelegate(A_1);
		}

		// Token: 0x06018BDB RID: 101339 RVA: 0x000D3114 File Offset: 0x000D2114
		public override void onresize(IHTMLEventObj A_1)
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018BDC RID: 101340 RVA: 0x000D3144 File Offset: 0x000D2144
		public override void onblur(IHTMLEventObj A_1)
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018BDD RID: 101341 RVA: 0x000D3174 File Offset: 0x000D2174
		public override void onfocus(IHTMLEventObj A_1)
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018BDE RID: 101342 RVA: 0x000D31A4 File Offset: 0x000D21A4
		public override void onscroll(IHTMLEventObj A_1)
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018BDF RID: 101343 RVA: 0x000D31D4 File Offset: 0x000D21D4
		public override void onpropertychange(IHTMLEventObj A_1)
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018BE0 RID: 101344 RVA: 0x000D3204 File Offset: 0x000D2204
		public override void onlosecapture(IHTMLEventObj A_1)
		{
			if (this.m_onlosecaptureDelegate != null)
			{
				this.m_onlosecaptureDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018BE1 RID: 101345 RVA: 0x000D3234 File Offset: 0x000D2234
		public override void ondatasetcomplete(IHTMLEventObj A_1)
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018BE2 RID: 101346 RVA: 0x000D3264 File Offset: 0x000D2264
		public override void ondataavailable(IHTMLEventObj A_1)
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018BE3 RID: 101347 RVA: 0x000D3294 File Offset: 0x000D2294
		public override void ondatasetchanged(IHTMLEventObj A_1)
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018BE4 RID: 101348 RVA: 0x000D32C4 File Offset: 0x000D22C4
		public override void onrowenter(IHTMLEventObj A_1)
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018BE5 RID: 101349 RVA: 0x000D32F4 File Offset: 0x000D22F4
		public override bool onrowexit(IHTMLEventObj A_1)
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate(A_1);
		}

		// Token: 0x06018BE6 RID: 101350 RVA: 0x000D3324 File Offset: 0x000D2324
		public override bool onerrorupdate(IHTMLEventObj A_1)
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate(A_1);
		}

		// Token: 0x06018BE7 RID: 101351 RVA: 0x000D3354 File Offset: 0x000D2354
		public override void onafterupdate(IHTMLEventObj A_1)
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018BE8 RID: 101352 RVA: 0x000D3384 File Offset: 0x000D2384
		public override bool onbeforeupdate(IHTMLEventObj A_1)
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate(A_1);
		}

		// Token: 0x06018BE9 RID: 101353 RVA: 0x000D33B4 File Offset: 0x000D23B4
		public override bool ondragstart(IHTMLEventObj A_1)
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate(A_1);
		}

		// Token: 0x06018BEA RID: 101354 RVA: 0x000D33E4 File Offset: 0x000D23E4
		public override void onfilterchange(IHTMLEventObj A_1)
		{
			if (this.m_onfilterchangeDelegate != null)
			{
				this.m_onfilterchangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018BEB RID: 101355 RVA: 0x000D3414 File Offset: 0x000D2414
		public override bool onselectstart(IHTMLEventObj A_1)
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate(A_1);
		}

		// Token: 0x06018BEC RID: 101356 RVA: 0x000D3444 File Offset: 0x000D2444
		public override void onmouseup(IHTMLEventObj A_1)
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018BED RID: 101357 RVA: 0x000D3474 File Offset: 0x000D2474
		public override void onmousedown(IHTMLEventObj A_1)
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018BEE RID: 101358 RVA: 0x000D34A4 File Offset: 0x000D24A4
		public override void onmousemove(IHTMLEventObj A_1)
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018BEF RID: 101359 RVA: 0x000D34D4 File Offset: 0x000D24D4
		public override void onmouseover(IHTMLEventObj A_1)
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018BF0 RID: 101360 RVA: 0x000D3504 File Offset: 0x000D2504
		public override void onmouseout(IHTMLEventObj A_1)
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018BF1 RID: 101361 RVA: 0x000D3534 File Offset: 0x000D2534
		public override void onkeyup(IHTMLEventObj A_1)
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018BF2 RID: 101362 RVA: 0x000D3564 File Offset: 0x000D2564
		public override void onkeydown(IHTMLEventObj A_1)
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018BF3 RID: 101363 RVA: 0x000D3594 File Offset: 0x000D2594
		public override bool onkeypress(IHTMLEventObj A_1)
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate(A_1);
		}

		// Token: 0x06018BF4 RID: 101364 RVA: 0x000D35C4 File Offset: 0x000D25C4
		public override bool ondblclick(IHTMLEventObj A_1)
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate(A_1);
		}

		// Token: 0x06018BF5 RID: 101365 RVA: 0x000D35F4 File Offset: 0x000D25F4
		public override bool onclick(IHTMLEventObj A_1)
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate(A_1);
		}

		// Token: 0x06018BF6 RID: 101366 RVA: 0x000D3624 File Offset: 0x000D2624
		public override bool onhelp(IHTMLEventObj A_1)
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate(A_1);
		}

		// Token: 0x06018BF7 RID: 101367 RVA: 0x000D3654 File Offset: 0x000D2654
		internal HTMLAreaEvents2_SinkHelper()
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

		// Token: 0x04000CAD RID: 3245
		public HTMLAreaEvents2_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x04000CAE RID: 3246
		public HTMLAreaEvents2_onresizeendEventHandler m_onresizeendDelegate;

		// Token: 0x04000CAF RID: 3247
		public HTMLAreaEvents2_onresizestartEventHandler m_onresizestartDelegate;

		// Token: 0x04000CB0 RID: 3248
		public HTMLAreaEvents2_onmoveendEventHandler m_onmoveendDelegate;

		// Token: 0x04000CB1 RID: 3249
		public HTMLAreaEvents2_onmovestartEventHandler m_onmovestartDelegate;

		// Token: 0x04000CB2 RID: 3250
		public HTMLAreaEvents2_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x04000CB3 RID: 3251
		public HTMLAreaEvents2_onmoveEventHandler m_onmoveDelegate;

		// Token: 0x04000CB4 RID: 3252
		public HTMLAreaEvents2_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x04000CB5 RID: 3253
		public HTMLAreaEvents2_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x04000CB6 RID: 3254
		public HTMLAreaEvents2_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x04000CB7 RID: 3255
		public HTMLAreaEvents2_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x04000CB8 RID: 3256
		public HTMLAreaEvents2_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x04000CB9 RID: 3257
		public HTMLAreaEvents2_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x04000CBA RID: 3258
		public HTMLAreaEvents2_onmouseleaveEventHandler m_onmouseleaveDelegate;

		// Token: 0x04000CBB RID: 3259
		public HTMLAreaEvents2_onmouseenterEventHandler m_onmouseenterDelegate;

		// Token: 0x04000CBC RID: 3260
		public HTMLAreaEvents2_onpageEventHandler m_onpageDelegate;

		// Token: 0x04000CBD RID: 3261
		public HTMLAreaEvents2_onlayoutcompleteEventHandler m_onlayoutcompleteDelegate;

		// Token: 0x04000CBE RID: 3262
		public HTMLAreaEvents2_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x04000CBF RID: 3263
		public HTMLAreaEvents2_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x04000CC0 RID: 3264
		public HTMLAreaEvents2_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x04000CC1 RID: 3265
		public HTMLAreaEvents2_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x04000CC2 RID: 3266
		public HTMLAreaEvents2_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x04000CC3 RID: 3267
		public HTMLAreaEvents2_onpasteEventHandler m_onpasteDelegate;

		// Token: 0x04000CC4 RID: 3268
		public HTMLAreaEvents2_onbeforepasteEventHandler m_onbeforepasteDelegate;

		// Token: 0x04000CC5 RID: 3269
		public HTMLAreaEvents2_oncopyEventHandler m_oncopyDelegate;

		// Token: 0x04000CC6 RID: 3270
		public HTMLAreaEvents2_onbeforecopyEventHandler m_onbeforecopyDelegate;

		// Token: 0x04000CC7 RID: 3271
		public HTMLAreaEvents2_oncutEventHandler m_oncutDelegate;

		// Token: 0x04000CC8 RID: 3272
		public HTMLAreaEvents2_onbeforecutEventHandler m_onbeforecutDelegate;

		// Token: 0x04000CC9 RID: 3273
		public HTMLAreaEvents2_ondropEventHandler m_ondropDelegate;

		// Token: 0x04000CCA RID: 3274
		public HTMLAreaEvents2_ondragleaveEventHandler m_ondragleaveDelegate;

		// Token: 0x04000CCB RID: 3275
		public HTMLAreaEvents2_ondragoverEventHandler m_ondragoverDelegate;

		// Token: 0x04000CCC RID: 3276
		public HTMLAreaEvents2_ondragenterEventHandler m_ondragenterDelegate;

		// Token: 0x04000CCD RID: 3277
		public HTMLAreaEvents2_ondragendEventHandler m_ondragendDelegate;

		// Token: 0x04000CCE RID: 3278
		public HTMLAreaEvents2_ondragEventHandler m_ondragDelegate;

		// Token: 0x04000CCF RID: 3279
		public HTMLAreaEvents2_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x04000CD0 RID: 3280
		public HTMLAreaEvents2_onblurEventHandler m_onblurDelegate;

		// Token: 0x04000CD1 RID: 3281
		public HTMLAreaEvents2_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x04000CD2 RID: 3282
		public HTMLAreaEvents2_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x04000CD3 RID: 3283
		public HTMLAreaEvents2_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x04000CD4 RID: 3284
		public HTMLAreaEvents2_onlosecaptureEventHandler m_onlosecaptureDelegate;

		// Token: 0x04000CD5 RID: 3285
		public HTMLAreaEvents2_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x04000CD6 RID: 3286
		public HTMLAreaEvents2_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x04000CD7 RID: 3287
		public HTMLAreaEvents2_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x04000CD8 RID: 3288
		public HTMLAreaEvents2_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x04000CD9 RID: 3289
		public HTMLAreaEvents2_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x04000CDA RID: 3290
		public HTMLAreaEvents2_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x04000CDB RID: 3291
		public HTMLAreaEvents2_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x04000CDC RID: 3292
		public HTMLAreaEvents2_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x04000CDD RID: 3293
		public HTMLAreaEvents2_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x04000CDE RID: 3294
		public HTMLAreaEvents2_onfilterchangeEventHandler m_onfilterchangeDelegate;

		// Token: 0x04000CDF RID: 3295
		public HTMLAreaEvents2_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x04000CE0 RID: 3296
		public HTMLAreaEvents2_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x04000CE1 RID: 3297
		public HTMLAreaEvents2_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x04000CE2 RID: 3298
		public HTMLAreaEvents2_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x04000CE3 RID: 3299
		public HTMLAreaEvents2_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x04000CE4 RID: 3300
		public HTMLAreaEvents2_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x04000CE5 RID: 3301
		public HTMLAreaEvents2_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x04000CE6 RID: 3302
		public HTMLAreaEvents2_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x04000CE7 RID: 3303
		public HTMLAreaEvents2_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x04000CE8 RID: 3304
		public HTMLAreaEvents2_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x04000CE9 RID: 3305
		public HTMLAreaEvents2_onclickEventHandler m_onclickDelegate;

		// Token: 0x04000CEA RID: 3306
		public HTMLAreaEvents2_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x04000CEB RID: 3307
		public int m_dwCookie;
	}
}
