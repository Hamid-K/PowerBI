using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000EF RID: 239
	[Key("Id")]
	[OriginalName("Folder")]
	public class Folder : CatalogItem
	{
		// Token: 0x06000AAF RID: 2735 RVA: 0x000152BD File Offset: 0x000134BD
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static Folder CreateFolder(Guid ID, CatalogItemType type, bool hidden, long size, DateTimeOffset modifiedDate, DateTimeOffset createdDate, bool isFavorite)
		{
			return new Folder
			{
				Id = ID,
				Type = type,
				Hidden = hidden,
				Size = size,
				ModifiedDate = modifiedDate,
				CreatedDate = createdDate,
				IsFavorite = isFavorite
			};
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x000152F8 File Offset: 0x000134F8
		// (set) Token: 0x06000AB1 RID: 2737 RVA: 0x00015300 File Offset: 0x00013500
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("CatalogItems")]
		public DataServiceCollection<CatalogItem> CatalogItems
		{
			get
			{
				return this._CatalogItems;
			}
			set
			{
				this._CatalogItems = value;
				this.OnPropertyChanged("CatalogItems");
			}
		}

		// Token: 0x040004E7 RID: 1255
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<CatalogItem> _CatalogItems = new DataServiceCollection<CatalogItem>(null, TrackingMode.None);
	}
}
