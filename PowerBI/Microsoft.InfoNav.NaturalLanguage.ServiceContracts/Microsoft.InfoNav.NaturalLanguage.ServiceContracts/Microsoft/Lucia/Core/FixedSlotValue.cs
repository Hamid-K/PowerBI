using System;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000D9 RID: 217
	[DataContract(Name = "FixedSlotValue", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public abstract class FixedSlotValue : SlotValue
	{
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x0000834F File Offset: 0x0000654F
		// (set) Token: 0x06000444 RID: 1092 RVA: 0x00008357 File Offset: 0x00006557
		[DataMember(IsRequired = true, Order = 1)]
		public string DisplayText { get; set; }
	}
}
