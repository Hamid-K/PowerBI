using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200036C RID: 876
	internal sealed class InternalTablixMemberCollection : TablixMemberCollection
	{
		// Token: 0x06002168 RID: 8552 RVA: 0x000812B1 File Offset: 0x0007F4B1
		internal InternalTablixMemberCollection(IDefinitionPath parentDefinitionPath, Microsoft.ReportingServices.OnDemandReportRendering.Tablix owner, TablixMember parent, TablixMemberList memberDefs)
			: base(parentDefinitionPath, owner)
		{
			this.m_parent = parent;
			this.m_memberDefs = memberDefs;
		}

		// Token: 0x170012D2 RID: 4818
		public override TablixMember this[int index]
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
				TablixMember tablixMember = (TablixMember)this.m_children[index];
				if (tablixMember == null)
				{
					IReportScope reportScope = ((this.m_parent != null) ? this.m_parent.ReportScope : this.m_owner.ReportScope);
					tablixMember = (this.m_children[index] = new InternalTablixMember(reportScope, this, base.OwnerTablix, this.m_parent, this.m_memberDefs[index], index));
				}
				return tablixMember;
			}
		}

		// Token: 0x170012D3 RID: 4819
		// (get) Token: 0x0600216A RID: 8554 RVA: 0x0008138D File Offset: 0x0007F58D
		public override int Count
		{
			get
			{
				return this.m_memberDefs.OriginalNodeCount;
			}
		}

		// Token: 0x170012D4 RID: 4820
		// (get) Token: 0x0600216B RID: 8555 RVA: 0x0008139A File Offset: 0x0007F59A
		internal TablixMemberList MemberDefs
		{
			get
			{
				return this.m_memberDefs;
			}
		}

		// Token: 0x040010C3 RID: 4291
		private TablixMember m_parent;

		// Token: 0x040010C4 RID: 4292
		private TablixMemberList m_memberDefs;
	}
}
