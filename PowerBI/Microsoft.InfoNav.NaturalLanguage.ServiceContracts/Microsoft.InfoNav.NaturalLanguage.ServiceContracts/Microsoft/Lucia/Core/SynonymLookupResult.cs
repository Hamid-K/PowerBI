using System;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000EA RID: 234
	[DataContract(Name = "SynonymLookupResult", Namespace = "http://schemas.microsoft.com/sqlbi/2014/10/LinguisticDataProviderService")]
	public sealed class SynonymLookupResult
	{
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x000087D9 File Offset: 0x000069D9
		// (set) Token: 0x0600048C RID: 1164 RVA: 0x000087E1 File Offset: 0x000069E1
		[DataMember(IsRequired = false, Order = 1)]
		public SynonymSearchDefinitionMatch[] Matches { get; set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x000087EA File Offset: 0x000069EA
		// (set) Token: 0x0600048E RID: 1166 RVA: 0x000087F2 File Offset: 0x000069F2
		[DataMember(IsRequired = false, Order = 2)]
		public SynonymLookupWarning Warnings { get; set; }
	}
}
