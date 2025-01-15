using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000061 RID: 97
	public abstract class ImageExprHost : ReportItemExprHost
	{
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000201 RID: 513 RVA: 0x0000308F File Offset: 0x0000128F
		public virtual object ValueExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00003092 File Offset: 0x00001292
		public virtual object MIMETypeExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00003095 File Offset: 0x00001295
		public virtual object TagExpr
		{
			get
			{
				return null;
			}
		}
	}
}
