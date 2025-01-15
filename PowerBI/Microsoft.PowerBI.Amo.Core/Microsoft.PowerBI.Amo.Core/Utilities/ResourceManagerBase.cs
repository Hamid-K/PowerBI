using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Utilities
{
	// Token: 0x02000141 RID: 321
	internal abstract class ResourceManagerBase
	{
		// Token: 0x06001118 RID: 4376 RVA: 0x0003B773 File Offset: 0x00039973
		protected ResourceManagerBase(MemoryCacheBase cache)
		{
			this.cache = cache;
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001119 RID: 4377 RVA: 0x0003B782 File Offset: 0x00039982
		public int Count
		{
			get
			{
				return this.GetItemCount();
			}
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x0003B78C File Offset: 0x0003998C
		public bool TryGetResource(string key, out object resource)
		{
			if (this.TryLocateResource(key, true, true, out resource))
			{
				return true;
			}
			if (this.cache != null && this.cache.Lookup(key, out resource))
			{
				this.AddResource(key, ref resource);
				this.cache.Remove(key, false);
				return true;
			}
			return false;
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x0003B7D8 File Offset: 0x000399D8
		public bool TryGetResource<TResource>(string key, out TResource resource)
		{
			object obj;
			if (!this.TryGetResource(key, out obj))
			{
				resource = default(TResource);
				return false;
			}
			resource = (TResource)((object)obj);
			return true;
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x0003B808 File Offset: 0x00039A08
		public bool Insert(string key, ref object resource)
		{
			object obj = resource;
			if (!this.AddResource(key, ref obj))
			{
				IDisposable disposable = resource as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
				resource = obj;
				return false;
			}
			return true;
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x0003B83C File Offset: 0x00039A3C
		public bool Insert<TResource>(string key, ref TResource resource)
		{
			object obj = resource;
			if (!this.AddResource(key, ref obj))
			{
				IDisposable disposable = resource as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
				resource = (TResource)((object)obj);
				return false;
			}
			return true;
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x0003B88C File Offset: 0x00039A8C
		public void AddReference(string key)
		{
			object obj;
			if (!this.TryLocateResource(key, true, false, out obj))
			{
				throw new InvalidOperationException(RuntimeSR.Exception_ResourceMissingInPool(key));
			}
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x0003B8B4 File Offset: 0x00039AB4
		public void Remove(string key)
		{
			bool flag;
			object obj;
			if (!this.DecrementRefCount(key, out flag, out obj))
			{
				throw new InvalidOperationException(RuntimeSR.Exception_ResourceMissingInPool(key));
			}
			if (flag)
			{
				if (this.cache != null)
				{
					this.cache.Insert(key, obj);
					return;
				}
				IDisposable disposable = obj as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x0003B904 File Offset: 0x00039B04
		public void Clear()
		{
			if (this.cache != null)
			{
				using (IEnumerator<KeyValuePair<string, object>> enumerator = this.ClearResources().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, object> keyValuePair = enumerator.Current;
						this.cache.Insert(keyValuePair.Key, keyValuePair.Value);
					}
					return;
				}
			}
			foreach (KeyValuePair<string, object> keyValuePair2 in this.ClearResources())
			{
				IDisposable disposable = keyValuePair2.Value as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x06001121 RID: 4385
		protected abstract int GetItemCount();

		// Token: 0x06001122 RID: 4386
		protected abstract bool TryLocateResource(string key, bool incrementRefCount, bool returnResource, out object resource);

		// Token: 0x06001123 RID: 4387
		protected abstract bool AddResource(string key, ref object resource);

		// Token: 0x06001124 RID: 4388
		protected abstract bool DecrementRefCount(string key, out bool isResourceRemoved, out object resource);

		// Token: 0x06001125 RID: 4389
		protected abstract IEnumerable<KeyValuePair<string, object>> ClearResources();

		// Token: 0x04000AD4 RID: 2772
		private readonly MemoryCacheBase cache;
	}
}
