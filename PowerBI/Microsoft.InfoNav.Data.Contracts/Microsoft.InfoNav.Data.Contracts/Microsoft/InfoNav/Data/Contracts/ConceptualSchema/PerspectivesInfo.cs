using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualSchema
{
	// Token: 0x02000122 RID: 290
	[DataContract]
	public sealed class PerspectivesInfo
	{
		// Token: 0x17000251 RID: 593
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x0000FDA8 File Offset: 0x0000DFA8
		// (set) Token: 0x0600079E RID: 1950 RVA: 0x0000FDB0 File Offset: 0x0000DFB0
		[DataMember(IsRequired = true, Order = 0, Name = "perspectiveIds")]
		public IList<string> PerspectiveIds { get; set; }
	}
}
