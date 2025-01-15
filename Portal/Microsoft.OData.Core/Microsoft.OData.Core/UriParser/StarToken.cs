using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001CC RID: 460
	public sealed class StarToken : PathToken
	{
		// Token: 0x0600151A RID: 5402 RVA: 0x0003C4D3 File Offset: 0x0003A6D3
		public StarToken(QueryToken nextToken)
		{
			this.nextToken = nextToken;
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x0600151B RID: 5403 RVA: 0x0003B72D File Offset: 0x0003992D
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Star;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x0600151C RID: 5404 RVA: 0x0003C4E2 File Offset: 0x0003A6E2
		// (set) Token: 0x0600151D RID: 5405 RVA: 0x0003C4EA File Offset: 0x0003A6EA
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

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x0600151E RID: 5406 RVA: 0x0003C4F3 File Offset: 0x0003A6F3
		public override string Identifier
		{
			get
			{
				return "*";
			}
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x0003C4FA File Offset: 0x0003A6FA
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0400092D RID: 2349
		private QueryToken nextToken;
	}
}
