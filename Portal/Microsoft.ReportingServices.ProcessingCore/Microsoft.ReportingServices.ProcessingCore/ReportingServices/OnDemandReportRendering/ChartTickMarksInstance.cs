using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000266 RID: 614
	public sealed class ChartTickMarksInstance : BaseInstance
	{
		// Token: 0x060017E3 RID: 6115 RVA: 0x00061B2C File Offset: 0x0005FD2C
		internal ChartTickMarksInstance(ChartTickMarks chartTickMarksDef)
			: base(chartTickMarksDef.ChartDef)
		{
			this.m_chartTickMarksDef = chartTickMarksDef;
		}

		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x060017E4 RID: 6116 RVA: 0x00061B41 File Offset: 0x0005FD41
		public StyleInstance Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new StyleInstance(this.m_chartTickMarksDef, this.m_chartTickMarksDef.ChartDef, this.m_chartTickMarksDef.ChartDef.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x060017E5 RID: 6117 RVA: 0x00061B80 File Offset: 0x0005FD80
		public ChartAutoBool Enabled
		{
			get
			{
				if (this.m_enabled == null && !this.m_chartTickMarksDef.ChartDef.IsOldSnapshot)
				{
					string text = this.m_chartTickMarksDef.ChartTickMarksDef.EvaluateEnabled(this.ReportScopeInstance, this.m_chartTickMarksDef.ChartDef.RenderingContext.OdpContext);
					this.m_enabled = new ChartAutoBool?(EnumTranslator.TranslateChartAutoBool(text, this.m_chartTickMarksDef.ChartDef.RenderingContext.OdpContext.ReportRuntime));
				}
				return this.m_enabled.Value;
			}
		}

		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x060017E6 RID: 6118 RVA: 0x00061C10 File Offset: 0x0005FE10
		public ChartTickMarksType Type
		{
			get
			{
				if (this.m_type == null && !this.m_chartTickMarksDef.ChartDef.IsOldSnapshot)
				{
					this.m_type = new ChartTickMarksType?(this.m_chartTickMarksDef.ChartTickMarksDef.EvaluateType(this.ReportScopeInstance, this.m_chartTickMarksDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_type.Value;
			}
		}

		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x060017E7 RID: 6119 RVA: 0x00061C80 File Offset: 0x0005FE80
		public double Length
		{
			get
			{
				if (this.m_length == null && !this.m_chartTickMarksDef.ChartDef.IsOldSnapshot)
				{
					this.m_length = new double?(this.m_chartTickMarksDef.ChartTickMarksDef.EvaluateLength(this.ReportScopeInstance, this.m_chartTickMarksDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_length.Value;
			}
		}

		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x060017E8 RID: 6120 RVA: 0x00061CF0 File Offset: 0x0005FEF0
		public double Interval
		{
			get
			{
				if (this.m_interval == null && !this.m_chartTickMarksDef.ChartDef.IsOldSnapshot)
				{
					this.m_interval = new double?(this.m_chartTickMarksDef.ChartTickMarksDef.EvaluateInterval(this.ReportScopeInstance, this.m_chartTickMarksDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_interval.Value;
			}
		}

		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x060017E9 RID: 6121 RVA: 0x00061D60 File Offset: 0x0005FF60
		public ChartIntervalType IntervalType
		{
			get
			{
				if (this.m_intervalType == null && !this.m_chartTickMarksDef.ChartDef.IsOldSnapshot)
				{
					this.m_intervalType = new ChartIntervalType?(this.m_chartTickMarksDef.ChartTickMarksDef.EvaluateIntervalType(this.ReportScopeInstance, this.m_chartTickMarksDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_intervalType.Value;
			}
		}

		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x060017EA RID: 6122 RVA: 0x00061DD0 File Offset: 0x0005FFD0
		public double IntervalOffset
		{
			get
			{
				if (this.m_intervalOffset == null && !this.m_chartTickMarksDef.ChartDef.IsOldSnapshot)
				{
					this.m_intervalOffset = new double?(this.m_chartTickMarksDef.ChartTickMarksDef.EvaluateIntervalOffset(this.ReportScopeInstance, this.m_chartTickMarksDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_intervalOffset.Value;
			}
		}

		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x060017EB RID: 6123 RVA: 0x00061E40 File Offset: 0x00060040
		public ChartIntervalType IntervalOffsetType
		{
			get
			{
				if (this.m_intervalOffsetType == null && !this.m_chartTickMarksDef.ChartDef.IsOldSnapshot)
				{
					this.m_intervalOffsetType = new ChartIntervalType?(this.m_chartTickMarksDef.ChartTickMarksDef.EvaluateIntervalOffsetType(this.ReportScopeInstance, this.m_chartTickMarksDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_intervalOffsetType.Value;
			}
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x00061EB0 File Offset: 0x000600B0
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_enabled = null;
			this.m_type = null;
			this.m_length = null;
			this.m_interval = null;
			this.m_intervalType = null;
			this.m_intervalOffset = null;
			this.m_intervalOffsetType = null;
		}

		// Token: 0x04000BFD RID: 3069
		private ChartTickMarks m_chartTickMarksDef;

		// Token: 0x04000BFE RID: 3070
		private StyleInstance m_style;

		// Token: 0x04000BFF RID: 3071
		private ChartAutoBool? m_enabled;

		// Token: 0x04000C00 RID: 3072
		private ChartTickMarksType? m_type;

		// Token: 0x04000C01 RID: 3073
		private double? m_length;

		// Token: 0x04000C02 RID: 3074
		private double? m_interval;

		// Token: 0x04000C03 RID: 3075
		private ChartIntervalType? m_intervalType;

		// Token: 0x04000C04 RID: 3076
		private double? m_intervalOffset;

		// Token: 0x04000C05 RID: 3077
		private ChartIntervalType? m_intervalOffsetType;
	}
}
