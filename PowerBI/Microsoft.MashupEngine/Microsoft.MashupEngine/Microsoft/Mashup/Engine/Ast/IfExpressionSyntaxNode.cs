using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BB8 RID: 7096
	public sealed class IfExpressionSyntaxNode : RangeSyntaxNode, IIfExpression, IExpression, ISyntaxNode
	{
		// Token: 0x0600B141 RID: 45377 RVA: 0x00243972 File Offset: 0x00241B72
		public IfExpressionSyntaxNode(IExpression condition, IExpression trueCase, IExpression falseCase, TokenRange range)
			: base(range)
		{
			this.condition = condition;
			this.trueCase = trueCase;
			this.falseCase = falseCase;
		}

		// Token: 0x17002C63 RID: 11363
		// (get) Token: 0x0600B142 RID: 45378 RVA: 0x00002475 File Offset: 0x00000675
		public ExpressionKind Kind
		{
			get
			{
				return ExpressionKind.If;
			}
		}

		// Token: 0x17002C64 RID: 11364
		// (get) Token: 0x0600B143 RID: 45379 RVA: 0x00243991 File Offset: 0x00241B91
		public IExpression Condition
		{
			get
			{
				return this.condition;
			}
		}

		// Token: 0x17002C65 RID: 11365
		// (get) Token: 0x0600B144 RID: 45380 RVA: 0x00243999 File Offset: 0x00241B99
		public IExpression TrueCase
		{
			get
			{
				return this.trueCase;
			}
		}

		// Token: 0x17002C66 RID: 11366
		// (get) Token: 0x0600B145 RID: 45381 RVA: 0x002439A1 File Offset: 0x00241BA1
		public IExpression FalseCase
		{
			get
			{
				return this.falseCase;
			}
		}

		// Token: 0x04005AE7 RID: 23271
		private IExpression condition;

		// Token: 0x04005AE8 RID: 23272
		private IExpression trueCase;

		// Token: 0x04005AE9 RID: 23273
		private IExpression falseCase;
	}
}
