using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DD8 RID: 3544
	[ClassInterface(0)]
	internal sealed class HTMLObjectElementEvents2_SinkHelper : HTMLObjectElementEvents2
	{
		// Token: 0x06017D97 RID: 97687 RVA: 0x00051FA4 File Offset: 0x00050FA4
		public override void onreadystatechange(IHTMLEventObj A_1)
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017D98 RID: 97688 RVA: 0x00051FD4 File Offset: 0x00050FD4
		public override void oncellchange(IHTMLEventObj A_1)
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017D99 RID: 97689 RVA: 0x00052004 File Offset: 0x00051004
		public override void onrowsinserted(IHTMLEventObj A_1)
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017D9A RID: 97690 RVA: 0x00052034 File Offset: 0x00051034
		public override void onrowsdelete(IHTMLEventObj A_1)
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017D9B RID: 97691 RVA: 0x00052064 File Offset: 0x00051064
		public override bool onerror(IHTMLEventObj A_1)
		{
			return this.m_onerrorDelegate != null && this.m_onerrorDelegate(A_1);
		}

		// Token: 0x06017D9C RID: 97692 RVA: 0x00052094 File Offset: 0x00051094
		public override void ondatasetcomplete(IHTMLEventObj A_1)
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017D9D RID: 97693 RVA: 0x000520C4 File Offset: 0x000510C4
		public override void ondataavailable(IHTMLEventObj A_1)
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017D9E RID: 97694 RVA: 0x000520F4 File Offset: 0x000510F4
		public override void ondatasetchanged(IHTMLEventObj A_1)
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017D9F RID: 97695 RVA: 0x00052124 File Offset: 0x00051124
		public override void onrowenter(IHTMLEventObj A_1)
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017DA0 RID: 97696 RVA: 0x00052154 File Offset: 0x00051154
		public override bool onrowexit(IHTMLEventObj A_1)
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate(A_1);
		}

		// Token: 0x06017DA1 RID: 97697 RVA: 0x00052184 File Offset: 0x00051184
		public override bool onerrorupdate(IHTMLEventObj A_1)
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate(A_1);
		}

		// Token: 0x06017DA2 RID: 97698 RVA: 0x000521B4 File Offset: 0x000511B4
		public override void onafterupdate(IHTMLEventObj A_1)
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017DA3 RID: 97699 RVA: 0x000521E4 File Offset: 0x000511E4
		public override bool onbeforeupdate(IHTMLEventObj A_1)
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate(A_1);
		}

		// Token: 0x06017DA4 RID: 97700 RVA: 0x00052214 File Offset: 0x00051214
		internal HTMLObjectElementEvents2_SinkHelper()
		{
			this.m_dwCookie = 0;
			this.m_onreadystatechangeDelegate = null;
			this.m_oncellchangeDelegate = null;
			this.m_onrowsinsertedDelegate = null;
			this.m_onrowsdeleteDelegate = null;
			this.m_onerrorDelegate = null;
			this.m_ondatasetcompleteDelegate = null;
			this.m_ondataavailableDelegate = null;
			this.m_ondatasetchangedDelegate = null;
			this.m_onrowenterDelegate = null;
			this.m_onrowexitDelegate = null;
			this.m_onerrorupdateDelegate = null;
			this.m_onafterupdateDelegate = null;
			this.m_onbeforeupdateDelegate = null;
		}

		// Token: 0x040007C6 RID: 1990
		public HTMLObjectElementEvents2_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x040007C7 RID: 1991
		public HTMLObjectElementEvents2_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x040007C8 RID: 1992
		public HTMLObjectElementEvents2_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x040007C9 RID: 1993
		public HTMLObjectElementEvents2_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x040007CA RID: 1994
		public HTMLObjectElementEvents2_onerrorEventHandler m_onerrorDelegate;

		// Token: 0x040007CB RID: 1995
		public HTMLObjectElementEvents2_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x040007CC RID: 1996
		public HTMLObjectElementEvents2_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x040007CD RID: 1997
		public HTMLObjectElementEvents2_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x040007CE RID: 1998
		public HTMLObjectElementEvents2_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x040007CF RID: 1999
		public HTMLObjectElementEvents2_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x040007D0 RID: 2000
		public HTMLObjectElementEvents2_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x040007D1 RID: 2001
		public HTMLObjectElementEvents2_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x040007D2 RID: 2002
		public HTMLObjectElementEvents2_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x040007D3 RID: 2003
		public int m_dwCookie;
	}
}
