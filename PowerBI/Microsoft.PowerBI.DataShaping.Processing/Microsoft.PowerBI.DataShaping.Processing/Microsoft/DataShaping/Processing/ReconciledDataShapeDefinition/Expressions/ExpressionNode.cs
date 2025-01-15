using System;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions
{
	// Token: 0x02000055 RID: 85
	internal abstract class ExpressionNode
	{
		// Token: 0x0600021F RID: 543
		internal abstract TResultType Accept<TResultType>(IExpressionNodeVisitor<TResultType> visitor);

		// Token: 0x06000220 RID: 544
		public abstract override string ToString();
	}
}
