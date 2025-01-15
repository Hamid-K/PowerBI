using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BAC RID: 7084
	public sealed class EqualsBinaryExpressionSyntaxNode : BinaryExpressionSyntaxNode
	{
		// Token: 0x0600B11F RID: 45343 RVA: 0x002438D4 File Offset: 0x00241AD4
		public EqualsBinaryExpressionSyntaxNode(IExpression left, IExpression right)
			: this(left, right, TokenRange.Null)
		{
		}

		// Token: 0x0600B120 RID: 45344 RVA: 0x0024386F File Offset: 0x00241A6F
		public EqualsBinaryExpressionSyntaxNode(IExpression left, IExpression right, TokenRange range)
			: base(left, right, range)
		{
		}

		// Token: 0x17002C57 RID: 11351
		// (get) Token: 0x0600B121 RID: 45345 RVA: 0x000024ED File Offset: 0x000006ED
		public override BinaryOperator2 Operator
		{
			get
			{
				return BinaryOperator2.Equals;
			}
		}
	}
}
