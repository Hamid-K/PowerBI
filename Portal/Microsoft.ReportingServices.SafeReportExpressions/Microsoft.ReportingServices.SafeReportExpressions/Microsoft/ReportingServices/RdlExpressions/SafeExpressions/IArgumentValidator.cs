using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x0200001A RID: 26
	internal interface IArgumentValidator
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600006E RID: 110
		string FunctionName { get; }

		// Token: 0x0600006F RID: 111
		void ValidateArguments(List<ExpressionSyntax> argumentExpressions);
	}
}
