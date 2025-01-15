using System;
using System.Runtime.Serialization;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x02000135 RID: 309
	[DataContract]
	internal sealed class RestartKindDefinition
	{
		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x0001023B File Offset: 0x0000E43B
		// (set) Token: 0x0600084D RID: 2125 RVA: 0x00010243 File Offset: 0x0000E443
		[DataMember(EmitDefaultValue = false, Order = 1)]
		internal ExpressionNode RestartIndicator { get; set; }
	}
}
