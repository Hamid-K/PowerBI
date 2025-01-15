using System;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x0200022F RID: 559
	internal static class JsonLightConstants
	{
		// Token: 0x04000AE0 RID: 2784
		internal const string ODataAnnotationNamespacePrefix = "odata.";

		// Token: 0x04000AE1 RID: 2785
		internal const char ODataPropertyAnnotationSeparatorChar = '@';

		// Token: 0x04000AE2 RID: 2786
		internal const string ODataNullPropertyName = "null";

		// Token: 0x04000AE3 RID: 2787
		internal const string ODataNullAnnotationTrueValue = "true";

		// Token: 0x04000AE4 RID: 2788
		internal const string ODataValuePropertyName = "value";

		// Token: 0x04000AE5 RID: 2789
		internal const string ODataErrorPropertyName = "error";

		// Token: 0x04000AE6 RID: 2790
		internal const string ODataSourcePropertyName = "source";

		// Token: 0x04000AE7 RID: 2791
		internal const string ODataTargetPropertyName = "target";

		// Token: 0x04000AE8 RID: 2792
		internal const string ODataRelationshipPropertyName = "relationship";

		// Token: 0x04000AE9 RID: 2793
		internal const string ODataIdPropertyName = "id";

		// Token: 0x04000AEA RID: 2794
		internal const string ODataDeltaPropertyName = "delta";

		// Token: 0x04000AEB RID: 2795
		internal const string ODataReasonPropertyName = "reason";

		// Token: 0x04000AEC RID: 2796
		internal const string ODataReasonChangedValue = "changed";

		// Token: 0x04000AED RID: 2797
		internal const string ODataReasonDeletedValue = "deleted";

		// Token: 0x04000AEE RID: 2798
		internal const string ODataServiceDocumentElementUrlName = "url";

		// Token: 0x04000AEF RID: 2799
		internal const string ODataServiceDocumentElementTitle = "title";

		// Token: 0x04000AF0 RID: 2800
		internal const string ODataServiceDocumentElementKind = "kind";

		// Token: 0x04000AF1 RID: 2801
		internal const string ODataServiceDocumentElementName = "name";

		// Token: 0x04000AF2 RID: 2802
		internal const string ContextUriSelectQueryOptionName = "$select";

		// Token: 0x04000AF3 RID: 2803
		internal const char ContextUriQueryOptionValueSeparator = '=';

		// Token: 0x04000AF4 RID: 2804
		internal const char ContextUriQueryOptionSeparator = '&';

		// Token: 0x04000AF5 RID: 2805
		internal const char FunctionParameterStart = '(';

		// Token: 0x04000AF6 RID: 2806
		internal const char FunctionParameterEnd = ')';

		// Token: 0x04000AF7 RID: 2807
		internal const string FunctionParameterSeparator = ",";

		// Token: 0x04000AF8 RID: 2808
		internal const char FunctionParameterSeparatorChar = ',';

		// Token: 0x04000AF9 RID: 2809
		internal const string FunctionParameterAssignment = "=@";

		// Token: 0x04000AFA RID: 2810
		internal const string ServiceDocumentSingletonKindName = "Singleton";

		// Token: 0x04000AFB RID: 2811
		internal const string ServiceDocumentFunctionImportKindName = "FunctionImport";

		// Token: 0x04000AFC RID: 2812
		internal const string ServiceDocumentEntitySetKindName = "EntitySet";

		// Token: 0x04000AFD RID: 2813
		internal const string SimplifiedODataContextPropertyName = "@context";

		// Token: 0x04000AFE RID: 2814
		internal const string SimplifiedODataIdPropertyName = "@id";

		// Token: 0x04000AFF RID: 2815
		internal const string SimplifiedODataTypePropertyName = "@type";

		// Token: 0x04000B00 RID: 2816
		internal const string SimplifiedODataRemovedPropertyName = "@removed";
	}
}
