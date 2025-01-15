using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Metrics.Extensibility;

namespace Microsoft.ApplicationInsights.Metrics
{
	// Token: 0x02000028 RID: 40
	internal class MeasurementAggregateToApplicationInsightsPipelineConverter : MetricAggregateToApplicationInsightsPipelineConverterBase
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00007F22 File Offset: 0x00006122
		public override string AggregationKindMoniker
		{
			get
			{
				return "Microsoft.Azure.Measurement";
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00007F2C File Offset: 0x0000612C
		[SuppressMessage("Microsoft.Design", "CA1062", Justification = "telemetryItem and aggregate are validated by base")]
		protected override void PopulateDataValues(MetricTelemetry telemetryItem, MetricAggregate aggregate)
		{
			telemetryItem.Count = new int?(aggregate.GetDataValue<int>("Count", 0));
			telemetryItem.Sum = aggregate.GetDataValue<double>("Sum", 0.0);
			telemetryItem.Min = new double?(aggregate.GetDataValue<double>("Min", 0.0));
			telemetryItem.Max = new double?(aggregate.GetDataValue<double>("Max", 0.0));
			telemetryItem.StandardDeviation = new double?(aggregate.GetDataValue<double>("StdDev", 0.0));
		}
	}
}
