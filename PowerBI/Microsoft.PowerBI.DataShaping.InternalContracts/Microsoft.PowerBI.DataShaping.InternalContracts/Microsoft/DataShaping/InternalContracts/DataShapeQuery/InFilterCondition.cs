using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x020000A1 RID: 161
	internal sealed class InFilterCondition : FilterCondition
	{
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0000734C File Offset: 0x0000554C
		// (set) Token: 0x060003C0 RID: 960 RVA: 0x00007354 File Offset: 0x00005554
		public List<Expression> Expressions { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000735D File Offset: 0x0000555D
		// (set) Token: 0x060003C2 RID: 962 RVA: 0x00007365 File Offset: 0x00005565
		public List<List<Expression>> Values { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000736E File Offset: 0x0000556E
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x00007376 File Offset: 0x00005576
		public Expression Table { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0000737F File Offset: 0x0000557F
		// (set) Token: 0x060003C6 RID: 966 RVA: 0x00007387 File Offset: 0x00005587
		public bool IdentityComparison { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x00007390 File Offset: 0x00005590
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.InFilterCondition;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00007394 File Offset: 0x00005594
		public bool HasValues
		{
			get
			{
				return this.Values != null;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000739F File Offset: 0x0000559F
		public bool HasTable
		{
			get
			{
				return this.Table != null;
			}
		}

		// Token: 0x060003CA RID: 970 RVA: 0x000073AA File Offset: 0x000055AA
		public override TResult Accept<TResult>(FilterVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x000073B4 File Offset: 0x000055B4
		public override bool Equals(FilterCondition other, IEqualityComparer<Expression> expressionComparer)
		{
			bool flag;
			InFilterCondition inFilterCondition;
			if (FilterCondition.CheckReferenceAndTypeEquality<InFilterCondition>(this, other, out flag, out inFilterCondition))
			{
				return flag;
			}
			return base.Id == inFilterCondition.Id && this.Expressions.SequenceEqual(inFilterCondition.Expressions, expressionComparer) && this.Values.SequenceEqual(inFilterCondition.Values, expressionComparer) && expressionComparer.Equals(this.Table, inFilterCondition.Table) && this.IdentityComparison == inFilterCondition.IdentityComparison;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00007430 File Offset: 0x00005630
		protected override int GetHashCodeImpl(IEqualityComparer<Expression> expressionComparer)
		{
			return Hashing.CombineHash(Hashing.GetHashCode<Identifier>(base.Id, null), Hashing.CombineHash<Expression>(this.Expressions, expressionComparer), Hashing.CombineHash<Expression>(this.Values, expressionComparer), Hashing.GetHashCode<Expression>(this.Table, expressionComparer), this.IdentityComparison.GetHashCode());
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00007480 File Offset: 0x00005680
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("InFilter");
			builder.WriteProperty<Identifier>("Id", base.Id, false);
			builder.WriteProperty<IEnumerable<IStructuredToString>>("Expressions", this.Expressions, false);
			builder.WriteProperty<IEnumerable<IEnumerable<IStructuredToString>>>("Values", this.Values, false);
			builder.WriteProperty<Expression>("Table", this.Table, false);
			builder.WriteProperty<bool>("IdentityComparison", this.IdentityComparison, false);
			builder.EndObject();
		}
	}
}
