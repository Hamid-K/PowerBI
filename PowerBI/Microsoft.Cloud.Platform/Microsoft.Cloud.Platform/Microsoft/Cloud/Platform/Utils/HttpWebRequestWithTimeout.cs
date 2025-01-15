using System;
using System.Net;
using System.Threading;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000223 RID: 547
	public sealed class HttpWebRequestWithTimeout
	{
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x00032C33 File Offset: 0x00030E33
		// (set) Token: 0x06000E6D RID: 3693 RVA: 0x00032C3B File Offset: 0x00030E3B
		public HttpWebRequest Request { get; private set; }

		// Token: 0x06000E6E RID: 3694 RVA: 0x00032C44 File Offset: 0x00030E44
		public HttpWebRequestWithTimeout([NotNull] HttpWebRequest request, int millisecondsTimeout)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<HttpWebRequest>(request, "request");
			ExtendedDiagnostics.EnsureArgument(millisecondsTimeout, "millisecondsTimeout", millisecondsTimeout == -1 || millisecondsTimeout >= 0);
			this.Request = request;
			this.m_millisecondsTimeout = millisecondsTimeout;
			this.m_pendingCallback = new PendingCallbackSlim(new Action<PendingCallbackReason>(this.OnPendingCallback));
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x00032CA0 File Offset: 0x00030EA0
		public IAsyncResult BeginGetResponse(AsyncCallback callback, object context)
		{
			Interlocked.Exchange(ref this.m_started, 1);
			if (this.m_millisecondsTimeout > 0)
			{
				this.m_pendingCallback.ScheduleTimer(this.m_millisecondsTimeout);
			}
			IAsyncResult asyncResult;
			try
			{
				asyncResult = this.Request.BeginGetResponse(callback, context);
			}
			catch (Exception ex)
			{
				if (!ex.IsFatal())
				{
					this.m_pendingCallback.InvokeCallback(PendingCallbackReason.Completed);
				}
				throw;
			}
			return asyncResult;
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x00032D10 File Offset: 0x00030F10
		public HttpWebResponse EndGetResponse(IAsyncResult asyncResult)
		{
			bool flag = false;
			if (asyncResult.IsCompleted)
			{
				flag = true;
				this.m_pendingCallback.InvokeCallback(PendingCallbackReason.Completed);
			}
			HttpWebResponse httpWebResponse;
			try
			{
				httpWebResponse = this.Request.EndGetResponse(asyncResult) as HttpWebResponse;
			}
			finally
			{
				if (!flag)
				{
					this.m_pendingCallback.InvokeCallback(PendingCallbackReason.Completed);
				}
			}
			return httpWebResponse;
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x00032D70 File Offset: 0x00030F70
		private void OnPendingCallback(PendingCallbackReason reason)
		{
			if (reason == PendingCallbackReason.TimeExpired)
			{
				TraceSourceBase<UtilsTrace>.Tracer.TraceWarning("Aborting http request to Uri '{0}' due to timeout of '{1}'ms", new object[]
				{
					this.Request.RequestUri.AbsoluteUri,
					this.m_millisecondsTimeout
				});
				this.Request.Abort();
			}
		}

		// Token: 0x0400059C RID: 1436
		private PendingCallbackSlim m_pendingCallback;

		// Token: 0x0400059D RID: 1437
		private int m_started;

		// Token: 0x0400059E RID: 1438
		private int m_millisecondsTimeout;
	}
}
