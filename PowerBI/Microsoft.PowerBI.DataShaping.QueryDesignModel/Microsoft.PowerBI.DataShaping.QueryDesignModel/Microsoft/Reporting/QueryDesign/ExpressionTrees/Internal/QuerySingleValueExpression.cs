using System;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001B6 RID: 438
	internal sealed class QuerySingleValueExpression : QueryUnaryExtensionExpression
	{
		// Token: 0x06001612 RID: 5650 RVA: 0x0003D42D File Offset: 0x0003B62D
		internal QuerySingleValueExpression(ConceptualResultType conceptualResultType, QueryExpression argument)
			: base(conceptualResultType, argument)
		{
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x0003D437 File Offset: 0x0003B637
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}
	}
}
