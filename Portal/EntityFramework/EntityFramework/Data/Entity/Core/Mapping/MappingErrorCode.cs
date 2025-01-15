using System;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000546 RID: 1350
	internal enum MappingErrorCode
	{
		// Token: 0x040016F1 RID: 5873
		Value = 2000,
		// Token: 0x040016F2 RID: 5874
		InvalidContent,
		// Token: 0x040016F3 RID: 5875
		InvalidEntityContainer,
		// Token: 0x040016F4 RID: 5876
		InvalidEntitySet,
		// Token: 0x040016F5 RID: 5877
		InvalidEntityType,
		// Token: 0x040016F6 RID: 5878
		InvalidAssociationSet,
		// Token: 0x040016F7 RID: 5879
		InvalidAssociationType,
		// Token: 0x040016F8 RID: 5880
		InvalidTable,
		// Token: 0x040016F9 RID: 5881
		InvalidComplexType,
		// Token: 0x040016FA RID: 5882
		InvalidEdmMember,
		// Token: 0x040016FB RID: 5883
		InvalidStorageMember,
		// Token: 0x040016FC RID: 5884
		TableMappingFragmentExpected,
		// Token: 0x040016FD RID: 5885
		SetMappingExpected,
		// Token: 0x040016FE RID: 5886
		DuplicateSetMapping = 2014,
		// Token: 0x040016FF RID: 5887
		DuplicateTypeMapping,
		// Token: 0x04001700 RID: 5888
		ConditionError,
		// Token: 0x04001701 RID: 5889
		RootMappingElementMissing = 2018,
		// Token: 0x04001702 RID: 5890
		IncompatibleMemberMapping,
		// Token: 0x04001703 RID: 5891
		InvalidEnumValue = 2023,
		// Token: 0x04001704 RID: 5892
		XmlSchemaParsingError,
		// Token: 0x04001705 RID: 5893
		XmlSchemaValidationError,
		// Token: 0x04001706 RID: 5894
		AmbiguousModificationFunctionMappingForAssociationSet,
		// Token: 0x04001707 RID: 5895
		MissingSetClosureInModificationFunctionMapping,
		// Token: 0x04001708 RID: 5896
		MissingModificationFunctionMappingForEntityType,
		// Token: 0x04001709 RID: 5897
		InvalidTableNameAttributeWithModificationFunctionMapping,
		// Token: 0x0400170A RID: 5898
		InvalidModificationFunctionMappingForMultipleTypes,
		// Token: 0x0400170B RID: 5899
		AmbiguousResultBindingInModificationFunctionMapping,
		// Token: 0x0400170C RID: 5900
		InvalidAssociationSetRoleInModificationFunctionMapping,
		// Token: 0x0400170D RID: 5901
		InvalidAssociationSetCardinalityInModificationFunctionMapping,
		// Token: 0x0400170E RID: 5902
		RedundantEntityTypeMappingInModificationFunctionMapping,
		// Token: 0x0400170F RID: 5903
		MissingVersionInModificationFunctionMapping,
		// Token: 0x04001710 RID: 5904
		InvalidVersionInModificationFunctionMapping,
		// Token: 0x04001711 RID: 5905
		InvalidParameterInModificationFunctionMapping,
		// Token: 0x04001712 RID: 5906
		ParameterBoundTwiceInModificationFunctionMapping,
		// Token: 0x04001713 RID: 5907
		CSpaceMemberMappedToMultipleSSpaceMemberWithDifferentTypes,
		// Token: 0x04001714 RID: 5908
		NoEquivalentStorePrimitiveTypeFound,
		// Token: 0x04001715 RID: 5909
		NoEquivalentStorePrimitiveTypeWithFacetsFound,
		// Token: 0x04001716 RID: 5910
		InvalidModificationFunctionMappingPropertyParameterTypeMismatch,
		// Token: 0x04001717 RID: 5911
		InvalidModificationFunctionMappingMultipleEndsOfAssociationMapped,
		// Token: 0x04001718 RID: 5912
		InvalidModificationFunctionMappingUnknownFunction,
		// Token: 0x04001719 RID: 5913
		InvalidModificationFunctionMappingAmbiguousFunction,
		// Token: 0x0400171A RID: 5914
		InvalidModificationFunctionMappingNotValidFunction,
		// Token: 0x0400171B RID: 5915
		InvalidModificationFunctionMappingNotValidFunctionParameter,
		// Token: 0x0400171C RID: 5916
		InvalidModificationFunctionMappingAssociationSetNotMappedForOperation,
		// Token: 0x0400171D RID: 5917
		InvalidModificationFunctionMappingAssociationEndMappingInvalidForEntityType,
		// Token: 0x0400171E RID: 5918
		MappingFunctionImportStoreFunctionDoesNotExist,
		// Token: 0x0400171F RID: 5919
		MappingFunctionImportStoreFunctionAmbiguous,
		// Token: 0x04001720 RID: 5920
		MappingFunctionImportFunctionImportDoesNotExist,
		// Token: 0x04001721 RID: 5921
		MappingFunctionImportFunctionImportMappedMultipleTimes,
		// Token: 0x04001722 RID: 5922
		MappingFunctionImportTargetFunctionMustBeNonComposable,
		// Token: 0x04001723 RID: 5923
		MappingFunctionImportTargetParameterHasNoCorrespondingImportParameter,
		// Token: 0x04001724 RID: 5924
		MappingFunctionImportImportParameterHasNoCorrespondingTargetParameter,
		// Token: 0x04001725 RID: 5925
		MappingFunctionImportIncompatibleParameterMode,
		// Token: 0x04001726 RID: 5926
		MappingFunctionImportIncompatibleParameterType,
		// Token: 0x04001727 RID: 5927
		MappingFunctionImportRowsAffectedParameterDoesNotExist,
		// Token: 0x04001728 RID: 5928
		MappingFunctionImportRowsAffectedParameterHasWrongType,
		// Token: 0x04001729 RID: 5929
		MappingFunctionImportRowsAffectedParameterHasWrongMode,
		// Token: 0x0400172A RID: 5930
		EmptyContainerMapping,
		// Token: 0x0400172B RID: 5931
		EmptySetMapping,
		// Token: 0x0400172C RID: 5932
		TableNameAttributeWithQueryView,
		// Token: 0x0400172D RID: 5933
		EmptyQueryView,
		// Token: 0x0400172E RID: 5934
		PropertyMapsWithQueryView,
		// Token: 0x0400172F RID: 5935
		MissingSetClosureInQueryViews,
		// Token: 0x04001730 RID: 5936
		InvalidQueryView,
		// Token: 0x04001731 RID: 5937
		InvalidQueryViewResultType,
		// Token: 0x04001732 RID: 5938
		ItemWithSameNameExistsBothInCSpaceAndSSpace,
		// Token: 0x04001733 RID: 5939
		MappingUnsupportedExpressionKindQueryView,
		// Token: 0x04001734 RID: 5940
		MappingUnsupportedScanTargetQueryView,
		// Token: 0x04001735 RID: 5941
		MappingUnsupportedPropertyKindQueryView,
		// Token: 0x04001736 RID: 5942
		MappingUnsupportedInitializationQueryView,
		// Token: 0x04001737 RID: 5943
		MappingFunctionImportEntityTypeMappingForFunctionNotReturningEntitySet,
		// Token: 0x04001738 RID: 5944
		MappingFunctionImportAmbiguousTypeConditions,
		// Token: 0x04001739 RID: 5945
		MappingOfAbstractType = 2078,
		// Token: 0x0400173A RID: 5946
		StorageEntityContainerNameMismatchWhileSpecifyingPartialMapping,
		// Token: 0x0400173B RID: 5947
		TypeNameForFirstQueryView,
		// Token: 0x0400173C RID: 5948
		NoTypeNameForTypeSpecificQueryView,
		// Token: 0x0400173D RID: 5949
		QueryViewExistsForEntitySetAndType,
		// Token: 0x0400173E RID: 5950
		TypeNameContainsMultipleTypesForQueryView,
		// Token: 0x0400173F RID: 5951
		IsTypeOfQueryViewForBaseType,
		// Token: 0x04001740 RID: 5952
		InvalidTypeInScalarProperty,
		// Token: 0x04001741 RID: 5953
		AlreadyMappedStorageEntityContainer,
		// Token: 0x04001742 RID: 5954
		UnsupportedQueryViewInEntityContainerMapping,
		// Token: 0x04001743 RID: 5955
		MappingAllQueryViewAtCompileTime,
		// Token: 0x04001744 RID: 5956
		MappingNoViewsCanBeGenerated,
		// Token: 0x04001745 RID: 5957
		MappingStoreProviderReturnsNullEdmType,
		// Token: 0x04001746 RID: 5958
		DuplicateMemberMapping = 2092,
		// Token: 0x04001747 RID: 5959
		MappingFunctionImportUnexpectedEntityTypeMapping,
		// Token: 0x04001748 RID: 5960
		MappingFunctionImportUnexpectedComplexTypeMapping,
		// Token: 0x04001749 RID: 5961
		DistinctFragmentInReadWriteContainer = 2096,
		// Token: 0x0400174A RID: 5962
		EntitySetMismatchOnAssociationSetEnd,
		// Token: 0x0400174B RID: 5963
		InvalidModificationFunctionMappingAssociationEndForeignKey,
		// Token: 0x0400174C RID: 5964
		CannotLoadDifferentVersionOfSchemaInTheSameItemCollection = 2100,
		// Token: 0x0400174D RID: 5965
		MappingDifferentMappingEdmStoreVersion,
		// Token: 0x0400174E RID: 5966
		MappingDifferentEdmStoreVersion,
		// Token: 0x0400174F RID: 5967
		UnmappedFunctionImport,
		// Token: 0x04001750 RID: 5968
		MappingFunctionImportReturnTypePropertyNotMapped,
		// Token: 0x04001751 RID: 5969
		InvalidType = 2106,
		// Token: 0x04001752 RID: 5970
		MappingFunctionImportTVFExpected = 2108,
		// Token: 0x04001753 RID: 5971
		MappingFunctionImportScalarMappingTypeMismatch,
		// Token: 0x04001754 RID: 5972
		MappingFunctionImportScalarMappingToMulticolumnTVF,
		// Token: 0x04001755 RID: 5973
		MappingFunctionImportTargetFunctionMustBeComposable,
		// Token: 0x04001756 RID: 5974
		UnsupportedFunctionCallInQueryView,
		// Token: 0x04001757 RID: 5975
		FunctionResultMappingCountMismatch,
		// Token: 0x04001758 RID: 5976
		MappingFunctionImportCannotInferTargetFunctionKeys
	}
}
