using System;
using System.Collections.Generic;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001B9F RID: 7071
	public sealed class InvocationExpressionSyntaxNode0 : RangeSyntaxNode, IInvocationExpression, IExpression, ISyntaxNode
	{
		// Token: 0x0600B0F1 RID: 45297 RVA: 0x0024376D File Offset: 0x0024196D
		public InvocationExpressionSyntaxNode0(IExpression function)
			: this(function, TokenRange.Null)
		{
		}

		// Token: 0x0600B0F2 RID: 45298 RVA: 0x0024377B File Offset: 0x0024197B
		public InvocationExpressionSyntaxNode0(IExpression function, TokenRange range)
			: base(range)
		{
			this.function = function;
		}

		// Token: 0x17002C42 RID: 11330
		// (get) Token: 0x0600B0F3 RID: 45299 RVA: 0x000024ED File Offset: 0x000006ED
		public ExpressionKind Kind
		{
			get
			{
				return ExpressionKind.Invocation;
			}
		}

		// Token: 0x17002C43 RID: 11331
		// (get) Token: 0x0600B0F4 RID: 45300 RVA: 0x0024378B File Offset: 0x0024198B
		public IExpression Function
		{
			get
			{
				return this.function;
			}
		}

		// Token: 0x17002C44 RID: 11332
		// (get) Token: 0x0600B0F5 RID: 45301 RVA: 0x00243793 File Offset: 0x00241993
		public IList<IExpression> Arguments
		{
			get
			{
				return EmptyArray<IExpression>.Instance;
			}
		}

		// Token: 0x04005ADF RID: 23263
		private IExpression function;
	}
}
