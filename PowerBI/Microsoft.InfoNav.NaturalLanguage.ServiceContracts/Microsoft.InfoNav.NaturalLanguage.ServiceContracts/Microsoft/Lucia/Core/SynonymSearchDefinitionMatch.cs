using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000EF RID: 239
	[DataContract(Name = "SynonymSearchDefinitionMatch", Namespace = "http://schemas.microsoft.com/sqlbi/2014/10/LinguisticDataProviderService")]
	public sealed class SynonymSearchDefinitionMatch
	{
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x00008879 File Offset: 0x00006A79
		// (set) Token: 0x0600049F RID: 1183 RVA: 0x00008881 File Offset: 0x00006A81
		[DataMember(IsRequired = true, Order = 1)]
		public IList<SynonymMatch> SynonymMatches { get; set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x0000888A File Offset: 0x00006A8A
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x00008892 File Offset: 0x00006A92
		[DataMember(IsRequired = false, Order = 2)]
		public SynonymLookupWarning Warnings { get; set; }
	}
}
