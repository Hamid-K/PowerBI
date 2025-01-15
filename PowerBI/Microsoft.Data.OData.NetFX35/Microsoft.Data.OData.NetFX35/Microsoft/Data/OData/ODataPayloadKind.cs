using System;

namespace Microsoft.Data.OData
{
	// Token: 0x02000273 RID: 627
	public enum ODataPayloadKind
	{
		// Token: 0x04000751 RID: 1873
		Feed,
		// Token: 0x04000752 RID: 1874
		Entry,
		// Token: 0x04000753 RID: 1875
		Property,
		// Token: 0x04000754 RID: 1876
		EntityReferenceLink,
		// Token: 0x04000755 RID: 1877
		EntityReferenceLinks,
		// Token: 0x04000756 RID: 1878
		Value,
		// Token: 0x04000757 RID: 1879
		BinaryValue,
		// Token: 0x04000758 RID: 1880
		Collection,
		// Token: 0x04000759 RID: 1881
		ServiceDocument,
		// Token: 0x0400075A RID: 1882
		MetadataDocument,
		// Token: 0x0400075B RID: 1883
		Error,
		// Token: 0x0400075C RID: 1884
		Batch,
		// Token: 0x0400075D RID: 1885
		Parameter,
		// Token: 0x0400075E RID: 1886
		Unsupported = 2147483647
	}
}
