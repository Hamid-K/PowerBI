using System;
using System.Globalization;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000258 RID: 600
	public sealed class ChartDataPointInstance : BaseInstance, IReportScopeInstance
	{
		// Token: 0x06001759 RID: 5977 RVA: 0x0005E86E File Offset: 0x0005CA6E
		internal ChartDataPointInstance(ChartDataPoint chartDataPointDef)
			: base(chartDataPointDef)
		{
			this.m_chartDataPointDef = chartDataPointDef;
		}

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x0600175A RID: 5978 RVA: 0x0005E885 File Offset: 0x0005CA85
		public StyleInstance Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new StyleInstance(this.m_chartDataPointDef, this.m_chartDataPointDef, this.m_chartDataPointDef.ChartDef.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x0600175B RID: 5979 RVA: 0x0005E8BC File Offset: 0x0005CABC
		public object AxisLabel
		{
			get
			{
				if (this.m_axisLabel == null && !this.m_chartDataPointDef.ChartDef.IsOldSnapshot)
				{
					this.m_axisLabel = this.m_chartDataPointDef.DataPointDef.EvaluateAxisLabel(this.ReportScopeInstance, this.m_chartDataPointDef.ChartDef.RenderingContext.OdpContext).Value;
				}
				return this.m_axisLabel;
			}
		}

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x0600175C RID: 5980 RVA: 0x0005E920 File Offset: 0x0005CB20
		public string ToolTip
		{
			get
			{
				if (this.m_toolTip == null)
				{
					this.m_toolTip = this.m_chartDataPointDef.DataPointDef.EvaluateToolTip(this.ReportScopeInstance, this.m_chartDataPointDef.ChartDef.RenderingContext.OdpContext);
				}
				return this.m_toolTip;
			}
		}

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x0600175D RID: 5981 RVA: 0x0005E96C File Offset: 0x0005CB6C
		string IReportScopeInstance.UniqueName
		{
			get
			{
				if (this.m_chartDataPointDef.ChartDef.IsOldSnapshot)
				{
					return this.m_chartDataPointDef.ChartDef.ID + "i" + this.m_chartDataPointDef.RenderItem.InstanceInfo.DataPointIndex.ToString(CultureInfo.InvariantCulture);
				}
				return this.m_chartDataPointDef.DataPointDef.UniqueName;
			}
		}

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x0600175E RID: 5982 RVA: 0x0005E9D8 File Offset: 0x0005CBD8
		// (set) Token: 0x0600175F RID: 5983 RVA: 0x0005E9E0 File Offset: 0x0005CBE0
		bool IReportScopeInstance.IsNewContext
		{
			get
			{
				return this.m_isNewContext;
			}
			set
			{
				this.m_isNewContext = value;
			}
		}

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x06001760 RID: 5984 RVA: 0x0005E9E9 File Offset: 0x0005CBE9
		IReportScope IReportScopeInstance.ReportScope
		{
			get
			{
				return this.m_reportScope;
			}
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x0005E9F1 File Offset: 0x0005CBF1
		internal override void SetNewContext()
		{
			if (this.m_isNewContext)
			{
				return;
			}
			this.m_isNewContext = true;
			base.SetNewContext();
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x0005EA09 File Offset: 0x0005CC09
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_axisLabel = null;
			this.m_toolTip = null;
		}

		// Token: 0x04000B83 RID: 2947
		private ChartDataPoint m_chartDataPointDef;

		// Token: 0x04000B84 RID: 2948
		private StyleInstance m_style;

		// Token: 0x04000B85 RID: 2949
		private object m_axisLabel;

		// Token: 0x04000B86 RID: 2950
		private bool m_isNewContext = true;

		// Token: 0x04000B87 RID: 2951
		private string m_toolTip;
	}
}
