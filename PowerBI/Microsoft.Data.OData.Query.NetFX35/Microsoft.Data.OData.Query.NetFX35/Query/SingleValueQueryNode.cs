using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000013 RID: 19
	public abstract class SingleValueQueryNode : QueryNode
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004F RID: 79
		public abstract IEdmTypeReference TypeReference { get; }
	}
}
