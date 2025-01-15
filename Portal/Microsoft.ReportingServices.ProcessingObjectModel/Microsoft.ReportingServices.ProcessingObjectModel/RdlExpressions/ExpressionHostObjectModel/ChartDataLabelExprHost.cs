using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000AC RID: 172
	public abstract class ChartDataLabelExprHost : StyleExprHost
	{
		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0000380C File Offset: 0x00001A0C
		public virtual object VisibleExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0000380F File Offset: 0x00001A0F
		public virtual object LabelExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060003BF RID: 959 RVA: 0x00003812 File Offset: 0x00001A12
		public virtual object ChartDataLabelPositionExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x00003815 File Offset: 0x00001A15
		public virtual object RotationExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x00003818 File Offset: 0x00001A18
		public virtual object UseValueAsLabelExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000381B File Offset: 0x00001A1B
		public virtual object ToolTipExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400011F RID: 287
		public ActionInfoExprHost ActionInfoHost;
	}
}
