using System;

namespace Microsoft.Data.Mashup
{
	// Token: 0x0200002C RID: 44
	public enum MashupDiscoveryKind
	{
		// Token: 0x0400013E RID: 318
		DataSource,
		// Token: 0x0400013F RID: 319
		UnknownNativeQuery,
		// Token: 0x04000140 RID: 320
		UnknownFunction,
		// Token: 0x04000141 RID: 321
		UnknownCallSite,
		// Token: 0x04000142 RID: 322
		Unsupported,
		// Token: 0x04000143 RID: 323
		ResolvedDependency,
		// Token: 0x04000144 RID: 324
		UnresolvedDependency
	}
}
