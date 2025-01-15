using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DCA RID: 3530
	[ClassInterface(0)]
	internal sealed class HTMLWindowEvents_SinkHelper : HTMLWindowEvents
	{
		// Token: 0x060178BB RID: 96443 RVA: 0x00025C98 File Offset: 0x00024C98
		public override void onafterprint()
		{
			if (this.m_onafterprintDelegate != null)
			{
				this.m_onafterprintDelegate();
				return;
			}
		}

		// Token: 0x060178BC RID: 96444 RVA: 0x00025CC4 File Offset: 0x00024CC4
		public override void onbeforeprint()
		{
			if (this.m_onbeforeprintDelegate != null)
			{
				this.m_onbeforeprintDelegate();
				return;
			}
		}

		// Token: 0x060178BD RID: 96445 RVA: 0x00025CF0 File Offset: 0x00024CF0
		public override void onbeforeunload()
		{
			if (this.m_onbeforeunloadDelegate != null)
			{
				this.m_onbeforeunloadDelegate();
				return;
			}
		}

		// Token: 0x060178BE RID: 96446 RVA: 0x00025D1C File Offset: 0x00024D1C
		public override void onscroll()
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate();
				return;
			}
		}

		// Token: 0x060178BF RID: 96447 RVA: 0x00025D48 File Offset: 0x00024D48
		public override void onresize()
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate();
				return;
			}
		}

		// Token: 0x060178C0 RID: 96448 RVA: 0x00025D74 File Offset: 0x00024D74
		public override void onerror(string A_1, string A_2, int A_3)
		{
			if (this.m_onerrorDelegate != null)
			{
				this.m_onerrorDelegate(A_1, A_2, A_3);
				return;
			}
		}

		// Token: 0x060178C1 RID: 96449 RVA: 0x00025DAC File Offset: 0x00024DAC
		public override void onblur()
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate();
				return;
			}
		}

		// Token: 0x060178C2 RID: 96450 RVA: 0x00025DD8 File Offset: 0x00024DD8
		public override void onfocus()
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate();
				return;
			}
		}

		// Token: 0x060178C3 RID: 96451 RVA: 0x00025E04 File Offset: 0x00024E04
		public override bool onhelp()
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate();
		}

		// Token: 0x060178C4 RID: 96452 RVA: 0x00025E30 File Offset: 0x00024E30
		public override void onunload()
		{
			if (this.m_onunloadDelegate != null)
			{
				this.m_onunloadDelegate();
				return;
			}
		}

		// Token: 0x060178C5 RID: 96453 RVA: 0x00025E5C File Offset: 0x00024E5C
		public override void onload()
		{
			if (this.m_onloadDelegate != null)
			{
				this.m_onloadDelegate();
				return;
			}
		}

		// Token: 0x060178C6 RID: 96454 RVA: 0x00025E88 File Offset: 0x00024E88
		internal HTMLWindowEvents_SinkHelper()
		{
			this.m_dwCookie = 0;
			this.m_onafterprintDelegate = null;
			this.m_onbeforeprintDelegate = null;
			this.m_onbeforeunloadDelegate = null;
			this.m_onscrollDelegate = null;
			this.m_onresizeDelegate = null;
			this.m_onerrorDelegate = null;
			this.m_onblurDelegate = null;
			this.m_onfocusDelegate = null;
			this.m_onhelpDelegate = null;
			this.m_onunloadDelegate = null;
			this.m_onloadDelegate = null;
		}

		// Token: 0x04000617 RID: 1559
		public HTMLWindowEvents_onafterprintEventHandler m_onafterprintDelegate;

		// Token: 0x04000618 RID: 1560
		public HTMLWindowEvents_onbeforeprintEventHandler m_onbeforeprintDelegate;

		// Token: 0x04000619 RID: 1561
		public HTMLWindowEvents_onbeforeunloadEventHandler m_onbeforeunloadDelegate;

		// Token: 0x0400061A RID: 1562
		public HTMLWindowEvents_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x0400061B RID: 1563
		public HTMLWindowEvents_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x0400061C RID: 1564
		public HTMLWindowEvents_onerrorEventHandler m_onerrorDelegate;

		// Token: 0x0400061D RID: 1565
		public HTMLWindowEvents_onblurEventHandler m_onblurDelegate;

		// Token: 0x0400061E RID: 1566
		public HTMLWindowEvents_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x0400061F RID: 1567
		public HTMLWindowEvents_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x04000620 RID: 1568
		public HTMLWindowEvents_onunloadEventHandler m_onunloadDelegate;

		// Token: 0x04000621 RID: 1569
		public HTMLWindowEvents_onloadEventHandler m_onloadDelegate;

		// Token: 0x04000622 RID: 1570
		public int m_dwCookie;
	}
}
