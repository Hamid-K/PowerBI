using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000154 RID: 340
	public abstract class RangeVariable
	{
		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000EDF RID: 3807
		public abstract string Name { get; }

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000EE0 RID: 3808
		public abstract IEdmTypeReference TypeReference { get; }

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000EE1 RID: 3809
		public abstract int Kind { get; }
	}
}
