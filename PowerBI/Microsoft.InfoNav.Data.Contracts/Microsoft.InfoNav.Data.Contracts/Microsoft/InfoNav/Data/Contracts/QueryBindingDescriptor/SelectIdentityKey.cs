using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor
{
	// Token: 0x020000E3 RID: 227
	[DataContract]
	public sealed class SelectIdentityKey
	{
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x0000C75D File Offset: 0x0000A95D
		// (set) Token: 0x06000603 RID: 1539 RVA: 0x0000C765 File Offset: 0x0000A965
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public ConceptualPropertyReference Source { get; set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x0000C76E File Offset: 0x0000A96E
		// (set) Token: 0x06000605 RID: 1541 RVA: 0x0000C776 File Offset: 0x0000A976
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public string Calc { get; set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x0000C77F File Offset: 0x0000A97F
		// (set) Token: 0x06000607 RID: 1543 RVA: 0x0000C787 File Offset: 0x0000A987
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public bool IsSameAsSelect { get; set; }
	}
}
