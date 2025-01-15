using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.RequestProtection;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004B5 RID: 1205
	public class ServiceExtender
	{
		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x060024E7 RID: 9447 RVA: 0x00083AD4 File Offset: 0x00081CD4
		// (set) Token: 0x060024E8 RID: 9448 RVA: 0x00083ADC File Offset: 0x00081CDC
		internal IErrorHandler ErrorHandler { get; private set; }

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x060024E9 RID: 9449 RVA: 0x00083AE5 File Offset: 0x00081CE5
		// (set) Token: 0x060024EA RID: 9450 RVA: 0x00083AED File Offset: 0x00081CED
		internal Func<IRequestProtectionContext, WorkTicket> CreateWorkTicketDelegate { get; private set; }

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x060024EB RID: 9451 RVA: 0x00083AF6 File Offset: 0x00081CF6
		// (set) Token: 0x060024EC RID: 9452 RVA: 0x00083AFE File Offset: 0x00081CFE
		internal EndpointActivity EndpointActivity { get; private set; }

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x060024ED RID: 9453 RVA: 0x00083B07 File Offset: 0x00081D07
		// (set) Token: 0x060024EE RID: 9454 RVA: 0x00083B0F File Offset: 0x00081D0F
		internal IPlatformActivityTracingPolicyEvaluator TracingEvaluator { get; private set; }

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x060024EF RID: 9455 RVA: 0x00083B18 File Offset: 0x00081D18
		// (set) Token: 0x060024F0 RID: 9456 RVA: 0x00083B20 File Offset: 0x00081D20
		internal Action<IRequestProtectionContext> OnIncomingRequest { get; private set; }

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x060024F1 RID: 9457 RVA: 0x00083B29 File Offset: 0x00081D29
		// (set) Token: 0x060024F2 RID: 9458 RVA: 0x00083B31 File Offset: 0x00081D31
		internal IEndpointBehavior ErrorHandlingBehavior { get; private set; }

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x060024F3 RID: 9459 RVA: 0x00083B3A File Offset: 0x00081D3A
		// (set) Token: 0x060024F4 RID: 9460 RVA: 0x00083B42 File Offset: 0x00081D42
		public ServiceHost Host { get; private set; }

		// Token: 0x060024F5 RID: 9461 RVA: 0x00083B4B File Offset: 0x00081D4B
		internal ServiceExtender([NotNull] ServiceHost serviceHost)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<ServiceHost>(serviceHost, "serviceHost");
			this.Host = serviceHost;
		}

		// Token: 0x060024F6 RID: 9462 RVA: 0x00083B65 File Offset: 0x00081D65
		public void SetErrorHandler(IErrorHandler errorHandler)
		{
			TraceSourceBase<CommunicationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Setting new error handler");
			this.ErrorHandler = errorHandler;
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x00083B7E File Offset: 0x00081D7E
		public void SetWorkTicketCreator(Func<IRequestProtectionContext, WorkTicket> workTicketCreator)
		{
			TraceSourceBase<CommunicationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Setting a work ticket creator");
			this.CreateWorkTicketDelegate = workTicketCreator;
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x00083B97 File Offset: 0x00081D97
		public void AddEndpointActivity(ClientActivityContextSource source, ActivityType activityType, string rootActivityIdHeaderName, string clientActivityIdHeaderName)
		{
			TraceSourceBase<CommunicationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Setting endpoint activity for activity type {0}".FormatWithInvariantCulture(new object[] { activityType }));
			this.EndpointActivity = new EndpointActivity(source, activityType, rootActivityIdHeaderName, clientActivityIdHeaderName);
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x00083BC8 File Offset: 0x00081DC8
		public void SetTracingEvaluator(IPlatformActivityTracingPolicyEvaluator evaluator)
		{
			TraceSourceBase<CommunicationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Setting a tracing evaluator");
			this.TracingEvaluator = evaluator;
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x00083BE1 File Offset: 0x00081DE1
		public void SetOnIncomingRequestCallback(Action<IRequestProtectionContext> onIncomingRequest)
		{
			TraceSourceBase<CommunicationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Setting an incoming request callback");
			this.OnIncomingRequest = onIncomingRequest;
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x00083BFA File Offset: 0x00081DFA
		public void SetErrorHandlingBehavior(IEndpointBehavior behavior)
		{
			TraceSourceBase<CommunicationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Setting new error handling behavior");
			this.ErrorHandlingBehavior = behavior;
		}
	}
}
