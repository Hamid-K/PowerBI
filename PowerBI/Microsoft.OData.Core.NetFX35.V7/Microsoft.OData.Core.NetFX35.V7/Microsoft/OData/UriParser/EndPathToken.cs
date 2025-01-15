using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200016E RID: 366
	public sealed class EndPathToken : PathToken
	{
		// Token: 0x06000F67 RID: 3943 RVA: 0x0002BD2A File Offset: 0x00029F2A
		public EndPathToken(string identifier, QueryToken nextToken)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(identifier, "Identifier");
			this.identifier = identifier;
			this.nextToken = nextToken;
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000F68 RID: 3944 RVA: 0x00028F17 File Offset: 0x00027117
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.EndPath;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000F69 RID: 3945 RVA: 0x0002BD4B File Offset: 0x00029F4B
		// (set) Token: 0x06000F6A RID: 3946 RVA: 0x0002BD53 File Offset: 0x00029F53
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

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000F6B RID: 3947 RVA: 0x0002BD5C File Offset: 0x00029F5C
		public override string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x0002BD64 File Offset: 0x00029F64
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040007B9 RID: 1977
		private readonly string identifier;

		// Token: 0x040007BA RID: 1978
		private QueryToken nextToken;
	}
}
