using System;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x02000124 RID: 292
	[DataContract]
	internal sealed class DataTransformColumn
	{
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x0000FF5F File Offset: 0x0000E15F
		// (set) Token: 0x060007F6 RID: 2038 RVA: 0x0000FF67 File Offset: 0x0000E167
		[DataMember(EmitDefaultValue = false, Order = 1)]
		internal string Name { get; set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x0000FF70 File Offset: 0x0000E170
		// (set) Token: 0x060007F8 RID: 2040 RVA: 0x0000FF78 File Offset: 0x0000E178
		[DataMember(EmitDefaultValue = false, Order = 2)]
		internal string Role { get; set; }
	}
}
