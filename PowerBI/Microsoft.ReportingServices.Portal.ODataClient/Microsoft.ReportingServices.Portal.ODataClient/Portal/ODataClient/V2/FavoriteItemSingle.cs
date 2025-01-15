using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000031 RID: 49
	[OriginalName("FavoriteItemSingle")]
	public class FavoriteItemSingle : DataServiceQuerySingle<FavoriteItem>
	{
		// Token: 0x0600020F RID: 527 RVA: 0x0000548D File Offset: 0x0000368D
		public FavoriteItemSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00005497 File Offset: 0x00003697
		public FavoriteItemSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000054A2 File Offset: 0x000036A2
		public FavoriteItemSingle(DataServiceQuerySingle<FavoriteItem> query)
			: base(query)
		{
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000212 RID: 530 RVA: 0x000054AB File Offset: 0x000036AB
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Item")]
		public CatalogItemSingle Item
		{
			get
			{
				if (!base.IsComposable)
				{
					throw new NotSupportedException("The previous function is not composable.");
				}
				if (this._Item == null)
				{
					this._Item = new CatalogItemSingle(base.Context, base.GetPath("Item"));
				}
				return this._Item;
			}
		}

		// Token: 0x0400010E RID: 270
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private CatalogItemSingle _Item;
	}
}
