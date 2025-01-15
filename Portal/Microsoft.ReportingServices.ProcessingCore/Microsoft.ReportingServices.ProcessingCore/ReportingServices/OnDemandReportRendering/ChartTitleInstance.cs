using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000263 RID: 611
	public sealed class ChartTitleInstance : BaseInstance
	{
		// Token: 0x060017C8 RID: 6088 RVA: 0x0006111B File Offset: 0x0005F31B
		internal ChartTitleInstance(ChartTitle chartTitleDef)
			: base(chartTitleDef.ChartDef)
		{
			this.m_chartTitleDef = chartTitleDef;
		}

		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x060017C9 RID: 6089 RVA: 0x00061130 File Offset: 0x0005F330
		public string Caption
		{
			get
			{
				if (!this.m_captionEvaluated)
				{
					this.m_captionEvaluated = true;
					if (this.m_chartTitleDef.ChartDef.IsOldSnapshot)
					{
						this.m_caption = this.m_chartTitleDef.RenderChartTitleInstance.Caption;
					}
					else
					{
						this.m_caption = this.m_chartTitleDef.ChartTitleDef.EvaluateCaption(this.ReportScopeInstance, this.m_chartTitleDef.ChartDef.RenderingContext.OdpContext);
					}
				}
				return this.m_caption;
			}
		}

		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x060017CA RID: 6090 RVA: 0x000611AD File Offset: 0x0005F3AD
		public StyleInstance Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new StyleInstance(this.m_chartTitleDef, this.m_chartTitleDef.ChartDef, this.m_chartTitleDef.ChartDef.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x060017CB RID: 6091 RVA: 0x000611EC File Offset: 0x0005F3EC
		public ChartTitlePositions Position
		{
			get
			{
				if (this.m_position == null && !this.m_chartTitleDef.ChartDef.IsOldSnapshot)
				{
					this.m_position = new ChartTitlePositions?(this.m_chartTitleDef.ChartTitleDef.EvaluatePosition(this.ReportScopeInstance, this.m_chartTitleDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_position.Value;
			}
		}

		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x060017CC RID: 6092 RVA: 0x0006125C File Offset: 0x0005F45C
		public bool Hidden
		{
			get
			{
				if (this.m_hidden == null && !this.m_chartTitleDef.ChartDef.IsOldSnapshot)
				{
					this.m_hidden = new bool?(this.m_chartTitleDef.ChartTitleDef.EvaluateHidden(this.ReportScopeInstance, this.m_chartTitleDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_hidden.Value;
			}
		}

		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x060017CD RID: 6093 RVA: 0x000612CC File Offset: 0x0005F4CC
		public int DockOffset
		{
			get
			{
				if (this.m_dockOffset == null && !this.m_chartTitleDef.ChartDef.IsOldSnapshot)
				{
					this.m_dockOffset = new int?(this.m_chartTitleDef.ChartTitleDef.EvaluateDockOffset(this.ReportScopeInstance, this.m_chartTitleDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_dockOffset.Value;
			}
		}

		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x060017CE RID: 6094 RVA: 0x0006133C File Offset: 0x0005F53C
		public bool DockOutsideChartArea
		{
			get
			{
				if (this.m_dockOutsideChartArea == null && !this.m_chartTitleDef.ChartDef.IsOldSnapshot)
				{
					this.m_dockOutsideChartArea = new bool?(this.m_chartTitleDef.ChartTitleDef.EvaluateDockOutsideChartArea(this.ReportScopeInstance, this.m_chartTitleDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_dockOutsideChartArea.Value;
			}
		}

		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x060017CF RID: 6095 RVA: 0x000613AC File Offset: 0x0005F5AC
		public string ToolTip
		{
			get
			{
				if (this.m_toolTip == null && !this.m_chartTitleDef.ChartDef.IsOldSnapshot)
				{
					this.m_toolTip = this.m_chartTitleDef.ChartTitleDef.EvaluateToolTip(this.ReportScopeInstance, this.m_chartTitleDef.ChartDef.RenderingContext.OdpContext);
				}
				return this.m_toolTip;
			}
		}

		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x060017D0 RID: 6096 RVA: 0x0006140C File Offset: 0x0005F60C
		public TextOrientations TextOrientation
		{
			get
			{
				if (this.m_textOrientation == null)
				{
					this.m_textOrientation = new TextOrientations?(this.m_chartTitleDef.ChartTitleDef.EvaluateTextOrientation(this.ReportScopeInstance, this.m_chartTitleDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_textOrientation.Value;
			}
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x00061468 File Offset: 0x0005F668
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_captionEvaluated = false;
			this.m_hidden = null;
			this.m_dockOffset = null;
			this.m_dockOutsideChartArea = null;
			this.m_toolTip = null;
			this.m_position = null;
			this.m_textOrientation = null;
		}

		// Token: 0x04000BE4 RID: 3044
		private ChartTitle m_chartTitleDef;

		// Token: 0x04000BE5 RID: 3045
		private StyleInstance m_style;

		// Token: 0x04000BE6 RID: 3046
		private bool m_captionEvaluated;

		// Token: 0x04000BE7 RID: 3047
		private string m_caption;

		// Token: 0x04000BE8 RID: 3048
		private bool? m_hidden;

		// Token: 0x04000BE9 RID: 3049
		private int? m_dockOffset;

		// Token: 0x04000BEA RID: 3050
		private bool? m_dockOutsideChartArea;

		// Token: 0x04000BEB RID: 3051
		private string m_toolTip;

		// Token: 0x04000BEC RID: 3052
		private ChartTitlePositions? m_position;

		// Token: 0x04000BED RID: 3053
		private TextOrientations? m_textOrientation;
	}
}
