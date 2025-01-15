using System;
using System.Threading;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000860 RID: 2144
	public class EventSemaphore
	{
		// Token: 0x1700102E RID: 4142
		// (get) Token: 0x0600443E RID: 17470 RVA: 0x000E553A File Offset: 0x000E373A
		protected internal bool Signaled
		{
			get
			{
				return this.m_signaled;
			}
		}

		// Token: 0x0600443F RID: 17471 RVA: 0x000E5542 File Offset: 0x000E3742
		public EventSemaphore()
			: this(false)
		{
		}

		// Token: 0x06004440 RID: 17472 RVA: 0x000E554B File Offset: 0x000E374B
		public EventSemaphore(bool inSignaled)
		{
			this.m_signaled = false;
			this.m_timedout = false;
			this.m_timeout = 0;
			this.m_signaled = inSignaled;
		}

		// Token: 0x06004441 RID: 17473 RVA: 0x000E5570 File Offset: 0x000E3770
		public virtual void Reset()
		{
			lock (this)
			{
				this.m_signaled = false;
			}
		}

		// Token: 0x06004442 RID: 17474 RVA: 0x000E55AC File Offset: 0x000E37AC
		public virtual void Signal()
		{
			lock (this)
			{
				try
				{
					this.m_signaled = true;
					Monitor.Pulse(this);
				}
				catch (SynchronizationLockException)
				{
				}
			}
		}

		// Token: 0x06004443 RID: 17475 RVA: 0x000E55F8 File Offset: 0x000E37F8
		public virtual void WaitSignaled()
		{
			lock (this)
			{
				try
				{
					while (!this.m_signaled)
					{
						Monitor.Wait(this);
					}
				}
				catch (ThreadInterruptedException)
				{
				}
			}
		}

		// Token: 0x06004444 RID: 17476 RVA: 0x000E5648 File Offset: 0x000E3848
		public virtual bool WaitSignaled(int in_Timeout)
		{
			lock (this)
			{
				try
				{
					if (in_Timeout <= 0)
					{
						while (!this.m_signaled)
						{
							Monitor.Wait(this);
						}
					}
					else
					{
						long ticks = DateTime.Now.Ticks;
						long num = (long)in_Timeout;
						while (!this.m_signaled && num > 0L)
						{
							Monitor.Wait(this, new TimeSpan(num));
							num = DateTime.Now.Ticks;
						}
					}
					return this.m_signaled;
				}
				catch (ThreadInterruptedException)
				{
				}
			}
			return false;
		}

		// Token: 0x04002FD0 RID: 12240
		private bool m_signaled;

		// Token: 0x04002FD1 RID: 12241
		protected internal bool m_timedout;

		// Token: 0x04002FD2 RID: 12242
		protected internal int m_timeout;
	}
}
