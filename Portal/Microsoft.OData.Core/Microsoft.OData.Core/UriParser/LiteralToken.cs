using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001C2 RID: 450
	public sealed class LiteralToken : QueryToken
	{
		// Token: 0x060014CF RID: 5327 RVA: 0x0003C096 File Offset: 0x0003A296
		public LiteralToken(object value)
		{
			this.value = value;
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x0003C0A5 File Offset: 0x0003A2A5
		internal LiteralToken(object value, string originalText)
			: this(value)
		{
			this.originalText = originalText;
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x0003C0B5 File Offset: 0x0003A2B5
		internal LiteralToken(object value, string originalText, IEdmTypeReference expectedEdmTypeReference)
			: this(value, originalText)
		{
			this.expectedEdmTypeReference = expectedEdmTypeReference;
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x060014D2 RID: 5330 RVA: 0x0003BB34 File Offset: 0x00039D34
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Literal;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x0003C0C6 File Offset: 0x0003A2C6
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060014D4 RID: 5332 RVA: 0x0003C0CE File Offset: 0x0003A2CE
		internal string OriginalText
		{
			get
			{
				return this.originalText;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060014D5 RID: 5333 RVA: 0x0003C0D6 File Offset: 0x0003A2D6
		internal IEdmTypeReference ExpectedEdmTypeReference
		{
			get
			{
				return this.expectedEdmTypeReference;
			}
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x0003C0DE File Offset: 0x0003A2DE
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000918 RID: 2328
		private readonly string originalText;

		// Token: 0x04000919 RID: 2329
		private readonly object value;

		// Token: 0x0400091A RID: 2330
		private readonly IEdmTypeReference expectedEdmTypeReference;
	}
}
