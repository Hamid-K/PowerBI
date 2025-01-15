using System;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x0200011D RID: 285
	public sealed class EndPathToken : PathToken
	{
		// Token: 0x06000BE4 RID: 3044 RVA: 0x0002CA92 File Offset: 0x0002AC92
		public EndPathToken(string identifier, QueryToken nextToken)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(identifier, "identifier");
			this.identifier = identifier;
			this.nextToken = nextToken;
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x0002CAB3 File Offset: 0x0002ACB3
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.EndPath;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x0002CAB6 File Offset: 0x0002ACB6
		// (set) Token: 0x06000BE7 RID: 3047 RVA: 0x0002CABE File Offset: 0x0002ACBE
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

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x0002CAC7 File Offset: 0x0002ACC7
		public override string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0002CACF File Offset: 0x0002ACCF
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0400065C RID: 1628
		private readonly string identifier;

		// Token: 0x0400065D RID: 1629
		private QueryToken nextToken;
	}
}
