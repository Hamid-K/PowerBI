using System;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000197 RID: 407
	internal sealed class QueryIsAggregateExpression : QueryUnaryExtensionExpression
	{
		// Token: 0x0600157D RID: 5501 RVA: 0x0003C21D File Offset: 0x0003A41D
		internal QueryIsAggregateExpression(ConceptualResultType conceptualResultType, QueryExpression argument)
			: base(conceptualResultType, argument)
		{
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x0003C227 File Offset: 0x0003A427
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}
	}
}
