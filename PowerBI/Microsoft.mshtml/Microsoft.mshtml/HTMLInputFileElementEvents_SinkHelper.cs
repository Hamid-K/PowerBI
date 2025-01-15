using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000E06 RID: 3590
	[ClassInterface(0)]
	internal sealed class HTMLInputFileElementEvents_SinkHelper : HTMLInputFileElementEvents
	{
		// Token: 0x06018D40 RID: 101696 RVA: 0x000E0960 File Offset: 0x000DF960
		public override void onabort()
		{
			if (this.m_onabortDelegate != null)
			{
				this.m_onabortDelegate();
				return;
			}
		}

		// Token: 0x06018D41 RID: 101697 RVA: 0x000E098C File Offset: 0x000DF98C
		public override void onerror()
		{
			if (this.m_onerrorDelegate != null)
			{
				this.m_onerrorDelegate();
				return;
			}
		}

		// Token: 0x06018D42 RID: 101698 RVA: 0x000E09B8 File Offset: 0x000DF9B8
		public override void onload()
		{
			if (this.m_onloadDelegate != null)
			{
				this.m_onloadDelegate();
				return;
			}
		}

		// Token: 0x06018D43 RID: 101699 RVA: 0x000E09E4 File Offset: 0x000DF9E4
		public override void onselect()
		{
			if (this.m_onselectDelegate != null)
			{
				this.m_onselectDelegate();
				return;
			}
		}

		// Token: 0x06018D44 RID: 101700 RVA: 0x000E0A10 File Offset: 0x000DFA10
		public override bool onchange()
		{
			return this.m_onchangeDelegate != null && this.m_onchangeDelegate();
		}

		// Token: 0x06018D45 RID: 101701 RVA: 0x000E0A3C File Offset: 0x000DFA3C
		public override void onfocusout()
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate();
				return;
			}
		}

		// Token: 0x06018D46 RID: 101702 RVA: 0x000E0A68 File Offset: 0x000DFA68
		public override void onfocusin()
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate();
				return;
			}
		}

		// Token: 0x06018D47 RID: 101703 RVA: 0x000E0A94 File Offset: 0x000DFA94
		public override void ondeactivate()
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate();
				return;
			}
		}

		// Token: 0x06018D48 RID: 101704 RVA: 0x000E0AC0 File Offset: 0x000DFAC0
		public override void onactivate()
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate();
				return;
			}
		}

		// Token: 0x06018D49 RID: 101705 RVA: 0x000E0AEC File Offset: 0x000DFAEC
		public override bool onmousewheel()
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate();
		}

		// Token: 0x06018D4A RID: 101706 RVA: 0x000E0B18 File Offset: 0x000DFB18
		public override void onmouseleave()
		{
			if (this.m_onmouseleaveDelegate != null)
			{
				this.m_onmouseleaveDelegate();
				return;
			}
		}

		// Token: 0x06018D4B RID: 101707 RVA: 0x000E0B44 File Offset: 0x000DFB44
		public override void onmouseenter()
		{
			if (this.m_onmouseenterDelegate != null)
			{
				this.m_onmouseenterDelegate();
				return;
			}
		}

		// Token: 0x06018D4C RID: 101708 RVA: 0x000E0B70 File Offset: 0x000DFB70
		public override void onresizeend()
		{
			if (this.m_onresizeendDelegate != null)
			{
				this.m_onresizeendDelegate();
				return;
			}
		}

		// Token: 0x06018D4D RID: 101709 RVA: 0x000E0B9C File Offset: 0x000DFB9C
		public override bool onresizestart()
		{
			return this.m_onresizestartDelegate != null && this.m_onresizestartDelegate();
		}

		// Token: 0x06018D4E RID: 101710 RVA: 0x000E0BC8 File Offset: 0x000DFBC8
		public override void onmoveend()
		{
			if (this.m_onmoveendDelegate != null)
			{
				this.m_onmoveendDelegate();
				return;
			}
		}

		// Token: 0x06018D4F RID: 101711 RVA: 0x000E0BF4 File Offset: 0x000DFBF4
		public override bool onmovestart()
		{
			return this.m_onmovestartDelegate != null && this.m_onmovestartDelegate();
		}

		// Token: 0x06018D50 RID: 101712 RVA: 0x000E0C20 File Offset: 0x000DFC20
		public override bool oncontrolselect()
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate();
		}

		// Token: 0x06018D51 RID: 101713 RVA: 0x000E0C4C File Offset: 0x000DFC4C
		public override void onmove()
		{
			if (this.m_onmoveDelegate != null)
			{
				this.m_onmoveDelegate();
				return;
			}
		}

		// Token: 0x06018D52 RID: 101714 RVA: 0x000E0C78 File Offset: 0x000DFC78
		public override bool onbeforeactivate()
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate();
		}

		// Token: 0x06018D53 RID: 101715 RVA: 0x000E0CA4 File Offset: 0x000DFCA4
		public override bool onbeforedeactivate()
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate();
		}

		// Token: 0x06018D54 RID: 101716 RVA: 0x000E0CD0 File Offset: 0x000DFCD0
		public override void onpage()
		{
			if (this.m_onpageDelegate != null)
			{
				this.m_onpageDelegate();
				return;
			}
		}

		// Token: 0x06018D55 RID: 101717 RVA: 0x000E0CFC File Offset: 0x000DFCFC
		public override void onlayoutcomplete()
		{
			if (this.m_onlayoutcompleteDelegate != null)
			{
				this.m_onlayoutcompleteDelegate();
				return;
			}
		}

		// Token: 0x06018D56 RID: 101718 RVA: 0x000E0D28 File Offset: 0x000DFD28
		public override void onbeforeeditfocus()
		{
			if (this.m_onbeforeeditfocusDelegate != null)
			{
				this.m_onbeforeeditfocusDelegate();
				return;
			}
		}

		// Token: 0x06018D57 RID: 101719 RVA: 0x000E0D54 File Offset: 0x000DFD54
		public override void onreadystatechange()
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate();
				return;
			}
		}

		// Token: 0x06018D58 RID: 101720 RVA: 0x000E0D80 File Offset: 0x000DFD80
		public override void oncellchange()
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate();
				return;
			}
		}

		// Token: 0x06018D59 RID: 101721 RVA: 0x000E0DAC File Offset: 0x000DFDAC
		public override void onrowsinserted()
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate();
				return;
			}
		}

		// Token: 0x06018D5A RID: 101722 RVA: 0x000E0DD8 File Offset: 0x000DFDD8
		public override void onrowsdelete()
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate();
				return;
			}
		}

		// Token: 0x06018D5B RID: 101723 RVA: 0x000E0E04 File Offset: 0x000DFE04
		public override bool oncontextmenu()
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate();
		}

		// Token: 0x06018D5C RID: 101724 RVA: 0x000E0E30 File Offset: 0x000DFE30
		public override bool onpaste()
		{
			return this.m_onpasteDelegate != null && this.m_onpasteDelegate();
		}

		// Token: 0x06018D5D RID: 101725 RVA: 0x000E0E5C File Offset: 0x000DFE5C
		public override bool onbeforepaste()
		{
			return this.m_onbeforepasteDelegate != null && this.m_onbeforepasteDelegate();
		}

		// Token: 0x06018D5E RID: 101726 RVA: 0x000E0E88 File Offset: 0x000DFE88
		public override bool oncopy()
		{
			return this.m_oncopyDelegate != null && this.m_oncopyDelegate();
		}

		// Token: 0x06018D5F RID: 101727 RVA: 0x000E0EB4 File Offset: 0x000DFEB4
		public override bool onbeforecopy()
		{
			return this.m_onbeforecopyDelegate != null && this.m_onbeforecopyDelegate();
		}

		// Token: 0x06018D60 RID: 101728 RVA: 0x000E0EE0 File Offset: 0x000DFEE0
		public override bool oncut()
		{
			return this.m_oncutDelegate != null && this.m_oncutDelegate();
		}

		// Token: 0x06018D61 RID: 101729 RVA: 0x000E0F0C File Offset: 0x000DFF0C
		public override bool onbeforecut()
		{
			return this.m_onbeforecutDelegate != null && this.m_onbeforecutDelegate();
		}

		// Token: 0x06018D62 RID: 101730 RVA: 0x000E0F38 File Offset: 0x000DFF38
		public override bool ondrop()
		{
			return this.m_ondropDelegate != null && this.m_ondropDelegate();
		}

		// Token: 0x06018D63 RID: 101731 RVA: 0x000E0F64 File Offset: 0x000DFF64
		public override void ondragleave()
		{
			if (this.m_ondragleaveDelegate != null)
			{
				this.m_ondragleaveDelegate();
				return;
			}
		}

		// Token: 0x06018D64 RID: 101732 RVA: 0x000E0F90 File Offset: 0x000DFF90
		public override bool ondragover()
		{
			return this.m_ondragoverDelegate != null && this.m_ondragoverDelegate();
		}

		// Token: 0x06018D65 RID: 101733 RVA: 0x000E0FBC File Offset: 0x000DFFBC
		public override bool ondragenter()
		{
			return this.m_ondragenterDelegate != null && this.m_ondragenterDelegate();
		}

		// Token: 0x06018D66 RID: 101734 RVA: 0x000E0FE8 File Offset: 0x000DFFE8
		public override void ondragend()
		{
			if (this.m_ondragendDelegate != null)
			{
				this.m_ondragendDelegate();
				return;
			}
		}

		// Token: 0x06018D67 RID: 101735 RVA: 0x000E1014 File Offset: 0x000E0014
		public override bool ondrag()
		{
			return this.m_ondragDelegate != null && this.m_ondragDelegate();
		}

		// Token: 0x06018D68 RID: 101736 RVA: 0x000E1040 File Offset: 0x000E0040
		public override void onresize()
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate();
				return;
			}
		}

		// Token: 0x06018D69 RID: 101737 RVA: 0x000E106C File Offset: 0x000E006C
		public override void onblur()
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate();
				return;
			}
		}

		// Token: 0x06018D6A RID: 101738 RVA: 0x000E1098 File Offset: 0x000E0098
		public override void onfocus()
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate();
				return;
			}
		}

		// Token: 0x06018D6B RID: 101739 RVA: 0x000E10C4 File Offset: 0x000E00C4
		public override void onscroll()
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate();
				return;
			}
		}

		// Token: 0x06018D6C RID: 101740 RVA: 0x000E10F0 File Offset: 0x000E00F0
		public override void onpropertychange()
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate();
				return;
			}
		}

		// Token: 0x06018D6D RID: 101741 RVA: 0x000E111C File Offset: 0x000E011C
		public override void onlosecapture()
		{
			if (this.m_onlosecaptureDelegate != null)
			{
				this.m_onlosecaptureDelegate();
				return;
			}
		}

		// Token: 0x06018D6E RID: 101742 RVA: 0x000E1148 File Offset: 0x000E0148
		public override void ondatasetcomplete()
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate();
				return;
			}
		}

		// Token: 0x06018D6F RID: 101743 RVA: 0x000E1174 File Offset: 0x000E0174
		public override void ondataavailable()
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate();
				return;
			}
		}

		// Token: 0x06018D70 RID: 101744 RVA: 0x000E11A0 File Offset: 0x000E01A0
		public override void ondatasetchanged()
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate();
				return;
			}
		}

		// Token: 0x06018D71 RID: 101745 RVA: 0x000E11CC File Offset: 0x000E01CC
		public override void onrowenter()
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate();
				return;
			}
		}

		// Token: 0x06018D72 RID: 101746 RVA: 0x000E11F8 File Offset: 0x000E01F8
		public override bool onrowexit()
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate();
		}

		// Token: 0x06018D73 RID: 101747 RVA: 0x000E1224 File Offset: 0x000E0224
		public override bool onerrorupdate()
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate();
		}

		// Token: 0x06018D74 RID: 101748 RVA: 0x000E1250 File Offset: 0x000E0250
		public override void onafterupdate()
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate();
				return;
			}
		}

		// Token: 0x06018D75 RID: 101749 RVA: 0x000E127C File Offset: 0x000E027C
		public override bool onbeforeupdate()
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate();
		}

		// Token: 0x06018D76 RID: 101750 RVA: 0x000E12A8 File Offset: 0x000E02A8
		public override bool ondragstart()
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate();
		}

		// Token: 0x06018D77 RID: 101751 RVA: 0x000E12D4 File Offset: 0x000E02D4
		public override void onfilterchange()
		{
			if (this.m_onfilterchangeDelegate != null)
			{
				this.m_onfilterchangeDelegate();
				return;
			}
		}

		// Token: 0x06018D78 RID: 101752 RVA: 0x000E1300 File Offset: 0x000E0300
		public override bool onselectstart()
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate();
		}

		// Token: 0x06018D79 RID: 101753 RVA: 0x000E132C File Offset: 0x000E032C
		public override void onmouseup()
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate();
				return;
			}
		}

		// Token: 0x06018D7A RID: 101754 RVA: 0x000E1358 File Offset: 0x000E0358
		public override void onmousedown()
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate();
				return;
			}
		}

		// Token: 0x06018D7B RID: 101755 RVA: 0x000E1384 File Offset: 0x000E0384
		public override void onmousemove()
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate();
				return;
			}
		}

		// Token: 0x06018D7C RID: 101756 RVA: 0x000E13B0 File Offset: 0x000E03B0
		public override void onmouseover()
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate();
				return;
			}
		}

		// Token: 0x06018D7D RID: 101757 RVA: 0x000E13DC File Offset: 0x000E03DC
		public override void onmouseout()
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate();
				return;
			}
		}

		// Token: 0x06018D7E RID: 101758 RVA: 0x000E1408 File Offset: 0x000E0408
		public override void onkeyup()
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate();
				return;
			}
		}

		// Token: 0x06018D7F RID: 101759 RVA: 0x000E1434 File Offset: 0x000E0434
		public override void onkeydown()
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate();
				return;
			}
		}

		// Token: 0x06018D80 RID: 101760 RVA: 0x000E1460 File Offset: 0x000E0460
		public override bool onkeypress()
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate();
		}

		// Token: 0x06018D81 RID: 101761 RVA: 0x000E148C File Offset: 0x000E048C
		public override bool ondblclick()
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate();
		}

		// Token: 0x06018D82 RID: 101762 RVA: 0x000E14B8 File Offset: 0x000E04B8
		public override bool onclick()
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate();
		}

		// Token: 0x06018D83 RID: 101763 RVA: 0x000E14E4 File Offset: 0x000E04E4
		public override bool onhelp()
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate();
		}

		// Token: 0x06018D84 RID: 101764 RVA: 0x000E1510 File Offset: 0x000E0510
		internal HTMLInputFileElementEvents_SinkHelper()
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

		// Token: 0x04000D34 RID: 3380
		public HTMLInputFileElementEvents_onabortEventHandler m_onabortDelegate;

		// Token: 0x04000D35 RID: 3381
		public HTMLInputFileElementEvents_onerrorEventHandler m_onerrorDelegate;

		// Token: 0x04000D36 RID: 3382
		public HTMLInputFileElementEvents_onloadEventHandler m_onloadDelegate;

		// Token: 0x04000D37 RID: 3383
		public HTMLInputFileElementEvents_onselectEventHandler m_onselectDelegate;

		// Token: 0x04000D38 RID: 3384
		public HTMLInputFileElementEvents_onchangeEventHandler m_onchangeDelegate;

		// Token: 0x04000D39 RID: 3385
		public HTMLInputFileElementEvents_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x04000D3A RID: 3386
		public HTMLInputFileElementEvents_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x04000D3B RID: 3387
		public HTMLInputFileElementEvents_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x04000D3C RID: 3388
		public HTMLInputFileElementEvents_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x04000D3D RID: 3389
		public HTMLInputFileElementEvents_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x04000D3E RID: 3390
		public HTMLInputFileElementEvents_onmouseleaveEventHandler m_onmouseleaveDelegate;

		// Token: 0x04000D3F RID: 3391
		public HTMLInputFileElementEvents_onmouseenterEventHandler m_onmouseenterDelegate;

		// Token: 0x04000D40 RID: 3392
		public HTMLInputFileElementEvents_onresizeendEventHandler m_onresizeendDelegate;

		// Token: 0x04000D41 RID: 3393
		public HTMLInputFileElementEvents_onresizestartEventHandler m_onresizestartDelegate;

		// Token: 0x04000D42 RID: 3394
		public HTMLInputFileElementEvents_onmoveendEventHandler m_onmoveendDelegate;

		// Token: 0x04000D43 RID: 3395
		public HTMLInputFileElementEvents_onmovestartEventHandler m_onmovestartDelegate;

		// Token: 0x04000D44 RID: 3396
		public HTMLInputFileElementEvents_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x04000D45 RID: 3397
		public HTMLInputFileElementEvents_onmoveEventHandler m_onmoveDelegate;

		// Token: 0x04000D46 RID: 3398
		public HTMLInputFileElementEvents_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x04000D47 RID: 3399
		public HTMLInputFileElementEvents_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x04000D48 RID: 3400
		public HTMLInputFileElementEvents_onpageEventHandler m_onpageDelegate;

		// Token: 0x04000D49 RID: 3401
		public HTMLInputFileElementEvents_onlayoutcompleteEventHandler m_onlayoutcompleteDelegate;

		// Token: 0x04000D4A RID: 3402
		public HTMLInputFileElementEvents_onbeforeeditfocusEventHandler m_onbeforeeditfocusDelegate;

		// Token: 0x04000D4B RID: 3403
		public HTMLInputFileElementEvents_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x04000D4C RID: 3404
		public HTMLInputFileElementEvents_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x04000D4D RID: 3405
		public HTMLInputFileElementEvents_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x04000D4E RID: 3406
		public HTMLInputFileElementEvents_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x04000D4F RID: 3407
		public HTMLInputFileElementEvents_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x04000D50 RID: 3408
		public HTMLInputFileElementEvents_onpasteEventHandler m_onpasteDelegate;

		// Token: 0x04000D51 RID: 3409
		public HTMLInputFileElementEvents_onbeforepasteEventHandler m_onbeforepasteDelegate;

		// Token: 0x04000D52 RID: 3410
		public HTMLInputFileElementEvents_oncopyEventHandler m_oncopyDelegate;

		// Token: 0x04000D53 RID: 3411
		public HTMLInputFileElementEvents_onbeforecopyEventHandler m_onbeforecopyDelegate;

		// Token: 0x04000D54 RID: 3412
		public HTMLInputFileElementEvents_oncutEventHandler m_oncutDelegate;

		// Token: 0x04000D55 RID: 3413
		public HTMLInputFileElementEvents_onbeforecutEventHandler m_onbeforecutDelegate;

		// Token: 0x04000D56 RID: 3414
		public HTMLInputFileElementEvents_ondropEventHandler m_ondropDelegate;

		// Token: 0x04000D57 RID: 3415
		public HTMLInputFileElementEvents_ondragleaveEventHandler m_ondragleaveDelegate;

		// Token: 0x04000D58 RID: 3416
		public HTMLInputFileElementEvents_ondragoverEventHandler m_ondragoverDelegate;

		// Token: 0x04000D59 RID: 3417
		public HTMLInputFileElementEvents_ondragenterEventHandler m_ondragenterDelegate;

		// Token: 0x04000D5A RID: 3418
		public HTMLInputFileElementEvents_ondragendEventHandler m_ondragendDelegate;

		// Token: 0x04000D5B RID: 3419
		public HTMLInputFileElementEvents_ondragEventHandler m_ondragDelegate;

		// Token: 0x04000D5C RID: 3420
		public HTMLInputFileElementEvents_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x04000D5D RID: 3421
		public HTMLInputFileElementEvents_onblurEventHandler m_onblurDelegate;

		// Token: 0x04000D5E RID: 3422
		public HTMLInputFileElementEvents_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x04000D5F RID: 3423
		public HTMLInputFileElementEvents_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x04000D60 RID: 3424
		public HTMLInputFileElementEvents_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x04000D61 RID: 3425
		public HTMLInputFileElementEvents_onlosecaptureEventHandler m_onlosecaptureDelegate;

		// Token: 0x04000D62 RID: 3426
		public HTMLInputFileElementEvents_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x04000D63 RID: 3427
		public HTMLInputFileElementEvents_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x04000D64 RID: 3428
		public HTMLInputFileElementEvents_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x04000D65 RID: 3429
		public HTMLInputFileElementEvents_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x04000D66 RID: 3430
		public HTMLInputFileElementEvents_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x04000D67 RID: 3431
		public HTMLInputFileElementEvents_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x04000D68 RID: 3432
		public HTMLInputFileElementEvents_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x04000D69 RID: 3433
		public HTMLInputFileElementEvents_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x04000D6A RID: 3434
		public HTMLInputFileElementEvents_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x04000D6B RID: 3435
		public HTMLInputFileElementEvents_onfilterchangeEventHandler m_onfilterchangeDelegate;

		// Token: 0x04000D6C RID: 3436
		public HTMLInputFileElementEvents_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x04000D6D RID: 3437
		public HTMLInputFileElementEvents_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x04000D6E RID: 3438
		public HTMLInputFileElementEvents_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x04000D6F RID: 3439
		public HTMLInputFileElementEvents_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x04000D70 RID: 3440
		public HTMLInputFileElementEvents_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x04000D71 RID: 3441
		public HTMLInputFileElementEvents_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x04000D72 RID: 3442
		public HTMLInputFileElementEvents_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x04000D73 RID: 3443
		public HTMLInputFileElementEvents_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x04000D74 RID: 3444
		public HTMLInputFileElementEvents_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x04000D75 RID: 3445
		public HTMLInputFileElementEvents_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x04000D76 RID: 3446
		public HTMLInputFileElementEvents_onclickEventHandler m_onclickDelegate;

		// Token: 0x04000D77 RID: 3447
		public HTMLInputFileElementEvents_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x04000D78 RID: 3448
		public int m_dwCookie;
	}
}
