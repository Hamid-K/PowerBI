using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DFE RID: 3582
	[ClassInterface(0)]
	internal sealed class HTMLButtonElementEvents_SinkHelper : HTMLButtonElementEvents
	{
		// Token: 0x06018A26 RID: 100902 RVA: 0x000C4628 File Offset: 0x000C3628
		public override void onfocusout()
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate();
				return;
			}
		}

		// Token: 0x06018A27 RID: 100903 RVA: 0x000C4654 File Offset: 0x000C3654
		public override void onfocusin()
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate();
				return;
			}
		}

		// Token: 0x06018A28 RID: 100904 RVA: 0x000C4680 File Offset: 0x000C3680
		public override void ondeactivate()
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate();
				return;
			}
		}

		// Token: 0x06018A29 RID: 100905 RVA: 0x000C46AC File Offset: 0x000C36AC
		public override void onactivate()
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate();
				return;
			}
		}

		// Token: 0x06018A2A RID: 100906 RVA: 0x000C46D8 File Offset: 0x000C36D8
		public override bool onmousewheel()
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate();
		}

		// Token: 0x06018A2B RID: 100907 RVA: 0x000C4704 File Offset: 0x000C3704
		public override void onmouseleave()
		{
			if (this.m_onmouseleaveDelegate != null)
			{
				this.m_onmouseleaveDelegate();
				return;
			}
		}

		// Token: 0x06018A2C RID: 100908 RVA: 0x000C4730 File Offset: 0x000C3730
		public override void onmouseenter()
		{
			if (this.m_onmouseenterDelegate != null)
			{
				this.m_onmouseenterDelegate();
				return;
			}
		}

		// Token: 0x06018A2D RID: 100909 RVA: 0x000C475C File Offset: 0x000C375C
		public override void onresizeend()
		{
			if (this.m_onresizeendDelegate != null)
			{
				this.m_onresizeendDelegate();
				return;
			}
		}

		// Token: 0x06018A2E RID: 100910 RVA: 0x000C4788 File Offset: 0x000C3788
		public override bool onresizestart()
		{
			return this.m_onresizestartDelegate != null && this.m_onresizestartDelegate();
		}

		// Token: 0x06018A2F RID: 100911 RVA: 0x000C47B4 File Offset: 0x000C37B4
		public override void onmoveend()
		{
			if (this.m_onmoveendDelegate != null)
			{
				this.m_onmoveendDelegate();
				return;
			}
		}

		// Token: 0x06018A30 RID: 100912 RVA: 0x000C47E0 File Offset: 0x000C37E0
		public override bool onmovestart()
		{
			return this.m_onmovestartDelegate != null && this.m_onmovestartDelegate();
		}

		// Token: 0x06018A31 RID: 100913 RVA: 0x000C480C File Offset: 0x000C380C
		public override bool oncontrolselect()
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate();
		}

		// Token: 0x06018A32 RID: 100914 RVA: 0x000C4838 File Offset: 0x000C3838
		public override void onmove()
		{
			if (this.m_onmoveDelegate != null)
			{
				this.m_onmoveDelegate();
				return;
			}
		}

		// Token: 0x06018A33 RID: 100915 RVA: 0x000C4864 File Offset: 0x000C3864
		public override bool onbeforeactivate()
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate();
		}

		// Token: 0x06018A34 RID: 100916 RVA: 0x000C4890 File Offset: 0x000C3890
		public override bool onbeforedeactivate()
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate();
		}

		// Token: 0x06018A35 RID: 100917 RVA: 0x000C48BC File Offset: 0x000C38BC
		public override void onpage()
		{
			if (this.m_onpageDelegate != null)
			{
				this.m_onpageDelegate();
				return;
			}
		}

		// Token: 0x06018A36 RID: 100918 RVA: 0x000C48E8 File Offset: 0x000C38E8
		public override void onlayoutcomplete()
		{
			if (this.m_onlayoutcompleteDelegate != null)
			{
				this.m_onlayoutcompleteDelegate();
				return;
			}
		}

		// Token: 0x06018A37 RID: 100919 RVA: 0x000C4914 File Offset: 0x000C3914
		public override void onbeforeeditfocus()
		{
			if (this.m_onbeforeeditfocusDelegate != null)
			{
				this.m_onbeforeeditfocusDelegate();
				return;
			}
		}

		// Token: 0x06018A38 RID: 100920 RVA: 0x000C4940 File Offset: 0x000C3940
		public override void onreadystatechange()
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate();
				return;
			}
		}

		// Token: 0x06018A39 RID: 100921 RVA: 0x000C496C File Offset: 0x000C396C
		public override void oncellchange()
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate();
				return;
			}
		}

		// Token: 0x06018A3A RID: 100922 RVA: 0x000C4998 File Offset: 0x000C3998
		public override void onrowsinserted()
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate();
				return;
			}
		}

		// Token: 0x06018A3B RID: 100923 RVA: 0x000C49C4 File Offset: 0x000C39C4
		public override void onrowsdelete()
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate();
				return;
			}
		}

		// Token: 0x06018A3C RID: 100924 RVA: 0x000C49F0 File Offset: 0x000C39F0
		public override bool oncontextmenu()
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate();
		}

		// Token: 0x06018A3D RID: 100925 RVA: 0x000C4A1C File Offset: 0x000C3A1C
		public override bool onpaste()
		{
			return this.m_onpasteDelegate != null && this.m_onpasteDelegate();
		}

		// Token: 0x06018A3E RID: 100926 RVA: 0x000C4A48 File Offset: 0x000C3A48
		public override bool onbeforepaste()
		{
			return this.m_onbeforepasteDelegate != null && this.m_onbeforepasteDelegate();
		}

		// Token: 0x06018A3F RID: 100927 RVA: 0x000C4A74 File Offset: 0x000C3A74
		public override bool oncopy()
		{
			return this.m_oncopyDelegate != null && this.m_oncopyDelegate();
		}

		// Token: 0x06018A40 RID: 100928 RVA: 0x000C4AA0 File Offset: 0x000C3AA0
		public override bool onbeforecopy()
		{
			return this.m_onbeforecopyDelegate != null && this.m_onbeforecopyDelegate();
		}

		// Token: 0x06018A41 RID: 100929 RVA: 0x000C4ACC File Offset: 0x000C3ACC
		public override bool oncut()
		{
			return this.m_oncutDelegate != null && this.m_oncutDelegate();
		}

		// Token: 0x06018A42 RID: 100930 RVA: 0x000C4AF8 File Offset: 0x000C3AF8
		public override bool onbeforecut()
		{
			return this.m_onbeforecutDelegate != null && this.m_onbeforecutDelegate();
		}

		// Token: 0x06018A43 RID: 100931 RVA: 0x000C4B24 File Offset: 0x000C3B24
		public override bool ondrop()
		{
			return this.m_ondropDelegate != null && this.m_ondropDelegate();
		}

		// Token: 0x06018A44 RID: 100932 RVA: 0x000C4B50 File Offset: 0x000C3B50
		public override void ondragleave()
		{
			if (this.m_ondragleaveDelegate != null)
			{
				this.m_ondragleaveDelegate();
				return;
			}
		}

		// Token: 0x06018A45 RID: 100933 RVA: 0x000C4B7C File Offset: 0x000C3B7C
		public override bool ondragover()
		{
			return this.m_ondragoverDelegate != null && this.m_ondragoverDelegate();
		}

		// Token: 0x06018A46 RID: 100934 RVA: 0x000C4BA8 File Offset: 0x000C3BA8
		public override bool ondragenter()
		{
			return this.m_ondragenterDelegate != null && this.m_ondragenterDelegate();
		}

		// Token: 0x06018A47 RID: 100935 RVA: 0x000C4BD4 File Offset: 0x000C3BD4
		public override void ondragend()
		{
			if (this.m_ondragendDelegate != null)
			{
				this.m_ondragendDelegate();
				return;
			}
		}

		// Token: 0x06018A48 RID: 100936 RVA: 0x000C4C00 File Offset: 0x000C3C00
		public override bool ondrag()
		{
			return this.m_ondragDelegate != null && this.m_ondragDelegate();
		}

		// Token: 0x06018A49 RID: 100937 RVA: 0x000C4C2C File Offset: 0x000C3C2C
		public override void onresize()
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate();
				return;
			}
		}

		// Token: 0x06018A4A RID: 100938 RVA: 0x000C4C58 File Offset: 0x000C3C58
		public override void onblur()
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate();
				return;
			}
		}

		// Token: 0x06018A4B RID: 100939 RVA: 0x000C4C84 File Offset: 0x000C3C84
		public override void onfocus()
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate();
				return;
			}
		}

		// Token: 0x06018A4C RID: 100940 RVA: 0x000C4CB0 File Offset: 0x000C3CB0
		public override void onscroll()
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate();
				return;
			}
		}

		// Token: 0x06018A4D RID: 100941 RVA: 0x000C4CDC File Offset: 0x000C3CDC
		public override void onpropertychange()
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate();
				return;
			}
		}

		// Token: 0x06018A4E RID: 100942 RVA: 0x000C4D08 File Offset: 0x000C3D08
		public override void onlosecapture()
		{
			if (this.m_onlosecaptureDelegate != null)
			{
				this.m_onlosecaptureDelegate();
				return;
			}
		}

		// Token: 0x06018A4F RID: 100943 RVA: 0x000C4D34 File Offset: 0x000C3D34
		public override void ondatasetcomplete()
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate();
				return;
			}
		}

		// Token: 0x06018A50 RID: 100944 RVA: 0x000C4D60 File Offset: 0x000C3D60
		public override void ondataavailable()
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate();
				return;
			}
		}

		// Token: 0x06018A51 RID: 100945 RVA: 0x000C4D8C File Offset: 0x000C3D8C
		public override void ondatasetchanged()
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate();
				return;
			}
		}

		// Token: 0x06018A52 RID: 100946 RVA: 0x000C4DB8 File Offset: 0x000C3DB8
		public override void onrowenter()
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate();
				return;
			}
		}

		// Token: 0x06018A53 RID: 100947 RVA: 0x000C4DE4 File Offset: 0x000C3DE4
		public override bool onrowexit()
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate();
		}

		// Token: 0x06018A54 RID: 100948 RVA: 0x000C4E10 File Offset: 0x000C3E10
		public override bool onerrorupdate()
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate();
		}

		// Token: 0x06018A55 RID: 100949 RVA: 0x000C4E3C File Offset: 0x000C3E3C
		public override void onafterupdate()
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate();
				return;
			}
		}

		// Token: 0x06018A56 RID: 100950 RVA: 0x000C4E68 File Offset: 0x000C3E68
		public override bool onbeforeupdate()
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate();
		}

		// Token: 0x06018A57 RID: 100951 RVA: 0x000C4E94 File Offset: 0x000C3E94
		public override bool ondragstart()
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate();
		}

		// Token: 0x06018A58 RID: 100952 RVA: 0x000C4EC0 File Offset: 0x000C3EC0
		public override void onfilterchange()
		{
			if (this.m_onfilterchangeDelegate != null)
			{
				this.m_onfilterchangeDelegate();
				return;
			}
		}

		// Token: 0x06018A59 RID: 100953 RVA: 0x000C4EEC File Offset: 0x000C3EEC
		public override bool onselectstart()
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate();
		}

		// Token: 0x06018A5A RID: 100954 RVA: 0x000C4F18 File Offset: 0x000C3F18
		public override void onmouseup()
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate();
				return;
			}
		}

		// Token: 0x06018A5B RID: 100955 RVA: 0x000C4F44 File Offset: 0x000C3F44
		public override void onmousedown()
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate();
				return;
			}
		}

		// Token: 0x06018A5C RID: 100956 RVA: 0x000C4F70 File Offset: 0x000C3F70
		public override void onmousemove()
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate();
				return;
			}
		}

		// Token: 0x06018A5D RID: 100957 RVA: 0x000C4F9C File Offset: 0x000C3F9C
		public override void onmouseover()
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate();
				return;
			}
		}

		// Token: 0x06018A5E RID: 100958 RVA: 0x000C4FC8 File Offset: 0x000C3FC8
		public override void onmouseout()
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate();
				return;
			}
		}

		// Token: 0x06018A5F RID: 100959 RVA: 0x000C4FF4 File Offset: 0x000C3FF4
		public override void onkeyup()
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate();
				return;
			}
		}

		// Token: 0x06018A60 RID: 100960 RVA: 0x000C5020 File Offset: 0x000C4020
		public override void onkeydown()
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate();
				return;
			}
		}

		// Token: 0x06018A61 RID: 100961 RVA: 0x000C504C File Offset: 0x000C404C
		public override bool onkeypress()
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate();
		}

		// Token: 0x06018A62 RID: 100962 RVA: 0x000C5078 File Offset: 0x000C4078
		public override bool ondblclick()
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate();
		}

		// Token: 0x06018A63 RID: 100963 RVA: 0x000C50A4 File Offset: 0x000C40A4
		public override bool onclick()
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate();
		}

		// Token: 0x06018A64 RID: 100964 RVA: 0x000C50D0 File Offset: 0x000C40D0
		public override bool onhelp()
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate();
		}

		// Token: 0x06018A65 RID: 100965 RVA: 0x000C50FC File Offset: 0x000C40FC
		internal HTMLButtonElementEvents_SinkHelper()
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

		// Token: 0x04000C22 RID: 3106
		public HTMLButtonElementEvents_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x04000C23 RID: 3107
		public HTMLButtonElementEvents_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x04000C24 RID: 3108
		public HTMLButtonElementEvents_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x04000C25 RID: 3109
		public HTMLButtonElementEvents_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x04000C26 RID: 3110
		public HTMLButtonElementEvents_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x04000C27 RID: 3111
		public HTMLButtonElementEvents_onmouseleaveEventHandler m_onmouseleaveDelegate;

		// Token: 0x04000C28 RID: 3112
		public HTMLButtonElementEvents_onmouseenterEventHandler m_onmouseenterDelegate;

		// Token: 0x04000C29 RID: 3113
		public HTMLButtonElementEvents_onresizeendEventHandler m_onresizeendDelegate;

		// Token: 0x04000C2A RID: 3114
		public HTMLButtonElementEvents_onresizestartEventHandler m_onresizestartDelegate;

		// Token: 0x04000C2B RID: 3115
		public HTMLButtonElementEvents_onmoveendEventHandler m_onmoveendDelegate;

		// Token: 0x04000C2C RID: 3116
		public HTMLButtonElementEvents_onmovestartEventHandler m_onmovestartDelegate;

		// Token: 0x04000C2D RID: 3117
		public HTMLButtonElementEvents_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x04000C2E RID: 3118
		public HTMLButtonElementEvents_onmoveEventHandler m_onmoveDelegate;

		// Token: 0x04000C2F RID: 3119
		public HTMLButtonElementEvents_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x04000C30 RID: 3120
		public HTMLButtonElementEvents_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x04000C31 RID: 3121
		public HTMLButtonElementEvents_onpageEventHandler m_onpageDelegate;

		// Token: 0x04000C32 RID: 3122
		public HTMLButtonElementEvents_onlayoutcompleteEventHandler m_onlayoutcompleteDelegate;

		// Token: 0x04000C33 RID: 3123
		public HTMLButtonElementEvents_onbeforeeditfocusEventHandler m_onbeforeeditfocusDelegate;

		// Token: 0x04000C34 RID: 3124
		public HTMLButtonElementEvents_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x04000C35 RID: 3125
		public HTMLButtonElementEvents_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x04000C36 RID: 3126
		public HTMLButtonElementEvents_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x04000C37 RID: 3127
		public HTMLButtonElementEvents_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x04000C38 RID: 3128
		public HTMLButtonElementEvents_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x04000C39 RID: 3129
		public HTMLButtonElementEvents_onpasteEventHandler m_onpasteDelegate;

		// Token: 0x04000C3A RID: 3130
		public HTMLButtonElementEvents_onbeforepasteEventHandler m_onbeforepasteDelegate;

		// Token: 0x04000C3B RID: 3131
		public HTMLButtonElementEvents_oncopyEventHandler m_oncopyDelegate;

		// Token: 0x04000C3C RID: 3132
		public HTMLButtonElementEvents_onbeforecopyEventHandler m_onbeforecopyDelegate;

		// Token: 0x04000C3D RID: 3133
		public HTMLButtonElementEvents_oncutEventHandler m_oncutDelegate;

		// Token: 0x04000C3E RID: 3134
		public HTMLButtonElementEvents_onbeforecutEventHandler m_onbeforecutDelegate;

		// Token: 0x04000C3F RID: 3135
		public HTMLButtonElementEvents_ondropEventHandler m_ondropDelegate;

		// Token: 0x04000C40 RID: 3136
		public HTMLButtonElementEvents_ondragleaveEventHandler m_ondragleaveDelegate;

		// Token: 0x04000C41 RID: 3137
		public HTMLButtonElementEvents_ondragoverEventHandler m_ondragoverDelegate;

		// Token: 0x04000C42 RID: 3138
		public HTMLButtonElementEvents_ondragenterEventHandler m_ondragenterDelegate;

		// Token: 0x04000C43 RID: 3139
		public HTMLButtonElementEvents_ondragendEventHandler m_ondragendDelegate;

		// Token: 0x04000C44 RID: 3140
		public HTMLButtonElementEvents_ondragEventHandler m_ondragDelegate;

		// Token: 0x04000C45 RID: 3141
		public HTMLButtonElementEvents_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x04000C46 RID: 3142
		public HTMLButtonElementEvents_onblurEventHandler m_onblurDelegate;

		// Token: 0x04000C47 RID: 3143
		public HTMLButtonElementEvents_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x04000C48 RID: 3144
		public HTMLButtonElementEvents_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x04000C49 RID: 3145
		public HTMLButtonElementEvents_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x04000C4A RID: 3146
		public HTMLButtonElementEvents_onlosecaptureEventHandler m_onlosecaptureDelegate;

		// Token: 0x04000C4B RID: 3147
		public HTMLButtonElementEvents_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x04000C4C RID: 3148
		public HTMLButtonElementEvents_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x04000C4D RID: 3149
		public HTMLButtonElementEvents_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x04000C4E RID: 3150
		public HTMLButtonElementEvents_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x04000C4F RID: 3151
		public HTMLButtonElementEvents_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x04000C50 RID: 3152
		public HTMLButtonElementEvents_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x04000C51 RID: 3153
		public HTMLButtonElementEvents_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x04000C52 RID: 3154
		public HTMLButtonElementEvents_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x04000C53 RID: 3155
		public HTMLButtonElementEvents_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x04000C54 RID: 3156
		public HTMLButtonElementEvents_onfilterchangeEventHandler m_onfilterchangeDelegate;

		// Token: 0x04000C55 RID: 3157
		public HTMLButtonElementEvents_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x04000C56 RID: 3158
		public HTMLButtonElementEvents_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x04000C57 RID: 3159
		public HTMLButtonElementEvents_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x04000C58 RID: 3160
		public HTMLButtonElementEvents_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x04000C59 RID: 3161
		public HTMLButtonElementEvents_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x04000C5A RID: 3162
		public HTMLButtonElementEvents_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x04000C5B RID: 3163
		public HTMLButtonElementEvents_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x04000C5C RID: 3164
		public HTMLButtonElementEvents_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x04000C5D RID: 3165
		public HTMLButtonElementEvents_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x04000C5E RID: 3166
		public HTMLButtonElementEvents_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x04000C5F RID: 3167
		public HTMLButtonElementEvents_onclickEventHandler m_onclickDelegate;

		// Token: 0x04000C60 RID: 3168
		public HTMLButtonElementEvents_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x04000C61 RID: 3169
		public int m_dwCookie;
	}
}
