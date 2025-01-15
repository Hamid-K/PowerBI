using System;
using System.Runtime.Serialization;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x0200012F RID: 303
	[DataContract]
	internal sealed class GroupKey
	{
		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x00010161 File Offset: 0x0000E361
		// (set) Token: 0x06000833 RID: 2099 RVA: 0x00010169 File Offset: 0x0000E369
		[DataMember(EmitDefaultValue = false, Order = 1)]
		internal ExpressionNode Value { get; set; }
	}
}
