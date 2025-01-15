using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Azure.Core.Pipeline
{
	// Token: 0x020000A6 RID: 166
	[NullableContext(1)]
	[Nullable(0)]
	internal readonly struct DiagnosticScope : IDisposable
	{
		// Token: 0x0600052D RID: 1325 RVA: 0x0000FE4C File Offset: 0x0000E04C
		[RequiresUnreferencedCode("The diagnosticSourceArgs are used in a call to DiagnosticSource.Write, all necessary properties need to be preserved on the type being passed in using DynamicDependency attributes.")]
		internal DiagnosticScope(string scopeName, DiagnosticListener source, [Nullable(2)] object diagnosticSourceArgs, [Nullable(2)] ActivitySource activitySource, ActivityKind kind, bool suppressNestedClientActivities)
		{
			this._suppressNestedClientActivities = (kind == ActivityKind.Client || kind == ActivityKind.Internal) && suppressNestedClientActivities;
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

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x0000FEDF File Offset: 0x0000E0DF
		public bool IsEnabled { get; }

		// Token: 0x0600052F RID: 1327 RVA: 0x0000FEE7 File Offset: 0x0000E0E7
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

		// Token: 0x06000530 RID: 1328 RVA: 0x0000FEFE File Offset: 0x0000E0FE
		public void AddIntegerAttribute(string name, int value)
		{
			DiagnosticScope.ActivityAdapter activityAdapter = this._activityAdapter;
			if (activityAdapter == null)
			{
				return;
			}
			activityAdapter.AddTag(name, value);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0000FF18 File Offset: 0x0000E118
		public void AddAttribute<[Nullable(2)] T>(string name, T value, Func<T, string> format)
		{
			if (this._activityAdapter != null && value != null)
			{
				string text = format(value);
				this._activityAdapter.AddTag(name, text);
			}
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0000FF4A File Offset: 0x0000E14A
		public void AddLink(string traceparent, [Nullable(2)] string tracestate, [Nullable(new byte[] { 2, 1, 1 })] IDictionary<string, string> attributes = null)
		{
			DiagnosticScope.ActivityAdapter activityAdapter = this._activityAdapter;
			if (activityAdapter == null)
			{
				return;
			}
			activityAdapter.AddLink(traceparent, tracestate, attributes);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0000FF60 File Offset: 0x0000E160
		public void Start()
		{
			DiagnosticScope.ActivityAdapter activityAdapter = this._activityAdapter;
			Activity activity = ((activityAdapter != null) ? activityAdapter.Start() : null);
			if (this._suppressNestedClientActivities && activity != null)
			{
				activity.SetCustomProperty("az.sdk.scope", DiagnosticScope.AzureSdkScopeValue);
			}
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0000FF9B File Offset: 0x0000E19B
		public void SetDisplayName(string displayName)
		{
			DiagnosticScope.ActivityAdapter activityAdapter = this._activityAdapter;
			if (activityAdapter == null)
			{
				return;
			}
			activityAdapter.SetDisplayName(displayName);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0000FFAE File Offset: 0x0000E1AE
		public void SetStartTime(DateTime dateTime)
		{
			DiagnosticScope.ActivityAdapter activityAdapter = this._activityAdapter;
			if (activityAdapter == null)
			{
				return;
			}
			activityAdapter.SetStartTime(dateTime);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0000FFC1 File Offset: 0x0000E1C1
		public void SetTraceContext(string traceparent, [Nullable(2)] string tracestate = null)
		{
			DiagnosticScope.ActivityAdapter activityAdapter = this._activityAdapter;
			if (activityAdapter == null)
			{
				return;
			}
			activityAdapter.SetTraceContext(traceparent, tracestate);
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0000FFD5 File Offset: 0x0000E1D5
		public void Dispose()
		{
			DiagnosticScope.ActivityAdapter activityAdapter = this._activityAdapter;
			if (activityAdapter == null)
			{
				return;
			}
			activityAdapter.Dispose();
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0000FFE8 File Offset: 0x0000E1E8
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

		// Token: 0x06000539 RID: 1337 RVA: 0x0001003B File Offset: 0x0000E23B
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

		// Token: 0x0400021F RID: 543
		private const string AzureSdkScopeLabel = "az.sdk.scope";

		// Token: 0x04000220 RID: 544
		internal const string OpenTelemetrySchemaAttribute = "az.schema_url";

		// Token: 0x04000221 RID: 545
		internal const string OpenTelemetrySchemaVersion = "https://opentelemetry.io/schemas/1.23.0";

		// Token: 0x04000222 RID: 546
		private static readonly object AzureSdkScopeValue = bool.TrueString;

		// Token: 0x04000223 RID: 547
		[Nullable(2)]
		private readonly DiagnosticScope.ActivityAdapter _activityAdapter;

		// Token: 0x04000224 RID: 548
		private readonly bool _suppressNestedClientActivities;

		// Token: 0x02000133 RID: 307
		[Nullable(0)]
		private class DiagnosticActivity : Activity
		{
			// Token: 0x170001E1 RID: 481
			// (get) Token: 0x0600083B RID: 2107 RVA: 0x0001FA5A File Offset: 0x0001DC5A
			// (set) Token: 0x0600083C RID: 2108 RVA: 0x0001FA62 File Offset: 0x0001DC62
			public new IEnumerable<Activity> Links { get; set; } = Array.Empty<Activity>();

			// Token: 0x0600083D RID: 2109 RVA: 0x0001FA6B File Offset: 0x0001DC6B
			public DiagnosticActivity(string operationName)
				: base(operationName)
			{
			}
		}

		// Token: 0x02000134 RID: 308
		[NullableContext(2)]
		[Nullable(0)]
		private class ActivityAdapter : IDisposable
		{
			// Token: 0x0600083E RID: 2110 RVA: 0x0001FA7F File Offset: 0x0001DC7F
			[NullableContext(1)]
			public ActivityAdapter([Nullable(2)] ActivitySource activitySource, DiagnosticSource diagnosticSource, string activityName, ActivityKind kind, [Nullable(2)] object diagnosticSourceArgs)
			{
				this._activitySource = activitySource;
				this._diagnosticSource = diagnosticSource;
				this._activityName = activityName;
				this._kind = kind;
				this._diagnosticSourceArgs = diagnosticSourceArgs;
			}

			// Token: 0x0600083F RID: 2111 RVA: 0x0001FAAC File Offset: 0x0001DCAC
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

			// Token: 0x06000840 RID: 2112 RVA: 0x0001FAE8 File Offset: 0x0001DCE8
			private List<ActivityLink> GetActivitySourceLinkCollection()
			{
				if (this._links == null)
				{
					return null;
				}
				List<ActivityLink> list = new List<ActivityLink>();
				foreach (Activity activity in this._links)
				{
					ActivityTagsCollection activityTagsCollection = new ActivityTagsCollection();
					foreach (KeyValuePair<string, string> keyValuePair in activity.Tags)
					{
						activityTagsCollection.Add(keyValuePair.Key, keyValuePair.Value);
					}
					ActivityContext activityContext;
					if (ActivityContext.TryParse(activity.ParentId, activity.TraceStateString, out activityContext))
					{
						ActivityLink activityLink = new ActivityLink(activityContext, activityTagsCollection);
						list.Add(activityLink);
					}
				}
				return list;
			}

			// Token: 0x06000841 RID: 2113 RVA: 0x0001FBC4 File Offset: 0x0001DDC4
			[NullableContext(1)]
			public void AddLink(string traceparent, [Nullable(2)] string tracestate, [Nullable(new byte[] { 2, 1, 1 })] IDictionary<string, string> attributes)
			{
				Activity activity = new Activity("LinkedActivity");
				activity.SetParentId(traceparent);
				activity.SetIdFormat(ActivityIdFormat.W3C);
				activity.TraceStateString = tracestate;
				if (attributes != null)
				{
					foreach (KeyValuePair<string, string> keyValuePair in attributes)
					{
						activity.AddTag(keyValuePair.Key, keyValuePair.Value);
					}
				}
				if (this._links == null)
				{
					this._links = new List<Activity>();
				}
				this._links.Add(activity);
			}

			// Token: 0x06000842 RID: 2114 RVA: 0x0001FC60 File Offset: 0x0001DE60
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
					this._currentActivity.AddTag("az.schema_url", "https://opentelemetry.io/schemas/1.23.0");
				}
				else
				{
					if (!this._diagnosticSource.IsEnabled(this._activityName, this._diagnosticSourceArgs, null))
					{
						return null;
					}
					switch (this._kind)
					{
					case ActivityKind.Internal:
						this.AddTag("kind", "internal");
						break;
					case ActivityKind.Server:
						this.AddTag("kind", "server");
						break;
					case ActivityKind.Client:
						this.AddTag("kind", "client");
						break;
					case ActivityKind.Producer:
						this.AddTag("kind", "producer");
						break;
					case ActivityKind.Consumer:
						this.AddTag("kind", "consumer");
						break;
					}
					DiagnosticScope.DiagnosticActivity diagnosticActivity = new DiagnosticScope.DiagnosticActivity(this._activityName);
					IEnumerable<Activity> links = this._links;
					diagnosticActivity.Links = links ?? Array.Empty<Activity>();
					this._currentActivity = diagnosticActivity;
					this._currentActivity.SetIdFormat(ActivityIdFormat.W3C);
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

			// Token: 0x06000843 RID: 2115 RVA: 0x0001FE78 File Offset: 0x0001E078
			[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "The values being passed into Write have the commonly used properties being preserved with DynamicDependency on the ActivityAdapter.Start() method, or the responsibility is on the user of this struct since the struct constructor is marked with RequiresUnreferencedCode.")]
			private void WriteStartEvent()
			{
				this._diagnosticSource.Write(this._activityName + ".Start", this._diagnosticSourceArgs ?? this._currentActivity);
			}

			// Token: 0x06000844 RID: 2116 RVA: 0x0001FEA5 File Offset: 0x0001E0A5
			[NullableContext(1)]
			public void SetDisplayName(string displayName)
			{
				this._displayName = displayName;
				if (this._currentActivity != null)
				{
					this._currentActivity.DisplayName = this._displayName;
				}
			}

			// Token: 0x06000845 RID: 2117 RVA: 0x0001FEC8 File Offset: 0x0001E0C8
			private Activity StartActivitySourceActivity()
			{
				if (this._activitySource == null)
				{
					return null;
				}
				ActivityContext activityContext;
				ActivityContext.TryParse(this._traceparent, this._tracestate, out activityContext);
				return this._activitySource.StartActivity(this._activityName, this._kind, activityContext, this._tagCollection, this.GetActivitySourceLinkCollection(), this._startTime);
			}

			// Token: 0x06000846 RID: 2118 RVA: 0x0001FF1D File Offset: 0x0001E11D
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

			// Token: 0x06000847 RID: 2119 RVA: 0x0001FF40 File Offset: 0x0001E140
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
				currentActivity2.SetStatus(ActivityStatusCode.Error, (exception != null) ? exception.ToString() : null);
			}

			// Token: 0x06000848 RID: 2120 RVA: 0x0001FFE7 File Offset: 0x0001E1E7
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

			// Token: 0x06000849 RID: 2121 RVA: 0x0002000C File Offset: 0x0001E20C
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

			// Token: 0x0600084A RID: 2122 RVA: 0x0002005C File Offset: 0x0001E25C
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

			// Token: 0x040004BA RID: 1210
			private readonly ActivitySource _activitySource;

			// Token: 0x040004BB RID: 1211
			[Nullable(1)]
			private readonly DiagnosticSource _diagnosticSource;

			// Token: 0x040004BC RID: 1212
			[Nullable(1)]
			private readonly string _activityName;

			// Token: 0x040004BD RID: 1213
			private readonly ActivityKind _kind;

			// Token: 0x040004BE RID: 1214
			private readonly object _diagnosticSourceArgs;

			// Token: 0x040004BF RID: 1215
			private Activity _currentActivity;

			// Token: 0x040004C0 RID: 1216
			private Activity _sampleOutActivity;

			// Token: 0x040004C1 RID: 1217
			private ActivityTagsCollection _tagCollection;

			// Token: 0x040004C2 RID: 1218
			private DateTimeOffset _startTime;

			// Token: 0x040004C3 RID: 1219
			[Nullable(new byte[] { 2, 1 })]
			private List<Activity> _links;

			// Token: 0x040004C4 RID: 1220
			private string _traceparent;

			// Token: 0x040004C5 RID: 1221
			private string _tracestate;

			// Token: 0x040004C6 RID: 1222
			private string _displayName;
		}
	}
}
