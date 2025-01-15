using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200016D RID: 365
	public sealed class DottedIdentifierToken : PathToken
	{
		// Token: 0x06000F61 RID: 3937 RVA: 0x0002BCE3 File Offset: 0x00029EE3
		public DottedIdentifierToken(string identifier, QueryToken nextToken)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(identifier, "Identifier");
			this.identifier = identifier;
			this.nextToken = nextToken;
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000F62 RID: 3938 RVA: 0x0002BD04 File Offset: 0x00029F04
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.DottedIdentifier;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000F63 RID: 3939 RVA: 0x0002BD08 File Offset: 0x00029F08
		public override string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000F64 RID: 3940 RVA: 0x0002BD10 File Offset: 0x00029F10
		// (set) Token: 0x06000F65 RID: 3941 RVA: 0x0002BD18 File Offset: 0x00029F18
		public override QueryToken NextToken
		{
			get
			{
				return this.nextToken;
			}
			set
			{
				this.nextToken = value;
			}
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0002BD21 File Offset: 0x00029F21
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040007B7 RID: 1975
		private readonly string identifier;

		// Token: 0x040007B8 RID: 1976
		private QueryToken nextToken;
	}
}
