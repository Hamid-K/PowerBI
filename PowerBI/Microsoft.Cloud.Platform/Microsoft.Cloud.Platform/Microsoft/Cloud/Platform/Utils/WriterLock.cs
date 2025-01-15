using System;
using System.Threading;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002A8 RID: 680
	public struct WriterLock : IDisposable
	{
		// Token: 0x06001269 RID: 4713 RVA: 0x00040490 File Offset: 0x0003E690
		public WriterLock(ReaderWriterLockSlim locker)
		{
			this.m_lock = locker;
			this.m_lock.EnterWriteLock();
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x000404A4 File Offset: 0x0003E6A4
		public void Dispose()
		{
			if (this.m_lock != null)
			{
				this.m_lock.ExitWriteLock();
				this.m_lock = null;
			}
		}

		// Token: 0x040006DA RID: 1754
		private ReaderWriterLockSlim m_lock;
	}
}
