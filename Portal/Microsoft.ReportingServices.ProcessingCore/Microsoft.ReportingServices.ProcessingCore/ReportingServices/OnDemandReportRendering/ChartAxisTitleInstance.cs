using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000262 RID: 610
	public sealed class ChartAxisTitleInstance : BaseInstance
	{
		// Token: 0x060017C2 RID: 6082 RVA: 0x00060F48 File Offset: 0x0005F148
		internal ChartAxisTitleInstance(ChartAxisTitle chartAxisTitleDef)
			: base(chartAxisTitleDef.ChartDef)
		{
			this.m_chartAxisTitleDef = chartAxisTitleDef;
		}

		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x060017C3 RID: 6083 RVA: 0x00060F60 File Offset: 0x0005F160
		public string Caption
		{
			get
			{
				if (!this.m_captionEvaluated)
				{
					this.m_captionEvaluated = true;
					if (this.m_chartAxisTitleDef.ChartDef.IsOldSnapshot)
					{
						this.m_caption = this.m_chartAxisTitleDef.RenderChartTitleInstance.Caption;
					}
					else
					{
						this.m_caption = this.m_chartAxisTitleDef.ChartAxisTitleDef.EvaluateCaption(this.ReportScopeInstance, this.m_chartAxisTitleDef.ChartDef.RenderingContext.OdpContext);
					}
				}
				return this.m_caption;
			}
		}

		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x060017C4 RID: 6084 RVA: 0x00060FDD File Offset: 0x0005F1DD
		public StyleInstance Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new StyleInstance(this.m_chartAxisTitleDef, this.m_chartAxisTitleDef.ChartDef, this.m_chartAxisTitleDef.ChartDef.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x060017C5 RID: 6085 RVA: 0x0006101C File Offset: 0x0005F21C
		public ChartAxisTitlePositions Position
		{
			get
			{
				if (this.m_position == null && !this.m_chartAxisTitleDef.ChartDef.IsOldSnapshot)
				{
					this.m_position = new ChartAxisTitlePositions?(this.m_chartAxisTitleDef.ChartAxisTitleDef.EvaluatePosition(this.ReportScopeInstance, this.m_chartAxisTitleDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_position.Value;
			}
		}

		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x060017C6 RID: 6086 RVA: 0x0006108C File Offset: 0x0005F28C
		public TextOrientations TextOrientation
		{
			get
			{
				if (this.m_textOrientation == null)
				{
					this.m_textOrientation = new TextOrientations?(this.m_chartAxisTitleDef.ChartAxisTitleDef.EvaluateTextOrientation(this.ReportScopeInstance, this.m_chartAxisTitleDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_textOrientation.Value;
			}
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x000610E7 File Offset: 0x0005F2E7
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_captionEvaluated = false;
			this.m_position = null;
			this.m_textOrientation = null;
		}

		// Token: 0x04000BDE RID: 3038
		private ChartAxisTitle m_chartAxisTitleDef;

		// Token: 0x04000BDF RID: 3039
		private StyleInstance m_style;

		// Token: 0x04000BE0 RID: 3040
		private bool m_captionEvaluated;

		// Token: 0x04000BE1 RID: 3041
		private string m_caption;

		// Token: 0x04000BE2 RID: 3042
		private ChartAxisTitlePositions? m_position;

		// Token: 0x04000BE3 RID: 3043
		private TextOrientations? m_textOrientation;
	}
}
