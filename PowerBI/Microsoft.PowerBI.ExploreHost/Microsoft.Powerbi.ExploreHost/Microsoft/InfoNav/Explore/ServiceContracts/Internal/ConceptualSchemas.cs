using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Explore.ServiceContracts.Internal
{
	// Token: 0x0200000C RID: 12
	[DataContract]
	public sealed class ConceptualSchemas
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000022DE File Offset: 0x000004DE
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000022E6 File Offset: 0x000004E6
		[DataMember(IsRequired = true, Order = 0, Name = "schemas")]
		public IList<ConceptualSchemaInfo> Schemas { get; set; }
	}
}
