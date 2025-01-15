using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.AdomdClient.Utilities
{
	// Token: 0x0200014C RID: 332
	internal abstract class ResourceManagerBase
	{
		// Token: 0x0600107D RID: 4221 RVA: 0x00038B3F File Offset: 0x00036D3F
		protected ResourceManagerBase(MemoryCacheBase cache)
		{
			this.cache = cache;
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x00038B4E File Offset: 0x00036D4E
		public int Count
		{
			get
			{
				return this.GetItemCount();
			}
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x00038B58 File Offset: 0x00036D58
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

		// Token: 0x06001080 RID: 4224 RVA: 0x00038BA4 File Offset: 0x00036DA4
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

		// Token: 0x06001081 RID: 4225 RVA: 0x00038BD4 File Offset: 0x00036DD4
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

		// Token: 0x06001082 RID: 4226 RVA: 0x00038C08 File Offset: 0x00036E08
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

		// Token: 0x06001083 RID: 4227 RVA: 0x00038C58 File Offset: 0x00036E58
		public void AddReference(string key)
		{
			object obj;
			if (!this.TryLocateResource(key, true, false, out obj))
			{
				throw new InvalidOperationException(RuntimeSR.Exception_ResourceMissingInPool(key));
			}
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x00038C80 File Offset: 0x00036E80
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

		// Token: 0x06001085 RID: 4229 RVA: 0x00038CD0 File Offset: 0x00036ED0
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

		// Token: 0x06001086 RID: 4230
		protected abstract int GetItemCount();

		// Token: 0x06001087 RID: 4231
		protected abstract bool TryLocateResource(string key, bool incrementRefCount, bool returnResource, out object resource);

		// Token: 0x06001088 RID: 4232
		protected abstract bool AddResource(string key, ref object resource);

		// Token: 0x06001089 RID: 4233
		protected abstract bool DecrementRefCount(string key, out bool isResourceRemoved, out object resource);

		// Token: 0x0600108A RID: 4234
		protected abstract IEnumerable<KeyValuePair<string, object>> ClearResources();

		// Token: 0x04000B0E RID: 2830
		private readonly MemoryCacheBase cache;
	}
}
