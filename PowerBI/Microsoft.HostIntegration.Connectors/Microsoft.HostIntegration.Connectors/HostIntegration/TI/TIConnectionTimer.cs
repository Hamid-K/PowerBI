using System;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using Microsoft.HostIntegration.EventLogging;
using Microsoft.HostIntegration.StrictResources.TIGlobals;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x02000733 RID: 1843
	public class TIConnectionTimer : IDisposable
	{
		// Token: 0x060039B6 RID: 14774 RVA: 0x000C63BC File Offset: 0x000C45BC
		public TIConnectionTimer(EventLogContainer eventLogging)
		{
			this._timer = new global::System.Timers.Timer();
			this._timer.Elapsed += this.ConnectionTimedOut;
			this._timer.AutoReset = false;
			this._transport = null;
			this._timerLock = 0;
			this._eventLogging = eventLogging;
		}

		// Token: 0x060039B7 RID: 14775 RVA: 0x000C6414 File Offset: 0x000C4614
		public void StartPersistentTimer(ITransport transport, int timeoutSeconds, Guid? persistentCorrelator)
		{
			if (persistentCorrelator == null)
			{
				return;
			}
			this._transport = transport;
			this._persistentCorrelator = persistentCorrelator.Value;
			this._timerLock = 0;
			this._timer.Interval = (double)(timeoutSeconds * 1000);
			this._timer.Enabled = true;
		}

		// Token: 0x060039B8 RID: 14776 RVA: 0x000C6465 File Offset: 0x000C4665
		public void CancelPersistentTimer(Guid? persistentCorrelator)
		{
			if (persistentCorrelator == null)
			{
				return;
			}
			if (Interlocked.CompareExchange(ref this._timerLock, 1, 0) == 0)
			{
				this._timer.Stop();
				this._timerLock = 0;
			}
		}

		// Token: 0x060039B9 RID: 14777 RVA: 0x000C6492 File Offset: 0x000C4692
		private void ConnectionTimedOut(object source, ElapsedEventArgs e)
		{
			if (Interlocked.CompareExchange(ref this._timerLock, 1, 0) == 0)
			{
				this._eventLogging.WriteEvent(SR.PersistentConnectionTimeout(this._transport.TransportName), EventLogEntryType.Warning);
				this._transport.Disconnect(DisconnectType.eForced);
				this._timerLock = 0;
			}
		}

		// Token: 0x060039BA RID: 14778 RVA: 0x000C64D2 File Offset: 0x000C46D2
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060039BB RID: 14779 RVA: 0x000036A9 File Offset: 0x000018A9
		private void Dispose(bool disposing)
		{
		}

		// Token: 0x040022FF RID: 8959
		private global::System.Timers.Timer _timer;

		// Token: 0x04002300 RID: 8960
		private ITransport _transport;

		// Token: 0x04002301 RID: 8961
		private Guid _persistentCorrelator;

		// Token: 0x04002302 RID: 8962
		private int _timerLock;

		// Token: 0x04002303 RID: 8963
		private EventLogContainer _eventLogging;
	}
}
