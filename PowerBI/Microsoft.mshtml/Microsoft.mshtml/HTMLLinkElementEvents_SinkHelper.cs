using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000E04 RID: 3588
	[ClassInterface(0)]
	internal sealed class HTMLLinkElementEvents_SinkHelper : HTMLLinkElementEvents
	{
		// Token: 0x06018C78 RID: 101496 RVA: 0x000D97F4 File Offset: 0x000D87F4
		public override void onerror()
		{
			if (this.m_onerrorDelegate != null)
			{
				this.m_onerrorDelegate();
				return;
			}
		}

		// Token: 0x06018C79 RID: 101497 RVA: 0x000D9820 File Offset: 0x000D8820
		public override void onload()
		{
			if (this.m_onloadDelegate != null)
			{
				this.m_onloadDelegate();
				return;
			}
		}

		// Token: 0x06018C7A RID: 101498 RVA: 0x000D984C File Offset: 0x000D884C
		public override void onfocusout()
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate();
				return;
			}
		}

		// Token: 0x06018C7B RID: 101499 RVA: 0x000D9878 File Offset: 0x000D8878
		public override void onfocusin()
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate();
				return;
			}
		}

		// Token: 0x06018C7C RID: 101500 RVA: 0x000D98A4 File Offset: 0x000D88A4
		public override void ondeactivate()
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate();
				return;
			}
		}

		// Token: 0x06018C7D RID: 101501 RVA: 0x000D98D0 File Offset: 0x000D88D0
		public override void onactivate()
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate();
				return;
			}
		}

		// Token: 0x06018C7E RID: 101502 RVA: 0x000D98FC File Offset: 0x000D88FC
		public override bool onmousewheel()
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate();
		}

		// Token: 0x06018C7F RID: 101503 RVA: 0x000D9928 File Offset: 0x000D8928
		public override void onmouseleave()
		{
			if (this.m_onmouseleaveDelegate != null)
			{
				this.m_onmouseleaveDelegate();
				return;
			}
		}

		// Token: 0x06018C80 RID: 101504 RVA: 0x000D9954 File Offset: 0x000D8954
		public override void onmouseenter()
		{
			if (this.m_onmouseenterDelegate != null)
			{
				this.m_onmouseenterDelegate();
				return;
			}
		}

		// Token: 0x06018C81 RID: 101505 RVA: 0x000D9980 File Offset: 0x000D8980
		public override void onresizeend()
		{
			if (this.m_onresizeendDelegate != null)
			{
				this.m_onresizeendDelegate();
				return;
			}
		}

		// Token: 0x06018C82 RID: 101506 RVA: 0x000D99AC File Offset: 0x000D89AC
		public override bool onresizestart()
		{
			return this.m_onresizestartDelegate != null && this.m_onresizestartDelegate();
		}

		// Token: 0x06018C83 RID: 101507 RVA: 0x000D99D8 File Offset: 0x000D89D8
		public override void onmoveend()
		{
			if (this.m_onmoveendDelegate != null)
			{
				this.m_onmoveendDelegate();
				return;
			}
		}

		// Token: 0x06018C84 RID: 101508 RVA: 0x000D9A04 File Offset: 0x000D8A04
		public override bool onmovestart()
		{
			return this.m_onmovestartDelegate != null && this.m_onmovestartDelegate();
		}

		// Token: 0x06018C85 RID: 101509 RVA: 0x000D9A30 File Offset: 0x000D8A30
		public override bool oncontrolselect()
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate();
		}

		// Token: 0x06018C86 RID: 101510 RVA: 0x000D9A5C File Offset: 0x000D8A5C
		public override void onmove()
		{
			if (this.m_onmoveDelegate != null)
			{
				this.m_onmoveDelegate();
				return;
			}
		}

		// Token: 0x06018C87 RID: 101511 RVA: 0x000D9A88 File Offset: 0x000D8A88
		public override bool onbeforeactivate()
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate();
		}

		// Token: 0x06018C88 RID: 101512 RVA: 0x000D9AB4 File Offset: 0x000D8AB4
		public override bool onbeforedeactivate()
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate();
		}

		// Token: 0x06018C89 RID: 101513 RVA: 0x000D9AE0 File Offset: 0x000D8AE0
		public override void onpage()
		{
			if (this.m_onpageDelegate != null)
			{
				this.m_onpageDelegate();
				return;
			}
		}

		// Token: 0x06018C8A RID: 101514 RVA: 0x000D9B0C File Offset: 0x000D8B0C
		public override void onlayoutcomplete()
		{
			if (this.m_onlayoutcompleteDelegate != null)
			{
				this.m_onlayoutcompleteDelegate();
				return;
			}
		}

		// Token: 0x06018C8B RID: 101515 RVA: 0x000D9B38 File Offset: 0x000D8B38
		public override void onbeforeeditfocus()
		{
			if (this.m_onbeforeeditfocusDelegate != null)
			{
				this.m_onbeforeeditfocusDelegate();
				return;
			}
		}

		// Token: 0x06018C8C RID: 101516 RVA: 0x000D9B64 File Offset: 0x000D8B64
		public override void onreadystatechange()
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate();
				return;
			}
		}

		// Token: 0x06018C8D RID: 101517 RVA: 0x000D9B90 File Offset: 0x000D8B90
		public override void oncellchange()
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate();
				return;
			}
		}

		// Token: 0x06018C8E RID: 101518 RVA: 0x000D9BBC File Offset: 0x000D8BBC
		public override void onrowsinserted()
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate();
				return;
			}
		}

		// Token: 0x06018C8F RID: 101519 RVA: 0x000D9BE8 File Offset: 0x000D8BE8
		public override void onrowsdelete()
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate();
				return;
			}
		}

		// Token: 0x06018C90 RID: 101520 RVA: 0x000D9C14 File Offset: 0x000D8C14
		public override bool oncontextmenu()
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate();
		}

		// Token: 0x06018C91 RID: 101521 RVA: 0x000D9C40 File Offset: 0x000D8C40
		public override bool onpaste()
		{
			return this.m_onpasteDelegate != null && this.m_onpasteDelegate();
		}

		// Token: 0x06018C92 RID: 101522 RVA: 0x000D9C6C File Offset: 0x000D8C6C
		public override bool onbeforepaste()
		{
			return this.m_onbeforepasteDelegate != null && this.m_onbeforepasteDelegate();
		}

		// Token: 0x06018C93 RID: 101523 RVA: 0x000D9C98 File Offset: 0x000D8C98
		public override bool oncopy()
		{
			return this.m_oncopyDelegate != null && this.m_oncopyDelegate();
		}

		// Token: 0x06018C94 RID: 101524 RVA: 0x000D9CC4 File Offset: 0x000D8CC4
		public override bool onbeforecopy()
		{
			return this.m_onbeforecopyDelegate != null && this.m_onbeforecopyDelegate();
		}

		// Token: 0x06018C95 RID: 101525 RVA: 0x000D9CF0 File Offset: 0x000D8CF0
		public override bool oncut()
		{
			return this.m_oncutDelegate != null && this.m_oncutDelegate();
		}

		// Token: 0x06018C96 RID: 101526 RVA: 0x000D9D1C File Offset: 0x000D8D1C
		public override bool onbeforecut()
		{
			return this.m_onbeforecutDelegate != null && this.m_onbeforecutDelegate();
		}

		// Token: 0x06018C97 RID: 101527 RVA: 0x000D9D48 File Offset: 0x000D8D48
		public override bool ondrop()
		{
			return this.m_ondropDelegate != null && this.m_ondropDelegate();
		}

		// Token: 0x06018C98 RID: 101528 RVA: 0x000D9D74 File Offset: 0x000D8D74
		public override void ondragleave()
		{
			if (this.m_ondragleaveDelegate != null)
			{
				this.m_ondragleaveDelegate();
				return;
			}
		}

		// Token: 0x06018C99 RID: 101529 RVA: 0x000D9DA0 File Offset: 0x000D8DA0
		public override bool ondragover()
		{
			return this.m_ondragoverDelegate != null && this.m_ondragoverDelegate();
		}

		// Token: 0x06018C9A RID: 101530 RVA: 0x000D9DCC File Offset: 0x000D8DCC
		public override bool ondragenter()
		{
			return this.m_ondragenterDelegate != null && this.m_ondragenterDelegate();
		}

		// Token: 0x06018C9B RID: 101531 RVA: 0x000D9DF8 File Offset: 0x000D8DF8
		public override void ondragend()
		{
			if (this.m_ondragendDelegate != null)
			{
				this.m_ondragendDelegate();
				return;
			}
		}

		// Token: 0x06018C9C RID: 101532 RVA: 0x000D9E24 File Offset: 0x000D8E24
		public override bool ondrag()
		{
			return this.m_ondragDelegate != null && this.m_ondragDelegate();
		}

		// Token: 0x06018C9D RID: 101533 RVA: 0x000D9E50 File Offset: 0x000D8E50
		public override void onresize()
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate();
				return;
			}
		}

		// Token: 0x06018C9E RID: 101534 RVA: 0x000D9E7C File Offset: 0x000D8E7C
		public override void onblur()
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate();
				return;
			}
		}

		// Token: 0x06018C9F RID: 101535 RVA: 0x000D9EA8 File Offset: 0x000D8EA8
		public override void onfocus()
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate();
				return;
			}
		}

		// Token: 0x06018CA0 RID: 101536 RVA: 0x000D9ED4 File Offset: 0x000D8ED4
		public override void onscroll()
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate();
				return;
			}
		}

		// Token: 0x06018CA1 RID: 101537 RVA: 0x000D9F00 File Offset: 0x000D8F00
		public override void onpropertychange()
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate();
				return;
			}
		}

		// Token: 0x06018CA2 RID: 101538 RVA: 0x000D9F2C File Offset: 0x000D8F2C
		public override void onlosecapture()
		{
			if (this.m_onlosecaptureDelegate != null)
			{
				this.m_onlosecaptureDelegate();
				return;
			}
		}

		// Token: 0x06018CA3 RID: 101539 RVA: 0x000D9F58 File Offset: 0x000D8F58
		public override void ondatasetcomplete()
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate();
				return;
			}
		}

		// Token: 0x06018CA4 RID: 101540 RVA: 0x000D9F84 File Offset: 0x000D8F84
		public override void ondataavailable()
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate();
				return;
			}
		}

		// Token: 0x06018CA5 RID: 101541 RVA: 0x000D9FB0 File Offset: 0x000D8FB0
		public override void ondatasetchanged()
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate();
				return;
			}
		}

		// Token: 0x06018CA6 RID: 101542 RVA: 0x000D9FDC File Offset: 0x000D8FDC
		public override void onrowenter()
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate();
				return;
			}
		}

		// Token: 0x06018CA7 RID: 101543 RVA: 0x000DA008 File Offset: 0x000D9008
		public override bool onrowexit()
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate();
		}

		// Token: 0x06018CA8 RID: 101544 RVA: 0x000DA034 File Offset: 0x000D9034
		public override bool onerrorupdate()
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate();
		}

		// Token: 0x06018CA9 RID: 101545 RVA: 0x000DA060 File Offset: 0x000D9060
		public override void onafterupdate()
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate();
				return;
			}
		}

		// Token: 0x06018CAA RID: 101546 RVA: 0x000DA08C File Offset: 0x000D908C
		public override bool onbeforeupdate()
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate();
		}

		// Token: 0x06018CAB RID: 101547 RVA: 0x000DA0B8 File Offset: 0x000D90B8
		public override bool ondragstart()
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate();
		}

		// Token: 0x06018CAC RID: 101548 RVA: 0x000DA0E4 File Offset: 0x000D90E4
		public override void onfilterchange()
		{
			if (this.m_onfilterchangeDelegate != null)
			{
				this.m_onfilterchangeDelegate();
				return;
			}
		}

		// Token: 0x06018CAD RID: 101549 RVA: 0x000DA110 File Offset: 0x000D9110
		public override bool onselectstart()
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate();
		}

		// Token: 0x06018CAE RID: 101550 RVA: 0x000DA13C File Offset: 0x000D913C
		public override void onmouseup()
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate();
				return;
			}
		}

		// Token: 0x06018CAF RID: 101551 RVA: 0x000DA168 File Offset: 0x000D9168
		public override void onmousedown()
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate();
				return;
			}
		}

		// Token: 0x06018CB0 RID: 101552 RVA: 0x000DA194 File Offset: 0x000D9194
		public override void onmousemove()
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate();
				return;
			}
		}

		// Token: 0x06018CB1 RID: 101553 RVA: 0x000DA1C0 File Offset: 0x000D91C0
		public override void onmouseover()
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate();
				return;
			}
		}

		// Token: 0x06018CB2 RID: 101554 RVA: 0x000DA1EC File Offset: 0x000D91EC
		public override void onmouseout()
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate();
				return;
			}
		}

		// Token: 0x06018CB3 RID: 101555 RVA: 0x000DA218 File Offset: 0x000D9218
		public override void onkeyup()
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate();
				return;
			}
		}

		// Token: 0x06018CB4 RID: 101556 RVA: 0x000DA244 File Offset: 0x000D9244
		public override void onkeydown()
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate();
				return;
			}
		}

		// Token: 0x06018CB5 RID: 101557 RVA: 0x000DA270 File Offset: 0x000D9270
		public override bool onkeypress()
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate();
		}

		// Token: 0x06018CB6 RID: 101558 RVA: 0x000DA29C File Offset: 0x000D929C
		public override bool ondblclick()
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate();
		}

		// Token: 0x06018CB7 RID: 101559 RVA: 0x000DA2C8 File Offset: 0x000D92C8
		public override bool onclick()
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate();
		}

		// Token: 0x06018CB8 RID: 101560 RVA: 0x000DA2F4 File Offset: 0x000D92F4
		public override bool onhelp()
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate();
		}

		// Token: 0x06018CB9 RID: 101561 RVA: 0x000DA320 File Offset: 0x000D9320
		internal HTMLLinkElementEvents_SinkHelper()
		{
			this.m_dwCookie = 0;
			this.m_onerrorDelegate = null;
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

		// Token: 0x04000CEF RID: 3311
		public HTMLLinkElementEvents_onerrorEventHandler m_onerrorDelegate;

		// Token: 0x04000CF0 RID: 3312
		public HTMLLinkElementEvents_onloadEventHandler m_onloadDelegate;

		// Token: 0x04000CF1 RID: 3313
		public HTMLLinkElementEvents_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x04000CF2 RID: 3314
		public HTMLLinkElementEvents_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x04000CF3 RID: 3315
		public HTMLLinkElementEvents_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x04000CF4 RID: 3316
		public HTMLLinkElementEvents_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x04000CF5 RID: 3317
		public HTMLLinkElementEvents_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x04000CF6 RID: 3318
		public HTMLLinkElementEvents_onmouseleaveEventHandler m_onmouseleaveDelegate;

		// Token: 0x04000CF7 RID: 3319
		public HTMLLinkElementEvents_onmouseenterEventHandler m_onmouseenterDelegate;

		// Token: 0x04000CF8 RID: 3320
		public HTMLLinkElementEvents_onresizeendEventHandler m_onresizeendDelegate;

		// Token: 0x04000CF9 RID: 3321
		public HTMLLinkElementEvents_onresizestartEventHandler m_onresizestartDelegate;

		// Token: 0x04000CFA RID: 3322
		public HTMLLinkElementEvents_onmoveendEventHandler m_onmoveendDelegate;

		// Token: 0x04000CFB RID: 3323
		public HTMLLinkElementEvents_onmovestartEventHandler m_onmovestartDelegate;

		// Token: 0x04000CFC RID: 3324
		public HTMLLinkElementEvents_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x04000CFD RID: 3325
		public HTMLLinkElementEvents_onmoveEventHandler m_onmoveDelegate;

		// Token: 0x04000CFE RID: 3326
		public HTMLLinkElementEvents_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x04000CFF RID: 3327
		public HTMLLinkElementEvents_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x04000D00 RID: 3328
		public HTMLLinkElementEvents_onpageEventHandler m_onpageDelegate;

		// Token: 0x04000D01 RID: 3329
		public HTMLLinkElementEvents_onlayoutcompleteEventHandler m_onlayoutcompleteDelegate;

		// Token: 0x04000D02 RID: 3330
		public HTMLLinkElementEvents_onbeforeeditfocusEventHandler m_onbeforeeditfocusDelegate;

		// Token: 0x04000D03 RID: 3331
		public HTMLLinkElementEvents_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x04000D04 RID: 3332
		public HTMLLinkElementEvents_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x04000D05 RID: 3333
		public HTMLLinkElementEvents_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x04000D06 RID: 3334
		public HTMLLinkElementEvents_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x04000D07 RID: 3335
		public HTMLLinkElementEvents_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x04000D08 RID: 3336
		public HTMLLinkElementEvents_onpasteEventHandler m_onpasteDelegate;

		// Token: 0x04000D09 RID: 3337
		public HTMLLinkElementEvents_onbeforepasteEventHandler m_onbeforepasteDelegate;

		// Token: 0x04000D0A RID: 3338
		public HTMLLinkElementEvents_oncopyEventHandler m_oncopyDelegate;

		// Token: 0x04000D0B RID: 3339
		public HTMLLinkElementEvents_onbeforecopyEventHandler m_onbeforecopyDelegate;

		// Token: 0x04000D0C RID: 3340
		public HTMLLinkElementEvents_oncutEventHandler m_oncutDelegate;

		// Token: 0x04000D0D RID: 3341
		public HTMLLinkElementEvents_onbeforecutEventHandler m_onbeforecutDelegate;

		// Token: 0x04000D0E RID: 3342
		public HTMLLinkElementEvents_ondropEventHandler m_ondropDelegate;

		// Token: 0x04000D0F RID: 3343
		public HTMLLinkElementEvents_ondragleaveEventHandler m_ondragleaveDelegate;

		// Token: 0x04000D10 RID: 3344
		public HTMLLinkElementEvents_ondragoverEventHandler m_ondragoverDelegate;

		// Token: 0x04000D11 RID: 3345
		public HTMLLinkElementEvents_ondragenterEventHandler m_ondragenterDelegate;

		// Token: 0x04000D12 RID: 3346
		public HTMLLinkElementEvents_ondragendEventHandler m_ondragendDelegate;

		// Token: 0x04000D13 RID: 3347
		public HTMLLinkElementEvents_ondragEventHandler m_ondragDelegate;

		// Token: 0x04000D14 RID: 3348
		public HTMLLinkElementEvents_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x04000D15 RID: 3349
		public HTMLLinkElementEvents_onblurEventHandler m_onblurDelegate;

		// Token: 0x04000D16 RID: 3350
		public HTMLLinkElementEvents_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x04000D17 RID: 3351
		public HTMLLinkElementEvents_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x04000D18 RID: 3352
		public HTMLLinkElementEvents_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x04000D19 RID: 3353
		public HTMLLinkElementEvents_onlosecaptureEventHandler m_onlosecaptureDelegate;

		// Token: 0x04000D1A RID: 3354
		public HTMLLinkElementEvents_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x04000D1B RID: 3355
		public HTMLLinkElementEvents_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x04000D1C RID: 3356
		public HTMLLinkElementEvents_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x04000D1D RID: 3357
		public HTMLLinkElementEvents_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x04000D1E RID: 3358
		public HTMLLinkElementEvents_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x04000D1F RID: 3359
		public HTMLLinkElementEvents_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x04000D20 RID: 3360
		public HTMLLinkElementEvents_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x04000D21 RID: 3361
		public HTMLLinkElementEvents_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x04000D22 RID: 3362
		public HTMLLinkElementEvents_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x04000D23 RID: 3363
		public HTMLLinkElementEvents_onfilterchangeEventHandler m_onfilterchangeDelegate;

		// Token: 0x04000D24 RID: 3364
		public HTMLLinkElementEvents_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x04000D25 RID: 3365
		public HTMLLinkElementEvents_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x04000D26 RID: 3366
		public HTMLLinkElementEvents_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x04000D27 RID: 3367
		public HTMLLinkElementEvents_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x04000D28 RID: 3368
		public HTMLLinkElementEvents_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x04000D29 RID: 3369
		public HTMLLinkElementEvents_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x04000D2A RID: 3370
		public HTMLLinkElementEvents_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x04000D2B RID: 3371
		public HTMLLinkElementEvents_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x04000D2C RID: 3372
		public HTMLLinkElementEvents_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x04000D2D RID: 3373
		public HTMLLinkElementEvents_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x04000D2E RID: 3374
		public HTMLLinkElementEvents_onclickEventHandler m_onclickDelegate;

		// Token: 0x04000D2F RID: 3375
		public HTMLLinkElementEvents_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x04000D30 RID: 3376
		public int m_dwCookie;
	}
}
