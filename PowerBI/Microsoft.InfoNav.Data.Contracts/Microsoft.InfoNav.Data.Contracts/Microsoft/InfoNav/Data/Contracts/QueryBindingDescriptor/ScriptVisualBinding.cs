using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor
{
	// Token: 0x020000DF RID: 223
	[DataContract]
	public class ScriptVisualBinding
	{
		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0000C602 File Offset: 0x0000A802
		// (set) Token: 0x060005DA RID: 1498 RVA: 0x0000C60A File Offset: 0x0000A80A
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public string ImageCalculationId { get; set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x0000C613 File Offset: 0x0000A813
		// (set) Token: 0x060005DC RID: 1500 RVA: 0x0000C61B File Offset: 0x0000A81B
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public string PayloadCalculationId { get; set; }
	}
}
