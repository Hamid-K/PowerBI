using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor
{
	// Token: 0x020000D5 RID: 213
	[DataContract]
	public sealed class ExtensionColumnBinding
	{
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x0000C42A File Offset: 0x0000A62A
		// (set) Token: 0x06000592 RID: 1426 RVA: 0x0000C432 File Offset: 0x0000A632
		[DataMember(IsRequired = true, Order = 1)]
		public string Name { get; set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x0000C43B File Offset: 0x0000A63B
		// (set) Token: 0x06000594 RID: 1428 RVA: 0x0000C443 File Offset: 0x0000A643
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 2)]
		public string NativeQueryName { get; set; }
	}
}
