using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000B3 RID: 179
	public abstract class DataValueExprHost : ReportObjectModelProxy
	{
		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x00003916 File Offset: 0x00001B16
		public virtual object DataValueNameExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x00003919 File Offset: 0x00001B19
		public virtual object DataValueValueExpr
		{
			get
			{
				return null;
			}
		}
	}
}
