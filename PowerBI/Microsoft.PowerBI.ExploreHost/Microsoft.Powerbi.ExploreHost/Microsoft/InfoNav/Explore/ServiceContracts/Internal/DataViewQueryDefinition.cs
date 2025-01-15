using System;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Explore.ServiceContracts.Internal
{
	// Token: 0x02000005 RID: 5
	public sealed class DataViewQueryDefinition
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002067 File Offset: 0x00000267
		public DataViewQueryDefinition(QueryDefinition queryDefinition)
		{
			this.Query = queryDefinition;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002076 File Offset: 0x00000276
		public QueryDefinition Query { get; }
	}
}
