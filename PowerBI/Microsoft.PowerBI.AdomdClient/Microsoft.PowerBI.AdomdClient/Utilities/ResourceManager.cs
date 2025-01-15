using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.AdomdClient.Utilities
{
	// Token: 0x0200014D RID: 333
	internal sealed class ResourceManager : ResourceManagerBase, IDisposable
	{
		// Token: 0x0600108B RID: 4235 RVA: 0x00038D84 File Offset: 0x00036F84
		public ResourceManager()
			: base(null)
		{
			this.resources = new Dictionary<string, KeyValuePair<object, int>>();
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x00038D98 File Offset: 0x00036F98
		public ResourceManager(IEqualityComparer<string> comparer)
			: base(null)
		{
			this.resources = new Dictionary<string, KeyValuePair<object, int>>(comparer);
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x00038DAD File Offset: 0x00036FAD
		public ResourceManager(MemoryCache cache)
			: base(cache)
		{
			this.resources = new Dictionary<string, KeyValuePair<object, int>>();
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x00038DC1 File Offset: 0x00036FC1
		public ResourceManager(MemoryCache cache, IEqualityComparer<string> comparer)
			: base(cache)
		{
			this.resources = new Dictionary<string, KeyValuePair<object, int>>(comparer);
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x0600108F RID: 4239 RVA: 0x00038DD6 File Offset: 0x00036FD6
		// (set) Token: 0x06001090 RID: 4240 RVA: 0x00038DDE File Offset: 0x00036FDE
		public bool IsDisposed { get; private set; }

		// Token: 0x06001091 RID: 4241 RVA: 0x00038DE7 File Offset: 0x00036FE7
		protected override int GetItemCount()
		{
			return this.resources.Count;
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x00038DF4 File Offset: 0x00036FF4
		public void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}
			base.Clear();
			GC.SuppressFinalize(this);
			this.IsDisposed = true;
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x00038E14 File Offset: 0x00037014
		protected override bool TryLocateResource(string key, bool incrementRefCount, bool returnResource, out object resource)
		{
			Dictionary<string, KeyValuePair<object, int>> dictionary = this.resources;
			lock (dictionary)
			{
				KeyValuePair<object, int> keyValuePair;
				if (!this.resources.TryGetValue(key, out keyValuePair))
				{
					resource = null;
					return false;
				}
				if (keyValuePair.Value == 0 && keyValuePair.Key is IDisposable)
				{
					this.resources.Remove(key);
					resource = null;
					return false;
				}
				resource = (returnResource ? keyValuePair.Key : null);
				if (incrementRefCount)
				{
					this.resources[key] = new KeyValuePair<object, int>(keyValuePair.Key, keyValuePair.Value + 1);
				}
			}
			return true;
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x00038ECC File Offset: 0x000370CC
		protected override bool AddResource(string key, ref object resource)
		{
			if (this.IsDisposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			Dictionary<string, KeyValuePair<object, int>> dictionary = this.resources;
			bool flag2;
			lock (dictionary)
			{
				KeyValuePair<object, int> keyValuePair;
				if (!this.resources.TryGetValue(key, out keyValuePair))
				{
					this.resources.Add(key, new KeyValuePair<object, int>(resource, 1));
					flag2 = true;
				}
				else
				{
					if (keyValuePair.Value > 0 || !(keyValuePair.Key is IDisposable))
					{
						resource = keyValuePair.Key;
					}
					this.resources[key] = new KeyValuePair<object, int>(resource, keyValuePair.Value + 1);
					flag2 = keyValuePair.Value == 0;
				}
			}
			return flag2;
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x00038F90 File Offset: 0x00037190
		protected override bool DecrementRefCount(string key, out bool isResourceRemoved, out object resource)
		{
			Dictionary<string, KeyValuePair<object, int>> dictionary = this.resources;
			bool flag2;
			lock (dictionary)
			{
				KeyValuePair<object, int> keyValuePair;
				if (!this.resources.TryGetValue(key, out keyValuePair))
				{
					isResourceRemoved = false;
					resource = null;
					flag2 = false;
				}
				else
				{
					if (keyValuePair.Value > 1)
					{
						this.resources[key] = new KeyValuePair<object, int>(keyValuePair.Key, keyValuePair.Value - 1);
						isResourceRemoved = false;
						resource = null;
					}
					else
					{
						if (!this.resources.Remove(key))
						{
							this.resources[key] = new KeyValuePair<object, int>(keyValuePair.Key, 0);
						}
						isResourceRemoved = true;
						resource = keyValuePair.Key;
					}
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x00039050 File Offset: 0x00037250
		protected override IEnumerable<KeyValuePair<string, object>> ClearResources()
		{
			Dictionary<string, KeyValuePair<object, int>> dictionary = this.resources;
			IEnumerable<KeyValuePair<string, object>> enumerable;
			lock (dictionary)
			{
				List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>(this.resources.Count);
				foreach (KeyValuePair<string, KeyValuePair<object, int>> keyValuePair in this.resources)
				{
					list.Add(new KeyValuePair<string, object>(keyValuePair.Key, keyValuePair.Value.Key));
				}
				this.resources.Clear();
				enumerable = list;
			}
			return enumerable;
		}

		// Token: 0x04000B0F RID: 2831
		private Dictionary<string, KeyValuePair<object, int>> resources;
	}
}
