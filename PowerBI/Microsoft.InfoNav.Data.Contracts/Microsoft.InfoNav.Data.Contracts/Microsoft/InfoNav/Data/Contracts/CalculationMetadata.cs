using System;

namespace Microsoft.InfoNav.Data.Contracts
{
	// Token: 0x02000080 RID: 128
	internal struct CalculationMetadata
	{
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00008154 File Offset: 0x00006354
		// (set) Token: 0x060002ED RID: 749 RVA: 0x0000815C File Offset: 0x0000635C
		public string Id { get; set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00008165 File Offset: 0x00006365
		// (set) Token: 0x060002EF RID: 751 RVA: 0x0000816D File Offset: 0x0000636D
		public ConceptualPrimitiveType Type { get; set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x00008176 File Offset: 0x00006376
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x0000817E File Offset: 0x0000637E
		public string DictionaryId { get; set; }
	}
}
