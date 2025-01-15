using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x02000007 RID: 7
	internal interface IExpressionEvaluator
	{
		// Token: 0x06000010 RID: 16
		ExpressionSyntax ParseExpression(string expressionString);

		// Token: 0x06000011 RID: 17
		object Evaluate(ExpressionSyntax expressionSyntax);

		// Token: 0x06000012 RID: 18
		void EvaluateAndCollectNodeEvaluations(ExpressionSyntax expressionSyntax, out List<SyntaxNodeEvaluation> nodeEvaluations);

		// Token: 0x06000013 RID: 19
		ExpressionValidationResult Validate(ExpressionSyntax expressionSyntax);

		// Token: 0x06000014 RID: 20
		ExpressionAnalysisResult Analyze(ExpressionSyntax expressionSyntax);
	}
}
