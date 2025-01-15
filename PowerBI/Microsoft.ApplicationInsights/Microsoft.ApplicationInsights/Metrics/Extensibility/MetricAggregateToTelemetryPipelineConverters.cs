using System;
using System.Collections.Concurrent;

namespace Microsoft.ApplicationInsights.Metrics.Extensibility
{
	// Token: 0x02000044 RID: 68
	internal sealed class MetricAggregateToTelemetryPipelineConverters
	{
		// Token: 0x0600024D RID: 589 RVA: 0x0000C6A8 File Offset: 0x0000A8A8
		public void Add(Type pipelineType, string aggregationKindMoniker, IMetricAggregateToTelemetryPipelineConverter converter)
		{
			MetricAggregateToTelemetryPipelineConverters.ValidateKeys(pipelineType, aggregationKindMoniker);
			Util.ValidateNotNull(converter, "converter");
			this.pipelineTable.GetOrAdd(pipelineType, new ConcurrentDictionary<string, IMetricAggregateToTelemetryPipelineConverter>())[aggregationKindMoniker] = converter;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000C6D4 File Offset: 0x0000A8D4
		public bool TryGet(Type pipelineType, string aggregationKindMoniker, out IMetricAggregateToTelemetryPipelineConverter converter)
		{
			MetricAggregateToTelemetryPipelineConverters.ValidateKeys(pipelineType, aggregationKindMoniker);
			ConcurrentDictionary<string, IMetricAggregateToTelemetryPipelineConverter> concurrentDictionary;
			if (!this.pipelineTable.TryGetValue(pipelineType, out concurrentDictionary))
			{
				converter = null;
				return false;
			}
			return concurrentDictionary.TryGetValue(aggregationKindMoniker, out converter);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000C705 File Offset: 0x0000A905
		private static void ValidateKeys(Type pipelineType, string aggregationKindMoniker)
		{
			Util.ValidateNotNull(pipelineType, "pipelineType");
			Util.ValidateNotNullOrWhitespace(aggregationKindMoniker, "aggregationKindMoniker");
		}

		// Token: 0x0400010D RID: 269
		public static readonly MetricAggregateToTelemetryPipelineConverters Registry = new MetricAggregateToTelemetryPipelineConverters();

		// Token: 0x0400010E RID: 270
		private ConcurrentDictionary<Type, ConcurrentDictionary<string, IMetricAggregateToTelemetryPipelineConverter>> pipelineTable = new ConcurrentDictionary<Type, ConcurrentDictionary<string, IMetricAggregateToTelemetryPipelineConverter>>();
	}
}
