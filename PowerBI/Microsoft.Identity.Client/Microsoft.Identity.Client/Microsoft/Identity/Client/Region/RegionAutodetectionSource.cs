using System;

namespace Microsoft.Identity.Client.Region
{
	// Token: 0x02000267 RID: 615
	internal enum RegionAutodetectionSource
	{
		// Token: 0x04000AF5 RID: 2805
		None,
		// Token: 0x04000AF6 RID: 2806
		FailedAutoDiscovery,
		// Token: 0x04000AF7 RID: 2807
		Cache,
		// Token: 0x04000AF8 RID: 2808
		EnvVariable,
		// Token: 0x04000AF9 RID: 2809
		Imds
	}
}
