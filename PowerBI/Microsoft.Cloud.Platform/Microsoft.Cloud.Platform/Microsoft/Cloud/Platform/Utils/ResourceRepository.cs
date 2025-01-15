using System;
using System.Collections.Generic;
using System.Threading;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000249 RID: 585
	internal class ResourceRepository<TKey, TValue> where TKey : IEquatable<TKey>
	{
		// Token: 0x06000EFA RID: 3834 RVA: 0x000338A1 File Offset: 0x00031AA1
		public ResourceRepository(Func<object, TValue> createHandler, Action<TValue> destroyHandler, Predicate<TValue> checkStateHandler)
		{
			this.m_resources = new Dictionary<TKey, Resource<TValue>>();
			this.m_lock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
			this.m_createHandler = createHandler;
			this.m_destroyHandler = destroyHandler;
			this.m_checkStateHandler = checkStateHandler;
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x000338D8 File Offset: 0x00031AD8
		public Resource<TValue> Get([NotNull] TKey key, object createParams)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<TKey>(key, "Key");
			bool flag = false;
			bool flag2 = false;
			Resource<TValue> resource;
			Resource<TValue> resource2;
			for (;;)
			{
				flag = false;
				using (this.AcquireLock(flag2))
				{
					if (this.m_resources.TryGetValue(key, out resource))
					{
						if (this.m_checkStateHandler(resource.ResourceValue))
						{
							resource.IncRefernce();
							return resource;
						}
						flag = true;
					}
					if (!flag2)
					{
						flag2 = true;
						continue;
					}
					if (flag)
					{
						this.m_resources.Remove(key);
					}
					resource2 = new Resource<TValue>(this.m_createHandler(createParams));
					this.m_resources.Add(key, resource2);
				}
				break;
			}
			if (flag && resource.TryDestroy(ResourceOptions.ForceRemove))
			{
				this.m_destroyHandler(resource.ResourceValue);
			}
			return resource2;
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x000339A8 File Offset: 0x00031BA8
		public void Remove([NotNull] TKey key, Resource<TValue> value, ResourceOptions options)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<TKey>(key, "key");
			bool flag = false;
			using (this.AcquireLock(true))
			{
				if (value.TryDestroy(options))
				{
					flag = true;
					Resource<TValue> resource;
					if (this.m_resources.TryGetValue(key, out resource) && value == resource)
					{
						this.m_resources.Remove(key);
					}
				}
			}
			if (flag)
			{
				this.m_destroyHandler(value.ResourceValue);
				TraceSourceBase<UtilsTrace>.Tracer.TraceInformation("Destroying resource for key: '{0}'", new object[] { key });
			}
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x00033A44 File Offset: 0x00031C44
		public void RemoveAll()
		{
			Dictionary<TKey, Resource<TValue>> dictionary = null;
			using (this.AcquireLock(true))
			{
				dictionary = this.m_resources;
				this.m_resources = new Dictionary<TKey, Resource<TValue>>();
			}
			foreach (KeyValuePair<TKey, Resource<TValue>> keyValuePair in dictionary)
			{
				if (keyValuePair.Value.TryDestroy(ResourceOptions.ForceRemove))
				{
					this.m_destroyHandler(keyValuePair.Value.ResourceValue);
				}
			}
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x00033AE8 File Offset: 0x00031CE8
		public void EvictAll()
		{
			Dictionary<TKey, Resource<TValue>> dictionary = null;
			using (this.AcquireLock(true))
			{
				dictionary = this.m_resources;
				this.m_resources = new Dictionary<TKey, Resource<TValue>>();
			}
			foreach (KeyValuePair<TKey, Resource<TValue>> keyValuePair in dictionary)
			{
				keyValuePair.Value.Invalidate();
				if (keyValuePair.Value.TryDestroy(ResourceOptions.None))
				{
					this.m_destroyHandler(keyValuePair.Value.ResourceValue);
				}
			}
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x00033B98 File Offset: 0x00031D98
		private IDisposable AcquireLock(bool isWritter)
		{
			if (isWritter)
			{
				return new WriterLock(this.m_lock);
			}
			return new ReaderLock(this.m_lock);
		}

		// Token: 0x040005BC RID: 1468
		private Dictionary<TKey, Resource<TValue>> m_resources;

		// Token: 0x040005BD RID: 1469
		private readonly ReaderWriterLockSlim m_lock;

		// Token: 0x040005BE RID: 1470
		private readonly Func<object, TValue> m_createHandler;

		// Token: 0x040005BF RID: 1471
		private readonly Action<TValue> m_destroyHandler;

		// Token: 0x040005C0 RID: 1472
		private readonly Predicate<TValue> m_checkStateHandler;
	}
}
