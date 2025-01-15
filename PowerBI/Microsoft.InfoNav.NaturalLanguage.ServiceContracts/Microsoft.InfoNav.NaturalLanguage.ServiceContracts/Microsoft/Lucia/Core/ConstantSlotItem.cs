using System;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000D6 RID: 214
	[DataContract(Name = "ConstantSlotItem", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class ConstantSlotItem : SlotItem
	{
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x000082F3 File Offset: 0x000064F3
		// (set) Token: 0x06000439 RID: 1081 RVA: 0x000082FB File Offset: 0x000064FB
		[DataMember(IsRequired = true, Order = 1)]
		public FixedSlotValue SlotValue { get; set; }
	}
}
