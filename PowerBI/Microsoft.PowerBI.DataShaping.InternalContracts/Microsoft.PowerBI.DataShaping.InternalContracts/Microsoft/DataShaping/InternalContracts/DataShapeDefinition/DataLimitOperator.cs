using System;
using System.Runtime.Serialization;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x0200010E RID: 270
	[DataContract]
	[KnownType(typeof(TopNPerLevelLimitOperator))]
	[KnownType(typeof(TopLimitOperator))]
	[KnownType(typeof(BottomLimitOperator))]
	[KnownType(typeof(SampleLimitOperator))]
	[KnownType(typeof(BinnedLineSampleLimitOperator))]
	[KnownType(typeof(OverlappingPointsSampleLimitOperator))]
	internal abstract class DataLimitOperator
	{
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x0000F5F1 File Offset: 0x0000D7F1
		// (set) Token: 0x06000744 RID: 1860 RVA: 0x0000F5F9 File Offset: 0x0000D7F9
		[DataMember(EmitDefaultValue = false, Order = 10)]
		internal ExpressionNode Count { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x0000F602 File Offset: 0x0000D802
		// (set) Token: 0x06000746 RID: 1862 RVA: 0x0000F60A File Offset: 0x0000D80A
		[DataMember(EmitDefaultValue = false, Order = 20)]
		internal ExpressionNode DbCount { get; set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x0000F613 File Offset: 0x0000D813
		// (set) Token: 0x06000748 RID: 1864 RVA: 0x0000F61B File Offset: 0x0000D81B
		[DataMember(EmitDefaultValue = false, Order = 25)]
		internal ExpressionNode IsExceededDbCount { get; set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x0000F624 File Offset: 0x0000D824
		// (set) Token: 0x0600074A RID: 1866 RVA: 0x0000F62C File Offset: 0x0000D82C
		[DataMember(EmitDefaultValue = false, Order = 30)]
		internal ExceededDetectionKind? Kind { get; set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x0000F635 File Offset: 0x0000D835
		// (set) Token: 0x0600074C RID: 1868 RVA: 0x0000F63D File Offset: 0x0000D83D
		[DataMember(EmitDefaultValue = false, Order = 40)]
		internal ExpressionNode WarningCount { get; set; }
	}
}
