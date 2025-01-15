using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Syntactic
{
	// Token: 0x0200027B RID: 635
	internal sealed class LiteralToken : QueryToken
	{
		// Token: 0x06001616 RID: 5654 RVA: 0x0004C460 File Offset: 0x0004A660
		public LiteralToken(object value)
		{
			this.value = value;
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x0004C46F File Offset: 0x0004A66F
		internal LiteralToken(object value, string originalText)
			: this(value)
		{
			this.originalText = originalText;
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x0004C47F File Offset: 0x0004A67F
		internal LiteralToken(object value, string originalText, IEdmTypeReference expectedEdmTypeReference)
			: this(value, originalText)
		{
			this.expectedEdmTypeReference = expectedEdmTypeReference;
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001619 RID: 5657 RVA: 0x0004C490 File Offset: 0x0004A690
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Literal;
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x0600161A RID: 5658 RVA: 0x0004C493 File Offset: 0x0004A693
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x0600161B RID: 5659 RVA: 0x0004C49B File Offset: 0x0004A69B
		internal string OriginalText
		{
			get
			{
				return this.originalText;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x0600161C RID: 5660 RVA: 0x0004C4A3 File Offset: 0x0004A6A3
		internal IEdmTypeReference ExpectedEdmTypeReference
		{
			get
			{
				return this.expectedEdmTypeReference;
			}
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x0004C4AB File Offset: 0x0004A6AB
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0400092C RID: 2348
		private readonly string originalText;

		// Token: 0x0400092D RID: 2349
		private readonly object value;

		// Token: 0x0400092E RID: 2350
		private readonly IEdmTypeReference expectedEdmTypeReference;
	}
}
