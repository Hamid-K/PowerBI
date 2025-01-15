using System;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions
{
	// Token: 0x0200005A RID: 90
	internal interface IExpressionNodeVisitor<TResultType>
	{
		// Token: 0x0600022F RID: 559
		TResultType Accept(FieldValueExpressionNode node);

		// Token: 0x06000230 RID: 560
		TResultType Accept(FunctionCallExpressionNode node);

		// Token: 0x06000231 RID: 561
		TResultType Accept(LiteralExpressionNode node);
	}
}
