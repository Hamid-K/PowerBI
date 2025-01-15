using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000305 RID: 773
	public sealed class PageSectionInstance : ReportElementInstance
	{
		// Token: 0x06001C4F RID: 7247 RVA: 0x00070AA8 File Offset: 0x0006ECA8
		internal PageSectionInstance(PageSection pageSectionDef)
			: base(pageSectionDef)
		{
		}

		// Token: 0x17000FDC RID: 4060
		// (get) Token: 0x06001C50 RID: 7248 RVA: 0x00070AB4 File Offset: 0x0006ECB4
		public string UniqueName
		{
			get
			{
				if (this.m_reportElementDef.IsOldSnapshot)
				{
					return this.PageSectionDefinition.PageDefinition.Instance.UniqueName + this.PageSectionDefinition.RenderReportItem.UniqueName;
				}
				string text = (this.PageSectionDefinition.IsHeader ? "xH" : "xF");
				PageSection pageSection = (PageSection)this.PageSectionDefinition.ReportItemDef;
				return InstancePathItem.GenerateUniqueNameString(pageSection.ID, pageSection.InstancePath) + text;
			}
		}

		// Token: 0x17000FDD RID: 4061
		// (get) Token: 0x06001C51 RID: 7249 RVA: 0x00070B3B File Offset: 0x0006ED3B
		internal PageSection PageSectionDefinition
		{
			get
			{
				return (PageSection)this.m_reportElementDef;
			}
		}
	}
}
