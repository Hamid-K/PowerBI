using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Text;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility.Implementation.External;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x02000071 RID: 113
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class JsonSerializer
	{
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000FA5C File Offset: 0x0000DC5C
		public static string CompressionType
		{
			get
			{
				return "gzip";
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000FA63 File Offset: 0x0000DC63
		public static string ContentType
		{
			get
			{
				return "application/x-json-stream";
			}
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000FA6C File Offset: 0x0000DC6C
		[SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "Disposing a MemoryStream multiple times is harmless.")]
		public static byte[] Serialize(IEnumerable<ITelemetry> telemetryItems, bool compress = true)
		{
			MemoryStream memoryStream = new MemoryStream();
			using (Stream stream = (compress ? JsonSerializer.CreateCompressedStream(memoryStream) : memoryStream))
			{
				using (StreamWriter streamWriter = new StreamWriter(stream, JsonSerializer.TransmissionEncoding))
				{
					JsonSerializer.SerializeToStream(telemetryItems, streamWriter);
				}
			}
			return memoryStream.ToArray();
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000FAD8 File Offset: 0x0000DCD8
		[SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "Disposing a MemoryStream multiple times is harmless.")]
		public static byte[] ConvertToByteArray(string telemetryItems, bool compress = true)
		{
			if (string.IsNullOrEmpty(telemetryItems))
			{
				throw new ArgumentNullException("telemetryItems");
			}
			MemoryStream memoryStream = new MemoryStream();
			using (Stream stream = (compress ? JsonSerializer.CreateCompressedStream(memoryStream) : memoryStream))
			{
				using (StreamWriter streamWriter = new StreamWriter(stream, JsonSerializer.TransmissionEncoding))
				{
					streamWriter.Write(telemetryItems);
				}
			}
			return memoryStream.ToArray();
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000FB58 File Offset: 0x0000DD58
		public static string Deserialize(byte[] telemetryItemsData, bool compress = true)
		{
			MemoryStream memoryStream = new MemoryStream(telemetryItemsData);
			string @string;
			using (Stream stream = (compress ? new GZipStream(memoryStream, CompressionMode.Decompress) : memoryStream))
			{
				using (MemoryStream memoryStream2 = new MemoryStream())
				{
					stream.CopyTo(memoryStream2);
					byte[] array = memoryStream2.ToArray();
					@string = Encoding.UTF8.GetString(array, 0, array.Length);
				}
			}
			return @string;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000FBD4 File Offset: 0x0000DDD4
		[SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "Disposing a MemoryStream multiple times is harmless.")]
		internal static byte[] Serialize(ITelemetry telemetryItem, bool compress = true)
		{
			return JsonSerializer.Serialize(new ITelemetry[] { telemetryItem }, compress);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000FBE8 File Offset: 0x0000DDE8
		internal static string SerializeAsString(IEnumerable<ITelemetry> telemetryItems)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text;
			using (StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture))
			{
				JsonSerializer.SerializeToStream(telemetryItems, stringWriter);
				text = stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000FC34 File Offset: 0x0000DE34
		internal static string SerializeAsString(ITelemetry telemetry)
		{
			return JsonSerializer.SerializeAsString(new ITelemetry[] { telemetry });
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000FC45 File Offset: 0x0000DE45
		private static Stream CreateCompressedStream(Stream stream)
		{
			return new GZipStream(stream, CompressionMode.Compress);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000FC50 File Offset: 0x0000DE50
		private static void SerializeTelemetryItem(ITelemetry telemetryItem, JsonSerializationWriter jsonSerializationWriter)
		{
			jsonSerializationWriter.WriteStartObject();
			IAiSerializableTelemetry aiSerializableTelemetry;
			if ((aiSerializableTelemetry = telemetryItem as IAiSerializableTelemetry) != null)
			{
				telemetryItem.CopyGlobalPropertiesIfExist();
				telemetryItem.FlattenIExtensionIfExists();
				string telemetryName = aiSerializableTelemetry.TelemetryName;
				JsonSerializer.SerializeHelper(telemetryItem, jsonSerializationWriter, aiSerializableTelemetry.BaseType, telemetryName);
			}
			else
			{
				JsonSerializer.SerializeUnknownTelemetryHelper(telemetryItem, jsonSerializationWriter);
			}
			jsonSerializationWriter.WriteEndObject();
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000FCA0 File Offset: 0x0000DEA0
		private static void SerializeHelper(ITelemetry telemetryItem, JsonSerializationWriter jsonSerializationWriter, string baseType, string telemetryName)
		{
			jsonSerializationWriter.WriteProperty("name", telemetryItem.WriteTelemetryName(telemetryName));
			telemetryItem.WriteEnvelopeProperties(jsonSerializationWriter);
			jsonSerializationWriter.WriteStartObject("data");
			jsonSerializationWriter.WriteProperty("baseType", baseType);
			jsonSerializationWriter.WriteStartObject("baseData");
			telemetryItem.SerializeData(jsonSerializationWriter);
			jsonSerializationWriter.WriteEndObject();
			jsonSerializationWriter.WriteEndObject();
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000FCFC File Offset: 0x0000DEFC
		private static void SerializeUnknownTelemetryHelper(ITelemetry telemetryItem, JsonSerializationWriter jsonSerializationWriter)
		{
			DictionarySerializationWriter dictionarySerializationWriter = new DictionarySerializationWriter();
			telemetryItem.SerializeData(dictionarySerializationWriter);
			telemetryItem.CopyGlobalPropertiesIfExist(dictionarySerializationWriter.AccumulatedDictionary);
			if (telemetryItem.Extension != null)
			{
				DictionarySerializationWriter dictionarySerializationWriter2 = new DictionarySerializationWriter();
				telemetryItem.Extension.Serialize(dictionarySerializationWriter2);
				Utils.CopyDictionary<string>(dictionarySerializationWriter2.AccumulatedDictionary, dictionarySerializationWriter.AccumulatedDictionary);
				Utils.CopyDictionary<double>(dictionarySerializationWriter2.AccumulatedMeasurements, dictionarySerializationWriter.AccumulatedMeasurements);
			}
			jsonSerializationWriter.WriteProperty("name", telemetryItem.WriteTelemetryName("Event"));
			telemetryItem.WriteEnvelopeProperties(jsonSerializationWriter);
			jsonSerializationWriter.WriteStartObject("data");
			jsonSerializationWriter.WriteProperty("baseType", typeof(EventData).Name);
			jsonSerializationWriter.WriteStartObject("baseData");
			jsonSerializationWriter.WriteProperty("ver", new int?(2));
			jsonSerializationWriter.WriteProperty("name", "ConvertedTelemetry");
			jsonSerializationWriter.WriteProperty("properties", dictionarySerializationWriter.AccumulatedDictionary);
			jsonSerializationWriter.WriteProperty("measurements", dictionarySerializationWriter.AccumulatedMeasurements);
			jsonSerializationWriter.WriteEndObject();
			jsonSerializationWriter.WriteEndObject();
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000FDFC File Offset: 0x0000DFFC
		private static void SerializeToStream(IEnumerable<ITelemetry> telemetryItems, TextWriter streamWriter)
		{
			JsonSerializationWriter jsonSerializationWriter = new JsonSerializationWriter(streamWriter);
			int num = 0;
			foreach (ITelemetry telemetry in telemetryItems)
			{
				if (num++ > 0)
				{
					streamWriter.Write(Environment.NewLine);
				}
				telemetry.Context.SanitizeGlobalProperties();
				telemetry.Sanitize();
				JsonSerializer.SerializeTelemetryItem(telemetry, jsonSerializationWriter);
			}
		}

		// Token: 0x0400016D RID: 365
		private static readonly UTF8Encoding TransmissionEncoding = new UTF8Encoding(false);
	}
}
