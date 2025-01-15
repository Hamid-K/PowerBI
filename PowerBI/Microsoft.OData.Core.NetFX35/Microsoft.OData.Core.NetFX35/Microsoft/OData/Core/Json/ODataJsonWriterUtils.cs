using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Core.Json
{
	// Token: 0x0200011E RID: 286
	internal static class ODataJsonWriterUtils
	{
		// Token: 0x06000AC0 RID: 2752 RVA: 0x00027304 File Offset: 0x00025504
		internal static void WriteError(IJsonWriter jsonWriter, Action<IEnumerable<ODataInstanceAnnotation>> writeInstanceAnnotationsDelegate, ODataError error, bool includeDebugInformation, int maxInnerErrorDepth, bool writingJsonLight)
		{
			string text;
			string text2;
			ErrorUtils.GetErrorDetails(error, out text, out text2);
			ODataInnerError odataInnerError = (includeDebugInformation ? error.InnerError : null);
			ODataJsonWriterUtils.WriteError(jsonWriter, text, text2, error.Target, error.Details, odataInnerError, error.GetInstanceAnnotations(), writeInstanceAnnotationsDelegate, maxInnerErrorDepth, writingJsonLight);
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x00027348 File Offset: 0x00025548
		internal static void StartJsonPaddingIfRequired(IJsonWriter jsonWriter, ODataMessageWriterSettings settings)
		{
			if (settings.HasJsonPaddingFunction())
			{
				jsonWriter.WritePaddingFunctionName(settings.JsonPCallback);
				jsonWriter.StartPaddingFunctionScope();
			}
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00027364 File Offset: 0x00025564
		internal static void EndJsonPaddingIfRequired(IJsonWriter jsonWriter, ODataMessageWriterSettings settings)
		{
			if (settings.HasJsonPaddingFunction())
			{
				jsonWriter.EndPaddingFunctionScope();
			}
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00027374 File Offset: 0x00025574
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

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0002742C File Offset: 0x0002562C
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

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00027510 File Offset: 0x00025710
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
