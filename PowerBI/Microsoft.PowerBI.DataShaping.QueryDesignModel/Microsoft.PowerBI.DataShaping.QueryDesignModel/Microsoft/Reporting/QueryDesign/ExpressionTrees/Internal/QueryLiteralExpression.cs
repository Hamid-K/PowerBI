using System;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x0200019E RID: 414
	internal sealed class QueryLiteralExpression : QueryExpression
	{
		// Token: 0x06001595 RID: 5525 RVA: 0x0003C46C File Offset: 0x0003A66C
		internal QueryLiteralExpression(ScalarValue value, ConceptualResultType conceptualResultType)
			: base(conceptualResultType)
		{
			this.Value = value;
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001596 RID: 5526 RVA: 0x0003C47C File Offset: 0x0003A67C
		public ScalarValue Value { get; }

		// Token: 0x06001597 RID: 5527 RVA: 0x0003C484 File Offset: 0x0003A684
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x0003C498 File Offset: 0x0003A698
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryLiteralExpression queryLiteralExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryLiteralExpression>(this, other, out flag, out queryLiteralExpression))
			{
				return flag;
			}
			return this.Value.Equals(queryLiteralExpression.Value);
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x0003C4C8 File Offset: 0x0003A6C8
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}
	}
}
