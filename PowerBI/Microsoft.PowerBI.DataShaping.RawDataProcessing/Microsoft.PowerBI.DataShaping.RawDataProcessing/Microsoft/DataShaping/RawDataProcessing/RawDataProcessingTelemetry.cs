using System;
using System.Runtime.Serialization;
using Microsoft.DataShaping.Processing.QueryExecutionCommon;

namespace Microsoft.DataShaping.RawDataProcessing
{
	// Token: 0x0200000C RID: 12
	[DataContract]
	internal sealed class RawDataProcessingTelemetry
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000025C3 File Offset: 0x000007C3
		// (set) Token: 0x0600001C RID: 28 RVA: 0x000025CB File Offset: 0x000007CB
		[DataMember(Name = "QueryExecutionStats", EmitDefaultValue = false, Order = 10)]
		internal QueryExecutionStatistics QueryExecutionStats { get; set; }
	}
}
