using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BA0 RID: 7072
	public sealed class InvocationExpressionSyntaxNode1 : RangeSyntaxNode, IInvocationExpression, IExpression, ISyntaxNode
	{
		// Token: 0x0600B0F6 RID: 45302 RVA: 0x0024379A File Offset: 0x0024199A
		public InvocationExpressionSyntaxNode1(IExpression function, IExpression argument0)
			: this(function, argument0, TokenRange.Null)
		{
		}

		// Token: 0x0600B0F7 RID: 45303 RVA: 0x002437A9 File Offset: 0x002419A9
		public InvocationExpressionSyntaxNode1(IExpression function, IExpression argument0, TokenRange range)
			: base(range)
		{
			this.function = function;
			this.argument0 = argument0;
		}

		// Token: 0x17002C45 RID: 11333
		// (get) Token: 0x0600B0F8 RID: 45304 RVA: 0x000024ED File Offset: 0x000006ED
		public ExpressionKind Kind
		{
			get
			{
				return ExpressionKind.Invocation;
			}
		}

		// Token: 0x17002C46 RID: 11334
		// (get) Token: 0x0600B0F9 RID: 45305 RVA: 0x002437C0 File Offset: 0x002419C0
		public IExpression Function
		{
			get
			{
				return this.function;
			}
		}

		// Token: 0x17002C47 RID: 11335
		// (get) Token: 0x0600B0FA RID: 45306 RVA: 0x002437C8 File Offset: 0x002419C8
		public IList<IExpression> Arguments
		{
			get
			{
				return new IExpression[] { this.argument0 };
			}
		}

		// Token: 0x04005AE0 RID: 23264
		private IExpression function;

		// Token: 0x04005AE1 RID: 23265
		private IExpression argument0;
	}
}
