using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000D7 RID: 215
	[DataContract(Name = "SelectionSlotItem", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class SelectionSlotItem : SlotItem
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x0000830C File Offset: 0x0000650C
		// (set) Token: 0x0600043C RID: 1084 RVA: 0x00008314 File Offset: 0x00006514
		[DataMember(IsRequired = true, Order = 1)]
		public IList<SlotValue> SlotValues { get; set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0000831D File Offset: 0x0000651D
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x00008325 File Offset: 0x00006525
		[DataMember(IsRequired = false, Order = 2, EmitDefaultValue = false)]
		public bool IsSimpleSelection { get; set; }
	}
}
