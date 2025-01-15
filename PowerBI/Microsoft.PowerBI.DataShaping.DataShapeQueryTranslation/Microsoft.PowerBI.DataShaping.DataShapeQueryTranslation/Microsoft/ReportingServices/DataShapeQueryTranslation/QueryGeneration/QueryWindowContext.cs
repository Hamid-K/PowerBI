using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration
{
	// Token: 0x02000094 RID: 148
	internal sealed class QueryWindowContext : IQueryConstraint, IEquatable<IQueryConstraint>
	{
		// Token: 0x060006F6 RID: 1782 RVA: 0x0001A508 File Offset: 0x00018708
		internal QueryWindowContext(DataShape dataShape)
		{
			this.m_dataShape = dataShape;
			this.m_groups = this.m_dataShape.PrimaryHierarchy.GetAllDynamicMembers().ToList<DataMember>();
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x0001A532 File Offset: 0x00018732
		public int Count
		{
			get
			{
				return this.m_dataShape.RequestedPrimaryLeafCount.Value;
			}
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0001A544 File Offset: 0x00018744
		public bool Contains(GroupAndSortingContext group)
		{
			return this.m_groups.Contains(group.Scope);
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x0001A557 File Offset: 0x00018757
		public int PaddedCount
		{
			get
			{
				return this.Count + 1;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x0001A561 File Offset: 0x00018761
		public int RawCount
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x0001A569 File Offset: 0x00018769
		public bool IsWindow
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0001A56C File Offset: 0x0001876C
		public bool Equals(IQueryConstraint other)
		{
			QueryWindowContext queryWindowContext = other as QueryWindowContext;
			return queryWindowContext != null && this.m_dataShape == queryWindowContext.m_dataShape;
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001A593 File Offset: 0x00018793
		public override bool Equals(object obj)
		{
			return this.Equals(obj as IQueryConstraint);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0001A5A1 File Offset: 0x000187A1
		public override int GetHashCode()
		{
			return this.m_dataShape.GetHashCode();
		}

		// Token: 0x04000362 RID: 866
		private readonly DataShape m_dataShape;

		// Token: 0x04000363 RID: 867
		private readonly List<DataMember> m_groups;
	}
}
