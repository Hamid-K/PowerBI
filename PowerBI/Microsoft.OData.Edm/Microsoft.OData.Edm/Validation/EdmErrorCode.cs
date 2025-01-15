using System;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x0200014A RID: 330
	public enum EdmErrorCode
	{
		// Token: 0x040003FE RID: 1022
		InvalidErrorCodeValue,
		// Token: 0x040003FF RID: 1023
		XmlError = 5,
		// Token: 0x04000400 RID: 1024
		UnexpectedXmlNodeType = 8,
		// Token: 0x04000401 RID: 1025
		UnexpectedXmlAttribute,
		// Token: 0x04000402 RID: 1026
		UnexpectedXmlElement,
		// Token: 0x04000403 RID: 1027
		TextNotAllowed,
		// Token: 0x04000404 RID: 1028
		EmptyFile,
		// Token: 0x04000405 RID: 1029
		MissingAttribute = 15,
		// Token: 0x04000406 RID: 1030
		InvalidName = 17,
		// Token: 0x04000407 RID: 1031
		MissingType,
		// Token: 0x04000408 RID: 1032
		AlreadyDefined,
		// Token: 0x04000409 RID: 1033
		InvalidVersionNumber = 25,
		// Token: 0x0400040A RID: 1034
		InvalidBoolean = 27,
		// Token: 0x0400040B RID: 1035
		BadProperty = 42,
		// Token: 0x0400040C RID: 1036
		InvalidPropertyType = 44,
		// Token: 0x0400040D RID: 1037
		PrecisionOutOfRange = 51,
		// Token: 0x0400040E RID: 1038
		ScaleOutOfRange,
		// Token: 0x0400040F RID: 1039
		NameTooLong = 60,
		// Token: 0x04000410 RID: 1040
		InvalidAssociation = 62,
		// Token: 0x04000411 RID: 1041
		BadNavigationProperty = 74,
		// Token: 0x04000412 RID: 1042
		InvalidKey,
		// Token: 0x04000413 RID: 1043
		InterfaceCriticalPropertyValueMustNotBeNull,
		// Token: 0x04000414 RID: 1044
		InterfaceCriticalKindValueMismatch,
		// Token: 0x04000415 RID: 1045
		InterfaceCriticalKindValueUnexpected,
		// Token: 0x04000416 RID: 1046
		InterfaceCriticalEnumerableMustNotHaveNullElements,
		// Token: 0x04000417 RID: 1047
		InterfaceCriticalEnumPropertyValueOutOfRange,
		// Token: 0x04000418 RID: 1048
		InterfaceCriticalNavigationPartnerInvalid,
		// Token: 0x04000419 RID: 1049
		InterfaceCriticalCycleInTypeHierarchy,
		// Token: 0x0400041A RID: 1050
		InvalidMultiplicity = 92,
		// Token: 0x0400041B RID: 1051
		InvalidAction = 96,
		// Token: 0x0400041C RID: 1052
		InvalidOnDelete,
		// Token: 0x0400041D RID: 1053
		BadUnresolvedComplexType,
		// Token: 0x0400041E RID: 1054
		InvalidEndEntitySet = 100,
		// Token: 0x0400041F RID: 1055
		OperationImportEntitySetExpressionIsInvalid = 103,
		// Token: 0x04000420 RID: 1056
		NavigationPropertyMappingMustPointToValidTargetForProperty = 109,
		// Token: 0x04000421 RID: 1057
		InvalidRoleInRelationshipConstraint,
		// Token: 0x04000422 RID: 1058
		InvalidPropertyInRelationshipConstraint,
		// Token: 0x04000423 RID: 1059
		TypeMismatchRelationshipConstraint,
		// Token: 0x04000424 RID: 1060
		InvalidMultiplicityOfPrincipalEnd,
		// Token: 0x04000425 RID: 1061
		MismatchNumberOfPropertiesInRelationshipConstraint,
		// Token: 0x04000426 RID: 1062
		InvalidMultiplicityOfDependentEnd = 116,
		// Token: 0x04000427 RID: 1063
		OpenTypeNotSupported,
		// Token: 0x04000428 RID: 1064
		SameRoleReferredInReferentialConstraint = 119,
		// Token: 0x04000429 RID: 1065
		EntityKeyMustBeScalar = 128,
		// Token: 0x0400042A RID: 1066
		EntityKeyMustNotBeBinary,
		// Token: 0x0400042B RID: 1067
		EndWithManyMultiplicityCannotHaveOperationsSpecified = 132,
		// Token: 0x0400042C RID: 1068
		NavigationSourceTypeHasNoKeys,
		// Token: 0x0400042D RID: 1069
		ConcurrencyRedefinedOnSubtypeOfEntitySetType = 145,
		// Token: 0x0400042E RID: 1070
		OperationImportUnsupportedReturnType,
		// Token: 0x0400042F RID: 1071
		OperationImportReturnsEntitiesButDoesNotSpecifyEntitySet = 148,
		// Token: 0x04000430 RID: 1072
		OperationImportEntityTypeDoesNotMatchEntitySet,
		// Token: 0x04000431 RID: 1073
		OperationImportSpecifiesEntitySetButDoesNotReturnEntityType,
		// Token: 0x04000432 RID: 1074
		OperationImportCannotImportBoundOperation,
		// Token: 0x04000433 RID: 1075
		FunctionMustHaveReturnType,
		// Token: 0x04000434 RID: 1076
		SimilarRelationshipEnd,
		// Token: 0x04000435 RID: 1077
		DuplicatePropertySpecifiedInEntityKey,
		// Token: 0x04000436 RID: 1078
		UrlEscapeFunctionMustBeBoundFunction,
		// Token: 0x04000437 RID: 1079
		UrlEscapeFunctionMustHaveOnlyOneEdmStringParameter,
		// Token: 0x04000438 RID: 1080
		NullableComplexTypeProperty,
		// Token: 0x04000439 RID: 1081
		KeyMissingOnEntityType = 159,
		// Token: 0x0400043A RID: 1082
		SystemNamespaceEncountered = 161,
		// Token: 0x0400043B RID: 1083
		InvalidNamespaceName = 163,
		// Token: 0x0400043C RID: 1084
		EnumMemberMustHaveValue = 206,
		// Token: 0x0400043D RID: 1085
		DuplicateEntityContainerMemberName = 218,
		// Token: 0x0400043E RID: 1086
		UnboundFunctionOverloadHasIncorrectReturnType,
		// Token: 0x0400043F RID: 1087
		InvalidAbstractComplexType,
		// Token: 0x04000440 RID: 1088
		InvalidPolymorphicComplexType,
		// Token: 0x04000441 RID: 1089
		NavigationPropertyEntityMustNotIndirectlyContainItself,
		// Token: 0x04000442 RID: 1090
		EntitySetRecursiveNavigationPropertyMappingsMustPointBackToSourceEntitySet,
		// Token: 0x04000443 RID: 1091
		BadAmbiguousElementBinding,
		// Token: 0x04000444 RID: 1092
		BadUnresolvedType,
		// Token: 0x04000445 RID: 1093
		BadUnresolvedPrimitiveType,
		// Token: 0x04000446 RID: 1094
		BadCyclicComplex,
		// Token: 0x04000447 RID: 1095
		BadCyclicEntityContainer,
		// Token: 0x04000448 RID: 1096
		BadCyclicEntity,
		// Token: 0x04000449 RID: 1097
		TypeSemanticsCouldNotConvertTypeReference,
		// Token: 0x0400044A RID: 1098
		ConstructibleEntitySetTypeInvalidFromEntityTypeRemoval,
		// Token: 0x0400044B RID: 1099
		BadUnresolvedEntityContainer,
		// Token: 0x0400044C RID: 1100
		BadUnresolvedEntitySet,
		// Token: 0x0400044D RID: 1101
		BadUnresolvedProperty,
		// Token: 0x0400044E RID: 1102
		BadNonComputableAssociationEnd,
		// Token: 0x0400044F RID: 1103
		NavigationPropertyTypeInvalidBecauseOfBadAssociation,
		// Token: 0x04000450 RID: 1104
		EntityMustHaveEntityBaseType,
		// Token: 0x04000451 RID: 1105
		ComplexTypeMustHaveComplexBaseType,
		// Token: 0x04000452 RID: 1106
		BadUnresolvedOperation,
		// Token: 0x04000453 RID: 1107
		KeyPropertyMustBelongToEntity = 242,
		// Token: 0x04000454 RID: 1108
		ReferentialConstraintPrincipalEndMustBelongToAssociation,
		// Token: 0x04000455 RID: 1109
		DependentPropertiesMustBelongToDependentEntity,
		// Token: 0x04000456 RID: 1110
		DeclaringTypeMustBeCorrect,
		// Token: 0x04000457 RID: 1111
		InvalidNavigationPropertyType = 258,
		// Token: 0x04000458 RID: 1112
		KeyPropertyTypeCannotBeEdmPrimitiveType,
		// Token: 0x04000459 RID: 1113
		UnderlyingTypeIsBadBecauseEnumTypeIsBad = 261,
		// Token: 0x0400045A RID: 1114
		ComplexTypeMustHaveProperties = 264,
		// Token: 0x0400045B RID: 1115
		OperationImportParameterIncorrectType,
		// Token: 0x0400045C RID: 1116
		DuplicateDependentProperty = 267,
		// Token: 0x0400045D RID: 1117
		BoundOperationMustHaveParameters,
		// Token: 0x0400045E RID: 1118
		OperationCannotHaveEntitySetPathWithUnBoundOperation,
		// Token: 0x0400045F RID: 1119
		InvalidPathFirstPathParameterNotMatchingFirstParameterName = 271,
		// Token: 0x04000460 RID: 1120
		InvalidPathWithNonEntityBindingParameter = 246,
		// Token: 0x04000461 RID: 1121
		OperationWithInvalidEntitySetPathMissingCompletePath = 248,
		// Token: 0x04000462 RID: 1122
		InvalidPathUnknownTypeCastSegment,
		// Token: 0x04000463 RID: 1123
		InvalidPathInvalidTypeCastSegment,
		// Token: 0x04000464 RID: 1124
		InvalidPathTypeCastSegmentMustBeEntityType,
		// Token: 0x04000465 RID: 1125
		InvalidPathUnknownNavigationProperty,
		// Token: 0x04000466 RID: 1126
		OperationWithEntitySetPathAndReturnTypeTypeNotAssignable,
		// Token: 0x04000467 RID: 1127
		OperationWithEntitySetPathResolvesToCollectionEntityTypeMismatchesEntityTypeReturnType,
		// Token: 0x04000468 RID: 1128
		OperationWithEntitySetPathResolvesToEntityTypeMismatchesCollectionEntityTypeReturnType,
		// Token: 0x04000469 RID: 1129
		OperationWithEntitySetPathReturnTypeInvalid,
		// Token: 0x0400046A RID: 1130
		OperationWithCollectionOfAbstractReturnTypeInvalid,
		// Token: 0x0400046B RID: 1131
		MaxLengthOutOfRange = 272,
		// Token: 0x0400046C RID: 1132
		PathExpressionHasNoEntityContext = 274,
		// Token: 0x0400046D RID: 1133
		InvalidSrid,
		// Token: 0x0400046E RID: 1134
		InvalidMaxLength,
		// Token: 0x0400046F RID: 1135
		InvalidLong,
		// Token: 0x04000470 RID: 1136
		InvalidInteger,
		// Token: 0x04000471 RID: 1137
		InvalidAssociationSet,
		// Token: 0x04000472 RID: 1138
		InvalidParameterMode,
		// Token: 0x04000473 RID: 1139
		BadUnresolvedEntityType,
		// Token: 0x04000474 RID: 1140
		InvalidValue,
		// Token: 0x04000475 RID: 1141
		InvalidBinary,
		// Token: 0x04000476 RID: 1142
		InvalidFloatingPoint,
		// Token: 0x04000477 RID: 1143
		InvalidDateTime,
		// Token: 0x04000478 RID: 1144
		InvalidDateTimeOffset,
		// Token: 0x04000479 RID: 1145
		InvalidDecimal,
		// Token: 0x0400047A RID: 1146
		InvalidGuid,
		// Token: 0x0400047B RID: 1147
		InvalidTypeKindNone,
		// Token: 0x0400047C RID: 1148
		InvalidIfExpressionIncorrectNumberOfOperands,
		// Token: 0x0400047D RID: 1149
		EnumMemberValueOutOfRange = 292,
		// Token: 0x0400047E RID: 1150
		InvalidIsTypeExpressionIncorrectNumberOfOperands,
		// Token: 0x0400047F RID: 1151
		InvalidTypeName,
		// Token: 0x04000480 RID: 1152
		InvalidQualifiedName,
		// Token: 0x04000481 RID: 1153
		NoReadersProvided,
		// Token: 0x04000482 RID: 1154
		NullXmlReader,
		// Token: 0x04000483 RID: 1155
		IsUnboundedCannotBeTrueWhileMaxLengthIsNotNull,
		// Token: 0x04000484 RID: 1156
		InvalidElementAnnotation,
		// Token: 0x04000485 RID: 1157
		InvalidLabeledElementExpressionIncorrectNumberOfOperands,
		// Token: 0x04000486 RID: 1158
		BadUnresolvedLabeledElement,
		// Token: 0x04000487 RID: 1159
		BadUnresolvedEnumMember,
		// Token: 0x04000488 RID: 1160
		InvalidCastExpressionIncorrectNumberOfOperands,
		// Token: 0x04000489 RID: 1161
		BadUnresolvedParameter,
		// Token: 0x0400048A RID: 1162
		NavigationPropertyWithRecursiveContainmentTargetMustBeOptional,
		// Token: 0x0400048B RID: 1163
		NavigationPropertyWithRecursiveContainmentSourceMustBeFromZeroOrOne,
		// Token: 0x0400048C RID: 1164
		NavigationPropertyWithNonRecursiveContainmentSourceMustBeFromOne,
		// Token: 0x0400048D RID: 1165
		ImpossibleAnnotationsTarget = 309,
		// Token: 0x0400048E RID: 1166
		CannotAssertNullableTypeAsNonNullableType,
		// Token: 0x0400048F RID: 1167
		CannotAssertPrimitiveExpressionAsNonPrimitiveType,
		// Token: 0x04000490 RID: 1168
		ExpressionPrimitiveKindNotValidForAssertedType,
		// Token: 0x04000491 RID: 1169
		NullCannotBeAssertedToBeANonNullableType,
		// Token: 0x04000492 RID: 1170
		ExpressionNotValidForTheAssertedType,
		// Token: 0x04000493 RID: 1171
		CollectionExpressionNotValidForNonCollectionType,
		// Token: 0x04000494 RID: 1172
		RecordExpressionNotValidForNonStructuredType,
		// Token: 0x04000495 RID: 1173
		RecordExpressionMissingRequiredProperty,
		// Token: 0x04000496 RID: 1174
		RecordExpressionHasExtraProperties,
		// Token: 0x04000497 RID: 1175
		DuplicateAnnotation,
		// Token: 0x04000498 RID: 1176
		IncorrectNumberOfArguments,
		// Token: 0x04000499 RID: 1177
		DuplicateAlias,
		// Token: 0x0400049A RID: 1178
		ReferencedTypeMustHaveValidName,
		// Token: 0x0400049B RID: 1179
		SingleFileExpected,
		// Token: 0x0400049C RID: 1180
		UnknownEdmxVersion,
		// Token: 0x0400049D RID: 1181
		UnknownEdmVersion,
		// Token: 0x0400049E RID: 1182
		NoSchemasProduced,
		// Token: 0x0400049F RID: 1183
		DuplicateEntityContainerName,
		// Token: 0x040004A0 RID: 1184
		ContainerElementContainerNameIncorrect,
		// Token: 0x040004A1 RID: 1185
		PrimitiveConstantExpressionNotValidForNonPrimitiveType,
		// Token: 0x040004A2 RID: 1186
		IntegerConstantValueOutOfRange,
		// Token: 0x040004A3 RID: 1187
		StringConstantLengthOutOfRange,
		// Token: 0x040004A4 RID: 1188
		BinaryConstantLengthOutOfRange,
		// Token: 0x040004A5 RID: 1189
		InvalidOperationImportParameterMode,
		// Token: 0x040004A6 RID: 1190
		TypeMustNotHaveKindOfNone,
		// Token: 0x040004A7 RID: 1191
		PrimitiveTypeMustNotHaveKindOfNone,
		// Token: 0x040004A8 RID: 1192
		PropertyMustNotHaveKindOfNone,
		// Token: 0x040004A9 RID: 1193
		PropertyTypeCannotBeCollectionOfAbstractType,
		// Token: 0x040004AA RID: 1194
		SchemaElementMustNotHaveKindOfNone,
		// Token: 0x040004AB RID: 1195
		EntityContainerElementMustNotHaveKindOfNone,
		// Token: 0x040004AC RID: 1196
		BinaryValueCannotHaveEmptyValue,
		// Token: 0x040004AD RID: 1197
		EntitySetCanOnlyBeContainedByASingleNavigationProperty,
		// Token: 0x040004AE RID: 1198
		InconsistentNavigationPropertyPartner,
		// Token: 0x040004AF RID: 1199
		EntitySetCanOnlyHaveSingleNavigationPropertyWithContainment,
		// Token: 0x040004B0 RID: 1200
		NavigationMappingMustBeBidirectional,
		// Token: 0x040004B1 RID: 1201
		DuplicateNavigationPropertyMapping,
		// Token: 0x040004B2 RID: 1202
		AllNavigationPropertiesMustBeMapped,
		// Token: 0x040004B3 RID: 1203
		TypeAnnotationMissingRequiredProperty,
		// Token: 0x040004B4 RID: 1204
		TypeAnnotationHasExtraProperties,
		// Token: 0x040004B5 RID: 1205
		InvalidDuration,
		// Token: 0x040004B6 RID: 1206
		InvalidPrimitiveValue,
		// Token: 0x040004B7 RID: 1207
		EnumMustHaveIntegerUnderlyingType,
		// Token: 0x040004B8 RID: 1208
		BadUnresolvedTerm,
		// Token: 0x040004B9 RID: 1209
		BadPrincipalPropertiesInReferentialConstraint,
		// Token: 0x040004BA RID: 1210
		DuplicateDirectValueAnnotationFullName,
		// Token: 0x040004BB RID: 1211
		NoEntitySetsFoundForType,
		// Token: 0x040004BC RID: 1212
		CannotInferEntitySetWithMultipleSetsPerType,
		// Token: 0x040004BD RID: 1213
		InvalidEntitySetPath,
		// Token: 0x040004BE RID: 1214
		InvalidEnumMemberPath,
		// Token: 0x040004BF RID: 1215
		QualifierMustBeSimpleName,
		// Token: 0x040004C0 RID: 1216
		BadUnresolvedEnumType,
		// Token: 0x040004C1 RID: 1217
		BadUnresolvedTarget,
		// Token: 0x040004C2 RID: 1218
		PathIsNotValidForTheGivenContext,
		// Token: 0x040004C3 RID: 1219
		BadUnresolvedNavigationPropertyPath,
		// Token: 0x040004C4 RID: 1220
		NavigationPropertyWithCollectionTypeCannotHaveNullableAttribute,
		// Token: 0x040004C5 RID: 1221
		MetadataDocumentCannotHaveMoreThanOneEntityContainer,
		// Token: 0x040004C6 RID: 1222
		DuplicateFunctions,
		// Token: 0x040004C7 RID: 1223
		DuplicateActions,
		// Token: 0x040004C8 RID: 1224
		BoundFunctionOverloadsMustHaveSameReturnType,
		// Token: 0x040004C9 RID: 1225
		SingletonTypeMustBeEntityType,
		// Token: 0x040004CA RID: 1226
		EntitySetTypeMustBeCollectionOfEntityType,
		// Token: 0x040004CB RID: 1227
		NavigationPropertyOfCollectionTypeMustNotTargetToSingleton,
		// Token: 0x040004CC RID: 1228
		ReferenceElementMustContainAtLeastOneIncludeOrIncludeAnnotationsElement,
		// Token: 0x040004CD RID: 1229
		FunctionImportWithParameterShouldNotBeIncludedInServiceDocument,
		// Token: 0x040004CE RID: 1230
		UnresolvedReferenceUriInEdmxReference,
		// Token: 0x040004CF RID: 1231
		InvalidDate,
		// Token: 0x040004D0 RID: 1232
		InvalidTimeOfDay,
		// Token: 0x040004D1 RID: 1233
		UnresolvedNavigationPropertyPartnerPath,
		// Token: 0x040004D2 RID: 1234
		UnresolvedNavigationPropertyBindingPath,
		// Token: 0x040004D3 RID: 1235
		RequiredParametersMustPrecedeOptional,
		// Token: 0x040004D4 RID: 1236
		ExpressionEnumKindNotValidForAssertedType,
		// Token: 0x040004D5 RID: 1237
		TypeDefinitionUnderlyingTypeCannotBeEdmPrimitiveType,
		// Token: 0x040004D6 RID: 1238
		EntityTypeBaseTypeCannotBeEdmEntityType,
		// Token: 0x040004D7 RID: 1239
		ComplexTypeBaseTypeCannotBeEdmComplexType,
		// Token: 0x040004D8 RID: 1240
		EntityTypeOfSingletonCannotBeEdmEntityType,
		// Token: 0x040004D9 RID: 1241
		EntityTypeOfEntitySetCannotBeEdmEntityType,
		// Token: 0x040004DA RID: 1242
		DeclaringTypeOfNavigationSourceCannotHavePathProperty,
		// Token: 0x040004DB RID: 1243
		TypeOfNavigationPropertyCannotHavePathProperty,
		// Token: 0x040004DC RID: 1244
		BadUnresolvedReturn,
		// Token: 0x040004DD RID: 1245
		EntityComposableBoundEscapeFunctionMustBeLessOne,
		// Token: 0x040004DE RID: 1246
		EntityNoncomposableBoundEscapeFunctionMustBeLessOne,
		// Token: 0x040004DF RID: 1247
		AnnotationApplyToNotAllowedAnnotatable = 400
	}
}
