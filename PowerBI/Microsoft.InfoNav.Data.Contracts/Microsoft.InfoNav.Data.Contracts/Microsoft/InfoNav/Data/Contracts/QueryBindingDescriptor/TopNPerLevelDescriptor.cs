using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor
{
	// Token: 0x020000E6 RID: 230
	[DataContract]
	public sealed class TopNPerLevelDescriptor
	{
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x0000C7EC File Offset: 0x0000A9EC
		// (set) Token: 0x06000614 RID: 1556 RVA: 0x0000C7F4 File Offset: 0x0000A9F4
		[DataMember(IsRequired = true, EmitDefaultValue = false, Order = 10)]
		public int Count { get; set; }
	}
}
