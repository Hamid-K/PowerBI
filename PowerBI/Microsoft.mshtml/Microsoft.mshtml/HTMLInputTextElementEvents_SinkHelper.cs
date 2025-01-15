using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DCC RID: 3532
	[ClassInterface(0)]
	internal sealed class HTMLInputTextElementEvents_SinkHelper : HTMLInputTextElementEvents
	{
		// Token: 0x060178E1 RID: 96481 RVA: 0x000271AC File Offset: 0x000261AC
		public override void onabort()
		{
			if (this.m_onabortDelegate != null)
			{
				this.m_onabortDelegate();
				return;
			}
		}

		// Token: 0x060178E2 RID: 96482 RVA: 0x000271D8 File Offset: 0x000261D8
		public override void onerror()
		{
			if (this.m_onerrorDelegate != null)
			{
				this.m_onerrorDelegate();
				return;
			}
		}

		// Token: 0x060178E3 RID: 96483 RVA: 0x00027204 File Offset: 0x00026204
		public override void onload()
		{
			if (this.m_onloadDelegate != null)
			{
				this.m_onloadDelegate();
				return;
			}
		}

		// Token: 0x060178E4 RID: 96484 RVA: 0x00027230 File Offset: 0x00026230
		public override void onselect()
		{
			if (this.m_onselectDelegate != null)
			{
				this.m_onselectDelegate();
				return;
			}
		}

		// Token: 0x060178E5 RID: 96485 RVA: 0x0002725C File Offset: 0x0002625C
		public override bool onchange()
		{
			return this.m_onchangeDelegate != null && this.m_onchangeDelegate();
		}

		// Token: 0x060178E6 RID: 96486 RVA: 0x00027288 File Offset: 0x00026288
		public override void onfocusout()
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate();
				return;
			}
		}

		// Token: 0x060178E7 RID: 96487 RVA: 0x000272B4 File Offset: 0x000262B4
		public override void onfocusin()
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate();
				return;
			}
		}

		// Token: 0x060178E8 RID: 96488 RVA: 0x000272E0 File Offset: 0x000262E0
		public override void ondeactivate()
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate();
				return;
			}
		}

		// Token: 0x060178E9 RID: 96489 RVA: 0x0002730C File Offset: 0x0002630C
		public override void onactivate()
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate();
				return;
			}
		}

		// Token: 0x060178EA RID: 96490 RVA: 0x00027338 File Offset: 0x00026338
		public override bool onmousewheel()
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate();
		}

		// Token: 0x060178EB RID: 96491 RVA: 0x00027364 File Offset: 0x00026364
		public override void onmouseleave()
		{
			if (this.m_onmouseleaveDelegate != null)
			{
				this.m_onmouseleaveDelegate();
				return;
			}
		}

		// Token: 0x060178EC RID: 96492 RVA: 0x00027390 File Offset: 0x00026390
		public override void onmouseenter()
		{
			if (this.m_onmouseenterDelegate != null)
			{
				this.m_onmouseenterDelegate();
				return;
			}
		}

		// Token: 0x060178ED RID: 96493 RVA: 0x000273BC File Offset: 0x000263BC
		public override void onresizeend()
		{
			if (this.m_onresizeendDelegate != null)
			{
				this.m_onresizeendDelegate();
				return;
			}
		}

		// Token: 0x060178EE RID: 96494 RVA: 0x000273E8 File Offset: 0x000263E8
		public override bool onresizestart()
		{
			return this.m_onresizestartDelegate != null && this.m_onresizestartDelegate();
		}

		// Token: 0x060178EF RID: 96495 RVA: 0x00027414 File Offset: 0x00026414
		public override void onmoveend()
		{
			if (this.m_onmoveendDelegate != null)
			{
				this.m_onmoveendDelegate();
				return;
			}
		}

		// Token: 0x060178F0 RID: 96496 RVA: 0x00027440 File Offset: 0x00026440
		public override bool onmovestart()
		{
			return this.m_onmovestartDelegate != null && this.m_onmovestartDelegate();
		}

		// Token: 0x060178F1 RID: 96497 RVA: 0x0002746C File Offset: 0x0002646C
		public override bool oncontrolselect()
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate();
		}

		// Token: 0x060178F2 RID: 96498 RVA: 0x00027498 File Offset: 0x00026498
		public override void onmove()
		{
			if (this.m_onmoveDelegate != null)
			{
				this.m_onmoveDelegate();
				return;
			}
		}

		// Token: 0x060178F3 RID: 96499 RVA: 0x000274C4 File Offset: 0x000264C4
		public override bool onbeforeactivate()
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate();
		}

		// Token: 0x060178F4 RID: 96500 RVA: 0x000274F0 File Offset: 0x000264F0
		public override bool onbeforedeactivate()
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate();
		}

		// Token: 0x060178F5 RID: 96501 RVA: 0x0002751C File Offset: 0x0002651C
		public override void onpage()
		{
			if (this.m_onpageDelegate != null)
			{
				this.m_onpageDelegate();
				return;
			}
		}

		// Token: 0x060178F6 RID: 96502 RVA: 0x00027548 File Offset: 0x00026548
		public override void onlayoutcomplete()
		{
			if (this.m_onlayoutcompleteDelegate != null)
			{
				this.m_onlayoutcompleteDelegate();
				return;
			}
		}

		// Token: 0x060178F7 RID: 96503 RVA: 0x00027574 File Offset: 0x00026574
		public override void onbeforeeditfocus()
		{
			if (this.m_onbeforeeditfocusDelegate != null)
			{
				this.m_onbeforeeditfocusDelegate();
				return;
			}
		}

		// Token: 0x060178F8 RID: 96504 RVA: 0x000275A0 File Offset: 0x000265A0
		public override void onreadystatechange()
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate();
				return;
			}
		}

		// Token: 0x060178F9 RID: 96505 RVA: 0x000275CC File Offset: 0x000265CC
		public override void oncellchange()
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate();
				return;
			}
		}

		// Token: 0x060178FA RID: 96506 RVA: 0x000275F8 File Offset: 0x000265F8
		public override void onrowsinserted()
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate();
				return;
			}
		}

		// Token: 0x060178FB RID: 96507 RVA: 0x00027624 File Offset: 0x00026624
		public override void onrowsdelete()
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate();
				return;
			}
		}

		// Token: 0x060178FC RID: 96508 RVA: 0x00027650 File Offset: 0x00026650
		public override bool oncontextmenu()
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate();
		}

		// Token: 0x060178FD RID: 96509 RVA: 0x0002767C File Offset: 0x0002667C
		public override bool onpaste()
		{
			return this.m_onpasteDelegate != null && this.m_onpasteDelegate();
		}

		// Token: 0x060178FE RID: 96510 RVA: 0x000276A8 File Offset: 0x000266A8
		public override bool onbeforepaste()
		{
			return this.m_onbeforepasteDelegate != null && this.m_onbeforepasteDelegate();
		}

		// Token: 0x060178FF RID: 96511 RVA: 0x000276D4 File Offset: 0x000266D4
		public override bool oncopy()
		{
			return this.m_oncopyDelegate != null && this.m_oncopyDelegate();
		}

		// Token: 0x06017900 RID: 96512 RVA: 0x00027700 File Offset: 0x00026700
		public override bool onbeforecopy()
		{
			return this.m_onbeforecopyDelegate != null && this.m_onbeforecopyDelegate();
		}

		// Token: 0x06017901 RID: 96513 RVA: 0x0002772C File Offset: 0x0002672C
		public override bool oncut()
		{
			return this.m_oncutDelegate != null && this.m_oncutDelegate();
		}

		// Token: 0x06017902 RID: 96514 RVA: 0x00027758 File Offset: 0x00026758
		public override bool onbeforecut()
		{
			return this.m_onbeforecutDelegate != null && this.m_onbeforecutDelegate();
		}

		// Token: 0x06017903 RID: 96515 RVA: 0x00027784 File Offset: 0x00026784
		public override bool ondrop()
		{
			return this.m_ondropDelegate != null && this.m_ondropDelegate();
		}

		// Token: 0x06017904 RID: 96516 RVA: 0x000277B0 File Offset: 0x000267B0
		public override void ondragleave()
		{
			if (this.m_ondragleaveDelegate != null)
			{
				this.m_ondragleaveDelegate();
				return;
			}
		}

		// Token: 0x06017905 RID: 96517 RVA: 0x000277DC File Offset: 0x000267DC
		public override bool ondragover()
		{
			return this.m_ondragoverDelegate != null && this.m_ondragoverDelegate();
		}

		// Token: 0x06017906 RID: 96518 RVA: 0x00027808 File Offset: 0x00026808
		public override bool ondragenter()
		{
			return this.m_ondragenterDelegate != null && this.m_ondragenterDelegate();
		}

		// Token: 0x06017907 RID: 96519 RVA: 0x00027834 File Offset: 0x00026834
		public override void ondragend()
		{
			if (this.m_ondragendDelegate != null)
			{
				this.m_ondragendDelegate();
				return;
			}
		}

		// Token: 0x06017908 RID: 96520 RVA: 0x00027860 File Offset: 0x00026860
		public override bool ondrag()
		{
			return this.m_ondragDelegate != null && this.m_ondragDelegate();
		}

		// Token: 0x06017909 RID: 96521 RVA: 0x0002788C File Offset: 0x0002688C
		public override void onresize()
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate();
				return;
			}
		}

		// Token: 0x0601790A RID: 96522 RVA: 0x000278B8 File Offset: 0x000268B8
		public override void onblur()
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate();
				return;
			}
		}

		// Token: 0x0601790B RID: 96523 RVA: 0x000278E4 File Offset: 0x000268E4
		public override void onfocus()
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate();
				return;
			}
		}

		// Token: 0x0601790C RID: 96524 RVA: 0x00027910 File Offset: 0x00026910
		public override void onscroll()
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate();
				return;
			}
		}

		// Token: 0x0601790D RID: 96525 RVA: 0x0002793C File Offset: 0x0002693C
		public override void onpropertychange()
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate();
				return;
			}
		}

		// Token: 0x0601790E RID: 96526 RVA: 0x00027968 File Offset: 0x00026968
		public override void onlosecapture()
		{
			if (this.m_onlosecaptureDelegate != null)
			{
				this.m_onlosecaptureDelegate();
				return;
			}
		}

		// Token: 0x0601790F RID: 96527 RVA: 0x00027994 File Offset: 0x00026994
		public override void ondatasetcomplete()
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate();
				return;
			}
		}

		// Token: 0x06017910 RID: 96528 RVA: 0x000279C0 File Offset: 0x000269C0
		public override void ondataavailable()
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate();
				return;
			}
		}

		// Token: 0x06017911 RID: 96529 RVA: 0x000279EC File Offset: 0x000269EC
		public override void ondatasetchanged()
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate();
				return;
			}
		}

		// Token: 0x06017912 RID: 96530 RVA: 0x00027A18 File Offset: 0x00026A18
		public override void onrowenter()
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate();
				return;
			}
		}

		// Token: 0x06017913 RID: 96531 RVA: 0x00027A44 File Offset: 0x00026A44
		public override bool onrowexit()
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate();
		}

		// Token: 0x06017914 RID: 96532 RVA: 0x00027A70 File Offset: 0x00026A70
		public override bool onerrorupdate()
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate();
		}

		// Token: 0x06017915 RID: 96533 RVA: 0x00027A9C File Offset: 0x00026A9C
		public override void onafterupdate()
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate();
				return;
			}
		}

		// Token: 0x06017916 RID: 96534 RVA: 0x00027AC8 File Offset: 0x00026AC8
		public override bool onbeforeupdate()
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate();
		}

		// Token: 0x06017917 RID: 96535 RVA: 0x00027AF4 File Offset: 0x00026AF4
		public override bool ondragstart()
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate();
		}

		// Token: 0x06017918 RID: 96536 RVA: 0x00027B20 File Offset: 0x00026B20
		public override void onfilterchange()
		{
			if (this.m_onfilterchangeDelegate != null)
			{
				this.m_onfilterchangeDelegate();
				return;
			}
		}

		// Token: 0x06017919 RID: 96537 RVA: 0x00027B4C File Offset: 0x00026B4C
		public override bool onselectstart()
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate();
		}

		// Token: 0x0601791A RID: 96538 RVA: 0x00027B78 File Offset: 0x00026B78
		public override void onmouseup()
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate();
				return;
			}
		}

		// Token: 0x0601791B RID: 96539 RVA: 0x00027BA4 File Offset: 0x00026BA4
		public override void onmousedown()
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate();
				return;
			}
		}

		// Token: 0x0601791C RID: 96540 RVA: 0x00027BD0 File Offset: 0x00026BD0
		public override void onmousemove()
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate();
				return;
			}
		}

		// Token: 0x0601791D RID: 96541 RVA: 0x00027BFC File Offset: 0x00026BFC
		public override void onmouseover()
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate();
				return;
			}
		}

		// Token: 0x0601791E RID: 96542 RVA: 0x00027C28 File Offset: 0x00026C28
		public override void onmouseout()
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate();
				return;
			}
		}

		// Token: 0x0601791F RID: 96543 RVA: 0x00027C54 File Offset: 0x00026C54
		public override void onkeyup()
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate();
				return;
			}
		}

		// Token: 0x06017920 RID: 96544 RVA: 0x00027C80 File Offset: 0x00026C80
		public override void onkeydown()
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate();
				return;
			}
		}

		// Token: 0x06017921 RID: 96545 RVA: 0x00027CAC File Offset: 0x00026CAC
		public override bool onkeypress()
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate();
		}

		// Token: 0x06017922 RID: 96546 RVA: 0x00027CD8 File Offset: 0x00026CD8
		public override bool ondblclick()
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate();
		}

		// Token: 0x06017923 RID: 96547 RVA: 0x00027D04 File Offset: 0x00026D04
		public override bool onclick()
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate();
		}

		// Token: 0x06017924 RID: 96548 RVA: 0x00027D30 File Offset: 0x00026D30
		public override bool onhelp()
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate();
		}

		// Token: 0x06017925 RID: 96549 RVA: 0x00027D5C File Offset: 0x00026D5C
		internal HTMLInputTextElementEvents_SinkHelper()
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

		// Token: 0x04000626 RID: 1574
		public HTMLInputTextElementEvents_onabortEventHandler m_onabortDelegate;

		// Token: 0x04000627 RID: 1575
		public HTMLInputTextElementEvents_onerrorEventHandler m_onerrorDelegate;

		// Token: 0x04000628 RID: 1576
		public HTMLInputTextElementEvents_onloadEventHandler m_onloadDelegate;

		// Token: 0x04000629 RID: 1577
		public HTMLInputTextElementEvents_onselectEventHandler m_onselectDelegate;

		// Token: 0x0400062A RID: 1578
		public HTMLInputTextElementEvents_onchangeEventHandler m_onchangeDelegate;

		// Token: 0x0400062B RID: 1579
		public HTMLInputTextElementEvents_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x0400062C RID: 1580
		public HTMLInputTextElementEvents_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x0400062D RID: 1581
		public HTMLInputTextElementEvents_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x0400062E RID: 1582
		public HTMLInputTextElementEvents_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x0400062F RID: 1583
		public HTMLInputTextElementEvents_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x04000630 RID: 1584
		public HTMLInputTextElementEvents_onmouseleaveEventHandler m_onmouseleaveDelegate;

		// Token: 0x04000631 RID: 1585
		public HTMLInputTextElementEvents_onmouseenterEventHandler m_onmouseenterDelegate;

		// Token: 0x04000632 RID: 1586
		public HTMLInputTextElementEvents_onresizeendEventHandler m_onresizeendDelegate;

		// Token: 0x04000633 RID: 1587
		public HTMLInputTextElementEvents_onresizestartEventHandler m_onresizestartDelegate;

		// Token: 0x04000634 RID: 1588
		public HTMLInputTextElementEvents_onmoveendEventHandler m_onmoveendDelegate;

		// Token: 0x04000635 RID: 1589
		public HTMLInputTextElementEvents_onmovestartEventHandler m_onmovestartDelegate;

		// Token: 0x04000636 RID: 1590
		public HTMLInputTextElementEvents_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x04000637 RID: 1591
		public HTMLInputTextElementEvents_onmoveEventHandler m_onmoveDelegate;

		// Token: 0x04000638 RID: 1592
		public HTMLInputTextElementEvents_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x04000639 RID: 1593
		public HTMLInputTextElementEvents_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x0400063A RID: 1594
		public HTMLInputTextElementEvents_onpageEventHandler m_onpageDelegate;

		// Token: 0x0400063B RID: 1595
		public HTMLInputTextElementEvents_onlayoutcompleteEventHandler m_onlayoutcompleteDelegate;

		// Token: 0x0400063C RID: 1596
		public HTMLInputTextElementEvents_onbeforeeditfocusEventHandler m_onbeforeeditfocusDelegate;

		// Token: 0x0400063D RID: 1597
		public HTMLInputTextElementEvents_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x0400063E RID: 1598
		public HTMLInputTextElementEvents_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x0400063F RID: 1599
		public HTMLInputTextElementEvents_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x04000640 RID: 1600
		public HTMLInputTextElementEvents_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x04000641 RID: 1601
		public HTMLInputTextElementEvents_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x04000642 RID: 1602
		public HTMLInputTextElementEvents_onpasteEventHandler m_onpasteDelegate;

		// Token: 0x04000643 RID: 1603
		public HTMLInputTextElementEvents_onbeforepasteEventHandler m_onbeforepasteDelegate;

		// Token: 0x04000644 RID: 1604
		public HTMLInputTextElementEvents_oncopyEventHandler m_oncopyDelegate;

		// Token: 0x04000645 RID: 1605
		public HTMLInputTextElementEvents_onbeforecopyEventHandler m_onbeforecopyDelegate;

		// Token: 0x04000646 RID: 1606
		public HTMLInputTextElementEvents_oncutEventHandler m_oncutDelegate;

		// Token: 0x04000647 RID: 1607
		public HTMLInputTextElementEvents_onbeforecutEventHandler m_onbeforecutDelegate;

		// Token: 0x04000648 RID: 1608
		public HTMLInputTextElementEvents_ondropEventHandler m_ondropDelegate;

		// Token: 0x04000649 RID: 1609
		public HTMLInputTextElementEvents_ondragleaveEventHandler m_ondragleaveDelegate;

		// Token: 0x0400064A RID: 1610
		public HTMLInputTextElementEvents_ondragoverEventHandler m_ondragoverDelegate;

		// Token: 0x0400064B RID: 1611
		public HTMLInputTextElementEvents_ondragenterEventHandler m_ondragenterDelegate;

		// Token: 0x0400064C RID: 1612
		public HTMLInputTextElementEvents_ondragendEventHandler m_ondragendDelegate;

		// Token: 0x0400064D RID: 1613
		public HTMLInputTextElementEvents_ondragEventHandler m_ondragDelegate;

		// Token: 0x0400064E RID: 1614
		public HTMLInputTextElementEvents_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x0400064F RID: 1615
		public HTMLInputTextElementEvents_onblurEventHandler m_onblurDelegate;

		// Token: 0x04000650 RID: 1616
		public HTMLInputTextElementEvents_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x04000651 RID: 1617
		public HTMLInputTextElementEvents_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x04000652 RID: 1618
		public HTMLInputTextElementEvents_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x04000653 RID: 1619
		public HTMLInputTextElementEvents_onlosecaptureEventHandler m_onlosecaptureDelegate;

		// Token: 0x04000654 RID: 1620
		public HTMLInputTextElementEvents_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x04000655 RID: 1621
		public HTMLInputTextElementEvents_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x04000656 RID: 1622
		public HTMLInputTextElementEvents_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x04000657 RID: 1623
		public HTMLInputTextElementEvents_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x04000658 RID: 1624
		public HTMLInputTextElementEvents_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x04000659 RID: 1625
		public HTMLInputTextElementEvents_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x0400065A RID: 1626
		public HTMLInputTextElementEvents_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x0400065B RID: 1627
		public HTMLInputTextElementEvents_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x0400065C RID: 1628
		public HTMLInputTextElementEvents_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x0400065D RID: 1629
		public HTMLInputTextElementEvents_onfilterchangeEventHandler m_onfilterchangeDelegate;

		// Token: 0x0400065E RID: 1630
		public HTMLInputTextElementEvents_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x0400065F RID: 1631
		public HTMLInputTextElementEvents_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x04000660 RID: 1632
		public HTMLInputTextElementEvents_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x04000661 RID: 1633
		public HTMLInputTextElementEvents_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x04000662 RID: 1634
		public HTMLInputTextElementEvents_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x04000663 RID: 1635
		public HTMLInputTextElementEvents_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x04000664 RID: 1636
		public HTMLInputTextElementEvents_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x04000665 RID: 1637
		public HTMLInputTextElementEvents_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x04000666 RID: 1638
		public HTMLInputTextElementEvents_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x04000667 RID: 1639
		public HTMLInputTextElementEvents_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x04000668 RID: 1640
		public HTMLInputTextElementEvents_onclickEventHandler m_onclickDelegate;

		// Token: 0x04000669 RID: 1641
		public HTMLInputTextElementEvents_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x0400066A RID: 1642
		public int m_dwCookie;
	}
}
