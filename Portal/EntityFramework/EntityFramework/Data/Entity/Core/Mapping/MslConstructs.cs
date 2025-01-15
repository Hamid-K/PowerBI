using System;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000551 RID: 1361
	internal static class MslConstructs
	{
		// Token: 0x060042BF RID: 17087 RVA: 0x000E5A34 File Offset: 0x000E3C34
		public static string GetMslNamespace(double version)
		{
			if (object.Equals(version, 1.0))
			{
				return "urn:schemas-microsoft-com:windows:storage:mapping:CS";
			}
			if (object.Equals(version, 2.0))
			{
				return "http://schemas.microsoft.com/ado/2008/09/mapping/cs";
			}
			return "http://schemas.microsoft.com/ado/2009/11/mapping/cs";
		}

		// Token: 0x04001783 RID: 6019
		internal const string NamespaceUriV1 = "urn:schemas-microsoft-com:windows:storage:mapping:CS";

		// Token: 0x04001784 RID: 6020
		internal const string NamespaceUriV2 = "http://schemas.microsoft.com/ado/2008/09/mapping/cs";

		// Token: 0x04001785 RID: 6021
		internal const string NamespaceUriV3 = "http://schemas.microsoft.com/ado/2009/11/mapping/cs";

		// Token: 0x04001786 RID: 6022
		internal const double MappingVersionV1 = 1.0;

		// Token: 0x04001787 RID: 6023
		internal const double MappingVersionV2 = 2.0;

		// Token: 0x04001788 RID: 6024
		internal const double MappingVersionV3 = 3.0;

		// Token: 0x04001789 RID: 6025
		internal const string MappingElement = "Mapping";

		// Token: 0x0400178A RID: 6026
		internal const string GenerateUpdateViews = "GenerateUpdateViews";

		// Token: 0x0400178B RID: 6027
		internal const string MappingSpaceAttribute = "Space";

		// Token: 0x0400178C RID: 6028
		internal const string EntityContainerMappingElement = "EntityContainerMapping";

		// Token: 0x0400178D RID: 6029
		internal const string CdmEntityContainerAttribute = "CdmEntityContainer";

		// Token: 0x0400178E RID: 6030
		internal const string StorageEntityContainerAttribute = "StorageEntityContainer";

		// Token: 0x0400178F RID: 6031
		internal const string AliasElement = "Alias";

		// Token: 0x04001790 RID: 6032
		internal const string AliasKeyAttribute = "Key";

		// Token: 0x04001791 RID: 6033
		internal const string AliasValueAttribute = "Value";

		// Token: 0x04001792 RID: 6034
		internal const string EntitySetMappingElement = "EntitySetMapping";

		// Token: 0x04001793 RID: 6035
		internal const string EntitySetMappingNameAttribute = "Name";

		// Token: 0x04001794 RID: 6036
		internal const string EntitySetMappingTypeNameAttribute = "TypeName";

		// Token: 0x04001795 RID: 6037
		internal const string EntitySetMappingStoreEntitySetAttribute = "StoreEntitySet";

		// Token: 0x04001796 RID: 6038
		internal const string EntityTypeMappingElement = "EntityTypeMapping";

		// Token: 0x04001797 RID: 6039
		internal const string QueryViewElement = "QueryView";

		// Token: 0x04001798 RID: 6040
		internal const string EntityTypeMappingTypeNameAttribute = "TypeName";

		// Token: 0x04001799 RID: 6041
		internal const string EntityTypeMappingStoreEntitySetAttribute = "StoreEntitySet";

		// Token: 0x0400179A RID: 6042
		internal const string AssociationSetMappingElement = "AssociationSetMapping";

		// Token: 0x0400179B RID: 6043
		internal const string AssociationSetMappingNameAttribute = "Name";

		// Token: 0x0400179C RID: 6044
		internal const string AssociationSetMappingTypeNameAttribute = "TypeName";

		// Token: 0x0400179D RID: 6045
		internal const string AssociationSetMappingStoreEntitySetAttribute = "StoreEntitySet";

		// Token: 0x0400179E RID: 6046
		internal const string EndPropertyMappingElement = "EndProperty";

		// Token: 0x0400179F RID: 6047
		internal const string EndPropertyMappingNameAttribute = "Name";

		// Token: 0x040017A0 RID: 6048
		internal const string CompositionSetMappingNameAttribute = "Name";

		// Token: 0x040017A1 RID: 6049
		internal const string CompositionSetMappingTypeNameAttribute = "TypeName";

		// Token: 0x040017A2 RID: 6050
		internal const string CompositionSetMappingStoreEntitySetAttribute = "StoreEntitySet";

		// Token: 0x040017A3 RID: 6051
		internal const string FunctionImportMappingElement = "FunctionImportMapping";

		// Token: 0x040017A4 RID: 6052
		internal const string FunctionImportMappingFunctionNameAttribute = "FunctionName";

		// Token: 0x040017A5 RID: 6053
		internal const string FunctionImportMappingFunctionImportNameAttribute = "FunctionImportName";

		// Token: 0x040017A6 RID: 6054
		internal const string CompositionSetParentEndName = "Parent";

		// Token: 0x040017A7 RID: 6055
		internal const string CompositionSetChildEndName = "Child";

		// Token: 0x040017A8 RID: 6056
		internal const string MappingFragmentElement = "MappingFragment";

		// Token: 0x040017A9 RID: 6057
		internal const string MappingFragmentStoreEntitySetAttribute = "StoreEntitySet";

		// Token: 0x040017AA RID: 6058
		internal const string MappingFragmentMakeColumnsDistinctAttribute = "MakeColumnsDistinct";

		// Token: 0x040017AB RID: 6059
		internal const string ScalarPropertyElement = "ScalarProperty";

		// Token: 0x040017AC RID: 6060
		internal const string ScalarPropertyNameAttribute = "Name";

		// Token: 0x040017AD RID: 6061
		internal const string ScalarPropertyColumnNameAttribute = "ColumnName";

		// Token: 0x040017AE RID: 6062
		internal const string ScalarPropertyValueAttribute = "Value";

		// Token: 0x040017AF RID: 6063
		internal const string ComplexPropertyElement = "ComplexProperty";

		// Token: 0x040017B0 RID: 6064
		internal const string AssociationEndElement = "AssociationEnd";

		// Token: 0x040017B1 RID: 6065
		internal const string ComplexPropertyNameAttribute = "Name";

		// Token: 0x040017B2 RID: 6066
		internal const string ComplexPropertyTypeNameAttribute = "TypeName";

		// Token: 0x040017B3 RID: 6067
		internal const string ComplexPropertyIsPartialAttribute = "IsPartial";

		// Token: 0x040017B4 RID: 6068
		internal const string ComplexTypeMappingElement = "ComplexTypeMapping";

		// Token: 0x040017B5 RID: 6069
		internal const string ComplexTypeMappingTypeNameAttribute = "TypeName";

		// Token: 0x040017B6 RID: 6070
		internal const string ConditionElement = "Condition";

		// Token: 0x040017B7 RID: 6071
		internal const string ConditionNameAttribute = "Name";

		// Token: 0x040017B8 RID: 6072
		internal const string ConditionValueAttribute = "Value";

		// Token: 0x040017B9 RID: 6073
		internal const string ConditionColumnNameAttribute = "ColumnName";

		// Token: 0x040017BA RID: 6074
		internal const string ConditionIsNullAttribute = "IsNull";

		// Token: 0x040017BB RID: 6075
		internal const string CollectionPropertyNameAttribute = "Name";

		// Token: 0x040017BC RID: 6076
		internal const string CollectionPropertyIsPartialAttribute = "IsPartial";

		// Token: 0x040017BD RID: 6077
		internal const string ResourceXsdNameV1 = "System.Data.Resources.CSMSL_1.xsd";

		// Token: 0x040017BE RID: 6078
		internal const string ResourceXsdNameV2 = "System.Data.Resources.CSMSL_2.xsd";

		// Token: 0x040017BF RID: 6079
		internal const string ResourceXsdNameV3 = "System.Data.Resources.CSMSL_3.xsd";

		// Token: 0x040017C0 RID: 6080
		internal const string IsTypeOf = "IsTypeOf(";

		// Token: 0x040017C1 RID: 6081
		internal const string IsTypeOfTerminal = ")";

		// Token: 0x040017C2 RID: 6082
		internal const string IsTypeOfOnly = "IsTypeOfOnly(";

		// Token: 0x040017C3 RID: 6083
		internal const string IsTypeOfOnlyTerminal = ")";

		// Token: 0x040017C4 RID: 6084
		internal const string ModificationFunctionMappingElement = "ModificationFunctionMapping";

		// Token: 0x040017C5 RID: 6085
		internal const string DeleteFunctionElement = "DeleteFunction";

		// Token: 0x040017C6 RID: 6086
		internal const string InsertFunctionElement = "InsertFunction";

		// Token: 0x040017C7 RID: 6087
		internal const string UpdateFunctionElement = "UpdateFunction";

		// Token: 0x040017C8 RID: 6088
		internal const string FunctionNameAttribute = "FunctionName";

		// Token: 0x040017C9 RID: 6089
		internal const string RowsAffectedParameterAttribute = "RowsAffectedParameter";

		// Token: 0x040017CA RID: 6090
		internal const string ParameterNameAttribute = "ParameterName";

		// Token: 0x040017CB RID: 6091
		internal const string ParameterVersionAttribute = "Version";

		// Token: 0x040017CC RID: 6092
		internal const string ParameterVersionAttributeCurrentValue = "Current";

		// Token: 0x040017CD RID: 6093
		internal const string ParameterVersionAttributeOriginalValue = "Original";

		// Token: 0x040017CE RID: 6094
		internal const string AssociationSetAttribute = "AssociationSet";

		// Token: 0x040017CF RID: 6095
		internal const string FromAttribute = "From";

		// Token: 0x040017D0 RID: 6096
		internal const string ToAttribute = "To";

		// Token: 0x040017D1 RID: 6097
		internal const string ResultBindingElement = "ResultBinding";

		// Token: 0x040017D2 RID: 6098
		internal const string ResultBindingPropertyNameAttribute = "Name";

		// Token: 0x040017D3 RID: 6099
		internal const string ResultBindingColumnNameAttribute = "ColumnName";

		// Token: 0x040017D4 RID: 6100
		internal const char TypeNameSeperator = ';';

		// Token: 0x040017D5 RID: 6101
		internal const char IdentitySeperator = ':';

		// Token: 0x040017D6 RID: 6102
		internal const string EntityViewGenerationTypeName = "Edm_EntityMappingGeneratedViews.ViewsForBaseEntitySets";

		// Token: 0x040017D7 RID: 6103
		internal const string FunctionImportMappingResultMapping = "ResultMapping";
	}
}
