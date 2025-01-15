using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Azure.Core.Pipeline
{
	// Token: 0x0200001A RID: 26
	[NullableContext(1)]
	[Nullable(0)]
	internal readonly struct DiagnosticScope : IDisposable
	{
		// Token: 0x0600006E RID: 110 RVA: 0x00003744 File Offset: 0x00001944
		[RequiresUnreferencedCode("The diagnosticSourceArgs are used in a call to DiagnosticSource.Write, all necessary properties need to be preserved on the type being passed in using DynamicDependency attributes.")]
		internal DiagnosticScope(string scopeName, DiagnosticListener source, [Nullable(2)] object diagnosticSourceArgs, [Nullable(2)] ActivitySource activitySource, ActivityKind kind, bool suppressNestedClientActivities)
		{
			this._suppressNestedClientActivities = (kind == 2 || kind == null) && suppressNestedClientActivities;
			bool flag = activitySource != null && activitySource.HasListeners();
			this.IsEnabled = source.IsEnabled() || flag;
			if (this._suppressNestedClientActivities)
			{
				bool isEnabled = this.IsEnabled;
				object azureSdkScopeValue = DiagnosticScope.AzureSdkScopeValue;
				Activity activity = Activity.Current;
				this.IsEnabled = isEnabled & !azureSdkScopeValue.Equals((activity != null) ? activity.GetCustomProperty("az.sdk.scope") : null);
			}
			this._activityAdapter = (this.IsEnabled ? new DiagnosticScope.ActivityAdapter(activitySource, source, scopeName, kind, diagnosticSourceArgs) : null);
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000037D7 File Offset: 0x000019D7
		public bool IsEnabled { get; }

		// Token: 0x06000070 RID: 112 RVA: 0x000037DF File Offset: 0x000019DF
		public void AddAttribute(string name, [Nullable(2)] string value)
		{
			if (value != null)
			{
				DiagnosticScope.ActivityAdapter activityAdapter = this._activityAdapter;
				if (activityAdapter == null)
				{
					return;
				}
				activityAdapter.AddTag(name, value);
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000037F6 File Offset: 0x000019F6
		public void AddIntegerAttribute(string name, int value)
		{
			DiagnosticScope.ActivityAdapter activityAdapter = this._activityAdapter;
			if (activityAdapter == null)
			{
				return;
			}
			activityAdapter.AddTag(name, value);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000380F File Offset: 0x00001A0F
		public void AddLongAttribute(string name, long value)
		{
			DiagnosticScope.ActivityAdapter activityAdapter = this._activityAdapter;
			if (activityAdapter == null)
			{
				return;
			}
			activityAdapter.AddTag(name, value);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003828 File Offset: 0x00001A28
		public void AddAttribute<[Nullable(2)] T>(string name, T value, Func<T, string> format)
		{
			if (this._activityAdapter != null && value != null)
			{
				string text = format(value);
				this._activityAdapter.AddTag(name, text);
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000385A File Offset: 0x00001A5A
		public void AddLink(string traceparent, [Nullable(2)] string tracestate, [Nullable(new byte[] { 2, 1, 2 })] IDictionary<string, object> attributes = null)
		{
			DiagnosticScope.ActivityAdapter activityAdapter = this._activityAdapter;
			if (activityAdapter == null)
			{
				return;
			}
			activityAdapter.AddLink(traceparent, tracestate, attributes);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003870 File Offset: 0x00001A70
		public void Start()
		{
			DiagnosticScope.ActivityAdapter activityAdapter = this._activityAdapter;
			Activity activity = ((activityAdapter != null) ? activityAdapter.Start() : null);
			if (this._suppressNestedClientActivities && activity != null)
			{
				activity.SetCustomProperty("az.sdk.scope", DiagnosticScope.AzureSdkScopeValue);
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000038AB File Offset: 0x00001AAB
		public void SetDisplayName(string displayName)
		{
			DiagnosticScope.ActivityAdapter activityAdapter = this._activityAdapter;
			if (activityAdapter == null)
			{
				return;
			}
			activityAdapter.SetDisplayName(displayName);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000038BE File Offset: 0x00001ABE
		public void SetStartTime(DateTime dateTime)
		{
			DiagnosticScope.ActivityAdapter activityAdapter = this._activityAdapter;
			if (activityAdapter == null)
			{
				return;
			}
			activityAdapter.SetStartTime(dateTime);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000038D1 File Offset: 0x00001AD1
		public void SetTraceContext(string traceparent, [Nullable(2)] string tracestate = null)
		{
			DiagnosticScope.ActivityAdapter activityAdapter = this._activityAdapter;
			if (activityAdapter == null)
			{
				return;
			}
			activityAdapter.SetTraceContext(traceparent, tracestate);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000038E5 File Offset: 0x00001AE5
		public void Dispose()
		{
			DiagnosticScope.ActivityAdapter activityAdapter = this._activityAdapter;
			if (activityAdapter == null)
			{
				return;
			}
			activityAdapter.Dispose();
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000038F8 File Offset: 0x00001AF8
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "The Exception being passed into this method has public properties preserved on the inner method MarkFailed.The public property System.Exception.TargetSite.get is not compatible with trimming and produces a warning when preserving all public properties. Since we do not use this property, andneither does Application Insights, we can suppress the warning coming from the inner method.")]
		public void Failed(Exception exception)
		{
			RequestFailedException ex = exception as RequestFailedException;
			if (ex != null)
			{
				string text = (string.IsNullOrEmpty(ex.ErrorCode) ? null : ex.ErrorCode);
				DiagnosticScope.ActivityAdapter activityAdapter = this._activityAdapter;
				if (activityAdapter == null)
				{
					return;
				}
				activityAdapter.MarkFailed<Exception>(exception, text);
				return;
			}
			else
			{
				DiagnosticScope.ActivityAdapter activityAdapter2 = this._activityAdapter;
				if (activityAdapter2 == null)
				{
					return;
				}
				activityAdapter2.MarkFailed<Exception>(exception, null);
				return;
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000394B File Offset: 0x00001B4B
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "The public property System.Exception.TargetSite.get is not compatible with trimming and produces a warning when preserving all public properties. Since we do not use this property, and neither does Application Insights, we can suppress the warning coming from the inner method.")]
		public void Failed(string errorCode)
		{
			DiagnosticScope.ActivityAdapter activityAdapter = this._activityAdapter;
			if (activityAdapter == null)
			{
				return;
			}
			activityAdapter.MarkFailed<Exception>(null, errorCode);
		}

		// Token: 0x04000043 RID: 67
		private const string AzureSdkScopeLabel = "az.sdk.scope";

		// Token: 0x04000044 RID: 68
		internal const string OpenTelemetrySchemaAttribute = "az.schema_url";

		// Token: 0x04000045 RID: 69
		internal const string OpenTelemetrySchemaVersion = "https://opentelemetry.io/schemas/1.23.0";

		// Token: 0x04000046 RID: 70
		private static readonly object AzureSdkScopeValue = bool.TrueString;

		// Token: 0x04000047 RID: 71
		[Nullable(2)]
		private readonly DiagnosticScope.ActivityAdapter _activityAdapter;

		// Token: 0x04000048 RID: 72
		private readonly bool _suppressNestedClientActivities;

		// Token: 0x02000099 RID: 153
		[Nullable(0)]
		private class DiagnosticActivity : Activity
		{
			// Token: 0x1700014B RID: 331
			// (get) Token: 0x060004B9 RID: 1209 RVA: 0x0000ECA6 File Offset: 0x0000CEA6
			// (set) Token: 0x060004BA RID: 1210 RVA: 0x0000ECAE File Offset: 0x0000CEAE
			public IEnumerable<Activity> Links { get; set; } = Array.Empty<Activity>();

			// Token: 0x060004BB RID: 1211 RVA: 0x0000ECB7 File Offset: 0x0000CEB7
			public DiagnosticActivity(string operationName)
				: base(operationName)
			{
			}
		}

		// Token: 0x0200009A RID: 154
		[NullableContext(2)]
		[Nullable(0)]
		private class ActivityAdapter : IDisposable
		{
			// Token: 0x060004BC RID: 1212 RVA: 0x0000ECCB File Offset: 0x0000CECB
			[NullableContext(1)]
			public ActivityAdapter([Nullable(2)] ActivitySource activitySource, DiagnosticSource diagnosticSource, string activityName, ActivityKind kind, [Nullable(2)] object diagnosticSourceArgs)
			{
				this._activitySource = activitySource;
				this._diagnosticSource = diagnosticSource;
				this._activityName = activityName;
				this._kind = kind;
				this._diagnosticSourceArgs = diagnosticSourceArgs;
			}

			// Token: 0x060004BD RID: 1213 RVA: 0x0000ECF8 File Offset: 0x0000CEF8
			[NullableContext(1)]
			public void AddTag(string name, object value)
			{
				if (this._sampleOutActivity == null)
				{
					if (this._currentActivity == null)
					{
						if (this._tagCollection == null)
						{
							this._tagCollection = new ActivityTagsCollection();
						}
						this._tagCollection[name] = value;
						return;
					}
					this.AddObjectTag(name, value);
				}
			}

			// Token: 0x060004BE RID: 1214 RVA: 0x0000ED34 File Offset: 0x0000CF34
			[NullableContext(1)]
			private IReadOnlyList<Activity> GetDiagnosticSourceLinkCollection()
			{
				if (this._links == null)
				{
					return Array.Empty<Activity>();
				}
				List<Activity> list = new List<Activity>();
				foreach (ActivityLink activityLink in this._links)
				{
					Activity activity = new Activity("LinkedActivity");
					activity.SetIdFormat(2);
					if (activityLink.Context != default(ActivityContext))
					{
						activity.SetParentId(DiagnosticScope.ActivityAdapter.ActivityContextToTraceParent(activityLink.Context));
						activity.TraceStateString = activityLink.Context.TraceState;
					}
					if (activityLink.Tags != null)
					{
						foreach (KeyValuePair<string, object> keyValuePair in activityLink.Tags)
						{
							if (keyValuePair.Value != null)
							{
								activity.AddTag(keyValuePair.Key, keyValuePair.Value.ToString());
							}
						}
					}
					list.Add(activity);
				}
				return list;
			}

			// Token: 0x060004BF RID: 1215 RVA: 0x0000EE60 File Offset: 0x0000D060
			[NullableContext(1)]
			private static string ActivityContextToTraceParent(ActivityContext context)
			{
				string text = ((context.TraceFlags == null) ? "00" : "01");
				return string.Concat(new string[]
				{
					"00-",
					context.TraceId.ToString(),
					"-",
					context.SpanId.ToString(),
					"-",
					text
				});
			}

			// Token: 0x060004C0 RID: 1216 RVA: 0x0000EEDC File Offset: 0x0000D0DC
			[NullableContext(1)]
			public void AddLink(string traceparent, [Nullable(2)] string tracestate, [Nullable(new byte[] { 2, 1, 2 })] IDictionary<string, object> attributes)
			{
				ActivityContext activityContext;
				ActivityContext.TryParse(traceparent, tracestate, ref activityContext);
				ActivityLink activityLink;
				activityLink..ctor(activityContext, (attributes == null) ? null : new ActivityTagsCollection(attributes));
				if (this._links == null)
				{
					this._links = new List<ActivityLink>();
				}
				this._links.Add(activityLink);
			}

			// Token: 0x060004C1 RID: 1217 RVA: 0x0000EF28 File Offset: 0x0000D128
			[DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.PublicProperties, typeof(Activity))]
			[DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.PublicProperties, typeof(DiagnosticScope.DiagnosticActivity))]
			public Activity Start()
			{
				this._currentActivity = this.StartActivitySourceActivity();
				if (this._currentActivity != null)
				{
					if (!this._currentActivity.IsAllDataRequested)
					{
						this._sampleOutActivity = this._currentActivity;
						this._currentActivity = null;
						return null;
					}
					this._currentActivity.SetTag("az.schema_url", "https://opentelemetry.io/schemas/1.23.0");
				}
				else
				{
					if (!this._diagnosticSource.IsEnabled(this._activityName, this._diagnosticSourceArgs, null))
					{
						return null;
					}
					switch (this._kind)
					{
					case 0:
						this.AddTag("kind", "internal");
						break;
					case 1:
						this.AddTag("kind", "server");
						break;
					case 2:
						this.AddTag("kind", "client");
						break;
					case 3:
						this.AddTag("kind", "producer");
						break;
					case 4:
						this.AddTag("kind", "consumer");
						break;
					}
					this._currentActivity = new DiagnosticScope.DiagnosticActivity(this._activityName)
					{
						Links = this.GetDiagnosticSourceLinkCollection()
					};
					this._currentActivity.SetIdFormat(2);
					if (this._startTime != default(DateTimeOffset))
					{
						this._currentActivity.SetStartTime(this._startTime.UtcDateTime);
					}
					if (this._tagCollection != null)
					{
						foreach (KeyValuePair<string, object> keyValuePair in this._tagCollection)
						{
							this.AddObjectTag(keyValuePair.Key, keyValuePair.Value);
						}
					}
					if (this._traceparent != null)
					{
						this._currentActivity.SetParentId(this._traceparent);
					}
					if (this._tracestate != null)
					{
						this._currentActivity.TraceStateString = this._tracestate;
					}
					this._currentActivity.Start();
				}
				this.WriteStartEvent();
				if (this._displayName != null)
				{
					this._currentActivity.DisplayName = this._displayName;
				}
				return this._currentActivity;
			}

			// Token: 0x060004C2 RID: 1218 RVA: 0x0000F134 File Offset: 0x0000D334
			[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "The values being passed into Write have the commonly used properties being preserved with DynamicDependency on the ActivityAdapter.Start() method, or the responsibility is on the user of this struct since the struct constructor is marked with RequiresUnreferencedCode.")]
			private void WriteStartEvent()
			{
				this._diagnosticSource.Write(this._activityName + ".Start", this._diagnosticSourceArgs ?? this._currentActivity);
			}

			// Token: 0x060004C3 RID: 1219 RVA: 0x0000F161 File Offset: 0x0000D361
			[NullableContext(1)]
			public void SetDisplayName(string displayName)
			{
				this._displayName = displayName;
				if (this._currentActivity != null)
				{
					this._currentActivity.DisplayName = this._displayName;
				}
			}

			// Token: 0x060004C4 RID: 1220 RVA: 0x0000F184 File Offset: 0x0000D384
			private Activity StartActivitySourceActivity()
			{
				if (this._activitySource == null)
				{
					return null;
				}
				ActivityContext activityContext;
				ActivityContext.TryParse(this._traceparent, this._tracestate, ref activityContext);
				return this._activitySource.StartActivity(this._activityName, this._kind, activityContext, this._tagCollection, this._links, this._startTime);
			}

			// Token: 0x060004C5 RID: 1221 RVA: 0x0000F1D9 File Offset: 0x0000D3D9
			public void SetStartTime(DateTime startTime)
			{
				this._startTime = startTime;
				Activity currentActivity = this._currentActivity;
				if (currentActivity == null)
				{
					return;
				}
				currentActivity.SetStartTime(startTime);
			}

			// Token: 0x060004C6 RID: 1222 RVA: 0x0000F1FC File Offset: 0x0000D3FC
			[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "The Exception being passed into this method has the commonly used properties being preserved with DynamicallyAccessedMemberTypes.")]
			public void MarkFailed<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] T>(T exception, string errorCode)
			{
				if (exception != null)
				{
					DiagnosticSource diagnosticSource = this._diagnosticSource;
					if (diagnosticSource != null)
					{
						diagnosticSource.Write(this._activityName + ".Exception", exception);
					}
				}
				if (errorCode == null && exception != null)
				{
					errorCode = exception.GetType().FullName;
				}
				if (errorCode == null)
				{
					errorCode = "_OTHER";
				}
				Activity currentActivity = this._currentActivity;
				if (currentActivity != null)
				{
					currentActivity.SetTag("error.type", errorCode);
				}
				Activity currentActivity2 = this._currentActivity;
				if (currentActivity2 == null)
				{
					return;
				}
				currentActivity2.SetStatus(2, (exception != null) ? exception.ToString() : null);
			}

			// Token: 0x060004C7 RID: 1223 RVA: 0x0000F2A3 File Offset: 0x0000D4A3
			[NullableContext(1)]
			public void SetTraceContext(string traceparent, [Nullable(2)] string tracestate)
			{
				if (this._currentActivity != null)
				{
					throw new InvalidOperationException("Traceparent can not be set after the activity is started.");
				}
				this._traceparent = traceparent;
				this._tracestate = tracestate;
			}

			// Token: 0x060004C8 RID: 1224 RVA: 0x0000F2C8 File Offset: 0x0000D4C8
			[NullableContext(1)]
			private void AddObjectTag(string name, object value)
			{
				ActivitySource activitySource = this._activitySource;
				if (activitySource != null && activitySource.HasListeners())
				{
					Activity currentActivity = this._currentActivity;
					if (currentActivity == null)
					{
						return;
					}
					currentActivity.SetTag(name, value);
					return;
				}
				else
				{
					Activity currentActivity2 = this._currentActivity;
					if (currentActivity2 == null)
					{
						return;
					}
					currentActivity2.AddTag(name, value.ToString());
					return;
				}
			}

			// Token: 0x060004C9 RID: 1225 RVA: 0x0000F318 File Offset: 0x0000D518
			[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "The class constructor is marked with RequiresUnreferencedCode.")]
			public void Dispose()
			{
				Activity activity = this._currentActivity ?? this._sampleOutActivity;
				if (activity == null)
				{
					return;
				}
				if (activity.Duration == TimeSpan.Zero)
				{
					activity.SetEndTime(DateTime.UtcNow);
				}
				this._diagnosticSource.Write(this._activityName + ".Stop", this._diagnosticSourceArgs);
				activity.Dispose();
				this._currentActivity = null;
				this._sampleOutActivity = null;
			}

			// Token: 0x040002C1 RID: 705
			private readonly ActivitySource _activitySource;

			// Token: 0x040002C2 RID: 706
			[Nullable(1)]
			private readonly DiagnosticSource _diagnosticSource;

			// Token: 0x040002C3 RID: 707
			[Nullable(1)]
			private readonly string _activityName;

			// Token: 0x040002C4 RID: 708
			private readonly ActivityKind _kind;

			// Token: 0x040002C5 RID: 709
			private readonly object _diagnosticSourceArgs;

			// Token: 0x040002C6 RID: 710
			private Activity _currentActivity;

			// Token: 0x040002C7 RID: 711
			private Activity _sampleOutActivity;

			// Token: 0x040002C8 RID: 712
			private ActivityTagsCollection _tagCollection;

			// Token: 0x040002C9 RID: 713
			private DateTimeOffset _startTime;

			// Token: 0x040002CA RID: 714
			private List<ActivityLink> _links;

			// Token: 0x040002CB RID: 715
			private string _traceparent;

			// Token: 0x040002CC RID: 716
			private string _tracestate;

			// Token: 0x040002CD RID: 717
			private string _displayName;
		}
	}
}
