using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BA8 RID: 7080
	public sealed class OrBinaryExpressionSyntaxNode : BinaryExpressionSyntaxNode
	{
		// Token: 0x0600B114 RID: 45332 RVA: 0x002438B6 File Offset: 0x00241AB6
		public OrBinaryExpressionSyntaxNode(IExpression left, IExpression right)
			: this(left, right, TokenRange.Null)
		{
		}

		// Token: 0x0600B115 RID: 45333 RVA: 0x0024386F File Offset: 0x00241A6F
		public OrBinaryExpressionSyntaxNode(IExpression left, IExpression right, TokenRange range)
			: base(left, right, range)
		{
		}

		// Token: 0x17002C53 RID: 11347
		// (get) Token: 0x0600B116 RID: 45334 RVA: 0x0014213C File Offset: 0x0014033C
		public override BinaryOperator2 Operator
		{
			get
			{
				return BinaryOperator2.Or;
			}
		}
	}
}
