using System;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x02000018 RID: 24
	public sealed class AtomResourceCollectionMetadata
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x000034A7 File Offset: 0x000016A7
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x000034AF File Offset: 0x000016AF
		public AtomTextConstruct Title { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000034B8 File Offset: 0x000016B8
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x000034C0 File Offset: 0x000016C0
		public string Accept { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000034C9 File Offset: 0x000016C9
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x000034D1 File Offset: 0x000016D1
		public AtomCategoriesMetadata Categories { get; set; }
	}
}
