using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000099 RID: 153
	public sealed class DataCacheFactory : IDisposable
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000363 RID: 867 RVA: 0x000114F5 File Offset: 0x0000F6F5
		// (set) Token: 0x06000364 RID: 868 RVA: 0x000114FD File Offset: 0x0000F6FD
		internal IDRMUtility DrmUtility { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000365 RID: 869 RVA: 0x00011506 File Offset: 0x0000F706
		internal DataCacheFactoryConfiguration Configuration
		{
			get
			{
				return this._config;
			}
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00011510 File Offset: 0x0000F710
		internal static DataCacheFactory InitializeOrFetchSingletonFactoryInstance(string clientConfigurationName)
		{
			DataCacheFactory dataCacheFactory = null;
			DataCacheFactory.SharedFactoryInstances.TryGetValue(clientConfigurationName, out dataCacheFactory);
			if (dataCacheFactory != null)
			{
				return dataCacheFactory;
			}
			if (string.CompareOrdinal(clientConfigurationName, "default") == 0)
			{
				dataCacheFactory = new DataCacheFactory();
			}
			else
			{
				DataCacheFactoryConfiguration dataCacheFactoryConfiguration = new DataCacheFactoryConfiguration(clientConfigurationName);
				dataCacheFactory = new DataCacheFactory(dataCacheFactoryConfiguration);
			}
			if (DataCacheFactory.SharedFactoryInstances.TryAdd(clientConfigurationName, dataCacheFactory))
			{
				return dataCacheFactory;
			}
			dataCacheFactory.Dispose();
			dataCacheFactory = null;
			DataCacheFactory.SharedFactoryInstances.TryGetValue(clientConfigurationName, out dataCacheFactory);
			if (dataCacheFactory != null)
			{
				return dataCacheFactory;
			}
			ReleaseAssert.Fail("Factory is null inside shared instances", new object[0]);
			return null;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00011591 File Offset: 0x0000F791
		public DataCacheFactory()
			: this(new DataCacheFactoryConfiguration())
		{
		}

		// Token: 0x06000368 RID: 872 RVA: 0x000115A0 File Offset: 0x0000F7A0
		public DataCacheFactory(DataCacheFactoryConfiguration configuration)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			if (configuration.ServerList.Count == 0 && !configuration.AutoDiscoverProperty.IsEnabled)
			{
				throw DataCache.NewException(21);
			}
			HashSet<DataCacheServerEndpoint> hashSet = new HashSet<DataCacheServerEndpoint>();
			for (int i = 0; i < configuration.ServerList.Count; i++)
			{
				if (configuration.ServerList[i] == null)
				{
					throw DataCache.NewException(21);
				}
				if (!hashSet.Add(configuration.ServerList[i]))
				{
					throw DataCache.NewException(37);
				}
			}
			this._identifier = Interlocked.Increment(ref DataCacheFactory._instanceCounter);
			DataCacheFactory._myComponentName = DataCacheFactory._myComponentName + "." + this._identifier.ToString(NumberFormatInfo.InvariantInfo);
			this._config = (DataCacheFactoryConfiguration)configuration.Clone();
			this._myCache = new Hashtable();
			ClientPerfCounterUpdate.IsPerfCounterEnabled = ClientPerformanceCounters.IsPerfCounterCategoryExists();
			if (this._config.LocalCacheProperties.IsEnabled)
			{
				EvictionParametrs evictionParametrs = new EvictionParametrs(this._config.LocalCacheProperties.ObjectCount, 20);
				this._localCacheStore = new LocalCacheStore(evictionParametrs);
			}
			if (configuration.AutoDiscoverProperty.IsEnabled)
			{
				this.AutoDiscoverServersAndUpdateProperties(configuration);
			}
			if (this._config.UseLegacyProtocol)
			{
				this.UpdateClientIdentityProvider(configuration);
			}
			if ((int)this._config.TransportProperties.ReceiveTimeout.TotalMilliseconds == DataCacheTransportProperties.NOT_ASSIGNED)
			{
				if (configuration.AutoDiscoverProperty.IsEnabled && configuration.AutoDiscoverProperty.IdentifierTypeSpecified != IdentifierType.EndPoint)
				{
					this._config.TransportProperties.ReceiveTimeout = ConfigManager.RECEIVE_TIMEOUT;
				}
				else
				{
					this._config.TransportProperties.ReceiveTimeout = ConfigManager.CLIENT_CHANNEL_RECIEVE_TIMEOUT;
				}
			}
			if (this._config.UseConnectionPool)
			{
				this._sendRcvModule = DataCacheFactory._connectionPool.GetItemFromPool(this._config.Name, this._config.SecurityProperties, this._config.TransportProperties, this._identifier.ToString(NumberFormatInfo.InvariantInfo), ClientVersionInfo.Singleton, new VerifyResponseCallback(ClientVersionInfo.VerifyClientVersion), this._config.ChannelOpenTimeout, this._config.RequestTimeout, this._config.MaxConnectionsToServer, this._clientIdentityProvider, this._config.UseLegacyProtocol);
			}
			else
			{
				this._sendRcvModule = new SimpleSendReceiveModule(this._config.SecurityProperties, this._config.TransportProperties, this._identifier.ToString(NumberFormatInfo.InvariantInfo), ClientVersionInfo.Singleton, new VerifyResponseCallback(ClientVersionInfo.VerifyClientVersion), this._config.ChannelOpenTimeout, this._config.RequestTimeout, this._config.MaxConnectionsToServer, this._clientIdentityProvider, this._config.UseLegacyProtocol);
			}
			if (ClientPerfCounterUpdate.IsPerfCounterEnabled)
			{
				this.AssignClientPerfCounterEvents();
				if (this._config.LocalCacheProperties.IsEnabled)
				{
					this.LocalCacheInstanceCreatedEvent(this._config.LocalCacheProperties.ObjectCount);
				}
			}
			DataCacheFactoryConfigurationSynchronizer.SynchronizerInstance.RegisterFactory(this);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x000118B8 File Offset: 0x0000FAB8
		private void UpdateClientIdentityProvider(DataCacheFactoryConfiguration configuration)
		{
			if (string.IsNullOrEmpty(this._config.SecurityProperties.SslSubjectIdentity))
			{
				this._clientIdentityProvider = new ClientIdentityProvider(configuration.SecurityProperties, configuration.DataCacheServiceAccountType);
				return;
			}
			this._clientIdentityProvider = new TestClientIdentityProvider(configuration.SecurityProperties.SslSubjectIdentity);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0001190C File Offset: 0x0000FB0C
		private void AutoDiscoverServersAndUpdateProperties(DataCacheFactoryConfiguration configuration)
		{
			if (configuration.AutoDiscoverProperty.Identifier.Equals("useDevelopmentCaching=true", StringComparison.OrdinalIgnoreCase))
			{
				this._config.AutoDiscoverProperty.IdentifierTypeSpecified = IdentifierType.ManagedCacheEmulator;
				this._config.ServerList = new ReadOnlyCollection<DataCacheServerEndpoint>(new List<DataCacheServerEndpoint>
				{
					new DataCacheServerEndpoint("localhost", this._config.AutoDiscoverProperty.DiscoveryPort)
				});
				this.ClearSecurityProperties();
				return;
			}
			if (this.TryAutoDiscoverServersWithinDeployment())
			{
				this._config.AutoDiscoverProperty.IdentifierTypeSpecified = IdentifierType.RoleName;
				this.ClearSecurityProperties();
				return;
			}
			if (this._config.SecurityProperties.SslEnabled)
			{
				this._config.AutoDiscoverProperty.UpdatePortsForSsl();
				if (!ConfigManager.IsTestingMode || this._config.SecurityProperties.SslSubjectIdentity == null)
				{
					this._config.SecurityProperties.SslSubjectIdentity = Utility.GetCertSubjectIdentity(this._config.AutoDiscoverProperty.Identifier);
				}
			}
			this._config.AutoDiscoverProperty.IdentifierTypeSpecified = IdentifierType.EndPoint;
			this._config.ServerList = this.AutoDiscoverServersOutsideDeployment();
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00011A22 File Offset: 0x0000FC22
		private void ClearSecurityProperties()
		{
			this._config.SecurityProperties = new DataCacheSecurity(DataCacheSecurityMode.None, DataCacheProtectionLevel.None);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00011A36 File Offset: 0x0000FC36
		private void AssignClientPerfCounterEvents()
		{
			this.LocalCacheInstanceDisposedEvent += ClientPerfCounterUpdate.OnLocalCacheInstanceDisposed;
			this.LocalCacheInstanceCreatedEvent += ClientPerfCounterUpdate.OnLocalCacheInstanceCreated;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00011A5C File Offset: 0x0000FC5C
		public DataCache GetCache(string cacheName)
		{
			return this.GetCache(cacheName, new CreateNewCacheDelegate(this.CreateNewCacheClient), null);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00011A74 File Offset: 0x0000FC74
		internal DataCache GetCache(string cacheName, CreateNewCacheDelegate cacheCreationDelegate, DataCacheInitializationViaCopyDelegate initializeDelegate)
		{
			if (cacheName == null)
			{
				throw new ArgumentNullException("cacheName");
			}
			DataCache dataCache = null;
			lock (this._lockObject)
			{
				if (this._myCache.ContainsKey(cacheName))
				{
					if (Provider.IsEnabled(TraceLevel.Verbose))
					{
						EventLogWriter.WriteVerbose<string>(DataCacheFactory._myComponentName, "GetCache: Named Cache '{0}' already exists, returning instance", cacheName);
					}
					dataCache = (DataCache)this._myCache[cacheName];
					if (initializeDelegate == null)
					{
						return dataCache;
					}
					return initializeDelegate(dataCache);
				}
				else
				{
					if (Provider.IsEnabled(TraceLevel.Verbose))
					{
						EventLogWriter.WriteVerbose<string>(DataCacheFactory._myComponentName, "GetCache: Creating the Named Cache '{0}'", cacheName);
					}
					SimpleSendReceiveModule sendRcvModule = this._sendRcvModule;
					IClientProtocol clientProtocol;
					if (this._config.UseLegacyProtocol)
					{
						clientProtocol = new WcfClientProtocol(cacheName, sendRcvModule, this.Configuration);
					}
					else
					{
						clientProtocol = new SocketClientProtocol(cacheName, new VelocityWireProtocol(), sendRcvModule, this.Configuration);
					}
					CacheServerProperties cacheServerProperties = clientProtocol.Initialize(DataCacheFactory.GetEndpoints(this.Configuration.ServerList));
					DataCacheDeploymentMode value = cacheServerProperties.CacheConfiguration.DeploymentMode.Value;
					this._config.DeploymentMode = value;
					this.InitializeDeploymentSpecificStateIfRequired(value, cacheServerProperties.CacheConfiguration);
					dataCache = this.CreateNewCacheClient(value, cacheName, cacheServerProperties.CacheConfiguration, clientProtocol, cacheServerProperties.InitialLookupTable, sendRcvModule, cacheCreationDelegate);
					this._myCache[cacheName] = dataCache;
				}
			}
			return dataCache;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00011BE0 File Offset: 0x0000FDE0
		private void InitializeDeploymentSpecificStateIfRequired(DataCacheDeploymentMode deploymentMode, NamedCacheConfiguration cacheConfiguration)
		{
			lock (this._lockObject)
			{
				if (this._notificationManager == null)
				{
					switch (deploymentMode)
					{
					case DataCacheDeploymentMode.SimpleClient:
						return;
					case DataCacheDeploymentMode.RoutingClient:
						this.DrmUtility = new RoutingClientDrmUtility(this._identifier.ToString(NumberFormatInfo.InvariantInfo));
						break;
					case DataCacheDeploymentMode.HybridClient:
					case DataCacheDeploymentMode.DIPClient:
						this.DrmUtility = new HybridClientDrmutility();
						break;
					}
					this._notificationManager = new NotificationManager(this._config.NotificationProperties, (int)this._config.RequestTimeout.TotalMilliseconds, this.DrmUtility, cacheConfiguration, this._cacheProtocolTable);
					this._notificationManager.Initialize();
				}
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00011CB0 File Offset: 0x0000FEB0
		private DataCache CreateNewCacheClient(DataCacheDeploymentMode mode, string cacheName, NamedCacheConfiguration config, IClientProtocol protocol, CacheLookupTableTransfer initialLookupTable, SimpleSendReceiveModule tempModule, CreateNewCacheDelegate cacheCreationDelegate)
		{
			DataCache dataCache = null;
			if (mode == DataCacheDeploymentMode.RoutingClient)
			{
				dataCache = this.CreateRoutingClient(cacheName, config, protocol, initialLookupTable, tempModule, cacheCreationDelegate);
			}
			else
			{
				if (!"default".Equals(cacheName, StringComparison.OrdinalIgnoreCase) && !ConfigManager.IsTestingMode)
				{
					DataCacheFactory.ThrowNotSupportedException();
				}
				if (mode == DataCacheDeploymentMode.SimpleClient)
				{
					dataCache = this.CreateSimpleClient(cacheName, config, protocol, tempModule, cacheCreationDelegate);
				}
				else if (mode == DataCacheDeploymentMode.DIPClient || mode == DataCacheDeploymentMode.HybridClient)
				{
					dataCache = this.CreateHybridClient(cacheName, config, protocol, initialLookupTable, tempModule, cacheCreationDelegate);
				}
			}
			return dataCache;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00011D20 File Offset: 0x0000FF20
		private DataCache CreateHybridClient(string cacheName, NamedCacheConfiguration config, IClientProtocol protocol, CacheLookupTableTransfer initialLookupTable, SimpleSendReceiveModule sendRcvModule, CreateNewCacheDelegate cacheCreationDelegate)
		{
			EndpointID endpointID = new EndpointID(this._config.ServerList[0].UriString);
			sendRcvModule.EnableVasRoutingChannel(endpointID, config.GetPartitionCount(32), config.DeploymentMode.Value);
			HybridClientStrategy hybridClientStrategy = new HybridClientStrategy(endpointID, sendRcvModule, this.Configuration, protocol, initialLookupTable);
			protocol.SetRoutingStrategy(hybridClientStrategy);
			if (!ConfigManager.IsTestingMode)
			{
				config.Name = "default";
			}
			HybridClientDrmutility hybridClientDrmutility = (HybridClientDrmutility)this.DrmUtility;
			hybridClientDrmutility.Add(config.Name, hybridClientStrategy.RoutingManager);
			if (config.Notification.IsEnabled)
			{
				this._cacheProtocolTable.Add(config.Name, protocol);
			}
			bool flag = false;
			DataCache dataCache2;
			try
			{
				DataCache dataCache = cacheCreationDelegate(cacheName, protocol, this, config, ClientOperationsSupportProvider.VAS);
				flag = true;
				dataCache2 = dataCache;
			}
			finally
			{
				if (!flag)
				{
					hybridClientDrmutility.Remove(config.Name);
					if (config.Notification.IsEnabled)
					{
						this._cacheProtocolTable.Remove(config.Name);
					}
				}
			}
			return dataCache2;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00011E28 File Offset: 0x00010028
		private DataCache CreateSimpleClient(string cacheName, NamedCacheConfiguration config, IClientProtocol protocol, SimpleSendReceiveModule sendRcvModule, CreateNewCacheDelegate cacheCreationDelegate)
		{
			SimpleClientStrategy simpleClientStrategy = new SimpleClientStrategy(new EndpointID(this._config.ServerList[0].UriString), sendRcvModule, this.Configuration);
			protocol.SetRoutingStrategy(simpleClientStrategy);
			DataCache dataCache = cacheCreationDelegate(cacheName, protocol, this, config, ClientOperationsSupportProvider.VAS);
			if (!ConfigManager.IsTestingMode)
			{
				config.Name = "default";
			}
			return dataCache;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00011E8C File Offset: 0x0001008C
		private DataCache CreateRoutingClient(string cacheName, NamedCacheConfiguration config, IClientProtocol protocol, CacheLookupTableTransfer initialLookupTable, SimpleSendReceiveModule sendRcvModule, CreateNewCacheDelegate cacheCreationDelegate)
		{
			ClientDRM newClientDrm = this.GetNewClientDrm(cacheName, sendRcvModule, protocol, initialLookupTable);
			RoutingClientStrategy routingClientStrategy = new RoutingClientStrategy(cacheName, this.Configuration, newClientDrm);
			protocol.SetRoutingStrategy(routingClientStrategy);
			RoutingClientDrmUtility routingClientDrmUtility = (RoutingClientDrmUtility)this.DrmUtility;
			routingClientDrmUtility.AddCacheDRM(cacheName, newClientDrm);
			if (config.Notification.IsEnabled)
			{
				this._cacheProtocolTable.Add(cacheName, protocol);
			}
			bool flag = false;
			DataCache dataCache2;
			try
			{
				DataCache dataCache = cacheCreationDelegate(cacheName, protocol, this, config, ClientOperationsSupportProvider.OnPremise);
				flag = true;
				dataCache2 = dataCache;
			}
			finally
			{
				if (!flag)
				{
					routingClientDrmUtility.RemoveCacheDRM(cacheName);
					if (config.Notification.IsEnabled)
					{
						this._cacheProtocolTable.Remove(cacheName);
					}
				}
			}
			return dataCache2;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00011F3C File Offset: 0x0001013C
		private DataCache CreateNewCacheClient(string name, IClientProtocol protocolToUse, DataCacheFactory parentFactory, NamedCacheConfiguration cacheConfiguration, ClientOperationsSupportProvider supportProvider)
		{
			return new DataCache(name, protocolToUse, parentFactory, cacheConfiguration, supportProvider);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00011F4C File Offset: 0x0001014C
		public DataCache GetDefaultCache()
		{
			string text = "default";
			return this.GetCache(text);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00011F68 File Offset: 0x00010168
		private ClientDRM GetNewClientDrm(string cacheName, SimpleSendReceiveModule tempModule, IClientProtocol protocol, CacheLookupTableTransfer initialLookupTable)
		{
			if (IOCompletionPortWorkQueue.NormalPriorityWorkQueue.WORKER_THREAD_IDLE_WAIT_TIME != DataCacheFactory.WORKER_THREAD_IDLE_WAIT_TIME)
			{
				IOCompletionPortWorkQueue.NormalPriorityWorkQueue.WORKER_THREAD_IDLE_WAIT_TIME = DataCacheFactory.WORKER_THREAD_IDLE_WAIT_TIME;
				IOCompletionPortWorkQueue.HighPriorityWorkQueue.WORKER_THREAD_IDLE_WAIT_TIME = DataCacheFactory.WORKER_THREAD_IDLE_WAIT_TIME;
			}
			return new ClientDRM(cacheName, DataCacheFactory.GetEndpoints(this._config.ServerList), "http://schemas.microsoft.com/velocity/msgs/DOMRequest", this._identifier.ToString(NumberFormatInfo.InvariantInfo), this._config.ChannelOpenTimeout, tempModule, protocol, initialLookupTable);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00011FE0 File Offset: 0x000101E0
		private ReadOnlyCollection<DataCacheServerEndpoint> AutoDiscoverServersOutsideDeployment()
		{
			if (string.IsNullOrEmpty(this._config.AutoDiscoverProperty.Identifier))
			{
				throw new ArgumentNullException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "IdentifierNotSpecified"));
			}
			this._config.UseLegacyProtocol = false;
			DataCacheServerEndpoint dataCacheServerEndpoint = new DataCacheServerEndpoint(this._config.AutoDiscoverProperty.Identifier, this._config.AutoDiscoverProperty.DiscoveryPort);
			if (!dataCacheServerEndpoint.IsServerEndpointWellFormed())
			{
				throw new DataCacheException(DataCacheFactory._myComponentName, 42, string.Format(CultureInfo.InvariantCulture, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "ERRCA0042"), new object[] { dataCacheServerEndpoint.HostName }));
			}
			return new ReadOnlyCollection<DataCacheServerEndpoint>(new List<DataCacheServerEndpoint> { dataCacheServerEndpoint });
		}

		// Token: 0x06000378 RID: 888 RVA: 0x000120A0 File Offset: 0x000102A0
		private bool TryAutoDiscoverServersWithinDeployment()
		{
			bool flag;
			try
			{
				this.AutoDiscoverServersWithinDeployment();
				flag = true;
			}
			catch (TargetInvocationException ex)
			{
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteInfo(DataCacheFactory._myComponentName, "TryAutoDiscoverServersWithinDeployment for Instance '{0}' failed to connect as RoleName type with exception {1} \n Assuming it as EndPoint.", new object[]
					{
						this._config.AutoDiscoverProperty.Identifier,
						ex.ToString()
					});
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0001210C File Offset: 0x0001030C
		private ReadOnlyCollection<DataCacheServerEndpoint> AutoDiscoverServersWithinDeployment()
		{
			Type type = Utility.LoadAzureClientAssembly();
			MethodInfo method = type.GetMethod("GetCacheRoleIPList");
			Dictionary<string, int> dictionary = (Dictionary<string, int>)method.Invoke(null, new object[]
			{
				this._config.AutoDiscoverProperty.Identifier,
				"cacheSocketPort"
			});
			if (dictionary.Count == 0)
			{
				dictionary = (Dictionary<string, int>)method.Invoke(null, new object[]
				{
					this._config.AutoDiscoverProperty.Identifier,
					"cacheServicePortInternal"
				});
				this._config.UseLegacyProtocol = true;
			}
			else
			{
				this._config.UseLegacyProtocol = false;
			}
			if (dictionary.Count == 0)
			{
				throw new InvalidOperationException("No Endpoints found");
			}
			List<DataCacheServerEndpoint> list = new List<DataCacheServerEndpoint>();
			foreach (KeyValuePair<string, int> keyValuePair in dictionary)
			{
				list.Add(new DataCacheServerEndpoint(keyValuePair.Key, keyValuePair.Value));
			}
			this._config.ServerList = new ReadOnlyCollection<DataCacheServerEndpoint>(list);
			return this._config.ServerList;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00012240 File Offset: 0x00010440
		private static string[] GetEndpoints(ReadOnlyCollection<DataCacheServerEndpoint> servers)
		{
			string[] array = new string[servers.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = servers[i].UriString;
			}
			return array;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00012277 File Offset: 0x00010477
		internal static void ThrowNotSupportedException()
		{
			throw new NotSupportedException(DataCache.GetErrorMsg(22));
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00012288 File Offset: 0x00010488
		internal static ResponseBody EstablishConnection(IEnumerable<string> servers, RequestBody request, Func<EndpointID, RequestBody, ResponseBody> sendMessageDelegate, DataCacheReadyRetryPolicy retryPolicy)
		{
			ResponseBody responseBody = null;
			int num = 0;
			do
			{
				foreach (string text in servers)
				{
					try
					{
						if (Provider.IsEnabled(TraceLevel.Verbose))
						{
							EventLogWriter.WriteVerbose<Guid, string, string>(DataCacheFactory._myComponentName, "{0}:Getting properties for cache {1} using Host - {2}", request.RequestTrackingId, request.CacheName, text);
						}
						EndpointID endpointID = new EndpointID(text);
						request.Destination = endpointID;
						responseBody = sendMessageDelegate(endpointID, request);
						if (responseBody.Ack == AckNack.Nack && responseBody.ResponseCode == ErrStatus.CACHE_REDIRECTED)
						{
							endpointID = (request.Destination = new EndpointID(responseBody.RedirectUri.ToString()));
							responseBody = sendMessageDelegate(endpointID, request);
						}
					}
					catch (DataCacheException ex)
					{
						if (Provider.IsEnabled(TraceLevel.Verbose))
						{
							EventLogWriter.WriteError(DataCacheFactory._myComponentName, "{0}:Get cache properties failed : {1}", new object[]
							{
								request.RequestTrackingId,
								ex.ToString()
							});
						}
						if (!ex.IsRetryable() || num >= retryPolicy.RetryCount)
						{
							throw;
						}
					}
					if (responseBody != null)
					{
						if (responseBody.Ack == AckNack.Ack)
						{
							return responseBody;
						}
						if (responseBody.Ack == AckNack.Nack && (responseBody.ResponseCode == ErrStatus.CLIENT_SERVER_VERSION_MISMATCH || responseBody.ResponseCode == ErrStatus.MESSAGE_LARGER_THAN_CONFIGURED || responseBody.ResponseCode == ErrStatus.AUTH_HEADER_INVALID))
						{
							DataCache.ThrowException(responseBody, request.Destination);
						}
					}
				}
				num++;
				int num2 = Math.Min(retryPolicy.MaximumRetryIntervalInSeconds, num);
				Thread.Sleep(num2 * 1000);
			}
			while (num <= retryPolicy.RetryCount);
			if (responseBody != null && responseBody.Ack == AckNack.Nack)
			{
				DataCache.ThrowException(responseBody, request.Destination);
			}
			return responseBody;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00012454 File Offset: 0x00010654
		internal bool Close()
		{
			bool flag = true;
			if (!this._isClosed)
			{
				DataCacheFactoryConfigurationSynchronizer.SynchronizerInstance.UnregisterFactory(this);
			}
			lock (this._lockObject)
			{
				if (this._isClosed)
				{
					return true;
				}
				this._isClosed = true;
				if (this._notificationManager != null)
				{
					this._notificationManager.Dispose();
					this._notificationManager = null;
				}
				if (this._myCache != null)
				{
					this._myCache.Clear();
					this._myCache = null;
				}
				if (this._localCacheStore != null)
				{
					if (this.LocalCacheInstanceDisposedEvent != null)
					{
						this.LocalCacheInstanceDisposedEvent(this._localCacheStore.CountLocalCacheItem, this._config.LocalCacheProperties.ObjectCount);
					}
					this._localCacheStore.Dispose();
				}
				if (this.DrmUtility != null)
				{
					this.DrmUtility.Dispose();
				}
				if (this._sendRcvModule != null)
				{
					if (this._config.UseConnectionPool)
					{
						DataCacheFactory._connectionPool.ReturnItemToPool(this._config.Name);
					}
					else
					{
						this._sendRcvModule.Dispose();
					}
				}
			}
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<bool>(DataCacheFactory._myComponentName + ".Close", "Closed: status {0}", flag);
			}
			return flag;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x000125A0 File Offset: 0x000107A0
		public void Dispose()
		{
			this.Close();
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x000125B0 File Offset: 0x000107B0
		~DataCacheFactory()
		{
			this.Close();
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000380 RID: 896 RVA: 0x000125E0 File Offset: 0x000107E0
		internal NotificationManager NotificationManagerInstance
		{
			get
			{
				return this._notificationManager;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000381 RID: 897 RVA: 0x000125E8 File Offset: 0x000107E8
		internal LocalCacheStore LocalCacheInstance
		{
			get
			{
				return this._localCacheStore;
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000382 RID: 898 RVA: 0x000125F0 File Offset: 0x000107F0
		// (remove) Token: 0x06000383 RID: 899 RVA: 0x00012628 File Offset: 0x00010828
		internal event Action<long, long> LocalCacheInstanceDisposedEvent;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000384 RID: 900 RVA: 0x00012660 File Offset: 0x00010860
		// (remove) Token: 0x06000385 RID: 901 RVA: 0x00012698 File Offset: 0x00010898
		internal event Action<long> LocalCacheInstanceCreatedEvent;

		// Token: 0x040002AD RID: 685
		private const string HelperMethodName = "GetCacheRoleIPList";

		// Token: 0x040002AE RID: 686
		private static SimpleSendReceiveModulePool _connectionPool = new SimpleSendReceiveModulePool();

		// Token: 0x040002AF RID: 687
		internal static ConcurrentDictionary<string, DataCacheFactory> SharedFactoryInstances = new ConcurrentDictionary<string, DataCacheFactory>();

		// Token: 0x040002B0 RID: 688
		internal static bool FactoryInstanceInitialized;

		// Token: 0x040002B1 RID: 689
		private object _lockObject = new object();

		// Token: 0x040002B2 RID: 690
		private bool _isClosed;

		// Token: 0x040002B3 RID: 691
		private DataCacheFactoryConfiguration _config;

		// Token: 0x040002B4 RID: 692
		private Hashtable _myCache;

		// Token: 0x040002B5 RID: 693
		private static string _myComponentName = "DistributedCache.CacheFactory";

		// Token: 0x040002B6 RID: 694
		private NotificationManager _notificationManager;

		// Token: 0x040002B7 RID: 695
		private Hashtable _cacheProtocolTable = new Hashtable();

		// Token: 0x040002B8 RID: 696
		private LocalCacheStore _localCacheStore;

		// Token: 0x040002B9 RID: 697
		private static int _instanceCounter;

		// Token: 0x040002BA RID: 698
		private int _identifier;

		// Token: 0x040002BB RID: 699
		private SimpleSendReceiveModule _sendRcvModule;

		// Token: 0x040002BC RID: 700
		private IEndpointIdentityProvider _clientIdentityProvider;

		// Token: 0x040002BD RID: 701
		internal static int WORKER_THREAD_IDLE_WAIT_TIME = 3000;
	}
}
