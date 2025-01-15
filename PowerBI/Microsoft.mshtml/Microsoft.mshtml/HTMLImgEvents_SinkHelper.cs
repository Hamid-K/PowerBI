using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DD4 RID: 3540
	[ClassInterface(0)]
	internal sealed class HTMLImgEvents_SinkHelper : HTMLImgEvents
	{
		// Token: 0x06017C07 RID: 97287 RVA: 0x00043BD0 File Offset: 0x00042BD0
		public override void onabort()
		{
			if (this.m_onabortDelegate != null)
			{
				this.m_onabortDelegate();
				return;
			}
		}

		// Token: 0x06017C08 RID: 97288 RVA: 0x00043BFC File Offset: 0x00042BFC
		public override void onerror()
		{
			if (this.m_onerrorDelegate != null)
			{
				this.m_onerrorDelegate();
				return;
			}
		}

		// Token: 0x06017C09 RID: 97289 RVA: 0x00043C28 File Offset: 0x00042C28
		public override void onload()
		{
			if (this.m_onloadDelegate != null)
			{
				this.m_onloadDelegate();
				return;
			}
		}

		// Token: 0x06017C0A RID: 97290 RVA: 0x00043C54 File Offset: 0x00042C54
		public override void onfocusout()
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate();
				return;
			}
		}

		// Token: 0x06017C0B RID: 97291 RVA: 0x00043C80 File Offset: 0x00042C80
		public override void onfocusin()
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate();
				return;
			}
		}

		// Token: 0x06017C0C RID: 97292 RVA: 0x00043CAC File Offset: 0x00042CAC
		public override void ondeactivate()
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate();
				return;
			}
		}

		// Token: 0x06017C0D RID: 97293 RVA: 0x00043CD8 File Offset: 0x00042CD8
		public override void onactivate()
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate();
				return;
			}
		}

		// Token: 0x06017C0E RID: 97294 RVA: 0x00043D04 File Offset: 0x00042D04
		public override bool onmousewheel()
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate();
		}

		// Token: 0x06017C0F RID: 97295 RVA: 0x00043D30 File Offset: 0x00042D30
		public override void onmouseleave()
		{
			if (this.m_onmouseleaveDelegate != null)
			{
				this.m_onmouseleaveDelegate();
				return;
			}
		}

		// Token: 0x06017C10 RID: 97296 RVA: 0x00043D5C File Offset: 0x00042D5C
		public override void onmouseenter()
		{
			if (this.m_onmouseenterDelegate != null)
			{
				this.m_onmouseenterDelegate();
				return;
			}
		}

		// Token: 0x06017C11 RID: 97297 RVA: 0x00043D88 File Offset: 0x00042D88
		public override void onresizeend()
		{
			if (this.m_onresizeendDelegate != null)
			{
				this.m_onresizeendDelegate();
				return;
			}
		}

		// Token: 0x06017C12 RID: 97298 RVA: 0x00043DB4 File Offset: 0x00042DB4
		public override bool onresizestart()
		{
			return this.m_onresizestartDelegate != null && this.m_onresizestartDelegate();
		}

		// Token: 0x06017C13 RID: 97299 RVA: 0x00043DE0 File Offset: 0x00042DE0
		public override void onmoveend()
		{
			if (this.m_onmoveendDelegate != null)
			{
				this.m_onmoveendDelegate();
				return;
			}
		}

		// Token: 0x06017C14 RID: 97300 RVA: 0x00043E0C File Offset: 0x00042E0C
		public override bool onmovestart()
		{
			return this.m_onmovestartDelegate != null && this.m_onmovestartDelegate();
		}

		// Token: 0x06017C15 RID: 97301 RVA: 0x00043E38 File Offset: 0x00042E38
		public override bool oncontrolselect()
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate();
		}

		// Token: 0x06017C16 RID: 97302 RVA: 0x00043E64 File Offset: 0x00042E64
		public override void onmove()
		{
			if (this.m_onmoveDelegate != null)
			{
				this.m_onmoveDelegate();
				return;
			}
		}

		// Token: 0x06017C17 RID: 97303 RVA: 0x00043E90 File Offset: 0x00042E90
		public override bool onbeforeactivate()
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate();
		}

		// Token: 0x06017C18 RID: 97304 RVA: 0x00043EBC File Offset: 0x00042EBC
		public override bool onbeforedeactivate()
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate();
		}

		// Token: 0x06017C19 RID: 97305 RVA: 0x00043EE8 File Offset: 0x00042EE8
		public override void onpage()
		{
			if (this.m_onpageDelegate != null)
			{
				this.m_onpageDelegate();
				return;
			}
		}

		// Token: 0x06017C1A RID: 97306 RVA: 0x00043F14 File Offset: 0x00042F14
		public override void onlayoutcomplete()
		{
			if (this.m_onlayoutcompleteDelegate != null)
			{
				this.m_onlayoutcompleteDelegate();
				return;
			}
		}

		// Token: 0x06017C1B RID: 97307 RVA: 0x00043F40 File Offset: 0x00042F40
		public override void onbeforeeditfocus()
		{
			if (this.m_onbeforeeditfocusDelegate != null)
			{
				this.m_onbeforeeditfocusDelegate();
				return;
			}
		}

		// Token: 0x06017C1C RID: 97308 RVA: 0x00043F6C File Offset: 0x00042F6C
		public override void onreadystatechange()
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate();
				return;
			}
		}

		// Token: 0x06017C1D RID: 97309 RVA: 0x00043F98 File Offset: 0x00042F98
		public override void oncellchange()
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate();
				return;
			}
		}

		// Token: 0x06017C1E RID: 97310 RVA: 0x00043FC4 File Offset: 0x00042FC4
		public override void onrowsinserted()
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate();
				return;
			}
		}

		// Token: 0x06017C1F RID: 97311 RVA: 0x00043FF0 File Offset: 0x00042FF0
		public override void onrowsdelete()
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate();
				return;
			}
		}

		// Token: 0x06017C20 RID: 97312 RVA: 0x0004401C File Offset: 0x0004301C
		public override bool oncontextmenu()
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate();
		}

		// Token: 0x06017C21 RID: 97313 RVA: 0x00044048 File Offset: 0x00043048
		public override bool onpaste()
		{
			return this.m_onpasteDelegate != null && this.m_onpasteDelegate();
		}

		// Token: 0x06017C22 RID: 97314 RVA: 0x00044074 File Offset: 0x00043074
		public override bool onbeforepaste()
		{
			return this.m_onbeforepasteDelegate != null && this.m_onbeforepasteDelegate();
		}

		// Token: 0x06017C23 RID: 97315 RVA: 0x000440A0 File Offset: 0x000430A0
		public override bool oncopy()
		{
			return this.m_oncopyDelegate != null && this.m_oncopyDelegate();
		}

		// Token: 0x06017C24 RID: 97316 RVA: 0x000440CC File Offset: 0x000430CC
		public override bool onbeforecopy()
		{
			return this.m_onbeforecopyDelegate != null && this.m_onbeforecopyDelegate();
		}

		// Token: 0x06017C25 RID: 97317 RVA: 0x000440F8 File Offset: 0x000430F8
		public override bool oncut()
		{
			return this.m_oncutDelegate != null && this.m_oncutDelegate();
		}

		// Token: 0x06017C26 RID: 97318 RVA: 0x00044124 File Offset: 0x00043124
		public override bool onbeforecut()
		{
			return this.m_onbeforecutDelegate != null && this.m_onbeforecutDelegate();
		}

		// Token: 0x06017C27 RID: 97319 RVA: 0x00044150 File Offset: 0x00043150
		public override bool ondrop()
		{
			return this.m_ondropDelegate != null && this.m_ondropDelegate();
		}

		// Token: 0x06017C28 RID: 97320 RVA: 0x0004417C File Offset: 0x0004317C
		public override void ondragleave()
		{
			if (this.m_ondragleaveDelegate != null)
			{
				this.m_ondragleaveDelegate();
				return;
			}
		}

		// Token: 0x06017C29 RID: 97321 RVA: 0x000441A8 File Offset: 0x000431A8
		public override bool ondragover()
		{
			return this.m_ondragoverDelegate != null && this.m_ondragoverDelegate();
		}

		// Token: 0x06017C2A RID: 97322 RVA: 0x000441D4 File Offset: 0x000431D4
		public override bool ondragenter()
		{
			return this.m_ondragenterDelegate != null && this.m_ondragenterDelegate();
		}

		// Token: 0x06017C2B RID: 97323 RVA: 0x00044200 File Offset: 0x00043200
		public override void ondragend()
		{
			if (this.m_ondragendDelegate != null)
			{
				this.m_ondragendDelegate();
				return;
			}
		}

		// Token: 0x06017C2C RID: 97324 RVA: 0x0004422C File Offset: 0x0004322C
		public override bool ondrag()
		{
			return this.m_ondragDelegate != null && this.m_ondragDelegate();
		}

		// Token: 0x06017C2D RID: 97325 RVA: 0x00044258 File Offset: 0x00043258
		public override void onresize()
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate();
				return;
			}
		}

		// Token: 0x06017C2E RID: 97326 RVA: 0x00044284 File Offset: 0x00043284
		public override void onblur()
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate();
				return;
			}
		}

		// Token: 0x06017C2F RID: 97327 RVA: 0x000442B0 File Offset: 0x000432B0
		public override void onfocus()
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate();
				return;
			}
		}

		// Token: 0x06017C30 RID: 97328 RVA: 0x000442DC File Offset: 0x000432DC
		public override void onscroll()
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate();
				return;
			}
		}

		// Token: 0x06017C31 RID: 97329 RVA: 0x00044308 File Offset: 0x00043308
		public override void onpropertychange()
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate();
				return;
			}
		}

		// Token: 0x06017C32 RID: 97330 RVA: 0x00044334 File Offset: 0x00043334
		public override void onlosecapture()
		{
			if (this.m_onlosecaptureDelegate != null)
			{
				this.m_onlosecaptureDelegate();
				return;
			}
		}

		// Token: 0x06017C33 RID: 97331 RVA: 0x00044360 File Offset: 0x00043360
		public override void ondatasetcomplete()
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate();
				return;
			}
		}

		// Token: 0x06017C34 RID: 97332 RVA: 0x0004438C File Offset: 0x0004338C
		public override void ondataavailable()
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate();
				return;
			}
		}

		// Token: 0x06017C35 RID: 97333 RVA: 0x000443B8 File Offset: 0x000433B8
		public override void ondatasetchanged()
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate();
				return;
			}
		}

		// Token: 0x06017C36 RID: 97334 RVA: 0x000443E4 File Offset: 0x000433E4
		public override void onrowenter()
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate();
				return;
			}
		}

		// Token: 0x06017C37 RID: 97335 RVA: 0x00044410 File Offset: 0x00043410
		public override bool onrowexit()
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate();
		}

		// Token: 0x06017C38 RID: 97336 RVA: 0x0004443C File Offset: 0x0004343C
		public override bool onerrorupdate()
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate();
		}

		// Token: 0x06017C39 RID: 97337 RVA: 0x00044468 File Offset: 0x00043468
		public override void onafterupdate()
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate();
				return;
			}
		}

		// Token: 0x06017C3A RID: 97338 RVA: 0x00044494 File Offset: 0x00043494
		public override bool onbeforeupdate()
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate();
		}

		// Token: 0x06017C3B RID: 97339 RVA: 0x000444C0 File Offset: 0x000434C0
		public override bool ondragstart()
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate();
		}

		// Token: 0x06017C3C RID: 97340 RVA: 0x000444EC File Offset: 0x000434EC
		public override void onfilterchange()
		{
			if (this.m_onfilterchangeDelegate != null)
			{
				this.m_onfilterchangeDelegate();
				return;
			}
		}

		// Token: 0x06017C3D RID: 97341 RVA: 0x00044518 File Offset: 0x00043518
		public override bool onselectstart()
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate();
		}

		// Token: 0x06017C3E RID: 97342 RVA: 0x00044544 File Offset: 0x00043544
		public override void onmouseup()
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate();
				return;
			}
		}

		// Token: 0x06017C3F RID: 97343 RVA: 0x00044570 File Offset: 0x00043570
		public override void onmousedown()
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate();
				return;
			}
		}

		// Token: 0x06017C40 RID: 97344 RVA: 0x0004459C File Offset: 0x0004359C
		public override void onmousemove()
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate();
				return;
			}
		}

		// Token: 0x06017C41 RID: 97345 RVA: 0x000445C8 File Offset: 0x000435C8
		public override void onmouseover()
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate();
				return;
			}
		}

		// Token: 0x06017C42 RID: 97346 RVA: 0x000445F4 File Offset: 0x000435F4
		public override void onmouseout()
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate();
				return;
			}
		}

		// Token: 0x06017C43 RID: 97347 RVA: 0x00044620 File Offset: 0x00043620
		public override void onkeyup()
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate();
				return;
			}
		}

		// Token: 0x06017C44 RID: 97348 RVA: 0x0004464C File Offset: 0x0004364C
		public override void onkeydown()
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate();
				return;
			}
		}

		// Token: 0x06017C45 RID: 97349 RVA: 0x00044678 File Offset: 0x00043678
		public override bool onkeypress()
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate();
		}

		// Token: 0x06017C46 RID: 97350 RVA: 0x000446A4 File Offset: 0x000436A4
		public override bool ondblclick()
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate();
		}

		// Token: 0x06017C47 RID: 97351 RVA: 0x000446D0 File Offset: 0x000436D0
		public override bool onclick()
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate();
		}

		// Token: 0x06017C48 RID: 97352 RVA: 0x000446FC File Offset: 0x000436FC
		public override bool onhelp()
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate();
		}

		// Token: 0x06017C49 RID: 97353 RVA: 0x00044728 File Offset: 0x00043728
		internal HTMLImgEvents_SinkHelper()
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

		// Token: 0x0400073C RID: 1852
		public HTMLImgEvents_onabortEventHandler m_onabortDelegate;

		// Token: 0x0400073D RID: 1853
		public HTMLImgEvents_onerrorEventHandler m_onerrorDelegate;

		// Token: 0x0400073E RID: 1854
		public HTMLImgEvents_onloadEventHandler m_onloadDelegate;

		// Token: 0x0400073F RID: 1855
		public HTMLImgEvents_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x04000740 RID: 1856
		public HTMLImgEvents_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x04000741 RID: 1857
		public HTMLImgEvents_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x04000742 RID: 1858
		public HTMLImgEvents_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x04000743 RID: 1859
		public HTMLImgEvents_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x04000744 RID: 1860
		public HTMLImgEvents_onmouseleaveEventHandler m_onmouseleaveDelegate;

		// Token: 0x04000745 RID: 1861
		public HTMLImgEvents_onmouseenterEventHandler m_onmouseenterDelegate;

		// Token: 0x04000746 RID: 1862
		public HTMLImgEvents_onresizeendEventHandler m_onresizeendDelegate;

		// Token: 0x04000747 RID: 1863
		public HTMLImgEvents_onresizestartEventHandler m_onresizestartDelegate;

		// Token: 0x04000748 RID: 1864
		public HTMLImgEvents_onmoveendEventHandler m_onmoveendDelegate;

		// Token: 0x04000749 RID: 1865
		public HTMLImgEvents_onmovestartEventHandler m_onmovestartDelegate;

		// Token: 0x0400074A RID: 1866
		public HTMLImgEvents_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x0400074B RID: 1867
		public HTMLImgEvents_onmoveEventHandler m_onmoveDelegate;

		// Token: 0x0400074C RID: 1868
		public HTMLImgEvents_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x0400074D RID: 1869
		public HTMLImgEvents_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x0400074E RID: 1870
		public HTMLImgEvents_onpageEventHandler m_onpageDelegate;

		// Token: 0x0400074F RID: 1871
		public HTMLImgEvents_onlayoutcompleteEventHandler m_onlayoutcompleteDelegate;

		// Token: 0x04000750 RID: 1872
		public HTMLImgEvents_onbeforeeditfocusEventHandler m_onbeforeeditfocusDelegate;

		// Token: 0x04000751 RID: 1873
		public HTMLImgEvents_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x04000752 RID: 1874
		public HTMLImgEvents_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x04000753 RID: 1875
		public HTMLImgEvents_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x04000754 RID: 1876
		public HTMLImgEvents_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x04000755 RID: 1877
		public HTMLImgEvents_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x04000756 RID: 1878
		public HTMLImgEvents_onpasteEventHandler m_onpasteDelegate;

		// Token: 0x04000757 RID: 1879
		public HTMLImgEvents_onbeforepasteEventHandler m_onbeforepasteDelegate;

		// Token: 0x04000758 RID: 1880
		public HTMLImgEvents_oncopyEventHandler m_oncopyDelegate;

		// Token: 0x04000759 RID: 1881
		public HTMLImgEvents_onbeforecopyEventHandler m_onbeforecopyDelegate;

		// Token: 0x0400075A RID: 1882
		public HTMLImgEvents_oncutEventHandler m_oncutDelegate;

		// Token: 0x0400075B RID: 1883
		public HTMLImgEvents_onbeforecutEventHandler m_onbeforecutDelegate;

		// Token: 0x0400075C RID: 1884
		public HTMLImgEvents_ondropEventHandler m_ondropDelegate;

		// Token: 0x0400075D RID: 1885
		public HTMLImgEvents_ondragleaveEventHandler m_ondragleaveDelegate;

		// Token: 0x0400075E RID: 1886
		public HTMLImgEvents_ondragoverEventHandler m_ondragoverDelegate;

		// Token: 0x0400075F RID: 1887
		public HTMLImgEvents_ondragenterEventHandler m_ondragenterDelegate;

		// Token: 0x04000760 RID: 1888
		public HTMLImgEvents_ondragendEventHandler m_ondragendDelegate;

		// Token: 0x04000761 RID: 1889
		public HTMLImgEvents_ondragEventHandler m_ondragDelegate;

		// Token: 0x04000762 RID: 1890
		public HTMLImgEvents_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x04000763 RID: 1891
		public HTMLImgEvents_onblurEventHandler m_onblurDelegate;

		// Token: 0x04000764 RID: 1892
		public HTMLImgEvents_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x04000765 RID: 1893
		public HTMLImgEvents_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x04000766 RID: 1894
		public HTMLImgEvents_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x04000767 RID: 1895
		public HTMLImgEvents_onlosecaptureEventHandler m_onlosecaptureDelegate;

		// Token: 0x04000768 RID: 1896
		public HTMLImgEvents_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x04000769 RID: 1897
		public HTMLImgEvents_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x0400076A RID: 1898
		public HTMLImgEvents_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x0400076B RID: 1899
		public HTMLImgEvents_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x0400076C RID: 1900
		public HTMLImgEvents_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x0400076D RID: 1901
		public HTMLImgEvents_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x0400076E RID: 1902
		public HTMLImgEvents_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x0400076F RID: 1903
		public HTMLImgEvents_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x04000770 RID: 1904
		public HTMLImgEvents_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x04000771 RID: 1905
		public HTMLImgEvents_onfilterchangeEventHandler m_onfilterchangeDelegate;

		// Token: 0x04000772 RID: 1906
		public HTMLImgEvents_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x04000773 RID: 1907
		public HTMLImgEvents_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x04000774 RID: 1908
		public HTMLImgEvents_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x04000775 RID: 1909
		public HTMLImgEvents_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x04000776 RID: 1910
		public HTMLImgEvents_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x04000777 RID: 1911
		public HTMLImgEvents_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x04000778 RID: 1912
		public HTMLImgEvents_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x04000779 RID: 1913
		public HTMLImgEvents_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x0400077A RID: 1914
		public HTMLImgEvents_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x0400077B RID: 1915
		public HTMLImgEvents_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x0400077C RID: 1916
		public HTMLImgEvents_onclickEventHandler m_onclickDelegate;

		// Token: 0x0400077D RID: 1917
		public HTMLImgEvents_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x0400077E RID: 1918
		public int m_dwCookie;
	}
}
