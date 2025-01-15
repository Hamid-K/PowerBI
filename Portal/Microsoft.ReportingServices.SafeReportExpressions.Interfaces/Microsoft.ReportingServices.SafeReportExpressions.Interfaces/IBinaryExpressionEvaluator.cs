using System;
using Microsoft.CodeAnalysis.VisualBasic;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions.Interfaces
{
	// Token: 0x02000002 RID: 2
	internal interface IBinaryExpressionEvaluator
	{
		// Token: 0x06000001 RID: 1
		object Evaluate(object leftValue, object rightValue, SyntaxKind expressionType);
	}
}
