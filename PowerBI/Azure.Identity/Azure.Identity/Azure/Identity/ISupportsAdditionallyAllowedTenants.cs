using System;
using System.Collections.Generic;

namespace Azure.Identity
{
	// Token: 0x02000048 RID: 72
	internal interface ISupportsAdditionallyAllowedTenants
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000282 RID: 642
		IList<string> AdditionallyAllowedTenants { get; }
	}
}
