using System;
using System.Threading;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200031D RID: 797
	internal class AsyncResultNoResult : IAsyncResult
	{
		// Token: 0x06001CEC RID: 7404 RVA: 0x00057E6C File Offset: 0x0005606C
		public AsyncResultNoResult(AsyncCallback asyncCallback, object state)
		{
			this._asyncCallback = asyncCallback;
			this._asyncState = state;
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001CED RID: 7405 RVA: 0x00057E82 File Offset: 0x00056082
		public object AsyncState
		{
			get
			{
				return this._asyncState;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001CEE RID: 7406 RVA: 0x00057E8A File Offset: 0x0005608A
		public bool CompletedSynchronously
		{
			get
			{
				return this._completedState == 1;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001CEF RID: 7407 RVA: 0x00057E98 File Offset: 0x00056098
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				if (this._asyncWaitHandle == null)
				{
					bool isCompleted = this.IsCompleted;
					ManualResetEvent manualResetEvent = new ManualResetEvent(isCompleted);
					if (Interlocked.CompareExchange<ManualResetEvent>(ref this._asyncWaitHandle, manualResetEvent, null) != null)
					{
						manualResetEvent.Close();
					}
					else if (!isCompleted && this.IsCompleted)
					{
						this._asyncWaitHandle.Set();
					}
				}
				return this._asyncWaitHandle;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001CF0 RID: 7408 RVA: 0x00057EEF File Offset: 0x000560EF
		public bool IsCompleted
		{
			get
			{
				return this._completedState != 0;
			}
		}

		// Token: 0x06001CF1 RID: 7409 RVA: 0x00057F00 File Offset: 0x00056100
		public void SetAsCompleted(Exception exception, bool completedSynchronously)
		{
			int num = Interlocked.Exchange(ref this._completedState, completedSynchronously ? 1 : 2);
			if (num != 0)
			{
				throw new InvalidOperationException("Operation already completed");
			}
			this._exception = exception;
			if (this._asyncWaitHandle != null)
			{
				this._asyncWaitHandle.Set();
			}
			if (this._asyncCallback != null)
			{
				this._asyncCallback(this);
			}
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x00057F5D File Offset: 0x0005615D
		public void EndInvoke()
		{
			if (!this.IsCompleted)
			{
				this.AsyncWaitHandle.WaitOne();
				this.AsyncWaitHandle.Close();
				this._asyncWaitHandle = null;
			}
			if (this._exception != null)
			{
				throw this._exception;
			}
		}

		// Token: 0x04001011 RID: 4113
		private const int StatePending = 0;

		// Token: 0x04001012 RID: 4114
		private const int StateCompletedSynchronously = 1;

		// Token: 0x04001013 RID: 4115
		private const int StateCompletedAsynchronously = 2;

		// Token: 0x04001014 RID: 4116
		private readonly AsyncCallback _asyncCallback;

		// Token: 0x04001015 RID: 4117
		private readonly object _asyncState;

		// Token: 0x04001016 RID: 4118
		private int _completedState;

		// Token: 0x04001017 RID: 4119
		private ManualResetEvent _asyncWaitHandle;

		// Token: 0x04001018 RID: 4120
		private Exception _exception;
	}
}
