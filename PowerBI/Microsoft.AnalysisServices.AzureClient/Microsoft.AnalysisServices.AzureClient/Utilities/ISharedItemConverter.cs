using System;

namespace Microsoft.AnalysisServices.AzureClient.Utilities
{
	// Token: 0x02000035 RID: 53
	internal interface ISharedItemConverter
	{
		// Token: 0x060001AF RID: 431
		void PrepareItemForCaching(string cacheName, ref object item, out string typeCode);

		// Token: 0x060001B0 RID: 432
		object ConvertCachedItem(string cacheName, object cachedItem, string typeCode);
	}
}
