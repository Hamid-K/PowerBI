using System;
using System.Collections;

namespace Microsoft.Data.OData
{
	// Token: 0x02000293 RID: 659
	public sealed class ODataCollectionValue : ODataValue
	{
		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x060014F6 RID: 5366 RVA: 0x0004CC35 File Offset: 0x0004AE35
		// (set) Token: 0x060014F7 RID: 5367 RVA: 0x0004CC3D File Offset: 0x0004AE3D
		public string TypeName { get; set; }

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x060014F8 RID: 5368 RVA: 0x0004CC46 File Offset: 0x0004AE46
		// (set) Token: 0x060014F9 RID: 5369 RVA: 0x0004CC4E File Offset: 0x0004AE4E
		public IEnumerable Items { get; set; }
	}
}
