using System;
using System.Diagnostics.Eventing;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200035C RID: 860
	internal static class EventLogProvider
	{
		// Token: 0x06001E38 RID: 7736 RVA: 0x0005A6FC File Offset: 0x000588FC
		static EventLogProvider()
		{
			if (EventLogProvider._vistaProvider)
			{
				EventLogProvider._provider = new EventLogProviderVersionTwo(EventLogProvider._providerId);
				return;
			}
			EventLogProvider._providerLegacy = new EventLogProviderVersionOne(EventLogProvider._providerId);
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x0005ABCC File Offset: 0x00058DCC
		public static bool EventWriteClientSecurityAuthorizationFailed(string Source, string Param)
		{
			if (EventLogProvider._vistaProvider)
			{
				return !EventLogProvider._provider.IsEnabled() || EventLogProvider._provider.TemplateStringTemplate(ref EventLogProvider.ClientSecurityAuthorizationFailed, Source, Param);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.TemplateStringTemplate(ref EventLogProvider.ClientSecurityAuthorizationFailed, ref EventLogProvider.TaskId, Source, Param);
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x0005AC24 File Offset: 0x00058E24
		public static bool EventWriteServerSecurityAuthorizationFailed(string Source, string Param)
		{
			if (EventLogProvider._vistaProvider)
			{
				return !EventLogProvider._provider.IsEnabled() || EventLogProvider._provider.TemplateStringTemplate(ref EventLogProvider.ServerSecurityAuthorizationFailed, Source, Param);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.TemplateStringTemplate(ref EventLogProvider.ServerSecurityAuthorizationFailed, ref EventLogProvider.TaskId, Source, Param);
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x0005AC7C File Offset: 0x00058E7C
		public static bool EventWriteServerReadConfigurationFailed(string Source)
		{
			if (EventLogProvider._vistaProvider)
			{
				return EventLogProvider._provider.WriteEvent(ref EventLogProvider.ServerReadConfigurationFailed, Source);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.WriteEvent(ref EventLogProvider.ServerReadConfigurationFailed, ref EventLogProvider.TaskId, Source);
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x0005ACB9 File Offset: 0x00058EB9
		public static bool EventWriteErrorShuttingDownService(string Source)
		{
			if (EventLogProvider._vistaProvider)
			{
				return EventLogProvider._provider.WriteEvent(ref EventLogProvider.ErrorShuttingDownService, Source);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.WriteEvent(ref EventLogProvider.ErrorShuttingDownService, ref EventLogProvider.TaskId, Source);
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x0005ACF8 File Offset: 0x00058EF8
		public static bool EventWriteServerServiceCrash(string Source, string Param)
		{
			if (EventLogProvider._vistaProvider)
			{
				return !EventLogProvider._provider.IsEnabled() || EventLogProvider._provider.TemplateStringTemplate(ref EventLogProvider.ServerServiceCrash, Source, Param);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.TemplateStringTemplate(ref EventLogProvider.ServerServiceCrash, ref EventLogProvider.TaskId, Source, Param);
		}

		// Token: 0x06001E3E RID: 7742 RVA: 0x0005AD50 File Offset: 0x00058F50
		public static bool EventWriteServiceCrashUnkownError(string Source, string Param)
		{
			if (EventLogProvider._vistaProvider)
			{
				return !EventLogProvider._provider.IsEnabled() || EventLogProvider._provider.TemplateStringTemplate(ref EventLogProvider.ServiceCrashUnkownError, Source, Param);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.TemplateStringTemplate(ref EventLogProvider.ServiceCrashUnkownError, ref EventLogProvider.TaskId, Source, Param);
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x0005ADA8 File Offset: 0x00058FA8
		public static bool EventWriteServiceStart(string Source)
		{
			if (EventLogProvider._vistaProvider)
			{
				return EventLogProvider._provider.WriteEvent(ref EventLogProvider.ServiceStart, Source);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.WriteEvent(ref EventLogProvider.ServiceStart, ref EventLogProvider.TaskId, Source);
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x0005ADE5 File Offset: 0x00058FE5
		public static bool EventWriteServicestop(string Source)
		{
			if (EventLogProvider._vistaProvider)
			{
				return EventLogProvider._provider.WriteEvent(ref EventLogProvider.Servicestop, Source);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.WriteEvent(ref EventLogProvider.Servicestop, ref EventLogProvider.TaskId, Source);
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x0005AE24 File Offset: 0x00059024
		public static bool EventWriteXmlConfigurationReadError(string Source, string Param)
		{
			if (EventLogProvider._vistaProvider)
			{
				return !EventLogProvider._provider.IsEnabled() || EventLogProvider._provider.TemplateStringTemplate(ref EventLogProvider.XmlConfigurationReadError, Source, Param);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.TemplateStringTemplate(ref EventLogProvider.XmlConfigurationReadError, ref EventLogProvider.TaskId, Source, Param);
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x0005AE7C File Offset: 0x0005907C
		public static bool EventWriteThrottleStarted(string Source, int Param1, int Param2, int Param3, int Param4, int Param5, int Param6)
		{
			if (EventLogProvider._vistaProvider)
			{
				return !EventLogProvider._provider.IsEnabled() || EventLogProvider._provider.TemplateIntIntIntIntIntIntTemplate(ref EventLogProvider.ThrottleStarted, Source, Param1, Param2, Param3, Param4, Param5, Param6);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.TemplateIntIntIntIntIntIntTemplate(ref EventLogProvider.ThrottleStarted, ref EventLogProvider.TaskId, Source, Param1, Param2, Param3, Param4, Param5, Param6);
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x0005AEE4 File Offset: 0x000590E4
		public static bool EventWriteInThrottledState(string Source, int Param1, int Param2, int Param3, int Param4, int Param5, int Param6)
		{
			if (EventLogProvider._vistaProvider)
			{
				return !EventLogProvider._provider.IsEnabled() || EventLogProvider._provider.TemplateIntIntIntIntIntIntTemplate(ref EventLogProvider.InThrottledState, Source, Param1, Param2, Param3, Param4, Param5, Param6);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.TemplateIntIntIntIntIntIntTemplate(ref EventLogProvider.InThrottledState, ref EventLogProvider.TaskId, Source, Param1, Param2, Param3, Param4, Param5, Param6);
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x0005AF4C File Offset: 0x0005914C
		public static bool EventWriteThrottleStopped(string Source, int Param1, int Param2, int Param3, int Param4, int Param5, int Param6)
		{
			if (EventLogProvider._vistaProvider)
			{
				return !EventLogProvider._provider.IsEnabled() || EventLogProvider._provider.TemplateIntIntIntIntIntIntTemplate(ref EventLogProvider.ThrottleStopped, Source, Param1, Param2, Param3, Param4, Param5, Param6);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.TemplateIntIntIntIntIntIntTemplate(ref EventLogProvider.ThrottleStopped, ref EventLogProvider.TaskId, Source, Param1, Param2, Param3, Param4, Param5, Param6);
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x0005AFB4 File Offset: 0x000591B4
		public static bool EventWriteAvailableMemoryLow(string Source, int Param1, int Param2, int Param3, int Param4, int Param5, int Param6)
		{
			if (EventLogProvider._vistaProvider)
			{
				return !EventLogProvider._provider.IsEnabled() || EventLogProvider._provider.TemplateIntIntIntIntIntIntTemplate(ref EventLogProvider.AvailableMemoryLow, Source, Param1, Param2, Param3, Param4, Param5, Param6);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.TemplateIntIntIntIntIntIntTemplate(ref EventLogProvider.AvailableMemoryLow, ref EventLogProvider.TaskId, Source, Param1, Param2, Param3, Param4, Param5, Param6);
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x0005B01C File Offset: 0x0005921C
		public static bool EventWritePoolThrottleStarted(string Source, string Param1, int Param2, int Param3, int Param4, int Param5, int Param6, int Param7, int Param8, int Param9, long Param10, long Param11, long Param12, long Param13, long Param14)
		{
			if (EventLogProvider._vistaProvider)
			{
				return !EventLogProvider._provider.IsEnabled() || EventLogProvider._provider.TemplateString8Int5LongTemplate(ref EventLogProvider.PoolThrottleStarted, Source, Param1, Param2, Param3, Param4, Param5, Param6, Param7, Param8, Param9, Param10, Param11, Param12, Param13, Param14);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.TemplateString8Int5LongTemplate(ref EventLogProvider.PoolThrottleStarted, ref EventLogProvider.TaskId, Source, Param1, Param2, Param3, Param4, Param5, Param6, Param7, Param8, Param9, Param10, Param11, Param12, Param13, Param14);
		}

		// Token: 0x06001E47 RID: 7751 RVA: 0x0005B0A4 File Offset: 0x000592A4
		public static bool EventWritePoolThrottleStopped(string Source, int Param1, int Param2, int Param3, int Param4, int Param5, int Param6, int Param7, int Param8, long Param9, long Param10, long Param11, long Param12, long Param13, long Param14)
		{
			if (EventLogProvider._vistaProvider)
			{
				return !EventLogProvider._provider.IsEnabled() || EventLogProvider._provider.Template8Int6LongTemplate(ref EventLogProvider.PoolThrottleStopped, Source, Param1, Param2, Param3, Param4, Param5, Param6, Param7, Param8, Param9, Param10, Param11, Param12, Param13, Param14);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.Template8Int6LongTemplate(ref EventLogProvider.PoolThrottleStopped, ref EventLogProvider.TaskId, Source, Param1, Param2, Param3, Param4, Param5, Param6, Param7, Param8, Param9, Param10, Param11, Param12, Param13, Param14);
		}

		// Token: 0x06001E48 RID: 7752 RVA: 0x0005B12C File Offset: 0x0005932C
		public static bool EventWritePoolGrowth(string Source, string Param1, long Param2, long Param3, long Param4)
		{
			if (EventLogProvider._vistaProvider)
			{
				return !EventLogProvider._provider.IsEnabled() || EventLogProvider._provider.TemplateStringLongLongLongTemplate(ref EventLogProvider.PoolGrowth, Source, Param1, Param2, Param3, Param4);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.TemplateStringLongLongLongTemplate(ref EventLogProvider.PoolGrowth, ref EventLogProvider.TaskId, Source, Param1, Param2, Param3, Param4);
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x0005B18C File Offset: 0x0005938C
		public static bool EventWriteInPoolThrottling(string Source, int Param1, int Param2, int Param3, int Param4, int Param5, int Param6, int Param7, int Param8, long Param9, long Param10, long Param11, long Param12, long Param13, long Param14)
		{
			if (EventLogProvider._vistaProvider)
			{
				return !EventLogProvider._provider.IsEnabled() || EventLogProvider._provider.Template8Int6LongTemplate(ref EventLogProvider.InPoolThrottling, Source, Param1, Param2, Param3, Param4, Param5, Param6, Param7, Param8, Param9, Param10, Param11, Param12, Param13, Param14);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.Template8Int6LongTemplate(ref EventLogProvider.InPoolThrottling, ref EventLogProvider.TaskId, Source, Param1, Param2, Param3, Param4, Param5, Param6, Param7, Param8, Param9, Param10, Param11, Param12, Param13, Param14);
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x0005B214 File Offset: 0x00059414
		public static bool EventWriteRequestReceived(string Source, long Param)
		{
			if (EventLogProvider._vistaProvider)
			{
				return !EventLogProvider._provider.IsEnabled() || EventLogProvider._provider.TemplateLongTemplate(ref EventLogProvider.RequestReceived, Source, Param);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.TemplateLongTemplate(ref EventLogProvider.RequestReceived, ref EventLogProvider.TaskId, Source, Param);
		}

		// Token: 0x06001E4B RID: 7755 RVA: 0x0005B26C File Offset: 0x0005946C
		public static bool EventWriteSendForReplication(string Source, long Param)
		{
			if (EventLogProvider._vistaProvider)
			{
				return !EventLogProvider._provider.IsEnabled() || EventLogProvider._provider.TemplateLongTemplate(ref EventLogProvider.SendForReplication, Source, Param);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.TemplateLongTemplate(ref EventLogProvider.SendForReplication, ref EventLogProvider.TaskId, Source, Param);
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x0005B2C4 File Offset: 0x000594C4
		public static bool EventWriteReplicationAcked(string Source, long Param)
		{
			if (EventLogProvider._vistaProvider)
			{
				return !EventLogProvider._provider.IsEnabled() || EventLogProvider._provider.TemplateLongTemplate(ref EventLogProvider.ReplicationAcked, Source, Param);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.TemplateLongTemplate(ref EventLogProvider.ReplicationAcked, ref EventLogProvider.TaskId, Source, Param);
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x0005B31C File Offset: 0x0005951C
		public static bool EventWriteRequestProcessingCompleted(string Source, long Param)
		{
			if (EventLogProvider._vistaProvider)
			{
				return !EventLogProvider._provider.IsEnabled() || EventLogProvider._provider.TemplateLongTemplate(ref EventLogProvider.RequestProcessingCompleted, Source, Param);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.TemplateLongTemplate(ref EventLogProvider.RequestProcessingCompleted, ref EventLogProvider.TaskId, Source, Param);
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x0005B374 File Offset: 0x00059574
		public static bool EventWriteResponseSent(string Source, long Param1, long Param2)
		{
			if (EventLogProvider._vistaProvider)
			{
				return !EventLogProvider._provider.IsEnabled() || EventLogProvider._provider.TemplateLongLongTemplate(ref EventLogProvider.ResponseSent, Source, Param1, Param2);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.TemplateLongLongTemplate(ref EventLogProvider.ResponseSent, ref EventLogProvider.TaskId, Source, Param1, Param2);
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x0005B3D0 File Offset: 0x000595D0
		public static bool EventWriteDelayedResponseSent(string Source, long Param1, long Param2)
		{
			if (EventLogProvider._vistaProvider)
			{
				return !EventLogProvider._provider.IsEnabled() || EventLogProvider._provider.TemplateLongLongTemplate(ref EventLogProvider.DelayedResponseSent, Source, Param1, Param2);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.TemplateLongLongTemplate(ref EventLogProvider.DelayedResponseSent, ref EventLogProvider.TaskId, Source, Param1, Param2);
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x0005B42C File Offset: 0x0005962C
		public static bool EventWriteRequestRetried(string Source, long Param)
		{
			if (EventLogProvider._vistaProvider)
			{
				return !EventLogProvider._provider.IsEnabled() || EventLogProvider._provider.TemplateLongTemplate(ref EventLogProvider.RequestRetried, Source, Param);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.TemplateLongTemplate(ref EventLogProvider.RequestRetried, ref EventLogProvider.TaskId, Source, Param);
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x0005B484 File Offset: 0x00059684
		public static bool EventWriteServerConfigurationError(string Source, string Param)
		{
			if (EventLogProvider._vistaProvider)
			{
				return !EventLogProvider._provider.IsEnabled() || EventLogProvider._provider.TemplateStringTemplate(ref EventLogProvider.ServerConfigurationError, Source, Param);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.TemplateStringTemplate(ref EventLogProvider.ServerConfigurationError, ref EventLogProvider.TaskId, Source, Param);
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x0005B4DC File Offset: 0x000596DC
		public static bool EventWriteGracefulShutdownStart(string Source)
		{
			if (EventLogProvider._vistaProvider)
			{
				return !EventLogProvider._provider.IsEnabled() || EventLogProvider._provider.WriteEvent(ref EventLogProvider.GracefulShutdownStart, Source);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.WriteEvent(ref EventLogProvider.GracefulShutdownStart, ref EventLogProvider.TaskId, Source);
		}

		// Token: 0x06001E53 RID: 7763 RVA: 0x0005B534 File Offset: 0x00059734
		public static bool EventWriteGracefulShutdownStop(string Source)
		{
			if (EventLogProvider._vistaProvider)
			{
				return !EventLogProvider._provider.IsEnabled() || EventLogProvider._provider.WriteEvent(ref EventLogProvider.GracefulShutdownStop, Source);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.WriteEvent(ref EventLogProvider.GracefulShutdownStop, ref EventLogProvider.TaskId, Source);
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x0005B58C File Offset: 0x0005978C
		public static bool EventWriteUsagePersistenceFailure(string Source, string Param)
		{
			if (EventLogProvider._vistaProvider)
			{
				return !EventLogProvider._provider.IsEnabled() || EventLogProvider._provider.TemplateStringTemplate(ref EventLogProvider.UsagePersistenceFailure, Source, Param);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.TemplateStringTemplate(ref EventLogProvider.UsagePersistenceFailure, ref EventLogProvider.TaskId, Source, Param);
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x0005B5E4 File Offset: 0x000597E4
		[Obsolete("UnhandledException is no longer available. It is superseded by ServiceCrashUnkownError for both VAS and on-prem.", true)]
		public static bool EventWriteUnhandledException(string Source, string Param)
		{
			if (EventLogProvider._vistaProvider)
			{
				return !EventLogProvider._provider.IsEnabled() || EventLogProvider._provider.TemplateStringTemplate(ref EventLogProvider.UnhandledException, Source, Param);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.TemplateStringTemplate(ref EventLogProvider.UnhandledException, ref EventLogProvider.TaskId, Source, Param);
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x0005B63C File Offset: 0x0005983C
		public static bool EventWriteExternalStoreFailure(string Source, string Param)
		{
			if (EventLogProvider._vistaProvider)
			{
				return !EventLogProvider._provider.IsEnabled() || EventLogProvider._provider.TemplateStringTemplate(ref EventLogProvider.ExternalStoreFailure, Source, Param);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.TemplateStringTemplate(ref EventLogProvider.ExternalStoreFailure, ref EventLogProvider.TaskId, Source, Param);
		}

		// Token: 0x06001E57 RID: 7767 RVA: 0x0005B694 File Offset: 0x00059894
		public static bool EventWriteInMemoryCommitmentExceededState(string Source, long Param1, long Param2)
		{
			if (EventLogProvider._vistaProvider)
			{
				return !EventLogProvider._provider.IsEnabled() || EventLogProvider._provider.TemplateLongLongTemplate(ref EventLogProvider.InMemoryCommitmentExceededState, Source, Param1, Param2);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.TemplateLongLongTemplate(ref EventLogProvider.InMemoryCommitmentExceededState, ref EventLogProvider.TaskId, Source, Param1, Param2);
		}

		// Token: 0x06001E58 RID: 7768 RVA: 0x0005B6F0 File Offset: 0x000598F0
		public static bool EventWriteProcessReservedMemoryUnavailable(string Source, long Param2, long Param3, long Param4)
		{
			if (EventLogProvider._vistaProvider)
			{
				return !EventLogProvider._provider.IsEnabled() || EventLogProvider._provider.TemplateLongLongLongTemplate(ref EventLogProvider.ProcessReservedMemoryUnavailable, Source, Param2, Param3, Param4);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.TemplateLongLongLongTemplate(ref EventLogProvider.ProcessReservedMemoryUnavailable, ref EventLogProvider.TaskId, Source, Param2, Param3, Param4);
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x0005B74C File Offset: 0x0005994C
		public static bool EventWriteCreateCacheCompleted(string Source, string Param)
		{
			if (EventLogProvider._vistaProvider)
			{
				return !EventLogProvider._provider.IsEnabled() || EventLogProvider._provider.TemplateStringTemplate(ref EventLogProvider.CreateCacheCompleted, Source, Param);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.TemplateStringTemplate(ref EventLogProvider.CreateCacheCompleted, ref EventLogProvider.TaskId, Source, Param);
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x0005B7A4 File Offset: 0x000599A4
		public static bool EventWriteDeleteCacheCompleted(string Source, string Param)
		{
			if (EventLogProvider._vistaProvider)
			{
				return !EventLogProvider._provider.IsEnabled() || EventLogProvider._provider.TemplateStringTemplate(ref EventLogProvider.DeleteCacheCompleted, Source, Param);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.TemplateStringTemplate(ref EventLogProvider.DeleteCacheCompleted, ref EventLogProvider.TaskId, Source, Param);
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x0005B7FC File Offset: 0x000599FC
		public static bool EventWriteCreateCacheFailed(string Source, string Param1, string Param2)
		{
			if (EventLogProvider._vistaProvider)
			{
				return !EventLogProvider._provider.IsEnabled() || EventLogProvider._provider.TemplateStringStringTemplate(ref EventLogProvider.CreateCacheFailed, Source, Param1, Param2);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.TemplateStringStringTemplate(ref EventLogProvider.CreateCacheFailed, ref EventLogProvider.TaskId, Source, Param1, Param2);
		}

		// Token: 0x06001E5C RID: 7772 RVA: 0x0005B858 File Offset: 0x00059A58
		public static bool EventWriteDeleteCacheFailed(string Source, string Param1, string Param2)
		{
			if (EventLogProvider._vistaProvider)
			{
				return !EventLogProvider._provider.IsEnabled() || EventLogProvider._provider.TemplateStringStringTemplate(ref EventLogProvider.DeleteCacheFailed, Source, Param1, Param2);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.TemplateStringStringTemplate(ref EventLogProvider.DeleteCacheFailed, ref EventLogProvider.TaskId, Source, Param1, Param2);
		}

		// Token: 0x06001E5D RID: 7773 RVA: 0x0005B8B4 File Offset: 0x00059AB4
		public static bool EventWriteCscfgChangeRequiringReboot(string Source, string Param)
		{
			if (EventLogProvider._vistaProvider)
			{
				return !EventLogProvider._provider.IsEnabled() || EventLogProvider._provider.TemplateStringTemplate(ref EventLogProvider.CscfgChangeRequiringReboot, Source, Param);
			}
			return !EventLogProvider._providerLegacy.IsEnabled() || EventLogProvider._providerLegacy.TemplateStringTemplate(ref EventLogProvider.CscfgChangeRequiringReboot, ref EventLogProvider.TaskId, Source, Param);
		}

		// Token: 0x04001100 RID: 4352
		private static EventLogProviderVersionTwo _provider;

		// Token: 0x04001101 RID: 4353
		private static EventLogProviderVersionOne _providerLegacy;

		// Token: 0x04001102 RID: 4354
		private static bool _vistaProvider = Environment.OSVersion.Version.Major > 5;

		// Token: 0x04001103 RID: 4355
		private static Guid _providerId = new Guid("A7AC9495-AA38-492F-9D7E-ABF7F7666395");

		// Token: 0x04001104 RID: 4356
		private static Guid TaskId = new Guid("d406518c-e017-4f4e-93a0-d6aacfc79e77");

		// Token: 0x04001105 RID: 4357
		private static EventDescriptor ClientSecurityAuthorizationFailed = new EventDescriptor(106, 0, 16, 2, 106, 1, long.MinValue);

		// Token: 0x04001106 RID: 4358
		private static EventDescriptor ServerSecurityAuthorizationFailed = new EventDescriptor(107, 0, 16, 2, 107, 1, long.MinValue);

		// Token: 0x04001107 RID: 4359
		private static EventDescriptor ServerReadConfigurationFailed = new EventDescriptor(108, 0, 16, 2, 108, 1, long.MinValue);

		// Token: 0x04001108 RID: 4360
		private static EventDescriptor ErrorShuttingDownService = new EventDescriptor(109, 0, 16, 2, 109, 1, long.MinValue);

		// Token: 0x04001109 RID: 4361
		private static EventDescriptor ServerServiceCrash = new EventDescriptor(110, 0, 16, 2, 110, 1, long.MinValue);

		// Token: 0x0400110A RID: 4362
		private static EventDescriptor ServiceCrashUnkownError = new EventDescriptor(111, 0, 16, 2, 111, 1, long.MinValue);

		// Token: 0x0400110B RID: 4363
		private static EventDescriptor ServiceStart = new EventDescriptor(112, 0, 16, 4, 112, 1, long.MinValue);

		// Token: 0x0400110C RID: 4364
		private static EventDescriptor Servicestop = new EventDescriptor(113, 0, 16, 4, 113, 1, long.MinValue);

		// Token: 0x0400110D RID: 4365
		private static EventDescriptor XmlConfigurationReadError = new EventDescriptor(114, 0, 16, 3, 114, 1, long.MinValue);

		// Token: 0x0400110E RID: 4366
		private static EventDescriptor ThrottleStarted = new EventDescriptor(115, 0, 17, 3, 115, 1, 4611686018427387904L);

		// Token: 0x0400110F RID: 4367
		private static EventDescriptor InThrottledState = new EventDescriptor(116, 0, 17, 3, 116, 1, 4611686018427387904L);

		// Token: 0x04001110 RID: 4368
		private static EventDescriptor ThrottleStopped = new EventDescriptor(117, 0, 17, 3, 117, 1, 4611686018427387904L);

		// Token: 0x04001111 RID: 4369
		private static EventDescriptor AvailableMemoryLow = new EventDescriptor(118, 0, 17, 3, 118, 1, 4611686018427387904L);

		// Token: 0x04001112 RID: 4370
		private static EventDescriptor PoolThrottleStarted = new EventDescriptor(119, 0, 17, 3, 119, 1, 4611686018427387904L);

		// Token: 0x04001113 RID: 4371
		private static EventDescriptor PoolThrottleStopped = new EventDescriptor(120, 0, 17, 3, 120, 1, 4611686018427387904L);

		// Token: 0x04001114 RID: 4372
		private static EventDescriptor PoolGrowth = new EventDescriptor(121, 0, 17, 3, 121, 1, 4611686018427387904L);

		// Token: 0x04001115 RID: 4373
		private static EventDescriptor InPoolThrottling = new EventDescriptor(122, 0, 17, 3, 122, 1, 4611686018427387904L);

		// Token: 0x04001116 RID: 4374
		private static EventDescriptor RequestReceived = new EventDescriptor(123, 0, 19, 5, 123, 1, 1152921504606846976L);

		// Token: 0x04001117 RID: 4375
		private static EventDescriptor SendForReplication = new EventDescriptor(124, 0, 19, 5, 124, 1, 1152921504606846976L);

		// Token: 0x04001118 RID: 4376
		private static EventDescriptor ReplicationAcked = new EventDescriptor(125, 0, 19, 5, 125, 1, 1152921504606846976L);

		// Token: 0x04001119 RID: 4377
		private static EventDescriptor RequestProcessingCompleted = new EventDescriptor(126, 0, 19, 5, 126, 1, 1152921504606846976L);

		// Token: 0x0400111A RID: 4378
		private static EventDescriptor ResponseSent = new EventDescriptor(127, 0, 19, 5, 127, 1, 1152921504606846976L);

		// Token: 0x0400111B RID: 4379
		private static EventDescriptor DelayedResponseSent = new EventDescriptor(129, 0, 19, 4, 129, 1, 1152921504606846976L);

		// Token: 0x0400111C RID: 4380
		private static EventDescriptor RequestRetried = new EventDescriptor(128, 0, 19, 5, 128, 1, 1152921504606846976L);

		// Token: 0x0400111D RID: 4381
		private static EventDescriptor ServerConfigurationError = new EventDescriptor(130, 0, 16, 2, 130, 1, long.MinValue);

		// Token: 0x0400111E RID: 4382
		private static EventDescriptor GracefulShutdownStart = new EventDescriptor(131, 0, 16, 4, 131, 1, long.MinValue);

		// Token: 0x0400111F RID: 4383
		private static EventDescriptor GracefulShutdownStop = new EventDescriptor(132, 0, 16, 4, 132, 1, long.MinValue);

		// Token: 0x04001120 RID: 4384
		private static EventDescriptor UsagePersistenceFailure = new EventDescriptor(133, 0, 16, 2, 133, 1, long.MinValue);

		// Token: 0x04001121 RID: 4385
		private static EventDescriptor UnhandledException = new EventDescriptor(134, 0, 16, 2, 134, 1, long.MinValue);

		// Token: 0x04001122 RID: 4386
		private static EventDescriptor ExternalStoreFailure = new EventDescriptor(135, 0, 16, 3, 135, 1, long.MinValue);

		// Token: 0x04001123 RID: 4387
		private static EventDescriptor InMemoryCommitmentExceededState = new EventDescriptor(136, 0, 17, 3, 136, 1, 4611686018427387904L);

		// Token: 0x04001124 RID: 4388
		private static EventDescriptor ProcessReservedMemoryUnavailable = new EventDescriptor(137, 0, 17, 3, 137, 1, 4611686018427387904L);

		// Token: 0x04001125 RID: 4389
		private static EventDescriptor CreateCacheCompleted = new EventDescriptor(138, 0, 16, 4, 138, 1, long.MinValue);

		// Token: 0x04001126 RID: 4390
		private static EventDescriptor DeleteCacheCompleted = new EventDescriptor(139, 0, 16, 4, 139, 1, long.MinValue);

		// Token: 0x04001127 RID: 4391
		private static EventDescriptor CreateCacheFailed = new EventDescriptor(140, 0, 16, 2, 140, 1, long.MinValue);

		// Token: 0x04001128 RID: 4392
		private static EventDescriptor DeleteCacheFailed = new EventDescriptor(141, 0, 16, 2, 141, 1, long.MinValue);

		// Token: 0x04001129 RID: 4393
		private static EventDescriptor CscfgChangeRequiringReboot = new EventDescriptor(142, 0, 16, 3, 142, 1, long.MinValue);
	}
}
