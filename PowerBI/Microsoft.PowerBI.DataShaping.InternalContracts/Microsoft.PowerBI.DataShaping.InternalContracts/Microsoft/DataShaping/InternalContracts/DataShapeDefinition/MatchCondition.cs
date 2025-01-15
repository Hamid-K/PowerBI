using System;
using System.Runtime.Serialization;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x02000133 RID: 307
	[DataContract]
	internal sealed class MatchCondition
	{
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x000101E7 File Offset: 0x0000E3E7
		// (set) Token: 0x06000843 RID: 2115 RVA: 0x000101EF File Offset: 0x0000E3EF
		[DataMember(EmitDefaultValue = false, Order = 1)]
		internal FieldValueExpressionNode Field { get; set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x000101F8 File Offset: 0x0000E3F8
		// (set) Token: 0x06000845 RID: 2117 RVA: 0x00010200 File Offset: 0x0000E400
		[DataMember(EmitDefaultValue = false, Order = 2)]
		internal bool Value { get; set; }
	}
}
