using System;

namespace Microsoft.OData.Json
{
	// Token: 0x020001E3 RID: 483
	internal static class JsonConstants
	{
		// Token: 0x04000988 RID: 2440
		internal const string ODataActionsMetadataName = "actions";

		// Token: 0x04000989 RID: 2441
		internal const string ODataFunctionsMetadataName = "functions";

		// Token: 0x0400098A RID: 2442
		internal const string ODataOperationTitleName = "title";

		// Token: 0x0400098B RID: 2443
		internal const string ODataOperationMetadataName = "metadata";

		// Token: 0x0400098C RID: 2444
		internal const string ODataOperationTargetName = "target";

		// Token: 0x0400098D RID: 2445
		internal const string ODataErrorName = "error";

		// Token: 0x0400098E RID: 2446
		internal const string ODataErrorCodeName = "code";

		// Token: 0x0400098F RID: 2447
		internal const string ODataErrorMessageName = "message";

		// Token: 0x04000990 RID: 2448
		internal const string ODataErrorTargetName = "target";

		// Token: 0x04000991 RID: 2449
		internal const string ODataErrorDetailsName = "details";

		// Token: 0x04000992 RID: 2450
		internal const string ODataErrorInnerErrorName = "innererror";

		// Token: 0x04000993 RID: 2451
		internal const string ODataErrorInnerErrorMessageName = "message";

		// Token: 0x04000994 RID: 2452
		internal const string ODataErrorInnerErrorTypeNameName = "type";

		// Token: 0x04000995 RID: 2453
		internal const string ODataErrorInnerErrorStackTraceName = "stacktrace";

		// Token: 0x04000996 RID: 2454
		internal const string ODataErrorInnerErrorInnerErrorName = "internalexception";

		// Token: 0x04000997 RID: 2455
		internal const string ODataDateTimeFormat = "\\/Date({0})\\/";

		// Token: 0x04000998 RID: 2456
		internal const string ODataDateTimeOffsetFormat = "\\/Date({0}{1}{2:D4})\\/";

		// Token: 0x04000999 RID: 2457
		internal const string ODataDateTimeOffsetPlusSign = "+";

		// Token: 0x0400099A RID: 2458
		internal const string ODataServiceDocumentEntitySetsName = "EntitySets";

		// Token: 0x0400099B RID: 2459
		internal const string JsonTrueLiteral = "true";

		// Token: 0x0400099C RID: 2460
		internal const string JsonFalseLiteral = "false";

		// Token: 0x0400099D RID: 2461
		internal const string JsonNullLiteral = "null";

		// Token: 0x0400099E RID: 2462
		internal const string StartObjectScope = "{";

		// Token: 0x0400099F RID: 2463
		internal const string EndObjectScope = "}";

		// Token: 0x040009A0 RID: 2464
		internal const string StartArrayScope = "[";

		// Token: 0x040009A1 RID: 2465
		internal const string EndArrayScope = "]";

		// Token: 0x040009A2 RID: 2466
		internal const string StartPaddingFunctionScope = "(";

		// Token: 0x040009A3 RID: 2467
		internal const string EndPaddingFunctionScope = ")";

		// Token: 0x040009A4 RID: 2468
		internal const string ObjectMemberSeparator = ",";

		// Token: 0x040009A5 RID: 2469
		internal const string ArrayElementSeparator = ",";

		// Token: 0x040009A6 RID: 2470
		internal const string NameValueSeparator = ":";

		// Token: 0x040009A7 RID: 2471
		internal const char QuoteCharacter = '"';
	}
}
