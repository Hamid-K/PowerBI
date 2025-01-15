using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001B9 RID: 441
	public sealed class DottedIdentifierToken : PathToken
	{
		// Token: 0x06001490 RID: 5264 RVA: 0x0003BCE3 File Offset: 0x00039EE3
		public DottedIdentifierToken(string identifier, QueryToken nextToken)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(identifier, "Identifier");
			this.identifier = identifier;
			this.nextToken = nextToken;
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x0003B599 File Offset: 0x00039799
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.DottedIdentifier;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001492 RID: 5266 RVA: 0x0003BD04 File Offset: 0x00039F04
		public override string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001493 RID: 5267 RVA: 0x0003BD0C File Offset: 0x00039F0C
		// (set) Token: 0x06001494 RID: 5268 RVA: 0x0003BD14 File Offset: 0x00039F14
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

		// Token: 0x06001495 RID: 5269 RVA: 0x0003BD1D File Offset: 0x00039F1D
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000902 RID: 2306
		private readonly string identifier;

		// Token: 0x04000903 RID: 2307
		private QueryToken nextToken;
	}
}
