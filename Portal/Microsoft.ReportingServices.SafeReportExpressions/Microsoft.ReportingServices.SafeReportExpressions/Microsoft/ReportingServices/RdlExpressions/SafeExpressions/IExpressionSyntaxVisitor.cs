using System;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x02000021 RID: 33
	internal interface IExpressionSyntaxVisitor<T>
	{
		// Token: 0x06000081 RID: 129
		ExpressionEvaluationResult Evaluate(IExpressionVisitorHost host, T node);

		// Token: 0x06000082 RID: 130
		void Validate(IExpressionVisitorHost host, T node);

		// Token: 0x06000083 RID: 131
		ExpressionAnalysisResult Analyze(IExpressionVisitorHost host, T node);

		// Token: 0x06000084 RID: 132
		bool IsEnabled(T node);
	}
}
