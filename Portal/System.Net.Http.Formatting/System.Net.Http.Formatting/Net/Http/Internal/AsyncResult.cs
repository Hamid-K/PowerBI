using System;
using System.Net.Http.Properties;
using System.Threading;
using System.Web.Http;

namespace System.Net.Http.Internal
{
	// Token: 0x0200002F RID: 47
	internal abstract class AsyncResult : IAsyncResult
	{
		// Token: 0x060001CE RID: 462 RVA: 0x0000662D File Offset: 0x0000482D
		protected AsyncResult(AsyncCallback callback, object state)
		{
			this._callback = callback;
			this._state = state;
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00006643 File Offset: 0x00004843
		public object AsyncState
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0000664B File Offset: 0x0000484B
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000664E File Offset: 0x0000484E
		public bool CompletedSynchronously
		{
			get
			{
				return this._completedSynchronously;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00006656 File Offset: 0x00004856
		public bool HasCallback
		{
			get
			{
				return this._callback != null;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00006661 File Offset: 0x00004861
		public bool IsCompleted
		{
			get
			{
				return this._isCompleted;
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000666C File Offset: 0x0000486C
		protected void Complete(bool completedSynchronously)
		{
			if (this._isCompleted)
			{
				throw Error.InvalidOperation(Resources.AsyncResult_MultipleCompletes, new object[] { base.GetType().Name });
			}
			this._completedSynchronously = completedSynchronously;
			this._isCompleted = true;
			if (this._callback != null)
			{
				try
				{
					this._callback(this);
				}
				catch (Exception ex)
				{
					throw Error.InvalidOperation(ex, Resources.AsyncResult_CallbackThrewException, new object[0]);
				}
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000066E8 File Offset: 0x000048E8
		protected void Complete(bool completedSynchronously, Exception exception)
		{
			this._exception = exception;
			this.Complete(completedSynchronously);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000066F8 File Offset: 0x000048F8
		protected static TAsyncResult End<TAsyncResult>(IAsyncResult result) where TAsyncResult : AsyncResult
		{
			if (result == null)
			{
				throw Error.ArgumentNull("result");
			}
			TAsyncResult tasyncResult = result as TAsyncResult;
			if (tasyncResult == null)
			{
				throw Error.Argument("result", Resources.AsyncResult_ResultMismatch, new object[0]);
			}
			if (!tasyncResult._isCompleted)
			{
				tasyncResult.AsyncWaitHandle.WaitOne();
			}
			if (tasyncResult._endCalled)
			{
				throw Error.InvalidOperation(Resources.AsyncResult_MultipleEnds, new object[0]);
			}
			tasyncResult._endCalled = true;
			if (tasyncResult._exception != null)
			{
				throw tasyncResult._exception;
			}
			return tasyncResult;
		}

		// Token: 0x0400008F RID: 143
		private AsyncCallback _callback;

		// Token: 0x04000090 RID: 144
		private object _state;

		// Token: 0x04000091 RID: 145
		private bool _isCompleted;

		// Token: 0x04000092 RID: 146
		private bool _completedSynchronously;

		// Token: 0x04000093 RID: 147
		private bool _endCalled;

		// Token: 0x04000094 RID: 148
		private Exception _exception;
	}
}
