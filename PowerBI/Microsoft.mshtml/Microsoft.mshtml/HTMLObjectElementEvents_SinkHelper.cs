using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000E1A RID: 3610
	[ClassInterface(0)]
	internal sealed class HTMLObjectElementEvents_SinkHelper : HTMLObjectElementEvents
	{
		// Token: 0x0601946E RID: 103534 RVA: 0x00121ED0 File Offset: 0x00120ED0
		public override void onreadystatechange()
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate();
				return;
			}
		}

		// Token: 0x0601946F RID: 103535 RVA: 0x00121EFC File Offset: 0x00120EFC
		public override void oncellchange()
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate();
				return;
			}
		}

		// Token: 0x06019470 RID: 103536 RVA: 0x00121F28 File Offset: 0x00120F28
		public override void onrowsinserted()
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate();
				return;
			}
		}

		// Token: 0x06019471 RID: 103537 RVA: 0x00121F54 File Offset: 0x00120F54
		public override void onrowsdelete()
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate();
				return;
			}
		}

		// Token: 0x06019472 RID: 103538 RVA: 0x00121F80 File Offset: 0x00120F80
		public override bool onerror()
		{
			return this.m_onerrorDelegate != null && this.m_onerrorDelegate();
		}

		// Token: 0x06019473 RID: 103539 RVA: 0x00121FAC File Offset: 0x00120FAC
		public override void ondatasetcomplete()
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate();
				return;
			}
		}

		// Token: 0x06019474 RID: 103540 RVA: 0x00121FD8 File Offset: 0x00120FD8
		public override void ondataavailable()
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate();
				return;
			}
		}

		// Token: 0x06019475 RID: 103541 RVA: 0x00122004 File Offset: 0x00121004
		public override void ondatasetchanged()
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate();
				return;
			}
		}

		// Token: 0x06019476 RID: 103542 RVA: 0x00122030 File Offset: 0x00121030
		public override void onrowenter()
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate();
				return;
			}
		}

		// Token: 0x06019477 RID: 103543 RVA: 0x0012205C File Offset: 0x0012105C
		public override bool onrowexit()
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate();
		}

		// Token: 0x06019478 RID: 103544 RVA: 0x00122088 File Offset: 0x00121088
		public override bool onerrorupdate()
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate();
		}

		// Token: 0x06019479 RID: 103545 RVA: 0x001220B4 File Offset: 0x001210B4
		public override void onafterupdate()
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate();
				return;
			}
		}

		// Token: 0x0601947A RID: 103546 RVA: 0x001220E0 File Offset: 0x001210E0
		public override bool onbeforeupdate()
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate();
		}

		// Token: 0x0601947B RID: 103547 RVA: 0x0012210C File Offset: 0x0012110C
		internal HTMLObjectElementEvents_SinkHelper()
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

		// Token: 0x04000FB0 RID: 4016
		public HTMLObjectElementEvents_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x04000FB1 RID: 4017
		public HTMLObjectElementEvents_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x04000FB2 RID: 4018
		public HTMLObjectElementEvents_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x04000FB3 RID: 4019
		public HTMLObjectElementEvents_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x04000FB4 RID: 4020
		public HTMLObjectElementEvents_onerrorEventHandler m_onerrorDelegate;

		// Token: 0x04000FB5 RID: 4021
		public HTMLObjectElementEvents_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x04000FB6 RID: 4022
		public HTMLObjectElementEvents_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x04000FB7 RID: 4023
		public HTMLObjectElementEvents_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x04000FB8 RID: 4024
		public HTMLObjectElementEvents_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x04000FB9 RID: 4025
		public HTMLObjectElementEvents_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x04000FBA RID: 4026
		public HTMLObjectElementEvents_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x04000FBB RID: 4027
		public HTMLObjectElementEvents_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x04000FBC RID: 4028
		public HTMLObjectElementEvents_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x04000FBD RID: 4029
		public int m_dwCookie;
	}
}
