using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000049 RID: 73
	public abstract class CustomCodeProxyBase
	{
		// Token: 0x06000142 RID: 322 RVA: 0x00002BAE File Offset: 0x00000DAE
		protected CustomCodeProxyBase(IReportObjectModelProxyForCustomCode reportObjectModel)
		{
			this.m_reportObjectModel = reportObjectModel;
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00002BBD File Offset: 0x00000DBD
		protected IReportObjectModelProxyForCustomCode Report
		{
			get
			{
				return this.m_reportObjectModel;
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00002BC5 File Offset: 0x00000DC5
		protected virtual void OnInit()
		{
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00002BC7 File Offset: 0x00000DC7
		internal void CallOnInit()
		{
			this.OnInit();
		}

		// Token: 0x0400007A RID: 122
		private IReportObjectModelProxyForCustomCode m_reportObjectModel;
	}
}
