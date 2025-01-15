using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor
{
	// Token: 0x020000DA RID: 218
	[DataContract]
	public sealed class OverlappingPointsSampleLimitDescriptor
	{
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x0000C4F4 File Offset: 0x0000A6F4
		// (set) Token: 0x060005BA RID: 1466 RVA: 0x0000C4FC File Offset: 0x0000A6FC
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 1)]
		public int Count { get; set; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x0000C505 File Offset: 0x0000A705
		// (set) Token: 0x060005BC RID: 1468 RVA: 0x0000C50D File Offset: 0x0000A70D
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 2)]
		public PlotAxisBindingDescriptor X { get; set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x0000C516 File Offset: 0x0000A716
		// (set) Token: 0x060005BE RID: 1470 RVA: 0x0000C51E File Offset: 0x0000A71E
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 3)]
		public PlotAxisBindingDescriptor Y { get; set; }
	}
}
