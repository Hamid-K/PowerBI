using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x0200005C RID: 92
	internal sealed class AnyValueFilterCondition : FilterCondition
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00005603 File Offset: 0x00003803
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x0000560B File Offset: 0x0000380B
		public List<Expression> Targets { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00005614 File Offset: 0x00003814
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x0000561C File Offset: 0x0000381C
		internal bool DefaultValueOverridesAncestors { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00005625 File Offset: 0x00003825
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.AnyValueFilterCondition;
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00005628 File Offset: 0x00003828
		public override TResult Accept<TResult>(FilterVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00005634 File Offset: 0x00003834
		public override bool Equals(FilterCondition other, IEqualityComparer<Expression> expressionComparer)
		{
			bool flag;
			AnyValueFilterCondition anyValueFilterCondition;
			if (FilterCondition.CheckReferenceAndTypeEquality<AnyValueFilterCondition>(this, other, out flag, out anyValueFilterCondition))
			{
				return flag;
			}
			return base.Id == anyValueFilterCondition.Id && this.Targets.SequenceEqual(anyValueFilterCondition.Targets, expressionComparer) && this.DefaultValueOverridesAncestors == anyValueFilterCondition.DefaultValueOverridesAncestors;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00005687 File Offset: 0x00003887
		protected override int GetHashCodeImpl(IEqualityComparer<Expression> expressionComparer)
		{
			return Hashing.CombineHash(Hashing.GetHashCode<Identifier>(base.Id, null), Hashing.CombineHash<Expression>(this.Targets, expressionComparer));
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000056A8 File Offset: 0x000038A8
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("AnyValueFilter");
			builder.WriteProperty<Identifier>("Id", base.Id, false);
			builder.WriteProperty<List<Expression>>("Targets", this.Targets, false);
			if (this.DefaultValueOverridesAncestors)
			{
				builder.WriteProperty<bool>("DefaultValueOverridesAncestors", true, false);
			}
			builder.EndObject();
		}
	}
}
