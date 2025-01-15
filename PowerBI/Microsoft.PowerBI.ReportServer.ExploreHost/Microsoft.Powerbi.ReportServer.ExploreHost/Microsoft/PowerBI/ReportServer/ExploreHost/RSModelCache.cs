using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using Microsoft.PowerBI.Telemetry;

namespace Microsoft.PowerBI.ReportServer.ExploreHost
{
	// Token: 0x02000016 RID: 22
	internal sealed class RSModelCache
	{
		// Token: 0x0600007F RID: 127 RVA: 0x00003004 File Offset: 0x00001204
		private void CacheExpiredCallBack(CacheEntryRemovedArguments arguments)
		{
			RSConnectionTrace.Trace(TraceType.Verbose, "CSDL Cache Entry for Id={0} removed for reason={1}", new object[]
			{
				arguments.CacheItem.Key,
				arguments.RemovedReason.ToString()
			});
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003048 File Offset: 0x00001248
		public ModelInfo GetOrAddModel(ModelKey key, Func<ModelInfo> modelInfoFunc)
		{
			ModelInfo modelInfo = this.Get(key);
			if (modelInfo == null)
			{
				this.Add(key, modelInfoFunc);
				modelInfo = this.Get(key);
			}
			else
			{
				RSConnectionTrace.Trace(TraceType.Verbose, "Model {0} retrieved from the cache...", new object[] { key.CacheKey });
			}
			return modelInfo;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000308E File Offset: 0x0000128E
		public bool ContainsKey(ModelKey key)
		{
			return this._cache.Contains(key.CacheKey, null);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000030A2 File Offset: 0x000012A2
		public void Remove(ModelKey key)
		{
			this._cache.Remove(key.CacheKey, null);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000030B8 File Offset: 0x000012B8
		public void Clear()
		{
			foreach (KeyValuePair<string, object> keyValuePair in this._cache.ToArray<KeyValuePair<string, object>>())
			{
				this._cache.Remove(keyValuePair.Key, null);
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000030FC File Offset: 0x000012FC
		private bool Add(ModelKey key, Func<ModelInfo> modelInfoFunc)
		{
			return this._cache.AddOrGetExisting(key.CacheKey, new Lazy<ModelInfo>(modelInfoFunc), new CacheItemPolicy
			{
				SlidingExpiration = TimeSpan.FromMinutes(10.0),
				RemovedCallback = new CacheEntryRemovedCallback(this.CacheExpiredCallBack)
			}, null) == null;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003150 File Offset: 0x00001350
		private ModelInfo Get(ModelKey key)
		{
			Lazy<ModelInfo> lazy = (Lazy<ModelInfo>)this._cache.Get(key.CacheKey, null);
			if (lazy == null)
			{
				return null;
			}
			return lazy.Value;
		}

		// Token: 0x04000055 RID: 85
		private readonly MemoryCache _cache = new MemoryCache("RSCSDLCache", null);
	}
}
