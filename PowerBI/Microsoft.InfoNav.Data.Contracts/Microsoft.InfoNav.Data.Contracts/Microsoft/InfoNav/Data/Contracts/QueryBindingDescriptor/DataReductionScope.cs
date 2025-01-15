using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor
{
	// Token: 0x020000CC RID: 204
	[DataContract]
	public sealed class DataReductionScope
	{
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x0000C1B1 File Offset: 0x0000A3B1
		// (set) Token: 0x06000547 RID: 1351 RVA: 0x0000C1B9 File Offset: 0x0000A3B9
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 1)]
		public IList<int> Primary { get; set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x0000C1C2 File Offset: 0x0000A3C2
		// (set) Token: 0x06000549 RID: 1353 RVA: 0x0000C1CA File Offset: 0x0000A3CA
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 2)]
		public IList<int> Secondary { get; set; }
	}
}
