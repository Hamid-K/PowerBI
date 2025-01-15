using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BC1 RID: 7105
	public sealed class RangeListExpressionSyntaxNode : RangeSyntaxNode, IRangeListExpression, IExpression, ISyntaxNode
	{
		// Token: 0x0600B16F RID: 45423 RVA: 0x00243B8E File Offset: 0x00241D8E
		public RangeListExpressionSyntaxNode(IList<IRangeExpression> elements, TokenRange range)
			: base(range)
		{
			this.elements = elements;
		}

		// Token: 0x17002C81 RID: 11393
		// (get) Token: 0x0600B170 RID: 45424 RVA: 0x00243B9E File Offset: 0x00241D9E
		public IList<IRangeExpression> Members
		{
			get
			{
				return this.elements;
			}
		}

		// Token: 0x17002C82 RID: 11394
		// (get) Token: 0x0600B171 RID: 45425 RVA: 0x0006808E File Offset: 0x0006628E
		public ExpressionKind Kind
		{
			get
			{
				return ExpressionKind.RangeList;
			}
		}

		// Token: 0x04005AF7 RID: 23287
		private IList<IRangeExpression> elements;
	}
}
