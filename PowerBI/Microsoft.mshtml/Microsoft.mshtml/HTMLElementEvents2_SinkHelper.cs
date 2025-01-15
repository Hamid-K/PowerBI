using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DFA RID: 3578
	[ClassInterface(0)]
	internal sealed class HTMLElementEvents2_SinkHelper : HTMLElementEvents2
	{
		// Token: 0x0601889F RID: 100511 RVA: 0x000B677C File Offset: 0x000B577C
		public override bool onmousewheel(IHTMLEventObj A_1)
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate(A_1);
		}

		// Token: 0x060188A0 RID: 100512 RVA: 0x000B67AC File Offset: 0x000B57AC
		public override void onresizeend(IHTMLEventObj A_1)
		{
			if (this.m_onresizeendDelegate != null)
			{
				this.m_onresizeendDelegate(A_1);
				return;
			}
		}

		// Token: 0x060188A1 RID: 100513 RVA: 0x000B67DC File Offset: 0x000B57DC
		public override bool onresizestart(IHTMLEventObj A_1)
		{
			return this.m_onresizestartDelegate != null && this.m_onresizestartDelegate(A_1);
		}

		// Token: 0x060188A2 RID: 100514 RVA: 0x000B680C File Offset: 0x000B580C
		public override void onmoveend(IHTMLEventObj A_1)
		{
			if (this.m_onmoveendDelegate != null)
			{
				this.m_onmoveendDelegate(A_1);
				return;
			}
		}

		// Token: 0x060188A3 RID: 100515 RVA: 0x000B683C File Offset: 0x000B583C
		public override bool onmovestart(IHTMLEventObj A_1)
		{
			return this.m_onmovestartDelegate != null && this.m_onmovestartDelegate(A_1);
		}

		// Token: 0x060188A4 RID: 100516 RVA: 0x000B686C File Offset: 0x000B586C
		public override bool oncontrolselect(IHTMLEventObj A_1)
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate(A_1);
		}

		// Token: 0x060188A5 RID: 100517 RVA: 0x000B689C File Offset: 0x000B589C
		public override void onmove(IHTMLEventObj A_1)
		{
			if (this.m_onmoveDelegate != null)
			{
				this.m_onmoveDelegate(A_1);
				return;
			}
		}

		// Token: 0x060188A6 RID: 100518 RVA: 0x000B68CC File Offset: 0x000B58CC
		public override void onfocusout(IHTMLEventObj A_1)
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate(A_1);
				return;
			}
		}

		// Token: 0x060188A7 RID: 100519 RVA: 0x000B68FC File Offset: 0x000B58FC
		public override void onfocusin(IHTMLEventObj A_1)
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate(A_1);
				return;
			}
		}

		// Token: 0x060188A8 RID: 100520 RVA: 0x000B692C File Offset: 0x000B592C
		public override bool onbeforeactivate(IHTMLEventObj A_1)
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate(A_1);
		}

		// Token: 0x060188A9 RID: 100521 RVA: 0x000B695C File Offset: 0x000B595C
		public override bool onbeforedeactivate(IHTMLEventObj A_1)
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate(A_1);
		}

		// Token: 0x060188AA RID: 100522 RVA: 0x000B698C File Offset: 0x000B598C
		public override void ondeactivate(IHTMLEventObj A_1)
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate(A_1);
				return;
			}
		}

		// Token: 0x060188AB RID: 100523 RVA: 0x000B69BC File Offset: 0x000B59BC
		public override void onactivate(IHTMLEventObj A_1)
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate(A_1);
				return;
			}
		}

		// Token: 0x060188AC RID: 100524 RVA: 0x000B69EC File Offset: 0x000B59EC
		public override void onmouseleave(IHTMLEventObj A_1)
		{
			if (this.m_onmouseleaveDelegate != null)
			{
				this.m_onmouseleaveDelegate(A_1);
				return;
			}
		}

		// Token: 0x060188AD RID: 100525 RVA: 0x000B6A1C File Offset: 0x000B5A1C
		public override void onmouseenter(IHTMLEventObj A_1)
		{
			if (this.m_onmouseenterDelegate != null)
			{
				this.m_onmouseenterDelegate(A_1);
				return;
			}
		}

		// Token: 0x060188AE RID: 100526 RVA: 0x000B6A4C File Offset: 0x000B5A4C
		public override void onpage(IHTMLEventObj A_1)
		{
			if (this.m_onpageDelegate != null)
			{
				this.m_onpageDelegate(A_1);
				return;
			}
		}

		// Token: 0x060188AF RID: 100527 RVA: 0x000B6A7C File Offset: 0x000B5A7C
		public override void onlayoutcomplete(IHTMLEventObj A_1)
		{
			if (this.m_onlayoutcompleteDelegate != null)
			{
				this.m_onlayoutcompleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x060188B0 RID: 100528 RVA: 0x000B6AAC File Offset: 0x000B5AAC
		public override void onreadystatechange(IHTMLEventObj A_1)
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x060188B1 RID: 100529 RVA: 0x000B6ADC File Offset: 0x000B5ADC
		public override void oncellchange(IHTMLEventObj A_1)
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x060188B2 RID: 100530 RVA: 0x000B6B0C File Offset: 0x000B5B0C
		public override void onrowsinserted(IHTMLEventObj A_1)
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate(A_1);
				return;
			}
		}

		// Token: 0x060188B3 RID: 100531 RVA: 0x000B6B3C File Offset: 0x000B5B3C
		public override void onrowsdelete(IHTMLEventObj A_1)
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x060188B4 RID: 100532 RVA: 0x000B6B6C File Offset: 0x000B5B6C
		public override bool oncontextmenu(IHTMLEventObj A_1)
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate(A_1);
		}

		// Token: 0x060188B5 RID: 100533 RVA: 0x000B6B9C File Offset: 0x000B5B9C
		public override bool onpaste(IHTMLEventObj A_1)
		{
			return this.m_onpasteDelegate != null && this.m_onpasteDelegate(A_1);
		}

		// Token: 0x060188B6 RID: 100534 RVA: 0x000B6BCC File Offset: 0x000B5BCC
		public override bool onbeforepaste(IHTMLEventObj A_1)
		{
			return this.m_onbeforepasteDelegate != null && this.m_onbeforepasteDelegate(A_1);
		}

		// Token: 0x060188B7 RID: 100535 RVA: 0x000B6BFC File Offset: 0x000B5BFC
		public override bool oncopy(IHTMLEventObj A_1)
		{
			return this.m_oncopyDelegate != null && this.m_oncopyDelegate(A_1);
		}

		// Token: 0x060188B8 RID: 100536 RVA: 0x000B6C2C File Offset: 0x000B5C2C
		public override bool onbeforecopy(IHTMLEventObj A_1)
		{
			return this.m_onbeforecopyDelegate != null && this.m_onbeforecopyDelegate(A_1);
		}

		// Token: 0x060188B9 RID: 100537 RVA: 0x000B6C5C File Offset: 0x000B5C5C
		public override bool oncut(IHTMLEventObj A_1)
		{
			return this.m_oncutDelegate != null && this.m_oncutDelegate(A_1);
		}

		// Token: 0x060188BA RID: 100538 RVA: 0x000B6C8C File Offset: 0x000B5C8C
		public override bool onbeforecut(IHTMLEventObj A_1)
		{
			return this.m_onbeforecutDelegate != null && this.m_onbeforecutDelegate(A_1);
		}

		// Token: 0x060188BB RID: 100539 RVA: 0x000B6CBC File Offset: 0x000B5CBC
		public override bool ondrop(IHTMLEventObj A_1)
		{
			return this.m_ondropDelegate != null && this.m_ondropDelegate(A_1);
		}

		// Token: 0x060188BC RID: 100540 RVA: 0x000B6CEC File Offset: 0x000B5CEC
		public override void ondragleave(IHTMLEventObj A_1)
		{
			if (this.m_ondragleaveDelegate != null)
			{
				this.m_ondragleaveDelegate(A_1);
				return;
			}
		}

		// Token: 0x060188BD RID: 100541 RVA: 0x000B6D1C File Offset: 0x000B5D1C
		public override bool ondragover(IHTMLEventObj A_1)
		{
			return this.m_ondragoverDelegate != null && this.m_ondragoverDelegate(A_1);
		}

		// Token: 0x060188BE RID: 100542 RVA: 0x000B6D4C File Offset: 0x000B5D4C
		public override bool ondragenter(IHTMLEventObj A_1)
		{
			return this.m_ondragenterDelegate != null && this.m_ondragenterDelegate(A_1);
		}

		// Token: 0x060188BF RID: 100543 RVA: 0x000B6D7C File Offset: 0x000B5D7C
		public override void ondragend(IHTMLEventObj A_1)
		{
			if (this.m_ondragendDelegate != null)
			{
				this.m_ondragendDelegate(A_1);
				return;
			}
		}

		// Token: 0x060188C0 RID: 100544 RVA: 0x000B6DAC File Offset: 0x000B5DAC
		public override bool ondrag(IHTMLEventObj A_1)
		{
			return this.m_ondragDelegate != null && this.m_ondragDelegate(A_1);
		}

		// Token: 0x060188C1 RID: 100545 RVA: 0x000B6DDC File Offset: 0x000B5DDC
		public override void onresize(IHTMLEventObj A_1)
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate(A_1);
				return;
			}
		}

		// Token: 0x060188C2 RID: 100546 RVA: 0x000B6E0C File Offset: 0x000B5E0C
		public override void onblur(IHTMLEventObj A_1)
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate(A_1);
				return;
			}
		}

		// Token: 0x060188C3 RID: 100547 RVA: 0x000B6E3C File Offset: 0x000B5E3C
		public override void onfocus(IHTMLEventObj A_1)
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate(A_1);
				return;
			}
		}

		// Token: 0x060188C4 RID: 100548 RVA: 0x000B6E6C File Offset: 0x000B5E6C
		public override void onscroll(IHTMLEventObj A_1)
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate(A_1);
				return;
			}
		}

		// Token: 0x060188C5 RID: 100549 RVA: 0x000B6E9C File Offset: 0x000B5E9C
		public override void onpropertychange(IHTMLEventObj A_1)
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x060188C6 RID: 100550 RVA: 0x000B6ECC File Offset: 0x000B5ECC
		public override void onlosecapture(IHTMLEventObj A_1)
		{
			if (this.m_onlosecaptureDelegate != null)
			{
				this.m_onlosecaptureDelegate(A_1);
				return;
			}
		}

		// Token: 0x060188C7 RID: 100551 RVA: 0x000B6EFC File Offset: 0x000B5EFC
		public override void ondatasetcomplete(IHTMLEventObj A_1)
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x060188C8 RID: 100552 RVA: 0x000B6F2C File Offset: 0x000B5F2C
		public override void ondataavailable(IHTMLEventObj A_1)
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate(A_1);
				return;
			}
		}

		// Token: 0x060188C9 RID: 100553 RVA: 0x000B6F5C File Offset: 0x000B5F5C
		public override void ondatasetchanged(IHTMLEventObj A_1)
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate(A_1);
				return;
			}
		}

		// Token: 0x060188CA RID: 100554 RVA: 0x000B6F8C File Offset: 0x000B5F8C
		public override void onrowenter(IHTMLEventObj A_1)
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate(A_1);
				return;
			}
		}

		// Token: 0x060188CB RID: 100555 RVA: 0x000B6FBC File Offset: 0x000B5FBC
		public override bool onrowexit(IHTMLEventObj A_1)
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate(A_1);
		}

		// Token: 0x060188CC RID: 100556 RVA: 0x000B6FEC File Offset: 0x000B5FEC
		public override bool onerrorupdate(IHTMLEventObj A_1)
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate(A_1);
		}

		// Token: 0x060188CD RID: 100557 RVA: 0x000B701C File Offset: 0x000B601C
		public override void onafterupdate(IHTMLEventObj A_1)
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate(A_1);
				return;
			}
		}

		// Token: 0x060188CE RID: 100558 RVA: 0x000B704C File Offset: 0x000B604C
		public override bool onbeforeupdate(IHTMLEventObj A_1)
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate(A_1);
		}

		// Token: 0x060188CF RID: 100559 RVA: 0x000B707C File Offset: 0x000B607C
		public override bool ondragstart(IHTMLEventObj A_1)
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate(A_1);
		}

		// Token: 0x060188D0 RID: 100560 RVA: 0x000B70AC File Offset: 0x000B60AC
		public override void onfilterchange(IHTMLEventObj A_1)
		{
			if (this.m_onfilterchangeDelegate != null)
			{
				this.m_onfilterchangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x060188D1 RID: 100561 RVA: 0x000B70DC File Offset: 0x000B60DC
		public override bool onselectstart(IHTMLEventObj A_1)
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate(A_1);
		}

		// Token: 0x060188D2 RID: 100562 RVA: 0x000B710C File Offset: 0x000B610C
		public override void onmouseup(IHTMLEventObj A_1)
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate(A_1);
				return;
			}
		}

		// Token: 0x060188D3 RID: 100563 RVA: 0x000B713C File Offset: 0x000B613C
		public override void onmousedown(IHTMLEventObj A_1)
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate(A_1);
				return;
			}
		}

		// Token: 0x060188D4 RID: 100564 RVA: 0x000B716C File Offset: 0x000B616C
		public override void onmousemove(IHTMLEventObj A_1)
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate(A_1);
				return;
			}
		}

		// Token: 0x060188D5 RID: 100565 RVA: 0x000B719C File Offset: 0x000B619C
		public override void onmouseover(IHTMLEventObj A_1)
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate(A_1);
				return;
			}
		}

		// Token: 0x060188D6 RID: 100566 RVA: 0x000B71CC File Offset: 0x000B61CC
		public override void onmouseout(IHTMLEventObj A_1)
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate(A_1);
				return;
			}
		}

		// Token: 0x060188D7 RID: 100567 RVA: 0x000B71FC File Offset: 0x000B61FC
		public override void onkeyup(IHTMLEventObj A_1)
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate(A_1);
				return;
			}
		}

		// Token: 0x060188D8 RID: 100568 RVA: 0x000B722C File Offset: 0x000B622C
		public override void onkeydown(IHTMLEventObj A_1)
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate(A_1);
				return;
			}
		}

		// Token: 0x060188D9 RID: 100569 RVA: 0x000B725C File Offset: 0x000B625C
		public override bool onkeypress(IHTMLEventObj A_1)
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate(A_1);
		}

		// Token: 0x060188DA RID: 100570 RVA: 0x000B728C File Offset: 0x000B628C
		public override bool ondblclick(IHTMLEventObj A_1)
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate(A_1);
		}

		// Token: 0x060188DB RID: 100571 RVA: 0x000B72BC File Offset: 0x000B62BC
		public override bool onclick(IHTMLEventObj A_1)
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate(A_1);
		}

		// Token: 0x060188DC RID: 100572 RVA: 0x000B72EC File Offset: 0x000B62EC
		public override bool onhelp(IHTMLEventObj A_1)
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate(A_1);
		}

		// Token: 0x060188DD RID: 100573 RVA: 0x000B731C File Offset: 0x000B631C
		internal HTMLElementEvents2_SinkHelper()
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

		// Token: 0x04000B9B RID: 2971
		public HTMLElementEvents2_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x04000B9C RID: 2972
		public HTMLElementEvents2_onresizeendEventHandler m_onresizeendDelegate;

		// Token: 0x04000B9D RID: 2973
		public HTMLElementEvents2_onresizestartEventHandler m_onresizestartDelegate;

		// Token: 0x04000B9E RID: 2974
		public HTMLElementEvents2_onmoveendEventHandler m_onmoveendDelegate;

		// Token: 0x04000B9F RID: 2975
		public HTMLElementEvents2_onmovestartEventHandler m_onmovestartDelegate;

		// Token: 0x04000BA0 RID: 2976
		public HTMLElementEvents2_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x04000BA1 RID: 2977
		public HTMLElementEvents2_onmoveEventHandler m_onmoveDelegate;

		// Token: 0x04000BA2 RID: 2978
		public HTMLElementEvents2_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x04000BA3 RID: 2979
		public HTMLElementEvents2_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x04000BA4 RID: 2980
		public HTMLElementEvents2_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x04000BA5 RID: 2981
		public HTMLElementEvents2_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x04000BA6 RID: 2982
		public HTMLElementEvents2_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x04000BA7 RID: 2983
		public HTMLElementEvents2_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x04000BA8 RID: 2984
		public HTMLElementEvents2_onmouseleaveEventHandler m_onmouseleaveDelegate;

		// Token: 0x04000BA9 RID: 2985
		public HTMLElementEvents2_onmouseenterEventHandler m_onmouseenterDelegate;

		// Token: 0x04000BAA RID: 2986
		public HTMLElementEvents2_onpageEventHandler m_onpageDelegate;

		// Token: 0x04000BAB RID: 2987
		public HTMLElementEvents2_onlayoutcompleteEventHandler m_onlayoutcompleteDelegate;

		// Token: 0x04000BAC RID: 2988
		public HTMLElementEvents2_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x04000BAD RID: 2989
		public HTMLElementEvents2_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x04000BAE RID: 2990
		public HTMLElementEvents2_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x04000BAF RID: 2991
		public HTMLElementEvents2_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x04000BB0 RID: 2992
		public HTMLElementEvents2_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x04000BB1 RID: 2993
		public HTMLElementEvents2_onpasteEventHandler m_onpasteDelegate;

		// Token: 0x04000BB2 RID: 2994
		public HTMLElementEvents2_onbeforepasteEventHandler m_onbeforepasteDelegate;

		// Token: 0x04000BB3 RID: 2995
		public HTMLElementEvents2_oncopyEventHandler m_oncopyDelegate;

		// Token: 0x04000BB4 RID: 2996
		public HTMLElementEvents2_onbeforecopyEventHandler m_onbeforecopyDelegate;

		// Token: 0x04000BB5 RID: 2997
		public HTMLElementEvents2_oncutEventHandler m_oncutDelegate;

		// Token: 0x04000BB6 RID: 2998
		public HTMLElementEvents2_onbeforecutEventHandler m_onbeforecutDelegate;

		// Token: 0x04000BB7 RID: 2999
		public HTMLElementEvents2_ondropEventHandler m_ondropDelegate;

		// Token: 0x04000BB8 RID: 3000
		public HTMLElementEvents2_ondragleaveEventHandler m_ondragleaveDelegate;

		// Token: 0x04000BB9 RID: 3001
		public HTMLElementEvents2_ondragoverEventHandler m_ondragoverDelegate;

		// Token: 0x04000BBA RID: 3002
		public HTMLElementEvents2_ondragenterEventHandler m_ondragenterDelegate;

		// Token: 0x04000BBB RID: 3003
		public HTMLElementEvents2_ondragendEventHandler m_ondragendDelegate;

		// Token: 0x04000BBC RID: 3004
		public HTMLElementEvents2_ondragEventHandler m_ondragDelegate;

		// Token: 0x04000BBD RID: 3005
		public HTMLElementEvents2_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x04000BBE RID: 3006
		public HTMLElementEvents2_onblurEventHandler m_onblurDelegate;

		// Token: 0x04000BBF RID: 3007
		public HTMLElementEvents2_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x04000BC0 RID: 3008
		public HTMLElementEvents2_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x04000BC1 RID: 3009
		public HTMLElementEvents2_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x04000BC2 RID: 3010
		public HTMLElementEvents2_onlosecaptureEventHandler m_onlosecaptureDelegate;

		// Token: 0x04000BC3 RID: 3011
		public HTMLElementEvents2_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x04000BC4 RID: 3012
		public HTMLElementEvents2_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x04000BC5 RID: 3013
		public HTMLElementEvents2_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x04000BC6 RID: 3014
		public HTMLElementEvents2_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x04000BC7 RID: 3015
		public HTMLElementEvents2_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x04000BC8 RID: 3016
		public HTMLElementEvents2_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x04000BC9 RID: 3017
		public HTMLElementEvents2_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x04000BCA RID: 3018
		public HTMLElementEvents2_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x04000BCB RID: 3019
		public HTMLElementEvents2_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x04000BCC RID: 3020
		public HTMLElementEvents2_onfilterchangeEventHandler m_onfilterchangeDelegate;

		// Token: 0x04000BCD RID: 3021
		public HTMLElementEvents2_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x04000BCE RID: 3022
		public HTMLElementEvents2_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x04000BCF RID: 3023
		public HTMLElementEvents2_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x04000BD0 RID: 3024
		public HTMLElementEvents2_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x04000BD1 RID: 3025
		public HTMLElementEvents2_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x04000BD2 RID: 3026
		public HTMLElementEvents2_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x04000BD3 RID: 3027
		public HTMLElementEvents2_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x04000BD4 RID: 3028
		public HTMLElementEvents2_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x04000BD5 RID: 3029
		public HTMLElementEvents2_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x04000BD6 RID: 3030
		public HTMLElementEvents2_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x04000BD7 RID: 3031
		public HTMLElementEvents2_onclickEventHandler m_onclickDelegate;

		// Token: 0x04000BD8 RID: 3032
		public HTMLElementEvents2_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x04000BD9 RID: 3033
		public int m_dwCookie;
	}
}
