using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.ReportingServices.CatalogAccess.Streams
{
	// Token: 0x02000031 RID: 49
	public static class VarbinaryReadableStreamFactory
	{
		// Token: 0x06000153 RID: 339 RVA: 0x00007474 File Offset: 0x00005674
		public static Stream CreateExtendedContentReadableStream(Guid catalogItemId, ExtendedContentType contentType)
		{
			return new VarbinaryReadableStream("GetCatalogExtendedContentData", new Dictionary<string, object>
			{
				{ "@CatalogItemId", catalogItemId },
				{
					"@ContentType",
					contentType.ToString()
				}
			});
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000074BC File Offset: 0x000056BC
		public static Stream CreateCatalogContentReadableStream(Guid catalogItemId)
		{
			return new VarbinaryReadableStream("GetCatalogContentData", new Dictionary<string, object> { { "@CatalogItemID", catalogItemId } });
		}
	}
}
