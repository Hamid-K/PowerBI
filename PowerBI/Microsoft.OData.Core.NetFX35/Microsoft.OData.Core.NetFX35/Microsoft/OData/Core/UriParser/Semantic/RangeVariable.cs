using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000236 RID: 566
	public abstract class RangeVariable : ODataAnnotatable
	{
		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x0600145A RID: 5210
		public abstract string Name { get; }

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x0600145B RID: 5211
		public abstract IEdmTypeReference TypeReference { get; }

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x0600145C RID: 5212
		public abstract int Kind { get; }
	}
}
