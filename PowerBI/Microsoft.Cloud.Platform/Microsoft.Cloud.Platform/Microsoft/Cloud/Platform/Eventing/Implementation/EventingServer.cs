using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.ConfigurationClasses.Eventing;
using Microsoft.Cloud.Platform.ConfigurationManagement;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Eventing.Implementation
{
	// Token: 0x020003AF RID: 943
	[BlockServiceProvider(typeof(IEventingServerControl))]
	[BlockServiceProvider(typeof(ISinkServices))]
	[BlockServiceProvider(typeof(IEventingServer))]
	public sealed class EventingServer : Block, IEventingServerControl, ISinkServices, IEventingServer
	{
		// Token: 0x06001D28 RID: 7464 RVA: 0x0006F2D8 File Offset: 0x0006D4D8
		public EventingServer()
			: this(typeof(EventingServer).Name)
		{
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x0006F2F0 File Offset: 0x0006D4F0
		public EventingServer(string name)
			: base(name)
		{
			this.m_sinks = new List<ISink>();
			this.m_sinkFactories = new SinkFactoryManager();
			this.m_sinkFactories.Register(new ReflectionSinkFactory());
			this.m_singletonSinks = new SingletonSinkManager();
			this.m_enabledEventTypes = EventPurpose.Testability;
			this.m_configurationLock = new object();
			this.m_inLoadMode = false;
			this.m_loadModeHighWatermark = 300000;
			this.m_loadModeLowWatermark = 100000;
		}

		// Token: 0x06001D2A RID: 7466 RVA: 0x0006F370 File Offset: 0x0006D570
		protected override BlockInitializationStatus OnInitialize()
		{
			BlockInitializationStatus blockInitializationStatus = BlockInitializationStatus.PartiallyDone;
			if (base.OnInitialize() == BlockInitializationStatus.Done)
			{
				this.m_eventsKitExplorer = this.m_ekExplorerFactory.Create();
				this.m_batchedQueue = new BatchedAsyncEvent<WireEventBase>(base.Name, this.m_maxMillisecondsBetweenBatches, 100, BatchedAsyncEventOptions.FireEmptyBatch);
				this.m_configurationManager = this.m_configurationManagerFactory.GetConfigurationManager();
				this.m_configurationManager.Subscribe(new List<Type> { typeof(EventingConfiguration) }, new CcsEventHandler(this.OnConfigurationChangeNotification));
				this.ChangeConfigurationIfRequired();
				this.m_batchedQueue.Subscribe(this, new EventHandler<EventBatchEventArgs<WireEventBase>>(this.DeliverEventsBatch), delegate
				{
					this.m_batchedQueue.Unsubscribe(this);
				}, base.WorkTicketManager.CreateWorkTicket(this));
				blockInitializationStatus = BlockInitializationStatus.Done;
			}
			return blockInitializationStatus;
		}

		// Token: 0x06001D2B RID: 7467 RVA: 0x0006F42B File Offset: 0x0006D62B
		protected override void OnStop()
		{
			this.m_configurationManager.Unsubscribe(new CcsEventHandler(this.OnConfigurationChangeNotification));
			base.OnStop();
		}

		// Token: 0x06001D2C RID: 7468 RVA: 0x0006F44A File Offset: 0x0006D64A
		protected override void OnShutdown()
		{
			this.m_configurationManager = null;
			this.m_singletonSinks = null;
			this.m_sinkFactories = null;
			this.m_configurationLock = null;
			this.m_sinks = null;
			base.OnShutdown();
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x0006F478 File Offset: 0x0006D678
		private void OnConfigurationChangeNotification(IConfigurationContainer configurationContainer)
		{
			object configurationLock = this.m_configurationLock;
			lock (configurationLock)
			{
				EventingConfiguration configuration = configurationContainer.GetConfiguration<EventingConfiguration>();
				this.m_pendingNewConfiguration = configuration;
			}
		}

		// Token: 0x06001D2E RID: 7470 RVA: 0x0006F4C0 File Offset: 0x0006D6C0
		private void ChangeConfigurationIfRequired()
		{
			EventingConfiguration eventingConfiguration = null;
			object configurationLock = this.m_configurationLock;
			lock (configurationLock)
			{
				eventingConfiguration = this.m_pendingNewConfiguration;
				this.m_pendingNewConfiguration = null;
			}
			if (eventingConfiguration != null)
			{
				TraceSourceBase<EventingTrace>.Tracer.TraceInformation("Server manager implementing new config");
				int maxMillisecondsBetweenBatches = this.m_maxMillisecondsBetweenBatches;
				this.m_maxMillisecondsBetweenBatches = eventingConfiguration.MaxMillisecondsBetweenBatches;
				if (this.m_batchedQueue != null && this.m_maxMillisecondsBetweenBatches != maxMillisecondsBetweenBatches)
				{
					this.m_batchedQueue.UpdateTimerPeriod(this.m_maxMillisecondsBetweenBatches);
				}
				this.m_enabledEventTypes = eventingConfiguration.EnabledEventTypesConfiguration.EnabledEventTypes;
				IEnumerable<SinkIdentifier> sidsFromConfig = eventingConfiguration.EnabledSinksConfiguration.Select((SinkConfiguration sinkConfig) => new SinkIdentifier(typeof(ReflectionSinkFactory).FullName, sinkConfig.Assembly, sinkConfig.Type, new SinkParametersCollection(new Dictionary<string, string>())));
				IEnumerable<SinkIdentifier> existingSids = this.m_sinks.Select((ISink sink) => sink.Id);
				foreach (SinkIdentifier sinkIdentifier in sidsFromConfig.Where((SinkIdentifier sid) => !existingSids.Contains(sid)).ToList<SinkIdentifier>())
				{
					this.AddSink(sinkIdentifier);
				}
				foreach (SinkIdentifier sinkIdentifier2 in existingSids.Where((SinkIdentifier sid) => !sidsFromConfig.Contains(sid)))
				{
					this.RemoveSink(sinkIdentifier2);
				}
				this.m_inLoadMode = false;
				this.m_loadModeHighWatermark = ((eventingConfiguration.LoadModeHighWatermark > 0) ? eventingConfiguration.LoadModeHighWatermark : 300000);
				this.m_loadModeLowWatermark = ((eventingConfiguration.LoadModeLowWatermark > 0) ? eventingConfiguration.LoadModeLowWatermark : 100000);
				TraceSourceBase<EventingTrace>.Tracer.TraceInformation("Server manager finished implementing new config");
			}
		}

		// Token: 0x06001D2F RID: 7471 RVA: 0x0006F6CC File Offset: 0x0006D8CC
		public void SubmitEvent(WireEventBase evt)
		{
			IEventMetadata eventMetadata = this.m_eventsKitExplorer.GetEventMetadata(evt.Id.EventId);
			if (this.m_inLoadMode)
			{
				EventPurpose eventPurpose = EventPurpose.Monitoring | EventPurpose.Audit | EventPurpose.Reporting;
				if ((eventMetadata.EventTypes & eventPurpose) == EventPurpose.None)
				{
					TraceSourceBase<EventingTrace>.Tracer.TraceVerbose("Event {0} was dropped due to load mode", new object[] { evt });
					return;
				}
			}
			if ((this.m_enabledEventTypes & eventMetadata.EventTypes) == EventPurpose.None)
			{
				TraceSourceBase<EventingTrace>.Tracer.TraceVerbose("Event {0} was dropped because it is not of an enabled type. Enabled types are {1}. Event types are {2}", new object[] { evt, this.m_enabledEventTypes, eventMetadata.EventTypes });
				return;
			}
			Interlocked.Increment(ref this.m_numPendingEvents);
			this.m_batchedQueue.FireEvent(evt);
		}

		// Token: 0x06001D30 RID: 7472 RVA: 0x0006F77F File Offset: 0x0006D97F
		public void RegisterSinkFactory([NotNull] ISinkFactory factory)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<ISinkFactory>(factory, "factory");
			this.m_sinkFactories.Register(factory);
		}

		// Token: 0x06001D31 RID: 7473 RVA: 0x0006F798 File Offset: 0x0006D998
		public void UnregisterSinkFactory([NotNull] ISinkFactory factory)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<ISinkFactory>(factory, "factory");
			this.m_sinkFactories.Unregister(factory);
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x0006F7B1 File Offset: 0x0006D9B1
		public IEventsKitExplorer GetEventsKitExplorer()
		{
			return this.m_eventsKitExplorer;
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x0006F7B9 File Offset: 0x0006D9B9
		public IConfigurationManager GetConfigManager()
		{
			return this.m_configurationManager;
		}

		// Token: 0x06001D34 RID: 7476 RVA: 0x0006F7C4 File Offset: 0x0006D9C4
		private void DeliverEventsBatch(object sender, [NotNull] EventBatchEventArgs<WireEventBase> events)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<EventBatchEventArgs<WireEventBase>>(events, "events");
			ExtendedDiagnostics.EnsureNotNull<ReadOnlyCollection<WireEventBase>>(events.Data, "events data");
			this.ChangeConfigurationIfRequired();
			int count = events.Data.Count;
			long num = Interlocked.Add(ref this.m_numPendingEvents, (long)(-(long)count));
			bool inLoadMode = this.m_inLoadMode;
			this.m_inLoadMode = EventingServer.IsInLoadMode(this.m_inLoadMode, num, this.m_loadModeHighWatermark, this.m_loadModeLowWatermark);
			if (inLoadMode != this.m_inLoadMode)
			{
				if (this.m_inLoadMode)
				{
					TraceSourceBase<EventingTrace>.Tracer.TraceWarning("Identified load in Eventing Server, set enabled events to monitoring & reporting only. Num pending events: {0}", new object[] { num });
					new WindowsEventLogWriter(WindowsEventLogConstants.DefaultEventLogSourceName).WriteEntry("Identified load in Eventing Server, set enabled events to monitoring & reporting only. Num pending events: {0}".FormatWithInvariantCulture(new object[] { num }), EventLogEntryType.Warning, 9990);
				}
				else
				{
					TraceSourceBase<EventingTrace>.Tracer.TraceInformation("Out of load in Eventing Server Queue. Num pending events: {0}", new object[] { num });
				}
			}
			if (this.m_sinks.None<ISink>())
			{
				TraceSourceBase<EventingTrace>.Tracer.TraceVerbose("No sinks configured for eventing server yet. Doing nothing");
				return;
			}
			IEnumerable<WireEventBase> data = events.Data;
			if (data.None<WireEventBase>())
			{
				return;
			}
			SinkCollector sinkCollector = new SinkCollector();
			foreach (WireEventBase wireEventBase in data)
			{
				this.Submit(wireEventBase, sinkCollector);
			}
			sinkCollector.ForEach(delegate(ISink sink)
			{
				TopLevelHandler.Run(this, TopLevelHandlerOption.SwallowNonfatal, delegate
				{
					sink.OnBatchCompleted();
				});
			});
			TraceSourceBase<EventingTrace>.Tracer.TraceVerbose("Eventing Server: consumed = {0}", new object[] { this.m_eventsConsumed });
		}

		// Token: 0x06001D35 RID: 7477 RVA: 0x0006F958 File Offset: 0x0006DB58
		private void Submit(WireEventBase e, SinkCollector participatingSinks)
		{
			SinkCollector sinkCollector = new SinkCollector();
			foreach (ISink sink in this.m_sinks)
			{
				SinkIdentifier id = sink.Id;
				if (!sinkCollector.Has(id))
				{
					try
					{
						sink.Submit(e);
						if ((sink.Properties & SinkProperties.Singleton) != SinkProperties.None)
						{
							sinkCollector.Collect(sink);
						}
						participatingSinks.Collect(sink);
					}
					catch (Exception ex)
					{
						TraceSourceBase<EventingTrace>.Tracer.Trace(TraceVerbosity.Fatal, "Server:Sink {0} threw exception {1} on event submission. Process will terminate", new object[] { id, ex });
						throw;
					}
				}
			}
			this.m_eventsConsumed += 1L;
		}

		// Token: 0x06001D36 RID: 7478 RVA: 0x0006FA1C File Offset: 0x0006DC1C
		private void AddSink(SinkIdentifier sid)
		{
			try
			{
				ISink sink = this.m_singletonSinks.Find(sid);
				if (sink == null)
				{
					TraceSourceBase<EventingTrace>.Tracer.Trace(TraceVerbosity.Info, "Server: Creating sink {0}", new object[] { sid });
					sink = this.m_sinkFactories.Create(sid);
					sink.Initialize(this, sid);
				}
				if ((sink.Properties & SinkProperties.Singleton) != SinkProperties.None)
				{
					this.m_singletonSinks.Add(sink);
					sink = new SinkProxy(sink);
				}
				TraceSourceBase<EventingTrace>.Tracer.Trace(TraceVerbosity.Info, "Server: Adding sink {0}", new object[] { sid });
				this.m_sinks.Add(sink);
			}
			catch (SinkFactoryNotFoundException ex)
			{
				TraceSourceBase<EventingTrace>.Tracer.Trace(TraceVerbosity.Error, "Server:Sink {0} cannot be initialized because it's factory is not registered: {1}. It will be ignored", new object[] { sid, ex });
			}
			catch (SinkNotFoundException ex2)
			{
				TraceSourceBase<EventingTrace>.Tracer.Trace(TraceVerbosity.Error, "Server:Sink {0} cannot be created: {1}. It will be ignored", new object[] { sid, ex2 });
			}
			catch (InvalidSinkParameterException ex3)
			{
				TraceSourceBase<EventingTrace>.Tracer.Trace(TraceVerbosity.Error, "Server:Sink {0} cannot be initialized: {1}. It will be ignored", new object[] { sid, ex3 });
			}
		}

		// Token: 0x06001D37 RID: 7479 RVA: 0x0006FB38 File Offset: 0x0006DD38
		private void RemoveSink(SinkIdentifier sid)
		{
			TraceSourceBase<EventingTrace>.Tracer.Trace(TraceVerbosity.Info, "Server: Removing sink {0}", new object[] { sid });
			ISink sink = this.m_sinks.Where((ISink s) => s.Id.Equals(sid)).First<ISink>();
			this.m_sinks.Remove(sink);
			if ((sink.Properties & SinkProperties.Singleton) != SinkProperties.None)
			{
				if (this.m_singletonSinks.Remove(sink) == 0)
				{
					SinkProxy sinkProxy = (SinkProxy)sink;
					this.m_sinkFactories.Destroy(sinkProxy.EmbeddedSink);
				}
			}
			else
			{
				this.m_sinkFactories.Destroy(sink);
			}
			TraceSourceBase<EventingTrace>.Tracer.Trace(TraceVerbosity.Info, "Server: Removed sink {0}", new object[] { sid });
		}

		// Token: 0x06001D38 RID: 7480 RVA: 0x0006FBF8 File Offset: 0x0006DDF8
		private static bool IsInLoadMode(bool isCurrentlyInLoadMode, long numPendingEvents, int highWatermark, int lowWatermark)
		{
			return numPendingEvents >= (long)highWatermark || (numPendingEvents > (long)lowWatermark && isCurrentlyInLoadMode);
		}

		// Token: 0x040009D1 RID: 2513
		[AutoShuttable]
		private BatchedAsyncEvent<WireEventBase> m_batchedQueue;

		// Token: 0x040009D2 RID: 2514
		private EventPurpose m_enabledEventTypes;

		// Token: 0x040009D3 RID: 2515
		private List<ISink> m_sinks;

		// Token: 0x040009D4 RID: 2516
		private SinkFactoryManager m_sinkFactories;

		// Token: 0x040009D5 RID: 2517
		private SingletonSinkManager m_singletonSinks;

		// Token: 0x040009D6 RID: 2518
		private const int c_maxNumEventsInBatch = 100;

		// Token: 0x040009D7 RID: 2519
		private int m_maxMillisecondsBetweenBatches = 3000;

		// Token: 0x040009D8 RID: 2520
		private IEventsKitExplorer m_eventsKitExplorer;

		// Token: 0x040009D9 RID: 2521
		private IConfigurationManager m_configurationManager;

		// Token: 0x040009DA RID: 2522
		[BlockServiceDependency]
		private IEventsKitExplorerFactory m_ekExplorerFactory;

		// Token: 0x040009DB RID: 2523
		[BlockServiceDependency]
		private IConfigurationManagerFactory m_configurationManagerFactory;

		// Token: 0x040009DC RID: 2524
		private long m_eventsConsumed;

		// Token: 0x040009DD RID: 2525
		private object m_configurationLock;

		// Token: 0x040009DE RID: 2526
		private EventingConfiguration m_pendingNewConfiguration;

		// Token: 0x040009DF RID: 2527
		private int m_loadModeHighWatermark;

		// Token: 0x040009E0 RID: 2528
		private int m_loadModeLowWatermark;

		// Token: 0x040009E1 RID: 2529
		private long m_numPendingEvents;

		// Token: 0x040009E2 RID: 2530
		private bool m_inLoadMode;

		// Token: 0x040009E3 RID: 2531
		private const int c_highWatermarkDefault = 300000;

		// Token: 0x040009E4 RID: 2532
		private const int c_lowWatermarkDefault = 100000;
	}
}
