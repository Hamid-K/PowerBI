using System;
using System.Diagnostics;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000371 RID: 881
	internal static class RSPerfCounterInfo
	{
		// Token: 0x06001CD9 RID: 7385 RVA: 0x000740DC File Offset: 0x000722DC
		internal static CounterCreationDataCollection GetWebServiceCounterInfo(bool defaultOnly)
		{
			if (RSPerfCounterInfo.m_webServicePerfCountersDefault == null)
			{
				RSPerfCounterInfo.m_webServicePerfCountersDefault = new CounterCreationDataCollection();
				RSPerfCounterInfo.m_webServicePerfCountersAll = new CounterCreationDataCollection();
				RSPerfCounterInfo.m_webServicePerfCountersDefault.Add(new CounterCreationData(RSPerfCounterInfo.ReportRequests, RSPerfCounterRes.REPORTREQUESTS, PerformanceCounterType.NumberOfItems32));
				RSPerfCounterInfo.m_webServicePerfCountersDefault.Add(new CounterCreationData(RSPerfCounterInfo.ExecutionCount, RSPerfCounterRes.REPORTSEXECUTED, PerformanceCounterType.NumberOfItems32));
				RSPerfCounterInfo.m_webServicePerfCountersDefault.Add(new CounterCreationData(RSPerfCounterInfo.ExecutionsPerSecond, RSPerfCounterRes.REPORTSEXECUTEDPERSECOND, PerformanceCounterType.RateOfCountsPerSecond32));
				RSPerfCounterInfo.m_webServicePerfCountersDefault.Add(new CounterCreationData(RSPerfCounterInfo.ReportProcessingErrorCount, RSPerfCounterRes.PROCESSINGFAILURES, PerformanceCounterType.NumberOfItems32));
				RSPerfCounterInfo.m_webServicePerfCountersDefault.Add(new CounterCreationData(RSPerfCounterInfo.RejectedThreads, RSPerfCounterRes.REJECTEDTHREADS, PerformanceCounterType.NumberOfItems32));
				RSPerfCounterInfo.m_webServicePerfCountersDefault.Add(new CounterCreationData(RSPerfCounterInfo.ActiveSessionsCount, RSPerfCounterRes.ACTIVESESSIONS, PerformanceCounterType.NumberOfItems32));
				RSPerfCounterInfo.m_webServicePerfCountersDefault.Add(new CounterCreationData(RSPerfCounterInfo.FirstSessionRequests, RSPerfCounterRes.FIRSTSESSIONPERSECOND, PerformanceCounterType.RateOfCountsPerSecond32));
				RSPerfCounterInfo.m_webServicePerfCountersDefault.Add(new CounterCreationData(RSPerfCounterInfo.NextSessionRequests, RSPerfCounterRes.NEXTSESSIONPERSECOND, PerformanceCounterType.RateOfCountsPerSecond32));
				RSPerfCounterInfo.m_webServicePerfCountersDefault.Add(new CounterCreationData(RSPerfCounterInfo.TotalCacheHitsCount, RSPerfCounterRes.CACHEHITS, PerformanceCounterType.NumberOfItems32));
				RSPerfCounterInfo.m_webServicePerfCountersDefault.Add(new CounterCreationData(RSPerfCounterInfo.TotalCacheHitsPerSecond, RSPerfCounterRes.CACHEHITSPERSECOND, PerformanceCounterType.RateOfCountsPerSecond32));
				RSPerfCounterInfo.m_webServicePerfCountersDefault.Add(new CounterCreationData(RSPerfCounterInfo.TotalCacheMissCount, RSPerfCounterRes.CACHEMISSES, PerformanceCounterType.NumberOfItems32));
				RSPerfCounterInfo.m_webServicePerfCountersDefault.Add(new CounterCreationData(RSPerfCounterInfo.TotalCacheMissPerSecond, RSPerfCounterRes.CACHEMISSESPERSECOND, PerformanceCounterType.RateOfCountsPerSecond32));
				RSPerfCounterInfo.m_webServicePerfCountersDefault.Add(new CounterCreationData(RSPerfCounterInfo.TotalRequestCount, RSPerfCounterRes.TOTALREQUESTS, PerformanceCounterType.NumberOfItems32));
				RSPerfCounterInfo.m_webServicePerfCountersDefault.Add(new CounterCreationData(RSPerfCounterInfo.TotalRequestPerSecond, RSPerfCounterRes.REQUESTSPERSECOND, PerformanceCounterType.RateOfCountsPerSecond32));
				RSPerfCounterInfo.m_webServicePerfCountersDefault.Add(new CounterCreationData(RSPerfCounterInfo.TotalMemoryCacheHitsCount, RSPerfCounterRes.MEMORYCACHEHITS, PerformanceCounterType.NumberOfItems32));
				RSPerfCounterInfo.m_webServicePerfCountersDefault.Add(new CounterCreationData(RSPerfCounterInfo.MemoryCacheHitsPerSecond, RSPerfCounterRes.MEMORYCACHEHITSPERSECOND, PerformanceCounterType.RateOfCountsPerSecond32));
				RSPerfCounterInfo.m_webServicePerfCountersDefault.Add(new CounterCreationData(RSPerfCounterInfo.TotalMemoryCacheMissCount, RSPerfCounterRes.MEMORYCACHEMISSES, PerformanceCounterType.NumberOfItems32));
				RSPerfCounterInfo.m_webServicePerfCountersDefault.Add(new CounterCreationData(RSPerfCounterInfo.MemoryCacheMissPerSecond, RSPerfCounterRes.MEMORYCACHEMISSESPERSECOND, PerformanceCounterType.RateOfCountsPerSecond32));
				RSPerfCounterInfo.m_webServicePerfCountersDefault.Add(new CounterCreationData(RSPerfCounterInfo.ModelsTotalCacheHitsCount, RSPerfCounterRes.MODELCACHEHITS, PerformanceCounterType.NumberOfItems32));
				RSPerfCounterInfo.m_webServicePerfCountersDefault.Add(new CounterCreationData(RSPerfCounterInfo.ModelsTotalCacheHitsPerSecond, RSPerfCounterRes.MODELCACHEHITSPERSECOND, PerformanceCounterType.RateOfCountsPerSecond32));
				RSPerfCounterInfo.m_webServicePerfCountersDefault.Add(new CounterCreationData(RSPerfCounterInfo.ModelsTotalCacheMissCount, RSPerfCounterRes.MODELCACHEMISS, PerformanceCounterType.NumberOfItems32));
				RSPerfCounterInfo.m_webServicePerfCountersDefault.Add(new CounterCreationData(RSPerfCounterInfo.ModelsTotalCacheMissPerSecond, RSPerfCounterRes.MODELCACHEMISSPERSECOND, PerformanceCounterType.RateOfCountsPerSecond32));
				RSPerfCounterInfo.m_webServicePerfCountersAll.AddRange(RSPerfCounterInfo.m_webServicePerfCountersDefault);
				RSPerfCounterInfo.m_webServicePerfCountersAll.Add(new CounterCreationData(RSPerfCounterInfo.SqlConnections, "", PerformanceCounterType.NumberOfItems32));
				RSPerfCounterInfo.m_webServicePerfCountersAll.Add(new CounterCreationData(RSPerfCounterInfo.ProcessingDatasourceConnections, "", PerformanceCounterType.NumberOfItems32));
				RSPerfCounterInfo.m_webServicePerfCountersAll.Add(new CounterCreationData(RSPerfCounterInfo.ReportServerActiveThreads, "", PerformanceCounterType.NumberOfItems32));
				RSPerfCounterInfo.m_webServicePerfCountersAll.Add(new CounterCreationData(RSPerfCounterInfo.TimeInProcessingName, "", PerformanceCounterType.NumberOfItems32));
				RSPerfCounterInfo.m_webServicePerfCountersAll.Add(new CounterCreationData(RSPerfCounterInfo.TimeInRenderingName, "", PerformanceCounterType.NumberOfItems32));
				RSPerfCounterInfo.m_webServicePerfCountersAll.Add(new CounterCreationData(RSPerfCounterInfo.TimeInDatasourceAccessName, "", PerformanceCounterType.NumberOfItems32));
				RSPerfCounterInfo.m_webServicePerfCountersAll.Add(new CounterCreationData(RSPerfCounterInfo.TimeInDatabaseName, "", PerformanceCounterType.NumberOfItems32));
				RSPerfCounterInfo.m_webServicePerfCountersAll.Add(new CounterCreationData(RSPerfCounterInfo.TimeInCompressionName, "", PerformanceCounterType.NumberOfItems32));
				RSPerfCounterInfo.m_webServicePerfCountersAll.Add(new CounterCreationData(RSPerfCounterInfo.TimeInUncompressionName, "", PerformanceCounterType.NumberOfItems32));
				RSPerfCounterInfo.m_webServicePerfCountersAll.Add(new CounterCreationData(RSPerfCounterInfo.RequestTimeName, "", PerformanceCounterType.NumberOfItems32));
				RSPerfCounterInfo.m_webServicePerfCountersAll.Add(new CounterCreationData(RSPerfCounterInfo.RowCountName, "", PerformanceCounterType.NumberOfItems32));
				RSPerfCounterInfo.m_webServicePerfCountersAll.Add(new CounterCreationData(RSPerfCounterInfo.ByteCountName, "", PerformanceCounterType.NumberOfItems32));
				RSPerfCounterInfo.m_webServicePerfCountersAll.Add(new CounterCreationData(RSPerfCounterInfo.TimeBetweenFinishName, "", PerformanceCounterType.NumberOfItems32));
				RSPerfCounterInfo.m_webServicePerfCountersAll.Add(new CounterCreationData(RSPerfCounterInfo.WaitOnNextStreamName, "", PerformanceCounterType.NumberOfItems32));
				RSPerfCounterInfo.m_webServicePerfCountersAll.Add(new CounterCreationData(RSPerfCounterInfo.SnapshotCompressionRatioName, "", PerformanceCounterType.NumberOfItems32));
				RSPerfCounterInfo.m_webServicePerfCountersAll.Add(new CounterCreationData(RSPerfCounterInfo.SnapshotBufferingRatioName, "", PerformanceCounterType.NumberOfItems32));
			}
			if (defaultOnly)
			{
				return RSPerfCounterInfo.m_webServicePerfCountersDefault;
			}
			return RSPerfCounterInfo.m_webServicePerfCountersAll;
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x000745C0 File Offset: 0x000727C0
		internal static CounterCreationDataCollection GetWindowsServiceCounterInfo(bool defaultOnly, bool isSharePointMode)
		{
			if (RSPerfCounterInfo.m_webServicePerfCountersDefault == null)
			{
				RSPerfCounterInfo.GetWebServiceCounterInfo(false);
			}
			if (RSPerfCounterInfo.m_winServicePerfCountersDefault == null)
			{
				RSPerfCounterInfo.m_winServicePerfCountersDefault = new CounterCreationDataCollection();
				RSPerfCounterInfo.m_winServicePerfCountersAll = new CounterCreationDataCollection();
				RSPerfCounterInfo.m_winServicePerfCountersDefault.AddRange(RSPerfCounterInfo.m_webServicePerfCountersDefault);
				RSPerfCounterInfo.m_winServicePerfCountersAll.AddRange(RSPerfCounterInfo.m_webServicePerfCountersAll);
				CounterCreationData counterCreationData = null;
				foreach (object obj in RSPerfCounterInfo.m_winServicePerfCountersAll)
				{
					CounterCreationData counterCreationData2 = (CounterCreationData)obj;
					if (counterCreationData2.CounterName == RSPerfCounterInfo.ActiveSessionsCount)
					{
						counterCreationData = counterCreationData2;
					}
				}
				if (counterCreationData != null)
				{
					RSPerfCounterInfo.m_winServicePerfCountersAll.Remove(counterCreationData);
					RSPerfCounterInfo.m_winServicePerfCountersDefault.Remove(counterCreationData);
				}
				RSPerfCounterInfo.AddAdditionalWinServicePerfCounters(RSPerfCounterInfo.m_winServicePerfCountersDefault, isSharePointMode);
				RSPerfCounterInfo.AddAdditionalWinServicePerfCounters(RSPerfCounterInfo.m_winServicePerfCountersAll, isSharePointMode);
			}
			if (defaultOnly)
			{
				return RSPerfCounterInfo.m_winServicePerfCountersDefault;
			}
			return RSPerfCounterInfo.m_winServicePerfCountersAll;
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x000746B0 File Offset: 0x000728B0
		internal static CounterCreationDataCollection GetSharePointServicePerfCounters()
		{
			if (RSPerfCounterInfo.m_sharepointServicePerfCounters == null)
			{
				RSPerfCounterInfo.m_sharepointServicePerfCounters = new CounterCreationDataCollection();
				RSPerfCounterInfo.m_sharepointServicePerfCounters.Add(new CounterCreationData(RSPerfCounterInfo.MemoryPressureLevel, RSPerfCounterRes.MEMORYPRESSURELEVEL, PerformanceCounterType.NumberOfItems32));
				RSPerfCounterInfo.m_sharepointServicePerfCounters.Add(new CounterCreationData(RSPerfCounterInfo.MemoryShrinkAmount, RSPerfCounterRes.MEMORYSHRINKAMOUNT, PerformanceCounterType.NumberOfItems32));
				RSPerfCounterInfo.m_sharepointServicePerfCounters.Add(new CounterCreationData(RSPerfCounterInfo.MemoryShrinkNotificationPerSecond, RSPerfCounterRes.MEMNOTIFICATIONPERSECOND, PerformanceCounterType.RateOfCountsPerSecond32));
			}
			return RSPerfCounterInfo.m_sharepointServicePerfCounters;
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x00074730 File Offset: 0x00072930
		private static void AddAdditionalWinServicePerfCounters(CounterCreationDataCollection list, bool isSharePointMode)
		{
			list.Add(new CounterCreationData(RSPerfCounterInfo.AppDomainRecycles, RSPerfCounterRes.APPDOMAINRECYCLES, PerformanceCounterType.NumberOfItems32));
			list.Add(new CounterCreationData(RSPerfCounterInfo.DeliveryCountStr, RSPerfCounterRes.DELIVERIES, PerformanceCounterType.NumberOfItems32));
			list.Add(new CounterCreationData(RSPerfCounterInfo.DeliveriesPerSecond, RSPerfCounterRes.DELIVERIESPERSECOND, PerformanceCounterType.RateOfCountsPerSecond32));
			list.Add(new CounterCreationData(RSPerfCounterInfo.EventHandleCount, RSPerfCounterRes.EVENTS, PerformanceCounterType.NumberOfItems32));
			list.Add(new CounterCreationData(RSPerfCounterInfo.EventsPerSecond, RSPerfCounterRes.EVENTSPERSECOND, PerformanceCounterType.RateOfCountsPerSecond32));
			list.Add(new CounterCreationData(RSPerfCounterInfo.SnapshotUpdates, RSPerfCounterRes.SNAPSHOTUPDATES, PerformanceCounterType.NumberOfItems32));
			list.Add(new CounterCreationData(RSPerfCounterInfo.ShapshotUpdatesPerSecond, RSPerfCounterRes.SNAPSHOTUPDATESPERSECOND, PerformanceCounterType.RateOfCountsPerSecond32));
			list.Add(new CounterCreationData(RSPerfCounterInfo.CacheFlushCount, RSPerfCounterRes.CACHEFLUSHES, PerformanceCounterType.NumberOfItems32));
			list.Add(new CounterCreationData(RSPerfCounterInfo.CacheFlushPerSecond, RSPerfCounterRes.CACHEFLUSHESPERSECOND, PerformanceCounterType.NumberOfItems32));
			if (isSharePointMode)
			{
				list.Add(new CounterCreationData(RSPerfCounterInfo.AlertingQueueSize, RSPerfCounterRes.ALERTINGQUEUESIZE, PerformanceCounterType.NumberOfItems32));
				list.Add(new CounterCreationData(RSPerfCounterInfo.FireAlertEventsProcessed, RSPerfCounterRes.FIREALERTEVENTSPROCESSED, PerformanceCounterType.NumberOfItems32));
				list.Add(new CounterCreationData(RSPerfCounterInfo.FireScheduleEventsProcessed, RSPerfCounterRes.FIRESCHEDULEEVENTSPROCESSED, PerformanceCounterType.NumberOfItems32));
				list.Add(new CounterCreationData(RSPerfCounterInfo.CreateScheduleEventsProcessed, RSPerfCounterRes.CREATESCHEDULEEVENTSPROCESSED, PerformanceCounterType.NumberOfItems32));
				list.Add(new CounterCreationData(RSPerfCounterInfo.UpdateScheduleEventsProcessed, RSPerfCounterRes.UPDATESCHEDULEEVENTSPROCESSED, PerformanceCounterType.NumberOfItems32));
				list.Add(new CounterCreationData(RSPerfCounterInfo.DeleteScheduleEventsProcessed, RSPerfCounterRes.DELETESCHEDULEEVENTSPROCESSED, PerformanceCounterType.NumberOfItems32));
				list.Add(new CounterCreationData(RSPerfCounterInfo.GenerateAlertEventsProcessed, RSPerfCounterRes.GENERATEALERTEVENTSPROCESSED, PerformanceCounterType.NumberOfItems32));
				list.Add(new CounterCreationData(RSPerfCounterInfo.DeliverAlertEventsProcessed, RSPerfCounterRes.DELIVERALERTEVENTSPROCESSED, PerformanceCounterType.NumberOfItems32));
			}
		}

		// Token: 0x04000BDE RID: 3038
		internal const string WindowServiceClass = "MSRS 2019 Windows Service";

		// Token: 0x04000BDF RID: 3039
		internal const string WebServiceClass = "MSRS 2019 Web Service";

		// Token: 0x04000BE0 RID: 3040
		internal const string WindowServiceClassKatmai = "MSRS 2008 Windows Service";

		// Token: 0x04000BE1 RID: 3041
		internal const string WebServiceClassKatmai = "MSRS 2008 Web Service";

		// Token: 0x04000BE2 RID: 3042
		internal const string WindowServiceSharePointClass = "MSRS 2019 Windows Service SharePoint Mode";

		// Token: 0x04000BE3 RID: 3043
		internal const string WebServiceSharePointClass = "MSRS 2019 Web Service SharePoint Mode";

		// Token: 0x04000BE4 RID: 3044
		internal const string SharePointServiceClass = "ReportServerSharePoint:Service";

		// Token: 0x04000BE5 RID: 3045
		internal static string ReportRequests = "Report Requests";

		// Token: 0x04000BE6 RID: 3046
		internal static string ExecutionCount = "Total Reports Executed";

		// Token: 0x04000BE7 RID: 3047
		internal static string ExecutionsPerSecond = "Reports Executed/Sec";

		// Token: 0x04000BE8 RID: 3048
		internal static string ReportProcessingErrorCount = "Total Processing Failures";

		// Token: 0x04000BE9 RID: 3049
		internal static string RejectedThreads = "Total Rejected Threads";

		// Token: 0x04000BEA RID: 3050
		internal static string ActiveSessionsCount = "Active Sessions";

		// Token: 0x04000BEB RID: 3051
		internal static string FirstSessionRequests = "First Session Requests/Sec";

		// Token: 0x04000BEC RID: 3052
		internal static string NextSessionRequests = "Next Session Requests/Sec";

		// Token: 0x04000BED RID: 3053
		internal static string TotalCacheHitsCount = "Total Cache Hits";

		// Token: 0x04000BEE RID: 3054
		internal static string TotalCacheHitsPerSecond = "Cache Hits/Sec";

		// Token: 0x04000BEF RID: 3055
		internal static string TotalCacheMissCount = "Total Cache Misses";

		// Token: 0x04000BF0 RID: 3056
		internal static string TotalCacheMissPerSecond = "Cache Misses/Sec";

		// Token: 0x04000BF1 RID: 3057
		internal static string RequestFromCachePercent = "Percent Reports from cache";

		// Token: 0x04000BF2 RID: 3058
		internal static string TotalRequestCount = "Total Requests";

		// Token: 0x04000BF3 RID: 3059
		internal static string TotalRequestPerSecond = "Requests/Sec";

		// Token: 0x04000BF4 RID: 3060
		internal static string TotalMemoryCacheHitsCount = "Total Memory Cache Hits";

		// Token: 0x04000BF5 RID: 3061
		internal static string MemoryCacheHitsPerSecond = "Memory Cache Hits/Sec";

		// Token: 0x04000BF6 RID: 3062
		internal static string TotalMemoryCacheMissCount = "Total Memory Cache Misses";

		// Token: 0x04000BF7 RID: 3063
		internal static string MemoryCacheMissPerSecond = "Memory Cache Miss/Sec";

		// Token: 0x04000BF8 RID: 3064
		internal static string ModelsTotalCacheHitsCount = "Total Cache Hits (Semantic Models)";

		// Token: 0x04000BF9 RID: 3065
		internal static string ModelsTotalCacheHitsPerSecond = "Cache Hits/Sec (Semantic Models)";

		// Token: 0x04000BFA RID: 3066
		internal static string ModelsTotalCacheMissCount = "Total Cache Misses (Semantic Models)";

		// Token: 0x04000BFB RID: 3067
		internal static string ModelsTotalCacheMissPerSecond = "Cache Misses/Sec (Semantic Models)";

		// Token: 0x04000BFC RID: 3068
		internal static string MemoryShrinkNotificationCount = "Total Memory Shrink Notification";

		// Token: 0x04000BFD RID: 3069
		internal static string MemoryShrinkNotificationPerSecond = "Memory Shrink Notifications/Sec";

		// Token: 0x04000BFE RID: 3070
		internal static string MemoryShrinkAmount = "Memory Shrink Amount";

		// Token: 0x04000BFF RID: 3071
		internal static string MemoryPressureLevel = "Memory Pressure State";

		// Token: 0x04000C00 RID: 3072
		internal static string SqlConnections = "Active Database Connections";

		// Token: 0x04000C01 RID: 3073
		internal static string ProcessingDatasourceConnections = "Active Datasource Connections";

		// Token: 0x04000C02 RID: 3074
		internal static string TotalDataSourceConnectionFailures = "Total Data Source connection failures";

		// Token: 0x04000C03 RID: 3075
		internal static string ReportServerActiveThreads = "Active Threads";

		// Token: 0x04000C04 RID: 3076
		internal static string TimeInProcessingName = "Time in Processing";

		// Token: 0x04000C05 RID: 3077
		internal static string TimeInRenderingName = "Time in Rendering";

		// Token: 0x04000C06 RID: 3078
		internal static string TimeInDatasourceAccessName = "Time in data source access";

		// Token: 0x04000C07 RID: 3079
		internal static string TimeInDatabaseName = "Time in database";

		// Token: 0x04000C08 RID: 3080
		internal static string TimeInCompressionName = "Time compressing";

		// Token: 0x04000C09 RID: 3081
		internal static string TimeInUncompressionName = "Time uncompressing";

		// Token: 0x04000C0A RID: 3082
		internal static string RequestTimeName = "Request time";

		// Token: 0x04000C0B RID: 3083
		internal static string RowCountName = "Row count";

		// Token: 0x04000C0C RID: 3084
		internal static string ByteCountName = "Byte count";

		// Token: 0x04000C0D RID: 3085
		internal static string TimeBetweenFinishName = "Time between Finish calls";

		// Token: 0x04000C0E RID: 3086
		internal static string WaitOnNextStreamName = "Time waiting on the next stream";

		// Token: 0x04000C0F RID: 3087
		internal static string SnapshotCompressionRatioName = "Snapshot compression ratio";

		// Token: 0x04000C10 RID: 3088
		internal static string SnapshotBufferingRatioName = "Snapshot buffering ratio";

		// Token: 0x04000C11 RID: 3089
		internal static string AppDomainRecycles = "Total App Domain Recycles";

		// Token: 0x04000C12 RID: 3090
		internal static string DeliveryCountStr = "Total Deliveries";

		// Token: 0x04000C13 RID: 3091
		internal static string DeliveriesPerSecond = "Delivers/Sec";

		// Token: 0x04000C14 RID: 3092
		internal static string EventHandleCount = "Total Events";

		// Token: 0x04000C15 RID: 3093
		internal static string EventsPerSecond = "Events/Sec";

		// Token: 0x04000C16 RID: 3094
		internal static string SnapshotUpdates = "Total Snapshot Updates";

		// Token: 0x04000C17 RID: 3095
		internal static string ShapshotUpdatesPerSecond = "Snapshot Updates/Sec";

		// Token: 0x04000C18 RID: 3096
		internal static string CacheFlushCount = "Total Cache Flushes";

		// Token: 0x04000C19 RID: 3097
		internal static string CacheFlushPerSecond = "Cache Flushes/Sec";

		// Token: 0x04000C1A RID: 3098
		internal static string AlertingQueueSize = "Alerting: event queue length";

		// Token: 0x04000C1B RID: 3099
		internal static string FireAlertEventsProcessed = "Alerting: events processed - FireAlert";

		// Token: 0x04000C1C RID: 3100
		internal static string FireScheduleEventsProcessed = "Alerting: events processed - FireSchedule";

		// Token: 0x04000C1D RID: 3101
		internal static string CreateScheduleEventsProcessed = "Alerting: events processed - CreateSchedule";

		// Token: 0x04000C1E RID: 3102
		internal static string UpdateScheduleEventsProcessed = "Alerting: events processed - UpdateSchedule";

		// Token: 0x04000C1F RID: 3103
		internal static string DeleteScheduleEventsProcessed = "Alerting: events processed - DeleteSchedule";

		// Token: 0x04000C20 RID: 3104
		internal static string GenerateAlertEventsProcessed = "Alerting: events processed - GenerateAlert";

		// Token: 0x04000C21 RID: 3105
		internal static string DeliverAlertEventsProcessed = "Alerting: events processed - DeliverAlert";

		// Token: 0x04000C22 RID: 3106
		private static CounterCreationDataCollection m_webServicePerfCountersDefault = null;

		// Token: 0x04000C23 RID: 3107
		private static CounterCreationDataCollection m_webServicePerfCountersAll = null;

		// Token: 0x04000C24 RID: 3108
		private static CounterCreationDataCollection m_winServicePerfCountersDefault = null;

		// Token: 0x04000C25 RID: 3109
		private static CounterCreationDataCollection m_winServicePerfCountersAll = null;

		// Token: 0x04000C26 RID: 3110
		private static CounterCreationDataCollection m_sharepointServicePerfCounters = null;
	}
}
