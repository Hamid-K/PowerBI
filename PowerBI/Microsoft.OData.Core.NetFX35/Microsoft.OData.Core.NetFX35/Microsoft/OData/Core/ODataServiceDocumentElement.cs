using System;

namespace Microsoft.OData.Core
{
	// Token: 0x0200016C RID: 364
	public abstract class ODataServiceDocumentElement : ODataAnnotatable
	{
		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x000311F4 File Offset: 0x0002F3F4
		// (set) Token: 0x06000D55 RID: 3413 RVA: 0x000311FC File Offset: 0x0002F3FC
		public Uri Url { get; set; }

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000D56 RID: 3414 RVA: 0x00031205 File Offset: 0x0002F405
		// (set) Token: 0x06000D57 RID: 3415 RVA: 0x0003120D File Offset: 0x0002F40D
		public string Name { get; set; }

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000D58 RID: 3416 RVA: 0x00031216 File Offset: 0x0002F416
		// (set) Token: 0x06000D59 RID: 3417 RVA: 0x0003121E File Offset: 0x0002F41E
		public string Title { get; set; }
	}
}
