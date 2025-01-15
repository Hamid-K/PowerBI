using System;
using System.Drawing;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000221 RID: 545
	public sealed class ChartCustomPaletteColor : ChartObjectCollectionItem<ChartCustomPaletteColorInstance>
	{
		// Token: 0x0600149E RID: 5278 RVA: 0x000543F4 File Offset: 0x000525F4
		internal ChartCustomPaletteColor(ChartCustomPaletteColor chartCustomPaletteColorDef, Chart chart)
		{
			this.m_chart = chart;
			this.m_chartCustomPaletteColorDef = chartCustomPaletteColorDef;
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x0600149F RID: 5279 RVA: 0x0005440C File Offset: 0x0005260C
		public ReportColorProperty Color
		{
			get
			{
				if (this.m_color == null && !this.m_chart.IsOldSnapshot && this.m_chartCustomPaletteColorDef.Color != null)
				{
					ExpressionInfo color = this.m_chartCustomPaletteColorDef.Color;
					if (color != null)
					{
						this.m_color = new ReportColorProperty(color.IsExpression, color.OriginalText, color.IsExpression ? null : new ReportColor(color.StringValue.Trim(), true), color.IsExpression ? new ReportColor("", global::System.Drawing.Color.Empty, true) : null);
					}
				}
				return this.m_color;
			}
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x0005449E File Offset: 0x0005269E
		internal Chart ChartDef
		{
			get
			{
				return this.m_chart;
			}
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x060014A1 RID: 5281 RVA: 0x000544A6 File Offset: 0x000526A6
		internal ChartCustomPaletteColor ChartCustomPaletteColorDef
		{
			get
			{
				return this.m_chartCustomPaletteColorDef;
			}
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x000544AE File Offset: 0x000526AE
		public ChartCustomPaletteColorInstance Instance
		{
			get
			{
				if (this.m_chart.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new ChartCustomPaletteColorInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x040009BA RID: 2490
		private Chart m_chart;

		// Token: 0x040009BB RID: 2491
		private ReportColorProperty m_color;

		// Token: 0x040009BC RID: 2492
		private ChartCustomPaletteColor m_chartCustomPaletteColorDef;
	}
}
