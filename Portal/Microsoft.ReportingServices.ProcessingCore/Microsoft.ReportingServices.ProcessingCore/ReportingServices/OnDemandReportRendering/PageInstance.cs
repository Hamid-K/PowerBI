using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000302 RID: 770
	public sealed class PageInstance : ReportElementInstance, IReportScopeInstance
	{
		// Token: 0x06001C38 RID: 7224 RVA: 0x000707A1 File Offset: 0x0006E9A1
		internal PageInstance(Page pageDef)
			: base(pageDef)
		{
		}

		// Token: 0x17000FCE RID: 4046
		// (get) Token: 0x06001C39 RID: 7225 RVA: 0x000707AC File Offset: 0x0006E9AC
		public string UniqueName
		{
			get
			{
				if (this.m_reportElementDef.IsOldSnapshot)
				{
					return this.PageDefinition.RenderReport.UniqueName + "xP";
				}
				ReportSection reportSection = (ReportSection)this.PageDefinition.ReportItemDef;
				return InstancePathItem.GenerateUniqueNameString(reportSection.ID, reportSection.InstancePath) + "xP";
			}
		}

		// Token: 0x17000FCF RID: 4047
		// (get) Token: 0x06001C3A RID: 7226 RVA: 0x00070810 File Offset: 0x0006EA10
		public override StyleInstance Style
		{
			get
			{
				Page pageDefinition = this.PageDefinition;
				if (pageDefinition.ShouldUseFirstSection)
				{
					return pageDefinition.FirstSectionPage.Instance.Style;
				}
				return base.Style;
			}
		}

		// Token: 0x17000FD0 RID: 4048
		// (get) Token: 0x06001C3B RID: 7227 RVA: 0x00070843 File Offset: 0x0006EA43
		internal Page PageDefinition
		{
			get
			{
				return (Page)this.m_reportElementDef;
			}
		}

		// Token: 0x17000FD1 RID: 4049
		// (get) Token: 0x06001C3C RID: 7228 RVA: 0x00070850 File Offset: 0x0006EA50
		IReportScope IReportScopeInstance.ReportScope
		{
			get
			{
				return this.PageDefinition;
			}
		}

		// Token: 0x17000FD2 RID: 4050
		// (get) Token: 0x06001C3D RID: 7229 RVA: 0x00070858 File Offset: 0x0006EA58
		// (set) Token: 0x06001C3E RID: 7230 RVA: 0x00070860 File Offset: 0x0006EA60
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

		// Token: 0x04000EE1 RID: 3809
		private bool m_isNewContext;
	}
}
