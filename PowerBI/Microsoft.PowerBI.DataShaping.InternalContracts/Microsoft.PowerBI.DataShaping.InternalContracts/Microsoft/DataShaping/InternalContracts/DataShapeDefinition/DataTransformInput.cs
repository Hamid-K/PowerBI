using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x02000125 RID: 293
	[DataContract]
	internal sealed class DataTransformInput
	{
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x0000FF89 File Offset: 0x0000E189
		// (set) Token: 0x060007FB RID: 2043 RVA: 0x0000FF91 File Offset: 0x0000E191
		[DataMember(EmitDefaultValue = false, Order = 1)]
		internal string TableId { get; set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x0000FF9A File Offset: 0x0000E19A
		// (set) Token: 0x060007FD RID: 2045 RVA: 0x0000FFA2 File Offset: 0x0000E1A2
		[DataMember(EmitDefaultValue = false, Order = 2)]
		internal DataTransformTableSchema Schema { get; set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x0000FFAB File Offset: 0x0000E1AB
		// (set) Token: 0x060007FF RID: 2047 RVA: 0x0000FFB3 File Offset: 0x0000E1B3
		[DataMember(EmitDefaultValue = false, Order = 3)]
		internal IList<DataTransformParameter> Parameters { get; set; }
	}
}
