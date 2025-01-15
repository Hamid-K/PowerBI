using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200023F RID: 575
	public class CachedResourceLifetimeManager<TKey, TValue> : IResourceLifetimeManager<TKey, TValue>, IResourceLifetimeManager, IDisposable where TKey : IEquatable<TKey>
	{
		// Token: 0x06000ED5 RID: 3797 RVA: 0x000334EC File Offset: 0x000316EC
		public CachedResourceLifetimeManager(IResourceOperations<TValue> operations)
		{
			this.m_repository = new ResourceRepository<TKey, TValue>(new Func<object, TValue>(operations.CreateResource), new Action<TValue>(operations.DestroyResource), new Predicate<TValue>(operations.IsHealtyResource));
			this.m_handleResourceFailure = new Func<TValue, Exception, bool>(operations.HandleResourceFailure);
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x00033544 File Offset: 0x00031744
		public IResourceHandle<TValue> Get(TKey key, object createParams)
		{
			Resource<TValue> resource = this.m_repository.Get(key, createParams);
			return new ResourceHandle<TKey, TValue>(key, resource);
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x00033568 File Offset: 0x00031768
		public void Release(IResourceHandle<TValue> resource)
		{
			ResourceHandle<TKey, TValue> resourceHandle = (ResourceHandle<TKey, TValue>)resource;
			resourceHandle.Resource.DecRefernce();
			this.m_repository.Remove(resourceHandle.Key, resourceHandle.Resource, ResourceOptions.None);
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x000335A0 File Offset: 0x000317A0
		public void ReportFaulted(IResourceHandle<TValue> resource, Exception e)
		{
			ResourceHandle<TKey, TValue> resourceHandle = (ResourceHandle<TKey, TValue>)resource;
			if (this.m_handleResourceFailure(resourceHandle.Value, e))
			{
				this.m_repository.Remove(resourceHandle.Key, resourceHandle.Resource, ResourceOptions.ForceRemove);
				return;
			}
			this.Release(resource);
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x000335E8 File Offset: 0x000317E8
		public void EvictAll()
		{
			if (this.m_repository != null)
			{
				this.m_repository.EvictAll();
			}
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x000335FD File Offset: 0x000317FD
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x00033606 File Offset: 0x00031806
		private void Dispose(bool disposing)
		{
			if (disposing && this.m_repository != null)
			{
				this.m_repository.RemoveAll();
				this.m_repository = null;
			}
		}

		// Token: 0x040005AB RID: 1451
		private ResourceRepository<TKey, TValue> m_repository;

		// Token: 0x040005AC RID: 1452
		private readonly Func<TValue, Exception, bool> m_handleResourceFailure;
	}
}
