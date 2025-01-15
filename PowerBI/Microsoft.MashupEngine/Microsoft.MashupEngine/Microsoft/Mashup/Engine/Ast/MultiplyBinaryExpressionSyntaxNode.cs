using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BA6 RID: 7078
	public sealed class MultiplyBinaryExpressionSyntaxNode : BinaryExpressionSyntaxNode
	{
		// Token: 0x0600B10E RID: 45326 RVA: 0x00243898 File Offset: 0x00241A98
		public MultiplyBinaryExpressionSyntaxNode(IExpression left, IExpression right)
			: this(left, right, TokenRange.Null)
		{
		}

		// Token: 0x0600B10F RID: 45327 RVA: 0x0024386F File Offset: 0x00241A6F
		public MultiplyBinaryExpressionSyntaxNode(IExpression left, IExpression right, TokenRange range)
			: base(left, right, range)
		{
		}

		// Token: 0x17002C51 RID: 11345
		// (get) Token: 0x0600B110 RID: 45328 RVA: 0x000023C4 File Offset: 0x000005C4
		public override BinaryOperator2 Operator
		{
			get
			{
				return BinaryOperator2.Multiply;
			}
		}
	}
}
