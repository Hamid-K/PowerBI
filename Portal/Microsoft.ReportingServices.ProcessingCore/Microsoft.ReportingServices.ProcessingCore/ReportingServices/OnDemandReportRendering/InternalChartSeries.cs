using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000228 RID: 552
	internal class InternalChartSeries : ChartSeries, IROMActionOwner
	{
		// Token: 0x060014CC RID: 5324 RVA: 0x00054904 File Offset: 0x00052B04
		internal InternalChartSeries(ChartDerivedSeries parentDerivedSeries)
			: this(parentDerivedSeries.ChartDef, 0, parentDerivedSeries.ChartDerivedSeriesDef.Series)
		{
			this.m_parentDerivedSeries = parentDerivedSeries;
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x00054925 File Offset: 0x00052B25
		internal InternalChartSeries(Microsoft.ReportingServices.OnDemandReportRendering.Chart chart, int seriesIndex, ChartSeries seriesDef)
			: base(chart, seriesIndex)
		{
			this.m_chartSeriesDef = seriesDef;
		}

		// Token: 0x17000B1A RID: 2842
		public override Microsoft.ReportingServices.OnDemandReportRendering.ChartDataPoint this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				if (this.m_chartDataPoints == null)
				{
					this.m_chartDataPoints = new Microsoft.ReportingServices.OnDemandReportRendering.ChartDataPoint[this.Count];
				}
				if (this.m_chartDataPoints[index] == null)
				{
					this.m_chartDataPoints[index] = new InternalChartDataPoint(this.m_chart, this.m_seriesIndex, index, this.m_chartSeriesDef.DataPoints[index]);
				}
				return this.m_chartDataPoints[index];
			}
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x060014CF RID: 5327 RVA: 0x000549D9 File Offset: 0x00052BD9
		public override int Count
		{
			get
			{
				return this.m_chartSeriesDef.Cells.Count;
			}
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x060014D0 RID: 5328 RVA: 0x000549EB File Offset: 0x00052BEB
		public override string Name
		{
			get
			{
				return this.m_chartSeriesDef.Name;
			}
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x060014D1 RID: 5329 RVA: 0x000549F8 File Offset: 0x00052BF8
		public string UniqueName
		{
			get
			{
				return this.m_chartSeriesDef.UniqueName;
			}
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x060014D2 RID: 5330 RVA: 0x00054A08 File Offset: 0x00052C08
		public override Microsoft.ReportingServices.OnDemandReportRendering.Style Style
		{
			get
			{
				if (this.m_style == null && this.m_chartSeriesDef.StyleClass != null)
				{
					this.m_style = new Microsoft.ReportingServices.OnDemandReportRendering.Style(this.m_chart, this.ReportScope, this.m_chartSeriesDef, this.m_chart.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x00054A58 File Offset: 0x00052C58
		internal override ActionInfo ActionInfo
		{
			get
			{
				if (this.m_actionInfo == null && this.m_chartSeriesDef.Action != null)
				{
					this.m_actionInfo = new ActionInfo(this.m_chart.RenderingContext, this.ReportScope, this.m_chartSeriesDef.Action, this.m_chartSeriesDef, this.m_chart, ObjectType.Chart, this.m_chart.Name, this);
				}
				return this.m_actionInfo;
			}
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x060014D4 RID: 5332 RVA: 0x00054AC1 File Offset: 0x00052CC1
		public List<string> FieldsUsedInValueExpression
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x060014D5 RID: 5333 RVA: 0x00054AC4 File Offset: 0x00052CC4
		public override CustomPropertyCollection CustomProperties
		{
			get
			{
				if (this.m_customProperties == null)
				{
					this.m_customPropertiesReady = true;
					this.m_customProperties = new CustomPropertyCollection(this.m_chart.ReportScope.ReportScopeInstance, this.m_chart.RenderingContext, null, this.m_chartSeriesDef, ObjectType.Chart, this.m_chart.Name);
				}
				else if (!this.m_customPropertiesReady)
				{
					this.m_customPropertiesReady = true;
					this.m_customProperties.UpdateCustomProperties(this.ReportScope.ReportScopeInstance, this.m_chartSeriesDef, this.m_chart.RenderingContext.OdpContext, ObjectType.Chart, this.m_chart.Name);
				}
				return this.m_customProperties;
			}
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x060014D6 RID: 5334 RVA: 0x00054B6C File Offset: 0x00052D6C
		public override ReportEnumProperty<ChartSeriesType> Type
		{
			get
			{
				if (this.m_type == null && this.m_chartSeriesDef.Type != null)
				{
					this.m_type = new ReportEnumProperty<ChartSeriesType>(this.m_chartSeriesDef.Type.IsExpression, this.m_chartSeriesDef.Type.OriginalText, EnumTranslator.TranslateChartSeriesType(this.m_chartSeriesDef.Type.StringValue, null));
				}
				return this.m_type;
			}
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x060014D7 RID: 5335 RVA: 0x00054BD8 File Offset: 0x00052DD8
		public override ReportEnumProperty<ChartSeriesSubtype> Subtype
		{
			get
			{
				if (this.m_subtype == null && this.m_chartSeriesDef.Subtype != null)
				{
					this.m_subtype = new ReportEnumProperty<ChartSeriesSubtype>(this.m_chartSeriesDef.Subtype.IsExpression, this.m_chartSeriesDef.Subtype.OriginalText, EnumTranslator.TranslateChartSeriesSubtype(this.m_chartSeriesDef.Subtype.StringValue, null));
				}
				return this.m_subtype;
			}
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x060014D8 RID: 5336 RVA: 0x00054C41 File Offset: 0x00052E41
		public override ChartSmartLabel SmartLabel
		{
			get
			{
				if (this.m_smartLabel == null && this.m_chartSeriesDef.ChartSmartLabel != null)
				{
					this.m_smartLabel = new ChartSmartLabel(this, this.m_chartSeriesDef.ChartSmartLabel, this.m_chart);
				}
				return this.m_smartLabel;
			}
		}

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x060014D9 RID: 5337 RVA: 0x00054C7B File Offset: 0x00052E7B
		public override ChartEmptyPoints EmptyPoints
		{
			get
			{
				if (this.m_emptyPoints == null && this.m_chartSeriesDef.EmptyPoints != null)
				{
					this.m_emptyPoints = new ChartEmptyPoints(this, this.m_chartSeriesDef.EmptyPoints, this.m_chart);
				}
				return this.m_emptyPoints;
			}
		}

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x060014DA RID: 5338 RVA: 0x00054CB5 File Offset: 0x00052EB5
		public override ReportStringProperty LegendName
		{
			get
			{
				if (this.m_legendName == null && this.m_chartSeriesDef.LegendName != null)
				{
					this.m_legendName = new ReportStringProperty(this.m_chartSeriesDef.LegendName);
				}
				return this.m_legendName;
			}
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x060014DB RID: 5339 RVA: 0x00054CE8 File Offset: 0x00052EE8
		internal override ReportStringProperty LegendText
		{
			get
			{
				if (this.m_legendText == null && this.m_chartSeriesDef.LegendText != null)
				{
					this.m_legendText = new ReportStringProperty(this.m_chartSeriesDef.LegendText);
				}
				return this.m_legendText;
			}
		}

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x060014DC RID: 5340 RVA: 0x00054D1B File Offset: 0x00052F1B
		internal override ReportBoolProperty HideInLegend
		{
			get
			{
				if (this.m_hideInLegend == null && this.m_chartSeriesDef.HideInLegend != null)
				{
					this.m_hideInLegend = new ReportBoolProperty(this.m_chartSeriesDef.HideInLegend);
				}
				return this.m_hideInLegend;
			}
		}

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x060014DD RID: 5341 RVA: 0x00054D4E File Offset: 0x00052F4E
		public override ReportStringProperty ChartAreaName
		{
			get
			{
				if (this.m_chartAreaName == null && this.m_chartSeriesDef.ChartAreaName != null)
				{
					this.m_chartAreaName = new ReportStringProperty(this.m_chartSeriesDef.ChartAreaName);
				}
				return this.m_chartAreaName;
			}
		}

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x060014DE RID: 5342 RVA: 0x00054D81 File Offset: 0x00052F81
		public override ReportStringProperty ValueAxisName
		{
			get
			{
				if (this.m_valueAxisName == null && this.m_chartSeriesDef.ValueAxisName != null)
				{
					this.m_valueAxisName = new ReportStringProperty(this.m_chartSeriesDef.ValueAxisName);
				}
				return this.m_valueAxisName;
			}
		}

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x060014DF RID: 5343 RVA: 0x00054DB4 File Offset: 0x00052FB4
		public override ReportStringProperty CategoryAxisName
		{
			get
			{
				if (this.m_categoryAxisName == null && this.m_chartSeriesDef.CategoryAxisName != null)
				{
					this.m_categoryAxisName = new ReportStringProperty(this.m_chartSeriesDef.CategoryAxisName);
				}
				return this.m_categoryAxisName;
			}
		}

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x060014E0 RID: 5344 RVA: 0x00054DE7 File Offset: 0x00052FE7
		public override Microsoft.ReportingServices.OnDemandReportRendering.ChartDataLabel DataLabel
		{
			get
			{
				if (this.m_dataLabel == null && this.m_chartSeriesDef.DataLabel != null)
				{
					this.m_dataLabel = new Microsoft.ReportingServices.OnDemandReportRendering.ChartDataLabel(this, this.m_chartSeriesDef.DataLabel, this.m_chart);
				}
				return this.m_dataLabel;
			}
		}

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x060014E1 RID: 5345 RVA: 0x00054E21 File Offset: 0x00053021
		public override ChartMarker Marker
		{
			get
			{
				if (this.m_marker == null && this.m_chartSeriesDef.Marker != null)
				{
					this.m_marker = new ChartMarker(this, this.m_chartSeriesDef.Marker, this.m_chart);
				}
				return this.m_marker;
			}
		}

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x060014E2 RID: 5346 RVA: 0x00054E5B File Offset: 0x0005305B
		internal override ReportStringProperty ToolTip
		{
			get
			{
				if (this.m_toolTip == null && this.m_chartSeriesDef.ToolTip != null)
				{
					this.m_toolTip = new ReportStringProperty(this.m_chartSeriesDef.ToolTip);
				}
				return this.m_toolTip;
			}
		}

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x060014E3 RID: 5347 RVA: 0x00054E8E File Offset: 0x0005308E
		public override ReportBoolProperty Hidden
		{
			get
			{
				if (this.m_hidden == null && this.m_chartSeriesDef.Hidden != null)
				{
					this.m_hidden = new ReportBoolProperty(this.m_chartSeriesDef.Hidden);
				}
				return this.m_hidden;
			}
		}

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x060014E4 RID: 5348 RVA: 0x00054EC1 File Offset: 0x000530C1
		public override ChartItemInLegend ChartItemInLegend
		{
			get
			{
				if (this.m_chartItemInLegend == null && this.m_chartSeriesDef.ChartItemInLegend != null)
				{
					this.m_chartItemInLegend = new ChartItemInLegend(this, this.m_chartSeriesDef.ChartItemInLegend, this.m_chart);
				}
				return this.m_chartItemInLegend;
			}
		}

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x060014E5 RID: 5349 RVA: 0x00054EFB File Offset: 0x000530FB
		internal ChartSeries ChartSeriesDef
		{
			get
			{
				return this.m_chartSeriesDef;
			}
		}

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x00054F03 File Offset: 0x00053103
		internal Microsoft.ReportingServices.OnDemandReportRendering.Chart ChartDef
		{
			get
			{
				return this.m_chart;
			}
		}

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x060014E7 RID: 5351 RVA: 0x00054F0B File Offset: 0x0005310B
		internal IReportScope ReportScope
		{
			get
			{
				if (this.m_reportScope == null)
				{
					if (this.m_parentDerivedSeries == null)
					{
						this.m_reportScope = this.m_chart.GetChartMember(this);
					}
					else
					{
						this.m_reportScope = this.m_parentDerivedSeries.ReportScope;
					}
				}
				return this.m_reportScope;
			}
		}

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x060014E8 RID: 5352 RVA: 0x00054F48 File Offset: 0x00053148
		public override ChartSeriesInstance Instance
		{
			get
			{
				if (this.m_chart.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new ChartSeriesInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x060014E9 RID: 5353 RVA: 0x00054F78 File Offset: 0x00053178
		internal List<ChartDerivedSeries> ChildrenDerivedSeries
		{
			get
			{
				if (this.m_childrenDerivedSeries == null)
				{
					this.m_childrenDerivedSeries = this.m_chart.GetChildrenDerivedSeries(this.Name);
				}
				return this.m_childrenDerivedSeries;
			}
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x00054FA0 File Offset: 0x000531A0
		internal override void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			if (this.m_actionInfo != null)
			{
				this.m_actionInfo.SetNewContext();
			}
			if (this.m_emptyPoints != null)
			{
				this.m_emptyPoints.SetNewContext();
			}
			if (this.m_smartLabel != null)
			{
				this.m_smartLabel.SetNewContext();
			}
			if (this.m_dataLabel != null)
			{
				this.m_dataLabel.SetNewContext();
			}
			if (this.m_marker != null)
			{
				this.m_marker.SetNewContext();
			}
			if (this.m_chartDataPoints != null)
			{
				foreach (Microsoft.ReportingServices.OnDemandReportRendering.ChartDataPoint chartDataPoint in this.m_chartDataPoints)
				{
					if (chartDataPoint != null)
					{
						chartDataPoint.SetNewContext();
					}
				}
			}
			List<ChartDerivedSeries> childrenDerivedSeries = this.ChildrenDerivedSeries;
			if (childrenDerivedSeries != null)
			{
				foreach (ChartDerivedSeries chartDerivedSeries in childrenDerivedSeries)
				{
					if (chartDerivedSeries != null)
					{
						chartDerivedSeries.SetNewContext();
					}
				}
			}
			if (this.m_chartItemInLegend != null)
			{
				this.m_chartItemInLegend.SetNewContext();
			}
			this.m_customPropertiesReady = false;
		}

		// Token: 0x040009C8 RID: 2504
		private ChartSeries m_chartSeriesDef;

		// Token: 0x040009C9 RID: 2505
		private ChartSeriesInstance m_instance;

		// Token: 0x040009CA RID: 2506
		private Microsoft.ReportingServices.OnDemandReportRendering.Style m_style;

		// Token: 0x040009CB RID: 2507
		private ActionInfo m_actionInfo;

		// Token: 0x040009CC RID: 2508
		private ReportEnumProperty<ChartSeriesType> m_type;

		// Token: 0x040009CD RID: 2509
		private ReportEnumProperty<ChartSeriesSubtype> m_subtype;

		// Token: 0x040009CE RID: 2510
		private ChartSmartLabel m_smartLabel;

		// Token: 0x040009CF RID: 2511
		private ChartEmptyPoints m_emptyPoints;

		// Token: 0x040009D0 RID: 2512
		private ReportStringProperty m_legendName;

		// Token: 0x040009D1 RID: 2513
		private ReportStringProperty m_legendText;

		// Token: 0x040009D2 RID: 2514
		private ReportBoolProperty m_hideInLegend;

		// Token: 0x040009D3 RID: 2515
		private ReportStringProperty m_chartAreaName;

		// Token: 0x040009D4 RID: 2516
		private ReportStringProperty m_valueAxisName;

		// Token: 0x040009D5 RID: 2517
		private ReportStringProperty m_categoryAxisName;

		// Token: 0x040009D6 RID: 2518
		private CustomPropertyCollection m_customProperties;

		// Token: 0x040009D7 RID: 2519
		private bool m_customPropertiesReady;

		// Token: 0x040009D8 RID: 2520
		private Microsoft.ReportingServices.OnDemandReportRendering.ChartDataLabel m_dataLabel;

		// Token: 0x040009D9 RID: 2521
		private ChartMarker m_marker;

		// Token: 0x040009DA RID: 2522
		private IReportScope m_reportScope;

		// Token: 0x040009DB RID: 2523
		private ChartDerivedSeries m_parentDerivedSeries;

		// Token: 0x040009DC RID: 2524
		private List<ChartDerivedSeries> m_childrenDerivedSeries;

		// Token: 0x040009DD RID: 2525
		private ReportStringProperty m_toolTip;

		// Token: 0x040009DE RID: 2526
		private ReportBoolProperty m_hidden;

		// Token: 0x040009DF RID: 2527
		private ChartItemInLegend m_chartItemInLegend;
	}
}
