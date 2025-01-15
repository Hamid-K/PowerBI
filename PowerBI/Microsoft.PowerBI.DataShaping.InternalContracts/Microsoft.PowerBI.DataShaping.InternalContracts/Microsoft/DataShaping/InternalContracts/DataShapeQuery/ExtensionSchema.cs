using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x02000091 RID: 145
	internal sealed class ExtensionSchema
	{
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000377 RID: 887 RVA: 0x00006FB8 File Offset: 0x000051B8
		// (set) Token: 0x06000378 RID: 888 RVA: 0x00006FC0 File Offset: 0x000051C0
		public string Name { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000379 RID: 889 RVA: 0x00006FC9 File Offset: 0x000051C9
		// (set) Token: 0x0600037A RID: 890 RVA: 0x00006FD1 File Offset: 0x000051D1
		public List<ExtensionEntity> Entities { get; set; }
	}
}
