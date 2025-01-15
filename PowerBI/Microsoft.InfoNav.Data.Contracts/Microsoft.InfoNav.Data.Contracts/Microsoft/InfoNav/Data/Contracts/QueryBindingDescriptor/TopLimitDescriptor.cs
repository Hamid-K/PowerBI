using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor
{
	// Token: 0x020000E5 RID: 229
	[DataContract]
	public sealed class TopLimitDescriptor
	{
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x0000C7C2 File Offset: 0x0000A9C2
		// (set) Token: 0x0600060F RID: 1551 RVA: 0x0000C7CA File Offset: 0x0000A9CA
		[DataMember(IsRequired = true, EmitDefaultValue = false, Order = 1)]
		public int Count { get; set; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x0000C7D3 File Offset: 0x0000A9D3
		// (set) Token: 0x06000611 RID: 1553 RVA: 0x0000C7DB File Offset: 0x0000A9DB
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 2)]
		public string Calc { get; set; }
	}
}
