using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000268 RID: 616
	public sealed class ChartEmptyPointsInstance : BaseInstance
	{
		// Token: 0x060017F2 RID: 6130 RVA: 0x0006205F File Offset: 0x0006025F
		internal ChartEmptyPointsInstance(ChartEmptyPoints chartEmptyPointsDef)
			: base(chartEmptyPointsDef.ReportScope)
		{
			this.m_chartEmptyPointsDef = chartEmptyPointsDef;
		}

		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x060017F3 RID: 6131 RVA: 0x00062074 File Offset: 0x00060274
		public StyleInstance Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new StyleInstance(this.m_chartEmptyPointsDef, this.m_chartEmptyPointsDef.ChartDef, this.m_chartEmptyPointsDef.ChartDef.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x060017F4 RID: 6132 RVA: 0x000620B0 File Offset: 0x000602B0
		public object AxisLabel
		{
			get
			{
				if (this.m_axisLabel == null && !this.m_chartEmptyPointsDef.ChartDef.IsOldSnapshot)
				{
					this.m_axisLabel = this.m_chartEmptyPointsDef.ChartEmptyPointsDef.EvaluateAxisLabel(this.ReportScopeInstance, this.m_chartEmptyPointsDef.ChartDef.RenderingContext.OdpContext).Value;
				}
				return this.m_axisLabel;
			}
		}

		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x060017F5 RID: 6133 RVA: 0x00062114 File Offset: 0x00060314
		public string ToolTip
		{
			get
			{
				if (this.m_toolTip == null)
				{
					this.m_toolTip = this.m_chartEmptyPointsDef.ChartEmptyPointsDef.EvaluateToolTip(this.ReportScopeInstance, this.m_chartEmptyPointsDef.ChartDef.RenderingContext.OdpContext);
				}
				return this.m_toolTip;
			}
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x00062160 File Offset: 0x00060360
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_axisLabel = null;
			this.m_toolTip = null;
		}

		// Token: 0x04000C0A RID: 3082
		private ChartEmptyPoints m_chartEmptyPointsDef;

		// Token: 0x04000C0B RID: 3083
		private StyleInstance m_style;

		// Token: 0x04000C0C RID: 3084
		private object m_axisLabel;

		// Token: 0x04000C0D RID: 3085
		private string m_toolTip;
	}
}
