using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DE2 RID: 3554
	[ClassInterface(0)]
	internal sealed class HTMLButtonElementEvents2_SinkHelper : HTMLButtonElementEvents2
	{
		// Token: 0x060180D4 RID: 98516 RVA: 0x0006F760 File Offset: 0x0006E760
		public override bool onmousewheel(IHTMLEventObj A_1)
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate(A_1);
		}

		// Token: 0x060180D5 RID: 98517 RVA: 0x0006F790 File Offset: 0x0006E790
		public override void onresizeend(IHTMLEventObj A_1)
		{
			if (this.m_onresizeendDelegate != null)
			{
				this.m_onresizeendDelegate(A_1);
				return;
			}
		}

		// Token: 0x060180D6 RID: 98518 RVA: 0x0006F7C0 File Offset: 0x0006E7C0
		public override bool onresizestart(IHTMLEventObj A_1)
		{
			return this.m_onresizestartDelegate != null && this.m_onresizestartDelegate(A_1);
		}

		// Token: 0x060180D7 RID: 98519 RVA: 0x0006F7F0 File Offset: 0x0006E7F0
		public override void onmoveend(IHTMLEventObj A_1)
		{
			if (this.m_onmoveendDelegate != null)
			{
				this.m_onmoveendDelegate(A_1);
				return;
			}
		}

		// Token: 0x060180D8 RID: 98520 RVA: 0x0006F820 File Offset: 0x0006E820
		public override bool onmovestart(IHTMLEventObj A_1)
		{
			return this.m_onmovestartDelegate != null && this.m_onmovestartDelegate(A_1);
		}

		// Token: 0x060180D9 RID: 98521 RVA: 0x0006F850 File Offset: 0x0006E850
		public override bool oncontrolselect(IHTMLEventObj A_1)
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate(A_1);
		}

		// Token: 0x060180DA RID: 98522 RVA: 0x0006F880 File Offset: 0x0006E880
		public override void onmove(IHTMLEventObj A_1)
		{
			if (this.m_onmoveDelegate != null)
			{
				this.m_onmoveDelegate(A_1);
				return;
			}
		}

		// Token: 0x060180DB RID: 98523 RVA: 0x0006F8B0 File Offset: 0x0006E8B0
		public override void onfocusout(IHTMLEventObj A_1)
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate(A_1);
				return;
			}
		}

		// Token: 0x060180DC RID: 98524 RVA: 0x0006F8E0 File Offset: 0x0006E8E0
		public override void onfocusin(IHTMLEventObj A_1)
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate(A_1);
				return;
			}
		}

		// Token: 0x060180DD RID: 98525 RVA: 0x0006F910 File Offset: 0x0006E910
		public override bool onbeforeactivate(IHTMLEventObj A_1)
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate(A_1);
		}

		// Token: 0x060180DE RID: 98526 RVA: 0x0006F940 File Offset: 0x0006E940
		public override bool onbeforedeactivate(IHTMLEventObj A_1)
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate(A_1);
		}

		// Token: 0x060180DF RID: 98527 RVA: 0x0006F970 File Offset: 0x0006E970
		public override void ondeactivate(IHTMLEventObj A_1)
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate(A_1);
				return;
			}
		}

		// Token: 0x060180E0 RID: 98528 RVA: 0x0006F9A0 File Offset: 0x0006E9A0
		public override void onactivate(IHTMLEventObj A_1)
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate(A_1);
				return;
			}
		}

		// Token: 0x060180E1 RID: 98529 RVA: 0x0006F9D0 File Offset: 0x0006E9D0
		public override void onmouseleave(IHTMLEventObj A_1)
		{
			if (this.m_onmouseleaveDelegate != null)
			{
				this.m_onmouseleaveDelegate(A_1);
				return;
			}
		}

		// Token: 0x060180E2 RID: 98530 RVA: 0x0006FA00 File Offset: 0x0006EA00
		public override void onmouseenter(IHTMLEventObj A_1)
		{
			if (this.m_onmouseenterDelegate != null)
			{
				this.m_onmouseenterDelegate(A_1);
				return;
			}
		}

		// Token: 0x060180E3 RID: 98531 RVA: 0x0006FA30 File Offset: 0x0006EA30
		public override void onpage(IHTMLEventObj A_1)
		{
			if (this.m_onpageDelegate != null)
			{
				this.m_onpageDelegate(A_1);
				return;
			}
		}

		// Token: 0x060180E4 RID: 98532 RVA: 0x0006FA60 File Offset: 0x0006EA60
		public override void onlayoutcomplete(IHTMLEventObj A_1)
		{
			if (this.m_onlayoutcompleteDelegate != null)
			{
				this.m_onlayoutcompleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x060180E5 RID: 98533 RVA: 0x0006FA90 File Offset: 0x0006EA90
		public override void onreadystatechange(IHTMLEventObj A_1)
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x060180E6 RID: 98534 RVA: 0x0006FAC0 File Offset: 0x0006EAC0
		public override void oncellchange(IHTMLEventObj A_1)
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x060180E7 RID: 98535 RVA: 0x0006FAF0 File Offset: 0x0006EAF0
		public override void onrowsinserted(IHTMLEventObj A_1)
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate(A_1);
				return;
			}
		}

		// Token: 0x060180E8 RID: 98536 RVA: 0x0006FB20 File Offset: 0x0006EB20
		public override void onrowsdelete(IHTMLEventObj A_1)
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x060180E9 RID: 98537 RVA: 0x0006FB50 File Offset: 0x0006EB50
		public override bool oncontextmenu(IHTMLEventObj A_1)
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate(A_1);
		}

		// Token: 0x060180EA RID: 98538 RVA: 0x0006FB80 File Offset: 0x0006EB80
		public override bool onpaste(IHTMLEventObj A_1)
		{
			return this.m_onpasteDelegate != null && this.m_onpasteDelegate(A_1);
		}

		// Token: 0x060180EB RID: 98539 RVA: 0x0006FBB0 File Offset: 0x0006EBB0
		public override bool onbeforepaste(IHTMLEventObj A_1)
		{
			return this.m_onbeforepasteDelegate != null && this.m_onbeforepasteDelegate(A_1);
		}

		// Token: 0x060180EC RID: 98540 RVA: 0x0006FBE0 File Offset: 0x0006EBE0
		public override bool oncopy(IHTMLEventObj A_1)
		{
			return this.m_oncopyDelegate != null && this.m_oncopyDelegate(A_1);
		}

		// Token: 0x060180ED RID: 98541 RVA: 0x0006FC10 File Offset: 0x0006EC10
		public override bool onbeforecopy(IHTMLEventObj A_1)
		{
			return this.m_onbeforecopyDelegate != null && this.m_onbeforecopyDelegate(A_1);
		}

		// Token: 0x060180EE RID: 98542 RVA: 0x0006FC40 File Offset: 0x0006EC40
		public override bool oncut(IHTMLEventObj A_1)
		{
			return this.m_oncutDelegate != null && this.m_oncutDelegate(A_1);
		}

		// Token: 0x060180EF RID: 98543 RVA: 0x0006FC70 File Offset: 0x0006EC70
		public override bool onbeforecut(IHTMLEventObj A_1)
		{
			return this.m_onbeforecutDelegate != null && this.m_onbeforecutDelegate(A_1);
		}

		// Token: 0x060180F0 RID: 98544 RVA: 0x0006FCA0 File Offset: 0x0006ECA0
		public override bool ondrop(IHTMLEventObj A_1)
		{
			return this.m_ondropDelegate != null && this.m_ondropDelegate(A_1);
		}

		// Token: 0x060180F1 RID: 98545 RVA: 0x0006FCD0 File Offset: 0x0006ECD0
		public override void ondragleave(IHTMLEventObj A_1)
		{
			if (this.m_ondragleaveDelegate != null)
			{
				this.m_ondragleaveDelegate(A_1);
				return;
			}
		}

		// Token: 0x060180F2 RID: 98546 RVA: 0x0006FD00 File Offset: 0x0006ED00
		public override bool ondragover(IHTMLEventObj A_1)
		{
			return this.m_ondragoverDelegate != null && this.m_ondragoverDelegate(A_1);
		}

		// Token: 0x060180F3 RID: 98547 RVA: 0x0006FD30 File Offset: 0x0006ED30
		public override bool ondragenter(IHTMLEventObj A_1)
		{
			return this.m_ondragenterDelegate != null && this.m_ondragenterDelegate(A_1);
		}

		// Token: 0x060180F4 RID: 98548 RVA: 0x0006FD60 File Offset: 0x0006ED60
		public override void ondragend(IHTMLEventObj A_1)
		{
			if (this.m_ondragendDelegate != null)
			{
				this.m_ondragendDelegate(A_1);
				return;
			}
		}

		// Token: 0x060180F5 RID: 98549 RVA: 0x0006FD90 File Offset: 0x0006ED90
		public override bool ondrag(IHTMLEventObj A_1)
		{
			return this.m_ondragDelegate != null && this.m_ondragDelegate(A_1);
		}

		// Token: 0x060180F6 RID: 98550 RVA: 0x0006FDC0 File Offset: 0x0006EDC0
		public override void onresize(IHTMLEventObj A_1)
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate(A_1);
				return;
			}
		}

		// Token: 0x060180F7 RID: 98551 RVA: 0x0006FDF0 File Offset: 0x0006EDF0
		public override void onblur(IHTMLEventObj A_1)
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate(A_1);
				return;
			}
		}

		// Token: 0x060180F8 RID: 98552 RVA: 0x0006FE20 File Offset: 0x0006EE20
		public override void onfocus(IHTMLEventObj A_1)
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate(A_1);
				return;
			}
		}

		// Token: 0x060180F9 RID: 98553 RVA: 0x0006FE50 File Offset: 0x0006EE50
		public override void onscroll(IHTMLEventObj A_1)
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate(A_1);
				return;
			}
		}

		// Token: 0x060180FA RID: 98554 RVA: 0x0006FE80 File Offset: 0x0006EE80
		public override void onpropertychange(IHTMLEventObj A_1)
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x060180FB RID: 98555 RVA: 0x0006FEB0 File Offset: 0x0006EEB0
		public override void onlosecapture(IHTMLEventObj A_1)
		{
			if (this.m_onlosecaptureDelegate != null)
			{
				this.m_onlosecaptureDelegate(A_1);
				return;
			}
		}

		// Token: 0x060180FC RID: 98556 RVA: 0x0006FEE0 File Offset: 0x0006EEE0
		public override void ondatasetcomplete(IHTMLEventObj A_1)
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x060180FD RID: 98557 RVA: 0x0006FF10 File Offset: 0x0006EF10
		public override void ondataavailable(IHTMLEventObj A_1)
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate(A_1);
				return;
			}
		}

		// Token: 0x060180FE RID: 98558 RVA: 0x0006FF40 File Offset: 0x0006EF40
		public override void ondatasetchanged(IHTMLEventObj A_1)
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate(A_1);
				return;
			}
		}

		// Token: 0x060180FF RID: 98559 RVA: 0x0006FF70 File Offset: 0x0006EF70
		public override void onrowenter(IHTMLEventObj A_1)
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018100 RID: 98560 RVA: 0x0006FFA0 File Offset: 0x0006EFA0
		public override bool onrowexit(IHTMLEventObj A_1)
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate(A_1);
		}

		// Token: 0x06018101 RID: 98561 RVA: 0x0006FFD0 File Offset: 0x0006EFD0
		public override bool onerrorupdate(IHTMLEventObj A_1)
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate(A_1);
		}

		// Token: 0x06018102 RID: 98562 RVA: 0x00070000 File Offset: 0x0006F000
		public override void onafterupdate(IHTMLEventObj A_1)
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018103 RID: 98563 RVA: 0x00070030 File Offset: 0x0006F030
		public override bool onbeforeupdate(IHTMLEventObj A_1)
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate(A_1);
		}

		// Token: 0x06018104 RID: 98564 RVA: 0x00070060 File Offset: 0x0006F060
		public override bool ondragstart(IHTMLEventObj A_1)
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate(A_1);
		}

		// Token: 0x06018105 RID: 98565 RVA: 0x00070090 File Offset: 0x0006F090
		public override void onfilterchange(IHTMLEventObj A_1)
		{
			if (this.m_onfilterchangeDelegate != null)
			{
				this.m_onfilterchangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018106 RID: 98566 RVA: 0x000700C0 File Offset: 0x0006F0C0
		public override bool onselectstart(IHTMLEventObj A_1)
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate(A_1);
		}

		// Token: 0x06018107 RID: 98567 RVA: 0x000700F0 File Offset: 0x0006F0F0
		public override void onmouseup(IHTMLEventObj A_1)
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018108 RID: 98568 RVA: 0x00070120 File Offset: 0x0006F120
		public override void onmousedown(IHTMLEventObj A_1)
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018109 RID: 98569 RVA: 0x00070150 File Offset: 0x0006F150
		public override void onmousemove(IHTMLEventObj A_1)
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601810A RID: 98570 RVA: 0x00070180 File Offset: 0x0006F180
		public override void onmouseover(IHTMLEventObj A_1)
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601810B RID: 98571 RVA: 0x000701B0 File Offset: 0x0006F1B0
		public override void onmouseout(IHTMLEventObj A_1)
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601810C RID: 98572 RVA: 0x000701E0 File Offset: 0x0006F1E0
		public override void onkeyup(IHTMLEventObj A_1)
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601810D RID: 98573 RVA: 0x00070210 File Offset: 0x0006F210
		public override void onkeydown(IHTMLEventObj A_1)
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601810E RID: 98574 RVA: 0x00070240 File Offset: 0x0006F240
		public override bool onkeypress(IHTMLEventObj A_1)
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate(A_1);
		}

		// Token: 0x0601810F RID: 98575 RVA: 0x00070270 File Offset: 0x0006F270
		public override bool ondblclick(IHTMLEventObj A_1)
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate(A_1);
		}

		// Token: 0x06018110 RID: 98576 RVA: 0x000702A0 File Offset: 0x0006F2A0
		public override bool onclick(IHTMLEventObj A_1)
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate(A_1);
		}

		// Token: 0x06018111 RID: 98577 RVA: 0x000702D0 File Offset: 0x0006F2D0
		public override bool onhelp(IHTMLEventObj A_1)
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate(A_1);
		}

		// Token: 0x06018112 RID: 98578 RVA: 0x00070300 File Offset: 0x0006F300
		internal HTMLButtonElementEvents2_SinkHelper()
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

		// Token: 0x040008E6 RID: 2278
		public HTMLButtonElementEvents2_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x040008E7 RID: 2279
		public HTMLButtonElementEvents2_onresizeendEventHandler m_onresizeendDelegate;

		// Token: 0x040008E8 RID: 2280
		public HTMLButtonElementEvents2_onresizestartEventHandler m_onresizestartDelegate;

		// Token: 0x040008E9 RID: 2281
		public HTMLButtonElementEvents2_onmoveendEventHandler m_onmoveendDelegate;

		// Token: 0x040008EA RID: 2282
		public HTMLButtonElementEvents2_onmovestartEventHandler m_onmovestartDelegate;

		// Token: 0x040008EB RID: 2283
		public HTMLButtonElementEvents2_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x040008EC RID: 2284
		public HTMLButtonElementEvents2_onmoveEventHandler m_onmoveDelegate;

		// Token: 0x040008ED RID: 2285
		public HTMLButtonElementEvents2_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x040008EE RID: 2286
		public HTMLButtonElementEvents2_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x040008EF RID: 2287
		public HTMLButtonElementEvents2_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x040008F0 RID: 2288
		public HTMLButtonElementEvents2_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x040008F1 RID: 2289
		public HTMLButtonElementEvents2_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x040008F2 RID: 2290
		public HTMLButtonElementEvents2_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x040008F3 RID: 2291
		public HTMLButtonElementEvents2_onmouseleaveEventHandler m_onmouseleaveDelegate;

		// Token: 0x040008F4 RID: 2292
		public HTMLButtonElementEvents2_onmouseenterEventHandler m_onmouseenterDelegate;

		// Token: 0x040008F5 RID: 2293
		public HTMLButtonElementEvents2_onpageEventHandler m_onpageDelegate;

		// Token: 0x040008F6 RID: 2294
		public HTMLButtonElementEvents2_onlayoutcompleteEventHandler m_onlayoutcompleteDelegate;

		// Token: 0x040008F7 RID: 2295
		public HTMLButtonElementEvents2_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x040008F8 RID: 2296
		public HTMLButtonElementEvents2_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x040008F9 RID: 2297
		public HTMLButtonElementEvents2_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x040008FA RID: 2298
		public HTMLButtonElementEvents2_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x040008FB RID: 2299
		public HTMLButtonElementEvents2_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x040008FC RID: 2300
		public HTMLButtonElementEvents2_onpasteEventHandler m_onpasteDelegate;

		// Token: 0x040008FD RID: 2301
		public HTMLButtonElementEvents2_onbeforepasteEventHandler m_onbeforepasteDelegate;

		// Token: 0x040008FE RID: 2302
		public HTMLButtonElementEvents2_oncopyEventHandler m_oncopyDelegate;

		// Token: 0x040008FF RID: 2303
		public HTMLButtonElementEvents2_onbeforecopyEventHandler m_onbeforecopyDelegate;

		// Token: 0x04000900 RID: 2304
		public HTMLButtonElementEvents2_oncutEventHandler m_oncutDelegate;

		// Token: 0x04000901 RID: 2305
		public HTMLButtonElementEvents2_onbeforecutEventHandler m_onbeforecutDelegate;

		// Token: 0x04000902 RID: 2306
		public HTMLButtonElementEvents2_ondropEventHandler m_ondropDelegate;

		// Token: 0x04000903 RID: 2307
		public HTMLButtonElementEvents2_ondragleaveEventHandler m_ondragleaveDelegate;

		// Token: 0x04000904 RID: 2308
		public HTMLButtonElementEvents2_ondragoverEventHandler m_ondragoverDelegate;

		// Token: 0x04000905 RID: 2309
		public HTMLButtonElementEvents2_ondragenterEventHandler m_ondragenterDelegate;

		// Token: 0x04000906 RID: 2310
		public HTMLButtonElementEvents2_ondragendEventHandler m_ondragendDelegate;

		// Token: 0x04000907 RID: 2311
		public HTMLButtonElementEvents2_ondragEventHandler m_ondragDelegate;

		// Token: 0x04000908 RID: 2312
		public HTMLButtonElementEvents2_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x04000909 RID: 2313
		public HTMLButtonElementEvents2_onblurEventHandler m_onblurDelegate;

		// Token: 0x0400090A RID: 2314
		public HTMLButtonElementEvents2_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x0400090B RID: 2315
		public HTMLButtonElementEvents2_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x0400090C RID: 2316
		public HTMLButtonElementEvents2_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x0400090D RID: 2317
		public HTMLButtonElementEvents2_onlosecaptureEventHandler m_onlosecaptureDelegate;

		// Token: 0x0400090E RID: 2318
		public HTMLButtonElementEvents2_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x0400090F RID: 2319
		public HTMLButtonElementEvents2_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x04000910 RID: 2320
		public HTMLButtonElementEvents2_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x04000911 RID: 2321
		public HTMLButtonElementEvents2_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x04000912 RID: 2322
		public HTMLButtonElementEvents2_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x04000913 RID: 2323
		public HTMLButtonElementEvents2_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x04000914 RID: 2324
		public HTMLButtonElementEvents2_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x04000915 RID: 2325
		public HTMLButtonElementEvents2_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x04000916 RID: 2326
		public HTMLButtonElementEvents2_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x04000917 RID: 2327
		public HTMLButtonElementEvents2_onfilterchangeEventHandler m_onfilterchangeDelegate;

		// Token: 0x04000918 RID: 2328
		public HTMLButtonElementEvents2_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x04000919 RID: 2329
		public HTMLButtonElementEvents2_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x0400091A RID: 2330
		public HTMLButtonElementEvents2_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x0400091B RID: 2331
		public HTMLButtonElementEvents2_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x0400091C RID: 2332
		public HTMLButtonElementEvents2_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x0400091D RID: 2333
		public HTMLButtonElementEvents2_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x0400091E RID: 2334
		public HTMLButtonElementEvents2_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x0400091F RID: 2335
		public HTMLButtonElementEvents2_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x04000920 RID: 2336
		public HTMLButtonElementEvents2_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x04000921 RID: 2337
		public HTMLButtonElementEvents2_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x04000922 RID: 2338
		public HTMLButtonElementEvents2_onclickEventHandler m_onclickDelegate;

		// Token: 0x04000923 RID: 2339
		public HTMLButtonElementEvents2_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x04000924 RID: 2340
		public int m_dwCookie;
	}
}
