using System;
using System.Threading;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x020000A3 RID: 163
	internal sealed class ManagedSemaphore
	{
		// Token: 0x0600050D RID: 1293 RVA: 0x0000FDF0 File Offset: 0x0000DFF0
		public ManagedSemaphore(int count, int available)
		{
			if (available > count)
			{
				throw new ArgumentException();
			}
			this.m_available = available;
			this.m_count = count;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0000FE28 File Offset: 0x0000E028
		public bool Acquire(int timeoutMs)
		{
			if (this.SingleAttemptAcquire())
			{
				return true;
			}
			bool flag2;
			for (;;)
			{
				object queue = this.m_queue;
				lock (queue)
				{
					if (this.SingleAttemptAcquire())
					{
						flag2 = true;
					}
					else
					{
						if (Monitor.Wait(this.m_queue, timeoutMs))
						{
							continue;
						}
						flag2 = false;
					}
				}
				break;
			}
			return flag2;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000FE8C File Offset: 0x0000E08C
		public void Release()
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				this.m_available = Math.Min(this.m_count, this.m_available + 1);
				object queue = this.m_queue;
				lock (queue)
				{
					Monitor.PulseAll(this.m_queue);
				}
			}
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000FF14 File Offset: 0x0000E114
		private bool SingleAttemptAcquire()
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				if (this.m_available > 0)
				{
					this.m_available--;
					return true;
				}
			}
			return false;
		}

		// Token: 0x040002ED RID: 749
		private readonly int m_count;

		// Token: 0x040002EE RID: 750
		private int m_available;

		// Token: 0x040002EF RID: 751
		private readonly object m_lock = new object();

		// Token: 0x040002F0 RID: 752
		private readonly object m_queue = new object();
	}
}
