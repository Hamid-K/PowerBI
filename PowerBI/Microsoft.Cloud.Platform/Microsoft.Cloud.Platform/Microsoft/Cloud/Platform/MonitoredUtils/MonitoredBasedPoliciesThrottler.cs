using System;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x02000148 RID: 328
	public class MonitoredBasedPoliciesThrottler : BasedPoliciesThrottler
	{
		// Token: 0x06000898 RID: 2200 RVA: 0x0001E02A File Offset: 0x0001C22A
		public MonitoredBasedPoliciesThrottler(IEventsKitFactory eventsKitFactory, string name, int maxPendingOperations, IThrottlingPolicy policy, QueueFullPolicyType policyType)
			: this(eventsKitFactory, name, maxPendingOperations, policy, policyType, true)
		{
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0001E03A File Offset: 0x0001C23A
		public MonitoredBasedPoliciesThrottler(IEventsKitFactory eventsKitFactory, string name, int maxPendingOperations, IThrottlingPolicy policy, QueueFullPolicyType policyType, bool failSlowOnDeadlockDetection)
			: base(name, maxPendingOperations, new MonitoredBasedPoliciesThrottler.BasedPoliciesThrottlerEventsWriter(eventsKitFactory, name, maxPendingOperations, policy), policy, failSlowOnDeadlockDetection)
		{
		}

		// Token: 0x02000614 RID: 1556
		private class BasedPoliciesThrottlerEventsWriter : IThrottlerNotifications
		{
			// Token: 0x06002C74 RID: 11380 RVA: 0x0009D174 File Offset: 0x0009B374
			public BasedPoliciesThrottlerEventsWriter(IEventsKitFactory eventsKitFactory, string name, int maxPendingOperations, IThrottlingPolicy policy)
			{
				this.m_maxPendingOperations = maxPendingOperations;
				this.m_throttlingPolicy = policy;
				this.m_eventsKit = eventsKitFactory.CreateEventsKit<IMonitoredThrottlerEventsKit>(name, PerformanceCounterPrefixSetting.ElementName);
			}

			// Token: 0x06002C75 RID: 11381 RVA: 0x0009D199 File Offset: 0x0009B399
			public void OnStateChanged(int pendingCount, int runningCount)
			{
				this.m_eventsKit.NotifyThrottlerItemsCountChanged(this.m_throttlingPolicy.MaxConcurrentOperations, runningCount, this.m_maxPendingOperations, pendingCount);
			}

			// Token: 0x06002C76 RID: 11382 RVA: 0x0009D1B9 File Offset: 0x0009B3B9
			public void OnOverflow()
			{
				this.m_eventsKit.NotifyThrottlerOverflow();
			}

			// Token: 0x06002C77 RID: 11383 RVA: 0x0009D1C6 File Offset: 0x0009B3C6
			public void OnPotentialDeadlockDetected(string operationName, string actionDetails)
			{
				this.m_eventsKit.NotifyOnPotentialDeadlockDetected(operationName, actionDetails);
			}

			// Token: 0x040010D3 RID: 4307
			private IMonitoredThrottlerEventsKit m_eventsKit;

			// Token: 0x040010D4 RID: 4308
			private int m_maxPendingOperations;

			// Token: 0x040010D5 RID: 4309
			private IThrottlingPolicy m_throttlingPolicy;
		}
	}
}
