using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BBA RID: 7098
	public sealed class TryCatchExpressionSyntaxNode : RangeSyntaxNode, ITryCatchExpression, IExpression, ISyntaxNode
	{
		// Token: 0x0600B14D RID: 45389 RVA: 0x00243A0D File Offset: 0x00241C0D
		public TryCatchExpressionSyntaxNode(IExpression tryExpression, TryCatchExceptionCase exceptionCase, TokenRange range)
			: base(range)
		{
			this.tryExpression = tryExpression;
			this.exceptionCase = exceptionCase;
		}

		// Token: 0x17002C6C RID: 11372
		// (get) Token: 0x0600B14E RID: 45390 RVA: 0x0012AF0D File Offset: 0x0012910D
		public ExpressionKind Kind
		{
			get
			{
				return ExpressionKind.TryCatch;
			}
		}

		// Token: 0x17002C6D RID: 11373
		// (get) Token: 0x0600B14F RID: 45391 RVA: 0x00243A24 File Offset: 0x00241C24
		public IExpression Try
		{
			get
			{
				return this.tryExpression;
			}
		}

		// Token: 0x17002C6E RID: 11374
		// (get) Token: 0x0600B150 RID: 45392 RVA: 0x00243A2C File Offset: 0x00241C2C
		public TryCatchExceptionCase ExceptionCase
		{
			get
			{
				return this.exceptionCase;
			}
		}

		// Token: 0x04005AEC RID: 23276
		private IExpression tryExpression;

		// Token: 0x04005AED RID: 23277
		private TryCatchExceptionCase exceptionCase;
	}
}
