using System;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x0200011C RID: 284
	public sealed class DottedIdentifierToken : PathToken
	{
		// Token: 0x06000BDE RID: 3038 RVA: 0x0002CA4B File Offset: 0x0002AC4B
		public DottedIdentifierToken(string identifier, QueryToken nextToken)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(identifier, "Identifier");
			this.identifier = identifier;
			this.nextToken = nextToken;
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x0002CA6C File Offset: 0x0002AC6C
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.DottedIdentifier;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x0002CA70 File Offset: 0x0002AC70
		public override string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x0002CA78 File Offset: 0x0002AC78
		// (set) Token: 0x06000BE2 RID: 3042 RVA: 0x0002CA80 File Offset: 0x0002AC80
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

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0002CA89 File Offset: 0x0002AC89
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0400065A RID: 1626
		private readonly string identifier;

		// Token: 0x0400065B RID: 1627
		private QueryToken nextToken;
	}
}
