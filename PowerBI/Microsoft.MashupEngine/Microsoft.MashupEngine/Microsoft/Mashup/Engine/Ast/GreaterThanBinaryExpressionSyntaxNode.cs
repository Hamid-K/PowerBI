using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BAE RID: 7086
	public sealed class GreaterThanBinaryExpressionSyntaxNode : BinaryExpressionSyntaxNode
	{
		// Token: 0x0600B125 RID: 45349 RVA: 0x002438F2 File Offset: 0x00241AF2
		public GreaterThanBinaryExpressionSyntaxNode(IExpression left, IExpression right)
			: this(left, right, TokenRange.Null)
		{
		}

		// Token: 0x0600B126 RID: 45350 RVA: 0x0024386F File Offset: 0x00241A6F
		public GreaterThanBinaryExpressionSyntaxNode(IExpression left, IExpression right, TokenRange range)
			: base(left, right, range)
		{
		}

		// Token: 0x17002C59 RID: 11353
		// (get) Token: 0x0600B127 RID: 45351 RVA: 0x0000244F File Offset: 0x0000064F
		public override BinaryOperator2 Operator
		{
			get
			{
				return BinaryOperator2.GreaterThan;
			}
		}
	}
}
