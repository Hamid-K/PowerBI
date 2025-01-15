using System;
using System.Text;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.JsonLight;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x02000244 RID: 580
	internal static class ReaderValidationUtils
	{
		// Token: 0x06001199 RID: 4505 RVA: 0x00042A34 File Offset: 0x00040C34
		internal static void ValidateMessageReaderSettings(ODataMessageReaderSettings messageReaderSettings, bool readingResponse)
		{
			if (messageReaderSettings.BaseUri != null && !messageReaderSettings.BaseUri.IsAbsoluteUri)
			{
				throw new ODataException(Strings.ReaderValidationUtils_MessageReaderSettingsBaseUriMustBeNullOrAbsolute(UriUtilsCommon.UriToString(messageReaderSettings.BaseUri)));
			}
			if (!readingResponse && messageReaderSettings.UndeclaredPropertyBehaviorKinds != ODataUndeclaredPropertyBehaviorKinds.None)
			{
				throw new ODataException(Strings.ReaderValidationUtils_UndeclaredPropertyBehaviorKindSpecifiedOnRequest);
			}
			if (!string.IsNullOrEmpty(messageReaderSettings.ReaderBehavior.ODataTypeScheme) && !string.Equals(messageReaderSettings.ReaderBehavior.ODataTypeScheme, "http://schemas.microsoft.com/ado/2007/08/dataservices/scheme"))
			{
				ODataVersionChecker.CheckCustomTypeScheme(messageReaderSettings.MaxProtocolVersion);
			}
			if (!string.IsNullOrEmpty(messageReaderSettings.ReaderBehavior.ODataNamespace) && !string.Equals(messageReaderSettings.ReaderBehavior.ODataNamespace, "http://schemas.microsoft.com/ado/2007/08/dataservices"))
			{
				ODataVersionChecker.CheckCustomDataNamespace(messageReaderSettings.MaxProtocolVersion);
			}
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x00042AF0 File Offset: 0x00040CF0
		internal static void ValidateEntityReferenceLink(ODataEntityReferenceLink link)
		{
			if (link.Url == null)
			{
				throw new ODataException(Strings.ReaderValidationUtils_EntityReferenceLinkMissingUri);
			}
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x00042B0B File Offset: 0x00040D0B
		internal static void ValidateStreamReferenceProperty(ODataProperty streamProperty, IEdmStructuredType structuredType, IEdmProperty streamEdmProperty, ODataMessageReaderSettings messageReaderSettings)
		{
			ValidationUtils.ValidateStreamReferenceProperty(streamProperty, streamEdmProperty);
			if (structuredType != null && structuredType.IsOpen && streamEdmProperty == null && !messageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.ReportUndeclaredLinkProperty))
			{
				ValidationUtils.ValidateOpenPropertyValue(streamProperty.Name, streamProperty.Value, messageReaderSettings.UndeclaredPropertyBehaviorKinds);
			}
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x00042B42 File Offset: 0x00040D42
		internal static void ValidateNullValue(IEdmModel model, IEdmTypeReference expectedTypeReference, ODataMessageReaderSettings messageReaderSettings, bool validateNullValue, ODataVersion version, string propertyName)
		{
			if (expectedTypeReference != null)
			{
				ReaderValidationUtils.ValidateTypeSupported(expectedTypeReference, version);
				if (!messageReaderSettings.DisablePrimitiveTypeConversion || expectedTypeReference.TypeKind() != EdmTypeKind.Primitive)
				{
					ReaderValidationUtils.ValidateNullValueAllowed(expectedTypeReference, validateNullValue, model, propertyName);
				}
			}
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x00042B6A File Offset: 0x00040D6A
		internal static void ValidateEntry(ODataEntry entry)
		{
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x00042B6C File Offset: 0x00040D6C
		internal static IEdmProperty FindDefinedProperty(string propertyName, IEdmStructuredType owningStructuredType)
		{
			if (owningStructuredType == null)
			{
				return null;
			}
			return owningStructuredType.FindProperty(propertyName);
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x00042B88 File Offset: 0x00040D88
		internal static IEdmProperty ValidateValuePropertyDefined(string propertyName, IEdmStructuredType owningStructuredType, ODataMessageReaderSettings messageReaderSettings, out bool ignoreProperty)
		{
			ignoreProperty = false;
			if (owningStructuredType == null)
			{
				return null;
			}
			IEdmProperty edmProperty = ReaderValidationUtils.FindDefinedProperty(propertyName, owningStructuredType);
			if (edmProperty == null && !owningStructuredType.IsOpen)
			{
				if (messageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.IgnoreUndeclaredValueProperty))
				{
					ignoreProperty = true;
				}
				else if (!messageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.SupportUndeclaredValueProperty))
				{
					throw new ODataException(Strings.ValidationUtils_PropertyDoesNotExistOnType(propertyName, owningStructuredType.ODataFullName()));
				}
			}
			return edmProperty;
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x00042BDA File Offset: 0x00040DDA
		internal static void ValidateExpectedPropertyName(string expectedPropertyName, string payloadPropertyName)
		{
			if (expectedPropertyName != null && string.CompareOrdinal(expectedPropertyName, payloadPropertyName) != 0)
			{
				throw new ODataException(Strings.ReaderValidationUtils_NonMatchingPropertyNames(payloadPropertyName, expectedPropertyName));
			}
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x00042BF8 File Offset: 0x00040DF8
		internal static IEdmProperty ValidateLinkPropertyDefined(string propertyName, IEdmStructuredType owningStructuredType, ODataMessageReaderSettings messageReaderSettings)
		{
			if (owningStructuredType == null)
			{
				return null;
			}
			IEdmProperty edmProperty = ReaderValidationUtils.FindDefinedProperty(propertyName, owningStructuredType);
			if (edmProperty == null && !owningStructuredType.IsOpen && !messageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.ReportUndeclaredLinkProperty))
			{
				throw new ODataException(Strings.ValidationUtils_PropertyDoesNotExistOnType(propertyName, owningStructuredType.ODataFullName()));
			}
			return edmProperty;
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x00042C3C File Offset: 0x00040E3C
		internal static IEdmNavigationProperty ValidateNavigationPropertyDefined(string propertyName, IEdmEntityType owningEntityType, ODataMessageReaderSettings messageReaderSettings)
		{
			if (owningEntityType == null)
			{
				return null;
			}
			IEdmProperty edmProperty = ReaderValidationUtils.ValidateLinkPropertyDefined(propertyName, owningEntityType, messageReaderSettings);
			if (edmProperty == null)
			{
				if (owningEntityType.IsOpen && !messageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.ReportUndeclaredLinkProperty))
				{
					throw new ODataException(Strings.ValidationUtils_OpenNavigationProperty(propertyName, owningEntityType.ODataFullName()));
				}
			}
			else if (edmProperty.PropertyKind != EdmPropertyKind.Navigation)
			{
				throw new ODataException(Strings.ValidationUtils_NavigationPropertyExpected(propertyName, owningEntityType.ODataFullName(), edmProperty.PropertyKind.ToString()));
			}
			return (IEdmNavigationProperty)edmProperty;
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x00042CAE File Offset: 0x00040EAE
		internal static ODataException GetPrimitiveTypeConversionException(IEdmPrimitiveTypeReference targetTypeReference, Exception innerException)
		{
			return new ODataException(Strings.ReaderValidationUtils_CannotConvertPrimitiveValue(targetTypeReference.ODataFullName()), innerException);
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x00042CC4 File Offset: 0x00040EC4
		internal static IEdmType ResolvePayloadTypeName(IEdmModel model, IEdmTypeReference expectedTypeReference, string payloadTypeName, EdmTypeKind expectedTypeKind, ODataReaderBehavior readerBehavior, ODataVersion version, out EdmTypeKind payloadTypeKind)
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
			IEdmType edmType = MetadataUtils.ResolveTypeNameForRead(model, (expectedTypeReference == null) ? null : expectedTypeReference.Definition, payloadTypeName, readerBehavior, version, out payloadTypeKind);
			if (payloadTypeKind == EdmTypeKind.None)
			{
				payloadTypeKind = expectedTypeKind;
			}
			return edmType;
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x00042D0C File Offset: 0x00040F0C
		internal static IEdmTypeReference ResolvePayloadTypeNameAndComputeTargetType(EdmTypeKind expectedTypeKind, IEdmType defaultPrimitivePayloadType, IEdmTypeReference expectedTypeReference, string payloadTypeName, IEdmModel model, ODataMessageReaderSettings messageReaderSettings, ODataVersion version, Func<EdmTypeKind> typeKindPeekedFromPayloadFunc, out EdmTypeKind targetTypeKind, out SerializationTypeNameAnnotation serializationTypeNameAnnotation)
		{
			serializationTypeNameAnnotation = null;
			EdmTypeKind edmTypeKind;
			IEdmType edmType = ReaderValidationUtils.ResolvePayloadTypeName(model, expectedTypeReference, payloadTypeName, EdmTypeKind.Complex, messageReaderSettings.ReaderBehavior, version, out edmTypeKind);
			targetTypeKind = ReaderValidationUtils.ComputeTargetTypeKind(expectedTypeReference, expectedTypeKind == EdmTypeKind.Entity, payloadTypeName, edmTypeKind, messageReaderSettings, typeKindPeekedFromPayloadFunc);
			IEdmTypeReference edmTypeReference;
			if (targetTypeKind == EdmTypeKind.Primitive)
			{
				edmTypeReference = ReaderValidationUtils.ResolveAndValidatePrimitiveTargetType(expectedTypeReference, edmTypeKind, edmType, payloadTypeName, defaultPrimitivePayloadType, model, messageReaderSettings, version);
			}
			else
			{
				edmTypeReference = ReaderValidationUtils.ResolveAndValidateNonPrimitiveTargetType(targetTypeKind, expectedTypeReference, edmTypeKind, edmType, payloadTypeName, model, messageReaderSettings, version);
				if (edmTypeReference != null)
				{
					serializationTypeNameAnnotation = ReaderValidationUtils.CreateSerializationTypeNameAnnotation(payloadTypeName, edmTypeReference);
				}
			}
			if (expectedTypeKind != EdmTypeKind.None && edmTypeReference != null)
			{
				ValidationUtils.ValidateTypeKind(targetTypeKind, expectedTypeKind, payloadTypeName);
			}
			return edmTypeReference;
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x00042D90 File Offset: 0x00040F90
		internal static IEdmTypeReference ResolveAndValidatePrimitiveTargetType(IEdmTypeReference expectedTypeReference, EdmTypeKind payloadTypeKind, IEdmType payloadType, string payloadTypeName, IEdmType defaultPayloadType, IEdmModel model, ODataMessageReaderSettings messageReaderSettings, ODataVersion version)
		{
			bool flag = messageReaderSettings.ReaderBehavior.TypeResolver != null && payloadType != null;
			if (expectedTypeReference != null && !flag)
			{
				ReaderValidationUtils.ValidateTypeSupported(expectedTypeReference, version);
			}
			if (payloadTypeKind != EdmTypeKind.None && (messageReaderSettings.DisablePrimitiveTypeConversion || !messageReaderSettings.DisableStrictMetadataValidation))
			{
				ValidationUtils.ValidateTypeKind(payloadTypeKind, EdmTypeKind.Primitive, payloadTypeName);
			}
			if (!model.IsUserModel())
			{
				return MetadataUtils.GetNullablePayloadTypeReference(payloadType ?? defaultPayloadType);
			}
			if (expectedTypeReference == null || flag || messageReaderSettings.DisablePrimitiveTypeConversion)
			{
				return MetadataUtils.GetNullablePayloadTypeReference(payloadType ?? defaultPayloadType);
			}
			if (messageReaderSettings.DisableStrictMetadataValidation)
			{
				return expectedTypeReference;
			}
			if (payloadType != null && !MetadataUtilsCommon.CanConvertPrimitiveTypeTo((IEdmPrimitiveType)payloadType, (IEdmPrimitiveType)expectedTypeReference.Definition))
			{
				throw new ODataException(Strings.ValidationUtils_IncompatibleType(payloadTypeName, expectedTypeReference.ODataFullName()));
			}
			return expectedTypeReference;
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x00042E4C File Offset: 0x0004104C
		internal static IEdmTypeReference ResolveAndValidateNonPrimitiveTargetType(EdmTypeKind expectedTypeKind, IEdmTypeReference expectedTypeReference, EdmTypeKind payloadTypeKind, IEdmType payloadType, string payloadTypeName, IEdmModel model, ODataMessageReaderSettings messageReaderSettings, ODataVersion version)
		{
			bool flag = messageReaderSettings.ReaderBehavior.TypeResolver != null && payloadType != null;
			if (!flag)
			{
				ReaderValidationUtils.ValidateTypeSupported(expectedTypeReference, version);
				if (model.IsUserModel() && (expectedTypeReference == null || !messageReaderSettings.DisableStrictMetadataValidation))
				{
					ReaderValidationUtils.VerifyPayloadTypeDefined(payloadTypeName, payloadType);
				}
			}
			else
			{
				ReaderValidationUtils.ValidateTypeSupported((payloadType == null) ? null : payloadType.ToTypeReference(true), version);
			}
			if (payloadTypeKind != EdmTypeKind.None && (!messageReaderSettings.DisableStrictMetadataValidation || expectedTypeReference == null))
			{
				ValidationUtils.ValidateTypeKind(payloadTypeKind, expectedTypeKind, payloadTypeName);
			}
			if (!model.IsUserModel())
			{
				return null;
			}
			if (expectedTypeReference == null || flag)
			{
				return ReaderValidationUtils.ResolveAndValidateTargetTypeWithNoExpectedType(expectedTypeKind, payloadType, messageReaderSettings.UndeclaredPropertyBehaviorKinds);
			}
			if (messageReaderSettings.DisableStrictMetadataValidation)
			{
				return ReaderValidationUtils.ResolveAndValidateTargetTypeStrictValidationDisabled(expectedTypeKind, expectedTypeReference, payloadType);
			}
			return ReaderValidationUtils.ResolveAndValidateTargetTypeStrictValidationEnabled(expectedTypeKind, expectedTypeReference, payloadType);
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x00042F02 File Offset: 0x00041102
		internal static void ValidateEncodingSupportedInBatch(Encoding encoding)
		{
			if (!encoding.IsSingleByte && Encoding.UTF8.CodePage != encoding.CodePage)
			{
				throw new ODataException(Strings.ODataBatchReaderStream_MultiByteEncodingsNotSupported(encoding.WebName));
			}
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x00042F2F File Offset: 0x0004112F
		internal static void ValidateTypeSupported(IEdmTypeReference typeReference, ODataVersion version)
		{
			if (typeReference != null)
			{
				if (typeReference.IsNonEntityCollectionType())
				{
					ODataVersionChecker.CheckCollectionValue(version);
					return;
				}
				if (typeReference.IsSpatial())
				{
					ODataVersionChecker.CheckSpatialValue(version);
				}
			}
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x00042F54 File Offset: 0x00041154
		internal static void ValidateFeedOrEntryMetadataUri(ODataJsonLightMetadataUriParseResult metadataUriParseResult, ODataReaderCore.Scope scope)
		{
			if (scope.EntitySet == null)
			{
				scope.EntitySet = metadataUriParseResult.EntitySet;
			}
			else if (string.CompareOrdinal(scope.EntitySet.FullName(), metadataUriParseResult.EntitySet.FullName()) != 0)
			{
				throw new ODataException(Strings.ReaderValidationUtils_MetadataUriValidationInvalidExpectedEntitySet(UriUtilsCommon.UriToString(metadataUriParseResult.MetadataUri), metadataUriParseResult.EntitySet.FullName(), scope.EntitySet.FullName()));
			}
			IEdmEntityType edmEntityType = (IEdmEntityType)metadataUriParseResult.EdmType;
			if (scope.EntityType == null)
			{
				scope.EntityType = edmEntityType;
				return;
			}
			if (scope.EntityType.IsAssignableFrom(edmEntityType))
			{
				scope.EntityType = edmEntityType;
				return;
			}
			if (!edmEntityType.IsAssignableFrom(scope.EntityType))
			{
				throw new ODataException(Strings.ReaderValidationUtils_MetadataUriValidationInvalidExpectedEntityType(UriUtilsCommon.UriToString(metadataUriParseResult.MetadataUri), metadataUriParseResult.EdmType.ODataFullName(), scope.EntityType.FullName()));
			}
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x0004302C File Offset: 0x0004122C
		internal static void ValidateEntityReferenceLinkMetadataUri(ODataJsonLightMetadataUriParseResult metadataUriParseResult, IEdmNavigationProperty navigationProperty)
		{
			if (navigationProperty == null)
			{
				return;
			}
			IEdmNavigationProperty navigationProperty2 = metadataUriParseResult.NavigationProperty;
			if (string.CompareOrdinal(navigationProperty.Name, navigationProperty2.Name) != 0)
			{
				throw new ODataException(Strings.ReaderValidationUtils_MetadataUriValidationNonMatchingPropertyNames(UriUtilsCommon.UriToString(metadataUriParseResult.MetadataUri), navigationProperty2.Name, navigationProperty2.DeclaringEntityType().FullName(), navigationProperty.Name));
			}
			if (!navigationProperty.DeclaringType.IsEquivalentTo(navigationProperty2.DeclaringType))
			{
				throw new ODataException(Strings.ReaderValidationUtils_MetadataUriValidationNonMatchingDeclaringTypes(UriUtilsCommon.UriToString(metadataUriParseResult.MetadataUri), navigationProperty2.Name, navigationProperty2.DeclaringEntityType().FullName(), navigationProperty.DeclaringEntityType().FullName()));
			}
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x000430CC File Offset: 0x000412CC
		internal static IEdmTypeReference ValidateCollectionMetadataUriAndGetPayloadItemTypeReference(ODataJsonLightMetadataUriParseResult metadataUriParseResult, IEdmTypeReference expectedItemTypeReference)
		{
			if (metadataUriParseResult == null)
			{
				return expectedItemTypeReference;
			}
			IEdmCollectionType edmCollectionType = (IEdmCollectionType)metadataUriParseResult.EdmType;
			if (expectedItemTypeReference != null && !expectedItemTypeReference.IsAssignableFrom(edmCollectionType.ElementType))
			{
				throw new ODataException(Strings.ReaderValidationUtils_MetadataUriDoesNotReferTypeAssignableToExpectedType(UriUtilsCommon.UriToString(metadataUriParseResult.MetadataUri), edmCollectionType.ElementType.ODataFullName(), expectedItemTypeReference.ODataFullName()));
			}
			return edmCollectionType.ElementType;
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x00043128 File Offset: 0x00041328
		internal static void ValidateOperationProperty(object propertyValue, string propertyName, string metadata, string operationsHeader)
		{
			if (propertyValue == null)
			{
				throw new ODataException(Strings.ODataJsonOperationsDeserializerUtils_OperationPropertyCannotBeNull(propertyName, metadata, operationsHeader));
			}
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x0004313C File Offset: 0x0004133C
		private static IEdmTypeReference ResolveAndValidateTargetTypeWithNoExpectedType(EdmTypeKind expectedTypeKind, IEdmType payloadType, ODataUndeclaredPropertyBehaviorKinds undeclaredPropertyBehaviorKinds)
		{
			if (payloadType != null)
			{
				return payloadType.ToTypeReference(true);
			}
			if (expectedTypeKind == EdmTypeKind.Entity)
			{
				throw new ODataException(Strings.ReaderValidationUtils_EntryWithoutType);
			}
			if (undeclaredPropertyBehaviorKinds.HasFlag(ODataUndeclaredPropertyBehaviorKinds.IgnoreUndeclaredValueProperty) || undeclaredPropertyBehaviorKinds.HasFlag(ODataUndeclaredPropertyBehaviorKinds.SupportUndeclaredValueProperty))
			{
				return null;
			}
			throw new ODataException(Strings.ReaderValidationUtils_ValueWithoutType);
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x0004318C File Offset: 0x0004138C
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
				if (payloadType != null && expectedTypeKind == payloadType.TypeKind)
				{
					ReaderValidationUtils.VerifyComplexType(expectedTypeReference, payloadType, false);
					return expectedTypeReference;
				}
				return expectedTypeReference;
			case EdmTypeKind.Collection:
				if (payloadType != null && expectedTypeKind == payloadType.TypeKind)
				{
					ReaderValidationUtils.VerifyCollectionComplexItemType(expectedTypeReference, payloadType);
					return expectedTypeReference;
				}
				return expectedTypeReference;
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ReaderValidationUtils_ResolveAndValidateTypeName_Strict_TypeKind));
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x00043224 File Offset: 0x00041424
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
					return expectedTypeReference;
				}
				return expectedTypeReference;
			case EdmTypeKind.Collection:
				if (payloadType != null && string.CompareOrdinal(payloadType.ODataFullName(), expectedTypeReference.ODataFullName()) != 0)
				{
					ReaderValidationUtils.VerifyCollectionComplexItemType(expectedTypeReference, payloadType);
					throw new ODataException(Strings.ValidationUtils_IncompatibleType(payloadType.ODataFullName(), expectedTypeReference.ODataFullName()));
				}
				return expectedTypeReference;
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ReaderValidationUtils_ResolveAndValidateTypeName_Strict_TypeKind));
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x000432BF File Offset: 0x000414BF
		private static void VerifyPayloadTypeDefined(string payloadTypeName, IEdmType payloadType)
		{
			if (payloadTypeName != null && payloadType == null)
			{
				throw new ODataException(Strings.ValidationUtils_UnrecognizedTypeName(payloadTypeName));
			}
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x000432D4 File Offset: 0x000414D4
		private static void VerifyComplexType(IEdmTypeReference expectedTypeReference, IEdmType payloadType, bool failIfNotRelated)
		{
			IEdmStructuredType edmStructuredType = expectedTypeReference.AsStructured().StructuredDefinition();
			IEdmStructuredType edmStructuredType2 = (IEdmStructuredType)payloadType;
			if (!edmStructuredType.IsEquivalentTo(edmStructuredType2))
			{
				if (edmStructuredType.IsAssignableFrom(edmStructuredType2))
				{
					throw new ODataException(Strings.ReaderValidationUtils_DerivedComplexTypesAreNotAllowed(edmStructuredType.ODataFullName(), edmStructuredType2.ODataFullName()));
				}
				if (failIfNotRelated)
				{
					throw new ODataException(Strings.ValidationUtils_IncompatibleType(edmStructuredType2.ODataFullName(), edmStructuredType.ODataFullName()));
				}
			}
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x00043338 File Offset: 0x00041538
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

		// Token: 0x060011B4 RID: 4532 RVA: 0x0004338C File Offset: 0x0004158C
		private static SerializationTypeNameAnnotation CreateSerializationTypeNameAnnotation(string payloadTypeName, IEdmTypeReference targetTypeReference)
		{
			if (payloadTypeName != null && string.CompareOrdinal(payloadTypeName, targetTypeReference.ODataFullName()) != 0)
			{
				return new SerializationTypeNameAnnotation
				{
					TypeName = payloadTypeName
				};
			}
			if (payloadTypeName == null)
			{
				return new SerializationTypeNameAnnotation
				{
					TypeName = null
				};
			}
			return null;
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x000433CC File Offset: 0x000415CC
		private static EdmTypeKind ComputeTargetTypeKind(IEdmTypeReference expectedTypeReference, bool forEntityValue, string payloadTypeName, EdmTypeKind payloadTypeKind, ODataMessageReaderSettings messageReaderSettings, Func<EdmTypeKind> typeKindFromPayloadFunc)
		{
			bool flag = messageReaderSettings.ReaderBehavior.TypeResolver != null && payloadTypeKind != EdmTypeKind.None;
			EdmTypeKind edmTypeKind = EdmTypeKind.None;
			if (!flag)
			{
				edmTypeKind = ReaderValidationUtils.GetExpectedTypeKind(expectedTypeReference, messageReaderSettings);
			}
			EdmTypeKind edmTypeKind2;
			if (edmTypeKind != EdmTypeKind.None)
			{
				edmTypeKind2 = edmTypeKind;
			}
			else if (payloadTypeKind != EdmTypeKind.None)
			{
				if (!forEntityValue)
				{
					ValidationUtils.ValidateValueTypeKind(payloadTypeKind, payloadTypeName);
				}
				edmTypeKind2 = payloadTypeKind;
			}
			else
			{
				edmTypeKind2 = typeKindFromPayloadFunc.Invoke();
			}
			if (ReaderValidationUtils.ShouldValidatePayloadTypeKind(messageReaderSettings, expectedTypeReference, payloadTypeKind))
			{
				ValidationUtils.ValidateTypeKind(edmTypeKind2, expectedTypeReference.TypeKind(), payloadTypeName);
			}
			return edmTypeKind2;
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x0004343C File Offset: 0x0004163C
		private static EdmTypeKind GetExpectedTypeKind(IEdmTypeReference expectedTypeReference, ODataMessageReaderSettings messageReaderSettings)
		{
			IEdmType definition;
			if (expectedTypeReference == null || (definition = expectedTypeReference.Definition) == null)
			{
				return EdmTypeKind.None;
			}
			EdmTypeKind typeKind = definition.TypeKind;
			if (messageReaderSettings.DisablePrimitiveTypeConversion && typeKind == EdmTypeKind.Primitive && !definition.IsStream())
			{
				return EdmTypeKind.None;
			}
			return typeKind;
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x00043478 File Offset: 0x00041678
		private static bool ShouldValidatePayloadTypeKind(ODataMessageReaderSettings messageReaderSettings, IEdmTypeReference expectedValueTypeReference, EdmTypeKind payloadTypeKind)
		{
			bool flag = messageReaderSettings.ReaderBehavior.TypeResolver != null && payloadTypeKind != EdmTypeKind.None;
			return expectedValueTypeReference != null && (!messageReaderSettings.DisableStrictMetadataValidation || flag || (expectedValueTypeReference.IsODataPrimitiveTypeKind() && messageReaderSettings.DisablePrimitiveTypeConversion));
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x000434C0 File Offset: 0x000416C0
		private static void ValidateNullValueAllowed(IEdmTypeReference expectedValueTypeReference, bool validateNullValue, IEdmModel model, string propertyName)
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
				else
				{
					if (expectedValueTypeReference.IsNonEntityCollectionType())
					{
						ReaderValidationUtils.ThrowNullValueForNonNullableTypeException(expectedValueTypeReference, propertyName);
						return;
					}
					if (expectedValueTypeReference.IsODataComplexTypeKind() && ValidationUtils.ShouldValidateComplexPropertyNullValue(model))
					{
						IEdmComplexTypeReference edmComplexTypeReference = expectedValueTypeReference.AsComplex();
						if (!edmComplexTypeReference.IsNullable)
						{
							ReaderValidationUtils.ThrowNullValueForNonNullableTypeException(expectedValueTypeReference, propertyName);
						}
					}
				}
			}
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x00043521 File Offset: 0x00041721
		private static void ThrowNullValueForNonNullableTypeException(IEdmTypeReference expectedValueTypeReference, string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				throw new ODataException(Strings.ReaderValidationUtils_NullValueForNonNullableType(expectedValueTypeReference.ODataFullName()));
			}
			throw new ODataException(Strings.ReaderValidationUtils_NullNamedValueForNonNullableType(propertyName, expectedValueTypeReference.ODataFullName()));
		}
	}
}
