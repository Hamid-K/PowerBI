using System;
using System.Runtime.Serialization;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x02000127 RID: 295
	[DataContract]
	internal sealed class DataTransformParameter
	{
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x0000FFDD File Offset: 0x0000E1DD
		// (set) Token: 0x06000805 RID: 2053 RVA: 0x0000FFE5 File Offset: 0x0000E1E5
		[DataMember(EmitDefaultValue = false, Order = 1)]
		internal string Name { get; set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x0000FFEE File Offset: 0x0000E1EE
		// (set) Token: 0x06000807 RID: 2055 RVA: 0x0000FFF6 File Offset: 0x0000E1F6
		[DataMember(EmitDefaultValue = false, Order = 2)]
		internal ExpressionNode Value { get; set; }
	}
}
