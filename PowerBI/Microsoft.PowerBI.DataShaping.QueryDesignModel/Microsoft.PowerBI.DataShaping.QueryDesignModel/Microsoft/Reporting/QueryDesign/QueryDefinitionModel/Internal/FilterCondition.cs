using System;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000D3 RID: 211
	internal abstract class FilterCondition
	{
		// Token: 0x06000D85 RID: 3461
		internal abstract QueryExpression ToPredicate();
	}
}
