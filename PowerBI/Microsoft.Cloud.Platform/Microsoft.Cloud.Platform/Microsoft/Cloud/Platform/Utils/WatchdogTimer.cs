using System;
using System.Threading;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002E7 RID: 743
	public class WatchdogTimer : IDisposable
	{
		// Token: 0x060013CE RID: 5070 RVA: 0x00044C0C File Offset: 0x00042E0C
		private WatchdogTimer(int timeout, WatchdogTimer.TimeoutHandler callback)
		{
			if (WatchdogTimer.sm_leakDetectionEnabledTweak.Value)
			{
				this.m_creationCallStack = CallStackRef.Capture(true);
			}
			Interlocked.Exchange<Timer>(ref this.m_timer, new Timer(new TimerCallback(this.OnTimeout), callback, -1, -1));
			this.m_timer.Change(timeout, -1);
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x00044C65 File Offset: 0x00042E65
		public static WatchdogTimer Start(int timeout, WatchdogTimer.TimeoutHandler callback)
		{
			return new WatchdogTimer(timeout, callback);
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x00044C70 File Offset: 0x00042E70
		public static WatchdogTimer Start(TimeSpan timeout, Action action)
		{
			return WatchdogTimer.Start((int)timeout.TotalMilliseconds, delegate
			{
				action();
			});
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x00044CA3 File Offset: 0x00042EA3
		public void Stop()
		{
			this.Dispose();
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x00044CAB File Offset: 0x00042EAB
		private void OnTimeout(object context)
		{
			(context as WatchdogTimer.TimeoutHandler)();
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x00044CB8 File Offset: 0x00042EB8
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x00044CC1 File Offset: 0x00042EC1
		private void Dispose(bool disposing)
		{
			if (disposing && Interlocked.Exchange(ref this.m_timerDisposed, 1) == 0)
			{
				this.m_timer.Dispose();
				if (this.m_creationCallStack != null)
				{
					this.m_creationCallStack.Dispose();
				}
			}
		}

		// Token: 0x04000771 RID: 1905
		private Timer m_timer;

		// Token: 0x04000772 RID: 1906
		private int m_timerDisposed;

		// Token: 0x04000773 RID: 1907
		private CallStackRef m_creationCallStack;

		// Token: 0x04000774 RID: 1908
		private const string c_leakDetectionEnabledTweakName = "Microsoft.Cloud.Platform.Utils.Watchdog.LeakDetectionEnabled";

		// Token: 0x04000775 RID: 1909
		private static Tweak<bool> sm_leakDetectionEnabledTweak = Anchor.Tweaks.RegisterTweak<bool>("Microsoft.Cloud.Platform.Utils.Watchdog.LeakDetectionEnabled", "When set, watchdog leaks (failure to call Dispose) are detected in debug builds", false);

		// Token: 0x02000789 RID: 1929
		// (Invoke) Token: 0x060030AE RID: 12462
		public delegate void TimeoutHandler();
	}
}
