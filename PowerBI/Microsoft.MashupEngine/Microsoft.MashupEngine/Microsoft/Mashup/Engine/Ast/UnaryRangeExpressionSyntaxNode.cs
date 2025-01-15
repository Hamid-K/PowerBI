using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BBF RID: 7103
	public sealed class UnaryRangeExpressionSyntaxNode : RangeSyntaxNode, IRangeExpression, ISyntaxNode
	{
		// Token: 0x0600B169 RID: 45417 RVA: 0x00243B4F File Offset: 0x00241D4F
		public UnaryRangeExpressionSyntaxNode(IExpression element, TokenRange range)
			: base(range)
		{
			this.element = element;
		}

		// Token: 0x17002C7D RID: 11389
		// (get) Token: 0x0600B16A RID: 45418 RVA: 0x00243B5F File Offset: 0x00241D5F
		public IExpression Lower
		{
			get
			{
				return this.element;
			}
		}

		// Token: 0x17002C7E RID: 11390
		// (get) Token: 0x0600B16B RID: 45419 RVA: 0x00243B5F File Offset: 0x00241D5F
		public IExpression Upper
		{
			get
			{
				return this.element;
			}
		}

		// Token: 0x04005AF4 RID: 23284
		private IExpression element;
	}
}
