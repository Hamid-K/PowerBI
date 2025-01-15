using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000025 RID: 37
	public static class Constants
	{
		// Token: 0x040000BC RID: 188
		internal const string MimeType_Json = "application/json";

		// Token: 0x040000BD RID: 189
		internal const string MimeType_Xml = "application/xml";

		// Token: 0x040000BE RID: 190
		internal const string MimeType_PNG = "image/png";

		// Token: 0x040000BF RID: 191
		internal const string MimeType_BinaryStreamedJson = "application/octet-stream";

		// Token: 0x040000C0 RID: 192
		internal const string MimeType_TextPlain = "text/plain";

		// Token: 0x040000C1 RID: 193
		internal const string Header_Accept = "Accept";

		// Token: 0x040000C2 RID: 194
		internal const string Header_AcceptEncoding = "Accept-Encoding";

		// Token: 0x040000C3 RID: 195
		internal const string Header_ContentEncoding = "Content-Encoding";

		// Token: 0x040000C4 RID: 196
		internal const string Header_User_Agent = "User-Agent";

		// Token: 0x040000C5 RID: 197
		internal const string ContentEncoding_gzip = "gzip";

		// Token: 0x040000C6 RID: 198
		internal static int StatusCode_SuccessNoBody = 204;

		// Token: 0x040000C7 RID: 199
		internal static int StatusCode_ServerInternalError = 500;

		// Token: 0x040000C8 RID: 200
		internal static int StatusCode_BadRequest = 400;

		// Token: 0x040000C9 RID: 201
		internal static int StatusCode_NotFound = 404;

		// Token: 0x040000CA RID: 202
		internal const string CancelResponse = "\r\n{{\r\n    \"numCancelableOperations\" : {0},\r\n    \"numCanceledOperations\" : {1}\r\n}}";

		// Token: 0x040000CB RID: 203
		internal static readonly Version ApiVersion4_2 = new Version("4.2");

		// Token: 0x040000CC RID: 204
		internal static readonly Version ApiVersion4_1 = new Version("4.1");

		// Token: 0x040000CD RID: 205
		internal static readonly Version ApiVersion4 = new Version("4.0");

		// Token: 0x040000CE RID: 206
		internal static readonly Version ApiVersion3 = new Version("3.0");

		// Token: 0x040000CF RID: 207
		internal static readonly Version ApiVersion2 = new Version("2.0");

		// Token: 0x040000D0 RID: 208
		internal static readonly Version ApiVersion1 = new Version("1.0");

		// Token: 0x040000D1 RID: 209
		internal const string DefaultModelMetadataVersion = "2.0";

		// Token: 0x040000D2 RID: 210
		internal const string DefaultEntityDataSourceName = "EntityDataSource";

		// Token: 0x040000D3 RID: 211
		internal const string ExternalEntityDataSourceName = "EntityDataSource";
	}
}
