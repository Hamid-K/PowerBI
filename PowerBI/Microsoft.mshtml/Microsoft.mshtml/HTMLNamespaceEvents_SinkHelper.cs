using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DE8 RID: 3560
	[ClassInterface(0)]
	internal sealed class HTMLNamespaceEvents_SinkHelper : HTMLNamespaceEvents
	{
		// Token: 0x06018317 RID: 99095 RVA: 0x0008419C File Offset: 0x0008319C
		public override void onreadystatechange(IHTMLEventObj A_1)
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06018318 RID: 99096 RVA: 0x000841CC File Offset: 0x000831CC
		internal HTMLNamespaceEvents_SinkHelper()
		{
			this.m_dwCookie = 0;
			this.m_onreadystatechangeDelegate = null;
		}

		// Token: 0x040009AE RID: 2478
		public HTMLNamespaceEvents_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x040009AF RID: 2479
		public int m_dwCookie;
	}
}
