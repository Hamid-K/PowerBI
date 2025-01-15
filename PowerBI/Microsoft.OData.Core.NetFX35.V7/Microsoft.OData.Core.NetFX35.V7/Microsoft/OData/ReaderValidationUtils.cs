using System;
using System.Text;
using Microsoft.OData.Edm;
using Microsoft.OData.JsonLight;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x020000AC RID: 172
	internal static class ReaderValidationUtils
	{
		// Token: 0x06000696 RID: 1686 RVA: 0x000121F3 File Offset: 0x000103F3
		internal static void ValidateMessageReaderSettings(ODataMessageReaderSettings messageReaderSettings, bool readingResponse)
		{
			if (messageReaderSettings.BaseUri != null && !messageReaderSettings.BaseUri.IsAbsoluteUri)
			{
				throw new ODataException(Strings.ReaderValidationUtils_MessageReaderSettingsBaseUriMustBeNullOrAbsolute(UriUtils.UriToString(messageReaderSettings.BaseUri)));
			}
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00012226 File Offset: 0x00010426
		internal static void ValidateEntityReferenceLink(ODataEntityReferenceLink link)
		{
			if (link.Url == null)
			{
				throw new ODataException(Strings.ReaderValidationUtils_EntityReferenceLinkMissingUri);
			}
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00012241 File Offset: 0x00010441
		internal static void ValidateStreamReferenceProperty(ODataProperty streamProperty, IEdmStructuredType structuredType, IEdmProperty streamEdmProperty, bool throwOnUndeclaredLinkProperty)
		{
			ValidationUtils.ValidateStreamReferenceProperty(streamProperty, streamEdmProperty);
			if (structuredType != null && structuredType.IsOpen && (streamEdmProperty == null && throwOnUndeclaredLinkProperty))
			{
				ValidationUtils.ValidateOpenPropertyValue(streamProperty.Name, streamProperty.Value);
			}
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0001226E File Offset: 0x0001046E
		internal static void ValidateNullValue(IEdmTypeReference expectedTypeReference, bool enablePrimitiveTypeConversion, bool validateNullValue, string propertyName, bool? isDynamicProperty)
		{
			if (expectedTypeReference != null && (enablePrimitiveTypeConversion || expectedTypeReference.TypeKind() != EdmTypeKind.Primitive))
			{
				ReaderValidationUtils.ValidateNullValueAllowed(expectedTypeReference, validateNullValue, propertyName, isDynamicProperty);
			}
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0001228C File Offset: 0x0001048C
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

		// Token: 0x0600069B RID: 1691 RVA: 0x000122C7 File Offset: 0x000104C7
		internal static ODataException GetPrimitiveTypeConversionException(IEdmPrimitiveTypeReference targetTypeReference, Exception innerException, string stringValue)
		{
			return new ODataException(Strings.ReaderValidationUtils_CannotConvertPrimitiveValue(stringValue, targetTypeReference.FullName()), innerException);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x000122DC File Offset: 0x000104DC
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

		// Token: 0x0600069D RID: 1693 RVA: 0x00012324 File Offset: 0x00010524
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

		// Token: 0x0600069E RID: 1694 RVA: 0x00012408 File Offset: 0x00010608
		internal static IEdmTypeReference ResolveAndValidatePrimitiveTargetType(IEdmTypeReference expectedTypeReference, EdmTypeKind payloadTypeKind, IEdmType payloadType, string payloadTypeName, IEdmType defaultPayloadType, IEdmModel model, Func<IEdmType, string, IEdmType> clientCustomTypeResolver, bool enablePrimitiveTypeConversion, bool throwIfTypeConflictsWithMetadata)
		{
			bool flag = clientCustomTypeResolver != null && payloadType != null;
			if (payloadTypeKind != EdmTypeKind.None && (!enablePrimitiveTypeConversion || throwIfTypeConflictsWithMetadata))
			{
				ValidationUtils.ValidateTypeKind(payloadTypeKind, EdmTypeKind.Primitive, default(bool?), payloadTypeName);
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
			if (payloadType != null && !MetadataUtilsCommon.CanConvertPrimitiveTypeTo(null, (IEdmPrimitiveType)payloadType.AsActualType(), (IEdmPrimitiveType)expectedTypeReference.Definition))
			{
				throw new ODataException(Strings.ValidationUtils_IncompatibleType(payloadTypeName, expectedTypeReference.FullName()));
			}
			return expectedTypeReference;
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x000124A8 File Offset: 0x000106A8
		internal static IEdmTypeReference ResolveAndValidateNonPrimitiveTargetType(EdmTypeKind expectedTypeKind, IEdmTypeReference expectedTypeReference, EdmTypeKind payloadTypeKind, IEdmType payloadType, string payloadTypeName, IEdmModel model, Func<IEdmType, string, IEdmType> clientCustomTypeResolver, bool throwIfTypeConflictsWithMetadata)
		{
			bool flag = clientCustomTypeResolver != null && payloadType != null;
			if (!flag && model.IsUserModel() && (expectedTypeReference == null || throwIfTypeConflictsWithMetadata))
			{
				ReaderValidationUtils.VerifyPayloadTypeDefined(payloadTypeName, payloadType);
			}
			if (payloadTypeKind != EdmTypeKind.None && (throwIfTypeConflictsWithMetadata || expectedTypeReference == null))
			{
				ValidationUtils.ValidateTypeKind(payloadTypeKind, expectedTypeKind, default(bool?), payloadTypeName);
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

		// Token: 0x060006A0 RID: 1696 RVA: 0x0001252A File Offset: 0x0001072A
		internal static void ValidateEncodingSupportedInBatch(Encoding encoding)
		{
			if (!encoding.IsSingleByte && Encoding.UTF8.CodePage != encoding.CodePage)
			{
				throw new ODataException(Strings.ODataBatchReaderStream_MultiByteEncodingsNotSupported(encoding.WebName));
			}
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00012557 File Offset: 0x00010757
		internal static void ValidateEncodingSupportedInAsync(Encoding encoding)
		{
			if (!encoding.IsSingleByte && Encoding.UTF8.CodePage != encoding.CodePage)
			{
				throw new ODataException(Strings.ODataAsyncReader_MultiByteEncodingsNotSupported(encoding.WebName));
			}
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00012584 File Offset: 0x00010784
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
					scope.ResourceType = edmStructuredType;
					return;
				}
			}
			else if (scope.ResourceType.IsAssignableFrom(edmStructuredType))
			{
				if (updateScope)
				{
					scope.ResourceType = edmStructuredType;
					return;
				}
			}
			else if (!edmStructuredType.IsAssignableFrom(scope.ResourceType))
			{
				throw new ODataException(Strings.ReaderValidationUtils_ContextUriValidationInvalidExpectedEntityType(UriUtils.UriToString(contextUriParseResult.ContextUri), contextUriParseResult.EdmType.FullTypeName(), scope.ResourceType.FullTypeName()));
			}
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00012688 File Offset: 0x00010888
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

		// Token: 0x060006A4 RID: 1700 RVA: 0x00012748 File Offset: 0x00010948
		internal static void ValidateOperationProperty(object propertyValue, string propertyName, string metadata, string operationsHeader)
		{
			if (propertyValue == null)
			{
				throw new ODataException(Strings.ODataJsonOperationsDeserializerUtils_OperationPropertyCannotBeNull(propertyName, metadata, operationsHeader));
			}
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0001275C File Offset: 0x0001095C
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

		// Token: 0x060006A6 RID: 1702 RVA: 0x00012788 File Offset: 0x00010988
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

		// Token: 0x060006A7 RID: 1703 RVA: 0x00012840 File Offset: 0x00010A40
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

		// Token: 0x060006A8 RID: 1704 RVA: 0x0001294A File Offset: 0x00010B4A
		private static void VerifyPayloadTypeDefined(string payloadTypeName, IEdmType payloadType)
		{
			if (payloadTypeName != null && payloadType == null)
			{
				throw new ODataException(Strings.ValidationUtils_UnrecognizedTypeName(payloadTypeName));
			}
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00012960 File Offset: 0x00010B60
		private static void VerifyComplexType(IEdmTypeReference expectedTypeReference, IEdmType payloadType, bool failIfNotRelated)
		{
			IEdmStructuredType edmStructuredType = expectedTypeReference.AsStructured().StructuredDefinition();
			IEdmStructuredType edmStructuredType2 = (IEdmStructuredType)payloadType;
			if (!edmStructuredType.IsAssignableFrom(edmStructuredType2) && failIfNotRelated)
			{
				throw new ODataException(Strings.ValidationUtils_IncompatibleType(edmStructuredType2.FullTypeName(), edmStructuredType.FullTypeName()));
			}
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x000129A4 File Offset: 0x00010BA4
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

		// Token: 0x060006AB RID: 1707 RVA: 0x000129F5 File Offset: 0x00010BF5
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

		// Token: 0x060006AC RID: 1708 RVA: 0x00012A1C File Offset: 0x00010C1C
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
				edmTypeKind2 = typeKindFromPayloadFunc.Invoke();
			}
			if (ReaderValidationUtils.ShouldValidatePayloadTypeKind(clientCustomTypeResolver, throwIfTypeConflictsWithMetadata, enablePrimitiveTypeConversion, expectedTypeReference, payloadTypeKind))
			{
				ValidationUtils.ValidateTypeKind(edmTypeKind2, expectedTypeReference.TypeKind(), default(bool?), payloadTypeName);
			}
			return edmTypeKind2;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00012A8C File Offset: 0x00010C8C
		private static bool ShouldValidatePayloadTypeKind(Func<IEdmType, string, IEdmType> clientCustomTypeResolver, bool throwIfTypeConflictsWithMetadata, bool enablePrimitiveTypeConversion, IEdmTypeReference expectedValueTypeReference, EdmTypeKind payloadTypeKind)
		{
			bool flag = clientCustomTypeResolver != null && payloadTypeKind > EdmTypeKind.None;
			return expectedValueTypeReference != null && (throwIfTypeConflictsWithMetadata || flag || (expectedValueTypeReference.IsODataPrimitiveTypeKind() && !enablePrimitiveTypeConversion));
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00012AC0 File Offset: 0x00010CC0
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
					}
				}
			}
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00012B4E File Offset: 0x00010D4E
		private static void ThrowNullValueForNonNullableTypeException(IEdmTypeReference expectedValueTypeReference, string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				throw new ODataException(Strings.ReaderValidationUtils_NullValueForNonNullableType(expectedValueTypeReference.FullName()));
			}
			throw new ODataException(Strings.ReaderValidationUtils_NullNamedValueForNonNullableType(propertyName, expectedValueTypeReference.FullName()));
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0000250D File Offset: 0x0000070D
		private static void ValidateResourceSetContextUri(ODataJsonLightContextUriParseResult contextUriParseResult, ODataReaderCore.Scope scope, bool updateScope)
		{
		}
	}
}
