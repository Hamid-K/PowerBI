using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.AnalysisServices.AdomdClient.Utilities
{
	// Token: 0x02000153 RID: 339
	internal sealed class SharedResourceManager : ResourceManagerBase
	{
		// Token: 0x060010C1 RID: 4289 RVA: 0x00039CE7 File Offset: 0x00037EE7
		private SharedResourceManager(string managerName, SharedResourceManager.GlobalNames names, IDictionary<string, object> resources, IDictionary<string, KeyValuePair<string, int>> metadata, SharedMemoryCache cache, ISharedItemConverter converter)
			: base(cache)
		{
			this.ManagerName = managerName;
			this.names = names;
			this.resources = resources;
			this.metadata = metadata;
			this.converter = converter;
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x060010C2 RID: 4290 RVA: 0x00039D16 File Offset: 0x00037F16
		public string ManagerName { get; }

		// Token: 0x060010C3 RID: 4291 RVA: 0x00039D20 File Offset: 0x00037F20
		public static SharedResourceManager Create(string managerName, IEqualityComparer<string> comparer = null, SharedMemoryCache cache = null, ISharedItemConverter converter = null)
		{
			SharedResourceManager.GlobalNames globalNames = new SharedResourceManager.GlobalNames(managerName);
			IDictionary<string, object> dictionary;
			IDictionary<string, KeyValuePair<string, int>> dictionary2;
			using (GlobalContext.CreateGlobalLockScope(globalNames.Lock))
			{
				if (!GlobalContext.TryGetGlobalObject<IDictionary<string, object>>(globalNames.Resources, out dictionary))
				{
					dictionary = ((comparer != null) ? new Dictionary<string, object>(comparer) : new Dictionary<string, object>());
					GlobalContext.SetGlobalObject(globalNames.Resources, dictionary);
					dictionary2 = ((comparer != null) ? new Dictionary<string, KeyValuePair<string, int>>(comparer) : new Dictionary<string, KeyValuePair<string, int>>());
					GlobalContext.SetGlobalObject(globalNames.Metadata, dictionary2);
				}
				else
				{
					dictionary2 = GlobalContext.GetGlobalObject<IDictionary<string, KeyValuePair<string, int>>>(globalNames.Metadata);
				}
			}
			return new SharedResourceManager(managerName, globalNames, dictionary, dictionary2, cache, converter);
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x00039DC0 File Offset: 0x00037FC0
		public static SharedResourceManager Create(string managerName, IEqualityComparer<string> comparer, SharedMemoryCache cache, PrepareItemForCaching prepareMethod, ConvertCachedItem convertMethod)
		{
			return SharedResourceManager.Create(managerName, comparer, cache, new SharedItemConverter(prepareMethod, convertMethod));
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x00039DD2 File Offset: 0x00037FD2
		protected override int GetItemCount()
		{
			return this.resources.Count;
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x00039DE0 File Offset: 0x00037FE0
		protected override bool TryLocateResource(string key, bool incrementRefCount, bool returnResource, out object resource)
		{
			KeyValuePair<string, int> keyValuePair;
			using (GlobalContext.CreateGlobalLockScope(this.names.Lock))
			{
				if (!this.metadata.TryGetValue(key, out keyValuePair))
				{
					resource = null;
					return false;
				}
				if (keyValuePair.Value == 0)
				{
					this.resources.Remove(key);
					resource = null;
					return false;
				}
				resource = (returnResource ? this.resources[key] : null);
				if (incrementRefCount)
				{
					this.metadata[key] = new KeyValuePair<string, int>(keyValuePair.Key, keyValuePair.Value + 1);
				}
			}
			if (returnResource && this.converter != null && !string.IsNullOrEmpty(keyValuePair.Key))
			{
				resource = this.converter.ConvertCachedItem(this.ManagerName, resource, keyValuePair.Key);
			}
			return true;
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x00039EC8 File Offset: 0x000380C8
		protected override bool AddResource(string key, ref object resource)
		{
			object obj = resource;
			string text;
			if (this.converter != null)
			{
				this.converter.PrepareItemForCaching(this.ManagerName, ref obj, out text);
			}
			else
			{
				text = null;
			}
			bool flag;
			using (GlobalContext.CreateGlobalLockScope(this.names.Lock))
			{
				object obj2;
				KeyValuePair<string, int> keyValuePair;
				if (!this.resources.TryGetValue(key, out obj2) || !this.metadata.TryGetValue(key, out keyValuePair) || keyValuePair.Value == 0)
				{
					this.resources[key] = obj;
					this.metadata[key] = new KeyValuePair<string, int>(text, 1);
					flag = true;
				}
				else
				{
					obj = obj2;
					text = keyValuePair.Key;
					this.metadata[key] = new KeyValuePair<string, int>(keyValuePair.Key, keyValuePair.Value + 1);
					flag = false;
				}
			}
			if (!flag)
			{
				if (this.converter != null && !string.IsNullOrEmpty(text))
				{
					resource = this.converter.ConvertCachedItem(this.ManagerName, obj, text);
				}
				else
				{
					resource = obj;
				}
			}
			return flag;
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x00039FD0 File Offset: 0x000381D0
		protected override bool DecrementRefCount(string key, out bool isResourceRemoved, out object resource)
		{
			KeyValuePair<string, int> keyValuePair;
			using (GlobalContext.CreateGlobalLockScope(this.names.Lock))
			{
				if (!this.metadata.TryGetValue(key, out keyValuePair))
				{
					isResourceRemoved = false;
					resource = null;
					return false;
				}
				if (keyValuePair.Value > 1)
				{
					isResourceRemoved = false;
					resource = null;
					this.metadata[key] = new KeyValuePair<string, int>(keyValuePair.Key, keyValuePair.Value - 1);
				}
				else
				{
					isResourceRemoved = true;
					resource = this.resources[key];
					if (this.resources.Remove(key))
					{
						this.metadata.Remove(key);
					}
					else
					{
						this.metadata[key] = new KeyValuePair<string, int>(keyValuePair.Key, 0);
					}
				}
			}
			if (isResourceRemoved && this.converter != null && !string.IsNullOrEmpty(keyValuePair.Key))
			{
				resource = this.converter.ConvertCachedItem(this.ManagerName, resource, keyValuePair.Key);
			}
			return true;
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x0003A0DC File Offset: 0x000382DC
		protected override IEnumerable<KeyValuePair<string, object>> ClearResources()
		{
			List<KeyValuePair<string, KeyValuePair<string, object>>> list = new List<KeyValuePair<string, KeyValuePair<string, object>>>(this.resources.Count);
			using (GlobalContext.CreateGlobalLockScope(this.names.Lock))
			{
				if (this.converter != null)
				{
					using (IEnumerator<KeyValuePair<string, object>> enumerator = this.resources.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<string, object> keyValuePair = enumerator.Current;
							KeyValuePair<string, int> keyValuePair2;
							string text = (this.metadata.TryGetValue(keyValuePair.Key, out keyValuePair2) ? keyValuePair2.Key : null);
							list.Add(new KeyValuePair<string, KeyValuePair<string, object>>(keyValuePair.Key, new KeyValuePair<string, object>(text, keyValuePair.Value)));
						}
						goto IL_00DE;
					}
				}
				foreach (KeyValuePair<string, object> keyValuePair3 in this.resources)
				{
					list.Add(new KeyValuePair<string, KeyValuePair<string, object>>(keyValuePair3.Key, new KeyValuePair<string, object>(null, keyValuePair3.Value)));
				}
				IL_00DE:
				this.resources.Clear();
				this.metadata.Clear();
			}
			if (this.converter != null)
			{
				return list.Select((KeyValuePair<string, KeyValuePair<string, object>> kvp) => new KeyValuePair<string, object>(kvp.Key, string.IsNullOrEmpty(kvp.Value.Key) ? kvp.Value.Value : this.converter.ConvertCachedItem(this.ManagerName, kvp.Value.Value, kvp.Value.Key)));
			}
			return list.Select((KeyValuePair<string, KeyValuePair<string, object>> kvp) => new KeyValuePair<string, object>(kvp.Key, kvp.Value.Value));
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x0003A254 File Offset: 0x00038454
		internal bool ReplaceSharedResource(string key, object sharedResource)
		{
			bool flag;
			using (GlobalContext.CreateGlobalLockScope(this.names.Lock))
			{
				KeyValuePair<string, int> keyValuePair;
				if (!this.metadata.TryGetValue(key, out keyValuePair) || keyValuePair.Value == 0)
				{
					flag = false;
				}
				else
				{
					this.resources[key] = sharedResource;
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x04000B2A RID: 2858
		private const string GlobalObjectNameTemplate_Resources = "MS_AS_SHARED_RESOURCE_MANAGER_{0}_RESOURCES";

		// Token: 0x04000B2B RID: 2859
		private const string GlobalObjectNameTemplate_Metadata = "MS_AS_SHARED_RESOURCE_MANAGER_{0}_METADATA";

		// Token: 0x04000B2C RID: 2860
		private const string GlobalObjectNameTemplate_Lock = "MS_AS_SHARED_RESOURCE_MANAGER_{0}_LOCK";

		// Token: 0x04000B2D RID: 2861
		private SharedResourceManager.GlobalNames names;

		// Token: 0x04000B2E RID: 2862
		private IDictionary<string, object> resources;

		// Token: 0x04000B2F RID: 2863
		private IDictionary<string, KeyValuePair<string, int>> metadata;

		// Token: 0x04000B30 RID: 2864
		private ISharedItemConverter converter;

		// Token: 0x02000204 RID: 516
		private struct GlobalNames
		{
			// Token: 0x060014B8 RID: 5304 RVA: 0x00046FD7 File Offset: 0x000451D7
			public GlobalNames(string managerName)
			{
				this.Resources = string.Format("MS_AS_SHARED_RESOURCE_MANAGER_{0}_RESOURCES", managerName);
				this.Metadata = string.Format("MS_AS_SHARED_RESOURCE_MANAGER_{0}_METADATA", managerName);
				this.Lock = string.Format("MS_AS_SHARED_RESOURCE_MANAGER_{0}_LOCK", managerName);
			}

			// Token: 0x04000F08 RID: 3848
			public readonly string Resources;

			// Token: 0x04000F09 RID: 3849
			public readonly string Metadata;

			// Token: 0x04000F0A RID: 3850
			public readonly string Lock;
		}
	}
}
