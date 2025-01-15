using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000055 RID: 85
	public enum MapTileCacheLevel
	{
		// Token: 0x0400012D RID: 301
		Default,
		// Token: 0x0400012E RID: 302
		BypassCache,
		// Token: 0x0400012F RID: 303
		CacheOnly,
		// Token: 0x04000130 RID: 304
		CacheIfAvailable,
		// Token: 0x04000131 RID: 305
		Revalidate,
		// Token: 0x04000132 RID: 306
		Reload,
		// Token: 0x04000133 RID: 307
		NoCacheNoStore
	}
}
