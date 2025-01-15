using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004B0 RID: 1200
	internal class ProxyInvoker<T> : IProxyInvoker<T>, IIdentifiable
	{
		// Token: 0x060024BD RID: 9405 RVA: 0x00083748 File Offset: 0x00081948
		public ProxyInvoker([NotNull] IResourceLifetimeManager<EndpointDifferentiator, T> lifetimeManager, [NotNull] IRouter router, ICommunicationOperationProgressEventsKit eventsKit, [NotNull] WorkTicketManager workTicketManager, [NotNull] ServiceDetails serviceDetails, [NotNull] IChannelInvoker<T> channelInvoker, [NotNull] ElementId elementId)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IResourceLifetimeManager<EndpointDifferentiator, T>>(lifetimeManager, "lifetimeManager");
			ExtendedDiagnostics.EnsureArgumentNotNull<IRouter>(router, "Router");
			ExtendedDiagnostics.EnsureArgumentNotNull<WorkTicketManager>(workTicketManager, "workTicketManager");
			ExtendedDiagnostics.EnsureArgumentNotNull<ServiceDetails>(serviceDetails, "serviceDetails");
			ExtendedDiagnostics.EnsureArgumentNotNull<IChannelInvoker<T>>(channelInvoker, "channelInvoker");
			ExtendedDiagnostics.EnsureArgumentNotNull<ElementId>(elementId, "elementId");
			this.m_lifetimeManager = lifetimeManager;
			this.m_router = router;
			this.m_eventsKit = eventsKit;
			this.m_workTicketManager = workTicketManager;
			this.m_serviceDetails = serviceDetails;
			this.m_channelInvoker = channelInvoker;
			this.m_elementId = elementId;
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x060024BE RID: 9406 RVA: 0x0000E56B File Offset: 0x0000C76B
		public string Name
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x000837D8 File Offset: 0x000819D8
		public Task<TResult> InvokeAsync<TResult>(string methodIdentification, object[] keys, IEnumerable<EcfHttpMessageHeader> httpHeaders, IEnumerable<EcfSoapMessageHeader> soapHeaders, Func<T, Task<TResult>> asyncMethod)
		{
			TResult result = default(TResult);
			Sequencer.AsyncBeginFunction<T> <>9__2;
			Sequencer.AsyncEndFunction <>9__3;
			return Task<TResult>.Factory.FromAsync<string>(delegate(string mi, AsyncCallback callback, object state)
			{
				ProxyInvoker<T> <>4__this = this;
				object[] keys2 = keys;
				IEnumerable<EcfHttpMessageHeader> httpHeaders2 = httpHeaders;
				IEnumerable<EcfSoapMessageHeader> soapHeaders2 = soapHeaders;
				Sequencer.AsyncBeginFunction<T> asyncBeginFunction;
				if ((asyncBeginFunction = <>9__2) == null)
				{
					asyncBeginFunction = (<>9__2 = (T t, AsyncCallback c, object s) => asyncMethod(t).ToApm(c, s));
				}
				Sequencer.AsyncEndFunction asyncEndFunction;
				if ((asyncEndFunction = <>9__3) == null)
				{
					asyncEndFunction = (<>9__3 = delegate(IAsyncResult ar)
					{
						result = ((Task<TResult>)ar).ExtendedResult<TResult>();
					});
				}
				return <>4__this.BeginInvoke(mi, keys2, httpHeaders2, soapHeaders2, asyncBeginFunction, asyncEndFunction, callback, state);
			}, delegate(IAsyncResult ar)
			{
				this.EndInvoke(ar);
				return result;
			}, methodIdentification, null);
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x00083840 File Offset: 0x00081A40
		public Task InvokeAsync(string methodIdentification, object[] keys, IEnumerable<EcfHttpMessageHeader> httpHeaders, IEnumerable<EcfSoapMessageHeader> soapHeaders, Func<T, Task> asyncMethod)
		{
			Sequencer.AsyncBeginFunction<T> <>9__1;
			return Task.Factory.FromAsync<string>(delegate(string mi, AsyncCallback callback, object state)
			{
				ProxyInvoker<T> <>4__this = this;
				object[] keys2 = keys;
				IEnumerable<EcfHttpMessageHeader> httpHeaders2 = httpHeaders;
				IEnumerable<EcfSoapMessageHeader> soapHeaders2 = soapHeaders;
				Sequencer.AsyncBeginFunction<T> asyncBeginFunction;
				if ((asyncBeginFunction = <>9__1) == null)
				{
					asyncBeginFunction = (<>9__1 = (T t, AsyncCallback c, object s) => asyncMethod(t).ToApm(c, s));
				}
				return <>4__this.BeginInvoke(mi, keys2, httpHeaders2, soapHeaders2, asyncBeginFunction, delegate(IAsyncResult ar)
				{
					((Task)ar).ExtendedWait();
				}, callback, state);
			}, new Action<IAsyncResult>(this.EndInvoke), methodIdentification, null);
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x0008389D File Offset: 0x00081A9D
		public IAsyncResult BeginInvoke(string methodIdentification, object[] keys, IEnumerable<EcfHttpMessageHeader> httpHeaders, IEnumerable<EcfSoapMessageHeader> soapHeaders, Sequencer.AsyncBeginFunction<T> beginExecute, Sequencer.AsyncEndFunction endExecute, AsyncCallback cb, object state)
		{
			return SequencerInvoker<ProxyInvoker<T>.ProxyInvokerFlow>.BeginExecute(new ProxyInvoker<T>.ProxyInvokerFlow(keys, httpHeaders, soapHeaders, beginExecute, endExecute, this, methodIdentification), this.m_workTicketManager.CreateWorkTicket(this), cb, state);
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x000838C3 File Offset: 0x00081AC3
		public void EndInvoke(IAsyncResult result)
		{
			SequencerInvoker<ProxyInvoker<T>.ProxyInvokerFlow>.EndExecute(result);
		}

		// Token: 0x04000CF0 RID: 3312
		private IResourceLifetimeManager<EndpointDifferentiator, T> m_lifetimeManager;

		// Token: 0x04000CF1 RID: 3313
		private IRouter m_router;

		// Token: 0x04000CF2 RID: 3314
		private ICommunicationOperationProgressEventsKit m_eventsKit;

		// Token: 0x04000CF3 RID: 3315
		private WorkTicketManager m_workTicketManager;

		// Token: 0x04000CF4 RID: 3316
		private ServiceDetails m_serviceDetails;

		// Token: 0x04000CF5 RID: 3317
		private IChannelInvoker<T> m_channelInvoker;

		// Token: 0x04000CF6 RID: 3318
		private ElementId m_elementId;

		// Token: 0x02000832 RID: 2098
		private class ProxyInvokerFlow : Sequencer
		{
			// Token: 0x060032D3 RID: 13011 RVA: 0x000AA3C8 File Offset: 0x000A85C8
			public ProxyInvokerFlow(object[] keys, IEnumerable<EcfHttpMessageHeader> httpHeaders, IEnumerable<EcfSoapMessageHeader> soapHeaders, Sequencer.AsyncBeginFunction<T> beginExecute, Sequencer.AsyncEndFunction endExecute, ProxyInvoker<T> invoker, string methodIdentification)
			{
				this.m_keys = keys;
				this.m_httpHeaders = httpHeaders;
				this.m_soapHeaders = soapHeaders;
				this.m_beginExecuteOperation = beginExecute;
				this.m_endExecuteOperation = endExecute;
				this.m_invoker = invoker;
				this.m_methodIdentification = methodIdentification;
				this.m_bottomLevelHandler = new BottomLevelHandler<ProxyInvoker<T>.ProxyInvokerFlow.ProxyInvokerInstanceContext, ProxyInvoker<T>.ProxyInvokerFlow.ProxyInvokerOperationContext>(ProxyInvoker<T>.ProxyInvokerFlow.s_regularExceptionToMonitoredException, new ProxyInvoker<T>.ProxyInvokerFlow.ProxyInvokerInstanceContext(this.m_invoker));
			}

			// Token: 0x060032D4 RID: 13012 RVA: 0x000AA42B File Offset: 0x000A862B
			protected override IEnumerable<IFlowStep> Run()
			{
				IRouterRequest request = null;
				IEnumerable<EndpointFault> exceptions = null;
				IEnumerable<EndpointIdentifier> endpoints = null;
				yield return base.RunAsyncStep<object[]>("Create request", delegate(string step, Exception ex)
				{
					CommunicationFrameworkRouterException ex3 = ex as CommunicationFrameworkRouterException;
					if (ex3 != null)
					{
						this.m_invoker.m_eventsKit.NotifyRoutingOperationError(typeof(T).Name, ex3);
					}
					return ex;
				}, new Sequencer.AsyncBeginFunction<object[]>(this.m_invoker.m_router.BeginCreateRequest), delegate(IAsyncResult ar)
				{
					request = this.m_invoker.m_router.EndCreateRequest(ar);
				}, this.m_keys);
				bool flag = true;
				int retryCount = 0;
				while (flag)
				{
					int num = retryCount;
					retryCount = num + 1;
					if (num > 1000)
					{
						CommunicationFrameworkRouterException ex2 = new CommunicationFrameworkRouterException("Exceeded maximum retry attempts");
						this.m_invoker.m_eventsKit.NotifyOperationRetryCountExceeded(this.m_invoker.m_serviceDetails.ServiceName, this.m_invoker.m_serviceDetails.Contract, this.m_invoker.m_elementId.ToString(), ex2);
						throw ex2;
					}
					yield return base.RunAsyncStep<IEnumerable<EndpointIdentifier>, IEnumerable<EndpointFault>>("Get next endpoints from router request for method: {0}".FormatWithInvariantCulture(new object[] { this.m_methodIdentification }), new Sequencer.AsyncBeginFunction<IEnumerable<EndpointIdentifier>, IEnumerable<EndpointFault>>(request.BeginGetNextEndpoints), delegate(IAsyncResult ar)
					{
						endpoints = request.EndGetNextEndpoints(ar);
					}, endpoints, exceptions);
					ExtendedDiagnostics.EnsureOperation(endpoints != null && endpoints.Any<EndpointIdentifier>(), "GetNextEndpoints() should not return empty result");
					yield return base.RunAsyncStep<IEnumerable<EndpointIdentifier>>("WCF Proxy for method {0}".FormatWithInvariantCulture(new object[] { this.m_methodIdentification }), new Sequencer.AsyncBeginFunction<IEnumerable<EndpointIdentifier>>(this.BeginExecuteOperation), delegate(IAsyncResult ar)
					{
						exceptions = this.EndExecuteOperation(ar);
					}, endpoints);
					flag = exceptions.Any<EndpointFault>();
				}
				yield return base.RunAsyncStep("Complete request", new Sequencer.AsyncBeginFunction(request.BeginCompleteRequest), delegate(IAsyncResult ar)
				{
					request.EndCompleteRequest(ar);
				});
				yield break;
			}

			// Token: 0x060032D5 RID: 13013 RVA: 0x000AA43C File Offset: 0x000A863C
			private IAsyncResult BeginExecuteOperation(IEnumerable<EndpointIdentifier> destinations, AsyncCallback cb, object state)
			{
				ProxyInvoker<T>.ProxyInvokerFlow.<>c__DisplayClass12_0 CS$<>8__locals1 = new ProxyInvoker<T>.ProxyInvokerFlow.<>c__DisplayClass12_0();
				CS$<>8__locals1.<>4__this = this;
				CS$<>8__locals1.destinations = destinations;
				CS$<>8__locals1.ecfMultiChainedAsyncResult = new ECFMultiChainedAsyncResult<T>(cb, state, new Collection<Pair<IResourceHandle<T>, EndpointIdentifier>>());
				CS$<>8__locals1.ecfResults = new List<IAsyncResult>(CS$<>8__locals1.destinations.Count<EndpointIdentifier>());
				List<EndpointFault> list = new List<EndpointFault>();
				int i;
				int j;
				for (i = 0; i < CS$<>8__locals1.destinations.Count<EndpointIdentifier>(); i = j + 1)
				{
					EndpointIdentifier endpointIdentifier = CS$<>8__locals1.destinations.ElementAt(i);
					IResourceHandle<T> resourceHandle = this.m_invoker.m_lifetimeManager.Get(new EndpointDifferentiator(endpointIdentifier.Uri), CS$<>8__locals1.destinations.ElementAt(i));
					this.m_bottomLevelHandler.Run(new ProxyInvoker<T>.ProxyInvokerFlow.ProxyInvokerOperationContext(list, resourceHandle, endpointIdentifier, RouterExceptionOrigin.Begin), delegate
					{
						CS$<>8__locals1.<>4__this.m_invoker.m_eventsKit.NotifyOperationStarted(CS$<>8__locals1.<>4__this.m_invoker.m_serviceDetails.ToString(), endpointIdentifier.Uri.ToString(), endpointIdentifier.Identifier);
						IAsyncResult asyncResult = CS$<>8__locals1.<>4__this.m_invoker.m_channelInvoker.BeginInvoke(resourceHandle.Value, CS$<>8__locals1.<>4__this.m_httpHeaders, CS$<>8__locals1.<>4__this.m_soapHeaders, CS$<>8__locals1.<>4__this.m_beginExecuteOperation, CS$<>8__locals1.<>4__this.m_endExecuteOperation, new AsyncCallback(CS$<>8__locals1.ecfMultiChainedAsyncResult.BeginAsyncFunctionCallback), null);
						CS$<>8__locals1.ecfResults.Add(asyncResult);
						CS$<>8__locals1.ecfMultiChainedAsyncResult.Data.Add(new Pair<IResourceHandle<T>, EndpointIdentifier>(resourceHandle, CS$<>8__locals1.destinations.ElementAt(i)));
					});
					j = i;
				}
				CS$<>8__locals1.ecfMultiChainedAsyncResult.Exceptions = list;
				CS$<>8__locals1.ecfMultiChainedAsyncResult.BeginJoin(CS$<>8__locals1.ecfResults);
				return CS$<>8__locals1.ecfMultiChainedAsyncResult;
			}

			// Token: 0x060032D6 RID: 13014 RVA: 0x000AA598 File Offset: 0x000A8798
			private IList<EndpointFault> EndExecuteOperation(IAsyncResult ar)
			{
				ECFMultiChainedAsyncResult<T> ecfMultiChainedAsyncResult = (ECFMultiChainedAsyncResult<T>)ar;
				IList<EndpointFault> exceptions = ecfMultiChainedAsyncResult.Exceptions;
				ReadOnlyCollection<IAsyncResult> innerResults = ecfMultiChainedAsyncResult.EndJoin();
				for (int i = 0; i < innerResults.Count; i++)
				{
					int j = i;
					EndpointIdentifier endpointIdentifier = ecfMultiChainedAsyncResult.Data[i].Second;
					this.m_bottomLevelHandler.Run(new ProxyInvoker<T>.ProxyInvokerFlow.ProxyInvokerOperationContext(exceptions, ecfMultiChainedAsyncResult.Data[i].First, endpointIdentifier, RouterExceptionOrigin.End), delegate
					{
						this.m_invoker.m_channelInvoker.EndInvoke(innerResults[j]);
						this.m_invoker.m_eventsKit.NotifyOperationEndedSuccessfully(this.m_invoker.m_serviceDetails.ToString(), endpointIdentifier.Uri.ToString(), endpointIdentifier.Identifier);
						this.m_invoker.m_lifetimeManager.Release(ecfMultiChainedAsyncResult.Data[j].First);
					});
				}
				return exceptions;
			}

			// Token: 0x060032D7 RID: 13015 RVA: 0x000AA66C File Offset: 0x000A886C
			private static void HandleException(Exception ex, MonitoredException mex, ProxyInvoker<T>.ProxyInvokerFlow.ProxyInvokerInstanceContext instanceContext, ProxyInvoker<T>.ProxyInvokerFlow.ProxyInvokerOperationContext operationContext)
			{
				instanceContext.Invoker.m_lifetimeManager.ReportFaulted(operationContext.ResourceHandle, ex);
				operationContext.ChannelExceptions.Add(new EndpointFault(operationContext.EndpointId, ex, mex, operationContext.ExceptionOrigin));
				instanceContext.Invoker.m_eventsKit.NotifyOperationFailed(instanceContext.Invoker.m_serviceDetails.ServiceName, instanceContext.Invoker.m_serviceDetails.Contract, instanceContext.Invoker.m_elementId.ToString(), operationContext.EndpointId.Uri.ToString(), mex, operationContext.EndpointId.Identifier);
			}

			// Token: 0x04001902 RID: 6402
			private const int c_retryAttemptsLimit = 1000;

			// Token: 0x04001903 RID: 6403
			private object[] m_keys;

			// Token: 0x04001904 RID: 6404
			private IEnumerable<EcfHttpMessageHeader> m_httpHeaders;

			// Token: 0x04001905 RID: 6405
			private IEnumerable<EcfSoapMessageHeader> m_soapHeaders;

			// Token: 0x04001906 RID: 6406
			private Sequencer.AsyncBeginFunction<T> m_beginExecuteOperation;

			// Token: 0x04001907 RID: 6407
			private Sequencer.AsyncEndFunction m_endExecuteOperation;

			// Token: 0x04001908 RID: 6408
			private ProxyInvoker<T> m_invoker;

			// Token: 0x04001909 RID: 6409
			private string m_methodIdentification;

			// Token: 0x0400190A RID: 6410
			private readonly BottomLevelHandler<ProxyInvoker<T>.ProxyInvokerFlow.ProxyInvokerInstanceContext, ProxyInvoker<T>.ProxyInvokerFlow.ProxyInvokerOperationContext> m_bottomLevelHandler;

			// Token: 0x0400190B RID: 6411
			private static readonly Dictionary<Type, Action<Exception, ProxyInvoker<T>.ProxyInvokerFlow.ProxyInvokerInstanceContext, ProxyInvoker<T>.ProxyInvokerFlow.ProxyInvokerOperationContext>> s_regularExceptionToMonitoredException = new Dictionary<Type, Action<Exception, ProxyInvoker<T>.ProxyInvokerFlow.ProxyInvokerInstanceContext, ProxyInvoker<T>.ProxyInvokerFlow.ProxyInvokerOperationContext>>
			{
				{
					typeof(EncoderFallbackException),
					delegate(Exception ex, ProxyInvoker<T>.ProxyInvokerFlow.ProxyInvokerInstanceContext instanceContext, ProxyInvoker<T>.ProxyInvokerFlow.ProxyInvokerOperationContext operationContext)
					{
						ProxyInvoker<T>.ProxyInvokerFlow.HandleException(ex, new CommunicationFrameworkEncodingException(instanceContext.Invoker.m_serviceDetails, "Encoding Problem", ex), instanceContext, operationContext);
					}
				},
				{
					typeof(ObjectDisposedException),
					delegate(Exception ex, ProxyInvoker<T>.ProxyInvokerFlow.ProxyInvokerInstanceContext instanceContext, ProxyInvoker<T>.ProxyInvokerFlow.ProxyInvokerOperationContext operationContext)
					{
						ProxyInvoker<T>.ProxyInvokerFlow.HandleException(ex, new CommunicationFrameworkCommunicationException(instanceContext.Invoker.m_serviceDetails, "Communication Problem", ex), instanceContext, operationContext);
					}
				},
				{
					typeof(TimeoutException),
					delegate(Exception ex, ProxyInvoker<T>.ProxyInvokerFlow.ProxyInvokerInstanceContext instanceContext, ProxyInvoker<T>.ProxyInvokerFlow.ProxyInvokerOperationContext operationContext)
					{
						ProxyInvoker<T>.ProxyInvokerFlow.HandleException(ex, new CommunicationFrameworkTimeoutException(instanceContext.Invoker.m_serviceDetails, "Timeout occurred", ex), instanceContext, operationContext);
					}
				},
				{
					typeof(ActionNotSupportedException),
					delegate(Exception ex, ProxyInvoker<T>.ProxyInvokerFlow.ProxyInvokerInstanceContext instanceContext, ProxyInvoker<T>.ProxyInvokerFlow.ProxyInvokerOperationContext operationContext)
					{
						ProxyInvoker<T>.ProxyInvokerFlow.HandleException(ex, new CommunicationFrameworkVersioningException("Versioning Problem", ex), instanceContext, operationContext);
					}
				},
				{
					typeof(FaultException),
					delegate(Exception ex, ProxyInvoker<T>.ProxyInvokerFlow.ProxyInvokerInstanceContext instanceContext, ProxyInvoker<T>.ProxyInvokerFlow.ProxyInvokerOperationContext operationContext)
					{
						ProxyInvoker<T>.ProxyInvokerFlow.HandleException(ex, new CommunicationFrameworkExternalException("External Problem", ex), instanceContext, operationContext);
					}
				},
				{
					typeof(ProtocolException),
					delegate(Exception ex, ProxyInvoker<T>.ProxyInvokerFlow.ProxyInvokerInstanceContext instanceContext, ProxyInvoker<T>.ProxyInvokerFlow.ProxyInvokerOperationContext operationContext)
					{
						ProxyInvoker<T>.ProxyInvokerFlow.HandleException(ex, new CommunicationFrameworkExternalException("External Problem", ex), instanceContext, operationContext);
					}
				},
				{
					typeof(CommunicationException),
					delegate(Exception ex, ProxyInvoker<T>.ProxyInvokerFlow.ProxyInvokerInstanceContext instanceContext, ProxyInvoker<T>.ProxyInvokerFlow.ProxyInvokerOperationContext operationContext)
					{
						ProxyInvoker<T>.ProxyInvokerFlow.HandleException(ex, new CommunicationFrameworkCommunicationException(instanceContext.Invoker.m_serviceDetails, "Communication Problem", ex), instanceContext, operationContext);
					}
				},
				{
					typeof(CryptographicException),
					delegate(Exception ex, ProxyInvoker<T>.ProxyInvokerFlow.ProxyInvokerInstanceContext instanceContext, ProxyInvoker<T>.ProxyInvokerFlow.ProxyInvokerOperationContext operationContext)
					{
						ProxyInvoker<T>.ProxyInvokerFlow.HandleException(ex, new CommunicationFrameworkCommunicationException(instanceContext.Invoker.m_serviceDetails, "Communication Problem", ex), instanceContext, operationContext);
					}
				},
				{
					typeof(HttpListenerException),
					delegate(Exception ex, ProxyInvoker<T>.ProxyInvokerFlow.ProxyInvokerInstanceContext instanceContext, ProxyInvoker<T>.ProxyInvokerFlow.ProxyInvokerOperationContext operationContext)
					{
						HttpListenerException ex2 = (HttpListenerException)ex;
						if (ex2.ErrorCode == 995 || ex2.ErrorCode == 1229)
						{
							ProxyInvoker<T>.ProxyInvokerFlow.HandleException(ex2, new CommunicationFrameworkCommunicationException("Communication Problem", ex2), instanceContext, operationContext);
							return;
						}
						ProxyInvoker<T>.ProxyInvokerFlow.HandleException(ex2, new CommunicationFrameworkNonMonitoredWrapperException(string.Empty, ex2), instanceContext, operationContext);
					}
				},
				{
					typeof(MonitoredException),
					delegate(Exception ex, ProxyInvoker<T>.ProxyInvokerFlow.ProxyInvokerInstanceContext instanceContext, ProxyInvoker<T>.ProxyInvokerFlow.ProxyInvokerOperationContext operationContext)
					{
						ProxyInvoker<T>.ProxyInvokerFlow.HandleException(ex, (MonitoredException)ex, instanceContext, operationContext);
					}
				},
				{
					typeof(Exception),
					delegate(Exception ex, ProxyInvoker<T>.ProxyInvokerFlow.ProxyInvokerInstanceContext instanceContext, ProxyInvoker<T>.ProxyInvokerFlow.ProxyInvokerOperationContext operationContext)
					{
						ProxyInvoker<T>.ProxyInvokerFlow.HandleException(ex, new CommunicationFrameworkNonMonitoredWrapperException(string.Empty, ex), instanceContext, operationContext);
					}
				}
			};

			// Token: 0x02000894 RID: 2196
			public sealed class ProxyInvokerOperationContext
			{
				// Token: 0x17000794 RID: 1940
				// (get) Token: 0x06003409 RID: 13321 RVA: 0x000ADCD1 File Offset: 0x000ABED1
				// (set) Token: 0x0600340A RID: 13322 RVA: 0x000ADCD9 File Offset: 0x000ABED9
				public ICollection<EndpointFault> ChannelExceptions { get; private set; }

				// Token: 0x17000795 RID: 1941
				// (get) Token: 0x0600340B RID: 13323 RVA: 0x000ADCE2 File Offset: 0x000ABEE2
				// (set) Token: 0x0600340C RID: 13324 RVA: 0x000ADCEA File Offset: 0x000ABEEA
				public IResourceHandle<T> ResourceHandle { get; private set; }

				// Token: 0x17000796 RID: 1942
				// (get) Token: 0x0600340D RID: 13325 RVA: 0x000ADCF3 File Offset: 0x000ABEF3
				// (set) Token: 0x0600340E RID: 13326 RVA: 0x000ADCFB File Offset: 0x000ABEFB
				public EndpointIdentifier EndpointId { get; private set; }

				// Token: 0x17000797 RID: 1943
				// (get) Token: 0x0600340F RID: 13327 RVA: 0x000ADD04 File Offset: 0x000ABF04
				// (set) Token: 0x06003410 RID: 13328 RVA: 0x000ADD0C File Offset: 0x000ABF0C
				public RouterExceptionOrigin ExceptionOrigin { get; private set; }

				// Token: 0x06003411 RID: 13329 RVA: 0x000ADD15 File Offset: 0x000ABF15
				public ProxyInvokerOperationContext(ICollection<EndpointFault> channelExceptions, IResourceHandle<T> resourceHandle, EndpointIdentifier endpointId, RouterExceptionOrigin exceptionOrigin)
				{
					this.ChannelExceptions = channelExceptions;
					this.ResourceHandle = resourceHandle;
					this.EndpointId = endpointId;
					this.ExceptionOrigin = exceptionOrigin;
				}
			}

			// Token: 0x02000895 RID: 2197
			public sealed class ProxyInvokerInstanceContext
			{
				// Token: 0x17000798 RID: 1944
				// (get) Token: 0x06003412 RID: 13330 RVA: 0x000ADD3A File Offset: 0x000ABF3A
				// (set) Token: 0x06003413 RID: 13331 RVA: 0x000ADD42 File Offset: 0x000ABF42
				public ProxyInvoker<T> Invoker { get; private set; }

				// Token: 0x06003414 RID: 13332 RVA: 0x000ADD4B File Offset: 0x000ABF4B
				public ProxyInvokerInstanceContext(ProxyInvoker<T> invoker)
				{
					this.Invoker = invoker;
				}
			}
		}
	}
}
