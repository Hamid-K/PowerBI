using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000140 RID: 320
	[Serializable]
	internal sealed class QuotaConfig : ConfigurationElement, ISerializable
	{
		// Token: 0x06000990 RID: 2448 RVA: 0x0002011F File Offset: 0x0001E31F
		public QuotaConfig()
		{
			this.InitializeDefault();
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x00019F87 File Offset: 0x00018187
		// (set) Token: 0x06000992 RID: 2450 RVA: 0x00019F99 File Offset: 0x00018199
		[ConfigurationProperty("enabled", DefaultValue = false, IsRequired = false)]
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

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x000206A2 File Offset: 0x0001E8A2
		// (set) Token: 0x06000994 RID: 2452 RVA: 0x000206B4 File Offset: 0x0001E8B4
		[ConfigurationProperty("sizeQuota", IsRequired = false)]
		public CacheSizeQuotaConfig SizeQuota
		{
			get
			{
				return (CacheSizeQuotaConfig)base["sizeQuota"];
			}
			set
			{
				base["sizeQuota"] = value;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x000206C2 File Offset: 0x0001E8C2
		// (set) Token: 0x06000996 RID: 2454 RVA: 0x000206D4 File Offset: 0x0001E8D4
		[ConfigurationProperty("connectionQuota", IsRequired = false)]
		public ConnectionQuotaConfig ConnectionQuota
		{
			get
			{
				return (ConnectionQuotaConfig)base["connectionQuota"];
			}
			set
			{
				base["connectionQuota"] = value;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x000206E2 File Offset: 0x0001E8E2
		// (set) Token: 0x06000998 RID: 2456 RVA: 0x000206F4 File Offset: 0x0001E8F4
		[ConfigurationProperty("dataTransferQuota", IsRequired = false)]
		public DataTransferQuotaConfig DataTransferQuota
		{
			get
			{
				return (DataTransferQuotaConfig)base["dataTransferQuota"];
			}
			set
			{
				base["dataTransferQuota"] = value;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x00020702 File Offset: 0x0001E902
		// (set) Token: 0x0600099A RID: 2458 RVA: 0x00020714 File Offset: 0x0001E914
		[ConfigurationProperty("transactionQuota", IsRequired = false)]
		public TransactionQuotaConfig TransactionQuota
		{
			get
			{
				return (TransactionQuotaConfig)base["transactionQuota"];
			}
			set
			{
				base["transactionQuota"] = value;
			}
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00020724 File Offset: 0x0001E924
		public QuotaConfig(SerializationInfo info, StreamingContext context)
		{
			Version version = null;
			if (context.Context != null)
			{
				SerializationContext serializationContext = context.Context as SerializationContext;
				version = serializationContext.StoreVersion;
			}
			this.Enabled = info.GetBoolean("enabled");
			this.SizeQuota = (CacheSizeQuotaConfig)info.GetValue("sizeQuota", typeof(CacheSizeQuotaConfig));
			if (ConfigManager.IsStoreVersionHigherThan2000(version))
			{
				this.ConnectionQuota = (ConnectionQuotaConfig)info.GetValue("connectionQuota", typeof(ConnectionQuotaConfig));
				this.TransactionQuota = (TransactionQuotaConfig)info.GetValue("transactionQuota", typeof(TransactionQuotaConfig));
				this.DataTransferQuota = (DataTransferQuotaConfig)info.GetValue("dataTransferQuota", typeof(DataTransferQuotaConfig));
				return;
			}
			this.ConnectionQuota = new ConnectionQuotaConfig();
			this.TransactionQuota = new TransactionQuotaConfig();
			this.DataTransferQuota = new DataTransferQuotaConfig();
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00020814 File Offset: 0x0001EA14
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("enabled", base["enabled"]);
			info.AddValue("sizeQuota", base["sizeQuota"]);
			info.AddValue("connectionQuota", base["connectionQuota"]);
			info.AddValue("transactionQuota", base["transactionQuota"]);
			info.AddValue("dataTransferQuota", base["dataTransferQuota"]);
		}

		// Token: 0x040006DE RID: 1758
		internal const string ENABLED = "enabled";

		// Token: 0x040006DF RID: 1759
		internal const string SIZE_QUOTA = "sizeQuota";

		// Token: 0x040006E0 RID: 1760
		internal const string CONNECTION_QUOTA = "connectionQuota";

		// Token: 0x040006E1 RID: 1761
		internal const string TRANSACTION_QUOTA = "transactionQuota";

		// Token: 0x040006E2 RID: 1762
		internal const string DATATRANSFER_QUOTA = "dataTransferQuota";
	}
}
