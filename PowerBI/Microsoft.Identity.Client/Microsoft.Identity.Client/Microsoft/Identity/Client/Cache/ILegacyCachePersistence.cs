using System;

namespace Microsoft.Identity.Client.Cache
{
	// Token: 0x020002AB RID: 683
	internal interface ILegacyCachePersistence
	{
		// Token: 0x060019B0 RID: 6576
		byte[] LoadCache();

		// Token: 0x060019B1 RID: 6577
		void WriteCache(byte[] serializedCache);
	}
}
