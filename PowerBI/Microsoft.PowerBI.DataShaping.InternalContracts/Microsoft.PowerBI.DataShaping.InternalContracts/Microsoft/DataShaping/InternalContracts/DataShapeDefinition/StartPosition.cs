using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x02000139 RID: 313
	[DataContract]
	internal sealed class StartPosition
	{
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x000102D0 File Offset: 0x0000E4D0
		// (set) Token: 0x0600085B RID: 2139 RVA: 0x000102D8 File Offset: 0x0000E4D8
		[DataMember(EmitDefaultValue = false, Order = 1)]
		internal IList<ExpressionNode> Expressions { get; set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x000102E1 File Offset: 0x0000E4E1
		// (set) Token: 0x0600085D RID: 2141 RVA: 0x000102E9 File Offset: 0x0000E4E9
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public IList<object> Values { get; set; }
	}
}
