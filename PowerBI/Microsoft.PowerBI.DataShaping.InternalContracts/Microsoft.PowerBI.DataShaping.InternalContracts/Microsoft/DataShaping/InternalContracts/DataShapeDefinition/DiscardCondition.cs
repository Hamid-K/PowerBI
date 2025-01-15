using System;
using System.Runtime.Serialization;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x0200012C RID: 300
	[DataContract]
	internal sealed class DiscardCondition
	{
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x000100FC File Offset: 0x0000E2FC
		// (set) Token: 0x06000827 RID: 2087 RVA: 0x00010104 File Offset: 0x0000E304
		[DataMember(EmitDefaultValue = false, Order = 1)]
		internal FieldValueExpressionNode Field { get; set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x0001010D File Offset: 0x0000E30D
		// (set) Token: 0x06000829 RID: 2089 RVA: 0x00010115 File Offset: 0x0000E315
		[DataMember(EmitDefaultValue = false, Order = 2)]
		internal bool Value { get; set; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x0001011E File Offset: 0x0000E31E
		// (set) Token: 0x0600082B RID: 2091 RVA: 0x00010126 File Offset: 0x0000E326
		[DataMember(EmitDefaultValue = false, Order = 3)]
		internal DiscardConditionComparisonOperator Operator { get; set; }
	}
}
