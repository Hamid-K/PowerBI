using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x02000022 RID: 34
	internal interface IExpressionVisitorHost
	{
		// Token: 0x06000085 RID: 133
		ExpressionEvaluationResult Evaluate(ExpressionSyntax node);

		// Token: 0x06000086 RID: 134
		[return: TupleElementNames(new string[] { "Result", "Details" })]
		ValueTuple<ExpressionEvaluationResult, ExpressionEvaluationDetails> EvaluateWithDetails(ExpressionSyntax node);

		// Token: 0x06000087 RID: 135
		void Validate(ExpressionSyntax node);

		// Token: 0x06000088 RID: 136
		ExpressionAnalysisResult Analyze(ExpressionSyntax node);

		// Token: 0x06000089 RID: 137
		void InvalidateTypeAlignment();
	}
}
