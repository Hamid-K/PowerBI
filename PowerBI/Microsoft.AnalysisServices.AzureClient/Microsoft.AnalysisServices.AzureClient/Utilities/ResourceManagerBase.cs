using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.AzureClient.Utilities
{
	// Token: 0x02000030 RID: 48
	internal abstract class ResourceManagerBase
	{
		// Token: 0x0600018A RID: 394 RVA: 0x000078BB File Offset: 0x00005ABB
		protected ResourceManagerBase(MemoryCacheBase cache)
		{
			this.cache = cache;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600018B RID: 395 RVA: 0x000078CA File Offset: 0x00005ACA
		public int Count
		{
			get
			{
				return this.GetItemCount();
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000078D4 File Offset: 0x00005AD4
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

		// Token: 0x0600018D RID: 397 RVA: 0x00007920 File Offset: 0x00005B20
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

		// Token: 0x0600018E RID: 398 RVA: 0x00007950 File Offset: 0x00005B50
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

		// Token: 0x0600018F RID: 399 RVA: 0x00007984 File Offset: 0x00005B84
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

		// Token: 0x06000190 RID: 400 RVA: 0x000079D4 File Offset: 0x00005BD4
		public void AddReference(string key)
		{
			object obj;
			if (!this.TryLocateResource(key, true, false, out obj))
			{
				throw new InvalidOperationException(RuntimeSR.Exception_ResourceMissingInPool(key));
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000079FC File Offset: 0x00005BFC
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

		// Token: 0x06000192 RID: 402 RVA: 0x00007A4C File Offset: 0x00005C4C
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

		// Token: 0x06000193 RID: 403
		protected abstract int GetItemCount();

		// Token: 0x06000194 RID: 404
		protected abstract bool TryLocateResource(string key, bool incrementRefCount, bool returnResource, out object resource);

		// Token: 0x06000195 RID: 405
		protected abstract bool AddResource(string key, ref object resource);

		// Token: 0x06000196 RID: 406
		protected abstract bool DecrementRefCount(string key, out bool isResourceRemoved, out object resource);

		// Token: 0x06000197 RID: 407
		protected abstract IEnumerable<KeyValuePair<string, object>> ClearResources();

		// Token: 0x040000D9 RID: 217
		private readonly MemoryCacheBase cache;
	}
}
