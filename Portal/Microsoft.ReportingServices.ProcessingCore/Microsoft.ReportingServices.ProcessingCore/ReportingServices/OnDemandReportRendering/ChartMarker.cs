using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200022F RID: 559
	public sealed class ChartMarker : IROMStyleDefinitionContainer
	{
		// Token: 0x06001570 RID: 5488 RVA: 0x00056651 File Offset: 0x00054851
		internal ChartMarker(Microsoft.ReportingServices.OnDemandReportRendering.ChartDataPoint dataPoint, ChartMarker markerDef, Microsoft.ReportingServices.OnDemandReportRendering.Chart chart)
			: this(markerDef, chart)
		{
			this.m_dataPoint = dataPoint;
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x00056662 File Offset: 0x00054862
		internal ChartMarker(InternalChartSeries chartSeries, ChartMarker markerDef, Microsoft.ReportingServices.OnDemandReportRendering.Chart chart)
			: this(markerDef, chart)
		{
			this.m_chartSeries = chartSeries;
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x00056673 File Offset: 0x00054873
		internal ChartMarker(ChartMarker markerDef, Microsoft.ReportingServices.OnDemandReportRendering.Chart chart)
		{
			this.m_markerDef = markerDef;
			this.m_chart = chart;
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x00056689 File Offset: 0x00054889
		internal ChartMarker(Microsoft.ReportingServices.OnDemandReportRendering.ChartDataPoint dataPoint, Microsoft.ReportingServices.OnDemandReportRendering.Chart chart)
		{
			this.m_dataPoint = dataPoint;
			this.m_chart = chart;
		}

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x06001574 RID: 5492 RVA: 0x000566A0 File Offset: 0x000548A0
		public Microsoft.ReportingServices.OnDemandReportRendering.Style Style
		{
			get
			{
				if (this.m_style == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						this.m_style = new Microsoft.ReportingServices.OnDemandReportRendering.Style(this.m_dataPoint.RenderDataPointDef.MarkerStyleClass, this.m_dataPoint.RenderItem.InstanceInfo.MarkerStyleAttributeValues, this.m_chart.RenderingContext);
					}
					else if (this.m_markerDef.StyleClass != null)
					{
						this.m_style = new Microsoft.ReportingServices.OnDemandReportRendering.Style(this.m_chart, this.ReportScope, this.m_markerDef, this.m_chart.RenderingContext);
					}
				}
				return this.m_style;
			}
		}

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x06001575 RID: 5493 RVA: 0x0005673C File Offset: 0x0005493C
		public ReportEnumProperty<ChartMarkerTypes> Type
		{
			get
			{
				if (this.m_type == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						ChartMarkerTypes chartMarkerTypes = ChartMarkerTypes.None;
						switch (this.m_dataPoint.RenderDataPointDef.MarkerType)
						{
						case Microsoft.ReportingServices.ReportProcessing.ChartDataPoint.MarkerTypes.None:
							chartMarkerTypes = ChartMarkerTypes.None;
							break;
						case Microsoft.ReportingServices.ReportProcessing.ChartDataPoint.MarkerTypes.Square:
							chartMarkerTypes = ChartMarkerTypes.Square;
							break;
						case Microsoft.ReportingServices.ReportProcessing.ChartDataPoint.MarkerTypes.Circle:
							chartMarkerTypes = ChartMarkerTypes.Circle;
							break;
						case Microsoft.ReportingServices.ReportProcessing.ChartDataPoint.MarkerTypes.Diamond:
							chartMarkerTypes = ChartMarkerTypes.Diamond;
							break;
						case Microsoft.ReportingServices.ReportProcessing.ChartDataPoint.MarkerTypes.Triangle:
							chartMarkerTypes = ChartMarkerTypes.Triangle;
							break;
						case Microsoft.ReportingServices.ReportProcessing.ChartDataPoint.MarkerTypes.Cross:
							chartMarkerTypes = ChartMarkerTypes.Cross;
							break;
						case Microsoft.ReportingServices.ReportProcessing.ChartDataPoint.MarkerTypes.Auto:
							chartMarkerTypes = ChartMarkerTypes.Auto;
							break;
						}
						this.m_type = new ReportEnumProperty<ChartMarkerTypes>(chartMarkerTypes);
					}
					else if (this.m_markerDef.Type != null)
					{
						this.m_type = new ReportEnumProperty<ChartMarkerTypes>(this.m_markerDef.Type.IsExpression, this.m_markerDef.Type.OriginalText, EnumTranslator.TranslateChartMarkerType(this.m_markerDef.Type.StringValue, null));
					}
				}
				return this.m_type;
			}
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x06001576 RID: 5494 RVA: 0x00056818 File Offset: 0x00054A18
		public ReportSizeProperty Size
		{
			get
			{
				if (this.m_size == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						string text;
						if (this.m_dataPoint.RenderDataPointDef.MarkerSize != null)
						{
							text = this.m_dataPoint.RenderDataPointDef.MarkerSize;
						}
						else
						{
							text = "5pt";
						}
						this.m_size = new ReportSizeProperty(false, text, new ReportSize(text));
					}
					else if (this.m_markerDef.Size != null)
					{
						this.m_size = new ReportSizeProperty(this.m_markerDef.Size, new ReportSize("5pt"));
					}
				}
				return this.m_size;
			}
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06001577 RID: 5495 RVA: 0x000568AD File Offset: 0x00054AAD
		internal Microsoft.ReportingServices.OnDemandReportRendering.Chart ChartDef
		{
			get
			{
				return this.m_chart;
			}
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06001578 RID: 5496 RVA: 0x000568B5 File Offset: 0x00054AB5
		internal ChartMarker MarkerDef
		{
			get
			{
				return this.m_markerDef;
			}
		}

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x06001579 RID: 5497 RVA: 0x000568BD File Offset: 0x00054ABD
		internal IReportScope ReportScope
		{
			get
			{
				if (this.m_dataPoint != null)
				{
					return this.m_dataPoint;
				}
				if (this.m_chartSeries != null)
				{
					return this.m_chartSeries.ReportScope;
				}
				return this.m_chart;
			}
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x000568E8 File Offset: 0x00054AE8
		public ChartMarkerInstance Instance
		{
			get
			{
				if (this.m_chart.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new ChartMarkerInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x00056918 File Offset: 0x00054B18
		internal void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
		}

		// Token: 0x04000A1C RID: 2588
		private ChartMarker m_markerDef;

		// Token: 0x04000A1D RID: 2589
		private Microsoft.ReportingServices.OnDemandReportRendering.Chart m_chart;

		// Token: 0x04000A1E RID: 2590
		private ChartMarkerInstance m_instance;

		// Token: 0x04000A1F RID: 2591
		private Microsoft.ReportingServices.OnDemandReportRendering.Style m_style;

		// Token: 0x04000A20 RID: 2592
		private ReportSizeProperty m_size;

		// Token: 0x04000A21 RID: 2593
		private ReportEnumProperty<ChartMarkerTypes> m_type;

		// Token: 0x04000A22 RID: 2594
		private Microsoft.ReportingServices.OnDemandReportRendering.ChartDataPoint m_dataPoint;

		// Token: 0x04000A23 RID: 2595
		private InternalChartSeries m_chartSeries;
	}
}
