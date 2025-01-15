using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200010C RID: 268
	public sealed class GaugeRow : IDataRegionRow
	{
		// Token: 0x06000BE3 RID: 3043 RVA: 0x0003462F File Offset: 0x0003282F
		internal GaugeRow(GaugePanel gaugePanel)
		{
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0003463E File Offset: 0x0003283E
		internal GaugeRow(GaugePanel gaugePanel, GaugeRow rowDef)
		{
			this.m_gaugePanel = gaugePanel;
			this.m_rowDef = rowDef;
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x00034654 File Offset: 0x00032854
		public GaugeCell GaugeCell
		{
			get
			{
				if (this.m_cell == null && this.m_rowDef.GaugeCell != null)
				{
					this.m_cell = new GaugeCell(this.m_gaugePanel, this.m_rowDef.GaugeCell);
				}
				return this.m_cell;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x0003468D File Offset: 0x0003288D
		int IDataRegionRow.Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x00034690 File Offset: 0x00032890
		internal GaugeRow GaugeRowDef
		{
			get
			{
				return this.m_rowDef;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x00034698 File Offset: 0x00032898
		internal GaugePanel GaugePanelDef
		{
			get
			{
				return this.m_gaugePanel;
			}
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x000346A0 File Offset: 0x000328A0
		IDataRegionCell IDataRegionRow.GetIfExists(int columnIndex)
		{
			if (columnIndex == 0)
			{
				return this.GaugeCell;
			}
			return null;
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x000346AD File Offset: 0x000328AD
		internal void SetNewContext()
		{
			if (this.m_cell != null)
			{
				this.m_cell.SetNewContext();
			}
		}

		// Token: 0x04000520 RID: 1312
		private GaugePanel m_gaugePanel;

		// Token: 0x04000521 RID: 1313
		private GaugeCell m_cell;

		// Token: 0x04000522 RID: 1314
		private GaugeRow m_rowDef;
	}
}
