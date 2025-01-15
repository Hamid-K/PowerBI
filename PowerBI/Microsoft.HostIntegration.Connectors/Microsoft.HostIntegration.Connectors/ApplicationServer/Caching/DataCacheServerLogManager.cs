using System;
using System.Diagnostics;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000300 RID: 768
	internal static class DataCacheServerLogManager
	{
		// Token: 0x06001C77 RID: 7287 RVA: 0x00056309 File Offset: 0x00054509
		static DataCacheServerLogManager()
		{
			EventLogWriter.AddSink(new ConsoleSink(), -2);
			Trace.Listeners.Remove("Default");
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x00056336 File Offset: 0x00054536
		public static void ClearLogSink()
		{
			EventLogWriter.ClearSinks();
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x0005633D File Offset: 0x0005453D
		public static void AddLogSink(IEventSink eventSink, TraceLevel traceLevel)
		{
			CacheEventHelper.AddSink(eventSink, traceLevel);
			Provider.DiagnosticTraceSourceLevel = traceLevel;
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x0005634C File Offset: 0x0005454C
		public static TraceLevel ChangeLogLevel(TraceLevel traceLevel)
		{
			TraceLevel current;
			lock (DataCacheServerLogManager._staticLockObject)
			{
				current = DataCacheServerLogManager._current;
				DataCacheServerLogManager._current = traceLevel;
				if (current == TraceLevel.Off && traceLevel != TraceLevel.Off)
				{
					if (CloudUtility.IsVASDeployment)
					{
						DataCacheServerLogManager.AddDiagnosticTraceSink(traceLevel);
					}
					else
					{
						CacheEventHelper.AddEtwSink(traceLevel);
					}
				}
				else if (current != TraceLevel.Off && traceLevel == TraceLevel.Off)
				{
					EventLogWriter.ClearSinks();
				}
				else if (current != traceLevel)
				{
					if (CloudUtility.IsVASDeployment)
					{
						DataCacheServerLogManager.AddDiagnosticTraceSink(traceLevel);
					}
					else
					{
						CacheEventHelper.ChangeEtwSinkSetting(traceLevel);
					}
				}
			}
			return current;
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x000563D8 File Offset: 0x000545D8
		internal static void AddDiagnosticTraceSink(TraceLevel traceLevel)
		{
			EventLogWriter.ClearSinks();
			string diagnosticProviderString = DataCacheServerLogManager.GetDiagnosticProviderString();
			CacheEventHelper.AddSink(new DiagnosticsTraceSink(diagnosticProviderString), traceLevel);
			Provider.DiagnosticTraceSourceLevel = traceLevel;
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x00056404 File Offset: 0x00054604
		private static string GetDiagnosticProviderString()
		{
			string text = string.Empty;
			if (CloudUtility.CloudProvider != null)
			{
				text = CloudUtility.CloudProvider.GetConfigurationValue("TraceProviderConfiguration");
			}
			return text;
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x00056430 File Offset: 0x00054630
		internal static void CreateLogSink(DataCacheLogSink userSink)
		{
			lock (DataCacheServerLogManager._staticLockObject)
			{
				EventLogWriter.ClearSinks();
				IEventSink eventSink = null;
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
				{
					string diagnosticProviderString = DataCacheServerLogManager.GetDiagnosticProviderString();
					eventSink = new DiagnosticsTraceSink(diagnosticProviderString);
					break;
				}
				}
				eventSink.Load(userSink.Param);
				CacheEventHelper.AddSink(eventSink, userSink.Level);
				DataCacheServerLogManager._current = userSink.Level;
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose<string, string>("DataCacheServerLogManager", "Sink created - Type: {0} ; Level: {1}", userSink.Type.ToString(), userSink.Level.ToString());
				}
			}
		}

		// Token: 0x04000F66 RID: 3942
		private static object _staticLockObject = new object();

		// Token: 0x04000F67 RID: 3943
		private static TraceLevel _current = TraceLevel.Off;
	}
}
