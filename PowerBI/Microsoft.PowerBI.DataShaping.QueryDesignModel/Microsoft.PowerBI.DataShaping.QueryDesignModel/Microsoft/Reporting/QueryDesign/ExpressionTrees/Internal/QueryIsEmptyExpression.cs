using System;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000198 RID: 408
	internal sealed class QueryIsEmptyExpression : QueryUnaryExtensionExpression
	{
		// Token: 0x0600157F RID: 5503 RVA: 0x0003C23A File Offset: 0x0003A43A
		internal QueryIsEmptyExpression(ConceptualResultType conceptualResultType, QueryExpression argument)
			: base(conceptualResultType, argument)
		{
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x0003C244 File Offset: 0x0003A444
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}
	}
}
