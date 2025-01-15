using System;

namespace Microsoft.OData.Core
{
	// Token: 0x0200018D RID: 397
	public enum ODataPayloadKind
	{
		// Token: 0x04000673 RID: 1651
		Feed,
		// Token: 0x04000674 RID: 1652
		Entry,
		// Token: 0x04000675 RID: 1653
		Property,
		// Token: 0x04000676 RID: 1654
		EntityReferenceLink,
		// Token: 0x04000677 RID: 1655
		EntityReferenceLinks,
		// Token: 0x04000678 RID: 1656
		Value,
		// Token: 0x04000679 RID: 1657
		BinaryValue,
		// Token: 0x0400067A RID: 1658
		Collection,
		// Token: 0x0400067B RID: 1659
		ServiceDocument,
		// Token: 0x0400067C RID: 1660
		MetadataDocument,
		// Token: 0x0400067D RID: 1661
		Error,
		// Token: 0x0400067E RID: 1662
		Batch,
		// Token: 0x0400067F RID: 1663
		Parameter,
		// Token: 0x04000680 RID: 1664
		IndividualProperty,
		// Token: 0x04000681 RID: 1665
		Delta,
		// Token: 0x04000682 RID: 1666
		Asynchronous,
		// Token: 0x04000683 RID: 1667
		Unsupported = 2147483647
	}
}
