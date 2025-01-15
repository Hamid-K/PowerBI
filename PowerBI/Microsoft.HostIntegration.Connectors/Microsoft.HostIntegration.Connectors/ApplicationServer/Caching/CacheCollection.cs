using System;
using System.Configuration;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000113 RID: 275
	internal class CacheCollection : ConfigurationElementCollection
	{
		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x0001C6B9 File Offset: 0x0001A8B9
		public static string Name
		{
			get
			{
				return "caches";
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x00002B16 File Offset: 0x00000D16
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.AddRemoveClearMap;
			}
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0001C6C0 File Offset: 0x0001A8C0
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((INamedCacheConfiguration)element).Name;
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0001C6CD File Offset: 0x0001A8CD
		protected override ConfigurationElement CreateNewElement()
		{
			return new NamedCacheConfiguration();
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0001C6D4 File Offset: 0x0001A8D4
		internal INamedCacheConfiguration Get(string key)
		{
			return (INamedCacheConfiguration)base.BaseGet(key);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00016D88 File Offset: 0x00014F88
		internal bool Delete(string name)
		{
			if (base.BaseGet(name) != null)
			{
				base.BaseRemove(name);
				return base.BaseGet(name) == null;
			}
			return false;
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0001C6E4 File Offset: 0x0001A8E4
		internal bool Add(INamedCacheConfiguration nc)
		{
			NamedCacheConfiguration namedCacheConfiguration = nc as NamedCacheConfiguration;
			if (base.BaseGet(this.GetElementKey(namedCacheConfiguration)) == null)
			{
				base.BaseAdd(namedCacheConfiguration, false);
				return true;
			}
			return false;
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x0001C712 File Offset: 0x0001A912
		// (set) Token: 0x06000765 RID: 1893 RVA: 0x0001C724 File Offset: 0x0001A924
		[ConfigurationProperty("maxCount", DefaultValue = 128, IsRequired = false)]
		[IntegerValidator(MinValue = 1)]
		public int MaxCount
		{
			get
			{
				return (int)base["maxCount"];
			}
			set
			{
				base["maxCount"] = value;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x0001C737 File Offset: 0x0001A937
		// (set) Token: 0x06000767 RID: 1895 RVA: 0x0001C749 File Offset: 0x0001A949
		[IntegerValidator(MinValue = 1)]
		[ConfigurationProperty("partitionCount", DefaultValue = 2147483647, IsRequired = false)]
		public int BasePartitionCount
		{
			get
			{
				return (int)base["partitionCount"];
			}
			set
			{
				base["partitionCount"] = value;
			}
		}

		// Token: 0x0400064C RID: 1612
		internal const string CACHE_COLLECTION_PROPERTIES = "cacheAttributes";

		// Token: 0x0400064D RID: 1613
		internal const string MAX_COUNT = "maxCount";

		// Token: 0x0400064E RID: 1614
		internal const int MAX_COUNT_LOW_BOUND = 1;
	}
}
