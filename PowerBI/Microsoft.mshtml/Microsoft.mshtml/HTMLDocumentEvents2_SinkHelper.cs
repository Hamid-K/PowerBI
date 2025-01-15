using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000E0E RID: 3598
	[ClassInterface(0)]
	internal sealed class HTMLDocumentEvents2_SinkHelper : HTMLDocumentEvents2
	{
		// Token: 0x06019069 RID: 102505 RVA: 0x000FD640 File Offset: 0x000FC640
		public override bool onbeforedeactivate(IHTMLEventObj A_1)
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate(A_1);
		}

		// Token: 0x0601906A RID: 102506 RVA: 0x000FD670 File Offset: 0x000FC670
		public override bool onbeforeactivate(IHTMLEventObj A_1)
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate(A_1);
		}

		// Token: 0x0601906B RID: 102507 RVA: 0x000FD6A0 File Offset: 0x000FC6A0
		public override void ondeactivate(IHTMLEventObj A_1)
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601906C RID: 102508 RVA: 0x000FD6D0 File Offset: 0x000FC6D0
		public override void onactivate(IHTMLEventObj A_1)
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601906D RID: 102509 RVA: 0x000FD700 File Offset: 0x000FC700
		public override void onfocusout(IHTMLEventObj A_1)
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601906E RID: 102510 RVA: 0x000FD730 File Offset: 0x000FC730
		public override void onfocusin(IHTMLEventObj A_1)
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601906F RID: 102511 RVA: 0x000FD760 File Offset: 0x000FC760
		public override bool onmousewheel(IHTMLEventObj A_1)
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate(A_1);
		}

		// Token: 0x06019070 RID: 102512 RVA: 0x000FD790 File Offset: 0x000FC790
		public override bool oncontrolselect(IHTMLEventObj A_1)
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate(A_1);
		}

		// Token: 0x06019071 RID: 102513 RVA: 0x000FD7C0 File Offset: 0x000FC7C0
		public override void onselectionchange(IHTMLEventObj A_1)
		{
			if (this.m_onselectionchangeDelegate != null)
			{
				this.m_onselectionchangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06019072 RID: 102514 RVA: 0x000FD7F0 File Offset: 0x000FC7F0
		public override void onbeforeeditfocus(IHTMLEventObj A_1)
		{
			if (this.m_onbeforeeditfocusDelegate != null)
			{
				this.m_onbeforeeditfocusDelegate(A_1);
				return;
			}
		}

		// Token: 0x06019073 RID: 102515 RVA: 0x000FD820 File Offset: 0x000FC820
		public override void ondatasetcomplete(IHTMLEventObj A_1)
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x06019074 RID: 102516 RVA: 0x000FD850 File Offset: 0x000FC850
		public override void ondataavailable(IHTMLEventObj A_1)
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate(A_1);
				return;
			}
		}

		// Token: 0x06019075 RID: 102517 RVA: 0x000FD880 File Offset: 0x000FC880
		public override void ondatasetchanged(IHTMLEventObj A_1)
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate(A_1);
				return;
			}
		}

		// Token: 0x06019076 RID: 102518 RVA: 0x000FD8B0 File Offset: 0x000FC8B0
		public override void onpropertychange(IHTMLEventObj A_1)
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06019077 RID: 102519 RVA: 0x000FD8E0 File Offset: 0x000FC8E0
		public override void oncellchange(IHTMLEventObj A_1)
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06019078 RID: 102520 RVA: 0x000FD910 File Offset: 0x000FC910
		public override void onrowsinserted(IHTMLEventObj A_1)
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate(A_1);
				return;
			}
		}

		// Token: 0x06019079 RID: 102521 RVA: 0x000FD940 File Offset: 0x000FC940
		public override void onrowsdelete(IHTMLEventObj A_1)
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601907A RID: 102522 RVA: 0x000FD970 File Offset: 0x000FC970
		public override bool onstop(IHTMLEventObj A_1)
		{
			return this.m_onstopDelegate != null && this.m_onstopDelegate(A_1);
		}

		// Token: 0x0601907B RID: 102523 RVA: 0x000FD9A0 File Offset: 0x000FC9A0
		public override bool oncontextmenu(IHTMLEventObj A_1)
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate(A_1);
		}

		// Token: 0x0601907C RID: 102524 RVA: 0x000FD9D0 File Offset: 0x000FC9D0
		public override bool onerrorupdate(IHTMLEventObj A_1)
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate(A_1);
		}

		// Token: 0x0601907D RID: 102525 RVA: 0x000FDA00 File Offset: 0x000FCA00
		public override bool onselectstart(IHTMLEventObj A_1)
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate(A_1);
		}

		// Token: 0x0601907E RID: 102526 RVA: 0x000FDA30 File Offset: 0x000FCA30
		public override bool ondragstart(IHTMLEventObj A_1)
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate(A_1);
		}

		// Token: 0x0601907F RID: 102527 RVA: 0x000FDA60 File Offset: 0x000FCA60
		public override void onrowenter(IHTMLEventObj A_1)
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate(A_1);
				return;
			}
		}

		// Token: 0x06019080 RID: 102528 RVA: 0x000FDA90 File Offset: 0x000FCA90
		public override bool onrowexit(IHTMLEventObj A_1)
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate(A_1);
		}

		// Token: 0x06019081 RID: 102529 RVA: 0x000FDAC0 File Offset: 0x000FCAC0
		public override void onafterupdate(IHTMLEventObj A_1)
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate(A_1);
				return;
			}
		}

		// Token: 0x06019082 RID: 102530 RVA: 0x000FDAF0 File Offset: 0x000FCAF0
		public override bool onbeforeupdate(IHTMLEventObj A_1)
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate(A_1);
		}

		// Token: 0x06019083 RID: 102531 RVA: 0x000FDB20 File Offset: 0x000FCB20
		public override void onreadystatechange(IHTMLEventObj A_1)
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06019084 RID: 102532 RVA: 0x000FDB50 File Offset: 0x000FCB50
		public override void onmouseover(IHTMLEventObj A_1)
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate(A_1);
				return;
			}
		}

		// Token: 0x06019085 RID: 102533 RVA: 0x000FDB80 File Offset: 0x000FCB80
		public override void onmouseout(IHTMLEventObj A_1)
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate(A_1);
				return;
			}
		}

		// Token: 0x06019086 RID: 102534 RVA: 0x000FDBB0 File Offset: 0x000FCBB0
		public override void onmouseup(IHTMLEventObj A_1)
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate(A_1);
				return;
			}
		}

		// Token: 0x06019087 RID: 102535 RVA: 0x000FDBE0 File Offset: 0x000FCBE0
		public override void onmousemove(IHTMLEventObj A_1)
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06019088 RID: 102536 RVA: 0x000FDC10 File Offset: 0x000FCC10
		public override void onmousedown(IHTMLEventObj A_1)
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate(A_1);
				return;
			}
		}

		// Token: 0x06019089 RID: 102537 RVA: 0x000FDC40 File Offset: 0x000FCC40
		public override bool onkeypress(IHTMLEventObj A_1)
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate(A_1);
		}

		// Token: 0x0601908A RID: 102538 RVA: 0x000FDC70 File Offset: 0x000FCC70
		public override void onkeyup(IHTMLEventObj A_1)
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601908B RID: 102539 RVA: 0x000FDCA0 File Offset: 0x000FCCA0
		public override void onkeydown(IHTMLEventObj A_1)
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601908C RID: 102540 RVA: 0x000FDCD0 File Offset: 0x000FCCD0
		public override bool ondblclick(IHTMLEventObj A_1)
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate(A_1);
		}

		// Token: 0x0601908D RID: 102541 RVA: 0x000FDD00 File Offset: 0x000FCD00
		public override bool onclick(IHTMLEventObj A_1)
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate(A_1);
		}

		// Token: 0x0601908E RID: 102542 RVA: 0x000FDD30 File Offset: 0x000FCD30
		public override bool onhelp(IHTMLEventObj A_1)
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate(A_1);
		}

		// Token: 0x0601908F RID: 102543 RVA: 0x000FDD60 File Offset: 0x000FCD60
		internal HTMLDocumentEvents2_SinkHelper()
		{
			this.m_dwCookie = 0;
			this.m_onbeforedeactivateDelegate = null;
			this.m_onbeforeactivateDelegate = null;
			this.m_ondeactivateDelegate = null;
			this.m_onactivateDelegate = null;
			this.m_onfocusoutDelegate = null;
			this.m_onfocusinDelegate = null;
			this.m_onmousewheelDelegate = null;
			this.m_oncontrolselectDelegate = null;
			this.m_onselectionchangeDelegate = null;
			this.m_onbeforeeditfocusDelegate = null;
			this.m_ondatasetcompleteDelegate = null;
			this.m_ondataavailableDelegate = null;
			this.m_ondatasetchangedDelegate = null;
			this.m_onpropertychangeDelegate = null;
			this.m_oncellchangeDelegate = null;
			this.m_onrowsinsertedDelegate = null;
			this.m_onrowsdeleteDelegate = null;
			this.m_onstopDelegate = null;
			this.m_oncontextmenuDelegate = null;
			this.m_onerrorupdateDelegate = null;
			this.m_onselectstartDelegate = null;
			this.m_ondragstartDelegate = null;
			this.m_onrowenterDelegate = null;
			this.m_onrowexitDelegate = null;
			this.m_onafterupdateDelegate = null;
			this.m_onbeforeupdateDelegate = null;
			this.m_onreadystatechangeDelegate = null;
			this.m_onmouseoverDelegate = null;
			this.m_onmouseoutDelegate = null;
			this.m_onmouseupDelegate = null;
			this.m_onmousemoveDelegate = null;
			this.m_onmousedownDelegate = null;
			this.m_onkeypressDelegate = null;
			this.m_onkeyupDelegate = null;
			this.m_onkeydownDelegate = null;
			this.m_ondblclickDelegate = null;
			this.m_onclickDelegate = null;
			this.m_onhelpDelegate = null;
		}

		// Token: 0x04000E4B RID: 3659
		public HTMLDocumentEvents2_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x04000E4C RID: 3660
		public HTMLDocumentEvents2_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x04000E4D RID: 3661
		public HTMLDocumentEvents2_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x04000E4E RID: 3662
		public HTMLDocumentEvents2_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x04000E4F RID: 3663
		public HTMLDocumentEvents2_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x04000E50 RID: 3664
		public HTMLDocumentEvents2_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x04000E51 RID: 3665
		public HTMLDocumentEvents2_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x04000E52 RID: 3666
		public HTMLDocumentEvents2_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x04000E53 RID: 3667
		public HTMLDocumentEvents2_onselectionchangeEventHandler m_onselectionchangeDelegate;

		// Token: 0x04000E54 RID: 3668
		public HTMLDocumentEvents2_onbeforeeditfocusEventHandler m_onbeforeeditfocusDelegate;

		// Token: 0x04000E55 RID: 3669
		public HTMLDocumentEvents2_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x04000E56 RID: 3670
		public HTMLDocumentEvents2_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x04000E57 RID: 3671
		public HTMLDocumentEvents2_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x04000E58 RID: 3672
		public HTMLDocumentEvents2_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x04000E59 RID: 3673
		public HTMLDocumentEvents2_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x04000E5A RID: 3674
		public HTMLDocumentEvents2_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x04000E5B RID: 3675
		public HTMLDocumentEvents2_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x04000E5C RID: 3676
		public HTMLDocumentEvents2_onstopEventHandler m_onstopDelegate;

		// Token: 0x04000E5D RID: 3677
		public HTMLDocumentEvents2_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x04000E5E RID: 3678
		public HTMLDocumentEvents2_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x04000E5F RID: 3679
		public HTMLDocumentEvents2_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x04000E60 RID: 3680
		public HTMLDocumentEvents2_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x04000E61 RID: 3681
		public HTMLDocumentEvents2_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x04000E62 RID: 3682
		public HTMLDocumentEvents2_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x04000E63 RID: 3683
		public HTMLDocumentEvents2_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x04000E64 RID: 3684
		public HTMLDocumentEvents2_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x04000E65 RID: 3685
		public HTMLDocumentEvents2_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x04000E66 RID: 3686
		public HTMLDocumentEvents2_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x04000E67 RID: 3687
		public HTMLDocumentEvents2_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x04000E68 RID: 3688
		public HTMLDocumentEvents2_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x04000E69 RID: 3689
		public HTMLDocumentEvents2_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x04000E6A RID: 3690
		public HTMLDocumentEvents2_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x04000E6B RID: 3691
		public HTMLDocumentEvents2_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x04000E6C RID: 3692
		public HTMLDocumentEvents2_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x04000E6D RID: 3693
		public HTMLDocumentEvents2_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x04000E6E RID: 3694
		public HTMLDocumentEvents2_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x04000E6F RID: 3695
		public HTMLDocumentEvents2_onclickEventHandler m_onclickDelegate;

		// Token: 0x04000E70 RID: 3696
		public HTMLDocumentEvents2_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x04000E71 RID: 3697
		public int m_dwCookie;
	}
}
