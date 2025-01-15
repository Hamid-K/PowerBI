using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200004A RID: 74
	internal sealed class Chart : DataRegion
	{
		// Token: 0x060005B5 RID: 1461 RVA: 0x00013750 File Offset: 0x00011950
		internal Chart(int intUniqueName, Chart reportItemDef, ChartInstance reportItemInstance, RenderingContext renderingContext)
			: base(intUniqueName, reportItemDef, reportItemInstance, renderingContext)
		{
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x00013774 File Offset: 0x00011974
		public ChartDataPointCollection DataPointCollection
		{
			get
			{
				ChartDataPointCollection chartDataPointCollection = this.m_datapoints;
				if (this.m_datapoints == null)
				{
					int num;
					int num2;
					if (base.ReportItemInstance != null && 0 < ((ChartInstance)base.ReportItemInstance).DataPoints.Count)
					{
						num = this.DataPointSeriesCount;
						num2 = this.DataPointCategoryCount;
					}
					else
					{
						num = ((Chart)base.ReportItemDef).StaticSeriesCount;
						num2 = ((Chart)base.ReportItemDef).StaticCategoryCount;
					}
					chartDataPointCollection = new ChartDataPointCollection(this, num, num2);
					if (base.RenderingContext.CacheState)
					{
						this.m_datapoints = chartDataPointCollection;
					}
				}
				return chartDataPointCollection;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x00013804 File Offset: 0x00011A04
		public ChartMemberCollection CategoryMemberCollection
		{
			get
			{
				ChartMemberCollection chartMemberCollection = this.m_categories;
				if (this.m_categories == null)
				{
					chartMemberCollection = new ChartMemberCollection(this, null, ((Chart)base.ReportItemDef).Columns, (base.ReportItemInstance == null) ? null : ((ChartInstance)base.ReportItemInstance).ColumnInstances);
					if (base.RenderingContext.CacheState)
					{
						this.m_categories = chartMemberCollection;
					}
				}
				return chartMemberCollection;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x00013868 File Offset: 0x00011A68
		public ChartMemberCollection SeriesMemberCollection
		{
			get
			{
				ChartMemberCollection chartMemberCollection = this.m_series;
				if (this.m_series == null)
				{
					chartMemberCollection = new ChartMemberCollection(this, null, ((Chart)base.ReportItemDef).Rows, (base.ReportItemInstance == null) ? null : ((ChartInstance)base.ReportItemInstance).RowInstances);
					if (base.RenderingContext.CacheState)
					{
						this.m_series = chartMemberCollection;
					}
				}
				return chartMemberCollection;
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x000138CC File Offset: 0x00011ACC
		public int DataPointCategoryCount
		{
			get
			{
				if (base.ReportItemInstance == null)
				{
					return 0;
				}
				return ((ChartInstance)base.ReportItemInstance).DataPointCategoryCount;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x000138E8 File Offset: 0x00011AE8
		public int DataPointSeriesCount
		{
			get
			{
				if (base.ReportItemInstance == null)
				{
					return 0;
				}
				return ((ChartInstance)base.ReportItemInstance).DataPointSeriesCount;
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x00013904 File Offset: 0x00011B04
		public int CategoriesCount
		{
			get
			{
				return ((Chart)base.ReportItemDef).StaticCategoryCount;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x00013916 File Offset: 0x00011B16
		public int SeriesCount
		{
			get
			{
				return ((Chart)base.ReportItemDef).StaticSeriesCount;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x00013928 File Offset: 0x00011B28
		public override bool NoRows
		{
			get
			{
				return base.ReportItemInstance == null || ((ChartInstance)base.ReportItemInstance).DataPointSeriesCount == 0 || ((ChartInstance)base.ReportItemInstance).DataPointCategoryCount == 0;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x00013959 File Offset: 0x00011B59
		internal ChartDataPointInstancesList DataPoints
		{
			get
			{
				return ((ChartInstance)base.ReportItemInstance).DataPoints;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x0001396C File Offset: 0x00011B6C
		internal int CategoryGroupingLevels
		{
			get
			{
				if (this.m_categoryGroupingLevels == 0)
				{
					this.m_categoryGroupingLevels = 1;
					ChartHeading chartHeading = ((Chart)base.ReportItemDef).Columns;
					Global.Tracer.Assert(chartHeading != null);
					while (chartHeading.SubHeading != null)
					{
						chartHeading = chartHeading.SubHeading;
						this.m_categoryGroupingLevels++;
					}
				}
				return this.m_categoryGroupingLevels;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x000139CC File Offset: 0x00011BCC
		internal override string InstanceInfoNoRowMessage
		{
			get
			{
				if (base.InstanceInfo != null)
				{
					return ((ChartInstanceInfo)base.InstanceInfo).NoRows;
				}
				return null;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x000139E8 File Offset: 0x00011BE8
		internal ImageMapAreasCollection[] DataPointMapAreas
		{
			get
			{
				if (this.m_imageMapAreaCollection == null)
				{
					this.RenderChartImageMap();
				}
				return this.m_imageMapAreaCollection;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x000139FF File Offset: 0x00011BFF
		// (set) Token: 0x060005C3 RID: 1475 RVA: 0x00013A07 File Offset: 0x00011C07
		internal float ScaleX
		{
			get
			{
				return this.m_scaleX;
			}
			set
			{
				this.m_scaleX = value;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x00013A10 File Offset: 0x00011C10
		// (set) Token: 0x060005C5 RID: 1477 RVA: 0x00013A18 File Offset: 0x00011C18
		internal float ScaleY
		{
			get
			{
				return this.m_scaleY;
			}
			set
			{
				this.m_scaleY = value;
			}
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00013A21 File Offset: 0x00011C21
		public void SetDpi(int xDpi, int yDpi)
		{
			if (xDpi > 0 && xDpi != 96)
			{
				this.m_scaleX = (float)xDpi / 96f;
			}
			if (yDpi > 0 && yDpi != 96)
			{
				this.m_scaleY = (float)yDpi / 96f;
			}
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00013A51 File Offset: 0x00011C51
		public MemoryStream GetImage(out bool hasImageMap)
		{
			return this.GetImage(Chart.ImageType.PNG, out hasImageMap);
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00013A5C File Offset: 0x00011C5C
		public MemoryStream GetImage()
		{
			bool flag;
			return this.GetImage(Chart.ImageType.PNG, out flag);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00013A74 File Offset: 0x00011C74
		public MemoryStream GetImage(Chart.ImageType type)
		{
			bool flag;
			return this.GetImage(type, out flag);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00013A8C File Offset: 0x00011C8C
		public MemoryStream GetImage(Chart.ImageType type, out bool hasImageMap)
		{
			hasImageMap = false;
			CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
			MemoryStream image;
			try
			{
				if (this.m_dundasChart == null)
				{
					this.m_dundasChart = new DundasChart(this);
				}
				ChartInstanceInfo chartInstanceInfo = (ChartInstanceInfo)base.InstanceInfo;
				if (chartInstanceInfo != null)
				{
					Style styleClass = ((Chart)base.ReportItemDef).StyleClass;
					if (chartInstanceInfo.CultureName == null)
					{
						Thread.CurrentThread.CurrentCulture = Localization.DefaultReportServerSpecificCulture;
					}
					else
					{
						CultureInfo cultureInfo = new CultureInfo(chartInstanceInfo.CultureName, false);
						if (cultureInfo.IsNeutralCulture)
						{
							cultureInfo = CultureInfo.CreateSpecificCulture(chartInstanceInfo.CultureName);
							cultureInfo = new CultureInfo(cultureInfo.Name, false);
						}
						string text = (string)Style.GetStyleValue("Calendar", styleClass, chartInstanceInfo.StyleAttributeValues);
						Global.Tracer.Assert(text != null);
						Global.Tracer.Assert(cultureInfo != null);
						Calendar calendar = ProcessingValidator.CreateCalendar(text);
						if (calendar != null)
						{
							cultureInfo.DateTimeFormat.Calendar = calendar;
						}
						Thread.CurrentThread.CurrentCulture = cultureInfo;
					}
				}
				image = this.m_dundasChart.GetImage(type, chartInstanceInfo, out hasImageMap);
			}
			catch (Exception ex)
			{
				Global.Tracer.Trace(TraceLevel.Error, ex.ToString());
				throw new RenderingObjectModelException(ErrorCode.rsErrorDuringChartRendering, ex, new object[]
				{
					base.ReportItemDef.Name,
					ex.Message
				});
			}
			finally
			{
				if (currentCulture != null)
				{
					Thread.CurrentThread.CurrentCulture = currentCulture;
				}
			}
			return image;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00013C18 File Offset: 0x00011E18
		private bool RenderChartImageMap()
		{
			if (this.m_dundasChart == null)
			{
				return false;
			}
			this.m_dundasChart.RenderChartMapAreas(ref this.m_imageMapAreaCollection);
			return this.m_imageMapAreaCollection != null;
		}

		// Token: 0x0400015E RID: 350
		private DundasChart m_dundasChart;

		// Token: 0x0400015F RID: 351
		private ChartDataPointCollection m_datapoints;

		// Token: 0x04000160 RID: 352
		private ChartMemberCollection m_categories;

		// Token: 0x04000161 RID: 353
		private ChartMemberCollection m_series;

		// Token: 0x04000162 RID: 354
		private int m_categoryGroupingLevels;

		// Token: 0x04000163 RID: 355
		private ImageMapAreasCollection[] m_imageMapAreaCollection;

		// Token: 0x04000164 RID: 356
		private float m_scaleX = -1f;

		// Token: 0x04000165 RID: 357
		private float m_scaleY = -1f;

		// Token: 0x02000910 RID: 2320
		public enum ImageType
		{
			// Token: 0x04003EF1 RID: 16113
			PNG,
			// Token: 0x04003EF2 RID: 16114
			EMF
		}
	}
}
