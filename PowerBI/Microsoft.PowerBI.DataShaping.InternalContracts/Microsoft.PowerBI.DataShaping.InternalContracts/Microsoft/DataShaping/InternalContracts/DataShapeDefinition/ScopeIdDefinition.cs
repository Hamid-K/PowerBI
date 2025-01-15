using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x02000130 RID: 304
	[DataContract]
	internal sealed class ScopeIdDefinition
	{
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x0001017A File Offset: 0x0000E37A
		// (set) Token: 0x06000836 RID: 2102 RVA: 0x00010182 File Offset: 0x0000E382
		[DataMember(EmitDefaultValue = false, Order = 1)]
		internal IList<ScopeKey> ScopeKeys { get; set; }
	}
}
