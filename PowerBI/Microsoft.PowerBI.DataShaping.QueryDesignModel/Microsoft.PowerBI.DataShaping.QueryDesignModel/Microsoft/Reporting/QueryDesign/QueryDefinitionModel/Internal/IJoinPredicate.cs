using System;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000E8 RID: 232
	internal interface IJoinPredicate
	{
		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000DE8 RID: 3560
		bool IsAnchored { get; }

		// Token: 0x06000DE9 RID: 3561
		QueryExpression ToPredicateExpression();
	}
}
