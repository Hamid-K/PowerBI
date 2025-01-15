using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000264 RID: 612
	public sealed class ChartGridLinesInstance : BaseInstance
	{
		// Token: 0x060017D2 RID: 6098 RVA: 0x000614D2 File Offset: 0x0005F6D2
		internal ChartGridLinesInstance(ChartGridLines gridlinesDef)
			: base(gridlinesDef.ChartDef)
		{
			this.m_gridLinesDef = gridlinesDef;
		}

		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x060017D3 RID: 6099 RVA: 0x000614E7 File Offset: 0x0005F6E7
		public StyleInstance Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new StyleInstance(this.m_gridLinesDef, this.m_gridLinesDef.ChartDef, this.m_gridLinesDef.ChartDef.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x060017D4 RID: 6100 RVA: 0x00061524 File Offset: 0x0005F724
		public ChartAutoBool Enabled
		{
			get
			{
				if (this.m_enabled == null && !this.m_gridLinesDef.ChartDef.IsOldSnapshot)
				{
					this.m_enabled = new ChartAutoBool?(this.m_gridLinesDef.ChartGridLinesDef.EvaluateEnabled(this.ReportScopeInstance, this.m_gridLinesDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_enabled.Value;
			}
		}

		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x060017D5 RID: 6101 RVA: 0x00061594 File Offset: 0x0005F794
		public double Interval
		{
			get
			{
				if (this.m_interval == null && !this.m_gridLinesDef.ChartDef.IsOldSnapshot)
				{
					this.m_interval = new double?(this.m_gridLinesDef.ChartGridLinesDef.EvaluateInterval(this.ReportScopeInstance, this.m_gridLinesDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_interval.Value;
			}
		}

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x060017D6 RID: 6102 RVA: 0x00061604 File Offset: 0x0005F804
		public double IntervalOffset
		{
			get
			{
				if (this.m_intervalOffset == null && !this.m_gridLinesDef.ChartDef.IsOldSnapshot)
				{
					this.m_intervalOffset = new double?(this.m_gridLinesDef.ChartGridLinesDef.EvaluateIntervalOffset(this.ReportScopeInstance, this.m_gridLinesDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_intervalOffset.Value;
			}
		}

		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x060017D7 RID: 6103 RVA: 0x00061674 File Offset: 0x0005F874
		public ChartIntervalType IntervalType
		{
			get
			{
				if (this.m_intervalType == null && !this.m_gridLinesDef.ChartDef.IsOldSnapshot)
				{
					this.m_intervalType = new ChartIntervalType?(this.m_gridLinesDef.ChartGridLinesDef.EvaluateIntervalType(this.ReportScopeInstance, this.m_gridLinesDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_intervalType.Value;
			}
		}

		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x060017D8 RID: 6104 RVA: 0x000616E4 File Offset: 0x0005F8E4
		public ChartIntervalType IntervalOffsetType
		{
			get
			{
				if (this.m_intervalOffsetType == null && !this.m_gridLinesDef.ChartDef.IsOldSnapshot)
				{
					this.m_intervalOffsetType = new ChartIntervalType?(this.m_gridLinesDef.ChartGridLinesDef.EvaluateIntervalOffsetType(this.ReportScopeInstance, this.m_gridLinesDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_intervalOffsetType.Value;
			}
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x00061754 File Offset: 0x0005F954
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_enabled = null;
			this.m_interval = null;
			this.m_intervalOffset = null;
			this.m_intervalType = null;
			this.m_intervalOffsetType = null;
		}

		// Token: 0x04000BEE RID: 3054
		private ChartGridLines m_gridLinesDef;

		// Token: 0x04000BEF RID: 3055
		private StyleInstance m_style;

		// Token: 0x04000BF0 RID: 3056
		private ChartAutoBool? m_enabled;

		// Token: 0x04000BF1 RID: 3057
		private double? m_interval;

		// Token: 0x04000BF2 RID: 3058
		private double? m_intervalOffset;

		// Token: 0x04000BF3 RID: 3059
		private ChartIntervalType? m_intervalType;

		// Token: 0x04000BF4 RID: 3060
		private ChartIntervalType? m_intervalOffsetType;
	}
}
