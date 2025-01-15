using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions
{
	// Token: 0x020000C1 RID: 193
	internal interface IExpressionStringBuilder
	{
		// Token: 0x06000506 RID: 1286
		string Write(ExpressionId expressionId);

		// Token: 0x06000507 RID: 1287
		string Write(IExpressionNode expressionNode);
	}
}
