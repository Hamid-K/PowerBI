using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BBC RID: 7100
	public sealed class FunctionExpressionSyntaxNode : RangeSyntaxNode, IFunctionExpression, IExpression, ISyntaxNode, IDeclarator
	{
		// Token: 0x0600B155 RID: 45397 RVA: 0x00243A5A File Offset: 0x00241C5A
		public FunctionExpressionSyntaxNode(IFunctionTypeExpression functionType, IExpression expression)
			: this(functionType, expression, TokenRange.Null)
		{
		}

		// Token: 0x0600B156 RID: 45398 RVA: 0x00243A69 File Offset: 0x00241C69
		public FunctionExpressionSyntaxNode(IFunctionTypeExpression functionType, IExpression expression, TokenRange range)
			: base(range)
		{
			this.functionType = functionType;
			this.expression = expression;
		}

		// Token: 0x17002C71 RID: 11377
		// (get) Token: 0x0600B157 RID: 45399 RVA: 0x00075E2C File Offset: 0x0007402C
		public ExpressionKind Kind
		{
			get
			{
				return ExpressionKind.Function;
			}
		}

		// Token: 0x17002C72 RID: 11378
		// (get) Token: 0x0600B158 RID: 45400 RVA: 0x00243A80 File Offset: 0x00241C80
		public IFunctionTypeExpression FunctionType
		{
			get
			{
				return this.functionType;
			}
		}

		// Token: 0x17002C73 RID: 11379
		// (get) Token: 0x0600B159 RID: 45401 RVA: 0x00243A88 File Offset: 0x00241C88
		public IExpression Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17002C74 RID: 11380
		// (get) Token: 0x0600B15A RID: 45402 RVA: 0x00243A90 File Offset: 0x00241C90
		int IDeclarator.Count
		{
			get
			{
				return this.FunctionType.Parameters.Count;
			}
		}

		// Token: 0x17002C75 RID: 11381
		Identifier IDeclarator.this[int index]
		{
			get
			{
				return this.FunctionType.Parameters[index].Identifier;
			}
		}

		// Token: 0x04005AEF RID: 23279
		private IFunctionTypeExpression functionType;

		// Token: 0x04005AF0 RID: 23280
		private IExpression expression;
	}
}
