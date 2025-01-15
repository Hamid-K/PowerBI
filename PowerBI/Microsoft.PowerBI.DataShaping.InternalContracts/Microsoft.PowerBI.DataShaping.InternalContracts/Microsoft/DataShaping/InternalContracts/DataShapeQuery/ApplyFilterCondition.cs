using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x0200005D RID: 93
	internal sealed class ApplyFilterCondition : FilterCondition
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00005707 File Offset: 0x00003907
		// (set) Token: 0x060001DD RID: 477 RVA: 0x0000570F File Offset: 0x0000390F
		public Expression DataShapeReference { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00005718 File Offset: 0x00003918
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.ApplyFilterCondition;
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000571B File Offset: 0x0000391B
		public override TResult Accept<TResult>(FilterVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00005724 File Offset: 0x00003924
		public override bool Equals(FilterCondition other, IEqualityComparer<Expression> expressionComparer)
		{
			bool flag;
			ApplyFilterCondition applyFilterCondition;
			if (FilterCondition.CheckReferenceAndTypeEquality<ApplyFilterCondition>(this, other, out flag, out applyFilterCondition))
			{
				return flag;
			}
			return base.Id == applyFilterCondition.Id && expressionComparer.Equals(this.DataShapeReference, applyFilterCondition.DataShapeReference);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00005767 File Offset: 0x00003967
		protected override int GetHashCodeImpl(IEqualityComparer<Expression> exprComparer)
		{
			return Hashing.CombineHash(Hashing.GetHashCode<Identifier>(base.Id, null), Hashing.GetHashCode<Expression>(this.DataShapeReference, exprComparer));
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00005786 File Offset: 0x00003986
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("ApplyFilter");
			builder.WriteProperty<Identifier>("Id", base.Id, false);
			builder.WriteProperty<Expression>("DataShapeReference", this.DataShapeReference, false);
			builder.EndObject();
		}
	}
}
