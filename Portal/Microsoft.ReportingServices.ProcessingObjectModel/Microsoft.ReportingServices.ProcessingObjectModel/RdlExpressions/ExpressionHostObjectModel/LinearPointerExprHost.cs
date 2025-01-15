using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000086 RID: 134
	public abstract class LinearPointerExprHost : GaugePointerExprHost
	{
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x000033D5 File Offset: 0x000015D5
		public virtual object TypeExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040000DF RID: 223
		public ThermometerExprHost ThermometerHost;
	}
}
