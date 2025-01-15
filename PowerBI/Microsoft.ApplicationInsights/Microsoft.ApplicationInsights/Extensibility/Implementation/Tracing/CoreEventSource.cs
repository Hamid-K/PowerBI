using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing
{
	// Token: 0x02000092 RID: 146
	[EventSource(Name = "Microsoft-ApplicationInsights-Core")]
	[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "appDomainName is required")]
	internal sealed class CoreEventSource : EventSource
	{
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x00013C78 File Offset: 0x00011E78
		public static bool IsVerboseEnabled
		{
			[NonEvent]
			get
			{
				return CoreEventSource.Log.IsEnabled(EventLevel.Verbose, EventKeywords.All);
			}
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00013C87 File Offset: 0x00011E87
		[Event(1, Message = "Operation object is null.", Level = EventLevel.Warning)]
		public void OperationIsNullWarning(string appDomainName = "Incorrect")
		{
			base.WriteEvent(1, this.nameProvider.Name);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00013C9B File Offset: 0x00011E9B
		[Event(2, Message = "Operation to stop does not match the current operation. Telemetry is not tracked.", Level = EventLevel.Error)]
		public void InvalidOperationToStopError(string appDomainName = "Incorrect")
		{
			base.WriteEvent(2, this.nameProvider.Name);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00013CAF File Offset: 0x00011EAF
		[Event(3, Keywords = (EventKeywords)4L, Message = "[msg=Log verbose];[msg={0}]", Level = EventLevel.Verbose)]
		public void LogVerbose(string msg, string appDomainName = "Incorrect")
		{
			base.WriteEvent(3, msg ?? string.Empty, this.nameProvider.Name);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00013CCD File Offset: 0x00011ECD
		[Event(4, Keywords = (EventKeywords)3L, Message = "Diagnostics event throttling has been started for the event {0}", Level = EventLevel.Informational)]
		public void DiagnosticsEventThrottlingHasBeenStartedForTheEvent(string eventId, string appDomainName = "Incorrect")
		{
			base.WriteEvent(4, eventId ?? "NULL", this.nameProvider.Name);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00013CEB File Offset: 0x00011EEB
		[Event(5, Keywords = (EventKeywords)3L, Message = "Diagnostics event throttling has been reset for the event {0}, event was fired {1} times during last interval", Level = EventLevel.Informational)]
		public void DiagnosticsEventThrottlingHasBeenResetForTheEvent(int eventId, int executionCount, string appDomainName = "Incorrect")
		{
			base.WriteEvent(5, new object[]
			{
				eventId,
				executionCount,
				this.nameProvider.Name
			});
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00013D1A File Offset: 0x00011F1A
		[Event(6, Keywords = (EventKeywords)2L, Message = "Scheduler timer dispose failure: {0}", Level = EventLevel.Warning)]
		public void DiagnoisticsEventThrottlingSchedulerDisposeTimerFailure(string exception, string appDomainName = "Incorrect")
		{
			base.WriteEvent(6, exception ?? string.Empty, this.nameProvider.Name);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00013D38 File Offset: 0x00011F38
		[Event(7, Keywords = (EventKeywords)2L, Message = "A scheduler timer was created for the interval: {0}", Level = EventLevel.Verbose)]
		public void DiagnoisticsEventThrottlingSchedulerTimerWasCreated(string intervalInMilliseconds, string appDomainName = "Incorrect")
		{
			base.WriteEvent(7, intervalInMilliseconds ?? "NULL", this.nameProvider.Name);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00013D56 File Offset: 0x00011F56
		[Event(8, Keywords = (EventKeywords)2L, Message = "A scheduler timer was removed", Level = EventLevel.Verbose)]
		public void DiagnoisticsEventThrottlingSchedulerTimerWasRemoved(string appDomainName = "Incorrect")
		{
			base.WriteEvent(8, this.nameProvider.Name);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00013D6A File Offset: 0x00011F6A
		[Event(9, Message = "No Telemetry Configuration provided. Using the default TelemetryConfiguration.Active.", Level = EventLevel.Warning)]
		public void TelemetryClientConstructorWithNoTelemetryConfiguration(string appDomainName = "Incorrect")
		{
			base.WriteEvent(9, this.nameProvider.Name);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00013D7F File Offset: 0x00011F7F
		[Event(10, Message = "Value for property '{0}' of {1} was not found. Populating it by default.", Level = EventLevel.Verbose)]
		public void PopulateRequiredStringWithValue(string parameterName, string telemetryType, string appDomainName = "Incorrect")
		{
			base.WriteEvent(10, parameterName ?? string.Empty, telemetryType ?? string.Empty, this.nameProvider.Name);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00013DA8 File Offset: 0x00011FA8
		[Event(11, Message = "Invalid duration for Telemetry. Setting it to '00:00:00'.", Level = EventLevel.Warning)]
		public void TelemetryIncorrectDuration(string appDomainName = "Incorrect")
		{
			base.WriteEvent(11, this.nameProvider.Name);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00013DBD File Offset: 0x00011FBD
		[Event(12, Message = "Telemetry tracking was disabled. Message is dropped.", Level = EventLevel.Verbose)]
		public void TrackingWasDisabled(string appDomainName = "Incorrect")
		{
			base.WriteEvent(12, this.nameProvider.Name);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00013DD2 File Offset: 0x00011FD2
		[Event(13, Message = "Telemetry tracking was enabled. Messages are being logged.", Level = EventLevel.Verbose)]
		public void TrackingWasEnabled(string appDomainName = "Incorrect")
		{
			base.WriteEvent(13, this.nameProvider.Name);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00013DE7 File Offset: 0x00011FE7
		[Event(14, Keywords = (EventKeywords)8L, Message = "[msg=Log Error];[msg={0}]", Level = EventLevel.Error)]
		public void LogError(string msg, string appDomainName = "Incorrect")
		{
			base.WriteEvent(14, msg ?? string.Empty, this.nameProvider.Name);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00013E06 File Offset: 0x00012006
		[Event(15, Keywords = (EventKeywords)1L, Message = "ApplicationInsights configuration file loading failed. Type '{0}' was not found. Type loading was skipped. Monitoring will continue.", Level = EventLevel.Error)]
		public void TypeWasNotFoundConfigurationError(string type, string appDomainName = "Incorrect")
		{
			base.WriteEvent(15, type ?? string.Empty, this.nameProvider.Name);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00013E25 File Offset: 0x00012025
		[Event(16, Keywords = (EventKeywords)1L, Message = "ApplicationInsights configuration file loading failed. Type '{0}' does not implement '{1}'. Type loading was skipped. Monitoring will continue.", Level = EventLevel.Error)]
		public void IncorrectTypeConfigurationError(string type, string expectedType, string appDomainName = "Incorrect")
		{
			base.WriteEvent(16, type ?? string.Empty, expectedType ?? string.Empty, this.nameProvider.Name);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00013E4E File Offset: 0x0001204E
		[Event(17, Keywords = (EventKeywords)1L, Message = "ApplicationInsights configuration file loading failed. Type '{0}' does not have property '{1}'. Property initialization was skipped. Monitoring will continue.", Level = EventLevel.Error)]
		public void IncorrectPropertyConfigurationError(string type, string property, string appDomainName = "Incorrect")
		{
			base.WriteEvent(17, type ?? string.Empty, property ?? string.Empty, this.nameProvider.Name);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00013E77 File Offset: 0x00012077
		[Event(18, Keywords = (EventKeywords)1L, Message = "ApplicationInsights configuration file loading failed. Element '{0}' element does not have a Type attribute, does not specify a value and is not a valid collection type. Type initialization was skipped. Monitoring will continue.", Level = EventLevel.Error)]
		public void IncorrectInstanceAtributesConfigurationError(string definition, string appDomainName = "Incorrect")
		{
			base.WriteEvent(18, definition ?? string.Empty, this.nameProvider.Name);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00013E98 File Offset: 0x00012098
		[Event(19, Keywords = (EventKeywords)1L, Message = "ApplicationInsights configuration file loading failed. '{0}' element has unexpected contents: '{1}': '{2}'. Type initialization was skipped. Monitoring will continue.", Level = EventLevel.Error)]
		public void LoadInstanceFromValueConfigurationError(string element, string contents, string error, string appDomainName = "Incorrect")
		{
			base.WriteEvent(19, new object[]
			{
				element ?? string.Empty,
				contents ?? string.Empty,
				error ?? string.Empty,
				this.nameProvider.Name
			});
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00013EE8 File Offset: 0x000120E8
		[Event(20, Keywords = (EventKeywords)1L, Message = "ApplicationInsights configuration file loading failed. Exception: '{0}'. Monitoring will continue if you set InstrumentationKey programmatically.", Level = EventLevel.Error)]
		public void ConfigurationFileCouldNotBeParsedError(string error, string appDomainName = "Incorrect")
		{
			base.WriteEvent(20, error ?? string.Empty, this.nameProvider.Name);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00013F07 File Offset: 0x00012107
		[Event(21, Keywords = (EventKeywords)1L, Message = "ApplicationInsights configuration file loading failed. Type '{0}' will not be create. Error: '{1}'. Monitoring will continue if you set InstrumentationKey programmatically.", Level = EventLevel.Error)]
		public void MissingMethodExceptionConfigurationError(string type, string error, string appDomainName = "Incorrect")
		{
			base.WriteEvent(21, type ?? string.Empty, error ?? string.Empty, this.nameProvider.Name);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00013F30 File Offset: 0x00012130
		[Event(22, Keywords = (EventKeywords)1L, Message = "ApplicationInsights configuration file loading failed. Type '{0}' will not be initialized. Error: '{1}'. Monitoring will continue if you set InstrumentationKey programmatically.", Level = EventLevel.Error)]
		public void ComponentInitializationConfigurationError(string type, string error, string appDomainName = "Incorrect")
		{
			base.WriteEvent(22, type ?? string.Empty, error ?? string.Empty, this.nameProvider.Name);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00013F59 File Offset: 0x00012159
		[Event(23, Message = "ApplicationInsights configuration file '{0}' was not found.", Level = EventLevel.Warning)]
		public void ApplicationInsightsConfigNotFoundWarning(string file, string appDomainName = "Incorrect")
		{
			base.WriteEvent(23, file ?? string.Empty, this.nameProvider.Name);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00013F78 File Offset: 0x00012178
		[Event(24, Message = "Failed to send: {0}.", Level = EventLevel.Warning)]
		public void FailedToSend(string msg, string appDomainName = "Incorrect")
		{
			base.WriteEvent(24, msg ?? string.Empty, this.nameProvider.Name);
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00013F97 File Offset: 0x00012197
		[Event(25, Message = "Exception happened during getting the machine name: '{0}'.", Level = EventLevel.Error)]
		public void FailedToGetMachineName(string error, string appDomainName = "Incorrect")
		{
			base.WriteEvent(25, error ?? string.Empty, this.nameProvider.Name);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00013FB6 File Offset: 0x000121B6
		[Event(26, Message = "Failed to flush aggregated metrics. Exception: {0}.", Level = EventLevel.Error)]
		public void FailedToFlushMetricAggregators(string ex, string appDomainName = "Incorrect")
		{
			base.WriteEvent(26, ex ?? string.Empty, this.nameProvider.Name);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00013FD5 File Offset: 0x000121D5
		[Event(27, Message = "Failed to snapshot aggregated metrics. Exception: {0}.", Level = EventLevel.Error)]
		public void FailedToSnapshotMetricAggregators(string ex, string appDomainName = "Incorrect")
		{
			base.WriteEvent(27, ex ?? string.Empty, this.nameProvider.Name);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00013FF4 File Offset: 0x000121F4
		[Event(28, Message = "Failed to invoke metric processor '{0}'. If the issue persists, remove the processor. Exception: {1}.", Level = EventLevel.Error)]
		public void FailedToRunMetricProcessor(string processorName, string ex, string appDomainName = "Incorrect")
		{
			base.WriteEvent(28, processorName ?? string.Empty, ex ?? string.Empty, this.nameProvider.Name);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0001401D File Offset: 0x0001221D
		[Event(29, Message = "The backlog of unsent items has reached maximum size of {0}. Items will be dropped until the backlog is cleared.", Level = EventLevel.Error)]
		public void ItemDroppedAsMaximumUnsentBacklogSizeReached(int maxBacklogSize, string appDomainName = "Incorrect")
		{
			base.WriteEvent(29, new object[]
			{
				maxBacklogSize,
				this.nameProvider.Name
			});
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00014044 File Offset: 0x00012244
		[Event(30, Message = "Flush was called on the telemetry channel (InMemoryChannel) after it was disposed.", Level = EventLevel.Warning)]
		public void InMemoryChannelFlushedAfterBeingDisposed(string appDomainName = "Incorrect")
		{
			base.WriteEvent(30, this.nameProvider.Name);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00014059 File Offset: 0x00012259
		[Event(31, Message = "Send was called on the telemetry channel (InMemoryChannel) after it was disposed, the telemetry data was dropped.", Level = EventLevel.Warning)]
		public void InMemoryChannelSendCalledAfterBeingDisposed(string appDomainName = "Incorrect")
		{
			base.WriteEvent(31, this.nameProvider.Name);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0001406E File Offset: 0x0001226E
		[Event(32, Message = "Failed to get environment variables due to security exception; code is likely running in partial trust. Exception: {0}.", Level = EventLevel.Warning)]
		public void FailedToLoadEnvironmentVariables(string ex, string appDomainName = "Incorrect")
		{
			base.WriteEvent(32, ex, this.nameProvider.Name);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00014084 File Offset: 0x00012284
		[Event(33, Message = "A Metric Extractor detected a telemetry item with SamplingPercentage < 100. Metrics Extractors should be used before Sampling Processors or any other Telemetry Processors that might filter out Telemetry Items. Otherwise, extracted metrics may be incorrect.", Level = EventLevel.Error, Keywords = (EventKeywords)3L)]
		public void MetricExtractorAfterSamplingError(string appDomainName = "Incorrect")
		{
			base.WriteEvent(33, this.nameProvider.Name);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00014099 File Offset: 0x00012299
		[Event(34, Message = "A Metric Extractor detected a telemetry item with SamplingPercentage < 100. Metrics Extractors Extractor should be used before Sampling Processors or any other Telemetry Processors that might filter out Telemetry Items. Otherwise, extracted metrics may be incorrect.", Level = EventLevel.Verbose, Keywords = (EventKeywords)3L)]
		public void MetricExtractorAfterSamplingVerbose(string appDomainName = "Incorrect")
		{
			base.WriteEvent(34, this.nameProvider.Name);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x000140AE File Offset: 0x000122AE
		[Event(35, Message = "Item was rejected because it has no instrumentation key set. Item: {0}", Level = EventLevel.Verbose)]
		public void ItemRejectedNoInstrumentationKey(string item, string appDomainName = "Incorrect")
		{
			base.WriteEvent(35, item ?? string.Empty, this.nameProvider.Name);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x000140CD File Offset: 0x000122CD
		[Event(36, Message = "Failed to obtain a value for default heartbeat payload property '{0}': Exception {1}.", Level = EventLevel.Warning)]
		public void FailedToObtainDefaultHeartbeatProperty(string heartbeatProperty, string ex, string appDomainName = "Incorrect")
		{
			base.WriteEvent(36, heartbeatProperty ?? string.Empty, ex ?? string.Empty, this.nameProvider.Name);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x000140F8 File Offset: 0x000122F8
		[Event(37, Message = "Could not add heartbeat payload property '{0}' = {1}. Exception: {2}.", Level = EventLevel.Warning)]
		public void FailedToAddHeartbeatProperty(string heartbeatProperty, string heartbeatPropertyValue, string ex = null, string appDomainName = "Incorrect")
		{
			base.WriteEvent(37, new object[]
			{
				heartbeatProperty ?? string.Empty,
				heartbeatPropertyValue ?? string.Empty,
				ex ?? string.Empty,
				this.nameProvider.Name
			});
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00014148 File Offset: 0x00012348
		[Event(38, Message = "Cannot add heartbeat payload property without any property name. Value given was '{0}', isHealthy given was {1}.", Level = EventLevel.Warning)]
		public void HeartbeatPropertyAddedWithoutAnyName(string heartbeatPropertyValue, bool isHealthy, string appDomainName = "Incorrect")
		{
			base.WriteEvent(38, new object[]
			{
				heartbeatPropertyValue ?? string.Empty,
				isHealthy,
				this.nameProvider.Name
			});
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0001417C File Offset: 0x0001237C
		[Event(39, Message = "Could not set heartbeat payload property '{0}' = {1}, isHealthy was set = {2}, isHealthy value = {3}. Exception: {4}.", Level = EventLevel.Warning)]
		public void FailedToSetHeartbeatProperty(string heartbeatProperty, string heartbeatPropertyValue, bool isHealthyHasValue, bool isHealthy, string ex = null, string appDomainName = "Incorrect")
		{
			base.WriteEvent(39, new object[]
			{
				heartbeatProperty ?? string.Empty,
				heartbeatPropertyValue ?? string.Empty,
				isHealthyHasValue,
				isHealthy,
				ex ?? string.Empty,
				this.nameProvider.Name
			});
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x000141E0 File Offset: 0x000123E0
		[Event(40, Message = "Cannot set heartbeat payload property without a propertyName, or cannot set one of the default SDK properties. Property name given:'{0}'. Property value: '{1}'. isHealthy was set = {2}, isHealthy = {3}.", Level = EventLevel.Warning)]
		public void CannotSetHeartbeatPropertyWithNoNameOrDefaultName(string heartbeatProperty, string heartbeatPropertyValue, bool isHealthyHasValue, bool isHealthy, string appDomainName = "Incorrect")
		{
			base.WriteEvent(40, new object[]
			{
				heartbeatProperty ?? string.Empty,
				heartbeatPropertyValue ?? string.Empty,
				isHealthyHasValue,
				isHealthy,
				this.nameProvider.Name
			});
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00014236 File Offset: 0x00012436
		[Event(41, Keywords = (EventKeywords)1L, Message = "Failed to retrieve Application Id for the current application insights resource. Make sure the configured instrumentation key is valid. Error: {0}", Level = EventLevel.Warning)]
		public void ApplicationIdProviderFetchApplicationIdFailed(string exception, string appDomainName = "Incorrect")
		{
			base.WriteEvent(41, exception, this.nameProvider.Name);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0001424C File Offset: 0x0001244C
		[Event(42, Keywords = (EventKeywords)1L, Message = "Failed to retrieve Application Id for the current application insights resource. Endpoint returned HttpStatusCode: {0}", Level = EventLevel.Warning)]
		public void ApplicationIdProviderFetchApplicationIdFailedWithResponseCode(string httpStatusCode, string appDomainName = "Incorrect")
		{
			base.WriteEvent(42, httpStatusCode, this.nameProvider.Name);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00014262 File Offset: 0x00012462
		[Event(43, Keywords = (EventKeywords)1L, Message = "Process was called on the TelemetrySink after it was disposed, the telemetry data was dropped.", Level = EventLevel.Error)]
		public void TelemetrySinkCalledAfterBeingDisposed(string appDomainName = "Incorrect")
		{
			base.WriteEvent(43, this.nameProvider.Name);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00014277 File Offset: 0x00012477
		[Event(44, Message = "Operation to stop does not match the current operation. Details {0}.", Level = EventLevel.Warning)]
		public void InvalidOperationToStopDetails(string details, string appDomainName = "Incorrect")
		{
			base.WriteEvent(44, details, this.nameProvider.Name);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0001428D File Offset: 0x0001248D
		[Event(45, Message = "File system containing ApplicationInsights configuration file is inaccessible.", Level = EventLevel.Warning)]
		public void ApplicationInsightsConfigNotAccessibleWarning(string appDomainName = "Incorrect")
		{
			base.WriteEvent(45, this.nameProvider.Name);
		}

		// Token: 0x040001CF RID: 463
		public static readonly CoreEventSource Log = new CoreEventSource();

		// Token: 0x040001D0 RID: 464
		private readonly ApplicationNameProvider nameProvider = new ApplicationNameProvider();

		// Token: 0x02000118 RID: 280
		public sealed class Keywords
		{
			// Token: 0x04000400 RID: 1024
			public const EventKeywords UserActionable = (EventKeywords)1L;

			// Token: 0x04000401 RID: 1025
			public const EventKeywords Diagnostics = (EventKeywords)2L;

			// Token: 0x04000402 RID: 1026
			public const EventKeywords VerboseFailure = (EventKeywords)4L;

			// Token: 0x04000403 RID: 1027
			public const EventKeywords ErrorFailure = (EventKeywords)8L;
		}
	}
}
