using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001CD RID: 461
	internal sealed class QueryVisualAxis : IEquatable<QueryVisualAxis>
	{
		// Token: 0x06001683 RID: 5763 RVA: 0x0003E208 File Offset: 0x0003C408
		public QueryVisualAxis(QueryVisualAxisName name, IReadOnlyList<QueryVisualAxisGroup> groups, IReadOnlyList<QuerySortClause> orderBy)
		{
			this.Name = name;
			this.Groups = groups;
			this.OrderBy = orderBy;
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001684 RID: 5764 RVA: 0x0003E225 File Offset: 0x0003C425
		public QueryVisualAxisName Name { get; }

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001685 RID: 5765 RVA: 0x0003E22D File Offset: 0x0003C42D
		public IReadOnlyList<QueryVisualAxisGroup> Groups { get; }

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001686 RID: 5766 RVA: 0x0003E235 File Offset: 0x0003C435
		public IReadOnlyList<QuerySortClause> OrderBy { get; }

		// Token: 0x06001687 RID: 5767 RVA: 0x0003E23D File Offset: 0x0003C43D
		public override bool Equals(object other)
		{
			return this.Equals(other as QueryVisualAxis);
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x0003E24C File Offset: 0x0003C44C
		public bool Equals(QueryVisualAxis other)
		{
			bool? flag = CompareUtil.AreEqual<QueryVisualAxis, QueryVisualAxis>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return this.Name == other.Name && this.Groups.SequenceEqualReadOnly(other.Groups) && this.OrderBy.SequenceEqualReadOnly(other.OrderBy);
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x0003E2A8 File Offset: 0x0003C4A8
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.Name.GetHashCode(), Hashing.CombineHashReadonly<QueryVisualAxisGroup>(this.Groups, null), Hashing.CombineHashReadonly<QuerySortClause>(this.OrderBy, null));
		}
	}
}
