using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BB4 RID: 7092
	public sealed class IsBinaryExpressionSyntaxNode : BinaryExpressionSyntaxNode
	{
		// Token: 0x0600B137 RID: 45367 RVA: 0x0024386F File Offset: 0x00241A6F
		public IsBinaryExpressionSyntaxNode(IExpression left, IExpression right, TokenRange range)
			: base(left, right, range)
		{
		}

		// Token: 0x17002C5F RID: 11359
		// (get) Token: 0x0600B138 RID: 45368 RVA: 0x00227072 File Offset: 0x00225272
		public override BinaryOperator2 Operator
		{
			get
			{
				return BinaryOperator2.Is;
			}
		}
	}
}
