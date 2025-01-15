using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x02000088 RID: 136
	internal sealed class ExistsFilterCondition : FilterCondition
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00006C9F File Offset: 0x00004E9F
		// (set) Token: 0x06000348 RID: 840 RVA: 0x00006CA7 File Offset: 0x00004EA7
		public List<ExistsFilterItem> Items { get; set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00006CB0 File Offset: 0x00004EB0
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.ExistsFilterCondition;
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00006CB4 File Offset: 0x00004EB4
		public override TResult Accept<TResult>(FilterVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00006CC0 File Offset: 0x00004EC0
		public override bool Equals(FilterCondition other, IEqualityComparer<Expression> expressionComparer)
		{
			bool flag;
			ExistsFilterCondition existsFilterCondition;
			if (FilterCondition.CheckReferenceAndTypeEquality<ExistsFilterCondition>(this, other, out flag, out existsFilterCondition))
			{
				return flag;
			}
			return base.Id == existsFilterCondition.Id && this.Items.SequenceEqual(existsFilterCondition.Items, new ExpressionEquatableComparer<ExistsFilterItem>(expressionComparer));
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00006D08 File Offset: 0x00004F08
		protected override int GetHashCodeImpl(IEqualityComparer<Expression> expressionComparer)
		{
			ExpressionEquatableComparer<ExistsFilterItem> expressionEquatableComparer = ((expressionComparer != null) ? new ExpressionEquatableComparer<ExistsFilterItem>(expressionComparer) : null);
			return Hashing.CombineHash(Hashing.GetHashCode<Identifier>(base.Id, null), Hashing.CombineHash<ExistsFilterItem>(this.Items, expressionEquatableComparer));
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00006D3F File Offset: 0x00004F3F
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("ExistsFilterCondition");
			builder.WriteProperty<Identifier>("Id", base.Id, false);
			builder.WriteProperty<List<ExistsFilterItem>>("Items", this.Items, false);
			builder.EndObject();
		}
	}
}
