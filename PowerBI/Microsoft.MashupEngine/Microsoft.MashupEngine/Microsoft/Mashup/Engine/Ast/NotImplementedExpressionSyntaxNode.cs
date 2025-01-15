using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BC3 RID: 7107
	public sealed class NotImplementedExpressionSyntaxNode : RangeSyntaxNode, INotImplementedExpression, IExpression, ISyntaxNode
	{
		// Token: 0x0600B175 RID: 45429 RVA: 0x00243BBE File Offset: 0x00241DBE
		public NotImplementedExpressionSyntaxNode()
			: base(TokenRange.Null)
		{
		}

		// Token: 0x0600B176 RID: 45430 RVA: 0x002436C8 File Offset: 0x002418C8
		public NotImplementedExpressionSyntaxNode(TokenRange range)
			: base(range)
		{
		}

		// Token: 0x17002C85 RID: 11397
		// (get) Token: 0x0600B177 RID: 45431 RVA: 0x001422C0 File Offset: 0x001404C0
		public ExpressionKind Kind
		{
			get
			{
				return ExpressionKind.NotImplemented;
			}
		}
	}
}
