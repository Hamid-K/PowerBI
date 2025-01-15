using System;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000F4 RID: 244
	internal sealed class NonPredicateMeasure : Measure
	{
		// Token: 0x06000E22 RID: 3618 RVA: 0x00023D85 File Offset: 0x00021F85
		internal NonPredicateMeasure(QueryExpression expression, string name)
			: base(expression, name)
		{
		}
	}
}
