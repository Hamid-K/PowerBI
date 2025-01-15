using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData
{
	// Token: 0x020002A7 RID: 679
	public sealed class ODataComplexValue : ODataValue
	{
		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x060015A9 RID: 5545 RVA: 0x0004EC9A File Offset: 0x0004CE9A
		// (set) Token: 0x060015AA RID: 5546 RVA: 0x0004ECA2 File Offset: 0x0004CEA2
		public IEnumerable<ODataProperty> Properties { get; set; }

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060015AB RID: 5547 RVA: 0x0004ECAB File Offset: 0x0004CEAB
		// (set) Token: 0x060015AC RID: 5548 RVA: 0x0004ECB3 File Offset: 0x0004CEB3
		public string TypeName { get; set; }
	}
}
