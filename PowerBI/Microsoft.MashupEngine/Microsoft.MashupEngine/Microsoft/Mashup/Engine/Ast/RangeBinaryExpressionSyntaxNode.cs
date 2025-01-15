using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BAA RID: 7082
	public sealed class RangeBinaryExpressionSyntaxNode : BinaryExpressionSyntaxNode
	{
		// Token: 0x0600B11A RID: 45338 RVA: 0x002438C5 File Offset: 0x00241AC5
		public RangeBinaryExpressionSyntaxNode(IExpression left, IExpression right)
			: this(left, right, TokenRange.Null)
		{
		}

		// Token: 0x0600B11B RID: 45339 RVA: 0x0024386F File Offset: 0x00241A6F
		public RangeBinaryExpressionSyntaxNode(IExpression left, IExpression right, TokenRange range)
			: base(left, right, range)
		{
		}

		// Token: 0x17002C55 RID: 11349
		// (get) Token: 0x0600B11C RID: 45340 RVA: 0x00140DB6 File Offset: 0x0013EFB6
		public override BinaryOperator2 Operator
		{
			get
			{
				return BinaryOperator2.Range;
			}
		}
	}
}
