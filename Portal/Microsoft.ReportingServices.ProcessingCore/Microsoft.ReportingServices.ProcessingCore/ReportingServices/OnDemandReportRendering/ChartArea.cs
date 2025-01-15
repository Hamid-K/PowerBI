using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000231 RID: 561
	public sealed class ChartArea : ChartObjectCollectionItem<ChartAreaInstance>, IROMStyleDefinitionContainer
	{
		// Token: 0x0600158C RID: 5516 RVA: 0x00056E20 File Offset: 0x00055020
		internal ChartArea(ChartArea chartAreaDef, Chart chart)
		{
			this.m_chart = chart;
			this.m_chartAreaDef = chartAreaDef;
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x00056E36 File Offset: 0x00055036
		internal ChartArea(Chart chart)
		{
			this.m_chart = chart;
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x0600158E RID: 5518 RVA: 0x00056E45 File Offset: 0x00055045
		public string Name
		{
			get
			{
				if (this.m_chart.IsOldSnapshot)
				{
					return "Default";
				}
				return this.m_chartAreaDef.ChartAreaName;
			}
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x0600158F RID: 5519 RVA: 0x00056E68 File Offset: 0x00055068
		public Style Style
		{
			get
			{
				if (this.m_style == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						if (this.m_chart.RenderChartDef.PlotArea != null)
						{
							this.m_style = new Style(this.m_chart.RenderChartDef.PlotArea.StyleClass, this.m_chart.ChartInstanceInfo.PlotAreaStyleAttributeValues, this.m_chart.RenderingContext);
						}
					}
					else if (this.m_chartAreaDef.StyleClass != null)
					{
						this.m_style = new Style(this.m_chart, this.m_chart, this.m_chartAreaDef, this.m_chart.RenderingContext);
					}
				}
				return this.m_style;
			}
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x06001590 RID: 5520 RVA: 0x00056F18 File Offset: 0x00055118
		public ChartThreeDProperties ThreeDProperties
		{
			get
			{
				if (this.m_threeDProperties == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						if (this.m_chart.RenderChartDef.ThreeDProperties != null)
						{
							this.m_threeDProperties = new ChartThreeDProperties(this.m_chart.RenderChartDef.ThreeDProperties, this.m_chart);
						}
					}
					else if (this.ChartAreaDef.ThreeDProperties != null)
					{
						this.m_threeDProperties = new ChartThreeDProperties(this.ChartAreaDef.ThreeDProperties, this.m_chart);
					}
				}
				return this.m_threeDProperties;
			}
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06001591 RID: 5521 RVA: 0x00056FA0 File Offset: 0x000551A0
		public ChartAxisCollection CategoryAxes
		{
			get
			{
				if (this.m_categoryAxes == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						if (this.m_chart.RenderChartDef.CategoryAxis != null)
						{
							this.m_categoryAxes = new ChartAxisCollection(this, this.m_chart, true);
						}
					}
					else if (this.m_chartAreaDef.CategoryAxes != null)
					{
						this.m_categoryAxes = new ChartAxisCollection(this, this.m_chart, true);
					}
				}
				return this.m_categoryAxes;
			}
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x06001592 RID: 5522 RVA: 0x00057010 File Offset: 0x00055210
		public ChartAxisCollection ValueAxes
		{
			get
			{
				if (this.m_valueAxes == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						if (this.m_chart.RenderChartDef.ValueAxis != null)
						{
							this.m_valueAxes = new ChartAxisCollection(this, this.m_chart, false);
						}
					}
					else if (this.ChartAreaDef.ValueAxes != null)
					{
						this.m_valueAxes = new ChartAxisCollection(this, this.m_chart, false);
					}
				}
				return this.m_valueAxes;
			}
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x06001593 RID: 5523 RVA: 0x0005707F File Offset: 0x0005527F
		public ReportBoolProperty Hidden
		{
			get
			{
				if (this.m_hidden == null && !this.m_chart.IsOldSnapshot && this.m_chartAreaDef.Hidden != null)
				{
					this.m_hidden = new ReportBoolProperty(this.m_chartAreaDef.Hidden);
				}
				return this.m_hidden;
			}
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x06001594 RID: 5524 RVA: 0x000570C0 File Offset: 0x000552C0
		public ReportEnumProperty<ChartAreaAlignOrientations> AlignOrientation
		{
			get
			{
				if (this.m_alignOrientation == null && !this.m_chart.IsOldSnapshot && this.m_chartAreaDef.AlignOrientation != null)
				{
					this.m_alignOrientation = new ReportEnumProperty<ChartAreaAlignOrientations>(this.m_chartAreaDef.AlignOrientation.IsExpression, this.m_chartAreaDef.AlignOrientation.OriginalText, EnumTranslator.TranslateChartAreaAlignOrientation(this.m_chartAreaDef.AlignOrientation.StringValue, null));
				}
				return this.m_alignOrientation;
			}
		}

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x06001595 RID: 5525 RVA: 0x00057138 File Offset: 0x00055338
		public ChartAlignType ChartAlignType
		{
			get
			{
				if (this.m_chartAlignType == null && !this.m_chart.IsOldSnapshot && this.m_chartAreaDef.ChartAlignType != null)
				{
					this.m_chartAlignType = new ChartAlignType(this.m_chartAreaDef.ChartAlignType, this.m_chart);
				}
				return this.m_chartAlignType;
			}
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x06001596 RID: 5526 RVA: 0x00057189 File Offset: 0x00055389
		public string AlignWithChartArea
		{
			get
			{
				if (this.m_chart.IsOldSnapshot)
				{
					return null;
				}
				return this.m_chartAreaDef.AlignWithChartArea;
			}
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x06001597 RID: 5527 RVA: 0x000571A5 File Offset: 0x000553A5
		public ReportBoolProperty EquallySizedAxesFont
		{
			get
			{
				if (this.m_equallySizedAxesFont == null && !this.m_chart.IsOldSnapshot && this.m_chartAreaDef.EquallySizedAxesFont != null)
				{
					this.m_equallySizedAxesFont = new ReportBoolProperty(this.m_chartAreaDef.EquallySizedAxesFont);
				}
				return this.m_equallySizedAxesFont;
			}
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x06001598 RID: 5528 RVA: 0x000571E8 File Offset: 0x000553E8
		public ChartElementPosition ChartElementPosition
		{
			get
			{
				if (this.m_chartElementPosition == null && !this.m_chart.IsOldSnapshot && this.m_chartAreaDef.ChartElementPosition != null)
				{
					this.m_chartElementPosition = new ChartElementPosition(this.m_chartAreaDef.ChartElementPosition, this.m_chart);
				}
				return this.m_chartElementPosition;
			}
		}

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x06001599 RID: 5529 RVA: 0x0005723C File Offset: 0x0005543C
		public ChartElementPosition ChartInnerPlotPosition
		{
			get
			{
				if (this.m_chartInnerPlotPosition == null && !this.m_chart.IsOldSnapshot && this.m_chartAreaDef.ChartInnerPlotPosition != null)
				{
					this.m_chartInnerPlotPosition = new ChartElementPosition(this.m_chartAreaDef.ChartInnerPlotPosition, this.m_chart);
				}
				return this.m_chartInnerPlotPosition;
			}
		}

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x0600159A RID: 5530 RVA: 0x0005728D File Offset: 0x0005548D
		internal ChartArea ChartAreaDef
		{
			get
			{
				return this.m_chartAreaDef;
			}
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x0600159B RID: 5531 RVA: 0x00057295 File Offset: 0x00055495
		internal Chart ChartDef
		{
			get
			{
				return this.m_chart;
			}
		}

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x0600159C RID: 5532 RVA: 0x0005729D File Offset: 0x0005549D
		public ChartAreaInstance Instance
		{
			get
			{
				if (this.m_chart.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new ChartAreaInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x000572D0 File Offset: 0x000554D0
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			if (this.m_categoryAxes != null)
			{
				this.m_categoryAxes.SetNewContext();
			}
			if (this.m_valueAxes != null)
			{
				this.m_valueAxes.SetNewContext();
			}
			if (this.m_threeDProperties != null)
			{
				this.m_threeDProperties.SetNewContext();
			}
			if (this.m_chartAlignType != null)
			{
				this.m_chartAlignType.SetNewContext();
			}
			if (this.m_chartElementPosition != null)
			{
				this.m_chartElementPosition.SetNewContext();
			}
			if (this.m_chartInnerPlotPosition != null)
			{
				this.m_chartInnerPlotPosition.SetNewContext();
			}
		}

		// Token: 0x04000A32 RID: 2610
		private Chart m_chart;

		// Token: 0x04000A33 RID: 2611
		private ChartArea m_chartAreaDef;

		// Token: 0x04000A34 RID: 2612
		private Style m_style;

		// Token: 0x04000A35 RID: 2613
		private ChartAxisCollection m_categoryAxes;

		// Token: 0x04000A36 RID: 2614
		private ChartAxisCollection m_valueAxes;

		// Token: 0x04000A37 RID: 2615
		private ChartThreeDProperties m_threeDProperties;

		// Token: 0x04000A38 RID: 2616
		private ReportBoolProperty m_hidden;

		// Token: 0x04000A39 RID: 2617
		private ReportEnumProperty<ChartAreaAlignOrientations> m_alignOrientation;

		// Token: 0x04000A3A RID: 2618
		private ChartAlignType m_chartAlignType;

		// Token: 0x04000A3B RID: 2619
		private ReportBoolProperty m_equallySizedAxesFont;

		// Token: 0x04000A3C RID: 2620
		private ChartElementPosition m_chartElementPosition;

		// Token: 0x04000A3D RID: 2621
		private ChartElementPosition m_chartInnerPlotPosition;
	}
}
