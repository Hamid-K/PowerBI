using System;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x02000138 RID: 312
	public sealed class StarToken : PathToken
	{
		// Token: 0x06000CB3 RID: 3251 RVA: 0x0002D3A3 File Offset: 0x0002B5A3
		public StarToken(QueryToken nextToken)
		{
			this.nextToken = nextToken;
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x0002D3B2 File Offset: 0x0002B5B2
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Star;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x0002D3B6 File Offset: 0x0002B5B6
		// (set) Token: 0x06000CB6 RID: 3254 RVA: 0x0002D3BE File Offset: 0x0002B5BE
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

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x0002D3C7 File Offset: 0x0002B5C7
		public override string Identifier
		{
			get
			{
				return "*";
			}
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x0002D3CE File Offset: 0x0002B5CE
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040006AA RID: 1706
		private QueryToken nextToken;
	}
}
