using System;

namespace Microsoft.AnalysisServices.AdomdClient.Utilities
{
	// Token: 0x02000151 RID: 337
	internal interface ISharedItemConverter
	{
		// Token: 0x060010AF RID: 4271
		void PrepareItemForCaching(string cacheName, ref object item, out string typeCode);

		// Token: 0x060010B0 RID: 4272
		object ConvertCachedItem(string cacheName, object cachedItem, string typeCode);
	}
}
