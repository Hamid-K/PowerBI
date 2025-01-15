using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000249 RID: 585
	public sealed class ChartLegendColumnHeader : IROMStyleDefinitionContainer
	{
		// Token: 0x06001695 RID: 5781 RVA: 0x0005AEDC File Offset: 0x000590DC
		internal ChartLegendColumnHeader(ChartLegendColumnHeader chartLegendColumnHeaderDef, Chart chart)
		{
			this.m_chartLegendColumnHeaderDef = chartLegendColumnHeaderDef;
			this.m_chart = chart;
		}

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x06001696 RID: 5782 RVA: 0x0005AEF4 File Offset: 0x000590F4
		public Style Style
		{
			get
			{
				if (this.m_style == null && !this.m_chart.IsOldSnapshot && this.m_chartLegendColumnHeaderDef.StyleClass != null)
				{
					this.m_style = new Style(this.m_chart, this.m_chart, this.m_chartLegendColumnHeaderDef, this.m_chart.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x06001697 RID: 5783 RVA: 0x0005AF51 File Offset: 0x00059151
		public ReportStringProperty Value
		{
			get
			{
				if (this.m_value == null && !this.m_chart.IsOldSnapshot && this.m_chartLegendColumnHeaderDef.Value != null)
				{
					this.m_value = new ReportStringProperty(this.m_chartLegendColumnHeaderDef.Value);
				}
				return this.m_value;
			}
		}

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x0005AF91 File Offset: 0x00059191
		internal Chart ChartDef
		{
			get
			{
				return this.m_chart;
			}
		}

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x06001699 RID: 5785 RVA: 0x0005AF99 File Offset: 0x00059199
		internal ChartLegendColumnHeader ChartLegendColumnHeaderDef
		{
			get
			{
				return this.m_chartLegendColumnHeaderDef;
			}
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x0600169A RID: 5786 RVA: 0x0005AFA1 File Offset: 0x000591A1
		public ChartLegendColumnHeaderInstance Instance
		{
			get
			{
				if (this.m_chart.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new ChartLegendColumnHeaderInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x0005AFD1 File Offset: 0x000591D1
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

		// Token: 0x04000AF0 RID: 2800
		private Chart m_chart;

		// Token: 0x04000AF1 RID: 2801
		private ChartLegendColumnHeader m_chartLegendColumnHeaderDef;

		// Token: 0x04000AF2 RID: 2802
		private ChartLegendColumnHeaderInstance m_instance;

		// Token: 0x04000AF3 RID: 2803
		private Style m_style;

		// Token: 0x04000AF4 RID: 2804
		private ReportStringProperty m_value;
	}
}
