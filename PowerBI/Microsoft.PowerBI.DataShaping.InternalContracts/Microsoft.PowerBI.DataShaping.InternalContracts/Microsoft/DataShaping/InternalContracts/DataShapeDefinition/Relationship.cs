using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x02000134 RID: 308
	[DataContract]
	internal sealed class Relationship
	{
		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x00010211 File Offset: 0x0000E411
		// (set) Token: 0x06000848 RID: 2120 RVA: 0x00010219 File Offset: 0x0000E419
		[DataMember(EmitDefaultValue = false, Order = 1)]
		internal string ParentScope { get; set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x00010222 File Offset: 0x0000E422
		// (set) Token: 0x0600084A RID: 2122 RVA: 0x0001022A File Offset: 0x0000E42A
		[DataMember(EmitDefaultValue = false, Order = 2)]
		internal IList<JoinCondition> JoinConditions { get; set; }
	}
}
