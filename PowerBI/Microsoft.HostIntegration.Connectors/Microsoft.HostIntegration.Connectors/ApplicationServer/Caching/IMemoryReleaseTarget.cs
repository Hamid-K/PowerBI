using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002D9 RID: 729
	internal interface IMemoryReleaseTarget
	{
		// Token: 0x06001AFA RID: 6906
		void PoolMemoryReleased(long internalReleasedMemory);

		// Token: 0x06001AFB RID: 6907
		bool UpdateStatsAndCheckIfTargetMet();
	}
}
