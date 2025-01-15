using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DEE RID: 3566
	[ClassInterface(0)]
	internal sealed class HTMLOptionButtonElementEvents_SinkHelper : HTMLOptionButtonElementEvents
	{
		// Token: 0x0601849D RID: 99485 RVA: 0x0009200C File Offset: 0x0009100C
		public override void onabort()
		{
			if (this.m_onabortDelegate != null)
			{
				this.m_onabortDelegate();
				return;
			}
		}

		// Token: 0x0601849E RID: 99486 RVA: 0x00092038 File Offset: 0x00091038
		public override void onerror()
		{
			if (this.m_onerrorDelegate != null)
			{
				this.m_onerrorDelegate();
				return;
			}
		}

		// Token: 0x0601849F RID: 99487 RVA: 0x00092064 File Offset: 0x00091064
		public override void onload()
		{
			if (this.m_onloadDelegate != null)
			{
				this.m_onloadDelegate();
				return;
			}
		}

		// Token: 0x060184A0 RID: 99488 RVA: 0x00092090 File Offset: 0x00091090
		public override void onselect()
		{
			if (this.m_onselectDelegate != null)
			{
				this.m_onselectDelegate();
				return;
			}
		}

		// Token: 0x060184A1 RID: 99489 RVA: 0x000920BC File Offset: 0x000910BC
		public override bool onchange()
		{
			return this.m_onchangeDelegate != null && this.m_onchangeDelegate();
		}

		// Token: 0x060184A2 RID: 99490 RVA: 0x000920E8 File Offset: 0x000910E8
		public override void onfocusout()
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate();
				return;
			}
		}

		// Token: 0x060184A3 RID: 99491 RVA: 0x00092114 File Offset: 0x00091114
		public override void onfocusin()
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate();
				return;
			}
		}

		// Token: 0x060184A4 RID: 99492 RVA: 0x00092140 File Offset: 0x00091140
		public override void ondeactivate()
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate();
				return;
			}
		}

		// Token: 0x060184A5 RID: 99493 RVA: 0x0009216C File Offset: 0x0009116C
		public override void onactivate()
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate();
				return;
			}
		}

		// Token: 0x060184A6 RID: 99494 RVA: 0x00092198 File Offset: 0x00091198
		public override bool onmousewheel()
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate();
		}

		// Token: 0x060184A7 RID: 99495 RVA: 0x000921C4 File Offset: 0x000911C4
		public override void onmouseleave()
		{
			if (this.m_onmouseleaveDelegate != null)
			{
				this.m_onmouseleaveDelegate();
				return;
			}
		}

		// Token: 0x060184A8 RID: 99496 RVA: 0x000921F0 File Offset: 0x000911F0
		public override void onmouseenter()
		{
			if (this.m_onmouseenterDelegate != null)
			{
				this.m_onmouseenterDelegate();
				return;
			}
		}

		// Token: 0x060184A9 RID: 99497 RVA: 0x0009221C File Offset: 0x0009121C
		public override void onresizeend()
		{
			if (this.m_onresizeendDelegate != null)
			{
				this.m_onresizeendDelegate();
				return;
			}
		}

		// Token: 0x060184AA RID: 99498 RVA: 0x00092248 File Offset: 0x00091248
		public override bool onresizestart()
		{
			return this.m_onresizestartDelegate != null && this.m_onresizestartDelegate();
		}

		// Token: 0x060184AB RID: 99499 RVA: 0x00092274 File Offset: 0x00091274
		public override void onmoveend()
		{
			if (this.m_onmoveendDelegate != null)
			{
				this.m_onmoveendDelegate();
				return;
			}
		}

		// Token: 0x060184AC RID: 99500 RVA: 0x000922A0 File Offset: 0x000912A0
		public override bool onmovestart()
		{
			return this.m_onmovestartDelegate != null && this.m_onmovestartDelegate();
		}

		// Token: 0x060184AD RID: 99501 RVA: 0x000922CC File Offset: 0x000912CC
		public override bool oncontrolselect()
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate();
		}

		// Token: 0x060184AE RID: 99502 RVA: 0x000922F8 File Offset: 0x000912F8
		public override void onmove()
		{
			if (this.m_onmoveDelegate != null)
			{
				this.m_onmoveDelegate();
				return;
			}
		}

		// Token: 0x060184AF RID: 99503 RVA: 0x00092324 File Offset: 0x00091324
		public override bool onbeforeactivate()
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate();
		}

		// Token: 0x060184B0 RID: 99504 RVA: 0x00092350 File Offset: 0x00091350
		public override bool onbeforedeactivate()
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate();
		}

		// Token: 0x060184B1 RID: 99505 RVA: 0x0009237C File Offset: 0x0009137C
		public override void onpage()
		{
			if (this.m_onpageDelegate != null)
			{
				this.m_onpageDelegate();
				return;
			}
		}

		// Token: 0x060184B2 RID: 99506 RVA: 0x000923A8 File Offset: 0x000913A8
		public override void onlayoutcomplete()
		{
			if (this.m_onlayoutcompleteDelegate != null)
			{
				this.m_onlayoutcompleteDelegate();
				return;
			}
		}

		// Token: 0x060184B3 RID: 99507 RVA: 0x000923D4 File Offset: 0x000913D4
		public override void onbeforeeditfocus()
		{
			if (this.m_onbeforeeditfocusDelegate != null)
			{
				this.m_onbeforeeditfocusDelegate();
				return;
			}
		}

		// Token: 0x060184B4 RID: 99508 RVA: 0x00092400 File Offset: 0x00091400
		public override void onreadystatechange()
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate();
				return;
			}
		}

		// Token: 0x060184B5 RID: 99509 RVA: 0x0009242C File Offset: 0x0009142C
		public override void oncellchange()
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate();
				return;
			}
		}

		// Token: 0x060184B6 RID: 99510 RVA: 0x00092458 File Offset: 0x00091458
		public override void onrowsinserted()
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate();
				return;
			}
		}

		// Token: 0x060184B7 RID: 99511 RVA: 0x00092484 File Offset: 0x00091484
		public override void onrowsdelete()
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate();
				return;
			}
		}

		// Token: 0x060184B8 RID: 99512 RVA: 0x000924B0 File Offset: 0x000914B0
		public override bool oncontextmenu()
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate();
		}

		// Token: 0x060184B9 RID: 99513 RVA: 0x000924DC File Offset: 0x000914DC
		public override bool onpaste()
		{
			return this.m_onpasteDelegate != null && this.m_onpasteDelegate();
		}

		// Token: 0x060184BA RID: 99514 RVA: 0x00092508 File Offset: 0x00091508
		public override bool onbeforepaste()
		{
			return this.m_onbeforepasteDelegate != null && this.m_onbeforepasteDelegate();
		}

		// Token: 0x060184BB RID: 99515 RVA: 0x00092534 File Offset: 0x00091534
		public override bool oncopy()
		{
			return this.m_oncopyDelegate != null && this.m_oncopyDelegate();
		}

		// Token: 0x060184BC RID: 99516 RVA: 0x00092560 File Offset: 0x00091560
		public override bool onbeforecopy()
		{
			return this.m_onbeforecopyDelegate != null && this.m_onbeforecopyDelegate();
		}

		// Token: 0x060184BD RID: 99517 RVA: 0x0009258C File Offset: 0x0009158C
		public override bool oncut()
		{
			return this.m_oncutDelegate != null && this.m_oncutDelegate();
		}

		// Token: 0x060184BE RID: 99518 RVA: 0x000925B8 File Offset: 0x000915B8
		public override bool onbeforecut()
		{
			return this.m_onbeforecutDelegate != null && this.m_onbeforecutDelegate();
		}

		// Token: 0x060184BF RID: 99519 RVA: 0x000925E4 File Offset: 0x000915E4
		public override bool ondrop()
		{
			return this.m_ondropDelegate != null && this.m_ondropDelegate();
		}

		// Token: 0x060184C0 RID: 99520 RVA: 0x00092610 File Offset: 0x00091610
		public override void ondragleave()
		{
			if (this.m_ondragleaveDelegate != null)
			{
				this.m_ondragleaveDelegate();
				return;
			}
		}

		// Token: 0x060184C1 RID: 99521 RVA: 0x0009263C File Offset: 0x0009163C
		public override bool ondragover()
		{
			return this.m_ondragoverDelegate != null && this.m_ondragoverDelegate();
		}

		// Token: 0x060184C2 RID: 99522 RVA: 0x00092668 File Offset: 0x00091668
		public override bool ondragenter()
		{
			return this.m_ondragenterDelegate != null && this.m_ondragenterDelegate();
		}

		// Token: 0x060184C3 RID: 99523 RVA: 0x00092694 File Offset: 0x00091694
		public override void ondragend()
		{
			if (this.m_ondragendDelegate != null)
			{
				this.m_ondragendDelegate();
				return;
			}
		}

		// Token: 0x060184C4 RID: 99524 RVA: 0x000926C0 File Offset: 0x000916C0
		public override bool ondrag()
		{
			return this.m_ondragDelegate != null && this.m_ondragDelegate();
		}

		// Token: 0x060184C5 RID: 99525 RVA: 0x000926EC File Offset: 0x000916EC
		public override void onresize()
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate();
				return;
			}
		}

		// Token: 0x060184C6 RID: 99526 RVA: 0x00092718 File Offset: 0x00091718
		public override void onblur()
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate();
				return;
			}
		}

		// Token: 0x060184C7 RID: 99527 RVA: 0x00092744 File Offset: 0x00091744
		public override void onfocus()
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate();
				return;
			}
		}

		// Token: 0x060184C8 RID: 99528 RVA: 0x00092770 File Offset: 0x00091770
		public override void onscroll()
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate();
				return;
			}
		}

		// Token: 0x060184C9 RID: 99529 RVA: 0x0009279C File Offset: 0x0009179C
		public override void onpropertychange()
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate();
				return;
			}
		}

		// Token: 0x060184CA RID: 99530 RVA: 0x000927C8 File Offset: 0x000917C8
		public override void onlosecapture()
		{
			if (this.m_onlosecaptureDelegate != null)
			{
				this.m_onlosecaptureDelegate();
				return;
			}
		}

		// Token: 0x060184CB RID: 99531 RVA: 0x000927F4 File Offset: 0x000917F4
		public override void ondatasetcomplete()
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate();
				return;
			}
		}

		// Token: 0x060184CC RID: 99532 RVA: 0x00092820 File Offset: 0x00091820
		public override void ondataavailable()
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate();
				return;
			}
		}

		// Token: 0x060184CD RID: 99533 RVA: 0x0009284C File Offset: 0x0009184C
		public override void ondatasetchanged()
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate();
				return;
			}
		}

		// Token: 0x060184CE RID: 99534 RVA: 0x00092878 File Offset: 0x00091878
		public override void onrowenter()
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate();
				return;
			}
		}

		// Token: 0x060184CF RID: 99535 RVA: 0x000928A4 File Offset: 0x000918A4
		public override bool onrowexit()
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate();
		}

		// Token: 0x060184D0 RID: 99536 RVA: 0x000928D0 File Offset: 0x000918D0
		public override bool onerrorupdate()
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate();
		}

		// Token: 0x060184D1 RID: 99537 RVA: 0x000928FC File Offset: 0x000918FC
		public override void onafterupdate()
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate();
				return;
			}
		}

		// Token: 0x060184D2 RID: 99538 RVA: 0x00092928 File Offset: 0x00091928
		public override bool onbeforeupdate()
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate();
		}

		// Token: 0x060184D3 RID: 99539 RVA: 0x00092954 File Offset: 0x00091954
		public override bool ondragstart()
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate();
		}

		// Token: 0x060184D4 RID: 99540 RVA: 0x00092980 File Offset: 0x00091980
		public override void onfilterchange()
		{
			if (this.m_onfilterchangeDelegate != null)
			{
				this.m_onfilterchangeDelegate();
				return;
			}
		}

		// Token: 0x060184D5 RID: 99541 RVA: 0x000929AC File Offset: 0x000919AC
		public override bool onselectstart()
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate();
		}

		// Token: 0x060184D6 RID: 99542 RVA: 0x000929D8 File Offset: 0x000919D8
		public override void onmouseup()
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate();
				return;
			}
		}

		// Token: 0x060184D7 RID: 99543 RVA: 0x00092A04 File Offset: 0x00091A04
		public override void onmousedown()
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate();
				return;
			}
		}

		// Token: 0x060184D8 RID: 99544 RVA: 0x00092A30 File Offset: 0x00091A30
		public override void onmousemove()
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate();
				return;
			}
		}

		// Token: 0x060184D9 RID: 99545 RVA: 0x00092A5C File Offset: 0x00091A5C
		public override void onmouseover()
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate();
				return;
			}
		}

		// Token: 0x060184DA RID: 99546 RVA: 0x00092A88 File Offset: 0x00091A88
		public override void onmouseout()
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate();
				return;
			}
		}

		// Token: 0x060184DB RID: 99547 RVA: 0x00092AB4 File Offset: 0x00091AB4
		public override void onkeyup()
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate();
				return;
			}
		}

		// Token: 0x060184DC RID: 99548 RVA: 0x00092AE0 File Offset: 0x00091AE0
		public override void onkeydown()
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate();
				return;
			}
		}

		// Token: 0x060184DD RID: 99549 RVA: 0x00092B0C File Offset: 0x00091B0C
		public override bool onkeypress()
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate();
		}

		// Token: 0x060184DE RID: 99550 RVA: 0x00092B38 File Offset: 0x00091B38
		public override bool ondblclick()
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate();
		}

		// Token: 0x060184DF RID: 99551 RVA: 0x00092B64 File Offset: 0x00091B64
		public override bool onclick()
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate();
		}

		// Token: 0x060184E0 RID: 99552 RVA: 0x00092B90 File Offset: 0x00091B90
		public override bool onhelp()
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate();
		}

		// Token: 0x060184E1 RID: 99553 RVA: 0x00092BBC File Offset: 0x00091BBC
		internal HTMLOptionButtonElementEvents_SinkHelper()
		{
			this.m_dwCookie = 0;
			this.m_onabortDelegate = null;
			this.m_onerrorDelegate = null;
			this.m_onloadDelegate = null;
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

		// Token: 0x04000A37 RID: 2615
		public HTMLOptionButtonElementEvents_onabortEventHandler m_onabortDelegate;

		// Token: 0x04000A38 RID: 2616
		public HTMLOptionButtonElementEvents_onerrorEventHandler m_onerrorDelegate;

		// Token: 0x04000A39 RID: 2617
		public HTMLOptionButtonElementEvents_onloadEventHandler m_onloadDelegate;

		// Token: 0x04000A3A RID: 2618
		public HTMLOptionButtonElementEvents_onselectEventHandler m_onselectDelegate;

		// Token: 0x04000A3B RID: 2619
		public HTMLOptionButtonElementEvents_onchangeEventHandler m_onchangeDelegate;

		// Token: 0x04000A3C RID: 2620
		public HTMLOptionButtonElementEvents_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x04000A3D RID: 2621
		public HTMLOptionButtonElementEvents_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x04000A3E RID: 2622
		public HTMLOptionButtonElementEvents_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x04000A3F RID: 2623
		public HTMLOptionButtonElementEvents_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x04000A40 RID: 2624
		public HTMLOptionButtonElementEvents_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x04000A41 RID: 2625
		public HTMLOptionButtonElementEvents_onmouseleaveEventHandler m_onmouseleaveDelegate;

		// Token: 0x04000A42 RID: 2626
		public HTMLOptionButtonElementEvents_onmouseenterEventHandler m_onmouseenterDelegate;

		// Token: 0x04000A43 RID: 2627
		public HTMLOptionButtonElementEvents_onresizeendEventHandler m_onresizeendDelegate;

		// Token: 0x04000A44 RID: 2628
		public HTMLOptionButtonElementEvents_onresizestartEventHandler m_onresizestartDelegate;

		// Token: 0x04000A45 RID: 2629
		public HTMLOptionButtonElementEvents_onmoveendEventHandler m_onmoveendDelegate;

		// Token: 0x04000A46 RID: 2630
		public HTMLOptionButtonElementEvents_onmovestartEventHandler m_onmovestartDelegate;

		// Token: 0x04000A47 RID: 2631
		public HTMLOptionButtonElementEvents_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x04000A48 RID: 2632
		public HTMLOptionButtonElementEvents_onmoveEventHandler m_onmoveDelegate;

		// Token: 0x04000A49 RID: 2633
		public HTMLOptionButtonElementEvents_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x04000A4A RID: 2634
		public HTMLOptionButtonElementEvents_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x04000A4B RID: 2635
		public HTMLOptionButtonElementEvents_onpageEventHandler m_onpageDelegate;

		// Token: 0x04000A4C RID: 2636
		public HTMLOptionButtonElementEvents_onlayoutcompleteEventHandler m_onlayoutcompleteDelegate;

		// Token: 0x04000A4D RID: 2637
		public HTMLOptionButtonElementEvents_onbeforeeditfocusEventHandler m_onbeforeeditfocusDelegate;

		// Token: 0x04000A4E RID: 2638
		public HTMLOptionButtonElementEvents_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x04000A4F RID: 2639
		public HTMLOptionButtonElementEvents_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x04000A50 RID: 2640
		public HTMLOptionButtonElementEvents_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x04000A51 RID: 2641
		public HTMLOptionButtonElementEvents_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x04000A52 RID: 2642
		public HTMLOptionButtonElementEvents_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x04000A53 RID: 2643
		public HTMLOptionButtonElementEvents_onpasteEventHandler m_onpasteDelegate;

		// Token: 0x04000A54 RID: 2644
		public HTMLOptionButtonElementEvents_onbeforepasteEventHandler m_onbeforepasteDelegate;

		// Token: 0x04000A55 RID: 2645
		public HTMLOptionButtonElementEvents_oncopyEventHandler m_oncopyDelegate;

		// Token: 0x04000A56 RID: 2646
		public HTMLOptionButtonElementEvents_onbeforecopyEventHandler m_onbeforecopyDelegate;

		// Token: 0x04000A57 RID: 2647
		public HTMLOptionButtonElementEvents_oncutEventHandler m_oncutDelegate;

		// Token: 0x04000A58 RID: 2648
		public HTMLOptionButtonElementEvents_onbeforecutEventHandler m_onbeforecutDelegate;

		// Token: 0x04000A59 RID: 2649
		public HTMLOptionButtonElementEvents_ondropEventHandler m_ondropDelegate;

		// Token: 0x04000A5A RID: 2650
		public HTMLOptionButtonElementEvents_ondragleaveEventHandler m_ondragleaveDelegate;

		// Token: 0x04000A5B RID: 2651
		public HTMLOptionButtonElementEvents_ondragoverEventHandler m_ondragoverDelegate;

		// Token: 0x04000A5C RID: 2652
		public HTMLOptionButtonElementEvents_ondragenterEventHandler m_ondragenterDelegate;

		// Token: 0x04000A5D RID: 2653
		public HTMLOptionButtonElementEvents_ondragendEventHandler m_ondragendDelegate;

		// Token: 0x04000A5E RID: 2654
		public HTMLOptionButtonElementEvents_ondragEventHandler m_ondragDelegate;

		// Token: 0x04000A5F RID: 2655
		public HTMLOptionButtonElementEvents_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x04000A60 RID: 2656
		public HTMLOptionButtonElementEvents_onblurEventHandler m_onblurDelegate;

		// Token: 0x04000A61 RID: 2657
		public HTMLOptionButtonElementEvents_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x04000A62 RID: 2658
		public HTMLOptionButtonElementEvents_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x04000A63 RID: 2659
		public HTMLOptionButtonElementEvents_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x04000A64 RID: 2660
		public HTMLOptionButtonElementEvents_onlosecaptureEventHandler m_onlosecaptureDelegate;

		// Token: 0x04000A65 RID: 2661
		public HTMLOptionButtonElementEvents_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x04000A66 RID: 2662
		public HTMLOptionButtonElementEvents_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x04000A67 RID: 2663
		public HTMLOptionButtonElementEvents_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x04000A68 RID: 2664
		public HTMLOptionButtonElementEvents_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x04000A69 RID: 2665
		public HTMLOptionButtonElementEvents_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x04000A6A RID: 2666
		public HTMLOptionButtonElementEvents_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x04000A6B RID: 2667
		public HTMLOptionButtonElementEvents_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x04000A6C RID: 2668
		public HTMLOptionButtonElementEvents_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x04000A6D RID: 2669
		public HTMLOptionButtonElementEvents_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x04000A6E RID: 2670
		public HTMLOptionButtonElementEvents_onfilterchangeEventHandler m_onfilterchangeDelegate;

		// Token: 0x04000A6F RID: 2671
		public HTMLOptionButtonElementEvents_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x04000A70 RID: 2672
		public HTMLOptionButtonElementEvents_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x04000A71 RID: 2673
		public HTMLOptionButtonElementEvents_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x04000A72 RID: 2674
		public HTMLOptionButtonElementEvents_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x04000A73 RID: 2675
		public HTMLOptionButtonElementEvents_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x04000A74 RID: 2676
		public HTMLOptionButtonElementEvents_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x04000A75 RID: 2677
		public HTMLOptionButtonElementEvents_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x04000A76 RID: 2678
		public HTMLOptionButtonElementEvents_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x04000A77 RID: 2679
		public HTMLOptionButtonElementEvents_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x04000A78 RID: 2680
		public HTMLOptionButtonElementEvents_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x04000A79 RID: 2681
		public HTMLOptionButtonElementEvents_onclickEventHandler m_onclickDelegate;

		// Token: 0x04000A7A RID: 2682
		public HTMLOptionButtonElementEvents_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x04000A7B RID: 2683
		public int m_dwCookie;
	}
}
