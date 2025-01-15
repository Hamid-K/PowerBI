using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Eventing.Base
{
	// Token: 0x020003C8 RID: 968
	[BlockServiceProvider(typeof(IOnBehalfOfEventService))]
	public sealed class OnBehalfOfEventService : Block, IOnBehalfOfEventService
	{
		// Token: 0x06001DEF RID: 7663 RVA: 0x00071565 File Offset: 0x0006F765
		public OnBehalfOfEventService()
			: this(typeof(OnBehalfOfEventService).Name)
		{
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x00010777 File Offset: 0x0000E977
		public OnBehalfOfEventService(string name)
			: base(name)
		{
		}

		// Token: 0x06001DF1 RID: 7665 RVA: 0x0007157C File Offset: 0x0006F77C
		protected override BlockInitializationStatus OnInitialize()
		{
			BlockInitializationStatus blockInitializationStatus = BlockInitializationStatus.PartiallyDone;
			if (base.OnInitialize() == BlockInitializationStatus.Done)
			{
				this.m_eventsKitExplorer = this.m_eventsKitExplorerFactory.Create();
				this.m_onBehalfOfEventsKit = this.m_eventsKitFactory.CreateEventsKit<IOnBehalfOfEventsKit>();
				blockInitializationStatus = BlockInitializationStatus.Done;
			}
			return blockInitializationStatus;
		}

		// Token: 0x06001DF2 RID: 7666 RVA: 0x000715B8 File Offset: 0x0006F7B8
		public void FireOnBehalfOfEvent([NotNull] string source, [NotNull] EtwEvent etwEvent)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(source, "source");
			ExtendedDiagnostics.EnsureArgumentNotNull<EtwEvent>(etwEvent, "etwEvent");
			TraceSourceBase<EventingTrace>.Tracer.TraceVerbose("Firing '{0}' EtwEvent on behalf of {1}.", new object[] { etwEvent.EventName, source });
			IEventMetadata eventMetadata;
			if (this.m_eventsKitExplorer.TryGetEventMetadata(etwEvent.EventId, out eventMetadata))
			{
				if (eventMetadata.IsPublishedEvent && eventMetadata.IsPublishedToEventingServer)
				{
					WireEventBase wireEventBase = OnBehalfOfEventService.CreateFromEtwEvent(eventMetadata, etwEvent);
					TraceSourceBase<EventingTrace>.Tracer.TraceVerbose("Transformed the EtwEvent '{0}' into OnBehalfOfWireEvent {1}", new object[] { etwEvent.EventName, wireEventBase.Id });
					this.m_eventingServer.SubmitEvent(wireEventBase);
				}
				else
				{
					TraceSourceBase<EventingTrace>.Tracer.TraceInformation("The EtwEvent was mapped to an eventId {0}. Since this event is not a PublishedEvent, it doesn't get submitted to the eventing server", new object[] { eventMetadata.Id });
				}
			}
			else
			{
				TraceSourceBase<EventingTrace>.Tracer.TraceWarning("The EtwEvent could not be mapped to a valid events-kit event and thus doesn't get submitted to the eventing server");
			}
			string text = etwEvent.ToJsonString();
			if (EtwEvent.IsJsonEventExceedSizeLimit(text))
			{
				EtwEvent.FireTruncatedJsonEvent(this.m_onBehalfOfEventsKit, etwEvent);
				TraceSourceBase<EventingTrace>.Tracer.TraceWarning("The EtwEvent '{0}' on behalf of '{1}' exceeds the maximum size limit 32 K characters and is fired with truncated payload to fit within the size limit", new object[] { etwEvent.EventName, source });
				return;
			}
			this.m_onBehalfOfEventsKit.FireOnBehalfOfJsonEvent(text);
		}

		// Token: 0x06001DF3 RID: 7667 RVA: 0x000716D8 File Offset: 0x0006F8D8
		private static WireEventBase CreateFromEtwEvent([NotNull] IEventMetadata eventMetadata, [NotNull] EtwEvent etwEvent)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IEventMetadata>(eventMetadata, "eventMetadata");
			ExtendedDiagnostics.EnsureArgumentNotNull<EtwEvent>(etwEvent, "etwEvent");
			EventParameter eventParameter = etwEvent.EventParameters.FirstOrDefault((EventParameter parameter) => typeof(IMonitoredError).IsAssignableFrom(parameter.Type));
			IMonitoredError monitoredError = ((eventParameter != null) ? (eventParameter.Value as IMonitoredError) : null);
			if (monitoredError != null)
			{
				return new OnBehalfOfEventService.OnBehalfOfWireEventWithMonitoredError(eventMetadata, etwEvent, monitoredError);
			}
			return new OnBehalfOfEventService.OnBehalfOfWireEvent(eventMetadata, etwEvent);
		}

		// Token: 0x04000A41 RID: 2625
		private IOnBehalfOfEventsKit m_onBehalfOfEventsKit;

		// Token: 0x04000A42 RID: 2626
		private IEventsKitExplorer m_eventsKitExplorer;

		// Token: 0x04000A43 RID: 2627
		[BlockServiceDependency]
		private IEventsKitExplorerFactory m_eventsKitExplorerFactory;

		// Token: 0x04000A44 RID: 2628
		[BlockServiceDependency]
		private IEventsKitFactory m_eventsKitFactory;

		// Token: 0x04000A45 RID: 2629
		[BlockServiceDependency]
		private IEventingServer m_eventingServer;

		// Token: 0x020007D6 RID: 2006
		private class OnBehalfOfWireEvent : WireEventBase
		{
			// Token: 0x060031F8 RID: 12792 RVA: 0x000A9118 File Offset: 0x000A7318
			public OnBehalfOfWireEvent(IEventMetadata eventMetadata, EtwEvent etwEvent)
				: base(eventMetadata.Id.FullId, etwEvent.ElementId, etwEvent.Activity, null, etwEvent.Timestamp)
			{
				if (etwEvent.EventParameters != null)
				{
					this.m_eventParameters = etwEvent.EventParameters.ToList<EventParameter>();
				}
				this.m_message = etwEvent.Message;
			}

			// Token: 0x17000776 RID: 1910
			// (get) Token: 0x060031F9 RID: 12793 RVA: 0x000A9176 File Offset: 0x000A7376
			public override IEnumerable<EventParameter> EventParameters
			{
				get
				{
					return this.m_eventParameters;
				}
			}

			// Token: 0x060031FA RID: 12794 RVA: 0x000A917E File Offset: 0x000A737E
			public override string ToString()
			{
				return this.m_message;
			}

			// Token: 0x060031FB RID: 12795 RVA: 0x000A917E File Offset: 0x000A737E
			public override string ToInvariantCultureString()
			{
				return this.m_message;
			}

			// Token: 0x060031FC RID: 12796 RVA: 0x000A917E File Offset: 0x000A737E
			public override string ToMonitoringString()
			{
				return this.m_message;
			}

			// Token: 0x04001723 RID: 5923
			private List<EventParameter> m_eventParameters;

			// Token: 0x04001724 RID: 5924
			private string m_message;
		}

		// Token: 0x020007D7 RID: 2007
		private class OnBehalfOfWireEventWithMonitoredError : OnBehalfOfEventService.OnBehalfOfWireEvent, IMonitoredError, IContainsPrivateInformation
		{
			// Token: 0x060031FD RID: 12797 RVA: 0x000A9188 File Offset: 0x000A7388
			public OnBehalfOfWireEventWithMonitoredError([NotNull] IEventMetadata eventMetadata, EtwEvent etwEvent, [NotNull] IMonitoredError monitoredError)
				: base(eventMetadata, etwEvent)
			{
				ExtendedDiagnostics.EnsureArgumentNotNull<IMonitoredError>(monitoredError, "monitoredError");
				ExtendedDiagnostics.EnsureArgumentNotNull<IEventMetadata>(eventMetadata, "eventMetadata");
				ExtendedDiagnostics.EnsureArgumentNotNull<EventsKitIdentifiers>(eventMetadata.Id, "eventMetadata.Id");
				ExtendedDiagnostics.EnsureArgumentNotNull<MethodInfo>(eventMetadata.EventMethod, "eventMetadata.EventMethod");
				this.m_monitoredError = monitoredError;
				this.MonitoringScope = monitoredError.MonitoringScope;
				this.ErrorEventId = eventMetadata.Id.EventId;
				this.ErrorEventsKitId = eventMetadata.Id.EventsKitId;
				this.ErrorEventName = eventMetadata.EventMethod.Name;
				this.ErrorEventsKitName = eventMetadata.EventMethod.DeclaringType.FullName;
				TraceSourceBase<EventingTrace>.Tracer.TraceInformation("OnBehalfOfWireEventWithMonitoredError was updated with eventMetadata information: ErrorEventId='{0}', ErrorEventsKitId='{1}', ErrorEventName='{2}', ErrorEventsKitName='{3}'", new object[] { this.ErrorEventId, this.ErrorEventsKitId, this.ErrorEventName, this.ErrorEventsKitName });
			}

			// Token: 0x060031FE RID: 12798 RVA: 0x000A9272 File Offset: 0x000A7472
			public bool IsFatal()
			{
				return this.m_monitoredError.IsFatal();
			}

			// Token: 0x060031FF RID: 12799 RVA: 0x000A927F File Offset: 0x000A747F
			public bool IsBenign()
			{
				return this.m_monitoredError.IsBenign();
			}

			// Token: 0x06003200 RID: 12800 RVA: 0x000A928C File Offset: 0x000A748C
			public bool IsPermanent()
			{
				return this.m_monitoredError.IsPermanent();
			}

			// Token: 0x17000777 RID: 1911
			// (get) Token: 0x06003201 RID: 12801 RVA: 0x000A9299 File Offset: 0x000A7499
			public string ErrorShortName
			{
				get
				{
					return this.m_monitoredError.ErrorShortName;
				}
			}

			// Token: 0x17000778 RID: 1912
			// (get) Token: 0x06003202 RID: 12802 RVA: 0x000A92A6 File Offset: 0x000A74A6
			public ErrorCorrelationId ErrorCorrelationId
			{
				get
				{
					return this.m_monitoredError.ErrorCorrelationId;
				}
			}

			// Token: 0x17000779 RID: 1913
			// (get) Token: 0x06003203 RID: 12803 RVA: 0x000A92B3 File Offset: 0x000A74B3
			// (set) Token: 0x06003204 RID: 12804 RVA: 0x000A92BB File Offset: 0x000A74BB
			public MonitoringScopeId MonitoringScope { get; set; }

			// Token: 0x1700077A RID: 1914
			// (get) Token: 0x06003205 RID: 12805 RVA: 0x000A92C4 File Offset: 0x000A74C4
			// (set) Token: 0x06003206 RID: 12806 RVA: 0x000A92CC File Offset: 0x000A74CC
			public long ErrorEventId { get; set; }

			// Token: 0x1700077B RID: 1915
			// (get) Token: 0x06003207 RID: 12807 RVA: 0x000A92D5 File Offset: 0x000A74D5
			// (set) Token: 0x06003208 RID: 12808 RVA: 0x000A92DD File Offset: 0x000A74DD
			public string ErrorEventName { get; set; }

			// Token: 0x1700077C RID: 1916
			// (get) Token: 0x06003209 RID: 12809 RVA: 0x000A92E6 File Offset: 0x000A74E6
			// (set) Token: 0x0600320A RID: 12810 RVA: 0x000A92EE File Offset: 0x000A74EE
			public long ErrorEventsKitId { get; set; }

			// Token: 0x1700077D RID: 1917
			// (get) Token: 0x0600320B RID: 12811 RVA: 0x000A92F7 File Offset: 0x000A74F7
			// (set) Token: 0x0600320C RID: 12812 RVA: 0x000A92FF File Offset: 0x000A74FF
			public string ErrorEventsKitName { get; set; }

			// Token: 0x0600320D RID: 12813 RVA: 0x0000E609 File Offset: 0x0000C809
			public string ToPrivateString()
			{
				return this.ToString();
			}

			// Token: 0x0600320E RID: 12814 RVA: 0x000A9308 File Offset: 0x000A7508
			public string ToInternalString()
			{
				return this.ToOriginalString();
			}

			// Token: 0x0600320F RID: 12815 RVA: 0x0000E609 File Offset: 0x0000C809
			public string ToOriginalString()
			{
				return this.ToString();
			}

			// Token: 0x04001725 RID: 5925
			private readonly IMonitoredError m_monitoredError;
		}
	}
}
