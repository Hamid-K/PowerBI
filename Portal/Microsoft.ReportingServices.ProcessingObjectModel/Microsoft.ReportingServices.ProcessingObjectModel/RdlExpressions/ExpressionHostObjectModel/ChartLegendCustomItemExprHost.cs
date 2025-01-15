using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000A7 RID: 167
	public abstract class ChartLegendCustomItemExprHost : StyleExprHost
	{
		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x00003794 File Offset: 0x00001994
		public virtual object SeparatorExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x00003797 File Offset: 0x00001997
		public virtual object SeparatorColorExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x0000379A File Offset: 0x0000199A
		public virtual object ToolTipExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000379D File Offset: 0x0000199D
		internal IList<ChartLegendCustomItemCellExprHost> ChartLegendCustomItemCellsHostsRemotable
		{
			get
			{
				return this.m_legendCustomItemCellsHostsRemotable;
			}
		}

		// Token: 0x04000112 RID: 274
		public ChartMarkerExprHost ChartMarkerHost;

		// Token: 0x04000113 RID: 275
		public ActionInfoExprHost ActionInfoHost;

		// Token: 0x04000114 RID: 276
		[CLSCompliant(false)]
		protected IList<ChartLegendCustomItemCellExprHost> m_legendCustomItemCellsHostsRemotable;
	}
}
