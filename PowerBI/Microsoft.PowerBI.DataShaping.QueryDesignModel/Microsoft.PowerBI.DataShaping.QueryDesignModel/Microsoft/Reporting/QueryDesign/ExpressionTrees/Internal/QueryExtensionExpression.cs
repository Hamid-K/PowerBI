using System;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x0200017F RID: 383
	internal abstract class QueryExtensionExpression : QueryExtensionExpressionBase
	{
		// Token: 0x060014F9 RID: 5369 RVA: 0x0003B485 File Offset: 0x00039685
		protected QueryExtensionExpression(ConceptualResultType conceptualResultType)
			: base(conceptualResultType)
		{
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x0003B48E File Offset: 0x0003968E
		public sealed override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}
	}
}
