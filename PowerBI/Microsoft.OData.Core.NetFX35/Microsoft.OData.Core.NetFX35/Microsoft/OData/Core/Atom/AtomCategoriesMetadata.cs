using System;
using System.Collections.Generic;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x0200000D RID: 13
	public sealed class AtomCategoriesMetadata
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002930 File Offset: 0x00000B30
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002938 File Offset: 0x00000B38
		public bool? Fixed { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002941 File Offset: 0x00000B41
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002949 File Offset: 0x00000B49
		public string Scheme { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002952 File Offset: 0x00000B52
		// (set) Token: 0x06000040 RID: 64 RVA: 0x0000295A File Offset: 0x00000B5A
		public Uri Href { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002963 File Offset: 0x00000B63
		// (set) Token: 0x06000042 RID: 66 RVA: 0x0000296B File Offset: 0x00000B6B
		public IEnumerable<AtomCategoryMetadata> Categories { get; set; }
	}
}
