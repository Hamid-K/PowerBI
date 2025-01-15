using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.AdomdClient.Utilities
{
	// Token: 0x0200014D RID: 333
	internal sealed class ResourceManager : ResourceManagerBase, IDisposable
	{
		// Token: 0x06001098 RID: 4248 RVA: 0x000390B4 File Offset: 0x000372B4
		public ResourceManager()
			: base(null)
		{
			this.resources = new Dictionary<string, KeyValuePair<object, int>>();
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x000390C8 File Offset: 0x000372C8
		public ResourceManager(IEqualityComparer<string> comparer)
			: base(null)
		{
			this.resources = new Dictionary<string, KeyValuePair<object, int>>(comparer);
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x000390DD File Offset: 0x000372DD
		public ResourceManager(MemoryCache cache)
			: base(cache)
		{
			this.resources = new Dictionary<string, KeyValuePair<object, int>>();
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x000390F1 File Offset: 0x000372F1
		public ResourceManager(MemoryCache cache, IEqualityComparer<string> comparer)
			: base(cache)
		{
			this.resources = new Dictionary<string, KeyValuePair<object, int>>(comparer);
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x0600109C RID: 4252 RVA: 0x00039106 File Offset: 0x00037306
		// (set) Token: 0x0600109D RID: 4253 RVA: 0x0003910E File Offset: 0x0003730E
		public bool IsDisposed { get; private set; }

		// Token: 0x0600109E RID: 4254 RVA: 0x00039117 File Offset: 0x00037317
		protected override int GetItemCount()
		{
			return this.resources.Count;
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x00039124 File Offset: 0x00037324
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

		// Token: 0x060010A0 RID: 4256 RVA: 0x00039144 File Offset: 0x00037344
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

		// Token: 0x060010A1 RID: 4257 RVA: 0x000391FC File Offset: 0x000373FC
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

		// Token: 0x060010A2 RID: 4258 RVA: 0x000392C0 File Offset: 0x000374C0
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

		// Token: 0x060010A3 RID: 4259 RVA: 0x00039380 File Offset: 0x00037580
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

		// Token: 0x04000B1C RID: 2844
		private Dictionary<string, KeyValuePair<object, int>> resources;
	}
}
