using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000AE RID: 174
	internal interface IContentCacheManager
	{
		// Token: 0x060007F7 RID: 2039
		void CreateOrUpdateCache();

		// Token: 0x060007F8 RID: 2040
		void CreateOrUpdateCacheIfNeededAsync();
	}
}
