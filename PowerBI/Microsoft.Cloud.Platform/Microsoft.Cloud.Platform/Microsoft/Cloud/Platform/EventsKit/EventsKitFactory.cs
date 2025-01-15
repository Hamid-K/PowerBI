using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.ConfigurationClasses.EventKitFactory;
using Microsoft.Cloud.Platform.ConfigurationManagement;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.Eventing.Etw;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.Utils;
using Microsoft.Diagnostics.Tracing;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x0200034F RID: 847
	[BlockServiceProvider(typeof(IEventsKitFactory), PublishWhen = BlockServicePublish.Default)]
	public sealed class EventsKitFactory : Block, IEventsKitFactory
	{
		// Token: 0x0600191C RID: 6428 RVA: 0x0005D3E0 File Offset: 0x0005B5E0
		public EventsKitFactory(string name)
			: this(name, EventsKitFactoryOptions.All)
		{
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x0005D3EB File Offset: 0x0005B5EB
		public EventsKitFactory()
			: this(typeof(EventsKitFactory).Name)
		{
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x0005D404 File Offset: 0x0005B604
		public EventsKitFactory(string name, EventsKitFactoryOptions options)
			: base(name)
		{
			this.m_options = options;
			this.m_eventKitsCache = new Dictionary<string, EventsKitBase>();
			this.m_eventSourceCache = new Dictionary<string, EventSource>();
			this.m_assemblyCache = new Dictionary<string, Assembly>();
			this.m_cacheLock = new ReaderWriterLockSlim();
			this.m_configurationChanged = false;
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x0005D454 File Offset: 0x0005B654
		protected override BlockInitializationStatus OnInitialize()
		{
			BlockInitializationStatus blockInitializationStatus = BlockInitializationStatus.PartiallyDone;
			if (base.OnInitialize() == BlockInitializationStatus.Done)
			{
				if (this.m_options.HasFlag(EventsKitFactoryOptions.EmitWindowsEventLogEvents) || this.m_options.HasFlag(EventsKitFactoryOptions.EmitPerformanceCounters))
				{
					this.m_configurationManager = this.m_configurationManagerFactory.GetConfigurationManager();
					this.m_configurationManager.Subscribe(new Type[] { typeof(EventKitFactoryConfiguration) }, new CcsEventHandler(this.OnConfigurationChange));
				}
				this.m_elementId = this.m_elementMgr.GetElementInstanceId();
				this.m_performanceCounterElementName = CurrentProcess.Name;
				if (this.m_options.HasFlag(EventsKitFactoryOptions.EmitEtwEvents))
				{
					IEtwSessionsManagerEventsKit eventsKitInstance = EventsKitFactoryUtils.CreateGeneratedEventsKitInstances<IEtwSessionsManagerEventsKit>(this.m_options & ~EventsKitFactoryOptions.EmitEtwEvents, this.m_elementId, null, this.m_packageMgr, this.m_performanceCounterElementName, EventsKitType.Production, this.m_eventingServer, this.m_etwSessionsManager, this.m_eventKitFactoryConfiguration).EventsKitInstance;
					this.m_etwSessionsManager.Initialize(eventsKitInstance);
				}
				blockInitializationStatus = BlockInitializationStatus.Done;
			}
			return blockInitializationStatus;
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x0005D556 File Offset: 0x0005B756
		protected override void OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x0005D560 File Offset: 0x0005B760
		protected override void OnStop()
		{
			if (this.m_options.HasFlag(EventsKitFactoryOptions.EmitWindowsEventLogEvents) || this.m_options.HasFlag(EventsKitFactoryOptions.EmitPerformanceCounters))
			{
				this.m_configurationManager.Unsubscribe(new CcsEventHandler(this.OnConfigurationChange));
			}
			base.OnStop();
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x0005D5BA File Offset: 0x0005B7BA
		public T CreateEventsKit<T>() where T : class
		{
			return this.CreateEventsKit<T>(this.m_elementId, null, this.m_packageMgr, this.m_performanceCounterElementName);
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x0005D5D5 File Offset: 0x0005B7D5
		public T CreateEventsKit<T>(ActivityType activityType) where T : class
		{
			return this.CreateEventsKit<T>(this.m_elementId, activityType, this.m_packageMgr, this.m_performanceCounterElementName + "." + activityType.ShortName);
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x0005D600 File Offset: 0x0005B800
		public T CreateEventsKit<T>(string performanceCountersInstanceName, PerformanceCounterPrefixSetting performanceCounterPrefixSetting) where T : class
		{
			string text = null;
			if (performanceCounterPrefixSetting != PerformanceCounterPrefixSetting.NoPrefix)
			{
				if (performanceCounterPrefixSetting != PerformanceCounterPrefixSetting.ElementName)
				{
					ExtendedDiagnostics.EnsureInvalidSwitchValue<PerformanceCounterPrefixSetting>(performanceCounterPrefixSetting);
				}
				else
				{
					text = this.m_performanceCounterElementName + ".";
				}
			}
			else
			{
				text = string.Empty;
			}
			return this.CreateEventsKit<T>(this.m_elementId, null, this.m_packageMgr, text + performanceCountersInstanceName);
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x0005D654 File Offset: 0x0005B854
		private T CreateEventsKit<T>(ElementId element, ActivityType activityType, IPackageManager packageMgr, string performanceCountersInstanceName) where T : class
		{
			Type typeFromHandle = typeof(T);
			string text = typeFromHandle + "." + performanceCountersInstanceName;
			object cacheLock = this.m_cacheLock;
			EventsKitBase eventsKitBase;
			lock (cacheLock)
			{
				if (!this.m_eventKitsCache.TryGetValue(text, out eventsKitBase))
				{
					Assembly assembly = null;
					if (!this.m_assemblyCache.TryGetValue(typeFromHandle.AssemblyQualifiedName, out assembly))
					{
						GeneratedEventsKitInstances<T> generatedEventsKitInstances = EventsKitFactoryUtils.CreateGeneratedEventsKitInstances<T>(this.m_options, element, activityType, packageMgr, performanceCountersInstanceName, EventsKitType.Production, this.m_eventingServer, this.m_etwSessionsManager, this.m_eventKitFactoryConfiguration);
						eventsKitBase = generatedEventsKitInstances.EventsKitInstance as EventsKitBase;
						this.m_assemblyCache.Add(typeFromHandle.AssemblyQualifiedName, eventsKitBase.GetType().Assembly);
						if (this.m_options.HasFlag(EventsKitFactoryOptions.EmitEtwEvents))
						{
							this.m_eventSourceCache.Add(typeFromHandle.AssemblyQualifiedName, generatedEventsKitInstances.EventSourceInstance);
						}
					}
					else
					{
						EventSource eventSource = null;
						if (this.m_options.HasFlag(EventsKitFactoryOptions.EmitEtwEvents))
						{
							eventSource = this.m_eventSourceCache[typeFromHandle.AssemblyQualifiedName];
						}
						eventsKitBase = EventsKitFactoryUtils.CreateEventsKitInstance<T>(assembly, element, activityType, packageMgr, performanceCountersInstanceName, EventsKitType.Production, this.m_eventingServer, this.m_eventKitFactoryConfiguration, eventSource) as EventsKitBase;
						TraceSourceBase<EventingTrace>.Tracer.TraceInformation("Created eventskit {0} instance name={1}, (activity: {2})", new object[]
						{
							typeFromHandle.Name,
							performanceCountersInstanceName,
							(activityType != null) ? activityType.ShortName : "N/A"
						});
					}
					if (eventsKitBase == null)
					{
						throw new EventsKitCreationFailedException(typeFromHandle.Name, typeFromHandle.Assembly.GetName().Name, "Cannot create the events kit");
					}
					this.m_eventKitsCache.Add(text, eventsKitBase);
				}
			}
			return eventsKitBase as T;
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x0005D834 File Offset: 0x0005BA34
		private void OnConfigurationChange(IConfigurationContainer configurationContainer)
		{
			this.m_configurationChanged = true;
			this.m_eventKitFactoryConfiguration = configurationContainer.GetConfiguration<EventKitFactoryConfiguration>();
			if (this.m_options.HasFlag(EventsKitFactoryOptions.EmitWindowsEventLogEvents))
			{
				ExtendedDiagnostics.EnsureStringNotNullOrEmpty(this.m_eventKitFactoryConfiguration.EventLogSourceName, "m_eventKitFactoryConfiguration.EventLogSourceName");
			}
		}

		// Token: 0x040008A2 RID: 2210
		private ElementId m_elementId;

		// Token: 0x040008A3 RID: 2211
		private string m_performanceCounterElementName;

		// Token: 0x040008A4 RID: 2212
		private bool m_configurationChanged;

		// Token: 0x040008A5 RID: 2213
		private EventKitFactoryConfiguration m_eventKitFactoryConfiguration;

		// Token: 0x040008A6 RID: 2214
		private EventsKitFactoryOptions m_options;

		// Token: 0x040008A7 RID: 2215
		private Dictionary<string, EventsKitBase> m_eventKitsCache;

		// Token: 0x040008A8 RID: 2216
		private Dictionary<string, Assembly> m_assemblyCache;

		// Token: 0x040008A9 RID: 2217
		private Dictionary<string, EventSource> m_eventSourceCache;

		// Token: 0x040008AA RID: 2218
		private object m_cacheLock;

		// Token: 0x040008AB RID: 2219
		[BlockServiceDependency]
		private IElementInstanceId m_elementMgr;

		// Token: 0x040008AC RID: 2220
		[BlockServiceDependency]
		private IPackageManager m_packageMgr;

		// Token: 0x040008AD RID: 2221
		[BlockServiceDependency]
		private IEventingServer m_eventingServer;

		// Token: 0x040008AE RID: 2222
		[BlockServiceDependency]
		private readonly IConfigurationManagerFactory m_configurationManagerFactory;

		// Token: 0x040008AF RID: 2223
		private IConfigurationManager m_configurationManager;

		// Token: 0x040008B0 RID: 2224
		[BlockServiceDependency]
		private IEtwSessionsManager m_etwSessionsManager;
	}
}
