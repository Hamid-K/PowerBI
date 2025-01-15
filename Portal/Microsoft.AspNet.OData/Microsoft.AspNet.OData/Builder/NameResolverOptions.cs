using System;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x0200011C RID: 284
	[Flags]
	public enum NameResolverOptions
	{
		// Token: 0x0400031E RID: 798
		ProcessReflectedPropertyNames = 1,
		// Token: 0x0400031F RID: 799
		ProcessDataMemberAttributePropertyNames = 2,
		// Token: 0x04000320 RID: 800
		ProcessExplicitPropertyNames = 4
	}
}
