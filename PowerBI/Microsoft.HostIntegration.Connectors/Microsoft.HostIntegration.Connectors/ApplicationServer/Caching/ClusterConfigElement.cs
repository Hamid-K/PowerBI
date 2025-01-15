using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200012B RID: 299
	[Serializable]
	internal class ClusterConfigElement : ConfigurationElement, ISerializable
	{
		// Token: 0x06000894 RID: 2196 RVA: 0x00015607 File Offset: 0x00013807
		private ClusterConfigElement()
		{
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0001EA51 File Offset: 0x0001CC51
		internal ClusterConfigElement(string provider, string connectionStr)
		{
			this.Provider = provider;
			this.ConnectionString = connectionStr;
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0001EA67 File Offset: 0x0001CC67
		internal ClusterConfigElement(string provider, string connectionStr, string providerType)
			: this(provider, connectionStr)
		{
			this.CloudProvider = providerType;
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x0001EA78 File Offset: 0x0001CC78
		// (set) Token: 0x06000898 RID: 2200 RVA: 0x0001EA8A File Offset: 0x0001CC8A
		[ConfigurationProperty("provider", DefaultValue = "xml", IsRequired = false)]
		internal string Provider
		{
			get
			{
				return (string)base["provider"];
			}
			set
			{
				base["provider"] = value;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x0001EA98 File Offset: 0x0001CC98
		// (set) Token: 0x0600089A RID: 2202 RVA: 0x0001EAAA File Offset: 0x0001CCAA
		[ConfigurationProperty("cloudProviderType", DefaultValue = "", IsRequired = false)]
		internal string CloudProvider
		{
			get
			{
				return (string)base["cloudProviderType"];
			}
			set
			{
				base["cloudProviderType"] = value;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x0001EAB8 File Offset: 0x0001CCB8
		// (set) Token: 0x0600089C RID: 2204 RVA: 0x0001EACA File Offset: 0x0001CCCA
		[ConfigurationProperty("connectionString", DefaultValue = "", IsRequired = true)]
		internal string ConnectionString
		{
			get
			{
				return (string)base["connectionString"];
			}
			set
			{
				base["connectionString"] = value;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x0001EAD8 File Offset: 0x0001CCD8
		// (set) Token: 0x0600089E RID: 2206 RVA: 0x0001EAEA File Offset: 0x0001CCEA
		[ConfigurationProperty("maxRetries", DefaultValue = 1, IsRequired = false)]
		internal int MaxStoreRetries
		{
			get
			{
				return (int)base["maxRetries"];
			}
			set
			{
				base["maxRetries"] = value;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x0001EAFD File Offset: 0x0001CCFD
		// (set) Token: 0x060008A0 RID: 2208 RVA: 0x0001EB0F File Offset: 0x0001CD0F
		[ConfigurationProperty("retryInterval", DefaultValue = 1, IsRequired = false)]
		internal int StoreRetryInterval
		{
			get
			{
				return (int)base["retryInterval"];
			}
			set
			{
				base["retryInterval"] = value;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x0001EB22 File Offset: 0x0001CD22
		internal ConnectionStringSettings ConnectionStringSettings
		{
			get
			{
				return new ConnectionStringSettings(this.Provider, this.ConnectionString, this.Provider);
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0001EB3C File Offset: 0x0001CD3C
		protected ClusterConfigElement(SerializationInfo info, StreamingContext context)
		{
			this.Provider = info.GetString("provider");
			this.CloudProvider = info.GetString("cloudProviderType");
			this.ConnectionString = info.GetString("connectionString");
			this.StoreRetryInterval = (int)info.GetValue("retryInterval", typeof(int));
			this.MaxStoreRetries = (int)info.GetValue("maxRetries", typeof(int));
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0001EBC4 File Offset: 0x0001CDC4
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("provider", this.Provider);
			info.AddValue("cloudProviderType", this.CloudProvider);
			info.AddValue("connectionString", this.ConnectionString);
			info.AddValue("maxRetries", this.MaxStoreRetries);
			info.AddValue("retryInterval", this.StoreRetryInterval);
		}

		// Token: 0x04000691 RID: 1681
		internal const string PROVIDER = "provider";

		// Token: 0x04000692 RID: 1682
		internal const string CONNECTION_STR = "connectionString";

		// Token: 0x04000693 RID: 1683
		internal const string CLOUD_PROVIDER_TYPE = "cloudProviderType";

		// Token: 0x04000694 RID: 1684
		internal const string XML_PROVIDER = "xml";

		// Token: 0x04000695 RID: 1685
		internal const string CSCFG_PROVIDER = "cscfg";

		// Token: 0x04000696 RID: 1686
		internal const string CONNECTION_STR_DEFAULT = "";

		// Token: 0x04000697 RID: 1687
		internal const string CLOUD_PROVIDER_STR_DEFAULT = "";

		// Token: 0x04000698 RID: 1688
		internal const string MAX_STORE_RETRIES = "maxRetries";

		// Token: 0x04000699 RID: 1689
		internal const string STORE_RETRY_INTERVAL = "retryInterval";
	}
}
