using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.Metrics.Extensibility;

namespace Microsoft.ApplicationInsights.Metrics
{
	// Token: 0x02000027 RID: 39
	internal class DefaultAggregationPeriodCycle
	{
		// Token: 0x06000153 RID: 339 RVA: 0x00007C84 File Offset: 0x00005E84
		public DefaultAggregationPeriodCycle(MetricAggregationManager aggregationManager, MetricManager metricManager)
		{
			Util.ValidateNotNull(aggregationManager, "aggregationManager");
			Util.ValidateNotNull(metricManager, "metricManager");
			this.aggregationManager = aggregationManager;
			this.metricManager = metricManager;
			this.runningState = 0;
			this.workerTaskCompletionControl = new TaskCompletionSource<bool>();
			this.aggregationThread = null;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00007CD4 File Offset: 0x00005ED4
		~DefaultAggregationPeriodCycle()
		{
			this.StopAsync();
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00007D04 File Offset: 0x00005F04
		public bool Start()
		{
			if (Interlocked.CompareExchange(ref this.runningState, 1, 0) != 0)
			{
				return false;
			}
			this.aggregationThread = new Thread(new ThreadStart(this.Run));
			this.aggregationThread.Name = "DefaultAggregationPeriodCycle";
			this.aggregationThread.IsBackground = true;
			this.aggregationThread.Start();
			return true;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00007D61 File Offset: 0x00005F61
		public Task StopAsync()
		{
			Interlocked.Exchange(ref this.runningState, 2);
			return this.workerTaskCompletionControl.Task;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00007D7C File Offset: 0x00005F7C
		public void FetchAndTrackMetrics()
		{
			DateTimeOffset dateTimeOffset = DateTimeOffset.Now;
			if (new DateTimeOffset(dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day, dateTimeOffset.Hour, dateTimeOffset.Minute, 0, dateTimeOffset.Offset) <= dateTimeOffset && new DateTimeOffset(dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day, dateTimeOffset.Hour, dateTimeOffset.Minute, 4, dateTimeOffset.Offset) >= dateTimeOffset)
			{
				dateTimeOffset = new DateTimeOffset(dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day, dateTimeOffset.Hour, dateTimeOffset.Minute, 1, dateTimeOffset.Offset);
			}
			else
			{
				dateTimeOffset = Util.RoundDownToSecond(dateTimeOffset);
			}
			AggregationPeriodSummary aggregates = this.aggregationManager.StartOrCycleAggregators(MetricAggregationCycleKind.Default, dateTimeOffset, null);
			if (aggregates != null)
			{
				Task.Run(delegate
				{
					this.metricManager.TrackMetricAggregates(aggregates, false);
				});
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00007E75 File Offset: 0x00006075
		[SuppressMessage("Naming Rules", "SA1310: C# Field must not contain an underscore", Justification = "By design: Structured name.")]
		internal static DateTimeOffset GetNextCycleTargetTime_UnitTestAccessor(DateTimeOffset periodStart)
		{
			return DefaultAggregationPeriodCycle.GetNextCycleTargetTime(periodStart);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00007E80 File Offset: 0x00006080
		private static DateTimeOffset GetNextCycleTargetTime(DateTimeOffset periodStart)
		{
			DateTimeOffset dateTimeOffset = Util.RoundDownToMinute(periodStart).AddSeconds(61.0);
			if ((dateTimeOffset - periodStart).TotalSeconds < 21.0)
			{
				dateTimeOffset = dateTimeOffset.AddMinutes(1.0);
			}
			return dateTimeOffset;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00007ED4 File Offset: 0x000060D4
		private void Run()
		{
			for (;;)
			{
				DateTimeOffset now = DateTimeOffset.Now;
				Thread.Sleep(DefaultAggregationPeriodCycle.GetNextCycleTargetTime(now) - now);
				if (Volatile.Read(ref this.runningState) != 1)
				{
					break;
				}
				this.FetchAndTrackMetrics();
			}
			this.aggregationThread = null;
			this.workerTaskCompletionControl.TrySetResult(true);
		}

		// Token: 0x0400009B RID: 155
		[SuppressMessage("Naming Rules", "SA1310: C# Field must not contain an underscore", Justification = "By design: Structured name.")]
		private const int RunningState_NotStarted = 0;

		// Token: 0x0400009C RID: 156
		[SuppressMessage("Naming Rules", "SA1310: C# Field must not contain an underscore", Justification = "By design: Structured name.")]
		private const int RunningState_Running = 1;

		// Token: 0x0400009D RID: 157
		[SuppressMessage("Naming Rules", "SA1310: C# Field must not contain an underscore", Justification = "By design: Structured name.")]
		private const int RunningState_Stopped = 2;

		// Token: 0x0400009E RID: 158
		private readonly MetricAggregationManager aggregationManager;

		// Token: 0x0400009F RID: 159
		private readonly MetricManager metricManager;

		// Token: 0x040000A0 RID: 160
		private readonly TaskCompletionSource<bool> workerTaskCompletionControl;

		// Token: 0x040000A1 RID: 161
		private int runningState;

		// Token: 0x040000A2 RID: 162
		private Thread aggregationThread;
	}
}
