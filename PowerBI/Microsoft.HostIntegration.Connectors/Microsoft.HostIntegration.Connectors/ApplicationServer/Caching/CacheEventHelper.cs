using System;
using System.Diagnostics;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002E3 RID: 739
	internal static class CacheEventHelper
	{
		// Token: 0x06001B60 RID: 7008 RVA: 0x00053566 File Offset: 0x00051766
		public static void WriteError(string componentName, string eventMessage, params object[] args)
		{
			if (Provider.IsEnabled(TraceLevel.Error))
			{
				EventLogWriter.WriteError(componentName, eventMessage, args);
			}
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x00053578 File Offset: 0x00051778
		public static void WriteWarning(string componentName, string eventMessage, params object[] args)
		{
			if (Provider.IsEnabled(TraceLevel.Warning))
			{
				EventLogWriter.WriteWarning(componentName, eventMessage, args);
			}
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x0005358A File Offset: 0x0005178A
		public static void WriteInformation(string componentName, string eventMessage, params object[] args)
		{
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(componentName, eventMessage, args);
			}
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x0005359C File Offset: 0x0005179C
		public static void WriteVerbose(string componentName, string eventMessage, params object[] args)
		{
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose(componentName, eventMessage, args);
			}
		}

		// Token: 0x06001B64 RID: 7012 RVA: 0x000535AE File Offset: 0x000517AE
		public static void WriteErrorToSink(string componentName, string eventMessage, params object[] args)
		{
			EventLogWriter.WriteError(componentName, eventMessage, args);
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x000535B8 File Offset: 0x000517B8
		public static void WriteWarningToSink(string componentName, string eventMessage, params object[] args)
		{
			EventLogWriter.WriteWarning(componentName, eventMessage, args);
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x000535C2 File Offset: 0x000517C2
		public static void WriteInformationToSink(string componentName, string eventMessage, params object[] args)
		{
			EventLogWriter.WriteInfo(componentName, eventMessage, args);
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x000535CC File Offset: 0x000517CC
		public static void WriteVerboseToSink(string componentName, string eventMessage, params object[] args)
		{
			EventLogWriter.WriteVerbose(componentName, eventMessage, args);
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x000535D8 File Offset: 0x000517D8
		public static void AddSink(IEventSink sink, TraceLevel traceLevel)
		{
			TraceLevel sinkLevelToUse = CacheEventHelper.GetSinkLevelToUse(traceLevel);
			EventLogWriter.AddSink(sink, TraceUtils.GetEventLogWriterLevelFromTraceLevel(sinkLevelToUse));
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x000535F8 File Offset: 0x000517F8
		public static void ChangeSinkSetting(Type sinkType, string source, TraceLevel traceLevel)
		{
			TraceLevel sinkLevelToUse = CacheEventHelper.GetSinkLevelToUse(traceLevel);
			EventLogWriter.ChangeSinkSetting(sinkType, source, TraceUtils.GetEventLogWriterLevelFromTraceLevel(sinkLevelToUse));
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x0005361C File Offset: 0x0005181C
		private static TraceLevel GetSinkLevelToUse(TraceLevel traceLevel)
		{
			TraceLevel traceLevel2;
			if (traceLevel > TraceLevel.Info)
			{
				traceLevel2 = traceLevel;
			}
			else if (traceLevel != TraceLevel.Off)
			{
				traceLevel2 = TraceLevel.Info;
			}
			else
			{
				traceLevel2 = TraceLevel.Off;
			}
			return traceLevel2;
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x0005363B File Offset: 0x0005183B
		public static void AddEtwSink(TraceLevel traceLevel)
		{
			CacheEventHelper.AddSink(new ETWSink(), traceLevel);
		}

		// Token: 0x06001B6C RID: 7020 RVA: 0x00053648 File Offset: 0x00051848
		public static void ChangeEtwSinkSetting(TraceLevel traceLevel)
		{
			CacheEventHelper.ChangeEtwSinkSetting(traceLevel, false);
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x00053651 File Offset: 0x00051851
		public static void ChangeEtwSinkSetting(TraceLevel traceLevel, bool overrideProvider)
		{
			if (overrideProvider)
			{
				Provider.OverrideProviderLevel(traceLevel);
			}
			CacheEventHelper.ChangeSinkSetting(typeof(ETWSink), null, traceLevel);
		}
	}
}
