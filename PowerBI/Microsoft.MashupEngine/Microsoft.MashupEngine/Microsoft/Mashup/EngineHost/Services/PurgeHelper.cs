using System;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A42 RID: 6722
	public static class PurgeHelper
	{
		// Token: 0x0600AA05 RID: 43525 RVA: 0x00232218 File Offset: 0x00230418
		public static void PurgeIfNotUsed(string identity, Action purgeAction)
		{
			using (SharedExclusiveLock sharedExclusiveLock = new SharedExclusiveLock(identity))
			{
				if (sharedExclusiveLock.TryAcquireSharedLock(TimeSpan.Zero))
				{
					try
					{
						if (sharedExclusiveLock.TryAcquireExclusiveLock(TimeSpan.Zero))
						{
							try
							{
								purgeAction();
							}
							finally
							{
								sharedExclusiveLock.ReleaseExclusiveLock();
							}
						}
					}
					finally
					{
						sharedExclusiveLock.ReleaseSharedLock();
					}
				}
			}
		}
	}
}
