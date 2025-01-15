using System;
using Microsoft.Data.OData;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000010 RID: 16
	public abstract class QueryNode : ODataAnnotatable
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000044 RID: 68
		public abstract QueryNodeKind Kind { get; }
	}
}
