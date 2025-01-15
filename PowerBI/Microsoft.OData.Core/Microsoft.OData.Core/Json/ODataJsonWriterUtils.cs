using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.OData.Json
{
	// Token: 0x0200021F RID: 543
	internal static class ODataJsonWriterUtils
	{
		// Token: 0x060017E1 RID: 6113 RVA: 0x00044318 File Offset: 0x00042518
		internal static void WriteError(IJsonWriter jsonWriter, Action<IEnumerable<ODataInstanceAnnotation>> writeInstanceAnnotationsDelegate, ODataError error, bool includeDebugInformation, int maxInnerErrorDepth, bool writingJsonLight)
		{
			string text;
			string text2;
			ErrorUtils.GetErrorDetails(error, out text, out text2);
			ODataInnerError odataInnerError = (includeDebugInformation ? error.InnerError : null);
			ODataJsonWriterUtils.WriteError(jsonWriter, text, text2, error.Target, error.Details, odataInnerError, error.GetInstanceAnnotations(), writeInstanceAnnotationsDelegate, maxInnerErrorDepth, writingJsonLight);
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x0004435C File Offset: 0x0004255C
		internal static void StartJsonPaddingIfRequired(IJsonWriter jsonWriter, ODataMessageWriterSettings settings)
		{
			if (settings.HasJsonPaddingFunction())
			{
				jsonWriter.WritePaddingFunctionName(settings.JsonPCallback);
				jsonWriter.StartPaddingFunctionScope();
			}
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x00044378 File Offset: 0x00042578
		internal static void EndJsonPaddingIfRequired(IJsonWriter jsonWriter, ODataMessageWriterSettings settings)
		{
			if (settings.HasJsonPaddingFunction())
			{
				jsonWriter.EndPaddingFunctionScope();
			}
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x00044388 File Offset: 0x00042588
		private static void WriteError(IJsonWriter jsonWriter, string code, string message, string target, IEnumerable<ODataErrorDetail> details, ODataInnerError innerError, IEnumerable<ODataInstanceAnnotation> instanceAnnotations, Action<IEnumerable<ODataInstanceAnnotation>> writeInstanceAnnotationsDelegate, int maxInnerErrorDepth, bool writingJsonLight)
		{
			jsonWriter.StartObjectScope();
			if (writingJsonLight)
			{
				jsonWriter.WriteName("error");
			}
			else
			{
				jsonWriter.WriteName("error");
			}
			jsonWriter.StartObjectScope();
			jsonWriter.WriteName("code");
			jsonWriter.WriteValue(code);
			jsonWriter.WriteName("message");
			jsonWriter.WriteValue(message);
			if (target != null)
			{
				jsonWriter.WriteName("target");
				jsonWriter.WriteValue(target);
			}
			if (details != null)
			{
				ODataJsonWriterUtils.WriteErrorDetails(jsonWriter, details, "details");
			}
			if (innerError != null)
			{
				ODataJsonWriterUtils.WriteInnerError(jsonWriter, innerError, "innererror", 0, maxInnerErrorDepth);
			}
			if (writingJsonLight)
			{
				writeInstanceAnnotationsDelegate(instanceAnnotations);
			}
			jsonWriter.EndObjectScope();
			jsonWriter.EndObjectScope();
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x00044434 File Offset: 0x00042634
		private static void WriteErrorDetails(IJsonWriter jsonWriter, IEnumerable<ODataErrorDetail> details, string odataErrorDetailsName)
		{
			jsonWriter.WriteName(odataErrorDetailsName);
			jsonWriter.StartArrayScope();
			foreach (ODataErrorDetail odataErrorDetail in details.Where((ODataErrorDetail d) => d != null))
			{
				jsonWriter.StartObjectScope();
				jsonWriter.WriteName("code");
				jsonWriter.WriteValue(odataErrorDetail.ErrorCode ?? string.Empty);
				if (odataErrorDetail.Target != null)
				{
					jsonWriter.WriteName("target");
					jsonWriter.WriteValue(odataErrorDetail.Target);
				}
				jsonWriter.WriteName("message");
				jsonWriter.WriteValue(odataErrorDetail.Message ?? string.Empty);
				jsonWriter.EndObjectScope();
			}
			jsonWriter.EndArrayScope();
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x00044518 File Offset: 0x00042718
		private static void WriteInnerError(IJsonWriter jsonWriter, ODataInnerError innerError, string innerErrorPropertyName, int recursionDepth, int maxInnerErrorDepth)
		{
			ValidationUtils.IncreaseAndValidateRecursionDepth(ref recursionDepth, maxInnerErrorDepth);
			jsonWriter.WriteName(innerErrorPropertyName);
			jsonWriter.StartObjectScope();
			if (innerError.Properties != null)
			{
				foreach (KeyValuePair<string, ODataValue> keyValuePair in innerError.Properties)
				{
					jsonWriter.WriteName(keyValuePair.Key);
					if (keyValuePair.Value is ODataNullValue && (keyValuePair.Key == "message" || keyValuePair.Key == "stacktrace" || keyValuePair.Key == "type"))
					{
						jsonWriter.WriteODataValue(new ODataPrimitiveValue(string.Empty));
					}
					else
					{
						jsonWriter.WriteODataValue(keyValuePair.Value);
					}
				}
			}
			if (innerError.InnerError != null)
			{
				ODataJsonWriterUtils.WriteInnerError(jsonWriter, innerError.InnerError, "internalexception", recursionDepth, maxInnerErrorDepth);
			}
			jsonWriter.EndObjectScope();
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x00044618 File Offset: 0x00042818
		internal static void ODataValueToString(StringBuilder sb, ODataValue value)
		{
			if (value == null || value is ODataNullValue)
			{
				sb.Append("null");
			}
			ODataCollectionValue odataCollectionValue = value as ODataCollectionValue;
			if (odataCollectionValue != null)
			{
				ODataJsonWriterUtils.ODataCollectionValueToString(sb, odataCollectionValue);
			}
			ODataResourceValue odataResourceValue = value as ODataResourceValue;
			if (odataResourceValue != null)
			{
				ODataJsonWriterUtils.ODataResourceValueToString(sb, odataResourceValue);
			}
			ODataPrimitiveValue odataPrimitiveValue = value as ODataPrimitiveValue;
			if (odataPrimitiveValue != null)
			{
				if (odataPrimitiveValue.FromODataValue() is string)
				{
					string text = "\"";
					object obj = value.FromODataValue();
					sb.Append(text + JsonValueUtils.GetEscapedJsonString((obj != null) ? obj.ToString() : null) + "\"");
					return;
				}
				object obj2 = value.FromODataValue();
				sb.Append(JsonValueUtils.GetEscapedJsonString((obj2 != null) ? obj2.ToString() : null));
			}
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x000446C4 File Offset: 0x000428C4
		private static void ODataCollectionValueToString(StringBuilder sb, ODataCollectionValue value)
		{
			bool flag = true;
			sb.Append("[");
			foreach (object obj in value.Items)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					sb.Append(",");
				}
				ODataValue odataValue = obj as ODataValue;
				if (odataValue == null)
				{
					throw new ODataException(Strings.ODataJsonWriter_UnsupportedValueInCollection);
				}
				ODataJsonWriterUtils.ODataValueToString(sb, odataValue);
			}
			sb.Append("]");
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x00044758 File Offset: 0x00042958
		private static void ODataResourceValueToString(StringBuilder sb, ODataResourceValue value)
		{
			bool flag = true;
			sb.Append("{");
			foreach (ODataProperty odataProperty in value.Properties)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					sb.Append(",");
				}
				sb.Append("\"").Append(odataProperty.Name).Append("\"")
					.Append(":");
				ODataJsonWriterUtils.ODataValueToString(sb, odataProperty.ODataValue);
			}
			sb.Append("}");
		}
	}
}
