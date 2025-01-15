using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DD2 RID: 3538
	[ClassInterface(0)]
	internal sealed class HTMLAreaEvents_SinkHelper : HTMLAreaEvents
	{
		// Token: 0x06017B45 RID: 97093 RVA: 0x0003CDD0 File Offset: 0x0003BDD0
		public override void onfocusout()
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate();
				return;
			}
		}

		// Token: 0x06017B46 RID: 97094 RVA: 0x0003CDFC File Offset: 0x0003BDFC
		public override void onfocusin()
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate();
				return;
			}
		}

		// Token: 0x06017B47 RID: 97095 RVA: 0x0003CE28 File Offset: 0x0003BE28
		public override void ondeactivate()
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate();
				return;
			}
		}

		// Token: 0x06017B48 RID: 97096 RVA: 0x0003CE54 File Offset: 0x0003BE54
		public override void onactivate()
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate();
				return;
			}
		}

		// Token: 0x06017B49 RID: 97097 RVA: 0x0003CE80 File Offset: 0x0003BE80
		public override bool onmousewheel()
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate();
		}

		// Token: 0x06017B4A RID: 97098 RVA: 0x0003CEAC File Offset: 0x0003BEAC
		public override void onmouseleave()
		{
			if (this.m_onmouseleaveDelegate != null)
			{
				this.m_onmouseleaveDelegate();
				return;
			}
		}

		// Token: 0x06017B4B RID: 97099 RVA: 0x0003CED8 File Offset: 0x0003BED8
		public override void onmouseenter()
		{
			if (this.m_onmouseenterDelegate != null)
			{
				this.m_onmouseenterDelegate();
				return;
			}
		}

		// Token: 0x06017B4C RID: 97100 RVA: 0x0003CF04 File Offset: 0x0003BF04
		public override void onresizeend()
		{
			if (this.m_onresizeendDelegate != null)
			{
				this.m_onresizeendDelegate();
				return;
			}
		}

		// Token: 0x06017B4D RID: 97101 RVA: 0x0003CF30 File Offset: 0x0003BF30
		public override bool onresizestart()
		{
			return this.m_onresizestartDelegate != null && this.m_onresizestartDelegate();
		}

		// Token: 0x06017B4E RID: 97102 RVA: 0x0003CF5C File Offset: 0x0003BF5C
		public override void onmoveend()
		{
			if (this.m_onmoveendDelegate != null)
			{
				this.m_onmoveendDelegate();
				return;
			}
		}

		// Token: 0x06017B4F RID: 97103 RVA: 0x0003CF88 File Offset: 0x0003BF88
		public override bool onmovestart()
		{
			return this.m_onmovestartDelegate != null && this.m_onmovestartDelegate();
		}

		// Token: 0x06017B50 RID: 97104 RVA: 0x0003CFB4 File Offset: 0x0003BFB4
		public override bool oncontrolselect()
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate();
		}

		// Token: 0x06017B51 RID: 97105 RVA: 0x0003CFE0 File Offset: 0x0003BFE0
		public override void onmove()
		{
			if (this.m_onmoveDelegate != null)
			{
				this.m_onmoveDelegate();
				return;
			}
		}

		// Token: 0x06017B52 RID: 97106 RVA: 0x0003D00C File Offset: 0x0003C00C
		public override bool onbeforeactivate()
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate();
		}

		// Token: 0x06017B53 RID: 97107 RVA: 0x0003D038 File Offset: 0x0003C038
		public override bool onbeforedeactivate()
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate();
		}

		// Token: 0x06017B54 RID: 97108 RVA: 0x0003D064 File Offset: 0x0003C064
		public override void onpage()
		{
			if (this.m_onpageDelegate != null)
			{
				this.m_onpageDelegate();
				return;
			}
		}

		// Token: 0x06017B55 RID: 97109 RVA: 0x0003D090 File Offset: 0x0003C090
		public override void onlayoutcomplete()
		{
			if (this.m_onlayoutcompleteDelegate != null)
			{
				this.m_onlayoutcompleteDelegate();
				return;
			}
		}

		// Token: 0x06017B56 RID: 97110 RVA: 0x0003D0BC File Offset: 0x0003C0BC
		public override void onbeforeeditfocus()
		{
			if (this.m_onbeforeeditfocusDelegate != null)
			{
				this.m_onbeforeeditfocusDelegate();
				return;
			}
		}

		// Token: 0x06017B57 RID: 97111 RVA: 0x0003D0E8 File Offset: 0x0003C0E8
		public override void onreadystatechange()
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate();
				return;
			}
		}

		// Token: 0x06017B58 RID: 97112 RVA: 0x0003D114 File Offset: 0x0003C114
		public override void oncellchange()
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate();
				return;
			}
		}

		// Token: 0x06017B59 RID: 97113 RVA: 0x0003D140 File Offset: 0x0003C140
		public override void onrowsinserted()
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate();
				return;
			}
		}

		// Token: 0x06017B5A RID: 97114 RVA: 0x0003D16C File Offset: 0x0003C16C
		public override void onrowsdelete()
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate();
				return;
			}
		}

		// Token: 0x06017B5B RID: 97115 RVA: 0x0003D198 File Offset: 0x0003C198
		public override bool oncontextmenu()
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate();
		}

		// Token: 0x06017B5C RID: 97116 RVA: 0x0003D1C4 File Offset: 0x0003C1C4
		public override bool onpaste()
		{
			return this.m_onpasteDelegate != null && this.m_onpasteDelegate();
		}

		// Token: 0x06017B5D RID: 97117 RVA: 0x0003D1F0 File Offset: 0x0003C1F0
		public override bool onbeforepaste()
		{
			return this.m_onbeforepasteDelegate != null && this.m_onbeforepasteDelegate();
		}

		// Token: 0x06017B5E RID: 97118 RVA: 0x0003D21C File Offset: 0x0003C21C
		public override bool oncopy()
		{
			return this.m_oncopyDelegate != null && this.m_oncopyDelegate();
		}

		// Token: 0x06017B5F RID: 97119 RVA: 0x0003D248 File Offset: 0x0003C248
		public override bool onbeforecopy()
		{
			return this.m_onbeforecopyDelegate != null && this.m_onbeforecopyDelegate();
		}

		// Token: 0x06017B60 RID: 97120 RVA: 0x0003D274 File Offset: 0x0003C274
		public override bool oncut()
		{
			return this.m_oncutDelegate != null && this.m_oncutDelegate();
		}

		// Token: 0x06017B61 RID: 97121 RVA: 0x0003D2A0 File Offset: 0x0003C2A0
		public override bool onbeforecut()
		{
			return this.m_onbeforecutDelegate != null && this.m_onbeforecutDelegate();
		}

		// Token: 0x06017B62 RID: 97122 RVA: 0x0003D2CC File Offset: 0x0003C2CC
		public override bool ondrop()
		{
			return this.m_ondropDelegate != null && this.m_ondropDelegate();
		}

		// Token: 0x06017B63 RID: 97123 RVA: 0x0003D2F8 File Offset: 0x0003C2F8
		public override void ondragleave()
		{
			if (this.m_ondragleaveDelegate != null)
			{
				this.m_ondragleaveDelegate();
				return;
			}
		}

		// Token: 0x06017B64 RID: 97124 RVA: 0x0003D324 File Offset: 0x0003C324
		public override bool ondragover()
		{
			return this.m_ondragoverDelegate != null && this.m_ondragoverDelegate();
		}

		// Token: 0x06017B65 RID: 97125 RVA: 0x0003D350 File Offset: 0x0003C350
		public override bool ondragenter()
		{
			return this.m_ondragenterDelegate != null && this.m_ondragenterDelegate();
		}

		// Token: 0x06017B66 RID: 97126 RVA: 0x0003D37C File Offset: 0x0003C37C
		public override void ondragend()
		{
			if (this.m_ondragendDelegate != null)
			{
				this.m_ondragendDelegate();
				return;
			}
		}

		// Token: 0x06017B67 RID: 97127 RVA: 0x0003D3A8 File Offset: 0x0003C3A8
		public override bool ondrag()
		{
			return this.m_ondragDelegate != null && this.m_ondragDelegate();
		}

		// Token: 0x06017B68 RID: 97128 RVA: 0x0003D3D4 File Offset: 0x0003C3D4
		public override void onresize()
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate();
				return;
			}
		}

		// Token: 0x06017B69 RID: 97129 RVA: 0x0003D400 File Offset: 0x0003C400
		public override void onblur()
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate();
				return;
			}
		}

		// Token: 0x06017B6A RID: 97130 RVA: 0x0003D42C File Offset: 0x0003C42C
		public override void onfocus()
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate();
				return;
			}
		}

		// Token: 0x06017B6B RID: 97131 RVA: 0x0003D458 File Offset: 0x0003C458
		public override void onscroll()
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate();
				return;
			}
		}

		// Token: 0x06017B6C RID: 97132 RVA: 0x0003D484 File Offset: 0x0003C484
		public override void onpropertychange()
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate();
				return;
			}
		}

		// Token: 0x06017B6D RID: 97133 RVA: 0x0003D4B0 File Offset: 0x0003C4B0
		public override void onlosecapture()
		{
			if (this.m_onlosecaptureDelegate != null)
			{
				this.m_onlosecaptureDelegate();
				return;
			}
		}

		// Token: 0x06017B6E RID: 97134 RVA: 0x0003D4DC File Offset: 0x0003C4DC
		public override void ondatasetcomplete()
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate();
				return;
			}
		}

		// Token: 0x06017B6F RID: 97135 RVA: 0x0003D508 File Offset: 0x0003C508
		public override void ondataavailable()
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate();
				return;
			}
		}

		// Token: 0x06017B70 RID: 97136 RVA: 0x0003D534 File Offset: 0x0003C534
		public override void ondatasetchanged()
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate();
				return;
			}
		}

		// Token: 0x06017B71 RID: 97137 RVA: 0x0003D560 File Offset: 0x0003C560
		public override void onrowenter()
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate();
				return;
			}
		}

		// Token: 0x06017B72 RID: 97138 RVA: 0x0003D58C File Offset: 0x0003C58C
		public override bool onrowexit()
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate();
		}

		// Token: 0x06017B73 RID: 97139 RVA: 0x0003D5B8 File Offset: 0x0003C5B8
		public override bool onerrorupdate()
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate();
		}

		// Token: 0x06017B74 RID: 97140 RVA: 0x0003D5E4 File Offset: 0x0003C5E4
		public override void onafterupdate()
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate();
				return;
			}
		}

		// Token: 0x06017B75 RID: 97141 RVA: 0x0003D610 File Offset: 0x0003C610
		public override bool onbeforeupdate()
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate();
		}

		// Token: 0x06017B76 RID: 97142 RVA: 0x0003D63C File Offset: 0x0003C63C
		public override bool ondragstart()
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate();
		}

		// Token: 0x06017B77 RID: 97143 RVA: 0x0003D668 File Offset: 0x0003C668
		public override void onfilterchange()
		{
			if (this.m_onfilterchangeDelegate != null)
			{
				this.m_onfilterchangeDelegate();
				return;
			}
		}

		// Token: 0x06017B78 RID: 97144 RVA: 0x0003D694 File Offset: 0x0003C694
		public override bool onselectstart()
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate();
		}

		// Token: 0x06017B79 RID: 97145 RVA: 0x0003D6C0 File Offset: 0x0003C6C0
		public override void onmouseup()
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate();
				return;
			}
		}

		// Token: 0x06017B7A RID: 97146 RVA: 0x0003D6EC File Offset: 0x0003C6EC
		public override void onmousedown()
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate();
				return;
			}
		}

		// Token: 0x06017B7B RID: 97147 RVA: 0x0003D718 File Offset: 0x0003C718
		public override void onmousemove()
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate();
				return;
			}
		}

		// Token: 0x06017B7C RID: 97148 RVA: 0x0003D744 File Offset: 0x0003C744
		public override void onmouseover()
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate();
				return;
			}
		}

		// Token: 0x06017B7D RID: 97149 RVA: 0x0003D770 File Offset: 0x0003C770
		public override void onmouseout()
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate();
				return;
			}
		}

		// Token: 0x06017B7E RID: 97150 RVA: 0x0003D79C File Offset: 0x0003C79C
		public override void onkeyup()
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate();
				return;
			}
		}

		// Token: 0x06017B7F RID: 97151 RVA: 0x0003D7C8 File Offset: 0x0003C7C8
		public override void onkeydown()
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate();
				return;
			}
		}

		// Token: 0x06017B80 RID: 97152 RVA: 0x0003D7F4 File Offset: 0x0003C7F4
		public override bool onkeypress()
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate();
		}

		// Token: 0x06017B81 RID: 97153 RVA: 0x0003D820 File Offset: 0x0003C820
		public override bool ondblclick()
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate();
		}

		// Token: 0x06017B82 RID: 97154 RVA: 0x0003D84C File Offset: 0x0003C84C
		public override bool onclick()
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate();
		}

		// Token: 0x06017B83 RID: 97155 RVA: 0x0003D878 File Offset: 0x0003C878
		public override bool onhelp()
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate();
		}

		// Token: 0x06017B84 RID: 97156 RVA: 0x0003D8A4 File Offset: 0x0003C8A4
		internal HTMLAreaEvents_SinkHelper()
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

		// Token: 0x040006F9 RID: 1785
		public HTMLAreaEvents_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x040006FA RID: 1786
		public HTMLAreaEvents_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x040006FB RID: 1787
		public HTMLAreaEvents_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x040006FC RID: 1788
		public HTMLAreaEvents_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x040006FD RID: 1789
		public HTMLAreaEvents_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x040006FE RID: 1790
		public HTMLAreaEvents_onmouseleaveEventHandler m_onmouseleaveDelegate;

		// Token: 0x040006FF RID: 1791
		public HTMLAreaEvents_onmouseenterEventHandler m_onmouseenterDelegate;

		// Token: 0x04000700 RID: 1792
		public HTMLAreaEvents_onresizeendEventHandler m_onresizeendDelegate;

		// Token: 0x04000701 RID: 1793
		public HTMLAreaEvents_onresizestartEventHandler m_onresizestartDelegate;

		// Token: 0x04000702 RID: 1794
		public HTMLAreaEvents_onmoveendEventHandler m_onmoveendDelegate;

		// Token: 0x04000703 RID: 1795
		public HTMLAreaEvents_onmovestartEventHandler m_onmovestartDelegate;

		// Token: 0x04000704 RID: 1796
		public HTMLAreaEvents_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x04000705 RID: 1797
		public HTMLAreaEvents_onmoveEventHandler m_onmoveDelegate;

		// Token: 0x04000706 RID: 1798
		public HTMLAreaEvents_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x04000707 RID: 1799
		public HTMLAreaEvents_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x04000708 RID: 1800
		public HTMLAreaEvents_onpageEventHandler m_onpageDelegate;

		// Token: 0x04000709 RID: 1801
		public HTMLAreaEvents_onlayoutcompleteEventHandler m_onlayoutcompleteDelegate;

		// Token: 0x0400070A RID: 1802
		public HTMLAreaEvents_onbeforeeditfocusEventHandler m_onbeforeeditfocusDelegate;

		// Token: 0x0400070B RID: 1803
		public HTMLAreaEvents_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x0400070C RID: 1804
		public HTMLAreaEvents_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x0400070D RID: 1805
		public HTMLAreaEvents_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x0400070E RID: 1806
		public HTMLAreaEvents_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x0400070F RID: 1807
		public HTMLAreaEvents_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x04000710 RID: 1808
		public HTMLAreaEvents_onpasteEventHandler m_onpasteDelegate;

		// Token: 0x04000711 RID: 1809
		public HTMLAreaEvents_onbeforepasteEventHandler m_onbeforepasteDelegate;

		// Token: 0x04000712 RID: 1810
		public HTMLAreaEvents_oncopyEventHandler m_oncopyDelegate;

		// Token: 0x04000713 RID: 1811
		public HTMLAreaEvents_onbeforecopyEventHandler m_onbeforecopyDelegate;

		// Token: 0x04000714 RID: 1812
		public HTMLAreaEvents_oncutEventHandler m_oncutDelegate;

		// Token: 0x04000715 RID: 1813
		public HTMLAreaEvents_onbeforecutEventHandler m_onbeforecutDelegate;

		// Token: 0x04000716 RID: 1814
		public HTMLAreaEvents_ondropEventHandler m_ondropDelegate;

		// Token: 0x04000717 RID: 1815
		public HTMLAreaEvents_ondragleaveEventHandler m_ondragleaveDelegate;

		// Token: 0x04000718 RID: 1816
		public HTMLAreaEvents_ondragoverEventHandler m_ondragoverDelegate;

		// Token: 0x04000719 RID: 1817
		public HTMLAreaEvents_ondragenterEventHandler m_ondragenterDelegate;

		// Token: 0x0400071A RID: 1818
		public HTMLAreaEvents_ondragendEventHandler m_ondragendDelegate;

		// Token: 0x0400071B RID: 1819
		public HTMLAreaEvents_ondragEventHandler m_ondragDelegate;

		// Token: 0x0400071C RID: 1820
		public HTMLAreaEvents_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x0400071D RID: 1821
		public HTMLAreaEvents_onblurEventHandler m_onblurDelegate;

		// Token: 0x0400071E RID: 1822
		public HTMLAreaEvents_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x0400071F RID: 1823
		public HTMLAreaEvents_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x04000720 RID: 1824
		public HTMLAreaEvents_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x04000721 RID: 1825
		public HTMLAreaEvents_onlosecaptureEventHandler m_onlosecaptureDelegate;

		// Token: 0x04000722 RID: 1826
		public HTMLAreaEvents_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x04000723 RID: 1827
		public HTMLAreaEvents_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x04000724 RID: 1828
		public HTMLAreaEvents_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x04000725 RID: 1829
		public HTMLAreaEvents_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x04000726 RID: 1830
		public HTMLAreaEvents_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x04000727 RID: 1831
		public HTMLAreaEvents_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x04000728 RID: 1832
		public HTMLAreaEvents_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x04000729 RID: 1833
		public HTMLAreaEvents_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x0400072A RID: 1834
		public HTMLAreaEvents_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x0400072B RID: 1835
		public HTMLAreaEvents_onfilterchangeEventHandler m_onfilterchangeDelegate;

		// Token: 0x0400072C RID: 1836
		public HTMLAreaEvents_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x0400072D RID: 1837
		public HTMLAreaEvents_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x0400072E RID: 1838
		public HTMLAreaEvents_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x0400072F RID: 1839
		public HTMLAreaEvents_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x04000730 RID: 1840
		public HTMLAreaEvents_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x04000731 RID: 1841
		public HTMLAreaEvents_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x04000732 RID: 1842
		public HTMLAreaEvents_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x04000733 RID: 1843
		public HTMLAreaEvents_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x04000734 RID: 1844
		public HTMLAreaEvents_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x04000735 RID: 1845
		public HTMLAreaEvents_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x04000736 RID: 1846
		public HTMLAreaEvents_onclickEventHandler m_onclickDelegate;

		// Token: 0x04000737 RID: 1847
		public HTMLAreaEvents_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x04000738 RID: 1848
		public int m_dwCookie;
	}
}
