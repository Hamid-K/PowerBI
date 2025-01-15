using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200011D RID: 285
	[Serializable]
	internal class HostConfiguration : ClientSideHostConfiguration, IHostConfiguration, IClientSideHostConfiguration, ICloneable
	{
		// Token: 0x060007FB RID: 2043 RVA: 0x0001D5E2 File Offset: 0x0001B7E2
		public HostConfiguration()
		{
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0001D5EC File Offset: 0x0001B7EC
		public HostConfiguration(CacheHostConfiguration host)
		{
			this.ArbitrationPort = host.ArbitrationPort;
			this.ClusterPort = host.ClusterPort;
			this.IsQuorumHost = host.IsQuorumHost;
			base.Name = host.Name;
			this.ReplicationPort = host.ReplicationPort;
			this.ServiceName = host.ServiceName;
			base.ServicePort = ((host.ServicePort < 1) ? int.MaxValue : host.ServicePort);
			this.CacheSocketPort = host.CacheSocketPort;
			this.CacheDiscoveryPort = host.CacheDiscoveryPort;
			this.SslSocketPort = host.SslSocketPort;
			this.SslDiscoveryPort = host.SslDiscoveryPort;
			this.ServicePortInternal = host.ServicePortInternal;
			this.SslPort = host.ServiceSslPort;
			this.NodeId = host.NodeId;
			this.DisplayFriendlyNodeId = host.FriendlyNodeId;
			this.RestPort = host.RestPort;
			this.RestSslPort = host.RestSslPort;
			if (host.CacheDataSizeInPercent != null)
			{
				this.Size = Utility.ConvertToHostSizeFromPercent(host.CacheDataSizeInPercent.Value);
			}
			if (host.NodeEvictionHWM != null)
			{
				this.HighWaterMarkPercentage = (long)host.NodeEvictionHWM.Value;
			}
			if (host.NodeEvictionLWM != null)
			{
				this.LowWaterMarkPercentage = (long)host.NodeEvictionLWM.Value;
			}
			if (host.DomainInformation != null)
			{
				this.DomainCollection = new HostNodeDomainConfigurationElementCollection();
				foreach (IHostNodeDomainConfiguration hostNodeDomainConfiguration in host.DomainInformation)
				{
					if (hostNodeDomainConfiguration != null)
					{
						HostNodeDomainConfigurationElement hostNodeDomainConfigurationElement = new HostNodeDomainConfigurationElement(hostNodeDomainConfiguration);
						this.DomainCollection.Add(hostNodeDomainConfigurationElement);
					}
				}
			}
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0001D7BC File Offset: 0x0001B9BC
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x0001D7C4 File Offset: 0x0001B9C4
		// (set) Token: 0x060007FF RID: 2047 RVA: 0x0001C54B File Offset: 0x0001A74B
		[ConfigurationProperty("domains", IsDefaultCollection = false, IsRequired = false)]
		[ConfigurationCollection(typeof(HostNodeDomainConfigurationElementCollection), AddItemName = "domain")]
		public HostNodeDomainConfigurationElementCollection DomainCollection
		{
			get
			{
				return (HostNodeDomainConfigurationElementCollection)base["domains"];
			}
			internal set
			{
				base["domains"] = value;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x0001D7E3 File Offset: 0x0001B9E3
		public string RestServiceURI
		{
			get
			{
				return Utility.GetServiceUri(base.Name, this.RestPort, TransportProtocol.Http);
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x0001D7F7 File Offset: 0x0001B9F7
		public string RestSslServiceURI
		{
			get
			{
				return Utility.GetServiceUri(base.Name, this.RestSslPort, TransportProtocol.Https);
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x0001D80C File Offset: 0x0001BA0C
		public IHostNodeDomainConfiguration[] DomainInformation
		{
			get
			{
				HostNodeDomainConfigurationElementCollection domainCollection = this.DomainCollection;
				if (domainCollection == null || domainCollection.Count <= 0)
				{
					return null;
				}
				HostNodeDomainConfigurationElement[] array = new HostNodeDomainConfigurationElement[domainCollection.Count];
				this.DomainCollection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x0001D848 File Offset: 0x0001BA48
		// (set) Token: 0x06000804 RID: 2052 RVA: 0x0001D85A File Offset: 0x0001BA5A
		[ConfigurationProperty("replicationPort", DefaultValue = 0, IsRequired = false)]
		public int ReplicationPort
		{
			get
			{
				return (int)base["replicationPort"];
			}
			set
			{
				base["replicationPort"] = value;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x0001D86D File Offset: 0x0001BA6D
		public string ServiceURIInternal
		{
			get
			{
				if (this.ServicePortInternal != 0)
				{
					return Utility.GetServiceUri(base.Name, this.ServicePortInternal, TransportProtocol.NetTcp);
				}
				return Utility.GetServiceUri(base.Name, base.ServicePort, TransportProtocol.NetTcp);
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x0001D89C File Offset: 0x0001BA9C
		public string ServiceSslUri
		{
			get
			{
				return Utility.GetServiceUri(base.Name, this.SslPort, TransportProtocol.NetTcp);
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x0001D8B0 File Offset: 0x0001BAB0
		// (set) Token: 0x06000808 RID: 2056 RVA: 0x0001D8C2 File Offset: 0x0001BAC2
		[ConfigurationProperty("servicePortInternal", DefaultValue = 0, IsRequired = false)]
		public int ServicePortInternal
		{
			get
			{
				return (int)base["servicePortInternal"];
			}
			set
			{
				base["servicePortInternal"] = value;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x0001D8D5 File Offset: 0x0001BAD5
		// (set) Token: 0x0600080A RID: 2058 RVA: 0x0001D8E7 File Offset: 0x0001BAE7
		[ConfigurationProperty("cacheSocketPort", DefaultValue = 0, IsRequired = false)]
		public int CacheSocketPort
		{
			get
			{
				return (int)base["cacheSocketPort"];
			}
			set
			{
				base["cacheSocketPort"] = value;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x0001D8FA File Offset: 0x0001BAFA
		// (set) Token: 0x0600080C RID: 2060 RVA: 0x0001D90C File Offset: 0x0001BB0C
		[ConfigurationProperty("cacheDiscoveryPort", DefaultValue = 0, IsRequired = false)]
		public int CacheDiscoveryPort
		{
			get
			{
				return (int)base["cacheDiscoveryPort"];
			}
			set
			{
				base["cacheDiscoveryPort"] = value;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x0001D91F File Offset: 0x0001BB1F
		// (set) Token: 0x0600080E RID: 2062 RVA: 0x0001D931 File Offset: 0x0001BB31
		[ConfigurationProperty("cacheSslSocketPort", DefaultValue = 0, IsRequired = false)]
		public int SslSocketPort
		{
			get
			{
				return (int)base["cacheSslSocketPort"];
			}
			set
			{
				base["cacheSslSocketPort"] = value;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x0001D944 File Offset: 0x0001BB44
		// (set) Token: 0x06000810 RID: 2064 RVA: 0x0001D956 File Offset: 0x0001BB56
		[ConfigurationProperty("cacheSslDiscoveryPort", DefaultValue = 0, IsRequired = false)]
		public int SslDiscoveryPort
		{
			get
			{
				return (int)base["cacheSslDiscoveryPort"];
			}
			set
			{
				base["cacheSslDiscoveryPort"] = value;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x0001D969 File Offset: 0x0001BB69
		// (set) Token: 0x06000812 RID: 2066 RVA: 0x0001D97B File Offset: 0x0001BB7B
		[ConfigurationProperty("sslPort", DefaultValue = 22243, IsRequired = false)]
		public int SslPort
		{
			get
			{
				return (int)base["sslPort"];
			}
			set
			{
				base["sslPort"] = value;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x0001D98E File Offset: 0x0001BB8E
		// (set) Token: 0x06000814 RID: 2068 RVA: 0x0001D9A0 File Offset: 0x0001BBA0
		[ConfigurationProperty("arbitrationPort", DefaultValue = 22235, IsRequired = true)]
		public int ArbitrationPort
		{
			get
			{
				return (int)base["arbitrationPort"];
			}
			set
			{
				base["arbitrationPort"] = value;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x0001D9B3 File Offset: 0x0001BBB3
		// (set) Token: 0x06000816 RID: 2070 RVA: 0x0001D9C5 File Offset: 0x0001BBC5
		[ConfigurationProperty("clusterPort", DefaultValue = 22234, IsRequired = true)]
		public int ClusterPort
		{
			get
			{
				return (int)base["clusterPort"];
			}
			set
			{
				base["clusterPort"] = value;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x0001D9D8 File Offset: 0x0001BBD8
		// (set) Token: 0x06000818 RID: 2072 RVA: 0x0001D9EA File Offset: 0x0001BBEA
		[ConfigurationProperty("restPort", DefaultValue = 0, IsRequired = false)]
		public int RestPort
		{
			get
			{
				return (int)base["restPort"];
			}
			set
			{
				base["restPort"] = value;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x0001D9FD File Offset: 0x0001BBFD
		// (set) Token: 0x0600081A RID: 2074 RVA: 0x0001DA0F File Offset: 0x0001BC0F
		[ConfigurationProperty("restSslPort", DefaultValue = 0, IsRequired = false)]
		public int RestSslPort
		{
			get
			{
				return (int)base["restSslPort"];
			}
			set
			{
				base["restSslPort"] = value;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x0001DA22 File Offset: 0x0001BC22
		// (set) Token: 0x0600081C RID: 2076 RVA: 0x0001DA34 File Offset: 0x0001BC34
		[ConfigurationProperty("hostId", DefaultValue = 0, IsRequired = true)]
		public int NodeId
		{
			get
			{
				return (int)base["hostId"];
			}
			set
			{
				base["hostId"] = value;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x0001DA47 File Offset: 0x0001BC47
		// (set) Token: 0x0600081E RID: 2078 RVA: 0x0001DA77 File Offset: 0x0001BC77
		public string DisplayFriendlyNodeId
		{
			get
			{
				if (string.IsNullOrEmpty(this.displayFriendlyNodeId))
				{
					return string.Concat((int)base["hostId"]);
				}
				return this.displayFriendlyNodeId;
			}
			set
			{
				this.displayFriendlyNodeId = value;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x0001DA80 File Offset: 0x0001BC80
		// (set) Token: 0x06000820 RID: 2080 RVA: 0x0001DA92 File Offset: 0x0001BC92
		[ConfigurationProperty("evictionInterval", DefaultValue = 180, IsRequired = false)]
		public int EvictionInterval
		{
			get
			{
				return (int)base["evictionInterval"];
			}
			set
			{
				base["evictionInterval"] = value;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x0001DAA5 File Offset: 0x0001BCA5
		// (set) Token: 0x06000822 RID: 2082 RVA: 0x0001DAB7 File Offset: 0x0001BCB7
		[ConfigurationProperty("highWaterMark", DefaultValue = 99L, IsRequired = false)]
		public long HighWaterMarkPercentage
		{
			get
			{
				return (long)base["highWaterMark"];
			}
			set
			{
				base["highWaterMark"] = value;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x0001DACA File Offset: 0x0001BCCA
		// (set) Token: 0x06000824 RID: 2084 RVA: 0x0001DADC File Offset: 0x0001BCDC
		[ConfigurationProperty("lowWaterMark", DefaultValue = 90L, IsRequired = false)]
		public long LowWaterMarkPercentage
		{
			get
			{
				return (long)base["lowWaterMark"];
			}
			set
			{
				base["lowWaterMark"] = value;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x0001DAF0 File Offset: 0x0001BCF0
		// (set) Token: 0x06000826 RID: 2086 RVA: 0x0001DB1A File Offset: 0x0001BD1A
		[ConfigurationProperty("size", DefaultValue = -1L, IsRequired = true)]
		public long Size
		{
			get
			{
				long num = (long)base["size"];
				if (num == -1L)
				{
					return HostConfiguration._defaultHostSize;
				}
				return num;
			}
			set
			{
				base["size"] = value;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x0001DB2D File Offset: 0x0001BD2D
		// (set) Token: 0x06000828 RID: 2088 RVA: 0x0001DB3F File Offset: 0x0001BD3F
		[ConfigurationProperty("leadHost", DefaultValue = false, IsRequired = true)]
		public bool IsQuorumHost
		{
			get
			{
				return (bool)base["leadHost"];
			}
			set
			{
				base["leadHost"] = value;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000829 RID: 2089 RVA: 0x0001DB52 File Offset: 0x0001BD52
		// (set) Token: 0x0600082A RID: 2090 RVA: 0x0001DB64 File Offset: 0x0001BD64
		[Obsolete("Do not call this method; it was used with FederatedCountMaintainer which has been removed.")]
		[ConfigurationProperty("cacheLineBuffer", DefaultValue = 3, IsRequired = false)]
		[IntegerValidator(MinValue = 2, MaxValue = 4)]
		public int CacheLineBuffer
		{
			get
			{
				return (int)base["cacheLineBuffer"];
			}
			set
			{
				base["cacheLineBuffer"] = value;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x0001DB77 File Offset: 0x0001BD77
		// (set) Token: 0x0600082C RID: 2092 RVA: 0x0001DB89 File Offset: 0x0001BD89
		[ConfigurationProperty("account", IsRequired = true)]
		public string Account
		{
			get
			{
				return (string)base["account"];
			}
			set
			{
				base["account"] = value;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x0001DB97 File Offset: 0x0001BD97
		// (set) Token: 0x0600082E RID: 2094 RVA: 0x0001DBA9 File Offset: 0x0001BDA9
		[ConfigurationProperty("cacheHostName", IsRequired = true)]
		public string ServiceName
		{
			get
			{
				return (string)base["cacheHostName"];
			}
			set
			{
				base["cacheHostName"] = value;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x0001DBB7 File Offset: 0x0001BDB7
		public IDictionary<string, int> MemcacheSocketPorts
		{
			get
			{
				return this.MemcachePortsCollection;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x0001DBBF File Offset: 0x0001BDBF
		// (set) Token: 0x06000831 RID: 2097 RVA: 0x0001DBD1 File Offset: 0x0001BDD1
		[ConfigurationProperty("memcachePorts", IsDefaultCollection = false, IsRequired = false)]
		[ConfigurationCollection(typeof(MemcachePortsCollection), AddItemName = "cache")]
		public MemcachePortsCollection MemcachePortsCollection
		{
			get
			{
				return (MemcachePortsCollection)base["memcachePorts"];
			}
			set
			{
				base["memcachePorts"] = value;
			}
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0001DBE0 File Offset: 0x0001BDE0
		public HostConfiguration(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.ReplicationPort = (int)info.GetValue("replicationPort", typeof(int));
			this.ArbitrationPort = (int)info.GetValue("arbitrationPort", typeof(int));
			this.ClusterPort = (int)info.GetValue("clusterPort", typeof(int));
			this.NodeId = (int)info.GetValue("hostId", typeof(int));
			this.EvictionInterval = (int)info.GetValue("evictionInterval", typeof(int));
			this.LowWaterMarkPercentage = (long)info.GetValue("lowWaterMark", typeof(long));
			this.HighWaterMarkPercentage = (long)info.GetValue("highWaterMark", typeof(long));
			this.Size = (long)info.GetValue("size", typeof(long));
			this.IsQuorumHost = info.GetBoolean("leadHost");
			this.CacheLineBuffer = (int)info.GetValue("cacheLineBuffer", typeof(int));
			this.Account = (string)info.GetValue("account", typeof(string));
			this.ServiceName = info.GetString("cacheHostName");
			try
			{
				this.ServicePortInternal = (int)info.GetValue("servicePortInternal", typeof(int));
			}
			catch (SerializationException)
			{
				this.ServicePortInternal = 0;
			}
			try
			{
				this.DomainCollection = (HostNodeDomainConfigurationElementCollection)info.GetValue("domains", typeof(HostNodeDomainConfigurationElementCollection));
			}
			catch (SerializationException)
			{
				this.DomainCollection = new HostNodeDomainConfigurationElementCollection();
			}
			try
			{
				this.RestPort = (int)info.GetValue("restPort", typeof(int));
			}
			catch (SerializationException)
			{
				this.RestPort = 0;
			}
			try
			{
				this.RestSslPort = (int)info.GetValue("restSslPort", typeof(int));
			}
			catch (SerializationException)
			{
				this.RestSslPort = 0;
			}
			try
			{
				this.CacheSocketPort = (int)info.GetValue("cacheSocketPort", typeof(int));
			}
			catch (SerializationException)
			{
				this.CacheSocketPort = 0;
			}
			try
			{
				this.CacheDiscoveryPort = (int)info.GetValue("cacheDiscoveryPort", typeof(int));
			}
			catch (SerializationException)
			{
				this.CacheDiscoveryPort = 0;
			}
			try
			{
				this.SslSocketPort = (int)info.GetValue("cacheSslSocketPort", typeof(int));
			}
			catch (SerializationException)
			{
				this.SslSocketPort = 0;
			}
			try
			{
				this.SslDiscoveryPort = (int)info.GetValue("cacheSslDiscoveryPort", typeof(int));
			}
			catch (SerializationException)
			{
				this.SslDiscoveryPort = 0;
			}
			try
			{
				this.MemcachePortsCollection = (MemcachePortsCollection)info.GetValue("memcachePorts", typeof(MemcachePortsCollection));
			}
			catch (SerializationException)
			{
				this.MemcachePortsCollection = new MemcachePortsCollection();
			}
			this.DomainCollection.InitializeAfterSerialization();
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0001DF68 File Offset: 0x0001C168
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("arbitrationPort", this.ArbitrationPort);
			info.AddValue("servicePortInternal", this.ServicePortInternal);
			info.AddValue("clusterPort", this.ClusterPort);
			info.AddValue("hostId", this.NodeId);
			info.AddValue("evictionInterval", this.EvictionInterval);
			info.AddValue("lowWaterMark", this.LowWaterMarkPercentage);
			info.AddValue("highWaterMark", this.HighWaterMarkPercentage);
			info.AddValue("size", this.Size);
			info.AddValue("leadHost", this.IsQuorumHost);
			info.AddValue("cacheLineBuffer", this.CacheLineBuffer);
			info.AddValue("replicationPort", this.ReplicationPort);
			info.AddValue("account", this.Account);
			info.AddValue("cacheHostName", this.ServiceName);
			info.AddValue("domains", this.DomainCollection);
			info.AddValue("restPort", this.RestPort);
			info.AddValue("restSslPort", this.RestSslPort);
			info.AddValue("cacheSocketPort", this.CacheSocketPort);
			info.AddValue("cacheDiscoveryPort", this.CacheDiscoveryPort);
			info.AddValue("cacheSslSocketPort", this.SslSocketPort);
			info.AddValue("cacheSslDiscoveryPort", this.SslDiscoveryPort);
			info.AddValue("memcachePorts", this.MemcachePortsCollection, typeof(MemcachePortsCollection));
		}

		// Token: 0x04000659 RID: 1625
		private const int _defaultServicePortInternal = 0;

		// Token: 0x0400065A RID: 1626
		private const int defaultRestPort = 0;

		// Token: 0x0400065B RID: 1627
		internal const string REPLICATION_PORT = "replicationPort";

		// Token: 0x0400065C RID: 1628
		internal const string SERVICE_PORT_INTERNAL = "servicePortInternal";

		// Token: 0x0400065D RID: 1629
		internal const string ARBITRATION_PORT = "arbitrationPort";

		// Token: 0x0400065E RID: 1630
		internal const string CLUSTER_PORT = "clusterPort";

		// Token: 0x0400065F RID: 1631
		internal const string NODE_ID = "hostId";

		// Token: 0x04000660 RID: 1632
		internal const string EVICTION_INTERVAL = "evictionInterval";

		// Token: 0x04000661 RID: 1633
		internal const string LOWWM = "lowWaterMark";

		// Token: 0x04000662 RID: 1634
		internal const string HIGHWM = "highWaterMark";

		// Token: 0x04000663 RID: 1635
		internal const string SIZE = "size";

		// Token: 0x04000664 RID: 1636
		internal const string IS_QUORUM_HOST = "leadHost";

		// Token: 0x04000665 RID: 1637
		internal const string CACHELINE_BUFFER = "cacheLineBuffer";

		// Token: 0x04000666 RID: 1638
		internal const string ACCOUNT = "account";

		// Token: 0x04000667 RID: 1639
		internal const string SERVICE_NAME = "cacheHostName";

		// Token: 0x04000668 RID: 1640
		internal const string SSL_PORT = "sslPort";

		// Token: 0x04000669 RID: 1641
		internal const string REST_PORT = "restPort";

		// Token: 0x0400066A RID: 1642
		internal const string REST_SSL_PORT = "restSslPort";

		// Token: 0x0400066B RID: 1643
		internal const string MEMCACHE_SOCKET_PORT = "memcachePort";

		// Token: 0x0400066C RID: 1644
		internal const string CACHE_SOCKET_PORT = "cacheSocketPort";

		// Token: 0x0400066D RID: 1645
		internal const string CACHE_DISCOVERY_PORT = "cacheDiscoveryPort";

		// Token: 0x0400066E RID: 1646
		internal const string CACHE_SSL_SOCKET_PORT = "cacheSslSocketPort";

		// Token: 0x0400066F RID: 1647
		internal const string CACHE_SSL_DISCOVERY_PORT = "cacheSslDiscoveryPort";

		// Token: 0x04000670 RID: 1648
		internal const string MEMCACHE_PORTS_COLLECTION = "memcachePorts";

		// Token: 0x04000671 RID: 1649
		internal const string DomainCollectionProperty = "domains";

		// Token: 0x04000672 RID: 1650
		private const long UNINITIALIZED_HOST_SIZE = -1L;

		// Token: 0x04000673 RID: 1651
		private static long _defaultHostSize = Utility.GetDefaultHostSize();

		// Token: 0x04000674 RID: 1652
		private string displayFriendlyNodeId;
	}
}
