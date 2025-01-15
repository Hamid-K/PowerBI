using System;
using System.Runtime.Serialization;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x02000108 RID: 264
	[DataContract]
	internal sealed class Calculation
	{
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x0000F453 File Offset: 0x0000D653
		// (set) Token: 0x06000713 RID: 1811 RVA: 0x0000F45B File Offset: 0x0000D65B
		[DataMember(EmitDefaultValue = false, Order = 1)]
		internal string Id { get; set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x0000F464 File Offset: 0x0000D664
		// (set) Token: 0x06000715 RID: 1813 RVA: 0x0000F46C File Offset: 0x0000D66C
		[DataMember(EmitDefaultValue = false, Order = 2)]
		internal ExpressionNode Value { get; set; }
	}
}
