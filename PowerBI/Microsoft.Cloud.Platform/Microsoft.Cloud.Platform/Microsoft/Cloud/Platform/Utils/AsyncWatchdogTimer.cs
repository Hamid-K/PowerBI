using System;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000196 RID: 406
	public class AsyncWatchdogTimer<T> : IShuttable
	{
		// Token: 0x06000A68 RID: 2664 RVA: 0x00023E0D File Offset: 0x0002200D
		public AsyncWatchdogTimer([NotNull] string identity)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(identity, "identity");
			this.m_workTicketFactory = new WorkTicketManager("AsyncWatchdogTimer<T>.WorkTicketManager." + identity);
			this.m_workTicketFactory.TrackTickets = true;
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00023E42 File Offset: 0x00022042
		public IAsyncResult BeginWatch(string identity, int timeoutInMilli, Action<IAsyncResult> preStartWatchdogDelegate, AsyncCallback callback, object asyncState)
		{
			return new AsyncPendingCallback<T>(identity, this.m_workTicketFactory, timeoutInMilli, preStartWatchdogDelegate, callback, asyncState);
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00023E56 File Offset: 0x00022056
		public AsyncWatchdogResult<T> EndWatch([NotNull] IAsyncResult asyncResult)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IAsyncResult>(asyncResult, "asyncResult");
			return ((AsyncPendingCallback<T>)asyncResult).End();
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00023E6E File Offset: 0x0002206E
		public void CancelWatch([NotNull] IAsyncResult asyncResult, T result)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IAsyncResult>(asyncResult, "asyncResult");
			((AsyncPendingCallback<T>)asyncResult).SignalCompletion(false, result);
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00023E88 File Offset: 0x00022088
		public void Stop()
		{
			this.m_workTicketFactory.Stop();
			foreach (WorkTicket workTicket in this.m_workTicketFactory.EnumeratePendingTickets(0))
			{
				((AsyncPendingCallback<T>)workTicket).InvokeCallback(PendingCallbackReason.Shutdown);
			}
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00023EEC File Offset: 0x000220EC
		public void WaitForStopToComplete()
		{
			this.m_workTicketFactory.WaitForStopToComplete();
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00023EF9 File Offset: 0x000220F9
		public void Shutdown()
		{
			this.m_workTicketFactory.Shutdown();
		}

		// Token: 0x04000413 RID: 1043
		private WorkTicketManager m_workTicketFactory;
	}
}
