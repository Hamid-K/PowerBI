using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000058 RID: 88
	public abstract class JoinConditionExprHost : ReportObjectModelProxy
	{
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00002F4B File Offset: 0x0000114B
		public virtual object ForeignKeyExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00002F4E File Offset: 0x0000114E
		public virtual object PrimaryKeyExpr
		{
			get
			{
				return null;
			}
		}
	}
}
