using System;

namespace Microsoft.OData.Core
{
	// Token: 0x0200018C RID: 396
	public enum ODataParameterReaderState
	{
		// Token: 0x0400066B RID: 1643
		Start,
		// Token: 0x0400066C RID: 1644
		Value,
		// Token: 0x0400066D RID: 1645
		Collection,
		// Token: 0x0400066E RID: 1646
		Exception,
		// Token: 0x0400066F RID: 1647
		Completed,
		// Token: 0x04000670 RID: 1648
		Entry,
		// Token: 0x04000671 RID: 1649
		Feed
	}
}
