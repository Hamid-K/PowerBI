using System;

namespace Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel
{
	// Token: 0x0200001F RID: 31
	public abstract class CustomCodeProxyBase
	{
		// Token: 0x0600007D RID: 125 RVA: 0x0000225D File Offset: 0x0000045D
		protected CustomCodeProxyBase(IReportObjectModelProxyForCustomCode reportObjectModel)
		{
			this.m_reportObjectModel = reportObjectModel;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600007E RID: 126 RVA: 0x0000226C File Offset: 0x0000046C
		protected IReportObjectModelProxyForCustomCode Report
		{
			get
			{
				return this.m_reportObjectModel;
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002274 File Offset: 0x00000474
		protected virtual void OnInit()
		{
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002276 File Offset: 0x00000476
		internal void CallOnInit()
		{
			this.OnInit();
		}

		// Token: 0x04000003 RID: 3
		private IReportObjectModelProxyForCustomCode m_reportObjectModel;
	}
}
