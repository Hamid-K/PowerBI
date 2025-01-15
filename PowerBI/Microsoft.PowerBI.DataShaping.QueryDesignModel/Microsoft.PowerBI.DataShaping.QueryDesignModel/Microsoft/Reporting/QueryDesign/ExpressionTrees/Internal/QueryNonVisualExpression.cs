using System;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001A9 RID: 425
	internal sealed class QueryNonVisualExpression : QueryUnaryExtensionExpression
	{
		// Token: 0x060015CA RID: 5578 RVA: 0x0003CBC7 File Offset: 0x0003ADC7
		internal QueryNonVisualExpression(ConceptualResultType conceptualResultType, QueryExpression argument)
			: base(conceptualResultType, argument)
		{
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x0003CBD1 File Offset: 0x0003ADD1
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}
	}
}
