using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Experimental.Insights.ServiceContracts.Internal;

namespace Microsoft.PowerBI.ExploreHost.Insights
{
	// Token: 0x02000086 RID: 134
	internal sealed class InsightsSessionStorage : IInsightsSessionStorage, IStorageService, IAsyncStorageService
	{
		// Token: 0x06000393 RID: 915 RVA: 0x0000B747 File Offset: 0x00009947
		public InsightsSessionStorage()
		{
			this.m_dictionary = new ConcurrentDictionary<string, object>();
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000B75A File Offset: 0x0000995A
		internal InsightsSessionStorage(ConcurrentDictionary<string, object> dictionary)
		{
			this.m_dictionary = dictionary;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000B769 File Offset: 0x00009969
		public void CacheItem<TItem>(string key, TItem item) where TItem : class
		{
			Contract.CheckValue<string>(key, "key");
			Contract.CheckValue<TItem>(item, "item");
			this.m_dictionary.TryAdd(key, item);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000B794 File Offset: 0x00009994
		public bool TryRetrieveItem<TItem>(string key, out TItem item) where TItem : class
		{
			Contract.CheckValue<string>(key, "key");
			object obj;
			this.m_dictionary.TryGetValue(key, out obj);
			item = obj as TItem;
			return item != null;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000B7DA File Offset: 0x000099DA
		public Task AddOrUpdateAsync<TItem>(string key, TItem item) where TItem : class
		{
			return Task.FromResult<bool>(false);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000B7E4 File Offset: 0x000099E4
		public Task<TItem> GetAsync<TItem>(string key) where TItem : class
		{
			return Task.FromResult<TItem>(default(TItem));
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000B7FF File Offset: 0x000099FF
		public void Clear()
		{
			ConcurrentDictionary<string, object> dictionary = this.m_dictionary;
			if (dictionary == null)
			{
				return;
			}
			dictionary.Clear();
		}

		// Token: 0x040001AC RID: 428
		private readonly ConcurrentDictionary<string, object> m_dictionary;
	}
}
