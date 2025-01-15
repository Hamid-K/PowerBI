using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001B8E RID: 7054
	public abstract class UnaryExpressionSyntaxNode : RangeSyntaxNode, IUnaryExpression, IExpression, ISyntaxNode
	{
		// Token: 0x0600B0B0 RID: 45232 RVA: 0x0024354F File Offset: 0x0024174F
		protected UnaryExpressionSyntaxNode(IExpression expression, TokenRange range)
			: base(range)
		{
			this.expression = expression;
		}

		// Token: 0x0600B0B1 RID: 45233 RVA: 0x0024355F File Offset: 0x0024175F
		public static UnaryExpressionSyntaxNode New(UnaryOperator2 unaryOperator, IExpression expression, TokenRange range)
		{
			switch (unaryOperator)
			{
			case UnaryOperator2.Not:
				return new NotUnaryExpressionSyntaxNode(expression, range);
			case UnaryOperator2.Negative:
				return new NegativeUnaryExpressionSyntaxNode(expression, range);
			case UnaryOperator2.Positive:
				return new PositiveUnaryExpressionSyntaxNode(expression, range);
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17002C1B RID: 11291
		// (get) Token: 0x0600B0B2 RID: 45234 RVA: 0x00243592 File Offset: 0x00241792
		public ExpressionKind Kind
		{
			get
			{
				return ExpressionKind.Unary;
			}
		}

		// Token: 0x17002C1C RID: 11292
		// (get) Token: 0x0600B0B3 RID: 45235 RVA: 0x00243596 File Offset: 0x00241796
		public IExpression Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17002C1D RID: 11293
		// (get) Token: 0x0600B0B4 RID: 45236
		public abstract UnaryOperator2 Operator { get; }

		// Token: 0x04005AD0 RID: 23248
		private IExpression expression;
	}
}
