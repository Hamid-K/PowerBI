using System;

namespace Microsoft.OData
{
	// Token: 0x020000A8 RID: 168
	public enum ODataPayloadKind
	{
		// Token: 0x040002C8 RID: 712
		ResourceSet,
		// Token: 0x040002C9 RID: 713
		Resource,
		// Token: 0x040002CA RID: 714
		Property,
		// Token: 0x040002CB RID: 715
		EntityReferenceLink,
		// Token: 0x040002CC RID: 716
		EntityReferenceLinks,
		// Token: 0x040002CD RID: 717
		Value,
		// Token: 0x040002CE RID: 718
		BinaryValue,
		// Token: 0x040002CF RID: 719
		Collection,
		// Token: 0x040002D0 RID: 720
		ServiceDocument,
		// Token: 0x040002D1 RID: 721
		MetadataDocument,
		// Token: 0x040002D2 RID: 722
		Error,
		// Token: 0x040002D3 RID: 723
		Batch,
		// Token: 0x040002D4 RID: 724
		Parameter,
		// Token: 0x040002D5 RID: 725
		IndividualProperty,
		// Token: 0x040002D6 RID: 726
		Delta,
		// Token: 0x040002D7 RID: 727
		Asynchronous,
		// Token: 0x040002D8 RID: 728
		Unsupported = 2147483647
	}
}
