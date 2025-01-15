using System;
using System.Globalization;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000077 RID: 119
	internal sealed class ThreadWorkItemCulture
	{
		// Token: 0x060003D3 RID: 979 RVA: 0x000101E3 File Offset: 0x0000E3E3
		public void Capture()
		{
			this.m_uiCulture = Thread.CurrentThread.CurrentUICulture;
			this.m_threadCulture = Thread.CurrentThread.CurrentCulture;
			this.m_clientCulture = CallContext.GetData(Localization.ClientPrimaryCultureKey);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00010215 File Offset: 0x0000E415
		public void Restore()
		{
			Thread.CurrentThread.CurrentUICulture = this.m_uiCulture;
			Thread.CurrentThread.CurrentCulture = this.m_threadCulture;
			CallContext.SetData(Localization.ClientPrimaryCultureKey, this.m_clientCulture);
		}

		// Token: 0x04000368 RID: 872
		private CultureInfo m_uiCulture;

		// Token: 0x04000369 RID: 873
		private CultureInfo m_threadCulture;

		// Token: 0x0400036A RID: 874
		private object m_clientCulture;
	}
}
