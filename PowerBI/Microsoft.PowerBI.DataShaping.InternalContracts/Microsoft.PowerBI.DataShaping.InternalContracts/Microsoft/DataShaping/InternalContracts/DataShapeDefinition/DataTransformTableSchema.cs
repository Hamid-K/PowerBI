using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x02000128 RID: 296
	[DataContract]
	internal sealed class DataTransformTableSchema
	{
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x00010007 File Offset: 0x0000E207
		// (set) Token: 0x0600080A RID: 2058 RVA: 0x0001000F File Offset: 0x0000E20F
		[DataMember(EmitDefaultValue = false, Order = 1)]
		internal IList<DataTransformColumn> Columns { get; set; }
	}
}
