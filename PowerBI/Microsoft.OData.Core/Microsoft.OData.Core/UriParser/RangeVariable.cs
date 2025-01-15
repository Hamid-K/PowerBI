using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001A0 RID: 416
	public abstract class RangeVariable
	{
		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001404 RID: 5124
		public abstract string Name { get; }

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001405 RID: 5125
		public abstract IEdmTypeReference TypeReference { get; }

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001406 RID: 5126
		public abstract int Kind { get; }
	}
}
