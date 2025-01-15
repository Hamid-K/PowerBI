using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Reflection;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility.Implementation.External;
using Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x02000078 RID: 120
	internal sealed class RichPayloadEventSource : IDisposable
	{
		// Token: 0x060003CB RID: 971 RVA: 0x000107D2 File Offset: 0x0000E9D2
		public RichPayloadEventSource()
			: this("Microsoft-ApplicationInsights-Data")
		{
		}

		// Token: 0x060003CC RID: 972 RVA: 0x000107E0 File Offset: 0x0000E9E0
		internal RichPayloadEventSource(string providerName)
		{
			if (AppDomain.CurrentDomain.IsHomogenous && AppDomain.CurrentDomain.IsFullyTrusted)
			{
				Type typeFromHandle = typeof(EventSource);
				Type type = typeFromHandle.Assembly.GetType("System.Diagnostics.Tracing.EventSourceSettings");
				if (type != null)
				{
					object obj = Enum.ToObject(type, 8);
					this.EventSourceInternal = (EventSource)Activator.CreateInstance(typeFromHandle, new object[] { providerName, obj });
					this.telemetryHandlers = this.CreateTelemetryHandlers(this.EventSourceInternal);
					this.operationStartStopHandler = this.CreateOperationStartStopHandler(this.EventSourceInternal);
					this.unknownTelemetryHandler = this.CreateHandlerForUnknownTelemetry(this.EventSourceInternal);
				}
			}
		}

		// Token: 0x060003CD RID: 973 RVA: 0x000108AC File Offset: 0x0000EAAC
		public void Process(ITelemetry item)
		{
			if (this.EventSourceInternal == null)
			{
				return;
			}
			Action<ITelemetry> action = null;
			Type type = item.GetType();
			if (this.telemetryHandlers.TryGetValue(type, out action))
			{
				item.FlattenIExtensionIfExists();
				action(item);
				return;
			}
			if (this.unknownTelemetryHandler != null)
			{
				item.Sanitize();
				EventData eventData = item.FlattenTelemetryIntoEventData();
				eventData.name = "ConvertedTelemetry";
				this.unknownTelemetryHandler(eventData, item.Context.InstrumentationKey, item.Context.SanitizedTags, item.Context.Flags);
			}
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00010936 File Offset: 0x0000EB36
		public void ProcessOperationStart(OperationTelemetry operation)
		{
			if (this.EventSourceInternal == null)
			{
				return;
			}
			if (this.EventSourceInternal.IsEnabled(EventLevel.Informational, (EventKeywords)1024L))
			{
				this.operationStartStopHandler(operation, EventOpcode.Start);
			}
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00010962 File Offset: 0x0000EB62
		public void ProcessOperationStop(OperationTelemetry operation)
		{
			if (this.EventSourceInternal == null)
			{
				return;
			}
			if (this.EventSourceInternal.IsEnabled(EventLevel.Informational, (EventKeywords)1024L))
			{
				this.operationStartStopHandler(operation, EventOpcode.Stop);
			}
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0001098E File Offset: 0x0000EB8E
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0001099D File Offset: 0x0000EB9D
		private void Dispose(bool disposing)
		{
			if (disposing && this.EventSourceInternal != null)
			{
				this.EventSourceInternal.Dispose();
			}
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x000109B5 File Offset: 0x0000EBB5
		private static void CopyGlobalPropertiesIfRequired(ITelemetry telemetry, IDictionary<string, string> itemProperties)
		{
			if (telemetry.Context.GlobalPropertiesValue != null)
			{
				Utils.CopyDictionary<string>(telemetry.Context.GlobalProperties, itemProperties);
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x000109D8 File Offset: 0x0000EBD8
		private Dictionary<Type, Action<ITelemetry>> CreateTelemetryHandlers(EventSource eventSource)
		{
			Dictionary<Type, Action<ITelemetry>> dictionary = new Dictionary<Type, Action<ITelemetry>>();
			Type type = eventSource.GetType();
			MethodInfo methodInfo = (from m in type.GetMethods(BindingFlags.Instance | BindingFlags.Public)
				where m.Name == "Write" && m.IsGenericMethod
				select new
				{
					Method = m,
					Parameters = m.GetParameters()
				} into m
				where m.Parameters.Length == 3 && m.Parameters[0].ParameterType.FullName == "System.String" && m.Parameters[1].ParameterType.FullName == "System.Diagnostics.Tracing.EventSourceOptions" && m.Parameters[2].ParameterType.FullName == null && !m.Parameters[2].ParameterType.IsByRef
				select m.Method).SingleOrDefault<MethodInfo>();
			if (methodInfo != null)
			{
				Type type2 = type.Assembly.GetType("System.Diagnostics.Tracing.EventSourceOptions");
				PropertyInfo property = type2.GetProperty("Keywords", BindingFlags.Instance | BindingFlags.Public);
				dictionary.Add(typeof(RequestTelemetry), this.CreateHandlerForRequestTelemetry(eventSource, methodInfo, type2, property));
				dictionary.Add(typeof(TraceTelemetry), this.CreateHandlerForTraceTelemetry(eventSource, methodInfo, type2, property));
				dictionary.Add(typeof(EventTelemetry), this.CreateHandlerForEventTelemetry(eventSource, methodInfo, type2, property));
				dictionary.Add(typeof(DependencyTelemetry), this.CreateHandlerForDependencyTelemetry(eventSource, methodInfo, type2, property));
				dictionary.Add(typeof(MetricTelemetry), this.CreateHandlerForMetricTelemetry(eventSource, methodInfo, type2, property));
				dictionary.Add(typeof(ExceptionTelemetry), this.CreateHandlerForExceptionTelemetry(eventSource, methodInfo, type2, property));
				dictionary.Add(typeof(PerformanceCounterTelemetry), this.CreateHandlerForPerformanceCounterTelemetry(eventSource, methodInfo, type2, property));
				dictionary.Add(typeof(PageViewTelemetry), this.CreateHandlerForPageViewTelemetry(eventSource, methodInfo, type2, property));
				dictionary.Add(typeof(PageViewPerformanceTelemetry), this.CreateHandlerForPageViewPerformanceTelemetry(eventSource, methodInfo, type2, property));
				dictionary.Add(typeof(SessionStateTelemetry), this.CreateHandlerForSessionStateTelemetry(eventSource, methodInfo, type2, property));
			}
			else
			{
				CoreEventSource.Log.LogVerbose("Unable to get method: EventSource.Write<T>(String,\u2002EventSourceOptions,\u2002T)", "Incorrect");
			}
			return dictionary;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00010BE4 File Offset: 0x0000EDE4
		private Action<OperationTelemetry, EventOpcode> CreateOperationStartStopHandler(EventSource eventSource)
		{
			Type type = eventSource.GetType();
			MethodInfo methodInfo = (from m in type.GetMethods(BindingFlags.Instance | BindingFlags.Public)
				where m.Name == "Write" && m.IsGenericMethod
				select new
				{
					Method = m,
					Parameters = m.GetParameters()
				} into m
				where m.Parameters.Length == 3 && m.Parameters[0].ParameterType.FullName == "System.String" && m.Parameters[1].ParameterType.FullName == "System.Diagnostics.Tracing.EventSourceOptions" && m.Parameters[2].ParameterType.FullName == null && !m.Parameters[2].ParameterType.IsByRef
				select m.Method).SingleOrDefault<MethodInfo>();
			if (methodInfo == null)
			{
				return null;
			}
			Type type2 = type.Assembly.GetType("System.Diagnostics.Tracing.EventSourceOptions");
			PropertyInfo property = type2.GetProperty("ActivityOptions", BindingFlags.Instance | BindingFlags.Public);
			PropertyInfo property2 = type2.GetProperty("Keywords", BindingFlags.Instance | BindingFlags.Public);
			PropertyInfo property3 = type2.GetProperty("Opcode", BindingFlags.Instance | BindingFlags.Public);
			PropertyInfo property4 = type2.GetProperty("Level", BindingFlags.Instance | BindingFlags.Public);
			object obj = Enum.Parse(type.Assembly.GetType("System.Diagnostics.Tracing.EventActivityOptions"), "Recursive");
			object eventSourceOptionsStart = Activator.CreateInstance(type2);
			property2.SetValue(eventSourceOptionsStart, (EventKeywords)1024L);
			property3.SetValue(eventSourceOptionsStart, EventOpcode.Start);
			property4.SetValue(eventSourceOptionsStart, EventLevel.Informational);
			object eventSourceOptionsStop = Activator.CreateInstance(type2);
			property2.SetValue(eventSourceOptionsStop, (EventKeywords)1024L);
			property3.SetValue(eventSourceOptionsStop, EventOpcode.Stop);
			property4.SetValue(eventSourceOptionsStop, EventLevel.Informational);
			object eventSourceOptionsStartRecursive = Activator.CreateInstance(type2);
			property.SetValue(eventSourceOptionsStartRecursive, obj);
			property2.SetValue(eventSourceOptionsStartRecursive, (EventKeywords)1024L);
			property3.SetValue(eventSourceOptionsStartRecursive, EventOpcode.Start);
			property4.SetValue(eventSourceOptionsStartRecursive, EventLevel.Informational);
			object eventSourceOptionsStopRecursive = Activator.CreateInstance(type2);
			property.SetValue(eventSourceOptionsStartRecursive, obj);
			property2.SetValue(eventSourceOptionsStopRecursive, (EventKeywords)1024L);
			property3.SetValue(eventSourceOptionsStopRecursive, EventOpcode.Stop);
			property4.SetValue(eventSourceOptionsStopRecursive, EventLevel.Informational);
			MethodInfo writeMethod = methodInfo.MakeGenericMethod(new Type[] { new
			{
				IKey = null,
				Id = null,
				Name = null,
				RootId = null
			}.GetType() });
			return delegate(OperationTelemetry item, EventOpcode opCode)
			{
				bool flag = item is RequestTelemetry;
				object obj2;
				if (opCode != EventOpcode.Start)
				{
					if (opCode != EventOpcode.Stop)
					{
						throw new ArgumentException("opCode");
					}
					obj2 = (flag ? eventSourceOptionsStop : eventSourceOptionsStopRecursive);
				}
				else
				{
					obj2 = (flag ? eventSourceOptionsStart : eventSourceOptionsStartRecursive);
				}
				var <>f__AnonymousType = new
				{
					IKey = item.Context.InstrumentationKey,
					Id = item.Id,
					Name = item.Name,
					RootId = item.Context.Operation.Id
				};
				object[] array = new object[]
				{
					flag ? "Request" : "Operation",
					obj2,
					<>f__AnonymousType
				};
				writeMethod.Invoke(this.EventSourceInternal, array);
			};
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00010E8C File Offset: 0x0000F08C
		private Action<EventData, string, IDictionary<string, string>, long> CreateHandlerForUnknownTelemetry(EventSource eventSource)
		{
			Type type = eventSource.GetType();
			MethodInfo methodInfo = (from m in type.GetMethods(BindingFlags.Instance | BindingFlags.Public)
				where m.Name == "Write" && m.IsGenericMethod
				select new
				{
					Method = m,
					Parameters = m.GetParameters()
				} into m
				where m.Parameters.Length == 3 && m.Parameters[0].ParameterType.FullName == "System.String" && m.Parameters[1].ParameterType.FullName == "System.Diagnostics.Tracing.EventSourceOptions" && m.Parameters[2].ParameterType.FullName == null && !m.Parameters[2].ParameterType.IsByRef
				select m.Method).SingleOrDefault<MethodInfo>();
			if (methodInfo == null)
			{
				return null;
			}
			Type type2 = type.Assembly.GetType("System.Diagnostics.Tracing.EventSourceOptions");
			PropertyInfo property = type2.GetProperty("Keywords", BindingFlags.Instance | BindingFlags.Public);
			object eventSourceOptions = Activator.CreateInstance(type2);
			EventKeywords keywords = (EventKeywords)4L;
			property.SetValue(eventSourceOptions, keywords);
			EventData eventData = new EventData();
			MethodInfo writeMethod = methodInfo.MakeGenericMethod(new Type[] { new
			{
				PartA_iKey = this.dummyPartAiKeyValue,
				PartA_Tags = this.dummyPartATagsValue,
				PartB_EventData = new { eventData.ver, eventData.name, eventData.properties, eventData.measurements },
				PartA_flags = this.dummyPartAFlagsValue
			}.GetType() });
			return delegate(EventData data, string iKey, IDictionary<string, string> tags, long flags)
			{
				if (this.EventSourceInternal.IsEnabled(EventLevel.Verbose, keywords))
				{
					var <>f__AnonymousType = new
					{
						PartA_iKey = iKey,
						PartA_Tags = tags,
						PartB_EventData = new { data.ver, data.name, data.properties, data.measurements },
						PartA_flags = flags
					};
					writeMethod.Invoke(eventSource, new object[] { "Event", eventSourceOptions, <>f__AnonymousType });
				}
			};
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00011010 File Offset: 0x0000F210
		private Action<ITelemetry> CreateHandlerForRequestTelemetry(EventSource eventSource, MethodInfo writeGenericMethod, Type eventSourceOptionsType, PropertyInfo eventSourceOptionsKeywordsProperty)
		{
			object eventSourceOptions = Activator.CreateInstance(eventSourceOptionsType);
			EventKeywords keywords = (EventKeywords)1L;
			eventSourceOptionsKeywordsProperty.SetValue(eventSourceOptions, keywords);
			RequestData requestData = new RequestData();
			MethodInfo writeMethod = writeGenericMethod.MakeGenericMethod(new Type[] { new
			{
				PartA_iKey = this.dummyPartAiKeyValue,
				PartA_Tags = this.dummyPartATagsValue,
				PartB_RequestData = new { requestData.ver, requestData.id, requestData.source, requestData.name, requestData.duration, requestData.responseCode, requestData.success, requestData.url, requestData.properties, requestData.measurements },
				PartA_flags = this.dummyPartAFlagsValue
			}.GetType() });
			return delegate(ITelemetry item)
			{
				if (this.EventSourceInternal.IsEnabled(EventLevel.Verbose, keywords))
				{
					RequestTelemetry requestTelemetry = item as RequestTelemetry;
					if (item.Context.GlobalPropertiesValue != null)
					{
						Utils.CopyDictionary<string>(item.Context.GlobalProperties, requestTelemetry.Properties);
					}
					item.Sanitize();
					RequestData data = requestTelemetry.Data;
					var <>f__AnonymousType = new
					{
						PartA_iKey = requestTelemetry.Context.InstrumentationKey,
						PartA_Tags = requestTelemetry.Context.SanitizedTags,
						PartB_RequestData = new { data.ver, data.id, data.source, data.name, data.duration, data.responseCode, data.success, data.url, data.properties, data.measurements },
						PartA_flags = requestTelemetry.Context.Flags
					};
					writeMethod.Invoke(eventSource, new object[] { "Request", eventSourceOptions, <>f__AnonymousType });
				}
			};
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x000110E4 File Offset: 0x0000F2E4
		private Action<ITelemetry> CreateHandlerForTraceTelemetry(EventSource eventSource, MethodInfo writeGenericMethod, Type eventSourceOptionsType, PropertyInfo eventSourceOptionsKeywordsProperty)
		{
			object eventSourceOptions = Activator.CreateInstance(eventSourceOptionsType);
			EventKeywords keywords = (EventKeywords)2L;
			eventSourceOptionsKeywordsProperty.SetValue(eventSourceOptions, keywords);
			MessageData messageData = new MessageData();
			MethodInfo writeMethod = writeGenericMethod.MakeGenericMethod(new Type[] { new
			{
				PartA_iKey = this.dummyPartAiKeyValue,
				PartA_Tags = this.dummyPartATagsValue,
				PartB_MessageData = new { messageData.ver, messageData.message, messageData.severityLevel, messageData.properties, messageData.measurements },
				PartA_flags = this.dummyPartAFlagsValue
			}.GetType() });
			return delegate(ITelemetry item)
			{
				if (this.EventSourceInternal.IsEnabled(EventLevel.Verbose, keywords))
				{
					TraceTelemetry traceTelemetry = item as TraceTelemetry;
					RichPayloadEventSource.CopyGlobalPropertiesIfRequired(item, traceTelemetry.Properties);
					item.Sanitize();
					MessageData data = traceTelemetry.Data;
					var <>f__AnonymousType = new
					{
						PartA_iKey = traceTelemetry.Context.InstrumentationKey,
						PartA_Tags = traceTelemetry.Context.SanitizedTags,
						PartB_MessageData = new { data.ver, data.message, data.severityLevel, data.properties, data.measurements },
						PartA_flags = traceTelemetry.Context.Flags
					};
					writeMethod.Invoke(eventSource, new object[] { "Message", eventSourceOptions, <>f__AnonymousType });
				}
			};
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00011198 File Offset: 0x0000F398
		private Action<ITelemetry> CreateHandlerForEventTelemetry(EventSource eventSource, MethodInfo writeGenericMethod, Type eventSourceOptionsType, PropertyInfo eventSourceOptionsKeywordsProperty)
		{
			object eventSourceOptions = Activator.CreateInstance(eventSourceOptionsType);
			EventKeywords keywords = (EventKeywords)4L;
			eventSourceOptionsKeywordsProperty.SetValue(eventSourceOptions, keywords);
			EventData eventData = new EventData();
			MethodInfo writeMethod = writeGenericMethod.MakeGenericMethod(new Type[] { new
			{
				PartA_iKey = this.dummyPartAiKeyValue,
				PartA_Tags = this.dummyPartATagsValue,
				PartB_EventData = new { eventData.ver, eventData.name, eventData.properties, eventData.measurements },
				PartA_flags = this.dummyPartAFlagsValue
			}.GetType() });
			return delegate(ITelemetry item)
			{
				if (this.EventSourceInternal.IsEnabled(EventLevel.Verbose, keywords))
				{
					EventTelemetry eventTelemetry = item as EventTelemetry;
					RichPayloadEventSource.CopyGlobalPropertiesIfRequired(item, eventTelemetry.Properties);
					item.Sanitize();
					EventData data = eventTelemetry.Data;
					var <>f__AnonymousType = new
					{
						PartA_iKey = eventTelemetry.Context.InstrumentationKey,
						PartA_Tags = eventTelemetry.Context.SanitizedTags,
						PartB_EventData = new { data.ver, data.name, data.properties, data.measurements },
						PartA_flags = eventTelemetry.Context.Flags
					};
					writeMethod.Invoke(eventSource, new object[] { "Event", eventSourceOptions, <>f__AnonymousType });
				}
			};
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00011248 File Offset: 0x0000F448
		private Action<ITelemetry> CreateHandlerForDependencyTelemetry(EventSource eventSource, MethodInfo writeGenericMethod, Type eventSourceOptionsType, PropertyInfo eventSourceOptionsKeywordsProperty)
		{
			object eventSourceOptions = Activator.CreateInstance(eventSourceOptionsType);
			EventKeywords keywords = (EventKeywords)16L;
			eventSourceOptionsKeywordsProperty.SetValue(eventSourceOptions, keywords);
			RemoteDependencyData remoteDependencyData = new RemoteDependencyData();
			MethodInfo writeMethod = writeGenericMethod.MakeGenericMethod(new Type[] { new
			{
				PartA_iKey = this.dummyPartAiKeyValue,
				PartA_Tags = this.dummyPartATagsValue,
				PartB_RemoteDependencyData = new
				{
					remoteDependencyData.ver, remoteDependencyData.name, remoteDependencyData.id, remoteDependencyData.resultCode, remoteDependencyData.duration, remoteDependencyData.success, remoteDependencyData.data, remoteDependencyData.target, remoteDependencyData.type, remoteDependencyData.properties,
					remoteDependencyData.measurements
				},
				PartA_flags = this.dummyPartAFlagsValue
			}.GetType() });
			return delegate(ITelemetry item)
			{
				if (this.EventSourceInternal.IsEnabled(EventLevel.Verbose, keywords))
				{
					DependencyTelemetry dependencyTelemetry = item as DependencyTelemetry;
					if (item.Context.GlobalPropertiesValue != null)
					{
						Utils.CopyDictionary<string>(item.Context.GlobalProperties, dependencyTelemetry.Properties);
					}
					item.Sanitize();
					RemoteDependencyData internalData = dependencyTelemetry.InternalData;
					var <>f__AnonymousType = new
					{
						PartA_iKey = dependencyTelemetry.Context.InstrumentationKey,
						PartA_Tags = dependencyTelemetry.Context.SanitizedTags,
						PartB_RemoteDependencyData = new
						{
							internalData.ver, internalData.name, internalData.id, internalData.resultCode, internalData.duration, internalData.success, internalData.data, internalData.target, internalData.type, internalData.properties,
							internalData.measurements
						},
						PartA_flags = dependencyTelemetry.Context.Flags
					};
					writeMethod.Invoke(eventSource, new object[] { "RemoteDependency", eventSourceOptions, <>f__AnonymousType });
				}
			};
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00011320 File Offset: 0x0000F520
		private Action<ITelemetry> CreateHandlerForMetricTelemetry(EventSource eventSource, MethodInfo writeGenericMethod, Type eventSourceOptionsType, PropertyInfo eventSourceOptionsKeywordsProperty)
		{
			object eventSourceOptions = Activator.CreateInstance(eventSourceOptionsType);
			EventKeywords keywords = (EventKeywords)32L;
			eventSourceOptionsKeywordsProperty.SetValue(eventSourceOptions, keywords);
			MetricData metricData = new MetricData();
			DataPoint dataPoint = new DataPoint();
			MethodInfo writeMethod = writeGenericMethod.MakeGenericMethod(new Type[] { new
			{
				PartA_iKey = this.dummyPartAiKeyValue,
				PartA_Tags = this.dummyPartATagsValue,
				PartB_MetricData = new
				{
					ver = metricData.ver,
					metrics = new <>f__AnonymousType12<string, string, DataPointType, double, int?, double?, double?, double?>[]
					{
						new { dataPoint.ns, dataPoint.name, dataPoint.kind, dataPoint.value, dataPoint.count, dataPoint.min, dataPoint.max, dataPoint.stdDev }
					}.AsEnumerable(),
					properties = metricData.properties
				},
				PartA_flags = this.dummyPartAFlagsValue
			}.GetType() });
			return delegate(ITelemetry item)
			{
				if (this.EventSourceInternal.IsEnabled(EventLevel.Verbose, keywords))
				{
					MetricTelemetry metricTelemetry = item as MetricTelemetry;
					RichPayloadEventSource.CopyGlobalPropertiesIfRequired(item, metricTelemetry.Properties);
					item.Sanitize();
					MetricData data = metricTelemetry.Data;
					var <>f__AnonymousType = new
					{
						PartA_iKey = metricTelemetry.Context.InstrumentationKey,
						PartA_Tags = metricTelemetry.Context.SanitizedTags,
						PartB_MetricData = new
						{
							ver = data.ver,
							metrics = data.metrics.Select((DataPoint i) => new { i.ns, i.name, i.kind, i.value, i.count, i.min, i.max, i.stdDev }),
							properties = data.properties
						},
						PartA_flags = metricTelemetry.Context.Flags
					};
					writeMethod.Invoke(eventSource, new object[] { "Metric", eventSourceOptions, <>f__AnonymousType });
				}
			};
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0001140C File Offset: 0x0000F60C
		private Action<ITelemetry> CreateHandlerForExceptionTelemetry(EventSource eventSource, MethodInfo writeGenericMethod, Type eventSourceOptionsType, PropertyInfo eventSourceOptionsKeywordsProperty)
		{
			object eventSourceOptions = Activator.CreateInstance(eventSourceOptionsType);
			EventKeywords keywords = (EventKeywords)8L;
			eventSourceOptionsKeywordsProperty.SetValue(eventSourceOptions, keywords);
			ExceptionData exceptionData = new ExceptionData();
			ExceptionDetails exceptionDetails = new ExceptionDetails();
			Microsoft.ApplicationInsights.Extensibility.Implementation.External.StackFrame stackFrame = new Microsoft.ApplicationInsights.Extensibility.Implementation.External.StackFrame();
			MethodInfo writeMethod = writeGenericMethod.MakeGenericMethod(new Type[] { new
			{
				PartA_iKey = this.dummyPartAiKeyValue,
				PartA_Tags = this.dummyPartATagsValue,
				PartB_ExceptionData = new
				{
					ver = exceptionData.ver,
					exceptions = new <>f__AnonymousType15<int, int, string, string, bool, string, IEnumerable<<>f__AnonymousType16<int, string, string, string, int>>>[]
					{
						new
						{
							id = exceptionDetails.id,
							outerId = exceptionDetails.outerId,
							typeName = exceptionDetails.typeName,
							message = exceptionDetails.message,
							hasFullStack = exceptionDetails.hasFullStack,
							stack = exceptionDetails.stack,
							parsedStack = new <>f__AnonymousType16<int, string, string, string, int>[]
							{
								new { stackFrame.level, stackFrame.method, stackFrame.assembly, stackFrame.fileName, stackFrame.line }
							}.AsEnumerable()
						}
					}.AsEnumerable(),
					severityLevel = exceptionData.severityLevel,
					problemId = exceptionData.problemId,
					properties = exceptionData.properties,
					measurements = exceptionData.measurements
				},
				PartA_flags = this.dummyPartAFlagsValue
			}.GetType() });
			return delegate(ITelemetry item)
			{
				if (this.EventSourceInternal.IsEnabled(EventLevel.Verbose, keywords))
				{
					ExceptionTelemetry exceptionTelemetry = item as ExceptionTelemetry;
					RichPayloadEventSource.CopyGlobalPropertiesIfRequired(item, exceptionTelemetry.Properties);
					item.Sanitize();
					ExceptionData data = exceptionTelemetry.Data.Data;
					var <>f__AnonymousType = new
					{
						PartA_iKey = exceptionTelemetry.Context.InstrumentationKey,
						PartA_Tags = exceptionTelemetry.Context.SanitizedTags,
						PartB_ExceptionData = new
						{
							ver = data.ver,
							exceptions = data.exceptions.Select((ExceptionDetails i) => new
							{
								id = i.id,
								outerId = i.outerId,
								typeName = i.typeName,
								message = i.message,
								hasFullStack = i.hasFullStack,
								stack = i.stack,
								parsedStack = i.parsedStack.Select((Microsoft.ApplicationInsights.Extensibility.Implementation.External.StackFrame j) => new { j.level, j.method, j.assembly, j.fileName, j.line })
							}),
							severityLevel = data.severityLevel,
							problemId = data.problemId,
							properties = data.properties,
							measurements = data.measurements
						},
						PartA_flags = exceptionTelemetry.Context.Flags
					};
					writeMethod.Invoke(eventSource, new object[] { "Exception", eventSourceOptions, <>f__AnonymousType });
				}
			};
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00011534 File Offset: 0x0000F734
		private Action<ITelemetry> CreateHandlerForPerformanceCounterTelemetry(EventSource eventSource, MethodInfo writeGenericMethod, Type eventSourceOptionsType, PropertyInfo eventSourceOptionsKeywordsProperty)
		{
			object eventSourceOptions = Activator.CreateInstance(eventSourceOptionsType);
			EventKeywords keywords = (EventKeywords)32L;
			eventSourceOptionsKeywordsProperty.SetValue(eventSourceOptions, keywords);
			MetricData metricData = new MetricData();
			DataPoint dataPoint = new DataPoint();
			MethodInfo writeMethod = writeGenericMethod.MakeGenericMethod(new Type[] { new
			{
				PartA_iKey = this.dummyPartAiKeyValue,
				PartA_Tags = this.dummyPartATagsValue,
				PartB_MetricData = new
				{
					ver = metricData.ver,
					metrics = new <>f__AnonymousType12<string, string, DataPointType, double, int?, double?, double?, double?>[]
					{
						new { dataPoint.ns, dataPoint.name, dataPoint.kind, dataPoint.value, dataPoint.count, dataPoint.min, dataPoint.max, dataPoint.stdDev }
					}.AsEnumerable(),
					properties = metricData.properties
				},
				PartA_flags = this.dummyPartAFlagsValue
			}.GetType() });
			return delegate(ITelemetry item)
			{
				if (this.EventSourceInternal.IsEnabled(EventLevel.Verbose, keywords))
				{
					MetricTelemetry data = (item as PerformanceCounterTelemetry).Data;
					RichPayloadEventSource.CopyGlobalPropertiesIfRequired(item, data.Properties);
					item.Sanitize();
					MetricData data2 = data.Data;
					var <>f__AnonymousType = new
					{
						PartA_iKey = data.Context.InstrumentationKey,
						PartA_Tags = data.Context.SanitizedTags,
						PartB_MetricData = new
						{
							ver = data2.ver,
							metrics = data2.metrics.Select((DataPoint i) => new { i.ns, i.name, i.kind, i.value, i.count, i.min, i.max, i.stdDev }),
							properties = data2.properties
						},
						PartA_flags = data.Context.Flags
					};
					writeMethod.Invoke(eventSource, new object[] { "Metric", eventSourceOptions, <>f__AnonymousType });
				}
			};
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00011620 File Offset: 0x0000F820
		private Action<ITelemetry> CreateHandlerForPageViewTelemetry(EventSource eventSource, MethodInfo writeGenericMethod, Type eventSourceOptionsType, PropertyInfo eventSourceOptionsKeywordsProperty)
		{
			object eventSourceOptions = Activator.CreateInstance(eventSourceOptionsType);
			EventKeywords keywords = (EventKeywords)64L;
			eventSourceOptionsKeywordsProperty.SetValue(eventSourceOptions, keywords);
			PageViewData pageViewData = new PageViewData();
			MethodInfo writeMethod = writeGenericMethod.MakeGenericMethod(new Type[] { new
			{
				PartA_iKey = this.dummyPartAiKeyValue,
				PartA_Tags = this.dummyPartATagsValue,
				PartB_PageViewData = new { pageViewData.url, pageViewData.duration, pageViewData.id, pageViewData.ver, pageViewData.name, pageViewData.properties, pageViewData.measurements },
				PartA_flags = this.dummyPartAFlagsValue
			}.GetType() });
			return delegate(ITelemetry item)
			{
				if (this.EventSourceInternal.IsEnabled(EventLevel.Verbose, keywords))
				{
					PageViewTelemetry pageViewTelemetry = item as PageViewTelemetry;
					RichPayloadEventSource.CopyGlobalPropertiesIfRequired(item, pageViewTelemetry.Properties);
					item.Sanitize();
					PageViewData data = pageViewTelemetry.Data;
					var <>f__AnonymousType = new
					{
						PartA_iKey = pageViewTelemetry.Context.InstrumentationKey,
						PartA_Tags = pageViewTelemetry.Context.SanitizedTags,
						PartB_PageViewData = new { data.url, data.duration, data.id, data.ver, data.name, data.properties, data.measurements },
						PartA_flags = pageViewTelemetry.Context.Flags
					};
					writeMethod.Invoke(eventSource, new object[] { "PageView", eventSourceOptions, <>f__AnonymousType });
				}
			};
		}

		// Token: 0x060003DE RID: 990 RVA: 0x000116E0 File Offset: 0x0000F8E0
		private Action<ITelemetry> CreateHandlerForPageViewPerformanceTelemetry(EventSource eventSource, MethodInfo writeGenericMethod, Type eventSourceOptionsType, PropertyInfo eventSourceOptionsKeywordsProperty)
		{
			object eventSourceOptions = Activator.CreateInstance(eventSourceOptionsType);
			EventKeywords keywords = (EventKeywords)64L;
			eventSourceOptionsKeywordsProperty.SetValue(eventSourceOptions, keywords);
			PageViewPerfData pageViewPerfData = new PageViewPerfData();
			MethodInfo writeMethod = writeGenericMethod.MakeGenericMethod(new Type[] { new
			{
				PartA_iKey = this.dummyPartAiKeyValue,
				PartA_Tags = this.dummyPartATagsValue,
				PartB_PageViewPerfData = new
				{
					pageViewPerfData.perfTotal, pageViewPerfData.networkConnect, pageViewPerfData.sentRequest, pageViewPerfData.receivedResponse, pageViewPerfData.domProcessing, pageViewPerfData.url, pageViewPerfData.duration, pageViewPerfData.ver, pageViewPerfData.name, pageViewPerfData.properties,
					pageViewPerfData.measurements
				}
			}.GetType() });
			return delegate(ITelemetry item)
			{
				if (this.EventSourceInternal.IsEnabled(EventLevel.Verbose, keywords))
				{
					PageViewTelemetry pageViewTelemetry = item as PageViewTelemetry;
					RichPayloadEventSource.CopyGlobalPropertiesIfRequired(item, pageViewTelemetry.Properties);
					item.Sanitize();
					PageViewData data = pageViewTelemetry.Data;
					var <>f__AnonymousType = new
					{
						PartA_iKey = pageViewTelemetry.Context.InstrumentationKey,
						PartA_Tags = pageViewTelemetry.Context.SanitizedTags,
						PartB_PageViewPerfData = new { data.url, data.duration, data.ver, data.name, data.properties, data.measurements }
					};
					writeMethod.Invoke(eventSource, new object[] { "PageView", eventSourceOptions, <>f__AnonymousType });
				}
			};
		}

		// Token: 0x060003DF RID: 991 RVA: 0x000117B4 File Offset: 0x0000F9B4
		private Action<ITelemetry> CreateHandlerForSessionStateTelemetry(EventSource eventSource, MethodInfo writeGenericMethod, Type eventSourceOptionsType, PropertyInfo eventSourceOptionsKeywordsProperty)
		{
			object eventSourceOptions = Activator.CreateInstance(eventSourceOptionsType);
			EventKeywords keywords = (EventKeywords)4L;
			eventSourceOptionsKeywordsProperty.SetValue(eventSourceOptions, keywords);
			EventData eventData = new EventData();
			MethodInfo writeMethod = writeGenericMethod.MakeGenericMethod(new Type[] { new
			{
				PartA_iKey = this.dummyPartAiKeyValue,
				PartA_Tags = this.dummyPartATagsValue,
				PartB_EventData = new { eventData.ver, eventData.name, eventData.properties, eventData.measurements },
				PartA_flags = this.dummyPartAFlagsValue
			}.GetType() });
			return delegate(ITelemetry item)
			{
				if (this.EventSourceInternal.IsEnabled(EventLevel.Verbose, keywords))
				{
					EventTelemetry data = (item as SessionStateTelemetry).Data;
					RichPayloadEventSource.CopyGlobalPropertiesIfRequired(item, data.Properties);
					item.Sanitize();
					EventData data2 = data.Data;
					var <>f__AnonymousType = new
					{
						PartA_iKey = data.Context.InstrumentationKey,
						PartA_Tags = data.Context.SanitizedTags,
						PartB_EventData = new { data2.ver, data2.name, data2.properties, data2.measurements },
						PartA_flags = data.Context.Flags
					};
					writeMethod.Invoke(eventSource, new object[] { "Event", eventSourceOptions, <>f__AnonymousType });
				}
			};
		}

		// Token: 0x0400018C RID: 396
		public static readonly RichPayloadEventSource Log = new RichPayloadEventSource();

		// Token: 0x0400018D RID: 397
		internal readonly EventSource EventSourceInternal;

		// Token: 0x0400018E RID: 398
		private const string EventProviderName = "Microsoft-ApplicationInsights-Data";

		// Token: 0x0400018F RID: 399
		private readonly Dictionary<Type, Action<ITelemetry>> telemetryHandlers;

		// Token: 0x04000190 RID: 400
		private readonly Action<OperationTelemetry, EventOpcode> operationStartStopHandler;

		// Token: 0x04000191 RID: 401
		private readonly Action<EventData, string, IDictionary<string, string>, long> unknownTelemetryHandler;

		// Token: 0x04000192 RID: 402
		private readonly string dummyPartAiKeyValue = string.Empty;

		// Token: 0x04000193 RID: 403
		private readonly long dummyPartAFlagsValue;

		// Token: 0x04000194 RID: 404
		private readonly IDictionary<string, string> dummyPartATagsValue = new Dictionary<string, string>();

		// Token: 0x02000100 RID: 256
		public sealed class Keywords
		{
			// Token: 0x04000394 RID: 916
			public const EventKeywords Requests = (EventKeywords)1L;

			// Token: 0x04000395 RID: 917
			public const EventKeywords Traces = (EventKeywords)2L;

			// Token: 0x04000396 RID: 918
			public const EventKeywords Events = (EventKeywords)4L;

			// Token: 0x04000397 RID: 919
			public const EventKeywords Exceptions = (EventKeywords)8L;

			// Token: 0x04000398 RID: 920
			public const EventKeywords Dependencies = (EventKeywords)16L;

			// Token: 0x04000399 RID: 921
			public const EventKeywords Metrics = (EventKeywords)32L;

			// Token: 0x0400039A RID: 922
			public const EventKeywords PageViews = (EventKeywords)64L;

			// Token: 0x0400039B RID: 923
			public const EventKeywords PerformanceCounters = (EventKeywords)128L;

			// Token: 0x0400039C RID: 924
			public const EventKeywords SessionState = (EventKeywords)256L;

			// Token: 0x0400039D RID: 925
			public const EventKeywords Availability = (EventKeywords)512L;

			// Token: 0x0400039E RID: 926
			public const EventKeywords Operations = (EventKeywords)1024L;

			// Token: 0x0400039F RID: 927
			public const EventKeywords PageViewPerformance = (EventKeywords)2048L;
		}
	}
}
