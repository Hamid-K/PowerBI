using System;
using System.Collections.Generic;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000022 RID: 34
	public sealed class ExpandQueryToken : QueryToken
	{
		// Token: 0x06000093 RID: 147 RVA: 0x00004890 File Offset: 0x00002A90
		public ExpandQueryToken(IEnumerable<QueryToken> properties)
		{
			this.properties = new ReadOnlyEnumerable<QueryToken>(properties ?? ((IEnumerable<QueryToken>)QueryToken.EmptyTokens));
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000094 RID: 148 RVA: 0x000048B2 File Offset: 0x00002AB2
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Expand;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000095 RID: 149 RVA: 0x000048B6 File Offset: 0x00002AB6
		public IEnumerable<QueryToken> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x040000FB RID: 251
		private readonly IEnumerable<QueryToken> properties;
	}
}
