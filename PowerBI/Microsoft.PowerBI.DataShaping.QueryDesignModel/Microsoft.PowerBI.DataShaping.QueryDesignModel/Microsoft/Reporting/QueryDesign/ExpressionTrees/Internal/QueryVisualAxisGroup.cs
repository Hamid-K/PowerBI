using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001CE RID: 462
	internal sealed class QueryVisualAxisGroup : IEquatable<QueryVisualAxisGroup>
	{
		// Token: 0x0600168A RID: 5770 RVA: 0x0003E2E6 File Offset: 0x0003C4E6
		public QueryVisualAxisGroup(IReadOnlyList<QueryExpression> keys, QueryExpression subtotalIndicator)
		{
			this.Keys = keys;
			this.SubtotalIndicator = subtotalIndicator;
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x0600168B RID: 5771 RVA: 0x0003E2FC File Offset: 0x0003C4FC
		public IReadOnlyList<QueryExpression> Keys { get; }

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x0600168C RID: 5772 RVA: 0x0003E304 File Offset: 0x0003C504
		public QueryExpression SubtotalIndicator { get; }

		// Token: 0x0600168D RID: 5773 RVA: 0x0003E30C File Offset: 0x0003C50C
		public override bool Equals(object other)
		{
			return this.Equals(other as QueryVisualAxisGroup);
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x0003E31C File Offset: 0x0003C51C
		public bool Equals(QueryVisualAxisGroup other)
		{
			bool? flag = CompareUtil.AreEqual<QueryVisualAxisGroup, QueryVisualAxisGroup>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return this.Keys.SequenceEqualReadOnly(other.Keys) && object.Equals(this.SubtotalIndicator, other.SubtotalIndicator);
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x0003E368 File Offset: 0x0003C568
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.CombineHashReadonly<QueryExpression>(this.Keys, null), Hashing.GetHashCode<QueryExpression>(this.SubtotalIndicator, null));
		}
	}
}
