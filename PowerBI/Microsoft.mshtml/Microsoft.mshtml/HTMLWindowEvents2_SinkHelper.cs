using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DF4 RID: 3572
	[ClassInterface(0)]
	internal sealed class HTMLWindowEvents2_SinkHelper : HTMLWindowEvents2
	{
		// Token: 0x060186F2 RID: 100082 RVA: 0x000A738C File Offset: 0x000A638C
		public override void onafterprint(IHTMLEventObj A_1)
		{
			if (this.m_onafterprintDelegate != null)
			{
				this.m_onafterprintDelegate(A_1);
				return;
			}
		}

		// Token: 0x060186F3 RID: 100083 RVA: 0x000A73BC File Offset: 0x000A63BC
		public override void onbeforeprint(IHTMLEventObj A_1)
		{
			if (this.m_onbeforeprintDelegate != null)
			{
				this.m_onbeforeprintDelegate(A_1);
				return;
			}
		}

		// Token: 0x060186F4 RID: 100084 RVA: 0x000A73EC File Offset: 0x000A63EC
		public override void onbeforeunload(IHTMLEventObj A_1)
		{
			if (this.m_onbeforeunloadDelegate != null)
			{
				this.m_onbeforeunloadDelegate(A_1);
				return;
			}
		}

		// Token: 0x060186F5 RID: 100085 RVA: 0x000A741C File Offset: 0x000A641C
		public override void onscroll(IHTMLEventObj A_1)
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate(A_1);
				return;
			}
		}

		// Token: 0x060186F6 RID: 100086 RVA: 0x000A744C File Offset: 0x000A644C
		public override void onresize(IHTMLEventObj A_1)
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate(A_1);
				return;
			}
		}

		// Token: 0x060186F7 RID: 100087 RVA: 0x000A747C File Offset: 0x000A647C
		public override void onerror(string A_1, string A_2, int A_3)
		{
			if (this.m_onerrorDelegate != null)
			{
				this.m_onerrorDelegate(A_1, A_2, A_3);
				return;
			}
		}

		// Token: 0x060186F8 RID: 100088 RVA: 0x000A74B4 File Offset: 0x000A64B4
		public override void onblur(IHTMLEventObj A_1)
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate(A_1);
				return;
			}
		}

		// Token: 0x060186F9 RID: 100089 RVA: 0x000A74E4 File Offset: 0x000A64E4
		public override void onfocus(IHTMLEventObj A_1)
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate(A_1);
				return;
			}
		}

		// Token: 0x060186FA RID: 100090 RVA: 0x000A7514 File Offset: 0x000A6514
		public override bool onhelp(IHTMLEventObj A_1)
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate(A_1);
		}

		// Token: 0x060186FB RID: 100091 RVA: 0x000A7544 File Offset: 0x000A6544
		public override void onunload(IHTMLEventObj A_1)
		{
			if (this.m_onunloadDelegate != null)
			{
				this.m_onunloadDelegate(A_1);
				return;
			}
		}

		// Token: 0x060186FC RID: 100092 RVA: 0x000A7574 File Offset: 0x000A6574
		public override void onload(IHTMLEventObj A_1)
		{
			if (this.m_onloadDelegate != null)
			{
				this.m_onloadDelegate(A_1);
				return;
			}
		}

		// Token: 0x060186FD RID: 100093 RVA: 0x000A75A4 File Offset: 0x000A65A4
		internal HTMLWindowEvents2_SinkHelper()
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

		// Token: 0x04000B05 RID: 2821
		public HTMLWindowEvents2_onafterprintEventHandler m_onafterprintDelegate;

		// Token: 0x04000B06 RID: 2822
		public HTMLWindowEvents2_onbeforeprintEventHandler m_onbeforeprintDelegate;

		// Token: 0x04000B07 RID: 2823
		public HTMLWindowEvents2_onbeforeunloadEventHandler m_onbeforeunloadDelegate;

		// Token: 0x04000B08 RID: 2824
		public HTMLWindowEvents2_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x04000B09 RID: 2825
		public HTMLWindowEvents2_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x04000B0A RID: 2826
		public HTMLWindowEvents2_onerrorEventHandler m_onerrorDelegate;

		// Token: 0x04000B0B RID: 2827
		public HTMLWindowEvents2_onblurEventHandler m_onblurDelegate;

		// Token: 0x04000B0C RID: 2828
		public HTMLWindowEvents2_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x04000B0D RID: 2829
		public HTMLWindowEvents2_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x04000B0E RID: 2830
		public HTMLWindowEvents2_onunloadEventHandler m_onunloadDelegate;

		// Token: 0x04000B0F RID: 2831
		public HTMLWindowEvents2_onloadEventHandler m_onloadDelegate;

		// Token: 0x04000B10 RID: 2832
		public int m_dwCookie;
	}
}
