using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000089 RID: 137
	public abstract class NumericIndicatorRangeExprHost : ReportObjectModelProxy
	{
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000343C File Offset: 0x0000163C
		public virtual object DecimalDigitColorExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000343F File Offset: 0x0000163F
		public virtual object DigitColorExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040000E5 RID: 229
		public GaugeInputValueExprHost StartValueHost;

		// Token: 0x040000E6 RID: 230
		public GaugeInputValueExprHost EndValueHost;
	}
}
