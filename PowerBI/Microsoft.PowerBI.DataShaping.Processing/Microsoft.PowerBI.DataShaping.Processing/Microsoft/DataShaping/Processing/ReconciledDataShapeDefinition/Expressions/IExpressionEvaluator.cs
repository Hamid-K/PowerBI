using System;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions
{
	// Token: 0x02000059 RID: 89
	internal interface IExpressionEvaluator<TResult>
	{
		// Token: 0x0600022E RID: 558
		TResult Evaluate(ExpressionNode expr);
	}
}
