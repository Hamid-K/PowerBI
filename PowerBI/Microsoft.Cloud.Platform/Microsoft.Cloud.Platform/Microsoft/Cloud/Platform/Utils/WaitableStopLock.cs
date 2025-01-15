using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002A6 RID: 678
	internal sealed class WaitableStopLock
	{
		// Token: 0x06001262 RID: 4706 RVA: 0x000402FC File Offset: 0x0003E4FC
		public WaitableStopLock()
		{
			this.m_stopLock = new StopLock(new Action(this.OnStopCompleted));
			this.m_lock = new object();
			this.m_notificationHandles = null;
			this.m_stopCompleted = false;
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x00040334 File Offset: 0x0003E534
		public bool TryEnter()
		{
			return this.m_stopLock.TryEnter();
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x00040341 File Offset: 0x0003E541
		public void Leave()
		{
			this.m_stopLock.Leave();
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x00040350 File Offset: 0x0003E550
		public void Stop(EventWaitHandle notificationHandle)
		{
			this.m_stopLock.Stop();
			if (notificationHandle == null)
			{
				return;
			}
			bool flag = false;
			object @lock = this.m_lock;
			lock (@lock)
			{
				flag = this.m_stopCompleted;
				if (!flag)
				{
					if (this.m_notificationHandles == null)
					{
						this.m_notificationHandles = new List<EventWaitHandle>();
					}
					this.m_notificationHandles.Add(notificationHandle);
				}
			}
			if (flag)
			{
				notificationHandle.Set();
			}
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x000403D0 File Offset: 0x0003E5D0
		private void OnStopCompleted()
		{
			List<EventWaitHandle> list = null;
			object @lock = this.m_lock;
			lock (@lock)
			{
				this.m_stopCompleted = true;
				list = this.m_notificationHandles;
				this.m_notificationHandles = null;
			}
			if (list != null)
			{
				foreach (EventWaitHandle eventWaitHandle in list)
				{
					eventWaitHandle.Set();
				}
			}
		}

		// Token: 0x040006D5 RID: 1749
		private StopLock m_stopLock;

		// Token: 0x040006D6 RID: 1750
		private object m_lock;

		// Token: 0x040006D7 RID: 1751
		private List<EventWaitHandle> m_notificationHandles;

		// Token: 0x040006D8 RID: 1752
		private bool m_stopCompleted;
	}
}
