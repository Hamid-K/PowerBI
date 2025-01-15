using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BA4 RID: 7076
	public sealed class ConcatenateBinaryExpressionSyntaxNode : BinaryExpressionSyntaxNode
	{
		// Token: 0x0600B108 RID: 45320 RVA: 0x0024387A File Offset: 0x00241A7A
		public ConcatenateBinaryExpressionSyntaxNode(IExpression left, IExpression right)
			: this(left, right, TokenRange.Null)
		{
		}

		// Token: 0x0600B109 RID: 45321 RVA: 0x0024386F File Offset: 0x00241A6F
		public ConcatenateBinaryExpressionSyntaxNode(IExpression left, IExpression right, TokenRange range)
			: base(left, right, range)
		{
		}

		// Token: 0x17002C4F RID: 11343
		// (get) Token: 0x0600B10A RID: 45322 RVA: 0x0006808E File Offset: 0x0006628E
		public override BinaryOperator2 Operator
		{
			get
			{
				return BinaryOperator2.Concatenate;
			}
		}
	}
}
