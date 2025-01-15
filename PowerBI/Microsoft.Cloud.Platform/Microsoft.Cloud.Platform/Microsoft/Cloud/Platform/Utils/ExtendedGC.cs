using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001DD RID: 477
	public static class ExtendedGC
	{
		// Token: 0x06000C7B RID: 3195 RVA: 0x0002BC7C File Offset: 0x00029E7C
		public static void CollectEverything()
		{
			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();
		}
	}
}
