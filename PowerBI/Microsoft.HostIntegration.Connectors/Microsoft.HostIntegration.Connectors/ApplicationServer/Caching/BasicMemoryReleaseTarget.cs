using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002DA RID: 730
	internal class BasicMemoryReleaseTarget : IMemoryReleaseTarget
	{
		// Token: 0x06001AFC RID: 6908 RVA: 0x00051CBD File Offset: 0x0004FEBD
		public BasicMemoryReleaseTarget(long totalMemoryToRelease)
		{
			this._memoryToBeReleased = totalMemoryToRelease;
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x00051CCC File Offset: 0x0004FECC
		public void PoolMemoryReleased(long internalReleasedMemory)
		{
			this._memoryToBeReleased -= internalReleasedMemory;
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x00051CDC File Offset: 0x0004FEDC
		public bool UpdateStatsAndCheckIfTargetMet()
		{
			return this._memoryToBeReleased <= 0L;
		}

		// Token: 0x04000E53 RID: 3667
		private long _memoryToBeReleased;
	}
}
