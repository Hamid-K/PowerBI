using System;

namespace Microsoft.AnalysisServices.AdomdClient.Utilities
{
	// Token: 0x02000151 RID: 337
	internal interface ISharedItemConverter
	{
		// Token: 0x060010A2 RID: 4258
		void PrepareItemForCaching(string cacheName, ref object item, out string typeCode);

		// Token: 0x060010A3 RID: 4259
		object ConvertCachedItem(string cacheName, object cachedItem, string typeCode);
	}
}
