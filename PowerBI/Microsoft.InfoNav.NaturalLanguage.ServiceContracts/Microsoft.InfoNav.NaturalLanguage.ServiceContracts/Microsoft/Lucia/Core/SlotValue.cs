using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000D8 RID: 216
	[DataContract(Name = "SlotValue", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	[KnownType(typeof(EntitySlotValue))]
	[KnownType(typeof(StringSlotValue))]
	[KnownType(typeof(EdmPropertySlotValue))]
	[KnownType(typeof(InputSlotValue))]
	public abstract class SlotValue
	{
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x00008336 File Offset: 0x00006536
		// (set) Token: 0x06000441 RID: 1089 RVA: 0x0000833E File Offset: 0x0000653E
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 2)]
		public IDictionary<string, IList<int>> SlotValueFilters { get; set; }
	}
}
