using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;

namespace Microsoft.OData.Json
{
	// Token: 0x020001E6 RID: 486
	internal static class JsonReaderExtensions
	{
		// Token: 0x06001307 RID: 4871 RVA: 0x00036EF0 File Offset: 0x000350F0
		internal static void ReadStartObject(this IJsonReader jsonReader)
		{
			jsonReader.ReadNext(JsonNodeType.StartObject);
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x00036EF9 File Offset: 0x000350F9
		internal static void ReadEndObject(this IJsonReader jsonReader)
		{
			jsonReader.ReadNext(JsonNodeType.EndObject);
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x00036F02 File Offset: 0x00035102
		internal static void ReadStartArray(this IJsonReader jsonReader)
		{
			jsonReader.ReadNext(JsonNodeType.StartArray);
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x00036F0B File Offset: 0x0003510B
		internal static void ReadEndArray(this IJsonReader jsonReader)
		{
			jsonReader.ReadNext(JsonNodeType.EndArray);
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x00036F14 File Offset: 0x00035114
		internal static string GetPropertyName(this IJsonReader jsonReader)
		{
			return (string)jsonReader.Value;
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x00036F30 File Offset: 0x00035130
		internal static string ReadPropertyName(this IJsonReader jsonReader)
		{
			jsonReader.ValidateNodeType(JsonNodeType.Property);
			string propertyName = jsonReader.GetPropertyName();
			jsonReader.ReadNext();
			return propertyName;
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x00036F54 File Offset: 0x00035154
		internal static object ReadPrimitiveValue(this IJsonReader jsonReader)
		{
			object value = jsonReader.Value;
			jsonReader.ReadNext(JsonNodeType.PrimitiveValue);
			return value;
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x00036F70 File Offset: 0x00035170
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

		// Token: 0x0600130F RID: 4879 RVA: 0x00036FA0 File Offset: 0x000351A0
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

		// Token: 0x06001310 RID: 4880 RVA: 0x00036FD0 File Offset: 0x000351D0
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

		// Token: 0x06001311 RID: 4881 RVA: 0x00037054 File Offset: 0x00035254
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

		// Token: 0x06001312 RID: 4882 RVA: 0x000370AC File Offset: 0x000352AC
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

		// Token: 0x06001313 RID: 4883 RVA: 0x00037194 File Offset: 0x00035394
		internal static ODataValue ReadAsUntypedOrNullValue(this IJsonReader jsonReader)
		{
			StringBuilder stringBuilder = new StringBuilder();
			jsonReader.SkipValue(stringBuilder);
			return new ODataUntypedValue
			{
				RawValue = stringBuilder.ToString()
			};
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x000371BF File Offset: 0x000353BF
		internal static JsonNodeType ReadNext(this IJsonReader jsonReader)
		{
			jsonReader.Read();
			return jsonReader.NodeType;
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x000371D0 File Offset: 0x000353D0
		internal static bool IsOnValueNode(this IJsonReader jsonReader)
		{
			JsonNodeType nodeType = jsonReader.NodeType;
			return nodeType == JsonNodeType.PrimitiveValue || nodeType == JsonNodeType.StartObject || nodeType == JsonNodeType.StartArray;
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x0000250D File Offset: 0x0000070D
		[Conditional("DEBUG")]
		internal static void AssertNotBuffering(this BufferingJsonReader bufferedJsonReader)
		{
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x0000250D File Offset: 0x0000070D
		[Conditional("DEBUG")]
		internal static void AssertBuffering(this BufferingJsonReader bufferedJsonReader)
		{
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x0001F734 File Offset: 0x0001D934
		internal static ODataException CreateException(string exceptionMessage)
		{
			return new ODataException(exceptionMessage);
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x000371F2 File Offset: 0x000353F2
		private static void ReadNext(this IJsonReader jsonReader, JsonNodeType expectedNodeType)
		{
			jsonReader.ValidateNodeType(expectedNodeType);
			jsonReader.Read();
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x00037202 File Offset: 0x00035402
		private static void ValidateNodeType(this IJsonReader jsonReader, JsonNodeType expectedNodeType)
		{
			if (jsonReader.NodeType != expectedNodeType)
			{
				throw JsonReaderExtensions.CreateException(Strings.JsonReaderExtensions_UnexpectedNodeDetected(expectedNodeType, jsonReader.NodeType));
			}
		}
	}
}
