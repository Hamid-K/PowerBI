using System;

namespace Microsoft.InfoNav.Data.Contracts.ExecutionMetadata
{
	// Token: 0x020000FC RID: 252
	public sealed class NoOpExecutionMetricsService : IExecutionMetricsService
	{
		// Token: 0x0600068D RID: 1677 RVA: 0x0000D925 File Offset: 0x0000BB25
		private NoOpExecutionMetricsService()
		{
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x0000D92D File Offset: 0x0000BB2D
		public static NoOpExecutionMetricsService Instance
		{
			get
			{
				return NoOpExecutionMetricsService._instance;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x0000D934 File Offset: 0x0000BB34
		public bool ExceededMaxEventCount
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0000D937 File Offset: 0x0000BB37
		public ITimedEventTracker BeginEvent(string eventName, string componentName)
		{
			return NoOpExecutionMetricsService.NoOpEventTracker.Instance;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0000D93E File Offset: 0x0000BB3E
		public IInstantEventTracker FireInstantEvent(string eventName, string componentName, bool bypassMaxEventCount = false)
		{
			return NoOpExecutionMetricsService.NoOpEventTracker.Instance;
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0000D945 File Offset: 0x0000BB45
		public void AttachExternalEvent(ExecutionEvent execEvent)
		{
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0000D947 File Offset: 0x0000BB47
		public ExecutionMetrics ToExecutionMetrics()
		{
			return null;
		}

		// Token: 0x040002C2 RID: 706
		private static readonly NoOpExecutionMetricsService _instance = new NoOpExecutionMetricsService();

		// Token: 0x02000310 RID: 784
		private sealed class NoOpEventTracker : ITimedEventTracker, IEventTracker, IDisposable, IInstantEventTracker
		{
			// Token: 0x06001974 RID: 6516 RVA: 0x0002DCDB File Offset: 0x0002BEDB
			private NoOpEventTracker()
			{
			}

			// Token: 0x1700054A RID: 1354
			// (get) Token: 0x06001975 RID: 6517 RVA: 0x0002DCE3 File Offset: 0x0002BEE3
			public static NoOpExecutionMetricsService.NoOpEventTracker Instance
			{
				get
				{
					return NoOpExecutionMetricsService.NoOpEventTracker._instance;
				}
			}

			// Token: 0x1700054B RID: 1355
			// (get) Token: 0x06001976 RID: 6518 RVA: 0x0002DCEA File Offset: 0x0002BEEA
			public string Id
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06001977 RID: 6519 RVA: 0x0002DCED File Offset: 0x0002BEED
			public void SetMetric(string name, object value)
			{
			}

			// Token: 0x06001978 RID: 6520 RVA: 0x0002DCEF File Offset: 0x0002BEEF
			public void Dispose()
			{
			}

			// Token: 0x0400096E RID: 2414
			private static readonly NoOpExecutionMetricsService.NoOpEventTracker _instance = new NoOpExecutionMetricsService.NoOpEventTracker();
		}
	}
}
