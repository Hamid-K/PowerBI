using System;
using System.Collections.Generic;

namespace Model
{
	// Token: 0x02000006 RID: 6
	public class CatalogItemDrillthroughTarget : DrillthroughTarget
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002137 File Offset: 0x00000337
		// (set) Token: 0x0600001A RID: 26 RVA: 0x0000213F File Offset: 0x0000033F
		public CatalogItemType CatalogItemType { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002148 File Offset: 0x00000348
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002150 File Offset: 0x00000350
		public string Path { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002159 File Offset: 0x00000359
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002161 File Offset: 0x00000361
		public Guid Id { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002172 File Offset: 0x00000372
		public IEnumerable<CatalogItemParameter> Parameters { get; set; }
	}
}
