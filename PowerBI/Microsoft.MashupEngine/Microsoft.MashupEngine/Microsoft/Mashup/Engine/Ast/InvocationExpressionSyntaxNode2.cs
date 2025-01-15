using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BA1 RID: 7073
	public sealed class InvocationExpressionSyntaxNode2 : RangeSyntaxNode, IInvocationExpression, IExpression, ISyntaxNode
	{
		// Token: 0x0600B0FB RID: 45307 RVA: 0x002437D9 File Offset: 0x002419D9
		public InvocationExpressionSyntaxNode2(IExpression function, IExpression argument0, IExpression argument1)
			: this(function, argument0, argument1, TokenRange.Null)
		{
		}

		// Token: 0x0600B0FC RID: 45308 RVA: 0x002437E9 File Offset: 0x002419E9
		public InvocationExpressionSyntaxNode2(IExpression function, IExpression argument0, IExpression argument1, TokenRange range)
			: base(range)
		{
			this.function = function;
			this.argument0 = argument0;
			this.argument1 = argument1;
		}

		// Token: 0x17002C48 RID: 11336
		// (get) Token: 0x0600B0FD RID: 45309 RVA: 0x000024ED File Offset: 0x000006ED
		public ExpressionKind Kind
		{
			get
			{
				return ExpressionKind.Invocation;
			}
		}

		// Token: 0x17002C49 RID: 11337
		// (get) Token: 0x0600B0FE RID: 45310 RVA: 0x00243808 File Offset: 0x00241A08
		public IExpression Function
		{
			get
			{
				return this.function;
			}
		}

		// Token: 0x17002C4A RID: 11338
		// (get) Token: 0x0600B0FF RID: 45311 RVA: 0x00243810 File Offset: 0x00241A10
		public IList<IExpression> Arguments
		{
			get
			{
				return new IExpression[] { this.argument0, this.argument1 };
			}
		}

		// Token: 0x04005AE2 RID: 23266
		private IExpression function;

		// Token: 0x04005AE3 RID: 23267
		private IExpression argument0;

		// Token: 0x04005AE4 RID: 23268
		private IExpression argument1;
	}
}
