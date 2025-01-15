using System;
using System.Configuration;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000CB RID: 203
	internal class DataCacheClientSection : ConfigurationSection
	{
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x0001793F File Offset: 0x00015B3F
		public static string Name
		{
			get
			{
				return "dataCacheClient";
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x00017946 File Offset: 0x00015B46
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x00017958 File Offset: 0x00015B58
		[ConfigurationProperty("autoDiscover", IsRequired = false)]
		public AutoDiscoverProperty AutoDiscover
		{
			get
			{
				return (AutoDiscoverProperty)base["autoDiscover"];
			}
			set
			{
				base["autoDiscover"] = value;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x00017966 File Offset: 0x00015B66
		// (set) Token: 0x0600055E RID: 1374 RVA: 0x00017978 File Offset: 0x00015B78
		[ConfigurationProperty("connectionPool", IsRequired = false, DefaultValue = true)]
		public bool ConnectionPool
		{
			get
			{
				return (bool)base["connectionPool"];
			}
			set
			{
				base["connectionPool"] = value;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x0001798B File Offset: 0x00015B8B
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x0001799D File Offset: 0x00015B9D
		[ConfigurationProperty("localCache", IsRequired = false)]
		public LocalCacheProperties LocalCache
		{
			get
			{
				return (LocalCacheProperties)base["localCache"];
			}
			set
			{
				base["localCache"] = value;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x000179AB File Offset: 0x00015BAB
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x000179BD File Offset: 0x00015BBD
		[ConfigurationProperty("clientNotification", IsRequired = false)]
		public ClientNotificationProperties Notification
		{
			get
			{
				return (ClientNotificationProperties)base["clientNotification"];
			}
			set
			{
				base["clientNotification"] = value;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x000179CB File Offset: 0x00015BCB
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x000179DD File Offset: 0x00015BDD
		[ConfigurationProperty("requestTimeout", DefaultValue = 15000, IsRequired = false)]
		public int RequestTimeout
		{
			get
			{
				return (int)base["requestTimeout"];
			}
			set
			{
				base["requestTimeout"] = value;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x000179F0 File Offset: 0x00015BF0
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x00017A02 File Offset: 0x00015C02
		[ConfigurationProperty("isCompressionEnabled", DefaultValue = false, IsRequired = false)]
		internal bool IsCompressionEnabled
		{
			get
			{
				return (bool)base["isCompressionEnabled"];
			}
			set
			{
				base["isCompressionEnabled"] = value;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x00017A15 File Offset: 0x00015C15
		// (set) Token: 0x06000568 RID: 1384 RVA: 0x00017A27 File Offset: 0x00015C27
		[IntegerValidator(MinValue = 0, MaxValue = 100)]
		[ConfigurationProperty("maxConnectionsToServer", DefaultValue = 0, IsRequired = false)]
		public int MaxConnectionsToServer
		{
			get
			{
				return (int)base["maxConnectionsToServer"];
			}
			set
			{
				base["maxConnectionsToServer"] = value;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x00017A3A File Offset: 0x00015C3A
		// (set) Token: 0x0600056A RID: 1386 RVA: 0x00017A4C File Offset: 0x00015C4C
		[IntegerValidator(MinValue = 0, MaxValue = 20000)]
		[ConfigurationProperty("channelOpenTimeout", DefaultValue = 12000, IsRequired = false)]
		public int ChannelOpenTimeout
		{
			get
			{
				return (int)base["channelOpenTimeout"];
			}
			set
			{
				base["channelOpenTimeout"] = value;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x00017A5F File Offset: 0x00015C5F
		// (set) Token: 0x0600056C RID: 1388 RVA: 0x00017A71 File Offset: 0x00015C71
		[ConfigurationProperty("useLegacyProtocol", DefaultValue = true, IsRequired = false)]
		public bool UseLegacyProtocol
		{
			get
			{
				return (bool)base["useLegacyProtocol"];
			}
			set
			{
				base["useLegacyProtocol"] = value;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x00017A84 File Offset: 0x00015C84
		// (set) Token: 0x0600056E RID: 1390 RVA: 0x00017A96 File Offset: 0x00015C96
		[ConfigurationProperty("hosts", IsDefaultCollection = false, IsRequired = false)]
		[ConfigurationCollection(typeof(ClientHostCollection), AddItemName = "host")]
		public ClientHostCollection Hosts
		{
			get
			{
				return (ClientHostCollection)base["hosts"];
			}
			set
			{
				base["hosts"] = value;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x00017AA4 File Offset: 0x00015CA4
		// (set) Token: 0x06000570 RID: 1392 RVA: 0x00015701 File Offset: 0x00013901
		[ConfigurationProperty("transportProperties", IsRequired = false)]
		public CommonTransportElement TransportProperties
		{
			get
			{
				return (CommonTransportElement)base["transportProperties"];
			}
			set
			{
				base["transportProperties"] = value;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x00017AB6 File Offset: 0x00015CB6
		// (set) Token: 0x06000572 RID: 1394 RVA: 0x000156E1 File Offset: 0x000138E1
		[ConfigurationProperty("securityProperties", IsRequired = false)]
		public ClientSecurityProperties SecurityProperties
		{
			get
			{
				return (ClientSecurityProperties)base["securityProperties"];
			}
			set
			{
				base["securityProperties"] = value;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x00017AC8 File Offset: 0x00015CC8
		// (set) Token: 0x06000574 RID: 1396 RVA: 0x00017ADA File Offset: 0x00015CDA
		[ConfigurationProperty("dataCacheServiceAccountType", DefaultValue = DataCacheServiceAccountType.SystemAccount, IsRequired = false)]
		public DataCacheServiceAccountType DataCacheServiceAccountType
		{
			get
			{
				return (DataCacheServiceAccountType)base["dataCacheServiceAccountType"];
			}
			set
			{
				base["dataCacheServiceAccountType"] = value;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x00017AED File Offset: 0x00015CED
		// (set) Token: 0x06000576 RID: 1398 RVA: 0x00017AFF File Offset: 0x00015CFF
		[ConfigurationProperty("tracing", IsRequired = false)]
		public virtual ClientTraceSettings TraceSettings
		{
			get
			{
				return (ClientTraceSettings)base["tracing"];
			}
			set
			{
				base["tracing"] = value;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x00017B0D File Offset: 0x00015D0D
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x00017B1F File Offset: 0x00015D1F
		[ConfigurationProperty("serializationProperties", IsRequired = false)]
		public SerializationConfig SerializationProperties
		{
			get
			{
				return (SerializationConfig)base["serializationProperties"];
			}
			set
			{
				base["serializationProperties"] = value;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x00017B2D File Offset: 0x00015D2D
		// (set) Token: 0x0600057A RID: 1402 RVA: 0x00017B3F File Offset: 0x00015D3F
		[ConfigurationProperty("cacheReadyRetryPolicy", IsRequired = false)]
		public DataCacheReadyRetryPolicyProperty CacheReadyRetryPolicy
		{
			get
			{
				return (DataCacheReadyRetryPolicyProperty)base["cacheReadyRetryPolicy"];
			}
			set
			{
				base["cacheReadyRetryPolicy"] = value;
			}
		}

		// Token: 0x040003AA RID: 938
		internal const string AUTO_DISCOVER = "autoDiscover";

		// Token: 0x040003AB RID: 939
		internal const string LOCAL_CACHE = "localCache";

		// Token: 0x040003AC RID: 940
		internal const string REQUEST_TIMEOUT = "requestTimeout";

		// Token: 0x040003AD RID: 941
		internal const string IsCompressionEnabledConfigurationElement = "isCompressionEnabled";

		// Token: 0x040003AE RID: 942
		internal const string MAX_CONNECTIONS = "maxConnectionsToServer";

		// Token: 0x040003AF RID: 943
		internal const string CHANNEL_OPEN_TIMEOUT = "channelOpenTimeout";

		// Token: 0x040003B0 RID: 944
		internal const string HOSTS = "hosts";

		// Token: 0x040003B1 RID: 945
		internal const string HOST = "host";

		// Token: 0x040003B2 RID: 946
		internal const string DCACHECLIENT = "dataCacheClient";

		// Token: 0x040003B3 RID: 947
		internal const string CLIENT_NOTIFICATION = "clientNotification";

		// Token: 0x040003B4 RID: 948
		internal const string SECURITY_PROPERTIES = "securityProperties";

		// Token: 0x040003B5 RID: 949
		internal const string TRANSPORT_PROPERTIES = "transportProperties";

		// Token: 0x040003B6 RID: 950
		internal const string SERVICE_ACCOUNT_TYPE = "dataCacheServiceAccountType";

		// Token: 0x040003B7 RID: 951
		internal const string TRACE_SETTINGS = "tracing";

		// Token: 0x040003B8 RID: 952
		internal const string CONNECTION_POOL = "connectionPool";

		// Token: 0x040003B9 RID: 953
		internal const string SERIALIZATION_PROPERTIES = "serializationProperties";

		// Token: 0x040003BA RID: 954
		internal const string USE_LEGACY_PROTOCOL = "useLegacyProtocol";

		// Token: 0x040003BB RID: 955
		internal const string CACHE_READY_RETRY_POLICY = "cacheReadyRetryPolicy";
	}
}
