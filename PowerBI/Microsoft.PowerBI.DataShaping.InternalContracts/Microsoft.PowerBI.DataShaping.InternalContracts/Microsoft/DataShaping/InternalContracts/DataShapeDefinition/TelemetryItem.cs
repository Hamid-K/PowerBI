using System;
using System.Runtime.Serialization;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x0200013A RID: 314
	[DataContract]
	internal sealed class TelemetryItem
	{
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x000102FA File Offset: 0x0000E4FA
		// (set) Token: 0x06000860 RID: 2144 RVA: 0x00010302 File Offset: 0x0000E502
		[DataMember(EmitDefaultValue = false, Order = 1)]
		internal string Name { get; set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x0001030B File Offset: 0x0000E50B
		// (set) Token: 0x06000862 RID: 2146 RVA: 0x00010313 File Offset: 0x0000E513
		[DataMember(EmitDefaultValue = false, Order = 2)]
		internal ExpressionNode Expression { get; set; }
	}
}
