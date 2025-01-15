using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DF8 RID: 3576
	[ClassInterface(0)]
	internal sealed class HTMLAnchorEvents_SinkHelper : HTMLAnchorEvents
	{
		// Token: 0x060187DD RID: 100317 RVA: 0x000AF97C File Offset: 0x000AE97C
		public override void onfocusout()
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate();
				return;
			}
		}

		// Token: 0x060187DE RID: 100318 RVA: 0x000AF9A8 File Offset: 0x000AE9A8
		public override void onfocusin()
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate();
				return;
			}
		}

		// Token: 0x060187DF RID: 100319 RVA: 0x000AF9D4 File Offset: 0x000AE9D4
		public override void ondeactivate()
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate();
				return;
			}
		}

		// Token: 0x060187E0 RID: 100320 RVA: 0x000AFA00 File Offset: 0x000AEA00
		public override void onactivate()
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate();
				return;
			}
		}

		// Token: 0x060187E1 RID: 100321 RVA: 0x000AFA2C File Offset: 0x000AEA2C
		public override bool onmousewheel()
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate();
		}

		// Token: 0x060187E2 RID: 100322 RVA: 0x000AFA58 File Offset: 0x000AEA58
		public override void onmouseleave()
		{
			if (this.m_onmouseleaveDelegate != null)
			{
				this.m_onmouseleaveDelegate();
				return;
			}
		}

		// Token: 0x060187E3 RID: 100323 RVA: 0x000AFA84 File Offset: 0x000AEA84
		public override void onmouseenter()
		{
			if (this.m_onmouseenterDelegate != null)
			{
				this.m_onmouseenterDelegate();
				return;
			}
		}

		// Token: 0x060187E4 RID: 100324 RVA: 0x000AFAB0 File Offset: 0x000AEAB0
		public override void onresizeend()
		{
			if (this.m_onresizeendDelegate != null)
			{
				this.m_onresizeendDelegate();
				return;
			}
		}

		// Token: 0x060187E5 RID: 100325 RVA: 0x000AFADC File Offset: 0x000AEADC
		public override bool onresizestart()
		{
			return this.m_onresizestartDelegate != null && this.m_onresizestartDelegate();
		}

		// Token: 0x060187E6 RID: 100326 RVA: 0x000AFB08 File Offset: 0x000AEB08
		public override void onmoveend()
		{
			if (this.m_onmoveendDelegate != null)
			{
				this.m_onmoveendDelegate();
				return;
			}
		}

		// Token: 0x060187E7 RID: 100327 RVA: 0x000AFB34 File Offset: 0x000AEB34
		public override bool onmovestart()
		{
			return this.m_onmovestartDelegate != null && this.m_onmovestartDelegate();
		}

		// Token: 0x060187E8 RID: 100328 RVA: 0x000AFB60 File Offset: 0x000AEB60
		public override bool oncontrolselect()
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate();
		}

		// Token: 0x060187E9 RID: 100329 RVA: 0x000AFB8C File Offset: 0x000AEB8C
		public override void onmove()
		{
			if (this.m_onmoveDelegate != null)
			{
				this.m_onmoveDelegate();
				return;
			}
		}

		// Token: 0x060187EA RID: 100330 RVA: 0x000AFBB8 File Offset: 0x000AEBB8
		public override bool onbeforeactivate()
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate();
		}

		// Token: 0x060187EB RID: 100331 RVA: 0x000AFBE4 File Offset: 0x000AEBE4
		public override bool onbeforedeactivate()
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate();
		}

		// Token: 0x060187EC RID: 100332 RVA: 0x000AFC10 File Offset: 0x000AEC10
		public override void onpage()
		{
			if (this.m_onpageDelegate != null)
			{
				this.m_onpageDelegate();
				return;
			}
		}

		// Token: 0x060187ED RID: 100333 RVA: 0x000AFC3C File Offset: 0x000AEC3C
		public override void onlayoutcomplete()
		{
			if (this.m_onlayoutcompleteDelegate != null)
			{
				this.m_onlayoutcompleteDelegate();
				return;
			}
		}

		// Token: 0x060187EE RID: 100334 RVA: 0x000AFC68 File Offset: 0x000AEC68
		public override void onbeforeeditfocus()
		{
			if (this.m_onbeforeeditfocusDelegate != null)
			{
				this.m_onbeforeeditfocusDelegate();
				return;
			}
		}

		// Token: 0x060187EF RID: 100335 RVA: 0x000AFC94 File Offset: 0x000AEC94
		public override void onreadystatechange()
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate();
				return;
			}
		}

		// Token: 0x060187F0 RID: 100336 RVA: 0x000AFCC0 File Offset: 0x000AECC0
		public override void oncellchange()
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate();
				return;
			}
		}

		// Token: 0x060187F1 RID: 100337 RVA: 0x000AFCEC File Offset: 0x000AECEC
		public override void onrowsinserted()
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate();
				return;
			}
		}

		// Token: 0x060187F2 RID: 100338 RVA: 0x000AFD18 File Offset: 0x000AED18
		public override void onrowsdelete()
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate();
				return;
			}
		}

		// Token: 0x060187F3 RID: 100339 RVA: 0x000AFD44 File Offset: 0x000AED44
		public override bool oncontextmenu()
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate();
		}

		// Token: 0x060187F4 RID: 100340 RVA: 0x000AFD70 File Offset: 0x000AED70
		public override bool onpaste()
		{
			return this.m_onpasteDelegate != null && this.m_onpasteDelegate();
		}

		// Token: 0x060187F5 RID: 100341 RVA: 0x000AFD9C File Offset: 0x000AED9C
		public override bool onbeforepaste()
		{
			return this.m_onbeforepasteDelegate != null && this.m_onbeforepasteDelegate();
		}

		// Token: 0x060187F6 RID: 100342 RVA: 0x000AFDC8 File Offset: 0x000AEDC8
		public override bool oncopy()
		{
			return this.m_oncopyDelegate != null && this.m_oncopyDelegate();
		}

		// Token: 0x060187F7 RID: 100343 RVA: 0x000AFDF4 File Offset: 0x000AEDF4
		public override bool onbeforecopy()
		{
			return this.m_onbeforecopyDelegate != null && this.m_onbeforecopyDelegate();
		}

		// Token: 0x060187F8 RID: 100344 RVA: 0x000AFE20 File Offset: 0x000AEE20
		public override bool oncut()
		{
			return this.m_oncutDelegate != null && this.m_oncutDelegate();
		}

		// Token: 0x060187F9 RID: 100345 RVA: 0x000AFE4C File Offset: 0x000AEE4C
		public override bool onbeforecut()
		{
			return this.m_onbeforecutDelegate != null && this.m_onbeforecutDelegate();
		}

		// Token: 0x060187FA RID: 100346 RVA: 0x000AFE78 File Offset: 0x000AEE78
		public override bool ondrop()
		{
			return this.m_ondropDelegate != null && this.m_ondropDelegate();
		}

		// Token: 0x060187FB RID: 100347 RVA: 0x000AFEA4 File Offset: 0x000AEEA4
		public override void ondragleave()
		{
			if (this.m_ondragleaveDelegate != null)
			{
				this.m_ondragleaveDelegate();
				return;
			}
		}

		// Token: 0x060187FC RID: 100348 RVA: 0x000AFED0 File Offset: 0x000AEED0
		public override bool ondragover()
		{
			return this.m_ondragoverDelegate != null && this.m_ondragoverDelegate();
		}

		// Token: 0x060187FD RID: 100349 RVA: 0x000AFEFC File Offset: 0x000AEEFC
		public override bool ondragenter()
		{
			return this.m_ondragenterDelegate != null && this.m_ondragenterDelegate();
		}

		// Token: 0x060187FE RID: 100350 RVA: 0x000AFF28 File Offset: 0x000AEF28
		public override void ondragend()
		{
			if (this.m_ondragendDelegate != null)
			{
				this.m_ondragendDelegate();
				return;
			}
		}

		// Token: 0x060187FF RID: 100351 RVA: 0x000AFF54 File Offset: 0x000AEF54
		public override bool ondrag()
		{
			return this.m_ondragDelegate != null && this.m_ondragDelegate();
		}

		// Token: 0x06018800 RID: 100352 RVA: 0x000AFF80 File Offset: 0x000AEF80
		public override void onresize()
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate();
				return;
			}
		}

		// Token: 0x06018801 RID: 100353 RVA: 0x000AFFAC File Offset: 0x000AEFAC
		public override void onblur()
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate();
				return;
			}
		}

		// Token: 0x06018802 RID: 100354 RVA: 0x000AFFD8 File Offset: 0x000AEFD8
		public override void onfocus()
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate();
				return;
			}
		}

		// Token: 0x06018803 RID: 100355 RVA: 0x000B0004 File Offset: 0x000AF004
		public override void onscroll()
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate();
				return;
			}
		}

		// Token: 0x06018804 RID: 100356 RVA: 0x000B0030 File Offset: 0x000AF030
		public override void onpropertychange()
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate();
				return;
			}
		}

		// Token: 0x06018805 RID: 100357 RVA: 0x000B005C File Offset: 0x000AF05C
		public override void onlosecapture()
		{
			if (this.m_onlosecaptureDelegate != null)
			{
				this.m_onlosecaptureDelegate();
				return;
			}
		}

		// Token: 0x06018806 RID: 100358 RVA: 0x000B0088 File Offset: 0x000AF088
		public override void ondatasetcomplete()
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate();
				return;
			}
		}

		// Token: 0x06018807 RID: 100359 RVA: 0x000B00B4 File Offset: 0x000AF0B4
		public override void ondataavailable()
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate();
				return;
			}
		}

		// Token: 0x06018808 RID: 100360 RVA: 0x000B00E0 File Offset: 0x000AF0E0
		public override void ondatasetchanged()
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate();
				return;
			}
		}

		// Token: 0x06018809 RID: 100361 RVA: 0x000B010C File Offset: 0x000AF10C
		public override void onrowenter()
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate();
				return;
			}
		}

		// Token: 0x0601880A RID: 100362 RVA: 0x000B0138 File Offset: 0x000AF138
		public override bool onrowexit()
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate();
		}

		// Token: 0x0601880B RID: 100363 RVA: 0x000B0164 File Offset: 0x000AF164
		public override bool onerrorupdate()
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate();
		}

		// Token: 0x0601880C RID: 100364 RVA: 0x000B0190 File Offset: 0x000AF190
		public override void onafterupdate()
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate();
				return;
			}
		}

		// Token: 0x0601880D RID: 100365 RVA: 0x000B01BC File Offset: 0x000AF1BC
		public override bool onbeforeupdate()
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate();
		}

		// Token: 0x0601880E RID: 100366 RVA: 0x000B01E8 File Offset: 0x000AF1E8
		public override bool ondragstart()
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate();
		}

		// Token: 0x0601880F RID: 100367 RVA: 0x000B0214 File Offset: 0x000AF214
		public override void onfilterchange()
		{
			if (this.m_onfilterchangeDelegate != null)
			{
				this.m_onfilterchangeDelegate();
				return;
			}
		}

		// Token: 0x06018810 RID: 100368 RVA: 0x000B0240 File Offset: 0x000AF240
		public override bool onselectstart()
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate();
		}

		// Token: 0x06018811 RID: 100369 RVA: 0x000B026C File Offset: 0x000AF26C
		public override void onmouseup()
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate();
				return;
			}
		}

		// Token: 0x06018812 RID: 100370 RVA: 0x000B0298 File Offset: 0x000AF298
		public override void onmousedown()
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate();
				return;
			}
		}

		// Token: 0x06018813 RID: 100371 RVA: 0x000B02C4 File Offset: 0x000AF2C4
		public override void onmousemove()
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate();
				return;
			}
		}

		// Token: 0x06018814 RID: 100372 RVA: 0x000B02F0 File Offset: 0x000AF2F0
		public override void onmouseover()
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate();
				return;
			}
		}

		// Token: 0x06018815 RID: 100373 RVA: 0x000B031C File Offset: 0x000AF31C
		public override void onmouseout()
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate();
				return;
			}
		}

		// Token: 0x06018816 RID: 100374 RVA: 0x000B0348 File Offset: 0x000AF348
		public override void onkeyup()
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate();
				return;
			}
		}

		// Token: 0x06018817 RID: 100375 RVA: 0x000B0374 File Offset: 0x000AF374
		public override void onkeydown()
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate();
				return;
			}
		}

		// Token: 0x06018818 RID: 100376 RVA: 0x000B03A0 File Offset: 0x000AF3A0
		public override bool onkeypress()
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate();
		}

		// Token: 0x06018819 RID: 100377 RVA: 0x000B03CC File Offset: 0x000AF3CC
		public override bool ondblclick()
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate();
		}

		// Token: 0x0601881A RID: 100378 RVA: 0x000B03F8 File Offset: 0x000AF3F8
		public override bool onclick()
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate();
		}

		// Token: 0x0601881B RID: 100379 RVA: 0x000B0424 File Offset: 0x000AF424
		public override bool onhelp()
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate();
		}

		// Token: 0x0601881C RID: 100380 RVA: 0x000B0450 File Offset: 0x000AF450
		internal HTMLAnchorEvents_SinkHelper()
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

		// Token: 0x04000B58 RID: 2904
		public HTMLAnchorEvents_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x04000B59 RID: 2905
		public HTMLAnchorEvents_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x04000B5A RID: 2906
		public HTMLAnchorEvents_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x04000B5B RID: 2907
		public HTMLAnchorEvents_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x04000B5C RID: 2908
		public HTMLAnchorEvents_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x04000B5D RID: 2909
		public HTMLAnchorEvents_onmouseleaveEventHandler m_onmouseleaveDelegate;

		// Token: 0x04000B5E RID: 2910
		public HTMLAnchorEvents_onmouseenterEventHandler m_onmouseenterDelegate;

		// Token: 0x04000B5F RID: 2911
		public HTMLAnchorEvents_onresizeendEventHandler m_onresizeendDelegate;

		// Token: 0x04000B60 RID: 2912
		public HTMLAnchorEvents_onresizestartEventHandler m_onresizestartDelegate;

		// Token: 0x04000B61 RID: 2913
		public HTMLAnchorEvents_onmoveendEventHandler m_onmoveendDelegate;

		// Token: 0x04000B62 RID: 2914
		public HTMLAnchorEvents_onmovestartEventHandler m_onmovestartDelegate;

		// Token: 0x04000B63 RID: 2915
		public HTMLAnchorEvents_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x04000B64 RID: 2916
		public HTMLAnchorEvents_onmoveEventHandler m_onmoveDelegate;

		// Token: 0x04000B65 RID: 2917
		public HTMLAnchorEvents_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x04000B66 RID: 2918
		public HTMLAnchorEvents_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x04000B67 RID: 2919
		public HTMLAnchorEvents_onpageEventHandler m_onpageDelegate;

		// Token: 0x04000B68 RID: 2920
		public HTMLAnchorEvents_onlayoutcompleteEventHandler m_onlayoutcompleteDelegate;

		// Token: 0x04000B69 RID: 2921
		public HTMLAnchorEvents_onbeforeeditfocusEventHandler m_onbeforeeditfocusDelegate;

		// Token: 0x04000B6A RID: 2922
		public HTMLAnchorEvents_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x04000B6B RID: 2923
		public HTMLAnchorEvents_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x04000B6C RID: 2924
		public HTMLAnchorEvents_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x04000B6D RID: 2925
		public HTMLAnchorEvents_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x04000B6E RID: 2926
		public HTMLAnchorEvents_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x04000B6F RID: 2927
		public HTMLAnchorEvents_onpasteEventHandler m_onpasteDelegate;

		// Token: 0x04000B70 RID: 2928
		public HTMLAnchorEvents_onbeforepasteEventHandler m_onbeforepasteDelegate;

		// Token: 0x04000B71 RID: 2929
		public HTMLAnchorEvents_oncopyEventHandler m_oncopyDelegate;

		// Token: 0x04000B72 RID: 2930
		public HTMLAnchorEvents_onbeforecopyEventHandler m_onbeforecopyDelegate;

		// Token: 0x04000B73 RID: 2931
		public HTMLAnchorEvents_oncutEventHandler m_oncutDelegate;

		// Token: 0x04000B74 RID: 2932
		public HTMLAnchorEvents_onbeforecutEventHandler m_onbeforecutDelegate;

		// Token: 0x04000B75 RID: 2933
		public HTMLAnchorEvents_ondropEventHandler m_ondropDelegate;

		// Token: 0x04000B76 RID: 2934
		public HTMLAnchorEvents_ondragleaveEventHandler m_ondragleaveDelegate;

		// Token: 0x04000B77 RID: 2935
		public HTMLAnchorEvents_ondragoverEventHandler m_ondragoverDelegate;

		// Token: 0x04000B78 RID: 2936
		public HTMLAnchorEvents_ondragenterEventHandler m_ondragenterDelegate;

		// Token: 0x04000B79 RID: 2937
		public HTMLAnchorEvents_ondragendEventHandler m_ondragendDelegate;

		// Token: 0x04000B7A RID: 2938
		public HTMLAnchorEvents_ondragEventHandler m_ondragDelegate;

		// Token: 0x04000B7B RID: 2939
		public HTMLAnchorEvents_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x04000B7C RID: 2940
		public HTMLAnchorEvents_onblurEventHandler m_onblurDelegate;

		// Token: 0x04000B7D RID: 2941
		public HTMLAnchorEvents_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x04000B7E RID: 2942
		public HTMLAnchorEvents_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x04000B7F RID: 2943
		public HTMLAnchorEvents_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x04000B80 RID: 2944
		public HTMLAnchorEvents_onlosecaptureEventHandler m_onlosecaptureDelegate;

		// Token: 0x04000B81 RID: 2945
		public HTMLAnchorEvents_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x04000B82 RID: 2946
		public HTMLAnchorEvents_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x04000B83 RID: 2947
		public HTMLAnchorEvents_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x04000B84 RID: 2948
		public HTMLAnchorEvents_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x04000B85 RID: 2949
		public HTMLAnchorEvents_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x04000B86 RID: 2950
		public HTMLAnchorEvents_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x04000B87 RID: 2951
		public HTMLAnchorEvents_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x04000B88 RID: 2952
		public HTMLAnchorEvents_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x04000B89 RID: 2953
		public HTMLAnchorEvents_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x04000B8A RID: 2954
		public HTMLAnchorEvents_onfilterchangeEventHandler m_onfilterchangeDelegate;

		// Token: 0x04000B8B RID: 2955
		public HTMLAnchorEvents_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x04000B8C RID: 2956
		public HTMLAnchorEvents_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x04000B8D RID: 2957
		public HTMLAnchorEvents_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x04000B8E RID: 2958
		public HTMLAnchorEvents_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x04000B8F RID: 2959
		public HTMLAnchorEvents_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x04000B90 RID: 2960
		public HTMLAnchorEvents_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x04000B91 RID: 2961
		public HTMLAnchorEvents_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x04000B92 RID: 2962
		public HTMLAnchorEvents_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x04000B93 RID: 2963
		public HTMLAnchorEvents_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x04000B94 RID: 2964
		public HTMLAnchorEvents_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x04000B95 RID: 2965
		public HTMLAnchorEvents_onclickEventHandler m_onclickDelegate;

		// Token: 0x04000B96 RID: 2966
		public HTMLAnchorEvents_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x04000B97 RID: 2967
		public int m_dwCookie;
	}
}
