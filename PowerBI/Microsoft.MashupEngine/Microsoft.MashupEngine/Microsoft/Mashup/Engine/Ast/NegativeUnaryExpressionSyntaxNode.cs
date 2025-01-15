using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BB6 RID: 7094
	public sealed class NegativeUnaryExpressionSyntaxNode : UnaryExpressionSyntaxNode
	{
		// Token: 0x0600B13B RID: 45371 RVA: 0x00243956 File Offset: 0x00241B56
		public NegativeUnaryExpressionSyntaxNode(IExpression expression)
			: this(expression, TokenRange.Null)
		{
		}

		// Token: 0x0600B13C RID: 45372 RVA: 0x0024394C File Offset: 0x00241B4C
		public NegativeUnaryExpressionSyntaxNode(IExpression expression, TokenRange range)
			: base(expression, range)
		{
		}

		// Token: 0x17002C61 RID: 11361
		// (get) Token: 0x0600B13D RID: 45373 RVA: 0x00002139 File Offset: 0x00000339
		public override UnaryOperator2 Operator
		{
			get
			{
				return UnaryOperator2.Negative;
			}
		}
	}
}
