using System;
using System.Threading;
using NLog.Common;

namespace NLog.Internal
{
	// Token: 0x0200014B RID: 331
	internal class TimeoutContinuation : IDisposable
	{
		// Token: 0x06000FF3 RID: 4083 RVA: 0x00029080 File Offset: 0x00027280
		public TimeoutContinuation(AsyncContinuation asyncContinuation, TimeSpan timeout)
		{
			this._asyncContinuation = asyncContinuation;
			this._timeoutTimer = new Timer(new TimerCallback(this.TimerElapsed), null, (int)timeout.TotalMilliseconds, -1);
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x000290B0 File Offset: 0x000272B0
		public void Function(Exception exception)
		{
			try
			{
				AsyncContinuation asyncContinuation = Interlocked.Exchange<AsyncContinuation>(ref this._asyncContinuation, null);
				this.StopTimer();
				if (asyncContinuation != null)
				{
					asyncContinuation(exception);
				}
			}
			catch (Exception ex)
			{
				InternalLogger.Error(ex, "Exception in asynchronous handler.");
				if (ex.MustBeRethrown())
				{
					throw;
				}
			}
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x00029104 File Offset: 0x00027304
		public void Dispose()
		{
			this.StopTimer();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x00029114 File Offset: 0x00027314
		private void StopTimer()
		{
			Timer timer = Interlocked.Exchange<Timer>(ref this._timeoutTimer, null);
			if (timer != null)
			{
				timer.WaitForDispose(TimeSpan.Zero);
			}
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0002913D File Offset: 0x0002733D
		private void TimerElapsed(object state)
		{
			this.Function(new TimeoutException("Timeout."));
		}

		// Token: 0x04000443 RID: 1091
		private AsyncContinuation _asyncContinuation;

		// Token: 0x04000444 RID: 1092
		private Timer _timeoutTimer;
	}
}
