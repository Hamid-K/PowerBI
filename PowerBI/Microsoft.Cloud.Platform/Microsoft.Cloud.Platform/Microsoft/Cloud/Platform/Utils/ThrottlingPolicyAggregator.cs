using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002BD RID: 701
	public class ThrottlingPolicyAggregator : IThrottlingPolicy
	{
		// Token: 0x060012DE RID: 4830 RVA: 0x0004165C File Offset: 0x0003F85C
		public ThrottlingPolicyAggregator([NotNull] IList<IThrottlingPolicy> policies)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IList<IThrottlingPolicy>>(policies, "policies");
			ExtendedDiagnostics.EnsureOperation(policies.Count > 0, "policies");
			this.m_policies = policies;
			foreach (IThrottlingPolicy throttlingPolicy in this.m_policies)
			{
				ExtendedDiagnostics.EnsureArgumentNotNull<IThrottlingPolicy>(throttlingPolicy, "policy");
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060012DF RID: 4831 RVA: 0x000416D8 File Offset: 0x0003F8D8
		public int CurrentlyRunningOperations
		{
			get
			{
				int num = 0;
				foreach (IThrottlingPolicy throttlingPolicy in this.m_policies)
				{
					num = Math.Max(throttlingPolicy.CurrentlyRunningOperations, num);
				}
				return num;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060012E0 RID: 4832 RVA: 0x0004172C File Offset: 0x0003F92C
		public int MaxConcurrentOperations
		{
			get
			{
				int num = 0;
				foreach (IThrottlingPolicy throttlingPolicy in this.m_policies)
				{
					num = Math.Max(throttlingPolicy.MaxConcurrentOperations, num);
				}
				return num;
			}
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x00041780 File Offset: 0x0003F980
		public TimeToRun GetTimeToRun(DateTime now)
		{
			TimeToRun timeToRun = new TimeToRun(true, TimeToRun.Infinite);
			foreach (IThrottlingPolicy throttlingPolicy in this.m_policies)
			{
				TimeToRun timeToRun2 = throttlingPolicy.GetTimeToRun(now);
				if (!timeToRun2.IsNow)
				{
					if (timeToRun.IsNow)
					{
						timeToRun = timeToRun2;
					}
					else if (timeToRun.When >= timeToRun2.When)
					{
						timeToRun = timeToRun2;
					}
				}
			}
			return timeToRun;
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x00041804 File Offset: 0x0003FA04
		public void OnOperationStarted(DateTime now)
		{
			foreach (IThrottlingPolicy throttlingPolicy in this.m_policies)
			{
				throttlingPolicy.OnOperationStarted(now);
			}
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x00041850 File Offset: 0x0003FA50
		public void OnOperationCompleted(DateTime now)
		{
			foreach (IThrottlingPolicy throttlingPolicy in this.m_policies)
			{
				throttlingPolicy.OnOperationCompleted(now);
			}
		}

		// Token: 0x040006FE RID: 1790
		private IList<IThrottlingPolicy> m_policies;
	}
}
