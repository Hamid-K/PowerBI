using System;
using System.Runtime.Serialization;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x02000132 RID: 306
	[DataContract]
	internal sealed class JoinCondition
	{
		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x000101AC File Offset: 0x0000E3AC
		// (set) Token: 0x0600083C RID: 2108 RVA: 0x000101B4 File Offset: 0x0000E3B4
		[DataMember(EmitDefaultValue = false, Order = 1)]
		internal ExpressionNode PrimaryKey { get; set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x000101BD File Offset: 0x0000E3BD
		// (set) Token: 0x0600083E RID: 2110 RVA: 0x000101C5 File Offset: 0x0000E3C5
		[DataMember(EmitDefaultValue = false, Order = 2)]
		internal ExpressionNode SecondaryKey { get; set; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x000101CE File Offset: 0x0000E3CE
		// (set) Token: 0x06000840 RID: 2112 RVA: 0x000101D6 File Offset: 0x0000E3D6
		[DataMember(EmitDefaultValue = false, Order = 3)]
		internal SortDirection SortDirection { get; set; }
	}
}
