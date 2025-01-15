using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200027E RID: 638
	internal static class ObjectManagerFactory
	{
		// Token: 0x06001625 RID: 5669 RVA: 0x00044387 File Offset: 0x00042587
		public static IObjectManager GetObjectManager(OMCacheNodeProperties props)
		{
			return new ObjectManager(props);
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x0004438F File Offset: 0x0004258F
		public static IObjectManager GetObjectManager(OMCacheNodeProperties props, IMemoryManager memoryManager, ICacheStatsContainer statsContainer)
		{
			return new ObjectManager(props, memoryManager, statsContainer);
		}
	}
}
