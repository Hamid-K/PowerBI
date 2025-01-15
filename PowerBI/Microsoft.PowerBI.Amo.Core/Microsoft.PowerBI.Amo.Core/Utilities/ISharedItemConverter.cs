using System;

namespace Microsoft.AnalysisServices.Utilities
{
	// Token: 0x02000146 RID: 326
	internal interface ISharedItemConverter
	{
		// Token: 0x0600113D RID: 4413
		void PrepareItemForCaching(string cacheName, ref object item, out string typeCode);

		// Token: 0x0600113E RID: 4414
		object ConvertCachedItem(string cacheName, object cachedItem, string typeCode);
	}
}
