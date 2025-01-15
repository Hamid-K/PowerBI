using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000EF RID: 239
	public abstract class PageBreakExprHost : ReportObjectModelProxy
	{
		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x00003D49 File Offset: 0x00001F49
		public virtual object DisabledExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x00003D4C File Offset: 0x00001F4C
		public virtual object ResetPageNumberExpr
		{
			get
			{
				return null;
			}
		}
	}
}
