using System;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000199 RID: 409
	internal sealed class QueryIsNullExpression : QueryUnaryExpression
	{
		// Token: 0x06001581 RID: 5505 RVA: 0x0003C257 File Offset: 0x0003A457
		internal QueryIsNullExpression(ConceptualResultType conceptualResultType, QueryExpression argument)
			: base(conceptualResultType, argument)
		{
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x0003C261 File Offset: 0x0003A461
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}
	}
}
