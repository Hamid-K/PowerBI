using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000B8 RID: 184
	public static class DataCacheClientLogManager
	{
		// Token: 0x06000465 RID: 1125 RVA: 0x0001510D File Offset: 0x0001330D
		static DataCacheClientLogManager()
		{
			EventLogWriter.AddSink(new ConsoleSink(), -2);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00015138 File Offset: 0x00013338
		internal static void Initialize(DataCacheLogSink logSink)
		{
			lock (DataCacheClientLogManager._staticLockObject)
			{
				object obj = EventLogWriter.ChangeSinkSetting(typeof(FileEventSink), null, 3);
				if (obj != null)
				{
					EventLogWriter.RestoreSetting(obj);
					DataCacheClientLogManager._initialized = true;
				}
				if (!DataCacheClientLogManager._initialized)
				{
					if (logSink == null)
					{
						logSink = new DataCacheLogSink(DataCacheSinkType.Etw, TraceLevel.Verbose);
					}
					DataCacheClientLogManager.SetSink(TraceUtils.GetTraceSinkTypeFromLogSinkType(logSink), logSink.Level);
					DataCacheClientLogManager.SetProviderDiagnosticTraceLevel(logSink.Level);
					DataCacheClientLogManager._initialized = true;
				}
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x000151C8 File Offset: 0x000133C8
		internal static void DisableLogSinks()
		{
			lock (DataCacheClientLogManager._staticLockObject)
			{
				EventLogWriter.ClearSinks();
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00015208 File Offset: 0x00013408
		public static TraceLevel ChangeLogLevel(TraceLevel level)
		{
			TraceLevel current;
			lock (DataCacheClientLogManager._staticLockObject)
			{
				current = DataCacheClientLogManager._current;
				DataCacheClientLogManager._current = level;
				if (current == TraceLevel.Off && level != TraceLevel.Off)
				{
					if (DataCacheClientLogManager._sinks.Count > 0)
					{
						DataCacheClientLogManager.RefillEventLogWriterSinks(level);
					}
				}
				else if (current != TraceLevel.Off && level == TraceLevel.Off)
				{
					EventLogWriter.ClearSinks();
				}
				else if (current != level)
				{
					foreach (DataCacheLogSink dataCacheLogSink in DataCacheClientLogManager._sinks)
					{
						Type type = null;
						switch (dataCacheLogSink.Type)
						{
						case DataCacheSinkType.Console:
							type = typeof(ConsoleSink);
							break;
						case DataCacheSinkType.File:
							type = typeof(FileEventSink);
							break;
						case DataCacheSinkType.Etw:
							type = typeof(ETWSink);
							break;
						case DataCacheSinkType.DiagnosticsTraceSink:
							type = typeof(DiagnosticsTraceSink);
							break;
						}
						CacheEventHelper.ChangeSinkSetting(type, null, level);
					}
				}
				DataCacheClientLogManager.SetProviderDiagnosticTraceLevel(level);
			}
			return current;
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00015328 File Offset: 0x00013528
		private static void RefillEventLogWriterSinks(TraceLevel traceLevel)
		{
			lock (DataCacheClientLogManager._staticLockObject)
			{
				foreach (DataCacheLogSink dataCacheLogSink in DataCacheClientLogManager._sinks)
				{
					IEventSink ieventSinkFromDataCacheSinkType = TraceUtils.GetIEventSinkFromDataCacheSinkType(dataCacheLogSink);
					if (dataCacheLogSink.Type == DataCacheSinkType.DiagnosticsTraceSink)
					{
						Provider.DiagnosticTraceLevel = dataCacheLogSink.Level;
					}
					ieventSinkFromDataCacheSinkType.Load(dataCacheLogSink.Param);
					CacheEventHelper.AddSink(ieventSinkFromDataCacheSinkType, traceLevel);
				}
			}
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose("DistributedCache.DataCacheClientLogManager", "Sinks Refilled in EventLogWriter.");
			}
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x000153E4 File Offset: 0x000135E4
		public static void SetSink(DataCacheTraceSink traceSink, TraceLevel traceLevel)
		{
			lock (DataCacheClientLogManager._staticLockObject)
			{
				DataCacheSinkType logSinkTypeFromTraceSinkType = TraceUtils.GetLogSinkTypeFromTraceSinkType(traceSink);
				if (DataCacheClientLogManager._sinks.Count == 1 && DataCacheClientLogManager._sinks[0].Type == logSinkTypeFromTraceSinkType)
				{
					DataCacheClientLogManager.ChangeLogLevel(traceLevel);
					return;
				}
				DataCacheLogSink dataCacheLogSink = new DataCacheLogSink(logSinkTypeFromTraceSinkType, traceLevel);
				IEventSink ieventSinkFromDataCacheSinkType = TraceUtils.GetIEventSinkFromDataCacheSinkType(dataCacheLogSink);
				if (dataCacheLogSink.Type == DataCacheSinkType.DiagnosticsTraceSink)
				{
					Provider.DiagnosticTraceLevel = dataCacheLogSink.Level;
				}
				ieventSinkFromDataCacheSinkType.Load(dataCacheLogSink.Param);
				EventLogWriter.ClearSinks();
				CacheEventHelper.AddSink(ieventSinkFromDataCacheSinkType, traceLevel);
				DataCacheClientLogManager._current = traceLevel;
				DataCacheClientLogManager._sinks.Clear();
				DataCacheClientLogManager._sinks.Add(dataCacheLogSink);
				DataCacheClientLogManager._initialized = true;
			}
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose("DistributedCache.DataCacheClientLogManager", "Sinks created.");
			}
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x000154C8 File Offset: 0x000136C8
		private static void SetProviderDiagnosticTraceLevel(TraceLevel level)
		{
			foreach (DataCacheLogSink dataCacheLogSink in DataCacheClientLogManager._sinks)
			{
				if (dataCacheLogSink.Type == DataCacheSinkType.DiagnosticsTraceSink)
				{
					Provider.DiagnosticTraceLevel = level;
				}
			}
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00015524 File Offset: 0x00013724
		internal static void CreateLogSinks(List<DataCacheLogSink> sinkList)
		{
			lock (DataCacheClientLogManager._staticLockObject)
			{
				if (sinkList.Count > 0)
				{
					DataCacheClientLogManager._initialized = true;
					EventLogWriter.ClearSinks();
				}
				foreach (DataCacheLogSink dataCacheLogSink in sinkList)
				{
					IEventSink ieventSinkFromDataCacheSinkType = TraceUtils.GetIEventSinkFromDataCacheSinkType(dataCacheLogSink);
					if (dataCacheLogSink.Type == DataCacheSinkType.DiagnosticsTraceSink)
					{
						Provider.DiagnosticTraceLevel = dataCacheLogSink.Level;
					}
					ieventSinkFromDataCacheSinkType.Load(dataCacheLogSink.Param);
					CacheEventHelper.AddSink(ieventSinkFromDataCacheSinkType, dataCacheLogSink.Level);
					DataCacheClientLogManager._sinks.Add(dataCacheLogSink);
				}
			}
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose("DistributedCache.DataCacheClientLogManager", "Sinks created.");
			}
		}

		// Token: 0x04000347 RID: 839
		private static object _staticLockObject = new object();

		// Token: 0x04000348 RID: 840
		private static TraceLevel _current = TraceLevel.Off;

		// Token: 0x04000349 RID: 841
		private static bool _initialized;

		// Token: 0x0400034A RID: 842
		private static List<DataCacheLogSink> _sinks = new List<DataCacheLogSink>();
	}
}
