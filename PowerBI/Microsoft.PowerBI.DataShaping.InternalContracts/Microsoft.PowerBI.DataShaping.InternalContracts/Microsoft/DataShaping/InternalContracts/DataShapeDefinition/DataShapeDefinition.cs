using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x0200011F RID: 287
	[DataContract]
	internal sealed class DataShapeDefinition
	{
		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x0000FA10 File Offset: 0x0000DC10
		// (set) Token: 0x060007C1 RID: 1985 RVA: 0x0000FA18 File Offset: 0x0000DC18
		[DataMember(EmitDefaultValue = false, Order = 1)]
		internal DataSource DataSource { get; set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x0000FA21 File Offset: 0x0000DC21
		// (set) Token: 0x060007C3 RID: 1987 RVA: 0x0000FA29 File Offset: 0x0000DC29
		[DataMember(EmitDefaultValue = false, Order = 2)]
		internal IList<DataSet> DataSets { get; set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x0000FA32 File Offset: 0x0000DC32
		// (set) Token: 0x060007C5 RID: 1989 RVA: 0x0000FA3A File Offset: 0x0000DC3A
		[DataMember(EmitDefaultValue = false, Order = 3)]
		internal ResultEncodingHints ResultEncodingHints { get; set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x0000FA43 File Offset: 0x0000DC43
		// (set) Token: 0x060007C7 RID: 1991 RVA: 0x0000FA4B File Offset: 0x0000DC4B
		[DataMember(EmitDefaultValue = false, Order = 4)]
		internal IList<DataTransform> DataTransforms { get; set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x0000FA54 File Offset: 0x0000DC54
		// (set) Token: 0x060007C9 RID: 1993 RVA: 0x0000FA5C File Offset: 0x0000DC5C
		[DataMember(EmitDefaultValue = false, Order = 5)]
		internal DataShape DataShape { get; set; }
	}
}
