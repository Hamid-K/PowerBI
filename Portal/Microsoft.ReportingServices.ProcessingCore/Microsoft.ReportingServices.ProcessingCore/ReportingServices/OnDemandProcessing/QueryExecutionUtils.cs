using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x020007F6 RID: 2038
	internal static class QueryExecutionUtils
	{
		// Token: 0x060071CF RID: 29135 RVA: 0x001D88B8 File Offset: 0x001D6AB8
		internal static void DisposeDataExtensionObject<T>(ref T obj, string objectType, string dataSetName) where T : class, IDisposable
		{
			QueryExecutionUtils.DisposeDataExtensionObject<T>(ref obj, objectType, dataSetName, null, null);
		}

		// Token: 0x060071D0 RID: 29136 RVA: 0x001D88D8 File Offset: 0x001D6AD8
		internal static void DisposeDataExtensionObject<T>(ref T obj, string objectType, string dataSetName, DataProcessingMetrics executionMetrics, DataProcessingMetrics.MetricType? metricType) where T : class, IDisposable
		{
			if (obj != null)
			{
				if (metricType != null)
				{
					executionMetrics.StartTimer(metricType.Value);
				}
				try
				{
					obj.Dispose();
				}
				catch (RSException)
				{
					throw;
				}
				catch (Exception ex)
				{
					if (AsynchronousExceptionDetection.IsStoppingException(ex))
					{
						throw;
					}
					Global.Tracer.Trace(TraceLevel.Warning, string.Concat(new string[]
					{
						"Error occurred while disposing the ",
						objectType,
						" for DataSet '",
						dataSetName,
						"'. Details: ",
						ex.ToString()
					}));
				}
				finally
				{
					obj = default(T);
					if (metricType != null)
					{
						executionMetrics.RecordTimerMeasurementWithUpdatedTotal(metricType.Value);
					}
				}
			}
		}
	}
}
