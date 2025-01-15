using System;
using Microsoft.Data.OData;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000009 RID: 9
	public abstract class QueryToken : ODataAnnotatable
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000021 RID: 33
		public abstract QueryTokenKind Kind { get; }

		// Token: 0x04000028 RID: 40
		public static readonly QueryToken[] EmptyTokens = new QueryToken[0];
	}
}
