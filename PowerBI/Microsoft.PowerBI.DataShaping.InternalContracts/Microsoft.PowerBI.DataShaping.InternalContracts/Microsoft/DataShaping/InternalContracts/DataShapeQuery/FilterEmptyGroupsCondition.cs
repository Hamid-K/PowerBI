using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x02000099 RID: 153
	internal sealed class FilterEmptyGroupsCondition : FilterCondition
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000398 RID: 920 RVA: 0x000070FE File Offset: 0x000052FE
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.FilterEmptyGroupsCondition;
			}
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00007102 File Offset: 0x00005302
		public override TResult Accept<TResult>(FilterVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000710C File Offset: 0x0000530C
		public override bool Equals(FilterCondition other, IEqualityComparer<Expression> expressionComparer)
		{
			bool flag;
			FilterEmptyGroupsCondition filterEmptyGroupsCondition;
			if (FilterCondition.CheckReferenceAndTypeEquality<FilterEmptyGroupsCondition>(this, other, out flag, out filterEmptyGroupsCondition))
			{
				return flag;
			}
			return base.Id == filterEmptyGroupsCondition.Id;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00007139 File Offset: 0x00005339
		protected override int GetHashCodeImpl(IEqualityComparer<Expression> expressionComparer)
		{
			return Hashing.GetHashCode<Identifier>(base.Id, null);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00007147 File Offset: 0x00005347
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("EmptyGroupsFilter");
			builder.WriteProperty<Identifier>("Id", base.Id, false);
			builder.EndObject();
		}
	}
}
