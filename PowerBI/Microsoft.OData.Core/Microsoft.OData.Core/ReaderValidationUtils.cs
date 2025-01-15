using System;
using System.Text;
using Microsoft.OData.Edm;
using Microsoft.OData.JsonLight;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x020000CA RID: 202
	internal static class ReaderValidationUtils
	{
		// Token: 0x0600094D RID: 2381 RVA: 0x00016AC7 File Offset: 0x00014CC7
		internal static void ValidateMessageReaderSettings(ODataMessageReaderSettings messageReaderSettings, bool readingResponse)
		{
			if (messageReaderSettings.BaseUri != null && !messageReaderSettings.BaseUri.IsAbsoluteUri)
			{
				throw new ODataException(Strings.ReaderValidationUtils_MessageReaderSettingsBaseUriMustBeNullOrAbsolute(UriUtils.UriToString(messageReaderSettings.BaseUri)));
			}
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00016AFA File Offset: 0x00014CFA
		internal static void ValidateEntityReferenceLink(ODataEntityReferenceLink link)
		{
			if (link.Url == null)
			{
				throw new ODataException(Strings.ReaderValidationUtils_EntityReferenceLinkMissingUri);
			}
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x00016B15 File Offset: 0x00014D15
		internal static void ValidateStreamReferenceProperty(IODataStreamReferenceInfo streamInfo, string propertyName, IEdmStructuredType structuredType, IEdmProperty streamEdmProperty, bool throwOnUndeclaredLinkProperty)
		{
			ValidationUtils.ValidateStreamPropertyInfo(streamInfo, streamEdmProperty, propertyName);
			if (structuredType != null && structuredType.IsOpen && (streamEdmProperty == null && throwOnUndeclaredLinkProperty))
			{
				throw new ODataException(Strings.ValidationUtils_OpenStreamProperty(propertyName));
			}
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x00016B3F File Offset: 0x00014D3F
		internal static void ValidateNullValue(IEdmTypeReference expectedTypeReference, bool enablePrimitiveTypeConversion, bool validateNullValue, string propertyName, bool? isDynamicProperty)
		{
			if (expectedTypeReference != null && (enablePrimitiveTypeConversion || expectedTypeReference.TypeKind() != EdmTypeKind.Primitive))
			{
				ReaderValidationUtils.ValidateNullValueAllowed(expectedTypeReference, validateNullValue, propertyName, isDynamicProperty);
			}
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x00016B5C File Offset: 0x00014D5C
		internal static IEdmProperty ValidatePropertyDefined(string propertyName, IEdmStructuredType owningStructuredType, bool throwOnUndeclaredPropertyForNonOpenType)
		{
			if (owningStructuredType == null)
			{
				return null;
			}
			IEdmProperty edmProperty = owningStructuredType.FindProperty(propertyName);
			if (edmProperty == null && !owningStructuredType.IsOpen && throwOnUndeclaredPropertyForNonOpenType)
			{
				throw new ODataException(Strings.ValidationUtils_PropertyDoesNotExistOnType(propertyName, owningStructuredType.FullTypeName()));
			}
			return edmProperty;
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x00016B97 File Offset: 0x00014D97
		internal static ODataException GetPrimitiveTypeConversionException(IEdmPrimitiveTypeReference targetTypeReference, Exception innerException, string stringValue)
		{
			return new ODataException(Strings.ReaderValidationUtils_CannotConvertPrimitiveValue(stringValue, targetTypeReference.FullName()), innerException);
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x00016BAC File Offset: 0x00014DAC
		internal static IEdmType ResolvePayloadTypeName(IEdmModel model, IEdmTypeReference expectedTypeReference, string payloadTypeName, EdmTypeKind expectedTypeKind, Func<IEdmType, string, IEdmType> clientCustomTypeResolver, out EdmTypeKind payloadTypeKind)
		{
			if (payloadTypeName == null)
			{
				payloadTypeKind = EdmTypeKind.None;
				return null;
			}
			if (payloadTypeName.Length == 0)
			{
				payloadTypeKind = expectedTypeKind;
				return null;
			}
			IEdmType edmType = MetadataUtils.ResolveTypeNameForRead(model, (expectedTypeReference == null) ? null : expectedTypeReference.Definition, payloadTypeName, clientCustomTypeResolver, out payloadTypeKind);
			if (payloadTypeKind == EdmTypeKind.None)
			{
				payloadTypeKind = expectedTypeKind;
			}
			return edmType;
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00016BF4 File Offset: 0x00014DF4
		internal static IEdmTypeReference ResolvePayloadTypeNameAndComputeTargetType(EdmTypeKind expectedTypeKind, bool? expectStructuredType, IEdmType defaultPrimitivePayloadType, IEdmTypeReference expectedTypeReference, string payloadTypeName, IEdmModel model, Func<IEdmType, string, IEdmType> clientCustomTypeResolver, bool throwIfTypeConflictsWithMetadata, bool enablePrimitiveTypeConversion, Func<EdmTypeKind> typeKindFromPayloadFunc, out EdmTypeKind targetTypeKind, out ODataTypeAnnotation typeAnnotation)
		{
			typeAnnotation = null;
			EdmTypeKind edmTypeKind;
			IEdmType edmType = ReaderValidationUtils.ResolvePayloadTypeName(model, expectedTypeReference, payloadTypeName, EdmTypeKind.Complex, clientCustomTypeResolver, out edmTypeKind);
			bool flag = expectStructuredType == true || (expectStructuredType == null && edmTypeKind.IsStructured());
			targetTypeKind = ReaderValidationUtils.ComputeTargetTypeKind(expectedTypeReference, flag, payloadTypeName, edmTypeKind, clientCustomTypeResolver, throwIfTypeConflictsWithMetadata, enablePrimitiveTypeConversion, typeKindFromPayloadFunc);
			IEdmTypeReference edmTypeReference;
			if (targetTypeKind == EdmTypeKind.Primitive)
			{
				edmTypeReference = ReaderValidationUtils.ResolveAndValidatePrimitiveTargetType(expectedTypeReference, edmTypeKind, edmType, payloadTypeName, defaultPrimitivePayloadType, model, clientCustomTypeResolver, enablePrimitiveTypeConversion, throwIfTypeConflictsWithMetadata);
			}
			else
			{
				edmTypeReference = ReaderValidationUtils.ResolveAndValidateNonPrimitiveTargetType(targetTypeKind, expectedTypeReference, edmTypeKind, edmType, payloadTypeName, model, clientCustomTypeResolver, throwIfTypeConflictsWithMetadata);
				if (edmTypeReference != null)
				{
					typeAnnotation = ReaderValidationUtils.CreateODataTypeAnnotation(payloadTypeName, edmType, edmTypeReference);
				}
			}
			if ((expectedTypeKind != EdmTypeKind.None || (targetTypeKind != EdmTypeKind.Untyped && expectStructuredType == true)) && edmTypeReference != null)
			{
				ValidationUtils.ValidateTypeKind(targetTypeKind, expectedTypeKind, new bool?(flag), payloadTypeName);
			}
			return edmTypeReference;
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00016CD8 File Offset: 0x00014ED8
		internal static IEdmTypeReference ResolveAndValidatePrimitiveTargetType(IEdmTypeReference expectedTypeReference, EdmTypeKind payloadTypeKind, IEdmType payloadType, string payloadTypeName, IEdmType defaultPayloadType, IEdmModel model, Func<IEdmType, string, IEdmType> clientCustomTypeResolver, bool enablePrimitiveTypeConversion, bool throwIfTypeConflictsWithMetadata)
		{
			bool flag = clientCustomTypeResolver != null && payloadType != null;
			if (payloadTypeKind != EdmTypeKind.None && (!enablePrimitiveTypeConversion || throwIfTypeConflictsWithMetadata))
			{
				ValidationUtils.ValidateTypeKind(payloadTypeKind, EdmTypeKind.Primitive, null, payloadTypeName);
			}
			if (!model.IsUserModel())
			{
				return MetadataUtils.GetNullablePayloadTypeReference(payloadType ?? defaultPayloadType);
			}
			if (expectedTypeReference == null || flag || !enablePrimitiveTypeConversion)
			{
				return MetadataUtils.GetNullablePayloadTypeReference(payloadType ?? defaultPayloadType);
			}
			if (!throwIfTypeConflictsWithMetadata)
			{
				return expectedTypeReference;
			}
			if (payloadType != null)
			{
				if (!MetadataUtilsCommon.CanConvertPrimitiveTypeTo(null, (IEdmPrimitiveType)payloadType.AsActualType(), (IEdmPrimitiveType)expectedTypeReference.Definition))
				{
					throw new ODataException(Strings.ValidationUtils_IncompatibleType(payloadTypeName, expectedTypeReference.FullName()));
				}
				if (expectedTypeReference.PrimitiveKind() == EdmPrimitiveTypeKind.PrimitiveType)
				{
					return payloadType.ToTypeReference(expectedTypeReference.IsNullable);
				}
			}
			return expectedTypeReference;
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x00016D90 File Offset: 0x00014F90
		internal static IEdmTypeReference ResolveAndValidateNonPrimitiveTargetType(EdmTypeKind expectedTypeKind, IEdmTypeReference expectedTypeReference, EdmTypeKind payloadTypeKind, IEdmType payloadType, string payloadTypeName, IEdmModel model, Func<IEdmType, string, IEdmType> clientCustomTypeResolver, bool throwIfTypeConflictsWithMetadata)
		{
			bool flag = clientCustomTypeResolver != null && payloadType != null;
			if (!flag && model.IsUserModel() && (expectedTypeReference == null || throwIfTypeConflictsWithMetadata))
			{
				ReaderValidationUtils.VerifyPayloadTypeDefined(payloadTypeName, payloadType);
			}
			if (payloadTypeKind != EdmTypeKind.None && (throwIfTypeConflictsWithMetadata || expectedTypeReference == null))
			{
				ValidationUtils.ValidateTypeKind(payloadTypeKind, expectedTypeKind, null, payloadTypeName);
			}
			if (!model.IsUserModel())
			{
				return null;
			}
			if (expectedTypeReference == null || flag)
			{
				return ReaderValidationUtils.ResolveAndValidateTargetTypeWithNoExpectedType(expectedTypeKind, payloadType);
			}
			if (!throwIfTypeConflictsWithMetadata)
			{
				return ReaderValidationUtils.ResolveAndValidateTargetTypeStrictValidationDisabled(expectedTypeKind, expectedTypeReference, payloadType);
			}
			return ReaderValidationUtils.ResolveAndValidateTargetTypeStrictValidationEnabled(expectedTypeKind, expectedTypeReference, payloadType);
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x00016E12 File Offset: 0x00015012
		internal static void ValidateEncodingSupportedInBatch(Encoding encoding)
		{
			if (string.CompareOrdinal(Encoding.UTF8.WebName, encoding.WebName) != 0)
			{
				throw new ODataException(Strings.ODataBatchReaderStream_MultiByteEncodingsNotSupported(encoding.WebName));
			}
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00016E3C File Offset: 0x0001503C
		internal static void ValidateEncodingSupportedInAsync(Encoding encoding)
		{
			if (string.CompareOrdinal(Encoding.UTF8.WebName, encoding.WebName) != 0)
			{
				throw new ODataException(Strings.ODataAsyncReader_MultiByteEncodingsNotSupported(encoding.WebName));
			}
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00016E68 File Offset: 0x00015068
		internal static void ValidateResourceSetOrResourceContextUri(ODataJsonLightContextUriParseResult contextUriParseResult, ODataReaderCore.Scope scope, bool updateScope)
		{
			if (contextUriParseResult.EdmType is IEdmCollectionType)
			{
				ReaderValidationUtils.ValidateResourceSetContextUri(contextUriParseResult, scope, updateScope);
				return;
			}
			if (scope.NavigationSource == null)
			{
				if (updateScope)
				{
					scope.NavigationSource = contextUriParseResult.NavigationSource;
				}
			}
			else if (contextUriParseResult.NavigationSource != null && string.CompareOrdinal(scope.NavigationSource.FullNavigationSourceName(), contextUriParseResult.NavigationSource.FullNavigationSourceName()) != 0)
			{
				throw new ODataException(Strings.ReaderValidationUtils_ContextUriValidationInvalidExpectedEntitySet(UriUtils.UriToString(contextUriParseResult.ContextUri), contextUriParseResult.NavigationSource.FullNavigationSourceName(), scope.NavigationSource.FullNavigationSourceName()));
			}
			IEdmStructuredType edmStructuredType = (IEdmStructuredType)contextUriParseResult.EdmType;
			if (edmStructuredType == null)
			{
				return;
			}
			if (scope.ResourceType == null)
			{
				if (updateScope)
				{
					scope.ResourceTypeReference = edmStructuredType.ToTypeReference(true).AsStructured();
					return;
				}
			}
			else if (scope.ResourceType.IsAssignableFrom(edmStructuredType))
			{
				if (updateScope)
				{
					scope.ResourceTypeReference = edmStructuredType.ToTypeReference(true).AsStructured();
					return;
				}
			}
			else if (!edmStructuredType.IsAssignableFrom(scope.ResourceType))
			{
				throw new ODataException(Strings.ReaderValidationUtils_ContextUriValidationInvalidExpectedEntityType(UriUtils.UriToString(contextUriParseResult.ContextUri), contextUriParseResult.EdmType.FullTypeName(), scope.ResourceType.FullTypeName()));
			}
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x00016F84 File Offset: 0x00015184
		internal static IEdmTypeReference ValidateCollectionContextUriAndGetPayloadItemTypeReference(ODataJsonLightContextUriParseResult contextUriParseResult, IEdmTypeReference expectedItemTypeReference)
		{
			if (contextUriParseResult == null || contextUriParseResult.EdmType == null)
			{
				return expectedItemTypeReference;
			}
			if (contextUriParseResult.EdmType is IEdmCollectionType)
			{
				IEdmCollectionType edmCollectionType = (IEdmCollectionType)contextUriParseResult.EdmType;
				if (expectedItemTypeReference != null && !expectedItemTypeReference.IsAssignableFrom(edmCollectionType.ElementType))
				{
					throw new ODataException(Strings.ReaderValidationUtils_ContextUriDoesNotReferTypeAssignableToExpectedType(UriUtils.UriToString(contextUriParseResult.ContextUri), edmCollectionType.ElementType.FullName(), expectedItemTypeReference.FullName()));
				}
				return edmCollectionType.ElementType;
			}
			else
			{
				if (expectedItemTypeReference != null && !expectedItemTypeReference.Definition.IsAssignableFrom(contextUriParseResult.EdmType))
				{
					throw new ODataException(Strings.ReaderValidationUtils_ContextUriDoesNotReferTypeAssignableToExpectedType(UriUtils.UriToString(contextUriParseResult.ContextUri), contextUriParseResult.EdmType.FullTypeName(), expectedItemTypeReference.Definition.FullTypeName()));
				}
				return contextUriParseResult.EdmType.ToTypeReference(true);
			}
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00017044 File Offset: 0x00015244
		internal static void ValidateOperationProperty(object propertyValue, string propertyName, string metadata, string operationsHeader)
		{
			if (propertyValue == null)
			{
				throw new ODataException(Strings.ODataJsonOperationsDeserializerUtils_OperationPropertyCannotBeNull(propertyName, metadata, operationsHeader));
			}
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x00017058 File Offset: 0x00015258
		private static IEdmTypeReference ResolveAndValidateTargetTypeWithNoExpectedType(EdmTypeKind expectedTypeKind, IEdmType payloadType)
		{
			if (payloadType != null)
			{
				return payloadType.ToTypeReference(true);
			}
			if (expectedTypeKind == EdmTypeKind.Entity)
			{
				throw new ODataException(Strings.ReaderValidationUtils_ResourceWithoutType);
			}
			return null;
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x00017084 File Offset: 0x00015284
		private static IEdmTypeReference ResolveAndValidateTargetTypeStrictValidationDisabled(EdmTypeKind expectedTypeKind, IEdmTypeReference expectedTypeReference, IEdmType payloadType)
		{
			switch (expectedTypeKind)
			{
			case EdmTypeKind.Entity:
				if (payloadType != null && expectedTypeKind == payloadType.TypeKind && expectedTypeReference.AsEntity().EntityDefinition().IsAssignableFrom((IEdmEntityType)payloadType))
				{
					return payloadType.ToTypeReference(true);
				}
				return expectedTypeReference;
			case EdmTypeKind.Complex:
				if (payloadType != null && expectedTypeKind == payloadType.TypeKind && expectedTypeReference.AsComplex().ComplexDefinition().IsAssignableFrom((IEdmComplexType)payloadType))
				{
					return payloadType.ToTypeReference(true);
				}
				return expectedTypeReference;
			case EdmTypeKind.Collection:
				if (payloadType != null && expectedTypeKind == payloadType.TypeKind)
				{
					ReaderValidationUtils.VerifyCollectionComplexItemType(expectedTypeReference, payloadType);
					return expectedTypeReference;
				}
				return expectedTypeReference;
			case EdmTypeKind.Enum:
			case EdmTypeKind.TypeDefinition:
			case EdmTypeKind.Untyped:
				return expectedTypeReference;
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ReaderValidationUtils_ResolveAndValidateTypeName_Strict_TypeKind));
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0001713C File Offset: 0x0001533C
		private static IEdmTypeReference ResolveAndValidateTargetTypeStrictValidationEnabled(EdmTypeKind expectedTypeKind, IEdmTypeReference expectedTypeReference, IEdmType payloadType)
		{
			switch (expectedTypeKind)
			{
			case EdmTypeKind.Entity:
				if (payloadType != null)
				{
					IEdmTypeReference edmTypeReference = payloadType.ToTypeReference(true);
					ValidationUtils.ValidateEntityTypeIsAssignable((IEdmEntityTypeReference)expectedTypeReference, (IEdmEntityTypeReference)edmTypeReference);
					return edmTypeReference;
				}
				return expectedTypeReference;
			case EdmTypeKind.Complex:
				if (payloadType != null)
				{
					ReaderValidationUtils.VerifyComplexType(expectedTypeReference, payloadType, true);
					return payloadType.ToTypeReference(true);
				}
				return expectedTypeReference;
			case EdmTypeKind.Collection:
				if (payloadType != null && !payloadType.IsElementTypeEquivalentTo(expectedTypeReference.Definition))
				{
					ReaderValidationUtils.VerifyCollectionComplexItemType(expectedTypeReference, payloadType);
					throw new ODataException(Strings.ValidationUtils_IncompatibleType(payloadType.FullTypeName(), expectedTypeReference.FullName()));
				}
				return expectedTypeReference;
			case EdmTypeKind.Enum:
				if (payloadType != null && string.CompareOrdinal(payloadType.FullTypeName(), expectedTypeReference.FullName()) != 0)
				{
					throw new ODataException(Strings.ValidationUtils_IncompatibleType(payloadType.FullTypeName(), expectedTypeReference.FullName()));
				}
				return expectedTypeReference;
			case EdmTypeKind.TypeDefinition:
				if (payloadType != null && !expectedTypeReference.Definition.IsAssignableFrom(payloadType))
				{
					throw new ODataException(Strings.ValidationUtils_IncompatibleType(payloadType.FullTypeName(), expectedTypeReference.FullName()));
				}
				return expectedTypeReference;
			case EdmTypeKind.Untyped:
				return expectedTypeReference;
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ReaderValidationUtils_ResolveAndValidateTypeName_Strict_TypeKind));
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x00017246 File Offset: 0x00015446
		private static void VerifyPayloadTypeDefined(string payloadTypeName, IEdmType payloadType)
		{
			if (payloadTypeName != null && payloadType == null)
			{
				throw new ODataException(Strings.ValidationUtils_UnrecognizedTypeName(payloadTypeName));
			}
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0001725C File Offset: 0x0001545C
		private static void VerifyComplexType(IEdmTypeReference expectedTypeReference, IEdmType payloadType, bool failIfNotRelated)
		{
			IEdmStructuredType edmStructuredType = expectedTypeReference.AsStructured().StructuredDefinition();
			IEdmStructuredType edmStructuredType2 = (IEdmStructuredType)payloadType;
			if (!edmStructuredType.IsAssignableFrom(edmStructuredType2) && failIfNotRelated)
			{
				throw new ODataException(Strings.ValidationUtils_IncompatibleType(edmStructuredType2.FullTypeName(), edmStructuredType.FullTypeName()));
			}
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x000172A0 File Offset: 0x000154A0
		private static void VerifyCollectionComplexItemType(IEdmTypeReference expectedTypeReference, IEdmType payloadType)
		{
			IEdmCollectionTypeReference edmCollectionTypeReference = ValidationUtils.ValidateCollectionType(expectedTypeReference);
			IEdmTypeReference collectionItemType = edmCollectionTypeReference.GetCollectionItemType();
			if (collectionItemType != null && collectionItemType.IsODataComplexTypeKind())
			{
				IEdmCollectionTypeReference edmCollectionTypeReference2 = ValidationUtils.ValidateCollectionType(payloadType.ToTypeReference());
				IEdmTypeReference collectionItemType2 = edmCollectionTypeReference2.GetCollectionItemType();
				if (collectionItemType2 != null && collectionItemType2.IsODataComplexTypeKind())
				{
					ReaderValidationUtils.VerifyComplexType(collectionItemType, collectionItemType2.Definition, false);
				}
			}
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x000172F1 File Offset: 0x000154F1
		private static ODataTypeAnnotation CreateODataTypeAnnotation(string payloadTypeName, IEdmType payloadType, IEdmTypeReference targetTypeReference)
		{
			if (payloadType != null && !payloadType.IsEquivalentTo(targetTypeReference.Definition))
			{
				return new ODataTypeAnnotation(payloadTypeName, payloadType);
			}
			if (payloadType == null)
			{
				return new ODataTypeAnnotation();
			}
			return null;
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x00017318 File Offset: 0x00015518
		private static EdmTypeKind ComputeTargetTypeKind(IEdmTypeReference expectedTypeReference, bool forResource, string payloadTypeName, EdmTypeKind payloadTypeKind, Func<IEdmType, string, IEdmType> clientCustomTypeResolver, bool throwIfTypeConflictsWithMetadata, bool enablePrimitiveTypeConversion, Func<EdmTypeKind> typeKindFromPayloadFunc)
		{
			bool flag = clientCustomTypeResolver != null && payloadTypeKind > EdmTypeKind.None;
			EdmTypeKind edmTypeKind = EdmTypeKind.None;
			if (!flag)
			{
				edmTypeKind = ReaderUtils.GetExpectedTypeKind(expectedTypeReference, enablePrimitiveTypeConversion);
			}
			EdmTypeKind edmTypeKind2;
			if (edmTypeKind != EdmTypeKind.None)
			{
				edmTypeKind2 = edmTypeKind;
			}
			else if (payloadTypeKind != EdmTypeKind.None)
			{
				if (!forResource)
				{
					ValidationUtils.ValidateValueTypeKind(payloadTypeKind, payloadTypeName);
				}
				edmTypeKind2 = payloadTypeKind;
			}
			else
			{
				edmTypeKind2 = typeKindFromPayloadFunc();
			}
			if (ReaderValidationUtils.ShouldValidatePayloadTypeKind(clientCustomTypeResolver, throwIfTypeConflictsWithMetadata, enablePrimitiveTypeConversion, expectedTypeReference, payloadTypeKind))
			{
				ValidationUtils.ValidateTypeKind(edmTypeKind2, expectedTypeReference.TypeKind(), null, payloadTypeName);
			}
			return edmTypeKind2;
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x00017388 File Offset: 0x00015588
		private static bool ShouldValidatePayloadTypeKind(Func<IEdmType, string, IEdmType> clientCustomTypeResolver, bool throwIfTypeConflictsWithMetadata, bool enablePrimitiveTypeConversion, IEdmTypeReference expectedValueTypeReference, EdmTypeKind payloadTypeKind)
		{
			bool flag = clientCustomTypeResolver != null && payloadTypeKind > EdmTypeKind.None;
			return expectedValueTypeReference != null && (throwIfTypeConflictsWithMetadata || flag || (expectedValueTypeReference.IsODataPrimitiveTypeKind() && !enablePrimitiveTypeConversion));
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x000173BC File Offset: 0x000155BC
		private static void ValidateNullValueAllowed(IEdmTypeReference expectedValueTypeReference, bool validateNullValue, string propertyName, bool? isDynamicProperty)
		{
			if (validateNullValue && expectedValueTypeReference != null)
			{
				if (expectedValueTypeReference.IsODataPrimitiveTypeKind())
				{
					if (!expectedValueTypeReference.IsNullable)
					{
						ReaderValidationUtils.ThrowNullValueForNonNullableTypeException(expectedValueTypeReference, propertyName);
						return;
					}
				}
				else if (expectedValueTypeReference.IsODataEnumTypeKind())
				{
					if (!expectedValueTypeReference.IsNullable)
					{
						ReaderValidationUtils.ThrowNullValueForNonNullableTypeException(expectedValueTypeReference, propertyName);
						return;
					}
				}
				else if (expectedValueTypeReference.IsNonEntityCollectionType())
				{
					if (isDynamicProperty != true)
					{
						ReaderValidationUtils.ThrowNullValueForNonNullableTypeException(expectedValueTypeReference, propertyName);
						return;
					}
				}
				else if (expectedValueTypeReference.IsODataComplexTypeKind())
				{
					IEdmComplexTypeReference edmComplexTypeReference = expectedValueTypeReference.AsComplex();
					if (!edmComplexTypeReference.IsNullable)
					{
						ReaderValidationUtils.ThrowNullValueForNonNullableTypeException(expectedValueTypeReference, propertyName);
						return;
					}
				}
				else if (expectedValueTypeReference.IsUntyped() && !expectedValueTypeReference.IsNullable)
				{
					ReaderValidationUtils.ThrowNullValueForNonNullableTypeException(expectedValueTypeReference, propertyName);
				}
			}
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0001746B File Offset: 0x0001566B
		private static void ThrowNullValueForNonNullableTypeException(IEdmTypeReference expectedValueTypeReference, string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				throw new ODataException(Strings.ReaderValidationUtils_NullValueForNonNullableType(expectedValueTypeReference.FullName()));
			}
			throw new ODataException(Strings.ReaderValidationUtils_NullNamedValueForNonNullableType(propertyName, expectedValueTypeReference.FullName()));
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0000239D File Offset: 0x0000059D
		private static void ValidateResourceSetContextUri(ODataJsonLightContextUriParseResult contextUriParseResult, ODataReaderCore.Scope scope, bool updateScope)
		{
		}
	}
}
