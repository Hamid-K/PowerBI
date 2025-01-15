using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.BIServer.Configuration.Catalog;
using Microsoft.ReportingServices.CatalogAccess.Streams;

namespace Microsoft.ReportingServices.CatalogAccess
{
	// Token: 0x02000029 RID: 41
	public sealed class CatalogItemAccessor : ICatalogItemAccessor
	{
		// Token: 0x06000116 RID: 278 RVA: 0x00006AE0 File Offset: 0x00004CE0
		public async Task<CatalogItemPropertiesEntity> GetCatalogItemPropertiesAsync(Guid catalogItemId)
		{
			var <>f__AnonymousType = new
			{
				CatalogItemID = catalogItemId
			};
			return await CatalogAccessFactory.QueryFirstOrDefaultAsync<CatalogItemPropertiesEntity>("GetCatalogItemProperties", <>f__AnonymousType);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00006B25 File Offset: 0x00004D25
		public Stream GetExtendedContentWritable(Guid catalogItemId, ExtendedContentType contentType, DateTime modifiedDate)
		{
			return new ExtendedContentWritableStream(catalogItemId, contentType, modifiedDate);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00006B2F File Offset: 0x00004D2F
		public Stream GetExtendedContentReadable(Guid catalogItemId, ExtendedContentType contentType)
		{
			return VarbinaryReadableStreamFactory.CreateExtendedContentReadableStream(catalogItemId, contentType);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00006B38 File Offset: 0x00004D38
		public async Task<bool> IsExtendedContentAvailable(Guid catalogItemId, ExtendedContentType contentType)
		{
			var <>f__AnonymousType = new
			{
				CatalogItemId = catalogItemId,
				ContentType = contentType.ToString()
			};
			return await CatalogAccessFactory.QueryFirstOrDefaultAsync<bool>("IsCatalogExtendedContentAvailable", <>f__AnonymousType);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00006B88 File Offset: 0x00004D88
		public async Task<int> DeleteExtendedContent(Guid catalogItemId, ExtendedContentType contentType)
		{
			var <>f__AnonymousType = new
			{
				CatalogItemId = catalogItemId,
				ContentType = contentType.ToString()
			};
			return await CatalogAccessFactory.ExecuteAsync("DeleteCatalogExtendedContent", <>f__AnonymousType);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00006BD8 File Offset: 0x00004DD8
		public async Task<DateTime> GetCatalogExtendedContentLastUpdate(Guid catalogItemId, ExtendedContentType contentType)
		{
			var <>f__AnonymousType = new
			{
				CatalogItemId = catalogItemId,
				ContentType = contentType.ToString()
			};
			return await CatalogAccessFactory.QueryFirstOrDefaultAsync<DateTime>("GetCatalogExtendedContentLastUpdate", <>f__AnonymousType);
		}
	}
}
