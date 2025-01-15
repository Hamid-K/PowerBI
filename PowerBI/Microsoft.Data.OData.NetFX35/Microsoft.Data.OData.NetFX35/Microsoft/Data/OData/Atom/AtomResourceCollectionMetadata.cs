using System;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x02000268 RID: 616
	public sealed class AtomResourceCollectionMetadata
	{
		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x0600133B RID: 4923 RVA: 0x00048389 File Offset: 0x00046589
		// (set) Token: 0x0600133C RID: 4924 RVA: 0x00048391 File Offset: 0x00046591
		public AtomTextConstruct Title { get; set; }

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x0600133D RID: 4925 RVA: 0x0004839A File Offset: 0x0004659A
		// (set) Token: 0x0600133E RID: 4926 RVA: 0x000483A2 File Offset: 0x000465A2
		public string Accept { get; set; }

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x0600133F RID: 4927 RVA: 0x000483AB File Offset: 0x000465AB
		// (set) Token: 0x06001340 RID: 4928 RVA: 0x000483B3 File Offset: 0x000465B3
		public AtomCategoriesMetadata Categories { get; set; }
	}
}
