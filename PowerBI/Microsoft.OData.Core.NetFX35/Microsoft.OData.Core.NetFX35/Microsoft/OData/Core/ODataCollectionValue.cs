using System;
using System.Collections;

namespace Microsoft.OData.Core
{
	// Token: 0x02000156 RID: 342
	public sealed class ODataCollectionValue : ODataValue
	{
		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x00030578 File Offset: 0x0002E778
		// (set) Token: 0x06000CE2 RID: 3298 RVA: 0x00030580 File Offset: 0x0002E780
		public string TypeName { get; set; }

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x00030589 File Offset: 0x0002E789
		// (set) Token: 0x06000CE4 RID: 3300 RVA: 0x00030591 File Offset: 0x0002E791
		public IEnumerable Items { get; set; }
	}
}
