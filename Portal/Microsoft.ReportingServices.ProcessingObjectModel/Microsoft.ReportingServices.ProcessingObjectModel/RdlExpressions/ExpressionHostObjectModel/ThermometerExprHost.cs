using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000094 RID: 148
	public abstract class ThermometerExprHost : StyleExprHost
	{
		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000310 RID: 784 RVA: 0x00003542 File Offset: 0x00001742
		public virtual object BulbOffsetExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00003545 File Offset: 0x00001745
		public virtual object BulbSizeExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000312 RID: 786 RVA: 0x00003548 File Offset: 0x00001748
		public virtual object ThermometerStyleExpr
		{
			get
			{
				return null;
			}
		}
	}
}
