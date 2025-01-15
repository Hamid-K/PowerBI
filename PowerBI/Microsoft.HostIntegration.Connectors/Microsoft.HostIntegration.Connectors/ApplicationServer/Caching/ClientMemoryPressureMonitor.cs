using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200009C RID: 156
	internal sealed class ClientMemoryPressureMonitor : IDisposable
	{
		// Token: 0x06000394 RID: 916 RVA: 0x00012990 File Offset: 0x00010B90
		public ClientMemoryPressureMonitor(ClientMemoryPressureCallback memoryPressureDelegate)
		{
			this._memoryPressure = new MemoryPressureStatus(85);
			this._ONMemoryPressure = memoryPressureDelegate;
			this._normalPollInterval = new TimeSpan(0, 0, 30);
			this._highPollInterval = new TimeSpan(0, 0, 1);
			this._timerCallback = new TimerCallback(this.CheckMemoryPressure);
			this._timer = new global::System.Threading.Timer(this._timerCallback, null, this._normalPollInterval, this._normalPollInterval);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00012A0B File Offset: 0x00010C0B
		internal ClientMemoryPressureMonitor(ClientMemoryPressureCallback memoryPressureDelegate, MemoryPressureStatus memPressure, TimeSpan normalPollIntvl, TimeSpan highPollIntvl)
			: this(memoryPressureDelegate)
		{
			this._memoryPressure = memPressure;
			this._normalPollInterval = normalPollIntvl;
			this._highPollInterval = highPollIntvl;
			this._timer.Change(normalPollIntvl, normalPollIntvl);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00012A38 File Offset: 0x00010C38
		public void CheckMemoryPressure(object state)
		{
			if (Interlocked.Exchange(ref this._checkMemoryStatusInProgress, 1) == 0)
			{
				try
				{
					ClientMemoryPressureLevel memoryPressure = this._memoryPressure.GetMemoryPressure();
					if (memoryPressure == ClientMemoryPressureLevel.High)
					{
						if (Provider.IsEnabled(TraceLevel.Warning))
						{
							EventLogWriter.WriteWarning("DistributedCache.ClientMemoryMonitor", "High memory pressure detected in client", new object[0]);
						}
						this._ONMemoryPressure(memoryPressure);
						if (this._lastMemoryPressure == ClientMemoryPressureLevel.Low)
						{
							this._timer.Change(this._highPollInterval, this._highPollInterval);
						}
					}
					else if (this._lastMemoryPressure == ClientMemoryPressureLevel.High)
					{
						this._timer.Change(this._normalPollInterval, this._normalPollInterval);
					}
					this._lastMemoryPressure = memoryPressure;
				}
				finally
				{
					Interlocked.Exchange(ref this._checkMemoryStatusInProgress, 0);
				}
			}
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00012AF8 File Offset: 0x00010CF8
		public void Dispose()
		{
			this._timer.Dispose();
		}

		// Token: 0x040002C6 RID: 710
		private MemoryPressureStatus _memoryPressure;

		// Token: 0x040002C7 RID: 711
		private ClientMemoryPressureCallback _ONMemoryPressure;

		// Token: 0x040002C8 RID: 712
		private TimerCallback _timerCallback;

		// Token: 0x040002C9 RID: 713
		private global::System.Threading.Timer _timer;

		// Token: 0x040002CA RID: 714
		private TimeSpan _normalPollInterval;

		// Token: 0x040002CB RID: 715
		private TimeSpan _highPollInterval;

		// Token: 0x040002CC RID: 716
		private ClientMemoryPressureLevel _lastMemoryPressure = ClientMemoryPressureLevel.Low;

		// Token: 0x040002CD RID: 717
		private int _checkMemoryStatusInProgress;
	}
}
