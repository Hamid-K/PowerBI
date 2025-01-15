using System;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000169 RID: 361
	internal sealed class QueryComparisonExpression : QueryBinaryExpression
	{
		// Token: 0x06001450 RID: 5200 RVA: 0x0003AB7D File Offset: 0x00038D7D
		internal QueryComparisonExpression(QueryComparisonKind comparisonKind, ConceptualResultType conceptualResultType, QueryExpression left, QueryExpression right)
			: base(conceptualResultType, left, right)
		{
			this.ComparisonKind = comparisonKind;
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001451 RID: 5201 RVA: 0x0003AB90 File Offset: 0x00038D90
		public QueryComparisonKind ComparisonKind { get; }

		// Token: 0x06001452 RID: 5202 RVA: 0x0003AB98 File Offset: 0x00038D98
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x0003ABAC File Offset: 0x00038DAC
		public override bool Equals(QueryExpression other)
		{
			if (!base.Equals(other))
			{
				return false;
			}
			QueryComparisonExpression queryComparisonExpression = other as QueryComparisonExpression;
			return queryComparisonExpression != null && base.Equals(queryComparisonExpression) && queryComparisonExpression.ComparisonKind == this.ComparisonKind;
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x0003ABE8 File Offset: 0x00038DE8
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetHashCode(), this.ComparisonKind.GetHashCode());
		}
	}
}
