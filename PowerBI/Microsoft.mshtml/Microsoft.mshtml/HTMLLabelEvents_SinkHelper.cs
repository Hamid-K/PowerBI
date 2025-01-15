using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DC4 RID: 3524
	[ClassInterface(0)]
	internal sealed class HTMLLabelEvents_SinkHelper : HTMLLabelEvents
	{
		// Token: 0x06017675 RID: 95861 RVA: 0x0001119C File Offset: 0x0001019C
		public override void onfocusout()
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate();
				return;
			}
		}

		// Token: 0x06017676 RID: 95862 RVA: 0x000111C8 File Offset: 0x000101C8
		public override void onfocusin()
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate();
				return;
			}
		}

		// Token: 0x06017677 RID: 95863 RVA: 0x000111F4 File Offset: 0x000101F4
		public override void ondeactivate()
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate();
				return;
			}
		}

		// Token: 0x06017678 RID: 95864 RVA: 0x00011220 File Offset: 0x00010220
		public override void onactivate()
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate();
				return;
			}
		}

		// Token: 0x06017679 RID: 95865 RVA: 0x0001124C File Offset: 0x0001024C
		public override bool onmousewheel()
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate();
		}

		// Token: 0x0601767A RID: 95866 RVA: 0x00011278 File Offset: 0x00010278
		public override void onmouseleave()
		{
			if (this.m_onmouseleaveDelegate != null)
			{
				this.m_onmouseleaveDelegate();
				return;
			}
		}

		// Token: 0x0601767B RID: 95867 RVA: 0x000112A4 File Offset: 0x000102A4
		public override void onmouseenter()
		{
			if (this.m_onmouseenterDelegate != null)
			{
				this.m_onmouseenterDelegate();
				return;
			}
		}

		// Token: 0x0601767C RID: 95868 RVA: 0x000112D0 File Offset: 0x000102D0
		public override void onresizeend()
		{
			if (this.m_onresizeendDelegate != null)
			{
				this.m_onresizeendDelegate();
				return;
			}
		}

		// Token: 0x0601767D RID: 95869 RVA: 0x000112FC File Offset: 0x000102FC
		public override bool onresizestart()
		{
			return this.m_onresizestartDelegate != null && this.m_onresizestartDelegate();
		}

		// Token: 0x0601767E RID: 95870 RVA: 0x00011328 File Offset: 0x00010328
		public override void onmoveend()
		{
			if (this.m_onmoveendDelegate != null)
			{
				this.m_onmoveendDelegate();
				return;
			}
		}

		// Token: 0x0601767F RID: 95871 RVA: 0x00011354 File Offset: 0x00010354
		public override bool onmovestart()
		{
			return this.m_onmovestartDelegate != null && this.m_onmovestartDelegate();
		}

		// Token: 0x06017680 RID: 95872 RVA: 0x00011380 File Offset: 0x00010380
		public override bool oncontrolselect()
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate();
		}

		// Token: 0x06017681 RID: 95873 RVA: 0x000113AC File Offset: 0x000103AC
		public override void onmove()
		{
			if (this.m_onmoveDelegate != null)
			{
				this.m_onmoveDelegate();
				return;
			}
		}

		// Token: 0x06017682 RID: 95874 RVA: 0x000113D8 File Offset: 0x000103D8
		public override bool onbeforeactivate()
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate();
		}

		// Token: 0x06017683 RID: 95875 RVA: 0x00011404 File Offset: 0x00010404
		public override bool onbeforedeactivate()
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate();
		}

		// Token: 0x06017684 RID: 95876 RVA: 0x00011430 File Offset: 0x00010430
		public override void onpage()
		{
			if (this.m_onpageDelegate != null)
			{
				this.m_onpageDelegate();
				return;
			}
		}

		// Token: 0x06017685 RID: 95877 RVA: 0x0001145C File Offset: 0x0001045C
		public override void onlayoutcomplete()
		{
			if (this.m_onlayoutcompleteDelegate != null)
			{
				this.m_onlayoutcompleteDelegate();
				return;
			}
		}

		// Token: 0x06017686 RID: 95878 RVA: 0x00011488 File Offset: 0x00010488
		public override void onbeforeeditfocus()
		{
			if (this.m_onbeforeeditfocusDelegate != null)
			{
				this.m_onbeforeeditfocusDelegate();
				return;
			}
		}

		// Token: 0x06017687 RID: 95879 RVA: 0x000114B4 File Offset: 0x000104B4
		public override void onreadystatechange()
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate();
				return;
			}
		}

		// Token: 0x06017688 RID: 95880 RVA: 0x000114E0 File Offset: 0x000104E0
		public override void oncellchange()
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate();
				return;
			}
		}

		// Token: 0x06017689 RID: 95881 RVA: 0x0001150C File Offset: 0x0001050C
		public override void onrowsinserted()
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate();
				return;
			}
		}

		// Token: 0x0601768A RID: 95882 RVA: 0x00011538 File Offset: 0x00010538
		public override void onrowsdelete()
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate();
				return;
			}
		}

		// Token: 0x0601768B RID: 95883 RVA: 0x00011564 File Offset: 0x00010564
		public override bool oncontextmenu()
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate();
		}

		// Token: 0x0601768C RID: 95884 RVA: 0x00011590 File Offset: 0x00010590
		public override bool onpaste()
		{
			return this.m_onpasteDelegate != null && this.m_onpasteDelegate();
		}

		// Token: 0x0601768D RID: 95885 RVA: 0x000115BC File Offset: 0x000105BC
		public override bool onbeforepaste()
		{
			return this.m_onbeforepasteDelegate != null && this.m_onbeforepasteDelegate();
		}

		// Token: 0x0601768E RID: 95886 RVA: 0x000115E8 File Offset: 0x000105E8
		public override bool oncopy()
		{
			return this.m_oncopyDelegate != null && this.m_oncopyDelegate();
		}

		// Token: 0x0601768F RID: 95887 RVA: 0x00011614 File Offset: 0x00010614
		public override bool onbeforecopy()
		{
			return this.m_onbeforecopyDelegate != null && this.m_onbeforecopyDelegate();
		}

		// Token: 0x06017690 RID: 95888 RVA: 0x00011640 File Offset: 0x00010640
		public override bool oncut()
		{
			return this.m_oncutDelegate != null && this.m_oncutDelegate();
		}

		// Token: 0x06017691 RID: 95889 RVA: 0x0001166C File Offset: 0x0001066C
		public override bool onbeforecut()
		{
			return this.m_onbeforecutDelegate != null && this.m_onbeforecutDelegate();
		}

		// Token: 0x06017692 RID: 95890 RVA: 0x00011698 File Offset: 0x00010698
		public override bool ondrop()
		{
			return this.m_ondropDelegate != null && this.m_ondropDelegate();
		}

		// Token: 0x06017693 RID: 95891 RVA: 0x000116C4 File Offset: 0x000106C4
		public override void ondragleave()
		{
			if (this.m_ondragleaveDelegate != null)
			{
				this.m_ondragleaveDelegate();
				return;
			}
		}

		// Token: 0x06017694 RID: 95892 RVA: 0x000116F0 File Offset: 0x000106F0
		public override bool ondragover()
		{
			return this.m_ondragoverDelegate != null && this.m_ondragoverDelegate();
		}

		// Token: 0x06017695 RID: 95893 RVA: 0x0001171C File Offset: 0x0001071C
		public override bool ondragenter()
		{
			return this.m_ondragenterDelegate != null && this.m_ondragenterDelegate();
		}

		// Token: 0x06017696 RID: 95894 RVA: 0x00011748 File Offset: 0x00010748
		public override void ondragend()
		{
			if (this.m_ondragendDelegate != null)
			{
				this.m_ondragendDelegate();
				return;
			}
		}

		// Token: 0x06017697 RID: 95895 RVA: 0x00011774 File Offset: 0x00010774
		public override bool ondrag()
		{
			return this.m_ondragDelegate != null && this.m_ondragDelegate();
		}

		// Token: 0x06017698 RID: 95896 RVA: 0x000117A0 File Offset: 0x000107A0
		public override void onresize()
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate();
				return;
			}
		}

		// Token: 0x06017699 RID: 95897 RVA: 0x000117CC File Offset: 0x000107CC
		public override void onblur()
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate();
				return;
			}
		}

		// Token: 0x0601769A RID: 95898 RVA: 0x000117F8 File Offset: 0x000107F8
		public override void onfocus()
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate();
				return;
			}
		}

		// Token: 0x0601769B RID: 95899 RVA: 0x00011824 File Offset: 0x00010824
		public override void onscroll()
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate();
				return;
			}
		}

		// Token: 0x0601769C RID: 95900 RVA: 0x00011850 File Offset: 0x00010850
		public override void onpropertychange()
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate();
				return;
			}
		}

		// Token: 0x0601769D RID: 95901 RVA: 0x0001187C File Offset: 0x0001087C
		public override void onlosecapture()
		{
			if (this.m_onlosecaptureDelegate != null)
			{
				this.m_onlosecaptureDelegate();
				return;
			}
		}

		// Token: 0x0601769E RID: 95902 RVA: 0x000118A8 File Offset: 0x000108A8
		public override void ondatasetcomplete()
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate();
				return;
			}
		}

		// Token: 0x0601769F RID: 95903 RVA: 0x000118D4 File Offset: 0x000108D4
		public override void ondataavailable()
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate();
				return;
			}
		}

		// Token: 0x060176A0 RID: 95904 RVA: 0x00011900 File Offset: 0x00010900
		public override void ondatasetchanged()
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate();
				return;
			}
		}

		// Token: 0x060176A1 RID: 95905 RVA: 0x0001192C File Offset: 0x0001092C
		public override void onrowenter()
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate();
				return;
			}
		}

		// Token: 0x060176A2 RID: 95906 RVA: 0x00011958 File Offset: 0x00010958
		public override bool onrowexit()
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate();
		}

		// Token: 0x060176A3 RID: 95907 RVA: 0x00011984 File Offset: 0x00010984
		public override bool onerrorupdate()
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate();
		}

		// Token: 0x060176A4 RID: 95908 RVA: 0x000119B0 File Offset: 0x000109B0
		public override void onafterupdate()
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate();
				return;
			}
		}

		// Token: 0x060176A5 RID: 95909 RVA: 0x000119DC File Offset: 0x000109DC
		public override bool onbeforeupdate()
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate();
		}

		// Token: 0x060176A6 RID: 95910 RVA: 0x00011A08 File Offset: 0x00010A08
		public override bool ondragstart()
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate();
		}

		// Token: 0x060176A7 RID: 95911 RVA: 0x00011A34 File Offset: 0x00010A34
		public override void onfilterchange()
		{
			if (this.m_onfilterchangeDelegate != null)
			{
				this.m_onfilterchangeDelegate();
				return;
			}
		}

		// Token: 0x060176A8 RID: 95912 RVA: 0x00011A60 File Offset: 0x00010A60
		public override bool onselectstart()
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate();
		}

		// Token: 0x060176A9 RID: 95913 RVA: 0x00011A8C File Offset: 0x00010A8C
		public override void onmouseup()
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate();
				return;
			}
		}

		// Token: 0x060176AA RID: 95914 RVA: 0x00011AB8 File Offset: 0x00010AB8
		public override void onmousedown()
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate();
				return;
			}
		}

		// Token: 0x060176AB RID: 95915 RVA: 0x00011AE4 File Offset: 0x00010AE4
		public override void onmousemove()
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate();
				return;
			}
		}

		// Token: 0x060176AC RID: 95916 RVA: 0x00011B10 File Offset: 0x00010B10
		public override void onmouseover()
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate();
				return;
			}
		}

		// Token: 0x060176AD RID: 95917 RVA: 0x00011B3C File Offset: 0x00010B3C
		public override void onmouseout()
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate();
				return;
			}
		}

		// Token: 0x060176AE RID: 95918 RVA: 0x00011B68 File Offset: 0x00010B68
		public override void onkeyup()
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate();
				return;
			}
		}

		// Token: 0x060176AF RID: 95919 RVA: 0x00011B94 File Offset: 0x00010B94
		public override void onkeydown()
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate();
				return;
			}
		}

		// Token: 0x060176B0 RID: 95920 RVA: 0x00011BC0 File Offset: 0x00010BC0
		public override bool onkeypress()
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate();
		}

		// Token: 0x060176B1 RID: 95921 RVA: 0x00011BEC File Offset: 0x00010BEC
		public override bool ondblclick()
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate();
		}

		// Token: 0x060176B2 RID: 95922 RVA: 0x00011C18 File Offset: 0x00010C18
		public override bool onclick()
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate();
		}

		// Token: 0x060176B3 RID: 95923 RVA: 0x00011C44 File Offset: 0x00010C44
		public override bool onhelp()
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate();
		}

		// Token: 0x060176B4 RID: 95924 RVA: 0x00011C70 File Offset: 0x00010C70
		internal HTMLLabelEvents_SinkHelper()
		{
			this.m_dwCookie = 0;
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

		// Token: 0x0400054E RID: 1358
		public HTMLLabelEvents_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x0400054F RID: 1359
		public HTMLLabelEvents_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x04000550 RID: 1360
		public HTMLLabelEvents_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x04000551 RID: 1361
		public HTMLLabelEvents_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x04000552 RID: 1362
		public HTMLLabelEvents_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x04000553 RID: 1363
		public HTMLLabelEvents_onmouseleaveEventHandler m_onmouseleaveDelegate;

		// Token: 0x04000554 RID: 1364
		public HTMLLabelEvents_onmouseenterEventHandler m_onmouseenterDelegate;

		// Token: 0x04000555 RID: 1365
		public HTMLLabelEvents_onresizeendEventHandler m_onresizeendDelegate;

		// Token: 0x04000556 RID: 1366
		public HTMLLabelEvents_onresizestartEventHandler m_onresizestartDelegate;

		// Token: 0x04000557 RID: 1367
		public HTMLLabelEvents_onmoveendEventHandler m_onmoveendDelegate;

		// Token: 0x04000558 RID: 1368
		public HTMLLabelEvents_onmovestartEventHandler m_onmovestartDelegate;

		// Token: 0x04000559 RID: 1369
		public HTMLLabelEvents_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x0400055A RID: 1370
		public HTMLLabelEvents_onmoveEventHandler m_onmoveDelegate;

		// Token: 0x0400055B RID: 1371
		public HTMLLabelEvents_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x0400055C RID: 1372
		public HTMLLabelEvents_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x0400055D RID: 1373
		public HTMLLabelEvents_onpageEventHandler m_onpageDelegate;

		// Token: 0x0400055E RID: 1374
		public HTMLLabelEvents_onlayoutcompleteEventHandler m_onlayoutcompleteDelegate;

		// Token: 0x0400055F RID: 1375
		public HTMLLabelEvents_onbeforeeditfocusEventHandler m_onbeforeeditfocusDelegate;

		// Token: 0x04000560 RID: 1376
		public HTMLLabelEvents_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x04000561 RID: 1377
		public HTMLLabelEvents_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x04000562 RID: 1378
		public HTMLLabelEvents_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x04000563 RID: 1379
		public HTMLLabelEvents_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x04000564 RID: 1380
		public HTMLLabelEvents_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x04000565 RID: 1381
		public HTMLLabelEvents_onpasteEventHandler m_onpasteDelegate;

		// Token: 0x04000566 RID: 1382
		public HTMLLabelEvents_onbeforepasteEventHandler m_onbeforepasteDelegate;

		// Token: 0x04000567 RID: 1383
		public HTMLLabelEvents_oncopyEventHandler m_oncopyDelegate;

		// Token: 0x04000568 RID: 1384
		public HTMLLabelEvents_onbeforecopyEventHandler m_onbeforecopyDelegate;

		// Token: 0x04000569 RID: 1385
		public HTMLLabelEvents_oncutEventHandler m_oncutDelegate;

		// Token: 0x0400056A RID: 1386
		public HTMLLabelEvents_onbeforecutEventHandler m_onbeforecutDelegate;

		// Token: 0x0400056B RID: 1387
		public HTMLLabelEvents_ondropEventHandler m_ondropDelegate;

		// Token: 0x0400056C RID: 1388
		public HTMLLabelEvents_ondragleaveEventHandler m_ondragleaveDelegate;

		// Token: 0x0400056D RID: 1389
		public HTMLLabelEvents_ondragoverEventHandler m_ondragoverDelegate;

		// Token: 0x0400056E RID: 1390
		public HTMLLabelEvents_ondragenterEventHandler m_ondragenterDelegate;

		// Token: 0x0400056F RID: 1391
		public HTMLLabelEvents_ondragendEventHandler m_ondragendDelegate;

		// Token: 0x04000570 RID: 1392
		public HTMLLabelEvents_ondragEventHandler m_ondragDelegate;

		// Token: 0x04000571 RID: 1393
		public HTMLLabelEvents_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x04000572 RID: 1394
		public HTMLLabelEvents_onblurEventHandler m_onblurDelegate;

		// Token: 0x04000573 RID: 1395
		public HTMLLabelEvents_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x04000574 RID: 1396
		public HTMLLabelEvents_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x04000575 RID: 1397
		public HTMLLabelEvents_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x04000576 RID: 1398
		public HTMLLabelEvents_onlosecaptureEventHandler m_onlosecaptureDelegate;

		// Token: 0x04000577 RID: 1399
		public HTMLLabelEvents_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x04000578 RID: 1400
		public HTMLLabelEvents_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x04000579 RID: 1401
		public HTMLLabelEvents_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x0400057A RID: 1402
		public HTMLLabelEvents_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x0400057B RID: 1403
		public HTMLLabelEvents_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x0400057C RID: 1404
		public HTMLLabelEvents_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x0400057D RID: 1405
		public HTMLLabelEvents_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x0400057E RID: 1406
		public HTMLLabelEvents_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x0400057F RID: 1407
		public HTMLLabelEvents_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x04000580 RID: 1408
		public HTMLLabelEvents_onfilterchangeEventHandler m_onfilterchangeDelegate;

		// Token: 0x04000581 RID: 1409
		public HTMLLabelEvents_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x04000582 RID: 1410
		public HTMLLabelEvents_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x04000583 RID: 1411
		public HTMLLabelEvents_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x04000584 RID: 1412
		public HTMLLabelEvents_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x04000585 RID: 1413
		public HTMLLabelEvents_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x04000586 RID: 1414
		public HTMLLabelEvents_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x04000587 RID: 1415
		public HTMLLabelEvents_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x04000588 RID: 1416
		public HTMLLabelEvents_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x04000589 RID: 1417
		public HTMLLabelEvents_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x0400058A RID: 1418
		public HTMLLabelEvents_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x0400058B RID: 1419
		public HTMLLabelEvents_onclickEventHandler m_onclickDelegate;

		// Token: 0x0400058C RID: 1420
		public HTMLLabelEvents_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x0400058D RID: 1421
		public int m_dwCookie;
	}
}
