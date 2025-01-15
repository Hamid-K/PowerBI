using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x02000089 RID: 137
	internal sealed class ExistsFilterItem : IStructuredToString, IEquatable<ExistsFilterItem>, IExpressionEquatable<ExistsFilterItem>
	{
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00006D7E File Offset: 0x00004F7E
		// (set) Token: 0x06000350 RID: 848 RVA: 0x00006D86 File Offset: 0x00004F86
		public List<Expression> Targets { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00006D8F File Offset: 0x00004F8F
		// (set) Token: 0x06000352 RID: 850 RVA: 0x00006D97 File Offset: 0x00004F97
		public Expression Exists { get; set; }

		// Token: 0x06000353 RID: 851 RVA: 0x00006DA0 File Offset: 0x00004FA0
		public void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("ExistsFilterItem");
			builder.WriteProperty<List<Expression>>("Targets", this.Targets, false);
			builder.WriteProperty<Expression>("Exists", this.Exists, false);
			builder.EndObject();
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00006DD7 File Offset: 0x00004FD7
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ExistsFilterItem);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00006DE5 File Offset: 0x00004FE5
		public bool Equals(ExistsFilterItem other)
		{
			return this.Equals(other, FilterConditionComparer.DefaultExpressionComparer);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00006DF3 File Offset: 0x00004FF3
		public bool Equals(ExistsFilterItem other, IEqualityComparer<Expression> expressionComparer)
		{
			return other != null && expressionComparer.Equals(this.Exists, other.Exists) && this.Targets.SequenceEqual(other.Targets, expressionComparer);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00006E24 File Offset: 0x00005024
		public int GetHashCode(IEqualityComparer<Expression> expressionComparer)
		{
			return Hashing.CombineHash(this.Exists.ExpressionId.GetHashCode(), Hashing.CombineHash<Expression>(this.Targets, expressionComparer));
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00006E5B File Offset: 0x0000505B
		public override int GetHashCode()
		{
			return this.GetHashCode(FilterConditionComparer.DefaultExpressionComparer);
		}
	}
}
