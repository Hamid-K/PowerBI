using System;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000DA RID: 218
	[DataContract(Name = "EntitySlotValue", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class EntitySlotValue : FixedSlotValue
	{
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x00008368 File Offset: 0x00006568
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x00008370 File Offset: 0x00006570
		[DataMember(IsRequired = true, Order = 1)]
		public string Entity { get; set; }
	}
}
