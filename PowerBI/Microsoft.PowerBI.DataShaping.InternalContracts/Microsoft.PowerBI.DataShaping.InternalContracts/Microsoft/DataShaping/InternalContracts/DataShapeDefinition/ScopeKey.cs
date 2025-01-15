using System;
using System.Runtime.Serialization;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x02000131 RID: 305
	[DataContract]
	internal sealed class ScopeKey
	{
		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x00010193 File Offset: 0x0000E393
		// (set) Token: 0x06000839 RID: 2105 RVA: 0x0001019B File Offset: 0x0000E39B
		[DataMember(EmitDefaultValue = false, Order = 1)]
		internal ExpressionNode Value { get; set; }
	}
}
