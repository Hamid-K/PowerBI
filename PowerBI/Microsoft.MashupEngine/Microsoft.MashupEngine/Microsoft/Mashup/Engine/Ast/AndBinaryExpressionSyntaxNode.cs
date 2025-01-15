using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BA9 RID: 7081
	public sealed class AndBinaryExpressionSyntaxNode : BinaryExpressionSyntaxNode
	{
		// Token: 0x0600B117 RID: 45335 RVA: 0x00243860 File Offset: 0x00241A60
		public AndBinaryExpressionSyntaxNode(IExpression left, IExpression right)
			: base(left, right, TokenRange.Null)
		{
		}

		// Token: 0x0600B118 RID: 45336 RVA: 0x0024386F File Offset: 0x00241A6F
		public AndBinaryExpressionSyntaxNode(IExpression left, IExpression right, TokenRange range)
			: base(left, right, range)
		{
		}

		// Token: 0x17002C54 RID: 11348
		// (get) Token: 0x0600B119 RID: 45337 RVA: 0x0014025A File Offset: 0x0013E45A
		public override BinaryOperator2 Operator
		{
			get
			{
				return BinaryOperator2.And;
			}
		}
	}
}
