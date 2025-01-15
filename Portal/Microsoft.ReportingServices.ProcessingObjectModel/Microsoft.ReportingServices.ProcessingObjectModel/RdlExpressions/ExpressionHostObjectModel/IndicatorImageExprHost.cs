using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x0200007A RID: 122
	public abstract class IndicatorImageExprHost : BaseGaugeImageExprHost
	{
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x0600026E RID: 622 RVA: 0x000032B2 File Offset: 0x000014B2
		public virtual object HueColorExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600026F RID: 623 RVA: 0x000032B5 File Offset: 0x000014B5
		public virtual object TransparencyExpr
		{
			get
			{
				return null;
			}
		}
	}
}
