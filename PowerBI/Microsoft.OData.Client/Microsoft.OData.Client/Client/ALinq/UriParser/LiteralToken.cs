using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x0200012B RID: 299
	public sealed class LiteralToken : QueryToken
	{
		// Token: 0x06000C62 RID: 3170 RVA: 0x0002CF08 File Offset: 0x0002B108
		public LiteralToken(object value)
		{
			this.value = value;
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x0002CF17 File Offset: 0x0002B117
		internal LiteralToken(object value, string originalText)
			: this(value)
		{
			this.originalText = originalText;
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0002CF27 File Offset: 0x0002B127
		internal LiteralToken(object value, string originalText, IEdmTypeReference expectedEdmTypeReference)
			: this(value, originalText)
		{
			this.expectedEdmTypeReference = expectedEdmTypeReference;
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000C65 RID: 3173 RVA: 0x0002CF38 File Offset: 0x0002B138
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Literal;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x0002CF3B File Offset: 0x0002B13B
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x0002CF43 File Offset: 0x0002B143
		internal string OriginalText
		{
			get
			{
				return this.originalText;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x0002CF4B File Offset: 0x0002B14B
		internal IEdmTypeReference ExpectedEdmTypeReference
		{
			get
			{
				return this.expectedEdmTypeReference;
			}
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x0002CF53 File Offset: 0x0002B153
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000675 RID: 1653
		private readonly string originalText;

		// Token: 0x04000676 RID: 1654
		private readonly object value;

		// Token: 0x04000677 RID: 1655
		private readonly IEdmTypeReference expectedEdmTypeReference;
	}
}
