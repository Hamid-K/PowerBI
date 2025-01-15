using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor
{
	// Token: 0x020000D4 RID: 212
	[DataContract]
	public sealed class DynamicFormatBinding
	{
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x0000C400 File Offset: 0x0000A600
		// (set) Token: 0x0600058D RID: 1421 RVA: 0x0000C408 File Offset: 0x0000A608
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public string Format { get; set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x0000C411 File Offset: 0x0000A611
		// (set) Token: 0x0600058F RID: 1423 RVA: 0x0000C419 File Offset: 0x0000A619
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public string Culture { get; set; }
	}
}
