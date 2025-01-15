using System;
using Microsoft.CodeAnalysis.VisualBasic;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions.Interfaces
{
	// Token: 0x02000005 RID: 5
	internal interface IObjectAccessEvaluator
	{
		// Token: 0x06000004 RID: 4
		object EvaluateProperty(object nativeObject, string propertyName, SyntaxKind expressionKind);
	}
}
