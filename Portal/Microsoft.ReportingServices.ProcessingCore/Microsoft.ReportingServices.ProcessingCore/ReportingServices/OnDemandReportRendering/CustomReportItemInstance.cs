using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000285 RID: 645
	public sealed class CustomReportItemInstance : ReportItemInstance
	{
		// Token: 0x0600190C RID: 6412 RVA: 0x00066A25 File Offset: 0x00064C25
		internal CustomReportItemInstance(CustomReportItem reportItemDef)
			: base(reportItemDef)
		{
		}

		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x0600190D RID: 6413 RVA: 0x00066A30 File Offset: 0x00064C30
		public bool NoRows
		{
			get
			{
				if (this.m_reportElementDef.IsOldSnapshot)
				{
					return ((CustomReportItem)this.m_reportElementDef).RenderCri.CustomData.NoRows;
				}
				this.m_reportElementDef.RenderingContext.OdpContext.SetupContext(this.m_reportElementDef.ReportItemDef, this.ReportScopeInstance);
				return ((DataRegion)this.m_reportElementDef.ReportItemDef).NoRows;
			}
		}
	}
}
