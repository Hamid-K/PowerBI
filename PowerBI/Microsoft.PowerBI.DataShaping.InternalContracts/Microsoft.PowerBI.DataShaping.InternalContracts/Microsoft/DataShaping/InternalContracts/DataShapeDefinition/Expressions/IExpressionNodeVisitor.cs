using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions
{
	// Token: 0x0200013F RID: 319
	internal interface IExpressionNodeVisitor<TResult>
	{
		// Token: 0x06000872 RID: 2162
		TResult Visit(FieldValueExpressionNode node);

		// Token: 0x06000873 RID: 2163
		TResult Visit(FunctionCallExpressionNode node);

		// Token: 0x06000874 RID: 2164
		TResult Visit(LiteralExpressionNode node);
	}
}
