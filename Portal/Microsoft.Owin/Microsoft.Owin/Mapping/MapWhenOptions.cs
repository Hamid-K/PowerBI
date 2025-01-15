using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Owin.Mapping
{
	// Token: 0x02000028 RID: 40
	public class MapWhenOptions
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00004AFF File Offset: 0x00002CFF
		// (set) Token: 0x060001CA RID: 458 RVA: 0x00004B07 File Offset: 0x00002D07
		public Func<IOwinContext, bool> Predicate { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00004B10 File Offset: 0x00002D10
		// (set) Token: 0x060001CC RID: 460 RVA: 0x00004B18 File Offset: 0x00002D18
		public Func<IOwinContext, Task<bool>> PredicateAsync { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00004B21 File Offset: 0x00002D21
		// (set) Token: 0x060001CE RID: 462 RVA: 0x00004B29 File Offset: 0x00002D29
		public Func<IDictionary<string, object>, Task> Branch { get; set; }
	}
}
