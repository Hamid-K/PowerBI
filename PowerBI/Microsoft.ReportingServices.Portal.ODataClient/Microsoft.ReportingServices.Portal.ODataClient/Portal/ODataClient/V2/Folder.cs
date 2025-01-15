using System;
using System.CodeDom.Compiler;
using System.Linq;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000034 RID: 52
	[Key("Id")]
	[EntitySet("Folders")]
	[OriginalName("Folder")]
	public class Folder : CatalogItem
	{
		// Token: 0x06000227 RID: 551 RVA: 0x000057CF File Offset: 0x000039CF
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

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000580A File Offset: 0x00003A0A
		// (set) Token: 0x06000229 RID: 553 RVA: 0x00005812 File Offset: 0x00003A12
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

		// Token: 0x0600022A RID: 554 RVA: 0x00005828 File Offset: 0x00003A28
		[OriginalName("SearchItems")]
		public DataServiceQuery<CatalogItem> SearchItems(string SearchText)
		{
			Uri uri;
			base.Context.TryGetUri(this, out uri);
			return base.Context.CreateFunctionQuery<CatalogItem>(string.Join("/", from s in uri.Segments.Skip(base.Context.BaseUri.Segments.Length)
				select s.Trim(new char[] { '/' })), "Model.SearchItems", false, new UriOperationParameter[]
			{
				new UriOperationParameter("SearchText", SearchText)
			});
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000058B4 File Offset: 0x00003AB4
		[OriginalName("Upload")]
		public DataServiceActionQuerySingle<Folder> Upload()
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuerySingle<Folder>(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.Upload", Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x0400011A RID: 282
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<CatalogItem> _CatalogItems = new DataServiceCollection<CatalogItem>(null, TrackingMode.None);
	}
}
