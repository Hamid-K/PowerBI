using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor
{
	// Token: 0x020000DC RID: 220
	[DataContract]
	public sealed class QueryBindingDescriptor
	{
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x0000C559 File Offset: 0x0000A759
		// (set) Token: 0x060005C6 RID: 1478 RVA: 0x0000C561 File Offset: 0x0000A761
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 1)]
		public SelectBinding[] Select { get; set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x0000C56A File Offset: 0x0000A76A
		// (set) Token: 0x060005C8 RID: 1480 RVA: 0x0000C572 File Offset: 0x0000A772
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 2)]
		public DataShapeExpressions Expressions { get; set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x0000C57B File Offset: 0x0000A77B
		// (set) Token: 0x060005CA RID: 1482 RVA: 0x0000C583 File Offset: 0x0000A783
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 3)]
		public DataShapeLimits Limits { get; set; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x0000C58C File Offset: 0x0000A78C
		// (set) Token: 0x060005CC RID: 1484 RVA: 0x0000C594 File Offset: 0x0000A794
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 4)]
		public ScriptVisualBinding ScriptVisualBinding { get; set; }

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x0000C59D File Offset: 0x0000A79D
		// (set) Token: 0x060005CE RID: 1486 RVA: 0x0000C5A5 File Offset: 0x0000A7A5
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 5)]
		public int? Version { get; set; }

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x0000C5AE File Offset: 0x0000A7AE
		// (set) Token: 0x060005D0 RID: 1488 RVA: 0x0000C5B6 File Offset: 0x0000A7B6
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 6)]
		public string Name { get; set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x0000C5BF File Offset: 0x0000A7BF
		// (set) Token: 0x060005D2 RID: 1490 RVA: 0x0000C5C7 File Offset: 0x0000A7C7
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 7)]
		public ExtensionSchemaBinding ExtensionSchema { get; set; }
	}
}
