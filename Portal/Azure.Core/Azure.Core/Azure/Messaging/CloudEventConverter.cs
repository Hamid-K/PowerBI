using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Azure.Messaging
{
	// Token: 0x02000037 RID: 55
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1 })]
	internal class CloudEventConverter : JsonConverter<CloudEvent>
	{
		// Token: 0x06000136 RID: 310 RVA: 0x000042F0 File Offset: 0x000024F0
		public override CloudEvent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			CloudEvent cloudEvent;
			using (JsonDocument jsonDocument = JsonDocument.ParseValue(ref reader))
			{
				cloudEvent = CloudEventConverter.DeserializeCloudEvent(jsonDocument.RootElement, false);
			}
			return cloudEvent;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004330 File Offset: 0x00002530
		internal static CloudEvent DeserializeCloudEvent(JsonElement element, bool skipValidation)
		{
			CloudEvent cloudEvent = new CloudEvent();
			foreach (JsonProperty jsonProperty in element.EnumerateObject())
			{
				if (jsonProperty.NameEquals("id"))
				{
					JsonElement jsonElement = jsonProperty.Value;
					if (jsonElement.ValueKind != 7)
					{
						CloudEvent cloudEvent2 = cloudEvent;
						jsonElement = jsonProperty.Value;
						cloudEvent2.Id = jsonElement.GetString();
					}
				}
				else if (jsonProperty.NameEquals("source"))
				{
					JsonElement jsonElement = jsonProperty.Value;
					if (jsonElement.ValueKind != 7)
					{
						CloudEvent cloudEvent3 = cloudEvent;
						jsonElement = jsonProperty.Value;
						cloudEvent3.Source = jsonElement.GetString();
					}
				}
				else if (jsonProperty.NameEquals("data"))
				{
					cloudEvent.Data = new BinaryData(jsonProperty.Value, null, null);
					cloudEvent.DataFormat = CloudEventDataFormat.Json;
				}
				else if (jsonProperty.NameEquals("data_base64"))
				{
					JsonElement jsonElement = jsonProperty.Value;
					if (jsonElement.ValueKind != 7)
					{
						CloudEvent cloudEvent4 = cloudEvent;
						jsonElement = jsonProperty.Value;
						cloudEvent4.Data = BinaryData.FromBytes(jsonElement.GetBytesFromBase64());
						cloudEvent.DataFormat = CloudEventDataFormat.Binary;
					}
				}
				else if (jsonProperty.NameEquals("type"))
				{
					JsonElement jsonElement = jsonProperty.Value;
					if (jsonElement.ValueKind != 7)
					{
						CloudEvent cloudEvent5 = cloudEvent;
						jsonElement = jsonProperty.Value;
						cloudEvent5.Type = jsonElement.GetString();
					}
				}
				else if (jsonProperty.NameEquals("time"))
				{
					JsonElement jsonElement = jsonProperty.Value;
					if (jsonElement.ValueKind != 7)
					{
						CloudEvent cloudEvent6 = cloudEvent;
						jsonElement = jsonProperty.Value;
						cloudEvent6.Time = new DateTimeOffset?(jsonElement.GetDateTimeOffset());
					}
				}
				else if (jsonProperty.NameEquals("specversion"))
				{
					CloudEvent cloudEvent7 = cloudEvent;
					JsonElement jsonElement = jsonProperty.Value;
					cloudEvent7.SpecVersion = jsonElement.GetString();
				}
				else if (jsonProperty.NameEquals("dataschema"))
				{
					CloudEvent cloudEvent8 = cloudEvent;
					JsonElement jsonElement = jsonProperty.Value;
					cloudEvent8.DataSchema = jsonElement.GetString();
				}
				else if (jsonProperty.NameEquals("datacontenttype"))
				{
					CloudEvent cloudEvent9 = cloudEvent;
					JsonElement jsonElement = jsonProperty.Value;
					cloudEvent9.DataContentType = jsonElement.GetString();
				}
				else if (jsonProperty.NameEquals("subject"))
				{
					CloudEvent cloudEvent10 = cloudEvent;
					JsonElement jsonElement = jsonProperty.Value;
					cloudEvent10.Subject = jsonElement.GetString();
				}
				else if (!skipValidation)
				{
					IDictionary<string, object> extensionAttributes = cloudEvent.ExtensionAttributes;
					string name = jsonProperty.Name;
					JsonElement jsonElement = jsonProperty.Value;
					extensionAttributes.Add(name, CloudEventConverter.GetObject(in jsonElement));
				}
				else
				{
					CloudEventExtensionAttributes<string, object> cloudEventExtensionAttributes = (CloudEventExtensionAttributes<string, object>)cloudEvent.ExtensionAttributes;
					string name2 = jsonProperty.Name;
					JsonElement jsonElement = jsonProperty.Value;
					cloudEventExtensionAttributes.AddWithoutValidation(name2, CloudEventConverter.GetObject(in jsonElement));
				}
			}
			if (!skipValidation)
			{
				if (cloudEvent.Source == null)
				{
					throw new ArgumentException("The source property must be specified in each CloudEvent. " + Environment.NewLine + "The `skipValidation` parameter can be set to 'true' in the CloudEvent.Parse or CloudEvent.ParseEvents method to skip this validation.");
				}
				if (cloudEvent.Type == null)
				{
					throw new ArgumentException("The type property must be specified in each CloudEvent. " + Environment.NewLine + "The `skipValidation` parameter can be set to 'true' in the CloudEvent.Parse or CloudEvent.ParseEvents method to skip this validation.");
				}
				if (cloudEvent.Id == null)
				{
					throw new ArgumentException("The Id property must be specified in each CloudEvent. " + Environment.NewLine + "The `skipValidation` parameter can be set to 'true' in the CloudEvent.Parse or CloudEvent.ParseEvents method to skip this validation.");
				}
				if (cloudEvent.SpecVersion != "1.0")
				{
					if (cloudEvent.SpecVersion == null)
					{
						throw new ArgumentException(string.Concat(new string[]
						{
							"The specverion was not set in at least one of the events in the payload. This type only supports specversion '1.0', which must be set for each event. ",
							Environment.NewLine,
							"The `skipValidation` parameter can be set to 'true' in the CloudEvent.Parse or CloudEvent.ParseEvents method to skip this validation.",
							Environment.NewLine,
							element.ToString()
						}), "element");
					}
					throw new ArgumentException(string.Concat(new string[]
					{
						"The specverion value of '",
						cloudEvent.SpecVersion,
						"' is not supported by CloudEvent. This type only supports specversion '1.0'. ",
						Environment.NewLine,
						"The `skipValidation` parameter can be set to 'true' in the CloudEvent.Parse or CloudEvent.ParseEvents method to skip this validation.",
						Environment.NewLine,
						element.ToString()
					}), "element");
				}
			}
			return cloudEvent;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000472C File Offset: 0x0000292C
		public override void Write(Utf8JsonWriter writer, CloudEvent value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("id");
			writer.WriteStringValue(value.Id);
			writer.WritePropertyName("source");
			writer.WriteStringValue(value.Source);
			writer.WritePropertyName("type");
			writer.WriteStringValue(value.Type);
			if (value.Data != null)
			{
				CloudEventDataFormat dataFormat = value.DataFormat;
				if (dataFormat != CloudEventDataFormat.Binary)
				{
					if (dataFormat == CloudEventDataFormat.Json)
					{
						using (JsonDocument jsonDocument = JsonDocument.Parse(value.Data.ToMemory(), default(JsonDocumentOptions)))
						{
							writer.WritePropertyName("data");
							jsonDocument.RootElement.WriteTo(writer);
						}
					}
				}
				else
				{
					writer.WritePropertyName("data_base64");
					writer.WriteBase64StringValue(value.Data.ToArray());
				}
			}
			if (value.Time != null)
			{
				writer.WritePropertyName("time");
				writer.WriteStringValue(value.Time.Value);
			}
			writer.WritePropertyName("specversion");
			writer.WriteStringValue(value.SpecVersion);
			if (value.DataSchema != null)
			{
				writer.WritePropertyName("dataschema");
				writer.WriteStringValue(value.DataSchema);
			}
			if (value.DataContentType != null)
			{
				writer.WritePropertyName("datacontenttype");
				writer.WriteStringValue(value.DataContentType);
			}
			if (value.Subject != null)
			{
				writer.WritePropertyName("subject");
				writer.WriteStringValue(value.Subject);
			}
			foreach (KeyValuePair<string, object> keyValuePair in value.ExtensionAttributes)
			{
				writer.WritePropertyName(keyValuePair.Key);
				CloudEventConverter.WriteObjectValue(writer, keyValuePair.Value);
			}
			writer.WriteEndObject();
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004910 File Offset: 0x00002B10
		private static void WriteObjectValue(Utf8JsonWriter writer, [Nullable(2)] object value)
		{
			if (value == null)
			{
				writer.WriteNullValue();
				return;
			}
			byte[] array = value as byte[];
			if (array != null)
			{
				writer.WriteStringValue(Convert.ToBase64String(array));
				return;
			}
			if (value is ReadOnlyMemory<byte>)
			{
				writer.WriteStringValue(Convert.ToBase64String(((ReadOnlyMemory<byte>)value).ToArray()));
				return;
			}
			if (value is int)
			{
				int num = (int)value;
				writer.WriteNumberValue(num);
				return;
			}
			string text = value as string;
			if (text != null)
			{
				writer.WriteStringValue(text);
				return;
			}
			if (value is bool)
			{
				bool flag = (bool)value;
				writer.WriteBooleanValue(flag);
				return;
			}
			if (value is Guid)
			{
				Guid guid = (Guid)value;
				writer.WriteStringValue(guid);
				return;
			}
			Uri uri = value as Uri;
			if (uri != null)
			{
				writer.WriteStringValue(uri.ToString());
				return;
			}
			if (value is DateTimeOffset)
			{
				DateTimeOffset dateTimeOffset = (DateTimeOffset)value;
				writer.WriteStringValue(dateTimeOffset);
				return;
			}
			if (value is DateTime)
			{
				DateTime dateTime = (DateTime)value;
				writer.WriteStringValue(dateTime);
				return;
			}
			IEnumerable<KeyValuePair<string, object>> enumerable = value as IEnumerable<KeyValuePair<string, object>>;
			if (enumerable != null)
			{
				writer.WriteStartObject();
				foreach (KeyValuePair<string, object> keyValuePair in enumerable)
				{
					writer.WritePropertyName(keyValuePair.Key);
					CloudEventConverter.WriteObjectValue(writer, keyValuePair.Value);
				}
				writer.WriteEndObject();
				return;
			}
			IEnumerable<object> enumerable2 = value as IEnumerable<object>;
			if (enumerable2 == null)
			{
				string text2 = "Not supported type ";
				Type type = value.GetType();
				throw new NotSupportedException(text2 + ((type != null) ? type.ToString() : null));
			}
			writer.WriteStartArray();
			foreach (object obj in enumerable2)
			{
				CloudEventConverter.WriteObjectValue(writer, obj);
			}
			writer.WriteEndArray();
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00004B20 File Offset: 0x00002D20
		[NullableContext(2)]
		private static object GetObject(in JsonElement element)
		{
			switch (element.ValueKind)
			{
			case 0:
			case 7:
				return null;
			case 1:
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				foreach (JsonProperty jsonProperty in element.EnumerateObject())
				{
					Dictionary<string, object> dictionary2 = dictionary;
					string name = jsonProperty.Name;
					JsonElement value = jsonProperty.Value;
					dictionary2.Add(name, CloudEventConverter.GetObject(in value));
				}
				return dictionary;
			}
			case 2:
			{
				List<object> list = new List<object>();
				foreach (JsonElement jsonElement in element.EnumerateArray())
				{
					list.Add(CloudEventConverter.GetObject(in jsonElement));
				}
				return list.ToArray();
			}
			case 3:
				return element.GetString();
			case 4:
			{
				int num;
				if (element.TryGetInt32(ref num))
				{
					return num;
				}
				long num2;
				if (element.TryGetInt64(ref num2))
				{
					return num2;
				}
				return element.GetDouble();
			}
			case 5:
				return true;
			case 6:
				return false;
			default:
				throw new NotSupportedException("Not supported value kind " + element.ValueKind.ToString());
			}
		}
	}
}
