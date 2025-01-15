using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000E14 RID: 3604
	[ClassInterface(0)]
	internal sealed class HTMLInputImageEvents_SinkHelper : HTMLInputImageEvents
	{
		// Token: 0x06019264 RID: 103012 RVA: 0x0010F70C File Offset: 0x0010E70C
		public override void onabort()
		{
			if (this.m_onabortDelegate != null)
			{
				this.m_onabortDelegate();
				return;
			}
		}

		// Token: 0x06019265 RID: 103013 RVA: 0x0010F738 File Offset: 0x0010E738
		public override void onerror()
		{
			if (this.m_onerrorDelegate != null)
			{
				this.m_onerrorDelegate();
				return;
			}
		}

		// Token: 0x06019266 RID: 103014 RVA: 0x0010F764 File Offset: 0x0010E764
		public override void onload()
		{
			if (this.m_onloadDelegate != null)
			{
				this.m_onloadDelegate();
				return;
			}
		}

		// Token: 0x06019267 RID: 103015 RVA: 0x0010F790 File Offset: 0x0010E790
		public override void onfocusout()
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate();
				return;
			}
		}

		// Token: 0x06019268 RID: 103016 RVA: 0x0010F7BC File Offset: 0x0010E7BC
		public override void onfocusin()
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate();
				return;
			}
		}

		// Token: 0x06019269 RID: 103017 RVA: 0x0010F7E8 File Offset: 0x0010E7E8
		public override void ondeactivate()
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate();
				return;
			}
		}

		// Token: 0x0601926A RID: 103018 RVA: 0x0010F814 File Offset: 0x0010E814
		public override void onactivate()
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate();
				return;
			}
		}

		// Token: 0x0601926B RID: 103019 RVA: 0x0010F840 File Offset: 0x0010E840
		public override bool onmousewheel()
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate();
		}

		// Token: 0x0601926C RID: 103020 RVA: 0x0010F86C File Offset: 0x0010E86C
		public override void onmouseleave()
		{
			if (this.m_onmouseleaveDelegate != null)
			{
				this.m_onmouseleaveDelegate();
				return;
			}
		}

		// Token: 0x0601926D RID: 103021 RVA: 0x0010F898 File Offset: 0x0010E898
		public override void onmouseenter()
		{
			if (this.m_onmouseenterDelegate != null)
			{
				this.m_onmouseenterDelegate();
				return;
			}
		}

		// Token: 0x0601926E RID: 103022 RVA: 0x0010F8C4 File Offset: 0x0010E8C4
		public override void onresizeend()
		{
			if (this.m_onresizeendDelegate != null)
			{
				this.m_onresizeendDelegate();
				return;
			}
		}

		// Token: 0x0601926F RID: 103023 RVA: 0x0010F8F0 File Offset: 0x0010E8F0
		public override bool onresizestart()
		{
			return this.m_onresizestartDelegate != null && this.m_onresizestartDelegate();
		}

		// Token: 0x06019270 RID: 103024 RVA: 0x0010F91C File Offset: 0x0010E91C
		public override void onmoveend()
		{
			if (this.m_onmoveendDelegate != null)
			{
				this.m_onmoveendDelegate();
				return;
			}
		}

		// Token: 0x06019271 RID: 103025 RVA: 0x0010F948 File Offset: 0x0010E948
		public override bool onmovestart()
		{
			return this.m_onmovestartDelegate != null && this.m_onmovestartDelegate();
		}

		// Token: 0x06019272 RID: 103026 RVA: 0x0010F974 File Offset: 0x0010E974
		public override bool oncontrolselect()
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate();
		}

		// Token: 0x06019273 RID: 103027 RVA: 0x0010F9A0 File Offset: 0x0010E9A0
		public override void onmove()
		{
			if (this.m_onmoveDelegate != null)
			{
				this.m_onmoveDelegate();
				return;
			}
		}

		// Token: 0x06019274 RID: 103028 RVA: 0x0010F9CC File Offset: 0x0010E9CC
		public override bool onbeforeactivate()
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate();
		}

		// Token: 0x06019275 RID: 103029 RVA: 0x0010F9F8 File Offset: 0x0010E9F8
		public override bool onbeforedeactivate()
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate();
		}

		// Token: 0x06019276 RID: 103030 RVA: 0x0010FA24 File Offset: 0x0010EA24
		public override void onpage()
		{
			if (this.m_onpageDelegate != null)
			{
				this.m_onpageDelegate();
				return;
			}
		}

		// Token: 0x06019277 RID: 103031 RVA: 0x0010FA50 File Offset: 0x0010EA50
		public override void onlayoutcomplete()
		{
			if (this.m_onlayoutcompleteDelegate != null)
			{
				this.m_onlayoutcompleteDelegate();
				return;
			}
		}

		// Token: 0x06019278 RID: 103032 RVA: 0x0010FA7C File Offset: 0x0010EA7C
		public override void onbeforeeditfocus()
		{
			if (this.m_onbeforeeditfocusDelegate != null)
			{
				this.m_onbeforeeditfocusDelegate();
				return;
			}
		}

		// Token: 0x06019279 RID: 103033 RVA: 0x0010FAA8 File Offset: 0x0010EAA8
		public override void onreadystatechange()
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate();
				return;
			}
		}

		// Token: 0x0601927A RID: 103034 RVA: 0x0010FAD4 File Offset: 0x0010EAD4
		public override void oncellchange()
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate();
				return;
			}
		}

		// Token: 0x0601927B RID: 103035 RVA: 0x0010FB00 File Offset: 0x0010EB00
		public override void onrowsinserted()
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate();
				return;
			}
		}

		// Token: 0x0601927C RID: 103036 RVA: 0x0010FB2C File Offset: 0x0010EB2C
		public override void onrowsdelete()
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate();
				return;
			}
		}

		// Token: 0x0601927D RID: 103037 RVA: 0x0010FB58 File Offset: 0x0010EB58
		public override bool oncontextmenu()
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate();
		}

		// Token: 0x0601927E RID: 103038 RVA: 0x0010FB84 File Offset: 0x0010EB84
		public override bool onpaste()
		{
			return this.m_onpasteDelegate != null && this.m_onpasteDelegate();
		}

		// Token: 0x0601927F RID: 103039 RVA: 0x0010FBB0 File Offset: 0x0010EBB0
		public override bool onbeforepaste()
		{
			return this.m_onbeforepasteDelegate != null && this.m_onbeforepasteDelegate();
		}

		// Token: 0x06019280 RID: 103040 RVA: 0x0010FBDC File Offset: 0x0010EBDC
		public override bool oncopy()
		{
			return this.m_oncopyDelegate != null && this.m_oncopyDelegate();
		}

		// Token: 0x06019281 RID: 103041 RVA: 0x0010FC08 File Offset: 0x0010EC08
		public override bool onbeforecopy()
		{
			return this.m_onbeforecopyDelegate != null && this.m_onbeforecopyDelegate();
		}

		// Token: 0x06019282 RID: 103042 RVA: 0x0010FC34 File Offset: 0x0010EC34
		public override bool oncut()
		{
			return this.m_oncutDelegate != null && this.m_oncutDelegate();
		}

		// Token: 0x06019283 RID: 103043 RVA: 0x0010FC60 File Offset: 0x0010EC60
		public override bool onbeforecut()
		{
			return this.m_onbeforecutDelegate != null && this.m_onbeforecutDelegate();
		}

		// Token: 0x06019284 RID: 103044 RVA: 0x0010FC8C File Offset: 0x0010EC8C
		public override bool ondrop()
		{
			return this.m_ondropDelegate != null && this.m_ondropDelegate();
		}

		// Token: 0x06019285 RID: 103045 RVA: 0x0010FCB8 File Offset: 0x0010ECB8
		public override void ondragleave()
		{
			if (this.m_ondragleaveDelegate != null)
			{
				this.m_ondragleaveDelegate();
				return;
			}
		}

		// Token: 0x06019286 RID: 103046 RVA: 0x0010FCE4 File Offset: 0x0010ECE4
		public override bool ondragover()
		{
			return this.m_ondragoverDelegate != null && this.m_ondragoverDelegate();
		}

		// Token: 0x06019287 RID: 103047 RVA: 0x0010FD10 File Offset: 0x0010ED10
		public override bool ondragenter()
		{
			return this.m_ondragenterDelegate != null && this.m_ondragenterDelegate();
		}

		// Token: 0x06019288 RID: 103048 RVA: 0x0010FD3C File Offset: 0x0010ED3C
		public override void ondragend()
		{
			if (this.m_ondragendDelegate != null)
			{
				this.m_ondragendDelegate();
				return;
			}
		}

		// Token: 0x06019289 RID: 103049 RVA: 0x0010FD68 File Offset: 0x0010ED68
		public override bool ondrag()
		{
			return this.m_ondragDelegate != null && this.m_ondragDelegate();
		}

		// Token: 0x0601928A RID: 103050 RVA: 0x0010FD94 File Offset: 0x0010ED94
		public override void onresize()
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate();
				return;
			}
		}

		// Token: 0x0601928B RID: 103051 RVA: 0x0010FDC0 File Offset: 0x0010EDC0
		public override void onblur()
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate();
				return;
			}
		}

		// Token: 0x0601928C RID: 103052 RVA: 0x0010FDEC File Offset: 0x0010EDEC
		public override void onfocus()
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate();
				return;
			}
		}

		// Token: 0x0601928D RID: 103053 RVA: 0x0010FE18 File Offset: 0x0010EE18
		public override void onscroll()
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate();
				return;
			}
		}

		// Token: 0x0601928E RID: 103054 RVA: 0x0010FE44 File Offset: 0x0010EE44
		public override void onpropertychange()
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate();
				return;
			}
		}

		// Token: 0x0601928F RID: 103055 RVA: 0x0010FE70 File Offset: 0x0010EE70
		public override void onlosecapture()
		{
			if (this.m_onlosecaptureDelegate != null)
			{
				this.m_onlosecaptureDelegate();
				return;
			}
		}

		// Token: 0x06019290 RID: 103056 RVA: 0x0010FE9C File Offset: 0x0010EE9C
		public override void ondatasetcomplete()
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate();
				return;
			}
		}

		// Token: 0x06019291 RID: 103057 RVA: 0x0010FEC8 File Offset: 0x0010EEC8
		public override void ondataavailable()
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate();
				return;
			}
		}

		// Token: 0x06019292 RID: 103058 RVA: 0x0010FEF4 File Offset: 0x0010EEF4
		public override void ondatasetchanged()
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate();
				return;
			}
		}

		// Token: 0x06019293 RID: 103059 RVA: 0x0010FF20 File Offset: 0x0010EF20
		public override void onrowenter()
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate();
				return;
			}
		}

		// Token: 0x06019294 RID: 103060 RVA: 0x0010FF4C File Offset: 0x0010EF4C
		public override bool onrowexit()
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate();
		}

		// Token: 0x06019295 RID: 103061 RVA: 0x0010FF78 File Offset: 0x0010EF78
		public override bool onerrorupdate()
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate();
		}

		// Token: 0x06019296 RID: 103062 RVA: 0x0010FFA4 File Offset: 0x0010EFA4
		public override void onafterupdate()
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate();
				return;
			}
		}

		// Token: 0x06019297 RID: 103063 RVA: 0x0010FFD0 File Offset: 0x0010EFD0
		public override bool onbeforeupdate()
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate();
		}

		// Token: 0x06019298 RID: 103064 RVA: 0x0010FFFC File Offset: 0x0010EFFC
		public override bool ondragstart()
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate();
		}

		// Token: 0x06019299 RID: 103065 RVA: 0x00110028 File Offset: 0x0010F028
		public override void onfilterchange()
		{
			if (this.m_onfilterchangeDelegate != null)
			{
				this.m_onfilterchangeDelegate();
				return;
			}
		}

		// Token: 0x0601929A RID: 103066 RVA: 0x00110054 File Offset: 0x0010F054
		public override bool onselectstart()
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate();
		}

		// Token: 0x0601929B RID: 103067 RVA: 0x00110080 File Offset: 0x0010F080
		public override void onmouseup()
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate();
				return;
			}
		}

		// Token: 0x0601929C RID: 103068 RVA: 0x001100AC File Offset: 0x0010F0AC
		public override void onmousedown()
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate();
				return;
			}
		}

		// Token: 0x0601929D RID: 103069 RVA: 0x001100D8 File Offset: 0x0010F0D8
		public override void onmousemove()
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate();
				return;
			}
		}

		// Token: 0x0601929E RID: 103070 RVA: 0x00110104 File Offset: 0x0010F104
		public override void onmouseover()
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate();
				return;
			}
		}

		// Token: 0x0601929F RID: 103071 RVA: 0x00110130 File Offset: 0x0010F130
		public override void onmouseout()
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate();
				return;
			}
		}

		// Token: 0x060192A0 RID: 103072 RVA: 0x0011015C File Offset: 0x0010F15C
		public override void onkeyup()
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate();
				return;
			}
		}

		// Token: 0x060192A1 RID: 103073 RVA: 0x00110188 File Offset: 0x0010F188
		public override void onkeydown()
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate();
				return;
			}
		}

		// Token: 0x060192A2 RID: 103074 RVA: 0x001101B4 File Offset: 0x0010F1B4
		public override bool onkeypress()
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate();
		}

		// Token: 0x060192A3 RID: 103075 RVA: 0x001101E0 File Offset: 0x0010F1E0
		public override bool ondblclick()
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate();
		}

		// Token: 0x060192A4 RID: 103076 RVA: 0x0011020C File Offset: 0x0010F20C
		public override bool onclick()
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate();
		}

		// Token: 0x060192A5 RID: 103077 RVA: 0x00110238 File Offset: 0x0010F238
		public override bool onhelp()
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate();
		}

		// Token: 0x060192A6 RID: 103078 RVA: 0x00110264 File Offset: 0x0010F264
		internal HTMLInputImageEvents_SinkHelper()
		{
			this.m_dwCookie = 0;
			this.m_onabortDelegate = null;
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

		// Token: 0x04000EFB RID: 3835
		public HTMLInputImageEvents_onabortEventHandler m_onabortDelegate;

		// Token: 0x04000EFC RID: 3836
		public HTMLInputImageEvents_onerrorEventHandler m_onerrorDelegate;

		// Token: 0x04000EFD RID: 3837
		public HTMLInputImageEvents_onloadEventHandler m_onloadDelegate;

		// Token: 0x04000EFE RID: 3838
		public HTMLInputImageEvents_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x04000EFF RID: 3839
		public HTMLInputImageEvents_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x04000F00 RID: 3840
		public HTMLInputImageEvents_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x04000F01 RID: 3841
		public HTMLInputImageEvents_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x04000F02 RID: 3842
		public HTMLInputImageEvents_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x04000F03 RID: 3843
		public HTMLInputImageEvents_onmouseleaveEventHandler m_onmouseleaveDelegate;

		// Token: 0x04000F04 RID: 3844
		public HTMLInputImageEvents_onmouseenterEventHandler m_onmouseenterDelegate;

		// Token: 0x04000F05 RID: 3845
		public HTMLInputImageEvents_onresizeendEventHandler m_onresizeendDelegate;

		// Token: 0x04000F06 RID: 3846
		public HTMLInputImageEvents_onresizestartEventHandler m_onresizestartDelegate;

		// Token: 0x04000F07 RID: 3847
		public HTMLInputImageEvents_onmoveendEventHandler m_onmoveendDelegate;

		// Token: 0x04000F08 RID: 3848
		public HTMLInputImageEvents_onmovestartEventHandler m_onmovestartDelegate;

		// Token: 0x04000F09 RID: 3849
		public HTMLInputImageEvents_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x04000F0A RID: 3850
		public HTMLInputImageEvents_onmoveEventHandler m_onmoveDelegate;

		// Token: 0x04000F0B RID: 3851
		public HTMLInputImageEvents_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x04000F0C RID: 3852
		public HTMLInputImageEvents_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x04000F0D RID: 3853
		public HTMLInputImageEvents_onpageEventHandler m_onpageDelegate;

		// Token: 0x04000F0E RID: 3854
		public HTMLInputImageEvents_onlayoutcompleteEventHandler m_onlayoutcompleteDelegate;

		// Token: 0x04000F0F RID: 3855
		public HTMLInputImageEvents_onbeforeeditfocusEventHandler m_onbeforeeditfocusDelegate;

		// Token: 0x04000F10 RID: 3856
		public HTMLInputImageEvents_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x04000F11 RID: 3857
		public HTMLInputImageEvents_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x04000F12 RID: 3858
		public HTMLInputImageEvents_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x04000F13 RID: 3859
		public HTMLInputImageEvents_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x04000F14 RID: 3860
		public HTMLInputImageEvents_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x04000F15 RID: 3861
		public HTMLInputImageEvents_onpasteEventHandler m_onpasteDelegate;

		// Token: 0x04000F16 RID: 3862
		public HTMLInputImageEvents_onbeforepasteEventHandler m_onbeforepasteDelegate;

		// Token: 0x04000F17 RID: 3863
		public HTMLInputImageEvents_oncopyEventHandler m_oncopyDelegate;

		// Token: 0x04000F18 RID: 3864
		public HTMLInputImageEvents_onbeforecopyEventHandler m_onbeforecopyDelegate;

		// Token: 0x04000F19 RID: 3865
		public HTMLInputImageEvents_oncutEventHandler m_oncutDelegate;

		// Token: 0x04000F1A RID: 3866
		public HTMLInputImageEvents_onbeforecutEventHandler m_onbeforecutDelegate;

		// Token: 0x04000F1B RID: 3867
		public HTMLInputImageEvents_ondropEventHandler m_ondropDelegate;

		// Token: 0x04000F1C RID: 3868
		public HTMLInputImageEvents_ondragleaveEventHandler m_ondragleaveDelegate;

		// Token: 0x04000F1D RID: 3869
		public HTMLInputImageEvents_ondragoverEventHandler m_ondragoverDelegate;

		// Token: 0x04000F1E RID: 3870
		public HTMLInputImageEvents_ondragenterEventHandler m_ondragenterDelegate;

		// Token: 0x04000F1F RID: 3871
		public HTMLInputImageEvents_ondragendEventHandler m_ondragendDelegate;

		// Token: 0x04000F20 RID: 3872
		public HTMLInputImageEvents_ondragEventHandler m_ondragDelegate;

		// Token: 0x04000F21 RID: 3873
		public HTMLInputImageEvents_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x04000F22 RID: 3874
		public HTMLInputImageEvents_onblurEventHandler m_onblurDelegate;

		// Token: 0x04000F23 RID: 3875
		public HTMLInputImageEvents_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x04000F24 RID: 3876
		public HTMLInputImageEvents_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x04000F25 RID: 3877
		public HTMLInputImageEvents_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x04000F26 RID: 3878
		public HTMLInputImageEvents_onlosecaptureEventHandler m_onlosecaptureDelegate;

		// Token: 0x04000F27 RID: 3879
		public HTMLInputImageEvents_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x04000F28 RID: 3880
		public HTMLInputImageEvents_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x04000F29 RID: 3881
		public HTMLInputImageEvents_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x04000F2A RID: 3882
		public HTMLInputImageEvents_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x04000F2B RID: 3883
		public HTMLInputImageEvents_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x04000F2C RID: 3884
		public HTMLInputImageEvents_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x04000F2D RID: 3885
		public HTMLInputImageEvents_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x04000F2E RID: 3886
		public HTMLInputImageEvents_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x04000F2F RID: 3887
		public HTMLInputImageEvents_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x04000F30 RID: 3888
		public HTMLInputImageEvents_onfilterchangeEventHandler m_onfilterchangeDelegate;

		// Token: 0x04000F31 RID: 3889
		public HTMLInputImageEvents_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x04000F32 RID: 3890
		public HTMLInputImageEvents_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x04000F33 RID: 3891
		public HTMLInputImageEvents_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x04000F34 RID: 3892
		public HTMLInputImageEvents_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x04000F35 RID: 3893
		public HTMLInputImageEvents_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x04000F36 RID: 3894
		public HTMLInputImageEvents_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x04000F37 RID: 3895
		public HTMLInputImageEvents_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x04000F38 RID: 3896
		public HTMLInputImageEvents_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x04000F39 RID: 3897
		public HTMLInputImageEvents_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x04000F3A RID: 3898
		public HTMLInputImageEvents_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x04000F3B RID: 3899
		public HTMLInputImageEvents_onclickEventHandler m_onclickDelegate;

		// Token: 0x04000F3C RID: 3900
		public HTMLInputImageEvents_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x04000F3D RID: 3901
		public int m_dwCookie;
	}
}
