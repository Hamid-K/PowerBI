using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000EB RID: 235
	[DataContract(Name = "SynonymLookupSearchDefinition", Namespace = "http://schemas.microsoft.com/sqlbi/2014/10/LinguisticDataProviderService")]
	public sealed class SynonymLookupSearchDefinition
	{
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x00008803 File Offset: 0x00006A03
		// (set) Token: 0x06000491 RID: 1169 RVA: 0x0000880B File Offset: 0x00006A0B
		[DataMember(IsRequired = true, Order = 1)]
		public IList<int> TokenIndices { get; set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x00008814 File Offset: 0x00006A14
		// (set) Token: 0x06000493 RID: 1171 RVA: 0x0000881C File Offset: 0x00006A1C
		[DataMember(IsRequired = false, Order = 2)]
		public IList<SynonymProvider> SynonymProviders { get; set; }
	}
}
