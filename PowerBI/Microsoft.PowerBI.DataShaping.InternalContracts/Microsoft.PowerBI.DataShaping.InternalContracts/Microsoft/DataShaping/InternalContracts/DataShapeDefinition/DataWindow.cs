using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x0200012A RID: 298
	[DataContract]
	internal sealed class DataWindow
	{
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x00010020 File Offset: 0x0000E220
		// (set) Token: 0x0600080D RID: 2061 RVA: 0x00010028 File Offset: 0x0000E228
		[DataMember(EmitDefaultValue = false, Order = 0)]
		internal string Id { get; set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x00010031 File Offset: 0x0000E231
		// (set) Token: 0x0600080F RID: 2063 RVA: 0x00010039 File Offset: 0x0000E239
		[DataMember(EmitDefaultValue = false, Order = 10)]
		internal ExpressionNode Count { get; set; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x00010042 File Offset: 0x0000E242
		// (set) Token: 0x06000811 RID: 2065 RVA: 0x0001004A File Offset: 0x0000E24A
		[DataMember(EmitDefaultValue = false, Order = 20)]
		internal ExpressionNode IsExceededDbCount { get; set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x00010053 File Offset: 0x0000E253
		// (set) Token: 0x06000813 RID: 2067 RVA: 0x0001005B File Offset: 0x0000E25B
		[DataMember(EmitDefaultValue = false, Order = 21)]
		internal ExpressionNode DbCount { get; set; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x00010064 File Offset: 0x0000E264
		// (set) Token: 0x06000815 RID: 2069 RVA: 0x0001006C File Offset: 0x0000E26C
		[DataMember(EmitDefaultValue = false, Order = 30)]
		internal IList<IList<ExpressionNode>> RestartDefinitions { get; set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x00010075 File Offset: 0x0000E275
		// (set) Token: 0x06000817 RID: 2071 RVA: 0x0001007D File Offset: 0x0000E27D
		[DataMember(EmitDefaultValue = false, Order = 40)]
		internal IList<string> SegmentationTableIds { get; set; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x00010086 File Offset: 0x0000E286
		// (set) Token: 0x06000819 RID: 2073 RVA: 0x0001008E File Offset: 0x0000E28E
		[DataMember(EmitDefaultValue = false, Order = 50)]
		internal IList<string> Targets { get; set; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x00010097 File Offset: 0x0000E297
		// (set) Token: 0x0600081B RID: 2075 RVA: 0x0001009F File Offset: 0x0000E29F
		[DataMember(EmitDefaultValue = false, Order = 60)]
		internal IList<string> AppliesTo { get; set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x000100A8 File Offset: 0x0000E2A8
		// (set) Token: 0x0600081D RID: 2077 RVA: 0x000100B0 File Offset: 0x0000E2B0
		[DataMember(EmitDefaultValue = false, Order = 70)]
		internal ExceededDetectionKind ExceededDetection { get; set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x000100B9 File Offset: 0x0000E2B9
		// (set) Token: 0x0600081F RID: 2079 RVA: 0x000100C1 File Offset: 0x0000E2C1
		[DataMember(EmitDefaultValue = false, Order = 80)]
		internal int? TelemetryId { get; set; }
	}
}
