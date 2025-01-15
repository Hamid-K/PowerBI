using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x02000116 RID: 278
	[DataContract]
	internal sealed class DataLimits
	{
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x0000F67E File Offset: 0x0000D87E
		// (set) Token: 0x06000755 RID: 1877 RVA: 0x0000F686 File Offset: 0x0000D886
		[DataMember(EmitDefaultValue = false, Order = 1)]
		internal IList<DataLimit> Limits { get; set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x0000F68F File Offset: 0x0000D88F
		// (set) Token: 0x06000757 RID: 1879 RVA: 0x0000F697 File Offset: 0x0000D897
		[DataMember(EmitDefaultValue = false, Order = 2)]
		internal DataBinding DataBinding { get; set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x0000F6A0 File Offset: 0x0000D8A0
		// (set) Token: 0x06000759 RID: 1881 RVA: 0x0000F6A8 File Offset: 0x0000D8A8
		[DataMember(EmitDefaultValue = false, Order = 3)]
		internal IList<TelemetryItem> TelemetryItems { get; set; }
	}
}
