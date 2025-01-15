using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.Utils;
using Microsoft.Diagnostics.Tracing;

namespace Microsoft.Cloud.Platform.Eventing.Base
{
	// Token: 0x020003BF RID: 959
	public class EtwEvent
	{
		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06001DA8 RID: 7592 RVA: 0x00070587 File Offset: 0x0006E787
		private MemoryStream TSMemoryStream
		{
			get
			{
				if (EtwEvent.s_memoryStream == null)
				{
					EtwEvent.s_memoryStream = new MemoryStream((int)EtwEvent.MaximumLimitationOfJsonEventSize);
				}
				return EtwEvent.s_memoryStream;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001DA9 RID: 7593 RVA: 0x000705A5 File Offset: 0x0006E7A5
		// (set) Token: 0x06001DAA RID: 7594 RVA: 0x000705AD File Offset: 0x0006E7AD
		public string Source { get; private set; }

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001DAB RID: 7595 RVA: 0x000705B6 File Offset: 0x0006E7B6
		// (set) Token: 0x06001DAC RID: 7596 RVA: 0x000705BE File Offset: 0x0006E7BE
		public int ThreadId { get; private set; }

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001DAD RID: 7597 RVA: 0x000705C7 File Offset: 0x0006E7C7
		// (set) Token: 0x06001DAE RID: 7598 RVA: 0x000705CF File Offset: 0x0006E7CF
		public int ProcessId { get; private set; }

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001DAF RID: 7599 RVA: 0x000705D8 File Offset: 0x0006E7D8
		// (set) Token: 0x06001DB0 RID: 7600 RVA: 0x000705E0 File Offset: 0x0006E7E0
		public long EventId { get; private set; }

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001DB1 RID: 7601 RVA: 0x000705E9 File Offset: 0x0006E7E9
		// (set) Token: 0x06001DB2 RID: 7602 RVA: 0x000705F1 File Offset: 0x0006E7F1
		public string EventName { get; private set; }

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001DB3 RID: 7603 RVA: 0x000705FA File Offset: 0x0006E7FA
		// (set) Token: 0x06001DB4 RID: 7604 RVA: 0x00070602 File Offset: 0x0006E802
		public string Message { get; private set; }

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001DB5 RID: 7605 RVA: 0x0007060B File Offset: 0x0006E80B
		// (set) Token: 0x06001DB6 RID: 7606 RVA: 0x00070613 File Offset: 0x0006E813
		public Activity Activity { get; private set; }

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001DB7 RID: 7607 RVA: 0x0007061C File Offset: 0x0006E81C
		// (set) Token: 0x06001DB8 RID: 7608 RVA: 0x00070624 File Offset: 0x0006E824
		public DateTime Timestamp { get; private set; }

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001DB9 RID: 7609 RVA: 0x0007062D File Offset: 0x0006E82D
		// (set) Token: 0x06001DBA RID: 7610 RVA: 0x00070635 File Offset: 0x0006E835
		public EventLevel Level { get; private set; }

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001DBB RID: 7611 RVA: 0x0007063E File Offset: 0x0006E83E
		// (set) Token: 0x06001DBC RID: 7612 RVA: 0x00070646 File Offset: 0x0006E846
		public ElementId ElementId { get; private set; }

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001DBD RID: 7613 RVA: 0x0007064F File Offset: 0x0006E84F
		// (set) Token: 0x06001DBE RID: 7614 RVA: 0x00070657 File Offset: 0x0006E857
		public IEnumerable<EventParameter> EventParameters { get; private set; }

		// Token: 0x06001DBF RID: 7615 RVA: 0x00070660 File Offset: 0x0006E860
		[CLSCompliant(false)]
		public static EtwEvent CreateFromTraceEvent(TraceEvent traceEvent, EtwEventsReaderOptions options)
		{
			string[] payloadNames = traceEvent.PayloadNames;
			int num = EtwEvent.CalculateCommonPayloadOffset(payloadNames);
			if (!string.Equals(payloadNames[num + 5], "generatedEventsKitId", StringComparison.Ordinal))
			{
				return new UnparsableEtwEvent(traceEvent);
			}
			long num2 = (long)traceEvent.PayloadValue(num + 5);
			if (num2 == 3040439000390277252L)
			{
				return EtwEvent.CreateOnBehalfOfEventFromTraceEvent(traceEvent, options, num2);
			}
			if (num2 == 7527287753132401043L || num2 == 3030375513894374788L || num2 == 6952302906064617795L || num2 == 3028741171140197300L)
			{
				return EtwEvent.CreateXeEventFromTraceEvent(traceEvent, options, num2, num);
			}
			string text = null;
			if ((options & EtwEventsReaderOptions.CreateEtwEventMessageField) != EtwEventsReaderOptions.None)
			{
				text = traceEvent.FormattedMessage;
			}
			Guid guid = (Guid)traceEvent.PayloadValue(num + 1);
			Guid guid2 = (Guid)traceEvent.PayloadValue(num + 3);
			string text2 = traceEvent.PayloadString(num + 2);
			string text3 = traceEvent.PayloadString(num + 4);
			Activity activity = new Activity(guid, new ActivityType(text2), guid2, text3);
			ElementId elementId = new ElementId(traceEvent.PayloadString(num));
			DateTime dateTime = traceEvent.TimeStamp.ToUniversalTime();
			EventLevel eventLevel = traceEvent.Level.ToTraceVerbosity();
			IEnumerable<EventParameter> enumerable = EtwEvent.ParseEventParameters(traceEvent);
			int threadID = traceEvent.ThreadID;
			int processID = traceEvent.ProcessID;
			string providerName = traceEvent.ProviderName;
			return new EtwEvent(num2, traceEvent.EventName, providerName, text, activity, dateTime, eventLevel, elementId, processID, threadID, enumerable);
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x000707AB File Offset: 0x0006E9AB
		private static int CalculateCommonPayloadOffset(string[] payloadNames)
		{
			if (!payloadNames[0].Equals("generatedElementId"))
			{
				return payloadNames.Length - EventSourceConstants.CommonEventSourcePayloadNamesCount;
			}
			return 0;
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x000707C7 File Offset: 0x0006E9C7
		private static int CalculatePayloadOffset(string[] payloadNames)
		{
			if (!payloadNames[0].Equals("generatedElementId"))
			{
				return 0;
			}
			return EventSourceConstants.CommonEventSourcePayloadNamesCount;
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x000707E0 File Offset: 0x0006E9E0
		private static EtwEvent CreateXeEventFromTraceEvent(TraceEvent traceEvent, EtwEventsReaderOptions options, long eventId, int commonPayloadOffset)
		{
			IEnumerable<EventParameter> enumerable = EtwEvent.ParseEventParameters(traceEvent);
			string text = string.Empty;
			string text2 = null;
			if (eventId != 3028741171140197300L)
			{
				if (eventId != 3030375513894374788L)
				{
					if (eventId == 7527287753132401043L)
					{
						text = "parentActivityId={0}, howEnded={1}, startTime={2:o}, duration={3}, name={4}, properties={5}, err={6}, rootcauseErrorEventId={7}";
					}
				}
				else
				{
					text = "failureTime={0:o}, err={1}";
				}
			}
			else
			{
				text = "name={0}, properties={1}";
			}
			if ((options & EtwEventsReaderOptions.CreateEtwEventMessageField) != EtwEventsReaderOptions.None)
			{
				object[] array = (from eventParameter in enumerable.Where((EventParameter eventParameter, int index) => index > 0)
					select eventParameter.Value ?? string.Empty).ToArray<object>();
				text2 = text.FormatWithInvariantCulture(array);
			}
			string[] array2 = ((string)enumerable.ElementAt(0).Value).Split(new char[] { ',' });
			int num = 0;
			int.TryParse(array2[0], out num);
			DateTime dateTime = DateTime.MinValue;
			DateTime.TryParse(array2[1], out dateTime);
			dateTime = dateTime.ToUniversalTime();
			string text3 = array2[2];
			Guid guid = (Guid)traceEvent.PayloadValue(commonPayloadOffset + 1);
			Guid guid2 = (Guid)traceEvent.PayloadValue(commonPayloadOffset + 3);
			string text4 = traceEvent.PayloadString(commonPayloadOffset + 2);
			string text5 = traceEvent.PayloadString(commonPayloadOffset + 4);
			Activity activity = new Activity(guid, new ActivityType(text4), guid2, text5);
			ElementId elementId = new ElementId(traceEvent.PayloadString(commonPayloadOffset));
			EventLevel eventLevel = traceEvent.Level.ToTraceVerbosity();
			int processID = traceEvent.ProcessID;
			return new EtwEvent(eventId, traceEvent.EventName, text3, text2, activity, dateTime, eventLevel, elementId, processID, num, enumerable);
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x00070968 File Offset: 0x0006EB68
		private static EtwEvent CreateOnBehalfOfEventFromTraceEvent(TraceEvent traceEvent, EtwEventsReaderOptions options, long eventId)
		{
			string[] payloadNames = traceEvent.PayloadNames;
			string text;
			if (!EtwEvent.ValidatePayloadNamesForJsonEvent(payloadNames, out text))
			{
				return new IllFormattedJsonEvent(eventId, "OnBehalfOfJsonEvent", text);
			}
			int num = EtwEvent.CalculatePayloadOffset(payloadNames);
			object obj = traceEvent.PayloadValue(num);
			if (obj.GetType() != typeof(string))
			{
				return new UnparsableEtwEvent(traceEvent);
			}
			string text2 = (string)obj;
			if (EtwEvent.IsJsonEventExceedSizeLimit(text2))
			{
				return new ExceedSizeLimitJsonEvent(eventId, traceEvent, text2);
			}
			return EtwEvent.CreateFromJsonString(traceEvent, text2, options);
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x000709E4 File Offset: 0x0006EBE4
		[CLSCompliant(false)]
		public static EtwEvent CreateFromJsonString(string jsonEvent, EtwEventsReaderOptions options)
		{
			return EtwEvent.CreateFromJsonString(null, jsonEvent, options);
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x000709F0 File Offset: 0x0006EBF0
		private static EtwEvent CreateFromJsonString(TraceEvent traceEvent, string jsonEvent, EtwEventsReaderOptions options)
		{
			EtwEvent etwEvent;
			try
			{
				etwEvent = Json.DeserializeFromString<EtwEvent.EtwEventDataContract>(jsonEvent).ToEtwEvent(options);
			}
			catch (Exception ex)
			{
				if (!(ex is SerializationException) && !(ex is ArgumentOutOfRangeException) && !(ex is IndexOutOfRangeException) && !(ex is ArgumentNullException))
				{
					throw;
				}
				if (traceEvent != null)
				{
					etwEvent = UnparsableJsonEvent.CreateFromTraceEvent(traceEvent, jsonEvent, ex);
				}
				else
				{
					etwEvent = new UnparsableJsonEvent(jsonEvent, ex);
				}
			}
			return etwEvent;
		}

		// Token: 0x06001DC6 RID: 7622 RVA: 0x00070A58 File Offset: 0x0006EC58
		[CLSCompliant(false)]
		public static IEnumerable<EtwEvent> CreateMultipleFromJsonStream(Stream stream, ref byte[] buffer, EtwEventsReaderOptions options)
		{
			int num = (int)stream.Length;
			if (buffer == null || buffer.Length < num)
			{
				buffer = new byte[num + 4096];
			}
			stream.Read(buffer, 0, (int)stream.Length);
			MultiStreamBuffer multiStreamBuffer = new MultiStreamBuffer(buffer, 0, num, Environment.NewLine);
			DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(EtwEvent.EtwEventDataContract));
			Queue<EtwEvent> queue = new Queue<EtwEvent>();
			Stream stream2;
			while ((stream2 = multiStreamBuffer.TryGetNextStream()) != null)
			{
				try
				{
					EtwEvent etwEvent = ((EtwEvent.EtwEventDataContract)dataContractJsonSerializer.ReadObject(stream2)).ToEtwEvent(options);
					queue.Enqueue(etwEvent);
				}
				catch (SerializationException ex)
				{
					queue.Enqueue(new UnparsableJsonEvent("TBD", ex));
				}
			}
			return queue;
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x00070B14 File Offset: 0x0006ED14
		public EtwEvent(long eventId, string eventName, string source, string message, Activity activity, DateTime timestamp, EventLevel level, ElementId elementId, int processId, int threadId, IEnumerable<EventParameter> parameters)
		{
			this.Set(eventId, eventName, source, message, activity, timestamp, level, elementId, processId, threadId, parameters);
		}

		// Token: 0x06001DC8 RID: 7624 RVA: 0x00070B44 File Offset: 0x0006ED44
		public EtwEvent Set(long eventId, string eventName, string source, string message, Activity activity, DateTime timestamp, EventLevel level, ElementId elementId, int processId, int threadId, IEnumerable<EventParameter> parameters)
		{
			this.EventId = eventId;
			this.EventName = eventName;
			this.Source = source;
			this.Message = message;
			this.Activity = activity;
			this.Timestamp = timestamp;
			this.Level = level;
			this.ElementId = elementId;
			this.ProcessId = processId;
			this.ThreadId = threadId;
			if (parameters != null)
			{
				this.EventParameters = parameters.Materialize<EventParameter>();
			}
			else
			{
				this.EventParameters = Enumerable.Empty<EventParameter>();
			}
			return this;
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x00070BC0 File Offset: 0x0006EDC0
		public static EtwEvent CreateFromEtwEvent(EtwEvent src, EtwEventsReaderOptions options)
		{
			if (options.HasFlag(EtwEventsReaderOptions.CreateEtwEventMessageField))
			{
				return new EtwEvent(src.EventId, src.EventName, src.Source, src.Message ?? src.GetMessageBasedOnTheOtherFields(), src.Activity, src.Timestamp, src.Level, src.ElementId, src.ProcessId, src.ThreadId, src.EventParameters);
			}
			return new EtwEvent(src.EventId, src.EventName, src.Message, src.Source, src.Activity, src.Timestamp, src.Level, src.ElementId, src.ProcessId, src.ThreadId, src.EventParameters);
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x00070C7C File Offset: 0x0006EE7C
		public static void SerializeMultipleToJsonFile(string file, Encoding encoding, IEnumerable<EtwEvent> events)
		{
			using (FileStream fileStream = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.None, 65536))
			{
				EtwEvent.SerializeMultipleToJsonStream(fileStream, encoding, events);
			}
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x00070CBC File Offset: 0x0006EEBC
		public static void SerializeMultipleToJsonStream([NotNull] Stream stream, [NotNull] Encoding encoding, [NotNull] IEnumerable<EtwEvent> events)
		{
			Ensure.ArgNotNull<Stream>(stream, "stream");
			Ensure.ArgNotNull<Encoding>(encoding, "encoding");
			Ensure.ArgNotNull<IEnumerable<EtwEvent>>(events, "events");
			using (EtwEvent.EtwEventSerializationContext etwEventSerializationContext = new EtwEvent.EtwEventSerializationContext(stream, encoding, Environment.NewLine))
			{
				int num = 0;
				foreach (EtwEvent etwEvent in events)
				{
					if (etwEvent.ShouldSerializeToStream())
					{
						etwEvent.SerializeToStream(etwEventSerializationContext);
					}
					num++;
				}
				TraceSourceBase<EventingTrace>.Tracer.TraceVerbose("SerializeMultipleToJsonStream wrote {0} events", new object[] { num });
			}
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x00070D78 File Offset: 0x0006EF78
		public string ToJsonString()
		{
			EtwEvent.EtwEventDataContract etwEventDataContract = EtwEvent.EtwEventDataContract.FromEtwEvent(this);
			List<Type> list = null;
			if (etwEventDataContract.EventParameters != null)
			{
				List<KeyValuePair<string, object>> list2 = new List<KeyValuePair<string, object>>();
				foreach (KeyValuePair<string, object> keyValuePair in etwEventDataContract.EventParameters)
				{
					object value = keyValuePair.Value;
					if (value != null)
					{
						Type type = value.GetType();
						if (type == typeof(Guid))
						{
							list2.Add(new KeyValuePair<string, object>(keyValuePair.Key, "/Guid(" + value.ToString() + ")/"));
							continue;
						}
						IMonitoredError monitoredError = value as IMonitoredError;
						if (monitoredError != null)
						{
							list2.Add(new KeyValuePair<string, object>(keyValuePair.Key, monitoredError.ToStringLimitedLength(true)));
							continue;
						}
						if (type.IsEnum)
						{
							if (list == null)
							{
								list = new List<Type>();
							}
							list.Add(value.GetType());
						}
					}
					list2.Add(keyValuePair);
				}
				etwEventDataContract.EventParameters = list2;
			}
			this.TSMemoryStream.Seek(0L, SeekOrigin.Begin);
			this.TSMemoryStream.SetLength(0L);
			DataContractJsonSerializer dataContractJsonSerializer = ((list != null) ? new DataContractJsonSerializer(etwEventDataContract.GetType(), list) : new DataContractJsonSerializer(etwEventDataContract.GetType()));
			using (XmlDictionaryWriter xmlDictionaryWriter = JsonReaderWriterFactory.CreateJsonWriter(this.TSMemoryStream, Encoding.UTF8, false))
			{
				dataContractJsonSerializer.WriteObject(xmlDictionaryWriter, etwEventDataContract);
				xmlDictionaryWriter.Flush();
			}
			return Encoding.UTF8.GetString(this.TSMemoryStream.ToArray());
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x000034FD File Offset: 0x000016FD
		protected virtual bool ShouldSerializeToStream()
		{
			return true;
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x00070F1C File Offset: 0x0006F11C
		private void SerializeToStream([NotNull] EtwEvent.EtwEventSerializationContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<EtwEvent.EtwEventSerializationContext>(context, "context");
			EtwEvent.EtwEventDataContract etwEventDataContract = EtwEvent.EtwEventDataContract.FromEtwEvent(this);
			List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>();
			if (etwEventDataContract.EventParameters != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in etwEventDataContract.EventParameters)
				{
					object value = keyValuePair.Value;
					if (value != null)
					{
						Type type = value.GetType();
						if (type == typeof(Guid))
						{
							list.Add(new KeyValuePair<string, object>(keyValuePair.Key, "/Guid(" + value.ToString() + ")/"));
							continue;
						}
						if (type.IsEnum)
						{
							context.KnownTypes.Add(value.GetType());
						}
					}
					list.Add(new KeyValuePair<string, object>(keyValuePair.Key, value));
				}
			}
			etwEventDataContract.EventParameters = list;
			DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(etwEventDataContract.GetType(), context.KnownTypes);
			try
			{
				context.WriteObject(dataContractJsonSerializer, etwEventDataContract);
			}
			catch (SerializationException ex)
			{
				ExtendedDiagnostics.AlertDebuggerIfAttached();
				TraceSourceBase<EventingTrace>.Tracer.TraceWarning("Not serializing an EtwEvent to stream due to the following exception: {0}", new object[] { ex.Message });
				TraceSourceBase<EventingTrace>.Tracer.TraceWarning(ex.ToString());
			}
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x00071078 File Offset: 0x0006F278
		[CLSCompliant(false)]
		protected static IEnumerable<EventParameter> ParseEventParameters(TraceEvent traceEvent)
		{
			string[] payloadNames = traceEvent.PayloadNames;
			int num = payloadNames.Length - EventSourceConstants.CommonEventSourcePayloadNamesCount;
			EventParameter[] array = new EventParameter[num];
			int num2 = EtwEvent.CalculatePayloadOffset(payloadNames);
			for (int i = 0; i < num; i++)
			{
				int num3 = num2 + i;
				array[i] = EtwEvent.GetTraceEventParameter(traceEvent, payloadNames[num3], num3);
			}
			return array;
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x000710CC File Offset: 0x0006F2CC
		private static EventParameter GetTraceEventParameter(TraceEvent traceEvent, string payloadName, int index)
		{
			object obj = traceEvent.PayloadValue(index);
			Type type = obj.GetType();
			return new EventParameter(payloadName, type.Equals(typeof(DateTime)) ? ((DateTime)obj).ToUniversalTime() : obj, type);
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x00071117 File Offset: 0x0006F317
		public static bool IsJsonEventExceedSizeLimit(string jsonEvent)
		{
			return (long)(2 * jsonEvent.Length) > EtwEvent.MaximumLimitationOfJsonEventSize;
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x0007112C File Offset: 0x0006F32C
		public static void FireTruncatedJsonEvent(IOnBehalfOfEventsKit onBehalfOfEventsKit, EtwEvent etwEvent)
		{
			EtwEvent etwEvent2 = new ExceedSizeLimitJsonEvent(etwEvent);
			onBehalfOfEventsKit.FireOnBehalfOfJsonEvent(etwEvent2.ToJsonString());
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x0007114C File Offset: 0x0006F34C
		private static bool ValidatePayloadNamesForJsonEvent(string[] payloadNames, out string error)
		{
			if (payloadNames == null)
			{
				error = "Ill-formatted on-behalf-of JSON event: payloadNames is null, but it should include at least the fixed part and a jsonEvent payload slot";
			}
			else if (payloadNames.Length != 1 + EventSourceConstants.CommonEventSourcePayloadNamesCount)
			{
				error = "Ill-formatted on-behalf-of JSON event: payloadNames has {0} entries, but we expect {1}".FormatWithInvariantCulture(new object[]
				{
					payloadNames.Length,
					1 + EventSourceConstants.CommonEventSourcePayloadNamesCount
				});
			}
			else
			{
				if ("jsonEvent".Equals(payloadNames[EtwEvent.CalculatePayloadOffset(payloadNames)], StringComparison.Ordinal))
				{
					error = null;
					return true;
				}
				error = "Ill-formatted on-behalf-of JSON event: payloadNames[{0}] should be 'jsonEvent', but it is: '{1}'".FormatWithInvariantCulture(new object[]
				{
					EventSourceConstants.CommonEventSourcePayloadNamesCount,
					payloadNames[EtwEvent.CalculatePayloadOffset(payloadNames)]
				});
			}
			return false;
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x000711EC File Offset: 0x0006F3EC
		private string GetMessageBasedOnTheOtherFields()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<Event: ");
			foreach (EventParameter eventParameter in this.EventParameters)
			{
				stringBuilder.AppendFormat("{0}={{{1}}}, ", eventParameter.Name, eventParameter.Value);
			}
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "ElementId={{{0}}}, ActivityId={{{1}}}, ActivityType={{{2}}}, RootActivityId={{{3}}}", new object[]
			{
				this.ElementId,
				this.Activity.ActivityId,
				this.Activity.ActivityType,
				this.Activity.RootActivityId
			});
			stringBuilder.Append(">");
			return stringBuilder.ToString();
		}

		// Token: 0x04000A14 RID: 2580
		[ThreadStatic]
		private static MemoryStream s_memoryStream = null;

		// Token: 0x04000A20 RID: 2592
		public const long OnBehalfOfEventsKitId = 3606054181238014444L;

		// Token: 0x04000A21 RID: 2593
		public const long OnBehalfOfJsonEventId = 3040439000390277252L;

		// Token: 0x04000A22 RID: 2594
		public static long MaximumLimitationOfJsonEventSize = ExtendedMath.KBtoBytes(32L);

		// Token: 0x020007D3 RID: 2003
		private class EtwEventSerializationContext : IDisposable
		{
			// Token: 0x060031E9 RID: 12777 RVA: 0x000A8C1C File Offset: 0x000A6E1C
			internal EtwEventSerializationContext(Stream stream, Encoding encoding, string delimiter)
			{
				this.m_stream = stream;
				this.m_encoding = encoding;
				this.m_delimiter = encoding.GetBytes(delimiter);
				this.m_knownTypes = new List<Type> { typeof(IMonitoredError) };
				this.m_numEvents = 0;
				this.CreateJsonWriter();
			}

			// Token: 0x060031EA RID: 12778 RVA: 0x000A8C74 File Offset: 0x000A6E74
			internal void WriteObject(DataContractJsonSerializer jsonSerializer, EtwEvent.EtwEventDataContract obj)
			{
				int numEvents = this.m_numEvents;
				this.m_numEvents = numEvents + 1;
				if (numEvents == 0)
				{
					ExtendedStream.WriteBomToStream(this.m_stream, this.m_encoding);
				}
				else
				{
					this.m_jsonWriter.Flush();
					((IXmlJsonWriterInitializer)this.m_jsonWriter).SetOutput(this.m_stream, this.m_encoding, false);
					this.m_stream.Write(this.m_delimiter, 0, this.m_delimiter.Length);
				}
				jsonSerializer.WriteObject(this.m_jsonWriter, obj);
			}

			// Token: 0x060031EB RID: 12779 RVA: 0x000A8CF6 File Offset: 0x000A6EF6
			public void Dispose()
			{
				this.DestroyJsonWriter();
			}

			// Token: 0x060031EC RID: 12780 RVA: 0x000A8CFE File Offset: 0x000A6EFE
			private void CreateJsonWriter()
			{
				this.m_jsonWriter = JsonReaderWriterFactory.CreateJsonWriter(this.m_stream, this.m_encoding, false);
			}

			// Token: 0x060031ED RID: 12781 RVA: 0x000A8D18 File Offset: 0x000A6F18
			private void DestroyJsonWriter()
			{
				if (this.m_jsonWriter != null)
				{
					this.m_jsonWriter.Flush();
					this.m_jsonWriter.Close();
					this.m_jsonWriter = null;
				}
			}

			// Token: 0x17000775 RID: 1909
			// (get) Token: 0x060031EE RID: 12782 RVA: 0x000A8D3F File Offset: 0x000A6F3F
			internal List<Type> KnownTypes
			{
				get
				{
					return this.m_knownTypes;
				}
			}

			// Token: 0x04001719 RID: 5913
			private Stream m_stream;

			// Token: 0x0400171A RID: 5914
			private Encoding m_encoding;

			// Token: 0x0400171B RID: 5915
			private byte[] m_delimiter;

			// Token: 0x0400171C RID: 5916
			private List<Type> m_knownTypes;

			// Token: 0x0400171D RID: 5917
			private XmlDictionaryWriter m_jsonWriter;

			// Token: 0x0400171E RID: 5918
			private int m_numEvents;
		}

		// Token: 0x020007D4 RID: 2004
		[DataContract]
		private class EtwEventDataContract : JsonSerializedEventDataContract
		{
			// Token: 0x060031EF RID: 12783 RVA: 0x000A8D48 File Offset: 0x000A6F48
			[OnDeserialized]
			private void OnDeserialized(StreamingContext context)
			{
				if (base.EventParameters == null || !base.EventParameters.Any<KeyValuePair<string, object>>())
				{
					return;
				}
				List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>();
				foreach (KeyValuePair<string, object> keyValuePair in base.EventParameters)
				{
					KeyValuePair<string, object> keyValuePair2 = keyValuePair;
					if (keyValuePair.Value != null && keyValuePair.Value.GetType() == typeof(string))
					{
						string text = (string)keyValuePair.Value;
						Guid guid;
						if (text.StartsWith("/Date(") && text.EndsWith(")/", StringComparison.Ordinal))
						{
							string text2 = "\"\\" + text.Substring(0, text.Length - 1) + "\\/\"";
							DateTime dateTime = (DateTime)EtwEvent.EtwEventDataContract.s_dateTimeSerializer.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(text2)));
							keyValuePair2 = new KeyValuePair<string, object>(keyValuePair.Key, dateTime);
						}
						else if (text.StartsWith("/Guid(") && text.EndsWith(")/", StringComparison.Ordinal) && Guid.TryParse(text.Substring("/Guid(".Length, text.Length - "/Guid(".Length - ")/".Length), out guid))
						{
							keyValuePair2 = new KeyValuePair<string, object>(keyValuePair.Key, guid);
						}
					}
					list.Add(keyValuePair2);
				}
				base.EventParameters = list;
			}

			// Token: 0x060031F0 RID: 12784 RVA: 0x000A8EE8 File Offset: 0x000A70E8
			public EtwEvent ToEtwEvent(EtwEventsReaderOptions options)
			{
				IEnumerable<EventParameter> enumerable = null;
				if (base.EventParameters != null && base.EventParameters.Any<KeyValuePair<string, object>>())
				{
					enumerable = base.EventParameters.Select((KeyValuePair<string, object> eventParameter) => new EventParameter(string.IsNullOrEmpty(eventParameter.Key) ? string.Empty : eventParameter.Key, eventParameter.Value, (eventParameter.Value != null) ? eventParameter.Value.GetType() : typeof(object))).Materialize<EventParameter>();
				}
				EtwEvent etwEvent = new EtwEvent(base.EventId, base.EventName, base.Source, base.Message, new Activity(base.ActivityId, new ActivityType(base.ActivityType), base.RootActivityId, base.ClientActivityId), base.Timestamp, base.Level, new ElementId(base.ElementId), base.ProcessId, base.ThreadId, enumerable);
				if (options.HasFlag(EtwEventsReaderOptions.CreateEtwEventMessageField))
				{
					etwEvent = EtwEvent.CreateFromEtwEvent(etwEvent, EtwEventsReaderOptions.CreateEtwEventMessageField);
				}
				return etwEvent;
			}

			// Token: 0x060031F1 RID: 12785 RVA: 0x000A8FBC File Offset: 0x000A71BC
			public static EtwEvent.EtwEventDataContract FromEtwEvent(EtwEvent ev)
			{
				EtwEvent.EtwEventDataContract etwEventDataContract = new EtwEvent.EtwEventDataContract();
				etwEventDataContract.EventId = ev.EventId;
				etwEventDataContract.EventName = ev.EventName;
				etwEventDataContract.Source = ev.Source;
				etwEventDataContract.Message = ev.Message;
				etwEventDataContract.ActivityId = ev.Activity.ActivityId;
				etwEventDataContract.ActivityType = ev.Activity.ActivityType.ShortName;
				etwEventDataContract.RootActivityId = ev.Activity.RootActivityId;
				etwEventDataContract.ClientActivityId = ev.Activity.ClientActivityId;
				etwEventDataContract.Timestamp = ev.Timestamp;
				etwEventDataContract.Level = ev.Level;
				etwEventDataContract.ElementId = ev.ElementId.Name;
				etwEventDataContract.ThreadId = ev.ThreadId;
				etwEventDataContract.ProcessId = ev.ProcessId;
				if (ev.EventParameters != null && ev.EventParameters.Any<EventParameter>())
				{
					etwEventDataContract.EventParameters = ev.EventParameters.Select((EventParameter eventParameter) => new KeyValuePair<string, object>(eventParameter.Name, eventParameter.Value)).Materialize<KeyValuePair<string, object>>();
				}
				return etwEventDataContract;
			}

			// Token: 0x0400171F RID: 5919
			private static DataContractJsonSerializer s_dateTimeSerializer = new DataContractJsonSerializer(typeof(DateTime));
		}
	}
}
