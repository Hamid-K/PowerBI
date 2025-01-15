using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200018F RID: 399
	public sealed class AsyncManualResetEvent
	{
		// Token: 0x06000A49 RID: 2633 RVA: 0x00023A55 File Offset: 0x00021C55
		public AsyncManualResetEvent()
		{
			this.m_locker = new object();
			this.m_waiters = new List<VoidAsyncResult>();
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00023A73 File Offset: 0x00021C73
		public void Set()
		{
			this.Set(null);
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x00023A7C File Offset: 0x00021C7C
		public void Set(Exception exception)
		{
			ExtendedEnvironment.ApplyFailSlowOnFatalPolicy(this, exception);
			object locker = this.m_locker;
			List<VoidAsyncResult> waiters;
			lock (locker)
			{
				ExtendedDiagnostics.EnsureOperation(!this.m_completed, "Cannot complete more than once");
				this.m_completed = true;
				this.m_exception = exception;
				waiters = this.m_waiters;
				this.m_waiters = null;
			}
			foreach (VoidAsyncResult voidAsyncResult in waiters)
			{
				VoidAsyncResult complete = voidAsyncResult;
				AsyncInvoker.InvokeMethodAsynchronously(delegate
				{
					complete.SignalCompletion(false, exception);
				}, WaitOrNot.DontWait, "Signal Completion");
			}
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x00023B68 File Offset: 0x00021D68
		public IAsyncResult BeginWait(AsyncCallback asyncCallback, object asyncState)
		{
			IAsyncResult asyncResult = null;
			Exception ex = null;
			object locker = this.m_locker;
			lock (locker)
			{
				if (this.m_completed)
				{
					ex = this.m_exception;
				}
				else
				{
					VoidAsyncResult voidAsyncResult = new VoidAsyncResult(asyncCallback, asyncState);
					this.m_waiters.Add(voidAsyncResult);
					asyncResult = voidAsyncResult;
				}
			}
			if (asyncResult == null)
			{
				asyncResult = new CompletedAsyncResult(ex, asyncCallback, asyncState);
			}
			return asyncResult;
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00023BE0 File Offset: 0x00021DE0
		public void EndWait(IAsyncResult asyncResult)
		{
			((VoidAsyncResult)asyncResult).End();
		}

		// Token: 0x04000408 RID: 1032
		private readonly object m_locker;

		// Token: 0x04000409 RID: 1033
		private bool m_completed;

		// Token: 0x0400040A RID: 1034
		private Exception m_exception;

		// Token: 0x0400040B RID: 1035
		private List<VoidAsyncResult> m_waiters;
	}
}
