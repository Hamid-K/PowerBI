using System;
using System.Collections.Specialized;
using Microsoft.Cloud.Platform.Modularization;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200008C RID: 140
	[BlockServiceProvider(typeof(IInMemoryCacheFactory))]
	public class InMemoryCacheFactory : Block, IInMemoryCacheFactory
	{
		// Token: 0x06000516 RID: 1302 RVA: 0x00010CDA File Offset: 0x0000EEDA
		public InMemoryCacheFactory()
			: base(typeof(InMemoryCacheFactory).Name)
		{
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00010CF1 File Offset: 0x0000EEF1
		public IInMemoryCache<T> CreateWithReferenceType<T>(string cacheName, NameValueCollection cacheSettings) where T : class
		{
			return new InMemoryCache<T>(cacheName, cacheSettings);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00010CFA File Offset: 0x0000EEFA
		public IInMemoryCache<T> CreateWithValueType<T>(string cacheName, NameValueCollection cacheSettings)
		{
			return new InMemoryCache<T>(cacheName, cacheSettings);
		}
	}
}
