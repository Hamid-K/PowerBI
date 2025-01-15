using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003E8 RID: 1000
	internal class OperationContext : TimerObject, IAsyncOperation, IAbortableAsyncResult, IAsyncResult
	{
		// Token: 0x0600230F RID: 8975 RVA: 0x0006BC69 File Offset: 0x00069E69
		public OperationContext(AsyncCallback callback, object state)
			: this(callback, state, 30000L)
		{
		}

		// Token: 0x06002310 RID: 8976 RVA: 0x0006BC79 File Offset: 0x00069E79
		public OperationContext(AsyncCallback callback, object state, int timeoutInMilliseconds)
			: this(callback, state, (long)timeoutInMilliseconds)
		{
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x0006BC85 File Offset: 0x00069E85
		public OperationContext(AsyncCallback callback, object state, long timeoutInmilliSeconds)
			: this(callback, state, new TimeSpan(timeoutInmilliSeconds * 10000L))
		{
		}

		// Token: 0x06002312 RID: 8978 RVA: 0x0006BC9C File Offset: 0x00069E9C
		public OperationContext(AsyncCallback callback, object state, TimeSpan timeout)
			: this(callback, state, FileTime.FromTimeSpan(timeout))
		{
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x0006BCAC File Offset: 0x00069EAC
		public OperationContext(AsyncCallback callback, object state, FileTime timeoutAt)
			: base(timeoutAt)
		{
			this.Initialize(callback, state);
		}

		// Token: 0x06002314 RID: 8980 RVA: 0x0006BCBD File Offset: 0x00069EBD
		public void Reinitialize(AsyncCallback callback, object state, TimeSpan timeout)
		{
			base.ExpirationTime = FileTime.FromTimeSpan(timeout);
			this.Initialize(callback, state);
		}

		// Token: 0x06002315 RID: 8981 RVA: 0x0006BCD4 File Offset: 0x00069ED4
		private void Initialize(AsyncCallback callback, object state)
		{
			OperationContextState operationContextState = state as OperationContextState;
			if (operationContextState != null)
			{
				this.m_state = operationContextState.CallbackState;
				this.m_contextState = operationContextState.GetContextState();
			}
			else
			{
				this.m_state = state;
				this.m_contextState = null;
			}
			this.m_callback = callback;
			this.m_opState = 0;
			this.m_completedSynchronously = 0;
			this.m_manualResetEvent = null;
			this.m_exception = null;
			this.m_secondaryContexts = null;
			this.m_operationPriority = Priority.GetPriority(Thread.CurrentThread);
			this.m_creatingThreadId = Thread.CurrentThread.ManagedThreadId;
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06002316 RID: 8982 RVA: 0x0006BD5D File Offset: 0x00069F5D
		public int CreatingThreadId
		{
			get
			{
				return this.m_creatingThreadId;
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06002317 RID: 8983 RVA: 0x0006BD65 File Offset: 0x00069F65
		public bool IsCompleted
		{
			get
			{
				return (this.m_opState & 2) != 0;
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06002318 RID: 8984 RVA: 0x0006BD75 File Offset: 0x00069F75
		// (set) Token: 0x06002319 RID: 8985 RVA: 0x0006BD7D File Offset: 0x00069F7D
		public Priority OperationPriority
		{
			get
			{
				return this.m_operationPriority;
			}
			set
			{
				this.m_operationPriority = value;
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x0600231A RID: 8986 RVA: 0x0006BD88 File Offset: 0x00069F88
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				ManualResetEvent manualResetEvent = this.m_manualResetEvent;
				if (manualResetEvent == null)
				{
					ManualResetEvent manualResetEvent2 = new ManualResetEvent(false);
					manualResetEvent = Interlocked.CompareExchange<ManualResetEvent>(ref this.m_manualResetEvent, manualResetEvent2, null);
					if (manualResetEvent == null)
					{
						manualResetEvent = manualResetEvent2;
					}
				}
				return manualResetEvent;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x0600231B RID: 8987 RVA: 0x0006BDBA File Offset: 0x00069FBA
		// (set) Token: 0x0600231C RID: 8988 RVA: 0x0006BDC2 File Offset: 0x00069FC2
		public object AsyncState
		{
			get
			{
				return this.m_state;
			}
			protected set
			{
				this.m_state = value;
			}
		}

		// Token: 0x0600231D RID: 8989 RVA: 0x0006BDCB File Offset: 0x00069FCB
		public object[] GetContextState()
		{
			return this.m_contextState;
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x0600231E RID: 8990 RVA: 0x0006BDD3 File Offset: 0x00069FD3
		// (set) Token: 0x0600231F RID: 8991 RVA: 0x0006BDDE File Offset: 0x00069FDE
		public bool CompletedSynchronously
		{
			get
			{
				return this.m_completedSynchronously > 0;
			}
			set
			{
				if (this.m_completedSynchronously >= 0)
				{
					this.m_completedSynchronously = (value ? 1 : (-1));
				}
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06002320 RID: 8992 RVA: 0x0006BDF6 File Offset: 0x00069FF6
		// (set) Token: 0x06002321 RID: 8993 RVA: 0x0006BDFE File Offset: 0x00069FFE
		public AsyncCallback Callback
		{
			get
			{
				return this.m_callback;
			}
			protected set
			{
				this.m_callback = value;
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06002322 RID: 8994 RVA: 0x0006BE07 File Offset: 0x0006A007
		protected bool IsOperationComplete
		{
			get
			{
				return (this.m_opState & 192) != 0;
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06002323 RID: 8995 RVA: 0x0006BE1B File Offset: 0x0006A01B
		public bool HasTimerStarted
		{
			get
			{
				return (this.m_opState & 16) != 0;
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06002324 RID: 8996 RVA: 0x0006BE2C File Offset: 0x0006A02C
		public bool HasTimerExpired
		{
			get
			{
				return (this.m_opState & 64) != 0;
			}
		}

		// Token: 0x06002325 RID: 8997 RVA: 0x0006BE40 File Offset: 0x0006A040
		protected void OperationCompleted(bool completedSynchronously, Exception exception)
		{
			this.CompletedSynchronously = completedSynchronously;
			ReleaseAssert.IsTrue(!this.CompletedSynchronously || (this.m_opState & 192) != 0 || this.m_manualResetEvent == null);
			int num = this.m_opState;
			for (;;)
			{
				int num2 = num;
				if ((num & 192) != 0)
				{
					break;
				}
				int num3 = (num & 119) | 128;
				num = Interlocked.CompareExchange(ref this.m_opState, num3, num2);
				if (num == num2)
				{
					goto Block_4;
				}
			}
			return;
			Block_4:
			if ((num & 16) != 0)
			{
				this.StopTimer();
			}
			ReleaseAssert.IsTrue((this.m_opState & 192) != 0);
			this.m_exception = exception;
			this.OnOperationComplete(this.CompletedSynchronously);
		}

		// Token: 0x06002326 RID: 8998 RVA: 0x0006BEE2 File Offset: 0x0006A0E2
		public void CompleteOperation(bool completedSynchronously, Exception exception)
		{
			this.OperationCompleted(completedSynchronously, exception);
		}

		// Token: 0x06002327 RID: 8999 RVA: 0x0006BEEC File Offset: 0x0006A0EC
		public OperationContext CreateSecondaryContext(AsyncCallback callback, object state, TimeSpan timeout)
		{
			SecondaryContext secondaryContext = new SecondaryContext(this, callback, state, timeout);
			int num = this.m_opState;
			int num2;
			bool flag;
			do
			{
				num2 = num;
				flag = (num & 2) != 0;
				if (!flag)
				{
					if ((num & 1) != 0)
					{
						break;
					}
					int num3 = num | 1;
					num = Interlocked.CompareExchange(ref this.m_opState, num3, num2);
				}
			}
			while (num != num2);
			if (!flag)
			{
				lock (this)
				{
					flag = this.IsCompleted;
					if (!flag)
					{
						if (this.m_secondaryContexts == null)
						{
							this.m_secondaryContexts = new List<SecondaryContext>();
						}
						this.m_secondaryContexts.Add(secondaryContext);
					}
				}
			}
			if (flag)
			{
				secondaryContext.CompleteOperation(true, this.m_exception);
			}
			return secondaryContext;
		}

		// Token: 0x06002328 RID: 9000 RVA: 0x0006BFA4 File Offset: 0x0006A1A4
		public void StartTimer()
		{
			if (base.ExpirationTime != FileTime.MaxValue)
			{
				int num = this.m_opState;
				for (;;)
				{
					int num2 = num;
					ReleaseAssert.IsTrue((num & 16) == 0);
					if ((num & 192) != 0)
					{
						break;
					}
					int num3 = (num & -33) | 16;
					num = Interlocked.CompareExchange(ref this.m_opState, num3, num2);
					if (num == num2)
					{
						goto Block_3;
					}
				}
				return;
				Block_3:
				NormalPriorityTimerQueue.Singleton.Enqueue(this);
				num = this.m_opState;
				if ((num & 128) != 0 && (num & 16) != 0)
				{
					this.StopTimer();
				}
			}
		}

		// Token: 0x06002329 RID: 9001 RVA: 0x0006C024 File Offset: 0x0006A224
		protected bool StopTimer()
		{
			bool flag = (this.m_opState & 16) != 0;
			if (flag)
			{
				flag = NormalPriorityTimerQueue.Singleton.Dequeue(this);
				if (flag)
				{
					int num = this.m_opState;
					int num2;
					do
					{
						num2 = num;
						ReleaseAssert.IsTrue((num & 16) != 0);
						int num3 = num & -17;
						num = Interlocked.CompareExchange(ref this.m_opState, num3, num2);
					}
					while (num != num2);
				}
			}
			return flag;
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x0006C084 File Offset: 0x0006A284
		protected virtual void OnTimerExpired()
		{
			int num = this.m_opState;
			for (;;)
			{
				int num2 = num;
				if ((num & 128) != 0)
				{
					break;
				}
				ReleaseAssert.IsTrue((num & 32) != 0);
				int num3 = (num & 119) | 64;
				num = Interlocked.CompareExchange(ref this.m_opState, num3, num2);
				if (num == num2)
				{
					goto Block_2;
				}
			}
			return;
			Block_2:
			if (this.m_exception == null)
			{
				this.m_exception = new TimeoutException();
			}
			this.OnOperationComplete(false);
		}

		// Token: 0x0600232B RID: 9003 RVA: 0x0006C0E8 File Offset: 0x0006A2E8
		protected sealed override void OnTimerElapsed()
		{
			int num = this.m_opState;
			for (;;)
			{
				int num2 = num;
				if ((num & 128) != 0)
				{
					break;
				}
				ReleaseAssert.IsTrue((num & 64) == 0 && (num & 16) != 0);
				int num3 = (num & -17) | 32;
				num = Interlocked.CompareExchange(ref this.m_opState, num3, num2);
				if (num == num2)
				{
					goto Block_3;
				}
			}
			return;
			Block_3:
			this.OnTimerExpired();
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x0600232C RID: 9004 RVA: 0x0006C140 File Offset: 0x0006A340
		// (set) Token: 0x0600232D RID: 9005 RVA: 0x0006C148 File Offset: 0x0006A348
		protected Exception Fault
		{
			get
			{
				return this.m_exception;
			}
			set
			{
				this.m_exception = value;
			}
		}

		// Token: 0x0600232E RID: 9006 RVA: 0x0006C154 File Offset: 0x0006A354
		public void WaitOne()
		{
			int num = this.m_opState;
			for (;;)
			{
				int num2 = num;
				if ((num & 2) != 0)
				{
					break;
				}
				if ((num & 4) != 0)
				{
					goto IL_002A;
				}
				int num3 = num | 4;
				num = Interlocked.CompareExchange(ref this.m_opState, num3, num2);
				if (num == num2)
				{
					goto IL_002A;
				}
			}
			return;
			IL_002A:
			lock (this)
			{
				if (!this.IsCompleted)
				{
					Monitor.Wait(this);
				}
			}
			ReleaseAssert.IsTrue(this.IsCompleted);
		}

		// Token: 0x0600232F RID: 9007 RVA: 0x0006C1C8 File Offset: 0x0006A3C8
		protected virtual void OnOperationComplete(bool completedSynchronously)
		{
			ReleaseAssert.IsTrue(completedSynchronously == this.CompletedSynchronously);
			this.EndOperation();
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x0006C1DE File Offset: 0x0006A3DE
		public virtual void Abort(Exception exception)
		{
			this.CompleteOperation(false, exception);
		}

		// Token: 0x06002331 RID: 9009 RVA: 0x0006C1E8 File Offset: 0x0006A3E8
		private void EndOperation()
		{
			ReleaseAssert.IsTrue((this.m_opState & 192) != 0);
			int num = this.m_opState;
			int num2;
			do
			{
				num2 = num;
				int num3 = num | 2;
				num = Interlocked.CompareExchange(ref this.m_opState, num3, num2);
			}
			while (num != num2);
			List<SecondaryContext> list = null;
			if ((num & 5) != 0)
			{
				lock (this)
				{
					if ((num & 4) != 0)
					{
						Monitor.PulseAll(this);
					}
					if ((num & 1) != 0)
					{
						list = this.m_secondaryContexts;
						this.m_secondaryContexts = null;
					}
				}
			}
			ManualResetEvent manualResetEvent = Interlocked.CompareExchange<ManualResetEvent>(ref this.m_manualResetEvent, OperationContext.SignaledSingletonManualResetEvent, null);
			if (manualResetEvent != null)
			{
				manualResetEvent.Set();
			}
			this.InvokeCallback(this.m_callback);
			if (list != null)
			{
				foreach (OperationContext operationContext in list)
				{
					operationContext.CompleteOperation(false, this.m_exception);
				}
			}
		}

		// Token: 0x06002332 RID: 9010 RVA: 0x0006C2E8 File Offset: 0x0006A4E8
		private void InvokeCallback(AsyncCallback callback)
		{
			ReleaseAssert.IsTrue(!this.CompletedSynchronously || Thread.CurrentThread.ManagedThreadId == this.m_creatingThreadId);
			Thread currentThread = Thread.CurrentThread;
			ThreadPriority priority = currentThread.Priority;
			try
			{
				Priority.SetPriority(currentThread, this.m_operationPriority, priority);
				OperationContext.InvokeCallback(callback, this);
			}
			finally
			{
				Priority.RevertPriority(currentThread, priority);
			}
		}

		// Token: 0x06002333 RID: 9011 RVA: 0x0006C354 File Offset: 0x0006A554
		private static void InvokeCallback(AsyncCallback callback, IAsyncResult result)
		{
			if (callback != null)
			{
				try
				{
					callback(result);
				}
				catch (Exception ex)
				{
					ReleaseAssert.Fail("Async callback {0} failed with exception {1}", new object[] { callback, ex });
				}
			}
		}

		// Token: 0x06002334 RID: 9012 RVA: 0x0006C39C File Offset: 0x0006A59C
		public void End()
		{
			ReleaseAssert.IsTrue(this.m_operationPriority == Priority.GetPriority(Thread.CurrentThread));
			this.WaitOne();
			if (this.m_exception != null)
			{
				throw new OperationCompletedException("Operation completed with an exception", this.m_exception);
			}
		}

		// Token: 0x06002335 RID: 9013 RVA: 0x0006C3D7 File Offset: 0x0006A5D7
		private static void ThrowException(object e)
		{
			throw new AsyncCallbackException("An invoked async callback raised an exception", (Exception)e);
		}

		// Token: 0x040015EB RID: 5611
		private const long DefaultOperationTimeout = 30000L;

		// Token: 0x040015EC RID: 5612
		private const int WaitFlags = 7;

		// Token: 0x040015ED RID: 5613
		private const int TimerFlags = 112;

		// Token: 0x040015EE RID: 5614
		private const int PreservedFlags = 119;

		// Token: 0x040015EF RID: 5615
		private const int CompletionFlags = 192;

		// Token: 0x040015F0 RID: 5616
		internal static ManualResetEvent SignaledSingletonManualResetEvent = new ManualResetEvent(true);

		// Token: 0x040015F1 RID: 5617
		internal static WaitCallback exceptionThrowingDelegate = new WaitCallback(OperationContext.ThrowException);

		// Token: 0x040015F2 RID: 5618
		private object m_state;

		// Token: 0x040015F3 RID: 5619
		private object[] m_contextState;

		// Token: 0x040015F4 RID: 5620
		private AsyncCallback m_callback;

		// Token: 0x040015F5 RID: 5621
		private int m_opState;

		// Token: 0x040015F6 RID: 5622
		private int m_completedSynchronously;

		// Token: 0x040015F7 RID: 5623
		private ManualResetEvent m_manualResetEvent;

		// Token: 0x040015F8 RID: 5624
		private Exception m_exception;

		// Token: 0x040015F9 RID: 5625
		private List<SecondaryContext> m_secondaryContexts;

		// Token: 0x040015FA RID: 5626
		private Priority m_operationPriority;

		// Token: 0x040015FB RID: 5627
		private int m_creatingThreadId;

		// Token: 0x020003E9 RID: 1001
		[Flags]
		private enum OperationState
		{
			// Token: 0x040015FD RID: 5629
			SecondariesPresent = 1,
			// Token: 0x040015FE RID: 5630
			Ended = 2,
			// Token: 0x040015FF RID: 5631
			Waiting = 4,
			// Token: 0x04001600 RID: 5632
			TimerStarted = 16,
			// Token: 0x04001601 RID: 5633
			TimerElapsed = 32,
			// Token: 0x04001602 RID: 5634
			TimerExpired = 64,
			// Token: 0x04001603 RID: 5635
			Completed = 128
		}
	}
}
