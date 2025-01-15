using System;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions
{
	// Token: 0x0200013B RID: 315
	[DataContract]
	[KnownType(typeof(FieldValueExpressionNode))]
	[KnownType(typeof(FunctionCallExpressionNode))]
	[KnownType(typeof(LiteralExpressionNode))]
	internal abstract class ExpressionNode
	{
		// Token: 0x06000864 RID: 2148
		internal abstract TResult Accept<TResult>(IExpressionNodeVisitor<TResult> visitor);
	}
}
