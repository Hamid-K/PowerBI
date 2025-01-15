using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000099 RID: 153
	internal sealed class DataShapeMemberHierarchy : MemberHierarchy<DataShapeMember>
	{
		// Token: 0x06000957 RID: 2391 RVA: 0x000270C0 File Offset: 0x000252C0
		internal DataShapeMemberHierarchy(DataShape owner, bool isColumn)
			: base(owner, isColumn)
		{
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x000270CA File Offset: 0x000252CA
		internal override void ResetContext()
		{
			this.ResetContext(true);
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x000270D4 File Offset: 0x000252D4
		internal DataShapeMemberCollection MemberCollection
		{
			get
			{
				if (this.m_members == null)
				{
					DataShape rifDataShapeDefinition = this.OwnerDataShape.RifDataShapeDefinition;
					DataShapeMemberList dataShapeMemberList = (this.m_isColumn ? rifDataShapeDefinition.SecondaryHierarchy : rifDataShapeDefinition.PrimaryHierarchy);
					if (dataShapeMemberList != null)
					{
						this.m_members = new DataShapeMemberCollection(this, this.OwnerDataShape, null, dataShapeMemberList);
					}
				}
				return (DataShapeMemberCollection)this.m_members;
			}
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0002712E File Offset: 0x0002532E
		internal void ResetContext(bool clearCache)
		{
			if (clearCache)
			{
				this.OwnerDataShape.ResetMemberCellDefinitionIndex(0);
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x0002713F File Offset: 0x0002533F
		private DataShape OwnerDataShape
		{
			get
			{
				return (DataShape)this.m_owner;
			}
		}
	}
}
