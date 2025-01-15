using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002BB RID: 699
	public class ConcurrentOperationsThrottlingPolicy : IThrottlingPolicy
	{
		// Token: 0x060012D2 RID: 4818 RVA: 0x00041540 File Offset: 0x0003F740
		public ConcurrentOperationsThrottlingPolicy(int maxConcurrentOperations)
		{
			ExtendedDiagnostics.EnsureArgumentIsPositive(maxConcurrentOperations, "maxConcurrentOperations");
			this.m_maxConcurrentOperations = maxConcurrentOperations;
			this.m_currentlyRunningOperations = 0;
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x00041561 File Offset: 0x0003F761
		public int CurrentlyRunningOperations
		{
			get
			{
				return this.m_currentlyRunningOperations;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x00041569 File Offset: 0x0003F769
		public int MaxConcurrentOperations
		{
			get
			{
				return this.m_maxConcurrentOperations;
			}
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x00041574 File Offset: 0x0003F774
		public TimeToRun GetTimeToRun(DateTime now)
		{
			if (this.m_currentlyRunningOperations < this.m_maxConcurrentOperations)
			{
				return new TimeToRun(true, TimeToRun.Infinite);
			}
			return new TimeToRun(false, DateTime.UtcNow.Add(TimeSpan.FromSeconds(10.0)));
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x000415BC File Offset: 0x0003F7BC
		public void OnOperationStarted(DateTime now)
		{
			this.m_currentlyRunningOperations++;
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x000415CC File Offset: 0x0003F7CC
		public void OnOperationCompleted(DateTime now)
		{
			this.m_currentlyRunningOperations--;
		}

		// Token: 0x040006FB RID: 1787
		private readonly int m_maxConcurrentOperations;

		// Token: 0x040006FC RID: 1788
		private int m_currentlyRunningOperations;
	}
}
