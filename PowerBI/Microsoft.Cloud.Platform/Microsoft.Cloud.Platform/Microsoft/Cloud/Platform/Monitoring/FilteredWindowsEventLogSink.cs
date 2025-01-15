using System;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.ConfigurationClasses.Monitoring;
using Microsoft.Cloud.Platform.ConfigurationManagement;
using Microsoft.Cloud.Platform.Eventing;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x02000076 RID: 118
	public class FilteredWindowsEventLogSink : ISink, IDisposable
	{
		// Token: 0x0600037C RID: 892 RVA: 0x0000D3F4 File Offset: 0x0000B5F4
		public FilteredWindowsEventLogSink()
		{
			this.m_monitoredErrorSinkServices = new MonitoredErrorSinkServices();
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000D414 File Offset: 0x0000B614
		internal FilteredWindowsEventLogSink(IMonitoredErrorSinkServices monitoredErrorSinkServices, IMonitoredEventHandler alertIssuer, IMonitoredEventHandler warningIssuer)
		{
			this.m_monitoredErrorSinkServices = monitoredErrorSinkServices;
			this.m_alertIssuerFactory = () => alertIssuer;
			this.m_warningIssuerFactory = () => warningIssuer;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000D474 File Offset: 0x0000B674
		private void CreateNewEventHandler(IConfigurationContainer configurationContainer)
		{
			WindowsEventLogWriterConfiguration configuration = configurationContainer.GetConfiguration<WindowsEventLogWriterConfiguration>();
			WindowsEventLogWriterConfiguration.ValidateConfiguration(configuration);
			ActivityTypeElementIdPairConfigCollection activityTypeElementIdPairConfigCollection = new ActivityTypeElementIdPairConfigCollection(configuration.ActivityElementIdMonitoringThresholdConfigList);
			object eventHandlerLock = this.m_eventHandlerLock;
			lock (eventHandlerLock)
			{
				this.m_alertIssuerFactory = () => new AlertIssuer(EventLogEntryType.Error, configuration.EventLogSourceName);
				this.m_warningIssuerFactory = () => new AlertIssuer(EventLogEntryType.Warning, configuration.EventLogSourceName);
				EventCorrelator eventCorrelator = new EventCorrelator(new HealthStateMachine(this.m_alertIssuerFactory(), this.m_warningIssuerFactory(), TimeSpan.FromMilliseconds(configuration.MinTimeBetweenEventsInMsec), activityTypeElementIdPairConfigCollection), configuration.EventStorageCapacity);
				if (this.m_eventHandler != null)
				{
					this.m_eventHandler.Dispose();
					this.m_eventHandler = null;
				}
				this.m_eventHandler = new EventSorter(eventCorrelator, TimeSpan.FromMilliseconds((double)configuration.EventDelay));
				TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "Monitoring FilteredWindowsEventLog sink has been restarted due to configuration change:  EventDelay={0} EventStorageCapacity={1} MinTimeBetweenEventsInMsec={2} ActivityElementIdMonitoringThresholdConfig={3}", new object[] { configuration.EventDelay, configuration.EventStorageCapacity, configuration.MinTimeBetweenEventsInMsec, activityTypeElementIdPairConfigCollection });
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000D5CC File Offset: 0x0000B7CC
		public void Submit(WireEventBase publishedEvent)
		{
			try
			{
				IMonitoredEventHandlerVisitor monitoredEventHandlerVisitor;
				if (this.TryCreateMonitoredEvent(publishedEvent, out monitoredEventHandlerVisitor))
				{
					TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Verbose, "The following event is about to be handled by the monitoring sink:  EventId = {0} ElementId = {1} ActivityType = {2} ActivityId = {3} RootActivityId = {4}", new object[]
					{
						publishedEvent.Id.EventId,
						publishedEvent.Id.ElementId,
						publishedEvent.Activity.ActivityType,
						publishedEvent.Activity.ActivityId,
						publishedEvent.Activity.RootActivityId
					});
					object eventHandlerLock = this.m_eventHandlerLock;
					lock (eventHandlerLock)
					{
						monitoredEventHandlerVisitor.Visit(this.m_eventHandler);
						this.m_relevantBatch = true;
					}
				}
			}
			catch (MonitoredErrorSinkServicesException ex)
			{
				TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Warning, "An exception occured the monitoring sink converted the following event: {0}  Exception: {1}", new object[] { publishedEvent.Id, ex });
			}
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000D6C8 File Offset: 0x0000B8C8
		public void OnBatchCompleted()
		{
			if (this.m_relevantBatch)
			{
				this.m_eventHandler.OnBatchCompleted();
				this.m_relevantBatch = false;
			}
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000D6E4 File Offset: 0x0000B8E4
		private bool TryCreateMonitoredEvent(WireEventBase publishedEvent, out IMonitoredEventHandlerVisitor monitoredEvent)
		{
			if (MonitoredFlowErrorEvent.TryCreate(publishedEvent, this.m_monitoredErrorSinkServices, out monitoredEvent))
			{
				TraceSourceBase<MonitoringTrace>.Tracer.TraceVerbose("The monitoring sink created a flow error event: {0}", new object[] { monitoredEvent });
				return true;
			}
			if (MonitoredFlowSuccessEvent.TryCreate(publishedEvent, this.m_monitoredErrorSinkServices, out monitoredEvent))
			{
				TraceSourceBase<MonitoringTrace>.Tracer.TraceVerbose("The monitoring sink created a flow success event: {0}", new object[] { monitoredEvent });
				return true;
			}
			if (MonitoredLowLevelErrorEvent.TryCreate(publishedEvent, this.m_monitoredErrorSinkServices, out monitoredEvent))
			{
				TraceSourceBase<MonitoringTrace>.Tracer.TraceVerbose("The monitoring sink created a low-level error event: {0}", new object[] { monitoredEvent });
				return true;
			}
			TraceSourceBase<MonitoringTrace>.Tracer.TraceVerbose("The monitoring sink cannot process the following event: {0}", new object[] { publishedEvent });
			monitoredEvent = null;
			return false;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000D790 File Offset: 0x0000B990
		public void Initialize([NotNull] ISinkServices services, [NotNull] SinkIdentifier sid)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SinkIdentifier>(sid, "sid");
			ExtendedDiagnostics.EnsureArgumentNotNull<ISinkServices>(services, "services");
			if (sid.Parameters.Count > 0)
			{
				throw new InvalidSinkParameterException(sid, "no parameters expected.");
			}
			this.m_sid = sid;
			this.m_monitoredErrorSinkServices.Initialize(services);
			this.m_configurationManager = services.GetConfigManager();
			this.m_configurationManager.Subscribe(new List<Type> { typeof(WindowsEventLogWriterConfiguration) }, new CcsEventHandler(this.CreateNewEventHandler));
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000383 RID: 899 RVA: 0x000034FD File Offset: 0x000016FD
		public SinkProperties Properties
		{
			get
			{
				return SinkProperties.Singleton;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000D818 File Offset: 0x0000BA18
		public SinkIdentifier Id
		{
			get
			{
				return this.m_sid;
			}
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000D820 File Offset: 0x0000BA20
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000D82C File Offset: 0x0000BA2C
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.m_configurationManager != null)
				{
					this.m_configurationManager.Unsubscribe(new CcsEventHandler(this.CreateNewEventHandler));
				}
				object eventHandlerLock = this.m_eventHandlerLock;
				lock (eventHandlerLock)
				{
					if (this.m_eventHandler != null)
					{
						this.m_eventHandler.Dispose();
						this.m_eventHandler = null;
					}
				}
			}
		}

		// Token: 0x0400012E RID: 302
		private readonly IMonitoredErrorSinkServices m_monitoredErrorSinkServices;

		// Token: 0x0400012F RID: 303
		private IMonitoredEventHandler m_eventHandler;

		// Token: 0x04000130 RID: 304
		private bool m_relevantBatch;

		// Token: 0x04000131 RID: 305
		private SinkIdentifier m_sid;

		// Token: 0x04000132 RID: 306
		private IConfigurationManager m_configurationManager;

		// Token: 0x04000133 RID: 307
		private readonly object m_eventHandlerLock = new object();

		// Token: 0x04000134 RID: 308
		private Func<IMonitoredEventHandler> m_alertIssuerFactory;

		// Token: 0x04000135 RID: 309
		private Func<IMonitoredEventHandler> m_warningIssuerFactory;
	}
}
