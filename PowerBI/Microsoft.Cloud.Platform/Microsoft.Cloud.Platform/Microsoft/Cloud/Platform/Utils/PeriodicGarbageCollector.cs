using System;
using System.Threading;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001DC RID: 476
	public class PeriodicGarbageCollector : IDisposable
	{
		// Token: 0x06000C75 RID: 3189 RVA: 0x0002BAA0 File Offset: 0x00029CA0
		public bool Start()
		{
			int value = this.m_periodicGarbageCollectionTweak.Value;
			if (value != -1)
			{
				object @lock = this.m_lock;
				lock (@lock)
				{
					if (this.m_timer == null)
					{
						this.m_timer = new Timer(new TimerCallback(this.OnTimerElapsed), null, value, -1);
						TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "PeriodicGarbageCollection enabled with interval={0} milliseconds", new object[] { value });
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x0002BB34 File Offset: 0x00029D34
		private void OnTimerElapsed(object state)
		{
			ExtendedGC.CollectEverything();
			object @lock = this.m_lock;
			lock (@lock)
			{
				if (this.m_timer != null)
				{
					this.m_timer.Change(this.m_periodicGarbageCollectionTweak.Value, -1);
				}
			}
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x0002BB94 File Offset: 0x00029D94
		private static void GCNow(Tweak tweak)
		{
			Tweak<int> tweak2 = tweak as Tweak<int>;
			if (tweak2 != null && tweak2.Value != 0)
			{
				ExtendedGC.CollectEverything();
			}
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x0002BBB8 File Offset: 0x00029DB8
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x0002BBC4 File Offset: 0x00029DC4
		protected virtual void Dispose(bool disposing)
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				if (this.m_timer != null && disposing)
				{
					this.m_timer.Dispose();
				}
				this.m_timer = null;
			}
		}

		// Token: 0x040004C6 RID: 1222
		private Timer m_timer;

		// Token: 0x040004C7 RID: 1223
		private object m_lock = new object();

		// Token: 0x040004C8 RID: 1224
		private const string c_periodicGarbageCollectionTweakName = "Microsoft.Cloud.Platform.Utils.ExtendedDiagnostics.PeriodicGarbageCollectionPeriod_ms";

		// Token: 0x040004C9 RID: 1225
		private Tweak<int> m_periodicGarbageCollectionTweak = Anchor.Tweaks.RegisterTweak<int>("Microsoft.Cloud.Platform.Utils.ExtendedDiagnostics.PeriodicGarbageCollectionPeriod_ms", "When set, periodically force garbage collection and wait for pending finalizers to complete. Set the tweak value to the interval, in milliseconds, at which to force garbage collection.", -1);

		// Token: 0x040004CA RID: 1226
		private const string c_gcNowTweakName = "Microsoft.Cloud.Platform.Utils.ExtendedDiagnostics.GCNow";

		// Token: 0x040004CB RID: 1227
		private Tweak<int> m_gcNowTweak = Anchor.Tweaks.RegisterTweak<int>("Microsoft.Cloud.Platform.Utils.ExtendedDiagnostics.GCNow", "A change of this value to any value that is not 0 will trigger a full GC cycle.", new Action<Tweak>(PeriodicGarbageCollector.GCNow), 0);
	}
}
