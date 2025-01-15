using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BB1 RID: 7089
	public sealed class LessThanOrEqualsBinaryExpressionSyntaxNode : BinaryExpressionSyntaxNode
	{
		// Token: 0x0600B12E RID: 45358 RVA: 0x0024391F File Offset: 0x00241B1F
		public LessThanOrEqualsBinaryExpressionSyntaxNode(IExpression left, IExpression right)
			: this(left, right, TokenRange.Null)
		{
		}

		// Token: 0x0600B12F RID: 45359 RVA: 0x0024386F File Offset: 0x00241A6F
		public LessThanOrEqualsBinaryExpressionSyntaxNode(IExpression left, IExpression right, TokenRange range)
			: base(left, right, range)
		{
		}

		// Token: 0x17002C5C RID: 11356
		// (get) Token: 0x0600B130 RID: 45360 RVA: 0x00002475 File Offset: 0x00000675
		public override BinaryOperator2 Operator
		{
			get
			{
				return BinaryOperator2.LessThanOrEquals;
			}
		}
	}
}
