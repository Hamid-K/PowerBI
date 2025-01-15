using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.ConfigurationClasses.Communication;
using Microsoft.Cloud.Platform.ConfigurationManagement;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.MonitoredUtils;
using Microsoft.Cloud.Platform.Security;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004DD RID: 1245
	[BlockServiceProvider(typeof(ICommunicationServices))]
	[CannotApplyEqualityOperator]
	public class CommunicationServicesBlock : Block, ICommunicationServices, ICertificateProvider
	{
		// Token: 0x060025C8 RID: 9672 RVA: 0x00085FCD File Offset: 0x000841CD
		public CommunicationServicesBlock()
			: base(typeof(CommunicationServicesBlock).Name)
		{
			this.m_proxyCache = new Dictionary<CommunicationServicesBlock.ProxyId, object>();
			this.m_locker = new object();
		}

		// Token: 0x060025C9 RID: 9673 RVA: 0x00085FFC File Offset: 0x000841FC
		protected override BlockInitializationStatus OnInitialize()
		{
			if (base.OnInitialize() == BlockInitializationStatus.PartiallyDone)
			{
				return BlockInitializationStatus.PartiallyDone;
			}
			this.m_configurationManager = this.m_configurationManagerFactory.GetConfigurationManager();
			this.m_communicationServices = new CommunicationServices(this.m_eventsKitFactory, this.m_activityFactory, this.m_elementInstanceId.GetElementInstanceId(), this, this.m_monitoredActivityCompletionModelFactory);
			this.m_configurationManager.Subscribe(new List<Type> { typeof(CommunicationConfiguration) }, new CcsEventHandler(this.OnConfigChange));
			IEnumerable<IConsumerServiceConfiguration> enumerable = this.m_consumedServices.Values.Where((IConsumerServiceConfiguration cer) => !string.IsNullOrEmpty(cer.ClientCertificateData.ClientCertificateKey));
			if (enumerable.Any<IConsumerServiceConfiguration>())
			{
				this.m_secretManager.Subscribe(enumerable.Select((IConsumerServiceConfiguration cer) => cer.ClientCertificateData.ClientCertificateKey).Distinct<string>(), new SecretManagerEventHandler(this.OnCertificateChange));
			}
			this.m_eventsKit = this.m_eventsKitFactory.CreateEventsKit<ICommunicationFrameworkEventsKit>();
			return BlockInitializationStatus.Done;
		}

		// Token: 0x060025CA RID: 9674 RVA: 0x00086108 File Offset: 0x00084308
		protected override void OnStop()
		{
			this.m_configurationManager.Unsubscribe(new CcsEventHandler(this.OnConfigChange));
			if (this.m_consumedServices.Values.Any((IConsumerServiceConfiguration cer) => !string.IsNullOrEmpty(cer.ClientCertificateData.ClientCertificateKey)))
			{
				this.m_secretManager.Unsubscribe(new SecretManagerEventHandler(this.OnCertificateChange));
			}
			base.OnStop();
		}

		// Token: 0x060025CB RID: 9675 RVA: 0x0008617A File Offset: 0x0008437A
		protected override void OnShutdown()
		{
			if (this.m_communicationServices != null)
			{
				this.m_communicationServices.Dispose();
				this.m_communicationServices = null;
			}
			this.m_proxyCache.Clear();
			base.OnShutdown();
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x000861A8 File Offset: 0x000843A8
		public T GetService<T>(ServiceIdentification serviceId, Router router, bool routerIdentifiable, OnProxyCreating onProxyCreating) where T : class
		{
			object locker = this.m_locker;
			T t;
			lock (locker)
			{
				CommunicationServicesBlock.ProxyId proxyId;
				if (routerIdentifiable)
				{
					proxyId = new CommunicationServicesBlock.RouterProxyId(serviceId.Name, router, typeof(T));
				}
				else
				{
					proxyId = new CommunicationServicesBlock.ProxyId(serviceId.Name, typeof(T));
				}
				object obj;
				if (this.m_proxyCache.TryGetValue(proxyId, out obj))
				{
					t = obj as T;
				}
				else
				{
					IConsumerServiceConfiguration consumerServiceConfiguration = null;
					if (!this.m_consumedServices.TryGetValue(serviceId.Name, out consumerServiceConfiguration))
					{
						CommunicationFrameworkArgumentException ex = new CommunicationFrameworkArgumentException("ServiceId '{0}' does not exist in configuration as a service which can be consumed".FormatWithInvariantCulture(new object[] { serviceId.Name }));
						this.m_eventsKit.NotifyInvalidConfiguration(serviceId.Name, "Consumer", ex);
						throw ex;
					}
					T service = this.m_communicationServices.GetService<T>(consumerServiceConfiguration, router, routerIdentifiable, onProxyCreating);
					this.m_proxyCache.Add(proxyId, service);
					t = service;
				}
			}
			return t;
		}

		// Token: 0x060025CD RID: 9677 RVA: 0x000862B8 File Offset: 0x000844B8
		public T GetService<T>(ServiceIdentification serviceId, Router router, OnProxyCreating onProxyCreating) where T : class
		{
			return this.GetService<T>(serviceId, router, false, onProxyCreating);
		}

		// Token: 0x060025CE RID: 9678 RVA: 0x000862C4 File Offset: 0x000844C4
		public T GetService<T>(ServiceIdentification serviceId, RouterIdentifier routerIdentifier, OnProxyCreating onProxyCreating) where T : class
		{
			return this.GetService<T>(serviceId, this.m_routerFactory.Create(routerIdentifier), onProxyCreating);
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x000862DA File Offset: 0x000844DA
		public void PublishService(ServiceIdentification serviceId, object singletonInstance, OnServiceCreating onServiceCreating)
		{
			this.PublishService(serviceId, singletonInstance, new DefaultEndpointProvider(), onServiceCreating);
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x000862EC File Offset: 0x000844EC
		public void PublishService(ServiceIdentification serviceId, object singletonInstance, IEndpointProvider endpointProvider, OnServiceCreating onServiceCreating)
		{
			IProviderServiceConfiguration providerServiceConfiguration = null;
			object locker = this.m_locker;
			lock (locker)
			{
				if (!this.m_publishedServices.TryGetValue(serviceId.Name, out providerServiceConfiguration))
				{
					CommunicationFrameworkArgumentException ex = new CommunicationFrameworkArgumentException("ServiceId '{0}' does not exist in configuration as a service which can be published".FormatWithInvariantCulture(new object[] { serviceId.Name }));
					this.m_eventsKit.NotifyInvalidConfiguration(serviceId.Name, "Provider", ex);
					throw ex;
				}
			}
			this.m_communicationServices.PublishService(providerServiceConfiguration, singletonInstance, endpointProvider, onServiceCreating);
		}

		// Token: 0x060025D1 RID: 9681 RVA: 0x00086388 File Offset: 0x00084588
		public ClientCertificateData GetCertificateData(string certificateKey)
		{
			object locker = this.m_locker;
			ClientCertificateData clientCertificateData;
			lock (locker)
			{
				clientCertificateData = (from cer in this.m_consumedServices.Values
					where certificateKey.Equals(cer.ClientCertificateData.ClientCertificateKey, StringComparison.OrdinalIgnoreCase)
					select cer.ClientCertificateData).FirstOrDefault<ClientCertificateData>();
			}
			return clientCertificateData;
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x00086418 File Offset: 0x00084618
		private static IEnumerable<Type> GetWellKnownExceptions()
		{
			return new Type[]
			{
				typeof(ShutdownSequenceStartedException),
				typeof(CommunicationFrameworkException)
			};
		}

		// Token: 0x060025D3 RID: 9683 RVA: 0x0008643C File Offset: 0x0008463C
		private void OnConfigChange(IConfigurationContainer configurationContainer)
		{
			CommunicationConfiguration configuration = configurationContainer.GetConfiguration<CommunicationConfiguration>();
			Dictionary<string, IProviderServiceConfiguration> dictionary = new Dictionary<string, IProviderServiceConfiguration>();
			Dictionary<string, IConsumerServiceConfiguration> dictionary2 = new Dictionary<string, IConsumerServiceConfiguration>();
			Dictionary<string, IEnumerable<Type>> dictionary3 = new Dictionary<string, IEnumerable<Type>>();
			List<Type> list = new List<Type>(CommunicationServicesBlock.GetWellKnownExceptions());
			if (configuration.KnownExceptions != null)
			{
				list.AddRange(configuration.KnownExceptions.Select((TypeIdentifier type) => CommunicationUtilities.GetKnownType(type)));
			}
			if (configuration.ContractKnownTypes != null)
			{
				foreach (ContractKnownTypesConfiguration contractKnownTypesConfiguration in configuration.ContractKnownTypes)
				{
					Type[] array = contractKnownTypesConfiguration.KnownTypes.Select((TypeIdentifier item) => CommunicationUtilities.GetKnownType(item)).ToArray<Type>();
					dictionary3.Add(contractKnownTypesConfiguration.Contract, array);
				}
			}
			foreach (ServiceConfiguration serviceConfiguration in configuration.Services)
			{
				IEnumerable<Type> enumerable = null;
				if (serviceConfiguration.ProviderConfiguration != null)
				{
					TraceSourceBase<CommunicationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Info, "Adding service '{0}' to the published services list", new object[] { serviceConfiguration.Name });
					dictionary3.TryGetValue(serviceConfiguration.ProviderConfiguration.EndpointsConfiguration.First<EndpointConfiguration>().Contract, out enumerable);
					IEnumerable<EndpointInfo> enumerable2 = serviceConfiguration.ProviderConfiguration.EndpointsConfiguration.Select((EndpointConfiguration ep) => new EndpointInfo(ep.BindingType, ep.Port, ep.Contract)
					{
						SecurityMode = ep.BindingSecurityMode,
						MaxMessageSize = ep.MaxMessageSize,
						ReceiveTimeout = TimeSpan.FromMinutes(ep.ReceiveTimeout),
						SendTimeout = TimeSpan.FromMinutes(ep.SendTimeout),
						OpenTimeout = TimeSpan.FromMinutes(ep.OpenTimeout),
						CloseTimeout = TimeSpan.FromMinutes(ep.CloseTimeout),
						ReliableSessionEnabled = ep.ReliableSessionEnabled,
						ReliableSessionOrderedMessages = ep.ReliableSessionOrderedMessages,
						ClientCredentialType = ep.ClientCredentialType,
						MaxStringContentLength = ep.MaxStringContentLength,
						MaxArrayLength = ep.MaxArrayLength,
						TransferDataMode = ep.TransferDataMode,
						MaxBufferPoolSize = ep.MaxBufferPoolSize,
						MaxConnections = ep.MaxConnections
					});
					dictionary.Add(serviceConfiguration.Name, new ProviderServiceConfiguration(serviceConfiguration.Name, enumerable, list, enumerable2, serviceConfiguration.ProviderConfiguration.MaxConcurrentCalls, serviceConfiguration.ProviderConfiguration.MaxConcurrentSessions, serviceConfiguration.ProviderConfiguration.CrashServerOnNonContractualException, serviceConfiguration.ProviderConfiguration.DisableDefaultErrorHandler, serviceConfiguration.ProviderConfiguration.RequestInitializationTimeoutInSeconds, serviceConfiguration.ProviderConfiguration.MaxPendingAccepts));
				}
				if (serviceConfiguration.ConsumerConfiguration != null)
				{
					TraceSourceBase<CommunicationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Adding service '{0}' to the published services list", new object[] { serviceConfiguration.Name });
					dictionary3.TryGetValue(serviceConfiguration.ConsumerConfiguration.EndpointConfiguration.Contract, out enumerable);
					EndpointConfiguration endpointConfiguration = serviceConfiguration.ConsumerConfiguration.EndpointConfiguration;
					CertificateDataOptions certificateDataOptions = CertificateDataOptions.None;
					if (serviceConfiguration.ConsumerConfiguration.Certificate.VerifyServiceCertificateRevocation)
					{
						certificateDataOptions |= CertificateDataOptions.VerifyServiceCertificateRevocation;
					}
					if (serviceConfiguration.ConsumerConfiguration.Certificate.VerifyServiceCertificateName)
					{
						certificateDataOptions |= CertificateDataOptions.VerifyServiceCertificateName;
					}
					dictionary2.Add(serviceConfiguration.Name, new ConsumerServiceConfiguration(serviceConfiguration.Name, enumerable, list, new EndpointInfo(endpointConfiguration.BindingType, endpointConfiguration.Port, endpointConfiguration.Contract)
					{
						SecurityMode = endpointConfiguration.BindingSecurityMode,
						MaxMessageSize = endpointConfiguration.MaxMessageSize,
						ReceiveTimeout = TimeSpan.FromMinutes(endpointConfiguration.ReceiveTimeout),
						SendTimeout = TimeSpan.FromMinutes(endpointConfiguration.SendTimeout),
						OpenTimeout = TimeSpan.FromMinutes(endpointConfiguration.OpenTimeout),
						CloseTimeout = TimeSpan.FromMinutes(endpointConfiguration.CloseTimeout),
						OperationTimeout = TimeSpan.FromMinutes(endpointConfiguration.OperationTimeout),
						ReliableSessionEnabled = endpointConfiguration.ReliableSessionEnabled,
						ReliableSessionOrderedMessages = endpointConfiguration.ReliableSessionOrderedMessages,
						ClientCredentialType = endpointConfiguration.ClientCredentialType,
						MaxStringContentLength = endpointConfiguration.MaxStringContentLength,
						MaxArrayLength = endpointConfiguration.MaxArrayLength,
						TransferDataMode = endpointConfiguration.TransferDataMode,
						MaxBufferPoolSize = endpointConfiguration.MaxBufferPoolSize
					}, serviceConfiguration.ConsumerConfiguration.AllowImpersonation, new ClientCertificateData(null, serviceConfiguration.ConsumerConfiguration.Certificate.ClientCertificateKey, certificateDataOptions), serviceConfiguration.ConsumerConfiguration.UseDoubleWrap, serviceConfiguration.ConsumerConfiguration.TraceHttpResponseHeadersOnFaultException));
				}
			}
			object locker = this.m_locker;
			lock (locker)
			{
				this.m_publishedServices = dictionary;
				this.m_consumedServices = dictionary2;
			}
		}

		// Token: 0x060025D4 RID: 9684 RVA: 0x00086884 File Offset: 0x00084A84
		private void OnCertificateChange(ICertificatesContainer certificatesContainer)
		{
			object locker = this.m_locker;
			lock (locker)
			{
				foreach (IConsumerServiceConfiguration consumerServiceConfiguration in this.m_consumedServices.Values)
				{
					if (consumerServiceConfiguration.ClientCertificateData.ClientCertificateKey != null)
					{
						X509Certificate2 primaryCertificate = certificatesContainer.GetPrimaryCertificate(consumerServiceConfiguration.ClientCertificateData.ClientCertificateKey);
						if (!primaryCertificate.Equals(consumerServiceConfiguration.ClientCertificateData.ClientCertificate))
						{
							consumerServiceConfiguration.ClientCertificateData.ClientCertificate = primaryCertificate;
							this.m_communicationServices.OnCertificateChange(consumerServiceConfiguration.ServiceName);
						}
					}
				}
			}
		}

		// Token: 0x04000D61 RID: 3425
		[AutoShuttable]
		private CommunicationServices m_communicationServices;

		// Token: 0x04000D62 RID: 3426
		private Dictionary<string, IProviderServiceConfiguration> m_publishedServices;

		// Token: 0x04000D63 RID: 3427
		private Dictionary<string, IConsumerServiceConfiguration> m_consumedServices;

		// Token: 0x04000D64 RID: 3428
		private object m_locker;

		// Token: 0x04000D65 RID: 3429
		private Dictionary<CommunicationServicesBlock.ProxyId, object> m_proxyCache;

		// Token: 0x04000D66 RID: 3430
		private IConfigurationManager m_configurationManager;

		// Token: 0x04000D67 RID: 3431
		private ICommunicationFrameworkEventsKit m_eventsKit;

		// Token: 0x04000D68 RID: 3432
		[BlockServiceDependency]
		private readonly IRouterFactory m_routerFactory;

		// Token: 0x04000D69 RID: 3433
		[BlockServiceDependency]
		private readonly IEventsKitFactory m_eventsKitFactory;

		// Token: 0x04000D6A RID: 3434
		[BlockServiceDependency]
		private readonly IActivityFactory m_activityFactory;

		// Token: 0x04000D6B RID: 3435
		[BlockServiceDependency]
		private readonly IConfigurationManagerFactory m_configurationManagerFactory;

		// Token: 0x04000D6C RID: 3436
		[BlockServiceDependency]
		private readonly IMonitoredActivityCompletionModelFactory m_monitoredActivityCompletionModelFactory;

		// Token: 0x04000D6D RID: 3437
		[BlockServiceDependency]
		private readonly IElementInstanceId m_elementInstanceId;

		// Token: 0x04000D6E RID: 3438
		[BlockServiceDependency]
		private readonly ISecretManager m_secretManager;

		// Token: 0x0200083F RID: 2111
		private class ProxyId : IEquatable<CommunicationServicesBlock.ProxyId>
		{
			// Token: 0x06003303 RID: 13059 RVA: 0x000AAC6F File Offset: 0x000A8E6F
			public ProxyId(string service, Type contract)
			{
				this.m_service = service;
				this.m_contract = contract;
			}

			// Token: 0x06003304 RID: 13060 RVA: 0x000AAC85 File Offset: 0x000A8E85
			public virtual bool Equals(CommunicationServicesBlock.ProxyId other)
			{
				return other != null && object.Equals(this.m_service, other.m_service) && object.Equals(this.m_contract, other.m_contract);
			}

			// Token: 0x06003305 RID: 13061 RVA: 0x000AACB0 File Offset: 0x000A8EB0
			public override bool Equals(object obj)
			{
				return this.Equals(obj as CommunicationServicesBlock.ProxyId);
			}

			// Token: 0x06003306 RID: 13062 RVA: 0x000AACBE File Offset: 0x000A8EBE
			public override int GetHashCode()
			{
				return this.m_service.GetHashCode() ^ this.m_contract.GetHashCode();
			}

			// Token: 0x04001941 RID: 6465
			private readonly string m_service;

			// Token: 0x04001942 RID: 6466
			private readonly Type m_contract;
		}

		// Token: 0x02000840 RID: 2112
		private class RouterProxyId : CommunicationServicesBlock.ProxyId, IEquatable<CommunicationServicesBlock.RouterProxyId>
		{
			// Token: 0x06003307 RID: 13063 RVA: 0x000AACD7 File Offset: 0x000A8ED7
			public RouterProxyId(string service, IRouter router, Type contract)
				: base(service, contract)
			{
				this.m_router = router;
			}

			// Token: 0x06003308 RID: 13064 RVA: 0x000AACE8 File Offset: 0x000A8EE8
			public override bool Equals(CommunicationServicesBlock.ProxyId other)
			{
				return this.Equals(other as CommunicationServicesBlock.RouterProxyId);
			}

			// Token: 0x06003309 RID: 13065 RVA: 0x000AACF6 File Offset: 0x000A8EF6
			public bool Equals(CommunicationServicesBlock.RouterProxyId other)
			{
				return other != null && base.Equals(other) && object.Equals(this.m_router, other.m_router);
			}

			// Token: 0x0600330A RID: 13066 RVA: 0x000AACE8 File Offset: 0x000A8EE8
			public override bool Equals(object obj)
			{
				return this.Equals(obj as CommunicationServicesBlock.RouterProxyId);
			}

			// Token: 0x0600330B RID: 13067 RVA: 0x000AAD17 File Offset: 0x000A8F17
			public override int GetHashCode()
			{
				return base.GetHashCode() ^ this.m_router.GetHashCode();
			}

			// Token: 0x04001943 RID: 6467
			private readonly IRouter m_router;
		}
	}
}
