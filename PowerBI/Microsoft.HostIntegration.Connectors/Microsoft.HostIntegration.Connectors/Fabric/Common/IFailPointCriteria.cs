using System;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003D5 RID: 981
	internal interface IFailPointCriteria
	{
		// Token: 0x06002286 RID: 8838
		bool Match(FailPointContext context);
	}
}
