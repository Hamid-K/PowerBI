using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x02000081 RID: 129
	internal sealed class DefaultValueFilterCondition : FilterCondition
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600031D RID: 797 RVA: 0x00006AC0 File Offset: 0x00004CC0
		// (set) Token: 0x0600031E RID: 798 RVA: 0x00006AC8 File Offset: 0x00004CC8
		public List<Expression> Targets { get; set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600031F RID: 799 RVA: 0x00006AD1 File Offset: 0x00004CD1
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.DefaultValueFilterCondition;
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00006AD5 File Offset: 0x00004CD5
		public override TResult Accept<TResult>(FilterVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00006AE0 File Offset: 0x00004CE0
		public override bool Equals(FilterCondition other, IEqualityComparer<Expression> expressionComparer)
		{
			bool flag;
			DefaultValueFilterCondition defaultValueFilterCondition;
			if (FilterCondition.CheckReferenceAndTypeEquality<DefaultValueFilterCondition>(this, other, out flag, out defaultValueFilterCondition))
			{
				return flag;
			}
			return base.Id == defaultValueFilterCondition.Id && this.Targets.SequenceEqual(defaultValueFilterCondition.Targets, expressionComparer);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00006B23 File Offset: 0x00004D23
		protected override int GetHashCodeImpl(IEqualityComparer<Expression> expressionComparer)
		{
			return Hashing.CombineHash(Hashing.GetHashCode<Identifier>(base.Id, null), Hashing.CombineHash<Expression>(this.Targets, expressionComparer));
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00006B42 File Offset: 0x00004D42
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("DefaultValueFilter");
			builder.WriteProperty<Identifier>("Id", base.Id, false);
			builder.WriteProperty<List<Expression>>("Targets", this.Targets, false);
			builder.EndObject();
		}
	}
}
