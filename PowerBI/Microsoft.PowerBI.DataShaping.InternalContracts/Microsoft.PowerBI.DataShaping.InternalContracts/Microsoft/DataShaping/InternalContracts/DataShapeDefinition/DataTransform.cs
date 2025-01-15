using System;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x02000123 RID: 291
	[DataContract]
	internal sealed class DataTransform
	{
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x0000FF13 File Offset: 0x0000E113
		// (set) Token: 0x060007ED RID: 2029 RVA: 0x0000FF1B File Offset: 0x0000E11B
		[DataMember(EmitDefaultValue = false, Order = 1)]
		internal string Id { get; set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x0000FF24 File Offset: 0x0000E124
		// (set) Token: 0x060007EF RID: 2031 RVA: 0x0000FF2C File Offset: 0x0000E12C
		[DataMember(EmitDefaultValue = false, Order = 2)]
		internal string Algorithm { get; set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x0000FF35 File Offset: 0x0000E135
		// (set) Token: 0x060007F1 RID: 2033 RVA: 0x0000FF3D File Offset: 0x0000E13D
		[DataMember(EmitDefaultValue = false, Order = 3)]
		internal DataTransformInput Input { get; set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x0000FF46 File Offset: 0x0000E146
		// (set) Token: 0x060007F3 RID: 2035 RVA: 0x0000FF4E File Offset: 0x0000E14E
		[DataMember(EmitDefaultValue = false, Order = 4)]
		internal DataTransformOutput Output { get; set; }
	}
}
