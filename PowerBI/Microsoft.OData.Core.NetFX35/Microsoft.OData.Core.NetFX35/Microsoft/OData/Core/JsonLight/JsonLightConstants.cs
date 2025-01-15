using System;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000B0 RID: 176
	internal static class JsonLightConstants
	{
		// Token: 0x040002DF RID: 735
		internal const string ODataAnnotationNamespacePrefix = "odata.";

		// Token: 0x040002E0 RID: 736
		internal const char ODataPropertyAnnotationSeparatorChar = '@';

		// Token: 0x040002E1 RID: 737
		internal const string ODataNullAnnotationTrueValue = "true";

		// Token: 0x040002E2 RID: 738
		internal const string ODataValuePropertyName = "value";

		// Token: 0x040002E3 RID: 739
		internal const string ODataErrorPropertyName = "error";

		// Token: 0x040002E4 RID: 740
		internal const string ODataSourcePropertyName = "source";

		// Token: 0x040002E5 RID: 741
		internal const string ODataTargetPropertyName = "target";

		// Token: 0x040002E6 RID: 742
		internal const string ODataRelationshipPropertyName = "relationship";

		// Token: 0x040002E7 RID: 743
		internal const string ODataIdPropertyName = "id";

		// Token: 0x040002E8 RID: 744
		internal const string ODataReasonPropertyName = "reason";

		// Token: 0x040002E9 RID: 745
		internal const string ODataReasonChangedValue = "changed";

		// Token: 0x040002EA RID: 746
		internal const string ODataReasonDeletedValue = "deleted";

		// Token: 0x040002EB RID: 747
		internal const string ODataServiceDocumentElementUrlName = "url";

		// Token: 0x040002EC RID: 748
		internal const string ODataServiceDocumentElementTitle = "title";

		// Token: 0x040002ED RID: 749
		internal const string ODataServiceDocumentElementKind = "kind";

		// Token: 0x040002EE RID: 750
		internal const string ODataServiceDocumentElementName = "name";

		// Token: 0x040002EF RID: 751
		internal const string ContextUriSelectQueryOptionName = "$select";

		// Token: 0x040002F0 RID: 752
		internal const char ContextUriQueryOptionValueSeparator = '=';

		// Token: 0x040002F1 RID: 753
		internal const char ContextUriQueryOptionSeparator = '&';

		// Token: 0x040002F2 RID: 754
		internal const char FunctionParameterStart = '(';

		// Token: 0x040002F3 RID: 755
		internal const char FunctionParameterEnd = ')';

		// Token: 0x040002F4 RID: 756
		internal const string FunctionParameterSeparator = ",";

		// Token: 0x040002F5 RID: 757
		internal const char FunctionParameterSeparatorChar = ',';

		// Token: 0x040002F6 RID: 758
		internal const string FunctionParameterAssignment = "=@";

		// Token: 0x040002F7 RID: 759
		internal const string ServiceDocumentSingletonKindName = "Singleton";

		// Token: 0x040002F8 RID: 760
		internal const string ServiceDocumentFunctionImportKindName = "FunctionImport";

		// Token: 0x040002F9 RID: 761
		internal const string ServiceDocumentEntitySetKindName = "EntitySet";

		// Token: 0x040002FA RID: 762
		internal const string SimplifiedODataContextPropertyName = "@context";

		// Token: 0x040002FB RID: 763
		internal const string SimplifiedODataTypePropertyName = "@type";
	}
}
