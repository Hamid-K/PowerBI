using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200024C RID: 588
	public sealed class ChartNoMoveDirections
	{
		// Token: 0x060016B5 RID: 5813 RVA: 0x0005B78B File Offset: 0x0005998B
		internal ChartNoMoveDirections(InternalChartSeries chartSeries, ChartNoMoveDirections chartNoMoveDirectionsDef, Chart chart)
		{
			this.m_chartSeries = chartSeries;
			this.m_chartNoMoveDirectionsDef = chartNoMoveDirectionsDef;
			this.m_chart = chart;
		}

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x060016B6 RID: 5814 RVA: 0x0005B7A8 File Offset: 0x000599A8
		public ReportBoolProperty Up
		{
			get
			{
				if (this.m_up == null && !this.m_chart.IsOldSnapshot && this.m_chartNoMoveDirectionsDef.Up != null)
				{
					this.m_up = new ReportBoolProperty(this.m_chartNoMoveDirectionsDef.Up);
				}
				return this.m_up;
			}
		}

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x060016B7 RID: 5815 RVA: 0x0005B7E8 File Offset: 0x000599E8
		public ReportBoolProperty Down
		{
			get
			{
				if (this.m_down == null && !this.m_chart.IsOldSnapshot && this.m_chartNoMoveDirectionsDef.Down != null)
				{
					this.m_down = new ReportBoolProperty(this.m_chartNoMoveDirectionsDef.Down);
				}
				return this.m_down;
			}
		}

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x060016B8 RID: 5816 RVA: 0x0005B828 File Offset: 0x00059A28
		public ReportBoolProperty Left
		{
			get
			{
				if (this.m_left == null && !this.m_chart.IsOldSnapshot && this.m_chartNoMoveDirectionsDef.Left != null)
				{
					this.m_left = new ReportBoolProperty(this.m_chartNoMoveDirectionsDef.Left);
				}
				return this.m_left;
			}
		}

		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x060016B9 RID: 5817 RVA: 0x0005B868 File Offset: 0x00059A68
		public ReportBoolProperty Right
		{
			get
			{
				if (this.m_right == null && !this.m_chart.IsOldSnapshot && this.m_chartNoMoveDirectionsDef.Right != null)
				{
					this.m_right = new ReportBoolProperty(this.m_chartNoMoveDirectionsDef.Right);
				}
				return this.m_right;
			}
		}

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x060016BA RID: 5818 RVA: 0x0005B8A8 File Offset: 0x00059AA8
		public ReportBoolProperty UpLeft
		{
			get
			{
				if (this.m_upLeft == null && !this.m_chart.IsOldSnapshot && this.m_chartNoMoveDirectionsDef.UpLeft != null)
				{
					this.m_upLeft = new ReportBoolProperty(this.m_chartNoMoveDirectionsDef.UpLeft);
				}
				return this.m_upLeft;
			}
		}

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x060016BB RID: 5819 RVA: 0x0005B8E8 File Offset: 0x00059AE8
		public ReportBoolProperty UpRight
		{
			get
			{
				if (this.m_upRight == null && !this.m_chart.IsOldSnapshot && this.m_chartNoMoveDirectionsDef.UpRight != null)
				{
					this.m_upRight = new ReportBoolProperty(this.m_chartNoMoveDirectionsDef.UpRight);
				}
				return this.m_upRight;
			}
		}

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x060016BC RID: 5820 RVA: 0x0005B928 File Offset: 0x00059B28
		public ReportBoolProperty DownLeft
		{
			get
			{
				if (this.m_downLeft == null && !this.m_chart.IsOldSnapshot && this.m_chartNoMoveDirectionsDef.DownLeft != null)
				{
					this.m_downLeft = new ReportBoolProperty(this.m_chartNoMoveDirectionsDef.DownLeft);
				}
				return this.m_downLeft;
			}
		}

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x060016BD RID: 5821 RVA: 0x0005B968 File Offset: 0x00059B68
		public ReportBoolProperty DownRight
		{
			get
			{
				if (this.m_downRight == null && !this.m_chart.IsOldSnapshot && this.m_chartNoMoveDirectionsDef.DownRight != null)
				{
					this.m_downRight = new ReportBoolProperty(this.m_chartNoMoveDirectionsDef.DownRight);
				}
				return this.m_downRight;
			}
		}

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x060016BE RID: 5822 RVA: 0x0005B9A8 File Offset: 0x00059BA8
		internal IReportScope ReportScope
		{
			get
			{
				if (this.m_chartSeries != null)
				{
					return this.m_chartSeries.ReportScope;
				}
				return this.m_chart;
			}
		}

		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x060016BF RID: 5823 RVA: 0x0005B9C4 File Offset: 0x00059BC4
		internal Chart ChartDef
		{
			get
			{
				return this.m_chart;
			}
		}

		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x060016C0 RID: 5824 RVA: 0x0005B9CC File Offset: 0x00059BCC
		internal ChartNoMoveDirections ChartNoMoveDirectionsDef
		{
			get
			{
				return this.m_chartNoMoveDirectionsDef;
			}
		}

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x060016C1 RID: 5825 RVA: 0x0005B9D4 File Offset: 0x00059BD4
		public ChartNoMoveDirectionsInstance Instance
		{
			get
			{
				if (this.m_chart.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new ChartNoMoveDirectionsInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x0005BA04 File Offset: 0x00059C04
		internal void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x04000B08 RID: 2824
		private Chart m_chart;

		// Token: 0x04000B09 RID: 2825
		private ChartNoMoveDirections m_chartNoMoveDirectionsDef;

		// Token: 0x04000B0A RID: 2826
		private ChartNoMoveDirectionsInstance m_instance;

		// Token: 0x04000B0B RID: 2827
		private ReportBoolProperty m_up;

		// Token: 0x04000B0C RID: 2828
		private ReportBoolProperty m_down;

		// Token: 0x04000B0D RID: 2829
		private ReportBoolProperty m_left;

		// Token: 0x04000B0E RID: 2830
		private ReportBoolProperty m_right;

		// Token: 0x04000B0F RID: 2831
		private ReportBoolProperty m_upLeft;

		// Token: 0x04000B10 RID: 2832
		private ReportBoolProperty m_upRight;

		// Token: 0x04000B11 RID: 2833
		private ReportBoolProperty m_downLeft;

		// Token: 0x04000B12 RID: 2834
		private ReportBoolProperty m_downRight;

		// Token: 0x04000B13 RID: 2835
		private InternalChartSeries m_chartSeries;
	}
}
