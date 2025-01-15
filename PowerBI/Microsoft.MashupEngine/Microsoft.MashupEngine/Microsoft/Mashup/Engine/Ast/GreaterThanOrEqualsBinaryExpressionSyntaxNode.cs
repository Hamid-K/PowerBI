using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BAF RID: 7087
	public sealed class GreaterThanOrEqualsBinaryExpressionSyntaxNode : BinaryExpressionSyntaxNode
	{
		// Token: 0x0600B128 RID: 45352 RVA: 0x00243901 File Offset: 0x00241B01
		public GreaterThanOrEqualsBinaryExpressionSyntaxNode(IExpression left, IExpression right)
			: this(left, right, TokenRange.Null)
		{
		}

		// Token: 0x0600B129 RID: 45353 RVA: 0x0024386F File Offset: 0x00241A6F
		public GreaterThanOrEqualsBinaryExpressionSyntaxNode(IExpression left, IExpression right, TokenRange range)
			: base(left, right, range)
		{
		}

		// Token: 0x17002C5A RID: 11354
		// (get) Token: 0x0600B12A RID: 45354 RVA: 0x00002461 File Offset: 0x00000661
		public override BinaryOperator2 Operator
		{
			get
			{
				return BinaryOperator2.GreaterThanOrEquals;
			}
		}
	}
}
