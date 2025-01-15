using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000E00 RID: 3584
	[ClassInterface(0)]
	internal sealed class HTMLMarqueeElementEvents_SinkHelper : HTMLMarqueeElementEvents
	{
		// Token: 0x06018AE8 RID: 101096 RVA: 0x000CB428 File Offset: 0x000CA428
		public override void onstart()
		{
			if (this.m_onstartDelegate != null)
			{
				this.m_onstartDelegate();
				return;
			}
		}

		// Token: 0x06018AE9 RID: 101097 RVA: 0x000CB454 File Offset: 0x000CA454
		public override void onfinish()
		{
			if (this.m_onfinishDelegate != null)
			{
				this.m_onfinishDelegate();
				return;
			}
		}

		// Token: 0x06018AEA RID: 101098 RVA: 0x000CB480 File Offset: 0x000CA480
		public override void onbounce()
		{
			if (this.m_onbounceDelegate != null)
			{
				this.m_onbounceDelegate();
				return;
			}
		}

		// Token: 0x06018AEB RID: 101099 RVA: 0x000CB4AC File Offset: 0x000CA4AC
		public override void onselect()
		{
			if (this.m_onselectDelegate != null)
			{
				this.m_onselectDelegate();
				return;
			}
		}

		// Token: 0x06018AEC RID: 101100 RVA: 0x000CB4D8 File Offset: 0x000CA4D8
		public override void onchange()
		{
			if (this.m_onchangeDelegate != null)
			{
				this.m_onchangeDelegate();
				return;
			}
		}

		// Token: 0x06018AED RID: 101101 RVA: 0x000CB504 File Offset: 0x000CA504
		public override void onfocusout()
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate();
				return;
			}
		}

		// Token: 0x06018AEE RID: 101102 RVA: 0x000CB530 File Offset: 0x000CA530
		public override void onfocusin()
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate();
				return;
			}
		}

		// Token: 0x06018AEF RID: 101103 RVA: 0x000CB55C File Offset: 0x000CA55C
		public override void ondeactivate()
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate();
				return;
			}
		}

		// Token: 0x06018AF0 RID: 101104 RVA: 0x000CB588 File Offset: 0x000CA588
		public override void onactivate()
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate();
				return;
			}
		}

		// Token: 0x06018AF1 RID: 101105 RVA: 0x000CB5B4 File Offset: 0x000CA5B4
		public override bool onmousewheel()
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate();
		}

		// Token: 0x06018AF2 RID: 101106 RVA: 0x000CB5E0 File Offset: 0x000CA5E0
		public override void onmouseleave()
		{
			if (this.m_onmouseleaveDelegate != null)
			{
				this.m_onmouseleaveDelegate();
				return;
			}
		}

		// Token: 0x06018AF3 RID: 101107 RVA: 0x000CB60C File Offset: 0x000CA60C
		public override void onmouseenter()
		{
			if (this.m_onmouseenterDelegate != null)
			{
				this.m_onmouseenterDelegate();
				return;
			}
		}

		// Token: 0x06018AF4 RID: 101108 RVA: 0x000CB638 File Offset: 0x000CA638
		public override void onresizeend()
		{
			if (this.m_onresizeendDelegate != null)
			{
				this.m_onresizeendDelegate();
				return;
			}
		}

		// Token: 0x06018AF5 RID: 101109 RVA: 0x000CB664 File Offset: 0x000CA664
		public override bool onresizestart()
		{
			return this.m_onresizestartDelegate != null && this.m_onresizestartDelegate();
		}

		// Token: 0x06018AF6 RID: 101110 RVA: 0x000CB690 File Offset: 0x000CA690
		public override void onmoveend()
		{
			if (this.m_onmoveendDelegate != null)
			{
				this.m_onmoveendDelegate();
				return;
			}
		}

		// Token: 0x06018AF7 RID: 101111 RVA: 0x000CB6BC File Offset: 0x000CA6BC
		public override bool onmovestart()
		{
			return this.m_onmovestartDelegate != null && this.m_onmovestartDelegate();
		}

		// Token: 0x06018AF8 RID: 101112 RVA: 0x000CB6E8 File Offset: 0x000CA6E8
		public override bool oncontrolselect()
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate();
		}

		// Token: 0x06018AF9 RID: 101113 RVA: 0x000CB714 File Offset: 0x000CA714
		public override void onmove()
		{
			if (this.m_onmoveDelegate != null)
			{
				this.m_onmoveDelegate();
				return;
			}
		}

		// Token: 0x06018AFA RID: 101114 RVA: 0x000CB740 File Offset: 0x000CA740
		public override bool onbeforeactivate()
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate();
		}

		// Token: 0x06018AFB RID: 101115 RVA: 0x000CB76C File Offset: 0x000CA76C
		public override bool onbeforedeactivate()
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate();
		}

		// Token: 0x06018AFC RID: 101116 RVA: 0x000CB798 File Offset: 0x000CA798
		public override void onpage()
		{
			if (this.m_onpageDelegate != null)
			{
				this.m_onpageDelegate();
				return;
			}
		}

		// Token: 0x06018AFD RID: 101117 RVA: 0x000CB7C4 File Offset: 0x000CA7C4
		public override void onlayoutcomplete()
		{
			if (this.m_onlayoutcompleteDelegate != null)
			{
				this.m_onlayoutcompleteDelegate();
				return;
			}
		}

		// Token: 0x06018AFE RID: 101118 RVA: 0x000CB7F0 File Offset: 0x000CA7F0
		public override void onbeforeeditfocus()
		{
			if (this.m_onbeforeeditfocusDelegate != null)
			{
				this.m_onbeforeeditfocusDelegate();
				return;
			}
		}

		// Token: 0x06018AFF RID: 101119 RVA: 0x000CB81C File Offset: 0x000CA81C
		public override void onreadystatechange()
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate();
				return;
			}
		}

		// Token: 0x06018B00 RID: 101120 RVA: 0x000CB848 File Offset: 0x000CA848
		public override void oncellchange()
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate();
				return;
			}
		}

		// Token: 0x06018B01 RID: 101121 RVA: 0x000CB874 File Offset: 0x000CA874
		public override void onrowsinserted()
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate();
				return;
			}
		}

		// Token: 0x06018B02 RID: 101122 RVA: 0x000CB8A0 File Offset: 0x000CA8A0
		public override void onrowsdelete()
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate();
				return;
			}
		}

		// Token: 0x06018B03 RID: 101123 RVA: 0x000CB8CC File Offset: 0x000CA8CC
		public override bool oncontextmenu()
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate();
		}

		// Token: 0x06018B04 RID: 101124 RVA: 0x000CB8F8 File Offset: 0x000CA8F8
		public override bool onpaste()
		{
			return this.m_onpasteDelegate != null && this.m_onpasteDelegate();
		}

		// Token: 0x06018B05 RID: 101125 RVA: 0x000CB924 File Offset: 0x000CA924
		public override bool onbeforepaste()
		{
			return this.m_onbeforepasteDelegate != null && this.m_onbeforepasteDelegate();
		}

		// Token: 0x06018B06 RID: 101126 RVA: 0x000CB950 File Offset: 0x000CA950
		public override bool oncopy()
		{
			return this.m_oncopyDelegate != null && this.m_oncopyDelegate();
		}

		// Token: 0x06018B07 RID: 101127 RVA: 0x000CB97C File Offset: 0x000CA97C
		public override bool onbeforecopy()
		{
			return this.m_onbeforecopyDelegate != null && this.m_onbeforecopyDelegate();
		}

		// Token: 0x06018B08 RID: 101128 RVA: 0x000CB9A8 File Offset: 0x000CA9A8
		public override bool oncut()
		{
			return this.m_oncutDelegate != null && this.m_oncutDelegate();
		}

		// Token: 0x06018B09 RID: 101129 RVA: 0x000CB9D4 File Offset: 0x000CA9D4
		public override bool onbeforecut()
		{
			return this.m_onbeforecutDelegate != null && this.m_onbeforecutDelegate();
		}

		// Token: 0x06018B0A RID: 101130 RVA: 0x000CBA00 File Offset: 0x000CAA00
		public override bool ondrop()
		{
			return this.m_ondropDelegate != null && this.m_ondropDelegate();
		}

		// Token: 0x06018B0B RID: 101131 RVA: 0x000CBA2C File Offset: 0x000CAA2C
		public override void ondragleave()
		{
			if (this.m_ondragleaveDelegate != null)
			{
				this.m_ondragleaveDelegate();
				return;
			}
		}

		// Token: 0x06018B0C RID: 101132 RVA: 0x000CBA58 File Offset: 0x000CAA58
		public override bool ondragover()
		{
			return this.m_ondragoverDelegate != null && this.m_ondragoverDelegate();
		}

		// Token: 0x06018B0D RID: 101133 RVA: 0x000CBA84 File Offset: 0x000CAA84
		public override bool ondragenter()
		{
			return this.m_ondragenterDelegate != null && this.m_ondragenterDelegate();
		}

		// Token: 0x06018B0E RID: 101134 RVA: 0x000CBAB0 File Offset: 0x000CAAB0
		public override void ondragend()
		{
			if (this.m_ondragendDelegate != null)
			{
				this.m_ondragendDelegate();
				return;
			}
		}

		// Token: 0x06018B0F RID: 101135 RVA: 0x000CBADC File Offset: 0x000CAADC
		public override bool ondrag()
		{
			return this.m_ondragDelegate != null && this.m_ondragDelegate();
		}

		// Token: 0x06018B10 RID: 101136 RVA: 0x000CBB08 File Offset: 0x000CAB08
		public override void onresize()
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate();
				return;
			}
		}

		// Token: 0x06018B11 RID: 101137 RVA: 0x000CBB34 File Offset: 0x000CAB34
		public override void onblur()
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate();
				return;
			}
		}

		// Token: 0x06018B12 RID: 101138 RVA: 0x000CBB60 File Offset: 0x000CAB60
		public override void onfocus()
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate();
				return;
			}
		}

		// Token: 0x06018B13 RID: 101139 RVA: 0x000CBB8C File Offset: 0x000CAB8C
		public override void onscroll()
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate();
				return;
			}
		}

		// Token: 0x06018B14 RID: 101140 RVA: 0x000CBBB8 File Offset: 0x000CABB8
		public override void onpropertychange()
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate();
				return;
			}
		}

		// Token: 0x06018B15 RID: 101141 RVA: 0x000CBBE4 File Offset: 0x000CABE4
		public override void onlosecapture()
		{
			if (this.m_onlosecaptureDelegate != null)
			{
				this.m_onlosecaptureDelegate();
				return;
			}
		}

		// Token: 0x06018B16 RID: 101142 RVA: 0x000CBC10 File Offset: 0x000CAC10
		public override void ondatasetcomplete()
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate();
				return;
			}
		}

		// Token: 0x06018B17 RID: 101143 RVA: 0x000CBC3C File Offset: 0x000CAC3C
		public override void ondataavailable()
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate();
				return;
			}
		}

		// Token: 0x06018B18 RID: 101144 RVA: 0x000CBC68 File Offset: 0x000CAC68
		public override void ondatasetchanged()
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate();
				return;
			}
		}

		// Token: 0x06018B19 RID: 101145 RVA: 0x000CBC94 File Offset: 0x000CAC94
		public override void onrowenter()
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate();
				return;
			}
		}

		// Token: 0x06018B1A RID: 101146 RVA: 0x000CBCC0 File Offset: 0x000CACC0
		public override bool onrowexit()
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate();
		}

		// Token: 0x06018B1B RID: 101147 RVA: 0x000CBCEC File Offset: 0x000CACEC
		public override bool onerrorupdate()
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate();
		}

		// Token: 0x06018B1C RID: 101148 RVA: 0x000CBD18 File Offset: 0x000CAD18
		public override void onafterupdate()
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate();
				return;
			}
		}

		// Token: 0x06018B1D RID: 101149 RVA: 0x000CBD44 File Offset: 0x000CAD44
		public override bool onbeforeupdate()
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate();
		}

		// Token: 0x06018B1E RID: 101150 RVA: 0x000CBD70 File Offset: 0x000CAD70
		public override bool ondragstart()
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate();
		}

		// Token: 0x06018B1F RID: 101151 RVA: 0x000CBD9C File Offset: 0x000CAD9C
		public override void onfilterchange()
		{
			if (this.m_onfilterchangeDelegate != null)
			{
				this.m_onfilterchangeDelegate();
				return;
			}
		}

		// Token: 0x06018B20 RID: 101152 RVA: 0x000CBDC8 File Offset: 0x000CADC8
		public override bool onselectstart()
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate();
		}

		// Token: 0x06018B21 RID: 101153 RVA: 0x000CBDF4 File Offset: 0x000CADF4
		public override void onmouseup()
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate();
				return;
			}
		}

		// Token: 0x06018B22 RID: 101154 RVA: 0x000CBE20 File Offset: 0x000CAE20
		public override void onmousedown()
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate();
				return;
			}
		}

		// Token: 0x06018B23 RID: 101155 RVA: 0x000CBE4C File Offset: 0x000CAE4C
		public override void onmousemove()
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate();
				return;
			}
		}

		// Token: 0x06018B24 RID: 101156 RVA: 0x000CBE78 File Offset: 0x000CAE78
		public override void onmouseover()
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate();
				return;
			}
		}

		// Token: 0x06018B25 RID: 101157 RVA: 0x000CBEA4 File Offset: 0x000CAEA4
		public override void onmouseout()
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate();
				return;
			}
		}

		// Token: 0x06018B26 RID: 101158 RVA: 0x000CBED0 File Offset: 0x000CAED0
		public override void onkeyup()
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate();
				return;
			}
		}

		// Token: 0x06018B27 RID: 101159 RVA: 0x000CBEFC File Offset: 0x000CAEFC
		public override void onkeydown()
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate();
				return;
			}
		}

		// Token: 0x06018B28 RID: 101160 RVA: 0x000CBF28 File Offset: 0x000CAF28
		public override bool onkeypress()
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate();
		}

		// Token: 0x06018B29 RID: 101161 RVA: 0x000CBF54 File Offset: 0x000CAF54
		public override bool ondblclick()
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate();
		}

		// Token: 0x06018B2A RID: 101162 RVA: 0x000CBF80 File Offset: 0x000CAF80
		public override bool onclick()
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate();
		}

		// Token: 0x06018B2B RID: 101163 RVA: 0x000CBFAC File Offset: 0x000CAFAC
		public override bool onhelp()
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate();
		}

		// Token: 0x06018B2C RID: 101164 RVA: 0x000CBFD8 File Offset: 0x000CAFD8
		internal HTMLMarqueeElementEvents_SinkHelper()
		{
			this.m_dwCookie = 0;
			this.m_onstartDelegate = null;
			this.m_onfinishDelegate = null;
			this.m_onbounceDelegate = null;
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

		// Token: 0x04000C65 RID: 3173
		public HTMLMarqueeElementEvents_onstartEventHandler m_onstartDelegate;

		// Token: 0x04000C66 RID: 3174
		public HTMLMarqueeElementEvents_onfinishEventHandler m_onfinishDelegate;

		// Token: 0x04000C67 RID: 3175
		public HTMLMarqueeElementEvents_onbounceEventHandler m_onbounceDelegate;

		// Token: 0x04000C68 RID: 3176
		public HTMLMarqueeElementEvents_onselectEventHandler m_onselectDelegate;

		// Token: 0x04000C69 RID: 3177
		public HTMLMarqueeElementEvents_onchangeEventHandler m_onchangeDelegate;

		// Token: 0x04000C6A RID: 3178
		public HTMLMarqueeElementEvents_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x04000C6B RID: 3179
		public HTMLMarqueeElementEvents_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x04000C6C RID: 3180
		public HTMLMarqueeElementEvents_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x04000C6D RID: 3181
		public HTMLMarqueeElementEvents_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x04000C6E RID: 3182
		public HTMLMarqueeElementEvents_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x04000C6F RID: 3183
		public HTMLMarqueeElementEvents_onmouseleaveEventHandler m_onmouseleaveDelegate;

		// Token: 0x04000C70 RID: 3184
		public HTMLMarqueeElementEvents_onmouseenterEventHandler m_onmouseenterDelegate;

		// Token: 0x04000C71 RID: 3185
		public HTMLMarqueeElementEvents_onresizeendEventHandler m_onresizeendDelegate;

		// Token: 0x04000C72 RID: 3186
		public HTMLMarqueeElementEvents_onresizestartEventHandler m_onresizestartDelegate;

		// Token: 0x04000C73 RID: 3187
		public HTMLMarqueeElementEvents_onmoveendEventHandler m_onmoveendDelegate;

		// Token: 0x04000C74 RID: 3188
		public HTMLMarqueeElementEvents_onmovestartEventHandler m_onmovestartDelegate;

		// Token: 0x04000C75 RID: 3189
		public HTMLMarqueeElementEvents_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x04000C76 RID: 3190
		public HTMLMarqueeElementEvents_onmoveEventHandler m_onmoveDelegate;

		// Token: 0x04000C77 RID: 3191
		public HTMLMarqueeElementEvents_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x04000C78 RID: 3192
		public HTMLMarqueeElementEvents_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x04000C79 RID: 3193
		public HTMLMarqueeElementEvents_onpageEventHandler m_onpageDelegate;

		// Token: 0x04000C7A RID: 3194
		public HTMLMarqueeElementEvents_onlayoutcompleteEventHandler m_onlayoutcompleteDelegate;

		// Token: 0x04000C7B RID: 3195
		public HTMLMarqueeElementEvents_onbeforeeditfocusEventHandler m_onbeforeeditfocusDelegate;

		// Token: 0x04000C7C RID: 3196
		public HTMLMarqueeElementEvents_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x04000C7D RID: 3197
		public HTMLMarqueeElementEvents_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x04000C7E RID: 3198
		public HTMLMarqueeElementEvents_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x04000C7F RID: 3199
		public HTMLMarqueeElementEvents_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x04000C80 RID: 3200
		public HTMLMarqueeElementEvents_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x04000C81 RID: 3201
		public HTMLMarqueeElementEvents_onpasteEventHandler m_onpasteDelegate;

		// Token: 0x04000C82 RID: 3202
		public HTMLMarqueeElementEvents_onbeforepasteEventHandler m_onbeforepasteDelegate;

		// Token: 0x04000C83 RID: 3203
		public HTMLMarqueeElementEvents_oncopyEventHandler m_oncopyDelegate;

		// Token: 0x04000C84 RID: 3204
		public HTMLMarqueeElementEvents_onbeforecopyEventHandler m_onbeforecopyDelegate;

		// Token: 0x04000C85 RID: 3205
		public HTMLMarqueeElementEvents_oncutEventHandler m_oncutDelegate;

		// Token: 0x04000C86 RID: 3206
		public HTMLMarqueeElementEvents_onbeforecutEventHandler m_onbeforecutDelegate;

		// Token: 0x04000C87 RID: 3207
		public HTMLMarqueeElementEvents_ondropEventHandler m_ondropDelegate;

		// Token: 0x04000C88 RID: 3208
		public HTMLMarqueeElementEvents_ondragleaveEventHandler m_ondragleaveDelegate;

		// Token: 0x04000C89 RID: 3209
		public HTMLMarqueeElementEvents_ondragoverEventHandler m_ondragoverDelegate;

		// Token: 0x04000C8A RID: 3210
		public HTMLMarqueeElementEvents_ondragenterEventHandler m_ondragenterDelegate;

		// Token: 0x04000C8B RID: 3211
		public HTMLMarqueeElementEvents_ondragendEventHandler m_ondragendDelegate;

		// Token: 0x04000C8C RID: 3212
		public HTMLMarqueeElementEvents_ondragEventHandler m_ondragDelegate;

		// Token: 0x04000C8D RID: 3213
		public HTMLMarqueeElementEvents_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x04000C8E RID: 3214
		public HTMLMarqueeElementEvents_onblurEventHandler m_onblurDelegate;

		// Token: 0x04000C8F RID: 3215
		public HTMLMarqueeElementEvents_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x04000C90 RID: 3216
		public HTMLMarqueeElementEvents_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x04000C91 RID: 3217
		public HTMLMarqueeElementEvents_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x04000C92 RID: 3218
		public HTMLMarqueeElementEvents_onlosecaptureEventHandler m_onlosecaptureDelegate;

		// Token: 0x04000C93 RID: 3219
		public HTMLMarqueeElementEvents_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x04000C94 RID: 3220
		public HTMLMarqueeElementEvents_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x04000C95 RID: 3221
		public HTMLMarqueeElementEvents_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x04000C96 RID: 3222
		public HTMLMarqueeElementEvents_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x04000C97 RID: 3223
		public HTMLMarqueeElementEvents_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x04000C98 RID: 3224
		public HTMLMarqueeElementEvents_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x04000C99 RID: 3225
		public HTMLMarqueeElementEvents_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x04000C9A RID: 3226
		public HTMLMarqueeElementEvents_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x04000C9B RID: 3227
		public HTMLMarqueeElementEvents_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x04000C9C RID: 3228
		public HTMLMarqueeElementEvents_onfilterchangeEventHandler m_onfilterchangeDelegate;

		// Token: 0x04000C9D RID: 3229
		public HTMLMarqueeElementEvents_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x04000C9E RID: 3230
		public HTMLMarqueeElementEvents_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x04000C9F RID: 3231
		public HTMLMarqueeElementEvents_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x04000CA0 RID: 3232
		public HTMLMarqueeElementEvents_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x04000CA1 RID: 3233
		public HTMLMarqueeElementEvents_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x04000CA2 RID: 3234
		public HTMLMarqueeElementEvents_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x04000CA3 RID: 3235
		public HTMLMarqueeElementEvents_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x04000CA4 RID: 3236
		public HTMLMarqueeElementEvents_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x04000CA5 RID: 3237
		public HTMLMarqueeElementEvents_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x04000CA6 RID: 3238
		public HTMLMarqueeElementEvents_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x04000CA7 RID: 3239
		public HTMLMarqueeElementEvents_onclickEventHandler m_onclickDelegate;

		// Token: 0x04000CA8 RID: 3240
		public HTMLMarqueeElementEvents_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x04000CA9 RID: 3241
		public int m_dwCookie;
	}
}
