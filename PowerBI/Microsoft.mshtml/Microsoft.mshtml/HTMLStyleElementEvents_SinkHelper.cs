using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000E16 RID: 3606
	[ClassInterface(0)]
	internal sealed class HTMLStyleElementEvents_SinkHelper : HTMLStyleElementEvents
	{
		// Token: 0x0601932F RID: 103215 RVA: 0x00116A2C File Offset: 0x00115A2C
		public override void onerror()
		{
			if (this.m_onerrorDelegate != null)
			{
				this.m_onerrorDelegate();
				return;
			}
		}

		// Token: 0x06019330 RID: 103216 RVA: 0x00116A58 File Offset: 0x00115A58
		public override void onload()
		{
			if (this.m_onloadDelegate != null)
			{
				this.m_onloadDelegate();
				return;
			}
		}

		// Token: 0x06019331 RID: 103217 RVA: 0x00116A84 File Offset: 0x00115A84
		public override void onfocusout()
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate();
				return;
			}
		}

		// Token: 0x06019332 RID: 103218 RVA: 0x00116AB0 File Offset: 0x00115AB0
		public override void onfocusin()
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate();
				return;
			}
		}

		// Token: 0x06019333 RID: 103219 RVA: 0x00116ADC File Offset: 0x00115ADC
		public override void ondeactivate()
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate();
				return;
			}
		}

		// Token: 0x06019334 RID: 103220 RVA: 0x00116B08 File Offset: 0x00115B08
		public override void onactivate()
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate();
				return;
			}
		}

		// Token: 0x06019335 RID: 103221 RVA: 0x00116B34 File Offset: 0x00115B34
		public override bool onmousewheel()
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate();
		}

		// Token: 0x06019336 RID: 103222 RVA: 0x00116B60 File Offset: 0x00115B60
		public override void onmouseleave()
		{
			if (this.m_onmouseleaveDelegate != null)
			{
				this.m_onmouseleaveDelegate();
				return;
			}
		}

		// Token: 0x06019337 RID: 103223 RVA: 0x00116B8C File Offset: 0x00115B8C
		public override void onmouseenter()
		{
			if (this.m_onmouseenterDelegate != null)
			{
				this.m_onmouseenterDelegate();
				return;
			}
		}

		// Token: 0x06019338 RID: 103224 RVA: 0x00116BB8 File Offset: 0x00115BB8
		public override void onresizeend()
		{
			if (this.m_onresizeendDelegate != null)
			{
				this.m_onresizeendDelegate();
				return;
			}
		}

		// Token: 0x06019339 RID: 103225 RVA: 0x00116BE4 File Offset: 0x00115BE4
		public override bool onresizestart()
		{
			return this.m_onresizestartDelegate != null && this.m_onresizestartDelegate();
		}

		// Token: 0x0601933A RID: 103226 RVA: 0x00116C10 File Offset: 0x00115C10
		public override void onmoveend()
		{
			if (this.m_onmoveendDelegate != null)
			{
				this.m_onmoveendDelegate();
				return;
			}
		}

		// Token: 0x0601933B RID: 103227 RVA: 0x00116C3C File Offset: 0x00115C3C
		public override bool onmovestart()
		{
			return this.m_onmovestartDelegate != null && this.m_onmovestartDelegate();
		}

		// Token: 0x0601933C RID: 103228 RVA: 0x00116C68 File Offset: 0x00115C68
		public override bool oncontrolselect()
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate();
		}

		// Token: 0x0601933D RID: 103229 RVA: 0x00116C94 File Offset: 0x00115C94
		public override void onmove()
		{
			if (this.m_onmoveDelegate != null)
			{
				this.m_onmoveDelegate();
				return;
			}
		}

		// Token: 0x0601933E RID: 103230 RVA: 0x00116CC0 File Offset: 0x00115CC0
		public override bool onbeforeactivate()
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate();
		}

		// Token: 0x0601933F RID: 103231 RVA: 0x00116CEC File Offset: 0x00115CEC
		public override bool onbeforedeactivate()
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate();
		}

		// Token: 0x06019340 RID: 103232 RVA: 0x00116D18 File Offset: 0x00115D18
		public override void onpage()
		{
			if (this.m_onpageDelegate != null)
			{
				this.m_onpageDelegate();
				return;
			}
		}

		// Token: 0x06019341 RID: 103233 RVA: 0x00116D44 File Offset: 0x00115D44
		public override void onlayoutcomplete()
		{
			if (this.m_onlayoutcompleteDelegate != null)
			{
				this.m_onlayoutcompleteDelegate();
				return;
			}
		}

		// Token: 0x06019342 RID: 103234 RVA: 0x00116D70 File Offset: 0x00115D70
		public override void onbeforeeditfocus()
		{
			if (this.m_onbeforeeditfocusDelegate != null)
			{
				this.m_onbeforeeditfocusDelegate();
				return;
			}
		}

		// Token: 0x06019343 RID: 103235 RVA: 0x00116D9C File Offset: 0x00115D9C
		public override void onreadystatechange()
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate();
				return;
			}
		}

		// Token: 0x06019344 RID: 103236 RVA: 0x00116DC8 File Offset: 0x00115DC8
		public override void oncellchange()
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate();
				return;
			}
		}

		// Token: 0x06019345 RID: 103237 RVA: 0x00116DF4 File Offset: 0x00115DF4
		public override void onrowsinserted()
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate();
				return;
			}
		}

		// Token: 0x06019346 RID: 103238 RVA: 0x00116E20 File Offset: 0x00115E20
		public override void onrowsdelete()
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate();
				return;
			}
		}

		// Token: 0x06019347 RID: 103239 RVA: 0x00116E4C File Offset: 0x00115E4C
		public override bool oncontextmenu()
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate();
		}

		// Token: 0x06019348 RID: 103240 RVA: 0x00116E78 File Offset: 0x00115E78
		public override bool onpaste()
		{
			return this.m_onpasteDelegate != null && this.m_onpasteDelegate();
		}

		// Token: 0x06019349 RID: 103241 RVA: 0x00116EA4 File Offset: 0x00115EA4
		public override bool onbeforepaste()
		{
			return this.m_onbeforepasteDelegate != null && this.m_onbeforepasteDelegate();
		}

		// Token: 0x0601934A RID: 103242 RVA: 0x00116ED0 File Offset: 0x00115ED0
		public override bool oncopy()
		{
			return this.m_oncopyDelegate != null && this.m_oncopyDelegate();
		}

		// Token: 0x0601934B RID: 103243 RVA: 0x00116EFC File Offset: 0x00115EFC
		public override bool onbeforecopy()
		{
			return this.m_onbeforecopyDelegate != null && this.m_onbeforecopyDelegate();
		}

		// Token: 0x0601934C RID: 103244 RVA: 0x00116F28 File Offset: 0x00115F28
		public override bool oncut()
		{
			return this.m_oncutDelegate != null && this.m_oncutDelegate();
		}

		// Token: 0x0601934D RID: 103245 RVA: 0x00116F54 File Offset: 0x00115F54
		public override bool onbeforecut()
		{
			return this.m_onbeforecutDelegate != null && this.m_onbeforecutDelegate();
		}

		// Token: 0x0601934E RID: 103246 RVA: 0x00116F80 File Offset: 0x00115F80
		public override bool ondrop()
		{
			return this.m_ondropDelegate != null && this.m_ondropDelegate();
		}

		// Token: 0x0601934F RID: 103247 RVA: 0x00116FAC File Offset: 0x00115FAC
		public override void ondragleave()
		{
			if (this.m_ondragleaveDelegate != null)
			{
				this.m_ondragleaveDelegate();
				return;
			}
		}

		// Token: 0x06019350 RID: 103248 RVA: 0x00116FD8 File Offset: 0x00115FD8
		public override bool ondragover()
		{
			return this.m_ondragoverDelegate != null && this.m_ondragoverDelegate();
		}

		// Token: 0x06019351 RID: 103249 RVA: 0x00117004 File Offset: 0x00116004
		public override bool ondragenter()
		{
			return this.m_ondragenterDelegate != null && this.m_ondragenterDelegate();
		}

		// Token: 0x06019352 RID: 103250 RVA: 0x00117030 File Offset: 0x00116030
		public override void ondragend()
		{
			if (this.m_ondragendDelegate != null)
			{
				this.m_ondragendDelegate();
				return;
			}
		}

		// Token: 0x06019353 RID: 103251 RVA: 0x0011705C File Offset: 0x0011605C
		public override bool ondrag()
		{
			return this.m_ondragDelegate != null && this.m_ondragDelegate();
		}

		// Token: 0x06019354 RID: 103252 RVA: 0x00117088 File Offset: 0x00116088
		public override void onresize()
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate();
				return;
			}
		}

		// Token: 0x06019355 RID: 103253 RVA: 0x001170B4 File Offset: 0x001160B4
		public override void onblur()
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate();
				return;
			}
		}

		// Token: 0x06019356 RID: 103254 RVA: 0x001170E0 File Offset: 0x001160E0
		public override void onfocus()
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate();
				return;
			}
		}

		// Token: 0x06019357 RID: 103255 RVA: 0x0011710C File Offset: 0x0011610C
		public override void onscroll()
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate();
				return;
			}
		}

		// Token: 0x06019358 RID: 103256 RVA: 0x00117138 File Offset: 0x00116138
		public override void onpropertychange()
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate();
				return;
			}
		}

		// Token: 0x06019359 RID: 103257 RVA: 0x00117164 File Offset: 0x00116164
		public override void onlosecapture()
		{
			if (this.m_onlosecaptureDelegate != null)
			{
				this.m_onlosecaptureDelegate();
				return;
			}
		}

		// Token: 0x0601935A RID: 103258 RVA: 0x00117190 File Offset: 0x00116190
		public override void ondatasetcomplete()
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate();
				return;
			}
		}

		// Token: 0x0601935B RID: 103259 RVA: 0x001171BC File Offset: 0x001161BC
		public override void ondataavailable()
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate();
				return;
			}
		}

		// Token: 0x0601935C RID: 103260 RVA: 0x001171E8 File Offset: 0x001161E8
		public override void ondatasetchanged()
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate();
				return;
			}
		}

		// Token: 0x0601935D RID: 103261 RVA: 0x00117214 File Offset: 0x00116214
		public override void onrowenter()
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate();
				return;
			}
		}

		// Token: 0x0601935E RID: 103262 RVA: 0x00117240 File Offset: 0x00116240
		public override bool onrowexit()
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate();
		}

		// Token: 0x0601935F RID: 103263 RVA: 0x0011726C File Offset: 0x0011626C
		public override bool onerrorupdate()
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate();
		}

		// Token: 0x06019360 RID: 103264 RVA: 0x00117298 File Offset: 0x00116298
		public override void onafterupdate()
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate();
				return;
			}
		}

		// Token: 0x06019361 RID: 103265 RVA: 0x001172C4 File Offset: 0x001162C4
		public override bool onbeforeupdate()
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate();
		}

		// Token: 0x06019362 RID: 103266 RVA: 0x001172F0 File Offset: 0x001162F0
		public override bool ondragstart()
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate();
		}

		// Token: 0x06019363 RID: 103267 RVA: 0x0011731C File Offset: 0x0011631C
		public override void onfilterchange()
		{
			if (this.m_onfilterchangeDelegate != null)
			{
				this.m_onfilterchangeDelegate();
				return;
			}
		}

		// Token: 0x06019364 RID: 103268 RVA: 0x00117348 File Offset: 0x00116348
		public override bool onselectstart()
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate();
		}

		// Token: 0x06019365 RID: 103269 RVA: 0x00117374 File Offset: 0x00116374
		public override void onmouseup()
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate();
				return;
			}
		}

		// Token: 0x06019366 RID: 103270 RVA: 0x001173A0 File Offset: 0x001163A0
		public override void onmousedown()
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate();
				return;
			}
		}

		// Token: 0x06019367 RID: 103271 RVA: 0x001173CC File Offset: 0x001163CC
		public override void onmousemove()
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate();
				return;
			}
		}

		// Token: 0x06019368 RID: 103272 RVA: 0x001173F8 File Offset: 0x001163F8
		public override void onmouseover()
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate();
				return;
			}
		}

		// Token: 0x06019369 RID: 103273 RVA: 0x00117424 File Offset: 0x00116424
		public override void onmouseout()
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate();
				return;
			}
		}

		// Token: 0x0601936A RID: 103274 RVA: 0x00117450 File Offset: 0x00116450
		public override void onkeyup()
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate();
				return;
			}
		}

		// Token: 0x0601936B RID: 103275 RVA: 0x0011747C File Offset: 0x0011647C
		public override void onkeydown()
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate();
				return;
			}
		}

		// Token: 0x0601936C RID: 103276 RVA: 0x001174A8 File Offset: 0x001164A8
		public override bool onkeypress()
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate();
		}

		// Token: 0x0601936D RID: 103277 RVA: 0x001174D4 File Offset: 0x001164D4
		public override bool ondblclick()
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate();
		}

		// Token: 0x0601936E RID: 103278 RVA: 0x00117500 File Offset: 0x00116500
		public override bool onclick()
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate();
		}

		// Token: 0x0601936F RID: 103279 RVA: 0x0011752C File Offset: 0x0011652C
		public override bool onhelp()
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate();
		}

		// Token: 0x06019370 RID: 103280 RVA: 0x00117558 File Offset: 0x00116558
		internal HTMLStyleElementEvents_SinkHelper()
		{
			this.m_dwCookie = 0;
			this.m_onerrorDelegate = null;
			this.m_onloadDelegate = null;
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

		// Token: 0x04000F41 RID: 3905
		public HTMLStyleElementEvents_onerrorEventHandler m_onerrorDelegate;

		// Token: 0x04000F42 RID: 3906
		public HTMLStyleElementEvents_onloadEventHandler m_onloadDelegate;

		// Token: 0x04000F43 RID: 3907
		public HTMLStyleElementEvents_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x04000F44 RID: 3908
		public HTMLStyleElementEvents_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x04000F45 RID: 3909
		public HTMLStyleElementEvents_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x04000F46 RID: 3910
		public HTMLStyleElementEvents_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x04000F47 RID: 3911
		public HTMLStyleElementEvents_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x04000F48 RID: 3912
		public HTMLStyleElementEvents_onmouseleaveEventHandler m_onmouseleaveDelegate;

		// Token: 0x04000F49 RID: 3913
		public HTMLStyleElementEvents_onmouseenterEventHandler m_onmouseenterDelegate;

		// Token: 0x04000F4A RID: 3914
		public HTMLStyleElementEvents_onresizeendEventHandler m_onresizeendDelegate;

		// Token: 0x04000F4B RID: 3915
		public HTMLStyleElementEvents_onresizestartEventHandler m_onresizestartDelegate;

		// Token: 0x04000F4C RID: 3916
		public HTMLStyleElementEvents_onmoveendEventHandler m_onmoveendDelegate;

		// Token: 0x04000F4D RID: 3917
		public HTMLStyleElementEvents_onmovestartEventHandler m_onmovestartDelegate;

		// Token: 0x04000F4E RID: 3918
		public HTMLStyleElementEvents_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x04000F4F RID: 3919
		public HTMLStyleElementEvents_onmoveEventHandler m_onmoveDelegate;

		// Token: 0x04000F50 RID: 3920
		public HTMLStyleElementEvents_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x04000F51 RID: 3921
		public HTMLStyleElementEvents_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x04000F52 RID: 3922
		public HTMLStyleElementEvents_onpageEventHandler m_onpageDelegate;

		// Token: 0x04000F53 RID: 3923
		public HTMLStyleElementEvents_onlayoutcompleteEventHandler m_onlayoutcompleteDelegate;

		// Token: 0x04000F54 RID: 3924
		public HTMLStyleElementEvents_onbeforeeditfocusEventHandler m_onbeforeeditfocusDelegate;

		// Token: 0x04000F55 RID: 3925
		public HTMLStyleElementEvents_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x04000F56 RID: 3926
		public HTMLStyleElementEvents_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x04000F57 RID: 3927
		public HTMLStyleElementEvents_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x04000F58 RID: 3928
		public HTMLStyleElementEvents_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x04000F59 RID: 3929
		public HTMLStyleElementEvents_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x04000F5A RID: 3930
		public HTMLStyleElementEvents_onpasteEventHandler m_onpasteDelegate;

		// Token: 0x04000F5B RID: 3931
		public HTMLStyleElementEvents_onbeforepasteEventHandler m_onbeforepasteDelegate;

		// Token: 0x04000F5C RID: 3932
		public HTMLStyleElementEvents_oncopyEventHandler m_oncopyDelegate;

		// Token: 0x04000F5D RID: 3933
		public HTMLStyleElementEvents_onbeforecopyEventHandler m_onbeforecopyDelegate;

		// Token: 0x04000F5E RID: 3934
		public HTMLStyleElementEvents_oncutEventHandler m_oncutDelegate;

		// Token: 0x04000F5F RID: 3935
		public HTMLStyleElementEvents_onbeforecutEventHandler m_onbeforecutDelegate;

		// Token: 0x04000F60 RID: 3936
		public HTMLStyleElementEvents_ondropEventHandler m_ondropDelegate;

		// Token: 0x04000F61 RID: 3937
		public HTMLStyleElementEvents_ondragleaveEventHandler m_ondragleaveDelegate;

		// Token: 0x04000F62 RID: 3938
		public HTMLStyleElementEvents_ondragoverEventHandler m_ondragoverDelegate;

		// Token: 0x04000F63 RID: 3939
		public HTMLStyleElementEvents_ondragenterEventHandler m_ondragenterDelegate;

		// Token: 0x04000F64 RID: 3940
		public HTMLStyleElementEvents_ondragendEventHandler m_ondragendDelegate;

		// Token: 0x04000F65 RID: 3941
		public HTMLStyleElementEvents_ondragEventHandler m_ondragDelegate;

		// Token: 0x04000F66 RID: 3942
		public HTMLStyleElementEvents_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x04000F67 RID: 3943
		public HTMLStyleElementEvents_onblurEventHandler m_onblurDelegate;

		// Token: 0x04000F68 RID: 3944
		public HTMLStyleElementEvents_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x04000F69 RID: 3945
		public HTMLStyleElementEvents_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x04000F6A RID: 3946
		public HTMLStyleElementEvents_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x04000F6B RID: 3947
		public HTMLStyleElementEvents_onlosecaptureEventHandler m_onlosecaptureDelegate;

		// Token: 0x04000F6C RID: 3948
		public HTMLStyleElementEvents_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x04000F6D RID: 3949
		public HTMLStyleElementEvents_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x04000F6E RID: 3950
		public HTMLStyleElementEvents_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x04000F6F RID: 3951
		public HTMLStyleElementEvents_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x04000F70 RID: 3952
		public HTMLStyleElementEvents_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x04000F71 RID: 3953
		public HTMLStyleElementEvents_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x04000F72 RID: 3954
		public HTMLStyleElementEvents_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x04000F73 RID: 3955
		public HTMLStyleElementEvents_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x04000F74 RID: 3956
		public HTMLStyleElementEvents_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x04000F75 RID: 3957
		public HTMLStyleElementEvents_onfilterchangeEventHandler m_onfilterchangeDelegate;

		// Token: 0x04000F76 RID: 3958
		public HTMLStyleElementEvents_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x04000F77 RID: 3959
		public HTMLStyleElementEvents_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x04000F78 RID: 3960
		public HTMLStyleElementEvents_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x04000F79 RID: 3961
		public HTMLStyleElementEvents_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x04000F7A RID: 3962
		public HTMLStyleElementEvents_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x04000F7B RID: 3963
		public HTMLStyleElementEvents_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x04000F7C RID: 3964
		public HTMLStyleElementEvents_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x04000F7D RID: 3965
		public HTMLStyleElementEvents_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x04000F7E RID: 3966
		public HTMLStyleElementEvents_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x04000F7F RID: 3967
		public HTMLStyleElementEvents_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x04000F80 RID: 3968
		public HTMLStyleElementEvents_onclickEventHandler m_onclickDelegate;

		// Token: 0x04000F81 RID: 3969
		public HTMLStyleElementEvents_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x04000F82 RID: 3970
		public int m_dwCookie;
	}
}
