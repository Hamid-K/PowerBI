using System;

namespace Microsoft.OData.Core.Json
{
	// Token: 0x02000112 RID: 274
	internal static class JsonConstants
	{
		// Token: 0x04000412 RID: 1042
		internal const string ODataActionsMetadataName = "actions";

		// Token: 0x04000413 RID: 1043
		internal const string ODataFunctionsMetadataName = "functions";

		// Token: 0x04000414 RID: 1044
		internal const string ODataOperationTitleName = "title";

		// Token: 0x04000415 RID: 1045
		internal const string ODataOperationMetadataName = "metadata";

		// Token: 0x04000416 RID: 1046
		internal const string ODataOperationTargetName = "target";

		// Token: 0x04000417 RID: 1047
		internal const string ODataErrorName = "error";

		// Token: 0x04000418 RID: 1048
		internal const string ODataErrorCodeName = "code";

		// Token: 0x04000419 RID: 1049
		internal const string ODataErrorMessageName = "message";

		// Token: 0x0400041A RID: 1050
		internal const string ODataErrorTargetName = "target";

		// Token: 0x0400041B RID: 1051
		internal const string ODataErrorDetailsName = "details";

		// Token: 0x0400041C RID: 1052
		internal const string ODataErrorInnerErrorName = "innererror";

		// Token: 0x0400041D RID: 1053
		internal const string ODataErrorInnerErrorMessageName = "message";

		// Token: 0x0400041E RID: 1054
		internal const string ODataErrorInnerErrorTypeNameName = "type";

		// Token: 0x0400041F RID: 1055
		internal const string ODataErrorInnerErrorStackTraceName = "stacktrace";

		// Token: 0x04000420 RID: 1056
		internal const string ODataErrorInnerErrorInnerErrorName = "internalexception";

		// Token: 0x04000421 RID: 1057
		internal const string ODataDateTimeFormat = "\\/Date({0})\\/";

		// Token: 0x04000422 RID: 1058
		internal const string ODataDateTimeOffsetFormat = "\\/Date({0}{1}{2:D4})\\/";

		// Token: 0x04000423 RID: 1059
		internal const string ODataDateTimeOffsetPlusSign = "+";

		// Token: 0x04000424 RID: 1060
		internal const string ODataServiceDocumentEntitySetsName = "EntitySets";

		// Token: 0x04000425 RID: 1061
		internal const string JsonTrueLiteral = "true";

		// Token: 0x04000426 RID: 1062
		internal const string JsonFalseLiteral = "false";

		// Token: 0x04000427 RID: 1063
		internal const string JsonNullLiteral = "null";

		// Token: 0x04000428 RID: 1064
		internal const string StartObjectScope = "{";

		// Token: 0x04000429 RID: 1065
		internal const string EndObjectScope = "}";

		// Token: 0x0400042A RID: 1066
		internal const string StartArrayScope = "[";

		// Token: 0x0400042B RID: 1067
		internal const string EndArrayScope = "]";

		// Token: 0x0400042C RID: 1068
		internal const string StartPaddingFunctionScope = "(";

		// Token: 0x0400042D RID: 1069
		internal const string EndPaddingFunctionScope = ")";

		// Token: 0x0400042E RID: 1070
		internal const string ObjectMemberSeparator = ",";

		// Token: 0x0400042F RID: 1071
		internal const string ArrayElementSeparator = ",";

		// Token: 0x04000430 RID: 1072
		internal const string NameValueSeparator = ":";

		// Token: 0x04000431 RID: 1073
		internal const char QuoteCharacter = '"';
	}
}
