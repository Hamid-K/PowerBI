using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.ExecutionMetadata
{
	// Token: 0x020000F1 RID: 241
	[DataContract]
	public sealed class ExecutionMetrics
	{
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x0000D516 File Offset: 0x0000B716
		// (set) Token: 0x0600065F RID: 1631 RVA: 0x0000D51E File Offset: 0x0000B71E
		[DataMember(IsRequired = true, Order = 0)]
		public string Version { get; set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x0000D527 File Offset: 0x0000B727
		// (set) Token: 0x06000661 RID: 1633 RVA: 0x0000D52F File Offset: 0x0000B72F
		[DataMember(IsRequired = true, Order = 10)]
		public List<ExecutionEvent> Events { get; set; }
	}
}
