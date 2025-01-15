using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor
{
	// Token: 0x020000E7 RID: 231
	[DataContract]
	public sealed class WindowLimitDescriptor
	{
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x0000C805 File Offset: 0x0000AA05
		// (set) Token: 0x06000617 RID: 1559 RVA: 0x0000C80D File Offset: 0x0000AA0D
		[DataMember(IsRequired = true, EmitDefaultValue = false, Order = 1)]
		public int Count { get; set; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x0000C816 File Offset: 0x0000AA16
		// (set) Token: 0x06000619 RID: 1561 RVA: 0x0000C81E File Offset: 0x0000AA1E
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 2)]
		public string Calc { get; set; }
	}
}
