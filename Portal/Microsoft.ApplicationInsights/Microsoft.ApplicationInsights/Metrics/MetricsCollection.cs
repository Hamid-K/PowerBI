using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.ApplicationInsights.Metrics
{
	// Token: 0x02000031 RID: 49
	public sealed class MetricsCollection : ICollection<Metric>, IEnumerable<Metric>, IEnumerable
	{
		// Token: 0x060001C1 RID: 449 RVA: 0x00009F6C File Offset: 0x0000816C
		internal MetricsCollection(MetricManager metricManager)
		{
			Util.ValidateNotNull(metricManager, "metricManager");
			this.metricManager = metricManager;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00009F91 File Offset: 0x00008191
		public int Count
		{
			get
			{
				return this.metrics.Count;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00009F9E File Offset: 0x0000819E
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00009FA4 File Offset: 0x000081A4
		public Metric GetOrCreate(MetricIdentifier metricIdentifier, MetricConfiguration metricConfiguration)
		{
			Util.ValidateNotNull(metricIdentifier, "metricIdentifier");
			Metric orAdd = this.metrics.GetOrAdd(metricIdentifier, (MetricIdentifier key) => new Metric(this.metricManager, metricIdentifier, metricConfiguration ?? MetricConfigurations.Common.Default()));
			if (metricConfiguration != null && !orAdd.configuration.Equals(metricConfiguration))
			{
				throw new ArgumentException("A Metric with the specified Namespace, Id and dimension names already exists, but it has a configuration that is different from the specified configuration. You may not change configurations once a metric was created for the first time. Either specify the same configuration every time, or specify 'null' during every invocation except the first one. 'Null' will match against any previously specified configuration when retrieving existing metrics, or fall back to" + global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create(" the default when creating new metrics. ({0} = \"{1}\".)", new object[]
				{
					"metricIdentifier",
					metricIdentifier.ToString()
				})));
			}
			return orAdd;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000A051 File Offset: 0x00008251
		public bool TryGet(MetricIdentifier metricIdentifier, out Metric metric)
		{
			Util.ValidateNotNull(metricIdentifier, "metricIdentifier");
			return this.metrics.TryGetValue(metricIdentifier, out metric);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000A06B File Offset: 0x0000826B
		public void Clear()
		{
			this.metrics.Clear();
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000A078 File Offset: 0x00008278
		public bool Contains(Metric metric)
		{
			return metric != null && this.metrics.ContainsKey(metric.Identifier);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000A090 File Offset: 0x00008290
		public bool Contains(MetricIdentifier metricIdentifier)
		{
			return metricIdentifier != null && this.metrics.ContainsKey(metricIdentifier);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000A0A3 File Offset: 0x000082A3
		public void CopyTo(Metric[] array, int arrayIndex)
		{
			Util.ValidateNotNull(array, "array");
			if (arrayIndex < 0 || arrayIndex >= array.Length)
			{
				throw new ArgumentOutOfRangeException("arrayIndex");
			}
			this.metrics.Values.CopyTo(array, arrayIndex);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000A0D8 File Offset: 0x000082D8
		public bool Remove(Metric metric)
		{
			Metric metric2;
			return metric != null && this.metrics.TryRemove(metric.Identifier, out metric2);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000A100 File Offset: 0x00008300
		public bool Remove(MetricIdentifier metricIdentifier)
		{
			Metric metric;
			return this.Remove(metricIdentifier, out metric);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000A116 File Offset: 0x00008316
		public bool Remove(MetricIdentifier metricIdentifier, out Metric removedMetric)
		{
			if (metricIdentifier == null)
			{
				removedMetric = null;
				return false;
			}
			return this.metrics.TryRemove(metricIdentifier, out removedMetric);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000A12D File Offset: 0x0000832D
		public IEnumerator<Metric> GetEnumerator()
		{
			return this.metrics.Values.GetEnumerator();
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000A13F File Offset: 0x0000833F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000A147 File Offset: 0x00008347
		void ICollection<Metric>.Add(Metric unsupported)
		{
			throw new NotSupportedException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("The Add(..) method is not supported by this {0}.", new object[] { "MetricsCollection" })) + " To add a new metric, use the GetOrCreate(..) method.");
		}

		// Token: 0x040000D2 RID: 210
		private readonly MetricManager metricManager;

		// Token: 0x040000D3 RID: 211
		private readonly ConcurrentDictionary<MetricIdentifier, Metric> metrics = new ConcurrentDictionary<MetricIdentifier, Metric>();
	}
}
