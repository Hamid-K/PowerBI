using System;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x02000122 RID: 290
	[DataContract]
	internal sealed class DataSource
	{
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x0000FED8 File Offset: 0x0000E0D8
		// (set) Token: 0x060007E6 RID: 2022 RVA: 0x0000FEE0 File Offset: 0x0000E0E0
		[DataMember(EmitDefaultValue = false, Order = 1)]
		internal string Id { get; set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x0000FEE9 File Offset: 0x0000E0E9
		// (set) Token: 0x060007E8 RID: 2024 RVA: 0x0000FEF1 File Offset: 0x0000E0F1
		[DataMember(EmitDefaultValue = false, Order = 2)]
		internal string DataSourceName { get; set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x0000FEFA File Offset: 0x0000E0FA
		// (set) Token: 0x060007EA RID: 2026 RVA: 0x0000FF02 File Offset: 0x0000E102
		[DataMember(EmitDefaultValue = false, Order = 3)]
		internal Collation Collation { get; set; }
	}
}
