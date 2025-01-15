using System;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x020005A6 RID: 1446
	internal class MemberMaps
	{
		// Token: 0x06004631 RID: 17969 RVA: 0x000F7C90 File Offset: 0x000F5E90
		internal MemberMaps(ViewTarget viewTarget, MemberProjectionIndex projectedSlotMap, MemberDomainMap queryDomainMap, MemberDomainMap updateDomainMap)
		{
			this.m_projectedSlotMap = projectedSlotMap;
			this.m_queryDomainMap = queryDomainMap;
			this.m_updateDomainMap = updateDomainMap;
			this.m_viewTarget = viewTarget;
		}

		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x06004632 RID: 17970 RVA: 0x000F7CB5 File Offset: 0x000F5EB5
		internal MemberProjectionIndex ProjectedSlotMap
		{
			get
			{
				return this.m_projectedSlotMap;
			}
		}

		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x06004633 RID: 17971 RVA: 0x000F7CBD File Offset: 0x000F5EBD
		internal MemberDomainMap QueryDomainMap
		{
			get
			{
				return this.m_queryDomainMap;
			}
		}

		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x06004634 RID: 17972 RVA: 0x000F7CC5 File Offset: 0x000F5EC5
		internal MemberDomainMap UpdateDomainMap
		{
			get
			{
				return this.m_updateDomainMap;
			}
		}

		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x06004635 RID: 17973 RVA: 0x000F7CCD File Offset: 0x000F5ECD
		internal MemberDomainMap RightDomainMap
		{
			get
			{
				if (this.m_viewTarget != ViewTarget.QueryView)
				{
					return this.m_queryDomainMap;
				}
				return this.m_updateDomainMap;
			}
		}

		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x06004636 RID: 17974 RVA: 0x000F7CE4 File Offset: 0x000F5EE4
		internal MemberDomainMap LeftDomainMap
		{
			get
			{
				if (this.m_viewTarget != ViewTarget.QueryView)
				{
					return this.m_updateDomainMap;
				}
				return this.m_queryDomainMap;
			}
		}

		// Token: 0x04001913 RID: 6419
		private readonly MemberProjectionIndex m_projectedSlotMap;

		// Token: 0x04001914 RID: 6420
		private readonly MemberDomainMap m_queryDomainMap;

		// Token: 0x04001915 RID: 6421
		private readonly MemberDomainMap m_updateDomainMap;

		// Token: 0x04001916 RID: 6422
		private readonly ViewTarget m_viewTarget;
	}
}
