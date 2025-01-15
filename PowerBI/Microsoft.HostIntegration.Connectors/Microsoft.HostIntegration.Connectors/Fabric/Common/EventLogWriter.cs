using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003D3 RID: 979
	internal static class EventLogWriter
	{
		// Token: 0x06002266 RID: 8806 RVA: 0x0006A188 File Offset: 0x00068388
		private static SinkWrapper[] LoadSinkFromConfig()
		{
			ConfigFile config = ConfigFile.Config;
			if (config != null)
			{
				EventLogWriter.s_sinkList = (ArrayList)config.GetValue("sinks");
				if (EventLogWriter.s_sinkList == null || EventLogWriter.s_sinkList.Count == 0)
				{
					SinkWrapper[] array = new SinkWrapper[]
					{
						new SinkWrapper(new ConsoleSink(), 0, null)
					};
					EventLogWriter.s_maxConfiguredTraceLevel = 0;
					return array;
				}
			}
			return EventLogWriter.CreateSinks();
		}

		// Token: 0x06002267 RID: 8807 RVA: 0x0006A1EC File Offset: 0x000683EC
		private static SinkWrapper[] CreateSinks()
		{
			if (EventLogWriter.s_sinkList == null)
			{
				return new SinkWrapper[0];
			}
			List<SinkWrapper> list = new List<SinkWrapper>();
			int num = -1;
			for (int i = 0; i < EventLogWriter.s_sinkList.Count; i++)
			{
				EventLogger eventLogger = (EventLogger)EventLogWriter.s_sinkList[i];
				if (eventLogger.defaultLevel >= 0)
				{
					SinkWrapper sinkWrapper = new SinkWrapper(eventLogger.Sink, eventLogger.defaultLevel, eventLogger.sourceOverride);
					if (sinkWrapper.MaxTraceLevel > num)
					{
						num = sinkWrapper.MaxTraceLevel;
					}
					list.Add(sinkWrapper);
				}
			}
			EventLogWriter.s_maxConfiguredTraceLevel = num;
			return list.ToArray();
		}

		// Token: 0x06002268 RID: 8808 RVA: 0x0006A280 File Offset: 0x00068480
		public static object ChangeSinkSetting(Type sinkType, string source, int level)
		{
			if (EventLogWriter.s_sinkList == null)
			{
				return null;
			}
			ArrayList arrayList = new ArrayList(EventLogWriter.s_sinkList.Count);
			for (int i = 0; i < EventLogWriter.s_sinkList.Count; i++)
			{
				arrayList.Add(new EventLogger((EventLogger)EventLogWriter.s_sinkList[i]));
			}
			bool flag = false;
			for (int j = EventLogWriter.s_sinkList.Count - 1; j >= 0; j--)
			{
				EventLogger eventLogger = (EventLogger)EventLogWriter.s_sinkList[j];
				if (eventLogger.Sink.GetType() == sinkType || sinkType == null)
				{
					if (source == null)
					{
						eventLogger.defaultLevel = level;
					}
					else
					{
						if (eventLogger.sourceOverride == null)
						{
							eventLogger.sourceOverride = new Hashtable();
						}
						if (level >= -1)
						{
							eventLogger.sourceOverride[source] = level.ToString(CultureInfo.InvariantCulture);
						}
						else
						{
							eventLogger.sourceOverride.Remove(source);
						}
					}
					flag = true;
				}
			}
			if (!flag)
			{
				return null;
			}
			EventLogWriter.s_sinks = EventLogWriter.CreateSinks();
			return arrayList;
		}

		// Token: 0x06002269 RID: 8809 RVA: 0x0006A374 File Offset: 0x00068574
		public static void RestoreSetting(object config)
		{
			EventLogWriter.s_sinkList = (ArrayList)config;
			EventLogWriter.s_sinks = EventLogWriter.CreateSinks();
		}

		// Token: 0x0600226A RID: 8810 RVA: 0x0006A38C File Offset: 0x0006858C
		public static void AddSink(IEventSink sink, int defaultLevel)
		{
			EventLogger eventLogger = new EventLogger();
			eventLogger.Sink = sink;
			eventLogger.defaultLevel = defaultLevel;
			eventLogger.sourceOverride = null;
			if (EventLogWriter.s_sinkList == null)
			{
				EventLogWriter.s_sinkList = new ArrayList();
			}
			EventLogWriter.s_sinkList.Add(eventLogger);
			EventLogWriter.s_sinks = EventLogWriter.CreateSinks();
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x0006A3DB File Offset: 0x000685DB
		public static void ClearSinks()
		{
			EventLogWriter.s_sinkList = null;
			EventLogWriter.s_sinks = EventLogWriter.CreateSinks();
		}

		// Token: 0x0600226C RID: 8812 RVA: 0x0006A3F0 File Offset: 0x000685F0
		public static void WriteError(string src, string format, params object[] args)
		{
			foreach (SinkWrapper sinkWrapper in EventLogWriter.s_sinks)
			{
				sinkWrapper.WriteEntry(0, src, TraceEventType.Error, format, args);
			}
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x0006A420 File Offset: 0x00068620
		public static void WriteWarning(string src, string format, params object[] args)
		{
			foreach (SinkWrapper sinkWrapper in EventLogWriter.s_sinks)
			{
				sinkWrapper.WriteEntry(1, src, TraceEventType.Warning, format, args);
			}
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x0006A450 File Offset: 0x00068650
		public static void WriteInfo(string src, string format, params object[] args)
		{
			foreach (SinkWrapper sinkWrapper in EventLogWriter.s_sinks)
			{
				sinkWrapper.WriteEntry(2, src, TraceEventType.Information, format, args);
			}
		}

		// Token: 0x0600226F RID: 8815 RVA: 0x0006A480 File Offset: 0x00068680
		public static void WriteVerbose(string src, string format, object[] args)
		{
			if (EventLogWriter.s_maxConfiguredTraceLevel >= 3)
			{
				EventLogWriter.WriteEntry(3, TraceEventType.Verbose, src, format, args);
			}
		}

		// Token: 0x06002270 RID: 8816 RVA: 0x0006A495 File Offset: 0x00068695
		public static void WriteVerbose(string src, string format)
		{
			if (EventLogWriter.s_maxConfiguredTraceLevel >= 3)
			{
				EventLogWriter.WriteEntry(3, TraceEventType.Verbose, src, format, new object[0]);
			}
		}

		// Token: 0x06002271 RID: 8817 RVA: 0x0006A4B0 File Offset: 0x000686B0
		public static void WriteVerbose<T1>(string src, string format, T1 t1)
		{
			if (EventLogWriter.s_maxConfiguredTraceLevel >= 3)
			{
				EventLogWriter.WriteEntry(3, TraceEventType.Verbose, src, format, new object[] { t1 });
			}
		}

		// Token: 0x06002272 RID: 8818 RVA: 0x0006A4E0 File Offset: 0x000686E0
		public static void WriteVerbose<T1, T2>(string src, string format, T1 t1, T2 t2)
		{
			if (EventLogWriter.s_maxConfiguredTraceLevel >= 3)
			{
				EventLogWriter.WriteEntry(3, TraceEventType.Verbose, src, format, new object[] { t1, t2 });
			}
		}

		// Token: 0x06002273 RID: 8819 RVA: 0x0006A51C File Offset: 0x0006871C
		public static void WriteVerbose<T1, T2, T3>(string src, string format, T1 t1, T2 t2, T3 t3)
		{
			if (EventLogWriter.s_maxConfiguredTraceLevel >= 3)
			{
				EventLogWriter.WriteEntry(3, TraceEventType.Verbose, src, format, new object[] { t1, t2, t3 });
			}
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x0006A560 File Offset: 0x00068760
		public static void WriteVerbose<T1, T2, T3, T4>(string src, string format, T1 t1, T2 t2, T3 t3, T4 t4)
		{
			if (EventLogWriter.s_maxConfiguredTraceLevel >= 3)
			{
				EventLogWriter.WriteEntry(3, TraceEventType.Verbose, src, format, new object[] { t1, t2, t3, t4 });
			}
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x0006A5B0 File Offset: 0x000687B0
		public static void WriteVerbose<T1, T2, T3, T4, T5>(string src, string format, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5)
		{
			if (EventLogWriter.s_maxConfiguredTraceLevel >= 3)
			{
				EventLogWriter.WriteEntry(3, TraceEventType.Verbose, src, format, new object[] { t1, t2, t3, t4, t5 });
			}
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x0006A608 File Offset: 0x00068808
		public static void WriteVerbose<T1, T2, T3, T4, T5, T6>(string src, string format, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6)
		{
			if (EventLogWriter.s_maxConfiguredTraceLevel >= 3)
			{
				EventLogWriter.WriteEntry(3, TraceEventType.Verbose, src, format, new object[] { t1, t2, t3, t4, t5, t6 });
			}
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x0006A66C File Offset: 0x0006886C
		public static void WriteEntry(int msgLevel, TraceEventType msgType, string src, string format, params object[] args)
		{
			foreach (SinkWrapper sinkWrapper in EventLogWriter.s_sinks)
			{
				sinkWrapper.WriteEntry(msgLevel, src, msgType, format, args);
			}
		}

		// Token: 0x040015B0 RID: 5552
		private static ArrayList s_sinkList;

		// Token: 0x040015B1 RID: 5553
		private static int s_maxConfiguredTraceLevel;

		// Token: 0x040015B2 RID: 5554
		private static SinkWrapper[] s_sinks = EventLogWriter.LoadSinkFromConfig();
	}
}
