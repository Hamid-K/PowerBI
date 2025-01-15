using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000245 RID: 581
	public class MultiResourceLifetimeManager<TKey, TValue> : IResourceLifetimeManager<TKey, TValue>, IResourceLifetimeManager, IDisposable where TKey : IEquatable<TKey>
	{
		// Token: 0x06000EE5 RID: 3813 RVA: 0x00033625 File Offset: 0x00031825
		public MultiResourceLifetimeManager(LifetimeManagerType lifetimeManagerType, int numOfResources, IResourceOperations<TValue> operations)
		{
			this.m_numOfResources = numOfResources;
			if (lifetimeManagerType == LifetimeManagerType.Cached)
			{
				this.m_internalResourceLifetimeManager = new CachedResourceLifetimeManager<EquatablePair<TKey, int>, TValue>(operations);
			}
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x00033644 File Offset: 0x00031844
		public IResourceHandle<TValue> Get(TKey key, object state)
		{
			int i = Randomizer.GetI32(this.m_numOfResources);
			return this.m_internalResourceLifetimeManager.Get(new EquatablePair<TKey, int>(key, i), state);
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x00033670 File Offset: 0x00031870
		public void Release(IResourceHandle<TValue> resource)
		{
			this.m_internalResourceLifetimeManager.Release(resource);
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0003367E File Offset: 0x0003187E
		public void EvictAll()
		{
			this.m_internalResourceLifetimeManager.EvictAll();
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0003368B File Offset: 0x0003188B
		public void ReportFaulted(IResourceHandle<TValue> resource, Exception e)
		{
			this.m_internalResourceLifetimeManager.ReportFaulted(resource, e);
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0003369A File Offset: 0x0003189A
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x000336A3 File Offset: 0x000318A3
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.m_internalResourceLifetimeManager.Dispose();
			}
		}

		// Token: 0x040005AF RID: 1455
		private readonly int m_numOfResources;

		// Token: 0x040005B0 RID: 1456
		private readonly IResourceLifetimeManager<EquatablePair<TKey, int>, TValue> m_internalResourceLifetimeManager;
	}
}
