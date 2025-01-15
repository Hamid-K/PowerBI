using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000338 RID: 824
	public sealed class Line : Microsoft.ReportingServices.OnDemandReportRendering.ReportItem
	{
		// Token: 0x06001ECE RID: 7886 RVA: 0x00076E65 File Offset: 0x00075065
		internal Line(IReportScope reportScope, IDefinitionPath parentDefinitionPath, int indexIntoParentCollectionDef, Microsoft.ReportingServices.ReportIntermediateFormat.Line reportItemDef, RenderingContext renderingContext)
			: base(reportScope, parentDefinitionPath, indexIntoParentCollectionDef, reportItemDef, renderingContext)
		{
		}

		// Token: 0x06001ECF RID: 7887 RVA: 0x00076E74 File Offset: 0x00075074
		internal Line(IDefinitionPath parentDefinitionPath, int indexIntoParentCollectionDef, bool inSubtotal, Microsoft.ReportingServices.ReportRendering.Line renderLine, RenderingContext renderingContext)
			: base(parentDefinitionPath, indexIntoParentCollectionDef, inSubtotal, renderLine, renderingContext)
		{
		}

		// Token: 0x1700114A RID: 4426
		// (get) Token: 0x06001ED0 RID: 7888 RVA: 0x00076E83 File Offset: 0x00075083
		public bool Slant
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return ((Microsoft.ReportingServices.ReportRendering.Line)this.m_renderReportItem).Slant;
				}
				return ((Microsoft.ReportingServices.ReportIntermediateFormat.Line)this.m_reportItemDef).LineSlant;
			}
		}

		// Token: 0x06001ED1 RID: 7889 RVA: 0x00076EAE File Offset: 0x000750AE
		internal override ReportItemInstance GetOrCreateInstance()
		{
			if (this.m_instance == null)
			{
				this.m_instance = new LineInstance(this);
			}
			return this.m_instance;
		}
	}
}
