using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x02000062 RID: 98
	internal sealed class CompoundFilterCondition : FilterCondition, INegatableCondition
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00005C15 File Offset: 0x00003E15
		// (set) Token: 0x06000211 RID: 529 RVA: 0x00005C1D File Offset: 0x00003E1D
		public Candidate<CompoundFilterOperator> Operator { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00005C26 File Offset: 0x00003E26
		// (set) Token: 0x06000213 RID: 531 RVA: 0x00005C2E File Offset: 0x00003E2E
		public List<FilterCondition> Conditions { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00005C37 File Offset: 0x00003E37
		public bool IsNegated
		{
			get
			{
				return this.Operator == CompoundFilterOperator.NotAll || this.Operator == CompoundFilterOperator.NotAny;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00005C5F File Offset: 0x00003E5F
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.CompoundFilterCondition;
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00005C63 File Offset: 0x00003E63
		public override TResult Accept<TResult>(FilterVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00005C6C File Offset: 0x00003E6C
		public override bool Equals(FilterCondition other, IEqualityComparer<Expression> expressionComparer)
		{
			bool flag;
			CompoundFilterCondition compoundFilterCondition;
			if (FilterCondition.CheckReferenceAndTypeEquality<CompoundFilterCondition>(this, other, out flag, out compoundFilterCondition))
			{
				return flag;
			}
			return base.Id == compoundFilterCondition.Id && this.Operator == compoundFilterCondition.Operator && this.Conditions.SequenceEqual(compoundFilterCondition.Conditions, new FilterConditionComparer(expressionComparer));
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00005CC8 File Offset: 0x00003EC8
		protected override int GetHashCodeImpl(IEqualityComparer<Expression> expressionComparer)
		{
			FilterConditionComparer filterConditionComparer = ((expressionComparer != null) ? new FilterConditionComparer(expressionComparer) : null);
			return Hashing.CombineHash(Hashing.GetHashCode<Identifier>(base.Id, null), Hashing.GetHashCode<Candidate<CompoundFilterOperator>>(this.Operator, null), Hashing.CombineHash<FilterCondition>(this.Conditions, filterConditionComparer));
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00005D0C File Offset: 0x00003F0C
		internal FilterCondition Clone(List<FilterCondition> newConditions)
		{
			FilterCondition filterCondition;
			if (this.TryUnwrapSingleOrSelf(newConditions, out filterCondition))
			{
				return filterCondition;
			}
			return new CompoundFilterCondition
			{
				Id = base.Id,
				Operator = this.Operator,
				Conditions = newConditions
			};
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00005D4C File Offset: 0x00003F4C
		internal FilterCondition UnwrapSingleOrSelf()
		{
			FilterCondition filterCondition;
			if (this.TryUnwrapSingleOrSelf(this.Conditions, out filterCondition))
			{
				return filterCondition;
			}
			return this;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00005D6C File Offset: 0x00003F6C
		private bool TryUnwrapSingleOrSelf(List<FilterCondition> conditions, out FilterCondition singleCondition)
		{
			if (!this.IsNegated && conditions != null && conditions.Count == 1)
			{
				singleCondition = conditions.First<FilterCondition>();
				return true;
			}
			singleCondition = null;
			return false;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00005D90 File Offset: 0x00003F90
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("CompoundFilter");
			builder.WriteProperty<Identifier>("Id", base.Id, false);
			builder.WriteProperty<CompoundFilterOperator>("Operator", this.Operator);
			builder.WriteProperty<IEnumerable<IStructuredToString>>("Conditions", this.Conditions, false);
			builder.EndObject();
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00005DE3 File Offset: 0x00003FE3
		internal bool ConditionsHaveIndependentContext()
		{
			return this.Operator.Value == CompoundFilterOperator.All;
		}
	}
}
