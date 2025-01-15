using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000093 RID: 147
	public abstract class IndicatorStateExprHost : ReportObjectModelProxy
	{
		// Token: 0x1700023A RID: 570
		// (get) Token: 0x0600030C RID: 780 RVA: 0x00003531 File Offset: 0x00001731
		public virtual object ColorExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00003534 File Offset: 0x00001734
		public virtual object ScaleFactorExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00003537 File Offset: 0x00001737
		public virtual object IndicatorStyleExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040000F4 RID: 244
		public GaugeInputValueExprHost StartValueHost;

		// Token: 0x040000F5 RID: 245
		public GaugeInputValueExprHost EndValueHost;

		// Token: 0x040000F6 RID: 246
		public IndicatorImageExprHost IndicatorImageHost;
	}
}
