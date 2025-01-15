using System;
using Microsoft.ReportingServices.Library;

namespace Microsoft.ReportingServices.Portal.Services.Extensions
{
	// Token: 0x02000063 RID: 99
	// (Invoke) Token: 0x06000309 RID: 777
	internal delegate void ResolveCatalogItem(Guid id, string path, ItemType itemType, bool throwIfNotExists, out Guid actualId, out string actualPath);
}
