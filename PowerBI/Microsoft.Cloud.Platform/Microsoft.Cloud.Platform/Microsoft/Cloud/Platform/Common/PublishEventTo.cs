using System;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x02000529 RID: 1321
	[Flags]
	public enum PublishEventTo
	{
		// Token: 0x04000E77 RID: 3703
		None = 0,
		// Token: 0x04000E78 RID: 3704
		PublishToEventingServer = 1,
		// Token: 0x04000E79 RID: 3705
		PublishToEtw = 2,
		// Token: 0x04000E7A RID: 3706
		All = 3
	}
}
