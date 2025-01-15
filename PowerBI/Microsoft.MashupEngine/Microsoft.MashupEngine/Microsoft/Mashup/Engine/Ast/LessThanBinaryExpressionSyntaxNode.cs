using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BB0 RID: 7088
	public sealed class LessThanBinaryExpressionSyntaxNode : BinaryExpressionSyntaxNode
	{
		// Token: 0x0600B12B RID: 45355 RVA: 0x00243910 File Offset: 0x00241B10
		public LessThanBinaryExpressionSyntaxNode(IExpression left, IExpression right)
			: this(left, right, TokenRange.Null)
		{
		}

		// Token: 0x0600B12C RID: 45356 RVA: 0x0024386F File Offset: 0x00241A6F
		public LessThanBinaryExpressionSyntaxNode(IExpression left, IExpression right, TokenRange range)
			: base(left, right, range)
		{
		}

		// Token: 0x17002C5B RID: 11355
		// (get) Token: 0x0600B12D RID: 45357 RVA: 0x00075E2C File Offset: 0x0007402C
		public override BinaryOperator2 Operator
		{
			get
			{
				return BinaryOperator2.LessThan;
			}
		}
	}
}
