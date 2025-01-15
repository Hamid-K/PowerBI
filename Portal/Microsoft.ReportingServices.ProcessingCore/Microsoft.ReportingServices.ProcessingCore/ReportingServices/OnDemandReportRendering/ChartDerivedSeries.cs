using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200023C RID: 572
	public sealed class ChartDerivedSeries : ChartObjectCollectionItem<BaseInstance>
	{
		// Token: 0x060015FC RID: 5628 RVA: 0x00058C21 File Offset: 0x00056E21
		internal ChartDerivedSeries(ChartDerivedSeries chartDerivedSeriesDef, Chart chart)
		{
			this.m_chartDerivedSeriesDef = chartDerivedSeriesDef;
			this.m_chart = chart;
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x060015FD RID: 5629 RVA: 0x00058C37 File Offset: 0x00056E37
		public ChartSeries Series
		{
			get
			{
				if (this.m_series == null && !this.m_chart.IsOldSnapshot && this.m_chartDerivedSeriesDef.Series != null)
				{
					this.m_series = new InternalChartSeries(this);
				}
				return this.m_series;
			}
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x060015FE RID: 5630 RVA: 0x00058C6D File Offset: 0x00056E6D
		public ChartFormulaParameterCollection FormulaParameters
		{
			get
			{
				if (this.m_chartFormulaParameters == null && !this.m_chart.IsOldSnapshot && this.ChartDerivedSeriesDef.FormulaParameters != null)
				{
					this.m_chartFormulaParameters = new ChartFormulaParameterCollection(this, this.m_chart);
				}
				return this.m_chartFormulaParameters;
			}
		}

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x060015FF RID: 5631 RVA: 0x00058CA9 File Offset: 0x00056EA9
		public string SourceChartSeriesName
		{
			get
			{
				if (this.m_chart.IsOldSnapshot)
				{
					return null;
				}
				return this.m_chartDerivedSeriesDef.SourceChartSeriesName;
			}
		}

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x06001600 RID: 5632 RVA: 0x00058CC5 File Offset: 0x00056EC5
		internal InternalChartSeries SourceSeries
		{
			get
			{
				if (this.m_sourceSeries == null)
				{
					this.m_sourceSeries = ((InternalChartSeriesCollection)this.m_chart.ChartData.SeriesCollection).GetByName(this.SourceChartSeriesName);
				}
				return this.m_sourceSeries;
			}
		}

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x06001601 RID: 5633 RVA: 0x00058CFB File Offset: 0x00056EFB
		public ChartSeriesFormula DerivedSeriesFormula
		{
			get
			{
				return this.m_chartDerivedSeriesDef.DerivedSeriesFormula;
			}
		}

		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x06001602 RID: 5634 RVA: 0x00058D08 File Offset: 0x00056F08
		internal IReportScope ReportScope
		{
			get
			{
				return this.SourceSeries.ReportScope;
			}
		}

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x06001603 RID: 5635 RVA: 0x00058D15 File Offset: 0x00056F15
		internal Chart ChartDef
		{
			get
			{
				return this.m_chart;
			}
		}

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x06001604 RID: 5636 RVA: 0x00058D1D File Offset: 0x00056F1D
		internal ChartDerivedSeries ChartDerivedSeriesDef
		{
			get
			{
				return this.m_chartDerivedSeriesDef;
			}
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x00058D25 File Offset: 0x00056F25
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_series != null)
			{
				this.m_series.SetNewContext();
			}
			if (this.m_chartFormulaParameters != null)
			{
				this.m_chartFormulaParameters.SetNewContext();
			}
		}

		// Token: 0x04000A81 RID: 2689
		private Chart m_chart;

		// Token: 0x04000A82 RID: 2690
		private ChartDerivedSeries m_chartDerivedSeriesDef;

		// Token: 0x04000A83 RID: 2691
		private ChartSeries m_series;

		// Token: 0x04000A84 RID: 2692
		private ChartFormulaParameterCollection m_chartFormulaParameters;

		// Token: 0x04000A85 RID: 2693
		private InternalChartSeries m_sourceSeries;
	}
}
