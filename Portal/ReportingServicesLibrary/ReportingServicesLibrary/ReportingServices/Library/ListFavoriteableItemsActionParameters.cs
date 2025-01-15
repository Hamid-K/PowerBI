using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000023 RID: 35
	internal sealed class ListFavoriteableItemsActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000558F File Offset: 0x0000378F
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00005597 File Offset: 0x00003797
		public string ItemPath
		{
			get
			{
				return this._itemPath;
			}
			set
			{
				this._itemPath = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x000055A0 File Offset: 0x000037A0
		// (set) Token: 0x060000CA RID: 202 RVA: 0x000055A8 File Offset: 0x000037A8
		public bool Recursive
		{
			get
			{
				return this._recursive;
			}
			set
			{
				this._recursive = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000CB RID: 203 RVA: 0x000055B1 File Offset: 0x000037B1
		// (set) Token: 0x060000CC RID: 204 RVA: 0x000055B9 File Offset: 0x000037B9
		public FavoriteableCatalogItemList Items
		{
			get
			{
				return this._items;
			}
			set
			{
				this._items = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000CD RID: 205 RVA: 0x000055C2 File Offset: 0x000037C2
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}, {1}", this.ItemPath, this.Recursive);
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000055E4 File Offset: 0x000037E4
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException("ItemPath");
			}
		}

		// Token: 0x040000B8 RID: 184
		private string _itemPath;

		// Token: 0x040000B9 RID: 185
		private bool _recursive;

		// Token: 0x040000BA RID: 186
		private FavoriteableCatalogItemList _items;
	}
}
