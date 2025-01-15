using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BC4 RID: 7108
	public sealed class TypeExpressionSyntaxNode : RangeSyntaxNode, ITypeExpression, IExpression, ISyntaxNode
	{
		// Token: 0x0600B178 RID: 45432 RVA: 0x00243BCB File Offset: 0x00241DCB
		public TypeExpressionSyntaxNode(IExpression expression, TokenRange range)
			: base(range)
		{
			this.expression = expression;
		}

		// Token: 0x17002C86 RID: 11398
		// (get) Token: 0x0600B179 RID: 45433 RVA: 0x00243BDB File Offset: 0x00241DDB
		public ExpressionKind Kind
		{
			get
			{
				return ExpressionKind.Type;
			}
		}

		// Token: 0x17002C87 RID: 11399
		// (get) Token: 0x0600B17A RID: 45434 RVA: 0x00243BDF File Offset: 0x00241DDF
		public IExpression Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x04005AF9 RID: 23289
		private IExpression expression;
	}
}
