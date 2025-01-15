using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200025D RID: 605
	public sealed class ChartAlignTypeInstance : BaseInstance
	{
		// Token: 0x0600178E RID: 6030 RVA: 0x0005FBCE File Offset: 0x0005DDCE
		internal ChartAlignTypeInstance(ChartAlignType chartAlignTypeDef)
			: base(chartAlignTypeDef.ChartDef)
		{
			this.m_chartAlignTypeDef = chartAlignTypeDef;
		}

		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x0600178F RID: 6031 RVA: 0x0005FBE4 File Offset: 0x0005DDE4
		public bool AxesView
		{
			get
			{
				if (this.m_axesView == null && !this.m_chartAlignTypeDef.ChartDef.IsOldSnapshot)
				{
					this.m_axesView = new bool?(this.m_chartAlignTypeDef.ChartAlignTypeDef.EvaluateAxesView(this.ReportScopeInstance, this.m_chartAlignTypeDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_axesView.Value;
			}
		}

		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x06001790 RID: 6032 RVA: 0x0005FC54 File Offset: 0x0005DE54
		public bool Cursor
		{
			get
			{
				if (this.m_cursor == null && !this.m_chartAlignTypeDef.ChartDef.IsOldSnapshot)
				{
					this.m_cursor = new bool?(this.m_chartAlignTypeDef.ChartAlignTypeDef.EvaluateCursor(this.ReportScopeInstance, this.m_chartAlignTypeDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_cursor.Value;
			}
		}

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x06001791 RID: 6033 RVA: 0x0005FCC4 File Offset: 0x0005DEC4
		public bool Position
		{
			get
			{
				if (this.m_position == null && !this.m_chartAlignTypeDef.ChartDef.IsOldSnapshot)
				{
					this.m_position = new bool?(this.m_chartAlignTypeDef.ChartAlignTypeDef.EvaluatePosition(this.ReportScopeInstance, this.m_chartAlignTypeDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_position.Value;
			}
		}

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x06001792 RID: 6034 RVA: 0x0005FD34 File Offset: 0x0005DF34
		public bool InnerPlotPosition
		{
			get
			{
				if (this.m_innerPlotPosition == null && !this.m_chartAlignTypeDef.ChartDef.IsOldSnapshot)
				{
					this.m_innerPlotPosition = new bool?(this.m_chartAlignTypeDef.ChartAlignTypeDef.EvaluateInnerPlotPosition(this.ReportScopeInstance, this.m_chartAlignTypeDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_innerPlotPosition.Value;
			}
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x0005FDA1 File Offset: 0x0005DFA1
		protected override void ResetInstanceCache()
		{
			this.m_axesView = null;
			this.m_cursor = null;
			this.m_position = null;
			this.m_innerPlotPosition = null;
		}

		// Token: 0x04000BAF RID: 2991
		private ChartAlignType m_chartAlignTypeDef;

		// Token: 0x04000BB0 RID: 2992
		private bool? m_axesView;

		// Token: 0x04000BB1 RID: 2993
		private bool? m_cursor;

		// Token: 0x04000BB2 RID: 2994
		private bool? m_position;

		// Token: 0x04000BB3 RID: 2995
		private bool? m_innerPlotPosition;
	}
}
