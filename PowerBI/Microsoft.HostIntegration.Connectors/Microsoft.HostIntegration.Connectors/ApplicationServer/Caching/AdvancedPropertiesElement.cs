using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000B9 RID: 185
	[Serializable]
	internal class AdvancedPropertiesElement : ConfigurationElement, ISerializable
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x00015600 File Offset: 0x00013800
		public static string Name
		{
			get
			{
				return "advancedProperties";
			}
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00015607 File Offset: 0x00013807
		public AdvancedPropertiesElement()
		{
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x0001560F File Offset: 0x0001380F
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x00015621 File Offset: 0x00013821
		[ConfigurationProperty("partitionStoreConnectionSettings", IsRequired = false)]
		public CASConfigElement CasConfigConnectionSettings
		{
			get
			{
				return (CASConfigElement)base["partitionStoreConnectionSettings"];
			}
			set
			{
				base["partitionStoreConnectionSettings"] = value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0001562F File Offset: 0x0001382F
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x00015641 File Offset: 0x00013841
		[ConfigurationProperty("routingLookupRetry", IsRequired = false)]
		public RoutingLookUpElement RoutingLookUpConfig
		{
			get
			{
				return (RoutingLookUpElement)base["routingLookupRetry"];
			}
			set
			{
				base["routingLookupRetry"] = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x0001564F File Offset: 0x0001384F
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x00015661 File Offset: 0x00013861
		[ConfigurationProperty("requestRetry", IsRequired = false)]
		public RequestRetryElement RequestRetryElement
		{
			get
			{
				return (RequestRetryElement)base["requestRetry"];
			}
			set
			{
				base["requestRetry"] = value;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x0001566F File Offset: 0x0001386F
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x00015681 File Offset: 0x00013881
		[ConfigurationProperty("regionProperties", IsRequired = false)]
		public RegionProperties RegionProperties
		{
			get
			{
				return (RegionProperties)base["regionProperties"];
			}
			set
			{
				base["regionProperties"] = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0001568F File Offset: 0x0001388F
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x000156A1 File Offset: 0x000138A1
		[ConfigurationProperty("storeProperties", IsRequired = false)]
		public StoreProperties StoreProperties
		{
			get
			{
				return (StoreProperties)base["storeProperties"];
			}
			set
			{
				base["storeProperties"] = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x000156AF File Offset: 0x000138AF
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x000156C1 File Offset: 0x000138C1
		[ConfigurationProperty("memoryPressureMonitor", IsRequired = false)]
		public MemoryPressureMonitorProperties MemoryPressureMonitorProperties
		{
			get
			{
				return (MemoryPressureMonitorProperties)base["memoryPressureMonitor"];
			}
			set
			{
				base["memoryPressureMonitor"] = value;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x000156CF File Offset: 0x000138CF
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x000156E1 File Offset: 0x000138E1
		[ConfigurationProperty("securityProperties", IsRequired = false)]
		public ServerSecurityProperties SecurityProperties
		{
			get
			{
				return (ServerSecurityProperties)base["securityProperties"];
			}
			set
			{
				base["securityProperties"] = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x000156EF File Offset: 0x000138EF
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x00015701 File Offset: 0x00013901
		[ConfigurationProperty("transportProperties", IsRequired = false)]
		public TransportElement TransportProperties
		{
			get
			{
				return (TransportElement)base["transportProperties"];
			}
			set
			{
				base["transportProperties"] = value;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0001570F File Offset: 0x0001390F
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x00015721 File Offset: 0x00013921
		[ConfigurationProperty("quotaProperties", IsRequired = false)]
		public QuotaProperties QuotaProperties
		{
			get
			{
				return (QuotaProperties)base["quotaProperties"];
			}
			set
			{
				base["quotaProperties"] = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0001572F File Offset: 0x0001392F
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x00015741 File Offset: 0x00013941
		[ConfigurationProperty("usageProperties", IsRequired = false)]
		public UsageProperties UsageProperties
		{
			get
			{
				return (UsageProperties)base["usageProperties"];
			}
			set
			{
				base["usageProperties"] = value;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0001574F File Offset: 0x0001394F
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x00015761 File Offset: 0x00013961
		[ConfigurationProperty("versionProperties", IsRequired = false)]
		public VersionProperties VersionProperties
		{
			get
			{
				return (VersionProperties)base["versionProperties"];
			}
			set
			{
				base["versionProperties"] = value;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0001576F File Offset: 0x0001396F
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x00015781 File Offset: 0x00013981
		[ConfigurationProperty("storeVersionProperties", IsRequired = false)]
		public StoreVersionProperties StoreVersion
		{
			get
			{
				return (StoreVersionProperties)base["storeVersionProperties"];
			}
			set
			{
				base["storeVersionProperties"] = value;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0001578F File Offset: 0x0001398F
		// (set) Token: 0x06000488 RID: 1160 RVA: 0x000157A1 File Offset: 0x000139A1
		[ConfigurationProperty("dnsDomain", IsRequired = false)]
		public string DnsDomain
		{
			get
			{
				return (string)base["dnsDomain"];
			}
			set
			{
				base["dnsDomain"] = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x000157AF File Offset: 0x000139AF
		// (set) Token: 0x0600048A RID: 1162 RVA: 0x000157C1 File Offset: 0x000139C1
		[ConfigurationProperty("diagnosticMode", IsRequired = false)]
		public string DiagnosticMode
		{
			get
			{
				return (string)base["diagnosticMode"];
			}
			set
			{
				base["diagnosticMode"] = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x000157CF File Offset: 0x000139CF
		// (set) Token: 0x0600048C RID: 1164 RVA: 0x000157E1 File Offset: 0x000139E1
		[ConfigurationProperty("diagnosticBufferSize", IsRequired = false, DefaultValue = 300)]
		public int DiagnosticBufferSize
		{
			get
			{
				return (int)base["diagnosticBufferSize"];
			}
			set
			{
				base["diagnosticBufferSize"] = value;
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x000157F4 File Offset: 0x000139F4
		protected AdvancedPropertiesElement(SerializationInfo info, StreamingContext context)
		{
			Version version = null;
			if (context.Context != null)
			{
				SerializationContext serializationContext = context.Context as SerializationContext;
				version = serializationContext.StoreVersion;
			}
			this.CasConfigConnectionSettings = (CASConfigElement)info.GetValue("partitionStoreConnectionSettings", typeof(CASConfigElement));
			this.RoutingLookUpConfig = (RoutingLookUpElement)info.GetValue("routingLookupRetry", typeof(RoutingLookUpElement));
			this.RequestRetryElement = (RequestRetryElement)info.GetValue("requestRetry", typeof(RequestRetryElement));
			this.StoreProperties = (StoreProperties)info.GetValue("storeProperties", typeof(StoreProperties));
			this.RegionProperties = (RegionProperties)info.GetValue("regionProperties", typeof(RegionProperties));
			this.MemoryPressureMonitorProperties = (MemoryPressureMonitorProperties)info.GetValue("memoryPressureMonitor", typeof(MemoryPressureMonitorProperties));
			this.SecurityProperties = (ServerSecurityProperties)info.GetValue("securityProperties", typeof(ServerSecurityProperties));
			this.TransportProperties = (TransportElement)info.GetValue("transportProperties", typeof(TransportElement));
			try
			{
				this.QuotaProperties = (QuotaProperties)info.GetValue("quotaProperties", typeof(QuotaProperties));
			}
			catch (SerializationException)
			{
				this.QuotaProperties = new QuotaProperties();
			}
			if (ConfigManager.IsStoreVersionHigherThan2000(version))
			{
				this.UsageProperties = (UsageProperties)info.GetValue("usageProperties", typeof(UsageProperties));
				this.VersionProperties = (VersionProperties)info.GetValue("versionProperties", typeof(VersionProperties));
			}
			else
			{
				this.UsageProperties = new UsageProperties();
				this.VersionProperties = new VersionProperties();
			}
			try
			{
				this.DnsDomain = (string)info.GetValue("dnsDomain", typeof(string));
			}
			catch (SerializationException)
			{
				this.DnsDomain = string.Empty;
			}
			try
			{
				this.DiagnosticMode = (string)info.GetValue("diagnosticMode", typeof(string));
			}
			catch (SerializationException)
			{
				this.DiagnosticMode = string.Empty;
			}
			try
			{
				this.DiagnosticBufferSize = (int)info.GetValue("diagnosticBufferSize", typeof(int));
			}
			catch (SerializationException)
			{
				this.DiagnosticBufferSize = 300;
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00015A7C File Offset: 0x00013C7C
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("partitionStoreConnectionSettings", this.CasConfigConnectionSettings);
			info.AddValue("requestRetry", this.RequestRetryElement);
			info.AddValue("routingLookupRetry", this.RoutingLookUpConfig);
			info.AddValue("storeProperties", this.StoreProperties);
			info.AddValue("regionProperties", this.RegionProperties);
			info.AddValue("memoryPressureMonitor", this.MemoryPressureMonitorProperties);
			info.AddValue("securityProperties", this.SecurityProperties);
			info.AddValue("transportProperties", this.TransportProperties);
			info.AddValue("quotaProperties", this.QuotaProperties);
			info.AddValue("usageProperties", this.UsageProperties);
			info.AddValue("versionProperties", this.VersionProperties);
			info.AddValue("dnsDomain", this.DnsDomain);
			info.AddValue("diagnosticMode", this.DiagnosticMode);
			info.AddValue("diagnosticBufferSize", this.DiagnosticBufferSize);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00015B78 File Offset: 0x00013D78
		public AdvancePropertiesChange ComputeDifferences(AdvancedPropertiesElement other)
		{
			return new AdvancePropertiesChange
			{
				MemPressureChange = this.MemoryPressureMonitorProperties.ComputeDifferences(other.MemoryPressureMonitorProperties),
				AllowedVersionsChange = this.VersionProperties.ComputeDifferences(other.VersionProperties)
			};
		}

		// Token: 0x0400034B RID: 843
		internal const string ROUTING_LOOKUP_RETRY = "routingLookupRetry";

		// Token: 0x0400034C RID: 844
		internal const string REQUEST_RETRY = "requestRetry";

		// Token: 0x0400034D RID: 845
		internal const string CASCONFIG_CONN_SETTINGS = "partitionStoreConnectionSettings";

		// Token: 0x0400034E RID: 846
		internal const string TKT_DIR = "ticketDirectory";

		// Token: 0x0400034F RID: 847
		internal const string STORE_PROPERTIES = "storeProperties";

		// Token: 0x04000350 RID: 848
		internal const string REGION_PROPERTIES = "regionProperties";

		// Token: 0x04000351 RID: 849
		internal const string MEM_PRESSURE_MONITOR_PROPERTIES = "memoryPressureMonitor";

		// Token: 0x04000352 RID: 850
		internal const string SECURITY_PROPERTIES = "securityProperties";

		// Token: 0x04000353 RID: 851
		internal const string TRANSPORT_PROPERTIES = "transportProperties";

		// Token: 0x04000354 RID: 852
		internal const string QUOTA_PROPERTIES = "quotaProperties";

		// Token: 0x04000355 RID: 853
		internal const string USAGE_PROPERTIES = "usageProperties";

		// Token: 0x04000356 RID: 854
		internal const string VERSION_PROPERTIES = "versionProperties";

		// Token: 0x04000357 RID: 855
		internal const string STORE_VERSION_PROPERTIES = "storeVersionProperties";

		// Token: 0x04000358 RID: 856
		internal const string DNS_DOMAIN = "dnsDomain";

		// Token: 0x04000359 RID: 857
		internal const string DIAGNOSTICMODE = "diagnosticMode";

		// Token: 0x0400035A RID: 858
		internal const string DIAGNOSTICBUFFERSIZE = "diagnosticBufferSize";
	}
}
