using System;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.MonitoredUtils;
using Microsoft.Cloud.Platform.RequestProtection;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004CB RID: 1227
	internal class ExternalEndpointBehavior : IEndpointBehavior
	{
		// Token: 0x06002562 RID: 9570 RVA: 0x00084D95 File Offset: 0x00082F95
		public ExternalEndpointBehavior(EndpointActivity endpointActivity, IActivityFactory activityFactory, IEventsKitFactory eventsKitFactory, Action<EcfRequestContext> onIncomingRequest, Func<IRequestProtectionContext, WorkTicket> createWorkTicket, IMonitoredActivityCompletionModelFactory completionModelFactory, IPlatformActivityTracingPolicyEvaluator tracingEvaluator)
		{
			this.m_endpointActivity = endpointActivity;
			this.m_activityFactory = activityFactory;
			this.m_tracingEvaluator = tracingEvaluator;
			this.m_eventsKitFactory = eventsKitFactory;
			this.m_createWorkTicket = createWorkTicket;
			this.m_onIncomingRequest = onIncomingRequest;
			this.m_completionModelFactory = completionModelFactory;
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
		}

		// Token: 0x06002565 RID: 9573 RVA: 0x00084DD4 File Offset: 0x00082FD4
		public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
		{
			foreach (OperationDescription operationDescription in endpoint.Contract.Operations)
			{
				if (this.m_onIncomingRequest != null)
				{
					if (!operationDescription.Behaviors.Where((IOperationBehavior b) => b.GetType().Equals(typeof(OnIncomingRequestOperationBehavior))).Any<IOperationBehavior>())
					{
						TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceInformation("Adding behavior of type {0} to endpoint {1}".FormatWithInvariantCulture(new object[]
						{
							typeof(OnIncomingRequestOperationBehavior),
							endpoint.Name
						}));
						operationDescription.Behaviors.Add(new OnIncomingRequestOperationBehavior(this.m_onIncomingRequest, this.m_eventsKitFactory));
					}
				}
				if (this.m_tracingEvaluator != null)
				{
					if (!operationDescription.Behaviors.Where((IOperationBehavior b) => b.GetType().Equals(typeof(TracingEvaluatorOperationBehavior))).Any<IOperationBehavior>())
					{
						TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceInformation("Adding behavior of type {0} to endpoint {1}".FormatWithInvariantCulture(new object[]
						{
							typeof(TracingEvaluatorOperationBehavior),
							endpoint.Name
						}));
						operationDescription.Behaviors.Add(new TracingEvaluatorOperationBehavior(this.m_eventsKitFactory, this.m_tracingEvaluator));
					}
				}
				if (this.m_createWorkTicket != null)
				{
					if (!operationDescription.Behaviors.Where((IOperationBehavior b) => b.GetType().Equals(typeof(WorkTicketProtectionOperationBehavior))).Any<IOperationBehavior>())
					{
						TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceInformation("Adding behavior of type {0} to endpoint {1}".FormatWithInvariantCulture(new object[]
						{
							typeof(WorkTicketProtectionOperationBehavior),
							endpoint.Name
						}));
						operationDescription.Behaviors.Add(new WorkTicketProtectionOperationBehavior(this.m_eventsKitFactory, this.m_createWorkTicket));
					}
				}
				if (this.m_endpointActivity != null && this.m_endpointActivity.ActivityType != null)
				{
					if (!operationDescription.Behaviors.Where((IOperationBehavior b) => b.GetType().Equals(typeof(ExternalMonitoredActivityOperationBehavior))).Any<IOperationBehavior>())
					{
						TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceInformation("Adding behavior of type {0} to endpoint {1}".FormatWithInvariantCulture(new object[]
						{
							typeof(ExternalMonitoredActivityOperationBehavior),
							endpoint.Name
						}));
						operationDescription.Behaviors.Add(new ExternalMonitoredActivityOperationBehavior(this.m_endpointActivity.CorrelationIdsSource, this.m_endpointActivity.ActivityType, this.m_endpointActivity.RootActivityIdHeaderName, this.m_endpointActivity.ClientActivityIdHeaderName, this.m_activityFactory, this.m_completionModelFactory));
					}
				}
			}
		}

		// Token: 0x06002566 RID: 9574 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void Validate(ServiceEndpoint endpoint)
		{
		}

		// Token: 0x04000D2A RID: 3370
		private IMonitoredActivityCompletionModelFactory m_completionModelFactory;

		// Token: 0x04000D2B RID: 3371
		private IPlatformActivityTracingPolicyEvaluator m_tracingEvaluator;

		// Token: 0x04000D2C RID: 3372
		private Func<IRequestProtectionContext, WorkTicket> m_createWorkTicket;

		// Token: 0x04000D2D RID: 3373
		private Action<EcfRequestContext> m_onIncomingRequest;

		// Token: 0x04000D2E RID: 3374
		private IEventsKitFactory m_eventsKitFactory;

		// Token: 0x04000D2F RID: 3375
		private IActivityFactory m_activityFactory;

		// Token: 0x04000D30 RID: 3376
		private EndpointActivity m_endpointActivity;
	}
}
