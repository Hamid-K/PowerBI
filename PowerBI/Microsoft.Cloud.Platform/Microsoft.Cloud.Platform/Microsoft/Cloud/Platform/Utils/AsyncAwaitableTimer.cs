using System;
using System.Threading.Tasks;
using Microsoft.Cloud.Platform.MonitoredUtils;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200017F RID: 383
	public sealed class AsyncAwaitableTimer : IDisposable
	{
		// Token: 0x060009E9 RID: 2537 RVA: 0x000224D0 File Offset: 0x000206D0
		public AsyncAwaitableTimer(TimeSpan tickInterval, Func<Task> callback, ActivityType activityType, IActivityFactory activityFactory, IMonitoredActivityCompletionModelFactory monitoredActivityCompletionModelFactory, IIdentifiable owner)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Func<Task>>(callback, "callback");
			ExtendedDiagnostics.EnsureArgumentNotNull<IIdentifiable>(owner, "owner");
			this.m_tickInterval = tickInterval;
			this.m_callback = callback;
			this.m_activityType = activityType;
			this.m_activityFactory = activityFactory;
			this.m_monitoredActivityCompletionModelFactory = monitoredActivityCompletionModelFactory;
			this.m_ownerName = owner.Name + "_" + Guid.NewGuid().ToString();
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0002254C File Offset: 0x0002074C
		~AsyncAwaitableTimer()
		{
			TraceSourceBase<UtilsTrace>.Tracer.TraceError("AsyncAwaitableTimer finalizer was called. Was not disposed of properly. Owner: {0}", new object[] { this.m_ownerName });
			this.Dispose(false);
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x00022598 File Offset: 0x00020798
		public void Start()
		{
			if (this.m_timerTask != null)
			{
				throw new InvalidOperationException("Timer is already started. Owner: {0}".FormatWithInvariantCulture(new object[] { this.m_ownerName }));
			}
			this.m_timerTask = Task.Run(async delegate
			{
				await AsyncUtils.ExecuteInMonitoredScope(this.m_activityType, this.m_activityFactory, this.m_monitoredActivityCompletionModelFactory, async delegate
				{
					await Task.Delay(this.m_tickInterval);
					while (!this.m_stopped)
					{
						await this.m_callback();
						await Task.Delay(this.m_tickInterval);
					}
				}, null, null, null);
			});
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x000225D8 File Offset: 0x000207D8
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x000225E1 File Offset: 0x000207E1
		private void Dispose(bool disposing)
		{
			if (this.m_disposed)
			{
				return;
			}
			this.m_stopped = true;
			if (disposing)
			{
				GC.SuppressFinalize(this);
			}
			this.m_disposed = true;
		}

		// Token: 0x040003DB RID: 987
		private readonly TimeSpan m_tickInterval;

		// Token: 0x040003DC RID: 988
		private readonly Func<Task> m_callback;

		// Token: 0x040003DD RID: 989
		private readonly string m_ownerName;

		// Token: 0x040003DE RID: 990
		private readonly ActivityType m_activityType;

		// Token: 0x040003DF RID: 991
		private readonly IActivityFactory m_activityFactory;

		// Token: 0x040003E0 RID: 992
		private readonly IMonitoredActivityCompletionModelFactory m_monitoredActivityCompletionModelFactory;

		// Token: 0x040003E1 RID: 993
		private Task m_timerTask;

		// Token: 0x040003E2 RID: 994
		private bool m_disposed;

		// Token: 0x040003E3 RID: 995
		private volatile bool m_stopped;
	}
}
