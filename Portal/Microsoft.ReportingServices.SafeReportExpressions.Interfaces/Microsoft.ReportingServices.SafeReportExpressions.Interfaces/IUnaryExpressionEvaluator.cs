using System;
using Microsoft.CodeAnalysis.VisualBasic;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions.Interfaces
{
	// Token: 0x02000006 RID: 6
	internal interface IUnaryExpressionEvaluator
	{
		// Token: 0x06000005 RID: 5
		object Evaluate(object operandValue, SyntaxKind expressionType);
	}
}
