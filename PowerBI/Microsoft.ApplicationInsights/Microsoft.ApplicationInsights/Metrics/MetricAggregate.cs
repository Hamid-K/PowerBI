using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.ApplicationInsights.Metrics
{
	// Token: 0x0200002C RID: 44
	public class MetricAggregate
	{
		// Token: 0x06000181 RID: 385 RVA: 0x00008DFC File Offset: 0x00006FFC
		public MetricAggregate(string metricNamespace, string metricId, string aggregationKindMoniker)
		{
			Util.ValidateNotNull(metricNamespace, "metricNamespace");
			Util.ValidateNotNull(metricId, "metricId");
			Util.ValidateNotNull(aggregationKindMoniker, "aggregationKindMoniker");
			this.MetricNamespace = metricNamespace;
			this.MetricId = metricId;
			this.AggregationKindMoniker = aggregationKindMoniker;
			this.aggregationPeriodStart = default(DateTimeOffset);
			this.aggregationPeriodDuration = default(TimeSpan);
			this.Dimensions = new ConcurrentDictionary<string, string>();
			this.Data = new ConcurrentDictionary<string, object>();
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00008E7E File Offset: 0x0000707E
		public string MetricNamespace { get; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00008E86 File Offset: 0x00007086
		public string MetricId { get; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00008E8E File Offset: 0x0000708E
		public string AggregationKindMoniker { get; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00008E98 File Offset: 0x00007098
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00008EDC File Offset: 0x000070DC
		public DateTimeOffset AggregationPeriodStart
		{
			get
			{
				object obj = this.updateLock;
				DateTimeOffset dateTimeOffset;
				lock (obj)
				{
					dateTimeOffset = this.aggregationPeriodStart;
				}
				return dateTimeOffset;
			}
			set
			{
				object obj = this.updateLock;
				lock (obj)
				{
					this.aggregationPeriodStart = value;
				}
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00008F20 File Offset: 0x00007120
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00008F64 File Offset: 0x00007164
		public TimeSpan AggregationPeriodDuration
		{
			get
			{
				object obj = this.updateLock;
				TimeSpan timeSpan;
				lock (obj)
				{
					timeSpan = this.aggregationPeriodDuration;
				}
				return timeSpan;
			}
			set
			{
				object obj = this.updateLock;
				lock (obj)
				{
					this.aggregationPeriodDuration = value;
				}
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00008FA8 File Offset: 0x000071A8
		public IDictionary<string, string> Dimensions { get; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00008FB0 File Offset: 0x000071B0
		public IDictionary<string, object> Data { get; }

		// Token: 0x0600018B RID: 395 RVA: 0x00008FB8 File Offset: 0x000071B8
		public T GetDataValue<T>(string dataKey, T defaultValue)
		{
			object obj;
			if (this.Data.TryGetValue(dataKey, out obj))
			{
				try
				{
					return (T)((object)Convert.ChangeType(obj, typeof(T), CultureInfo.InvariantCulture));
				}
				catch
				{
					return defaultValue;
				}
				return defaultValue;
			}
			return defaultValue;
		}

		// Token: 0x040000AF RID: 175
		private readonly object updateLock = new object();

		// Token: 0x040000B0 RID: 176
		private DateTimeOffset aggregationPeriodStart;

		// Token: 0x040000B1 RID: 177
		private TimeSpan aggregationPeriodDuration;
	}
}
