using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x0200005A RID: 90
	public abstract class ParamExprHost : ReportObjectModelProxy
	{
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00002F64 File Offset: 0x00001164
		public virtual object ValueExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00002F67 File Offset: 0x00001167
		public virtual object OmitExpr
		{
			get
			{
				return null;
			}
		}
	}
}
