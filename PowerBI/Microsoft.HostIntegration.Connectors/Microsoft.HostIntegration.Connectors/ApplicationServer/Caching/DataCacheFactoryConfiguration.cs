using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000005 RID: 5
	public class DataCacheFactoryConfiguration : ICloneable
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020D0 File Offset: 0x000002D0
		public static bool CreateNamedConfiguration(string clientName, DataCacheFactoryConfiguration config, bool useConnectionPool)
		{
			if (clientName == null)
			{
				throw new ArgumentNullException("clientName");
			}
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			bool flag2;
			lock (DataCacheFactoryConfiguration._staticLockObject)
			{
				if (DataCacheFactoryConfiguration._cfr[clientName] != null || DataCacheFactoryConfiguration._namedClients.ContainsKey(clientName))
				{
					flag2 = false;
				}
				else
				{
					DataCacheFactoryConfiguration dataCacheFactoryConfiguration = config.Clone() as DataCacheFactoryConfiguration;
					dataCacheFactoryConfiguration._useConnectionPool = useConnectionPool;
					dataCacheFactoryConfiguration._name = clientName;
					DataCacheFactoryConfiguration._namedClients[clientName] = dataCacheFactoryConfiguration;
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000216C File Offset: 0x0000036C
		public static bool RemoveNamedConfiguration(string clientName)
		{
			if (clientName == null)
			{
				throw new ArgumentNullException("clientName");
			}
			bool flag2;
			lock (DataCacheFactoryConfiguration._staticLockObject)
			{
				flag2 = DataCacheFactoryConfiguration._namedClients.Remove(clientName);
			}
			return flag2;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021C0 File Offset: 0x000003C0
		public DataCacheFactoryConfiguration()
		{
			this.Initialize("default");
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021D3 File Offset: 0x000003D3
		public DataCacheFactoryConfiguration(string clientName)
		{
			this.Initialize(clientName);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021E2 File Offset: 0x000003E2
		internal DataCacheFactoryConfiguration(ClientConfigReader reader, string clientName)
		{
			DataCacheFactoryConfiguration._cfr = reader;
			this.Initialize(clientName);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021F7 File Offset: 0x000003F7
		internal DataCacheFactoryConfiguration(DataCacheNamedClient client, ProtocolType protocol)
		{
			this.Protocol = protocol;
			this.InitializeProperties(client);
			this.InitializeServers(client);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002214 File Offset: 0x00000414
		private void CloneProperties(DataCacheFactoryConfiguration config)
		{
			this._localCacheProps = config.LocalCacheProperties;
			this._transportProps = config.TransportProperties;
			this._notification = config.NotificationProperties;
			this._requestTimeout = config.RequestTimeout;
			this._channelOpenTimeout = config.ChannelOpenTimeout;
			this._maxConnectionsToServer = config.MaxConnectionsToServer;
			this._securityProps = config.SecurityProperties;
			this._dataCacheServiceAccountType = config.DataCacheServiceAccountType;
			this._name = config.Name;
			this._useConnectionPool = config.UseConnectionPool;
			this._servers = config.ServerList;
			this._autoDiscover = new DataCacheAutoDiscoverProperty(config.AutoDiscoverProperty.IsEnabled, config.AutoDiscoverProperty.IdentifierTypeSpecified, config.AutoDiscoverProperty.Identifier, config.AutoDiscoverProperty.DiscoveryPort, config.AutoDiscoverProperty.StartPort);
			this._cacheReadyRetryPolicy = new DataCacheReadyRetryPolicy(config.CacheReadyRetryPolicy.RetryCount, config.CacheReadyRetryPolicy.MaximumRetryIntervalInSeconds);
			this._serializationProperties = config.SerializationProperties;
			this._useLegacyProtocol = config.UseLegacyProtocol;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002320 File Offset: 0x00000520
		private bool InitializeFromNamedClients(string clientName)
		{
			lock (DataCacheFactoryConfiguration._staticLockObject)
			{
				DataCacheFactoryConfiguration dataCacheFactoryConfiguration;
				if (!DataCacheFactoryConfiguration._namedClients.TryGetValue(clientName, out dataCacheFactoryConfiguration))
				{
					return false;
				}
				this.CloneProperties(dataCacheFactoryConfiguration);
			}
			return true;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002378 File Offset: 0x00000578
		private void Initialize(string clientName)
		{
			DataCacheNamedClient dataCacheNamedClient = DataCacheFactoryConfiguration._cfr[clientName];
			if (dataCacheNamedClient == null)
			{
				if (!this.InitializeFromNamedClients(clientName))
				{
					throw new DataCacheException("CONFIGURATION_MANAGER", 8003, string.Format(CultureInfo.CurrentCulture, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "ERRCMC0001"), new object[] { clientName }));
				}
			}
			else
			{
				this.InitializeProperties(dataCacheNamedClient);
				this.InitializeServers(dataCacheNamedClient);
			}
			Type type = null;
			bool flag = false;
			try
			{
				type = Utility.LoadAzureClientAssembly();
				MethodInfo method = type.GetMethod("IsAzureEnvironmentAvailable");
				ReleaseAssert.IsTrue(method != null);
				flag = (bool)method.Invoke(null, null);
			}
			catch (FileNotFoundException)
			{
				flag = false;
			}
			catch (FileLoadException)
			{
				flag = false;
			}
			catch (TypeInitializationException)
			{
				flag = false;
			}
			catch (TargetInvocationException)
			{
				flag = false;
			}
			if (flag)
			{
				MethodInfo method2 = type.GetMethod("RegisterAzureCallbacks");
				ReleaseAssert.IsTrue(method2 != null);
				method2.Invoke(null, null);
				if (DataCacheFactoryConfiguration._cfr.LogSink == null || !DataCacheFactoryConfiguration._cfr.LogSink.IsConfigEntryPresent)
				{
					MethodInfo method3 = type.GetMethod("ReadClientLogLevel");
					ReleaseAssert.IsTrue(method3 != null);
					TraceLevel traceLevel = (TraceLevel)method3.Invoke(null, null);
					DataCacheFactoryConfiguration._cfr.LogSink = new DataCacheLogSink(DataCacheSinkType.DiagnosticsTraceSink, traceLevel);
					Provider.OverrideProviderLevel(traceLevel);
				}
				else
				{
					Provider.OverrideProviderLevel(DataCacheFactoryConfiguration._cfr.LogSink.Level);
				}
			}
			DataCacheClientLogManager.Initialize(DataCacheFactoryConfiguration._cfr.LogSink);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002514 File Offset: 0x00000714
		private void InitializeServers(DataCacheNamedClient client)
		{
			if (!client.AutoDiscover.IsEnabled)
			{
				List<IClientSideHostConfiguration> list = new List<IClientSideHostConfiguration>();
				foreach (object obj in client.Hosts)
				{
					list.Add((ClientSideHostConfiguration)obj);
				}
				List<DataCacheServerEndpoint> list2 = new List<DataCacheServerEndpoint>(list.Count);
				for (int i = 0; i < list.Count; i++)
				{
					list2.Add(new DataCacheServerEndpoint(list[i].Name, list[i].ServicePort));
				}
				this._servers = new ReadOnlyCollection<DataCacheServerEndpoint>(list2);
				return;
			}
			if (client.Hosts.Count != 0)
			{
				throw new InvalidOperationException("Cannot set both Auto-Discover and Servers");
			}
			this._servers = new ReadOnlyCollection<DataCacheServerEndpoint>(new List<DataCacheServerEndpoint>());
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000025D0 File Offset: 0x000007D0
		private void InitializeProperties(DataCacheNamedClient client)
		{
			this.isCompressionEnabled = client.IsCompressionEnabled;
			if (client.LocalCache.IsEnabled)
			{
				this._localCacheProps = new DataCacheLocalCacheProperties(client.LocalCache.ObjectCount, new TimeSpan(0, 0, client.LocalCache.DefaultTTL), client.LocalCache.SyncPolicy);
			}
			else
			{
				this._localCacheProps = new DataCacheLocalCacheProperties();
			}
			this._transportProps = client.TransportProperties.GetDataCacheTransportProperties();
			this._notification = new DataCacheNotificationProperties((long)client.Notification.MaxQueueLength, new TimeSpan(0, 0, client.Notification.PollInterval));
			this._requestTimeout = new TimeSpan(0, 0, 0, 0, client.RequestTimeout);
			this._channelOpenTimeout = new TimeSpan(0, 0, 0, 0, client.ChannelOpenTimeout);
			if (client.MaxConnectionsToServer == 0)
			{
				this._maxConnectionsToServer = Environment.ProcessorCount;
				if (this._maxConnectionsToServer <= 0)
				{
					this._maxConnectionsToServer = 1;
				}
			}
			else
			{
				this._maxConnectionsToServer = client.MaxConnectionsToServer;
			}
			this._useLegacyProtocol = client.UseLegacyProtocol;
			this._cacheReadyRetryPolicy = new DataCacheReadyRetryPolicy(client.CacheReadyRetryPolicy.RetryCount, client.CacheReadyRetryPolicy.MaximumRetryIntervalInSeconds);
			if (client.AutoDiscover.IsEnabled)
			{
				this._autoDiscover = new DataCacheAutoDiscoverProperty(client.AutoDiscover.IsEnabled, IdentifierType.RoleName, client.AutoDiscover.Identifier);
			}
			else
			{
				this._autoDiscover = new DataCacheAutoDiscoverProperty(client.AutoDiscover.IsEnabled);
			}
			this._securityProps = new DataCacheSecurity(client.SecurityProperties);
			this._dataCacheServiceAccountType = client.DataCacheServiceAccountType;
			this._name = client.ClientName;
			this._useConnectionPool = client.ConnectionPool;
			this._serializationProperties = new DataCacheSerializationProperties(client.SerializationProperties.SerializerType, client.SerializationProperties.CustomSerializerTypeName);
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002795 File Offset: 0x00000995
		// (set) Token: 0x06000012 RID: 18 RVA: 0x0000279D File Offset: 0x0000099D
		public bool IsCompressionEnabled
		{
			get
			{
				return this.isCompressionEnabled;
			}
			set
			{
				this._useConnectionPool = false;
				this.isCompressionEnabled = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000027AD File Offset: 0x000009AD
		// (set) Token: 0x06000014 RID: 20 RVA: 0x000027B5 File Offset: 0x000009B5
		public TimeSpan RequestTimeout
		{
			get
			{
				return this._requestTimeout;
			}
			set
			{
				if (value <= TimeSpan.Zero)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "TimeoutZeroOrLess"), "value");
				}
				this._requestTimeout = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000027E5 File Offset: 0x000009E5
		// (set) Token: 0x06000016 RID: 22 RVA: 0x000027ED File Offset: 0x000009ED
		public int MaxConnectionsToServer
		{
			get
			{
				return this._maxConnectionsToServer;
			}
			set
			{
				if (value > 100 || value < 1)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "MaxConnectionsInvalid"), "value");
				}
				this._useConnectionPool = false;
				this._maxConnectionsToServer = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002820 File Offset: 0x00000A20
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002828 File Offset: 0x00000A28
		public DataCacheAutoDiscoverProperty AutoDiscoverProperty
		{
			get
			{
				return this._autoDiscover;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this._servers.Count != 0)
				{
					throw new ArgumentException("Cannot set both AutoDiscover and Servers");
				}
				this._autoDiscover = value;
				this._useConnectionPool = false;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000285E File Offset: 0x00000A5E
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002866 File Offset: 0x00000A66
		public DataCacheReadyRetryPolicy CacheReadyRetryPolicy
		{
			get
			{
				return this._cacheReadyRetryPolicy;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._cacheReadyRetryPolicy = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000287D File Offset: 0x00000A7D
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002885 File Offset: 0x00000A85
		public TimeSpan ChannelOpenTimeout
		{
			get
			{
				return this._channelOpenTimeout;
			}
			set
			{
				if (value.CompareTo(TimeSpan.Zero) >= 0)
				{
					this._channelOpenTimeout = value;
					return;
				}
				throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "TimeoutZero"), "value");
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000028B7 File Offset: 0x00000AB7
		// (set) Token: 0x0600001E RID: 30 RVA: 0x000028C0 File Offset: 0x00000AC0
		public IEnumerable<DataCacheServerEndpoint> Servers
		{
			get
			{
				return this._servers;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this._autoDiscover.IsEnabled)
				{
					throw new ArgumentException("Cannot set both AutoDiscover and Servers");
				}
				List<DataCacheServerEndpoint> list = new List<DataCacheServerEndpoint>();
				foreach (DataCacheServerEndpoint dataCacheServerEndpoint in value)
				{
					if (dataCacheServerEndpoint == null)
					{
						throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "OneItemNull"), "value");
					}
					list.Add(dataCacheServerEndpoint);
				}
				if (list.Count == 0)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "OneItemNull"), "value");
				}
				this._useConnectionPool = false;
				this._servers = new ReadOnlyCollection<DataCacheServerEndpoint>(list);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002988 File Offset: 0x00000B88
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002990 File Offset: 0x00000B90
		public DataCacheLocalCacheProperties LocalCacheProperties
		{
			get
			{
				return this._localCacheProps;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._localCacheProps = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000029A7 File Offset: 0x00000BA7
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000029AF File Offset: 0x00000BAF
		public DataCacheNotificationProperties NotificationProperties
		{
			get
			{
				return this._notification;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._notification = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000029C6 File Offset: 0x00000BC6
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000029CE File Offset: 0x00000BCE
		public DataCacheSecurity SecurityProperties
		{
			get
			{
				return this._securityProps;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._useConnectionPool = false;
				this._securityProps = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000029EC File Offset: 0x00000BEC
		// (set) Token: 0x06000026 RID: 38 RVA: 0x000029F4 File Offset: 0x00000BF4
		public DataCacheTransportProperties TransportProperties
		{
			get
			{
				return this._transportProps;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._useConnectionPool = false;
				this._transportProps = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002A12 File Offset: 0x00000C12
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002A1A File Offset: 0x00000C1A
		public DataCacheServiceAccountType DataCacheServiceAccountType
		{
			get
			{
				return this._dataCacheServiceAccountType;
			}
			set
			{
				this._useConnectionPool = false;
				this._dataCacheServiceAccountType = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002A2A File Offset: 0x00000C2A
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002A32 File Offset: 0x00000C32
		public bool UseLegacyProtocol
		{
			get
			{
				return this._useLegacyProtocol;
			}
			set
			{
				if (value != this._useLegacyProtocol)
				{
					this._useConnectionPool = false;
					this._useLegacyProtocol = value;
				}
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002A4B File Offset: 0x00000C4B
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002A53 File Offset: 0x00000C53
		public DataCacheSerializationProperties SerializationProperties
		{
			get
			{
				return this._serializationProperties;
			}
			set
			{
				this._serializationProperties = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000028B7 File Offset: 0x00000AB7
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002A5C File Offset: 0x00000C5C
		internal ReadOnlyCollection<DataCacheServerEndpoint> ServerList
		{
			get
			{
				return this._servers;
			}
			set
			{
				this._servers = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002A65 File Offset: 0x00000C65
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002A6D File Offset: 0x00000C6D
		internal DataCacheDeploymentMode DeploymentMode
		{
			get
			{
				return this._deploymentMode;
			}
			set
			{
				this._deploymentMode = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002A76 File Offset: 0x00000C76
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002A7E File Offset: 0x00000C7E
		internal string SslSubjectIdentity
		{
			get
			{
				return this._sslSubjectIdentity;
			}
			set
			{
				this._sslSubjectIdentity = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002A87 File Offset: 0x00000C87
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002A8F File Offset: 0x00000C8F
		internal bool UseConnectionPool
		{
			get
			{
				return this._useConnectionPool;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002A97 File Offset: 0x00000C97
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002A9F File Offset: 0x00000C9F
		internal ProtocolType Protocol { get; set; }

		// Token: 0x06000037 RID: 55 RVA: 0x00002AA8 File Offset: 0x00000CA8
		public object Clone()
		{
			DataCacheFactoryConfiguration dataCacheFactoryConfiguration = (DataCacheFactoryConfiguration)base.MemberwiseClone();
			if (dataCacheFactoryConfiguration.TransportProperties != null)
			{
				dataCacheFactoryConfiguration._transportProps = (DataCacheTransportProperties)dataCacheFactoryConfiguration.TransportProperties.Clone();
			}
			return dataCacheFactoryConfiguration;
		}

		// Token: 0x04000004 RID: 4
		private static object _staticLockObject = new object();

		// Token: 0x04000005 RID: 5
		private DataCacheDeploymentMode _deploymentMode;

		// Token: 0x04000006 RID: 6
		private TimeSpan _requestTimeout;

		// Token: 0x04000007 RID: 7
		private int _maxConnectionsToServer;

		// Token: 0x04000008 RID: 8
		private TimeSpan _channelOpenTimeout;

		// Token: 0x04000009 RID: 9
		private ReadOnlyCollection<DataCacheServerEndpoint> _servers;

		// Token: 0x0400000A RID: 10
		private DataCacheNotificationProperties _notification;

		// Token: 0x0400000B RID: 11
		private DataCacheLocalCacheProperties _localCacheProps;

		// Token: 0x0400000C RID: 12
		private DataCacheSecurity _securityProps;

		// Token: 0x0400000D RID: 13
		private DataCacheTransportProperties _transportProps;

		// Token: 0x0400000E RID: 14
		private DataCacheServiceAccountType _dataCacheServiceAccountType;

		// Token: 0x0400000F RID: 15
		private DataCacheReadyRetryPolicy _cacheReadyRetryPolicy;

		// Token: 0x04000010 RID: 16
		private static ClientConfigReader _cfr = new ClientConfigReader();

		// Token: 0x04000011 RID: 17
		private static Dictionary<string, DataCacheFactoryConfiguration> _namedClients = new Dictionary<string, DataCacheFactoryConfiguration>();

		// Token: 0x04000012 RID: 18
		private string _name;

		// Token: 0x04000013 RID: 19
		private DataCacheAutoDiscoverProperty _autoDiscover;

		// Token: 0x04000014 RID: 20
		private bool isCompressionEnabled;

		// Token: 0x04000015 RID: 21
		private string _sslSubjectIdentity;

		// Token: 0x04000016 RID: 22
		private bool _useConnectionPool;

		// Token: 0x04000017 RID: 23
		private bool _useLegacyProtocol;

		// Token: 0x04000018 RID: 24
		private DataCacheSerializationProperties _serializationProperties;
	}
}
