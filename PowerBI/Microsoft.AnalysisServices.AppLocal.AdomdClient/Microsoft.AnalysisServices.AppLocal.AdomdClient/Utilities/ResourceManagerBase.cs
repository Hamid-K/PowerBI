using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.AdomdClient.Utilities
{
	// Token: 0x0200014C RID: 332
	internal abstract class ResourceManagerBase
	{
		// Token: 0x0600108A RID: 4234 RVA: 0x00038E6F File Offset: 0x0003706F
		protected ResourceManagerBase(MemoryCacheBase cache)
		{
			this.cache = cache;
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x0600108B RID: 4235 RVA: 0x00038E7E File Offset: 0x0003707E
		public int Count
		{
			get
			{
				return this.GetItemCount();
			}
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x00038E88 File Offset: 0x00037088
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

		// Token: 0x0600108D RID: 4237 RVA: 0x00038ED4 File Offset: 0x000370D4
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

		// Token: 0x0600108E RID: 4238 RVA: 0x00038F04 File Offset: 0x00037104
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

		// Token: 0x0600108F RID: 4239 RVA: 0x00038F38 File Offset: 0x00037138
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

		// Token: 0x06001090 RID: 4240 RVA: 0x00038F88 File Offset: 0x00037188
		public void AddReference(string key)
		{
			object obj;
			if (!this.TryLocateResource(key, true, false, out obj))
			{
				throw new InvalidOperationException(RuntimeSR.Exception_ResourceMissingInPool(key));
			}
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x00038FB0 File Offset: 0x000371B0
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

		// Token: 0x06001092 RID: 4242 RVA: 0x00039000 File Offset: 0x00037200
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

		// Token: 0x06001093 RID: 4243
		protected abstract int GetItemCount();

		// Token: 0x06001094 RID: 4244
		protected abstract bool TryLocateResource(string key, bool incrementRefCount, bool returnResource, out object resource);

		// Token: 0x06001095 RID: 4245
		protected abstract bool AddResource(string key, ref object resource);

		// Token: 0x06001096 RID: 4246
		protected abstract bool DecrementRefCount(string key, out bool isResourceRemoved, out object resource);

		// Token: 0x06001097 RID: 4247
		protected abstract IEnumerable<KeyValuePair<string, object>> ClearResources();

		// Token: 0x04000B1B RID: 2843
		private readonly MemoryCacheBase cache;
	}
}
