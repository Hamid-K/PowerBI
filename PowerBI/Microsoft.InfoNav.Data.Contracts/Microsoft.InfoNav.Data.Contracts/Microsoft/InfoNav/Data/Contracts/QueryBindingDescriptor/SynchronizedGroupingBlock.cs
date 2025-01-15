using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor
{
	// Token: 0x020000E4 RID: 228
	[DataContract]
	public sealed class SynchronizedGroupingBlock
	{
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x0000C798 File Offset: 0x0000A998
		// (set) Token: 0x0600060A RID: 1546 RVA: 0x0000C7A0 File Offset: 0x0000A9A0
		[DataMember(IsRequired = true, EmitDefaultValue = false, Order = 10)]
		public string DataShape { get; set; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x0000C7A9 File Offset: 0x0000A9A9
		// (set) Token: 0x0600060C RID: 1548 RVA: 0x0000C7B1 File Offset: 0x0000A9B1
		[DataMember(IsRequired = true, EmitDefaultValue = false, Order = 20)]
		public IList<int> Groupings { get; set; }
	}
}
