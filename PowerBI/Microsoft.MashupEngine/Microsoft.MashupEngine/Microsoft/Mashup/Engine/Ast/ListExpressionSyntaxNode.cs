using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BBE RID: 7102
	public class ListExpressionSyntaxNode : RangeSyntaxNode, IListExpression, IExpression, ISyntaxNode
	{
		// Token: 0x0600B165 RID: 45413 RVA: 0x00243B29 File Offset: 0x00241D29
		public ListExpressionSyntaxNode(IList<IExpression> elements)
			: this(elements, TokenRange.Null)
		{
		}

		// Token: 0x0600B166 RID: 45414 RVA: 0x00243B37 File Offset: 0x00241D37
		public ListExpressionSyntaxNode(IList<IExpression> elements, TokenRange range)
			: base(range)
		{
			this.elements = elements;
		}

		// Token: 0x17002C7B RID: 11387
		// (get) Token: 0x0600B167 RID: 45415 RVA: 0x00243B47 File Offset: 0x00241D47
		public IList<IExpression> Members
		{
			get
			{
				return this.elements;
			}
		}

		// Token: 0x17002C7C RID: 11388
		// (get) Token: 0x0600B168 RID: 45416 RVA: 0x0014025A File Offset: 0x0013E45A
		public ExpressionKind Kind
		{
			get
			{
				return ExpressionKind.List;
			}
		}

		// Token: 0x04005AF3 RID: 23283
		private IList<IExpression> elements;
	}
}
