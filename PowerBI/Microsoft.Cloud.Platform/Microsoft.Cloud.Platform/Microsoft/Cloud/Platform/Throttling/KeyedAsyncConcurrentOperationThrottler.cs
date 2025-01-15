using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Cloud.Platform.Utils;
using Microsoft.PowerBI.ServiceContracts;

namespace Microsoft.Cloud.Platform.Throttling
{
	// Token: 0x020000FF RID: 255
	public sealed class KeyedAsyncConcurrentOperationThrottler<TKey> : IDisposable
	{
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x00018CEC File Offset: 0x00016EEC
		// (set) Token: 0x06000714 RID: 1812 RVA: 0x00009B3B File Offset: 0x00007D3B
		public IDictionary<TKey, int> KeyToNumOfQueuedOperation
		{
			get
			{
				object syncRoot = this.m_syncRoot;
				IDictionary<TKey, int> dictionary2;
				lock (syncRoot)
				{
					Dictionary<TKey, int> dictionary = new Dictionary<TKey, int>();
					foreach (LRUCache<TKey, AsyncConcurrentOperationThrottler>.KeyValue keyValue in this.m_keysToThrottlers)
					{
						dictionary.Add(keyValue.Key, (int)keyValue.Value.QueuedOperationsCount);
					}
					dictionary2 = dictionary;
				}
				return dictionary2;
			}
			private set
			{
			}
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00018D84 File Offset: 0x00016F84
		public KeyedAsyncConcurrentOperationThrottler(int maxNumKeys, int maxConcurrentOperationsPerKey, int maxQueuedOperationsPerKey, ThrottlingBehavior behavior, MaxKeysExceededBehvaior maxKeysBehavior)
		{
			ExtendedDiagnostics.EnsureArgumentIsPositive(maxNumKeys, "maxNumKeys");
			ExtendedDiagnostics.EnsureArgumentIsPositive(maxConcurrentOperationsPerKey, "maxConcurrentOperationsPerKey");
			ExtendedDiagnostics.EnsureArgumentIsPositive(maxQueuedOperationsPerKey, "maxQueuedOperationsPerKey");
			this.m_maxNumKeys = maxNumKeys;
			this.m_maxConcurrentOperationsPerKey = maxConcurrentOperationsPerKey;
			this.m_maxQueuedRequestsPerKey = maxQueuedOperationsPerKey;
			this.m_behavior = behavior;
			this.m_maxKeysBehavior = maxKeysBehavior;
			this.m_keysToThrottlers = new LRUCache<TKey, AsyncConcurrentOperationThrottler>(maxNumKeys);
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x00018DF4 File Offset: 0x00016FF4
		public int NumTrackedKeys
		{
			get
			{
				object syncRoot = this.m_syncRoot;
				int size;
				lock (syncRoot)
				{
					size = this.m_keysToThrottlers.Size;
				}
				return size;
			}
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x00018E3C File Offset: 0x0001703C
		public void Dispose()
		{
			object syncRoot = this.m_syncRoot;
			lock (syncRoot)
			{
				foreach (LRUCache<TKey, AsyncConcurrentOperationThrottler>.KeyValue keyValue in this.m_keysToThrottlers)
				{
					keyValue.Value.Dispose();
				}
			}
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00018EB4 File Offset: 0x000170B4
		public Task<IDisposable> ThrottleConcurrentRequestAsync(TKey key)
		{
			return this.ThrottleConcurrentRequestsAsync(key, 1L);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00018EBF File Offset: 0x000170BF
		public Task<IDisposable> ThrottleConcurrentRequestsAsync(TKey key, long numRequests)
		{
			TryGetResult<Task<IDisposable>> tryGetResult = this.TryThrottleConcurrentRequestsAsync(key, numRequests);
			if (!tryGetResult)
			{
				throw new OperationThrottledException();
			}
			return tryGetResult.Result;
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00018EDC File Offset: 0x000170DC
		public TryGetResult<Task<IDisposable>> TryThrottleConcurrentRequestsAsync(TKey key)
		{
			return this.TryThrottleConcurrentRequestsAsync(key, 1L);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00018EE8 File Offset: 0x000170E8
		public TryGetResult<Task<IDisposable>> TryThrottleConcurrentRequestsAsync(TKey key, long numRequests)
		{
			KeyedAsyncConcurrentOperationThrottler<TKey>.<>c__DisplayClass17_0 CS$<>8__locals1 = new KeyedAsyncConcurrentOperationThrottler<TKey>.<>c__DisplayClass17_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.key = key;
			object syncRoot = this.m_syncRoot;
			lock (syncRoot)
			{
				AsyncConcurrentOperationThrottler asyncConcurrentOperationThrottler;
				if (this.m_keysToThrottlers.TryGet(CS$<>8__locals1.key, out asyncConcurrentOperationThrottler))
				{
					CS$<>8__locals1.throttlingUnitTaskResult = asyncConcurrentOperationThrottler.TryThrottleConcurrentRequestsAsync(numRequests, 0.0);
				}
				else
				{
					AsyncConcurrentOperationThrottler asyncConcurrentOperationThrottler2 = new AsyncConcurrentOperationThrottler((long)this.m_maxConcurrentOperationsPerKey, (long)this.m_maxQueuedRequestsPerKey, this.m_behavior, ThrottlerQueueingBehavior.Fifo);
					AsyncConcurrentOperationThrottler asyncConcurrentOperationThrottler3 = this.m_keysToThrottlers.Set(CS$<>8__locals1.key, asyncConcurrentOperationThrottler2);
					if (asyncConcurrentOperationThrottler3 != null)
					{
						TraceSourceBase<UtilsTrace>.Tracer.TraceWarning("Overloaded KeyedAsyncConcurrentOperationThrottler for key: {0}", new object[] { CS$<>8__locals1.key });
						asyncConcurrentOperationThrottler3.ReleaseAllRequests(this.m_maxKeysBehavior);
					}
					CS$<>8__locals1.throttlingUnitTaskResult = asyncConcurrentOperationThrottler2.TryThrottleConcurrentRequestsAsync(numRequests, 0.0);
				}
			}
			if (!CS$<>8__locals1.throttlingUnitTaskResult)
			{
				return CS$<>8__locals1.throttlingUnitTaskResult;
			}
			return TryGetResult.Create<Task<IDisposable>>(CS$<>8__locals1.<TryThrottleConcurrentRequestsAsync>g__ReleaseThrottlingUnit|0());
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00019004 File Offset: 0x00017204
		private void RemoveThrottlerIfEmpty(TKey throttlerKey)
		{
			object syncRoot = this.m_syncRoot;
			lock (syncRoot)
			{
				AsyncConcurrentOperationThrottler asyncConcurrentOperationThrottler;
				if (this.m_keysToThrottlers.TryGet(throttlerKey, out asyncConcurrentOperationThrottler) && asyncConcurrentOperationThrottler.IsEmpty)
				{
					this.m_keysToThrottlers.TryRemove(throttlerKey, out asyncConcurrentOperationThrottler);
					asyncConcurrentOperationThrottler.Dispose();
				}
			}
		}

		// Token: 0x04000269 RID: 617
		private readonly object m_syncRoot = new object();

		// Token: 0x0400026A RID: 618
		private readonly LRUCache<TKey, AsyncConcurrentOperationThrottler> m_keysToThrottlers;

		// Token: 0x0400026B RID: 619
		private readonly int m_maxNumKeys;

		// Token: 0x0400026C RID: 620
		private readonly int m_maxConcurrentOperationsPerKey;

		// Token: 0x0400026D RID: 621
		private readonly int m_maxQueuedRequestsPerKey;

		// Token: 0x0400026E RID: 622
		private readonly ThrottlingBehavior m_behavior;

		// Token: 0x0400026F RID: 623
		private readonly MaxKeysExceededBehvaior m_maxKeysBehavior;

		// Token: 0x020005E9 RID: 1513
		private sealed class ThrottlerKeyReleaser : IDisposable
		{
			// Token: 0x06002BE8 RID: 11240 RVA: 0x0009B53A File Offset: 0x0009973A
			public ThrottlerKeyReleaser(KeyedAsyncConcurrentOperationThrottler<TKey> parent, TKey throttlerKey, IDisposable throttlingUnit)
			{
				this.m_parent = parent;
				this.m_throttlerKey = throttlerKey;
				this.m_throttlingUnit = throttlingUnit;
			}

			// Token: 0x06002BE9 RID: 11241 RVA: 0x0009B557 File Offset: 0x00099757
			public void Dispose()
			{
				this.m_throttlingUnit.Dispose();
				this.m_parent.RemoveThrottlerIfEmpty(this.m_throttlerKey);
			}

			// Token: 0x0400100E RID: 4110
			private readonly KeyedAsyncConcurrentOperationThrottler<TKey> m_parent;

			// Token: 0x0400100F RID: 4111
			private readonly TKey m_throttlerKey;

			// Token: 0x04001010 RID: 4112
			private readonly IDisposable m_throttlingUnit;
		}
	}
}
