using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DD6 RID: 3542
	[ClassInterface(0)]
	internal sealed class HTMLTextContainerEvents2_SinkHelper : HTMLTextContainerEvents2
	{
		// Token: 0x06017CD2 RID: 97490 RVA: 0x0004AEF0 File Offset: 0x00049EF0
		public override void onselect(IHTMLEventObj A_1)
		{
			if (this.m_onselectDelegate != null)
			{
				this.m_onselectDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017CD3 RID: 97491 RVA: 0x0004AF20 File Offset: 0x00049F20
		public override void onchange(IHTMLEventObj A_1)
		{
			if (this.m_onchangeDelegate != null)
			{
				this.m_onchangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017CD4 RID: 97492 RVA: 0x0004AF50 File Offset: 0x00049F50
		public override bool onmousewheel(IHTMLEventObj A_1)
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate(A_1);
		}

		// Token: 0x06017CD5 RID: 97493 RVA: 0x0004AF80 File Offset: 0x00049F80
		public override void onresizeend(IHTMLEventObj A_1)
		{
			if (this.m_onresizeendDelegate != null)
			{
				this.m_onresizeendDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017CD6 RID: 97494 RVA: 0x0004AFB0 File Offset: 0x00049FB0
		public override bool onresizestart(IHTMLEventObj A_1)
		{
			return this.m_onresizestartDelegate != null && this.m_onresizestartDelegate(A_1);
		}

		// Token: 0x06017CD7 RID: 97495 RVA: 0x0004AFE0 File Offset: 0x00049FE0
		public override void onmoveend(IHTMLEventObj A_1)
		{
			if (this.m_onmoveendDelegate != null)
			{
				this.m_onmoveendDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017CD8 RID: 97496 RVA: 0x0004B010 File Offset: 0x0004A010
		public override bool onmovestart(IHTMLEventObj A_1)
		{
			return this.m_onmovestartDelegate != null && this.m_onmovestartDelegate(A_1);
		}

		// Token: 0x06017CD9 RID: 97497 RVA: 0x0004B040 File Offset: 0x0004A040
		public override bool oncontrolselect(IHTMLEventObj A_1)
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate(A_1);
		}

		// Token: 0x06017CDA RID: 97498 RVA: 0x0004B070 File Offset: 0x0004A070
		public override void onmove(IHTMLEventObj A_1)
		{
			if (this.m_onmoveDelegate != null)
			{
				this.m_onmoveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017CDB RID: 97499 RVA: 0x0004B0A0 File Offset: 0x0004A0A0
		public override void onfocusout(IHTMLEventObj A_1)
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017CDC RID: 97500 RVA: 0x0004B0D0 File Offset: 0x0004A0D0
		public override void onfocusin(IHTMLEventObj A_1)
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017CDD RID: 97501 RVA: 0x0004B100 File Offset: 0x0004A100
		public override bool onbeforeactivate(IHTMLEventObj A_1)
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate(A_1);
		}

		// Token: 0x06017CDE RID: 97502 RVA: 0x0004B130 File Offset: 0x0004A130
		public override bool onbeforedeactivate(IHTMLEventObj A_1)
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate(A_1);
		}

		// Token: 0x06017CDF RID: 97503 RVA: 0x0004B160 File Offset: 0x0004A160
		public override void ondeactivate(IHTMLEventObj A_1)
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017CE0 RID: 97504 RVA: 0x0004B190 File Offset: 0x0004A190
		public override void onactivate(IHTMLEventObj A_1)
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017CE1 RID: 97505 RVA: 0x0004B1C0 File Offset: 0x0004A1C0
		public override void onmouseleave(IHTMLEventObj A_1)
		{
			if (this.m_onmouseleaveDelegate != null)
			{
				this.m_onmouseleaveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017CE2 RID: 97506 RVA: 0x0004B1F0 File Offset: 0x0004A1F0
		public override void onmouseenter(IHTMLEventObj A_1)
		{
			if (this.m_onmouseenterDelegate != null)
			{
				this.m_onmouseenterDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017CE3 RID: 97507 RVA: 0x0004B220 File Offset: 0x0004A220
		public override void onpage(IHTMLEventObj A_1)
		{
			if (this.m_onpageDelegate != null)
			{
				this.m_onpageDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017CE4 RID: 97508 RVA: 0x0004B250 File Offset: 0x0004A250
		public override void onlayoutcomplete(IHTMLEventObj A_1)
		{
			if (this.m_onlayoutcompleteDelegate != null)
			{
				this.m_onlayoutcompleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017CE5 RID: 97509 RVA: 0x0004B280 File Offset: 0x0004A280
		public override void onreadystatechange(IHTMLEventObj A_1)
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017CE6 RID: 97510 RVA: 0x0004B2B0 File Offset: 0x0004A2B0
		public override void oncellchange(IHTMLEventObj A_1)
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017CE7 RID: 97511 RVA: 0x0004B2E0 File Offset: 0x0004A2E0
		public override void onrowsinserted(IHTMLEventObj A_1)
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017CE8 RID: 97512 RVA: 0x0004B310 File Offset: 0x0004A310
		public override void onrowsdelete(IHTMLEventObj A_1)
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017CE9 RID: 97513 RVA: 0x0004B340 File Offset: 0x0004A340
		public override bool oncontextmenu(IHTMLEventObj A_1)
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate(A_1);
		}

		// Token: 0x06017CEA RID: 97514 RVA: 0x0004B370 File Offset: 0x0004A370
		public override bool onpaste(IHTMLEventObj A_1)
		{
			return this.m_onpasteDelegate != null && this.m_onpasteDelegate(A_1);
		}

		// Token: 0x06017CEB RID: 97515 RVA: 0x0004B3A0 File Offset: 0x0004A3A0
		public override bool onbeforepaste(IHTMLEventObj A_1)
		{
			return this.m_onbeforepasteDelegate != null && this.m_onbeforepasteDelegate(A_1);
		}

		// Token: 0x06017CEC RID: 97516 RVA: 0x0004B3D0 File Offset: 0x0004A3D0
		public override bool oncopy(IHTMLEventObj A_1)
		{
			return this.m_oncopyDelegate != null && this.m_oncopyDelegate(A_1);
		}

		// Token: 0x06017CED RID: 97517 RVA: 0x0004B400 File Offset: 0x0004A400
		public override bool onbeforecopy(IHTMLEventObj A_1)
		{
			return this.m_onbeforecopyDelegate != null && this.m_onbeforecopyDelegate(A_1);
		}

		// Token: 0x06017CEE RID: 97518 RVA: 0x0004B430 File Offset: 0x0004A430
		public override bool oncut(IHTMLEventObj A_1)
		{
			return this.m_oncutDelegate != null && this.m_oncutDelegate(A_1);
		}

		// Token: 0x06017CEF RID: 97519 RVA: 0x0004B460 File Offset: 0x0004A460
		public override bool onbeforecut(IHTMLEventObj A_1)
		{
			return this.m_onbeforecutDelegate != null && this.m_onbeforecutDelegate(A_1);
		}

		// Token: 0x06017CF0 RID: 97520 RVA: 0x0004B490 File Offset: 0x0004A490
		public override bool ondrop(IHTMLEventObj A_1)
		{
			return this.m_ondropDelegate != null && this.m_ondropDelegate(A_1);
		}

		// Token: 0x06017CF1 RID: 97521 RVA: 0x0004B4C0 File Offset: 0x0004A4C0
		public override void ondragleave(IHTMLEventObj A_1)
		{
			if (this.m_ondragleaveDelegate != null)
			{
				this.m_ondragleaveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017CF2 RID: 97522 RVA: 0x0004B4F0 File Offset: 0x0004A4F0
		public override bool ondragover(IHTMLEventObj A_1)
		{
			return this.m_ondragoverDelegate != null && this.m_ondragoverDelegate(A_1);
		}

		// Token: 0x06017CF3 RID: 97523 RVA: 0x0004B520 File Offset: 0x0004A520
		public override bool ondragenter(IHTMLEventObj A_1)
		{
			return this.m_ondragenterDelegate != null && this.m_ondragenterDelegate(A_1);
		}

		// Token: 0x06017CF4 RID: 97524 RVA: 0x0004B550 File Offset: 0x0004A550
		public override void ondragend(IHTMLEventObj A_1)
		{
			if (this.m_ondragendDelegate != null)
			{
				this.m_ondragendDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017CF5 RID: 97525 RVA: 0x0004B580 File Offset: 0x0004A580
		public override bool ondrag(IHTMLEventObj A_1)
		{
			return this.m_ondragDelegate != null && this.m_ondragDelegate(A_1);
		}

		// Token: 0x06017CF6 RID: 97526 RVA: 0x0004B5B0 File Offset: 0x0004A5B0
		public override void onresize(IHTMLEventObj A_1)
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017CF7 RID: 97527 RVA: 0x0004B5E0 File Offset: 0x0004A5E0
		public override void onblur(IHTMLEventObj A_1)
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017CF8 RID: 97528 RVA: 0x0004B610 File Offset: 0x0004A610
		public override void onfocus(IHTMLEventObj A_1)
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017CF9 RID: 97529 RVA: 0x0004B640 File Offset: 0x0004A640
		public override void onscroll(IHTMLEventObj A_1)
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017CFA RID: 97530 RVA: 0x0004B670 File Offset: 0x0004A670
		public override void onpropertychange(IHTMLEventObj A_1)
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017CFB RID: 97531 RVA: 0x0004B6A0 File Offset: 0x0004A6A0
		public override void onlosecapture(IHTMLEventObj A_1)
		{
			if (this.m_onlosecaptureDelegate != null)
			{
				this.m_onlosecaptureDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017CFC RID: 97532 RVA: 0x0004B6D0 File Offset: 0x0004A6D0
		public override void ondatasetcomplete(IHTMLEventObj A_1)
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017CFD RID: 97533 RVA: 0x0004B700 File Offset: 0x0004A700
		public override void ondataavailable(IHTMLEventObj A_1)
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017CFE RID: 97534 RVA: 0x0004B730 File Offset: 0x0004A730
		public override void ondatasetchanged(IHTMLEventObj A_1)
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017CFF RID: 97535 RVA: 0x0004B760 File Offset: 0x0004A760
		public override void onrowenter(IHTMLEventObj A_1)
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017D00 RID: 97536 RVA: 0x0004B790 File Offset: 0x0004A790
		public override bool onrowexit(IHTMLEventObj A_1)
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate(A_1);
		}

		// Token: 0x06017D01 RID: 97537 RVA: 0x0004B7C0 File Offset: 0x0004A7C0
		public override bool onerrorupdate(IHTMLEventObj A_1)
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate(A_1);
		}

		// Token: 0x06017D02 RID: 97538 RVA: 0x0004B7F0 File Offset: 0x0004A7F0
		public override void onafterupdate(IHTMLEventObj A_1)
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017D03 RID: 97539 RVA: 0x0004B820 File Offset: 0x0004A820
		public override bool onbeforeupdate(IHTMLEventObj A_1)
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate(A_1);
		}

		// Token: 0x06017D04 RID: 97540 RVA: 0x0004B850 File Offset: 0x0004A850
		public override bool ondragstart(IHTMLEventObj A_1)
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate(A_1);
		}

		// Token: 0x06017D05 RID: 97541 RVA: 0x0004B880 File Offset: 0x0004A880
		public override void onfilterchange(IHTMLEventObj A_1)
		{
			if (this.m_onfilterchangeDelegate != null)
			{
				this.m_onfilterchangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017D06 RID: 97542 RVA: 0x0004B8B0 File Offset: 0x0004A8B0
		public override bool onselectstart(IHTMLEventObj A_1)
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate(A_1);
		}

		// Token: 0x06017D07 RID: 97543 RVA: 0x0004B8E0 File Offset: 0x0004A8E0
		public override void onmouseup(IHTMLEventObj A_1)
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017D08 RID: 97544 RVA: 0x0004B910 File Offset: 0x0004A910
		public override void onmousedown(IHTMLEventObj A_1)
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017D09 RID: 97545 RVA: 0x0004B940 File Offset: 0x0004A940
		public override void onmousemove(IHTMLEventObj A_1)
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017D0A RID: 97546 RVA: 0x0004B970 File Offset: 0x0004A970
		public override void onmouseover(IHTMLEventObj A_1)
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017D0B RID: 97547 RVA: 0x0004B9A0 File Offset: 0x0004A9A0
		public override void onmouseout(IHTMLEventObj A_1)
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017D0C RID: 97548 RVA: 0x0004B9D0 File Offset: 0x0004A9D0
		public override void onkeyup(IHTMLEventObj A_1)
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017D0D RID: 97549 RVA: 0x0004BA00 File Offset: 0x0004AA00
		public override void onkeydown(IHTMLEventObj A_1)
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017D0E RID: 97550 RVA: 0x0004BA30 File Offset: 0x0004AA30
		public override bool onkeypress(IHTMLEventObj A_1)
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate(A_1);
		}

		// Token: 0x06017D0F RID: 97551 RVA: 0x0004BA60 File Offset: 0x0004AA60
		public override bool ondblclick(IHTMLEventObj A_1)
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate(A_1);
		}

		// Token: 0x06017D10 RID: 97552 RVA: 0x0004BA90 File Offset: 0x0004AA90
		public override bool onclick(IHTMLEventObj A_1)
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate(A_1);
		}

		// Token: 0x06017D11 RID: 97553 RVA: 0x0004BAC0 File Offset: 0x0004AAC0
		public override bool onhelp(IHTMLEventObj A_1)
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate(A_1);
		}

		// Token: 0x06017D12 RID: 97554 RVA: 0x0004BAF0 File Offset: 0x0004AAF0
		internal HTMLTextContainerEvents2_SinkHelper()
		{
			this.m_dwCookie = 0;
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

		// Token: 0x04000782 RID: 1922
		public HTMLTextContainerEvents2_onselectEventHandler m_onselectDelegate;

		// Token: 0x04000783 RID: 1923
		public HTMLTextContainerEvents2_onchangeEventHandler m_onchangeDelegate;

		// Token: 0x04000784 RID: 1924
		public HTMLTextContainerEvents2_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x04000785 RID: 1925
		public HTMLTextContainerEvents2_onresizeendEventHandler m_onresizeendDelegate;

		// Token: 0x04000786 RID: 1926
		public HTMLTextContainerEvents2_onresizestartEventHandler m_onresizestartDelegate;

		// Token: 0x04000787 RID: 1927
		public HTMLTextContainerEvents2_onmoveendEventHandler m_onmoveendDelegate;

		// Token: 0x04000788 RID: 1928
		public HTMLTextContainerEvents2_onmovestartEventHandler m_onmovestartDelegate;

		// Token: 0x04000789 RID: 1929
		public HTMLTextContainerEvents2_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x0400078A RID: 1930
		public HTMLTextContainerEvents2_onmoveEventHandler m_onmoveDelegate;

		// Token: 0x0400078B RID: 1931
		public HTMLTextContainerEvents2_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x0400078C RID: 1932
		public HTMLTextContainerEvents2_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x0400078D RID: 1933
		public HTMLTextContainerEvents2_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x0400078E RID: 1934
		public HTMLTextContainerEvents2_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x0400078F RID: 1935
		public HTMLTextContainerEvents2_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x04000790 RID: 1936
		public HTMLTextContainerEvents2_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x04000791 RID: 1937
		public HTMLTextContainerEvents2_onmouseleaveEventHandler m_onmouseleaveDelegate;

		// Token: 0x04000792 RID: 1938
		public HTMLTextContainerEvents2_onmouseenterEventHandler m_onmouseenterDelegate;

		// Token: 0x04000793 RID: 1939
		public HTMLTextContainerEvents2_onpageEventHandler m_onpageDelegate;

		// Token: 0x04000794 RID: 1940
		public HTMLTextContainerEvents2_onlayoutcompleteEventHandler m_onlayoutcompleteDelegate;

		// Token: 0x04000795 RID: 1941
		public HTMLTextContainerEvents2_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x04000796 RID: 1942
		public HTMLTextContainerEvents2_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x04000797 RID: 1943
		public HTMLTextContainerEvents2_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x04000798 RID: 1944
		public HTMLTextContainerEvents2_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x04000799 RID: 1945
		public HTMLTextContainerEvents2_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x0400079A RID: 1946
		public HTMLTextContainerEvents2_onpasteEventHandler m_onpasteDelegate;

		// Token: 0x0400079B RID: 1947
		public HTMLTextContainerEvents2_onbeforepasteEventHandler m_onbeforepasteDelegate;

		// Token: 0x0400079C RID: 1948
		public HTMLTextContainerEvents2_oncopyEventHandler m_oncopyDelegate;

		// Token: 0x0400079D RID: 1949
		public HTMLTextContainerEvents2_onbeforecopyEventHandler m_onbeforecopyDelegate;

		// Token: 0x0400079E RID: 1950
		public HTMLTextContainerEvents2_oncutEventHandler m_oncutDelegate;

		// Token: 0x0400079F RID: 1951
		public HTMLTextContainerEvents2_onbeforecutEventHandler m_onbeforecutDelegate;

		// Token: 0x040007A0 RID: 1952
		public HTMLTextContainerEvents2_ondropEventHandler m_ondropDelegate;

		// Token: 0x040007A1 RID: 1953
		public HTMLTextContainerEvents2_ondragleaveEventHandler m_ondragleaveDelegate;

		// Token: 0x040007A2 RID: 1954
		public HTMLTextContainerEvents2_ondragoverEventHandler m_ondragoverDelegate;

		// Token: 0x040007A3 RID: 1955
		public HTMLTextContainerEvents2_ondragenterEventHandler m_ondragenterDelegate;

		// Token: 0x040007A4 RID: 1956
		public HTMLTextContainerEvents2_ondragendEventHandler m_ondragendDelegate;

		// Token: 0x040007A5 RID: 1957
		public HTMLTextContainerEvents2_ondragEventHandler m_ondragDelegate;

		// Token: 0x040007A6 RID: 1958
		public HTMLTextContainerEvents2_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x040007A7 RID: 1959
		public HTMLTextContainerEvents2_onblurEventHandler m_onblurDelegate;

		// Token: 0x040007A8 RID: 1960
		public HTMLTextContainerEvents2_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x040007A9 RID: 1961
		public HTMLTextContainerEvents2_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x040007AA RID: 1962
		public HTMLTextContainerEvents2_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x040007AB RID: 1963
		public HTMLTextContainerEvents2_onlosecaptureEventHandler m_onlosecaptureDelegate;

		// Token: 0x040007AC RID: 1964
		public HTMLTextContainerEvents2_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x040007AD RID: 1965
		public HTMLTextContainerEvents2_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x040007AE RID: 1966
		public HTMLTextContainerEvents2_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x040007AF RID: 1967
		public HTMLTextContainerEvents2_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x040007B0 RID: 1968
		public HTMLTextContainerEvents2_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x040007B1 RID: 1969
		public HTMLTextContainerEvents2_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x040007B2 RID: 1970
		public HTMLTextContainerEvents2_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x040007B3 RID: 1971
		public HTMLTextContainerEvents2_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x040007B4 RID: 1972
		public HTMLTextContainerEvents2_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x040007B5 RID: 1973
		public HTMLTextContainerEvents2_onfilterchangeEventHandler m_onfilterchangeDelegate;

		// Token: 0x040007B6 RID: 1974
		public HTMLTextContainerEvents2_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x040007B7 RID: 1975
		public HTMLTextContainerEvents2_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x040007B8 RID: 1976
		public HTMLTextContainerEvents2_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x040007B9 RID: 1977
		public HTMLTextContainerEvents2_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x040007BA RID: 1978
		public HTMLTextContainerEvents2_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x040007BB RID: 1979
		public HTMLTextContainerEvents2_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x040007BC RID: 1980
		public HTMLTextContainerEvents2_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x040007BD RID: 1981
		public HTMLTextContainerEvents2_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x040007BE RID: 1982
		public HTMLTextContainerEvents2_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x040007BF RID: 1983
		public HTMLTextContainerEvents2_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x040007C0 RID: 1984
		public HTMLTextContainerEvents2_onclickEventHandler m_onclickDelegate;

		// Token: 0x040007C1 RID: 1985
		public HTMLTextContainerEvents2_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x040007C2 RID: 1986
		public int m_dwCookie;
	}
}
