using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Json
{
	// Token: 0x020001C2 RID: 450
	internal static class JsonWriterExtensions
	{
		// Token: 0x06000D34 RID: 3380 RVA: 0x0002ED54 File Offset: 0x0002CF54
		internal static void WriteJsonObjectValue(this IJsonWriter jsonWriter, IDictionary<string, object> jsonObjectValue, Action<IJsonWriter> injectPropertyAction, ODataVersion odataVersion)
		{
			jsonWriter.StartObjectScope();
			if (injectPropertyAction != null)
			{
				injectPropertyAction.Invoke(jsonWriter);
			}
			foreach (KeyValuePair<string, object> keyValuePair in jsonObjectValue)
			{
				jsonWriter.WriteName(keyValuePair.Key);
				jsonWriter.WriteJsonValue(keyValuePair.Value, odataVersion);
			}
			jsonWriter.EndObjectScope();
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x0002EDC8 File Offset: 0x0002CFC8
		internal static void WritePrimitiveValue(this IJsonWriter jsonWriter, object value, ODataVersion odataVersion)
		{
			switch (PlatformHelper.GetTypeCode(value.GetType()))
			{
			case 3:
				jsonWriter.WriteValue((bool)value);
				return;
			case 5:
				jsonWriter.WriteValue((sbyte)value);
				return;
			case 6:
				jsonWriter.WriteValue((byte)value);
				return;
			case 7:
				jsonWriter.WriteValue((short)value);
				return;
			case 9:
				jsonWriter.WriteValue((int)value);
				return;
			case 11:
				jsonWriter.WriteValue((long)value);
				return;
			case 13:
				jsonWriter.WriteValue((float)value);
				return;
			case 14:
				jsonWriter.WriteValue((double)value);
				return;
			case 15:
				jsonWriter.WriteValue((decimal)value);
				return;
			case 16:
				jsonWriter.WriteValue((DateTime)value, odataVersion);
				return;
			case 18:
				jsonWriter.WriteValue((string)value);
				return;
			}
			byte[] array = value as byte[];
			if (array != null)
			{
				jsonWriter.WriteValue(Convert.ToBase64String(array));
				return;
			}
			if (value is DateTimeOffset)
			{
				jsonWriter.WriteValue((DateTimeOffset)value, odataVersion);
				return;
			}
			if (value is Guid)
			{
				jsonWriter.WriteValue((Guid)value);
				return;
			}
			if (value is TimeSpan)
			{
				jsonWriter.WriteValue((TimeSpan)value);
				return;
			}
			throw new ODataException(Strings.ODataJsonWriter_UnsupportedValueType(value.GetType().FullName));
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0002EF2C File Offset: 0x0002D12C
		private static void WriteJsonArrayValue(this IJsonWriter jsonWriter, IEnumerable arrayValue, ODataVersion odataVersion)
		{
			jsonWriter.StartArrayScope();
			foreach (object obj in arrayValue)
			{
				jsonWriter.WriteJsonValue(obj, odataVersion);
			}
			jsonWriter.EndArrayScope();
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x0002EF88 File Offset: 0x0002D188
		private static void WriteJsonValue(this IJsonWriter jsonWriter, object propertyValue, ODataVersion odataVersion)
		{
			if (propertyValue == null)
			{
				jsonWriter.WriteValue(null);
				return;
			}
			if (EdmLibraryExtensions.IsPrimitiveType(propertyValue.GetType()))
			{
				jsonWriter.WritePrimitiveValue(propertyValue, odataVersion);
				return;
			}
			IDictionary<string, object> dictionary = propertyValue as IDictionary<string, object>;
			if (dictionary != null)
			{
				jsonWriter.WriteJsonObjectValue(dictionary, null, odataVersion);
				return;
			}
			IEnumerable enumerable = propertyValue as IEnumerable;
			jsonWriter.WriteJsonArrayValue(enumerable, odataVersion);
		}
	}
}
