using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor
{
	// Token: 0x020000D7 RID: 215
	[DataContract]
	public sealed class ExtensionMeasureBinding
	{
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x0000C4A0 File Offset: 0x0000A6A0
		// (set) Token: 0x060005A0 RID: 1440 RVA: 0x0000C4A8 File Offset: 0x0000A6A8
		[DataMember(IsRequired = true, Order = 1)]
		public string Name { get; set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x0000C4B1 File Offset: 0x0000A6B1
		// (set) Token: 0x060005A2 RID: 1442 RVA: 0x0000C4B9 File Offset: 0x0000A6B9
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 2)]
		public string NativeQueryName { get; set; }
	}
}
