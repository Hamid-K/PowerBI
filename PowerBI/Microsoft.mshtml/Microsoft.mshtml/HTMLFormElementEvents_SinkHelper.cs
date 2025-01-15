using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DE0 RID: 3552
	[ClassInterface(0)]
	internal sealed class HTMLFormElementEvents_SinkHelper : HTMLFormElementEvents
	{
		// Token: 0x0601800C RID: 98316 RVA: 0x000685F4 File Offset: 0x000675F4
		public override bool onreset()
		{
			return this.m_onresetDelegate != null && this.m_onresetDelegate();
		}

		// Token: 0x0601800D RID: 98317 RVA: 0x00068620 File Offset: 0x00067620
		public override bool onsubmit()
		{
			return this.m_onsubmitDelegate != null && this.m_onsubmitDelegate();
		}

		// Token: 0x0601800E RID: 98318 RVA: 0x0006864C File Offset: 0x0006764C
		public override void onfocusout()
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate();
				return;
			}
		}

		// Token: 0x0601800F RID: 98319 RVA: 0x00068678 File Offset: 0x00067678
		public override void onfocusin()
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate();
				return;
			}
		}

		// Token: 0x06018010 RID: 98320 RVA: 0x000686A4 File Offset: 0x000676A4
		public override void ondeactivate()
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate();
				return;
			}
		}

		// Token: 0x06018011 RID: 98321 RVA: 0x000686D0 File Offset: 0x000676D0
		public override void onactivate()
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate();
				return;
			}
		}

		// Token: 0x06018012 RID: 98322 RVA: 0x000686FC File Offset: 0x000676FC
		public override bool onmousewheel()
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate();
		}

		// Token: 0x06018013 RID: 98323 RVA: 0x00068728 File Offset: 0x00067728
		public override void onmouseleave()
		{
			if (this.m_onmouseleaveDelegate != null)
			{
				this.m_onmouseleaveDelegate();
				return;
			}
		}

		// Token: 0x06018014 RID: 98324 RVA: 0x00068754 File Offset: 0x00067754
		public override void onmouseenter()
		{
			if (this.m_onmouseenterDelegate != null)
			{
				this.m_onmouseenterDelegate();
				return;
			}
		}

		// Token: 0x06018015 RID: 98325 RVA: 0x00068780 File Offset: 0x00067780
		public override void onresizeend()
		{
			if (this.m_onresizeendDelegate != null)
			{
				this.m_onresizeendDelegate();
				return;
			}
		}

		// Token: 0x06018016 RID: 98326 RVA: 0x000687AC File Offset: 0x000677AC
		public override bool onresizestart()
		{
			return this.m_onresizestartDelegate != null && this.m_onresizestartDelegate();
		}

		// Token: 0x06018017 RID: 98327 RVA: 0x000687D8 File Offset: 0x000677D8
		public override void onmoveend()
		{
			if (this.m_onmoveendDelegate != null)
			{
				this.m_onmoveendDelegate();
				return;
			}
		}

		// Token: 0x06018018 RID: 98328 RVA: 0x00068804 File Offset: 0x00067804
		public override bool onmovestart()
		{
			return this.m_onmovestartDelegate != null && this.m_onmovestartDelegate();
		}

		// Token: 0x06018019 RID: 98329 RVA: 0x00068830 File Offset: 0x00067830
		public override bool oncontrolselect()
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate();
		}

		// Token: 0x0601801A RID: 98330 RVA: 0x0006885C File Offset: 0x0006785C
		public override void onmove()
		{
			if (this.m_onmoveDelegate != null)
			{
				this.m_onmoveDelegate();
				return;
			}
		}

		// Token: 0x0601801B RID: 98331 RVA: 0x00068888 File Offset: 0x00067888
		public override bool onbeforeactivate()
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate();
		}

		// Token: 0x0601801C RID: 98332 RVA: 0x000688B4 File Offset: 0x000678B4
		public override bool onbeforedeactivate()
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate();
		}

		// Token: 0x0601801D RID: 98333 RVA: 0x000688E0 File Offset: 0x000678E0
		public override void onpage()
		{
			if (this.m_onpageDelegate != null)
			{
				this.m_onpageDelegate();
				return;
			}
		}

		// Token: 0x0601801E RID: 98334 RVA: 0x0006890C File Offset: 0x0006790C
		public override void onlayoutcomplete()
		{
			if (this.m_onlayoutcompleteDelegate != null)
			{
				this.m_onlayoutcompleteDelegate();
				return;
			}
		}

		// Token: 0x0601801F RID: 98335 RVA: 0x00068938 File Offset: 0x00067938
		public override void onbeforeeditfocus()
		{
			if (this.m_onbeforeeditfocusDelegate != null)
			{
				this.m_onbeforeeditfocusDelegate();
				return;
			}
		}

		// Token: 0x06018020 RID: 98336 RVA: 0x00068964 File Offset: 0x00067964
		public override void onreadystatechange()
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate();
				return;
			}
		}

		// Token: 0x06018021 RID: 98337 RVA: 0x00068990 File Offset: 0x00067990
		public override void oncellchange()
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate();
				return;
			}
		}

		// Token: 0x06018022 RID: 98338 RVA: 0x000689BC File Offset: 0x000679BC
		public override void onrowsinserted()
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate();
				return;
			}
		}

		// Token: 0x06018023 RID: 98339 RVA: 0x000689E8 File Offset: 0x000679E8
		public override void onrowsdelete()
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate();
				return;
			}
		}

		// Token: 0x06018024 RID: 98340 RVA: 0x00068A14 File Offset: 0x00067A14
		public override bool oncontextmenu()
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate();
		}

		// Token: 0x06018025 RID: 98341 RVA: 0x00068A40 File Offset: 0x00067A40
		public override bool onpaste()
		{
			return this.m_onpasteDelegate != null && this.m_onpasteDelegate();
		}

		// Token: 0x06018026 RID: 98342 RVA: 0x00068A6C File Offset: 0x00067A6C
		public override bool onbeforepaste()
		{
			return this.m_onbeforepasteDelegate != null && this.m_onbeforepasteDelegate();
		}

		// Token: 0x06018027 RID: 98343 RVA: 0x00068A98 File Offset: 0x00067A98
		public override bool oncopy()
		{
			return this.m_oncopyDelegate != null && this.m_oncopyDelegate();
		}

		// Token: 0x06018028 RID: 98344 RVA: 0x00068AC4 File Offset: 0x00067AC4
		public override bool onbeforecopy()
		{
			return this.m_onbeforecopyDelegate != null && this.m_onbeforecopyDelegate();
		}

		// Token: 0x06018029 RID: 98345 RVA: 0x00068AF0 File Offset: 0x00067AF0
		public override bool oncut()
		{
			return this.m_oncutDelegate != null && this.m_oncutDelegate();
		}

		// Token: 0x0601802A RID: 98346 RVA: 0x00068B1C File Offset: 0x00067B1C
		public override bool onbeforecut()
		{
			return this.m_onbeforecutDelegate != null && this.m_onbeforecutDelegate();
		}

		// Token: 0x0601802B RID: 98347 RVA: 0x00068B48 File Offset: 0x00067B48
		public override bool ondrop()
		{
			return this.m_ondropDelegate != null && this.m_ondropDelegate();
		}

		// Token: 0x0601802C RID: 98348 RVA: 0x00068B74 File Offset: 0x00067B74
		public override void ondragleave()
		{
			if (this.m_ondragleaveDelegate != null)
			{
				this.m_ondragleaveDelegate();
				return;
			}
		}

		// Token: 0x0601802D RID: 98349 RVA: 0x00068BA0 File Offset: 0x00067BA0
		public override bool ondragover()
		{
			return this.m_ondragoverDelegate != null && this.m_ondragoverDelegate();
		}

		// Token: 0x0601802E RID: 98350 RVA: 0x00068BCC File Offset: 0x00067BCC
		public override bool ondragenter()
		{
			return this.m_ondragenterDelegate != null && this.m_ondragenterDelegate();
		}

		// Token: 0x0601802F RID: 98351 RVA: 0x00068BF8 File Offset: 0x00067BF8
		public override void ondragend()
		{
			if (this.m_ondragendDelegate != null)
			{
				this.m_ondragendDelegate();
				return;
			}
		}

		// Token: 0x06018030 RID: 98352 RVA: 0x00068C24 File Offset: 0x00067C24
		public override bool ondrag()
		{
			return this.m_ondragDelegate != null && this.m_ondragDelegate();
		}

		// Token: 0x06018031 RID: 98353 RVA: 0x00068C50 File Offset: 0x00067C50
		public override void onresize()
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate();
				return;
			}
		}

		// Token: 0x06018032 RID: 98354 RVA: 0x00068C7C File Offset: 0x00067C7C
		public override void onblur()
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate();
				return;
			}
		}

		// Token: 0x06018033 RID: 98355 RVA: 0x00068CA8 File Offset: 0x00067CA8
		public override void onfocus()
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate();
				return;
			}
		}

		// Token: 0x06018034 RID: 98356 RVA: 0x00068CD4 File Offset: 0x00067CD4
		public override void onscroll()
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate();
				return;
			}
		}

		// Token: 0x06018035 RID: 98357 RVA: 0x00068D00 File Offset: 0x00067D00
		public override void onpropertychange()
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate();
				return;
			}
		}

		// Token: 0x06018036 RID: 98358 RVA: 0x00068D2C File Offset: 0x00067D2C
		public override void onlosecapture()
		{
			if (this.m_onlosecaptureDelegate != null)
			{
				this.m_onlosecaptureDelegate();
				return;
			}
		}

		// Token: 0x06018037 RID: 98359 RVA: 0x00068D58 File Offset: 0x00067D58
		public override void ondatasetcomplete()
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate();
				return;
			}
		}

		// Token: 0x06018038 RID: 98360 RVA: 0x00068D84 File Offset: 0x00067D84
		public override void ondataavailable()
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate();
				return;
			}
		}

		// Token: 0x06018039 RID: 98361 RVA: 0x00068DB0 File Offset: 0x00067DB0
		public override void ondatasetchanged()
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate();
				return;
			}
		}

		// Token: 0x0601803A RID: 98362 RVA: 0x00068DDC File Offset: 0x00067DDC
		public override void onrowenter()
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate();
				return;
			}
		}

		// Token: 0x0601803B RID: 98363 RVA: 0x00068E08 File Offset: 0x00067E08
		public override bool onrowexit()
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate();
		}

		// Token: 0x0601803C RID: 98364 RVA: 0x00068E34 File Offset: 0x00067E34
		public override bool onerrorupdate()
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate();
		}

		// Token: 0x0601803D RID: 98365 RVA: 0x00068E60 File Offset: 0x00067E60
		public override void onafterupdate()
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate();
				return;
			}
		}

		// Token: 0x0601803E RID: 98366 RVA: 0x00068E8C File Offset: 0x00067E8C
		public override bool onbeforeupdate()
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate();
		}

		// Token: 0x0601803F RID: 98367 RVA: 0x00068EB8 File Offset: 0x00067EB8
		public override bool ondragstart()
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate();
		}

		// Token: 0x06018040 RID: 98368 RVA: 0x00068EE4 File Offset: 0x00067EE4
		public override void onfilterchange()
		{
			if (this.m_onfilterchangeDelegate != null)
			{
				this.m_onfilterchangeDelegate();
				return;
			}
		}

		// Token: 0x06018041 RID: 98369 RVA: 0x00068F10 File Offset: 0x00067F10
		public override bool onselectstart()
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate();
		}

		// Token: 0x06018042 RID: 98370 RVA: 0x00068F3C File Offset: 0x00067F3C
		public override void onmouseup()
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate();
				return;
			}
		}

		// Token: 0x06018043 RID: 98371 RVA: 0x00068F68 File Offset: 0x00067F68
		public override void onmousedown()
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate();
				return;
			}
		}

		// Token: 0x06018044 RID: 98372 RVA: 0x00068F94 File Offset: 0x00067F94
		public override void onmousemove()
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate();
				return;
			}
		}

		// Token: 0x06018045 RID: 98373 RVA: 0x00068FC0 File Offset: 0x00067FC0
		public override void onmouseover()
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate();
				return;
			}
		}

		// Token: 0x06018046 RID: 98374 RVA: 0x00068FEC File Offset: 0x00067FEC
		public override void onmouseout()
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate();
				return;
			}
		}

		// Token: 0x06018047 RID: 98375 RVA: 0x00069018 File Offset: 0x00068018
		public override void onkeyup()
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate();
				return;
			}
		}

		// Token: 0x06018048 RID: 98376 RVA: 0x00069044 File Offset: 0x00068044
		public override void onkeydown()
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate();
				return;
			}
		}

		// Token: 0x06018049 RID: 98377 RVA: 0x00069070 File Offset: 0x00068070
		public override bool onkeypress()
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate();
		}

		// Token: 0x0601804A RID: 98378 RVA: 0x0006909C File Offset: 0x0006809C
		public override bool ondblclick()
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate();
		}

		// Token: 0x0601804B RID: 98379 RVA: 0x000690C8 File Offset: 0x000680C8
		public override bool onclick()
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate();
		}

		// Token: 0x0601804C RID: 98380 RVA: 0x000690F4 File Offset: 0x000680F4
		public override bool onhelp()
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate();
		}

		// Token: 0x0601804D RID: 98381 RVA: 0x00069120 File Offset: 0x00068120
		internal HTMLFormElementEvents_SinkHelper()
		{
			this.m_dwCookie = 0;
			this.m_onresetDelegate = null;
			this.m_onsubmitDelegate = null;
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

		// Token: 0x040008A1 RID: 2209
		public HTMLFormElementEvents_onresetEventHandler m_onresetDelegate;

		// Token: 0x040008A2 RID: 2210
		public HTMLFormElementEvents_onsubmitEventHandler m_onsubmitDelegate;

		// Token: 0x040008A3 RID: 2211
		public HTMLFormElementEvents_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x040008A4 RID: 2212
		public HTMLFormElementEvents_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x040008A5 RID: 2213
		public HTMLFormElementEvents_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x040008A6 RID: 2214
		public HTMLFormElementEvents_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x040008A7 RID: 2215
		public HTMLFormElementEvents_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x040008A8 RID: 2216
		public HTMLFormElementEvents_onmouseleaveEventHandler m_onmouseleaveDelegate;

		// Token: 0x040008A9 RID: 2217
		public HTMLFormElementEvents_onmouseenterEventHandler m_onmouseenterDelegate;

		// Token: 0x040008AA RID: 2218
		public HTMLFormElementEvents_onresizeendEventHandler m_onresizeendDelegate;

		// Token: 0x040008AB RID: 2219
		public HTMLFormElementEvents_onresizestartEventHandler m_onresizestartDelegate;

		// Token: 0x040008AC RID: 2220
		public HTMLFormElementEvents_onmoveendEventHandler m_onmoveendDelegate;

		// Token: 0x040008AD RID: 2221
		public HTMLFormElementEvents_onmovestartEventHandler m_onmovestartDelegate;

		// Token: 0x040008AE RID: 2222
		public HTMLFormElementEvents_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x040008AF RID: 2223
		public HTMLFormElementEvents_onmoveEventHandler m_onmoveDelegate;

		// Token: 0x040008B0 RID: 2224
		public HTMLFormElementEvents_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x040008B1 RID: 2225
		public HTMLFormElementEvents_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x040008B2 RID: 2226
		public HTMLFormElementEvents_onpageEventHandler m_onpageDelegate;

		// Token: 0x040008B3 RID: 2227
		public HTMLFormElementEvents_onlayoutcompleteEventHandler m_onlayoutcompleteDelegate;

		// Token: 0x040008B4 RID: 2228
		public HTMLFormElementEvents_onbeforeeditfocusEventHandler m_onbeforeeditfocusDelegate;

		// Token: 0x040008B5 RID: 2229
		public HTMLFormElementEvents_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x040008B6 RID: 2230
		public HTMLFormElementEvents_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x040008B7 RID: 2231
		public HTMLFormElementEvents_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x040008B8 RID: 2232
		public HTMLFormElementEvents_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x040008B9 RID: 2233
		public HTMLFormElementEvents_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x040008BA RID: 2234
		public HTMLFormElementEvents_onpasteEventHandler m_onpasteDelegate;

		// Token: 0x040008BB RID: 2235
		public HTMLFormElementEvents_onbeforepasteEventHandler m_onbeforepasteDelegate;

		// Token: 0x040008BC RID: 2236
		public HTMLFormElementEvents_oncopyEventHandler m_oncopyDelegate;

		// Token: 0x040008BD RID: 2237
		public HTMLFormElementEvents_onbeforecopyEventHandler m_onbeforecopyDelegate;

		// Token: 0x040008BE RID: 2238
		public HTMLFormElementEvents_oncutEventHandler m_oncutDelegate;

		// Token: 0x040008BF RID: 2239
		public HTMLFormElementEvents_onbeforecutEventHandler m_onbeforecutDelegate;

		// Token: 0x040008C0 RID: 2240
		public HTMLFormElementEvents_ondropEventHandler m_ondropDelegate;

		// Token: 0x040008C1 RID: 2241
		public HTMLFormElementEvents_ondragleaveEventHandler m_ondragleaveDelegate;

		// Token: 0x040008C2 RID: 2242
		public HTMLFormElementEvents_ondragoverEventHandler m_ondragoverDelegate;

		// Token: 0x040008C3 RID: 2243
		public HTMLFormElementEvents_ondragenterEventHandler m_ondragenterDelegate;

		// Token: 0x040008C4 RID: 2244
		public HTMLFormElementEvents_ondragendEventHandler m_ondragendDelegate;

		// Token: 0x040008C5 RID: 2245
		public HTMLFormElementEvents_ondragEventHandler m_ondragDelegate;

		// Token: 0x040008C6 RID: 2246
		public HTMLFormElementEvents_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x040008C7 RID: 2247
		public HTMLFormElementEvents_onblurEventHandler m_onblurDelegate;

		// Token: 0x040008C8 RID: 2248
		public HTMLFormElementEvents_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x040008C9 RID: 2249
		public HTMLFormElementEvents_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x040008CA RID: 2250
		public HTMLFormElementEvents_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x040008CB RID: 2251
		public HTMLFormElementEvents_onlosecaptureEventHandler m_onlosecaptureDelegate;

		// Token: 0x040008CC RID: 2252
		public HTMLFormElementEvents_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x040008CD RID: 2253
		public HTMLFormElementEvents_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x040008CE RID: 2254
		public HTMLFormElementEvents_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x040008CF RID: 2255
		public HTMLFormElementEvents_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x040008D0 RID: 2256
		public HTMLFormElementEvents_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x040008D1 RID: 2257
		public HTMLFormElementEvents_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x040008D2 RID: 2258
		public HTMLFormElementEvents_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x040008D3 RID: 2259
		public HTMLFormElementEvents_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x040008D4 RID: 2260
		public HTMLFormElementEvents_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x040008D5 RID: 2261
		public HTMLFormElementEvents_onfilterchangeEventHandler m_onfilterchangeDelegate;

		// Token: 0x040008D6 RID: 2262
		public HTMLFormElementEvents_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x040008D7 RID: 2263
		public HTMLFormElementEvents_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x040008D8 RID: 2264
		public HTMLFormElementEvents_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x040008D9 RID: 2265
		public HTMLFormElementEvents_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x040008DA RID: 2266
		public HTMLFormElementEvents_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x040008DB RID: 2267
		public HTMLFormElementEvents_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x040008DC RID: 2268
		public HTMLFormElementEvents_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x040008DD RID: 2269
		public HTMLFormElementEvents_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x040008DE RID: 2270
		public HTMLFormElementEvents_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x040008DF RID: 2271
		public HTMLFormElementEvents_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x040008E0 RID: 2272
		public HTMLFormElementEvents_onclickEventHandler m_onclickDelegate;

		// Token: 0x040008E1 RID: 2273
		public HTMLFormElementEvents_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x040008E2 RID: 2274
		public int m_dwCookie;
	}
}
