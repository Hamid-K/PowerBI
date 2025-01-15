using System;
using System.IO;
using Microsoft.ReportingServices.Library;
using Model;

namespace Microsoft.ReportingServices.Portal.Services.Extensions
{
	// Token: 0x0200005C RID: 92
	internal static class CatalogItemExtensions
	{
		// Token: 0x060002ED RID: 749 RVA: 0x00012A70 File Offset: 0x00010C70
		public static CatalogItemContent ToCatalogItemStreamContent(this global::Model.CatalogItem item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (item.Content == null && item.GetContentStream() == null)
			{
				throw new ArgumentException("item must have a byte or stream content.");
			}
			return new CatalogItemContent(item.GetContentStream() ?? new MemoryStream(item.Content));
		}
	}
}
