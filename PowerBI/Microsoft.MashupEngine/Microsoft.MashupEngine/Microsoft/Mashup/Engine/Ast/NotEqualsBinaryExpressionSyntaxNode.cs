using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BAD RID: 7085
	public sealed class NotEqualsBinaryExpressionSyntaxNode : BinaryExpressionSyntaxNode
	{
		// Token: 0x0600B122 RID: 45346 RVA: 0x002438E3 File Offset: 0x00241AE3
		public NotEqualsBinaryExpressionSyntaxNode(IExpression left, IExpression right)
			: this(left, right, TokenRange.Null)
		{
		}

		// Token: 0x0600B123 RID: 45347 RVA: 0x0024386F File Offset: 0x00241A6F
		public NotEqualsBinaryExpressionSyntaxNode(IExpression left, IExpression right, TokenRange range)
			: base(left, right, range)
		{
		}

		// Token: 0x17002C58 RID: 11352
		// (get) Token: 0x0600B124 RID: 45348 RVA: 0x00142610 File Offset: 0x00140810
		public override BinaryOperator2 Operator
		{
			get
			{
				return BinaryOperator2.NotEquals;
			}
		}
	}
}
