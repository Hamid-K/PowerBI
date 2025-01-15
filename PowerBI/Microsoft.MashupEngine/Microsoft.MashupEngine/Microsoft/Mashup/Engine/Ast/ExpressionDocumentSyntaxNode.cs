using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BCC RID: 7116
	public sealed class ExpressionDocumentSyntaxNode : RangeSyntaxNode, IExpressionDocument, IDocument, ISyntaxNode
	{
		// Token: 0x0600B197 RID: 45463 RVA: 0x00243D11 File Offset: 0x00241F11
		public ExpressionDocumentSyntaxNode(IExpression expression)
			: this(null, null, expression, TokenRange.Null)
		{
		}

		// Token: 0x0600B198 RID: 45464 RVA: 0x00243D21 File Offset: 0x00241F21
		public ExpressionDocumentSyntaxNode(IDocumentHost host, ITokens tokens, IExpression expression, TokenRange range)
			: base(range)
		{
			this.host = host;
			this.tokens = tokens;
			this.expression = expression;
		}

		// Token: 0x17002C9C RID: 11420
		// (get) Token: 0x0600B199 RID: 45465 RVA: 0x00002139 File Offset: 0x00000339
		public DocumentKind Kind
		{
			get
			{
				return DocumentKind.Expression;
			}
		}

		// Token: 0x17002C9D RID: 11421
		// (get) Token: 0x0600B19A RID: 45466 RVA: 0x00243D40 File Offset: 0x00241F40
		public IDocumentHost Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x17002C9E RID: 11422
		// (get) Token: 0x0600B19B RID: 45467 RVA: 0x00243D48 File Offset: 0x00241F48
		public IExpression Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17002C9F RID: 11423
		// (get) Token: 0x0600B19C RID: 45468 RVA: 0x00243D50 File Offset: 0x00241F50
		public ITokens Tokens
		{
			get
			{
				return this.tokens;
			}
		}

		// Token: 0x04005B08 RID: 23304
		private IDocumentHost host;

		// Token: 0x04005B09 RID: 23305
		private ITokens tokens;

		// Token: 0x04005B0A RID: 23306
		private IExpression expression;
	}
}
