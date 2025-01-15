using System;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x0200017D RID: 381
	internal abstract class QueryExtensionExpressionBase : QueryExpression
	{
		// Token: 0x060014F4 RID: 5364 RVA: 0x0003B415 File Offset: 0x00039615
		protected QueryExtensionExpressionBase(ConceptualResultType conceptualResultType)
			: base(conceptualResultType)
		{
		}
	}
}
