using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000394 RID: 916
	internal static class TraceUtils
	{
		// Token: 0x0600203E RID: 8254 RVA: 0x00062160 File Offset: 0x00060360
		internal static DataCacheSinkType GetLogSinkTypeFromTraceSinkType(DataCacheTraceSink dataCacheTraceSink)
		{
			switch (dataCacheTraceSink)
			{
			case DataCacheTraceSink.EtwSink:
				return DataCacheSinkType.Etw;
			case DataCacheTraceSink.DiagnosticSink:
				return DataCacheSinkType.DiagnosticsTraceSink;
			default:
				throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "InvalidValue"), "dataCacheTraceSink");
			}
		}

		// Token: 0x0600203F RID: 8255 RVA: 0x0006219C File Offset: 0x0006039C
		internal static DataCacheTraceSink GetTraceSinkTypeFromLogSinkType(DataCacheLogSink logSinks)
		{
			switch (logSinks.Type)
			{
			case DataCacheSinkType.Etw:
				return DataCacheTraceSink.EtwSink;
			case DataCacheSinkType.DiagnosticsTraceSink:
				return DataCacheTraceSink.DiagnosticSink;
			default:
				throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "InvalidValue"), "logSinks");
			}
		}

		// Token: 0x06002040 RID: 8256 RVA: 0x000621E0 File Offset: 0x000603E0
		internal static int GetEventLogWriterLevelFromTraceLevel(TraceLevel traceLevel)
		{
			int num;
			switch (traceLevel)
			{
			case TraceLevel.Off:
				num = -1;
				break;
			case TraceLevel.Error:
				num = 0;
				break;
			case TraceLevel.Warning:
				num = 1;
				break;
			case TraceLevel.Info:
				num = 2;
				break;
			case TraceLevel.Verbose:
				num = 3;
				break;
			default:
				num = -1;
				break;
			}
			return num;
		}

		// Token: 0x06002041 RID: 8257 RVA: 0x00062224 File Offset: 0x00060424
		internal static IEventSink GetIEventSinkFromDataCacheSinkType(DataCacheLogSink userSink)
		{
			IEventSink eventSink;
			switch (userSink.Type)
			{
			case DataCacheSinkType.Console:
				eventSink = new ConsoleSink();
				break;
			case DataCacheSinkType.File:
				eventSink = new FileEventSink();
				break;
			case DataCacheSinkType.Etw:
				eventSink = new ETWSink();
				break;
			case DataCacheSinkType.DiagnosticsTraceSink:
				eventSink = new DiagnosticsTraceSink();
				break;
			default:
				eventSink = null;
				break;
			}
			return eventSink;
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x00062274 File Offset: 0x00060474
		public static bool GetTraceLevelFromString(string traceLevelString, out TraceLevel traceLevel)
		{
			if (traceLevelString != null)
			{
				if (<PrivateImplementationDetails>{C5649821-9298-4CE6-8FD1-A6928AD7CE48}.$$method0x6001b62-1 == null)
				{
					<PrivateImplementationDetails>{C5649821-9298-4CE6-8FD1-A6928AD7CE48}.$$method0x6001b62-1 = new Dictionary<string, int>(10)
					{
						{ "OFF", 0 },
						{ "ERROR", 1 },
						{ "WARNING", 2 },
						{ "INFO", 3 },
						{ "VERBOSE", 4 },
						{ "WARNINGWITHFAILEDREQ", 5 },
						{ "INFOWITHALLREQ", 6 },
						{ "NOBUFFERING", 7 },
						{ "WARNINGWITHFAILEDREQEXT", 8 },
						{ "INFOWITHALLREQLITE", 9 }
					};
				}
				int num;
				if (<PrivateImplementationDetails>{C5649821-9298-4CE6-8FD1-A6928AD7CE48}.$$method0x6001b62-1.TryGetValue(traceLevelString, out num))
				{
					switch (num)
					{
					case 0:
						traceLevel = TraceLevel.Off;
						DiagConfigManager.DiagMode = VelocityDiagMode.NoBuffering;
						break;
					case 1:
						traceLevel = TraceLevel.Error;
						DiagConfigManager.DiagMode = VelocityDiagMode.NoBuffering;
						break;
					case 2:
						traceLevel = TraceLevel.Warning;
						DiagConfigManager.DiagMode = VelocityDiagMode.NoBuffering;
						break;
					case 3:
						traceLevel = TraceLevel.Info;
						DiagConfigManager.DiagMode = VelocityDiagMode.NoBuffering;
						break;
					case 4:
						traceLevel = TraceLevel.Verbose;
						DiagConfigManager.DiagMode = VelocityDiagMode.NoBuffering;
						break;
					case 5:
						traceLevel = TraceLevel.Warning;
						DiagConfigManager.DiagMode = VelocityDiagMode.WarningWithFailedReq;
						break;
					case 6:
						traceLevel = TraceLevel.Info;
						DiagConfigManager.DiagMode = VelocityDiagMode.InfoWithAllReq;
						break;
					case 7:
						traceLevel = TraceLevel.Info;
						DiagConfigManager.DiagMode = VelocityDiagMode.NoBuffering;
						break;
					case 8:
						traceLevel = TraceLevel.Info;
						DiagConfigManager.DiagMode = VelocityDiagMode.WarningWithFailedReqExt;
						break;
					case 9:
						traceLevel = TraceLevel.Info;
						DiagConfigManager.DiagMode = VelocityDiagMode.InfoWithAllReqLite;
						break;
					default:
						goto IL_014D;
					}
					return true;
				}
			}
			IL_014D:
			traceLevel = TraceLevel.Off;
			return false;
		}

		// Token: 0x06002043 RID: 8259 RVA: 0x000623D4 File Offset: 0x000605D4
		internal static void AddFileSink(string logLocation, TraceLevel level)
		{
			DataCacheLogSink dataCacheLogSink = new DataCacheLogSink(DataCacheSinkType.File);
			IEventSink ieventSinkFromDataCacheSinkType = TraceUtils.GetIEventSinkFromDataCacheSinkType(dataCacheLogSink);
			string text = Path.Combine(logLocation, "DCacheTrace[$]/dd-HH");
			ieventSinkFromDataCacheSinkType.Load(text);
			DataCacheServerLogManager.AddLogSink(ieventSinkFromDataCacheSinkType, level);
		}
	}
}
