using System;
using System.Collections.Specialized;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200008A RID: 138
	public interface IInMemoryCacheFactory
	{
		// Token: 0x0600050C RID: 1292
		IInMemoryCache<T> CreateWithReferenceType<T>(string cacheName, NameValueCollection cacheSettings) where T : class;

		// Token: 0x0600050D RID: 1293
		IInMemoryCache<T> CreateWithValueType<T>(string cacheName, NameValueCollection cacheSettings);
	}
}
