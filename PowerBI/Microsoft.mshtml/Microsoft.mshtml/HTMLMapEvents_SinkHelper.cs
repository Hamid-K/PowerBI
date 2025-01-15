using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DE4 RID: 3556
	[ClassInterface(0)]
	internal sealed class HTMLMapEvents_SinkHelper : HTMLMapEvents
	{
		// Token: 0x06018193 RID: 98707 RVA: 0x000764A0 File Offset: 0x000754A0
		public override void onfocusout()
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate();
				return;
			}
		}

		// Token: 0x06018194 RID: 98708 RVA: 0x000764CC File Offset: 0x000754CC
		public override void onfocusin()
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate();
				return;
			}
		}

		// Token: 0x06018195 RID: 98709 RVA: 0x000764F8 File Offset: 0x000754F8
		public override void ondeactivate()
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate();
				return;
			}
		}

		// Token: 0x06018196 RID: 98710 RVA: 0x00076524 File Offset: 0x00075524
		public override void onactivate()
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate();
				return;
			}
		}

		// Token: 0x06018197 RID: 98711 RVA: 0x00076550 File Offset: 0x00075550
		public override bool onmousewheel()
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate();
		}

		// Token: 0x06018198 RID: 98712 RVA: 0x0007657C File Offset: 0x0007557C
		public override void onmouseleave()
		{
			if (this.m_onmouseleaveDelegate != null)
			{
				this.m_onmouseleaveDelegate();
				return;
			}
		}

		// Token: 0x06018199 RID: 98713 RVA: 0x000765A8 File Offset: 0x000755A8
		public override void onmouseenter()
		{
			if (this.m_onmouseenterDelegate != null)
			{
				this.m_onmouseenterDelegate();
				return;
			}
		}

		// Token: 0x0601819A RID: 98714 RVA: 0x000765D4 File Offset: 0x000755D4
		public override void onresizeend()
		{
			if (this.m_onresizeendDelegate != null)
			{
				this.m_onresizeendDelegate();
				return;
			}
		}

		// Token: 0x0601819B RID: 98715 RVA: 0x00076600 File Offset: 0x00075600
		public override bool onresizestart()
		{
			return this.m_onresizestartDelegate != null && this.m_onresizestartDelegate();
		}

		// Token: 0x0601819C RID: 98716 RVA: 0x0007662C File Offset: 0x0007562C
		public override void onmoveend()
		{
			if (this.m_onmoveendDelegate != null)
			{
				this.m_onmoveendDelegate();
				return;
			}
		}

		// Token: 0x0601819D RID: 98717 RVA: 0x00076658 File Offset: 0x00075658
		public override bool onmovestart()
		{
			return this.m_onmovestartDelegate != null && this.m_onmovestartDelegate();
		}

		// Token: 0x0601819E RID: 98718 RVA: 0x00076684 File Offset: 0x00075684
		public override bool oncontrolselect()
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate();
		}

		// Token: 0x0601819F RID: 98719 RVA: 0x000766B0 File Offset: 0x000756B0
		public override void onmove()
		{
			if (this.m_onmoveDelegate != null)
			{
				this.m_onmoveDelegate();
				return;
			}
		}

		// Token: 0x060181A0 RID: 98720 RVA: 0x000766DC File Offset: 0x000756DC
		public override bool onbeforeactivate()
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate();
		}

		// Token: 0x060181A1 RID: 98721 RVA: 0x00076708 File Offset: 0x00075708
		public override bool onbeforedeactivate()
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate();
		}

		// Token: 0x060181A2 RID: 98722 RVA: 0x00076734 File Offset: 0x00075734
		public override void onpage()
		{
			if (this.m_onpageDelegate != null)
			{
				this.m_onpageDelegate();
				return;
			}
		}

		// Token: 0x060181A3 RID: 98723 RVA: 0x00076760 File Offset: 0x00075760
		public override void onlayoutcomplete()
		{
			if (this.m_onlayoutcompleteDelegate != null)
			{
				this.m_onlayoutcompleteDelegate();
				return;
			}
		}

		// Token: 0x060181A4 RID: 98724 RVA: 0x0007678C File Offset: 0x0007578C
		public override void onbeforeeditfocus()
		{
			if (this.m_onbeforeeditfocusDelegate != null)
			{
				this.m_onbeforeeditfocusDelegate();
				return;
			}
		}

		// Token: 0x060181A5 RID: 98725 RVA: 0x000767B8 File Offset: 0x000757B8
		public override void onreadystatechange()
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate();
				return;
			}
		}

		// Token: 0x060181A6 RID: 98726 RVA: 0x000767E4 File Offset: 0x000757E4
		public override void oncellchange()
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate();
				return;
			}
		}

		// Token: 0x060181A7 RID: 98727 RVA: 0x00076810 File Offset: 0x00075810
		public override void onrowsinserted()
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate();
				return;
			}
		}

		// Token: 0x060181A8 RID: 98728 RVA: 0x0007683C File Offset: 0x0007583C
		public override void onrowsdelete()
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate();
				return;
			}
		}

		// Token: 0x060181A9 RID: 98729 RVA: 0x00076868 File Offset: 0x00075868
		public override bool oncontextmenu()
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate();
		}

		// Token: 0x060181AA RID: 98730 RVA: 0x00076894 File Offset: 0x00075894
		public override bool onpaste()
		{
			return this.m_onpasteDelegate != null && this.m_onpasteDelegate();
		}

		// Token: 0x060181AB RID: 98731 RVA: 0x000768C0 File Offset: 0x000758C0
		public override bool onbeforepaste()
		{
			return this.m_onbeforepasteDelegate != null && this.m_onbeforepasteDelegate();
		}

		// Token: 0x060181AC RID: 98732 RVA: 0x000768EC File Offset: 0x000758EC
		public override bool oncopy()
		{
			return this.m_oncopyDelegate != null && this.m_oncopyDelegate();
		}

		// Token: 0x060181AD RID: 98733 RVA: 0x00076918 File Offset: 0x00075918
		public override bool onbeforecopy()
		{
			return this.m_onbeforecopyDelegate != null && this.m_onbeforecopyDelegate();
		}

		// Token: 0x060181AE RID: 98734 RVA: 0x00076944 File Offset: 0x00075944
		public override bool oncut()
		{
			return this.m_oncutDelegate != null && this.m_oncutDelegate();
		}

		// Token: 0x060181AF RID: 98735 RVA: 0x00076970 File Offset: 0x00075970
		public override bool onbeforecut()
		{
			return this.m_onbeforecutDelegate != null && this.m_onbeforecutDelegate();
		}

		// Token: 0x060181B0 RID: 98736 RVA: 0x0007699C File Offset: 0x0007599C
		public override bool ondrop()
		{
			return this.m_ondropDelegate != null && this.m_ondropDelegate();
		}

		// Token: 0x060181B1 RID: 98737 RVA: 0x000769C8 File Offset: 0x000759C8
		public override void ondragleave()
		{
			if (this.m_ondragleaveDelegate != null)
			{
				this.m_ondragleaveDelegate();
				return;
			}
		}

		// Token: 0x060181B2 RID: 98738 RVA: 0x000769F4 File Offset: 0x000759F4
		public override bool ondragover()
		{
			return this.m_ondragoverDelegate != null && this.m_ondragoverDelegate();
		}

		// Token: 0x060181B3 RID: 98739 RVA: 0x00076A20 File Offset: 0x00075A20
		public override bool ondragenter()
		{
			return this.m_ondragenterDelegate != null && this.m_ondragenterDelegate();
		}

		// Token: 0x060181B4 RID: 98740 RVA: 0x00076A4C File Offset: 0x00075A4C
		public override void ondragend()
		{
			if (this.m_ondragendDelegate != null)
			{
				this.m_ondragendDelegate();
				return;
			}
		}

		// Token: 0x060181B5 RID: 98741 RVA: 0x00076A78 File Offset: 0x00075A78
		public override bool ondrag()
		{
			return this.m_ondragDelegate != null && this.m_ondragDelegate();
		}

		// Token: 0x060181B6 RID: 98742 RVA: 0x00076AA4 File Offset: 0x00075AA4
		public override void onresize()
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate();
				return;
			}
		}

		// Token: 0x060181B7 RID: 98743 RVA: 0x00076AD0 File Offset: 0x00075AD0
		public override void onblur()
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate();
				return;
			}
		}

		// Token: 0x060181B8 RID: 98744 RVA: 0x00076AFC File Offset: 0x00075AFC
		public override void onfocus()
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate();
				return;
			}
		}

		// Token: 0x060181B9 RID: 98745 RVA: 0x00076B28 File Offset: 0x00075B28
		public override void onscroll()
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate();
				return;
			}
		}

		// Token: 0x060181BA RID: 98746 RVA: 0x00076B54 File Offset: 0x00075B54
		public override void onpropertychange()
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate();
				return;
			}
		}

		// Token: 0x060181BB RID: 98747 RVA: 0x00076B80 File Offset: 0x00075B80
		public override void onlosecapture()
		{
			if (this.m_onlosecaptureDelegate != null)
			{
				this.m_onlosecaptureDelegate();
				return;
			}
		}

		// Token: 0x060181BC RID: 98748 RVA: 0x00076BAC File Offset: 0x00075BAC
		public override void ondatasetcomplete()
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate();
				return;
			}
		}

		// Token: 0x060181BD RID: 98749 RVA: 0x00076BD8 File Offset: 0x00075BD8
		public override void ondataavailable()
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate();
				return;
			}
		}

		// Token: 0x060181BE RID: 98750 RVA: 0x00076C04 File Offset: 0x00075C04
		public override void ondatasetchanged()
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate();
				return;
			}
		}

		// Token: 0x060181BF RID: 98751 RVA: 0x00076C30 File Offset: 0x00075C30
		public override void onrowenter()
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate();
				return;
			}
		}

		// Token: 0x060181C0 RID: 98752 RVA: 0x00076C5C File Offset: 0x00075C5C
		public override bool onrowexit()
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate();
		}

		// Token: 0x060181C1 RID: 98753 RVA: 0x00076C88 File Offset: 0x00075C88
		public override bool onerrorupdate()
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate();
		}

		// Token: 0x060181C2 RID: 98754 RVA: 0x00076CB4 File Offset: 0x00075CB4
		public override void onafterupdate()
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate();
				return;
			}
		}

		// Token: 0x060181C3 RID: 98755 RVA: 0x00076CE0 File Offset: 0x00075CE0
		public override bool onbeforeupdate()
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate();
		}

		// Token: 0x060181C4 RID: 98756 RVA: 0x00076D0C File Offset: 0x00075D0C
		public override bool ondragstart()
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate();
		}

		// Token: 0x060181C5 RID: 98757 RVA: 0x00076D38 File Offset: 0x00075D38
		public override void onfilterchange()
		{
			if (this.m_onfilterchangeDelegate != null)
			{
				this.m_onfilterchangeDelegate();
				return;
			}
		}

		// Token: 0x060181C6 RID: 98758 RVA: 0x00076D64 File Offset: 0x00075D64
		public override bool onselectstart()
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate();
		}

		// Token: 0x060181C7 RID: 98759 RVA: 0x00076D90 File Offset: 0x00075D90
		public override void onmouseup()
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate();
				return;
			}
		}

		// Token: 0x060181C8 RID: 98760 RVA: 0x00076DBC File Offset: 0x00075DBC
		public override void onmousedown()
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate();
				return;
			}
		}

		// Token: 0x060181C9 RID: 98761 RVA: 0x00076DE8 File Offset: 0x00075DE8
		public override void onmousemove()
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate();
				return;
			}
		}

		// Token: 0x060181CA RID: 98762 RVA: 0x00076E14 File Offset: 0x00075E14
		public override void onmouseover()
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate();
				return;
			}
		}

		// Token: 0x060181CB RID: 98763 RVA: 0x00076E40 File Offset: 0x00075E40
		public override void onmouseout()
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate();
				return;
			}
		}

		// Token: 0x060181CC RID: 98764 RVA: 0x00076E6C File Offset: 0x00075E6C
		public override void onkeyup()
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate();
				return;
			}
		}

		// Token: 0x060181CD RID: 98765 RVA: 0x00076E98 File Offset: 0x00075E98
		public override void onkeydown()
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate();
				return;
			}
		}

		// Token: 0x060181CE RID: 98766 RVA: 0x00076EC4 File Offset: 0x00075EC4
		public override bool onkeypress()
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate();
		}

		// Token: 0x060181CF RID: 98767 RVA: 0x00076EF0 File Offset: 0x00075EF0
		public override bool ondblclick()
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate();
		}

		// Token: 0x060181D0 RID: 98768 RVA: 0x00076F1C File Offset: 0x00075F1C
		public override bool onclick()
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate();
		}

		// Token: 0x060181D1 RID: 98769 RVA: 0x00076F48 File Offset: 0x00075F48
		public override bool onhelp()
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate();
		}

		// Token: 0x060181D2 RID: 98770 RVA: 0x00076F74 File Offset: 0x00075F74
		internal HTMLMapEvents_SinkHelper()
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

		// Token: 0x04000928 RID: 2344
		public HTMLMapEvents_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x04000929 RID: 2345
		public HTMLMapEvents_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x0400092A RID: 2346
		public HTMLMapEvents_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x0400092B RID: 2347
		public HTMLMapEvents_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x0400092C RID: 2348
		public HTMLMapEvents_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x0400092D RID: 2349
		public HTMLMapEvents_onmouseleaveEventHandler m_onmouseleaveDelegate;

		// Token: 0x0400092E RID: 2350
		public HTMLMapEvents_onmouseenterEventHandler m_onmouseenterDelegate;

		// Token: 0x0400092F RID: 2351
		public HTMLMapEvents_onresizeendEventHandler m_onresizeendDelegate;

		// Token: 0x04000930 RID: 2352
		public HTMLMapEvents_onresizestartEventHandler m_onresizestartDelegate;

		// Token: 0x04000931 RID: 2353
		public HTMLMapEvents_onmoveendEventHandler m_onmoveendDelegate;

		// Token: 0x04000932 RID: 2354
		public HTMLMapEvents_onmovestartEventHandler m_onmovestartDelegate;

		// Token: 0x04000933 RID: 2355
		public HTMLMapEvents_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x04000934 RID: 2356
		public HTMLMapEvents_onmoveEventHandler m_onmoveDelegate;

		// Token: 0x04000935 RID: 2357
		public HTMLMapEvents_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x04000936 RID: 2358
		public HTMLMapEvents_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x04000937 RID: 2359
		public HTMLMapEvents_onpageEventHandler m_onpageDelegate;

		// Token: 0x04000938 RID: 2360
		public HTMLMapEvents_onlayoutcompleteEventHandler m_onlayoutcompleteDelegate;

		// Token: 0x04000939 RID: 2361
		public HTMLMapEvents_onbeforeeditfocusEventHandler m_onbeforeeditfocusDelegate;

		// Token: 0x0400093A RID: 2362
		public HTMLMapEvents_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x0400093B RID: 2363
		public HTMLMapEvents_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x0400093C RID: 2364
		public HTMLMapEvents_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x0400093D RID: 2365
		public HTMLMapEvents_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x0400093E RID: 2366
		public HTMLMapEvents_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x0400093F RID: 2367
		public HTMLMapEvents_onpasteEventHandler m_onpasteDelegate;

		// Token: 0x04000940 RID: 2368
		public HTMLMapEvents_onbeforepasteEventHandler m_onbeforepasteDelegate;

		// Token: 0x04000941 RID: 2369
		public HTMLMapEvents_oncopyEventHandler m_oncopyDelegate;

		// Token: 0x04000942 RID: 2370
		public HTMLMapEvents_onbeforecopyEventHandler m_onbeforecopyDelegate;

		// Token: 0x04000943 RID: 2371
		public HTMLMapEvents_oncutEventHandler m_oncutDelegate;

		// Token: 0x04000944 RID: 2372
		public HTMLMapEvents_onbeforecutEventHandler m_onbeforecutDelegate;

		// Token: 0x04000945 RID: 2373
		public HTMLMapEvents_ondropEventHandler m_ondropDelegate;

		// Token: 0x04000946 RID: 2374
		public HTMLMapEvents_ondragleaveEventHandler m_ondragleaveDelegate;

		// Token: 0x04000947 RID: 2375
		public HTMLMapEvents_ondragoverEventHandler m_ondragoverDelegate;

		// Token: 0x04000948 RID: 2376
		public HTMLMapEvents_ondragenterEventHandler m_ondragenterDelegate;

		// Token: 0x04000949 RID: 2377
		public HTMLMapEvents_ondragendEventHandler m_ondragendDelegate;

		// Token: 0x0400094A RID: 2378
		public HTMLMapEvents_ondragEventHandler m_ondragDelegate;

		// Token: 0x0400094B RID: 2379
		public HTMLMapEvents_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x0400094C RID: 2380
		public HTMLMapEvents_onblurEventHandler m_onblurDelegate;

		// Token: 0x0400094D RID: 2381
		public HTMLMapEvents_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x0400094E RID: 2382
		public HTMLMapEvents_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x0400094F RID: 2383
		public HTMLMapEvents_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x04000950 RID: 2384
		public HTMLMapEvents_onlosecaptureEventHandler m_onlosecaptureDelegate;

		// Token: 0x04000951 RID: 2385
		public HTMLMapEvents_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x04000952 RID: 2386
		public HTMLMapEvents_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x04000953 RID: 2387
		public HTMLMapEvents_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x04000954 RID: 2388
		public HTMLMapEvents_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x04000955 RID: 2389
		public HTMLMapEvents_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x04000956 RID: 2390
		public HTMLMapEvents_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x04000957 RID: 2391
		public HTMLMapEvents_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x04000958 RID: 2392
		public HTMLMapEvents_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x04000959 RID: 2393
		public HTMLMapEvents_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x0400095A RID: 2394
		public HTMLMapEvents_onfilterchangeEventHandler m_onfilterchangeDelegate;

		// Token: 0x0400095B RID: 2395
		public HTMLMapEvents_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x0400095C RID: 2396
		public HTMLMapEvents_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x0400095D RID: 2397
		public HTMLMapEvents_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x0400095E RID: 2398
		public HTMLMapEvents_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x0400095F RID: 2399
		public HTMLMapEvents_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x04000960 RID: 2400
		public HTMLMapEvents_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x04000961 RID: 2401
		public HTMLMapEvents_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x04000962 RID: 2402
		public HTMLMapEvents_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x04000963 RID: 2403
		public HTMLMapEvents_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x04000964 RID: 2404
		public HTMLMapEvents_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x04000965 RID: 2405
		public HTMLMapEvents_onclickEventHandler m_onclickDelegate;

		// Token: 0x04000966 RID: 2406
		public HTMLMapEvents_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x04000967 RID: 2407
		public int m_dwCookie;
	}
}
