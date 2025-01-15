using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000176 RID: 374
	public sealed class LiteralToken : QueryToken
	{
		// Token: 0x06000FA2 RID: 4002 RVA: 0x0002C0C6 File Offset: 0x0002A2C6
		public LiteralToken(object value)
		{
			this.value = value;
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x0002C0D5 File Offset: 0x0002A2D5
		internal LiteralToken(object value, string originalText)
			: this(value)
		{
			this.originalText = originalText;
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x0002C0E5 File Offset: 0x0002A2E5
		internal LiteralToken(object value, string originalText, IEdmTypeReference expectedEdmTypeReference)
			: this(value, originalText)
		{
			this.expectedEdmTypeReference = expectedEdmTypeReference;
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x0002BA78 File Offset: 0x00029C78
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Literal;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000FA6 RID: 4006 RVA: 0x0002C0F6 File Offset: 0x0002A2F6
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x0002C0FE File Offset: 0x0002A2FE
		internal string OriginalText
		{
			get
			{
				return this.originalText;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000FA8 RID: 4008 RVA: 0x0002C106 File Offset: 0x0002A306
		internal IEdmTypeReference ExpectedEdmTypeReference
		{
			get
			{
				return this.expectedEdmTypeReference;
			}
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x0002C10E File Offset: 0x0002A30E
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040007D5 RID: 2005
		private readonly string originalText;

		// Token: 0x040007D6 RID: 2006
		private readonly object value;

		// Token: 0x040007D7 RID: 2007
		private readonly IEdmTypeReference expectedEdmTypeReference;
	}
}
