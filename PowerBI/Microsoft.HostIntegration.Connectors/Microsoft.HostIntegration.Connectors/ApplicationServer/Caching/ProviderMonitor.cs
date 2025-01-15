using System;
using System.Diagnostics;
using System.Threading;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000366 RID: 870
	internal class ProviderMonitor : IDisposable
	{
		// Token: 0x06001E86 RID: 7814 RVA: 0x0005D579 File Offset: 0x0005B779
		internal ProviderMonitor(int monitorInterval)
		{
			this._interval = monitorInterval;
			this._previousLevel = TraceLevel.Verbose;
			this._logLevelMonitor = new Timer(new TimerCallback(this.MonitorLogLevel), null, 0, this._interval);
		}

		// Token: 0x06001E87 RID: 7815 RVA: 0x0005D5AE File Offset: 0x0005B7AE
		internal static bool ShouldCreate(int monitorInterval)
		{
			return monitorInterval != 0;
		}

		// Token: 0x06001E88 RID: 7816 RVA: 0x0005D5B8 File Offset: 0x0005B7B8
		private void MonitorLogLevel(object obj)
		{
			TraceLevel traceLevel;
			if (!Provider.IsEnabled())
			{
				traceLevel = TraceLevel.Off;
			}
			else if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				traceLevel = TraceLevel.Verbose;
			}
			else if (Provider.IsEnabled(TraceLevel.Info))
			{
				traceLevel = TraceLevel.Info;
			}
			else if (Provider.IsEnabled(TraceLevel.Warning))
			{
				traceLevel = TraceLevel.Warning;
			}
			else if (Provider.IsEnabled(TraceLevel.Error))
			{
				traceLevel = TraceLevel.Error;
			}
			else
			{
				traceLevel = TraceLevel.Off;
			}
			if (traceLevel != this._previousLevel)
			{
				CacheEventHelper.ChangeEtwSinkSetting(traceLevel);
				this._previousLevel = traceLevel;
			}
		}

		// Token: 0x06001E89 RID: 7817 RVA: 0x0005D61A File Offset: 0x0005B81A
		public void Cancel()
		{
			if (this._logLevelMonitor != null)
			{
				this._logLevelMonitor.Dispose();
				this._logLevelMonitor = null;
			}
		}

		// Token: 0x06001E8A RID: 7818 RVA: 0x0005D636 File Offset: 0x0005B836
		public void Dispose()
		{
			this.Cancel();
		}

		// Token: 0x0400114B RID: 4427
		private readonly int _interval;

		// Token: 0x0400114C RID: 4428
		private TraceLevel _previousLevel;

		// Token: 0x0400114D RID: 4429
		private Timer _logLevelMonitor;
	}
}
