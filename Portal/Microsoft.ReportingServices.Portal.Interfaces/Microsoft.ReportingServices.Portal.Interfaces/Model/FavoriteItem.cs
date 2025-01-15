using System;

namespace Model
{
	// Token: 0x0200003A RID: 58
	public class FavoriteItem
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00002C3B File Offset: 0x00000E3B
		// (set) Token: 0x06000159 RID: 345 RVA: 0x00002C43 File Offset: 0x00000E43
		public Guid Id { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00002C4C File Offset: 0x00000E4C
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00002C54 File Offset: 0x00000E54
		public CatalogItem Item { get; set; }
	}
}
