using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor
{
	// Token: 0x020000DE RID: 222
	[DataContract]
	public sealed class SampleLimitDescriptor
	{
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x0000C5D8 File Offset: 0x0000A7D8
		// (set) Token: 0x060005D5 RID: 1493 RVA: 0x0000C5E0 File Offset: 0x0000A7E0
		[DataMember(IsRequired = true, EmitDefaultValue = false, Order = 1)]
		public int Count { get; set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x0000C5E9 File Offset: 0x0000A7E9
		// (set) Token: 0x060005D7 RID: 1495 RVA: 0x0000C5F1 File Offset: 0x0000A7F1
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 2)]
		public string Calc { get; set; }
	}
}
