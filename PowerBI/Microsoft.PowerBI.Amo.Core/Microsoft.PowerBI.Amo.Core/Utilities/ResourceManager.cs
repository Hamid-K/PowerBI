using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Utilities
{
	// Token: 0x02000142 RID: 322
	internal sealed class ResourceManager : ResourceManagerBase, IDisposable
	{
		// Token: 0x06001126 RID: 4390 RVA: 0x0003B9B8 File Offset: 0x00039BB8
		public ResourceManager()
			: base(null)
		{
			this.resources = new Dictionary<string, KeyValuePair<object, int>>();
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x0003B9CC File Offset: 0x00039BCC
		public ResourceManager(IEqualityComparer<string> comparer)
			: base(null)
		{
			this.resources = new Dictionary<string, KeyValuePair<object, int>>(comparer);
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x0003B9E1 File Offset: 0x00039BE1
		public ResourceManager(MemoryCache cache)
			: base(cache)
		{
			this.resources = new Dictionary<string, KeyValuePair<object, int>>();
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x0003B9F5 File Offset: 0x00039BF5
		public ResourceManager(MemoryCache cache, IEqualityComparer<string> comparer)
			: base(cache)
		{
			this.resources = new Dictionary<string, KeyValuePair<object, int>>(comparer);
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x0003BA0A File Offset: 0x00039C0A
		// (set) Token: 0x0600112B RID: 4395 RVA: 0x0003BA12 File Offset: 0x00039C12
		public bool IsDisposed { get; private set; }

		// Token: 0x0600112C RID: 4396 RVA: 0x0003BA1B File Offset: 0x00039C1B
		protected override int GetItemCount()
		{
			return this.resources.Count;
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x0003BA28 File Offset: 0x00039C28
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

		// Token: 0x0600112E RID: 4398 RVA: 0x0003BA48 File Offset: 0x00039C48
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

		// Token: 0x0600112F RID: 4399 RVA: 0x0003BB00 File Offset: 0x00039D00
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

		// Token: 0x06001130 RID: 4400 RVA: 0x0003BBC4 File Offset: 0x00039DC4
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

		// Token: 0x06001131 RID: 4401 RVA: 0x0003BC84 File Offset: 0x00039E84
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

		// Token: 0x04000AD5 RID: 2773
		private Dictionary<string, KeyValuePair<object, int>> resources;
	}
}
