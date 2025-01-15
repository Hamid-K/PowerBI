using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Microsoft.InfoNav.Explore.ServiceContracts.Internal
{
	// Token: 0x0200000D RID: 13
	public static class ServiceErrorJsonWriter
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002270 File Offset: 0x00000470
		public static void WriteServiceErrorIntoJson(this JsonTextWriter jsonWriter, ServiceError serviceError)
		{
			jsonWriter.WritePropertyName("error");
			jsonWriter.WriteStartObject();
			jsonWriter.WritePropertyName("statusCode");
			jsonWriter.WriteValue(serviceError.StatusCode);
			if (!string.IsNullOrWhiteSpace(serviceError.PowerBIErrorCode))
			{
				jsonWriter.WritePropertyName("errorCode");
				jsonWriter.WriteValue(serviceError.PowerBIErrorCode);
			}
			if (!string.IsNullOrWhiteSpace(serviceError.Message))
			{
				jsonWriter.WritePropertyName("message");
				jsonWriter.WriteValue(serviceError.Message);
			}
			if (serviceError.ErrorDetails != null && serviceError.ErrorDetails.Count > 0)
			{
				ServiceErrorJsonWriter.WriteErrorDetailsArray(jsonWriter, "errorDetails", serviceError.ErrorDetails);
			}
			jsonWriter.WriteEndObject();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002320 File Offset: 0x00000520
		private static void WriteErrorDetailsArray(JsonTextWriter jsonWriter, string propertyName, IList<ErrorDetail> errorDetails)
		{
			jsonWriter.WritePropertyName(propertyName);
			jsonWriter.WriteStartArray();
			foreach (ErrorDetail errorDetail in errorDetails)
			{
				ServiceErrorJsonWriter.WriteDetailObject(jsonWriter, errorDetail);
			}
			jsonWriter.WriteEndArray();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000237C File Offset: 0x0000057C
		private static void WriteDetailObject(JsonTextWriter jsonWriter, ErrorDetail detail)
		{
			jsonWriter.WriteStartObject();
			jsonWriter.WritePropertyName("code");
			jsonWriter.WriteValue(detail.NameCode);
			jsonWriter.WritePropertyName("detail");
			ServiceErrorJsonWriter.WriteValueObject(jsonWriter, detail.Value);
			jsonWriter.WriteEndObject();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000023B8 File Offset: 0x000005B8
		private static void WriteValueObject(JsonTextWriter jsonWriter, ErrorDetailValue value)
		{
			jsonWriter.WriteStartObject();
			jsonWriter.WritePropertyName("type");
			jsonWriter.WriteValue(value.ResourceType);
			jsonWriter.WritePropertyName("value");
			jsonWriter.WriteValue(value.ResourceValue);
			jsonWriter.WriteEndObject();
		}
	}
}
