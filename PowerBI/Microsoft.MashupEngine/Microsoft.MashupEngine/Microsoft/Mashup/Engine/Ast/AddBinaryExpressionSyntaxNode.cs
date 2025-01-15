using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BA3 RID: 7075
	public sealed class AddBinaryExpressionSyntaxNode : BinaryExpressionSyntaxNode
	{
		// Token: 0x0600B105 RID: 45317 RVA: 0x00243860 File Offset: 0x00241A60
		public AddBinaryExpressionSyntaxNode(IExpression left, IExpression right)
			: base(left, right, TokenRange.Null)
		{
		}

		// Token: 0x0600B106 RID: 45318 RVA: 0x0024386F File Offset: 0x00241A6F
		public AddBinaryExpressionSyntaxNode(IExpression left, IExpression right, TokenRange range)
			: base(left, right, range)
		{
		}

		// Token: 0x17002C4E RID: 11342
		// (get) Token: 0x0600B107 RID: 45319 RVA: 0x00002105 File Offset: 0x00000305
		public override BinaryOperator2 Operator
		{
			get
			{
				return BinaryOperator2.Add;
			}
		}
	}
}
