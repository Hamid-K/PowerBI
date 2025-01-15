using System;
using System.Configuration;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000134 RID: 308
	[Serializable]
	internal class NamedCacheConfiguration : ConfigurationElement, INamedCacheConfiguration, ISerializable
	{
		// Token: 0x06000902 RID: 2306 RVA: 0x0001F31A File Offset: 0x0001D51A
		public NamedCacheConfiguration()
		{
			this.Consistency = ConsistencyType.StrongConsistency;
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x00017DAF File Offset: 0x00015FAF
		// (set) Token: 0x06000904 RID: 2308 RVA: 0x00017DC1 File Offset: 0x00015FC1
		[ConfigurationProperty("name", IsKey = true, IsRequired = true)]
		[StringValidator(MaxLength = 255)]
		public string Name
		{
			get
			{
				return (string)base["name"];
			}
			set
			{
				base["name"] = value;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x0001F330 File Offset: 0x0001D530
		// (set) Token: 0x06000906 RID: 2310 RVA: 0x0001F342 File Offset: 0x0001D542
		[ConfigurationProperty("type", IsRequired = false, DefaultValue = NamedCacheDeploymentType.Partitioned)]
		public NamedCacheDeploymentType Type
		{
			get
			{
				return (NamedCacheDeploymentType)base["type"];
			}
			set
			{
				base["type"] = value;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x0001F355 File Offset: 0x0001D555
		// (set) Token: 0x06000908 RID: 2312 RVA: 0x0001F367 File Offset: 0x0001D567
		[ConfigurationProperty("consistency", IsRequired = false)]
		public ConsistencyType Consistency
		{
			get
			{
				return (ConsistencyType)base["consistency"];
			}
			set
			{
				base["consistency"] = value;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x0001F37A File Offset: 0x0001D57A
		// (set) Token: 0x0600090A RID: 2314 RVA: 0x0001F38C File Offset: 0x0001D58C
		[ConfigurationProperty("policy", IsRequired = true)]
		internal PolicyConfig Policy
		{
			get
			{
				return (PolicyConfig)base["policy"];
			}
			set
			{
				base["policy"] = value;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x0001F39A File Offset: 0x0001D59A
		// (set) Token: 0x0600090C RID: 2316 RVA: 0x0001F3AC File Offset: 0x0001D5AC
		[ConfigurationProperty("secondaries", DefaultValue = 0, IsRequired = false)]
		[IntegerValidator(MinValue = 0)]
		public int Secondaries
		{
			get
			{
				return (int)base["secondaries"];
			}
			set
			{
				base["secondaries"] = value;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x00019F87 File Offset: 0x00018187
		// (set) Token: 0x0600090E RID: 2318 RVA: 0x00019F99 File Offset: 0x00018199
		[ConfigurationProperty("enabled", DefaultValue = true, IsRequired = false)]
		public bool Enabled
		{
			get
			{
				return (bool)base["enabled"];
			}
			set
			{
				base["enabled"] = value;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x0001F3BF File Offset: 0x0001D5BF
		// (set) Token: 0x06000910 RID: 2320 RVA: 0x0001F3D1 File Offset: 0x0001D5D1
		[ConfigurationProperty("redirectUri", IsRequired = false)]
		public Uri RedirectUri
		{
			get
			{
				return (Uri)base["redirectUri"];
			}
			set
			{
				base["redirectUri"] = value;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x0001F3DF File Offset: 0x0001D5DF
		// (set) Token: 0x06000912 RID: 2322 RVA: 0x00019E21 File Offset: 0x00018021
		[ConfigurationProperty("deploymentMode", IsRequired = false)]
		public DeploymentModeElement DeploymentMode
		{
			get
			{
				return (DeploymentModeElement)base["deploymentMode"];
			}
			set
			{
				base["deploymentMode"] = value;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x0001F2EE File Offset: 0x0001D4EE
		// (set) Token: 0x06000914 RID: 2324 RVA: 0x0001F300 File Offset: 0x0001D500
		[ConfigurationProperty("memcachePort", DefaultValue = 0, IsRequired = false)]
		public int MemcacheSocketPort
		{
			get
			{
				return (int)base["memcachePort"];
			}
			set
			{
				base["memcachePort"] = value;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x0001F3F1 File Offset: 0x0001D5F1
		// (set) Token: 0x06000916 RID: 2326 RVA: 0x0001F3FE File Offset: 0x0001D5FE
		public int MinWriteQuorum
		{
			get
			{
				return NamedCacheConfiguration.MinSecondariesToMinWriteQuorum(this.MinSecondaries);
			}
			set
			{
				this.MinSecondaries = NamedCacheConfiguration.MinWriteQuorumToMinSecondaries(value);
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x0001F40C File Offset: 0x0001D60C
		// (set) Token: 0x06000918 RID: 2328 RVA: 0x0001F43A File Offset: 0x0001D63A
		[ConfigurationProperty("minSecondaries", DefaultValue = 2147483647, IsRequired = false)]
		[IntegerValidator(MinValue = 0)]
		public int MinSecondaries
		{
			get
			{
				int num = (int)base["minSecondaries"];
				if (num == 2147483647)
				{
					return this.Secondaries;
				}
				return num;
			}
			set
			{
				base["minSecondaries"] = value;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x0001F450 File Offset: 0x0001D650
		internal bool IsMinSecondariesImplicit
		{
			get
			{
				return int.MaxValue.Equals(base["minSecondaries"]);
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x0001F475 File Offset: 0x0001D675
		public int Replicas
		{
			get
			{
				return this.Secondaries + 1;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x0001F47F File Offset: 0x0001D67F
		// (set) Token: 0x0600091C RID: 2332 RVA: 0x0001F4A6 File Offset: 0x0001D6A6
		[IntegerValidator(MinValue = 1)]
		[ConfigurationProperty("systemRegionCount", DefaultValue = 1024, IsRequired = false)]
		public int SystemRegionCount
		{
			get
			{
				if (this.cachedRegionCount == -1)
				{
					this.cachedRegionCount = (int)base["systemRegionCount"];
				}
				return this.cachedRegionCount;
			}
			set
			{
				base["systemRegionCount"] = value;
				this.cachedRegionCount = value;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x0001C737 File Offset: 0x0001A937
		// (set) Token: 0x0600091E RID: 2334 RVA: 0x0001C749 File Offset: 0x0001A949
		[IntegerValidator(MinValue = 1)]
		[ConfigurationProperty("partitionCount", DefaultValue = 2147483647, IsRequired = false)]
		public int PartitionCount
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

		// Token: 0x0600091F RID: 2335 RVA: 0x0001F4C0 File Offset: 0x0001D6C0
		public int GetPartitionCount(int defaultValue)
		{
			int num = this.PartitionCount;
			if (num == 2147483647)
			{
				num = defaultValue;
			}
			return num;
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x0001F4DF File Offset: 0x0001D6DF
		// (set) Token: 0x06000921 RID: 2337 RVA: 0x0001F4E7 File Offset: 0x0001D6E7
		public long Version
		{
			get
			{
				return this._version;
			}
			set
			{
				this._version = value;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x0001F4F0 File Offset: 0x0001D6F0
		// (set) Token: 0x06000923 RID: 2339 RVA: 0x0001F502 File Offset: 0x0001D702
		public long DefaultTTL
		{
			get
			{
				return this.Policy.Expiration.DefaultTTL;
			}
			set
			{
				this.Policy.Expiration.DefaultTTL = value;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x0001F515 File Offset: 0x0001D715
		// (set) Token: 0x06000925 RID: 2341 RVA: 0x0001F527 File Offset: 0x0001D727
		public ExpirationType ExpirationType
		{
			get
			{
				return this.Policy.Expiration.Type;
			}
			set
			{
				this.Policy.Expiration.Type = value;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x0001F53A File Offset: 0x0001D73A
		// (set) Token: 0x06000927 RID: 2343 RVA: 0x0001F54C File Offset: 0x0001D74C
		public EvictionType EvictionType
		{
			get
			{
				return this.Policy.Eviction.Type;
			}
			set
			{
				this.Policy.Eviction.Type = value;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x0001F55F File Offset: 0x0001D75F
		// (set) Token: 0x06000929 RID: 2345 RVA: 0x0001F571 File Offset: 0x0001D771
		public bool IsExpirable
		{
			get
			{
				return this.Policy.Expiration.IsExpirable;
			}
			set
			{
				this.Policy.Expiration.IsExpirable = value;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x0001F584 File Offset: 0x0001D784
		// (set) Token: 0x0600092B RID: 2347 RVA: 0x0001F591 File Offset: 0x0001D791
		public ServerNotificationProperties Notification
		{
			get
			{
				return this.Policy.Notification;
			}
			set
			{
				this.Policy.Notification = value;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x0001F59F File Offset: 0x0001D79F
		// (set) Token: 0x0600092D RID: 2349 RVA: 0x0001F5AC File Offset: 0x0001D7AC
		public BackingStoreConfig BackingStore
		{
			get
			{
				return this.Policy.BackingStore;
			}
			set
			{
				this.Policy.BackingStore = value;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x0001F5BA File Offset: 0x0001D7BA
		// (set) Token: 0x0600092F RID: 2351 RVA: 0x0001F5C7 File Offset: 0x0001D7C7
		public QuotaConfig Quota
		{
			get
			{
				return this.Policy.Quota;
			}
			set
			{
				this.Policy.Quota = value;
			}
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0001F5D8 File Offset: 0x0001D7D8
		protected NamedCacheConfiguration(SerializationInfo info, StreamingContext context)
		{
			Version version = null;
			if (context.Context != null)
			{
				SerializationContext serializationContext = context.Context as SerializationContext;
				version = serializationContext.StoreVersion;
			}
			this.Name = info.GetString("name");
			base["type"] = (NamedCacheDeploymentType)info.GetValue("type", typeof(NamedCacheDeploymentType));
			base["consistency"] = (ConsistencyType)info.GetValue("consistency", typeof(ConsistencyType));
			this.Policy = (PolicyConfig)info.GetValue("policy", typeof(PolicyConfig));
			this.PartitionCount = (int)info.GetValue("partitionCount", typeof(int));
			this.Secondaries = (int)info.GetValue("secondaries", typeof(int));
			try
			{
				this.MinSecondaries = (int)info.GetValue("minSecondaries", typeof(int));
			}
			catch (SerializationException)
			{
				this.MinSecondaries = int.MaxValue;
			}
			try
			{
				this.MemcacheSocketPort = (int)info.GetValue("memcachePort", typeof(int));
			}
			catch (SerializationException)
			{
				this.MemcacheSocketPort = 0;
			}
			if (ConfigManager.IsStoreVersionHigherThan2000(version))
			{
				if (ConfigManager.IsStoreVersionHigherThan3000(version))
				{
					this.RedirectUri = (Uri)info.GetValue("redirectUri", typeof(Uri));
				}
				else
				{
					this.RedirectUri = null;
				}
				this.Enabled = (bool)info.GetValue("enabled", typeof(bool));
				this.SystemRegionCount = (int)info.GetValue("systemRegionCount", typeof(int));
				this.DeploymentMode = (DeploymentModeElement)info.GetValue("deploymentMode", typeof(DeploymentModeElement));
			}
			else
			{
				this.Enabled = true;
				this.RedirectUri = null;
				this.SystemRegionCount = 1024;
			}
			if (context.Context != null)
			{
				SerializationContext serializationContext2 = context.Context as SerializationContext;
				if (version != null && serializationContext2.StoreVersion.Equals(ConfigManager.StoreVersion1000))
				{
					this.PartitionCount = int.MaxValue;
				}
			}
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0001F844 File Offset: 0x0001DA44
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("name", this.Name);
			info.AddValue("type", base["type"], typeof(NamedCacheDeploymentType));
			info.AddValue("consistency", base["consistency"], typeof(ConsistencyType));
			info.AddValue("policy", this.Policy);
			info.AddValue("partitionCount", this.PartitionCount);
			info.AddValue("secondaries", this.Secondaries);
			info.AddValue("minSecondaries", this.MinSecondaries);
			bool flag = false;
			if (context.Context != null)
			{
				SerializationContext serializationContext = context.Context as SerializationContext;
				ClientVersionInfo clientVersionInfo = serializationContext.ClientVersionInfo;
				if (clientVersionInfo == null || (!clientVersionInfo.IsInvalid && clientVersionInfo.CodeVersion < ConfigManager.CodeVersion3_GettingClientVersion))
				{
					flag = true;
				}
			}
			if (!flag)
			{
				info.AddValue("enabled", this.Enabled);
				info.AddValue("redirectUri", this.RedirectUri);
				info.AddValue("systemRegionCount", this.SystemRegionCount);
				info.AddValue("deploymentMode", this.DeploymentMode);
				info.AddValue("memcachePort", this.MemcacheSocketPort);
			}
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0001F977 File Offset: 0x0001DB77
		internal static int MinSecondariesToMinWriteQuorum(int minSecondaries)
		{
			return minSecondaries + 1;
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0001F97C File Offset: 0x0001DB7C
		internal static int MinWriteQuorumToMinSecondaries(int minWriteQuorum)
		{
			return minWriteQuorum - 1;
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0001F984 File Offset: 0x0001DB84
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "Cache Name : {0}", new object[] { this.Name });
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " Version : {0}", new object[] { this.Version });
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " Secondaries : {0}", new object[] { this.Secondaries });
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " MinSecondaries : {0}", new object[] { this.MinSecondaries });
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " SystemRegionCount : {0}", new object[] { this.SystemRegionCount });
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " EvictionType : {0}", new object[] { this.EvictionType });
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " ExpirationType : {0}", new object[] { this.ExpirationType });
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " IsExpirable : {0}", new object[] { this.IsExpirable });
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " DefaultTTL : {0}", new object[] { this.DefaultTTL });
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " Enabled : {0}", new object[] { this.Enabled });
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " MemcacheSocketPort : {0}", new object[] { this.MemcacheSocketPort });
			if (this.Quota != null && this.Quota.SizeQuota != null && this.Quota.ConnectionQuota != null && this.Quota.DataTransferQuota != null && this.Quota.TransactionQuota != null)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " CacheSizeInMB : {0}", new object[] { this.Quota.SizeQuota.SizeInMB });
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " ConnectionQuota : {0}", new object[] { this.Quota.ConnectionQuota.ConnectionCount });
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " DataTransferQuota : {0}", new object[] { this.Quota.DataTransferQuota.DataTransferCount });
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " TransactionQuota : {0}", new object[] { this.Quota.TransactionQuota.TransactionCount });
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040006AC RID: 1708
		internal const string NAME = "name";

		// Token: 0x040006AD RID: 1709
		internal const string TYPE = "type";

		// Token: 0x040006AE RID: 1710
		internal const string CONSISTENCY = "consistency";

		// Token: 0x040006AF RID: 1711
		internal const string POLICY = "policy";

		// Token: 0x040006B0 RID: 1712
		internal const string PARTITION_COUNT = "partitionCount";

		// Token: 0x040006B1 RID: 1713
		internal const string SYSTEM_REGION_COUNT = "systemRegionCount";

		// Token: 0x040006B2 RID: 1714
		internal const string SECONDARIES = "secondaries";

		// Token: 0x040006B3 RID: 1715
		internal const string MINSECONDARIES = "minSecondaries";

		// Token: 0x040006B4 RID: 1716
		internal const string ENABLED = "enabled";

		// Token: 0x040006B5 RID: 1717
		internal const string REDIRECT_URI = "redirectUri";

		// Token: 0x040006B6 RID: 1718
		internal const int DefaultMinSecondaries = 2147483647;

		// Token: 0x040006B7 RID: 1719
		internal const int MIN_PARTITION_COUNT = 1;

		// Token: 0x040006B8 RID: 1720
		internal const int INVALID_PARTITION_COUNT = 2147483647;

		// Token: 0x040006B9 RID: 1721
		internal const int MIN_SYSTEM_REGION_COUNT = 1;

		// Token: 0x040006BA RID: 1722
		internal const int INVALID_SYSTEM_REGION_COUNT = 2147483647;

		// Token: 0x040006BB RID: 1723
		private long _version;

		// Token: 0x040006BC RID: 1724
		private int cachedRegionCount = -1;
	}
}
