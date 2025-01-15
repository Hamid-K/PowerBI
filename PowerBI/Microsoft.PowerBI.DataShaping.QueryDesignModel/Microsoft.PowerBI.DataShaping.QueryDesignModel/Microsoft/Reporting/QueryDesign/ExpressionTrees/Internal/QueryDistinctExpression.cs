using System;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000175 RID: 373
	internal sealed class QueryDistinctExpression : QueryUnaryExtensionExpression
	{
		// Token: 0x06001486 RID: 5254 RVA: 0x0003B0CB File Offset: 0x000392CB
		internal QueryDistinctExpression(ConceptualResultType conceptualResultType, QueryExpression argument)
			: base(conceptualResultType, argument)
		{
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x0003B0D5 File Offset: 0x000392D5
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}
	}
}
