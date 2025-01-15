using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200036D RID: 877
	internal abstract class ShimMemberCollection : TablixMemberCollection
	{
		// Token: 0x0600216C RID: 8556 RVA: 0x000813A2 File Offset: 0x0007F5A2
		internal ShimMemberCollection(IDefinitionPath parentDefinitionPath, Tablix owner, bool isColumnGroup)
			: base(parentDefinitionPath, owner)
		{
			this.m_isColumnGroup = isColumnGroup;
		}

		// Token: 0x040010C5 RID: 4293
		protected bool m_isColumnGroup;
	}
}
