using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200010B RID: 267
	public sealed class GaugeMemberCollection : DataRegionMemberCollection<GaugeMember>
	{
		// Token: 0x06000BDD RID: 3037 RVA: 0x00034509 File Offset: 0x00032709
		internal GaugeMemberCollection(IDefinitionPath parentDefinitionPath, GaugePanel owner)
			: base(parentDefinitionPath, owner)
		{
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x00034513 File Offset: 0x00032713
		internal GaugeMemberCollection(IDefinitionPath parentDefinitionPath, GaugePanel owner, GaugeMember parent, GaugeMemberList memberDefs)
			: base(parentDefinitionPath, owner)
		{
			this.m_parent = parent;
			this.m_memberDefs = memberDefs;
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x0003452C File Offset: 0x0003272C
		public override string DefinitionPath
		{
			get
			{
				if (this.m_parentDefinitionPath is GaugeMember)
				{
					return this.m_parentDefinitionPath.DefinitionPath + "xM";
				}
				return this.m_parentDefinitionPath.DefinitionPath;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x0003455C File Offset: 0x0003275C
		internal GaugePanel OwnerGaugePanel
		{
			get
			{
				return this.m_owner as GaugePanel;
			}
		}

		// Token: 0x170006CC RID: 1740
		public override GaugeMember this[int index]
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
				GaugeMember gaugeMember = (GaugeMember)this.m_children[index];
				if (gaugeMember == null)
				{
					IReportScope reportScope = ((this.m_parent != null) ? this.m_parent.ReportScope : this.m_owner.ReportScope);
					gaugeMember = (this.m_children[index] = new GaugeMember(reportScope, this, this.OwnerGaugePanel, this.m_parent, this.m_memberDefs[index]));
				}
				return gaugeMember;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x0003462C File Offset: 0x0003282C
		public override int Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0400051E RID: 1310
		private GaugeMember m_parent;

		// Token: 0x0400051F RID: 1311
		private GaugeMemberList m_memberDefs;
	}
}
