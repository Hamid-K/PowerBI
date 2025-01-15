using System;

namespace Microsoft.OData
{
	// Token: 0x02000083 RID: 131
	public enum ODataPayloadKind
	{
		// Token: 0x04000262 RID: 610
		ResourceSet,
		// Token: 0x04000263 RID: 611
		Resource,
		// Token: 0x04000264 RID: 612
		Property,
		// Token: 0x04000265 RID: 613
		EntityReferenceLink,
		// Token: 0x04000266 RID: 614
		EntityReferenceLinks,
		// Token: 0x04000267 RID: 615
		Value,
		// Token: 0x04000268 RID: 616
		BinaryValue,
		// Token: 0x04000269 RID: 617
		Collection,
		// Token: 0x0400026A RID: 618
		ServiceDocument,
		// Token: 0x0400026B RID: 619
		MetadataDocument,
		// Token: 0x0400026C RID: 620
		Error,
		// Token: 0x0400026D RID: 621
		Batch,
		// Token: 0x0400026E RID: 622
		Parameter,
		// Token: 0x0400026F RID: 623
		IndividualProperty,
		// Token: 0x04000270 RID: 624
		Delta,
		// Token: 0x04000271 RID: 625
		Asynchronous,
		// Token: 0x04000272 RID: 626
		Unsupported = 2147483647
	}
}
