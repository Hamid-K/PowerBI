using System;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000032 RID: 50
	public sealed class StarQueryToken : QueryToken
	{
		// Token: 0x06000113 RID: 275 RVA: 0x00005F94 File Offset: 0x00004194
		public StarQueryToken(QueryToken parent)
		{
			this.parent = parent;
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00005FA3 File Offset: 0x000041A3
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Star;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00005FA7 File Offset: 0x000041A7
		public QueryToken Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x0400015D RID: 349
		private readonly QueryToken parent;
	}
}
