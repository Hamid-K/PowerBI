using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;

namespace Microsoft.Mashup.Engine1.Library.Diagnostics
{
	// Token: 0x02000CB4 RID: 3252
	internal sealed class DiagnosticsModule : Module
	{
		// Token: 0x17001A76 RID: 6774
		// (get) Token: 0x0600580E RID: 22542 RVA: 0x00133666 File Offset: 0x00131866
		public override string Name
		{
			get
			{
				return "Diagnostics";
			}
		}

		// Token: 0x17001A77 RID: 6775
		// (get) Token: 0x0600580F RID: 22543 RVA: 0x0013366D File Offset: 0x0013186D
		public override Keys ExportKeys
		{
			get
			{
				if (this.exportKeys == null)
				{
					this.exportKeys = Keys.New(9, delegate(int index)
					{
						switch (index)
						{
						case 0:
							return "Diagnostics.Trace";
						case 1:
							return "Diagnostics.ActivityId";
						case 2:
							return "Diagnostics.CorrelationId";
						case 3:
							return DiagnosticsModule.TraceLevels.Type.GetName();
						case 4:
							return DiagnosticsModule.TraceLevels.Critical.GetName();
						case 5:
							return DiagnosticsModule.TraceLevels.Error.GetName();
						case 6:
							return DiagnosticsModule.TraceLevels.Warning.GetName();
						case 7:
							return DiagnosticsModule.TraceLevels.Information.GetName();
						case 8:
							return DiagnosticsModule.TraceLevels.Verbose.GetName();
						default:
							throw new InvalidOperationException();
						}
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x06005810 RID: 22544 RVA: 0x001336AC File Offset: 0x001318AC
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				switch (index)
				{
				case 0:
					return new DiagnosticsModule.Diagnostics.TraceFunctionValue(hostEnvironment);
				case 1:
					return new DiagnosticsModule.Diagnostics.ActivityIdFunctionValue(hostEnvironment);
				case 2:
					return new DiagnosticsModule.Diagnostics.CorrelationIdFunctionValue(hostEnvironment);
				case 3:
					return DiagnosticsModule.TraceLevels.Type;
				case 4:
					return DiagnosticsModule.TraceLevels.Critical;
				case 5:
					return DiagnosticsModule.TraceLevels.Error;
				case 6:
					return DiagnosticsModule.TraceLevels.Warning;
				case 7:
					return DiagnosticsModule.TraceLevels.Information;
				case 8:
					return DiagnosticsModule.TraceLevels.Verbose;
				default:
					throw new InvalidOperationException();
				}
			});
		}

		// Token: 0x0400319A RID: 12698
		private Keys exportKeys;

		// Token: 0x02000CB5 RID: 3253
		private enum Exports
		{
			// Token: 0x0400319C RID: 12700
			Diagnostics_Trace,
			// Token: 0x0400319D RID: 12701
			Diagnostics_ActivityId,
			// Token: 0x0400319E RID: 12702
			Diagnostics_CorrelationId,
			// Token: 0x0400319F RID: 12703
			TraceLevel_Type,
			// Token: 0x040031A0 RID: 12704
			TraceLevel_Critical,
			// Token: 0x040031A1 RID: 12705
			TraceLevel_Error,
			// Token: 0x040031A2 RID: 12706
			TraceLevel_Warning,
			// Token: 0x040031A3 RID: 12707
			TraceLevel_Information,
			// Token: 0x040031A4 RID: 12708
			TraceLevel_Verbose,
			// Token: 0x040031A5 RID: 12709
			Count
		}

		// Token: 0x02000CB6 RID: 3254
		private static class TraceLevels
		{
			// Token: 0x040031A6 RID: 12710
			public static IntEnumTypeValue<TraceEventType> Type = new IntEnumTypeValue<TraceEventType>("TraceLevel.Type");

			// Token: 0x040031A7 RID: 12711
			public static readonly NumberValue Critical = DiagnosticsModule.TraceLevels.Type.NewEnumValue("TraceLevel.Critical", 1, TraceEventType.Critical, null);

			// Token: 0x040031A8 RID: 12712
			public static readonly NumberValue Error = DiagnosticsModule.TraceLevels.Type.NewEnumValue("TraceLevel.Error", 2, TraceEventType.Error, null);

			// Token: 0x040031A9 RID: 12713
			public static readonly NumberValue Warning = DiagnosticsModule.TraceLevels.Type.NewEnumValue("TraceLevel.Warning", 4, TraceEventType.Warning, null);

			// Token: 0x040031AA RID: 12714
			public static readonly NumberValue Information = DiagnosticsModule.TraceLevels.Type.NewEnumValue("TraceLevel.Information", 8, TraceEventType.Information, null);

			// Token: 0x040031AB RID: 12715
			public static readonly NumberValue Verbose = DiagnosticsModule.TraceLevels.Type.NewEnumValue("TraceLevel.Verbose", 16, TraceEventType.Verbose, null);
		}

		// Token: 0x02000CB7 RID: 3255
		private static class Diagnostics
		{
			// Token: 0x02000CB8 RID: 3256
			public sealed class TraceFunctionValue : NativeFunctionValue4<Value, NumberValue, Value, Value, Value>
			{
				// Token: 0x06005813 RID: 22547 RVA: 0x00133774 File Offset: 0x00131974
				public TraceFunctionValue(IEngineHost engineHost)
					: base(TypeValue.Any, 3, "traceLevel", TypeValue.Number, "message", TypeValue.Any.NonNullable, "value", TypeValue.Any, "delayed", NullableTypeValue.Logical)
				{
					this.engineHost = engineHost;
				}

				// Token: 0x06005814 RID: 22548 RVA: 0x001337C4 File Offset: 0x001319C4
				public override Value TypedInvoke(NumberValue traceLevel, Value message, Value value, Value delayed)
				{
					RecordValue recordValue;
					if (message.IsRecord)
					{
						recordValue = message.AsRecord;
					}
					else
					{
						recordValue = RecordValue.New(DiagnosticsModule.Diagnostics.TraceFunctionValue.legacyDataKeys, new Value[] { RecordValue.New(DiagnosticsModule.Diagnostics.TraceFunctionValue.legacyMessageKeys, new Value[] { message.AsText }) });
					}
					string text = string.Empty;
					Value value2;
					if (recordValue.TryGetValue("Name", out value2))
					{
						text = "/" + value2.AsText.String;
					}
					RecordValue recordValue2 = recordValue["Data"].AsRecord;
					RecordValue recordValue3 = RecordValue.Empty;
					if (recordValue.TryGetValue("SafeData", out value2))
					{
						recordValue3 = value2.AsRecord;
					}
					recordValue = Library.Record.RemoveFields.Invoke(recordValue, DiagnosticsModule.Diagnostics.TraceFunctionValue.knownKeys, Library.MissingField.Ignore).AsRecord;
					if (recordValue.Count > 0)
					{
						throw ValueException.NewExpressionError<Message1>(Strings.ValueException_UnexpectedField(recordValue.Keys[0]), message, null);
					}
					string text2 = recordValue2.Keys.Intersect(recordValue3.Keys).FirstOrDefault<string>();
					if (text2 != null)
					{
						throw ValueException.NewExpressionError<Message1>(Strings.ValueException_DuplicatedField(text2), recordValue3, null);
					}
					TraceEventType asInteger = (TraceEventType)traceLevel.AsInteger32;
					if ((asInteger & (TraceEventType)31) != asInteger)
					{
						throw ValueException.NewExpressionError<Message1>(Strings.InvalidTraceEventType((int)asInteger), null, null);
					}
					string text3;
					if (!ExtensionModule.TryGetModuleName(this.engineHost, out text3))
					{
						text3 = null;
					}
					string text4 = "Engine/User";
					if (text3 != null)
					{
						text4 = string.Format(CultureInfo.InvariantCulture, "Engine/Module/{0}", text3);
					}
					IResource resource = null;
					IExtensibilityService extensibilityService = this.engineHost.QueryService<IExtensibilityService>();
					if (extensibilityService != null)
					{
						resource = extensibilityService.CurrentResource;
						text4 = string.Format(CultureInfo.InvariantCulture, "{0}/IO", text4);
					}
					Value value4;
					using (IHostTrace hostTrace = this.CreateTrace(text3, text4 + text, asInteger, resource))
					{
						Value value3 = value;
						recordValue2 = recordValue2.Concatenate(recordValue3).AsRecord;
						for (int i = 0; i < recordValue2.Count; i++)
						{
							string text5 = recordValue2.Keys[i];
							bool flag = !recordValue3.Keys.Contains(text5) && ValueException2.IsPii(recordValue2[i]);
							hostTrace.Add(text5, DiagnosticsModule.Diagnostics.TraceFunctionValue.GetValue(recordValue2[i]), flag);
						}
						if (!delayed.IsNull && delayed.AsBoolean)
						{
							value3 = value.AsFunction.Invoke();
						}
						value4 = value3;
					}
					return value4;
				}

				// Token: 0x06005815 RID: 22549 RVA: 0x00133A20 File Offset: 0x00131C20
				private IHostTrace CreateTrace(string moduleName, string traceName, TraceEventType eventType, IResource resource)
				{
					ITracingService service = TracingService.GetService(this.engineHost);
					IEvaluationConstants evaluationConstants = this.engineHost.GetEvaluationConstants();
					if (moduleName != null)
					{
						return service.CreateTrace(traceName, evaluationConstants, eventType, resource);
					}
					return service.CreateUserTrace(evaluationConstants, traceName, eventType);
				}

				// Token: 0x06005816 RID: 22550 RVA: 0x00133A60 File Offset: 0x00131C60
				private static object GetValue(Value value)
				{
					object obj;
					try
					{
						obj = ValueMarshaller.MarshalToClr(value);
					}
					catch (ValueException ex)
					{
						obj = ex.ToString();
					}
					return obj;
				}

				// Token: 0x040031AC RID: 12716
				private const string MessageKey = "Message";

				// Token: 0x040031AD RID: 12717
				private const string DataKey = "Data";

				// Token: 0x040031AE RID: 12718
				private const string NameKey = "Name";

				// Token: 0x040031AF RID: 12719
				private const string SafeDataKey = "SafeData";

				// Token: 0x040031B0 RID: 12720
				private static readonly Keys legacyDataKeys = Keys.New("Data");

				// Token: 0x040031B1 RID: 12721
				private static readonly Keys legacyMessageKeys = Keys.New("Message");

				// Token: 0x040031B2 RID: 12722
				private static readonly ListValue knownKeys = ListValue.New(new string[] { "Name", "Data", "SafeData" });

				// Token: 0x040031B3 RID: 12723
				private readonly IEngineHost engineHost;
			}

			// Token: 0x02000CB9 RID: 3257
			public sealed class ActivityIdFunctionValue : NativeFunctionValue0<Value>
			{
				// Token: 0x06005818 RID: 22552 RVA: 0x00133AE3 File Offset: 0x00131CE3
				public ActivityIdFunctionValue(IEngineHost engineHost)
					: base(NullableTypeValue.Text)
				{
					this.engineHost = engineHost;
				}

				// Token: 0x06005819 RID: 22553 RVA: 0x00133AF8 File Offset: 0x00131CF8
				public override Value TypedInvoke()
				{
					IEvaluationConstants evaluationConstants = this.engineHost.QueryService<IEvaluationConstants>();
					return TextValue.NewOrNull((evaluationConstants == null) ? null : evaluationConstants.ActivityId.ToString());
				}

				// Token: 0x040031B4 RID: 12724
				private readonly IEngineHost engineHost;
			}

			// Token: 0x02000CBA RID: 3258
			public sealed class CorrelationIdFunctionValue : NativeFunctionValue0<Value>
			{
				// Token: 0x0600581A RID: 22554 RVA: 0x00133B30 File Offset: 0x00131D30
				public CorrelationIdFunctionValue(IEngineHost engineHost)
					: base(NullableTypeValue.Text)
				{
					this.engineHost = engineHost;
				}

				// Token: 0x0600581B RID: 22555 RVA: 0x00133B44 File Offset: 0x00131D44
				public override Value TypedInvoke()
				{
					IEvaluationConstants evaluationConstants = this.engineHost.QueryService<IEvaluationConstants>();
					return TextValue.NewOrNull((evaluationConstants != null) ? evaluationConstants.CorrelationId : null);
				}

				// Token: 0x040031B5 RID: 12725
				private readonly IEngineHost engineHost;
			}
		}
	}
}
