using System;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.DataShaping.Engine
{
	// Token: 0x02000017 RID: 23
	internal struct ExecutionMetricsMetadata
	{
		// Token: 0x06000089 RID: 137 RVA: 0x0000318A File Offset: 0x0000138A
		public ExecutionMetricsMetadata(ExecutionMetricsKind effectiveMetrics, RequestExecutionMetricsKind effectiveCommandMetrics, ExecutionMetricsTelemetry telemetry)
		{
			this.EffectiveMetrics = effectiveMetrics;
			this.EffectiveCommandMetrics = effectiveCommandMetrics;
			this.Telemetry = telemetry;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000031A4 File Offset: 0x000013A4
		public static ExecutionMetricsMetadata None
		{
			get
			{
				return default(ExecutionMetricsMetadata);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000031BA File Offset: 0x000013BA
		public readonly ExecutionMetricsKind EffectiveMetrics { get; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000031C2 File Offset: 0x000013C2
		public readonly RequestExecutionMetricsKind EffectiveCommandMetrics { get; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000031CA File Offset: 0x000013CA
		public readonly ExecutionMetricsTelemetry Telemetry { get; }
	}
}
