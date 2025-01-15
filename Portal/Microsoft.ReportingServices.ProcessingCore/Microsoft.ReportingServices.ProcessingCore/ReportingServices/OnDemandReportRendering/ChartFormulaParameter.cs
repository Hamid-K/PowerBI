using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000248 RID: 584
	public sealed class ChartFormulaParameter : ChartObjectCollectionItem<ChartFormulaParameterInstance>
	{
		// Token: 0x0600168D RID: 5773 RVA: 0x0005ADEB File Offset: 0x00058FEB
		internal ChartFormulaParameter(ChartDerivedSeries chartDerivedSeries, ChartFormulaParameter chartFormulaParameterDef, Chart chart)
		{
			this.m_chartDerivedSeries = chartDerivedSeries;
			this.m_chartFormulaParameterDef = chartFormulaParameterDef;
			this.m_chart = chart;
		}

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x0600168E RID: 5774 RVA: 0x0005AE08 File Offset: 0x00059008
		public string Name
		{
			get
			{
				if (this.m_chart.IsOldSnapshot)
				{
					return null;
				}
				return this.m_chartFormulaParameterDef.FormulaParameterName;
			}
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x0600168F RID: 5775 RVA: 0x0005AE24 File Offset: 0x00059024
		public ReportVariantProperty Value
		{
			get
			{
				if (this.m_value == null && !this.m_chart.IsOldSnapshot && this.m_chartFormulaParameterDef.Value != null)
				{
					this.m_value = new ReportVariantProperty(this.m_chartFormulaParameterDef.Value);
				}
				return this.m_value;
			}
		}

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x06001690 RID: 5776 RVA: 0x0005AE64 File Offset: 0x00059064
		public string Source
		{
			get
			{
				if (this.m_chart.IsOldSnapshot)
				{
					return null;
				}
				if (this.m_chartFormulaParameterDef.Source != null)
				{
					return this.m_chartFormulaParameterDef.Source;
				}
				return null;
			}
		}

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x06001691 RID: 5777 RVA: 0x0005AE8F File Offset: 0x0005908F
		internal IReportScope ReportScope
		{
			get
			{
				return this.m_chartDerivedSeries.ReportScope;
			}
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x06001692 RID: 5778 RVA: 0x0005AE9C File Offset: 0x0005909C
		internal Chart ChartDef
		{
			get
			{
				return this.m_chart;
			}
		}

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x06001693 RID: 5779 RVA: 0x0005AEA4 File Offset: 0x000590A4
		internal ChartFormulaParameter ChartFormulaParameterDef
		{
			get
			{
				return this.m_chartFormulaParameterDef;
			}
		}

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x06001694 RID: 5780 RVA: 0x0005AEAC File Offset: 0x000590AC
		public ChartFormulaParameterInstance Instance
		{
			get
			{
				if (this.m_chart.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new ChartFormulaParameterInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x04000AEC RID: 2796
		private Chart m_chart;

		// Token: 0x04000AED RID: 2797
		private ChartFormulaParameter m_chartFormulaParameterDef;

		// Token: 0x04000AEE RID: 2798
		private ReportVariantProperty m_value;

		// Token: 0x04000AEF RID: 2799
		private ChartDerivedSeries m_chartDerivedSeries;
	}
}
