using System;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x020000DB RID: 219
	public enum EdmErrorCode
	{
		// Token: 0x0400030E RID: 782
		InvalidErrorCodeValue,
		// Token: 0x0400030F RID: 783
		XmlError = 5,
		// Token: 0x04000310 RID: 784
		UnexpectedXmlNodeType = 8,
		// Token: 0x04000311 RID: 785
		UnexpectedXmlAttribute,
		// Token: 0x04000312 RID: 786
		UnexpectedXmlElement,
		// Token: 0x04000313 RID: 787
		TextNotAllowed,
		// Token: 0x04000314 RID: 788
		EmptyFile,
		// Token: 0x04000315 RID: 789
		MissingAttribute = 15,
		// Token: 0x04000316 RID: 790
		InvalidName = 17,
		// Token: 0x04000317 RID: 791
		MissingType,
		// Token: 0x04000318 RID: 792
		AlreadyDefined,
		// Token: 0x04000319 RID: 793
		InvalidVersionNumber = 25,
		// Token: 0x0400031A RID: 794
		InvalidBoolean = 27,
		// Token: 0x0400031B RID: 795
		BadProperty = 42,
		// Token: 0x0400031C RID: 796
		InvalidPropertyType = 44,
		// Token: 0x0400031D RID: 797
		PrecisionOutOfRange = 51,
		// Token: 0x0400031E RID: 798
		ScaleOutOfRange,
		// Token: 0x0400031F RID: 799
		NameTooLong = 60,
		// Token: 0x04000320 RID: 800
		InvalidAssociation = 62,
		// Token: 0x04000321 RID: 801
		BadNavigationProperty = 74,
		// Token: 0x04000322 RID: 802
		InvalidKey,
		// Token: 0x04000323 RID: 803
		InterfaceCriticalPropertyValueMustNotBeNull,
		// Token: 0x04000324 RID: 804
		InterfaceCriticalKindValueMismatch,
		// Token: 0x04000325 RID: 805
		InterfaceCriticalKindValueUnexpected,
		// Token: 0x04000326 RID: 806
		InterfaceCriticalEnumerableMustNotHaveNullElements,
		// Token: 0x04000327 RID: 807
		InterfaceCriticalEnumPropertyValueOutOfRange,
		// Token: 0x04000328 RID: 808
		InterfaceCriticalNavigationPartnerInvalid,
		// Token: 0x04000329 RID: 809
		InterfaceCriticalCycleInTypeHierarchy,
		// Token: 0x0400032A RID: 810
		InvalidMultiplicity = 92,
		// Token: 0x0400032B RID: 811
		InvalidAction = 96,
		// Token: 0x0400032C RID: 812
		InvalidOnDelete,
		// Token: 0x0400032D RID: 813
		BadUnresolvedComplexType,
		// Token: 0x0400032E RID: 814
		InvalidEndEntitySet = 100,
		// Token: 0x0400032F RID: 815
		OperationImportEntitySetExpressionIsInvalid = 103,
		// Token: 0x04000330 RID: 816
		NavigationPropertyMappingMustPointToValidTargetForProperty = 109,
		// Token: 0x04000331 RID: 817
		InvalidRoleInRelationshipConstraint,
		// Token: 0x04000332 RID: 818
		InvalidPropertyInRelationshipConstraint,
		// Token: 0x04000333 RID: 819
		TypeMismatchRelationshipConstraint,
		// Token: 0x04000334 RID: 820
		InvalidMultiplicityOfPrincipalEnd,
		// Token: 0x04000335 RID: 821
		MismatchNumberOfPropertiesInRelationshipConstraint,
		// Token: 0x04000336 RID: 822
		InvalidMultiplicityOfDependentEnd = 116,
		// Token: 0x04000337 RID: 823
		OpenTypeNotSupported,
		// Token: 0x04000338 RID: 824
		SameRoleReferredInReferentialConstraint = 119,
		// Token: 0x04000339 RID: 825
		EntityKeyMustBeScalar = 128,
		// Token: 0x0400033A RID: 826
		EntityKeyMustNotBeBinary,
		// Token: 0x0400033B RID: 827
		EndWithManyMultiplicityCannotHaveOperationsSpecified = 132,
		// Token: 0x0400033C RID: 828
		NavigationSourceTypeHasNoKeys,
		// Token: 0x0400033D RID: 829
		ConcurrencyRedefinedOnSubtypeOfEntitySetType = 145,
		// Token: 0x0400033E RID: 830
		OperationImportUnsupportedReturnType,
		// Token: 0x0400033F RID: 831
		OperationImportReturnsEntitiesButDoesNotSpecifyEntitySet = 148,
		// Token: 0x04000340 RID: 832
		OperationImportEntityTypeDoesNotMatchEntitySet,
		// Token: 0x04000341 RID: 833
		OperationImportSpecifiesEntitySetButDoesNotReturnEntityType,
		// Token: 0x04000342 RID: 834
		OperationImportCannotImportBoundOperation,
		// Token: 0x04000343 RID: 835
		FunctionMustHaveReturnType,
		// Token: 0x04000344 RID: 836
		SimilarRelationshipEnd,
		// Token: 0x04000345 RID: 837
		DuplicatePropertySpecifiedInEntityKey,
		// Token: 0x04000346 RID: 838
		NullableComplexTypeProperty = 157,
		// Token: 0x04000347 RID: 839
		KeyMissingOnEntityType = 159,
		// Token: 0x04000348 RID: 840
		SystemNamespaceEncountered = 161,
		// Token: 0x04000349 RID: 841
		InvalidNamespaceName = 163,
		// Token: 0x0400034A RID: 842
		EnumMemberMustHaveValue = 206,
		// Token: 0x0400034B RID: 843
		DuplicateEntityContainerMemberName = 218,
		// Token: 0x0400034C RID: 844
		UnboundFunctionOverloadHasIncorrectReturnType,
		// Token: 0x0400034D RID: 845
		InvalidAbstractComplexType,
		// Token: 0x0400034E RID: 846
		InvalidPolymorphicComplexType,
		// Token: 0x0400034F RID: 847
		NavigationPropertyEntityMustNotIndirectlyContainItself,
		// Token: 0x04000350 RID: 848
		EntitySetRecursiveNavigationPropertyMappingsMustPointBackToSourceEntitySet,
		// Token: 0x04000351 RID: 849
		BadAmbiguousElementBinding,
		// Token: 0x04000352 RID: 850
		BadUnresolvedType,
		// Token: 0x04000353 RID: 851
		BadUnresolvedPrimitiveType,
		// Token: 0x04000354 RID: 852
		BadCyclicComplex,
		// Token: 0x04000355 RID: 853
		BadCyclicEntityContainer,
		// Token: 0x04000356 RID: 854
		BadCyclicEntity,
		// Token: 0x04000357 RID: 855
		TypeSemanticsCouldNotConvertTypeReference,
		// Token: 0x04000358 RID: 856
		ConstructibleEntitySetTypeInvalidFromEntityTypeRemoval,
		// Token: 0x04000359 RID: 857
		BadUnresolvedEntityContainer,
		// Token: 0x0400035A RID: 858
		BadUnresolvedEntitySet,
		// Token: 0x0400035B RID: 859
		BadUnresolvedProperty,
		// Token: 0x0400035C RID: 860
		BadNonComputableAssociationEnd,
		// Token: 0x0400035D RID: 861
		NavigationPropertyTypeInvalidBecauseOfBadAssociation,
		// Token: 0x0400035E RID: 862
		EntityMustHaveEntityBaseType,
		// Token: 0x0400035F RID: 863
		ComplexTypeMustHaveComplexBaseType,
		// Token: 0x04000360 RID: 864
		BadUnresolvedOperation,
		// Token: 0x04000361 RID: 865
		KeyPropertyMustBelongToEntity = 242,
		// Token: 0x04000362 RID: 866
		ReferentialConstraintPrincipalEndMustBelongToAssociation,
		// Token: 0x04000363 RID: 867
		DependentPropertiesMustBelongToDependentEntity,
		// Token: 0x04000364 RID: 868
		DeclaringTypeMustBeCorrect,
		// Token: 0x04000365 RID: 869
		InvalidNavigationPropertyType = 258,
		// Token: 0x04000366 RID: 870
		UnderlyingTypeIsBadBecauseEnumTypeIsBad = 261,
		// Token: 0x04000367 RID: 871
		ComplexTypeMustHaveProperties = 264,
		// Token: 0x04000368 RID: 872
		OperationImportParameterIncorrectType,
		// Token: 0x04000369 RID: 873
		DuplicateDependentProperty = 267,
		// Token: 0x0400036A RID: 874
		BoundOperationMustHaveParameters,
		// Token: 0x0400036B RID: 875
		OperationCannotHaveEntitySetPathWithUnBoundOperation,
		// Token: 0x0400036C RID: 876
		InvalidPathFirstPathParameterNotMatchingFirstParameterName = 271,
		// Token: 0x0400036D RID: 877
		InvalidPathWithNonEntityBindingParameter = 246,
		// Token: 0x0400036E RID: 878
		OperationWithInvalidEntitySetPathMissingCompletePath = 248,
		// Token: 0x0400036F RID: 879
		InvalidPathUnknownTypeCastSegment,
		// Token: 0x04000370 RID: 880
		InvalidPathInvalidTypeCastSegment,
		// Token: 0x04000371 RID: 881
		InvalidPathTypeCastSegmentMustBeEntityType,
		// Token: 0x04000372 RID: 882
		InvalidPathUnknownNavigationProperty,
		// Token: 0x04000373 RID: 883
		OperationWithEntitySetPathAndReturnTypeTypeNotAssignable,
		// Token: 0x04000374 RID: 884
		OperationWithEntitySetPathResolvesToCollectionEntityTypeMismatchesEntityTypeReturnType,
		// Token: 0x04000375 RID: 885
		OperationWithEntitySetPathResolvesToEntityTypeMismatchesCollectionEntityTypeReturnType,
		// Token: 0x04000376 RID: 886
		OperationWithEntitySetPathReturnTypeInvalid,
		// Token: 0x04000377 RID: 887
		MaxLengthOutOfRange = 272,
		// Token: 0x04000378 RID: 888
		PathExpressionHasNoEntityContext = 274,
		// Token: 0x04000379 RID: 889
		InvalidSrid,
		// Token: 0x0400037A RID: 890
		InvalidMaxLength,
		// Token: 0x0400037B RID: 891
		InvalidLong,
		// Token: 0x0400037C RID: 892
		InvalidInteger,
		// Token: 0x0400037D RID: 893
		InvalidAssociationSet,
		// Token: 0x0400037E RID: 894
		InvalidParameterMode,
		// Token: 0x0400037F RID: 895
		BadUnresolvedEntityType,
		// Token: 0x04000380 RID: 896
		InvalidValue,
		// Token: 0x04000381 RID: 897
		InvalidBinary,
		// Token: 0x04000382 RID: 898
		InvalidFloatingPoint,
		// Token: 0x04000383 RID: 899
		InvalidDateTime,
		// Token: 0x04000384 RID: 900
		InvalidDateTimeOffset,
		// Token: 0x04000385 RID: 901
		InvalidDecimal,
		// Token: 0x04000386 RID: 902
		InvalidGuid,
		// Token: 0x04000387 RID: 903
		InvalidTypeKindNone,
		// Token: 0x04000388 RID: 904
		InvalidIfExpressionIncorrectNumberOfOperands,
		// Token: 0x04000389 RID: 905
		EnumMemberValueOutOfRange = 292,
		// Token: 0x0400038A RID: 906
		InvalidIsTypeExpressionIncorrectNumberOfOperands,
		// Token: 0x0400038B RID: 907
		InvalidTypeName,
		// Token: 0x0400038C RID: 908
		InvalidQualifiedName,
		// Token: 0x0400038D RID: 909
		NoReadersProvided,
		// Token: 0x0400038E RID: 910
		NullXmlReader,
		// Token: 0x0400038F RID: 911
		IsUnboundedCannotBeTrueWhileMaxLengthIsNotNull,
		// Token: 0x04000390 RID: 912
		InvalidElementAnnotation,
		// Token: 0x04000391 RID: 913
		InvalidLabeledElementExpressionIncorrectNumberOfOperands,
		// Token: 0x04000392 RID: 914
		BadUnresolvedLabeledElement,
		// Token: 0x04000393 RID: 915
		BadUnresolvedEnumMember,
		// Token: 0x04000394 RID: 916
		InvalidCastExpressionIncorrectNumberOfOperands,
		// Token: 0x04000395 RID: 917
		BadUnresolvedParameter,
		// Token: 0x04000396 RID: 918
		NavigationPropertyWithRecursiveContainmentTargetMustBeOptional,
		// Token: 0x04000397 RID: 919
		NavigationPropertyWithRecursiveContainmentSourceMustBeFromZeroOrOne,
		// Token: 0x04000398 RID: 920
		NavigationPropertyWithNonRecursiveContainmentSourceMustBeFromOne,
		// Token: 0x04000399 RID: 921
		ImpossibleAnnotationsTarget = 309,
		// Token: 0x0400039A RID: 922
		CannotAssertNullableTypeAsNonNullableType,
		// Token: 0x0400039B RID: 923
		CannotAssertPrimitiveExpressionAsNonPrimitiveType,
		// Token: 0x0400039C RID: 924
		ExpressionPrimitiveKindNotValidForAssertedType,
		// Token: 0x0400039D RID: 925
		NullCannotBeAssertedToBeANonNullableType,
		// Token: 0x0400039E RID: 926
		ExpressionNotValidForTheAssertedType,
		// Token: 0x0400039F RID: 927
		CollectionExpressionNotValidForNonCollectionType,
		// Token: 0x040003A0 RID: 928
		RecordExpressionNotValidForNonStructuredType,
		// Token: 0x040003A1 RID: 929
		RecordExpressionMissingRequiredProperty,
		// Token: 0x040003A2 RID: 930
		RecordExpressionHasExtraProperties,
		// Token: 0x040003A3 RID: 931
		DuplicateAnnotation,
		// Token: 0x040003A4 RID: 932
		IncorrectNumberOfArguments,
		// Token: 0x040003A5 RID: 933
		DuplicateAlias,
		// Token: 0x040003A6 RID: 934
		ReferencedTypeMustHaveValidName,
		// Token: 0x040003A7 RID: 935
		SingleFileExpected,
		// Token: 0x040003A8 RID: 936
		UnknownEdmxVersion,
		// Token: 0x040003A9 RID: 937
		UnknownEdmVersion,
		// Token: 0x040003AA RID: 938
		NoSchemasProduced,
		// Token: 0x040003AB RID: 939
		DuplicateEntityContainerName,
		// Token: 0x040003AC RID: 940
		ContainerElementContainerNameIncorrect,
		// Token: 0x040003AD RID: 941
		PrimitiveConstantExpressionNotValidForNonPrimitiveType,
		// Token: 0x040003AE RID: 942
		IntegerConstantValueOutOfRange,
		// Token: 0x040003AF RID: 943
		StringConstantLengthOutOfRange,
		// Token: 0x040003B0 RID: 944
		BinaryConstantLengthOutOfRange,
		// Token: 0x040003B1 RID: 945
		InvalidOperationImportParameterMode,
		// Token: 0x040003B2 RID: 946
		TypeMustNotHaveKindOfNone,
		// Token: 0x040003B3 RID: 947
		PrimitiveTypeMustNotHaveKindOfNone,
		// Token: 0x040003B4 RID: 948
		PropertyMustNotHaveKindOfNone,
		// Token: 0x040003B5 RID: 949
		SchemaElementMustNotHaveKindOfNone = 338,
		// Token: 0x040003B6 RID: 950
		EntityContainerElementMustNotHaveKindOfNone,
		// Token: 0x040003B7 RID: 951
		BinaryValueCannotHaveEmptyValue,
		// Token: 0x040003B8 RID: 952
		EntitySetCanOnlyBeContainedByASingleNavigationProperty,
		// Token: 0x040003B9 RID: 953
		InconsistentNavigationPropertyPartner,
		// Token: 0x040003BA RID: 954
		EntitySetCanOnlyHaveSingleNavigationPropertyWithContainment,
		// Token: 0x040003BB RID: 955
		NavigationMappingMustBeBidirectional,
		// Token: 0x040003BC RID: 956
		DuplicateNavigationPropertyMapping,
		// Token: 0x040003BD RID: 957
		AllNavigationPropertiesMustBeMapped,
		// Token: 0x040003BE RID: 958
		TypeAnnotationMissingRequiredProperty,
		// Token: 0x040003BF RID: 959
		TypeAnnotationHasExtraProperties,
		// Token: 0x040003C0 RID: 960
		InvalidDuration,
		// Token: 0x040003C1 RID: 961
		InvalidPrimitiveValue,
		// Token: 0x040003C2 RID: 962
		EnumMustHaveIntegerUnderlyingType,
		// Token: 0x040003C3 RID: 963
		BadUnresolvedTerm,
		// Token: 0x040003C4 RID: 964
		BadPrincipalPropertiesInReferentialConstraint,
		// Token: 0x040003C5 RID: 965
		DuplicateDirectValueAnnotationFullName,
		// Token: 0x040003C6 RID: 966
		NoEntitySetsFoundForType,
		// Token: 0x040003C7 RID: 967
		CannotInferEntitySetWithMultipleSetsPerType,
		// Token: 0x040003C8 RID: 968
		InvalidEntitySetPath,
		// Token: 0x040003C9 RID: 969
		InvalidEnumMemberPath,
		// Token: 0x040003CA RID: 970
		QualifierMustBeSimpleName,
		// Token: 0x040003CB RID: 971
		BadUnresolvedEnumType,
		// Token: 0x040003CC RID: 972
		BadUnresolvedTarget,
		// Token: 0x040003CD RID: 973
		PathIsNotValidForTheGivenContext,
		// Token: 0x040003CE RID: 974
		BadUnresolvedNavigationPropertyPath,
		// Token: 0x040003CF RID: 975
		NavigationPropertyWithCollectionTypeCannotHaveNullableAttribute,
		// Token: 0x040003D0 RID: 976
		MetadataDocumentCannotHaveMoreThanOneEntityContainer,
		// Token: 0x040003D1 RID: 977
		DuplicateFunctions,
		// Token: 0x040003D2 RID: 978
		DuplicateActions,
		// Token: 0x040003D3 RID: 979
		BoundFunctionOverloadsMustHaveSameReturnType,
		// Token: 0x040003D4 RID: 980
		SingletonTypeMustBeEntityType,
		// Token: 0x040003D5 RID: 981
		EntitySetTypeMustBeCollectionOfEntityType,
		// Token: 0x040003D6 RID: 982
		NavigationPropertyOfCollectionTypeMustNotTargetToSingleton,
		// Token: 0x040003D7 RID: 983
		ReferenceElementMustContainAtLeastOneIncludeOrIncludeAnnotationsElement,
		// Token: 0x040003D8 RID: 984
		FunctionImportWithParameterShouldNotBeIncludedInServiceDocument,
		// Token: 0x040003D9 RID: 985
		UnresolvedReferenceUriInEdmxReference,
		// Token: 0x040003DA RID: 986
		InvalidDate,
		// Token: 0x040003DB RID: 987
		InvalidTimeOfDay,
		// Token: 0x040003DC RID: 988
		UnresolvedNavigationPropertyPartnerPath,
		// Token: 0x040003DD RID: 989
		UnresolvedNavigationPropertyBindingPath,
		// Token: 0x040003DE RID: 990
		RequiredParametersMustPrecedeOptional
	}
}
