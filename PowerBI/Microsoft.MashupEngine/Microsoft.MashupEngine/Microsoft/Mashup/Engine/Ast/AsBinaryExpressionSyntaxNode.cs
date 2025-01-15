using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BB3 RID: 7091
	public sealed class AsBinaryExpressionSyntaxNode : BinaryExpressionSyntaxNode
	{
		// Token: 0x0600B134 RID: 45364 RVA: 0x0024393D File Offset: 0x00241B3D
		public AsBinaryExpressionSyntaxNode(IExpression left, IExpression right)
			: this(left, right, TokenRange.Null)
		{
		}

		// Token: 0x0600B135 RID: 45365 RVA: 0x0024386F File Offset: 0x00241A6F
		public AsBinaryExpressionSyntaxNode(IExpression left, IExpression right, TokenRange range)
			: base(left, right, range)
		{
		}

		// Token: 0x17002C5E RID: 11358
		// (get) Token: 0x0600B136 RID: 45366 RVA: 0x001AA8D9 File Offset: 0x001A8AD9
		public override BinaryOperator2 Operator
		{
			get
			{
				return BinaryOperator2.As;
			}
		}
	}
}
