using System;
using Microsoft.Identity.Client.Cache;

namespace Microsoft.Identity.Client.PlatformsCommon.Shared
{
	// Token: 0x020001F2 RID: 498
	internal class InMemoryLegacyCachePersistance : ILegacyCachePersistence
	{
		// Token: 0x0600154D RID: 5453 RVA: 0x00046F6E File Offset: 0x0004516E
		public byte[] LoadCache()
		{
			return this.data;
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x00046F76 File Offset: 0x00045176
		public void WriteCache(byte[] serializedCache)
		{
			this.data = serializedCache;
		}

		// Token: 0x040008C8 RID: 2248
		private byte[] data;
	}
}
