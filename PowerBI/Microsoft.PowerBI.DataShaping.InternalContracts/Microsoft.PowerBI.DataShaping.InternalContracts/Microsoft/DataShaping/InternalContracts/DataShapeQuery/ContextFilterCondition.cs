using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x02000064 RID: 100
	internal sealed class ContextFilterCondition : FilterCondition
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00005DFB File Offset: 0x00003FFB
		// (set) Token: 0x06000220 RID: 544 RVA: 0x00005E03 File Offset: 0x00004003
		public DataShape DataShape { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00005E0C File Offset: 0x0000400C
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.ContextFilterCondition;
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00005E10 File Offset: 0x00004010
		public override TResult Accept<TResult>(FilterVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00005E1C File Offset: 0x0000401C
		public override bool Equals(FilterCondition other, IEqualityComparer<Expression> expressionComparer)
		{
			bool flag;
			ContextFilterCondition contextFilterCondition;
			if (FilterCondition.CheckReferenceAndTypeEquality<ContextFilterCondition>(this, other, out flag, out contextFilterCondition))
			{
				return flag;
			}
			return base.Id == contextFilterCondition.Id && this.DataShape == contextFilterCondition.DataShape;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00005E5B File Offset: 0x0000405B
		protected override int GetHashCodeImpl(IEqualityComparer<Expression> exprComparer)
		{
			return Hashing.CombineHash(Hashing.GetHashCode<Identifier>(base.Id, null), Hashing.GetHashCode<DataShape>(this.DataShape, null));
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00005E7A File Offset: 0x0000407A
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("ContextFilter");
			builder.WriteProperty<Identifier>("Id", base.Id, false);
			builder.WriteProperty<DataShape>("DataShape", this.DataShape, false);
			builder.EndObject();
		}
	}
}
