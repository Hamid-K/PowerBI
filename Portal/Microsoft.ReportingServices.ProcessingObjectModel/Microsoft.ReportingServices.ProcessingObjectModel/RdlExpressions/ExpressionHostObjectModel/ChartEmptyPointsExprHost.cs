using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000AE RID: 174
	public abstract class ChartEmptyPointsExprHost : StyleExprHost
	{
		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x00003831 File Offset: 0x00001A31
		public virtual object AxisLabelExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x00003834 File Offset: 0x00001A34
		internal IList<DataValueExprHost> CustomPropertyHostsRemotable
		{
			get
			{
				return this.m_customPropertyHostsRemotable;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0000383C File Offset: 0x00001A3C
		public virtual object ToolTipExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000120 RID: 288
		public ChartMarkerExprHost ChartMarkerHost;

		// Token: 0x04000121 RID: 289
		public ChartDataLabelExprHost DataLabelHost;

		// Token: 0x04000122 RID: 290
		public ChartDataPointInLegendExprHost DataPointInLegendHost;

		// Token: 0x04000123 RID: 291
		public ActionInfoExprHost ActionInfoHost;

		// Token: 0x04000124 RID: 292
		[CLSCompliant(false)]
		protected IList<DataValueExprHost> m_customPropertyHostsRemotable;
	}
}
