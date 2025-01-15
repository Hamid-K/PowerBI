using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BB7 RID: 7095
	public sealed class NotUnaryExpressionSyntaxNode : UnaryExpressionSyntaxNode
	{
		// Token: 0x0600B13E RID: 45374 RVA: 0x00243964 File Offset: 0x00241B64
		public NotUnaryExpressionSyntaxNode(IExpression expression)
			: this(expression, TokenRange.Null)
		{
		}

		// Token: 0x0600B13F RID: 45375 RVA: 0x0024394C File Offset: 0x00241B4C
		public NotUnaryExpressionSyntaxNode(IExpression expression, TokenRange range)
			: base(expression, range)
		{
		}

		// Token: 0x17002C62 RID: 11362
		// (get) Token: 0x0600B140 RID: 45376 RVA: 0x00002105 File Offset: 0x00000305
		public override UnaryOperator2 Operator
		{
			get
			{
				return UnaryOperator2.Not;
			}
		}
	}
}
