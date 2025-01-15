using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x020000B9 RID: 185
	internal sealed class UnaryFilterCondition : FilterCondition, INegatableCondition
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x00007BEC File Offset: 0x00005DEC
		// (set) Token: 0x0600044B RID: 1099 RVA: 0x00007BF4 File Offset: 0x00005DF4
		public Expression Expression { get; set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x00007BFD File Offset: 0x00005DFD
		// (set) Token: 0x0600044D RID: 1101 RVA: 0x00007C05 File Offset: 0x00005E05
		public Candidate<bool> Not { get; set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x00007C0E File Offset: 0x00005E0E
		public bool IsNegated
		{
			get
			{
				return this.Not == true;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x00007C21 File Offset: 0x00005E21
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.UnaryFilterCondition;
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00007C25 File Offset: 0x00005E25
		public override TResult Accept<TResult>(FilterVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00007C30 File Offset: 0x00005E30
		public override bool Equals(FilterCondition other, IEqualityComparer<Expression> expressionComparer)
		{
			bool flag;
			UnaryFilterCondition unaryFilterCondition;
			if (FilterCondition.CheckReferenceAndTypeEquality<UnaryFilterCondition>(this, other, out flag, out unaryFilterCondition))
			{
				return flag;
			}
			return base.Id == unaryFilterCondition.Id && this.Not == unaryFilterCondition.Not && expressionComparer.Equals(this.Expression, unaryFilterCondition.Expression);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00007C86 File Offset: 0x00005E86
		protected override int GetHashCodeImpl(IEqualityComparer<Expression> expressionComparer)
		{
			return Hashing.CombineHash(Hashing.GetHashCode<Identifier>(base.Id, null), Hashing.GetHashCode<Candidate<bool>>(this.Not, null), Hashing.GetHashCode<Expression>(this.Expression, expressionComparer));
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00007CB4 File Offset: 0x00005EB4
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("UnaryFilter");
			builder.WriteProperty<Identifier>("Id", base.Id, false);
			builder.WriteProperty<bool>("Not", this.Not);
			builder.WriteProperty<Expression>("Expression", this.Expression, false);
			builder.EndObject();
		}
	}
}
