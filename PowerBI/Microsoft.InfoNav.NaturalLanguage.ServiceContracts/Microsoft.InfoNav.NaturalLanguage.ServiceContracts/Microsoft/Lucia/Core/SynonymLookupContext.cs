using System;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000E7 RID: 231
	[DataContract(Name = "SynonymLookupContext", Namespace = "http://schemas.microsoft.com/sqlbi/2014/10/LinguisticDataProviderService")]
	public sealed class SynonymLookupContext
	{
		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x00008741 File Offset: 0x00006941
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x00008749 File Offset: 0x00006949
		[DataMember(IsRequired = true, Order = 1)]
		public string[] Words { get; set; }
	}
}
