using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.AzureClient.Utilities
{
	// Token: 0x02000031 RID: 49
	internal sealed class ResourceManager : ResourceManagerBase, IDisposable
	{
		// Token: 0x06000198 RID: 408 RVA: 0x00007B00 File Offset: 0x00005D00
		public ResourceManager()
			: base(null)
		{
			this.resources = new Dictionary<string, KeyValuePair<object, int>>();
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00007B14 File Offset: 0x00005D14
		public ResourceManager(IEqualityComparer<string> comparer)
			: base(null)
		{
			this.resources = new Dictionary<string, KeyValuePair<object, int>>(comparer);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00007B29 File Offset: 0x00005D29
		public ResourceManager(MemoryCache cache)
			: base(cache)
		{
			this.resources = new Dictionary<string, KeyValuePair<object, int>>();
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00007B3D File Offset: 0x00005D3D
		public ResourceManager(MemoryCache cache, IEqualityComparer<string> comparer)
			: base(cache)
		{
			this.resources = new Dictionary<string, KeyValuePair<object, int>>(comparer);
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00007B52 File Offset: 0x00005D52
		// (set) Token: 0x0600019D RID: 413 RVA: 0x00007B5A File Offset: 0x00005D5A
		public bool IsDisposed { get; private set; }

		// Token: 0x0600019E RID: 414 RVA: 0x00007B63 File Offset: 0x00005D63
		protected override int GetItemCount()
		{
			return this.resources.Count;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00007B70 File Offset: 0x00005D70
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

		// Token: 0x060001A0 RID: 416 RVA: 0x00007B90 File Offset: 0x00005D90
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

		// Token: 0x060001A1 RID: 417 RVA: 0x00007C48 File Offset: 0x00005E48
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

		// Token: 0x060001A2 RID: 418 RVA: 0x00007D0C File Offset: 0x00005F0C
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

		// Token: 0x060001A3 RID: 419 RVA: 0x00007DCC File Offset: 0x00005FCC
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

		// Token: 0x040000DA RID: 218
		private Dictionary<string, KeyValuePair<object, int>> resources;
	}
}
