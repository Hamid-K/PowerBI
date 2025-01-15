using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.AnalysisServices.AdomdClient.Utilities
{
	// Token: 0x02000153 RID: 339
	internal sealed class SharedResourceManager : ResourceManagerBase
	{
		// Token: 0x060010B4 RID: 4276 RVA: 0x000399B7 File Offset: 0x00037BB7
		private SharedResourceManager(string managerName, SharedResourceManager.GlobalNames names, IDictionary<string, object> resources, IDictionary<string, KeyValuePair<string, int>> metadata, SharedMemoryCache cache, ISharedItemConverter converter)
			: base(cache)
		{
			this.ManagerName = managerName;
			this.names = names;
			this.resources = resources;
			this.metadata = metadata;
			this.converter = converter;
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x060010B5 RID: 4277 RVA: 0x000399E6 File Offset: 0x00037BE6
		public string ManagerName { get; }

		// Token: 0x060010B6 RID: 4278 RVA: 0x000399F0 File Offset: 0x00037BF0
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

		// Token: 0x060010B7 RID: 4279 RVA: 0x00039A90 File Offset: 0x00037C90
		public static SharedResourceManager Create(string managerName, IEqualityComparer<string> comparer, SharedMemoryCache cache, PrepareItemForCaching prepareMethod, ConvertCachedItem convertMethod)
		{
			return SharedResourceManager.Create(managerName, comparer, cache, new SharedItemConverter(prepareMethod, convertMethod));
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x00039AA2 File Offset: 0x00037CA2
		protected override int GetItemCount()
		{
			return this.resources.Count;
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x00039AB0 File Offset: 0x00037CB0
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

		// Token: 0x060010BA RID: 4282 RVA: 0x00039B98 File Offset: 0x00037D98
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

		// Token: 0x060010BB RID: 4283 RVA: 0x00039CA0 File Offset: 0x00037EA0
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

		// Token: 0x060010BC RID: 4284 RVA: 0x00039DAC File Offset: 0x00037FAC
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

		// Token: 0x060010BD RID: 4285 RVA: 0x00039F24 File Offset: 0x00038124
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

		// Token: 0x04000B1D RID: 2845
		private const string GlobalObjectNameTemplate_Resources = "MS_AS_SHARED_RESOURCE_MANAGER_{0}_RESOURCES";

		// Token: 0x04000B1E RID: 2846
		private const string GlobalObjectNameTemplate_Metadata = "MS_AS_SHARED_RESOURCE_MANAGER_{0}_METADATA";

		// Token: 0x04000B1F RID: 2847
		private const string GlobalObjectNameTemplate_Lock = "MS_AS_SHARED_RESOURCE_MANAGER_{0}_LOCK";

		// Token: 0x04000B20 RID: 2848
		private SharedResourceManager.GlobalNames names;

		// Token: 0x04000B21 RID: 2849
		private IDictionary<string, object> resources;

		// Token: 0x04000B22 RID: 2850
		private IDictionary<string, KeyValuePair<string, int>> metadata;

		// Token: 0x04000B23 RID: 2851
		private ISharedItemConverter converter;

		// Token: 0x02000204 RID: 516
		private struct GlobalNames
		{
			// Token: 0x060014AB RID: 5291 RVA: 0x00046A9B File Offset: 0x00044C9B
			public GlobalNames(string managerName)
			{
				this.Resources = string.Format("MS_AS_SHARED_RESOURCE_MANAGER_{0}_RESOURCES", managerName);
				this.Metadata = string.Format("MS_AS_SHARED_RESOURCE_MANAGER_{0}_METADATA", managerName);
				this.Lock = string.Format("MS_AS_SHARED_RESOURCE_MANAGER_{0}_LOCK", managerName);
			}

			// Token: 0x04000EF2 RID: 3826
			public readonly string Resources;

			// Token: 0x04000EF3 RID: 3827
			public readonly string Metadata;

			// Token: 0x04000EF4 RID: 3828
			public readonly string Lock;
		}
	}
}
