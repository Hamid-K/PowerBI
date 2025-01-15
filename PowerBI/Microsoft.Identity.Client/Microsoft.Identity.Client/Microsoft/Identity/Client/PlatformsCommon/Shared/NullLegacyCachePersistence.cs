using System;
using Microsoft.Identity.Client.Cache;

namespace Microsoft.Identity.Client.PlatformsCommon.Shared
{
	// Token: 0x020001F7 RID: 503
	internal class NullLegacyCachePersistence : ILegacyCachePersistence
	{
		// Token: 0x06001583 RID: 5507 RVA: 0x00047C7D File Offset: 0x00045E7D
		public byte[] LoadCache()
		{
			return null;
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x00047C80 File Offset: 0x00045E80
		public void WriteCache(byte[] serializedCache)
		{
		}
	}
}
