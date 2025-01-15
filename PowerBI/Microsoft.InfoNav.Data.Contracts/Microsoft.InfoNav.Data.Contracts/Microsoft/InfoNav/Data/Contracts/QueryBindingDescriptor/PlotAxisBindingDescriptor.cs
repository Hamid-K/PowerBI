using System;
using System.Runtime.Serialization;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor
{
	// Token: 0x020000DB RID: 219
	[DataContract]
	public sealed class PlotAxisBindingDescriptor
	{
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x0000C52F File Offset: 0x0000A72F
		// (set) Token: 0x060005C1 RID: 1473 RVA: 0x0000C537 File Offset: 0x0000A737
		[DataMember(IsRequired = true, EmitDefaultValue = true, Order = 10)]
		public DataReductionPlotAxisTransform Transform { get; set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x0000C540 File Offset: 0x0000A740
		// (set) Token: 0x060005C3 RID: 1475 RVA: 0x0000C548 File Offset: 0x0000A748
		[DataMember(IsRequired = true, EmitDefaultValue = true, Order = 20)]
		public string Applied { get; set; }
	}
}
