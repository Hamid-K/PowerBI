using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000032 RID: 50
	[Key("Id")]
	[EntitySet("FavoriteItems")]
	[OriginalName("FavoriteItem")]
	public class FavoriteItem : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x06000213 RID: 531 RVA: 0x000054EA File Offset: 0x000036EA
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static FavoriteItem CreateFavoriteItem(Guid ID)
		{
			return new FavoriteItem
			{
				Id = ID
			};
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000214 RID: 532 RVA: 0x000054F8 File Offset: 0x000036F8
		// (set) Token: 0x06000215 RID: 533 RVA: 0x00005500 File Offset: 0x00003700
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Id")]
		public Guid Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
				this.OnPropertyChanged("Id");
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00005514 File Offset: 0x00003714
		// (set) Token: 0x06000217 RID: 535 RVA: 0x0000551C File Offset: 0x0000371C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Item")]
		public CatalogItem Item
		{
			get
			{
				return this._Item;
			}
			set
			{
				this._Item = value;
				this.OnPropertyChanged("Item");
			}
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000218 RID: 536 RVA: 0x00005530 File Offset: 0x00003730
		// (remove) Token: 0x06000219 RID: 537 RVA: 0x00005568 File Offset: 0x00003768
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600021A RID: 538 RVA: 0x0000559D File Offset: 0x0000379D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0400010F RID: 271
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x04000110 RID: 272
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private CatalogItem _Item;
	}
}
