using System;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000EE RID: 238
	[DataContract]
	public enum SynonymProvider
	{
		// Token: 0x04000520 RID: 1312
		[EnumMember]
		BingUnigram,
		// Token: 0x04000521 RID: 1313
		[EnumMember]
		BingBigram,
		// Token: 0x04000522 RID: 1314
		[EnumMember]
		OfficeThesaurus,
		// Token: 0x04000523 RID: 1315
		[EnumMember]
		CuratedCommonEntities,
		// Token: 0x04000524 RID: 1316
		[EnumMember]
		BingContextualProvider,
		// Token: 0x04000525 RID: 1317
		[EnumMember]
		CountryRegion,
		// Token: 0x04000526 RID: 1318
		[EnumMember]
		CityStateZipCode,
		// Token: 0x04000527 RID: 1319
		[EnumMember]
		InstanceValueSynonyms,
		// Token: 0x04000528 RID: 1320
		[EnumMember]
		Nationality
	}
}
