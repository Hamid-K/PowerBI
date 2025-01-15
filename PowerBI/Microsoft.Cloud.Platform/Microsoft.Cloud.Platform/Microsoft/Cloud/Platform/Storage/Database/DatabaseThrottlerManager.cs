using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Cloud.Platform.ConfigurationClasses.Resources;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.MonitoredUtils;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x02000050 RID: 80
	internal sealed class DatabaseThrottlerManager : IShuttable
	{
		// Token: 0x060001E7 RID: 487 RVA: 0x000065BA File Offset: 0x000047BA
		public DatabaseThrottlerManager(IEventsKitFactory eventskitFactory)
		{
			this.m_eventskitFactory = eventskitFactory;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000065E0 File Offset: 0x000047E0
		public MonitoredBasedPoliciesThrottler Create(string name, ResourceThrottlerConfiguration throttlerCfg)
		{
			MonitoredBasedPoliciesThrottler monitoredBasedPoliciesThrottler = null;
			if (throttlerCfg.MaxThrottledConcurrentOperations > 0)
			{
				object @lock = this.m_lock;
				lock (@lock)
				{
					if (this.m_stopped)
					{
						throw new ShutdownSequenceStartedException();
					}
					monitoredBasedPoliciesThrottler = new MonitoredBasedPoliciesThrottler(this.m_eventskitFactory, name, throttlerCfg.MaxThrottledPendingOperations, new ConcurrentOperationsThrottlingPolicy(throttlerCfg.MaxThrottledConcurrentOperations), QueueFullPolicyType.DropNewOperation);
					this.m_throttlers.Add(monitoredBasedPoliciesThrottler);
					TraceSourceBase<StorageTrace>.Tracer.TraceInformation("Created throttler for '{0}' {1}/{2}", new object[] { name, throttlerCfg.MaxThrottledConcurrentOperations, throttlerCfg.MaxThrottledPendingOperations });
					return monitoredBasedPoliciesThrottler;
				}
			}
			TraceSourceBase<StorageTrace>.Tracer.TraceInformation("No throttler configured for '{0}'", new object[] { name });
			return monitoredBasedPoliciesThrottler;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x000066B0 File Offset: 0x000048B0
		public void Stop()
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				this.m_throttlers.ForEach(delegate(MonitoredBasedPoliciesThrottler t)
				{
					t.Stop();
				});
				this.m_stopped = true;
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000671C File Offset: 0x0000491C
		public void WaitForStopToComplete()
		{
			object @lock = this.m_lock;
			List<MonitoredBasedPoliciesThrottler> list;
			lock (@lock)
			{
				list = this.m_throttlers.ToList<MonitoredBasedPoliciesThrottler>();
			}
			list.ForEach(delegate(MonitoredBasedPoliciesThrottler t)
			{
				t.WaitForStopToComplete();
			});
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00006788 File Offset: 0x00004988
		public void Shutdown()
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				this.m_throttlers.ForEach(delegate(MonitoredBasedPoliciesThrottler t)
				{
					t.Shutdown();
				});
			}
		}

		// Token: 0x040000D9 RID: 217
		private readonly IEventsKitFactory m_eventskitFactory;

		// Token: 0x040000DA RID: 218
		private readonly List<MonitoredBasedPoliciesThrottler> m_throttlers = new List<MonitoredBasedPoliciesThrottler>();

		// Token: 0x040000DB RID: 219
		private bool m_stopped;

		// Token: 0x040000DC RID: 220
		private readonly object m_lock = new object();
	}
}
