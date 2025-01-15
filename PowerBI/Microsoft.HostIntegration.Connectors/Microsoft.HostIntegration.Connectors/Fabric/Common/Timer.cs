using System;
using System.Threading;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000421 RID: 1057
	internal class Timer : TimerObject
	{
		// Token: 0x060024DC RID: 9436 RVA: 0x00070E60 File Offset: 0x0006F060
		public Timer(WaitCallback callback, object state)
			: this(callback, state, NormalPriorityTimerQueue.Singleton)
		{
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x00070E6F File Offset: 0x0006F06F
		public Timer(WaitCallback callback, object state, TimerQueue timerQueue)
			: base(FileTime.MaxValue)
		{
			this.m_callback = callback;
			this.m_state = state;
			this.m_timerQueue = timerQueue;
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x060024DE RID: 9438 RVA: 0x00070E91 File Offset: 0x0006F091
		// (set) Token: 0x060024DF RID: 9439 RVA: 0x00070E99 File Offset: 0x0006F099
		public object AsyncState
		{
			get
			{
				return this.m_state;
			}
			set
			{
				this.m_state = value;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x060024E0 RID: 9440 RVA: 0x00070EA2 File Offset: 0x0006F0A2
		public TimerQueue TimerQueue
		{
			get
			{
				return this.m_timerQueue;
			}
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x00070EAA File Offset: 0x0006F0AA
		public void Disable()
		{
			if (this.m_callback != null)
			{
				this.m_callback = null;
				this.Dequeue();
			}
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x00070EC4 File Offset: 0x0006F0C4
		public bool Enqueue(FileTime expirationTime)
		{
			if (this.m_callback == null)
			{
				return false;
			}
			ReleaseAssert.IsTrue(expirationTime != FileTime.MaxValue);
			base.ExpirationTime = expirationTime;
			this.m_timerQueue.Enqueue(this);
			if (this.m_callback == null)
			{
				this.Dequeue();
				return false;
			}
			return true;
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x00070F10 File Offset: 0x0006F110
		public bool Enqueue(TimeSpan timeout)
		{
			return this.Enqueue(FileTime.FromTimeSpan(timeout));
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x00070F20 File Offset: 0x0006F120
		public bool Dequeue()
		{
			return this.m_timerQueue.Dequeue(this);
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x00070F3C File Offset: 0x0006F13C
		protected override void OnTimerElapsed()
		{
			WaitCallback callback = this.m_callback;
			if (callback != null)
			{
				callback(this.m_state);
			}
		}

		// Token: 0x04001681 RID: 5761
		private WaitCallback m_callback;

		// Token: 0x04001682 RID: 5762
		private object m_state;

		// Token: 0x04001683 RID: 5763
		private TimerQueue m_timerQueue;
	}
}
