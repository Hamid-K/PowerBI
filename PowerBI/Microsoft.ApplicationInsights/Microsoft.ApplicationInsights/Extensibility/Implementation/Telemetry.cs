using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility.Implementation.External;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x02000080 RID: 128
	internal static class Telemetry
	{
		// Token: 0x06000413 RID: 1043 RVA: 0x00011F90 File Offset: 0x00010190
		public static void WriteEnvelopeProperties(this ITelemetry telemetry, ISerializationWriter json)
		{
			json.WriteProperty("time", telemetry.Timestamp.UtcDateTime.ToString("o", CultureInfo.InvariantCulture));
			ISupportSampling supportSampling = telemetry as ISupportSampling;
			if (supportSampling != null && supportSampling.SamplingPercentage != null && supportSampling.SamplingPercentage.Value > 1E-12 && supportSampling.SamplingPercentage.Value < 99.999999999999)
			{
				json.WriteProperty("sampleRate", new double?(supportSampling.SamplingPercentage.Value));
			}
			json.WriteProperty("seq", telemetry.Sequence);
			Telemetry.WriteTelemetryContext(json, telemetry.Context);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00012050 File Offset: 0x00010250
		public static string WriteTelemetryName(this ITelemetry telemetry, string telemetryName)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", new object[]
			{
				telemetry.IsDeveloperMode() ? "Microsoft.ApplicationInsights.Dev." : "Microsoft.ApplicationInsights.",
				Telemetry.NormalizeInstrumentationKey(telemetry.Context.InstrumentationKey),
				telemetryName
			});
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x000120A0 File Offset: 0x000102A0
		public static void WriteTelemetryContext(ISerializationWriter json, TelemetryContext context)
		{
			if (context != null)
			{
				json.WriteProperty("iKey", context.InstrumentationKey);
				if (context.Flags != 0L)
				{
					json.WriteProperty("flags", new double?((double)context.Flags));
				}
				json.WriteProperty("tags", context.SanitizedTags);
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000120F4 File Offset: 0x000102F4
		internal static void CopyGlobalPropertiesIfExist(this ITelemetry telemetry)
		{
			ISupportProperties supportProperties;
			if (telemetry.Context.GlobalPropertiesValue != null && (supportProperties = telemetry as ISupportProperties) != null)
			{
				Utils.CopyDictionary<string>(telemetry.Context.GlobalProperties, supportProperties.Properties);
			}
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0001212E File Offset: 0x0001032E
		internal static void CopyGlobalPropertiesIfExist(this ITelemetry telemetry, IDictionary<string, string> target)
		{
			if (telemetry.Context.GlobalPropertiesValue != null)
			{
				Utils.CopyDictionary<string>(telemetry.Context.GlobalProperties, target);
			}
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00012150 File Offset: 0x00010350
		internal static void FlattenIExtensionIfExists(this ITelemetry telemetry)
		{
			if (telemetry.Extension != null)
			{
				ISupportProperties supportProperties = telemetry as ISupportProperties;
				ISupportMetrics supportMetrics = telemetry as ISupportMetrics;
				if (supportProperties != null || supportMetrics != null)
				{
					DictionarySerializationWriter dictionarySerializationWriter = new DictionarySerializationWriter();
					telemetry.Extension.Serialize(dictionarySerializationWriter);
					if (supportProperties != null)
					{
						Utils.CopyDictionary<string>(dictionarySerializationWriter.AccumulatedDictionary, supportProperties.Properties);
					}
					if (supportMetrics != null)
					{
						Utils.CopyDictionary<double>(dictionarySerializationWriter.AccumulatedMeasurements, supportMetrics.Metrics);
					}
				}
			}
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x000121B4 File Offset: 0x000103B4
		internal static EventData FlattenTelemetryIntoEventData(this ITelemetry telemetry)
		{
			EventData eventData = new EventData();
			DictionarySerializationWriter dictionarySerializationWriter = new DictionarySerializationWriter();
			telemetry.SerializeData(dictionarySerializationWriter);
			Utils.CopyDictionary<string>(dictionarySerializationWriter.AccumulatedDictionary, eventData.properties);
			Utils.CopyDictionary<double>(dictionarySerializationWriter.AccumulatedMeasurements, eventData.measurements);
			if (telemetry.Context.GlobalPropertiesValue != null)
			{
				Utils.CopyDictionary<string>(telemetry.Context.GlobalProperties, eventData.properties);
			}
			if (telemetry.Extension != null)
			{
				DictionarySerializationWriter dictionarySerializationWriter2 = new DictionarySerializationWriter();
				telemetry.Extension.Serialize(dictionarySerializationWriter2);
				Utils.CopyDictionary<string>(dictionarySerializationWriter2.AccumulatedDictionary, eventData.properties);
				Utils.CopyDictionary<double>(dictionarySerializationWriter2.AccumulatedMeasurements, eventData.measurements);
			}
			return eventData;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00012258 File Offset: 0x00010458
		private static bool IsDeveloperMode(this ITelemetry telemetry)
		{
			ISupportProperties supportProperties;
			string text;
			bool flag;
			return (supportProperties = telemetry as ISupportProperties) != null && supportProperties != null && supportProperties.Properties.TryGetValue("DeveloperMode", out text) && bool.TryParse(text, out flag) && flag;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00012293 File Offset: 0x00010493
		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "Implementation expects lower case")]
		private static string NormalizeInstrumentationKey(string instrumentationKey)
		{
			if (instrumentationKey.IsNullOrWhiteSpace())
			{
				return string.Empty;
			}
			return instrumentationKey.Replace("-", string.Empty).ToLowerInvariant() + ".";
		}
	}
}
