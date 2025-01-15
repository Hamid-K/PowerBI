using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;

namespace Microsoft.OData.Json
{
	// Token: 0x02000218 RID: 536
	internal static class JsonReaderExtensions
	{
		// Token: 0x06001776 RID: 6006 RVA: 0x00042C52 File Offset: 0x00040E52
		internal static void ReadStartObject(this IJsonReader jsonReader)
		{
			jsonReader.ReadNext(JsonNodeType.StartObject);
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x00042C5B File Offset: 0x00040E5B
		internal static void ReadEndObject(this IJsonReader jsonReader)
		{
			jsonReader.ReadNext(JsonNodeType.EndObject);
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x00042C64 File Offset: 0x00040E64
		internal static void ReadStartArray(this IJsonReader jsonReader)
		{
			jsonReader.ReadNext(JsonNodeType.StartArray);
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x00042C6D File Offset: 0x00040E6D
		internal static void ReadEndArray(this IJsonReader jsonReader)
		{
			jsonReader.ReadNext(JsonNodeType.EndArray);
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x00042C78 File Offset: 0x00040E78
		internal static string GetPropertyName(this IJsonReader jsonReader)
		{
			return (string)jsonReader.Value;
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x00042C94 File Offset: 0x00040E94
		internal static string ReadPropertyName(this IJsonReader jsonReader)
		{
			jsonReader.ValidateNodeType(JsonNodeType.Property);
			string propertyName = jsonReader.GetPropertyName();
			jsonReader.ReadNext();
			return propertyName;
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x00042CB8 File Offset: 0x00040EB8
		internal static object ReadPrimitiveValue(this IJsonReader jsonReader)
		{
			object value = jsonReader.Value;
			jsonReader.ReadNext(JsonNodeType.PrimitiveValue);
			return value;
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x00042CD4 File Offset: 0x00040ED4
		internal static string ReadStringValue(this IJsonReader jsonReader)
		{
			object obj = jsonReader.ReadPrimitiveValue();
			string text = obj as string;
			if (obj == null || text != null)
			{
				return text;
			}
			throw JsonReaderExtensions.CreateException(Strings.JsonReaderExtensions_CannotReadValueAsString(obj));
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x00042D02 File Offset: 0x00040F02
		internal static Uri ReadUriValue(this IJsonReader jsonReader)
		{
			return UriUtils.StringToUri(jsonReader.ReadStringValue());
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x00042D10 File Offset: 0x00040F10
		internal static string ReadStringValue(this IJsonReader jsonReader, string propertyName)
		{
			object obj = jsonReader.ReadPrimitiveValue();
			string text = obj as string;
			if (obj == null || text != null)
			{
				return text;
			}
			throw JsonReaderExtensions.CreateException(Strings.JsonReaderExtensions_CannotReadPropertyValueAsString(obj, propertyName));
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x00042D40 File Offset: 0x00040F40
		internal static double? ReadDoubleValue(this IJsonReader jsonReader)
		{
			object obj = jsonReader.ReadPrimitiveValue();
			double? num = obj as double?;
			if (obj == null || num != null)
			{
				return num;
			}
			int? num2 = obj as int?;
			if (num2 != null)
			{
				return new double?((double)num2.Value);
			}
			decimal? num3 = obj as decimal?;
			if (num3 != null)
			{
				return new double?((double)num3.Value);
			}
			throw JsonReaderExtensions.CreateException(Strings.JsonReaderExtensions_CannotReadValueAsDouble(obj));
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x00042DC4 File Offset: 0x00040FC4
		internal static void SkipValue(this IJsonReader jsonReader)
		{
			int num = 0;
			do
			{
				switch (jsonReader.NodeType)
				{
				case JsonNodeType.StartObject:
				case JsonNodeType.StartArray:
					num++;
					break;
				case JsonNodeType.EndObject:
				case JsonNodeType.EndArray:
					num--;
					break;
				}
			}
			while (jsonReader.Read() && num > 0);
			if (num > 0)
			{
				throw JsonReaderExtensions.CreateException(Strings.JsonReader_EndOfInputWithOpenScope);
			}
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x00042E1C File Offset: 0x0004101C
		internal static void SkipValue(this IJsonReader jsonReader, StringBuilder jsonRawValueStringBuilder)
		{
			using (StringWriter stringWriter = new StringWriter(jsonRawValueStringBuilder, CultureInfo.InvariantCulture))
			{
				JsonWriter jsonWriter = new JsonWriter(stringWriter, false);
				int num = 0;
				do
				{
					switch (jsonReader.NodeType)
					{
					case JsonNodeType.StartObject:
						jsonWriter.StartObjectScope();
						num++;
						break;
					case JsonNodeType.EndObject:
						jsonWriter.EndObjectScope();
						num--;
						break;
					case JsonNodeType.StartArray:
						jsonWriter.StartArrayScope();
						num++;
						break;
					case JsonNodeType.EndArray:
						jsonWriter.EndArrayScope();
						num--;
						break;
					case JsonNodeType.Property:
						jsonWriter.WriteName(jsonReader.GetPropertyName());
						break;
					case JsonNodeType.PrimitiveValue:
						if (jsonReader.Value == null)
						{
							jsonWriter.WriteValue(null);
						}
						else
						{
							jsonWriter.WritePrimitiveValue(jsonReader.Value);
						}
						break;
					}
				}
				while (jsonReader.Read() && num > 0);
				if (num > 0)
				{
					throw JsonReaderExtensions.CreateException(Strings.JsonReader_EndOfInputWithOpenScope);
				}
				jsonWriter.Flush();
			}
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x00042F04 File Offset: 0x00041104
		internal static ODataValue ReadAsUntypedOrNullValue(this IJsonReader jsonReader)
		{
			StringBuilder stringBuilder = new StringBuilder();
			jsonReader.SkipValue(stringBuilder);
			return new ODataUntypedValue
			{
				RawValue = stringBuilder.ToString()
			};
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x00042F30 File Offset: 0x00041130
		internal static ODataValue ReadODataValue(this IJsonReader jsonReader)
		{
			if (jsonReader.NodeType == JsonNodeType.PrimitiveValue)
			{
				object obj = jsonReader.ReadPrimitiveValue();
				return obj.ToODataValue();
			}
			if (jsonReader.NodeType == JsonNodeType.StartObject)
			{
				jsonReader.ReadStartObject();
				ODataResourceValue odataResourceValue = new ODataResourceValue();
				IList<ODataProperty> list = new List<ODataProperty>();
				while (jsonReader.NodeType != JsonNodeType.EndObject)
				{
					list.Add(new ODataProperty
					{
						Name = jsonReader.ReadPropertyName(),
						Value = jsonReader.ReadODataValue()
					});
				}
				odataResourceValue.Properties = list;
				jsonReader.ReadEndObject();
				return odataResourceValue;
			}
			if (jsonReader.NodeType == JsonNodeType.StartArray)
			{
				jsonReader.ReadStartArray();
				ODataCollectionValue odataCollectionValue = new ODataCollectionValue();
				IList<object> list2 = new List<object>();
				while (jsonReader.NodeType != JsonNodeType.EndArray)
				{
					list2.Add(jsonReader.ReadODataValue());
				}
				odataCollectionValue.Items = list2;
				jsonReader.ReadEndArray();
				return odataCollectionValue;
			}
			return jsonReader.ReadAsUntypedOrNullValue();
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x00042FFB File Offset: 0x000411FB
		internal static JsonNodeType ReadNext(this IJsonReader jsonReader)
		{
			jsonReader.Read();
			return jsonReader.NodeType;
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x0004300C File Offset: 0x0004120C
		internal static bool IsOnValueNode(this IJsonReader jsonReader)
		{
			JsonNodeType nodeType = jsonReader.NodeType;
			return nodeType == JsonNodeType.PrimitiveValue || nodeType == JsonNodeType.StartObject || nodeType == JsonNodeType.StartArray;
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x0000239D File Offset: 0x0000059D
		[Conditional("DEBUG")]
		internal static void AssertNotBuffering(this BufferingJsonReader bufferedJsonReader)
		{
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x0000239D File Offset: 0x0000059D
		[Conditional("DEBUG")]
		internal static void AssertBuffering(this BufferingJsonReader bufferedJsonReader)
		{
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x0002CC1C File Offset: 0x0002AE1C
		internal static ODataException CreateException(string exceptionMessage)
		{
			return new ODataException(exceptionMessage);
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x0004302E File Offset: 0x0004122E
		private static void ReadNext(this IJsonReader jsonReader, JsonNodeType expectedNodeType)
		{
			jsonReader.ValidateNodeType(expectedNodeType);
			jsonReader.Read();
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x0004303E File Offset: 0x0004123E
		private static void ValidateNodeType(this IJsonReader jsonReader, JsonNodeType expectedNodeType)
		{
			if (jsonReader.NodeType != expectedNodeType)
			{
				throw JsonReaderExtensions.CreateException(Strings.JsonReaderExtensions_UnexpectedNodeDetected(expectedNodeType, jsonReader.NodeType));
			}
		}
	}
}
