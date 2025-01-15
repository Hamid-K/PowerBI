using System;
using System.Collections.Generic;

namespace Microsoft.OData
{
	// Token: 0x020000E3 RID: 227
	public sealed class ODataCollectionValue : ODataValue
	{
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x0001CABB File Offset: 0x0001ACBB
		// (set) Token: 0x06000A96 RID: 2710 RVA: 0x0001CAC3 File Offset: 0x0001ACC3
		public string TypeName { get; set; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x0001CACC File Offset: 0x0001ACCC
		// (set) Token: 0x06000A98 RID: 2712 RVA: 0x0001CAD4 File Offset: 0x0001ACD4
		public IEnumerable<object> Items { get; set; }
	}
}
