using System;
using Microsoft.InfoNav.Data.Contracts.ExecutionMetadata;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.DataShaping.InternalContracts.ExecutionMetadata
{
	// Token: 0x0200002C RID: 44
	public interface IDataShapingExecutionMetricsService : IExecutionMetricsService
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000100 RID: 256
		ExecutionMetricsKind RequestedMetricsKind { get; }
	}
}
