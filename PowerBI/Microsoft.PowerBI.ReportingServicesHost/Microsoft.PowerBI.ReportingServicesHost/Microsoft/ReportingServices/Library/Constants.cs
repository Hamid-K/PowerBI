using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000014 RID: 20
	public static class Constants
	{
		// Token: 0x04000059 RID: 89
		internal const string MimeType_Json = "application/json";

		// Token: 0x0400005A RID: 90
		internal const string MimeType_Xml = "application/xml";

		// Token: 0x0400005B RID: 91
		internal const string MimeType_PNG = "image/png";

		// Token: 0x0400005C RID: 92
		internal const string MimeType_BinaryStreamedJson = "application/octet-stream";

		// Token: 0x0400005D RID: 93
		internal const string MimeType_TextPlain = "text/plain";

		// Token: 0x0400005E RID: 94
		internal const string Header_Accept = "Accept";

		// Token: 0x0400005F RID: 95
		internal const string Header_AcceptEncoding = "Accept-Encoding";

		// Token: 0x04000060 RID: 96
		internal const string Header_ContentEncoding = "Content-Encoding";

		// Token: 0x04000061 RID: 97
		internal const string Header_User_Agent = "User-Agent";

		// Token: 0x04000062 RID: 98
		internal const string ContentEncoding_gzip = "gzip";

		// Token: 0x04000063 RID: 99
		internal static int StatusCode_SuccessNoBody = 204;

		// Token: 0x04000064 RID: 100
		internal static int StatusCode_ServerInternalError = 500;

		// Token: 0x04000065 RID: 101
		internal static int StatusCode_BadRequest = 400;

		// Token: 0x04000066 RID: 102
		internal static int StatusCode_NotFound = 404;

		// Token: 0x04000067 RID: 103
		internal const string CancelResponse = "\r\n{{\r\n    \"numCancelableOperations\" : {0},\r\n    \"numCanceledOperations\" : {1}\r\n}}";

		// Token: 0x04000068 RID: 104
		internal static readonly Version ApiVersion4_2 = new Version("4.2");

		// Token: 0x04000069 RID: 105
		internal static readonly Version ApiVersion4_1 = new Version("4.1");

		// Token: 0x0400006A RID: 106
		internal static readonly Version ApiVersion4 = new Version("4.0");

		// Token: 0x0400006B RID: 107
		internal static readonly Version ApiVersion3 = new Version("3.0");

		// Token: 0x0400006C RID: 108
		internal static readonly Version ApiVersion2 = new Version("2.0");

		// Token: 0x0400006D RID: 109
		internal static readonly Version ApiVersion1 = new Version("1.0");

		// Token: 0x0400006E RID: 110
		internal const string DefaultModelMetadataVersion = "2.0";

		// Token: 0x0400006F RID: 111
		internal const string ModelMetadataVersion2_5 = "2.5";

		// Token: 0x04000070 RID: 112
		internal const string DefaultEntityDataSourceName = "EntityDataSource";

		// Token: 0x04000071 RID: 113
		internal const string ExternalEntityDataSourceName = "EntityDataSource";
	}
}
