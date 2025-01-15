using System;

namespace Microsoft.Data.OData
{
	// Token: 0x020001B4 RID: 436
	internal sealed class MediaTypeWithFormat
	{
		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x0002D4AA File Offset: 0x0002B6AA
		// (set) Token: 0x06000CD1 RID: 3281 RVA: 0x0002D4B2 File Offset: 0x0002B6B2
		public MediaType MediaType { get; set; }

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x0002D4BB File Offset: 0x0002B6BB
		// (set) Token: 0x06000CD3 RID: 3283 RVA: 0x0002D4C3 File Offset: 0x0002B6C3
		public ODataFormat Format { get; set; }
	}
}
