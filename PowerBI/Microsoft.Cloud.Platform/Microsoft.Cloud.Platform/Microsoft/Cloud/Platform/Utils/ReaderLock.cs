using System;
using System.Threading;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002A7 RID: 679
	public struct ReaderLock : IDisposable
	{
		// Token: 0x06001267 RID: 4711 RVA: 0x00040460 File Offset: 0x0003E660
		public ReaderLock(ReaderWriterLockSlim locker)
		{
			this.m_lock = locker;
			this.m_lock.EnterReadLock();
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x00040474 File Offset: 0x0003E674
		public void Dispose()
		{
			if (this.m_lock != null)
			{
				this.m_lock.ExitReadLock();
				this.m_lock = null;
			}
		}

		// Token: 0x040006D9 RID: 1753
		private ReaderWriterLockSlim m_lock;
	}
}
