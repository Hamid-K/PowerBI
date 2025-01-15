using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000232 RID: 562
	public sealed class ChartAlignType
	{
		// Token: 0x0600159E RID: 5534 RVA: 0x00057368 File Offset: 0x00055568
		internal ChartAlignType(ChartAlignType chartAlignTypeDef, Chart chart)
		{
			this.m_chartAlignTypeDef = chartAlignTypeDef;
			this.m_chart = chart;
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x0600159F RID: 5535 RVA: 0x0005737E File Offset: 0x0005557E
		public ReportBoolProperty AxesView
		{
			get
			{
				if (this.m_axesView == null && !this.m_chart.IsOldSnapshot && this.m_chartAlignTypeDef.AxesView != null)
				{
					this.m_axesView = new ReportBoolProperty(this.m_chartAlignTypeDef.AxesView);
				}
				return this.m_axesView;
			}
		}

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x060015A0 RID: 5536 RVA: 0x000573BE File Offset: 0x000555BE
		public ReportBoolProperty Cursor
		{
			get
			{
				if (this.m_cursor == null && !this.m_chart.IsOldSnapshot && this.m_chartAlignTypeDef.Cursor != null)
				{
					this.m_cursor = new ReportBoolProperty(this.m_chartAlignTypeDef.Cursor);
				}
				return this.m_cursor;
			}
		}

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x060015A1 RID: 5537 RVA: 0x000573FE File Offset: 0x000555FE
		public ReportBoolProperty Position
		{
			get
			{
				if (this.m_position == null && !this.m_chart.IsOldSnapshot && this.m_chartAlignTypeDef.Position != null)
				{
					this.m_position = new ReportBoolProperty(this.m_chartAlignTypeDef.Position);
				}
				return this.m_position;
			}
		}

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x060015A2 RID: 5538 RVA: 0x0005743E File Offset: 0x0005563E
		public ReportBoolProperty InnerPlotPosition
		{
			get
			{
				if (this.m_innerPlotPosition == null && !this.m_chart.IsOldSnapshot && this.m_chartAlignTypeDef.InnerPlotPosition != null)
				{
					this.m_innerPlotPosition = new ReportBoolProperty(this.m_chartAlignTypeDef.InnerPlotPosition);
				}
				return this.m_innerPlotPosition;
			}
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x060015A3 RID: 5539 RVA: 0x0005747E File Offset: 0x0005567E
		internal Chart ChartDef
		{
			get
			{
				return this.m_chart;
			}
		}

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x060015A4 RID: 5540 RVA: 0x00057486 File Offset: 0x00055686
		internal ChartAlignType ChartAlignTypeDef
		{
			get
			{
				return this.m_chartAlignTypeDef;
			}
		}

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x060015A5 RID: 5541 RVA: 0x0005748E File Offset: 0x0005568E
		public ChartAlignTypeInstance Instance
		{
			get
			{
				if (this.m_chart.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new ChartAlignTypeInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x000574BE File Offset: 0x000556BE
		internal void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x04000A3E RID: 2622
		private Chart m_chart;

		// Token: 0x04000A3F RID: 2623
		private ChartAlignType m_chartAlignTypeDef;

		// Token: 0x04000A40 RID: 2624
		private ChartAlignTypeInstance m_instance;

		// Token: 0x04000A41 RID: 2625
		private ReportBoolProperty m_axesView;

		// Token: 0x04000A42 RID: 2626
		private ReportBoolProperty m_cursor;

		// Token: 0x04000A43 RID: 2627
		private ReportBoolProperty m_position;

		// Token: 0x04000A44 RID: 2628
		private ReportBoolProperty m_innerPlotPosition;
	}
}
