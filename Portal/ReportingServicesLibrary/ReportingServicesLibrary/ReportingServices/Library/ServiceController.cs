using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Exceptions;
using Microsoft.ReportingServices.Extensions;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000232 RID: 562
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	internal sealed class ServiceController : IEventResolver, IDisposable
	{
		// Token: 0x06001463 RID: 5219 RVA: 0x0004E450 File Offset: 0x0004C650
		public ServiceController(IServiceAppDomainController appDomainController)
		{
			this.m_appDomainController = appDomainController;
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001464 RID: 5220 RVA: 0x0004E4A3 File Offset: 0x0004C6A3
		// (set) Token: 0x06001465 RID: 5221 RVA: 0x0004E4AA File Offset: 0x0004C6AA
		public static ServiceController Current
		{
			get
			{
				return ServiceController.m_currentService;
			}
			set
			{
				ServiceController.m_currentService = value;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001466 RID: 5222 RVA: 0x0004E4B2 File Offset: 0x0004C6B2
		public bool ServiceStarted
		{
			get
			{
				return this.m_serviceState == ServiceController.CurrentRunningState.AllStarted;
			}
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x0004E4C0 File Offset: 0x0004C6C0
		public void StartService(bool firstStart)
		{
			try
			{
				Sku.GetInstalledSku(Globals.Configuration.InstanceID);
				this.m_startThread = new Thread(new ParameterizedThreadStart(this.ServiceStartThread));
				this.m_startThread.Start(firstStart);
				this.m_serviceState = ServiceController.CurrentRunningState.AllStarted;
			}
			catch (Exception ex)
			{
				this.m_serviceState = ServiceController.CurrentRunningState.None;
				if (this.m_tracer.TraceError)
				{
					this.m_tracer.Trace(TraceLevel.Error, "StartService: Error Starting Service: {0}", new object[] { ex.ToString() });
				}
			}
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x0004E558 File Offset: 0x0004C758
		internal void StartDatabaseVersionCheckTimer()
		{
			object timersSync = this.m_timersSync;
			lock (timersSync)
			{
				DatabaseVersionCheckTimer databaseVersionCheckTimer = new DatabaseVersionCheckTimer();
				if (!this.FoundTimer(databaseVersionCheckTimer.GetType()))
				{
					databaseVersionCheckTimer.Start(60, "Database version check");
					this.m_timers.Add(databaseVersionCheckTimer);
				}
			}
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x0004E5C0 File Offset: 0x0004C7C0
		private void StartTimers()
		{
			if (this.m_startThreadExitStarted)
			{
				return;
			}
			object obj = this.m_timersSync;
			lock (obj)
			{
				DatabaseCleanupTimer databaseCleanupTimer = new DatabaseCleanupTimer();
				if (!this.FoundTimer(databaseCleanupTimer.GetType()))
				{
					databaseCleanupTimer.Start(Globals.Configuration.CleanupCycleMinutes * 60, "Database Cleanup (NT Service)");
					this.m_timers.Add(databaseCleanupTimer);
				}
			}
			if (this.m_startThreadExitStarted)
			{
				return;
			}
			if (this.m_startThreadExitStarted)
			{
				return;
			}
			obj = this.m_timersSync;
			lock (obj)
			{
				RunningRequestsScavenger runningRequestsScavenger = new RunningRequestsScavenger();
				if (!this.FoundTimer(runningRequestsScavenger.GetType()))
				{
					runningRequestsScavenger.Start(Globals.Configuration.RunningRequestsScavengerCycle, "Running Requests Scavenger");
					this.m_timers.Add(runningRequestsScavenger);
				}
				if (this.m_startThreadExitStarted)
				{
					return;
				}
				RunningJobDbTimer runningJobDbTimer = new RunningJobDbTimer();
				if (!this.FoundTimer(runningJobDbTimer.GetType()))
				{
					runningJobDbTimer.Start(Globals.Configuration.RunningRequestsDBCycle, "Running Requests DB");
					this.m_timers.Add(runningJobDbTimer);
				}
				if (this.m_startThreadExitStarted)
				{
					return;
				}
				ExecutionLogEntryExpirer executionLogEntryExpirer = new ExecutionLogEntryExpirer();
				if (!this.FoundTimer(executionLogEntryExpirer.GetType()))
				{
					executionLogEntryExpirer.Start(ExecutionLogEntryExpirer.SecondsToNextEvent, 86400, "Execution Log Entry Expiration");
					this.m_timers.Add(executionLogEntryExpirer);
				}
				DatabaseUsernameSIDRefreshTimer databaseUsernameSIDRefreshTimer = new DatabaseUsernameSIDRefreshTimer();
				if (!this.FoundTimer(databaseUsernameSIDRefreshTimer.GetType()))
				{
					databaseUsernameSIDRefreshTimer.Start(TimeSpan.FromMinutes((double)Globals.Configuration.UsernameSIDRefreshMinutesParam).Seconds, "Username refresh based on the SID stored in the database");
					this.m_timers.Add(databaseUsernameSIDRefreshTimer);
				}
				UpdatePoliciesTimer updatePoliciesTimer = new UpdatePoliciesTimer();
				if (!this.FoundTimer(databaseUsernameSIDRefreshTimer.GetType()))
				{
					updatePoliciesTimer.Start(Globals.Configuration.UpdatePolicySecondsParam, "Update Policies");
					this.m_timers.Add(updatePoliciesTimer);
				}
			}
			if (this.m_startThreadExitStarted)
			{
				return;
			}
			if (!Sku.IsFeatureEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.NoMemoryThrottling))
			{
				obj = this.m_timersSync;
				lock (obj)
				{
					MemoryStatsTimer memoryStatsTimer = new MemoryStatsTimer();
					if (!this.FoundTimer(memoryStatsTimer.GetType()))
					{
						memoryStatsTimer.Start(ResourceUtilities.UpdateStatsTimerCycle, "Memory stats update");
						this.m_timers.Add(memoryStatsTimer);
					}
				}
			}
			bool startThreadExitStarted = this.m_startThreadExitStarted;
			return;
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x0004E860 File Offset: 0x0004CA60
		private void StopTimers()
		{
			object timersSync = this.m_timersSync;
			lock (timersSync)
			{
				foreach (TimerActionBase timerActionBase in this.m_timers)
				{
					timerActionBase.Stop();
					((IDisposable)timerActionBase).Dispose();
				}
			}
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x0004E8E0 File Offset: 0x0004CAE0
		private bool FoundTimer(Type timerType)
		{
			bool flag = false;
			using (List<TimerActionBase>.Enumerator enumerator = this.m_timers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.GetType() == timerType.GetType())
					{
						flag = true;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x0004E944 File Offset: 0x0004CB44
		private void SetAppDomainPolicy()
		{
			try
			{
				PolicyLevel policyLevel = SecurityManager.LoadPolicyLevelFromString(File.ReadAllText(Globals.Configuration.PolicyLevel), PolicyLevelType.AppDomain);
				AppDomain.CurrentDomain.SetAppDomainPolicy(policyLevel);
			}
			catch (Exception ex)
			{
				if (this.m_tracer.TraceError)
				{
					this.m_tracer.Trace(TraceLevel.Error, "Exception caught loading and setting code permissions policy level: {0}", new object[] { ex.ToString() });
				}
				throw;
			}
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x0004E9B4 File Offset: 0x0004CBB4
		public static void ItemPlacedInNotificationQueue()
		{
			if (ServiceController.Current != null && ServiceController.Current.m_notificationWorker != null)
			{
				ServiceController.Current.m_notificationWorker.ItemPlacedInQueue();
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x0600146E RID: 5230 RVA: 0x0004E9D8 File Offset: 0x0004CBD8
		public bool StillProcessing
		{
			get
			{
				if (this.m_eventWorker != null && this.m_eventWorker.IsStillWorking)
				{
					return true;
				}
				if (this.m_notificationWorker != null && this.m_notificationWorker.IsStillWorking)
				{
					return true;
				}
				if (this.m_scheduleWorker != null && this.m_scheduleWorker.IsStillWorking)
				{
					return true;
				}
				if (this.m_upgradeWorker != null && this.m_upgradeWorker.IsStillWorking)
				{
					return true;
				}
				if (this.m_timers != null)
				{
					if (this.m_timers.Exists((TimerActionBase t) => t.IsExecuting))
					{
						return true;
					}
				}
				return ReportServerThreadPool.HasRunningReportThreads;
			}
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x0004EA84 File Offset: 0x0004CC84
		public IEventHandler ResolveEvent(string eventType)
		{
			IEventHandler eventHandler;
			if (eventType == InternalEvents.DataDrivenSubscription.ToString())
			{
				eventHandler = new DataDrivenSubscriptionHandler();
			}
			else if (eventType == InternalEvents.SharedSchedule.ToString())
			{
				eventHandler = new ScheduleFiredEventHandler();
			}
			else if (eventType == InternalEvents.ReportHistorySchedule.ToString())
			{
				eventHandler = new HistorySnapShotScheduleEventHandler();
			}
			else if (eventType == InternalEvents.CacheInvalidateSchedule.ToString())
			{
				eventHandler = new InvalidateCacheScheduleEventHandler();
			}
			else if (eventType == InternalEvents.ReportExecutionUpdateSchedule.ToString())
			{
				eventHandler = new ReportExecutionSnapshotScheduleEventHandler();
			}
			else
			{
				eventHandler = ExtensionClassFactory.GetEventHandlerByEventType(eventType) as IEventHandler;
			}
			return eventHandler;
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x0004EB40 File Offset: 0x0004CD40
		public void ItemPlacedInEventQueue()
		{
			ServiceController serviceController = ServiceController.Current;
			if (serviceController != null && serviceController.m_eventWorker != null)
			{
				serviceController.m_eventWorker.ItemPlacedInQueue();
			}
			ServiceController.ItemPlacedInNotificationQueue();
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001471 RID: 5233 RVA: 0x0004EB6E File Offset: 0x0004CD6E
		public bool IsServiceWorking
		{
			get
			{
				return !this.m_serviceStartThreadExited || (this.m_pollingService != null && Global.IsInitialized() && this.m_pollingService.IsPollingWorking);
			}
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x0004EB98 File Offset: 0x0004CD98
		private void ServiceStartThread(object firstStart)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			Exception ex = null;
			try
			{
				do
				{
					flag = false;
					ConnectionManager connectionManager = null;
					try
					{
						connectionManager = new ConnectionManager();
						connectionManager.WillDisconnectStorage();
						connectionManager.VerifyConnectionAndDbVersion();
						connectionManager.EnsureDBCmptLevel();
						ServiceController.EnsureConfigurationFromDB();
						if (!flag3)
						{
							this.SetAppDomainPolicy();
							flag3 = true;
						}
						this.StartDatabaseVersionCheckTimer();
						this.PreparePollingMaintenance();
						this.StartAllWorkers();
						ex = null;
					}
					catch (Exception ex2)
					{
						if (ExceptionUtils.IsStoppingException(ex2))
						{
							if (ex2 is ThreadAbortException)
							{
								if (this.m_tracer.TraceInfo)
								{
									this.m_tracer.Trace(TraceLevel.Info, "ServiceStartThread: Exiting for the following exception: {0}", new object[] { ex2.Message });
								}
								ex = ex2;
								break;
							}
							if (this.m_tracer.TraceError)
							{
								this.m_tracer.Trace(TraceLevel.Error, "ServiceStartThread: Exiting for the following exception: {0}", new object[] { ex2.ToString() });
							}
							ex = ex2;
							break;
						}
						else
						{
							if (ex == null || ex2.GetType() != ex.GetType())
							{
								if (Global.m_Tracer.TraceError)
								{
									Global.m_Tracer.Trace(TraceLevel.Error, "ServiceStartThread: Exception caught while starting service. Error: {0}", new object[] { ex2.ToString() });
									Global.m_Tracer.Trace(TraceLevel.Error, "ServiceStartThread: Attempting to start service again...");
								}
								Global.m_Tracer.BufferOutput = true;
								Global.m_Tracer.WriteBuffer();
							}
							else
							{
								Global.m_Tracer.ClearBuffer();
							}
							ex = ex2;
							flag = !this.m_startThreadExitStarted;
						}
					}
					finally
					{
						if (connectionManager != null)
						{
							connectionManager.DisconnectStorage();
						}
					}
					if (!flag2 && !this.m_startThreadExitStarted)
					{
						flag2 = this.m_appDomainController.StartRPCServer((bool)firstStart);
					}
					if (flag)
					{
						this.m_serviceResetEvent.WaitOne(5000, false);
					}
				}
				while (flag);
			}
			finally
			{
				Global.m_Tracer.BufferOutput = false;
				Global.m_Tracer.WriteBuffer();
				if (ex == null && flag2)
				{
					Global.SetInitialized();
					string text = string.Format(CultureInfo.InvariantCulture, "Appdomain:{0} {1} initialized.", AppDomain.CurrentDomain.Id, AppDomain.CurrentDomain.FriendlyName);
					Console.WriteLine(text);
					if (RSTrace.AppDomainManagerTracer.TraceInfo)
					{
						RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, text);
					}
				}
				else if (!this.m_startThreadExitStarted)
				{
					string text2 = "";
					if (ex != null)
					{
						text2 = ex.ToString();
					}
					else if (!flag2)
					{
						text2 = new Win32Exception(1720).Message;
					}
					RSEventLog.Current.WriteError(Microsoft.ReportingServices.Diagnostics.Event.AppDomainFailedToInitialize, new object[]
					{
						AppDomain.CurrentDomain.FriendlyName,
						text2
					});
					string text = string.Format(CultureInfo.InvariantCulture, "Appdomain:{0} {1} failed to initialize. Error: {2}.", AppDomain.CurrentDomain.Id, AppDomain.CurrentDomain.FriendlyName, text2);
					Console.WriteLine(text);
					if (RSTrace.AppDomainManagerTracer.TraceError)
					{
						RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Error, text);
					}
				}
				this.m_serviceStartThreadExited = true;
			}
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x0004EEB8 File Offset: 0x0004D0B8
		private PollWorker AddPollingWorker(PollWorker worker)
		{
			RSTrace.ServiceControllerTracer.Assert(this.m_pollingService != null, "null != m_pollingService");
			RSTrace.ServiceControllerTracer.Assert(worker != null, "null != worker");
			this.m_pollingService.AddWorker(worker);
			return worker;
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x0004EEF4 File Offset: 0x0004D0F4
		private void PreparePollingMaintenance()
		{
			ProviderManager.Configure();
			if (this.m_startThreadExitStarted)
			{
				return;
			}
			if (this.m_pollingService == null)
			{
				this.m_pollingService = new DBPoll();
			}
			if (Globals.Configuration.IsEventService && this.m_eventWorker == null)
			{
				this.m_eventWorker = (EventQueueWorker)this.AddPollingWorker(new EventQueueWorker(this));
			}
			if (this.m_startThreadExitStarted)
			{
				return;
			}
			if (this.m_startThreadExitStarted)
			{
				return;
			}
			if (Globals.Configuration.IsNotificationService && this.m_notificationWorker == null)
			{
				this.m_notificationWorker = (NotificationQueueWorker)this.AddPollingWorker(new NotificationQueueWorker());
			}
			if (this.m_startThreadExitStarted)
			{
				return;
			}
			if (Globals.Configuration.IsSchedulingService && this.m_scheduleWorker == null)
			{
				this.m_scheduleWorker = (SchedulePollWorker)this.AddPollingWorker(new SchedulePollWorker());
			}
			if (this.m_startThreadExitStarted)
			{
				return;
			}
			if (this.m_upgradeWorker == null)
			{
				this.m_upgradeWorker = (UpgradePollWorker)this.AddPollingWorker(new UpgradePollWorker());
			}
			bool startThreadExitStarted = this.m_startThreadExitStarted;
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x0004EFEA File Offset: 0x0004D1EA
		internal void ReleasePollingMaintenance()
		{
			if (this.m_pollingService != null)
			{
				this.m_pollingService.ReleasePollingMaintenance();
			}
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x0004EFFF File Offset: 0x0004D1FF
		internal void StartAllWorkers()
		{
			if (this.m_startThreadExitStarted)
			{
				return;
			}
			this.m_pollingService.StartPolling();
			Globals.RunningJobType = JobTypeEnum.System;
			new RunningJobsDbInitializer();
			this.StartTimers();
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x0004F027 File Offset: 0x0004D227
		internal void StopAllWorkers()
		{
			this.StopTimers();
			this.m_pollingService.StopPollWorkers(Globals.ServiceStopMode.FullStop);
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x0004F03B File Offset: 0x0004D23B
		internal IEnumerator CreatePollingMaintenanceLoop()
		{
			this.PreparePollingMaintenance();
			return this.m_pollingService.PollingMaintenanceLoop().GetEnumerator();
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x0004F053 File Offset: 0x0004D253
		internal void ReleasePollingMaintenanceLoop()
		{
			this.ReleasePollingMaintenance();
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x0004F05C File Offset: 0x0004D25C
		public void EndService(Globals.ServiceStopMode mode)
		{
			try
			{
				if (this.m_serviceState != ServiceController.CurrentRunningState.None)
				{
					if (this.m_startThread != null && this.m_startThread.ThreadState != global::System.Threading.ThreadState.Stopped)
					{
						this.m_startThreadExitStarted = true;
						int num = this.m_startThreadWaitToAbortTimeout;
						bool flag = false;
						while (!flag && num > 0)
						{
							if (RSTrace.ServiceControllerTracer.TraceInfo)
							{
								RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "ServiceAppDomainController::EndService - the Windows Service start thread is still running; Wait for it to finish; Mark the WindowsService (worker) AppDomain as active.");
							}
							this.m_appDomainController.MarkProcessAsActive();
							int num2 = Math.Min(num, 5000);
							num -= num2;
							flag = this.m_startThread.Join(num2);
						}
						if (!flag)
						{
							this.m_tracer.Trace(TraceLevel.Info, "[ThreadAborted] Service is existing therefore ServiceStartThread is aborted after over 5 seconds of wait time.");
							this.m_startThread.Abort();
						}
					}
					if (this.m_pollingService != null)
					{
						this.m_pollingService.StopPolling(mode);
					}
					if (mode != Globals.ServiceStopMode.StopPollingOnly)
					{
						this.m_eventWorker = null;
						this.m_notificationWorker = null;
						this.m_scheduleWorker = null;
						this.m_upgradeWorker = null;
					}
					ExtensionClassFactory.ClearAllExtensions();
					this.StopTimers();
					if (this.m_tracer.TraceInfo && mode == Globals.ServiceStopMode.FullStop)
					{
						this.m_tracer.Trace(TraceLevel.Info, "Service controller exiting.");
					}
				}
			}
			catch (Exception ex)
			{
				if (this.m_tracer.TraceError)
				{
					this.m_tracer.Trace(TraceLevel.Error, "Error Stopping Service: {0}", new object[] { ex.ToString() });
				}
				if (ex is RSException)
				{
					throw;
				}
				throw new InternalCatalogException(ex, null);
			}
			finally
			{
				this.m_startThreadExitStarted = false;
				((IDisposable)this).Dispose();
			}
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x0004F1F4 File Offset: 0x0004D3F4
		internal static void EnsureConfigurationFromDB()
		{
			RSConfiguration configuration = Globals.ConfigurationManager.GetConfiguration();
			ConfigurationPropertyBag configurationPropertyBag = null;
			long currentSequence = configuration.CurrentSequence;
			Property[] nonPermissionedProperties = Global.ConfigurationFromCatalog.GetNonPermissionedProperties();
			if (configurationPropertyBag == null)
			{
				configurationPropertyBag = new ConfigurationPropertyBag();
			}
			configurationPropertyBag.Add(ConfigurationProperty.EnablePowerBIFeatures, new ConfigurationPropertyInfo
			{
				Value = false
			});
			ConfigurationPropertyInfo configurationPropertyInfo = new ConfigurationPropertyInfo();
			OAuthConnectionConfigurationPropertyBag oauthConnectionConfigurationPropertyBag = new OAuthConnectionConfigurationPropertyBag();
			configurationPropertyInfo.Value = oauthConnectionConfigurationPropertyBag;
			bool flag = false;
			Property[] array = nonPermissionedProperties;
			int i = 0;
			while (i < array.Length)
			{
				Property property = array[i];
				string name = property.Name;
				uint num = global::<PrivateImplementationDetails>.ComputeStringHash(name);
				if (num <= 2386758756U)
				{
					if (num <= 931787539U)
					{
						if (num != 428540062U)
						{
							if (num == 931787539U)
							{
								if (name == "OAuthLogoutUrl")
								{
									goto IL_024C;
								}
							}
						}
						else if (name == "OAuthAuthorizationUrl")
						{
							goto IL_024C;
						}
					}
					else if (num != 1074849877U)
					{
						if (num != 1858534777U)
						{
							if (num == 2386758756U)
							{
								if (name == "OAuthTenant")
								{
									goto IL_024C;
								}
							}
						}
						else if (name == "OAuthResourceUrl")
						{
							goto IL_024C;
						}
					}
					else if (name == "OAuthSessionCookieName")
					{
						goto IL_024C;
					}
				}
				else if (num <= 3433020637U)
				{
					if (num != 2707123363U)
					{
						if (num != 3383634638U)
						{
							if (num == 3433020637U)
							{
								if (name == "OAuthNativeClientId")
								{
									goto IL_024C;
								}
							}
						}
						else if (name == "OAuthClientId")
						{
							if (property.Value != null)
							{
								if (configuration.OAuthConfiguration != null && property.Value.Equals(configuration.OAuthConfiguration.ClientId, StringComparison.InvariantCultureIgnoreCase))
								{
									flag = false;
								}
								else
								{
									flag = true;
									oauthConnectionConfigurationPropertyBag.Add(property.Name, property.Value);
								}
							}
						}
					}
					else if (name == "OAuthClientSecret")
					{
						goto IL_024C;
					}
				}
				else if (num != 3529716053U)
				{
					if (num != 3621259225U)
					{
						if (num == 3694534626U)
						{
							if (name == "OAuthTokenUrl")
							{
								goto IL_024C;
							}
						}
					}
					else if (name == "OAuthGraphUrl")
					{
						goto IL_024C;
					}
				}
				else if (name == "OAuthFederationMetadataUrl")
				{
					goto IL_024C;
				}
				IL_0261:
				i++;
				continue;
				IL_024C:
				oauthConnectionConfigurationPropertyBag.Add(property.Name, property.Value);
				goto IL_0261;
			}
			if (flag && oauthConnectionConfigurationPropertyBag.Any<KeyValuePair<string, ConfigurationPropertyInfo>>())
			{
				if (configurationPropertyBag == null)
				{
					configurationPropertyBag = new ConfigurationPropertyBag();
				}
				configurationPropertyBag.Add(ConfigurationProperty.OAuthConnectionConfiguration, configurationPropertyInfo);
			}
			if (configurationPropertyBag != null)
			{
				ConfigurationPropertyInfo configurationPropertyInfo2 = new ConfigurationPropertyInfo
				{
					Value = currentSequence
				};
				configurationPropertyBag.Add(ConfigurationProperty.CurrentSequence, configurationPropertyInfo2);
				Globals.ConfigurationManager.ChangeConfiguration(configurationPropertyBag, false);
			}
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x0004F4BD File Offset: 0x0004D6BD
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x0004F4C6 File Offset: 0x0004D6C6
		private void Dispose(bool disposing)
		{
			if (disposing && this.m_serviceResetEvent != null)
			{
				this.m_serviceResetEvent.Close();
				this.m_serviceResetEvent = null;
			}
		}

		// Token: 0x04000737 RID: 1847
		internal static ServiceController m_currentService;

		// Token: 0x04000738 RID: 1848
		private const int SecondsPerDay = 86400;

		// Token: 0x04000739 RID: 1849
		private IServiceAppDomainController m_appDomainController;

		// Token: 0x0400073A RID: 1850
		private RSTrace m_tracer = RSTrace.ServiceControllerTracer;

		// Token: 0x0400073B RID: 1851
		private DBPoll m_pollingService;

		// Token: 0x0400073C RID: 1852
		private bool m_serviceStartThreadExited;

		// Token: 0x0400073D RID: 1853
		private ServiceController.CurrentRunningState m_serviceState;

		// Token: 0x0400073E RID: 1854
		private Thread m_startThread;

		// Token: 0x0400073F RID: 1855
		private int m_startThreadWaitToAbortTimeout = 30000;

		// Token: 0x04000740 RID: 1856
		private bool m_startThreadExitStarted;

		// Token: 0x04000741 RID: 1857
		private QueuePollWorker m_eventWorker;

		// Token: 0x04000742 RID: 1858
		private QueuePollWorker m_notificationWorker;

		// Token: 0x04000743 RID: 1859
		private PollWorker m_scheduleWorker;

		// Token: 0x04000744 RID: 1860
		private PollWorker m_upgradeWorker;

		// Token: 0x04000745 RID: 1861
		private AutoResetEvent m_serviceResetEvent = new AutoResetEvent(false);

		// Token: 0x04000746 RID: 1862
		private List<TimerActionBase> m_timers = new List<TimerActionBase>(7);

		// Token: 0x04000747 RID: 1863
		private readonly object m_timersSync = new object();

		// Token: 0x020004AD RID: 1197
		private enum CurrentRunningState
		{
			// Token: 0x04001097 RID: 4247
			None,
			// Token: 0x04001098 RID: 4248
			AllStarted
		}
	}
}
