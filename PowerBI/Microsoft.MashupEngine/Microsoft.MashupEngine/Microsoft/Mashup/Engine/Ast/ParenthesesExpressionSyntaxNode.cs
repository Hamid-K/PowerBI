using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BC2 RID: 7106
	public sealed class ParenthesesExpressionSyntaxNode : RangeSyntaxNode, IParenthesesExpression, IExpression, ISyntaxNode
	{
		// Token: 0x0600B172 RID: 45426 RVA: 0x00243BA6 File Offset: 0x00241DA6
		public ParenthesesExpressionSyntaxNode(IExpression expression, TokenRange range)
			: base(range)
		{
			this.expression = expression;
		}

		// Token: 0x17002C83 RID: 11395
		// (get) Token: 0x0600B173 RID: 45427 RVA: 0x00140DB6 File Offset: 0x0013EFB6
		public ExpressionKind Kind
		{
			get
			{
				return ExpressionKind.Parentheses;
			}
		}

		// Token: 0x17002C84 RID: 11396
		// (get) Token: 0x0600B174 RID: 45428 RVA: 0x00243BB6 File Offset: 0x00241DB6
		public IExpression Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x04005AF8 RID: 23288
		private IExpression expression;
	}
}
