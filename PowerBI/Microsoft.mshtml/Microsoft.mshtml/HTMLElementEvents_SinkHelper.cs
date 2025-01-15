using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000E0C RID: 3596
	[ClassInterface(0)]
	internal sealed class HTMLElementEvents_SinkHelper : HTMLElementEvents
	{
		// Token: 0x06018FA7 RID: 102311 RVA: 0x000F6840 File Offset: 0x000F5840
		public override void onfocusout()
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate();
				return;
			}
		}

		// Token: 0x06018FA8 RID: 102312 RVA: 0x000F686C File Offset: 0x000F586C
		public override void onfocusin()
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate();
				return;
			}
		}

		// Token: 0x06018FA9 RID: 102313 RVA: 0x000F6898 File Offset: 0x000F5898
		public override void ondeactivate()
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate();
				return;
			}
		}

		// Token: 0x06018FAA RID: 102314 RVA: 0x000F68C4 File Offset: 0x000F58C4
		public override void onactivate()
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate();
				return;
			}
		}

		// Token: 0x06018FAB RID: 102315 RVA: 0x000F68F0 File Offset: 0x000F58F0
		public override bool onmousewheel()
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate();
		}

		// Token: 0x06018FAC RID: 102316 RVA: 0x000F691C File Offset: 0x000F591C
		public override void onmouseleave()
		{
			if (this.m_onmouseleaveDelegate != null)
			{
				this.m_onmouseleaveDelegate();
				return;
			}
		}

		// Token: 0x06018FAD RID: 102317 RVA: 0x000F6948 File Offset: 0x000F5948
		public override void onmouseenter()
		{
			if (this.m_onmouseenterDelegate != null)
			{
				this.m_onmouseenterDelegate();
				return;
			}
		}

		// Token: 0x06018FAE RID: 102318 RVA: 0x000F6974 File Offset: 0x000F5974
		public override void onresizeend()
		{
			if (this.m_onresizeendDelegate != null)
			{
				this.m_onresizeendDelegate();
				return;
			}
		}

		// Token: 0x06018FAF RID: 102319 RVA: 0x000F69A0 File Offset: 0x000F59A0
		public override bool onresizestart()
		{
			return this.m_onresizestartDelegate != null && this.m_onresizestartDelegate();
		}

		// Token: 0x06018FB0 RID: 102320 RVA: 0x000F69CC File Offset: 0x000F59CC
		public override void onmoveend()
		{
			if (this.m_onmoveendDelegate != null)
			{
				this.m_onmoveendDelegate();
				return;
			}
		}

		// Token: 0x06018FB1 RID: 102321 RVA: 0x000F69F8 File Offset: 0x000F59F8
		public override bool onmovestart()
		{
			return this.m_onmovestartDelegate != null && this.m_onmovestartDelegate();
		}

		// Token: 0x06018FB2 RID: 102322 RVA: 0x000F6A24 File Offset: 0x000F5A24
		public override bool oncontrolselect()
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate();
		}

		// Token: 0x06018FB3 RID: 102323 RVA: 0x000F6A50 File Offset: 0x000F5A50
		public override void onmove()
		{
			if (this.m_onmoveDelegate != null)
			{
				this.m_onmoveDelegate();
				return;
			}
		}

		// Token: 0x06018FB4 RID: 102324 RVA: 0x000F6A7C File Offset: 0x000F5A7C
		public override bool onbeforeactivate()
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate();
		}

		// Token: 0x06018FB5 RID: 102325 RVA: 0x000F6AA8 File Offset: 0x000F5AA8
		public override bool onbeforedeactivate()
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate();
		}

		// Token: 0x06018FB6 RID: 102326 RVA: 0x000F6AD4 File Offset: 0x000F5AD4
		public override void onpage()
		{
			if (this.m_onpageDelegate != null)
			{
				this.m_onpageDelegate();
				return;
			}
		}

		// Token: 0x06018FB7 RID: 102327 RVA: 0x000F6B00 File Offset: 0x000F5B00
		public override void onlayoutcomplete()
		{
			if (this.m_onlayoutcompleteDelegate != null)
			{
				this.m_onlayoutcompleteDelegate();
				return;
			}
		}

		// Token: 0x06018FB8 RID: 102328 RVA: 0x000F6B2C File Offset: 0x000F5B2C
		public override void onbeforeeditfocus()
		{
			if (this.m_onbeforeeditfocusDelegate != null)
			{
				this.m_onbeforeeditfocusDelegate();
				return;
			}
		}

		// Token: 0x06018FB9 RID: 102329 RVA: 0x000F6B58 File Offset: 0x000F5B58
		public override void onreadystatechange()
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate();
				return;
			}
		}

		// Token: 0x06018FBA RID: 102330 RVA: 0x000F6B84 File Offset: 0x000F5B84
		public override void oncellchange()
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate();
				return;
			}
		}

		// Token: 0x06018FBB RID: 102331 RVA: 0x000F6BB0 File Offset: 0x000F5BB0
		public override void onrowsinserted()
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate();
				return;
			}
		}

		// Token: 0x06018FBC RID: 102332 RVA: 0x000F6BDC File Offset: 0x000F5BDC
		public override void onrowsdelete()
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate();
				return;
			}
		}

		// Token: 0x06018FBD RID: 102333 RVA: 0x000F6C08 File Offset: 0x000F5C08
		public override bool oncontextmenu()
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate();
		}

		// Token: 0x06018FBE RID: 102334 RVA: 0x000F6C34 File Offset: 0x000F5C34
		public override bool onpaste()
		{
			return this.m_onpasteDelegate != null && this.m_onpasteDelegate();
		}

		// Token: 0x06018FBF RID: 102335 RVA: 0x000F6C60 File Offset: 0x000F5C60
		public override bool onbeforepaste()
		{
			return this.m_onbeforepasteDelegate != null && this.m_onbeforepasteDelegate();
		}

		// Token: 0x06018FC0 RID: 102336 RVA: 0x000F6C8C File Offset: 0x000F5C8C
		public override bool oncopy()
		{
			return this.m_oncopyDelegate != null && this.m_oncopyDelegate();
		}

		// Token: 0x06018FC1 RID: 102337 RVA: 0x000F6CB8 File Offset: 0x000F5CB8
		public override bool onbeforecopy()
		{
			return this.m_onbeforecopyDelegate != null && this.m_onbeforecopyDelegate();
		}

		// Token: 0x06018FC2 RID: 102338 RVA: 0x000F6CE4 File Offset: 0x000F5CE4
		public override bool oncut()
		{
			return this.m_oncutDelegate != null && this.m_oncutDelegate();
		}

		// Token: 0x06018FC3 RID: 102339 RVA: 0x000F6D10 File Offset: 0x000F5D10
		public override bool onbeforecut()
		{
			return this.m_onbeforecutDelegate != null && this.m_onbeforecutDelegate();
		}

		// Token: 0x06018FC4 RID: 102340 RVA: 0x000F6D3C File Offset: 0x000F5D3C
		public override bool ondrop()
		{
			return this.m_ondropDelegate != null && this.m_ondropDelegate();
		}

		// Token: 0x06018FC5 RID: 102341 RVA: 0x000F6D68 File Offset: 0x000F5D68
		public override void ondragleave()
		{
			if (this.m_ondragleaveDelegate != null)
			{
				this.m_ondragleaveDelegate();
				return;
			}
		}

		// Token: 0x06018FC6 RID: 102342 RVA: 0x000F6D94 File Offset: 0x000F5D94
		public override bool ondragover()
		{
			return this.m_ondragoverDelegate != null && this.m_ondragoverDelegate();
		}

		// Token: 0x06018FC7 RID: 102343 RVA: 0x000F6DC0 File Offset: 0x000F5DC0
		public override bool ondragenter()
		{
			return this.m_ondragenterDelegate != null && this.m_ondragenterDelegate();
		}

		// Token: 0x06018FC8 RID: 102344 RVA: 0x000F6DEC File Offset: 0x000F5DEC
		public override void ondragend()
		{
			if (this.m_ondragendDelegate != null)
			{
				this.m_ondragendDelegate();
				return;
			}
		}

		// Token: 0x06018FC9 RID: 102345 RVA: 0x000F6E18 File Offset: 0x000F5E18
		public override bool ondrag()
		{
			return this.m_ondragDelegate != null && this.m_ondragDelegate();
		}

		// Token: 0x06018FCA RID: 102346 RVA: 0x000F6E44 File Offset: 0x000F5E44
		public override void onresize()
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate();
				return;
			}
		}

		// Token: 0x06018FCB RID: 102347 RVA: 0x000F6E70 File Offset: 0x000F5E70
		public override void onblur()
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate();
				return;
			}
		}

		// Token: 0x06018FCC RID: 102348 RVA: 0x000F6E9C File Offset: 0x000F5E9C
		public override void onfocus()
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate();
				return;
			}
		}

		// Token: 0x06018FCD RID: 102349 RVA: 0x000F6EC8 File Offset: 0x000F5EC8
		public override void onscroll()
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate();
				return;
			}
		}

		// Token: 0x06018FCE RID: 102350 RVA: 0x000F6EF4 File Offset: 0x000F5EF4
		public override void onpropertychange()
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate();
				return;
			}
		}

		// Token: 0x06018FCF RID: 102351 RVA: 0x000F6F20 File Offset: 0x000F5F20
		public override void onlosecapture()
		{
			if (this.m_onlosecaptureDelegate != null)
			{
				this.m_onlosecaptureDelegate();
				return;
			}
		}

		// Token: 0x06018FD0 RID: 102352 RVA: 0x000F6F4C File Offset: 0x000F5F4C
		public override void ondatasetcomplete()
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate();
				return;
			}
		}

		// Token: 0x06018FD1 RID: 102353 RVA: 0x000F6F78 File Offset: 0x000F5F78
		public override void ondataavailable()
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate();
				return;
			}
		}

		// Token: 0x06018FD2 RID: 102354 RVA: 0x000F6FA4 File Offset: 0x000F5FA4
		public override void ondatasetchanged()
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate();
				return;
			}
		}

		// Token: 0x06018FD3 RID: 102355 RVA: 0x000F6FD0 File Offset: 0x000F5FD0
		public override void onrowenter()
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate();
				return;
			}
		}

		// Token: 0x06018FD4 RID: 102356 RVA: 0x000F6FFC File Offset: 0x000F5FFC
		public override bool onrowexit()
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate();
		}

		// Token: 0x06018FD5 RID: 102357 RVA: 0x000F7028 File Offset: 0x000F6028
		public override bool onerrorupdate()
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate();
		}

		// Token: 0x06018FD6 RID: 102358 RVA: 0x000F7054 File Offset: 0x000F6054
		public override void onafterupdate()
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate();
				return;
			}
		}

		// Token: 0x06018FD7 RID: 102359 RVA: 0x000F7080 File Offset: 0x000F6080
		public override bool onbeforeupdate()
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate();
		}

		// Token: 0x06018FD8 RID: 102360 RVA: 0x000F70AC File Offset: 0x000F60AC
		public override bool ondragstart()
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate();
		}

		// Token: 0x06018FD9 RID: 102361 RVA: 0x000F70D8 File Offset: 0x000F60D8
		public override void onfilterchange()
		{
			if (this.m_onfilterchangeDelegate != null)
			{
				this.m_onfilterchangeDelegate();
				return;
			}
		}

		// Token: 0x06018FDA RID: 102362 RVA: 0x000F7104 File Offset: 0x000F6104
		public override bool onselectstart()
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate();
		}

		// Token: 0x06018FDB RID: 102363 RVA: 0x000F7130 File Offset: 0x000F6130
		public override void onmouseup()
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate();
				return;
			}
		}

		// Token: 0x06018FDC RID: 102364 RVA: 0x000F715C File Offset: 0x000F615C
		public override void onmousedown()
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate();
				return;
			}
		}

		// Token: 0x06018FDD RID: 102365 RVA: 0x000F7188 File Offset: 0x000F6188
		public override void onmousemove()
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate();
				return;
			}
		}

		// Token: 0x06018FDE RID: 102366 RVA: 0x000F71B4 File Offset: 0x000F61B4
		public override void onmouseover()
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate();
				return;
			}
		}

		// Token: 0x06018FDF RID: 102367 RVA: 0x000F71E0 File Offset: 0x000F61E0
		public override void onmouseout()
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate();
				return;
			}
		}

		// Token: 0x06018FE0 RID: 102368 RVA: 0x000F720C File Offset: 0x000F620C
		public override void onkeyup()
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate();
				return;
			}
		}

		// Token: 0x06018FE1 RID: 102369 RVA: 0x000F7238 File Offset: 0x000F6238
		public override void onkeydown()
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate();
				return;
			}
		}

		// Token: 0x06018FE2 RID: 102370 RVA: 0x000F7264 File Offset: 0x000F6264
		public override bool onkeypress()
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate();
		}

		// Token: 0x06018FE3 RID: 102371 RVA: 0x000F7290 File Offset: 0x000F6290
		public override bool ondblclick()
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate();
		}

		// Token: 0x06018FE4 RID: 102372 RVA: 0x000F72BC File Offset: 0x000F62BC
		public override bool onclick()
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate();
		}

		// Token: 0x06018FE5 RID: 102373 RVA: 0x000F72E8 File Offset: 0x000F62E8
		public override bool onhelp()
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate();
		}

		// Token: 0x06018FE6 RID: 102374 RVA: 0x000F7314 File Offset: 0x000F6314
		internal HTMLElementEvents_SinkHelper()
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

		// Token: 0x04000E08 RID: 3592
		public HTMLElementEvents_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x04000E09 RID: 3593
		public HTMLElementEvents_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x04000E0A RID: 3594
		public HTMLElementEvents_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x04000E0B RID: 3595
		public HTMLElementEvents_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x04000E0C RID: 3596
		public HTMLElementEvents_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x04000E0D RID: 3597
		public HTMLElementEvents_onmouseleaveEventHandler m_onmouseleaveDelegate;

		// Token: 0x04000E0E RID: 3598
		public HTMLElementEvents_onmouseenterEventHandler m_onmouseenterDelegate;

		// Token: 0x04000E0F RID: 3599
		public HTMLElementEvents_onresizeendEventHandler m_onresizeendDelegate;

		// Token: 0x04000E10 RID: 3600
		public HTMLElementEvents_onresizestartEventHandler m_onresizestartDelegate;

		// Token: 0x04000E11 RID: 3601
		public HTMLElementEvents_onmoveendEventHandler m_onmoveendDelegate;

		// Token: 0x04000E12 RID: 3602
		public HTMLElementEvents_onmovestartEventHandler m_onmovestartDelegate;

		// Token: 0x04000E13 RID: 3603
		public HTMLElementEvents_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x04000E14 RID: 3604
		public HTMLElementEvents_onmoveEventHandler m_onmoveDelegate;

		// Token: 0x04000E15 RID: 3605
		public HTMLElementEvents_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x04000E16 RID: 3606
		public HTMLElementEvents_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x04000E17 RID: 3607
		public HTMLElementEvents_onpageEventHandler m_onpageDelegate;

		// Token: 0x04000E18 RID: 3608
		public HTMLElementEvents_onlayoutcompleteEventHandler m_onlayoutcompleteDelegate;

		// Token: 0x04000E19 RID: 3609
		public HTMLElementEvents_onbeforeeditfocusEventHandler m_onbeforeeditfocusDelegate;

		// Token: 0x04000E1A RID: 3610
		public HTMLElementEvents_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x04000E1B RID: 3611
		public HTMLElementEvents_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x04000E1C RID: 3612
		public HTMLElementEvents_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x04000E1D RID: 3613
		public HTMLElementEvents_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x04000E1E RID: 3614
		public HTMLElementEvents_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x04000E1F RID: 3615
		public HTMLElementEvents_onpasteEventHandler m_onpasteDelegate;

		// Token: 0x04000E20 RID: 3616
		public HTMLElementEvents_onbeforepasteEventHandler m_onbeforepasteDelegate;

		// Token: 0x04000E21 RID: 3617
		public HTMLElementEvents_oncopyEventHandler m_oncopyDelegate;

		// Token: 0x04000E22 RID: 3618
		public HTMLElementEvents_onbeforecopyEventHandler m_onbeforecopyDelegate;

		// Token: 0x04000E23 RID: 3619
		public HTMLElementEvents_oncutEventHandler m_oncutDelegate;

		// Token: 0x04000E24 RID: 3620
		public HTMLElementEvents_onbeforecutEventHandler m_onbeforecutDelegate;

		// Token: 0x04000E25 RID: 3621
		public HTMLElementEvents_ondropEventHandler m_ondropDelegate;

		// Token: 0x04000E26 RID: 3622
		public HTMLElementEvents_ondragleaveEventHandler m_ondragleaveDelegate;

		// Token: 0x04000E27 RID: 3623
		public HTMLElementEvents_ondragoverEventHandler m_ondragoverDelegate;

		// Token: 0x04000E28 RID: 3624
		public HTMLElementEvents_ondragenterEventHandler m_ondragenterDelegate;

		// Token: 0x04000E29 RID: 3625
		public HTMLElementEvents_ondragendEventHandler m_ondragendDelegate;

		// Token: 0x04000E2A RID: 3626
		public HTMLElementEvents_ondragEventHandler m_ondragDelegate;

		// Token: 0x04000E2B RID: 3627
		public HTMLElementEvents_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x04000E2C RID: 3628
		public HTMLElementEvents_onblurEventHandler m_onblurDelegate;

		// Token: 0x04000E2D RID: 3629
		public HTMLElementEvents_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x04000E2E RID: 3630
		public HTMLElementEvents_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x04000E2F RID: 3631
		public HTMLElementEvents_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x04000E30 RID: 3632
		public HTMLElementEvents_onlosecaptureEventHandler m_onlosecaptureDelegate;

		// Token: 0x04000E31 RID: 3633
		public HTMLElementEvents_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x04000E32 RID: 3634
		public HTMLElementEvents_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x04000E33 RID: 3635
		public HTMLElementEvents_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x04000E34 RID: 3636
		public HTMLElementEvents_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x04000E35 RID: 3637
		public HTMLElementEvents_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x04000E36 RID: 3638
		public HTMLElementEvents_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x04000E37 RID: 3639
		public HTMLElementEvents_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x04000E38 RID: 3640
		public HTMLElementEvents_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x04000E39 RID: 3641
		public HTMLElementEvents_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x04000E3A RID: 3642
		public HTMLElementEvents_onfilterchangeEventHandler m_onfilterchangeDelegate;

		// Token: 0x04000E3B RID: 3643
		public HTMLElementEvents_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x04000E3C RID: 3644
		public HTMLElementEvents_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x04000E3D RID: 3645
		public HTMLElementEvents_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x04000E3E RID: 3646
		public HTMLElementEvents_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x04000E3F RID: 3647
		public HTMLElementEvents_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x04000E40 RID: 3648
		public HTMLElementEvents_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x04000E41 RID: 3649
		public HTMLElementEvents_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x04000E42 RID: 3650
		public HTMLElementEvents_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x04000E43 RID: 3651
		public HTMLElementEvents_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x04000E44 RID: 3652
		public HTMLElementEvents_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x04000E45 RID: 3653
		public HTMLElementEvents_onclickEventHandler m_onclickDelegate;

		// Token: 0x04000E46 RID: 3654
		public HTMLElementEvents_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x04000E47 RID: 3655
		public int m_dwCookie;
	}
}
