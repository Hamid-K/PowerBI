using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200028B RID: 651
	internal sealed class InternalDataMemberCollection : DataMemberCollection
	{
		// Token: 0x0600192F RID: 6447 RVA: 0x00066DB5 File Offset: 0x00064FB5
		internal InternalDataMemberCollection(IDefinitionPath parentDefinitionPath, Microsoft.ReportingServices.OnDemandReportRendering.CustomReportItem owner, DataMember parent, DataMemberList memberDefs)
			: base(parentDefinitionPath, owner)
		{
			this.m_parent = parent;
			this.m_memberDefs = memberDefs;
		}

		// Token: 0x17000E65 RID: 3685
		public override DataMember this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				if (this.m_children == null)
				{
					this.m_children = new DataRegionMember[this.Count];
				}
				DataMember dataMember = (DataMember)this.m_children[index];
				if (dataMember == null)
				{
					IReportScope reportScope = ((this.m_parent != null) ? this.m_parent.ReportScope : this.m_owner.ReportScope);
					dataMember = (this.m_children[index] = new InternalDataMember(reportScope, this, base.OwnerCri, this.m_parent, this.m_memberDefs[index], index));
				}
				return dataMember;
			}
		}

		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x06001931 RID: 6449 RVA: 0x00066E91 File Offset: 0x00065091
		public override int Count
		{
			get
			{
				return this.m_memberDefs.OriginalNodeCount;
			}
		}

		// Token: 0x04000CA3 RID: 3235
		private DataMember m_parent;

		// Token: 0x04000CA4 RID: 3236
		private DataMemberList m_memberDefs;
	}
}
