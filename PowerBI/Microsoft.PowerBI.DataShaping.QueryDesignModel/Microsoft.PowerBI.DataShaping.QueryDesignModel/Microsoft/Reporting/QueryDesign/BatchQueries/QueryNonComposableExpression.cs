using System;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x0200025B RID: 603
	internal sealed class QueryNonComposableExpression : QueryExtensionExpressionBase
	{
		// Token: 0x06001A35 RID: 6709 RVA: 0x00048272 File Offset: 0x00046472
		internal QueryNonComposableExpression(ConceptualResultType conceptualResultType)
			: base(conceptualResultType)
		{
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x0004827B File Offset: 0x0004647B
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			throw new InvalidOperationException("The specified operation may not be composed with other expressions.");
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x00048288 File Offset: 0x00046488
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryNonComposableExpression queryNonComposableExpression;
			return !QueryExpression.CheckReferenceAndTypeEquality<QueryNonComposableExpression>(this, other, out flag, out queryNonComposableExpression) || flag;
		}
	}
}
