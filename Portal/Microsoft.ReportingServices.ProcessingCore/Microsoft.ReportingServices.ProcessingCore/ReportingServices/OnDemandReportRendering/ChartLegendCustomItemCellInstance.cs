using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200026D RID: 621
	public sealed class ChartLegendCustomItemCellInstance : BaseInstance
	{
		// Token: 0x0600180D RID: 6157 RVA: 0x000627A6 File Offset: 0x000609A6
		internal ChartLegendCustomItemCellInstance(ChartLegendCustomItemCell chartLegendCustomItemCellDef)
			: base(chartLegendCustomItemCellDef.ChartDef)
		{
			this.m_chartLegendCustomItemCellDef = chartLegendCustomItemCellDef;
		}

		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x0600180E RID: 6158 RVA: 0x000627BB File Offset: 0x000609BB
		public StyleInstance Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new StyleInstance(this.m_chartLegendCustomItemCellDef, this.m_chartLegendCustomItemCellDef.ChartDef, this.m_chartLegendCustomItemCellDef.ChartDef.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x000627F8 File Offset: 0x000609F8
		public ChartCellType CellType
		{
			get
			{
				if (this.m_cellType == null && !this.m_chartLegendCustomItemCellDef.ChartDef.IsOldSnapshot)
				{
					this.m_cellType = new ChartCellType?(this.m_chartLegendCustomItemCellDef.ChartLegendCustomItemCellDef.EvaluateCellType(this.ReportScopeInstance, this.m_chartLegendCustomItemCellDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_cellType.Value;
			}
		}

		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x06001810 RID: 6160 RVA: 0x00062868 File Offset: 0x00060A68
		public string Text
		{
			get
			{
				if (this.m_text == null && !this.m_chartLegendCustomItemCellDef.ChartDef.IsOldSnapshot)
				{
					this.m_text = this.m_chartLegendCustomItemCellDef.ChartLegendCustomItemCellDef.EvaluateText(this.ReportScopeInstance, this.m_chartLegendCustomItemCellDef.ChartDef.RenderingContext.OdpContext);
				}
				return this.m_text;
			}
		}

		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x06001811 RID: 6161 RVA: 0x000628C8 File Offset: 0x00060AC8
		public int CellSpan
		{
			get
			{
				if (this.m_cellSpan == null && !this.m_chartLegendCustomItemCellDef.ChartDef.IsOldSnapshot)
				{
					this.m_cellSpan = new int?(this.m_chartLegendCustomItemCellDef.ChartLegendCustomItemCellDef.EvaluateCellSpan(this.ReportScopeInstance, this.m_chartLegendCustomItemCellDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_cellSpan.Value;
			}
		}

		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x06001812 RID: 6162 RVA: 0x00062938 File Offset: 0x00060B38
		public string ToolTip
		{
			get
			{
				if (this.m_toolTip == null && !this.m_chartLegendCustomItemCellDef.ChartDef.IsOldSnapshot)
				{
					this.m_toolTip = this.m_chartLegendCustomItemCellDef.ChartLegendCustomItemCellDef.EvaluateToolTip(this.ReportScopeInstance, this.m_chartLegendCustomItemCellDef.ChartDef.RenderingContext.OdpContext);
				}
				return this.m_toolTip;
			}
		}

		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x06001813 RID: 6163 RVA: 0x00062998 File Offset: 0x00060B98
		public int ImageWidth
		{
			get
			{
				if (this.m_imageWidth == null && !this.m_chartLegendCustomItemCellDef.ChartDef.IsOldSnapshot)
				{
					this.m_imageWidth = new int?(this.m_chartLegendCustomItemCellDef.ChartLegendCustomItemCellDef.EvaluateImageWidth(this.ReportScopeInstance, this.m_chartLegendCustomItemCellDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_imageWidth.Value;
			}
		}

		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x06001814 RID: 6164 RVA: 0x00062A08 File Offset: 0x00060C08
		public int ImageHeight
		{
			get
			{
				if (this.m_imageHeight == null && !this.m_chartLegendCustomItemCellDef.ChartDef.IsOldSnapshot)
				{
					this.m_imageHeight = new int?(this.m_chartLegendCustomItemCellDef.ChartLegendCustomItemCellDef.EvaluateImageHeight(this.ReportScopeInstance, this.m_chartLegendCustomItemCellDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_imageHeight.Value;
			}
		}

		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x06001815 RID: 6165 RVA: 0x00062A78 File Offset: 0x00060C78
		public int SymbolHeight
		{
			get
			{
				if (this.m_symbolHeight == null && !this.m_chartLegendCustomItemCellDef.ChartDef.IsOldSnapshot)
				{
					this.m_symbolHeight = new int?(this.m_chartLegendCustomItemCellDef.ChartLegendCustomItemCellDef.EvaluateSymbolHeight(this.ReportScopeInstance, this.m_chartLegendCustomItemCellDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_symbolHeight.Value;
			}
		}

		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x06001816 RID: 6166 RVA: 0x00062AE8 File Offset: 0x00060CE8
		public int SymbolWidth
		{
			get
			{
				if (this.m_symbolWidth == null && !this.m_chartLegendCustomItemCellDef.ChartDef.IsOldSnapshot)
				{
					this.m_symbolWidth = new int?(this.m_chartLegendCustomItemCellDef.ChartLegendCustomItemCellDef.EvaluateSymbolWidth(this.ReportScopeInstance, this.m_chartLegendCustomItemCellDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_symbolWidth.Value;
			}
		}

		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x06001817 RID: 6167 RVA: 0x00062B58 File Offset: 0x00060D58
		public ChartCellAlignment Alignment
		{
			get
			{
				if (this.m_alignment == null && !this.m_chartLegendCustomItemCellDef.ChartDef.IsOldSnapshot)
				{
					this.m_alignment = new ChartCellAlignment?(this.m_chartLegendCustomItemCellDef.ChartLegendCustomItemCellDef.EvaluateAlignment(this.ReportScopeInstance, this.m_chartLegendCustomItemCellDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_alignment.Value;
			}
		}

		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x06001818 RID: 6168 RVA: 0x00062BC8 File Offset: 0x00060DC8
		public int TopMargin
		{
			get
			{
				if (this.m_topMargin == null && !this.m_chartLegendCustomItemCellDef.ChartDef.IsOldSnapshot)
				{
					this.m_topMargin = new int?(this.m_chartLegendCustomItemCellDef.ChartLegendCustomItemCellDef.EvaluateTopMargin(this.ReportScopeInstance, this.m_chartLegendCustomItemCellDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_topMargin.Value;
			}
		}

		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x06001819 RID: 6169 RVA: 0x00062C38 File Offset: 0x00060E38
		public int BottomMargin
		{
			get
			{
				if (this.m_bottomMargin == null && !this.m_chartLegendCustomItemCellDef.ChartDef.IsOldSnapshot)
				{
					this.m_bottomMargin = new int?(this.m_chartLegendCustomItemCellDef.ChartLegendCustomItemCellDef.EvaluateBottomMargin(this.ReportScopeInstance, this.m_chartLegendCustomItemCellDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_bottomMargin.Value;
			}
		}

		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x0600181A RID: 6170 RVA: 0x00062CA8 File Offset: 0x00060EA8
		public int LeftMargin
		{
			get
			{
				if (this.m_leftMargin == null && !this.m_chartLegendCustomItemCellDef.ChartDef.IsOldSnapshot)
				{
					this.m_leftMargin = new int?(this.m_chartLegendCustomItemCellDef.ChartLegendCustomItemCellDef.EvaluateLeftMargin(this.ReportScopeInstance, this.m_chartLegendCustomItemCellDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_leftMargin.Value;
			}
		}

		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x0600181B RID: 6171 RVA: 0x00062D18 File Offset: 0x00060F18
		public int RightMargin
		{
			get
			{
				if (this.m_rightMargin == null && !this.m_chartLegendCustomItemCellDef.ChartDef.IsOldSnapshot)
				{
					this.m_rightMargin = new int?(this.m_chartLegendCustomItemCellDef.ChartLegendCustomItemCellDef.EvaluateRightMargin(this.ReportScopeInstance, this.m_chartLegendCustomItemCellDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_rightMargin.Value;
			}
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x00062D88 File Offset: 0x00060F88
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_cellType = null;
			this.m_text = null;
			this.m_cellSpan = null;
			this.m_toolTip = null;
			this.m_imageWidth = null;
			this.m_imageHeight = null;
			this.m_symbolHeight = null;
			this.m_symbolWidth = null;
			this.m_alignment = null;
			this.m_topMargin = null;
			this.m_bottomMargin = null;
			this.m_leftMargin = null;
			this.m_rightMargin = null;
		}

		// Token: 0x04000C20 RID: 3104
		private ChartLegendCustomItemCell m_chartLegendCustomItemCellDef;

		// Token: 0x04000C21 RID: 3105
		private StyleInstance m_style;

		// Token: 0x04000C22 RID: 3106
		private ChartCellType? m_cellType;

		// Token: 0x04000C23 RID: 3107
		private string m_text;

		// Token: 0x04000C24 RID: 3108
		private int? m_cellSpan;

		// Token: 0x04000C25 RID: 3109
		private string m_toolTip;

		// Token: 0x04000C26 RID: 3110
		private int? m_imageWidth;

		// Token: 0x04000C27 RID: 3111
		private int? m_imageHeight;

		// Token: 0x04000C28 RID: 3112
		private int? m_symbolHeight;

		// Token: 0x04000C29 RID: 3113
		private int? m_symbolWidth;

		// Token: 0x04000C2A RID: 3114
		private ChartCellAlignment? m_alignment;

		// Token: 0x04000C2B RID: 3115
		private int? m_topMargin;

		// Token: 0x04000C2C RID: 3116
		private int? m_bottomMargin;

		// Token: 0x04000C2D RID: 3117
		private int? m_leftMargin;

		// Token: 0x04000C2E RID: 3118
		private int? m_rightMargin;
	}
}
