using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x0200005E RID: 94
	internal sealed class BinaryFilterCondition : FilterCondition, INegatableCondition
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x000057C5 File Offset: 0x000039C5
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x000057CD File Offset: 0x000039CD
		public Expression LeftExpression { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x000057D6 File Offset: 0x000039D6
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x000057DE File Offset: 0x000039DE
		public Expression RightExpression { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x000057E7 File Offset: 0x000039E7
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x000057EF File Offset: 0x000039EF
		public Candidate<BinaryFilterOperator> Operator { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001EA RID: 490 RVA: 0x000057F8 File Offset: 0x000039F8
		// (set) Token: 0x060001EB RID: 491 RVA: 0x00005800 File Offset: 0x00003A00
		public Candidate<bool> Not { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00005809 File Offset: 0x00003A09
		public bool IsNegated
		{
			get
			{
				return this.Not == true;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000581C File Offset: 0x00003A1C
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.BinaryFilterCondition;
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000581F File Offset: 0x00003A1F
		public override TResult Accept<TResult>(FilterVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00005828 File Offset: 0x00003A28
		public override bool Equals(FilterCondition other, IEqualityComparer<Expression> expressionComparer)
		{
			bool flag;
			BinaryFilterCondition binaryFilterCondition;
			if (FilterCondition.CheckReferenceAndTypeEquality<BinaryFilterCondition>(this, other, out flag, out binaryFilterCondition))
			{
				return flag;
			}
			return base.Id == binaryFilterCondition.Id && this.Not == binaryFilterCondition.Not && this.Operator == binaryFilterCondition.Operator && expressionComparer.Equals(this.LeftExpression, binaryFilterCondition.LeftExpression) && expressionComparer.Equals(this.RightExpression, binaryFilterCondition.RightExpression);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000058A8 File Offset: 0x00003AA8
		protected override int GetHashCodeImpl(IEqualityComparer<Expression> exprComparer)
		{
			return Hashing.CombineHash(Hashing.GetHashCode<Identifier>(base.Id, null), Hashing.GetHashCode<Candidate<bool>>(this.Not, null), Hashing.GetHashCode<Candidate<BinaryFilterOperator>>(this.Operator, null), Hashing.GetHashCode<Expression>(this.LeftExpression, exprComparer), Hashing.GetHashCode<Expression>(this.RightExpression, exprComparer));
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000058F8 File Offset: 0x00003AF8
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("BinaryFilter");
			builder.WriteProperty<Identifier>("Id", base.Id, false);
			builder.WriteProperty<Expression>("Left", this.LeftExpression, false);
			builder.WriteProperty<bool>("Not", this.Not);
			builder.WriteProperty<BinaryFilterOperator>("Operator", this.Operator);
			builder.WriteProperty<Expression>("Right", this.RightExpression, false);
			builder.EndObject();
		}
	}
}
