using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001BA RID: 442
	public sealed class EndPathToken : PathToken
	{
		// Token: 0x06001496 RID: 5270 RVA: 0x0003BD26 File Offset: 0x00039F26
		public EndPathToken(string identifier, QueryToken nextToken)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(identifier, "identifier");
			this.identifier = identifier;
			this.nextToken = nextToken;
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001497 RID: 5271 RVA: 0x0003884B File Offset: 0x00036A4B
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.EndPath;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001498 RID: 5272 RVA: 0x0003BD47 File Offset: 0x00039F47
		// (set) Token: 0x06001499 RID: 5273 RVA: 0x0003BD4F File Offset: 0x00039F4F
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

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x0600149A RID: 5274 RVA: 0x0003BD58 File Offset: 0x00039F58
		public override string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x0003BD60 File Offset: 0x00039F60
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000904 RID: 2308
		private readonly string identifier;

		// Token: 0x04000905 RID: 2309
		private QueryToken nextToken;
	}
}
