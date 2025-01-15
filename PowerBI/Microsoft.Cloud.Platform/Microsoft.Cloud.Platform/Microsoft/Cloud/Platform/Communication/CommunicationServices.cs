using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.MonitoredUtils;
using Microsoft.Cloud.Platform.RequestProtection;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x02000497 RID: 1175
	public class CommunicationServices : IExtendedCommunication, IShuttable, IDisposable
	{
		// Token: 0x06002432 RID: 9266 RVA: 0x00081ADA File Offset: 0x0007FCDA
		public CommunicationServices()
			: this(new VoidEventsKitFactory(), null, ElementId.None, new CommunicationServices.ConfigurationCertificateProvider(), null)
		{
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x00081AF4 File Offset: 0x0007FCF4
		public CommunicationServices(IEventsKitFactory eventsKitFactory, IActivityFactory activityFactory, ElementId elementId, ICertificateProvider certificateProvider, IMonitoredActivityCompletionModelFactory completionModelFactory)
		{
			this.m_lifetimeManagers = new Dictionary<string, object>();
			this.m_completionModelFactory = completionModelFactory;
			this.m_eventsKitFactory = eventsKitFactory;
			this.m_elementId = elementId;
			this.m_eventsKit = this.m_eventsKitFactory.CreateEventsKit<ICommunicationFrameworkEventsKit>();
			this.m_generatedProxyEventsKit = this.m_eventsKitFactory.CreateEventsKit<ICommunicationOperationProgressEventsKit>();
			this.m_activityFactory = activityFactory;
			this.m_serviceHosts = new List<ServiceHost>();
			this.m_locker = new object();
			this.m_workTicketManager = new WorkTicketManager("Communication Services");
			this.m_certificateProvider = certificateProvider;
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x00081B7F File Offset: 0x0007FD7F
		public T GetService<T>([NotNull] IConsumerServiceConfiguration serviceConfiguration, [NotNull] Router router, OnProxyCreating onProxyCreating) where T : class
		{
			return this.GetService<T>(serviceConfiguration, router, false, onProxyCreating);
		}

		// Token: 0x06002435 RID: 9269 RVA: 0x00081B8C File Offset: 0x0007FD8C
		public T GetService<T>([NotNull] IConsumerServiceConfiguration serviceConfiguration, [NotNull] Router router, bool routerIdentifiable, OnProxyCreating onProxyCreating) where T : class
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IConsumerServiceConfiguration>(serviceConfiguration, "serviceConfiguration");
			ExtendedDiagnostics.EnsureArgumentNotNull<Router>(router, "Router");
			string text = null;
			if (serviceConfiguration.ClientCertificateData != null)
			{
				text = serviceConfiguration.ClientCertificateData.ClientCertificateKey;
				CommunicationServices.ConfigurationCertificateProvider configurationCertificateProvider = this.m_certificateProvider as CommunicationServices.ConfigurationCertificateProvider;
				if (configurationCertificateProvider != null && !string.IsNullOrEmpty(text))
				{
					configurationCertificateProvider.SetCertificateData(serviceConfiguration.ClientCertificateData);
				}
			}
			ServiceDetails serviceDetails = null;
			if (routerIdentifiable)
			{
				serviceDetails = new ServiceDetails(typeof(T).Name, "{0}[{1}]".FormatWithInvariantCulture(new object[] { serviceConfiguration.ServiceName, router.Identifier }));
			}
			else
			{
				serviceDetails = new ServiceDetails(typeof(T).Name, serviceConfiguration.ServiceName);
			}
			router.Register(serviceDetails, this.m_workTicketManager);
			ProxyCreator proxyCreator = null;
			try
			{
				ProxyGenerationOptions proxyGenerationOptions = ProxyGenerationOptions.None;
				if (serviceConfiguration.TraceHttpResponseHeadersOnFaultException)
				{
					proxyGenerationOptions |= ProxyGenerationOptions.TraceHttpResponseHeadersOnFaultException;
				}
				proxyCreator = ProxyGenerator<T>.Generate(proxyGenerationOptions);
			}
			catch (CodeGenerationException ex)
			{
				CommunicationFrameworkProxyInitializationException ex2 = new CommunicationFrameworkProxyInitializationException("Can not generate proxy for service {0}".FormatWithInvariantCulture(new object[] { serviceConfiguration.ServiceName }), ex);
				this.m_eventsKit.NotifyProxyInitializationError(serviceConfiguration.ServiceName, ex2);
				throw ex2;
			}
			Type type4 = null;
			if (proxyCreator.ProxyNamespace != null)
			{
				TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceInformation("Proxy Namespace is {0}".FormatWithInvariantCulture(new object[] { proxyCreator.ProxyNamespace }));
				using (IEnumerator<Type> enumerator = (from t in proxyCreator.Assembly.GetTypes()
					where t.Namespace == proxyCreator.ProxyNamespace && t.IsInterface
					select t).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Type type2 = enumerator.Current;
						object[] customAttributes = type2.GetCustomAttributes(typeof(ECFGeneratedInterfaceAttribute), false);
						for (int i = 0; i < customAttributes.Length; i++)
						{
							if (((ECFGeneratedInterfaceAttribute)customAttributes[i]).OriginalInterface.Equals(typeof(T).FullName, StringComparison.Ordinal))
							{
								type4 = type2;
							}
						}
					}
					goto IL_0229;
				}
			}
			type4 = CommunicationUtilities.GetTypeWithAttribute<ECFGeneratedInterfaceAttribute>(proxyCreator.Assembly.GetTypes(), (ECFGeneratedInterfaceAttribute attribute, Type type) => attribute.OriginalInterface.Equals(typeof(T).FullName, StringComparison.Ordinal));
			IL_0229:
			Type type3 = type4 ?? typeof(T);
			TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceInformation("channel type is: {0}".FormatWithInvariantCulture(new object[] { type3.FullName }));
			object obj = DynamicLoader.Instantiate(typeof(ExtendedChannelOperations<>).MakeGenericType(new Type[] { type3 }), new Predicate<Type>(DynamicLoader.IsValidType), new object[]
			{
				serviceConfiguration.KnownTypes,
				serviceConfiguration.EndpointInfo,
				this.m_certificateProvider,
				text,
				CommunicationServices.IsInternal(typeof(T)),
				serviceConfiguration.UseDoubleWrap,
				serviceConfiguration.AllowImpersonation,
				this.m_eventsKit,
				this.m_activityFactory,
				onProxyCreating
			});
			object locker = this.m_locker;
			object obj2;
			lock (locker)
			{
				if (!this.m_lifetimeManagers.TryGetValue(serviceDetails.ServiceName, out obj2))
				{
					obj2 = DynamicLoader.Instantiate(typeof(CachedResourceLifetimeManager<, >).MakeGenericType(new Type[]
					{
						typeof(EndpointDifferentiator),
						type3
					}), new Predicate<Type>(DynamicLoader.IsValidType), new object[] { obj });
					this.m_lifetimeManagers.Add(serviceDetails.ServiceName, obj2);
				}
			}
			object obj3 = DynamicLoader.Instantiate(typeof(ChannelInvoker<>).MakeGenericType(new Type[] { type3 }), new Predicate<Type>(DynamicLoader.IsValidType), new object[] { serviceConfiguration.EndpointInfo.OperationTimeout });
			object obj4 = DynamicLoader.Instantiate(typeof(ProxyInvoker<>).MakeGenericType(new Type[] { type3 }), new Predicate<Type>(DynamicLoader.IsValidType), new object[] { obj2, router, this.m_generatedProxyEventsKit, this.m_workTicketManager, serviceDetails, obj3, this.m_elementId });
			T t2;
			try
			{
				t2 = proxyCreator.Create<T>(obj4);
			}
			catch (InvalidOperationException ex3)
			{
				CommunicationFrameworkProxyInitializationException ex4 = new CommunicationFrameworkProxyInitializationException("Generated proxy for service {0} can not be initialized".FormatWithInvariantCulture(new object[] { serviceConfiguration.ServiceName }), ex3);
				this.m_eventsKit.NotifyProxyInitializationError(serviceConfiguration.ServiceName, ex4);
				throw ex4;
			}
			return t2;
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x0008204C File Offset: 0x0008024C
		public void PublishService(IProviderServiceConfiguration serviceConfiguration, object singletonInstance, OnServiceCreating serviceHostExtensibilityDelegate)
		{
			this.PublishService(serviceConfiguration, singletonInstance, new DefaultEndpointProvider(), serviceHostExtensibilityDelegate);
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x0008205C File Offset: 0x0008025C
		public void PublishService(IProviderServiceConfiguration serviceConfiguration, object singletonInstance, IEndpointProvider endpointProvider, OnServiceCreating serviceHostExtensibilityDelegate)
		{
			List<Uri> list = new List<Uri>();
			ServiceHost serviceHost = new ServiceHost(singletonInstance, new Uri[0]);
			foreach (EndpointInfo endpointInfo in serviceConfiguration.EndpointsInformation)
			{
				Uri publishedEndpoint = endpointProvider.GetPublishedEndpoint(endpointInfo, serviceConfiguration.ServiceName);
				list.Add(publishedEndpoint);
				IBindingData bindingData = BindingFactory.CreateBinding(endpointInfo);
				ServiceEndpoint serviceEndpoint = null;
				try
				{
					serviceEndpoint = serviceHost.AddServiceEndpoint(endpointInfo.Contract, bindingData.Binding, publishedEndpoint);
				}
				catch (InvalidOperationException ex)
				{
					CommunicationFrameworkArgumentException ex2 = new CommunicationFrameworkArgumentException("Can not initialize endpoints. This must be caused by an error in the provided parameters:" + " Contract - '{0}', Binding - '{1}', Uri - {2}".FormatWithCurrentCulture(new object[] { endpointInfo.Contract, bindingData.Binding, publishedEndpoint }), ex);
					this.m_eventsKit.NotifyInvalidConfiguration(serviceConfiguration.ServiceName, "provider endpoint", ex2);
					throw ex2;
				}
				bindingData.AddBehaviors(serviceEndpoint);
				TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceInformation("EndPoint with address '{0}', binding '{1}' and contract '{2}' was added to the ServiceHost of serviceId '{3}'", new object[] { publishedEndpoint, endpointInfo.BindingType, endpointInfo.Contract, serviceConfiguration.ServiceName });
				if (this.m_activityFactory != null && CommunicationServices.IsInternal(endpointInfo.Contract, singletonInstance))
				{
					serviceEndpoint.Behaviors.Add(new AddContextToHeaderEndpointBehavior(this.m_activityFactory));
				}
				if (serviceConfiguration.CrashServerOnNonContractualException != NonContractualExceptionBehavior.NoCrash)
				{
					serviceEndpoint.Behaviors.Add(new CrashOnNonContractualExceptionEndpointBehavior(serviceConfiguration.KnownExceptions, serviceConfiguration.CrashServerOnNonContractualException, this.m_eventsKit));
				}
				IEnumerable<Type> knownTypes = serviceConfiguration.KnownTypes;
				if (knownTypes != null)
				{
					CommunicationUtilities.AddKnownTypesToEndPoint(serviceEndpoint, knownTypes);
				}
				CommunicationServices.UpdateHttpTransportElementParameters(serviceEndpoint, serviceConfiguration);
			}
			serviceHost.Description.Behaviors.Add(new ServiceErrorHandlerBehavior(serviceConfiguration.DisableDefaultErrorHandler));
			ServiceThrottlingBehavior serviceThrottlingBehavior = new ServiceThrottlingBehavior
			{
				MaxConcurrentCalls = serviceConfiguration.MaxConcurrentCalls
			};
			if (serviceConfiguration.MaxConcurrentSessions >= 0)
			{
				serviceThrottlingBehavior.MaxConcurrentSessions = serviceConfiguration.MaxConcurrentSessions;
			}
			serviceHost.Description.Behaviors.Add(serviceThrottlingBehavior);
			if (serviceHostExtensibilityDelegate != null)
			{
				ServiceExtender serviceExtender = new ServiceExtender(serviceHost);
				serviceHostExtensibilityDelegate(serviceExtender);
				EndpointActivity endpointActivity = serviceExtender.EndpointActivity;
				Func<IRequestProtectionContext, WorkTicket> createWorkTicketDelegate = serviceExtender.CreateWorkTicketDelegate;
				Action<IRequestProtectionContext> onIncomingRequest = serviceExtender.OnIncomingRequest;
				IPlatformActivityTracingPolicyEvaluator tracingEvaluator = serviceExtender.TracingEvaluator;
				ExtendedDiagnostics.EnsureOperation(tracingEvaluator == null || endpointActivity != null, "If TracingEvaluator is provided, an EndpointActivity should also be provided");
				ExtendedDiagnostics.EnsureOperation(endpointActivity == null || !CommunicationServices.IsInternal(serviceConfiguration.EndpointsInformation.First<EndpointInfo>().Contract, singletonInstance), "Endpoint activity should only be provided when the contract is external");
				ExtendedDiagnostics.EnsureOperation(endpointActivity == null || this.m_completionModelFactory != null, "If endpoint activity is provided, m_completionModelFactory should not be null");
				if (endpointActivity != null || createWorkTicketDelegate != null || onIncomingRequest != null || tracingEvaluator != null)
				{
					foreach (ServiceEndpoint serviceEndpoint2 in serviceHost.Description.Endpoints)
					{
						serviceEndpoint2.Behaviors.Add(new ExternalEndpointBehavior(endpointActivity, this.m_activityFactory, this.m_eventsKitFactory, onIncomingRequest, createWorkTicketDelegate, this.m_completionModelFactory, tracingEvaluator));
					}
				}
				IEndpointBehavior errorHandlingBehavior = serviceExtender.ErrorHandlingBehavior;
				if (errorHandlingBehavior != null)
				{
					foreach (ServiceEndpoint serviceEndpoint3 in serviceHost.Description.Endpoints)
					{
						serviceEndpoint3.Behaviors.Add(errorHandlingBehavior);
					}
				}
			}
			ExceptionHandler.AsynchronousThreadExceptionHandler = new CommunicationFrameworkExceptionHandler();
			if (serviceConfiguration.ServiceCertificateName != null)
			{
				TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceInformation("Setting on service '{0}' certificate with name '{1}' and store name 'My'", new object[] { serviceConfiguration.ServiceName, serviceConfiguration.ServiceCertificateName });
				serviceHost.Credentials.ServiceCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectDistinguishedName, serviceConfiguration.ServiceCertificateName);
				TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceVerbose("Successfully set certificate on service '{0}'", new object[] { serviceConfiguration.ServiceName });
			}
			try
			{
				UtilsContext.Current.RunWithClearContext(new Action(serviceHost.Open));
				TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceInformation("ServiceHost '{0}' for serviceId '{1}' was opened", new object[]
				{
					serviceHost.Description.ServiceType,
					serviceConfiguration.ServiceName
				});
			}
			catch (AddressAlreadyInUseException ex3)
			{
				this.AnalyzeConflictingProcesses(list, ex3);
				throw;
			}
			catch (InvalidOperationException ex4)
			{
				TraceSourceBase<CommunicationFrameworkTrace> tracer = TraceSourceBase<CommunicationFrameworkTrace>.Tracer;
				string text = "Unable to listen at one of the port(s) '{0}', Exception '{1}'";
				object[] array = new object[2];
				array[0] = string.Join<int>(",", list.Select((Uri r) => r.Port));
				array[1] = ex4;
				tracer.TraceError(text, array);
				throw;
			}
			object locker = this.m_locker;
			lock (locker)
			{
				this.m_serviceHosts.Add(serviceHost);
			}
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x0008258C File Offset: 0x0008078C
		private static void UpdateHttpTransportElementParameters(ServiceEndpoint serviceEndpoint, IProviderServiceConfiguration serviceConfiguration)
		{
			WebHttpBinding webHttpBinding = serviceEndpoint.Binding as WebHttpBinding;
			if (webHttpBinding != null)
			{
				Type type = webHttpBinding.GetType();
				foreach (string text in CommunicationServices.s_httpTransportElements)
				{
					FieldInfo field = type.GetField(text, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField);
					if (field != null)
					{
						CommunicationServices.UpdateHttpTransportElementParameters(field.GetValue(webHttpBinding) as HttpTransportBindingElement, serviceConfiguration);
					}
				}
			}
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x000825F4 File Offset: 0x000807F4
		private static void UpdateHttpTransportElementParameters(HttpTransportBindingElement transportElement, IProviderServiceConfiguration serviceConfiguration)
		{
			if (transportElement != null)
			{
				if (serviceConfiguration.RequestInitializationTimeout != TimeSpan.Zero)
				{
					PropertyInfo property = typeof(HttpTransportBindingElement).GetProperty("RequestInitializationTimeout");
					if (property == null)
					{
						TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceInformation("Skipped updating RequestInitializationTimeout since service is not running under .Net 4.5, though RequestInitializationTimeout was defined as '{0}'", new object[] { serviceConfiguration.RequestInitializationTimeout });
					}
					else
					{
						TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceInformation("RequestInitializationTimeout was updated to '{0}'", new object[] { serviceConfiguration.RequestInitializationTimeout });
						property.SetValue(transportElement, serviceConfiguration.RequestInitializationTimeout, null);
					}
				}
				if (serviceConfiguration.MaxPendingAccepts != 0)
				{
					PropertyInfo property2 = typeof(HttpTransportBindingElement).GetProperty("MaxPendingAccepts");
					if (property2 == null)
					{
						TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceInformation("Skipped updating maxPendingAccepts since service is not running under .Net 4.5, though MaxPendingAccepts was defined as '{0}'", new object[] { serviceConfiguration.MaxPendingAccepts });
						return;
					}
					TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceInformation("MaxPendingAccepts was updated to '{0}'", new object[] { serviceConfiguration.MaxPendingAccepts });
					property2.SetValue(transportElement, serviceConfiguration.MaxPendingAccepts, null);
				}
			}
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x00082714 File Offset: 0x00080914
		private void AnalyzeConflictingProcesses(IList<Uri> usedUrls, Exception e)
		{
			foreach (Uri uri in usedUrls)
			{
				int port = uri.Port;
				TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceError("Unable to listen at port '{0}', Exception '{1}'", new object[] { port, e });
				string allProcesses = CommunicationUtilities.GetAllProcesses();
				if (allProcesses != null)
				{
					TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceError("Active Processes '{0}'", new object[] { allProcesses });
				}
				else
				{
					TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceError("Could not get list of active processes");
				}
				string listeningProcessName = CommunicationUtilities.GetListeningProcessName(port);
				if (listeningProcessName != null)
				{
					TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceError("The port is owned by process '{0}'.", new object[] { listeningProcessName });
				}
				else
				{
					TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceError("Could not find the owner of the port (netstat output doesn't include the port)");
				}
			}
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x000827EC File Offset: 0x000809EC
		private static bool IsInternal(string endpointContract, object singletonInstance)
		{
			return CommunicationServices.IsInternal(singletonInstance.GetType().GetInterfaces().FirstOrDefault((Type c) => c.FullName.Equals(endpointContract, StringComparison.Ordinal)));
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x00082827 File Offset: 0x00080A27
		private static bool IsInternal(Type type)
		{
			return !(type.GetCustomAttributes(typeof(ECFContractAttribute), false).FirstOrDefault<object>() as ECFContractAttribute).IsExternal;
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x0008284C File Offset: 0x00080A4C
		private void CloseCommunicationServices()
		{
			object locker = this.m_locker;
			lock (locker)
			{
				foreach (ServiceHost serviceHost in this.m_serviceHosts)
				{
					foreach (ServiceEndpoint serviceEndpoint in serviceHost.Description.Endpoints)
					{
						TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceInformation("Attempting to close host '{0}' with contract '{1}' with address '{2}'", new object[]
						{
							serviceHost.Description.ServiceType,
							serviceEndpoint.Contract.Name,
							serviceEndpoint.Address
						});
						try
						{
							serviceHost.Close();
						}
						catch (CommunicationException ex)
						{
							TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceWarning("Closing host '{0}' with contract '{1}' with address '{2}' failed on exception '{3}'", new object[]
							{
								serviceHost.Description.ServiceType,
								serviceEndpoint.Contract.Name,
								serviceEndpoint.Address,
								ex
							});
							serviceHost.Abort();
						}
						catch (TimeoutException ex2)
						{
							TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceWarning("Closing host '{0}' with contract '{1}' with address '{2}' failed on exception '{3}'", new object[]
							{
								serviceHost.Description.ServiceType,
								serviceEndpoint.Contract.Name,
								serviceEndpoint.Address,
								ex2
							});
							serviceHost.Abort();
						}
					}
				}
				this.m_serviceHosts.Clear();
				foreach (IDisposable disposable in this.m_lifetimeManagers.Values.Cast<IDisposable>())
				{
					disposable.Dispose();
				}
				this.m_lifetimeManagers.Clear();
			}
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x00082AA4 File Offset: 0x00080CA4
		public void OnCertificateChange(string serviceName)
		{
			object locker = this.m_locker;
			object obj;
			lock (locker)
			{
				this.m_lifetimeManagers.TryGetValue(serviceName, out obj);
			}
			if (obj != null)
			{
				(obj as IResourceLifetimeManager).EvictAll();
			}
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x00082AFC File Offset: 0x00080CFC
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x00082B05 File Offset: 0x00080D05
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Stop();
				this.WaitForStopToComplete();
				this.Shutdown();
			}
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x00082B1C File Offset: 0x00080D1C
		public void Stop()
		{
			if (this.m_workTicketManager != null)
			{
				this.m_workTicketManager.Stop();
			}
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x00082B31 File Offset: 0x00080D31
		public void WaitForStopToComplete()
		{
			if (this.m_workTicketManager != null)
			{
				this.m_workTicketManager.WaitForStopToComplete();
			}
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x00082B46 File Offset: 0x00080D46
		public void Shutdown()
		{
			if (this.m_workTicketManager != null)
			{
				this.m_workTicketManager.Shutdown();
			}
			this.m_workTicketManager = null;
			this.CloseCommunicationServices();
		}

		// Token: 0x04000CC0 RID: 3264
		internal const int DefaultMaxConcurrentCalls = 512;

		// Token: 0x04000CC1 RID: 3265
		internal const int InvalidMaxConcurrentSessions = -1;

		// Token: 0x04000CC2 RID: 3266
		private readonly Dictionary<string, object> m_lifetimeManagers;

		// Token: 0x04000CC3 RID: 3267
		private readonly IList<ServiceHost> m_serviceHosts;

		// Token: 0x04000CC4 RID: 3268
		private readonly ICommunicationFrameworkEventsKit m_eventsKit;

		// Token: 0x04000CC5 RID: 3269
		private readonly ICommunicationOperationProgressEventsKit m_generatedProxyEventsKit;

		// Token: 0x04000CC6 RID: 3270
		private WorkTicketManager m_workTicketManager;

		// Token: 0x04000CC7 RID: 3271
		private readonly IEventsKitFactory m_eventsKitFactory;

		// Token: 0x04000CC8 RID: 3272
		private readonly IActivityFactory m_activityFactory;

		// Token: 0x04000CC9 RID: 3273
		private readonly ElementId m_elementId;

		// Token: 0x04000CCA RID: 3274
		private readonly object m_locker;

		// Token: 0x04000CCB RID: 3275
		private readonly ICertificateProvider m_certificateProvider;

		// Token: 0x04000CCC RID: 3276
		private readonly IMonitoredActivityCompletionModelFactory m_completionModelFactory;

		// Token: 0x04000CCD RID: 3277
		private static readonly string[] s_httpTransportElements = new string[] { "httpsTransportBindingElement", "httpTransportBindingElement" };

		// Token: 0x0200082C RID: 2092
		private sealed class ConfigurationCertificateProvider : ICertificateProvider
		{
			// Token: 0x060032C3 RID: 12995 RVA: 0x000AA2C8 File Offset: 0x000A84C8
			public ConfigurationCertificateProvider()
			{
				this.m_certificateData = new Dictionary<string, ClientCertificateData>();
			}

			// Token: 0x060032C4 RID: 12996 RVA: 0x000AA2DC File Offset: 0x000A84DC
			public ClientCertificateData GetCertificateData([NotNull] string certificateKey)
			{
				ExtendedDiagnostics.EnsureArgumentNotNull<string>(certificateKey, "certificateKey");
				ClientCertificateData clientCertificateData;
				this.m_certificateData.TryGetValue(certificateKey, out clientCertificateData);
				return clientCertificateData;
			}

			// Token: 0x060032C5 RID: 12997 RVA: 0x000AA304 File Offset: 0x000A8504
			public void SetCertificateData([NotNull] ClientCertificateData clientCertificateData)
			{
				ExtendedDiagnostics.EnsureArgumentNotNull<ClientCertificateData>(clientCertificateData, "clientCertificateData");
				ExtendedDiagnostics.EnsureArgumentNotNull<string>(clientCertificateData.ClientCertificateKey, "clientCertificateData.ClientCertificateKey");
				this.m_certificateData[clientCertificateData.ClientCertificateKey] = clientCertificateData;
			}

			// Token: 0x040018F9 RID: 6393
			private readonly Dictionary<string, ClientCertificateData> m_certificateData;
		}
	}
}
