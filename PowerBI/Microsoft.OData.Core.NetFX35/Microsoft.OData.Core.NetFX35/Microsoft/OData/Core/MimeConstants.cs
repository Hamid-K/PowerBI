using System;

namespace Microsoft.OData.Core
{
	// Token: 0x02000134 RID: 308
	internal static class MimeConstants
	{
		// Token: 0x040004C3 RID: 1219
		internal const string MimeAny = "*/*";

		// Token: 0x040004C4 RID: 1220
		internal const string MimeApplicationType = "application";

		// Token: 0x040004C5 RID: 1221
		internal const string MimeTextType = "text";

		// Token: 0x040004C6 RID: 1222
		internal const string MimeMultipartType = "multipart";

		// Token: 0x040004C7 RID: 1223
		internal const string MimeAtomXmlSubType = "atom+xml";

		// Token: 0x040004C8 RID: 1224
		internal const string MimeAtomSvcXmlSubType = "atomsvc+xml";

		// Token: 0x040004C9 RID: 1225
		internal const string MimeXmlSubType = "xml";

		// Token: 0x040004CA RID: 1226
		internal const string MimeJsonSubType = "json";

		// Token: 0x040004CB RID: 1227
		internal const string MimePlainSubType = "plain";

		// Token: 0x040004CC RID: 1228
		internal const string MimeJavaScriptType = "javascript";

		// Token: 0x040004CD RID: 1229
		internal const string MimeOctetStreamSubType = "octet-stream";

		// Token: 0x040004CE RID: 1230
		internal const string MimeMixedSubType = "mixed";

		// Token: 0x040004CF RID: 1231
		internal const string MimeHttpSubType = "http";

		// Token: 0x040004D0 RID: 1232
		internal const string MimeTypeParameterName = "type";

		// Token: 0x040004D1 RID: 1233
		internal const string MimeTypeParameterValueEntry = "entry";

		// Token: 0x040004D2 RID: 1234
		internal const string MimeTypeParameterValueFeed = "feed";

		// Token: 0x040004D3 RID: 1235
		internal const string MimeMetadataParameterName = "odata.metadata";

		// Token: 0x040004D4 RID: 1236
		internal const string MimeMetadataParameterValueVerbose = "verbose";

		// Token: 0x040004D5 RID: 1237
		internal const string MimeMetadataParameterValueFull = "full";

		// Token: 0x040004D6 RID: 1238
		internal const string MimeMetadataParameterValueMinimal = "minimal";

		// Token: 0x040004D7 RID: 1239
		internal const string MimeMetadataParameterValueNone = "none";

		// Token: 0x040004D8 RID: 1240
		internal const string MimeStreamingParameterName = "odata.streaming";

		// Token: 0x040004D9 RID: 1241
		internal const string MimeIeee754CompatibleParameterName = "IEEE754Compatible";

		// Token: 0x040004DA RID: 1242
		internal const string MimeParameterValueTrue = "true";

		// Token: 0x040004DB RID: 1243
		internal const string MimeParameterValueFalse = "false";

		// Token: 0x040004DC RID: 1244
		internal const string MimeApplicationXml = "application/xml";

		// Token: 0x040004DD RID: 1245
		internal const string MimeApplicationAtomXml = "application/atom+xml";

		// Token: 0x040004DE RID: 1246
		internal const string MimeApplicationAtomXmlTypeEntry = "application/atom+xml;type=entry";

		// Token: 0x040004DF RID: 1247
		internal const string MimeApplicationAtomXmlTypeFeed = "application/atom+xml;type=feed";

		// Token: 0x040004E0 RID: 1248
		internal const string MimeApplicationJson = "application/json";

		// Token: 0x040004E1 RID: 1249
		internal const string MimeApplicationOctetStream = "application/octet-stream";

		// Token: 0x040004E2 RID: 1250
		internal const string MimeApplicationHttp = "application/http";

		// Token: 0x040004E3 RID: 1251
		internal const string MimeTextXml = "text/xml";

		// Token: 0x040004E4 RID: 1252
		internal const string MimeTextPlain = "text/plain";

		// Token: 0x040004E5 RID: 1253
		internal const string TextJavaScript = "text/javascript";

		// Token: 0x040004E6 RID: 1254
		internal const string MimeMultipartMixed = "multipart/mixed";

		// Token: 0x040004E7 RID: 1255
		internal const string MimeStar = "*";

		// Token: 0x040004E8 RID: 1256
		private const string Separator = "/";
	}
}
