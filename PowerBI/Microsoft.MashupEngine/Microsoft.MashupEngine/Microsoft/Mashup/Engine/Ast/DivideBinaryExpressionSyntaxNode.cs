using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BA7 RID: 7079
	public sealed class DivideBinaryExpressionSyntaxNode : BinaryExpressionSyntaxNode
	{
		// Token: 0x0600B111 RID: 45329 RVA: 0x002438A7 File Offset: 0x00241AA7
		public DivideBinaryExpressionSyntaxNode(IExpression left, IExpression right)
			: this(left, right, TokenRange.Null)
		{
		}

		// Token: 0x0600B112 RID: 45330 RVA: 0x0024386F File Offset: 0x00241A6F
		public DivideBinaryExpressionSyntaxNode(IExpression left, IExpression right, TokenRange range)
			: base(left, right, range)
		{
		}

		// Token: 0x17002C52 RID: 11346
		// (get) Token: 0x0600B113 RID: 45331 RVA: 0x0000240C File Offset: 0x0000060C
		public override BinaryOperator2 Operator
		{
			get
			{
				return BinaryOperator2.Divide;
			}
		}
	}
}
