using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.Json
{
	// Token: 0x020001EA RID: 490
	internal static class JsonWriterExtensions
	{
		// Token: 0x06001352 RID: 4946 RVA: 0x00037ABC File Offset: 0x00035CBC
		internal static void WriteJsonObjectValue(this IJsonWriter jsonWriter, IDictionary<string, object> jsonObjectValue, Action<IJsonWriter> injectPropertyAction)
		{
			jsonWriter.StartObjectScope();
			if (injectPropertyAction != null)
			{
				injectPropertyAction.Invoke(jsonWriter);
			}
			foreach (KeyValuePair<string, object> keyValuePair in jsonObjectValue)
			{
				jsonWriter.WriteName(keyValuePair.Key);
				jsonWriter.WriteJsonValue(keyValuePair.Value);
			}
			jsonWriter.EndObjectScope();
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x00037B30 File Offset: 0x00035D30
		internal static void WritePrimitiveValue(this IJsonWriter jsonWriter, object value)
		{
			if (value is bool)
			{
				jsonWriter.WriteValue((bool)value);
				return;
			}
			if (value is byte)
			{
				jsonWriter.WriteValue((byte)value);
				return;
			}
			if (value is decimal)
			{
				jsonWriter.WriteValue((decimal)value);
				return;
			}
			if (value is double)
			{
				jsonWriter.WriteValue((double)value);
				return;
			}
			if (value is short)
			{
				jsonWriter.WriteValue((short)value);
				return;
			}
			if (value is int)
			{
				jsonWriter.WriteValue((int)value);
				return;
			}
			if (value is long)
			{
				jsonWriter.WriteValue((long)value);
				return;
			}
			if (value is sbyte)
			{
				jsonWriter.WriteValue((sbyte)value);
				return;
			}
			if (value is float)
			{
				jsonWriter.WriteValue((float)value);
				return;
			}
			string text = value as string;
			if (text != null)
			{
				jsonWriter.WriteValue(text);
				return;
			}
			byte[] array = value as byte[];
			if (array != null)
			{
				jsonWriter.WriteValue(array);
				return;
			}
			if (value is DateTimeOffset)
			{
				jsonWriter.WriteValue((DateTimeOffset)value);
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
			if (value is Date)
			{
				jsonWriter.WriteValue((Date)value);
				return;
			}
			if (value is TimeOfDay)
			{
				jsonWriter.WriteValue((TimeOfDay)value);
				return;
			}
			throw new ODataException(Strings.ODataJsonWriter_UnsupportedValueType(value.GetType().FullName));
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x00037C9C File Offset: 0x00035E9C
		private static void WriteJsonArrayValue(this IJsonWriter jsonWriter, IEnumerable arrayValue)
		{
			jsonWriter.StartArrayScope();
			foreach (object obj in arrayValue)
			{
				jsonWriter.WriteJsonValue(obj);
			}
			jsonWriter.EndArrayScope();
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x00037CF8 File Offset: 0x00035EF8
		private static void WriteJsonValue(this IJsonWriter jsonWriter, object propertyValue)
		{
			if (propertyValue == null)
			{
				jsonWriter.WriteValue(null);
				return;
			}
			if (EdmLibraryExtensions.IsPrimitiveType(propertyValue.GetType()))
			{
				jsonWriter.WritePrimitiveValue(propertyValue);
				return;
			}
			IDictionary<string, object> dictionary = propertyValue as IDictionary<string, object>;
			if (dictionary != null)
			{
				jsonWriter.WriteJsonObjectValue(dictionary, null);
				return;
			}
			IEnumerable enumerable = propertyValue as IEnumerable;
			jsonWriter.WriteJsonArrayValue(enumerable);
		}
	}
}
