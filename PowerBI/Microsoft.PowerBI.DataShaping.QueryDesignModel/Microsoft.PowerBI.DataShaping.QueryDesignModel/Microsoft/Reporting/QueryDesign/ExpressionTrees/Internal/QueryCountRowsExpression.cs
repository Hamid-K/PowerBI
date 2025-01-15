using System;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x0200016E RID: 366
	internal sealed class QueryCountRowsExpression : QueryUnaryExtensionExpression
	{
		// Token: 0x06001466 RID: 5222 RVA: 0x0003ADD1 File Offset: 0x00038FD1
		internal QueryCountRowsExpression(ConceptualResultType conceptualResultType, QueryExpression argument)
			: base(conceptualResultType, argument)
		{
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x0003ADDB File Offset: 0x00038FDB
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}
	}
}
