using System;

namespace Microsoft.ReportingServices.OData.Json
{
	// Token: 0x02000011 RID: 17
	internal static class JsonConstants
	{
		// Token: 0x04000045 RID: 69
		internal const string ODataResultsName = "results";

		// Token: 0x04000046 RID: 70
		internal const string ODataDataWrapper = "\"d\":";

		// Token: 0x04000047 RID: 71
		internal const string ODataDataWrapperPropertyName = "d";

		// Token: 0x04000048 RID: 72
		internal const string ODataEntryIdName = "id";

		// Token: 0x04000049 RID: 73
		internal const string ODataMetadataName = "__metadata";

		// Token: 0x0400004A RID: 74
		internal const string ODataMetadataUriName = "uri";

		// Token: 0x0400004B RID: 75
		internal const string ODataMetadataTypeName = "type";

		// Token: 0x0400004C RID: 76
		internal const string ODataMetadataETagName = "etag";

		// Token: 0x0400004D RID: 77
		internal const string ODataMetadataMediaResourceName = "__mediaresource";

		// Token: 0x0400004E RID: 78
		internal const string ODataMetadataMediaUriName = "media_src";

		// Token: 0x0400004F RID: 79
		internal const string ODataMetadataContentTypeName = "content_type";

		// Token: 0x04000050 RID: 80
		internal const string ODataMetadataMediaETagName = "media_etag";

		// Token: 0x04000051 RID: 81
		internal const string ODataMetadataEditMediaName = "edit_media";

		// Token: 0x04000052 RID: 82
		internal const string ODataMetadataPropertiesName = "properties";

		// Token: 0x04000053 RID: 83
		internal const string ODataMetadataPropertiesAssociationUriName = "associationuri";

		// Token: 0x04000054 RID: 84
		internal const string ODataCountName = "__count";

		// Token: 0x04000055 RID: 85
		internal const string ODataNextLinkName = "__next";

		// Token: 0x04000056 RID: 86
		internal const string ODataDeferredName = "__deferred";

		// Token: 0x04000057 RID: 87
		internal const string ODataNavigationLinkUriName = "uri";

		// Token: 0x04000058 RID: 88
		internal const string ODataUriName = "uri";

		// Token: 0x04000059 RID: 89
		internal const string ODataActionsMetadataName = "actions";

		// Token: 0x0400005A RID: 90
		internal const string ODataFunctionsMetadataName = "functions";

		// Token: 0x0400005B RID: 91
		internal const string ODataOperationTitleName = "title";

		// Token: 0x0400005C RID: 92
		internal const string ODataOperationMetadataName = "metadata";

		// Token: 0x0400005D RID: 93
		internal const string ODataOperationTargetName = "target";

		// Token: 0x0400005E RID: 94
		internal const string ODataErrorName = "error";

		// Token: 0x0400005F RID: 95
		internal const string ODataErrorCodeName = "code";

		// Token: 0x04000060 RID: 96
		internal const string ODataErrorMessageName = "message";

		// Token: 0x04000061 RID: 97
		internal const string ODataErrorMessageLanguageName = "lang";

		// Token: 0x04000062 RID: 98
		internal const string ODataErrorMessageValueName = "value";

		// Token: 0x04000063 RID: 99
		internal const string ODataErrorInnerErrorName = "innererror";

		// Token: 0x04000064 RID: 100
		internal const string ODataErrorInnerErrorMessageName = "message";

		// Token: 0x04000065 RID: 101
		internal const string ODataErrorInnerErrorTypeNameName = "type";

		// Token: 0x04000066 RID: 102
		internal const string ODataErrorInnerErrorStackTraceName = "stacktrace";

		// Token: 0x04000067 RID: 103
		internal const string ODataErrorInnerErrorInnerErrorName = "internalexception";

		// Token: 0x04000068 RID: 104
		internal const string ODataDateTimeFormat = "\\/Date({0})\\/";

		// Token: 0x04000069 RID: 105
		internal const string ODataDateTimeOffsetFormat = "\\/Date({0}{1}{2:D4})\\/";

		// Token: 0x0400006A RID: 106
		internal const string ODataDateTimeOffsetPlusSign = "+";

		// Token: 0x0400006B RID: 107
		internal const string ODataServiceDocumentEntitySetsName = "EntitySets";

		// Token: 0x0400006C RID: 108
		internal const string JsonTrueLiteral = "true";

		// Token: 0x0400006D RID: 109
		internal const string JsonFalseLiteral = "false";

		// Token: 0x0400006E RID: 110
		internal const string JsonNullLiteral = "null";

		// Token: 0x0400006F RID: 111
		internal const string StartObjectScope = "{";

		// Token: 0x04000070 RID: 112
		internal const string EndObjectScope = "}";

		// Token: 0x04000071 RID: 113
		internal const string StartArrayScope = "[";

		// Token: 0x04000072 RID: 114
		internal const string EndArrayScope = "]";

		// Token: 0x04000073 RID: 115
		internal const string ObjectMemberSeparator = ",";

		// Token: 0x04000074 RID: 116
		internal const string ArrayElementSeparator = ",";

		// Token: 0x04000075 RID: 117
		internal const string NameValueSeparator = ":";

		// Token: 0x04000076 RID: 118
		internal const char QuoteCharacter = '"';

		// Token: 0x04000077 RID: 119
		internal const char SingleQuoteCharacter = '\'';

		// Token: 0x04000078 RID: 120
		internal const string BinaryDataPrefix = "base64";
	}
}
