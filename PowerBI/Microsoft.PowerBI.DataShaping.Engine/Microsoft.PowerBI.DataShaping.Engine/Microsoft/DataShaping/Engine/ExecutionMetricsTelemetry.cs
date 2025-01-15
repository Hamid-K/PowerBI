using System;
using System.Runtime.Serialization;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.DataShaping.Engine
{
	// Token: 0x02000015 RID: 21
	[DataContract]
	internal sealed class ExecutionMetricsTelemetry
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002FF4 File Offset: 0x000011F4
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00002FFC File Offset: 0x000011FC
		[DataMember(Name = "Requested", IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public ExecutionMetricsKind Requested { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003005 File Offset: 0x00001205
		// (set) Token: 0x0600007F RID: 127 RVA: 0x0000300D File Offset: 0x0000120D
		[DataMember(Name = "Actual", IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public ExecutionMetricsKind Actual { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003016 File Offset: 0x00001216
		// (set) Token: 0x06000081 RID: 129 RVA: 0x0000301E File Offset: 0x0000121E
		[DataMember(Name = "Query", IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public RequestExecutionMetricsKind Query { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003027 File Offset: 0x00001227
		// (set) Token: 0x06000083 RID: 131 RVA: 0x0000302F File Offset: 0x0000122F
		[DataMember(Name = "Events", IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public int EventCount { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003038 File Offset: 0x00001238
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00003040 File Offset: 0x00001240
		[DataMember(Name = "Truncated", IsRequired = false, EmitDefaultValue = false, Order = 50)]
		public bool Truncated { get; set; }
	}
}
