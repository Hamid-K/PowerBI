using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000A0 RID: 160
	public abstract class ChartLegendColumnExprHost : StyleExprHost
	{
		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000366 RID: 870 RVA: 0x000036B7 File Offset: 0x000018B7
		public virtual object ColumnTypeExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000367 RID: 871 RVA: 0x000036BA File Offset: 0x000018BA
		public virtual object ValueExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000368 RID: 872 RVA: 0x000036BD File Offset: 0x000018BD
		public virtual object ToolTipExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000369 RID: 873 RVA: 0x000036C0 File Offset: 0x000018C0
		public virtual object MinimumWidthExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x0600036A RID: 874 RVA: 0x000036C3 File Offset: 0x000018C3
		public virtual object MaximumWidthExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x0600036B RID: 875 RVA: 0x000036C6 File Offset: 0x000018C6
		public virtual object SeriesSymbolWidthExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x0600036C RID: 876 RVA: 0x000036C9 File Offset: 0x000018C9
		public virtual object SeriesSymbolHeightExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400010D RID: 269
		public ChartLegendColumnHeaderExprHost HeaderHost;

		// Token: 0x0400010E RID: 270
		public ActionInfoExprHost ActionInfoHost;
	}
}
