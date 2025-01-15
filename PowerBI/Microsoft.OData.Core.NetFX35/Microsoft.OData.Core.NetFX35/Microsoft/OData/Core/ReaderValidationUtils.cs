using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.OData.Core.JsonLight;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x020001B4 RID: 436
	internal static class ReaderValidationUtils
	{
		// Token: 0x0600101B RID: 4123 RVA: 0x00037A8C File Offset: 0x00035C8C
		internal static void ValidateMessageReaderSettings(ODataMessageReaderSettings messageReaderSettings, bool readingResponse)
		{
			if (messageReaderSettings.BaseUri != null && !messageReaderSettings.BaseUri.IsAbsoluteUri)
			{
				throw new ODataException(Strings.ReaderValidationUtils_MessageReaderSettingsBaseUriMustBeNullOrAbsolute(UriUtils.UriToString(messageReaderSettings.BaseUri)));
			}
			if (!readingResponse && messageReaderSettings.UndeclaredPropertyBehaviorKinds != ODataUndeclaredPropertyBehaviorKinds.None)
			{
				throw new ODataException(Strings.ReaderValidationUtils_UndeclaredPropertyBehaviorKindSpecifiedOnRequest);
			}
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x00037AE0 File Offset: 0x00035CE0
		internal static void ValidateEntityReferenceLink(ODataEntityReferenceLink link)
		{
			if (link.Url == null)
			{
				throw new ODataException(Strings.ReaderValidationUtils_EntityReferenceLinkMissingUri);
			}
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x00037AFB File Offset: 0x00035CFB
		internal static void ValidateStreamReferenceProperty(ODataProperty streamProperty, IEdmStructuredType structuredType, IEdmProperty streamEdmProperty, ODataMessageReaderSettings messageReaderSettings)
		{
			ValidationUtils.ValidateStreamReferenceProperty(streamProperty, streamEdmProperty);
			if (structuredType != null && structuredType.IsOpen && streamEdmProperty == null && !messageReaderSettings.ReportUndeclaredLinkProperties)
			{
				ValidationUtils.ValidateOpenPropertyValue(streamProperty.Name, streamProperty.Value);
			}
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x00037B2B File Offset: 0x00035D2B
		internal static void ValidateNullValue(IEdmModel model, IEdmTypeReference expectedTypeReference, ODataMessageReaderSettings messageReaderSettings, bool validateNullValue, string propertyName, bool? isDynamicProperty = null)
		{
			if (expectedTypeReference != null)
			{
				ReaderValidationUtils.ValidateTypeSupported(expectedTypeReference);
				if (!messageReaderSettings.DisablePrimitiveTypeConversion || expectedTypeReference.TypeKind() != EdmTypeKind.Primitive)
				{
					ReaderValidationUtils.ValidateNullValueAllowed(expectedTypeReference, validateNullValue, model, propertyName, isDynamicProperty);
				}
			}
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x00037B53 File Offset: 0x00035D53
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "entry", Justification = "Used only in debug asserts.")]
		internal static void ValidateEntry(ODataEntry entry)
		{
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x00037B58 File Offset: 0x00035D58
		internal static IEdmProperty FindDefinedProperty(string propertyName, IEdmStructuredType owningStructuredType)
		{
			if (owningStructuredType == null)
			{
				return null;
			}
			return owningStructuredType.FindProperty(propertyName);
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x00037B74 File Offset: 0x00035D74
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
				if (!messageReaderSettings.IgnoreUndeclaredValueProperties)
				{
					throw new ODataException(Strings.ValidationUtils_PropertyDoesNotExistOnType(propertyName, owningStructuredType.FullTypeName()));
				}
				ignoreProperty = true;
			}
			return edmProperty;
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x00037BBC File Offset: 0x00035DBC
		internal static void ValidateExpectedPropertyName(string expectedPropertyName, string payloadPropertyName)
		{
			if (expectedPropertyName != null && string.CompareOrdinal(expectedPropertyName, payloadPropertyName) != 0)
			{
				throw new ODataException(Strings.ReaderValidationUtils_NonMatchingPropertyNames(payloadPropertyName, expectedPropertyName));
			}
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x00037BD8 File Offset: 0x00035DD8
		internal static IEdmProperty ValidateLinkPropertyDefined(string propertyName, IEdmStructuredType owningStructuredType, ODataMessageReaderSettings messageReaderSettings)
		{
			if (owningStructuredType == null)
			{
				return null;
			}
			IEdmProperty edmProperty = ReaderValidationUtils.FindDefinedProperty(propertyName, owningStructuredType);
			if (edmProperty == null && !owningStructuredType.IsOpen && !messageReaderSettings.ReportUndeclaredLinkProperties)
			{
				throw new ODataException(Strings.ValidationUtils_PropertyDoesNotExistOnType(propertyName, owningStructuredType.FullTypeName()));
			}
			return edmProperty;
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x00037C18 File Offset: 0x00035E18
		internal static IEdmNavigationProperty ValidateNavigationPropertyDefined(string propertyName, IEdmEntityType owningEntityType, ODataMessageReaderSettings messageReaderSettings)
		{
			if (owningEntityType == null)
			{
				return null;
			}
			IEdmProperty edmProperty = ReaderValidationUtils.ValidateLinkPropertyDefined(propertyName, owningEntityType, messageReaderSettings);
			if (edmProperty == null)
			{
				if (owningEntityType.IsOpen && !messageReaderSettings.ReportUndeclaredLinkProperties)
				{
					throw new ODataException(Strings.ValidationUtils_OpenNavigationProperty(propertyName, owningEntityType.FullTypeName()));
				}
			}
			else if (edmProperty.PropertyKind != EdmPropertyKind.Navigation)
			{
				throw new ODataException(Strings.ValidationUtils_NavigationPropertyExpected(propertyName, owningEntityType.FullTypeName(), edmProperty.PropertyKind.ToString()));
			}
			return (IEdmNavigationProperty)edmProperty;
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x00037C89 File Offset: 0x00035E89
		internal static ODataException GetPrimitiveTypeConversionException(IEdmPrimitiveTypeReference targetTypeReference, Exception innerException, string stringValue)
		{
			return new ODataException(Strings.ReaderValidationUtils_CannotConvertPrimitiveValue(stringValue, targetTypeReference.FullName()), innerException);
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x00037CA0 File Offset: 0x00035EA0
		internal static IEdmType ResolvePayloadTypeName(IEdmModel model, IEdmTypeReference expectedTypeReference, string payloadTypeName, EdmTypeKind expectedTypeKind, ODataReaderBehavior readerBehavior, out EdmTypeKind payloadTypeKind)
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
			IEdmType edmType = MetadataUtils.ResolveTypeNameForRead(model, (expectedTypeReference == null) ? null : expectedTypeReference.Definition, payloadTypeName, readerBehavior, out payloadTypeKind);
			if (payloadTypeKind == EdmTypeKind.None)
			{
				payloadTypeKind = expectedTypeKind;
			}
			return edmType;
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x00037CE8 File Offset: 0x00035EE8
		internal static IEdmTypeReference ResolvePayloadTypeNameAndComputeTargetType(EdmTypeKind expectedTypeKind, IEdmType defaultPrimitivePayloadType, IEdmTypeReference expectedTypeReference, string payloadTypeName, IEdmModel model, ODataMessageReaderSettings messageReaderSettings, Func<EdmTypeKind> typeKindFromPayloadFunc, out EdmTypeKind targetTypeKind, out SerializationTypeNameAnnotation serializationTypeNameAnnotation)
		{
			serializationTypeNameAnnotation = null;
			EdmTypeKind edmTypeKind;
			IEdmType edmType = ReaderValidationUtils.ResolvePayloadTypeName(model, expectedTypeReference, payloadTypeName, EdmTypeKind.Complex, messageReaderSettings.ReaderBehavior, out edmTypeKind);
			targetTypeKind = ReaderValidationUtils.ComputeTargetTypeKind(expectedTypeReference, expectedTypeKind == EdmTypeKind.Entity, payloadTypeName, edmTypeKind, messageReaderSettings, typeKindFromPayloadFunc);
			IEdmTypeReference edmTypeReference;
			if (targetTypeKind == EdmTypeKind.Primitive)
			{
				edmTypeReference = ReaderValidationUtils.ResolveAndValidatePrimitiveTargetType(expectedTypeReference, edmTypeKind, edmType, payloadTypeName, defaultPrimitivePayloadType, model, messageReaderSettings);
			}
			else
			{
				edmTypeReference = ReaderValidationUtils.ResolveAndValidateNonPrimitiveTargetType(targetTypeKind, expectedTypeReference, edmTypeKind, edmType, payloadTypeName, model, messageReaderSettings);
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

		// Token: 0x06001028 RID: 4136 RVA: 0x00037D68 File Offset: 0x00035F68
		internal static IEdmTypeReference ResolveAndValidatePrimitiveTargetType(IEdmTypeReference expectedTypeReference, EdmTypeKind payloadTypeKind, IEdmType payloadType, string payloadTypeName, IEdmType defaultPayloadType, IEdmModel model, ODataMessageReaderSettings messageReaderSettings)
		{
			bool flag = messageReaderSettings.ReaderBehavior.TypeResolver != null && payloadType != null;
			if (expectedTypeReference != null && !flag)
			{
				ReaderValidationUtils.ValidateTypeSupported(expectedTypeReference);
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
			if (payloadType != null && !MetadataUtilsCommon.CanConvertPrimitiveTypeTo(null, (IEdmPrimitiveType)payloadType.AsActualType(), (IEdmPrimitiveType)expectedTypeReference.Definition))
			{
				throw new ODataException(Strings.ValidationUtils_IncompatibleType(payloadTypeName, expectedTypeReference.FullName()));
			}
			return expectedTypeReference;
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x00037E28 File Offset: 0x00036028
		internal static IEdmTypeReference ResolveAndValidateNonPrimitiveTargetType(EdmTypeKind expectedTypeKind, IEdmTypeReference expectedTypeReference, EdmTypeKind payloadTypeKind, IEdmType payloadType, string payloadTypeName, IEdmModel model, ODataMessageReaderSettings messageReaderSettings)
		{
			bool flag = messageReaderSettings.ReaderBehavior.TypeResolver != null && payloadType != null;
			if (!flag)
			{
				ReaderValidationUtils.ValidateTypeSupported(expectedTypeReference);
				if (model.IsUserModel() && (expectedTypeReference == null || !messageReaderSettings.DisableStrictMetadataValidation))
				{
					ReaderValidationUtils.VerifyPayloadTypeDefined(payloadTypeName, payloadType);
				}
			}
			else
			{
				ReaderValidationUtils.ValidateTypeSupported((payloadType == null) ? null : payloadType.ToTypeReference(true));
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
				return ReaderValidationUtils.ResolveAndValidateTargetTypeWithNoExpectedType(expectedTypeKind, payloadType);
			}
			if (messageReaderSettings.DisableStrictMetadataValidation)
			{
				return ReaderValidationUtils.ResolveAndValidateTargetTypeStrictValidationDisabled(expectedTypeKind, expectedTypeReference, payloadType);
			}
			return ReaderValidationUtils.ResolveAndValidateTargetTypeStrictValidationEnabled(expectedTypeKind, expectedTypeReference, payloadType);
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x00037ED3 File Offset: 0x000360D3
		internal static void ValidateEncodingSupportedInBatch(Encoding encoding)
		{
			if (!encoding.IsSingleByte && Encoding.UTF8.CodePage != encoding.CodePage)
			{
				throw new ODataException(Strings.ODataBatchReaderStream_MultiByteEncodingsNotSupported(encoding.WebName));
			}
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x00037F00 File Offset: 0x00036100
		internal static void ValidateEncodingSupportedInAsync(Encoding encoding)
		{
			if (!encoding.IsSingleByte && Encoding.UTF8.CodePage != encoding.CodePage)
			{
				throw new ODataException(Strings.ODataAsyncReader_MultiByteEncodingsNotSupported(encoding.WebName));
			}
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x00037F2D File Offset: 0x0003612D
		internal static void ValidateTypeSupported(IEdmTypeReference typeReference)
		{
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x00037F34 File Offset: 0x00036134
		internal static void ValidateFeedOrEntryContextUri(ODataJsonLightContextUriParseResult contextUriParseResult, ODataReaderCore.Scope scope, bool updateScope)
		{
			if (contextUriParseResult.EdmType is IEdmCollectionType)
			{
				ReaderValidationUtils.ValidateFeedContextUri(contextUriParseResult, scope, updateScope);
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
			IEdmEntityType edmEntityType = (IEdmEntityType)contextUriParseResult.EdmType;
			if (scope.EntityType == null)
			{
				if (updateScope)
				{
					scope.EntityType = edmEntityType;
					return;
				}
			}
			else if (scope.EntityType.IsAssignableFrom(edmEntityType))
			{
				if (updateScope)
				{
					scope.EntityType = edmEntityType;
					return;
				}
			}
			else if (!edmEntityType.IsAssignableFrom(scope.EntityType))
			{
				throw new ODataException(Strings.ReaderValidationUtils_ContextUriValidationInvalidExpectedEntityType(UriUtils.UriToString(contextUriParseResult.ContextUri), contextUriParseResult.EdmType.FullTypeName(), scope.EntityType.FullName()));
			}
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x00038034 File Offset: 0x00036234
		internal static IEdmTypeReference ValidateCollectionContextUriAndGetPayloadItemTypeReference(ODataJsonLightContextUriParseResult contextUriParseResult, IEdmTypeReference expectedItemTypeReference)
		{
			if (contextUriParseResult == null || contextUriParseResult.EdmType == null)
			{
				return expectedItemTypeReference;
			}
			IEdmCollectionType edmCollectionType = (IEdmCollectionType)contextUriParseResult.EdmType;
			if (expectedItemTypeReference != null && !expectedItemTypeReference.IsAssignableFrom(edmCollectionType.ElementType))
			{
				throw new ODataException(Strings.ReaderValidationUtils_ContextUriDoesNotReferTypeAssignableToExpectedType(UriUtils.UriToString(contextUriParseResult.ContextUri), edmCollectionType.ElementType.FullName(), expectedItemTypeReference.FullName()));
			}
			return edmCollectionType.ElementType;
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x00038098 File Offset: 0x00036298
		internal static void ValidateOperationProperty(object propertyValue, string propertyName, string metadata, string operationsHeader)
		{
			if (propertyValue == null)
			{
				throw new ODataException(Strings.ODataJsonOperationsDeserializerUtils_OperationPropertyCannotBeNull(propertyName, metadata, operationsHeader));
			}
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x000380AC File Offset: 0x000362AC
		private static IEdmTypeReference ResolveAndValidateTargetTypeWithNoExpectedType(EdmTypeKind expectedTypeKind, IEdmType payloadType)
		{
			if (payloadType != null)
			{
				return payloadType.ToTypeReference(true);
			}
			if (expectedTypeKind == EdmTypeKind.Entity)
			{
				throw new ODataException(Strings.ReaderValidationUtils_EntryWithoutType);
			}
			return null;
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x000380D8 File Offset: 0x000362D8
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
				if (payloadType == null || expectedTypeKind != payloadType.TypeKind)
				{
					return expectedTypeReference;
				}
				ReaderValidationUtils.VerifyComplexType(expectedTypeReference, payloadType, false);
				if (expectedTypeReference.AsComplex().ComplexDefinition().IsAssignableFrom((IEdmComplexType)payloadType))
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
				return expectedTypeReference;
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ReaderValidationUtils_ResolveAndValidateTypeName_Strict_TypeKind));
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x00038198 File Offset: 0x00036398
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
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ReaderValidationUtils_ResolveAndValidateTypeName_Strict_TypeKind));
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x000382A0 File Offset: 0x000364A0
		private static void VerifyPayloadTypeDefined(string payloadTypeName, IEdmType payloadType)
		{
			if (payloadTypeName != null && payloadType == null)
			{
				throw new ODataException(Strings.ValidationUtils_UnrecognizedTypeName(payloadTypeName));
			}
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x000382B4 File Offset: 0x000364B4
		private static void VerifyComplexType(IEdmTypeReference expectedTypeReference, IEdmType payloadType, bool failIfNotRelated)
		{
			IEdmStructuredType edmStructuredType = expectedTypeReference.AsStructured().StructuredDefinition();
			IEdmStructuredType edmStructuredType2 = (IEdmStructuredType)payloadType;
			if (!edmStructuredType.IsAssignableFrom(edmStructuredType2) && failIfNotRelated)
			{
				throw new ODataException(Strings.ValidationUtils_IncompatibleType(edmStructuredType2.FullTypeName(), edmStructuredType.FullTypeName()));
			}
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x000382F8 File Offset: 0x000364F8
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

		// Token: 0x06001036 RID: 4150 RVA: 0x0003834C File Offset: 0x0003654C
		private static SerializationTypeNameAnnotation CreateSerializationTypeNameAnnotation(string payloadTypeName, IEdmTypeReference targetTypeReference)
		{
			if (payloadTypeName != null && string.CompareOrdinal(payloadTypeName, targetTypeReference.FullName()) != 0)
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

		// Token: 0x06001037 RID: 4151 RVA: 0x0003838C File Offset: 0x0003658C
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

		// Token: 0x06001038 RID: 4152 RVA: 0x000383FC File Offset: 0x000365FC
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

		// Token: 0x06001039 RID: 4153 RVA: 0x00038438 File Offset: 0x00036638
		private static bool ShouldValidatePayloadTypeKind(ODataMessageReaderSettings messageReaderSettings, IEdmTypeReference expectedValueTypeReference, EdmTypeKind payloadTypeKind)
		{
			bool flag = messageReaderSettings.ReaderBehavior.TypeResolver != null && payloadTypeKind != EdmTypeKind.None;
			return expectedValueTypeReference != null && (!messageReaderSettings.DisableStrictMetadataValidation || flag || (expectedValueTypeReference.IsODataPrimitiveTypeKind() && messageReaderSettings.DisablePrimitiveTypeConversion));
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x00038480 File Offset: 0x00036680
		private static void ValidateNullValueAllowed(IEdmTypeReference expectedValueTypeReference, bool validateNullValue, IEdmModel model, string propertyName, bool? isDynamicProperty)
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
				else if (expectedValueTypeReference.IsODataComplexTypeKind() && ValidationUtils.ShouldValidateComplexPropertyNullValue(model))
				{
					IEdmComplexTypeReference edmComplexTypeReference = expectedValueTypeReference.AsComplex();
					if (!edmComplexTypeReference.IsNullable)
					{
						ReaderValidationUtils.ThrowNullValueForNonNullableTypeException(expectedValueTypeReference, propertyName);
					}
				}
			}
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x0003851A File Offset: 0x0003671A
		private static void ThrowNullValueForNonNullableTypeException(IEdmTypeReference expectedValueTypeReference, string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				throw new ODataException(Strings.ReaderValidationUtils_NullValueForNonNullableType(expectedValueTypeReference.FullName()));
			}
			throw new ODataException(Strings.ReaderValidationUtils_NullNamedValueForNonNullableType(propertyName, expectedValueTypeReference.FullName()));
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x00038546 File Offset: 0x00036746
		private static void ValidateFeedContextUri(ODataJsonLightContextUriParseResult contextUriParseResult, ODataReaderCore.Scope scope, bool updateScope)
		{
		}
	}
}
