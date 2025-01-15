using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.CatalogAccess.DataAccessObject
{
	// Token: 0x0200001B RID: 27
	public class PolicyEntity
	{
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000119 RID: 281 RVA: 0x0000279A File Offset: 0x0000099A
		// (set) Token: 0x0600011A RID: 282 RVA: 0x000027A2 File Offset: 0x000009A2
		public string GroupUserName { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600011B RID: 283 RVA: 0x000027AB File Offset: 0x000009AB
		// (set) Token: 0x0600011C RID: 284 RVA: 0x000027B3 File Offset: 0x000009B3
		public string GroupUserId { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600011D RID: 285 RVA: 0x000027BC File Offset: 0x000009BC
		// (set) Token: 0x0600011E RID: 286 RVA: 0x000027C4 File Offset: 0x000009C4
		public IEnumerable<RoleEntity> Roles { get; set; }
	}
}
