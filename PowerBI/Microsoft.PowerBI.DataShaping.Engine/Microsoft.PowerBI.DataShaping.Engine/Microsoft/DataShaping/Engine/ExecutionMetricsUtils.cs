using System;
using System.Diagnostics;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.Reporting.QueryDesign.Edm.Internal;

namespace Microsoft.DataShaping.Engine
{
	// Token: 0x02000016 RID: 22
	internal static class ExecutionMetricsUtils
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00003054 File Offset: 0x00001254
		internal static ExecutionMetricsMetadata DetermineExecutionMetrics(ITracer tracer, IFeatureSwitchProvider featureSwitches, ExecutionMetricsKind requestedMetrics, ExecutionMetricsKind allowedMetrics, EngineDataModel model)
		{
			if (!featureSwitches.IsEnabled(FeatureSwitchKind.QueryExecutionMetrics) || requestedMetrics == ExecutionMetricsKind.None)
			{
				return ExecutionMetricsMetadata.None;
			}
			ExecutionMetricsKind executionMetricsKind = requestedMetrics & ExecutionMetricsKind.All;
			if (requestedMetrics != executionMetricsKind)
			{
				tracer.SanitizedTrace(TraceLevel.Warning, "Request ExecutionMetricsKind: [{0}] contains an unknown value.  The unknown value will be ignored.", requestedMetrics);
			}
			ExecutionMetricsKind executionMetricsKind2 = executionMetricsKind & allowedMetrics;
			if (executionMetricsKind2 != executionMetricsKind)
			{
				tracer.SanitizedTrace(TraceLevel.Warning, "Request ExecutionMetricsKind: [{0}] contains a value not allowed by this host.  Allowed: [{1}].  Disallowed values will be ignored.", requestedMetrics, allowedMetrics);
			}
			if (executionMetricsKind2 != ExecutionMetricsKind.None && !executionMetricsKind2.HasFlag(ExecutionMetricsKind.Basic))
			{
				tracer.SanitizedTrace(TraceLevel.Warning, "Request ExecutionMetricsKind: [{0}] contains an optional value but does not contain Basic.  No metrics will be returned unless Basic is also requested.", requestedMetrics);
				executionMetricsKind2 = ExecutionMetricsKind.None;
			}
			RequestExecutionMetricsKind requestExecutionMetricsKind;
			if (featureSwitches.IsEnabled(FeatureSwitchKind.QDMConceptualSchema) ? (!model.Schema.GetDaxCapabilitiesAnnotation().SupportsExecutionMetrics) : (model.Model.ModelCapabilities.ExecutionMetrics == ExecutionMetricsType.NotSupported))
			{
				requestExecutionMetricsKind = RequestExecutionMetricsKind.None;
				if (executionMetricsKind2 != ExecutionMetricsKind.None)
				{
					tracer.SanitizedTrace(TraceLevel.Info, "The target AS server does not support execution metrics.  The response will only contain metrics from the DSE.");
				}
			}
			else
			{
				requestExecutionMetricsKind = executionMetricsKind2.ToDataExtensionExecutionMetricsKind();
			}
			ExecutionMetricsTelemetry executionMetricsTelemetry = new ExecutionMetricsTelemetry
			{
				Requested = requestedMetrics,
				Actual = executionMetricsKind2,
				Query = requestExecutionMetricsKind
			};
			return new ExecutionMetricsMetadata(executionMetricsKind2, requestExecutionMetricsKind, executionMetricsTelemetry);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000314C File Offset: 0x0000134C
		internal static RequestExecutionMetricsKind ToDataExtensionExecutionMetricsKind(this ExecutionMetricsKind metricsKind)
		{
			RequestExecutionMetricsKind requestExecutionMetricsKind = RequestExecutionMetricsKind.None;
			if (metricsKind.HasFlag(ExecutionMetricsKind.Basic))
			{
				requestExecutionMetricsKind |= RequestExecutionMetricsKind.Basic;
			}
			if (metricsKind.HasFlag(ExecutionMetricsKind.QueryText))
			{
				requestExecutionMetricsKind |= RequestExecutionMetricsKind.QueryText;
			}
			return requestExecutionMetricsKind;
		}
	}
}
