using System;
using System.Collections.Generic;

namespace Model
{
	// Token: 0x02000075 RID: 117
	public class Folder : CatalogItem
	{
		// Token: 0x06000370 RID: 880 RVA: 0x000041BB File Offset: 0x000023BB
		public Folder()
			: base(CatalogItemType.Folder)
		{
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000371 RID: 881 RVA: 0x000041C4 File Offset: 0x000023C4
		public IList<CatalogItem> CatalogItems
		{
			get
			{
				IList<CatalogItem> list;
				if ((list = this.catalogItems) == null)
				{
					list = (this.catalogItems = this.LoadCatalogItems());
				}
				return list;
			}
		}

		// Token: 0x06000372 RID: 882 RVA: 0x000041EA File Offset: 0x000023EA
		protected virtual IList<CatalogItem> LoadCatalogItems()
		{
			return new List<CatalogItem>();
		}

		// Token: 0x04000273 RID: 627
		private IList<CatalogItem> catalogItems;
	}
}
