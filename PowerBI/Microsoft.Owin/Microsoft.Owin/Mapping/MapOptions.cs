using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Owin.Mapping
{
	// Token: 0x02000026 RID: 38
	public class MapOptions
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00004A57 File Offset: 0x00002C57
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x00004A5F File Offset: 0x00002C5F
		public PathString PathMatch { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00004A68 File Offset: 0x00002C68
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x00004A70 File Offset: 0x00002C70
		public Func<IDictionary<string, object>, Task> Branch { get; set; }
	}
}
