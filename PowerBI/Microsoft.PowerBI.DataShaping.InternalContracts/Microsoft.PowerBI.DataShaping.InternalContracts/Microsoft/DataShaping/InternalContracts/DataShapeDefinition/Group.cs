using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x0200012E RID: 302
	[DataContract]
	internal sealed class Group
	{
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x00010137 File Offset: 0x0000E337
		// (set) Token: 0x0600082E RID: 2094 RVA: 0x0001013F File Offset: 0x0000E33F
		[DataMember(EmitDefaultValue = false, Order = 1)]
		internal IList<GroupKey> GroupKeys { get; set; }

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x00010148 File Offset: 0x0000E348
		// (set) Token: 0x06000830 RID: 2096 RVA: 0x00010150 File Offset: 0x0000E350
		[DataMember(EmitDefaultValue = false, Order = 2)]
		internal ScopeIdDefinition ScopeIdDefinition { get; set; }
	}
}
