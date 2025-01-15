using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000265 RID: 613
	public sealed class ChartAxisScaleBreakInstance : BaseInstance
	{
		// Token: 0x060017DA RID: 6106 RVA: 0x000617B0 File Offset: 0x0005F9B0
		internal ChartAxisScaleBreakInstance(ChartAxisScaleBreak chartAxisScaleBreakDef)
			: base(chartAxisScaleBreakDef.ChartDef)
		{
			this.m_chartAxisScaleBreakDef = chartAxisScaleBreakDef;
		}

		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x060017DB RID: 6107 RVA: 0x000617C5 File Offset: 0x0005F9C5
		public StyleInstance Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new StyleInstance(this.m_chartAxisScaleBreakDef, this.m_chartAxisScaleBreakDef.ChartDef, this.m_chartAxisScaleBreakDef.ChartDef.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x060017DC RID: 6108 RVA: 0x00061804 File Offset: 0x0005FA04
		public bool Enabled
		{
			get
			{
				if (this.m_enabled == null && !this.m_chartAxisScaleBreakDef.ChartDef.IsOldSnapshot)
				{
					this.m_enabled = new bool?(this.m_chartAxisScaleBreakDef.ChartAxisScaleBreakDef.EvaluateEnabled(this.ReportScopeInstance, this.m_chartAxisScaleBreakDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_enabled.Value;
			}
		}

		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x060017DD RID: 6109 RVA: 0x00061874 File Offset: 0x0005FA74
		public ChartBreakLineType BreakLineType
		{
			get
			{
				if (this.m_breakLineType == null && !this.m_chartAxisScaleBreakDef.ChartDef.IsOldSnapshot)
				{
					this.m_breakLineType = new ChartBreakLineType?(this.m_chartAxisScaleBreakDef.ChartAxisScaleBreakDef.EvaluateBreakLineType(this.ReportScopeInstance, this.m_chartAxisScaleBreakDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_breakLineType.Value;
			}
		}

		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x060017DE RID: 6110 RVA: 0x000618E4 File Offset: 0x0005FAE4
		public int CollapsibleSpaceThreshold
		{
			get
			{
				if (this.m_collapsibleSpaceThreshold == null && !this.m_chartAxisScaleBreakDef.ChartDef.IsOldSnapshot)
				{
					this.m_collapsibleSpaceThreshold = new int?(this.m_chartAxisScaleBreakDef.ChartAxisScaleBreakDef.EvaluateCollapsibleSpaceThreshold(this.ReportScopeInstance, this.m_chartAxisScaleBreakDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_collapsibleSpaceThreshold.Value;
			}
		}

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x060017DF RID: 6111 RVA: 0x00061954 File Offset: 0x0005FB54
		public int MaxNumberOfBreaks
		{
			get
			{
				if (this.m_maxNumberOfBreaks == null && !this.m_chartAxisScaleBreakDef.ChartDef.IsOldSnapshot)
				{
					this.m_maxNumberOfBreaks = new int?(this.m_chartAxisScaleBreakDef.ChartAxisScaleBreakDef.EvaluateMaxNumberOfBreaks(this.ReportScopeInstance, this.m_chartAxisScaleBreakDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_maxNumberOfBreaks.Value;
			}
		}

		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x060017E0 RID: 6112 RVA: 0x000619C4 File Offset: 0x0005FBC4
		public double Spacing
		{
			get
			{
				if (this.m_spacing == null && !this.m_chartAxisScaleBreakDef.ChartDef.IsOldSnapshot)
				{
					this.m_spacing = new double?(this.m_chartAxisScaleBreakDef.ChartAxisScaleBreakDef.EvaluateSpacing(this.ReportScopeInstance, this.m_chartAxisScaleBreakDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_spacing.Value;
			}
		}

		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x060017E1 RID: 6113 RVA: 0x00061A34 File Offset: 0x0005FC34
		public ChartAutoBool IncludeZero
		{
			get
			{
				if (this.m_includeZero == null && !this.m_chartAxisScaleBreakDef.ChartDef.IsOldSnapshot)
				{
					string text = this.m_chartAxisScaleBreakDef.ChartAxisScaleBreakDef.EvaluateIncludeZero(this.ReportScopeInstance, this.m_chartAxisScaleBreakDef.ChartDef.RenderingContext.OdpContext);
					this.m_includeZero = new ChartAutoBool?(EnumTranslator.TranslateChartAutoBool(text, this.m_chartAxisScaleBreakDef.ChartDef.RenderingContext.OdpContext.ReportRuntime));
				}
				return this.m_includeZero.Value;
			}
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x00061AC4 File Offset: 0x0005FCC4
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_enabled = null;
			this.m_breakLineType = null;
			this.m_collapsibleSpaceThreshold = null;
			this.m_maxNumberOfBreaks = null;
			this.m_spacing = null;
			this.m_includeZero = null;
		}

		// Token: 0x04000BF5 RID: 3061
		private ChartAxisScaleBreak m_chartAxisScaleBreakDef;

		// Token: 0x04000BF6 RID: 3062
		private StyleInstance m_style;

		// Token: 0x04000BF7 RID: 3063
		private bool? m_enabled;

		// Token: 0x04000BF8 RID: 3064
		private ChartBreakLineType? m_breakLineType;

		// Token: 0x04000BF9 RID: 3065
		private int? m_collapsibleSpaceThreshold;

		// Token: 0x04000BFA RID: 3066
		private int? m_maxNumberOfBreaks;

		// Token: 0x04000BFB RID: 3067
		private double? m_spacing;

		// Token: 0x04000BFC RID: 3068
		private ChartAutoBool? m_includeZero;
	}
}
