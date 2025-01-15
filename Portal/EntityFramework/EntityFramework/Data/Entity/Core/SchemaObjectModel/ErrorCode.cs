using System;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x020002EF RID: 751
	internal enum ErrorCode
	{
		// Token: 0x04000C30 RID: 3120
		InvalidErrorCodeValue,
		// Token: 0x04000C31 RID: 3121
		SecurityError = 2,
		// Token: 0x04000C32 RID: 3122
		IOException = 4,
		// Token: 0x04000C33 RID: 3123
		XmlError,
		// Token: 0x04000C34 RID: 3124
		TooManyErrors,
		// Token: 0x04000C35 RID: 3125
		MalformedXml,
		// Token: 0x04000C36 RID: 3126
		UnexpectedXmlNodeType,
		// Token: 0x04000C37 RID: 3127
		UnexpectedXmlAttribute,
		// Token: 0x04000C38 RID: 3128
		UnexpectedXmlElement,
		// Token: 0x04000C39 RID: 3129
		TextNotAllowed,
		// Token: 0x04000C3A RID: 3130
		EmptyFile,
		// Token: 0x04000C3B RID: 3131
		XsdError,
		// Token: 0x04000C3C RID: 3132
		InvalidAlias,
		// Token: 0x04000C3D RID: 3133
		IntegerExpected = 16,
		// Token: 0x04000C3E RID: 3134
		InvalidName,
		// Token: 0x04000C3F RID: 3135
		AlreadyDefined = 19,
		// Token: 0x04000C40 RID: 3136
		ElementNotInSchema,
		// Token: 0x04000C41 RID: 3137
		InvalidBaseType = 22,
		// Token: 0x04000C42 RID: 3138
		NoConcreteDescendants,
		// Token: 0x04000C43 RID: 3139
		CycleInTypeHierarchy,
		// Token: 0x04000C44 RID: 3140
		InvalidVersionNumber,
		// Token: 0x04000C45 RID: 3141
		InvalidSize,
		// Token: 0x04000C46 RID: 3142
		InvalidBoolean,
		// Token: 0x04000C47 RID: 3143
		BadType = 29,
		// Token: 0x04000C48 RID: 3144
		InvalidVersioningClass = 32,
		// Token: 0x04000C49 RID: 3145
		InvalidVersionIntroduced,
		// Token: 0x04000C4A RID: 3146
		BadNamespace,
		// Token: 0x04000C4B RID: 3147
		UnresolvedReferenceSchema = 38,
		// Token: 0x04000C4C RID: 3148
		NotInNamespace = 40,
		// Token: 0x04000C4D RID: 3149
		NotUnnestedType,
		// Token: 0x04000C4E RID: 3150
		BadProperty,
		// Token: 0x04000C4F RID: 3151
		UndefinedProperty,
		// Token: 0x04000C50 RID: 3152
		InvalidPropertyType,
		// Token: 0x04000C51 RID: 3153
		InvalidAsNestedType,
		// Token: 0x04000C52 RID: 3154
		InvalidChangeUnit,
		// Token: 0x04000C53 RID: 3155
		UnauthorizedAccessException,
		// Token: 0x04000C54 RID: 3156
		MissingNamespaceAttribute = 50,
		// Token: 0x04000C55 RID: 3157
		PrecisionOutOfRange,
		// Token: 0x04000C56 RID: 3158
		ScaleOutOfRange,
		// Token: 0x04000C57 RID: 3159
		DefaultNotAllowed,
		// Token: 0x04000C58 RID: 3160
		InvalidDefault,
		// Token: 0x04000C59 RID: 3161
		RequiredFacetMissing,
		// Token: 0x04000C5A RID: 3162
		BadImageFormatException,
		// Token: 0x04000C5B RID: 3163
		MissingSchemaXml,
		// Token: 0x04000C5C RID: 3164
		BadPrecisionAndScale,
		// Token: 0x04000C5D RID: 3165
		InvalidChangeUnitUsage,
		// Token: 0x04000C5E RID: 3166
		NameTooLong,
		// Token: 0x04000C5F RID: 3167
		CircularlyDefinedType,
		// Token: 0x04000C60 RID: 3168
		InvalidAssociation,
		// Token: 0x04000C61 RID: 3169
		FacetNotAllowedByType,
		// Token: 0x04000C62 RID: 3170
		ConstantFacetSpecifiedInSchema,
		// Token: 0x04000C63 RID: 3171
		BadNavigationProperty = 74,
		// Token: 0x04000C64 RID: 3172
		InvalidKey,
		// Token: 0x04000C65 RID: 3173
		InvalidMultiplicity = 92,
		// Token: 0x04000C66 RID: 3174
		InvalidAction = 96,
		// Token: 0x04000C67 RID: 3175
		InvalidOperation,
		// Token: 0x04000C68 RID: 3176
		InvalidContainerTypeForEnd = 99,
		// Token: 0x04000C69 RID: 3177
		InvalidEndEntitySet,
		// Token: 0x04000C6A RID: 3178
		AmbiguousEntityContainerEnd,
		// Token: 0x04000C6B RID: 3179
		MissingExtentEntityContainerEnd,
		// Token: 0x04000C6C RID: 3180
		BadParameterDirection = 106,
		// Token: 0x04000C6D RID: 3181
		FailedInference,
		// Token: 0x04000C6E RID: 3182
		InvalidFacetInProviderManifest = 109,
		// Token: 0x04000C6F RID: 3183
		InvalidRoleInRelationshipConstraint,
		// Token: 0x04000C70 RID: 3184
		InvalidPropertyInRelationshipConstraint,
		// Token: 0x04000C71 RID: 3185
		TypeMismatchRelationshipConstraint,
		// Token: 0x04000C72 RID: 3186
		InvalidMultiplicityInRoleInRelationshipConstraint,
		// Token: 0x04000C73 RID: 3187
		MismatchNumberOfPropertiesInRelationshipConstraint,
		// Token: 0x04000C74 RID: 3188
		MissingPropertyInRelationshipConstraint,
		// Token: 0x04000C75 RID: 3189
		MissingConstraintOnRelationshipType,
		// Token: 0x04000C76 RID: 3190
		SameRoleReferredInReferentialConstraint = 119,
		// Token: 0x04000C77 RID: 3191
		InvalidValueForParameterTypeSemantics,
		// Token: 0x04000C78 RID: 3192
		InvalidRelationshipEndType,
		// Token: 0x04000C79 RID: 3193
		InvalidPrimitiveTypeKind,
		// Token: 0x04000C7A RID: 3194
		InvalidTypeConversionDestinationType = 124,
		// Token: 0x04000C7B RID: 3195
		ByteValueExpected,
		// Token: 0x04000C7C RID: 3196
		FunctionWithNonPrimitiveTypeNotSupported,
		// Token: 0x04000C7D RID: 3197
		PrecisionMoreThanAllowedMax,
		// Token: 0x04000C7E RID: 3198
		EntityKeyMustBeScalar,
		// Token: 0x04000C7F RID: 3199
		EntityKeyTypeCurrentlyNotSupported,
		// Token: 0x04000C80 RID: 3200
		NoPreferredMappingForPrimitiveTypeKind,
		// Token: 0x04000C81 RID: 3201
		TooManyPreferredMappingsForPrimitiveTypeKind,
		// Token: 0x04000C82 RID: 3202
		EndWithManyMultiplicityCannotHaveOperationsSpecified,
		// Token: 0x04000C83 RID: 3203
		EntitySetTypeHasNoKeys,
		// Token: 0x04000C84 RID: 3204
		InvalidNumberOfParametersForAggregateFunction,
		// Token: 0x04000C85 RID: 3205
		InvalidParameterTypeForAggregateFunction,
		// Token: 0x04000C86 RID: 3206
		ComposableFunctionOrFunctionImportWithoutReturnType,
		// Token: 0x04000C87 RID: 3207
		NonComposableFunctionWithReturnType,
		// Token: 0x04000C88 RID: 3208
		NonComposableFunctionAttributesNotValid,
		// Token: 0x04000C89 RID: 3209
		ComposableFunctionWithCommandText,
		// Token: 0x04000C8A RID: 3210
		FunctionDeclaresCommandTextAndStoreFunctionName,
		// Token: 0x04000C8B RID: 3211
		SystemNamespace,
		// Token: 0x04000C8C RID: 3212
		EmptyDefiningQuery,
		// Token: 0x04000C8D RID: 3213
		TableAndSchemaAreMutuallyExclusiveWithDefiningQuery,
		// Token: 0x04000C8E RID: 3214
		ConcurrencyRedefinedOnSubTypeOfEntitySetType = 145,
		// Token: 0x04000C8F RID: 3215
		FunctionImportUnsupportedReturnType,
		// Token: 0x04000C90 RID: 3216
		FunctionImportUnknownEntitySet,
		// Token: 0x04000C91 RID: 3217
		FunctionImportReturnsEntitiesButDoesNotSpecifyEntitySet,
		// Token: 0x04000C92 RID: 3218
		FunctionImportEntityTypeDoesNotMatchEntitySet,
		// Token: 0x04000C93 RID: 3219
		FunctionImportSpecifiesEntitySetButDoesNotReturnEntityType,
		// Token: 0x04000C94 RID: 3220
		InternalError = 152,
		// Token: 0x04000C95 RID: 3221
		SimilarRelationshipEnd,
		// Token: 0x04000C96 RID: 3222
		DuplicatePropertySpecifiedInEntityKey,
		// Token: 0x04000C97 RID: 3223
		AmbiguousFunctionReturnType = 156,
		// Token: 0x04000C98 RID: 3224
		NullableComplexType,
		// Token: 0x04000C99 RID: 3225
		NonComplexCollections,
		// Token: 0x04000C9A RID: 3226
		KeyMissingOnEntityType,
		// Token: 0x04000C9B RID: 3227
		InvalidNamespaceInUsing,
		// Token: 0x04000C9C RID: 3228
		NeedNotUseSystemNamespaceInUsing,
		// Token: 0x04000C9D RID: 3229
		CannotUseSystemNamespaceAsAlias,
		// Token: 0x04000C9E RID: 3230
		InvalidNamespaceName,
		// Token: 0x04000C9F RID: 3231
		InvalidEntityContainerNameInExtends,
		// Token: 0x04000CA0 RID: 3232
		InvalidNamespaceOrAliasSpecified = 166,
		// Token: 0x04000CA1 RID: 3233
		EntityContainerCannotExtendItself,
		// Token: 0x04000CA2 RID: 3234
		FailedToRetrieveProviderManifest,
		// Token: 0x04000CA3 RID: 3235
		ProviderManifestTokenMismatch,
		// Token: 0x04000CA4 RID: 3236
		ProviderManifestTokenNotFound,
		// Token: 0x04000CA5 RID: 3237
		EmptyCommandText,
		// Token: 0x04000CA6 RID: 3238
		InconsistentProvider,
		// Token: 0x04000CA7 RID: 3239
		InconsistentProviderManifestToken,
		// Token: 0x04000CA8 RID: 3240
		DuplicatedFunctionoverloads,
		// Token: 0x04000CA9 RID: 3241
		InvalidProvider,
		// Token: 0x04000CAA RID: 3242
		FunctionWithNonEdmTypeNotSupported,
		// Token: 0x04000CAB RID: 3243
		ComplexTypeAsReturnTypeAndDefinedEntitySet,
		// Token: 0x04000CAC RID: 3244
		ComplexTypeAsReturnTypeAndNestedComplexProperty,
		// Token: 0x04000CAD RID: 3245
		FunctionImportComposableAndSideEffectingNotAllowed = 180,
		// Token: 0x04000CAE RID: 3246
		FunctionImportEntitySetAndEntitySetPathDeclared,
		// Token: 0x04000CAF RID: 3247
		FacetOnNonScalarType,
		// Token: 0x04000CB0 RID: 3248
		IncorrectlyPlacedFacet,
		// Token: 0x04000CB1 RID: 3249
		ReturnTypeNotDeclared,
		// Token: 0x04000CB2 RID: 3250
		TypeNotDeclared,
		// Token: 0x04000CB3 RID: 3251
		RowTypeWithoutProperty,
		// Token: 0x04000CB4 RID: 3252
		ReturnTypeDeclaredAsAttributeAndElement,
		// Token: 0x04000CB5 RID: 3253
		TypeDeclaredAsAttributeAndElement,
		// Token: 0x04000CB6 RID: 3254
		ReferenceToNonEntityType,
		// Token: 0x04000CB7 RID: 3255
		FunctionImportCollectionAndRefParametersNotAllowed,
		// Token: 0x04000CB8 RID: 3256
		IncompatibleSchemaVersion,
		// Token: 0x04000CB9 RID: 3257
		NoCodeGenNamespaceInStructuralAnnotation,
		// Token: 0x04000CBA RID: 3258
		AmbiguousFunctionAndType,
		// Token: 0x04000CBB RID: 3259
		CannotLoadDifferentVersionOfSchemaInTheSameItemCollection,
		// Token: 0x04000CBC RID: 3260
		BoolValueExpected,
		// Token: 0x04000CBD RID: 3261
		EndWithoutMultiplicity,
		// Token: 0x04000CBE RID: 3262
		TVFReturnTypeRowHasNonScalarProperty,
		// Token: 0x04000CBF RID: 3263
		FunctionImportNonNullableParametersNotAllowed = 201,
		// Token: 0x04000CC0 RID: 3264
		FunctionWithDefiningExpressionAndEntitySetNotAllowed,
		// Token: 0x04000CC1 RID: 3265
		FunctionEntityTypeScopeDoesNotMatchReturnType,
		// Token: 0x04000CC2 RID: 3266
		InvalidEnumUnderlyingType,
		// Token: 0x04000CC3 RID: 3267
		DuplicateEnumMember,
		// Token: 0x04000CC4 RID: 3268
		CalculatedEnumValueOutOfRange,
		// Token: 0x04000CC5 RID: 3269
		EnumMemberValueOutOfItsUnderylingTypeRange,
		// Token: 0x04000CC6 RID: 3270
		InvalidSystemReferenceId,
		// Token: 0x04000CC7 RID: 3271
		UnexpectedSpatialType
	}
}
