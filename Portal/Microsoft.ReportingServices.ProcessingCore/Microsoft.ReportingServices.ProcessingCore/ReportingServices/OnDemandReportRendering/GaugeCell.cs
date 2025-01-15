using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200010E RID: 270
	public sealed class GaugeCell : IReportScope, IDataRegionCell
	{
		// Token: 0x06000BF1 RID: 3057 RVA: 0x00034747 File Offset: 0x00032947
		internal GaugeCell(GaugePanel owner, GaugeCell gaugeCellDef)
		{
			this.m_owner = owner;
			this.m_gaugeCellDef = gaugeCellDef;
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x0003475D File Offset: 0x0003295D
		internal GaugeCell GaugeCellDef
		{
			get
			{
				return this.m_gaugeCellDef;
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x00034765 File Offset: 0x00032965
		internal GaugePanel GaugePanelDef
		{
			get
			{
				return this.m_owner;
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x0003476D File Offset: 0x0003296D
		public GaugeCellInstance Instance
		{
			get
			{
				if (this.m_owner.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new GaugeCellInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x0003479D File Offset: 0x0003299D
		IReportScopeInstance IReportScope.ReportScopeInstance
		{
			get
			{
				return this.Instance;
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x000347A5 File Offset: 0x000329A5
		IRIFReportScope IReportScope.RIFReportScope
		{
			get
			{
				return this.GaugeCellDef;
			}
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x000347AD File Offset: 0x000329AD
		void IDataRegionCell.SetNewContext()
		{
			this.SetNewContext();
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x000347B8 File Offset: 0x000329B8
		internal void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			List<GaugeInputValue> gaugeInputValues = this.GetGaugeInputValues();
			if (gaugeInputValues != null)
			{
				foreach (GaugeInputValue gaugeInputValue in gaugeInputValues)
				{
					gaugeInputValue.SetNewContext();
				}
			}
			if (this.m_gaugeCellDef != null)
			{
				this.m_gaugeCellDef.ClearStreamingScopeInstanceBinding();
			}
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x00034834 File Offset: 0x00032A34
		private List<GaugeInputValue> GetGaugeInputValues()
		{
			if (this.m_gaugeInputValues == null)
			{
				this.m_gaugeInputValues = this.m_owner.GetGaugeInputValues();
			}
			return this.m_gaugeInputValues;
		}

		// Token: 0x04000526 RID: 1318
		private GaugePanel m_owner;

		// Token: 0x04000527 RID: 1319
		private GaugeCell m_gaugeCellDef;

		// Token: 0x04000528 RID: 1320
		private GaugeCellInstance m_instance;

		// Token: 0x04000529 RID: 1321
		private List<GaugeInputValue> m_gaugeInputValues;
	}
}
