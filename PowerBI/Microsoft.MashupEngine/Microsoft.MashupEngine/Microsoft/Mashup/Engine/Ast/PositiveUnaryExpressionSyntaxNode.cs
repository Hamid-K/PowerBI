using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BB5 RID: 7093
	public sealed class PositiveUnaryExpressionSyntaxNode : UnaryExpressionSyntaxNode
	{
		// Token: 0x0600B139 RID: 45369 RVA: 0x0024394C File Offset: 0x00241B4C
		public PositiveUnaryExpressionSyntaxNode(IExpression expression, TokenRange range)
			: base(expression, range)
		{
		}

		// Token: 0x17002C60 RID: 11360
		// (get) Token: 0x0600B13A RID: 45370 RVA: 0x000023C4 File Offset: 0x000005C4
		public override UnaryOperator2 Operator
		{
			get
			{
				return UnaryOperator2.Positive;
			}
		}
	}
}
