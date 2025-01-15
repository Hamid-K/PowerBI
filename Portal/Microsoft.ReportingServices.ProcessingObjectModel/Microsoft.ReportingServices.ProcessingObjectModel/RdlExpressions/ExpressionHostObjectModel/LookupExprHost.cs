using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x0200004D RID: 77
	public abstract class LookupExprHost : ReportObjectModelProxy
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00002CE3 File Offset: 0x00000EE3
		public virtual object SourceExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00002CE6 File Offset: 0x00000EE6
		public virtual object ResultExpr
		{
			get
			{
				return null;
			}
		}
	}
}
