using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Security;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000BB RID: 187
	internal sealed class RSEventLog
	{
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x00011CE9 File Offset: 0x0000FEE9
		public static RSEventLog Current
		{
			get
			{
				return RSEventLog.m_currentLog;
			}
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00011CF0 File Offset: 0x0000FEF0
		private RSEventLog()
		{
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00011D0C File Offset: 0x0000FF0C
		internal void Init(IRSEventLogProvider provider)
		{
			if (ProcessingContext.Configuration.CurrentApplication == RunningApplication.Unknown)
			{
				throw new InternalCatalogException("RSEvent log class being used and diaganostic dll was not initialized correctly.");
			}
			if (this.m_initialized)
			{
				return;
			}
			this.m_provider = provider;
			this.m_sourceName = this.m_provider.GetEventSourceName(ProcessingContext.Configuration.CurrentApplication);
			if (this.m_sourceName == null)
			{
				this.m_writeEvents = false;
			}
			this.EnsureEventLogSourceInstalled();
			this.m_initialized = true;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00011D79 File Offset: 0x0000FF79
		private void EnsureEventLogSourceInstalled()
		{
			RevertImpersonationContext.Run(delegate
			{
				try
				{
					if (this.m_writeEvents && !EventLog.SourceExists(this.m_sourceName))
					{
						if (RSTrace.CatalogTrace.TraceWarning)
						{
							RSTrace.CatalogTrace.TraceWithNoEventLog(TraceLevel.Warning, "Event log source {0} does not exist", new object[] { this.m_sourceName });
						}
						throw new EventLogSourceNotFound(this.m_sourceName);
					}
				}
				catch (SecurityException)
				{
					throw new EventLogSourceNotFound(this.m_sourceName);
				}
			});
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x00011D8C File Offset: 0x0000FF8C
		public string SourceName
		{
			get
			{
				return this.m_sourceName;
			}
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00011D94 File Offset: 0x0000FF94
		public void WriteError(Event id, params object[] args)
		{
			this.WriteLog(EventLogEntryType.Error, id, args);
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00011D9F File Offset: 0x0000FF9F
		public void WriteInformation(Event id, params object[] args)
		{
			this.WriteLog(EventLogEntryType.Information, id, args);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00011DAA File Offset: 0x0000FFAA
		public void WriteSuccess(Event id, params object[] args)
		{
			this.WriteLog(EventLogEntryType.Information, id, args);
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00011DB5 File Offset: 0x0000FFB5
		public void WriteWarning(Event id, params object[] args)
		{
			this.WriteLog(EventLogEntryType.Warning, id, args);
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00011DC0 File Offset: 0x0000FFC0
		private RSEventLog.Category GetCategory(Event id)
		{
			RSEventLog.Category category;
			switch (id)
			{
			case Event.SqlAgentNotRunning:
			case Event.ScheduleUpdated:
				category = RSEventLog.Category.Scheduling;
				break;
			case Event.CouldNotCommunicateToCatalog:
			case Event.ConfigFileChanged:
			case Event.InvalidConfigEntry:
			case Event.FailedToDecryptDSN:
			case Event.ConfigFileNotFound:
			case Event.FailureToLoadConfigFile:
			case Event.FailureToEncryptConfigData:
			case Event.SetExtensionConfigFailed:
			case Event.AppDomainMaxMemoryLimitReached:
				category = RSEventLog.Category.Management;
				break;
			case Event.CouldNotLoadExtension:
				category = RSEventLog.Category.Extension;
				break;
			case Event.CouldNotCreateTraceFile:
			case Event.CantCreatePerfCounters:
			case Event.TraceNotDefaultLocation:
			case Event.FailedTraceWrite:
				category = RSEventLog.Category.Logging;
				break;
			case Event.DenialOfService:
			case Event.FailureToDecryptData:
			case Event.FailureToEncryptData:
				category = RSEventLog.Category.Security;
				break;
			case Event.CantCommunicateToReportServer:
			case Event.InvalidDBVersion:
			case Event.RPCFailedStart:
			case Event.AppDomainFailedToStart:
			case Event.AppDomainFailedToInitialize:
			case Event.ServerStarted:
			case Event.ServerFailedToStart:
			case Event.ServerStopped:
			case Event.ServerFailedToStop:
				category = RSEventLog.Category.Startup_Shutdown;
				break;
			case Event.InternalError:
				category = RSEventLog.Category.Internal;
				break;
			case Event.NotActivated:
			case Event.IsDisabled:
			case Event.ActivationSuccessful:
			case Event.KeyExtractionSuccessful:
			case Event.KeyImportSuccessful:
			case Event.EncryptDataCleaned:
			case Event.SKUMismatch:
			case Event.KeyDeleteSuccessful:
			case Event.EvaluationPeriodExpired:
			case Event.WebFarmNodeActivated:
				category = RSEventLog.Category.Activation;
				break;
			case Event.InvalidSMTP:
			case Event.PollQueueFull:
				category = RSEventLog.Category.Delivery;
				break;
			default:
				throw new InternalCatalogException(string.Format(CultureInfo.CurrentCulture, "Unknown event id passed in: {0}", id));
			}
			return category;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00011EC0 File Offset: 0x000100C0
		private void WriteLog(EventLogEntryType type, Event evt, params object[] args)
		{
			if (!this.m_initialized)
			{
				return;
			}
			if (!this.m_writeEvents)
			{
				return;
			}
			if (!this.CanWrite(evt))
			{
				return;
			}
			RSEventLog.Category category = this.GetCategory(evt);
			this.m_provider.WriteLog(this.m_sourceName, type, evt, category, args);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00011F08 File Offset: 0x00010108
		private bool CanWrite(Event id)
		{
			switch (id)
			{
			case Event.SqlAgentNotRunning:
			case Event.CouldNotCommunicateToCatalog:
			case Event.CouldNotCreateTraceFile:
			case Event.DenialOfService:
			case Event.CantCommunicateToReportServer:
			case Event.InvalidDBVersion:
			case Event.NotActivated:
			case Event.IsDisabled:
			case Event.InvalidSMTP:
				goto IL_0063;
			case Event.CouldNotLoadExtension:
			case Event.ConfigFileChanged:
			case Event.InvalidConfigEntry:
			case Event.CantCreatePerfCounters:
			case Event.ScheduleUpdated:
			case Event.InternalError:
			case Event.TraceNotDefaultLocation:
			case Event.RPCFailedStart:
				break;
			default:
				if (id - Event.SKUMismatch <= 1 || id - Event.ServerStarted <= 3)
				{
					goto IL_0063;
				}
				break;
			}
			return true;
			IL_0063:
			if (this.m_writeOnceEvents.Contains(id))
			{
				return false;
			}
			object syncRoot = this.m_writeOnceEvents.SyncRoot;
			lock (syncRoot)
			{
				if (this.m_writeOnceEvents.Contains(id))
				{
					return false;
				}
				this.m_writeOnceEvents.Add(id, null);
			}
			return true;
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00011FEC File Offset: 0x000101EC
		public void ResetWriteOnceEvent(Event id)
		{
			if (!this.m_writeOnceEvents.Contains(id))
			{
				return;
			}
			object syncRoot = this.m_writeOnceEvents.SyncRoot;
			lock (syncRoot)
			{
				this.m_writeOnceEvents.Remove(id);
			}
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00012050 File Offset: 0x00010250
		private void ConfigManager_DSNChanged(object sender, EventArgs e)
		{
			this.ResetWriteOnceEvent(Event.FailedToDecryptDSN);
			this.ResetWriteOnceEvent(Event.SKUMismatch);
		}

		// Token: 0x04000343 RID: 835
		private static RSEventLog m_currentLog = new RSEventLog();

		// Token: 0x04000344 RID: 836
		private string m_sourceName;

		// Token: 0x04000345 RID: 837
		private bool m_writeEvents = true;

		// Token: 0x04000346 RID: 838
		private bool m_initialized;

		// Token: 0x04000347 RID: 839
		private Hashtable m_writeOnceEvents = new Hashtable();

		// Token: 0x04000348 RID: 840
		private IRSEventLogProvider m_provider;

		// Token: 0x02000107 RID: 263
		public enum Category
		{
			// Token: 0x04000544 RID: 1348
			Delivery = 1,
			// Token: 0x04000545 RID: 1349
			Extension,
			// Token: 0x04000546 RID: 1350
			Internal,
			// Token: 0x04000547 RID: 1351
			Logging,
			// Token: 0x04000548 RID: 1352
			Management,
			// Token: 0x04000549 RID: 1353
			Scheduling,
			// Token: 0x0400054A RID: 1354
			Security,
			// Token: 0x0400054B RID: 1355
			Activation,
			// Token: 0x0400054C RID: 1356
			Startup_Shutdown,
			// Token: 0x0400054D RID: 1357
			TotalCategories = 9
		}
	}
}
