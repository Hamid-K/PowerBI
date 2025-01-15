using System;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.ReportingServices.CatalogAccess
{
	// Token: 0x02000010 RID: 16
	public interface ICatalogItemAccessor
	{
		// Token: 0x060000D1 RID: 209
		Task<CatalogItemPropertiesEntity> GetCatalogItemPropertiesAsync(Guid catalogItemId);

		// Token: 0x060000D2 RID: 210
		Stream GetExtendedContentWritable(Guid catalogItemId, ExtendedContentType contentType, DateTime modifiedDate);

		// Token: 0x060000D3 RID: 211
		Stream GetExtendedContentReadable(Guid catalogItemId, ExtendedContentType contentType);

		// Token: 0x060000D4 RID: 212
		Task<bool> IsExtendedContentAvailable(Guid catalogItemId, ExtendedContentType contentType);

		// Token: 0x060000D5 RID: 213
		Task<int> DeleteExtendedContent(Guid catalogItemId, ExtendedContentType contentType);

		// Token: 0x060000D6 RID: 214
		Task<DateTime> GetCatalogExtendedContentLastUpdate(Guid catalogItemId, ExtendedContentType contentType);
	}
}
