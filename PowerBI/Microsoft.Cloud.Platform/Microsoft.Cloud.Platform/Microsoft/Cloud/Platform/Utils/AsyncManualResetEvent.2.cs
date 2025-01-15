using System;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000190 RID: 400
	public sealed class AsyncManualResetEvent<T> where T : class
	{
		// Token: 0x06000A4E RID: 2638 RVA: 0x00023BED File Offset: 0x00021DED
		public AsyncManualResetEvent()
		{
			this.m_asyncManualResetEvent = new AsyncManualResetEvent();
			this.m_result = default(T);
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x00023C0C File Offset: 0x00021E0C
		public void Set([NotNull] T result)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<T>(result, "result");
			this.m_result = result;
			this.m_asyncManualResetEvent.Set();
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00023C2B File Offset: 0x00021E2B
		public void Set(Exception exception)
		{
			this.m_asyncManualResetEvent.Set(exception);
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x00023C3C File Offset: 0x00021E3C
		public IAsyncResult BeginWait(AsyncCallback asyncCallback, object asyncState)
		{
			ChainedAsyncResult<WorkTicket, string> chainedAsyncResult = new ChainedAsyncResult<WorkTicket, string>(asyncCallback, asyncState, null);
			chainedAsyncResult.InnerResult = this.m_asyncManualResetEvent.BeginWait(new AsyncCallback(chainedAsyncResult.BeginAsyncFunctionCallback), null);
			return chainedAsyncResult;
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x00023C74 File Offset: 0x00021E74
		public T EndWait(IAsyncResult asyncResult)
		{
			ChainedAsyncResult<WorkTicket> chainedAsyncResult = (ChainedAsyncResult<WorkTicket>)asyncResult;
			chainedAsyncResult.End();
			this.m_asyncManualResetEvent.EndWait(chainedAsyncResult.InnerResult);
			return this.m_result;
		}

		// Token: 0x0400040C RID: 1036
		private AsyncManualResetEvent m_asyncManualResetEvent;

		// Token: 0x0400040D RID: 1037
		private T m_result;
	}
}
