using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.ApplicationInsights.Metrics.Extensibility
{
	// Token: 0x02000042 RID: 66
	[SuppressMessage("Microsoft.Design", "CA1001: Types that own disposable fields should be disposable", Justification = "OK not to explicitly dispose a released SemaphoreSlim.")]
	internal class MemoryMetricTelemetryPipeline : IMetricTelemetryPipeline, IReadOnlyList<MetricAggregate>, IReadOnlyCollection<MetricAggregate>, IEnumerable<MetricAggregate>, IEnumerable
	{
		// Token: 0x0600023C RID: 572 RVA: 0x0000BC13 File Offset: 0x00009E13
		public MemoryMetricTelemetryPipeline()
			: this(1000)
		{
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000BC20 File Offset: 0x00009E20
		public MemoryMetricTelemetryPipeline(int countLimit)
		{
			if (countLimit <= 0)
			{
				throw new ArgumentOutOfRangeException("countLimit");
			}
			this.CountLimit = countLimit;
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000BC55 File Offset: 0x00009E55
		public int CountLimit { get; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000BC60 File Offset: 0x00009E60
		public int Count
		{
			get
			{
				this.updateLock.WaitAsync().GetAwaiter().GetResult();
				int count;
				try
				{
					count = this.metricAgregates.Count;
				}
				finally
				{
					this.updateLock.Release();
				}
				return count;
			}
		}

		// Token: 0x170000A7 RID: 167
		public MetricAggregate this[int index]
		{
			get
			{
				this.updateLock.WaitAsync().GetAwaiter().GetResult();
				MetricAggregate metricAggregate;
				try
				{
					metricAggregate = this.metricAgregates[index];
				}
				finally
				{
					this.updateLock.Release();
				}
				return metricAggregate;
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000BD08 File Offset: 0x00009F08
		public void Clear()
		{
			this.updateLock.WaitAsync().GetAwaiter().GetResult();
			try
			{
				this.metricAgregates.Clear();
			}
			finally
			{
				this.updateLock.Release();
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000BD58 File Offset: 0x00009F58
		public async Task TrackAsync(MetricAggregate metricAggregate, CancellationToken cancelToken)
		{
			Util.ValidateNotNull(metricAggregate, "metricAggregate");
			await this.updateLock.WaitAsync(cancelToken).ConfigureAwait(true);
			try
			{
				while (this.metricAgregates.Count >= this.CountLimit)
				{
					this.metricAgregates.RemoveAt(0);
				}
				this.metricAgregates.Add(metricAggregate);
			}
			finally
			{
				this.updateLock.Release();
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000BDAD File Offset: 0x00009FAD
		public Task FlushAsync(CancellationToken cancelToken)
		{
			return Task.FromResult<bool>(true);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000BDB8 File Offset: 0x00009FB8
		IEnumerator<MetricAggregate> IEnumerable<MetricAggregate>.GetEnumerator()
		{
			this.updateLock.WaitAsync().GetAwaiter().GetResult();
			IEnumerator<MetricAggregate> enumerator;
			try
			{
				enumerator = this.metricAgregates.GetEnumerator();
			}
			finally
			{
				this.updateLock.Release();
			}
			return enumerator;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000BE0C File Offset: 0x0000A00C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<MetricAggregate>)this).GetEnumerator();
		}

		// Token: 0x04000108 RID: 264
		public const int CountLimitDefault = 1000;

		// Token: 0x04000109 RID: 265
		private readonly SemaphoreSlim updateLock = new SemaphoreSlim(1);

		// Token: 0x0400010A RID: 266
		private readonly IList<MetricAggregate> metricAgregates = new List<MetricAggregate>();
	}
}
