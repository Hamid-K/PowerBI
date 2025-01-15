using System;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200036E RID: 878
	internal sealed class ShimListMemberCollection : ShimMemberCollection
	{
		// Token: 0x0600216D RID: 8557 RVA: 0x000813B3 File Offset: 0x0007F5B3
		internal ShimListMemberCollection(IDefinitionPath parentDefinitionPath, Tablix owner)
			: base(parentDefinitionPath, owner, true)
		{
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x000813BE File Offset: 0x0007F5BE
		internal ShimListMemberCollection(IDefinitionPath parentDefinitionPath, Tablix owner, ListContentCollection renderListContents)
			: base(parentDefinitionPath, owner, false)
		{
			this.m_renderGroups = new ShimRenderGroups(renderListContents);
		}

		// Token: 0x170012D5 RID: 4821
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
					tablixMember = (this.m_children[index] = new ShimListMember(this, base.OwnerTablix, this.m_renderGroups, index, this.m_isColumnGroup));
				}
				return tablixMember;
			}
		}

		// Token: 0x170012D6 RID: 4822
		// (get) Token: 0x06002170 RID: 8560 RVA: 0x00081471 File Offset: 0x0007F671
		public override int Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x00081474 File Offset: 0x0007F674
		internal void UpdateContext(ListContentCollection renderListContents)
		{
			this.m_renderGroups = new ShimRenderGroups(renderListContents);
			if (this.m_children != null)
			{
				((ShimListMember)this.m_children[0]).ResetContext(this.m_renderGroups);
			}
		}

		// Token: 0x040010C6 RID: 4294
		private ShimRenderGroups m_renderGroups;
	}
}
