using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BA2 RID: 7074
	public sealed class InvocationExpressionSyntaxNodeN : RangeSyntaxNode, IInvocationExpression, IExpression, ISyntaxNode
	{
		// Token: 0x0600B100 RID: 45312 RVA: 0x0024382A File Offset: 0x00241A2A
		public InvocationExpressionSyntaxNodeN(IExpression function, params IExpression[] arguments)
			: this(function, arguments, TokenRange.Null)
		{
		}

		// Token: 0x0600B101 RID: 45313 RVA: 0x00243839 File Offset: 0x00241A39
		public InvocationExpressionSyntaxNodeN(IExpression function, IList<IExpression> arguments, TokenRange range)
			: base(range)
		{
			this.function = function;
			this.arguments = arguments;
		}

		// Token: 0x17002C4B RID: 11339
		// (get) Token: 0x0600B102 RID: 45314 RVA: 0x000024ED File Offset: 0x000006ED
		public ExpressionKind Kind
		{
			get
			{
				return ExpressionKind.Invocation;
			}
		}

		// Token: 0x17002C4C RID: 11340
		// (get) Token: 0x0600B103 RID: 45315 RVA: 0x00243850 File Offset: 0x00241A50
		public IExpression Function
		{
			get
			{
				return this.function;
			}
		}

		// Token: 0x17002C4D RID: 11341
		// (get) Token: 0x0600B104 RID: 45316 RVA: 0x00243858 File Offset: 0x00241A58
		public IList<IExpression> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x04005AE5 RID: 23269
		private IExpression function;

		// Token: 0x04005AE6 RID: 23270
		private IList<IExpression> arguments;
	}
}
