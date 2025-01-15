using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000E18 RID: 3608
	[ClassInterface(0)]
	internal sealed class HTMLDocumentEvents_SinkHelper : HTMLDocumentEvents
	{
		// Token: 0x060193F7 RID: 103415 RVA: 0x0011DB98 File Offset: 0x0011CB98
		public override bool onbeforedeactivate()
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate();
		}

		// Token: 0x060193F8 RID: 103416 RVA: 0x0011DBC4 File Offset: 0x0011CBC4
		public override bool onbeforeactivate()
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate();
		}

		// Token: 0x060193F9 RID: 103417 RVA: 0x0011DBF0 File Offset: 0x0011CBF0
		public override void ondeactivate()
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate();
				return;
			}
		}

		// Token: 0x060193FA RID: 103418 RVA: 0x0011DC1C File Offset: 0x0011CC1C
		public override void onactivate()
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate();
				return;
			}
		}

		// Token: 0x060193FB RID: 103419 RVA: 0x0011DC48 File Offset: 0x0011CC48
		public override void onfocusout()
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate();
				return;
			}
		}

		// Token: 0x060193FC RID: 103420 RVA: 0x0011DC74 File Offset: 0x0011CC74
		public override void onfocusin()
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate();
				return;
			}
		}

		// Token: 0x060193FD RID: 103421 RVA: 0x0011DCA0 File Offset: 0x0011CCA0
		public override bool onmousewheel()
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate();
		}

		// Token: 0x060193FE RID: 103422 RVA: 0x0011DCCC File Offset: 0x0011CCCC
		public override bool oncontrolselect()
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate();
		}

		// Token: 0x060193FF RID: 103423 RVA: 0x0011DCF8 File Offset: 0x0011CCF8
		public override void onselectionchange()
		{
			if (this.m_onselectionchangeDelegate != null)
			{
				this.m_onselectionchangeDelegate();
				return;
			}
		}

		// Token: 0x06019400 RID: 103424 RVA: 0x0011DD24 File Offset: 0x0011CD24
		public override void onbeforeeditfocus()
		{
			if (this.m_onbeforeeditfocusDelegate != null)
			{
				this.m_onbeforeeditfocusDelegate();
				return;
			}
		}

		// Token: 0x06019401 RID: 103425 RVA: 0x0011DD50 File Offset: 0x0011CD50
		public override void ondatasetcomplete()
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate();
				return;
			}
		}

		// Token: 0x06019402 RID: 103426 RVA: 0x0011DD7C File Offset: 0x0011CD7C
		public override void ondataavailable()
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate();
				return;
			}
		}

		// Token: 0x06019403 RID: 103427 RVA: 0x0011DDA8 File Offset: 0x0011CDA8
		public override void ondatasetchanged()
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate();
				return;
			}
		}

		// Token: 0x06019404 RID: 103428 RVA: 0x0011DDD4 File Offset: 0x0011CDD4
		public override void onpropertychange()
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate();
				return;
			}
		}

		// Token: 0x06019405 RID: 103429 RVA: 0x0011DE00 File Offset: 0x0011CE00
		public override void oncellchange()
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate();
				return;
			}
		}

		// Token: 0x06019406 RID: 103430 RVA: 0x0011DE2C File Offset: 0x0011CE2C
		public override void onrowsinserted()
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate();
				return;
			}
		}

		// Token: 0x06019407 RID: 103431 RVA: 0x0011DE58 File Offset: 0x0011CE58
		public override void onrowsdelete()
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate();
				return;
			}
		}

		// Token: 0x06019408 RID: 103432 RVA: 0x0011DE84 File Offset: 0x0011CE84
		public override bool onstop()
		{
			return this.m_onstopDelegate != null && this.m_onstopDelegate();
		}

		// Token: 0x06019409 RID: 103433 RVA: 0x0011DEB0 File Offset: 0x0011CEB0
		public override bool oncontextmenu()
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate();
		}

		// Token: 0x0601940A RID: 103434 RVA: 0x0011DEDC File Offset: 0x0011CEDC
		public override bool onerrorupdate()
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate();
		}

		// Token: 0x0601940B RID: 103435 RVA: 0x0011DF08 File Offset: 0x0011CF08
		public override bool onselectstart()
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate();
		}

		// Token: 0x0601940C RID: 103436 RVA: 0x0011DF34 File Offset: 0x0011CF34
		public override bool ondragstart()
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate();
		}

		// Token: 0x0601940D RID: 103437 RVA: 0x0011DF60 File Offset: 0x0011CF60
		public override void onrowenter()
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate();
				return;
			}
		}

		// Token: 0x0601940E RID: 103438 RVA: 0x0011DF8C File Offset: 0x0011CF8C
		public override bool onrowexit()
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate();
		}

		// Token: 0x0601940F RID: 103439 RVA: 0x0011DFB8 File Offset: 0x0011CFB8
		public override void onafterupdate()
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate();
				return;
			}
		}

		// Token: 0x06019410 RID: 103440 RVA: 0x0011DFE4 File Offset: 0x0011CFE4
		public override bool onbeforeupdate()
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate();
		}

		// Token: 0x06019411 RID: 103441 RVA: 0x0011E010 File Offset: 0x0011D010
		public override void onreadystatechange()
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate();
				return;
			}
		}

		// Token: 0x06019412 RID: 103442 RVA: 0x0011E03C File Offset: 0x0011D03C
		public override void onmouseover()
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate();
				return;
			}
		}

		// Token: 0x06019413 RID: 103443 RVA: 0x0011E068 File Offset: 0x0011D068
		public override void onmouseout()
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate();
				return;
			}
		}

		// Token: 0x06019414 RID: 103444 RVA: 0x0011E094 File Offset: 0x0011D094
		public override void onmouseup()
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate();
				return;
			}
		}

		// Token: 0x06019415 RID: 103445 RVA: 0x0011E0C0 File Offset: 0x0011D0C0
		public override void onmousemove()
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate();
				return;
			}
		}

		// Token: 0x06019416 RID: 103446 RVA: 0x0011E0EC File Offset: 0x0011D0EC
		public override void onmousedown()
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate();
				return;
			}
		}

		// Token: 0x06019417 RID: 103447 RVA: 0x0011E118 File Offset: 0x0011D118
		public override bool onkeypress()
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate();
		}

		// Token: 0x06019418 RID: 103448 RVA: 0x0011E144 File Offset: 0x0011D144
		public override void onkeyup()
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate();
				return;
			}
		}

		// Token: 0x06019419 RID: 103449 RVA: 0x0011E170 File Offset: 0x0011D170
		public override void onkeydown()
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate();
				return;
			}
		}

		// Token: 0x0601941A RID: 103450 RVA: 0x0011E19C File Offset: 0x0011D19C
		public override bool ondblclick()
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate();
		}

		// Token: 0x0601941B RID: 103451 RVA: 0x0011E1C8 File Offset: 0x0011D1C8
		public override bool onclick()
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate();
		}

		// Token: 0x0601941C RID: 103452 RVA: 0x0011E1F4 File Offset: 0x0011D1F4
		public override bool onhelp()
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate();
		}

		// Token: 0x0601941D RID: 103453 RVA: 0x0011E220 File Offset: 0x0011D220
		internal HTMLDocumentEvents_SinkHelper()
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

		// Token: 0x04000F86 RID: 3974
		public HTMLDocumentEvents_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x04000F87 RID: 3975
		public HTMLDocumentEvents_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x04000F88 RID: 3976
		public HTMLDocumentEvents_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x04000F89 RID: 3977
		public HTMLDocumentEvents_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x04000F8A RID: 3978
		public HTMLDocumentEvents_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x04000F8B RID: 3979
		public HTMLDocumentEvents_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x04000F8C RID: 3980
		public HTMLDocumentEvents_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x04000F8D RID: 3981
		public HTMLDocumentEvents_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x04000F8E RID: 3982
		public HTMLDocumentEvents_onselectionchangeEventHandler m_onselectionchangeDelegate;

		// Token: 0x04000F8F RID: 3983
		public HTMLDocumentEvents_onbeforeeditfocusEventHandler m_onbeforeeditfocusDelegate;

		// Token: 0x04000F90 RID: 3984
		public HTMLDocumentEvents_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x04000F91 RID: 3985
		public HTMLDocumentEvents_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x04000F92 RID: 3986
		public HTMLDocumentEvents_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x04000F93 RID: 3987
		public HTMLDocumentEvents_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x04000F94 RID: 3988
		public HTMLDocumentEvents_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x04000F95 RID: 3989
		public HTMLDocumentEvents_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x04000F96 RID: 3990
		public HTMLDocumentEvents_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x04000F97 RID: 3991
		public HTMLDocumentEvents_onstopEventHandler m_onstopDelegate;

		// Token: 0x04000F98 RID: 3992
		public HTMLDocumentEvents_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x04000F99 RID: 3993
		public HTMLDocumentEvents_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x04000F9A RID: 3994
		public HTMLDocumentEvents_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x04000F9B RID: 3995
		public HTMLDocumentEvents_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x04000F9C RID: 3996
		public HTMLDocumentEvents_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x04000F9D RID: 3997
		public HTMLDocumentEvents_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x04000F9E RID: 3998
		public HTMLDocumentEvents_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x04000F9F RID: 3999
		public HTMLDocumentEvents_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x04000FA0 RID: 4000
		public HTMLDocumentEvents_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x04000FA1 RID: 4001
		public HTMLDocumentEvents_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x04000FA2 RID: 4002
		public HTMLDocumentEvents_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x04000FA3 RID: 4003
		public HTMLDocumentEvents_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x04000FA4 RID: 4004
		public HTMLDocumentEvents_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x04000FA5 RID: 4005
		public HTMLDocumentEvents_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x04000FA6 RID: 4006
		public HTMLDocumentEvents_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x04000FA7 RID: 4007
		public HTMLDocumentEvents_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x04000FA8 RID: 4008
		public HTMLDocumentEvents_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x04000FA9 RID: 4009
		public HTMLDocumentEvents_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x04000FAA RID: 4010
		public HTMLDocumentEvents_onclickEventHandler m_onclickDelegate;

		// Token: 0x04000FAB RID: 4011
		public HTMLDocumentEvents_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x04000FAC RID: 4012
		public int m_dwCookie;
	}
}
