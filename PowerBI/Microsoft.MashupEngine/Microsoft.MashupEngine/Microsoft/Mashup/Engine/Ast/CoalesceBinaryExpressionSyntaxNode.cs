using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BB2 RID: 7090
	public sealed class CoalesceBinaryExpressionSyntaxNode : BinaryExpressionSyntaxNode
	{
		// Token: 0x0600B131 RID: 45361 RVA: 0x0024392E File Offset: 0x00241B2E
		public CoalesceBinaryExpressionSyntaxNode(IExpression left, IExpression right)
			: this(left, right, TokenRange.Null)
		{
		}

		// Token: 0x0600B132 RID: 45362 RVA: 0x0024386F File Offset: 0x00241A6F
		public CoalesceBinaryExpressionSyntaxNode(IExpression left, IExpression right, TokenRange range)
			: base(left, right, range)
		{
		}

		// Token: 0x17002C5D RID: 11357
		// (get) Token: 0x0600B133 RID: 45363 RVA: 0x000E78AE File Offset: 0x000E5AAE
		public override BinaryOperator2 Operator
		{
			get
			{
				return BinaryOperator2.Coalesce;
			}
		}
	}
}
