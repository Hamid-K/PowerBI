using System;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000E9 RID: 233
	[DataContract(Name = "SynonymLookupRequestOptions", Namespace = "http://schemas.microsoft.com/sqlbi/2014/10/LinguisticDataProviderService")]
	public enum SynonymLookupRequestOptions
	{
		// Token: 0x0400050C RID: 1292
		[EnumMember]
		DefaultLookup,
		// Token: 0x0400050D RID: 1293
		[EnumMember]
		ExtendedLookup
	}
}
