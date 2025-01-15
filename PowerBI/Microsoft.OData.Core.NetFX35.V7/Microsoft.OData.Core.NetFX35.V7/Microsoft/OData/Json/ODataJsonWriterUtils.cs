using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Json
{
	// Token: 0x020001ED RID: 493
	internal static class ODataJsonWriterUtils
	{
		// Token: 0x0600135E RID: 4958 RVA: 0x00037F44 File Offset: 0x00036144
		internal static void WriteError(IJsonWriter jsonWriter, Action<IEnumerable<ODataInstanceAnnotation>> writeInstanceAnnotationsDelegate, ODataError error, bool includeDebugInformation, int maxInnerErrorDepth, bool writingJsonLight)
		{
			string text;
			string text2;
			ErrorUtils.GetErrorDetails(error, out text, out text2);
			ODataInnerError odataInnerError = (includeDebugInformation ? error.InnerError : null);
			ODataJsonWriterUtils.WriteError(jsonWriter, text, text2, error.Target, error.Details, odataInnerError, error.GetInstanceAnnotations(), writeInstanceAnnotationsDelegate, maxInnerErrorDepth, writingJsonLight);
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x00037F88 File Offset: 0x00036188
		internal static void StartJsonPaddingIfRequired(IJsonWriter jsonWriter, ODataMessageWriterSettings settings)
		{
			if (settings.HasJsonPaddingFunction())
			{
				jsonWriter.WritePaddingFunctionName(settings.JsonPCallback);
				jsonWriter.StartPaddingFunctionScope();
			}
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x00037FA4 File Offset: 0x000361A4
		internal static void EndJsonPaddingIfRequired(IJsonWriter jsonWriter, ODataMessageWriterSettings settings)
		{
			if (settings.HasJsonPaddingFunction())
			{
				jsonWriter.EndPaddingFunctionScope();
			}
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x00037FB4 File Offset: 0x000361B4
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
				writeInstanceAnnotationsDelegate.Invoke(instanceAnnotations);
			}
			jsonWriter.EndObjectScope();
			jsonWriter.EndObjectScope();
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x00038060 File Offset: 0x00036260
		private static void WriteErrorDetails(IJsonWriter jsonWriter, IEnumerable<ODataErrorDetail> details, string odataErrorDetailsName)
		{
			jsonWriter.WriteName(odataErrorDetailsName);
			jsonWriter.StartArrayScope();
			foreach (ODataErrorDetail odataErrorDetail in Enumerable.Where<ODataErrorDetail>(details, (ODataErrorDetail d) => d != null))
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

		// Token: 0x06001363 RID: 4963 RVA: 0x00038144 File Offset: 0x00036344
		private static void WriteInnerError(IJsonWriter jsonWriter, ODataInnerError innerError, string innerErrorPropertyName, int recursionDepth, int maxInnerErrorDepth)
		{
			ValidationUtils.IncreaseAndValidateRecursionDepth(ref recursionDepth, maxInnerErrorDepth);
			jsonWriter.WriteName(innerErrorPropertyName);
			jsonWriter.StartObjectScope();
			jsonWriter.WriteName("message");
			jsonWriter.WriteValue(innerError.Message ?? string.Empty);
			jsonWriter.WriteName("type");
			jsonWriter.WriteValue(innerError.TypeName ?? string.Empty);
			jsonWriter.WriteName("stacktrace");
			jsonWriter.WriteValue(innerError.StackTrace ?? string.Empty);
			if (innerError.InnerError != null)
			{
				ODataJsonWriterUtils.WriteInnerError(jsonWriter, innerError.InnerError, "internalexception", recursionDepth, maxInnerErrorDepth);
			}
			jsonWriter.EndObjectScope();
		}
	}
}
