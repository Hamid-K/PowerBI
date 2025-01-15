using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.ReportingServices.ProgressivePackaging
{
	// Token: 0x02000014 RID: 20
	internal class ProgressiveTypeDictionary
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00002F20 File Offset: 0x00001120
		static ProgressiveTypeDictionary()
		{
			ProgressiveTypeDictionary.m_TypeDictionary.Add("rdlx", typeof(Stream));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("rdlxPath", typeof(string));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("dsq", typeof(Stream));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("eqr", typeof(Stream));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("is", typeof(Stream));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("getExternalImagesRequest", typeof(Stream));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("logClientTraceEventsRequest", typeof(Stream));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("additionalInformation", typeof(Dictionary<string, object>));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("sessionId", typeof(string));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("publishingWarnings", typeof(string[]));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("processingWarnings", typeof(string[]));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("rpds", typeof(Stream));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("modelDefinition", typeof(Stream));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("dataSources", typeof(Stream));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("getExternalImagesResponse", typeof(Stream));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("processedTraceEvents", typeof(bool));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("numCancellableJobs", typeof(int));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("numCancelledJobs", typeof(int));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("serverError", typeof(string));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("serverErrorCode", typeof(string));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("additionalModelMetadata", typeof(Stream));
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003149 File Offset: 0x00001349
		internal static Type GetType(string name)
		{
			if (ProgressiveTypeDictionary.m_TypeDictionary.ContainsKey(name))
			{
				return ProgressiveTypeDictionary.m_TypeDictionary[name];
			}
			return null;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003168 File Offset: 0x00001368
		internal static bool IsErrorMessageElement(MessageElement messageElement)
		{
			if (messageElement == null)
			{
				return false;
			}
			string name = messageElement.Name;
			return "serverError".Equals(name, StringComparison.Ordinal) || "serverErrorCode".Equals(name, StringComparison.Ordinal);
		}

		// Token: 0x0400002E RID: 46
		internal const string DummyContent = ".";

		// Token: 0x0400002F RID: 47
		private static readonly Dictionary<string, Type> m_TypeDictionary = new Dictionary<string, Type>(StringComparer.Ordinal);

		// Token: 0x04000030 RID: 48
		internal const string KeyRdlx = "rdlx";

		// Token: 0x04000031 RID: 49
		internal const string KeyRdlxPath = "rdlxPath";

		// Token: 0x04000032 RID: 50
		internal const string KeyDataSegmentationQuery = "dsq";

		// Token: 0x04000033 RID: 51
		internal const string KeyExecuteQueriesRequest = "eqr";

		// Token: 0x04000034 RID: 52
		internal const string KeyInteractiveState = "is";

		// Token: 0x04000035 RID: 53
		internal const string KeyGetExternalImagesRequest = "getExternalImagesRequest";

		// Token: 0x04000036 RID: 54
		internal const string KeyLogClientTraceEventsRequest = "logClientTraceEventsRequest";

		// Token: 0x04000037 RID: 55
		internal const string KeySessionId = "sessionId";

		// Token: 0x04000038 RID: 56
		internal const string KeyPublishingWarnings = "publishingWarnings";

		// Token: 0x04000039 RID: 57
		internal const string KeyProcessingWarnings = "processingWarnings";

		// Token: 0x0400003A RID: 58
		internal const string KeyRpds = "rpds";

		// Token: 0x0400003B RID: 59
		internal const string KeyModelDefinition = "modelDefinition";

		// Token: 0x0400003C RID: 60
		internal const string KeyDataSources = "dataSources";

		// Token: 0x0400003D RID: 61
		internal const string KeyGetExternalImagesResponse = "getExternalImagesResponse";

		// Token: 0x0400003E RID: 62
		internal const string KeyLogClientTraceEventsResponse = "processedTraceEvents";

		// Token: 0x0400003F RID: 63
		internal const string KeyNumCancellableJobs = "numCancellableJobs";

		// Token: 0x04000040 RID: 64
		internal const string KeyNumCancelledJobs = "numCancelledJobs";

		// Token: 0x04000041 RID: 65
		internal const string KeyWasReportLastModifiedByCurrentUser = "wasReportLastModifiedByCurrentUser";

		// Token: 0x04000042 RID: 66
		internal const string KeyAdditionalInformation = "additionalInformation";

		// Token: 0x04000043 RID: 67
		internal const string KeyAdditionalModelMetadata = "additionalModelMetadata";

		// Token: 0x04000044 RID: 68
		internal const string KeyServerError = "serverError";

		// Token: 0x04000045 RID: 69
		internal const string KeyServerErrorCode = "serverErrorCode";

		// Token: 0x04000046 RID: 70
		internal const string ServerErrorCodeInvalidReportArchiveFormat = "rsInvalidReportArchiveFormat";

		// Token: 0x04000047 RID: 71
		internal const string ServerErrorCodeSessionNotFound = "SessionNotFound";

		// Token: 0x04000048 RID: 72
		internal const string ServerErrorCodeInvalidSessionId = "InvalidSessionId";

		// Token: 0x04000049 RID: 73
		internal const string ServerErrorCodeProcessingError = "rsProcessingError";

		// Token: 0x0400004A RID: 74
		internal const string ServerErrorCodeRenderingError = "rsRenderingError";

		// Token: 0x0400004B RID: 75
		internal const string ServerErrorCodeCommandExecutionError = "rsErrorExecutingCommand";

		// Token: 0x0400004C RID: 76
		internal const string ServerErrorCodeMissingExecuteQueriesRequest = "MissingExecuteQueriesRequest";

		// Token: 0x0400004D RID: 77
		internal const string ServerErrorCodeInvalidConcurrentRenderEditSessionRequest = "InvalidConcurrentRenderEditSessionRequest";

		// Token: 0x0400004E RID: 78
		internal const string ServerErrorCodeMissingGetExternalImagesRequest = "MissingGetExternalImagesRequest";

		// Token: 0x0400004F RID: 79
		internal const string ServerErrorCodeExternalImageInvalidUri = "ExternalImageInvalidUri";

		// Token: 0x04000050 RID: 80
		internal const string ServerErrorCodeExternalImageHttpError = "ExternalImageHttpError";

		// Token: 0x04000051 RID: 81
		internal const string ServerErrorCodeExternalImageNetworkError = "ExternalImageNetworkError";

		// Token: 0x04000052 RID: 82
		internal const string ServerErrorCodeExternalImageInvalidContent = "ExternalImageInvalidContent";

		// Token: 0x04000053 RID: 83
		internal const string ServerErrorCodeExternalImageUnexpectedError = "ExternalImageUnexpectedError";

		// Token: 0x04000054 RID: 84
		internal const string ServerErrorCodeExternalImageDisallowedError = "ExternalImageDisallowedError";

		// Token: 0x04000055 RID: 85
		internal const string ServerErrorCodeMissingLogClientTraceEventsRequest = "MissingLogClientTraceEventsRequest";

		// Token: 0x04000056 RID: 86
		internal const string ServerErrorCodeReadingNextDataRowError = "rsErrorReadingNextDataRow";

		// Token: 0x04000057 RID: 87
		internal const string ServerErrorCodeReadingDataFieldError = "rsErrorReadingDataField";

		// Token: 0x04000058 RID: 88
		internal const string ServerErrorCodeASCloudConnectionError = "ASCloudConnectionError";

		// Token: 0x04000059 RID: 89
		internal const string ServerErrorCodeASQueryExceededMemoryLimitError = "ASQueryExceededMemoryLimitError";

		// Token: 0x0400005A RID: 90
		internal const string ServerErrorCodeGatewayCommunicationError = "gwCommunicationError";
	}
}
