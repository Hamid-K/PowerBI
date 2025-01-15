using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Portal.Interfaces.Models.Impl
{
	// Token: 0x020000A8 RID: 168
	public sealed class Policy
	{
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x00004AB0 File Offset: 0x00002CB0
		// (set) Token: 0x0600055B RID: 1371 RVA: 0x00004AB8 File Offset: 0x00002CB8
		public string GroupUserName { get; set; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x00004AC1 File Offset: 0x00002CC1
		// (set) Token: 0x0600055D RID: 1373 RVA: 0x00004AC9 File Offset: 0x00002CC9
		public IEnumerable<Role> Roles { get; set; }
	}
}
