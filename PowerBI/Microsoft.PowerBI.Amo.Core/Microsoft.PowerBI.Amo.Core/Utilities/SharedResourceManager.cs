using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.AnalysisServices.Utilities
{
	// Token: 0x02000148 RID: 328
	internal sealed class SharedResourceManager : ResourceManagerBase
	{
		// Token: 0x0600114F RID: 4431 RVA: 0x0003C5EB File Offset: 0x0003A7EB
		private SharedResourceManager(string managerName, SharedResourceManager.GlobalNames names, IDictionary<string, object> resources, IDictionary<string, KeyValuePair<string, int>> metadata, SharedMemoryCache cache, ISharedItemConverter converter)
			: base(cache)
		{
			this.ManagerName = managerName;
			this.names = names;
			this.resources = resources;
			this.metadata = metadata;
			this.converter = converter;
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001150 RID: 4432 RVA: 0x0003C61A File Offset: 0x0003A81A
		public string ManagerName { get; }

		// Token: 0x06001151 RID: 4433 RVA: 0x0003C624 File Offset: 0x0003A824
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

		// Token: 0x06001152 RID: 4434 RVA: 0x0003C6C4 File Offset: 0x0003A8C4
		public static SharedResourceManager Create(string managerName, IEqualityComparer<string> comparer, SharedMemoryCache cache, PrepareItemForCaching prepareMethod, ConvertCachedItem convertMethod)
		{
			return SharedResourceManager.Create(managerName, comparer, cache, new SharedItemConverter(prepareMethod, convertMethod));
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x0003C6D6 File Offset: 0x0003A8D6
		protected override int GetItemCount()
		{
			return this.resources.Count;
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x0003C6E4 File Offset: 0x0003A8E4
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

		// Token: 0x06001155 RID: 4437 RVA: 0x0003C7CC File Offset: 0x0003A9CC
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

		// Token: 0x06001156 RID: 4438 RVA: 0x0003C8D4 File Offset: 0x0003AAD4
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

		// Token: 0x06001157 RID: 4439 RVA: 0x0003C9E0 File Offset: 0x0003ABE0
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

		// Token: 0x06001158 RID: 4440 RVA: 0x0003CB58 File Offset: 0x0003AD58
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

		// Token: 0x04000AE3 RID: 2787
		private const string GlobalObjectNameTemplate_Resources = "MS_AS_SHARED_RESOURCE_MANAGER_{0}_RESOURCES";

		// Token: 0x04000AE4 RID: 2788
		private const string GlobalObjectNameTemplate_Metadata = "MS_AS_SHARED_RESOURCE_MANAGER_{0}_METADATA";

		// Token: 0x04000AE5 RID: 2789
		private const string GlobalObjectNameTemplate_Lock = "MS_AS_SHARED_RESOURCE_MANAGER_{0}_LOCK";

		// Token: 0x04000AE6 RID: 2790
		private SharedResourceManager.GlobalNames names;

		// Token: 0x04000AE7 RID: 2791
		private IDictionary<string, object> resources;

		// Token: 0x04000AE8 RID: 2792
		private IDictionary<string, KeyValuePair<string, int>> metadata;

		// Token: 0x04000AE9 RID: 2793
		private ISharedItemConverter converter;

		// Token: 0x020001E1 RID: 481
		private struct GlobalNames
		{
			// Token: 0x06001413 RID: 5139 RVA: 0x000451F7 File Offset: 0x000433F7
			public GlobalNames(string managerName)
			{
				this.Resources = string.Format("MS_AS_SHARED_RESOURCE_MANAGER_{0}_RESOURCES", managerName);
				this.Metadata = string.Format("MS_AS_SHARED_RESOURCE_MANAGER_{0}_METADATA", managerName);
				this.Lock = string.Format("MS_AS_SHARED_RESOURCE_MANAGER_{0}_LOCK", managerName);
			}

			// Token: 0x040011BE RID: 4542
			public readonly string Resources;

			// Token: 0x040011BF RID: 4543
			public readonly string Metadata;

			// Token: 0x040011C0 RID: 4544
			public readonly string Lock;
		}
	}
}
