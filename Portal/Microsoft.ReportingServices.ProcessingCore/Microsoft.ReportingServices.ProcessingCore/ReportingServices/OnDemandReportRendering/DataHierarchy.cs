using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000289 RID: 649
	public sealed class DataHierarchy : MemberHierarchy<Microsoft.ReportingServices.OnDemandReportRendering.DataMember>
	{
		// Token: 0x06001928 RID: 6440 RVA: 0x00066C71 File Offset: 0x00064E71
		internal DataHierarchy(Microsoft.ReportingServices.OnDemandReportRendering.CustomReportItem owner, bool isColumn)
			: base(owner, isColumn)
		{
		}

		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x06001929 RID: 6441 RVA: 0x00066C7B File Offset: 0x00064E7B
		private Microsoft.ReportingServices.OnDemandReportRendering.CustomReportItem OwnerCri
		{
			get
			{
				return this.m_owner as Microsoft.ReportingServices.OnDemandReportRendering.CustomReportItem;
			}
		}

		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x0600192A RID: 6442 RVA: 0x00066C88 File Offset: 0x00064E88
		public DataMemberCollection MemberCollection
		{
			get
			{
				if (this.m_members == null)
				{
					if (this.OwnerCri.IsOldSnapshot)
					{
						this.OwnerCri.ResetMemberCellDefinitionIndex(0);
						DataGroupingCollection dataGroupingCollection = (this.m_isColumn ? this.OwnerCri.RenderCri.CustomData.DataColumnGroupings : this.OwnerCri.RenderCri.CustomData.DataRowGroupings);
						this.m_members = new ShimDataMemberCollection(this, this.OwnerCri, this.m_isColumn, null, dataGroupingCollection);
					}
					else
					{
						DataMemberList dataMemberList = (this.m_isColumn ? this.OwnerCri.CriDef.DataColumnMembers : this.OwnerCri.CriDef.DataRowMembers);
						this.m_members = new InternalDataMemberCollection(this, this.OwnerCri, null, dataMemberList);
					}
				}
				return (DataMemberCollection)this.m_members;
			}
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x00066D54 File Offset: 0x00064F54
		internal override void ResetContext()
		{
			if (this.m_members != null)
			{
				((ShimDataMemberCollection)this.m_members).UpdateContext();
			}
		}
	}
}
