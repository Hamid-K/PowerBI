using System;
using System.ComponentModel;

namespace Model
{
	// Token: 0x0200001D RID: 29
	public sealed class SystemResourceItem
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000080 RID: 128 RVA: 0x0000254C File Offset: 0x0000074C
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00002554 File Offset: 0x00000754
		[ReadOnly(true)]
		public Guid Id { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000082 RID: 130 RVA: 0x0000255D File Offset: 0x0000075D
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00002565 File Offset: 0x00000765
		public string Key { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000084 RID: 132 RVA: 0x0000256E File Offset: 0x0000076E
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00002576 File Offset: 0x00000776
		public CatalogItem ItemContent { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000086 RID: 134 RVA: 0x0000257F File Offset: 0x0000077F
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00002587 File Offset: 0x00000787
		public string TypeName { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00002590 File Offset: 0x00000790
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00002598 File Offset: 0x00000798
		public bool IsEmbedded { get; set; }
	}
}
