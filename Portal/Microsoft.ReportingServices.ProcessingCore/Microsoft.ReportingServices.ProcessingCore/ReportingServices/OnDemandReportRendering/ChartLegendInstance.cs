using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000261 RID: 609
	public sealed class ChartLegendInstance : BaseInstance
	{
		// Token: 0x060017AE RID: 6062 RVA: 0x000606DD File Offset: 0x0005E8DD
		internal ChartLegendInstance(ChartLegend legendDef)
			: base(legendDef.ChartDef)
		{
			this.m_legendDef = legendDef;
		}

		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x060017AF RID: 6063 RVA: 0x000606F2 File Offset: 0x0005E8F2
		public StyleInstance Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new StyleInstance(this.m_legendDef, this.m_legendDef.ChartDef, this.m_legendDef.ChartDef.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x060017B0 RID: 6064 RVA: 0x00060730 File Offset: 0x0005E930
		public bool Hidden
		{
			get
			{
				if (this.m_hidden == null && !this.m_legendDef.ChartDef.IsOldSnapshot)
				{
					this.m_hidden = new bool?(this.m_legendDef.ChartLegendDef.EvaluateHidden(this.ReportScopeInstance, this.m_legendDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_hidden.Value;
			}
		}

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x060017B1 RID: 6065 RVA: 0x000607A0 File Offset: 0x0005E9A0
		public ChartLegendPositions Position
		{
			get
			{
				if (this.m_position == null && !this.m_legendDef.ChartDef.IsOldSnapshot)
				{
					this.m_position = new ChartLegendPositions?(this.m_legendDef.ChartLegendDef.EvaluatePosition(this.ReportScopeInstance, this.m_legendDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_position.Value;
			}
		}

		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x060017B2 RID: 6066 RVA: 0x00060810 File Offset: 0x0005EA10
		public ChartLegendLayouts Layout
		{
			get
			{
				if (this.m_layout == null && !this.m_legendDef.ChartDef.IsOldSnapshot)
				{
					this.m_layout = new ChartLegendLayouts?(this.m_legendDef.ChartLegendDef.EvaluateLayout(this.ReportScopeInstance, this.m_legendDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_layout.Value;
			}
		}

		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x060017B3 RID: 6067 RVA: 0x00060880 File Offset: 0x0005EA80
		public bool DockOutsideChartArea
		{
			get
			{
				if (this.m_dockOutsideChartArea == null && !this.m_legendDef.ChartDef.IsOldSnapshot)
				{
					this.m_dockOutsideChartArea = new bool?(this.m_legendDef.ChartLegendDef.EvaluateDockOutsideChartArea(this.ReportScopeInstance, this.m_legendDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_dockOutsideChartArea.Value;
			}
		}

		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x060017B4 RID: 6068 RVA: 0x000608F0 File Offset: 0x0005EAF0
		public bool AutoFitTextDisabled
		{
			get
			{
				if (this.m_autoFitTextDisabled == null && !this.m_legendDef.ChartDef.IsOldSnapshot)
				{
					this.m_autoFitTextDisabled = new bool?(this.m_legendDef.ChartLegendDef.EvaluateAutoFitTextDisabled(this.ReportScopeInstance, this.m_legendDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_autoFitTextDisabled.Value;
			}
		}

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x060017B5 RID: 6069 RVA: 0x00060960 File Offset: 0x0005EB60
		public ReportSize MinFontSize
		{
			get
			{
				if (this.m_minFontSize == null && !this.m_legendDef.ChartDef.IsOldSnapshot)
				{
					this.m_minFontSize = new ReportSize(this.m_legendDef.ChartLegendDef.EvaluateMinFontSize(this.ReportScopeInstance, this.m_legendDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_minFontSize;
			}
		}

		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x060017B6 RID: 6070 RVA: 0x000609C4 File Offset: 0x0005EBC4
		public ChartSeparators HeaderSeparator
		{
			get
			{
				if (this.m_headerSeparator == null && !this.m_legendDef.ChartDef.IsOldSnapshot)
				{
					this.m_headerSeparator = new ChartSeparators?(this.m_legendDef.ChartLegendDef.EvaluateHeaderSeparator(this.ReportScopeInstance, this.m_legendDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_headerSeparator.Value;
			}
		}

		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x060017B7 RID: 6071 RVA: 0x00060A34 File Offset: 0x0005EC34
		public ReportColor HeaderSeparatorColor
		{
			get
			{
				if (this.m_headerSeparatorColor == null && !this.m_legendDef.ChartDef.IsOldSnapshot)
				{
					this.m_headerSeparatorColor = new ReportColor(this.m_legendDef.ChartLegendDef.EvaluateHeaderSeparatorColor(this.ReportScopeInstance, this.m_legendDef.ChartDef.RenderingContext.OdpContext), true);
				}
				return this.m_headerSeparatorColor;
			}
		}

		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x060017B8 RID: 6072 RVA: 0x00060A98 File Offset: 0x0005EC98
		public ChartSeparators ColumnSeparator
		{
			get
			{
				if (this.m_columnSeparator == null && !this.m_legendDef.ChartDef.IsOldSnapshot)
				{
					this.m_columnSeparator = new ChartSeparators?(this.m_legendDef.ChartLegendDef.EvaluateColumnSeparator(this.ReportScopeInstance, this.m_legendDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_columnSeparator.Value;
			}
		}

		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x060017B9 RID: 6073 RVA: 0x00060B08 File Offset: 0x0005ED08
		public ReportColor ColumnSeparatorColor
		{
			get
			{
				if (this.m_columnSeparatorColor == null && !this.m_legendDef.ChartDef.IsOldSnapshot)
				{
					this.m_columnSeparatorColor = new ReportColor(this.m_legendDef.ChartLegendDef.EvaluateColumnSeparatorColor(this.ReportScopeInstance, this.m_legendDef.ChartDef.RenderingContext.OdpContext), true);
				}
				return this.m_columnSeparatorColor;
			}
		}

		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x060017BA RID: 6074 RVA: 0x00060B6C File Offset: 0x0005ED6C
		public int ColumnSpacing
		{
			get
			{
				if (this.m_columnSpacing == null && !this.m_legendDef.ChartDef.IsOldSnapshot)
				{
					this.m_columnSpacing = new int?(this.m_legendDef.ChartLegendDef.EvaluateColumnSpacing(this.ReportScopeInstance, this.m_legendDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_columnSpacing.Value;
			}
		}

		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x060017BB RID: 6075 RVA: 0x00060BDC File Offset: 0x0005EDDC
		public bool InterlacedRows
		{
			get
			{
				if (this.m_interlacedRows == null && !this.m_legendDef.ChartDef.IsOldSnapshot)
				{
					this.m_interlacedRows = new bool?(this.m_legendDef.ChartLegendDef.EvaluateInterlacedRows(this.ReportScopeInstance, this.m_legendDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_interlacedRows.Value;
			}
		}

		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x060017BC RID: 6076 RVA: 0x00060C4C File Offset: 0x0005EE4C
		public ReportColor InterlacedRowsColor
		{
			get
			{
				if (this.m_interlacedRowsColor == null && !this.m_legendDef.ChartDef.IsOldSnapshot)
				{
					this.m_interlacedRowsColor = new ReportColor(this.m_legendDef.ChartLegendDef.EvaluateInterlacedRowsColor(this.ReportScopeInstance, this.m_legendDef.ChartDef.RenderingContext.OdpContext), true);
				}
				return this.m_interlacedRowsColor;
			}
		}

		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x060017BD RID: 6077 RVA: 0x00060CB0 File Offset: 0x0005EEB0
		public bool EquallySpacedItems
		{
			get
			{
				if (this.m_equallySpacedItems == null && !this.m_legendDef.ChartDef.IsOldSnapshot)
				{
					this.m_equallySpacedItems = new bool?(this.m_legendDef.ChartLegendDef.EvaluateEquallySpacedItems(this.ReportScopeInstance, this.m_legendDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_equallySpacedItems.Value;
			}
		}

		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x060017BE RID: 6078 RVA: 0x00060D20 File Offset: 0x0005EF20
		public ChartAutoBool Reversed
		{
			get
			{
				if (this.m_reversed == null && !this.m_legendDef.ChartDef.IsOldSnapshot)
				{
					this.m_reversed = new ChartAutoBool?(this.m_legendDef.ChartLegendDef.EvaluateReversed(this.ReportScopeInstance, this.m_legendDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_reversed.Value;
			}
		}

		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x060017BF RID: 6079 RVA: 0x00060D90 File Offset: 0x0005EF90
		public int MaxAutoSize
		{
			get
			{
				if (this.m_maxAutoSize == null && !this.m_legendDef.ChartDef.IsOldSnapshot)
				{
					this.m_maxAutoSize = new int?(this.m_legendDef.ChartLegendDef.EvaluateMaxAutoSize(this.ReportScopeInstance, this.m_legendDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_maxAutoSize.Value;
			}
		}

		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x060017C0 RID: 6080 RVA: 0x00060E00 File Offset: 0x0005F000
		public int TextWrapThreshold
		{
			get
			{
				if (this.m_textWrapThreshold == null && !this.m_legendDef.ChartDef.IsOldSnapshot)
				{
					this.m_textWrapThreshold = new int?(this.m_legendDef.ChartLegendDef.EvaluateTextWrapThreshold(this.ReportScopeInstance, this.m_legendDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_textWrapThreshold.Value;
			}
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x00060E70 File Offset: 0x0005F070
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_hidden = null;
			this.m_position = null;
			this.m_layout = null;
			this.m_dockOutsideChartArea = null;
			this.m_autoFitTextDisabled = null;
			this.m_minFontSize = null;
			this.m_headerSeparator = null;
			this.m_headerSeparatorColor = null;
			this.m_columnSeparator = null;
			this.m_columnSeparatorColor = null;
			this.m_columnSpacing = null;
			this.m_interlacedRows = null;
			this.m_interlacedRowsColor = null;
			this.m_equallySpacedItems = null;
			this.m_reversed = null;
			this.m_maxAutoSize = null;
			this.m_textWrapThreshold = null;
		}

		// Token: 0x04000BCB RID: 3019
		private ChartLegend m_legendDef;

		// Token: 0x04000BCC RID: 3020
		private StyleInstance m_style;

		// Token: 0x04000BCD RID: 3021
		private bool? m_hidden;

		// Token: 0x04000BCE RID: 3022
		private ChartLegendPositions? m_position;

		// Token: 0x04000BCF RID: 3023
		private ChartLegendLayouts? m_layout;

		// Token: 0x04000BD0 RID: 3024
		private bool? m_dockOutsideChartArea;

		// Token: 0x04000BD1 RID: 3025
		private bool? m_autoFitTextDisabled;

		// Token: 0x04000BD2 RID: 3026
		private ReportSize m_minFontSize;

		// Token: 0x04000BD3 RID: 3027
		private ChartSeparators? m_headerSeparator;

		// Token: 0x04000BD4 RID: 3028
		private ReportColor m_headerSeparatorColor;

		// Token: 0x04000BD5 RID: 3029
		private ChartSeparators? m_columnSeparator;

		// Token: 0x04000BD6 RID: 3030
		private ReportColor m_columnSeparatorColor;

		// Token: 0x04000BD7 RID: 3031
		private int? m_columnSpacing;

		// Token: 0x04000BD8 RID: 3032
		private bool? m_interlacedRows;

		// Token: 0x04000BD9 RID: 3033
		private ReportColor m_interlacedRowsColor;

		// Token: 0x04000BDA RID: 3034
		private bool? m_equallySpacedItems;

		// Token: 0x04000BDB RID: 3035
		private ChartAutoBool? m_reversed;

		// Token: 0x04000BDC RID: 3036
		private int? m_maxAutoSize;

		// Token: 0x04000BDD RID: 3037
		private int? m_textWrapThreshold;
	}
}
