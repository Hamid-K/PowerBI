using System;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.ConfigurationClasses.Eventing;
using Microsoft.Cloud.Platform.ConfigurationManagement;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.Utils;
using Microsoft.Diagnostics.Tracing;

namespace Microsoft.Cloud.Platform.Eventing.Etw
{
	// Token: 0x020003D9 RID: 985
	[BlockServiceProvider(typeof(IEtwSessionsManager))]
	public sealed class EtwSessionsManager : Block, IEtwSessionsManager
	{
		// Token: 0x06001E50 RID: 7760 RVA: 0x000722F8 File Offset: 0x000704F8
		public EtwSessionsManager()
			: base(typeof(EtwSessionsManager).Name)
		{
			this.m_locker = new object();
			this.m_eventSources = new List<EventSource>();
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x00072328 File Offset: 0x00070528
		protected override BlockInitializationStatus OnInitialize()
		{
			if (base.OnInitialize() == BlockInitializationStatus.PartiallyDone)
			{
				return BlockInitializationStatus.Done;
			}
			this.m_configurationManager = this.m_configurationManagerFactory.GetConfigurationManager();
			this.m_configurationManager.Subscribe(new Type[] { typeof(EtwConfiguration) }, new CcsEventHandler(this.OnConfigurationChange));
			return BlockInitializationStatus.Done;
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x0007237C File Offset: 0x0007057C
		protected override void OnStop()
		{
			this.m_configurationManager.Unsubscribe(new CcsEventHandler(this.OnConfigurationChange));
			base.OnStop();
		}

		// Token: 0x06001E53 RID: 7763 RVA: 0x0007239B File Offset: 0x0007059B
		protected override void OnShutdown()
		{
			if (this.m_shouldCloseSessionOnShutdown)
			{
				EtwSessionsManager.TryCloseSession(this.m_eventsSessionName);
			}
			base.OnShutdown();
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x000723B8 File Offset: 0x000705B8
		public void RegisterEventSource([NotNull] EventSource eventSource)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<EventSource>(eventSource, "eventSource");
			if (this.m_shouldDisableEventsKitOutput)
			{
				return;
			}
			object locker = this.m_locker;
			lock (locker)
			{
				this.m_eventSources.Add(eventSource);
			}
			EtwSession etwSession = this.TryGetProvidersManifestSession();
			EtwSession etwSession2 = this.TryGetEventsSession();
			EtwProviderSynchronizer.EnableEventSource(eventSource, EventLevel.Verbose, etwSession, etwSession2, this.m_eventsKit);
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x00072430 File Offset: 0x00070630
		public void Initialize([NotNull] IEtwSessionsManagerEventsKit eventsKit)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IEtwSessionsManagerEventsKit>(eventsKit, "eventsKit");
			this.m_eventsKit = eventsKit;
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x00072444 File Offset: 0x00070644
		public void NotifyEventWriteFailure(string eventName, string exceptionType, string exceptionMessage, int failureCount)
		{
			ExtendedDiagnostics.EnsureNotNull<IEtwSessionsManagerEventsKit>(this.m_eventsKit, "m_eventsKit");
			this.m_eventsKit.NotifyFailedWritingEvent(eventName, exceptionType, exceptionMessage, failureCount);
		}

		// Token: 0x06001E57 RID: 7767 RVA: 0x00072466 File Offset: 0x00070666
		public static string FormatEventsSessionName(string sessionPrefix, Guid epoch)
		{
			return "{0}_{1}".FormatWithInvariantCulture(new object[]
			{
				sessionPrefix,
				epoch.ToString()
			});
		}

		// Token: 0x06001E58 RID: 7768 RVA: 0x0007248C File Offset: 0x0007068C
		[CanBeNull]
		private EtwSession TryGetProvidersManifestSession()
		{
			object locker = this.m_locker;
			string providersManifestsSessionName;
			lock (locker)
			{
				providersManifestsSessionName = this.m_providersManifestsSessionName;
			}
			if (!string.IsNullOrEmpty(providersManifestsSessionName))
			{
				return EtwSession.FindExisting(providersManifestsSessionName);
			}
			return null;
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x000724E0 File Offset: 0x000706E0
		private EtwSession TryGetEventsSession()
		{
			object locker = this.m_locker;
			string eventsSessionName;
			lock (locker)
			{
				eventsSessionName = this.m_eventsSessionName;
			}
			return EtwSession.FindExisting(eventsSessionName);
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x00072528 File Offset: 0x00070728
		private void OnConfigurationChange(IConfigurationContainer container)
		{
			object locker = this.m_locker;
			lock (locker)
			{
				EtwConfiguration configuration = container.GetConfiguration<EtwConfiguration>();
				this.m_shouldCloseSessionOnShutdown = configuration.ShouldCloseSessionOnShutdown;
				this.m_shouldDisableEventsKitOutput = configuration.DisableEventsKitOutput;
				EtwSession etwSession = this.TryGetEventsSession();
				this.m_epoch = configuration.Epoch;
				string text = ((etwSession == null) ? string.Empty : etwSession.Properties.Name);
				this.m_eventsSessionName = EtwSessionsManager.FormatEventsSessionName(configuration.EventsSessionNamePrefix, this.m_epoch);
				string text2 = Path.Combine(this.m_eventingDirectoriesManager.EventingFilesSourceDirectory, "{0}_{1}_%d.etl".FormatWithInvariantCulture(new object[]
				{
					this.m_eventsSessionName,
					Guid.NewGuid().ToString()
				}));
				EtwSession etwSession2 = EtwSessionsManager.TryStartSession(new SessionCreationProperties(Guid.NewGuid(), this.m_eventsSessionName, text2, SessionKinds.Log)
				{
					FlushTimerPeriodInSeconds = configuration.FlushTimerPeriodInSeconds,
					TimerType = TimerType.QueryPerformanceCounter,
					MaxFileSize = configuration.MaxEventsSessionFileSizeInMb,
					BufferSize = configuration.EventsSessionBufferSizeInKb,
					MaxBuffers = configuration.EventsSessionMaxBuffers,
					SessionFlags = SessionFlags.LocalSequenceNumbers,
					LogFileMode = LogFileMode.Circular
				});
				if (configuration.UseManifestSession)
				{
					this.m_providersManifestsSessionName = configuration.ProvidersManifestSessionName;
					text2 = Path.Combine(this.m_eventingDirectoriesManager.ProvidersManifestDirectory, "{0}_{1}.etl".FormatWithInvariantCulture(new object[]
					{
						this.m_providersManifestsSessionName,
						Guid.NewGuid().ToString()
					}));
					if (EtwSessionsManager.TryStartSession(new SessionCreationProperties(Guid.NewGuid(), this.m_providersManifestsSessionName, text2, SessionKinds.Log)
					{
						FlushTimerPeriodInSeconds = configuration.FlushTimerPeriodInSeconds,
						TimerType = TimerType.QueryPerformanceCounter,
						MaxFileSize = configuration.MaxProvidersManifestSessionFileSizeInMb,
						BufferSize = configuration.EventsSessionBufferSizeInKb,
						MaxBuffers = configuration.EventsSessionMaxBuffers,
						SessionFlags = SessionFlags.LocalSequenceNumbers,
						LogFileMode = LogFileMode.Circular
					}) != null)
					{
						TraceSourceBase<EventingTrace>.Tracer.TraceInformation("Providers Manifest Session '{0}' started or already exists. Output file: '{1}'", new object[] { this.m_providersManifestsSessionName, text2 });
					}
					else
					{
						TraceSourceBase<EventingTrace>.Tracer.TraceInformation("Provider Manifest Session '{0}' is not started.", new object[] { this.m_providersManifestsSessionName });
					}
				}
				if (etwSession2 != null && !string.Equals(text, etwSession2.Properties.Name, StringComparison.OrdinalIgnoreCase))
				{
					if (this.m_eventSources.Count > 0)
					{
						ExtendedDiagnostics.EnsureNotNull<EtwSession>(etwSession, "oldEventsSession");
						foreach (EventSource eventSource in this.m_eventSources)
						{
							EtwProviderSynchronizer.EnableEventSource(eventSource, EventLevel.Verbose, etwSession2, this.m_eventsKit);
						}
					}
					EtwSessionsManager.TryCloseSession(text);
				}
			}
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x00072804 File Offset: 0x00070A04
		private static EtwSession TryStartSession(SessionCreationProperties properties)
		{
			EtwSession etwSession = EtwSession.FindExisting(properties.Name);
			if (etwSession == null)
			{
				try
				{
					etwSession = new EtwSession(properties);
					TraceSourceBase<EventingTrace>.Tracer.TraceInformation("Successfully Started Events Session - Name: '{0}', Path: '{1}', MaxFileSize: '{2}', Flush Timer: '{3}', BufferSize: '{4}', MaxBuffers: '{5}'", new object[] { properties.Name, properties.Path, properties.MaxFileSize, properties.FlushTimerPeriodInSeconds, properties.BufferSize, properties.MaxBuffers });
					return etwSession;
				}
				catch (EtwException ex)
				{
					if (ex.NativeErrorCode == 183)
					{
						etwSession = EtwSession.FindExisting(properties.Name);
					}
					if (etwSession == null)
					{
						TraceSourceBase<EventingTrace>.Tracer.TraceError("Failed starting ETW session - Name: '{0}', Path: '{1}'. Exception: '{2}'", new object[] { properties.Name, properties.Path, ex });
					}
					return etwSession;
				}
			}
			properties = etwSession.Properties;
			TraceSourceBase<EventingTrace>.Tracer.TraceInformation("Events Session already existed - Name: '{0}', Path: '{1}', MaxFileSize: '{2}', Flush Timer: '{3}', BufferSize: '{4}', MaxBuffers: '{5}'".FormatWithInvariantCulture(new object[] { properties.Name, properties.Path, properties.MaxFileSize, properties.FlushTimerPeriodInSeconds, properties.BufferSize, properties.MaxBuffers }));
			return etwSession;
		}

		// Token: 0x06001E5C RID: 7772 RVA: 0x00072958 File Offset: 0x00070B58
		private static bool TryCloseSession(string sessionName)
		{
			EtwSession etwSession = EtwSession.FindExisting(sessionName);
			if (etwSession != null)
			{
				try
				{
					etwSession.Flush();
					etwSession.Stop();
					return true;
				}
				catch (EtwException ex)
				{
					TraceSourceBase<EventingTrace>.Tracer.TraceError("Failed starting ETW session - Name: '{0}'. Exception: '{1}'", new object[] { sessionName, ex });
					return false;
				}
				return false;
			}
			return false;
		}

		// Token: 0x04000A67 RID: 2663
		[BlockServiceDependency]
		private IConfigurationManagerFactory m_configurationManagerFactory;

		// Token: 0x04000A68 RID: 2664
		private IConfigurationManager m_configurationManager;

		// Token: 0x04000A69 RID: 2665
		[BlockServiceDependency]
		private IEventingDirectoriesManager m_eventingDirectoriesManager;

		// Token: 0x04000A6A RID: 2666
		private string m_eventsSessionName;

		// Token: 0x04000A6B RID: 2667
		private string m_providersManifestsSessionName;

		// Token: 0x04000A6C RID: 2668
		private bool m_shouldCloseSessionOnShutdown;

		// Token: 0x04000A6D RID: 2669
		private bool m_shouldDisableEventsKitOutput;

		// Token: 0x04000A6E RID: 2670
		private object m_locker;

		// Token: 0x04000A6F RID: 2671
		private List<EventSource> m_eventSources;

		// Token: 0x04000A70 RID: 2672
		private IEtwSessionsManagerEventsKit m_eventsKit;

		// Token: 0x04000A71 RID: 2673
		private Guid m_epoch;

		// Token: 0x04000A72 RID: 2674
		private const int c_errorCodeSessionAlreadyExists = 183;
	}
}
