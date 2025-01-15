using System;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x02000126 RID: 294
	[DataContract]
	internal sealed class DataTransformOutput
	{
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x0000FFC4 File Offset: 0x0000E1C4
		// (set) Token: 0x06000802 RID: 2050 RVA: 0x0000FFCC File Offset: 0x0000E1CC
		[DataMember(EmitDefaultValue = false, Order = 1)]
		internal ResultTable Table { get; set; }
	}
}
