using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000220 RID: 544
	public sealed class Chart : Microsoft.ReportingServices.OnDemandReportRendering.DataRegion
	{
		// Token: 0x0600147B RID: 5243 RVA: 0x00053BA8 File Offset: 0x00051DA8
		internal Chart(IDefinitionPath parentDefinitionPath, int indexIntoParentCollectionDef, Microsoft.ReportingServices.ReportIntermediateFormat.Chart reportItemDef, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
			: base(parentDefinitionPath, indexIntoParentCollectionDef, reportItemDef, renderingContext)
		{
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x00053BB5 File Offset: 0x00051DB5
		internal Chart(IDefinitionPath parentDefinitionPath, int indexIntoParentCollectionDef, bool inSubtotal, Microsoft.ReportingServices.ReportRendering.Chart renderChart, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
			: base(parentDefinitionPath, indexIntoParentCollectionDef, inSubtotal, renderChart, renderingContext)
		{
			this.m_snapshotDataRegionType = Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.Chart;
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x0600147D RID: 5245 RVA: 0x00053BCB File Offset: 0x00051DCB
		public bool DataValueSequenceRendering
		{
			get
			{
				return this.m_isOldSnapshot || this.ChartDef.DataValueSequenceRendering;
			}
		}

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x0600147E RID: 5246 RVA: 0x00053BE2 File Offset: 0x00051DE2
		public ChartHierarchy CategoryHierarchy
		{
			get
			{
				if (this.m_categories == null)
				{
					this.m_categories = new ChartHierarchy(this, true);
				}
				return this.m_categories;
			}
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x0600147F RID: 5247 RVA: 0x00053BFF File Offset: 0x00051DFF
		public ChartHierarchy SeriesHierarchy
		{
			get
			{
				if (this.m_series == null)
				{
					this.m_series = new ChartHierarchy(this, false);
				}
				return this.m_series;
			}
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06001480 RID: 5248 RVA: 0x00053C1C File Offset: 0x00051E1C
		public ChartData ChartData
		{
			get
			{
				if (this.m_chartData == null)
				{
					this.m_chartData = new ChartData(this);
				}
				return this.m_chartData;
			}
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06001481 RID: 5249 RVA: 0x00053C38 File Offset: 0x00051E38
		public int Categories
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return this.RenderChart.CategoriesCount;
				}
				return this.ChartDef.CategoryCount;
			}
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06001482 RID: 5250 RVA: 0x00053C59 File Offset: 0x00051E59
		public int Series
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return this.RenderChart.SeriesCount;
				}
				return this.ChartDef.SeriesCount;
			}
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x00053C7C File Offset: 0x00051E7C
		public ChartTitleCollection Titles
		{
			get
			{
				if (this.m_titles == null)
				{
					if (this.m_isOldSnapshot)
					{
						if (this.RenderChartDef.Title != null)
						{
							this.m_titles = new ChartTitleCollection(this);
						}
					}
					else if (this.ChartDef.Titles != null)
					{
						this.m_titles = new ChartTitleCollection(this);
					}
				}
				return this.m_titles;
			}
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06001484 RID: 5252 RVA: 0x00053CD3 File Offset: 0x00051ED3
		public ChartCustomPaletteColorCollection CustomPaletteColors
		{
			get
			{
				if (this.m_customPaletteColors == null && !this.m_isOldSnapshot && this.ChartDef.CustomPaletteColors != null)
				{
					this.m_customPaletteColors = new ChartCustomPaletteColorCollection(this);
				}
				return this.m_customPaletteColors;
			}
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06001485 RID: 5253 RVA: 0x00053D04 File Offset: 0x00051F04
		public ChartBorderSkin BorderSkin
		{
			get
			{
				if (this.m_borderSkin == null && !this.m_isOldSnapshot && this.ChartDef.BorderSkin != null)
				{
					this.m_borderSkin = new ChartBorderSkin(this.ChartDef.BorderSkin, this);
				}
				return this.m_borderSkin;
			}
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06001486 RID: 5254 RVA: 0x00053D40 File Offset: 0x00051F40
		internal Microsoft.ReportingServices.ReportIntermediateFormat.Chart ChartDef
		{
			get
			{
				return this.m_reportItemDef as Microsoft.ReportingServices.ReportIntermediateFormat.Chart;
			}
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06001487 RID: 5255 RVA: 0x00053D4D File Offset: 0x00051F4D
		internal override bool HasDataCells
		{
			get
			{
				return this.m_chartData != null && this.m_chartData.HasSeriesCollection;
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06001488 RID: 5256 RVA: 0x00053D64 File Offset: 0x00051F64
		internal override IDataRegionRowCollection RowCollection
		{
			get
			{
				if (this.m_chartData != null)
				{
					return this.m_chartData.SeriesCollection;
				}
				return null;
			}
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06001489 RID: 5257 RVA: 0x00053D7B File Offset: 0x00051F7B
		public ChartAreaCollection ChartAreas
		{
			get
			{
				if (this.m_chartAreas == null)
				{
					if (this.m_isOldSnapshot)
					{
						this.m_chartAreas = new ChartAreaCollection(this);
					}
					else if (this.ChartDef.ChartAreas != null)
					{
						this.m_chartAreas = new ChartAreaCollection(this);
					}
				}
				return this.m_chartAreas;
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x0600148A RID: 5258 RVA: 0x00053DBC File Offset: 0x00051FBC
		public ChartLegendCollection Legends
		{
			get
			{
				if (this.m_legends == null)
				{
					if (this.m_isOldSnapshot)
					{
						if (this.RenderChartDef.Legend != null)
						{
							this.m_legends = new ChartLegendCollection(this);
						}
					}
					else if (this.ChartDef.Legends != null)
					{
						this.m_legends = new ChartLegendCollection(this);
					}
				}
				return this.m_legends;
			}
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x0600148B RID: 5259 RVA: 0x00053E14 File Offset: 0x00052014
		public ReportEnumProperty<ChartPalette> Palette
		{
			get
			{
				if (this.m_palette == null)
				{
					if (this.m_isOldSnapshot)
					{
						this.m_palette = new ReportEnumProperty<ChartPalette>(false, null, (ChartPalette)this.RenderChartDef.Palette);
					}
					else if (this.ChartDef.Palette != null)
					{
						this.m_palette = new ReportEnumProperty<ChartPalette>(this.ChartDef.Palette.IsExpression, this.ChartDef.Palette.OriginalText, EnumTranslator.TranslateChartPalette(this.ChartDef.Palette.StringValue, null));
					}
				}
				return this.m_palette;
			}
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x0600148C RID: 5260 RVA: 0x00053EA0 File Offset: 0x000520A0
		public ReportEnumProperty<PaletteHatchBehavior> PaletteHatchBehavior
		{
			get
			{
				if (this.m_paletteHatchBehavior == null)
				{
					if (this.m_isOldSnapshot)
					{
						if (Microsoft.ReportingServices.ReportProcessing.Chart.ChartPalette.GrayScale == this.RenderChartDef.Palette)
						{
							this.m_paletteHatchBehavior = new ReportEnumProperty<PaletteHatchBehavior>(Microsoft.ReportingServices.OnDemandReportRendering.PaletteHatchBehavior.Always);
						}
					}
					else if (this.ChartDef.PaletteHatchBehavior != null)
					{
						this.m_paletteHatchBehavior = new ReportEnumProperty<PaletteHatchBehavior>(this.ChartDef.PaletteHatchBehavior.IsExpression, this.ChartDef.PaletteHatchBehavior.OriginalText, EnumTranslator.TranslatePaletteHatchBehavior(this.ChartDef.PaletteHatchBehavior.StringValue, null));
					}
				}
				return this.m_paletteHatchBehavior;
			}
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x0600148D RID: 5261 RVA: 0x00053F30 File Offset: 0x00052130
		public ReportSizeProperty DynamicHeight
		{
			get
			{
				if (this.m_dynamicHeight == null)
				{
					if (this.m_isOldSnapshot)
					{
						this.m_dynamicHeight = new ReportSizeProperty(false, this.m_renderReportItem.ReportItemDef.Height, new ReportSize(this.m_renderReportItem.ReportItemDef.Height));
					}
					else if (this.ChartDef.DynamicHeight != null)
					{
						this.m_dynamicHeight = new ReportSizeProperty(this.ChartDef.DynamicHeight);
					}
					else
					{
						this.m_dynamicHeight = new ReportSizeProperty(false, this.m_reportItemDef.Height, new ReportSize(this.m_reportItemDef.Height));
					}
				}
				return this.m_dynamicHeight;
			}
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x00053FD8 File Offset: 0x000521D8
		public ReportSizeProperty DynamicWidth
		{
			get
			{
				if (this.m_dynamicWidth == null)
				{
					if (this.m_isOldSnapshot)
					{
						this.m_dynamicWidth = new ReportSizeProperty(false, this.m_renderReportItem.ReportItemDef.Width, new ReportSize(this.m_renderReportItem.ReportItemDef.Width));
					}
					else if (this.ChartDef.DynamicWidth != null)
					{
						this.m_dynamicWidth = new ReportSizeProperty(this.ChartDef.DynamicWidth);
					}
					else
					{
						this.m_dynamicWidth = new ReportSizeProperty(false, this.m_reportItemDef.Width, new ReportSize(this.m_reportItemDef.Width));
					}
				}
				return this.m_dynamicWidth;
			}
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x0005407D File Offset: 0x0005227D
		public Microsoft.ReportingServices.OnDemandReportRendering.ChartTitle NoDataMessage
		{
			get
			{
				if (this.m_noDataMessage == null && !base.IsOldSnapshot && this.ChartDef.NoDataMessage != null)
				{
					this.m_noDataMessage = new Microsoft.ReportingServices.OnDemandReportRendering.ChartTitle(this.ChartDef.NoDataMessage, this);
				}
				return this.m_noDataMessage;
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x06001490 RID: 5264 RVA: 0x000540BC File Offset: 0x000522BC
		public bool SpecialBorderHandling
		{
			get
			{
				ChartBorderSkin borderSkin = this.BorderSkin;
				if (borderSkin == null)
				{
					return false;
				}
				ReportEnumProperty<ChartBorderSkinType> borderSkinType = borderSkin.BorderSkinType;
				if (borderSkinType == null)
				{
					return false;
				}
				ChartBorderSkinType chartBorderSkinType;
				if (!borderSkinType.IsExpression)
				{
					chartBorderSkinType = borderSkinType.Value;
				}
				else
				{
					chartBorderSkinType = borderSkin.Instance.BorderSkinType;
				}
				return chartBorderSkinType > ChartBorderSkinType.None;
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x00054102 File Offset: 0x00052302
		internal ChartInstanceInfo ChartInstanceInfo
		{
			get
			{
				return (ChartInstanceInfo)this.RenderReportItem.InstanceInfo;
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x06001492 RID: 5266 RVA: 0x00054114 File Offset: 0x00052314
		internal Microsoft.ReportingServices.ReportProcessing.Chart RenderChartDef
		{
			get
			{
				return (Microsoft.ReportingServices.ReportProcessing.Chart)this.RenderReportItem.ReportItemDef;
			}
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x00054126 File Offset: 0x00052326
		internal override ReportItemInstance GetOrCreateInstance()
		{
			if (this.m_instance == null)
			{
				this.m_instance = new ChartInstance(this);
			}
			return this.m_instance;
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x00054144 File Offset: 0x00052344
		internal override void SetNewContextChildren()
		{
			if (this.m_categories != null)
			{
				this.m_categories.ResetContext();
			}
			if (this.m_series != null)
			{
				this.m_series.ResetContext();
			}
			if (this.m_chartAreas != null)
			{
				this.m_chartAreas.SetNewContext();
			}
			if (this.m_titles != null)
			{
				this.m_titles.SetNewContext();
			}
			if (this.m_customPaletteColors != null)
			{
				this.m_customPaletteColors.SetNewContext();
			}
			if (this.m_legends != null)
			{
				this.m_legends.SetNewContext();
			}
			if (this.m_borderSkin != null)
			{
				this.m_borderSkin.SetNewContext();
			}
			if (this.m_noDataMessage != null)
			{
				this.m_noDataMessage.SetNewContext();
			}
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x000541E9 File Offset: 0x000523E9
		internal Microsoft.ReportingServices.ReportRendering.Chart RenderChart
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return (Microsoft.ReportingServices.ReportRendering.Chart)this.m_renderReportItem;
				}
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
			}
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x0005420C File Offset: 0x0005240C
		internal override void UpdateRenderReportItem(Microsoft.ReportingServices.ReportRendering.ReportItem renderReportItem)
		{
			base.UpdateRenderReportItem(renderReportItem);
			if (renderReportItem != null)
			{
				this.m_categories = null;
				this.m_series = null;
				this.m_memberCellDefinitionIndex = 0;
			}
			else
			{
				if (this.m_categories != null)
				{
					this.m_categories.ResetContext();
				}
				if (this.m_series != null)
				{
					this.m_series.ResetContext();
				}
			}
			this.m_chartAreas = null;
			this.m_titles = null;
			this.m_customPaletteColors = null;
			this.m_legends = null;
			this.m_borderSkin = null;
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x00054283 File Offset: 0x00052483
		internal int GetCurrentMemberCellDefinitionIndex()
		{
			return this.m_memberCellDefinitionIndex;
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x0005428C File Offset: 0x0005248C
		internal int GetAndIncrementMemberCellDefinitionIndex()
		{
			int memberCellDefinitionIndex = this.m_memberCellDefinitionIndex;
			this.m_memberCellDefinitionIndex = memberCellDefinitionIndex + 1;
			return memberCellDefinitionIndex;
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x000542AA File Offset: 0x000524AA
		internal void ResetMemberCellDefinitionIndex(int startIndex)
		{
			this.m_memberCellDefinitionIndex = startIndex;
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x000542B3 File Offset: 0x000524B3
		internal Microsoft.ReportingServices.OnDemandReportRendering.ChartMember GetChartMember(ChartSeries chartSeries)
		{
			return this.GetChartMember(this.SeriesHierarchy.MemberCollection, this.GetSeriesIndex(chartSeries));
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x000542D0 File Offset: 0x000524D0
		private int GetSeriesIndex(ChartSeries series)
		{
			ChartSeriesCollection seriesCollection = this.ChartData.SeriesCollection;
			for (int i = 0; i < seriesCollection.Count; i++)
			{
				if (seriesCollection[i] == series)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x00054308 File Offset: 0x00052508
		internal Microsoft.ReportingServices.OnDemandReportRendering.ChartMember GetChartMember(ChartMemberCollection chartMemberCollection, int memberCellIndex)
		{
			foreach (Microsoft.ReportingServices.OnDemandReportRendering.ChartMember chartMember in chartMemberCollection)
			{
				if (chartMember != null)
				{
					if (chartMember.Children == null)
					{
						if (chartMember.MemberCellIndex == memberCellIndex)
						{
							return chartMember;
						}
					}
					else
					{
						Microsoft.ReportingServices.OnDemandReportRendering.ChartMember chartMember2 = this.GetChartMember(chartMember.Children, memberCellIndex);
						if (chartMember2 != null)
						{
							return chartMember2;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x0005437C File Offset: 0x0005257C
		internal List<ChartDerivedSeries> GetChildrenDerivedSeries(string chartSeriesName)
		{
			ChartDerivedSeriesCollection derivedSeriesCollection = this.ChartData.DerivedSeriesCollection;
			if (derivedSeriesCollection == null)
			{
				return null;
			}
			List<ChartDerivedSeries> list = null;
			foreach (ChartDerivedSeries chartDerivedSeries in derivedSeriesCollection)
			{
				if (chartDerivedSeries != null && ReportProcessing.CompareWithInvariantCulture(chartDerivedSeries.SourceChartSeriesName, chartSeriesName, false) == 0)
				{
					if (list == null)
					{
						list = new List<ChartDerivedSeries>();
					}
					list.Add(chartDerivedSeries);
				}
			}
			return list;
		}

		// Token: 0x040009AC RID: 2476
		private int m_memberCellDefinitionIndex;

		// Token: 0x040009AD RID: 2477
		private ChartHierarchy m_categories;

		// Token: 0x040009AE RID: 2478
		private ChartHierarchy m_series;

		// Token: 0x040009AF RID: 2479
		private ChartData m_chartData;

		// Token: 0x040009B0 RID: 2480
		private ReportSizeProperty m_dynamicHeight;

		// Token: 0x040009B1 RID: 2481
		private ReportSizeProperty m_dynamicWidth;

		// Token: 0x040009B2 RID: 2482
		private ChartAreaCollection m_chartAreas;

		// Token: 0x040009B3 RID: 2483
		private ChartTitleCollection m_titles;

		// Token: 0x040009B4 RID: 2484
		private ChartLegendCollection m_legends;

		// Token: 0x040009B5 RID: 2485
		private ChartBorderSkin m_borderSkin;

		// Token: 0x040009B6 RID: 2486
		private ChartCustomPaletteColorCollection m_customPaletteColors;

		// Token: 0x040009B7 RID: 2487
		private ReportEnumProperty<ChartPalette> m_palette;

		// Token: 0x040009B8 RID: 2488
		private ReportEnumProperty<PaletteHatchBehavior> m_paletteHatchBehavior;

		// Token: 0x040009B9 RID: 2489
		private Microsoft.ReportingServices.OnDemandReportRendering.ChartTitle m_noDataMessage;
	}
}
