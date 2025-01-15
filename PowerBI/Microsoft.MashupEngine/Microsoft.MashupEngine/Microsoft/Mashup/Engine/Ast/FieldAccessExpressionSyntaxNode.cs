using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001B95 RID: 7061
	public abstract class FieldAccessExpressionSyntaxNode : RangeSyntaxNode, IFieldAccessExpression, IExpression, ISyntaxNode
	{
		// Token: 0x0600B0CC RID: 45260 RVA: 0x0024366B File Offset: 0x0024186B
		protected FieldAccessExpressionSyntaxNode(IExpression expression, Identifier identifier, TokenRange range)
			: base(range)
		{
			this.expression = expression;
			this.identifier = identifier;
		}

		// Token: 0x17002C2D RID: 11309
		// (get) Token: 0x0600B0CD RID: 45261 RVA: 0x0000244F File Offset: 0x0000064F
		public virtual ExpressionKind Kind
		{
			get
			{
				return ExpressionKind.FieldAccess;
			}
		}

		// Token: 0x17002C2E RID: 11310
		// (get) Token: 0x0600B0CE RID: 45262 RVA: 0x00243682 File Offset: 0x00241882
		public IExpression Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17002C2F RID: 11311
		// (get) Token: 0x0600B0CF RID: 45263 RVA: 0x0024368A File Offset: 0x0024188A
		public Identifier MemberName
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x17002C30 RID: 11312
		// (get) Token: 0x0600B0D0 RID: 45264
		public abstract bool IsOptional { get; }

		// Token: 0x04005AD9 RID: 23257
		private IExpression expression;

		// Token: 0x04005ADA RID: 23258
		private Identifier identifier;
	}
}
