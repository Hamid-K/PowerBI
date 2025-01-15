using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200008C RID: 140
	internal sealed class Chart : DataRegion
	{
		// Token: 0x060002C0 RID: 704 RVA: 0x0000CAF8 File Offset: 0x0000ACF8
		internal Chart(string name, ReportItemRect rect, int zIndex, ReportParsingDiagnosticContext diagnosticContext, string chartTitle, bool chartTitleHidden, string dataSetName, ChartCategoryHierarchy categoryHierarchy, ChartSeriesHierarchy seriesHierarchy, ChartData chartData, ChartPositions legendPosition, bool legendHidden, bool scalarXAxis, int fontSizeOffset, bool experimentalFeaturesEnabled = false, int numberOfVisibleForecastPoints = 0)
			: base("Chart", name, rect, zIndex, diagnosticContext, dataSetName)
		{
			this._chartTitle = chartTitle;
			this._chartTitleHidden = chartTitleHidden;
			this._categoryHierarchy = categoryHierarchy;
			this._seriesHierarchy = seriesHierarchy;
			this._chartData = chartData;
			this._legendPosition = legendPosition;
			this._legendHidden = legendHidden;
			this._scalarXAxis = scalarXAxis;
			this._fontSizeOffset = fontSizeOffset;
			this._experimentalFeaturesEnabled = experimentalFeaturesEnabled;
			this._numberOfVisibleForecastPoints = numberOfVisibleForecastPoints;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000CB6F File Offset: 0x0000AD6F
		internal ChartCategoryHierarchy CategoryHierarchy
		{
			get
			{
				return this._categoryHierarchy;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000CB77 File Offset: 0x0000AD77
		internal ChartSeriesHierarchy SeriesHierarchy
		{
			get
			{
				return this._seriesHierarchy;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000CB7F File Offset: 0x0000AD7F
		internal ChartData ChartData
		{
			get
			{
				return this._chartData;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000CB88 File Offset: 0x0000AD88
		internal string Type
		{
			get
			{
				ChartSeries firstSeries = this.GetFirstSeries();
				if (firstSeries != null)
				{
					return firstSeries.Type;
				}
				return "Category";
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000CBAC File Offset: 0x0000ADAC
		internal string Subtype
		{
			get
			{
				ChartSeries firstSeries = this.GetFirstSeries();
				if (firstSeries != null)
				{
					return firstSeries.Subtype;
				}
				return "Column";
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000CBD0 File Offset: 0x0000ADD0
		internal MapBackdropType MapBackdrop
		{
			get
			{
				ChartSeries firstSeries = this.GetFirstSeries();
				if (firstSeries != null)
				{
					return firstSeries.MapBackdrop;
				}
				return MapBackdropType.Road;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000CBEF File Offset: 0x0000ADEF
		internal ChartPositions LegendPosition
		{
			get
			{
				return this._legendPosition;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000CBF8 File Offset: 0x0000ADF8
		internal bool IsLabelsVisible
		{
			get
			{
				ChartSeries firstSeries = this.GetFirstSeries();
				return firstSeries != null && firstSeries.IsLabelsVisible;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000CC18 File Offset: 0x0000AE18
		internal ChartDataLabelPositions LabelsPositions
		{
			get
			{
				ChartSeries firstSeries = this.GetFirstSeries();
				if (firstSeries != null)
				{
					return firstSeries.LabelsPosition;
				}
				return ChartDataLabelPositions.Auto;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000CC37 File Offset: 0x0000AE37
		internal bool ChartTitleHidden
		{
			get
			{
				return this._chartTitleHidden;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000CC3F File Offset: 0x0000AE3F
		internal bool LegendHidden
		{
			get
			{
				return this._legendHidden;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000CC47 File Offset: 0x0000AE47
		internal bool ScalarXAxis
		{
			get
			{
				return this._scalarXAxis;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000CC4F File Offset: 0x0000AE4F
		internal int FontSizeOffset
		{
			get
			{
				return this._fontSizeOffset;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000CC57 File Offset: 0x0000AE57
		internal bool ExperimentalFeaturesEnabled
		{
			get
			{
				return this._experimentalFeaturesEnabled;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000CC5F File Offset: 0x0000AE5F
		internal int NumberOfVisibleForecastPoints
		{
			get
			{
				return this._numberOfVisibleForecastPoints;
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000CC68 File Offset: 0x0000AE68
		private ChartSeries GetFirstSeries()
		{
			ChartData chartData = this.ChartData;
			if (chartData != null)
			{
				List<ChartSeries> chartSeries = chartData.ChartSeries;
				if (chartSeries.Count > 0)
				{
					return chartSeries[0];
				}
			}
			return null;
		}

		// Token: 0x040001D4 RID: 468
		private readonly string _chartTitle;

		// Token: 0x040001D5 RID: 469
		private readonly bool _chartTitleHidden;

		// Token: 0x040001D6 RID: 470
		private readonly ChartCategoryHierarchy _categoryHierarchy;

		// Token: 0x040001D7 RID: 471
		private readonly ChartSeriesHierarchy _seriesHierarchy;

		// Token: 0x040001D8 RID: 472
		private readonly ChartData _chartData;

		// Token: 0x040001D9 RID: 473
		private readonly ChartPositions _legendPosition;

		// Token: 0x040001DA RID: 474
		private readonly bool _legendHidden;

		// Token: 0x040001DB RID: 475
		private readonly bool _scalarXAxis;

		// Token: 0x040001DC RID: 476
		private readonly int _fontSizeOffset;

		// Token: 0x040001DD RID: 477
		private readonly bool _experimentalFeaturesEnabled;

		// Token: 0x040001DE RID: 478
		private readonly int _numberOfVisibleForecastPoints;
	}
}
