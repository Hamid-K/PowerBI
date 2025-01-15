using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200023D RID: 573
	public sealed class ChartElementPosition
	{
		// Token: 0x06001606 RID: 5638 RVA: 0x00058D53 File Offset: 0x00056F53
		internal ChartElementPosition(ChartElementPosition defObject, Chart chart)
		{
			this.m_defObject = defObject;
			this.m_chart = chart;
		}

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x06001607 RID: 5639 RVA: 0x00058D69 File Offset: 0x00056F69
		public ReportDoubleProperty Top
		{
			get
			{
				if (this.m_top == null && this.m_defObject.Top != null)
				{
					this.m_top = new ReportDoubleProperty(this.m_defObject.Top);
				}
				return this.m_top;
			}
		}

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x06001608 RID: 5640 RVA: 0x00058D9C File Offset: 0x00056F9C
		public ReportDoubleProperty Left
		{
			get
			{
				if (this.m_left == null && this.m_defObject.Left != null)
				{
					this.m_left = new ReportDoubleProperty(this.m_defObject.Left);
				}
				return this.m_left;
			}
		}

		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x06001609 RID: 5641 RVA: 0x00058DCF File Offset: 0x00056FCF
		public ReportDoubleProperty Height
		{
			get
			{
				if (this.m_height == null && this.m_defObject.Height != null)
				{
					this.m_height = new ReportDoubleProperty(this.m_defObject.Height);
				}
				return this.m_height;
			}
		}

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x0600160A RID: 5642 RVA: 0x00058E02 File Offset: 0x00057002
		public ReportDoubleProperty Width
		{
			get
			{
				if (this.m_width == null && this.m_defObject.Width != null)
				{
					this.m_width = new ReportDoubleProperty(this.m_defObject.Width);
				}
				return this.m_width;
			}
		}

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x0600160B RID: 5643 RVA: 0x00058E35 File Offset: 0x00057035
		internal Chart ChartDef
		{
			get
			{
				return this.m_chart;
			}
		}

		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x0600160C RID: 5644 RVA: 0x00058E3D File Offset: 0x0005703D
		internal ChartElementPosition ChartElementPositionDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x00058E45 File Offset: 0x00057045
		public ChartElementPositionInstance Instance
		{
			get
			{
				if (this.m_chart.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new ChartElementPositionInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x00058E75 File Offset: 0x00057075
		internal void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x04000A86 RID: 2694
		private Chart m_chart;

		// Token: 0x04000A87 RID: 2695
		private ChartElementPosition m_defObject;

		// Token: 0x04000A88 RID: 2696
		private ChartElementPositionInstance m_instance;

		// Token: 0x04000A89 RID: 2697
		private ReportDoubleProperty m_top;

		// Token: 0x04000A8A RID: 2698
		private ReportDoubleProperty m_left;

		// Token: 0x04000A8B RID: 2699
		private ReportDoubleProperty m_height;

		// Token: 0x04000A8C RID: 2700
		private ReportDoubleProperty m_width;
	}
}
