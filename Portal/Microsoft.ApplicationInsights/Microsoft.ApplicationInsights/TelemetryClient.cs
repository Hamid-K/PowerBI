using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.ApplicationInsights.Extensibility.Implementation.Platform;
using Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing;
using Microsoft.ApplicationInsights.Metrics;
using Microsoft.ApplicationInsights.Metrics.Extensibility;

namespace Microsoft.ApplicationInsights
{
	// Token: 0x02000024 RID: 36
	public sealed class TelemetryClient
	{
		// Token: 0x06000117 RID: 279 RVA: 0x00006FF0 File Offset: 0x000051F0
		public TelemetryClient()
			: this(TelemetryConfiguration.Active)
		{
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00007000 File Offset: 0x00005200
		public TelemetryClient(TelemetryConfiguration configuration)
		{
			if (configuration == null)
			{
				CoreEventSource.Log.TelemetryClientConstructorWithNoTelemetryConfiguration("Incorrect");
				configuration = TelemetryConfiguration.Active;
			}
			this.configuration = configuration;
			if (this.configuration.TelemetryChannel == null)
			{
				throw new ArgumentException("The specified configuration does not have a telemetry channel.", "configuration");
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000119 RID: 281 RVA: 0x0000705B File Offset: 0x0000525B
		// (set) Token: 0x0600011A RID: 282 RVA: 0x00007063 File Offset: 0x00005263
		public TelemetryContext Context { get; internal set; } = new TelemetryContext();

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600011B RID: 283 RVA: 0x0000706C File Offset: 0x0000526C
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00007079 File Offset: 0x00005279
		public string InstrumentationKey
		{
			get
			{
				return this.Context.InstrumentationKey;
			}
			set
			{
				this.Context.InstrumentationKey = value;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00007087 File Offset: 0x00005287
		internal TelemetryConfiguration TelemetryConfiguration
		{
			get
			{
				return this.configuration;
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000708F File Offset: 0x0000528F
		public bool IsEnabled()
		{
			return !this.configuration.DisableTelemetry;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000070A0 File Offset: 0x000052A0
		public void TrackEvent(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
		{
			EventTelemetry eventTelemetry = new EventTelemetry(eventName);
			if (properties != null && properties.Count > 0)
			{
				Utils.CopyDictionary<string>(properties, eventTelemetry.Properties);
			}
			if (metrics != null && metrics.Count > 0)
			{
				Utils.CopyDictionary<double>(metrics, eventTelemetry.Metrics);
			}
			this.TrackEvent(eventTelemetry);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000070EB File Offset: 0x000052EB
		public void TrackEvent(EventTelemetry telemetry)
		{
			if (telemetry == null)
			{
				telemetry = new EventTelemetry();
			}
			this.Track(telemetry);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000070FE File Offset: 0x000052FE
		public void TrackTrace(string message)
		{
			this.TrackTrace(new TraceTelemetry(message));
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000710C File Offset: 0x0000530C
		public void TrackTrace(string message, SeverityLevel severityLevel)
		{
			this.TrackTrace(new TraceTelemetry(message, severityLevel));
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000711C File Offset: 0x0000531C
		public void TrackTrace(string message, IDictionary<string, string> properties)
		{
			TraceTelemetry traceTelemetry = new TraceTelemetry(message);
			if (properties != null && properties.Count > 0)
			{
				Utils.CopyDictionary<string>(properties, traceTelemetry.Properties);
			}
			this.TrackTrace(traceTelemetry);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00007150 File Offset: 0x00005350
		public void TrackTrace(string message, SeverityLevel severityLevel, IDictionary<string, string> properties)
		{
			TraceTelemetry traceTelemetry = new TraceTelemetry(message, severityLevel);
			if (properties != null && properties.Count > 0)
			{
				Utils.CopyDictionary<string>(properties, traceTelemetry.Properties);
			}
			this.TrackTrace(traceTelemetry);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00007184 File Offset: 0x00005384
		public void TrackTrace(TraceTelemetry telemetry)
		{
			telemetry = telemetry ?? new TraceTelemetry();
			this.Track(telemetry);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000719C File Offset: 0x0000539C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void TrackMetric(string name, double value, IDictionary<string, string> properties = null)
		{
			MetricTelemetry metricTelemetry = new MetricTelemetry(name, value);
			if (properties != null && properties.Count > 0)
			{
				Utils.CopyDictionary<string>(properties, metricTelemetry.Properties);
			}
			this.TrackMetric(metricTelemetry);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000071D0 File Offset: 0x000053D0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void TrackMetric(MetricTelemetry telemetry)
		{
			if (telemetry == null)
			{
				telemetry = new MetricTelemetry();
			}
			this.Track(telemetry);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000071E4 File Offset: 0x000053E4
		public void TrackException(Exception exception, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
		{
			if (exception == null)
			{
				exception = new Exception(Utils.PopulateRequiredStringValue(null, "message", typeof(ExceptionTelemetry).FullName));
			}
			ExceptionTelemetry exceptionTelemetry = new ExceptionTelemetry(exception);
			if (properties != null && properties.Count > 0)
			{
				Utils.CopyDictionary<string>(properties, exceptionTelemetry.Properties);
			}
			if (metrics != null && metrics.Count > 0)
			{
				Utils.CopyDictionary<double>(metrics, exceptionTelemetry.Metrics);
			}
			this.TrackException(exceptionTelemetry);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00007253 File Offset: 0x00005453
		public void TrackException(ExceptionTelemetry telemetry)
		{
			if (telemetry == null)
			{
				telemetry = new ExceptionTelemetry(new Exception(Utils.PopulateRequiredStringValue(null, "message", typeof(ExceptionTelemetry).FullName)));
			}
			this.Track(telemetry);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00007285 File Offset: 0x00005485
		[Obsolete("Please use a different overload of TrackDependency")]
		public void TrackDependency(string dependencyName, string data, DateTimeOffset startTime, TimeSpan duration, bool success)
		{
			this.TrackDependency(new DependencyTelemetry(dependencyName, data, startTime, duration, success));
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000729C File Offset: 0x0000549C
		public void TrackDependency(string dependencyTypeName, string dependencyName, string data, DateTimeOffset startTime, TimeSpan duration, bool success)
		{
			this.TrackDependency(new DependencyTelemetry(dependencyTypeName, null, dependencyName, data, startTime, duration, null, success));
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000072C0 File Offset: 0x000054C0
		public void TrackDependency(string dependencyTypeName, string target, string dependencyName, string data, DateTimeOffset startTime, TimeSpan duration, string resultCode, bool success)
		{
			this.TrackDependency(new DependencyTelemetry(dependencyTypeName, target, dependencyName, data, startTime, duration, resultCode, success));
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000072E5 File Offset: 0x000054E5
		public void TrackDependency(DependencyTelemetry telemetry)
		{
			if (telemetry == null)
			{
				telemetry = new DependencyTelemetry();
			}
			this.Track(telemetry);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000072F8 File Offset: 0x000054F8
		public void TrackAvailability(string name, DateTimeOffset timeStamp, TimeSpan duration, string runLocation, bool success, string message = null, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
		{
			AvailabilityTelemetry availabilityTelemetry = new AvailabilityTelemetry(name, timeStamp, duration, runLocation, success, message);
			if (properties != null && properties.Count > 0)
			{
				Utils.CopyDictionary<string>(properties, availabilityTelemetry.Properties);
			}
			if (metrics != null && metrics.Count > 0)
			{
				Utils.CopyDictionary<double>(metrics, availabilityTelemetry.Metrics);
			}
			this.TrackAvailability(availabilityTelemetry);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00007351 File Offset: 0x00005551
		public void TrackAvailability(AvailabilityTelemetry telemetry)
		{
			if (telemetry == null)
			{
				telemetry = new AvailabilityTelemetry();
			}
			this.Track(telemetry);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00007364 File Offset: 0x00005564
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Track(ITelemetry telemetry)
		{
			if (this.IsEnabled())
			{
				this.Initialize(telemetry);
				telemetry.Context.ClearTempRawObjects();
				this.configuration.TelemetryProcessorChain.Process(telemetry);
				RichPayloadEventSource.Log.Process(telemetry);
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000739C File Offset: 0x0000559C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void InitializeInstrumentationKey(ITelemetry telemetry)
		{
			string text = this.Context.InstrumentationKey;
			if (string.IsNullOrEmpty(text))
			{
				text = this.configuration.InstrumentationKey;
			}
			telemetry.Context.InitializeInstrumentationkey(text);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000073D8 File Offset: 0x000055D8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Initialize(ITelemetry telemetry)
		{
			string text = this.Context.InstrumentationKey;
			if (string.IsNullOrEmpty(text))
			{
				text = this.configuration.InstrumentationKey;
			}
			ISupportProperties supportProperties = telemetry as ISupportProperties;
			if (supportProperties != null && this.configuration.TelemetryChannel != null && this.configuration.TelemetryChannel.DeveloperMode != null && this.configuration.TelemetryChannel.DeveloperMode.Value && !supportProperties.Properties.ContainsKey("DeveloperMode"))
			{
				supportProperties.Properties.Add("DeveloperMode", "true");
			}
			if (this.Context.PropertiesValue != null)
			{
				Utils.CopyDictionary<string>(this.Context.Properties, telemetry.Context.Properties);
			}
			if (this.Context.GlobalPropertiesValue != null)
			{
				Utils.CopyDictionary<string>(this.Context.GlobalProperties, telemetry.Context.GlobalProperties);
			}
			telemetry.Context.Initialize(this.Context, text);
			foreach (ITelemetryInitializer telemetryInitializer in this.configuration.TelemetryInitializers)
			{
				try
				{
					telemetryInitializer.Initialize(telemetry);
				}
				catch (Exception ex)
				{
					CoreEventSource.Log.LogError(string.Format(CultureInfo.InvariantCulture, "Exception while initializing {0}, exception message - {1}", new object[]
					{
						telemetryInitializer.GetType().FullName,
						ex
					}), "Incorrect");
				}
			}
			if (telemetry.Timestamp == default(DateTimeOffset))
			{
				telemetry.Timestamp = PreciseTimestamp.GetUtcNow();
			}
			if (string.IsNullOrEmpty(telemetry.Context.Internal.SdkVersion))
			{
				string text2;
				if ((text2 = this.sdkVersion) == null)
				{
					text2 = (this.sdkVersion = SdkVersionUtils.GetSdkVersion("dotnet:"));
				}
				string text3 = text2;
				telemetry.Context.Internal.SdkVersion = text3;
			}
			if (string.IsNullOrEmpty(telemetry.Context.Internal.NodeName) && !string.IsNullOrEmpty(telemetry.Context.Cloud.RoleInstance))
			{
				telemetry.Context.Internal.NodeName = PlatformSingleton.Current.GetMachineName();
			}
			if (string.IsNullOrEmpty(telemetry.Context.Cloud.RoleInstance))
			{
				telemetry.Context.Cloud.RoleInstance = PlatformSingleton.Current.GetMachineName();
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00007650 File Offset: 0x00005850
		public void TrackPageView(string name)
		{
			this.Track(new PageViewTelemetry(name));
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000765E File Offset: 0x0000585E
		public void TrackPageView(PageViewTelemetry telemetry)
		{
			if (telemetry == null)
			{
				telemetry = new PageViewTelemetry();
			}
			this.Track(telemetry);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00007671 File Offset: 0x00005871
		public void TrackRequest(string name, DateTimeOffset startTime, TimeSpan duration, string responseCode, bool success)
		{
			this.Track(new RequestTelemetry(name, startTime, duration, responseCode, success));
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00007685 File Offset: 0x00005885
		public void TrackRequest(RequestTelemetry request)
		{
			if (request == null)
			{
				request = new RequestTelemetry();
			}
			this.Track(request);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00007698 File Offset: 0x00005898
		public void Flush()
		{
			MetricManager metricManager;
			if (this.TryGetMetricManager(out metricManager))
			{
				metricManager.Flush(false);
			}
			TelemetryConfiguration telemetryConfiguration = this.configuration;
			if (telemetryConfiguration != null)
			{
				MetricManager metricManager2 = telemetryConfiguration.GetMetricManager(false);
				if (metricManager2 != null)
				{
					metricManager2.Flush(false);
				}
				ITelemetryChannel telemetryChannel = telemetryConfiguration.TelemetryChannel;
				if (telemetryChannel == null)
				{
					return;
				}
				telemetryChannel.Flush();
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000076E3 File Offset: 0x000058E3
		public Metric GetMetric(string metricId)
		{
			return this.GetOrCreateMetric(MetricAggregationScope.TelemetryConfiguration, new MetricIdentifier(metricId), null);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000076F3 File Offset: 0x000058F3
		public Metric GetMetric(string metricId, MetricConfiguration metricConfiguration)
		{
			return this.GetOrCreateMetric(MetricAggregationScope.TelemetryConfiguration, new MetricIdentifier(metricId), metricConfiguration);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00007703 File Offset: 0x00005903
		public Metric GetMetric(string metricId, MetricConfiguration metricConfiguration, MetricAggregationScope aggregationScope)
		{
			return this.GetOrCreateMetric(aggregationScope, new MetricIdentifier(metricId), metricConfiguration);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00007713 File Offset: 0x00005913
		public Metric GetMetric(string metricId, string dimension1Name)
		{
			return this.GetOrCreateMetric(MetricAggregationScope.TelemetryConfiguration, new MetricIdentifier(MetricIdentifier.DefaultMetricNamespace, metricId, dimension1Name), null);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00007729 File Offset: 0x00005929
		public Metric GetMetric(string metricId, string dimension1Name, MetricConfiguration metricConfiguration)
		{
			return this.GetOrCreateMetric(MetricAggregationScope.TelemetryConfiguration, new MetricIdentifier(MetricIdentifier.DefaultMetricNamespace, metricId, dimension1Name), metricConfiguration);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000773F File Offset: 0x0000593F
		public Metric GetMetric(string metricId, string dimension1Name, MetricConfiguration metricConfiguration, MetricAggregationScope aggregationScope)
		{
			return this.GetOrCreateMetric(aggregationScope, new MetricIdentifier(MetricIdentifier.DefaultMetricNamespace, metricId, dimension1Name), metricConfiguration);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00007756 File Offset: 0x00005956
		public Metric GetMetric(string metricId, string dimension1Name, string dimension2Name)
		{
			return this.GetOrCreateMetric(MetricAggregationScope.TelemetryConfiguration, new MetricIdentifier(MetricIdentifier.DefaultMetricNamespace, metricId, dimension1Name, dimension2Name), null);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000776D File Offset: 0x0000596D
		public Metric GetMetric(string metricId, string dimension1Name, string dimension2Name, MetricConfiguration metricConfiguration)
		{
			return this.GetOrCreateMetric(MetricAggregationScope.TelemetryConfiguration, new MetricIdentifier(MetricIdentifier.DefaultMetricNamespace, metricId, dimension1Name, dimension2Name), metricConfiguration);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00007785 File Offset: 0x00005985
		public Metric GetMetric(string metricId, string dimension1Name, string dimension2Name, MetricConfiguration metricConfiguration, MetricAggregationScope aggregationScope)
		{
			return this.GetOrCreateMetric(aggregationScope, new MetricIdentifier(MetricIdentifier.DefaultMetricNamespace, metricId, dimension1Name, dimension2Name), metricConfiguration);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000779E File Offset: 0x0000599E
		public Metric GetMetric(string metricId, string dimension1Name, string dimension2Name, string dimension3Name)
		{
			return this.GetOrCreateMetric(MetricAggregationScope.TelemetryConfiguration, new MetricIdentifier(MetricIdentifier.DefaultMetricNamespace, metricId, dimension1Name, dimension2Name, dimension3Name), null);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000077B7 File Offset: 0x000059B7
		public Metric GetMetric(string metricId, string dimension1Name, string dimension2Name, string dimension3Name, MetricConfiguration metricConfiguration)
		{
			return this.GetOrCreateMetric(MetricAggregationScope.TelemetryConfiguration, new MetricIdentifier(MetricIdentifier.DefaultMetricNamespace, metricId, dimension1Name, dimension2Name, dimension3Name), metricConfiguration);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000077D1 File Offset: 0x000059D1
		public Metric GetMetric(string metricId, string dimension1Name, string dimension2Name, string dimension3Name, MetricConfiguration metricConfiguration, MetricAggregationScope aggregationScope)
		{
			return this.GetOrCreateMetric(aggregationScope, new MetricIdentifier(MetricIdentifier.DefaultMetricNamespace, metricId, dimension1Name, dimension2Name, dimension3Name), metricConfiguration);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000077EC File Offset: 0x000059EC
		public Metric GetMetric(string metricId, string dimension1Name, string dimension2Name, string dimension3Name, string dimension4Name)
		{
			return this.GetOrCreateMetric(MetricAggregationScope.TelemetryConfiguration, new MetricIdentifier(MetricIdentifier.DefaultMetricNamespace, metricId, dimension1Name, dimension2Name, dimension3Name, dimension4Name), null);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00007807 File Offset: 0x00005A07
		public Metric GetMetric(string metricId, string dimension1Name, string dimension2Name, string dimension3Name, string dimension4Name, MetricConfiguration metricConfiguration)
		{
			return this.GetOrCreateMetric(MetricAggregationScope.TelemetryConfiguration, new MetricIdentifier(MetricIdentifier.DefaultMetricNamespace, metricId, dimension1Name, dimension2Name, dimension3Name, dimension4Name), metricConfiguration);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00007823 File Offset: 0x00005A23
		public Metric GetMetric(string metricId, string dimension1Name, string dimension2Name, string dimension3Name, string dimension4Name, MetricConfiguration metricConfiguration, MetricAggregationScope aggregationScope)
		{
			return this.GetOrCreateMetric(aggregationScope, new MetricIdentifier(MetricIdentifier.DefaultMetricNamespace, metricId, dimension1Name, dimension2Name, dimension3Name, dimension4Name), metricConfiguration);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00007840 File Offset: 0x00005A40
		public Metric GetMetric(MetricIdentifier metricIdentifier)
		{
			return this.GetOrCreateMetric(MetricAggregationScope.TelemetryConfiguration, metricIdentifier, null);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000784B File Offset: 0x00005A4B
		public Metric GetMetric(MetricIdentifier metricIdentifier, MetricConfiguration metricConfiguration)
		{
			return this.GetOrCreateMetric(MetricAggregationScope.TelemetryConfiguration, metricIdentifier, metricConfiguration);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00007856 File Offset: 0x00005A56
		public Metric GetMetric(MetricIdentifier metricIdentifier, MetricConfiguration metricConfiguration, MetricAggregationScope aggregationScope)
		{
			return this.GetOrCreateMetric(aggregationScope, metricIdentifier, metricConfiguration);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00007861 File Offset: 0x00005A61
		private Metric GetOrCreateMetric(MetricAggregationScope aggregationScope, MetricIdentifier metricIdentifier, MetricConfiguration metricConfiguration)
		{
			return this.GetMetricManager(aggregationScope).Metrics.GetOrCreate(metricIdentifier, metricConfiguration);
		}

		// Token: 0x04000096 RID: 150
		private const string VersionPrefix = "dotnet:";

		// Token: 0x04000097 RID: 151
		private readonly TelemetryConfiguration configuration;

		// Token: 0x04000098 RID: 152
		private string sdkVersion;
	}
}
