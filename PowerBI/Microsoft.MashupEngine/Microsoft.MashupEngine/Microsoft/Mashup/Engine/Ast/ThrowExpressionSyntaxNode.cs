using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BBB RID: 7099
	public sealed class ThrowExpressionSyntaxNode : RangeSyntaxNode, IThrowExpression, IExpression, ISyntaxNode
	{
		// Token: 0x0600B151 RID: 45393 RVA: 0x00243A34 File Offset: 0x00241C34
		public ThrowExpressionSyntaxNode(IExpression expression)
			: this(expression, TokenRange.Null)
		{
		}

		// Token: 0x0600B152 RID: 45394 RVA: 0x00243A42 File Offset: 0x00241C42
		public ThrowExpressionSyntaxNode(IExpression expression, TokenRange range)
			: base(range)
		{
			this.expression = expression;
		}

		// Token: 0x17002C6F RID: 11375
		// (get) Token: 0x0600B153 RID: 45395 RVA: 0x000E78AE File Offset: 0x000E5AAE
		public ExpressionKind Kind
		{
			get
			{
				return ExpressionKind.Throw;
			}
		}

		// Token: 0x17002C70 RID: 11376
		// (get) Token: 0x0600B154 RID: 45396 RVA: 0x00243A52 File Offset: 0x00241C52
		public IExpression Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x04005AEE RID: 23278
		private IExpression expression;
	}
}
