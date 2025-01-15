using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Eventing;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace Microsoft.Cis.Eventing.Listeners
{
	// Token: 0x02000491 RID: 1169
	public class RDEventMonitoringAgentListener : TraceListener
	{
		// Token: 0x06002882 RID: 10370 RVA: 0x0007A1B0 File Offset: 0x000783B0
		public RDEventMonitoringAgentListener(Guid providerGuid)
		{
			this.BuildTypeMappings();
			this.m_providerGuid = providerGuid;
			this.m_eventProvider = new EventProvider(this.m_providerGuid);
			RDEventMonitoringAgentListener.m_gcWorker.SetListener(this);
		}

		// Token: 0x06002883 RID: 10371 RVA: 0x0007A20D File Offset: 0x0007840D
		public RDEventMonitoringAgentListener(string providerGuid)
			: this(new Guid(providerGuid))
		{
			this.BuildTypeMappings();
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06002884 RID: 10372 RVA: 0x00002B16 File Offset: 0x00000D16
		public override bool IsThreadSafe
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002885 RID: 10373 RVA: 0x0007A221 File Offset: 0x00078421
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.m_eventProvider != null)
			{
				this.m_eventProvider.Dispose();
				this.m_eventProvider = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06002886 RID: 10374 RVA: 0x0007A248 File Offset: 0x00078448
		public override void Write(string message)
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.AppendToEvent(stringBuilder, "EventName", "DirectWrite");
			this.AppendToEvent(stringBuilder, "Message", message);
			this.LogToEtw(TraceEventType.Verbose, 64259, stringBuilder.ToString());
		}

		// Token: 0x06002887 RID: 10375 RVA: 0x0007A28C File Offset: 0x0007848C
		public override void WriteLine(string message)
		{
			this.Write(message);
		}

		// Token: 0x06002888 RID: 10376 RVA: 0x0007A298 File Offset: 0x00078498
		public override void TraceTransfer(TraceEventCache eventCache, string source, int id, string message, Guid relatedActivityId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.AppendToEvent(stringBuilder, "EventName", "MessageEvent");
			this.AppendToEvent(stringBuilder, "Message", message);
			this.AppendToEvent(stringBuilder, "TraceSource", source);
			this.LogTransferToEtw(id, stringBuilder.ToString(), relatedActivityId);
		}

		// Token: 0x06002889 RID: 10377 RVA: 0x0007A2E8 File Offset: 0x000784E8
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id)
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.AppendToEvent(stringBuilder, "TraceSource", source);
			this.LogToEtw(eventType, id, stringBuilder.ToString());
		}

		// Token: 0x0600288A RID: 10378 RVA: 0x0007A318 File Offset: 0x00078518
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.AppendToEvent(stringBuilder, "EventName", "MessageEvent");
			this.AppendToEvent(stringBuilder, "Message", message);
			this.AppendToEvent(stringBuilder, "TraceSource", source);
			this.LogToEtw(eventType, id, stringBuilder.ToString());
		}

		// Token: 0x0600288B RID: 10379 RVA: 0x0007A368 File Offset: 0x00078568
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
		{
			if (args != null && args.Length == 0)
			{
				this.TraceEvent(eventCache, source, eventType, id, format);
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			this.AppendToEvent(stringBuilder, "EventName", "FormattedMessageEvent");
			this.AppendToEvent(stringBuilder, "FormattedMessage", format);
			if (args != null)
			{
				for (int i = 0; i < args.Length; i++)
				{
					string text = "Argument" + i;
					this.AppendToEvent(stringBuilder, text, args[i]);
				}
			}
			this.AppendToEvent(stringBuilder, "TraceSource", source);
			if (id == 0)
			{
				id = 64258;
			}
			this.LogToEtw(eventType, id, stringBuilder.ToString());
		}

		// Token: 0x0600288C RID: 10380 RVA: 0x0007A40C File Offset: 0x0007860C
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.AppendToEvent(stringBuilder, "EventName", "ObjectArrayEvent");
			int num = 0;
			while (data != null && num < data.Length)
			{
				string text = "Object" + num;
				this.AppendToEvent(stringBuilder, text, data[num]);
				num++;
			}
			this.AppendToEvent(stringBuilder, "TraceSource", source);
			this.LogToEtw(eventType, id, stringBuilder.ToString());
		}

		// Token: 0x0600288D RID: 10381 RVA: 0x0007A47C File Offset: 0x0007867C
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
		{
			string text = "";
			if (data == null)
			{
				return;
			}
			if (data is MessageEvent)
			{
				MessageEvent messageEvent = (MessageEvent)data;
				this.TraceEvent(eventCache, source, eventType, id, messageEvent.Message);
				return;
			}
			if (!(data is FormattedMessageEvent))
			{
				Dictionary<string, object> dictionary = data as Dictionary<string, object>;
				bool flag = true;
				StringBuilder stringBuilder = new StringBuilder();
				if (dictionary != null)
				{
					this.AppendToEvent(stringBuilder, "EventName", "DictionaryEvent");
					using (Dictionary<string, object>.Enumerator enumerator = dictionary.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<string, object> keyValuePair = enumerator.Current;
							this.AppendToEvent(stringBuilder, keyValuePair.Key, keyValuePair.Value);
						}
						goto IL_039C;
					}
				}
				Type type = data.GetType();
				RDEventMonitoringAgentListener.CachedTypeIfo cachedTypeIfo = null;
				if (this.IsTraceable(type, out cachedTypeIfo))
				{
					flag = cachedTypeIfo.writeTracingIndexEvent;
					this.AppendToEvent(stringBuilder, "EventName", type.Name);
					using (List<PropertyInfo>.Enumerator enumerator2 = cachedTypeIfo.properties.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							PropertyInfo propertyInfo = enumerator2.Current;
							this.AppendToEvent(stringBuilder, propertyInfo.Name, propertyInfo.GetValue(data, null));
							if (id == RDEventMonitoringAgentListener.OperationTraceId && propertyInfo.Name == "TraceEventName")
							{
								text = propertyInfo.GetValue(data, null) as string;
							}
						}
						goto IL_039C;
					}
				}
				this.AppendToEvent(stringBuilder, "EventName", "ObjectEvent");
				this.AppendToEvent(stringBuilder, "Object", data);
				IL_039C:
				this.AppendToEvent(stringBuilder, "TraceSource", source);
				if (id != RDEventMonitoringAgentListener.OperationTraceId)
				{
					this.LogToEtw(eventType, id, stringBuilder.ToString());
				}
				bool flag2 = true;
				bool flag3 = false;
				if (flag && (id == RDEventMonitoringAgentListener.OperationTraceId || !this.CheckRedundantEvent(Trace.CorrelationManager.ActivityId.ToString(), data.GetType().Name, ref text)))
				{
					string text2 = Trace.CorrelationManager.ActivityId.ToString();
					string operationTraceMessage = this.GetOperationTraceMessage(data.GetType().Name, text, id.ToString());
					if (text == "Start")
					{
						Dictionary<string, DateTime> dictionary2 = new Dictionary<string, DateTime>();
						lock (RDEventMonitoringAgentListener.OperationEventsDict)
						{
							if (!RDEventMonitoringAgentListener.OperationEventsDict.ContainsKey(text2))
							{
								RDEventMonitoringAgentListener.OperationEventsDict.Add(text2, dictionary2);
							}
						}
						lock (RDEventMonitoringAgentListener.OperationEventsReference)
						{
							if (!RDEventMonitoringAgentListener.OperationEventsReference.ContainsKey(text2))
							{
								RDEventMonitoringAgentListener.OperationEventsReference[text2] = 1;
							}
							else
							{
								Dictionary<string, int> operationEventsReference2;
								string text3;
								(operationEventsReference2 = RDEventMonitoringAgentListener.OperationEventsReference)[text3 = text2] = operationEventsReference2[text3] + 1;
							}
							goto IL_0624;
						}
					}
					if (text == "Stop")
					{
						lock (RDEventMonitoringAgentListener.OperationEventsReference)
						{
							if (RDEventMonitoringAgentListener.OperationEventsReference.ContainsKey(text2) && RDEventMonitoringAgentListener.OperationEventsReference[text2] > 0)
							{
								Dictionary<string, int> operationEventsReference4;
								string text4;
								(operationEventsReference4 = RDEventMonitoringAgentListener.OperationEventsReference)[text4 = text2] = operationEventsReference4[text4] - 1;
								if (RDEventMonitoringAgentListener.OperationEventsReference[text2] == 0)
								{
									RDEventMonitoringAgentListener.OperationEventsReference.Remove(text2);
									flag3 = true;
								}
							}
							else
							{
								flag3 = true;
							}
						}
						if (!flag3)
						{
							goto IL_0624;
						}
						lock (RDEventMonitoringAgentListener.OperationEventsDict)
						{
							try
							{
								RDEventMonitoringAgentListener.OperationEventsDict[text2].Clear();
								RDEventMonitoringAgentListener.OperationEventsDict.Remove(text2);
							}
							catch
							{
							}
							goto IL_0624;
						}
					}
					if (text == "AddRef")
					{
						lock (RDEventMonitoringAgentListener.OperationEventsDict)
						{
							if (RDEventMonitoringAgentListener.OperationEventsReference.ContainsKey(text2))
							{
								Dictionary<string, int> operationEventsReference5;
								string text5;
								(operationEventsReference5 = RDEventMonitoringAgentListener.OperationEventsReference)[text5 = text2] = operationEventsReference5[text5] + 1;
							}
						}
						flag2 = false;
					}
					IL_0624:
					if (operationTraceMessage.Length > 0 && flag2)
					{
						this.LogToEtw(eventType, RDEventMonitoringAgentListener.OperationTraceId, operationTraceMessage);
					}
					if (flag3)
					{
						string empty = string.Empty;
						string empty2 = string.Empty;
						RDEventMonitoringAgentListener.GetLastOpIdFromStack(ref empty, ref empty2);
						Trace.CorrelationManager.StopLogicalOperation();
						Trace.CorrelationManager.ActivityId = new Guid(empty2);
					}
				}
				return;
			}
			FormattedMessageEvent formattedMessageEvent = (FormattedMessageEvent)data;
			switch (formattedMessageEvent.ArgsLength)
			{
			case 0:
				this.TraceEvent(eventCache, source, eventType, id, formattedMessageEvent.Format);
				return;
			case 1:
				this.TraceEvent(eventCache, source, eventType, id, formattedMessageEvent.Format, new object[] { formattedMessageEvent.Argument0 });
				return;
			case 2:
				this.TraceEvent(eventCache, source, eventType, id, formattedMessageEvent.Format, new object[] { formattedMessageEvent.Argument0, formattedMessageEvent.Argument1 });
				return;
			case 3:
				this.TraceEvent(eventCache, source, eventType, id, formattedMessageEvent.Format, new object[] { formattedMessageEvent.Argument0, formattedMessageEvent.Argument1, formattedMessageEvent.Argument2 });
				return;
			case 4:
				this.TraceEvent(eventCache, source, eventType, id, formattedMessageEvent.Format, new object[] { formattedMessageEvent.Argument0, formattedMessageEvent.Argument1, formattedMessageEvent.Argument2, formattedMessageEvent.Argument3 });
				return;
			case 5:
				this.TraceEvent(eventCache, source, eventType, id, formattedMessageEvent.Format, new object[] { formattedMessageEvent.Argument0, formattedMessageEvent.Argument1, formattedMessageEvent.Argument2, formattedMessageEvent.Argument3, formattedMessageEvent.Argument4 });
				return;
			case 6:
				this.TraceEvent(eventCache, source, eventType, id, formattedMessageEvent.Format, new object[] { formattedMessageEvent.Argument0, formattedMessageEvent.Argument1, formattedMessageEvent.Argument2, formattedMessageEvent.Argument3, formattedMessageEvent.Argument4, formattedMessageEvent.Argument5 });
				return;
			default:
				this.TraceEvent(eventCache, source, eventType, id, formattedMessageEvent.Format, new object[] { formattedMessageEvent.Argument0, formattedMessageEvent.Argument1, formattedMessageEvent.Argument2, formattedMessageEvent.Argument3, formattedMessageEvent.Argument4, formattedMessageEvent.Argument5, formattedMessageEvent.Argument6 });
				return;
			}
		}

		// Token: 0x0600288E RID: 10382 RVA: 0x0007AB64 File Offset: 0x00078D64
		public void LogDanglingE2ETraceEvent(string operationId)
		{
			int num = 64014;
			StringBuilder stringBuilder = new StringBuilder();
			this.AppendToEvent(stringBuilder, "OperationId", operationId);
			this.AppendToEvent(stringBuilder, "RootId", "");
			this.AppendToEvent(stringBuilder, "ParentId", "");
			this.AppendToEvent(stringBuilder, "Scope", "");
			this.AppendToEvent(stringBuilder, "TraceEventName", "Dangling");
			this.AppendToEvent(stringBuilder, "TraceEventId", "");
			this.LogToEtw(TraceEventType.Information, num, stringBuilder.ToString());
		}

		// Token: 0x0600288F RID: 10383 RVA: 0x0007ABF0 File Offset: 0x00078DF0
		private bool CheckRedundantEvent(string opId, string eventName, ref string logTransitionStr)
		{
			lock (RDEventMonitoringAgentListener.OperationEventsDict)
			{
				if (!RDEventMonitoringAgentListener.OperationEventsDict.ContainsKey(opId))
				{
					return true;
				}
				if (RDEventMonitoringAgentListener.OperationEventsDict[opId].ContainsKey(eventName))
				{
					return true;
				}
				RDEventMonitoringAgentListener.OperationEventsDict[opId][eventName] = DateTime.UtcNow;
			}
			return false;
		}

		// Token: 0x06002890 RID: 10384 RVA: 0x0007AC6C File Offset: 0x00078E6C
		private string GetOperationTraceMessage(string eventName, string transitionStr, string eventId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			string text = Trace.CorrelationManager.ActivityId.ToString();
			string text2 = string.Empty;
			string text3 = string.Empty;
			int num = 0;
			foreach (object obj in Trace.CorrelationManager.LogicalOperationStack)
			{
				string[] array = obj.ToString().Split(new char[] { ';' });
				if (array != null && array.Length >= 2)
				{
					stringBuilder2.Append(string.Format("{0}<", array[0]));
					stringBuilder.Append(string.Format("{0}<", array[1]));
				}
				if (num == Trace.CorrelationManager.LogicalOperationStack.Count - 1 && array.Length >= 2)
				{
					text3 = array[1];
				}
				if (num == 0 && array.Length >= 3)
				{
					text2 = array[2];
				}
				num++;
			}
			if (transitionStr.Length == 0 && (text2.Length == 0 || text3.Length == 0))
			{
				return string.Empty;
			}
			StringBuilder stringBuilder3 = new StringBuilder();
			this.AppendToEvent(stringBuilder3, "OperationId", text);
			this.AppendToEvent(stringBuilder3, "TraceEventId", eventId);
			this.AppendToEvent(stringBuilder3, "TraceEventName", (transitionStr.Length > 0) ? transitionStr : eventName);
			this.AppendToEvent(stringBuilder3, "Scope", stringBuilder2.ToString().TrimEnd(new char[] { '<' }));
			this.AppendToEvent(stringBuilder3, "ParentOpId", text2);
			this.AppendToEvent(stringBuilder3, "RootOpId", text3);
			this.AppendToEvent(stringBuilder3, "OpIdPath", stringBuilder.ToString().TrimEnd(new char[] { '<' }));
			return stringBuilder3.ToString();
		}

		// Token: 0x06002891 RID: 10385 RVA: 0x0007AE5C File Offset: 0x0007905C
		private static void GetLastOpIdFromStack(ref string currentActityId, ref string previousActivityId)
		{
			Stack logicalOperationStack = Trace.CorrelationManager.LogicalOperationStack;
			if (logicalOperationStack.Count == 0)
			{
				Trace.CorrelationManager.ActivityId = Guid.Empty;
				return;
			}
			string text = logicalOperationStack.Peek() as string;
			string[] array = text.Split(new char[] { ';' });
			if (array.Length >= 3)
			{
				currentActityId = array[1];
				previousActivityId = array[2];
			}
		}

		// Token: 0x06002892 RID: 10386 RVA: 0x0007AEBC File Offset: 0x000790BC
		private void AppendToEvent(StringBuilder eventXml, string name, object value)
		{
			string text = name ?? "null";
			string text2 = null;
			string text3;
			if (value == null)
			{
				text3 = "null";
				text2 = "mt:wstr";
			}
			else
			{
				if (value is DateTime)
				{
					if ((DateTime)value <= RDEventMonitoringAgentListener.MinDateTime)
					{
						text3 = RDEventMonitoringAgentListener.MinDateTimeString;
					}
					else
					{
						text3 = ((DateTime)value).ToUniversalTime().ToString("o");
					}
				}
				else if (value is string)
				{
					text3 = (string)value;
				}
				else if (value is int)
				{
					text3 = ((int)value).ToString(CultureInfo.InvariantCulture);
				}
				else if (value is long)
				{
					text3 = ((long)value).ToString(CultureInfo.InvariantCulture);
				}
				else if (value is double)
				{
					text3 = ((double)value).ToString(CultureInfo.InvariantCulture);
				}
				else if (value is uint)
				{
					text3 = ((uint)value).ToString(CultureInfo.InvariantCulture);
				}
				else if (value is ulong)
				{
					text3 = ((ulong)value).ToString(CultureInfo.InvariantCulture);
				}
				else if (value is short)
				{
					text3 = ((short)value).ToString(CultureInfo.InvariantCulture);
				}
				else if (value is ushort)
				{
					text3 = ((ushort)value).ToString(CultureInfo.InvariantCulture);
				}
				else if (value is sbyte)
				{
					text3 = ((sbyte)value).ToString(CultureInfo.InvariantCulture);
				}
				else
				{
					text3 = value.ToString() ?? string.Empty;
				}
				if (!this.m_TypeNameMapping.TryGetValue(value.GetType(), out text2))
				{
					text2 = "mt:wstr";
				}
				if (text2 == "mt:wstr")
				{
					bool flag = false;
					int length = text3.Length;
					int num = 0;
					while (num < length && !flag)
					{
						char c = text3[num];
						if (c < ' ')
						{
							flag = true;
						}
						else if (c <= '>')
						{
							char c2 = c;
							if (c2 != '"' && c2 != '&')
							{
								switch (c2)
								{
								case '<':
								case '>':
									break;
								case '=':
									goto IL_0211;
								default:
									goto IL_0211;
								}
							}
							flag = true;
						}
						IL_0211:
						num++;
					}
					if (flag)
					{
						StringBuilder stringBuilder = new StringBuilder();
						for (int i = 0; i < length; i++)
						{
							char c3 = text3[i];
							if (c3 <= '>')
							{
								if (c3 == '\r' || c3 == '\n')
								{
									stringBuilder.Append("&#xA;");
									if (c3 == '\r' && i + 1 < length && text3[i + 1] == '\n')
									{
										i++;
									}
								}
								else if (c3 < ' ' && c3 != '\t')
								{
									stringBuilder.Append('?');
								}
								else
								{
									char c4 = c3;
									if (c4 != '"')
									{
										if (c4 != '&')
										{
											switch (c4)
											{
											case '<':
												stringBuilder.Append("&lt;");
												goto IL_0323;
											case '>':
												stringBuilder.Append("&gt;");
												goto IL_0323;
											}
											stringBuilder.Append(c3);
										}
										else
										{
											stringBuilder.Append("&amp;");
										}
									}
									else
									{
										stringBuilder.Append("&quot;");
									}
								}
							}
							else
							{
								stringBuilder.Append(c3);
							}
							IL_0323:;
						}
						text3 = stringBuilder.ToString();
					}
				}
			}
			eventXml.Append("<Param Name=\"");
			eventXml.Append(text);
			eventXml.Append("\" Value=\"");
			eventXml.Append(text3);
			eventXml.Append("\" T=\"");
			eventXml.Append(text2);
			eventXml.Append("\" />");
		}

		// Token: 0x06002893 RID: 10387 RVA: 0x0007B24C File Offset: 0x0007944C
		private void LogTransferToEtw(int eventID, string eventMessage, Guid relatedActivityId)
		{
			RDEventMonitoringAgentListener.EtwEventLevel etwEventLevel = RDEventMonitoringAgentListener.EtwEventLevel.Verbose;
			ushort num;
			if (eventID < 0 || eventID > 65535)
			{
				num = 64257;
			}
			else
			{
				num = Convert.ToUInt16(eventID);
			}
			global::System.Diagnostics.Eventing.EventDescriptor eventDescriptor = new global::System.Diagnostics.Eventing.EventDescriptor(61468, 0, 0, (byte)etwEventLevel, 0, 0, 0L);
			try
			{
				this.m_eventProvider.WriteTransferEvent(ref eventDescriptor, relatedActivityId, new object[]
				{
					(ulong)num,
					eventMessage
				});
			}
			catch (ArgumentException)
			{
			}
			catch (Win32Exception)
			{
			}
		}

		// Token: 0x06002894 RID: 10388 RVA: 0x0007B2D8 File Offset: 0x000794D8
		private void LogToEtw(TraceEventType eventType, int eventID, string eventMessage)
		{
			RDEventMonitoringAgentListener.EtwEventLevel etwEventLevel = this.ConvertTraceEventTypeToEventLevel(eventType);
			ulong num;
			if (eventID < 0 || eventID > 65535)
			{
				if (eventID >= 70000 && eventID <= 70004)
				{
					num = Convert.ToUInt64(eventID);
				}
				else
				{
					num = 64257UL;
				}
			}
			else
			{
				num = Convert.ToUInt64(eventID);
			}
			Guid activityId = Trace.CorrelationManager.ActivityId;
			EventProvider.SetActivityId(ref activityId);
			global::System.Diagnostics.Eventing.EventDescriptor eventDescriptor = new global::System.Diagnostics.Eventing.EventDescriptor(61468, 0, 0, (byte)etwEventLevel, 0, 0, 0L);
			try
			{
				this.m_eventProvider.WriteEvent(ref eventDescriptor, new object[] { num, eventMessage });
			}
			catch (ArgumentException)
			{
			}
			catch (Win32Exception)
			{
			}
		}

		// Token: 0x06002895 RID: 10389 RVA: 0x0007B398 File Offset: 0x00079598
		private RDEventMonitoringAgentListener.EtwEventLevel ConvertTraceEventTypeToEventLevel(TraceEventType eventType)
		{
			RDEventMonitoringAgentListener.EtwEventLevel etwEventLevel = RDEventMonitoringAgentListener.EtwEventLevel.Verbose;
			switch (eventType)
			{
			case TraceEventType.Critical:
				etwEventLevel = RDEventMonitoringAgentListener.EtwEventLevel.Critical;
				break;
			case TraceEventType.Error:
				etwEventLevel = RDEventMonitoringAgentListener.EtwEventLevel.Error;
				break;
			case (TraceEventType)3:
				break;
			case TraceEventType.Warning:
				etwEventLevel = RDEventMonitoringAgentListener.EtwEventLevel.Warning;
				break;
			default:
				if (eventType != TraceEventType.Information)
				{
					if (eventType == TraceEventType.Verbose)
					{
						etwEventLevel = RDEventMonitoringAgentListener.EtwEventLevel.Verbose;
					}
				}
				else
				{
					etwEventLevel = RDEventMonitoringAgentListener.EtwEventLevel.Info;
				}
				break;
			}
			return etwEventLevel;
		}

		// Token: 0x06002896 RID: 10390 RVA: 0x0007B3E0 File Offset: 0x000795E0
		private void BuildTypeMappings()
		{
			this.m_TypeNameMapping.Clear();
			this.m_TypeNameMapping.Add(Type.GetType("System.Boolean"), "mt:bool");
			this.m_TypeNameMapping.Add(Type.GetType("System.Byte"), "mt:int32");
			this.m_TypeNameMapping.Add(Type.GetType("System.SByte"), "mt:int32");
			this.m_TypeNameMapping.Add(Type.GetType("System.Int16"), "mt:int32");
			this.m_TypeNameMapping.Add(Type.GetType("System.UInt16"), "mt:int32");
			this.m_TypeNameMapping.Add(Type.GetType("System.Int32"), "mt:int32");
			this.m_TypeNameMapping.Add(Type.GetType("System.UInt32"), "mt:int32");
			this.m_TypeNameMapping.Add(Type.GetType("System.Int64"), "mt:int64");
			this.m_TypeNameMapping.Add(Type.GetType("System.UInt64"), "mt:int64");
			this.m_TypeNameMapping.Add(Type.GetType("System.Double"), "mt:float64");
			this.m_TypeNameMapping.Add(Type.GetType("System.Single"), "mt:float64");
			this.m_TypeNameMapping.Add(Type.GetType("System.DateTime"), "mt:utc");
			this.m_TypeNameMapping.Add(Type.GetType("System.String"), "mt:wstr");
			this.m_TypeNameMapping.Add(Type.GetType("System.Decimal"), "mt:wstr");
			this.m_TypeNameMapping.Add(Type.GetType("System.Char"), "mt:wstr");
		}

		// Token: 0x06002897 RID: 10391 RVA: 0x0007B580 File Offset: 0x00079780
		private bool IsTraceable(Type t, out RDEventMonitoringAgentListener.CachedTypeIfo typeInfo)
		{
			typeInfo = (RDEventMonitoringAgentListener.CachedTypeIfo)this.typeInfoMap[t];
			if (typeInfo == null)
			{
				typeInfo = new RDEventMonitoringAgentListener.CachedTypeIfo();
				RDEvent[] array = (RDEvent[])t.GetCustomAttributes(typeof(RDEvent), true);
				if (array.Length == 0)
				{
					typeInfo.isTraceable = false;
					typeInfo.properties = null;
					typeInfo.writeTracingIndexEvent = true;
				}
				else
				{
					foreach (RDEvent rdevent in array)
					{
						if (rdevent.ExcludeFromE2ETracing)
						{
							typeInfo.writeTracingIndexEvent = false;
						}
					}
					typeInfo.isTraceable = true;
					typeInfo.properties = new List<PropertyInfo>();
					foreach (PropertyInfo propertyInfo in t.GetProperties())
					{
						if (propertyInfo.GetCustomAttributes(typeof(RDEventProperty), true).Length > 0)
						{
							typeInfo.properties.Add(propertyInfo);
						}
					}
				}
				lock (this.typeInfoSyncRoot)
				{
					if (!this.typeInfoMap.ContainsKey(t))
					{
						this.typeInfoMap.Add(t, typeInfo);
					}
				}
			}
			return typeInfo.isTraceable;
		}

		// Token: 0x040017C3 RID: 6083
		private static ActivityGCWorker m_gcWorker = new ActivityGCWorker(RDEventMonitoringAgentListener.OperationEventsDict, RDEventMonitoringAgentListener.OperationEventsReference);

		// Token: 0x040017C4 RID: 6084
		private static readonly DateTime MinDateTime = DateTime.FromFileTimeUtc(0L).ToUniversalTime();

		// Token: 0x040017C5 RID: 6085
		private static readonly string MinDateTimeString = RDEventMonitoringAgentListener.MinDateTime.ToString("o");

		// Token: 0x040017C6 RID: 6086
		private static readonly int OperationTraceId = 64014;

		// Token: 0x040017C7 RID: 6087
		private static Dictionary<string, Dictionary<string, DateTime>> OperationEventsDict = new Dictionary<string, Dictionary<string, DateTime>>();

		// Token: 0x040017C8 RID: 6088
		private static Dictionary<string, int> OperationEventsReference = new Dictionary<string, int>();

		// Token: 0x040017C9 RID: 6089
		private Guid m_providerGuid;

		// Token: 0x040017CA RID: 6090
		private EventProvider m_eventProvider;

		// Token: 0x040017CB RID: 6091
		private Dictionary<Type, string> m_TypeNameMapping = new Dictionary<Type, string>();

		// Token: 0x040017CC RID: 6092
		private Hashtable typeInfoMap = new Hashtable();

		// Token: 0x040017CD RID: 6093
		private object typeInfoSyncRoot = new object();

		// Token: 0x02000492 RID: 1170
		public enum EtwEventLevel
		{
			// Token: 0x040017CF RID: 6095
			LogAlways,
			// Token: 0x040017D0 RID: 6096
			Critical,
			// Token: 0x040017D1 RID: 6097
			Error,
			// Token: 0x040017D2 RID: 6098
			Warning,
			// Token: 0x040017D3 RID: 6099
			Info,
			// Token: 0x040017D4 RID: 6100
			Verbose
		}

		// Token: 0x02000493 RID: 1171
		internal class CachedTypeIfo
		{
			// Token: 0x040017D5 RID: 6101
			public bool isTraceable;

			// Token: 0x040017D6 RID: 6102
			public List<PropertyInfo> properties;

			// Token: 0x040017D7 RID: 6103
			public bool writeTracingIndexEvent;
		}
	}
}
