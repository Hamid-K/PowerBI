using System;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.DataShaping.InternalContracts.ExecutionMetadata
{
	// Token: 0x0200002B RID: 43
	public static class ExecutionMetricsServiceFactory
	{
		// Token: 0x060000FF RID: 255 RVA: 0x00003C14 File Offset: 0x00001E14
		public static IDataShapingExecutionMetricsService CreateExecutionMetricsService(ExecutionMetricsKind metricsKind, int? maxEventCount)
		{
			if (metricsKind != ExecutionMetricsKind.None)
			{
				return new DataShapingExecutionMetricsService(metricsKind, maxEventCount);
			}
			return NoOpDataShapingExecutionMetricsService.Instance;
		}
	}
}
