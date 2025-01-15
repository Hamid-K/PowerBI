using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000825 RID: 2085
	public class Logger
	{
		// Token: 0x17000FB0 RID: 4016
		// (get) Token: 0x06004222 RID: 16930 RVA: 0x000DCD5C File Offset: 0x000DAF5C
		public static List<IDrdaTraceListener> CustomLoggers
		{
			get
			{
				return Logger.customLoggers;
			}
		}

		// Token: 0x17000FB1 RID: 4017
		// (set) Token: 0x06004223 RID: 16931 RVA: 0x000DCD63 File Offset: 0x000DAF63
		public static Action<LogData> TracePointLogger
		{
			set
			{
				Logger._tracePointLogger = value;
			}
		}

		// Token: 0x06004224 RID: 16932 RVA: 0x000DCD6C File Offset: 0x000DAF6C
		static Logger()
		{
			Logger.RefreshTraceConfiguration();
			Logger.StartTracingThread();
			Logger.StartEventThread();
		}

		// Token: 0x06004225 RID: 16933 RVA: 0x000DCE30 File Offset: 0x000DB030
		private static void StartTracingThread()
		{
			if (Logger._tracegWriteThread != null && Logger._tracegWriteThread.ThreadState == global::System.Threading.ThreadState.Running)
			{
				return;
			}
			Logger._tracegWriteThread = new Thread(delegate
			{
				for (;;)
				{
					Logger._writeTraceSemaphore.WaitSignaled();
					try
					{
						while ((Logger._traceReadBuffer.Equals(Logger._traceWriteBuffer) && Logger._currentEntryReadIndex <= Logger._currentEntryWriteIndex) || (!Logger._traceReadBuffer.Equals(Logger._traceWriteBuffer) && Logger._currentEntryReadIndex < 10000))
						{
							LogData logData = Logger._traceReadBuffer.LogDataArray[Logger._currentEntryReadIndex];
							if (logData != null)
							{
								foreach (IDrdaTraceListener drdaTraceListener in Logger.customLoggers)
								{
									if (drdaTraceListener.TraceLevel >= logData.level)
									{
										drdaTraceListener.TraceEvent(logData.evenType, logData.id, logData.message);
									}
								}
								if (Logger.dadt != null)
								{
									Logger.dadt.TraceEvent(logData.evenType, logData.id, logData.message);
								}
								Logger._traceReadBuffer.LogDataArray[Logger._currentEntryReadIndex] = null;
								Logger._traceEntryBuffered = Interlocked.Decrement(ref Logger._traceEntryBuffered);
							}
							else if (Logger._traceReadBuffer.Equals(Logger._traceWriteBuffer) && Logger._currentEntryReadIndex == Logger._currentEntryWriteIndex)
							{
								break;
							}
							Logger._currentEntryReadIndex++;
							if (Logger._currentEntryReadIndex == 10000)
							{
								TraceEntryArrayPool.Put(Logger._traceReadBuffer);
								Logger._currentEntryReadIndex = 0;
								if (Logger._traceQueue.Count > 0)
								{
									Logger._traceQueue.TryDequeue(out Logger._traceReadBuffer);
								}
								else
								{
									if (Logger._traceReadBuffer.Equals(Logger._traceWriteBuffer))
									{
										break;
									}
									Logger._traceReadBuffer = Logger._traceWriteBuffer;
								}
							}
						}
					}
					catch (Exception ex)
					{
						Logger.Error(0, "Failed to write trace data: " + ex.Message, Array.Empty<object>());
						ArrayList arrayList = new ArrayList();
						foreach (IDrdaTraceListener drdaTraceListener2 in Logger.customLoggers)
						{
							try
							{
								drdaTraceListener2.TraceEvent(TraceEventType.Error, 0, string.Format("{0}:[{1}] {2}", 0, DateTime.Now.ToString("MMM dd yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture), "Checking tracing listener..."));
							}
							catch (Exception)
							{
								arrayList.Add(drdaTraceListener2);
							}
						}
						if (arrayList.Count > 0)
						{
							foreach (object obj in arrayList)
							{
								IDrdaTraceListener drdaTraceListener3 = (IDrdaTraceListener)obj;
								Logger.Error(0, string.Format("There was problem with tracing listener: {0}. It will be detached from the Drda service.", drdaTraceListener3.GetType().ToString()), Array.Empty<object>());
								Logger.customLoggers.Remove(drdaTraceListener3);
							}
						}
					}
					finally
					{
						Logger._writeTraceSemaphore.Reset();
					}
				}
			});
			Logger._tracegWriteThread.IsBackground = true;
			Logger._tracegWriteThread.Start();
		}

		// Token: 0x17000FB2 RID: 4018
		// (get) Token: 0x06004226 RID: 16934 RVA: 0x000DCE8F File Offset: 0x000DB08F
		// (set) Token: 0x06004227 RID: 16935 RVA: 0x000DCE96 File Offset: 0x000DB096
		public static Dictionary<int, DrdaEvent> DrdaEventMessageMappings
		{
			get
			{
				return Logger.drdaEventMessages;
			}
			set
			{
				Logger.drdaEventMessages = value;
			}
		}

		// Token: 0x06004228 RID: 16936 RVA: 0x000DCEA0 File Offset: 0x000DB0A0
		private static void StartEventThread()
		{
			if (Logger._eventLogWriteThread != null && Logger._eventLogWriteThread.ThreadState == global::System.Threading.ThreadState.Running)
			{
				return;
			}
			Logger._eventLogWriteThread = new Thread(delegate
			{
				for (;;)
				{
					try
					{
						DrdaEvent drdaEvent = null;
						while (Logger._eventQueue.TryDequeue(out drdaEvent))
						{
							if (Logger.evtl != null)
							{
								Logger.evtl.EventLog.WriteEntry(drdaEvent.generalText, drdaEvent.eventType, drdaEvent.eventId, drdaEvent.category);
							}
						}
						if (Logger.evtl != null)
						{
							Logger.evtl.Flush();
						}
					}
					catch
					{
						Logger.Warning(0, "Failed to write to EventLog", Array.Empty<object>());
					}
					finally
					{
						Logger._writeEventSemaphore.Reset();
					}
					Logger._writeEventSemaphore.Wait();
				}
			});
			Logger._eventLogWriteThread.IsBackground = true;
			Logger._eventLogWriteThread.Start();
		}

		// Token: 0x06004229 RID: 16937 RVA: 0x000DCF00 File Offset: 0x000DB100
		public static void Flush()
		{
			if (!Logger._writeTraceSemaphore.Signaled)
			{
				Logger._writeTraceSemaphore.Signal();
			}
			Thread.Sleep(100);
			foreach (IDrdaTraceListener drdaTraceListener in Logger.customLoggers)
			{
				drdaTraceListener.Flush();
			}
		}

		// Token: 0x0600422A RID: 16938 RVA: 0x000DCF6C File Offset: 0x000DB16C
		public static void LogEvent(int eventId)
		{
			if (Logger.evtl == null || Logger.evtl.EventLog == null)
			{
				return;
			}
			if (Logger._loggedEventIds.ContainsKey(eventId))
			{
				return;
			}
			if (Logger.drdaEventMessages.ContainsKey(eventId))
			{
				Logger.WriteToEventQueue(Logger.drdaEventMessages[eventId]);
				Logger._loggedEventIds[eventId] = true;
				return;
			}
			Logger.Warning(0, string.Format("Event {0} is not defined", eventId), Array.Empty<object>());
		}

		// Token: 0x0600422B RID: 16939 RVA: 0x000DCFF0 File Offset: 0x000DB1F0
		public static void LogEventWithParams(int eventId, string param)
		{
			if (Logger.evtl == null || Logger.evtl.EventLog == null)
			{
				return;
			}
			if (Logger._loggedEventIds.ContainsKey(eventId))
			{
				return;
			}
			if (Logger.drdaEventMessages.ContainsKey(eventId))
			{
				DrdaEvent drdaEvent = Logger.drdaEventMessages[eventId];
				drdaEvent.generalText = string.Format(drdaEvent.generalText, param);
				Logger.WriteToEventQueue(drdaEvent);
				Logger._loggedEventIds[eventId] = true;
				return;
			}
			Logger.Warning(0, string.Format("Event {0} is not defined", eventId), Array.Empty<object>());
		}

		// Token: 0x0600422C RID: 16940 RVA: 0x000DD085 File Offset: 0x000DB285
		private static TraceEventType LogEventToTraceEventType(EventLogEntryType eventLogEntryType)
		{
			if (eventLogEntryType == EventLogEntryType.Error)
			{
				return TraceEventType.Error;
			}
			if (eventLogEntryType != EventLogEntryType.Warning)
			{
				return TraceEventType.Information;
			}
			return TraceEventType.Warning;
		}

		// Token: 0x0600422D RID: 16941 RVA: 0x000DD094 File Offset: 0x000DB294
		public static void LogEventWithParams(int eventId, string param1, string param2)
		{
			if (Logger.evtl == null || Logger.evtl.EventLog == null)
			{
				return;
			}
			if (Logger._loggedEventIds.ContainsKey(eventId))
			{
				return;
			}
			if (Logger.drdaEventMessages.ContainsKey(eventId))
			{
				DrdaEvent drdaEvent = Logger.drdaEventMessages[eventId];
				drdaEvent.generalText = string.Format(drdaEvent.generalText, param1, param2);
				Logger.WriteToEventQueue(drdaEvent);
				Logger._loggedEventIds[eventId] = true;
				return;
			}
			Logger.Warning(0, string.Format("Event {0} is not defined", eventId), Array.Empty<object>());
		}

		// Token: 0x0600422E RID: 16942 RVA: 0x000DD12C File Offset: 0x000DB32C
		public static void LogEventWithParams(int eventId, string param1, string param2, string param3)
		{
			if (Logger.evtl == null || Logger.evtl.EventLog == null)
			{
				return;
			}
			if (Logger._loggedEventIds.ContainsKey(eventId))
			{
				return;
			}
			if (Logger.drdaEventMessages.ContainsKey(eventId))
			{
				DrdaEvent drdaEvent = Logger.drdaEventMessages[eventId];
				drdaEvent.generalText = string.Format(drdaEvent.generalText, param1, param2, param3);
				Logger.WriteToEventQueue(drdaEvent);
				Logger._loggedEventIds[eventId] = true;
				return;
			}
			Logger.Warning(0, string.Format("Event {0} is not defined", eventId), Array.Empty<object>());
		}

		// Token: 0x0600422F RID: 16943 RVA: 0x000DD1C4 File Offset: 0x000DB3C4
		private static void WriteToEventQueue(DrdaEvent de)
		{
			if (Logger.evtl.Filter.ShouldTrace(null, "DrdaService1", Logger.LogEventToTraceEventType(de.eventType), de.eventId, de.generalText, null, null, null))
			{
				Logger._eventQueue.Enqueue(de);
				if (!Logger._writeEventSemaphore.IsSet)
				{
					Logger._writeEventSemaphore.Set();
				}
			}
		}

		// Token: 0x06004230 RID: 16944 RVA: 0x000DD224 File Offset: 0x000DB424
		public static void LogEvent(int eventId, string msg)
		{
			if (Logger.evtl == null || Logger.evtl.EventLog == null)
			{
				return;
			}
			if (Logger._loggedEventIds.ContainsKey(eventId))
			{
				return;
			}
			if (Logger.drdaEventMessages.ContainsKey(eventId))
			{
				DrdaEvent drdaEvent = Logger.drdaEventMessages[eventId];
				drdaEvent.generalText = msg;
				Logger.WriteToEventQueue(drdaEvent);
				Logger._loggedEventIds[eventId] = true;
				return;
			}
			Logger.Warning(0, string.Format("Event {0} is not defined", eventId), Array.Empty<object>());
		}

		// Token: 0x06004231 RID: 16945 RVA: 0x000DD2B0 File Offset: 0x000DB4B0
		public static void LogEvent(string eventMsg, EventLogEntryType type)
		{
			if (Logger.evtl == null || Logger.evtl.EventLog == null)
			{
				return;
			}
			if (Logger.evtl.Filter.ShouldTrace(null, "DrdaService1", Logger.LogEventToTraceEventType(type), 0, eventMsg, null, null, null))
			{
				DrdaEvent drdaEvent = new DrdaEvent();
				drdaEvent.generalText = eventMsg;
				drdaEvent.eventId = 0;
				drdaEvent.eventType = type;
				drdaEvent.category = 0;
				Logger._eventQueue.Enqueue(drdaEvent);
			}
		}

		// Token: 0x06004232 RID: 16946 RVA: 0x000DD320 File Offset: 0x000DB520
		public static void RefreshTraceConfiguration()
		{
			List<IDrdaTraceListener> list = Logger.customLoggers;
			lock (list)
			{
				Logger.customLoggers.Clear();
				foreach (object obj in Logger.trc.Listeners)
				{
					TraceListener traceListener = (TraceListener)obj;
					if (traceListener is IDrdaTraceListener)
					{
						IDrdaTraceListener drdaTraceListener = (IDrdaTraceListener)traceListener;
						Logger.customLoggers.Add(drdaTraceListener);
						if (drdaTraceListener.TraceLevel > Logger.maxTracingLevel)
						{
							Logger.maxTracingLevel = drdaTraceListener.TraceLevel;
						}
						Logger._autoFlush = drdaTraceListener.AutoFlush || Logger._autoFlush;
					}
					if (traceListener is EventLogTraceListener)
					{
						Logger.evtl = (EventLogTraceListener)traceListener;
					}
					if (traceListener is IDrdaAzureTraceListener)
					{
						Logger.dadt = (IDrdaAzureTraceListener)traceListener;
						if (Logger.dadt.TraceLevel > Logger.maxTracingLevel)
						{
							Logger.maxTracingLevel = Logger.dadt.TraceLevel;
						}
					}
				}
				foreach (IDrdaTraceListener drdaTraceListener2 in Logger.customLoggers)
				{
					Logger.trc.Listeners.Remove((TraceListener)drdaTraceListener2);
				}
				if (Logger.evtl != null)
				{
					try
					{
						Logger.trc.Listeners.Remove(Logger.evtl);
					}
					catch (Exception)
					{
						Logger.evtl = null;
					}
				}
				if (Logger.dadt != null)
				{
					try
					{
						Logger.trc.Listeners.Remove((TraceListener)Logger.dadt);
					}
					catch (Exception)
					{
						Logger.dadt = null;
					}
				}
			}
		}

		// Token: 0x06004233 RID: 16947 RVA: 0x000DD53C File Offset: 0x000DB73C
		private static void PrepareLogData(object tracePoint, int id, TraceEventType type, int level, string message, params object[] args)
		{
			LogData logData = new LogData();
			logData.id = id;
			logData.tracePoint = tracePoint;
			logData.evenType = type;
			logData.level = level;
			logData.message = string.Format(message, args);
			if (tracePoint == null)
			{
				logData.message = string.Format("{0}:[{1}] {2}", level, DateTime.Now.ToString("MMM dd yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture), logData.message);
				int num = Interlocked.Increment(ref Logger._currentEntryWriteIndex);
				if (num >= 9999)
				{
					TraceEntryArrayPool instance = TraceEntryArrayPool.Instance;
					lock (instance)
					{
						if (Logger._currentEntryWriteIndex >= 9999)
						{
							Logger._traceWriteBuffer.LogDataArray[9999] = logData;
							if (Logger._traceReadBuffer != Logger._traceWriteBuffer)
							{
								Logger._traceQueue.Enqueue(Logger._traceWriteBuffer);
							}
							Logger._traceWriteBuffer = TraceEntryArrayPool.Get();
							Interlocked.Exchange(ref Logger._currentEntryWriteIndex, -1);
							goto IL_0111;
						}
						Logger._traceWriteBuffer.LogDataArray[Interlocked.Increment(ref Logger._currentEntryWriteIndex)] = logData;
						goto IL_0111;
					}
				}
				Logger._traceWriteBuffer.LogDataArray[num] = logData;
				IL_0111:
				Logger._traceEntryBuffered = Interlocked.Increment(ref Logger._traceEntryBuffered);
				if (!Logger._writeTraceSemaphore.Signaled && (Logger._autoFlush || Logger._traceEntryBuffered > 100))
				{
					Logger._writeTraceSemaphore.Signal();
					return;
				}
			}
			else if (Logger._tracePointLogger != null)
			{
				Logger._tracePointLogger(logData);
			}
		}

		// Token: 0x06004234 RID: 16948 RVA: 0x000DD6B4 File Offset: 0x000DB8B4
		public static void Log(TraceEventType type, int level, int id, string message, params object[] args)
		{
			Logger.PrepareLogData(null, id, type, level, message, args);
		}

		// Token: 0x06004235 RID: 16949 RVA: 0x000DD6C2 File Offset: 0x000DB8C2
		public static void Log(TraceEventType type, int level, object tracePoint, int id, string message, params object[] args)
		{
			Logger.PrepareLogData(tracePoint, id, type, level, message, args);
		}

		// Token: 0x06004236 RID: 16950 RVA: 0x000DD6D1 File Offset: 0x000DB8D1
		public static void Info(int id, string message, params object[] args)
		{
			Logger.PrepareLogData(null, id, TraceEventType.Information, 3, message, args);
		}

		// Token: 0x06004237 RID: 16951 RVA: 0x000DD6DE File Offset: 0x000DB8DE
		public static void Info(object tracePoint, int id, string message, params object[] args)
		{
			Logger.PrepareLogData(tracePoint, id, TraceEventType.Information, 3, message, args);
		}

		// Token: 0x06004238 RID: 16952 RVA: 0x000DD6EB File Offset: 0x000DB8EB
		public static void Verbose(int id, string message, params object[] args)
		{
			Logger.PrepareLogData(null, id, TraceEventType.Information, 4, message, args);
		}

		// Token: 0x06004239 RID: 16953 RVA: 0x000DD6F8 File Offset: 0x000DB8F8
		public static void Verbose(object tracePoint, int id, string message, params object[] args)
		{
			Logger.PrepareLogData(tracePoint, id, TraceEventType.Information, 4, message, args);
		}

		// Token: 0x0600423A RID: 16954 RVA: 0x000DD708 File Offset: 0x000DB908
		public static void Info2(int id, params object[] args)
		{
			if (args == null)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < args.Length; i++)
			{
				stringBuilder.Append(args[i]);
			}
			Logger.PrepareLogData(null, id, TraceEventType.Information, 3, stringBuilder.ToString(), Array.Empty<object>());
		}

		// Token: 0x0600423B RID: 16955 RVA: 0x000DD74B File Offset: 0x000DB94B
		public static void FunctionEntry(object tracePoint, int id, string function)
		{
			Logger.PrepareLogData(tracePoint, id, TraceEventType.Information, 5, "Enter " + function, Array.Empty<object>());
		}

		// Token: 0x0600423C RID: 16956 RVA: 0x000DD766 File Offset: 0x000DB966
		public static void FunctionEntry(int id, string function)
		{
			Logger.PrepareLogData(null, id, TraceEventType.Information, 5, "Enter " + function, Array.Empty<object>());
		}

		// Token: 0x0600423D RID: 16957 RVA: 0x000DD781 File Offset: 0x000DB981
		public static void FunctionExit(int id, string function)
		{
			Logger.PrepareLogData(null, id, TraceEventType.Information, 5, "Exit " + function, Array.Empty<object>());
		}

		// Token: 0x0600423E RID: 16958 RVA: 0x000DD79C File Offset: 0x000DB99C
		public static void FunctionExit(object tracePoint, int id, string function)
		{
			Logger.PrepareLogData(tracePoint, id, TraceEventType.Information, 5, "Exit " + function, Array.Empty<object>());
		}

		// Token: 0x0600423F RID: 16959 RVA: 0x000DD7B7 File Offset: 0x000DB9B7
		public static void Warning(int id, string message, params object[] args)
		{
			Logger.PrepareLogData(null, id, TraceEventType.Warning, 2, message, args);
		}

		// Token: 0x06004240 RID: 16960 RVA: 0x000DD7C4 File Offset: 0x000DB9C4
		public static void Warning(object tracePoint, int id, string message, params object[] args)
		{
			Logger.PrepareLogData(tracePoint, id, TraceEventType.Warning, 2, message, args);
		}

		// Token: 0x06004241 RID: 16961 RVA: 0x000DD7D1 File Offset: 0x000DB9D1
		public static void Warning(int id, int tracingLevel, string message, params object[] args)
		{
			Logger.PrepareLogData(null, id, TraceEventType.Warning, tracingLevel, message, args);
		}

		// Token: 0x06004242 RID: 16962 RVA: 0x000DD7DE File Offset: 0x000DB9DE
		public static void Warning(object tracePoint, int id, int tracingLevel, string message, params object[] args)
		{
			Logger.PrepareLogData(tracePoint, id, TraceEventType.Warning, tracingLevel, message, args);
		}

		// Token: 0x06004243 RID: 16963 RVA: 0x000DD7EC File Offset: 0x000DB9EC
		public static void Error(int id, string message, params object[] args)
		{
			Logger.PrepareLogData(null, id, TraceEventType.Error, 1, message, args);
		}

		// Token: 0x06004244 RID: 16964 RVA: 0x000DD7F9 File Offset: 0x000DB9F9
		public static void Error(object tracePoint, int id, string message, params object[] args)
		{
			Logger.PrepareLogData(tracePoint, id, TraceEventType.Error, 1, message, args);
		}

		// Token: 0x06004245 RID: 16965 RVA: 0x000DD806 File Offset: 0x000DBA06
		public static void DataStream(int id, StringBuilder header, byte[] buffer, int offset, int length)
		{
			Logger.DataStream(null, id, header, buffer, offset, length);
		}

		// Token: 0x06004246 RID: 16966 RVA: 0x000DD814 File Offset: 0x000DBA14
		public static void DataStream(object tracePoint, int id, StringBuilder header, byte[] buffer, int offset, int length)
		{
			if (Logger.maxTracingLevel >= 4)
			{
				StringBuilder stringBuilder = new StringBuilder("\r\n");
				stringBuilder.AppendFormat("{0, -7}{1, -36}{2,-18}{3,-18}\r\n", new object[] { "", header, "(ASCII)", "(EBCDIC)" });
				stringBuilder.AppendFormat("{0, -7}{1, -18}{2, -18}{3,-18}{4,-18}\r\n", new object[] { "", "0 1 2 3 4 5 6 7", "8 9 A B C D E F", "0123456789ABCDEF", "0123456789ABCDEF" });
				byte[] array = new byte[16];
				int num = offset;
				while (num < offset + length && num <= buffer.Length)
				{
					if (num + 16 < offset + length)
					{
						int num2 = 16;
						if (num + 16 > buffer.Length)
						{
							num2 = buffer.Length - num;
							array = new byte[16];
							Array.Resize<byte>(ref buffer, buffer.Length - num2 + 16);
						}
						Buffer.BlockCopy(buffer, num, array, 0, num2);
						byte[] array2 = Encoding.Convert(Logger.ebcdic, Encoding.ASCII, array);
						StringBuilder stringBuilder2 = new StringBuilder(Encoding.ASCII.GetString(array));
						StringBuilder stringBuilder3 = new StringBuilder(Encoding.ASCII.GetString(array2));
						for (int i = 0; i < 16; i++)
						{
							if (!char.IsLetterOrDigit(stringBuilder2[i]))
							{
								stringBuilder2[i] = '.';
							}
							if (!char.IsLetterOrDigit(stringBuilder3[i]))
							{
								stringBuilder3[i] = '.';
							}
						}
						string text = string.Format("{0, -7}{1, -18}{2, -18}{3,-18}{4,-18}\r\n", new object[]
						{
							(num - offset).ToString("X4"),
							string.Concat(new string[]
							{
								buffer[num].ToString("X2"),
								buffer[num + 1].ToString("X2"),
								buffer[num + 2].ToString("X2"),
								buffer[num + 3].ToString("X2"),
								buffer[num + 4].ToString("X2"),
								buffer[num + 5].ToString("X2"),
								buffer[num + 6].ToString("X2"),
								buffer[num + 7].ToString("X2")
							}),
							string.Concat(new string[]
							{
								buffer[num + 8].ToString("X2"),
								buffer[num + 9].ToString("X2"),
								buffer[num + 10].ToString("X2"),
								buffer[num + 11].ToString("X2"),
								buffer[num + 12].ToString("X2"),
								buffer[num + 13].ToString("X2"),
								buffer[num + 14].ToString("X2"),
								buffer[num + 15].ToString("X2")
							}),
							stringBuilder2,
							stringBuilder3
						});
						if (num2 < 16)
						{
							text = text.Substring(0, 7 + num2 * 2) + "\r\n";
							Array.Resize<byte>(ref buffer, buffer.Length + num2 - 16);
						}
						stringBuilder.Append(text);
					}
					else
					{
						int num3 = offset + length - num;
						array = new byte[num3];
						Buffer.BlockCopy(buffer, num, array, 0, num3);
						byte[] array3 = Encoding.Convert(Logger.ebcdic, Encoding.ASCII, array);
						StringBuilder stringBuilder4 = new StringBuilder(Encoding.ASCII.GetString(array));
						StringBuilder stringBuilder5 = new StringBuilder(Encoding.ASCII.GetString(array3));
						for (int j = 0; j < num3; j++)
						{
							if (!char.IsLetterOrDigit(stringBuilder4[j]))
							{
								stringBuilder4[j] = '.';
							}
							if (!char.IsLetterOrDigit(stringBuilder5[j]))
							{
								stringBuilder5[j] = '.';
							}
						}
						StringBuilder stringBuilder6 = new StringBuilder();
						for (int k = 0; k < num3; k++)
						{
							stringBuilder6.Append(buffer[num + k].ToString("X2"));
						}
						if (stringBuilder6.Length > 16)
						{
							stringBuilder.AppendFormat("{0, -7}{1, -18}{2, -18}{3,-18}{4,-18}", new object[]
							{
								(num - offset).ToString("X4"),
								stringBuilder6.ToString(0, 16),
								stringBuilder6.ToString(16, stringBuilder6.Length - 16),
								stringBuilder4,
								stringBuilder5
							});
						}
						else
						{
							stringBuilder.AppendFormat("{0, -7}{1, -18}{2, -18}{3,-18}{4,-18}", new object[]
							{
								(num - offset).ToString("X4"),
								stringBuilder6,
								"",
								stringBuilder4,
								stringBuilder5
							});
						}
					}
					num += 16;
				}
				Logger.PrepareLogData(tracePoint, id, TraceEventType.Information, 6, stringBuilder.ToString(), Array.Empty<object>());
			}
		}

		// Token: 0x06004247 RID: 16967 RVA: 0x000DDCF5 File Offset: 0x000DBEF5
		public static void LogException(int id, string message, Exception ex)
		{
			Logger.LogException(null, id, message, ex);
		}

		// Token: 0x06004248 RID: 16968 RVA: 0x000DDD00 File Offset: 0x000DBF00
		public static void LogException(object tracePoint, int id, string message, Exception ex)
		{
			if (Logger.maxTracingLevel != 0)
			{
				SqlException ex2 = ex as SqlException;
				if (ex2 != null)
				{
					Logger.Error(id, message + string.Format(" SqlException: Server={0}, Error={1}, Class={2}, Number={3}, State={4}", new object[] { ex2.Server, ex2.ErrorCode, ex2.Class, ex2.Number, ex2.State }), Array.Empty<object>());
				}
				Logger.Log(TraceEventType.Error, 1, tracePoint, id, message + " {0}", new object[] { ex.Message });
				Logger.Log(TraceEventType.Error, 1, tracePoint, id, message + "{0}", new object[] { ex.StackTrace });
			}
		}

		// Token: 0x06004249 RID: 16969 RVA: 0x000DDDC9 File Offset: 0x000DBFC9
		public static void LogSqlException(int id, string msg, SqlException sqlException)
		{
			Logger.LogSqlException(null, id, msg, sqlException);
		}

		// Token: 0x0600424A RID: 16970 RVA: 0x000DDDD4 File Offset: 0x000DBFD4
		public static void LogSqlException(object tracePoint, int id, string msg, SqlException sqlException)
		{
			if (Logger.maxTracingLevel != 0)
			{
				Logger.Error(tracePoint, id, msg + string.Format(" SqlException: Server={0}, Error={1}, Class={2}, Number={3}, State={4}", new object[] { sqlException.Server, sqlException.ErrorCode, sqlException.Class, sqlException.Number, sqlException.State }), Array.Empty<object>());
			}
		}

		// Token: 0x04002E59 RID: 11865
		private static TraceSource trc = new TraceSource("DrdaAs");

		// Token: 0x04002E5A RID: 11866
		private static List<IDrdaTraceListener> customLoggers = new List<IDrdaTraceListener>();

		// Token: 0x04002E5B RID: 11867
		private static Encoding ebcdic = Encoding.GetEncoding(37);

		// Token: 0x04002E5C RID: 11868
		public static int maxTracingLevel = 0;

		// Token: 0x04002E5D RID: 11869
		private static Dictionary<int, DrdaEvent> drdaEventMessages = new Dictionary<int, DrdaEvent>();

		// Token: 0x04002E5E RID: 11870
		private static EventLogTraceListener evtl = null;

		// Token: 0x04002E5F RID: 11871
		private static IDrdaAzureTraceListener dadt = null;

		// Token: 0x04002E60 RID: 11872
		private static Hashtable _loggedEventIds = new Hashtable();

		// Token: 0x04002E61 RID: 11873
		private static Thread _eventLogWriteThread;

		// Token: 0x04002E62 RID: 11874
		private static Thread _tracegWriteThread;

		// Token: 0x04002E63 RID: 11875
		private static ManualResetEventSlim _writeEventSemaphore = new ManualResetEventSlim(false);

		// Token: 0x04002E64 RID: 11876
		private static EventSemaphore _writeTraceSemaphore = new EventSemaphore(false);

		// Token: 0x04002E65 RID: 11877
		private static bool _autoFlush = false;

		// Token: 0x04002E66 RID: 11878
		private static Action<LogData> _tracePointLogger = null;

		// Token: 0x04002E67 RID: 11879
		private static ConcurrentQueue<DrdaEvent> _eventQueue = new ConcurrentQueue<DrdaEvent>();

		// Token: 0x04002E68 RID: 11880
		private static ConcurrentQueue<LogDataArrayItem> _traceQueue = new ConcurrentQueue<LogDataArrayItem>();

		// Token: 0x04002E69 RID: 11881
		private static LogDataArrayItem _traceWriteBuffer = TraceEntryArrayPool.Get();

		// Token: 0x04002E6A RID: 11882
		private static LogDataArrayItem _traceReadBuffer = Logger._traceWriteBuffer;

		// Token: 0x04002E6B RID: 11883
		private static int _currentEntryWriteIndex = -1;

		// Token: 0x04002E6C RID: 11884
		private static int _currentEntryReadIndex = 0;

		// Token: 0x04002E6D RID: 11885
		private static int _traceEntryBuffered = 0;
	}
}
