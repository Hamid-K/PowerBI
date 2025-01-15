using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x0200009E RID: 158
	public abstract class ChartDataPointInLegendExprHost : StyleExprHost
	{
		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000369B File Offset: 0x0000189B
		public virtual object LegendTextExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000369E File Offset: 0x0000189E
		public virtual object ToolTipExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000362 RID: 866 RVA: 0x000036A1 File Offset: 0x000018A1
		public virtual object HiddenExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400010C RID: 268
		public ActionInfoExprHost ActionInfoHost;
	}
}
