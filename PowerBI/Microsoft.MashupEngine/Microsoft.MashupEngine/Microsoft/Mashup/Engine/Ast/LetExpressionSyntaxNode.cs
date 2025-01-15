using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BB9 RID: 7097
	public sealed class LetExpressionSyntaxNode : RangeSyntaxNode, ILetExpression, IExpression, ISyntaxNode, IDeclarator
	{
		// Token: 0x0600B146 RID: 45382 RVA: 0x002439A9 File Offset: 0x00241BA9
		public LetExpressionSyntaxNode(IList<VariableInitializer> initializers, IExpression expression)
			: this(initializers, expression, TokenRange.Null)
		{
		}

		// Token: 0x0600B147 RID: 45383 RVA: 0x002439B8 File Offset: 0x00241BB8
		public LetExpressionSyntaxNode(IList<VariableInitializer> initializers, IExpression expression, TokenRange range)
			: base(range)
		{
			this.initializers = initializers;
			this.expression = expression;
		}

		// Token: 0x17002C67 RID: 11367
		// (get) Token: 0x0600B148 RID: 45384 RVA: 0x00142610 File Offset: 0x00140810
		public ExpressionKind Kind
		{
			get
			{
				return ExpressionKind.Let;
			}
		}

		// Token: 0x17002C68 RID: 11368
		// (get) Token: 0x0600B149 RID: 45385 RVA: 0x002439CF File Offset: 0x00241BCF
		public IList<VariableInitializer> Variables
		{
			get
			{
				return this.initializers;
			}
		}

		// Token: 0x17002C69 RID: 11369
		// (get) Token: 0x0600B14A RID: 45386 RVA: 0x002439D7 File Offset: 0x00241BD7
		public IExpression Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17002C6A RID: 11370
		// (get) Token: 0x0600B14B RID: 45387 RVA: 0x002439DF File Offset: 0x00241BDF
		int IDeclarator.Count
		{
			get
			{
				return this.initializers.Count;
			}
		}

		// Token: 0x17002C6B RID: 11371
		Identifier IDeclarator.this[int index]
		{
			get
			{
				return this.initializers[index].Name;
			}
		}

		// Token: 0x04005AEA RID: 23274
		private IList<VariableInitializer> initializers;

		// Token: 0x04005AEB RID: 23275
		private IExpression expression;
	}
}
