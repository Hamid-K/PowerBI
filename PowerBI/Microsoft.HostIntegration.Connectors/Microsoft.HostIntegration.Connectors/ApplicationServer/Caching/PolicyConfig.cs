using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000135 RID: 309
	[Serializable]
	internal class PolicyConfig : ConfigurationElement, ISerializable
	{
		// Token: 0x06000935 RID: 2357 RVA: 0x00015607 File Offset: 0x00013807
		public PolicyConfig()
		{
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x0001FC78 File Offset: 0x0001DE78
		// (set) Token: 0x06000937 RID: 2359 RVA: 0x0001FC8A File Offset: 0x0001DE8A
		[ConfigurationProperty("eviction", IsRequired = true)]
		public EvictionConfig Eviction
		{
			get
			{
				return (EvictionConfig)base["eviction"];
			}
			set
			{
				base["eviction"] = value;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x0001FC98 File Offset: 0x0001DE98
		// (set) Token: 0x06000939 RID: 2361 RVA: 0x0001FCAA File Offset: 0x0001DEAA
		[ConfigurationProperty("expiration", IsRequired = true)]
		public ExpirationConfig Expiration
		{
			get
			{
				return (ExpirationConfig)base["expiration"];
			}
			set
			{
				base["expiration"] = value;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x0001FCB8 File Offset: 0x0001DEB8
		// (set) Token: 0x0600093B RID: 2363 RVA: 0x0001FCCA File Offset: 0x0001DECA
		[ConfigurationProperty("serverNotification", IsRequired = false)]
		public ServerNotificationProperties Notification
		{
			get
			{
				return (ServerNotificationProperties)base["serverNotification"];
			}
			set
			{
				base["serverNotification"] = value;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x0001FCD8 File Offset: 0x0001DED8
		// (set) Token: 0x0600093D RID: 2365 RVA: 0x0001FCEA File Offset: 0x0001DEEA
		[ConfigurationProperty("backingStore", IsRequired = false)]
		public BackingStoreConfig BackingStore
		{
			get
			{
				return (BackingStoreConfig)base["backingStore"];
			}
			set
			{
				base["backingStore"] = value;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x0001FCF8 File Offset: 0x0001DEF8
		// (set) Token: 0x0600093F RID: 2367 RVA: 0x0001FD0A File Offset: 0x0001DF0A
		[ConfigurationProperty("quota", IsRequired = false)]
		public QuotaConfig Quota
		{
			get
			{
				return (QuotaConfig)base["quota"];
			}
			set
			{
				base["quota"] = value;
			}
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0001FD18 File Offset: 0x0001DF18
		protected PolicyConfig(SerializationInfo info, StreamingContext context)
		{
			bool flag = false;
			if (context.Context != null)
			{
				SerializationContext serializationContext = context.Context as SerializationContext;
				ClientVersionInfo clientVersionInfo = serializationContext.ClientVersionInfo;
				if (clientVersionInfo == null || !clientVersionInfo.IsInvalid)
				{
					flag = true;
				}
			}
			this.Eviction = (EvictionConfig)info.GetValue("eviction", typeof(EvictionConfig));
			this.Expiration = (ExpirationConfig)info.GetValue("expiration", typeof(ExpirationConfig));
			this.Notification = (ServerNotificationProperties)info.GetValue("serverNotification", typeof(ServerNotificationProperties));
			if (!flag)
			{
				try
				{
					this.BackingStore = (BackingStoreConfig)info.GetValue("backingStore", typeof(BackingStoreConfig));
				}
				catch (SerializationException)
				{
					this.BackingStore = new BackingStoreConfig();
					this.BackingStore.WriteBehind = new WriteBehindConfig();
					this.BackingStore.ReadThrough = new ReadThroughConfig();
					this.BackingStore.SerializationConfig = new SerializationConfig();
				}
				try
				{
					this.Quota = (QuotaConfig)info.GetValue("quota", typeof(QuotaConfig));
				}
				catch (SerializationException)
				{
					this.Quota = new QuotaConfig();
					this.Quota.SizeQuota = new CacheSizeQuotaConfig();
				}
			}
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0001FE7C File Offset: 0x0001E07C
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			bool flag = false;
			if (context.Context != null)
			{
				SerializationContext serializationContext = context.Context as SerializationContext;
				ClientVersionInfo clientVersionInfo = serializationContext.ClientVersionInfo;
				if (clientVersionInfo == null || !clientVersionInfo.IsInvalid)
				{
					flag = true;
				}
			}
			info.AddValue("expiration", this.Expiration);
			info.AddValue("eviction", this.Eviction);
			info.AddValue("serverNotification", this.Notification);
			if (!this.simulateOldConfigTestHook && !flag)
			{
				info.AddValue("backingStore", this.BackingStore);
				info.AddValue("quota", this.Quota);
			}
		}

		// Token: 0x040006BD RID: 1725
		internal const string EXPIRATION = "expiration";

		// Token: 0x040006BE RID: 1726
		internal const string EVICTION = "eviction";

		// Token: 0x040006BF RID: 1727
		internal const string SERVER_NOTIFICATION = "serverNotification";

		// Token: 0x040006C0 RID: 1728
		internal const string BACKING_STORE = "backingStore";

		// Token: 0x040006C1 RID: 1729
		internal const string QUOTA = "quota";

		// Token: 0x040006C2 RID: 1730
		internal bool simulateOldConfigTestHook;
	}
}
