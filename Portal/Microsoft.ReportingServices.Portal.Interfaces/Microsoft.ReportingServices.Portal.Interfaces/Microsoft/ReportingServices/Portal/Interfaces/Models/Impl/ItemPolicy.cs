using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Portal.Interfaces.Models.Impl
{
	// Token: 0x020000A7 RID: 167
	public sealed class ItemPolicy
	{
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x00004A8E File Offset: 0x00002C8E
		// (set) Token: 0x06000556 RID: 1366 RVA: 0x00004A96 File Offset: 0x00002C96
		public bool InheritParentPolicy { get; set; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x00004A9F File Offset: 0x00002C9F
		// (set) Token: 0x06000558 RID: 1368 RVA: 0x00004AA7 File Offset: 0x00002CA7
		public IEnumerable<Policy> Policies { get; set; }
	}
}
