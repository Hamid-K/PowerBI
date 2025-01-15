using System;
using System.Threading;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000197 RID: 407
	internal sealed class AsyncPendingCallback<T> : PendingCallback, IAsyncResult
	{
		// Token: 0x06000A6F RID: 2671 RVA: 0x00023F08 File Offset: 0x00022108
		public AsyncPendingCallback(string identity, IWorkTicketFactory workTicketFactory, int timeoutInMilli, Action<IAsyncResult> preStartWatchdogDelegate, AsyncCallback callback, object state)
			: base(identity, workTicketFactory)
		{
			if (callback != null)
			{
				this.m_userCallback = callback;
				this.m_asyncResult = new AsyncResult<AsyncWatchdogResult<T>>(new AsyncCallback(this.InvokeUserCallback), state);
			}
			else
			{
				this.m_asyncResult = new AsyncResult<AsyncWatchdogResult<T>>(null, state);
			}
			if (preStartWatchdogDelegate != null)
			{
				preStartWatchdogDelegate(this);
				if (this.m_asyncResult.IsCompleted)
				{
					return;
				}
			}
			base.ScheduleTimer(timeoutInMilli);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00023F74 File Offset: 0x00022174
		private void InvokeUserCallback(IAsyncResult ar)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<AsyncCallback>(this.m_userCallback, "m_userCallback");
			this.m_userCallback(this);
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00023F94 File Offset: 0x00022194
		protected override void OnCallback(PendingCallbackReason reason)
		{
			switch (reason)
			{
			case PendingCallbackReason.Completed:
			{
				AsyncWatchdogResult<T> asyncWatchdogResult = new AsyncWatchdogResult<T>(false, this.m_cancelResult);
				this.m_asyncResult.SignalCompletion(false, asyncWatchdogResult);
				return;
			}
			case PendingCallbackReason.TimeExpired:
			{
				AsyncWatchdogResult<T> asyncWatchdogResult2 = new AsyncWatchdogResult<T>(true, default(T));
				this.m_asyncResult.SignalCompletion(false, asyncWatchdogResult2);
				return;
			}
			case PendingCallbackReason.Shutdown:
				this.m_asyncResult.SignalCompletion(false, new ShutdownSequenceStartedException());
				return;
			}
			ExtendedDiagnostics.EnsureInvalidSwitchValue<PendingCallbackReason>(reason);
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x0002400E File Offset: 0x0002220E
		public bool IsCompleted
		{
			get
			{
				return this.m_asyncResult.IsCompleted;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x0002401B File Offset: 0x0002221B
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				return this.m_asyncResult.AsyncWaitHandle;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x00024028 File Offset: 0x00022228
		public object AsyncState
		{
			get
			{
				return this.m_asyncResult.AsyncState;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x00024035 File Offset: 0x00022235
		public bool CompletedSynchronously
		{
			get
			{
				return this.m_asyncResult.CompletedSynchronously;
			}
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00024042 File Offset: 0x00022242
		public void SignalCompletion(bool completedSynchronously, T res)
		{
			this.m_cancelResult = res;
			base.InvokeCallback(PendingCallbackReason.Completed);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x00024053 File Offset: 0x00022253
		public AsyncWatchdogResult<T> End()
		{
			return this.m_asyncResult.End();
		}

		// Token: 0x04000414 RID: 1044
		private AsyncResult<AsyncWatchdogResult<T>> m_asyncResult;

		// Token: 0x04000415 RID: 1045
		private T m_cancelResult;

		// Token: 0x04000416 RID: 1046
		private AsyncCallback m_userCallback;
	}
}
