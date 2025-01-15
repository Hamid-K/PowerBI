using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor
{
	// Token: 0x020000D3 RID: 211
	[DataContract]
	public sealed class DataShapeLimits
	{
		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x0000C3B4 File Offset: 0x0000A5B4
		// (set) Token: 0x06000584 RID: 1412 RVA: 0x0000C3BC File Offset: 0x0000A5BC
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 1)]
		public DataShapeLimitDescriptor Primary { get; set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x0000C3C5 File Offset: 0x0000A5C5
		// (set) Token: 0x06000586 RID: 1414 RVA: 0x0000C3CD File Offset: 0x0000A5CD
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 2)]
		public DataShapeLimitDescriptor Secondary { get; set; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x0000C3D6 File Offset: 0x0000A5D6
		// (set) Token: 0x06000588 RID: 1416 RVA: 0x0000C3DE File Offset: 0x0000A5DE
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 3)]
		public DataShapeLimitDescriptor Intersection { get; set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x0000C3E7 File Offset: 0x0000A5E7
		// (set) Token: 0x0600058A RID: 1418 RVA: 0x0000C3EF File Offset: 0x0000A5EF
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 4)]
		public IList<DataShapeLimitDescriptor> Scoped { get; set; }
	}
}
