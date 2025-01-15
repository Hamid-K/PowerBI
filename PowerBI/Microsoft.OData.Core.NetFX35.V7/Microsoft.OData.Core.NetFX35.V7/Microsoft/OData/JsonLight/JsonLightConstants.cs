using System;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x020001F6 RID: 502
	internal static class JsonLightConstants
	{
		// Token: 0x040009CA RID: 2506
		internal const string ODataAnnotationNamespacePrefix = "odata.";

		// Token: 0x040009CB RID: 2507
		internal const char ODataPropertyAnnotationSeparatorChar = '@';

		// Token: 0x040009CC RID: 2508
		internal const string ODataNullAnnotationTrueValue = "true";

		// Token: 0x040009CD RID: 2509
		internal const string ODataValuePropertyName = "value";

		// Token: 0x040009CE RID: 2510
		internal const string ODataErrorPropertyName = "error";

		// Token: 0x040009CF RID: 2511
		internal const string ODataSourcePropertyName = "source";

		// Token: 0x040009D0 RID: 2512
		internal const string ODataTargetPropertyName = "target";

		// Token: 0x040009D1 RID: 2513
		internal const string ODataRelationshipPropertyName = "relationship";

		// Token: 0x040009D2 RID: 2514
		internal const string ODataIdPropertyName = "id";

		// Token: 0x040009D3 RID: 2515
		internal const string ODataReasonPropertyName = "reason";

		// Token: 0x040009D4 RID: 2516
		internal const string ODataReasonChangedValue = "changed";

		// Token: 0x040009D5 RID: 2517
		internal const string ODataReasonDeletedValue = "deleted";

		// Token: 0x040009D6 RID: 2518
		internal const string ODataServiceDocumentElementUrlName = "url";

		// Token: 0x040009D7 RID: 2519
		internal const string ODataServiceDocumentElementTitle = "title";

		// Token: 0x040009D8 RID: 2520
		internal const string ODataServiceDocumentElementKind = "kind";

		// Token: 0x040009D9 RID: 2521
		internal const string ODataServiceDocumentElementName = "name";

		// Token: 0x040009DA RID: 2522
		internal const string ContextUriSelectQueryOptionName = "$select";

		// Token: 0x040009DB RID: 2523
		internal const char ContextUriQueryOptionValueSeparator = '=';

		// Token: 0x040009DC RID: 2524
		internal const char ContextUriQueryOptionSeparator = '&';

		// Token: 0x040009DD RID: 2525
		internal const char FunctionParameterStart = '(';

		// Token: 0x040009DE RID: 2526
		internal const char FunctionParameterEnd = ')';

		// Token: 0x040009DF RID: 2527
		internal const string FunctionParameterSeparator = ",";

		// Token: 0x040009E0 RID: 2528
		internal const char FunctionParameterSeparatorChar = ',';

		// Token: 0x040009E1 RID: 2529
		internal const string FunctionParameterAssignment = "=@";

		// Token: 0x040009E2 RID: 2530
		internal const string ServiceDocumentSingletonKindName = "Singleton";

		// Token: 0x040009E3 RID: 2531
		internal const string ServiceDocumentFunctionImportKindName = "FunctionImport";

		// Token: 0x040009E4 RID: 2532
		internal const string ServiceDocumentEntitySetKindName = "EntitySet";

		// Token: 0x040009E5 RID: 2533
		internal const string SimplifiedODataContextPropertyName = "@context";

		// Token: 0x040009E6 RID: 2534
		internal const string SimplifiedODataTypePropertyName = "@type";
	}
}
