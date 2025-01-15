using System;

namespace Microsoft.ReportingServices.OData.Json
{
	// Token: 0x02000014 RID: 20
	internal static class JsonConstants
	{
		// Token: 0x04000069 RID: 105
		internal const string ODataResultsName = "results";

		// Token: 0x0400006A RID: 106
		internal const string ODataDataWrapper = "\"d\":";

		// Token: 0x0400006B RID: 107
		internal const string ODataDataWrapperPropertyName = "d";

		// Token: 0x0400006C RID: 108
		internal const string ODataEntryIdName = "id";

		// Token: 0x0400006D RID: 109
		internal const string ODataMetadataName = "__metadata";

		// Token: 0x0400006E RID: 110
		internal const string ODataMetadataUriName = "uri";

		// Token: 0x0400006F RID: 111
		internal const string ODataMetadataTypeName = "type";

		// Token: 0x04000070 RID: 112
		internal const string ODataMetadataETagName = "etag";

		// Token: 0x04000071 RID: 113
		internal const string ODataMetadataMediaResourceName = "__mediaresource";

		// Token: 0x04000072 RID: 114
		internal const string ODataMetadataMediaUriName = "media_src";

		// Token: 0x04000073 RID: 115
		internal const string ODataMetadataContentTypeName = "content_type";

		// Token: 0x04000074 RID: 116
		internal const string ODataMetadataMediaETagName = "media_etag";

		// Token: 0x04000075 RID: 117
		internal const string ODataMetadataEditMediaName = "edit_media";

		// Token: 0x04000076 RID: 118
		internal const string ODataMetadataPropertiesName = "properties";

		// Token: 0x04000077 RID: 119
		internal const string ODataMetadataPropertiesAssociationUriName = "associationuri";

		// Token: 0x04000078 RID: 120
		internal const string ODataCountName = "__count";

		// Token: 0x04000079 RID: 121
		internal const string ODataNextLinkName = "__next";

		// Token: 0x0400007A RID: 122
		internal const string ODataDeferredName = "__deferred";

		// Token: 0x0400007B RID: 123
		internal const string ODataNavigationLinkUriName = "uri";

		// Token: 0x0400007C RID: 124
		internal const string ODataUriName = "uri";

		// Token: 0x0400007D RID: 125
		internal const string ODataActionsMetadataName = "actions";

		// Token: 0x0400007E RID: 126
		internal const string ODataFunctionsMetadataName = "functions";

		// Token: 0x0400007F RID: 127
		internal const string ODataOperationTitleName = "title";

		// Token: 0x04000080 RID: 128
		internal const string ODataOperationMetadataName = "metadata";

		// Token: 0x04000081 RID: 129
		internal const string ODataOperationTargetName = "target";

		// Token: 0x04000082 RID: 130
		internal const string ODataErrorName = "error";

		// Token: 0x04000083 RID: 131
		internal const string ODataErrorCodeName = "code";

		// Token: 0x04000084 RID: 132
		internal const string ODataErrorMessageName = "message";

		// Token: 0x04000085 RID: 133
		internal const string ODataErrorMessageLanguageName = "lang";

		// Token: 0x04000086 RID: 134
		internal const string ODataErrorMessageValueName = "value";

		// Token: 0x04000087 RID: 135
		internal const string ODataErrorInnerErrorName = "innererror";

		// Token: 0x04000088 RID: 136
		internal const string ODataErrorInnerErrorMessageName = "message";

		// Token: 0x04000089 RID: 137
		internal const string ODataErrorInnerErrorTypeNameName = "type";

		// Token: 0x0400008A RID: 138
		internal const string ODataErrorInnerErrorStackTraceName = "stacktrace";

		// Token: 0x0400008B RID: 139
		internal const string ODataErrorInnerErrorInnerErrorName = "internalexception";

		// Token: 0x0400008C RID: 140
		internal const string ODataDateTimeFormat = "\\/Date({0})\\/";

		// Token: 0x0400008D RID: 141
		internal const string ODataDateTimeOffsetFormat = "\\/Date({0}{1}{2:D4})\\/";

		// Token: 0x0400008E RID: 142
		internal const string ODataDateTimeOffsetPlusSign = "+";

		// Token: 0x0400008F RID: 143
		internal const string ODataServiceDocumentEntitySetsName = "EntitySets";

		// Token: 0x04000090 RID: 144
		internal const string JsonTrueLiteral = "true";

		// Token: 0x04000091 RID: 145
		internal const string JsonFalseLiteral = "false";

		// Token: 0x04000092 RID: 146
		internal const string JsonNullLiteral = "null";

		// Token: 0x04000093 RID: 147
		internal const string StartObjectScope = "{";

		// Token: 0x04000094 RID: 148
		internal const string EndObjectScope = "}";

		// Token: 0x04000095 RID: 149
		internal const string StartArrayScope = "[";

		// Token: 0x04000096 RID: 150
		internal const string EndArrayScope = "]";

		// Token: 0x04000097 RID: 151
		internal const string ObjectMemberSeparator = ",";

		// Token: 0x04000098 RID: 152
		internal const string ArrayElementSeparator = ",";

		// Token: 0x04000099 RID: 153
		internal const string NameValueSeparator = ":";

		// Token: 0x0400009A RID: 154
		internal const char QuoteCharacter = '"';

		// Token: 0x0400009B RID: 155
		internal const char SingleQuoteCharacter = '\'';

		// Token: 0x0400009C RID: 156
		internal const string BinaryDataPrefix = "base64";
	}
}
