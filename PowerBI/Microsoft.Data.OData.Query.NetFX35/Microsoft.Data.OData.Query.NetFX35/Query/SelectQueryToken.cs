using System;
using System.Collections.Generic;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000030 RID: 48
	public sealed class SelectQueryToken : QueryToken
	{
		// Token: 0x06000107 RID: 263 RVA: 0x00005F1F File Offset: 0x0000411F
		public SelectQueryToken(IEnumerable<QueryToken> properties)
		{
			this.properties = new ReadOnlyEnumerable<QueryToken>(properties ?? ((IEnumerable<QueryToken>)QueryToken.EmptyTokens));
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00005F41 File Offset: 0x00004141
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Select;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00005F45 File Offset: 0x00004145
		public IEnumerable<QueryToken> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04000159 RID: 345
		private readonly IEnumerable<QueryToken> properties;
	}
}
